Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class LoginFormLogic

    Public Class UserPermissionData
        Public Property FormClass As String
        Public Property CanView As Boolean
        Public Property CanAdd As Boolean
        Public Property CanEdit As Boolean
        Public Property CanDelete As Boolean
    End Class

    Public Class AuthenticationResult
        Public Property IsSuccess As Boolean
        Public Property UserId As Integer
        Public Property FirstName As String
        Public Property LastName As String
        Public Property Username As String
        Public Property Role As String
        Public Property ErrorMessage As String
        Public Property AccessibleForms As List(Of String)
        Public Property Permissions As Dictionary(Of String, UserPermissionData)

        Public Sub New()
            IsSuccess = False
            UserId = -1
            FirstName = ""
            LastName = ""
            Username = ""
            Role = ""
            ErrorMessage = ""
            AccessibleForms = New List(Of String)()
            Permissions = New Dictionary(Of String, UserPermissionData)()
        End Sub
    End Class

    Public Function AuthenticateUser(username As String, password As String) As AuthenticationResult
        Dim result As New AuthenticationResult()
        Dim connection As MySqlConnection = Nothing

        Try
            If String.IsNullOrWhiteSpace(username) Then
                result.ErrorMessage = "Please enter your username."
                Return result
            End If

            If String.IsNullOrWhiteSpace(password) Then
                result.ErrorMessage = "Please enter your password."
                Return result
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.ErrorMessage = "Unable to connect to database."
                Return result
            End If

            Dim userId As Integer = -1
            Dim firstName As String = ""
            Dim lastName As String = ""
            Dim storedPassword As String = ""
            Dim userRole As String = ""
            Dim isActive As Boolean = False
            Dim userFound As Boolean = False

            Dim query As String =
                "SELECT UserId, FirstName, LastName, Username, Password, Role, IsActive " &
                "FROM UserAccounts " &
                "WHERE Username = @Username " &
                "LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.CommandTimeout = 30

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        userFound = True
                        userId = CInt(reader("UserId"))
                        firstName = reader("FirstName").ToString()
                        lastName = reader("LastName").ToString()
                        storedPassword = reader("Password").ToString()
                        userRole = reader("Role").ToString()
                        isActive = CBool(reader("IsActive"))
                    End If
                End Using
            End Using

            ' ── FAILURE: User does not exist ─────────────────────────
            If Not userFound Then
                GlobalAuditLogger.LogLogin(
    "LoginForm",
    "LOGIN FAILED",
    "Failed login attempt for user: " & username &
    " — Reason: Invalid credentials.",
    0,
    username)
                result.ErrorMessage = "Invalid username or password."
                Return result
            End If

            ' ── FAILURE: Account is inactive ─────────────────────────
            If Not isActive Then
                GlobalAuditLogger.LogLogin(
    "LoginForm",
    "LOGIN FAILED",
    "Failed login attempt for user: " & username &
    " — Reason: Account inactive.",
    userId,
    username)
                result.ErrorMessage = "Your account is inactive. Please contact the administrator."
                Return result
            End If

            ' ── FAILURE: Password does not match ─────────────────────
            If Not VerifyPassword(password, storedPassword) Then
                GlobalAuditLogger.LogLogin(
    "LoginForm",
    "LOGIN FAILED",
    "Failed login attempt for user: " & username &
    " — Reason: Invalid password.",
    userId,
    username)
                result.ErrorMessage = "Invalid username or password."
                Return result
            End If

            ' ── SUCCESS ───────────────────────────────────────────────
            result.IsSuccess = True
            result.UserId = userId
            result.FirstName = firstName
            result.LastName = lastName
            result.Username = username
            result.Role = userRole

            result.AccessibleForms = LoadUserPermissions(userId, userRole)
            result.Permissions = LoadUserPermissionDetails(userId, userRole)

            MsgBox("Welcome, " & username & "!", MsgBoxStyle.Information, "Login Successful")

            GlobalAuditLogger.LogLogin(
    "LoginForm",
    "LOGIN SUCCESS",
    username & " logged in successfully.",
    userId,
    username)
            Return result

        Catch ex As MySqlException
            ' ── EXCEPTION: MySQL-specific database error ──────────────
            result.ErrorMessage = "Database error: " & ex.Message
            Debug.WriteLine("Database error in AuthenticateUser: " & ex.Message)
            GlobalAuditLogger.Log("LoginForm", "DATABASE ERROR",
                "Login transaction failed: " & ex.Message)
            Return result

        Catch ex As Exception
            ' ── EXCEPTION: Any unexpected runtime error ───────────────
            result.ErrorMessage = "An error occurred: " & ex.Message
            Debug.WriteLine("Error in AuthenticateUser: " & ex.Message)
            GlobalAuditLogger.Log("LoginForm", "DATABASE ERROR",
                "Login transaction failed: " & ex.Message)
            Return result

        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    Private Function VerifyPassword(inputPassword As String, storedPassword As String) As Boolean
        If inputPassword = storedPassword Then
            Return True
        End If

        Dim md5Hash As String = GetMD5Hash(inputPassword)
        If md5Hash.Equals(storedPassword, StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        Dim sha1Hash As String = GetSHA1Hash(inputPassword)
        If sha1Hash.Equals(storedPassword, StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        Return False
    End Function

    Private Function LoadUserPermissions(userId As Integer, userRole As String) As List(Of String)
        Dim accessibleForms As New List(Of String)()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return accessibleForms

            If userRole.ToLower() = "super admin" Then
                Dim query As String =
                    "SELECT FormClass " &
                    "FROM Forms " &
                    "WHERE IsActive = 1"

                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            accessibleForms.Add(reader("FormClass").ToString())
                        End While
                    End Using
                End Using
            Else
                Dim query As String =
                    "SELECT f.FormClass " &
                    "FROM UserPermissions up " &
                    "INNER JOIN Forms f ON up.FormID = f.FormID " &
                    "WHERE up.UserId = @UserId " &
                    "AND up.CanView = 1 " &
                    "AND f.IsActive = 1"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@UserId", userId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            accessibleForms.Add(reader("FormClass").ToString())
                        End While
                    End Using
                End Using
            End If

        Catch ex As Exception
            Debug.WriteLine("LoadUserPermissions Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return accessibleForms
    End Function

    Private Function LoadUserPermissionDetails(userId As Integer, userRole As String) As Dictionary(Of String, UserPermissionData)
        Dim permissions As New Dictionary(Of String, UserPermissionData)()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return permissions

            If userRole.ToLower() = "super admin" Then
                Dim query As String =
                    "SELECT FormClass " &
                    "FROM Forms " &
                    "WHERE IsActive = 1"

                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim formClass As String = reader("FormClass").ToString()

                            permissions(formClass) = New UserPermissionData With {
                                .FormClass = formClass,
                                .CanView = True,
                                .CanAdd = True,
                                .CanEdit = True,
                                .CanDelete = True
                            }
                        End While
                    End Using
                End Using
            Else
                Dim query As String =
                    "SELECT f.FormClass, up.CanView, up.CanAdd, up.CanEdit, up.CanDelete " &
                    "FROM UserPermissions up " &
                    "INNER JOIN Forms f ON up.FormID = f.FormID " &
                    "WHERE up.UserId = @UserId " &
                    "AND f.IsActive = 1"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@UserId", userId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim formClass As String = reader("FormClass").ToString()

                            permissions(formClass) = New UserPermissionData With {
                                .FormClass = formClass,
                                .CanView = CBool(reader("CanView")),
                                .CanAdd = CBool(reader("CanAdd")),
                                .CanEdit = CBool(reader("CanEdit")),
                                .CanDelete = CBool(reader("CanDelete"))
                            }
                        End While
                    End Using
                End Using
            End If

        Catch ex As Exception
            Debug.WriteLine("LoadUserPermissionDetails Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return permissions
    End Function

    Private Function GetMD5Hash(input As String) As String
        Using md5 As New MD5CryptoServiceProvider()
            Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(input)
            Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)
            Dim hashString As New StringBuilder()

            For Each b In hashBytes
                hashString.Append(b.ToString("X2"))
            Next

            Return hashString.ToString()
        End Using
    End Function

    Private Function GetSHA1Hash(input As String) As String
        Using sha1 As New SHA1CryptoServiceProvider()
            Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(input)
            Dim hashBytes As Byte() = sha1.ComputeHash(inputBytes)
            Dim hashString As New StringBuilder()

            For Each b In hashBytes
                hashString.Append(b.ToString("X2"))
            Next

            Return hashString.ToString()
        End Using
    End Function

    ' ── LogSuccessfulLogin and LogFailedLogin private wrapper Subs have
    '    been removed. All audit calls are now inline direct calls to
    '    GlobalAuditLogger inside AuthenticateUser, matching the
    '    convention used by every other logic file in the system.
    ' ────────────────────────────────────────────────────────────────

End Class
