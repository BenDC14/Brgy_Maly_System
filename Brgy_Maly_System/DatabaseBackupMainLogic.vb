Imports MySql.Data.MySqlClient

Public Class DatabaseBackupMainLogic

    Public Function GetAllLogs() As DataTable
        Return GetLogs("")
    End Function

    Public Function SearchLogs(searchTerm As String) As DataTable
        Return GetLogs(searchTerm)
    End Function

    Private Function GetLogs(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT 'Backup' AS LogType, " &
                "b.BackupId AS LogId, " &
                "b.BackupId AS BackupId, " &
                "b.BackupBy AS PerformedById, " &
                "IFNULL(ua.Username, 'Unknown') AS PerformedBy, " &
                "b.BackupDateTime AS DateAndTime, " &
                "b.FileName AS FileName, " &
                "b.FilePath AS FilePath, " &
                "b.BackupStatus AS Status, " &
                "b.ErrorMessage AS ErrorMessage, " &
                "b.Description AS Description " &
                "FROM backuplogs b " &
                "LEFT JOIN UserAccounts ua ON b.BackupBy = ua.UserId " &
                "WHERE @Search = '' " &
                "OR b.FileName LIKE @SearchLike " &
                "OR b.FilePath LIKE @SearchLike " &
                "OR b.BackupStatus LIKE @SearchLike " &
                "OR b.Description LIKE @SearchLike " &
                "OR b.ErrorMessage LIKE @SearchLike " &
                "OR ua.Username LIKE @SearchLike " &
                "UNION ALL " &
                "SELECT 'Restore' AS LogType, " &
                "r.RestoreId AS LogId, " &
                "r.BackupId AS BackupId, " &
                "r.RestoredBy AS PerformedById, " &
                "IFNULL(ua.Username, 'Unknown') AS PerformedBy, " &
                "r.RestoreDateTime AS DateAndTime, " &
                "r.RestoreFileName AS FileName, " &
                "r.RestoreFilePath AS FilePath, " &
                "r.RestoreStatus AS Status, " &
                "r.ErrorMessage AS ErrorMessage, " &
                "'' AS Description " &
                "FROM restorelogs r " &
                "LEFT JOIN UserAccounts ua ON r.RestoredBy = ua.UserId " &
                "WHERE @Search = '' " &
                "OR r.RestoreFileName LIKE @SearchLike " &
                "OR r.RestoreFilePath LIKE @SearchLike " &
                "OR r.RestoreStatus LIKE @SearchLike " &
                "OR r.ErrorMessage LIKE @SearchLike " &
                "OR ua.Username LIKE @SearchLike " &
                "ORDER BY DateAndTime DESC"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Search", If(searchTerm, "").Trim())
                cmd.Parameters.AddWithValue("@SearchLike", "%" & If(searchTerm, "").Trim() & "%")

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetLogs Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function GetPage(sourceTable As DataTable, pageNumber As Integer, pageSize As Integer) As DataTable
        If sourceTable Is Nothing Then Return New DataTable()

        Dim pageTable As DataTable = sourceTable.Clone()
        Dim startIndex As Integer = (pageNumber - 1) * pageSize
        Dim endIndex As Integer = Math.Min(startIndex + pageSize, sourceTable.Rows.Count)

        If startIndex >= sourceTable.Rows.Count Then
            Return pageTable
        End If

        For index As Integer = startIndex To endIndex - 1
            pageTable.ImportRow(sourceTable.Rows(index))
        Next

        Return pageTable
    End Function

    Public Function GetTotalPages(totalRows As Integer, pageSize As Integer) As Integer
        If totalRows <= 0 Then Return 1
        Return CInt(Math.Ceiling(totalRows / CDbl(pageSize)))
    End Function

End Class