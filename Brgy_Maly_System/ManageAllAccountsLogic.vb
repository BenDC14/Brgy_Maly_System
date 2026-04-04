Imports MySql.Data.MySqlClient
Imports System.Collections.Generic

''' <summary>
''' Manage All Accounts Logic - Handles all business logic for account management
''' Separated from UI form logic for reusability and maintainability
''' </summary>
Public Class ManageAllAccountsLogic

    ''' <summary>
    ''' Get all Admin accounts from database
    ''' </summary>
    Public Function GetAllAdminAccounts() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT UserId, FirstName, LastName, Username, Role, IsActive, CreatedAt FROM UserAccounts WHERE Role = 'Admin' ORDER BY FirstName"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting admin accounts: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Search admin accounts by username or name
    ''' </summary>
    Public Function SearchAdminAccounts(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT UserId, FirstName, LastName, Username, Role, IsActive, CreatedAt FROM UserAccounts " &
                                 "WHERE Role = 'Admin' AND (FirstName LIKE @Search OR LastName LIKE @Search OR Username LIKE @Search) " &
                                 "ORDER BY FirstName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Search", "%" & searchTerm & "%")
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error searching accounts: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Validate account information before adding or updating
    ''' </summary>
    Public Function ValidateAccountData(firstName As String, lastName As String, username As String, password As String, role As String, isAddMode As Boolean) As String
        If String.IsNullOrWhiteSpace(firstName) Then
            Return "Please enter first name."
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

        Return "" ' Empty string = valid
    End Function

    ''' <summary>
    ''' Get all available forms for assignment
    ''' </summary>
    Public Function GetAllAvailableForms() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT FormID, FormName, FormClass FROM Forms WHERE IsActive = 1 ORDER BY FormName"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting available forms: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Convert checkbox selections to FormID list
    ''' </summary>
    Public Function GetSelectedFormIds(cbResident As Boolean, cbHousehold As Boolean, cbAyuda As Boolean, cbReports As Boolean, allFormsData As DataTable) As List(Of Integer)
        Dim selectedFormIds As New List(Of Integer)

        If allFormsData Is Nothing Then Return selectedFormIds

        For Each row As DataRow In allFormsData.Rows
            Dim formClass As String = row("FormClass").ToString()
            Dim formId As Integer = CInt(row("FormID"))

            If (cbResident AndAlso formClass.Contains("Resident")) OrElse
               (cbHousehold AndAlso formClass.Contains("Household")) OrElse
               (cbAyuda AndAlso formClass.Contains("Ayuda")) OrElse
               (cbReports AndAlso formClass.Contains("Reports")) Then
                selectedFormIds.Add(formId)
            End If
        Next

        Return selectedFormIds
    End Function

    ''' <summary>
    ''' Load assigned forms for a user (returns as checkbox states)
    ''' </summary>
    Public Function GetAssignedFormCheckStates(userId As Integer, allFormsData As DataTable) As Dictionary(Of String, Boolean)
        Dim states As New Dictionary(Of String, Boolean) From {
            {"Resident", False},
            {"Household", False},
            {"Ayuda", False},
            {"Reports", False}
        }

        Try
            Dim assignedFormIds As List(Of Integer) = GetUserPermissions(userId)

            If allFormsData IsNot Nothing Then
                For Each formId In assignedFormIds
                    For Each row As DataRow In allFormsData.Rows
                        If CInt(row("FormID")) = formId Then
                            Dim formClass As String = row("FormClass").ToString()
                            If formClass.Contains("Resident") Then states("Resident") = True
                            If formClass.Contains("Household") Then states("Household") = True
                            If formClass.Contains("Ayuda") Then states("Ayuda") = True
                            If formClass.Contains("Reports") Then states("Reports") = True
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            Debug.WriteLine("Error getting assigned form states: " & ex.Message)
        End Try

        Return states
    End Function

    ''' <summary>
    ''' Get user permissions (FormIDs only)
    ''' </summary>
    Public Function GetUserPermissions(userId As Integer) As List(Of Integer)
        Dim formIds As New List(Of Integer)
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return formIds

            Dim query As String = "SELECT FormID FROM UserPermissions WHERE UserId = @UserId AND CanView = 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@UserId", userId)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        formIds.Add(CInt(reader("FormID")))
                    End While
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting permissions: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return formIds
    End Function

    ''' <summary>
    ''' Add new admin account
    ''' </summary>
    Public Function AddAdminAccount(firstName As String, lastName As String, username As String, password As String, role As String) As Integer
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1

            ' === CHECK IF USERNAME EXISTS ===
            Dim checkQuery As String = "SELECT COUNT(*) FROM UserAccounts WHERE Username = @Username"
            Using checkCmd As New MySqlCommand(checkQuery, connection)
                checkCmd.Parameters.AddWithValue("@Username", username)
                Dim count As Integer = CInt(checkCmd.ExecuteScalar())
                If count > 0 Then
                    Debug.WriteLine("Username already exists")
                    Return -1
                End If
            End Using

            ' === INSERT NEW ACCOUNT ===
            Dim hashedPassword As String = HashPassword(password)
            Dim query As String = "INSERT INTO UserAccounts (FirstName, LastName, Username, Password, Role, IsActive, CreatedAt) " &
                                 "VALUES (@FirstName, @LastName, @Username, @Password, @Role, 1, NOW()); SELECT LAST_INSERT_ID();"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Password", hashedPassword)
                cmd.Parameters.AddWithValue("@Role", role)

                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Return CInt(result)
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error adding account: " & ex.Message)
            Return -1
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return -1
    End Function

    ''' <summary>
    ''' Update existing admin account
    ''' </summary>
    Public Function UpdateAdminAccount(userId As Integer, firstName As String, lastName As String, username As String, password As String) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            ' === CHECK IF USERNAME ALREADY EXISTS (excluding current user) ===
            Dim checkQuery As String = "SELECT COUNT(*) FROM UserAccounts WHERE Username = @Username AND UserId != @UserId"
            Using checkCmd As New MySqlCommand(checkQuery, connection)
                checkCmd.Parameters.AddWithValue("@Username", username)
                checkCmd.Parameters.AddWithValue("@UserId", userId)
                Dim count As Integer = CInt(checkCmd.ExecuteScalar())
                If count > 0 Then
                    Debug.WriteLine("Username already exists")
                    Return False
                End If
            End Using

            ' === UPDATE ACCOUNT ===
            Dim query As String = "UPDATE UserAccounts SET FirstName = @FirstName, LastName = @LastName, Username = @Username"
            If Not String.IsNullOrEmpty(password) Then
                query &= ", Password = @Password"
            End If
            query &= ", UpdatedAt = NOW() WHERE UserId = @UserId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Username", username)
                If Not String.IsNullOrEmpty(password) Then
                    cmd.Parameters.AddWithValue("@Password", HashPassword(password))
                End If
                cmd.Parameters.AddWithValue("@UserId", userId)

                cmd.ExecuteNonQuery()
                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error updating account: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Archive (deactivate) an account
    ''' </summary>
    Public Function ArchiveAccount(userId As Integer) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim query As String = "UPDATE UserAccounts SET IsActive = 0, UpdatedAt = NOW() WHERE UserId = @UserId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.ExecuteNonQuery()
                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error archiving account: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Assign forms to a user
    ''' </summary>
    Public Function AssignFormsToUser(userId As Integer, formIds As List(Of Integer), assignedBy As Integer) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            ' === DELETE EXISTING PERMISSIONS ===
            Dim deleteQuery As String = "DELETE FROM UserPermissions WHERE UserId = @UserId"
            Using cmd As New MySqlCommand(deleteQuery, connection)
                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.ExecuteNonQuery()
            End Using

            ' === INSERT NEW PERMISSIONS ===
            If formIds.Count > 0 Then
                For Each formId In formIds
                    Dim insertQuery As String = "INSERT INTO UserPermissions (UserId, FormID, CanView, CanAdd, CanEdit, CanDelete, AssignedBy, AssignedAt) " &
                                               "VALUES (@UserId, @FormID, 1, 0, 0, 0, @AssignedBy, NOW())"
                    Using cmd As New MySqlCommand(insertQuery, connection)
                        cmd.Parameters.AddWithValue("@UserId", userId)
                        cmd.Parameters.AddWithValue("@FormID", formId)
                        cmd.Parameters.AddWithValue("@AssignedBy", assignedBy)
                        cmd.ExecuteNonQuery()
                    End Using
                Next
            End If

            Return True

        Catch ex As Exception
            Debug.WriteLine("Error assigning forms: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Hash password using MD5 (matches PhpMyAdmin)
    ''' </summary>
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

    ''' <summary>
    ''' Get user password from database
    ''' Returns encrypted password (will be displayed as * in txtPass)
    ''' </summary>
    Public Function GetUserPassword(userId As Integer) As String
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return ""

            Dim query As String = "SELECT Password FROM UserAccounts WHERE UserId = @UserId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@UserId", userId)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Return result.ToString()
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting user password: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return ""
    End Function

End Class
