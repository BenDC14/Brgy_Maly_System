Imports MySql.Data.MySqlClient

Public Class DatabaseViewLogic

    Public Function GetLogDetails(logType As String, logId As Integer) As DataTable
        If logType.ToLower() = "backup" Then
            Return GetBackupLogDetails(logId)
        Else
            Return GetRestoreLogDetails(logId)
        End If
    End Function

    Private Function GetBackupLogDetails(backupId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT 'Backup' AS LogType, b.BackupStatus AS Status, b.BackupDateTime AS DateAndTime, " &
                "IFNULL(ua.Username, 'Unknown') AS PerformedBy, b.FileName, b.FilePath, b.ErrorMessage " &
                "FROM backuplogs b " &
                "LEFT JOIN UserAccounts ua ON b.BackupBy = ua.UserId " &
                "WHERE b.BackupId = @BackupId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@BackupId", backupId)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetBackupLogDetails Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return dataTable
    End Function

    Private Function GetRestoreLogDetails(restoreId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT 'Restore' AS LogType, r.RestoreStatus AS Status, r.RestoreDateTime AS DateAndTime, " &
                "IFNULL(ua.Username, 'Unknown') AS PerformedBy, r.RestoreFileName AS FileName, " &
                "r.RestoreFilePath AS FilePath, r.ErrorMessage " &
                "FROM restorelogs r " &
                "LEFT JOIN UserAccounts ua ON r.RestoredBy = ua.UserId " &
                "WHERE r.RestoreId = @RestoreId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@RestoreId", restoreId)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetRestoreLogDetails Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return dataTable
    End Function

End Class