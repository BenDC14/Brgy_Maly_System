Imports MySql.Data.MySqlClient
Imports System.Text

''' <summary>
''' Forget Password Logic - Handles all password reset business logic
''' Separated from UI form logic for reusability and maintainability
''' </summary>
Public Class ForgetPassLogic

    ''' <summary>
    ''' Result class for password reset operations
    ''' </summary>
    Public Class PasswordResetResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public Property ErrorCode As Integer ' 0=Success, 1=UserNotFound, 2=PasswordMismatch, 3=DatabaseError
        Public Property Username As String

        Public Sub New()
            IsSuccess = False
            Message = ""
            ErrorCode = 0
            Username = ""
        End Sub
    End Class

    ''' <summary>
    ''' Search for user by username
    ''' </summary>
    Public Function SearchUser(username As String) As PasswordResetResult
        Dim result As New PasswordResetResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATION ===
            If String.IsNullOrWhiteSpace(username) Then
                result.ErrorCode = 1
                result.Message = "Please enter a username."
                Return result
            End If

            ' === GET DATABASE CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.ErrorCode = 3
                result.Message = "Unable to connect to database."
                Return result
            End If

            ' === SEARCH FOR USER ===
            Dim query As String = "SELECT UserId, Username, FirstName, LastName FROM UserAccounts WHERE Username = @Username LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Username", username)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' === USER FOUND ===
                        result.IsSuccess = True
                        result.Username = reader("Username").ToString()
                        result.Message = "User found successfully."
                        result.ErrorCode = 0
                    Else
                        ' === USER NOT FOUND ===
                        result.IsSuccess = False
                        result.ErrorCode = 1
                        result.Message = "Username not found in the system."
                    End If
                End Using
            End Using

        Catch ex As Exception
            result.ErrorCode = 3
            result.Message = "Database error: " & ex.Message
            Debug.WriteLine("SearchUser Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Validate password inputs
    ''' </summary>
    Public Function ValidatePasswords(newPassword As String, confirmPassword As String) As PasswordResetResult
        Dim result As New PasswordResetResult()

        ' === CHECK IF PASSWORDS ARE EMPTY ===
        If String.IsNullOrWhiteSpace(newPassword) OrElse String.IsNullOrWhiteSpace(confirmPassword) Then
            result.ErrorCode = 1
            result.Message = "Please enter both passwords."
            Return result
        End If

        ' === CHECK IF PASSWORDS MATCH ===
        If newPassword <> confirmPassword Then
            result.ErrorCode = 2
            result.Message = "Passwords do not match."
            Return result
        End If

        ' === CHECK PASSWORD LENGTH (Minimum 6 characters) ===
        If newPassword.Length < 6 Then
            result.ErrorCode = 1
            result.Message = "Password must be at least 6 characters long."
            Return result
        End If

        ' === PASSWORDS ARE VALID ===
        result.IsSuccess = True
        result.Message = "Passwords are valid."
        result.ErrorCode = 0
        Return result
    End Function

    ''' <summary>
    ''' Update password for user
    ''' </summary>
    Public Function UpdatePassword(username As String, newPassword As String) As PasswordResetResult
        Dim result As New PasswordResetResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATION ===
            If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(newPassword) Then
                result.ErrorCode = 1
                result.Message = "Username and password are required."
                Return result
            End If

            ' === GET DATABASE CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.ErrorCode = 3
                result.Message = "Unable to connect to database."
                Return result
            End If

            ' === HASH THE PASSWORD ===
            Dim hashedPassword As String = HashPassword(newPassword)

            ' === UPDATE PASSWORD IN DATABASE ===
            Dim query As String = "UPDATE UserAccounts SET Password = @Password, UpdatedAt = NOW() WHERE Username = @Username"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Password", hashedPassword)
                cmd.Parameters.AddWithValue("@Username", username)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    ' === LOG AUDIT TRAIL ===
                    GlobalAuditLogger.Log("ForgetPass", "RESET PASSWORD",
                    "User reset password for account: " & username)

                    result.IsSuccess = True
                    result.Message = "Password has been updated successfully."
                    result.ErrorCode = 0
                Else
                    ' === LOG AUDIT TRAIL ===
                    GlobalAuditLogger.Log("ForgetPass", "RESET PASSWORD",
                    "User failed to reset password for account: " & username)
                    result.ErrorCode = 1
                    result.Message = "Failed to update password. User not found."
                End If
            End Using

        Catch ex As Exception
            result.ErrorCode = 3
            result.Message = "Database error: " & ex.Message
            Debug.WriteLine("UpdatePassword Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
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

End Class
