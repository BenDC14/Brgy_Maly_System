Imports System.Collections.Generic
Imports System.Transactions
Imports MySql.Data.MySqlClient

Public Class ManageAllAccountsLogic

    Public Class FormPermissionData
        Public Property FormID As Integer
        Public Property CanView As Boolean
        Public Property CanAdd As Boolean
        Public Property CanEdit As Boolean
        Public Property CanDelete As Boolean
    End Class

    Public Function GetAllAdminAccounts() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT UserId, FirstName, LastName, Username, Role, IsActive, CreatedAt " &
                "FROM UserAccounts " &
                "WHERE Role = 'Admin' " &
                "ORDER BY FirstName"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAllAdminAccounts Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function SearchAdminAccounts(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT UserId, FirstName, LastName, Username, Role, IsActive, CreatedAt " &
                "FROM UserAccounts " &
                "WHERE Role = 'Admin' " &
                "AND (FirstName LIKE @Search OR LastName LIKE @Search OR Username LIKE @Search) " &
                "ORDER BY FirstName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Search", "%" & searchTerm & "%")

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("SearchAdminAccounts Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function ValidateAccountData(firstName As String, lastName As String, username As String, password As String, role As String, isAddMode As Boolean) As String
        If String.IsNullOrWhiteSpace(firstName) Then
            Return "Please enter first name."
        End If

        If String.IsNullOrWhiteSpace(lastName) Then
            Return "Please enter last name."
        End If

        If String.IsNullOrWhiteSpace(username) Then
            Return "Please enter username."
        End If

        If isAddMode AndAlso String.IsNullOrWhiteSpace(password) Then
            Return "Please enter password."
        End If

        If String.IsNullOrWhiteSpace(role) Then
            Return "Please select a role."
        End If

        Return ""
    End Function

    Public Function GetAllAvailableForms() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT FormID, FormName, FormClass " &
                "FROM Forms " &
                "WHERE IsActive = 1 " &
                "ORDER BY FormName"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAllAvailableForms Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function GetUserPermissionRows(userId As Integer) As Dictionary(Of Integer, FormPermissionData)
        Dim permissions As New Dictionary(Of Integer, FormPermissionData)
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return permissions

            Dim query As String =
                "SELECT FormID, CanView, CanAdd, CanEdit, CanDelete " &
                "FROM UserPermissions " &
                "WHERE UserId = @UserId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@UserId", userId)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim permission As New FormPermissionData With {
                            .FormID = CInt(reader("FormID")),
                            .CanView = CBool(reader("CanView")),
                            .CanAdd = CBool(reader("CanAdd")),
                            .CanEdit = CBool(reader("CanEdit")),
                            .CanDelete = CBool(reader("CanDelete"))
                        }

                        permissions(permission.FormID) = permission
                    End While
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetUserPermissionRows Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return permissions
    End Function

    Public Function AddAdminAccount(firstName As String, lastName As String, username As String, password As String, role As String) As Integer
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1

            Dim checkQuery As String = "SELECT COUNT(*) FROM UserAccounts WHERE Username = @Username"

            Using checkCmd As New MySqlCommand(checkQuery, connection)
                checkCmd.Parameters.AddWithValue("@Username", username)

                If CInt(checkCmd.ExecuteScalar()) > 0 Then
                    Return -1
                End If
            End Using

            Dim hashedPassword As String = HashPassword(password)

            Dim query As String =
                "INSERT INTO UserAccounts (FirstName, LastName, Username, Password, Role, IsActive, CreatedAt) " &
                "VALUES (@FirstName, @LastName, @Username, @Password, @Role, 1, NOW()); " &
                "SELECT LAST_INSERT_ID();"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Password", hashedPassword)
                cmd.Parameters.AddWithValue("@Role", role)

                Dim result = cmd.ExecuteScalar()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("ManageAllAccounts_Form", "ADD USER ACCOUNT",
                LogInForm.CurrentUsername & " created new user account for: " & username & " (Role: " & role & ")")

                If result IsNot Nothing Then
                    Return CInt(result)
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("AddAdminAccount Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return -1
    End Function

    Public Function UpdateAdminAccount(userId As Integer, firstName As String, lastName As String, username As String, password As String, role As String) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim checkQuery As String =
                "SELECT COUNT(*) FROM UserAccounts " &
                "WHERE Username = @Username AND UserId <> @UserId"

            Using checkCmd As New MySqlCommand(checkQuery, connection)
                checkCmd.Parameters.AddWithValue("@Username", username)
                checkCmd.Parameters.AddWithValue("@UserId", userId)

                If CInt(checkCmd.ExecuteScalar()) > 0 Then
                    Return False
                End If
            End Using

            Dim query As String =
                "UPDATE UserAccounts SET " &
                "FirstName = @FirstName, " &
                "LastName = @LastName, " &
                "Username = @Username, " &
                "Role = @Role"

            If Not String.IsNullOrWhiteSpace(password) Then
                query &= ", Password = @Password"
            End If

            query &= ", UpdatedAt = NOW() WHERE UserId = @UserId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Role", role)

                If Not String.IsNullOrWhiteSpace(password) Then
                    cmd.Parameters.AddWithValue("@Password", HashPassword(password))
                End If

                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.ExecuteNonQuery()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("ManageAllAccounts_Form", "UPDATE USER ACCOUNT",
                    LogInForm.CurrentUsername & " updated user account (ID: " & userId & "): " & username)

                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("UpdateAdminAccount Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    Public Function ArchiveAccount(userId As Integer) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim query As String =
                "UPDATE UserAccounts " &
                "SET IsActive = 0, UpdatedAt = NOW() " &
                "WHERE UserId = @UserId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.ExecuteNonQuery()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("ManageAllAccounts_Form", "ARCHIVE USER ACCOUNT",
                LogInForm.CurrentUsername & " archived/deactivated user account (ID: " & userId & ")")
                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("ArchiveAccount Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    Public Function AssignFormPermissionsToUser(userId As Integer, permissions As List(Of FormPermissionData), assignedBy As Integer) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim deleteQuery As String = "DELETE FROM UserPermissions WHERE UserId = @UserId"

            Using deleteCmd As New MySqlCommand(deleteQuery, connection)
                deleteCmd.Parameters.AddWithValue("@UserId", userId)
                deleteCmd.ExecuteNonQuery()
            End Using

            For Each permission In permissions
                If permission.CanAdd OrElse permission.CanEdit OrElse permission.CanDelete Then
                    permission.CanView = True
                End If

                Dim insertQuery As String =
                    "INSERT INTO UserPermissions " &
                    "(UserId, FormID, CanView, CanAdd, CanEdit, CanDelete, AssignedBy, AssignedAt) " &
                    "VALUES " &
                    "(@UserId, @FormID, @CanView, @CanAdd, @CanEdit, @CanDelete, @AssignedBy, NOW())"

                Using insertCmd As New MySqlCommand(insertQuery, connection)
                    insertCmd.Parameters.AddWithValue("@UserId", userId)
                    insertCmd.Parameters.AddWithValue("@FormID", permission.FormID)
                    insertCmd.Parameters.AddWithValue("@CanView", If(permission.CanView, 1, 0))
                    insertCmd.Parameters.AddWithValue("@CanAdd", If(permission.CanAdd, 1, 0))
                    insertCmd.Parameters.AddWithValue("@CanEdit", If(permission.CanEdit, 1, 0))
                    insertCmd.Parameters.AddWithValue("@CanDelete", If(permission.CanDelete, 1, 0))
                    insertCmd.Parameters.AddWithValue("@AssignedBy", assignedBy)

                    insertCmd.ExecuteNonQuery()
                End Using
            Next

            Return True

        Catch ex As Exception
            Debug.WriteLine("AssignFormPermissionsToUser Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    Private Function HashPassword(password As String) As String
        Using md5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
            Dim inputBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(password)
            Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)
            Dim hashString As New System.Text.StringBuilder()

            For Each b In hashBytes
                hashString.Append(b.ToString("X2"))
            Next

            Return hashString.ToString()
        End Using
    End Function

End Class
