Imports MySql.Data.MySqlClient

''' <summary>
''' Business logic for ManageUserAccount form
''' Handles user account operations (load, update, validate)
''' Includes audit logging for user activities
''' </summary>
Public Class ManageUserAccountLogic

    ''' <summary>
    ''' Result class for user operations
    ''' </summary>
    Public Class UserOperationResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public Property ErrorCode As Integer
        Public Property Data As Object

        Public Sub New()
            IsSuccess = False
            Message = ""
            ErrorCode = 0
            Data = Nothing
        End Sub
    End Class

    ''' <summary>
    ''' User account data class
    ''' </summary>
    Public Class UserAccountData
        Public Property UserId As Integer
        Public Property FirstName As String
        Public Property LastName As String
        Public Property Username As String
        Public Property Password As String
        Public Property Role As String
        Public Property IsActive As Boolean
        Public Property CreatedAt As DateTime
    End Class

    ''' <summary>
    ''' Get current logged-in user by username
    ''' </summary>
    Public Function GetUserByUsername(username As String) As UserOperationResult
        Dim result As New UserOperationResult()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            Dim query As String = "SELECT UserId, FirstName, LastName, Username, Password, Role, IsActive, CreatedAt " &
                                 "FROM useraccounts WHERE Username = @Username LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Username", username)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim userData As New UserAccountData With {
                            .UserId = CInt(reader("UserId")),
                            .FirstName = If(IsDBNull(reader("FirstName")), "", reader("FirstName").ToString()),
                            .LastName = If(IsDBNull(reader("LastName")), "", reader("LastName").ToString()),
                            .Username = If(IsDBNull(reader("Username")), "", reader("Username").ToString()),
                            .Password = If(IsDBNull(reader("Password")), "", reader("Password").ToString()),
                            .Role = If(IsDBNull(reader("Role")), "", reader("Role").ToString()),
                            .IsActive = If(IsDBNull(reader("IsActive")), False, CBool(reader("IsActive"))),
                            .CreatedAt = If(IsDBNull(reader("CreatedAt")), Now, CDate(reader("CreatedAt")))
                        }

                        result.IsSuccess = True
                        result.Message = "User data loaded successfully."
                        result.ErrorCode = 0
                        result.Data = userData
                    Else
                        result.Message = "User not found."
                        result.ErrorCode = 1
                    End If
                End Using
            End Using

        Catch ex As Exception
            result.Message = "Error retrieving user data: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in GetUserByUsername: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Validate user account fields for saving
    ''' isChangingPassword: True if password should be validated
    ''' </summary>
    Public Function ValidateUserDataForSave(userData As UserAccountData, isChangingPassword As Boolean) As UserOperationResult
        Dim result As New UserOperationResult()

        ' === VALIDATE FIRST NAME ===
        If String.IsNullOrWhiteSpace(userData.FirstName) Then
            result.Message = "First Name cannot be empty."
            result.ErrorCode = 1
            Return result
        End If

        ' === VALIDATE LAST NAME ===
        If String.IsNullOrWhiteSpace(userData.LastName) Then
            result.Message = "Last Name cannot be empty."
            result.ErrorCode = 1
            Return result
        End If

        ' === IF CHANGING PASSWORD, VALIDATE IT ===
        If isChangingPassword Then
            If String.IsNullOrWhiteSpace(userData.Password) Then
                result.Message = "Password is required."
                result.ErrorCode = 1
                Return result
            End If

            If userData.Password.Length < 6 Then
                result.Message = "Password must be at least 6 characters long."
                result.ErrorCode = 1
                Return result
            End If
        End If

        result.IsSuccess = True
        result.Message = "Validation passed."
        result.ErrorCode = 0
        Return result
    End Function

    ''' <summary>
    ''' Update user account information in database
    ''' If password is empty string, only update name fields
    ''' If password is provided, it will be ENCRYPTED using SHA256
    ''' </summary>
    Public Function UpdateUserAccount(userData As UserAccountData) As UserOperationResult
        Dim result As New UserOperationResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === GET CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            Dim transaction As MySqlTransaction = connection.BeginTransaction()

            Try
                ' === BUILD UPDATE QUERY - CONDITIONALLY UPDATE PASSWORD ===
                Dim updateQuery As String

                If String.IsNullOrEmpty(userData.Password) Then
                    ' === UPDATE ONLY NAME FIELDS ===
                    updateQuery = "UPDATE useraccounts SET FirstName = @FirstName, LastName = @LastName, " &
                                 "UpdatedAt = NOW() WHERE UserId = @UserId"
                Else
                    ' === UPDATE NAME AND PASSWORD (ENCRYPTED with SHA256) ===
                    updateQuery = "UPDATE useraccounts SET FirstName = @FirstName, LastName = @LastName, " &
                                 "Password = @Password, UpdatedAt = NOW() WHERE UserId = @UserId"
                End If

                Using updateCmd As New MySqlCommand(updateQuery, connection, transaction)
                    updateCmd.Parameters.AddWithValue("@FirstName", userData.FirstName)
                    updateCmd.Parameters.AddWithValue("@LastName", userData.LastName)
                    updateCmd.Parameters.AddWithValue("@UserId", userData.UserId)

                    ' === ENCRYPT PASSWORD BEFORE STORING ===
                    If Not String.IsNullOrEmpty(userData.Password) Then
                        Dim encryptedPassword As String = PasswordEncryption.HashPassword(userData.Password)
                        updateCmd.Parameters.AddWithValue("@Password", encryptedPassword)
                    End If

                    updateCmd.ExecuteNonQuery()
                End Using

                ' === LOG AUDIT TRAIL ===
                LogAuditTrail(userData.UserId, userData.Username, "ManageUserAccount_Form", "UPDATE",
                             "User updated their account information", connection, transaction)

                transaction.Commit()
                result.IsSuccess = True
                result.Message = "Account updated successfully."
                result.ErrorCode = 0

            Catch ex As Exception
                transaction.Rollback()
                result.Message = "Error updating account: " & ex.Message
                result.ErrorCode = 3
                Debug.WriteLine("Error in transaction: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in UpdateUserAccount: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Log user activity to audit trail table
    ''' </summary>
    Private Sub LogAuditTrail(userId As Integer, username As String, form As String, action As String,
                             description As String, connection As MySqlConnection, transaction As MySqlTransaction)
        Try
            Dim auditQuery As String = "INSERT INTO audittrail (UserId, Username, Form, Action, Description, Timestamp) " &
                                      "VALUES (@UserId, @Username, @Form, @Action, @Description, NOW())"

            Using auditCmd As New MySqlCommand(auditQuery, connection, transaction)
                auditCmd.Parameters.AddWithValue("@UserId", userId)
                auditCmd.Parameters.AddWithValue("@Username", username)
                auditCmd.Parameters.AddWithValue("@Form", form)
                auditCmd.Parameters.AddWithValue("@Action", action)
                auditCmd.Parameters.AddWithValue("@Description", description)
                auditCmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error logging audit trail: " & ex.Message)
        End Try
    End Sub

End Class
