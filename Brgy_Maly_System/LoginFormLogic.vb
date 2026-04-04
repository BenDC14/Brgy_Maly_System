Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

''' <summary>
''' Login Form Logic - Handles all login-related business logic
''' Separated from UI form logic for reusability and maintainability
''' </summary>
Public Class LoginFormLogic

    ''' <summary>
    ''' Result class for authentication attempts
    ''' </summary>
    Public Class AuthenticationResult
        Public Property IsSuccess As Boolean
        Public Property UserId As Integer
        Public Property FirstName As String
        Public Property LastName As String
        Public Property Username As String
        Public Property Role As String
        Public Property ErrorMessage As String
        Public Property AccessibleForms As List(Of String)

        Public Sub New()
            AccessibleForms = New List(Of String)
        End Sub
    End Class

    ''' <summary>
    ''' Authenticate user from database
    ''' Returns AuthenticationResult with all necessary information
    ''' </summary>
    Public Function AuthenticateUser(username As String, password As String) As AuthenticationResult
        Dim result As New AuthenticationResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATION ===
            If String.IsNullOrWhiteSpace(username) Then
                result.ErrorMessage = "Please enter your username."
                Return result
            End If

            If String.IsNullOrWhiteSpace(password) Then
                result.ErrorMessage = "Please enter your password."
                Return result
            End If

            ' === GET DATABASE CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.ErrorMessage = "Unable to connect to database."
                Return result
            End If

            ' === QUERY USER FROM DATABASE ===
            Dim query As String = "SELECT UserId, FirstName, LastName, Username, Password, Role, IsActive FROM UserAccounts WHERE Username = @Username LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.CommandTimeout = 30

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' === USER FOUND ===
                        Dim userId As Integer = CInt(reader("UserId"))
                        Dim firstName As String = reader("FirstName").ToString()
                        Dim lastName As String = reader("LastName").ToString()
                        Dim storedPassword As String = reader("Password").ToString()
                        Dim userRole As String = reader("Role").ToString()
                        Dim isActive As Boolean = CBool(reader("IsActive"))

                        ' === CHECK IF ACCOUNT IS ACTIVE ===
                        If Not isActive Then
                            result.ErrorMessage = "Your account is inactive. Please contact the administrator."
                            LogFailedLogin(username, "Account Inactive")
                            Return result
                        End If

                        ' === VERIFY PASSWORD ===
                        If VerifyPassword(password, storedPassword) Then
                            ' === PASSWORD CORRECT ===
                            result.IsSuccess = True
                            result.UserId = userId
                            result.FirstName = firstName
                            result.LastName = lastName
                            result.Username = username
                            result.Role = userRole


                            ' === LOAD USER PERMISSIONS ===
                            result.AccessibleForms = LoadUserPermissions(userId, userRole)
                            MsgBox("Welcome, " & username & "!", MsgBoxStyle.Information, "Login Successful")

                            ' === LOG SUCCESSFUL LOGIN ===
                            LogSuccessfulLogin(userId, username)

                            Return result
                        Else
                            ' === PASSWORD INCORRECT ===
                            result.ErrorMessage = "Invalid username or password."
                            LogFailedLogin(username, "Invalid Password")
                            Return result
                        End If
                    Else
                        ' === USER NOT FOUND ===
                        result.ErrorMessage = "Invalid username or password."
                        LogFailedLogin(username, "User Not Found")
                        Return result
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            result.ErrorMessage = "Database error: " & ex.Message
            Debug.WriteLine("Database error in AuthenticateUser: " & ex.Message)
            Return result
        Catch ex As Exception
            result.ErrorMessage = "An error occurred: " & ex.Message
            Debug.WriteLine("Error in AuthenticateUser: " & ex.Message)
            Return result
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Verify password against stored encrypted password
    ''' Supports multiple encryption methods (plain text, MD5, SHA1)
    ''' </summary>
    Private Function VerifyPassword(inputPassword As String, storedPassword As String) As Boolean
        ' === METHOD 1: Direct comparison (plain text) ===
        If inputPassword = storedPassword Then
            Return True
        End If

        ' === METHOD 2: MD5 (PhpMyAdmin default) ===
        Dim md5Hash As String = GetMD5Hash(inputPassword)
        If md5Hash.Equals(storedPassword, StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        ' === METHOD 3: SHA1 ===
        Dim sha1Hash As String = GetSHA1Hash(inputPassword)
        If sha1Hash.Equals(storedPassword, StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' Load user permissions from UserPermissions table
    ''' For Super Admin: Returns all forms
    ''' For Admin: Returns only assigned forms
    ''' </summary>
    Private Function LoadUserPermissions(userId As Integer, userRole As String) As List(Of String)
        Dim accessibleForms As New List(Of String)
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return accessibleForms

            If userRole.ToLower() = "super admin" Then
                ' === SUPER ADMIN: Access to all forms ===
                Dim query As String = "SELECT FormClass FROM Forms WHERE IsActive = 1"
                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            accessibleForms.Add(reader("FormClass").ToString())
                        End While
                    End Using
                End Using
            Else
                ' === ADMIN: Load only assigned forms ===
                Dim query As String = "SELECT f.FormClass FROM UserPermissions up " &
                                     "INNER JOIN Forms f ON up.FormID = f.FormID " &
                                     "WHERE up.UserId = @UserId AND up.CanView = 1 AND f.IsActive = 1"
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
            Debug.WriteLine("Error loading permissions: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return accessibleForms
    End Function

    ''' <summary>
    ''' Generate MD5 hash (PhpMyAdmin default)
    ''' </summary>
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

    ''' <summary>
    ''' Generate SHA1 hash (alternative encryption method)
    ''' </summary>
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

    ''' <summary>
    ''' Log successful login attempt
    ''' </summary>
    Private Sub LogSuccessfulLogin(userId As Integer, username As String)
        ' TODO: Implement if needed
        ' INSERT INTO AuditTrail (UserId, Username, Form, Action, Description, Timestamp)
        ' VALUES (@UserId, @Username, 'Login', 'LOGIN_SUCCESS', 'User logged in successfully', NOW())
    End Sub

    ''' <summary>
    ''' Log failed login attempts
    ''' </summary>
    Private Sub LogFailedLogin(username As String, reason As String)
        ' TODO: Implement if needed
        ' INSERT INTO AuditTrail (UserId, Username, Form, Action, Description, Timestamp)
        ' VALUES (-1, @Username, 'Login', 'LOGIN_FAILED', @Reason, NOW())
    End Sub

End Class
