Imports MySql.Data.MySqlClient

''' <summary>
''' GlobalAuditLogger — Centralized audit trail writer for all forms.
''' Writes a single row to the `audittrail` table for every tracked user action.
'''
''' TABLE SCHEMA EXPECTED:
'''   audittrail (
'''     AuditId     INT AUTO_INCREMENT PRIMARY KEY,
'''     UserId      INT,
'''     Username    VARCHAR(100),
'''     Form        VARCHAR(100),
'''     Action      VARCHAR(100),
'''     Description TEXT,
'''     Timestamp   DATETIME
'''   )
'''
''' USAGE — Inside a transaction (preferred, atomically safe):
'''   GlobalAuditLogger.Log("ResidentAdding_Form", "ADD", "Added resident: Juan Dela Cruz", connection, transaction)
'''
''' USAGE — Standalone (own connection, fire-and-forget):
'''   GlobalAuditLogger.Log("ForgetPass", "RESET PASSWORD", "User reset password for account: jdelacruz")
'''
''' The logger reads the current user session from LogInForm.CurrentUserID and LogInForm.CurrentUsername.
''' </summary>
Public Module GlobalAuditLogger

    ''' <summary>
    ''' [PRIMARY OVERLOAD] Logs an audit entry WITHIN an existing transaction.
    ''' Use this overload when the audit log must be atomic with the main DB operation.
    ''' If the main transaction rolls back, the audit log also rolls back — keeping data consistent.
    ''' </summary>
    Public Sub Log(formName As String, action As String, description As String,
                   connection As MySqlConnection, transaction As MySqlTransaction)
        Try
            Dim userId As Integer = LogInForm.CurrentUserID
            Dim username As String = If(String.IsNullOrWhiteSpace(LogInForm.CurrentUsername), "Unknown", LogInForm.CurrentUsername)

            Dim auditQuery As String =
                "INSERT INTO audittrail (UserId, Username, Form, Action, Description, Timestamp) " &
                "VALUES (@UserId, @Username, @Form, @Action, @Description, NOW())"

            Using cmd As New MySqlCommand(auditQuery, connection, transaction)
                cmd.Parameters.AddWithValue("@UserId", If(userId > 0, CObj(userId), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Form", formName)
                cmd.Parameters.AddWithValue("@Action", action)
                cmd.Parameters.AddWithValue("@Description", description)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Debug.WriteLine("[GlobalAuditLogger] Transactional log failed: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' [STANDALONE OVERLOAD] Logs an audit entry using its own independent DB connection.
    ''' Use this overload for operations that do NOT use a transaction (e.g., password reset,
    ''' login events) or when you want a best-effort audit that won't block the main operation.
    ''' </summary>
    Public Sub Log(formName As String, action As String, description As String)
        Dim connection As MySqlConnection = Nothing
        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                Debug.WriteLine("[GlobalAuditLogger] Standalone log skipped — no DB connection.")
                Return
            End If

            Dim userId As Integer = LogInForm.CurrentUserID
            Dim username As String = If(String.IsNullOrWhiteSpace(LogInForm.CurrentUsername), "Unknown", LogInForm.CurrentUsername)

            Dim auditQuery As String =
                "INSERT INTO audittrail (UserId, Username, Form, Action, Description, Timestamp) " &
                "VALUES (@UserId, @Username, @Form, @Action, @Description, NOW())"

            Using cmd As New MySqlCommand(auditQuery, connection)
                cmd.Parameters.AddWithValue("@UserId", If(userId > 0, CObj(userId), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Form", formName)
                cmd.Parameters.AddWithValue("@Action", action)
                cmd.Parameters.AddWithValue("@Description", description)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Debug.WriteLine("[GlobalAuditLogger] Standalone log failed: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Sub
    Public Sub LogLogin(
    formName As String,
    action As String,
    description As String,
    userId As Integer,
    username As String)

        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()

            If connection Is Nothing Then
                Debug.WriteLine("[GlobalAuditLogger] Login log skipped - no DB connection.")
                Return
            End If

            Dim auditQuery As String =
                "INSERT INTO audittrail " &
                "(UserId, Username, Form, Action, Description, Timestamp) " &
                "VALUES " &
                "(@UserId, @Username, @Form, @Action, @Description, NOW())"

            Using cmd As New MySqlCommand(auditQuery, connection)

                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Form", formName)
                cmd.Parameters.AddWithValue("@Action", action)
                cmd.Parameters.AddWithValue("@Description", description)

                cmd.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Debug.WriteLine("[GlobalAuditLogger] Login log failed: " & ex.Message)

        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

    End Sub

End Module
