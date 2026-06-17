Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text

Public Class DatabaseDownloadLogsLogic

    Public Function GetBackupLogs() As DataTable
        Return GetLogs("Backup")
    End Function

    Public Function GetRestoreLogs() As DataTable
        Return GetLogs("Restore")
    End Function

    Private Function GetLogs(logType As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = ""

            If logType = "Backup" Then
                query =
                    "SELECT b.BackupId, IFNULL(ua.Username, 'Unknown') AS BackupBy, b.FilePath, b.FileName, " &
                    "b.Description, b.BackupDateTime, b.BackupStatus, b.ErrorMessage " &
                    "FROM backuplogs b " &
                    "LEFT JOIN UserAccounts ua ON b.BackupBy = ua.UserId " &
                    "ORDER BY b.BackupDateTime DESC"
            Else
                query =
                    "SELECT r.RestoreId, r.BackupId, IFNULL(ua.Username, 'Unknown') AS RestoredBy, " &
                    "r.RestoreDateTime, r.RestoreFileName, r.RestoreFilePath, r.RestoreStatus, r.ErrorMessage " &
                    "FROM restorelogs r " &
                    "LEFT JOIN UserAccounts ua ON r.RestoredBy = ua.UserId " &
                    "ORDER BY r.RestoreDateTime DESC"
            End If

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetLogs Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return dataTable
    End Function

    Public Function ValidateDownloadSelection(includeBackup As Boolean, includeRestore As Boolean, format As String) As String
        If Not includeBackup AndAlso Not includeRestore Then
            Return "Please select Backup Logs, Restore Logs, or Both."
        End If

        If String.IsNullOrWhiteSpace(format) Then
            Return "Please select CSV or Excel format."
        End If

        Return ""
    End Function

    Public Function BuildDefaultFileName(includeBackup As Boolean, includeRestore As Boolean, format As String) As String
        Dim prefix As String = "DatabaseLogs"

        If includeBackup AndAlso Not includeRestore Then prefix = "BackupLogs"
        If includeRestore AndAlso Not includeBackup Then prefix = "RestoreLogs"

        Dim extensionName As String = If(format.ToLower() = "excel", ".xls", ".csv")
        Return prefix & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & extensionName
    End Function

    ''' <summary>
    ''' Writes the selected log datasets to disk in the chosen format.
    ''' UPDATED: Wraps the file-write delegates in Try/Catch so that
    ''' GlobalAuditLogger can record both success and failure outcomes.
    ''' </summary>
    Public Sub ExportLogs(filePath As String, includeBackup As Boolean, includeRestore As Boolean, format As String)
        Try
            If format.ToLower() = "excel" Then
                ExportAsExcelHtml(filePath, includeBackup, includeRestore)
            Else
                ExportAsCsv(filePath, includeBackup, includeRestore)
            End If

            ' ── SUCCESS: file written to disk cleanly ─────────────────
            GlobalAuditLogger.Log(
                "databasedownloadlogs_form",
                "DATABASE_BACKUP_DOWNLOAD",
                LogInForm.CurrentUsername &
                " successfully downloaded a complete system database backup file.")

        Catch ex As Exception
            ' ── FAILURE: file permission error, disk full, DB drop, etc.
            Debug.WriteLine("ExportLogs Error: " & ex.Message)
            GlobalAuditLogger.Log(
                "databasedownloadlogs_form",
                "DATABASE ERROR",
                "Database backup extraction failed. Exception details: " & ex.Message)

            ' Re-throw so the form layer can still show the user an error dialog
            Throw
        End Try
    End Sub


    Private Sub ExportAsCsv(filePath As String, includeBackup As Boolean, includeRestore As Boolean)
        Dim sb As New StringBuilder()

        If includeBackup Then
            sb.AppendLine("BACKUP LOGS")
            AppendCsvTable(sb, GetBackupLogs())
            sb.AppendLine()
        End If

        If includeRestore Then
            sb.AppendLine("RESTORE LOGS")
            AppendCsvTable(sb, GetRestoreLogs())
        End If

        File.WriteAllText(filePath, sb.ToString())
    End Sub

    Private Sub AppendCsvTable(sb As StringBuilder, table As DataTable)
        For colIndex As Integer = 0 To table.Columns.Count - 1
            sb.Append(table.Columns(colIndex).ColumnName)
            If colIndex < table.Columns.Count - 1 Then sb.Append(",")
        Next
        sb.AppendLine()

        For Each row As DataRow In table.Rows
            For colIndex As Integer = 0 To table.Columns.Count - 1
                Dim value As String = row(colIndex).ToString().Replace("""", """""")
                sb.Append("""" & value & """")
                If colIndex < table.Columns.Count - 1 Then sb.Append(",")
            Next
            sb.AppendLine()
        Next
    End Sub

    Private Sub ExportAsExcelHtml(filePath As String, includeBackup As Boolean, includeRestore As Boolean)
        Dim sb As New StringBuilder()

        sb.AppendLine("<html><body>")

        If includeBackup Then
            sb.AppendLine("<h2>BACKUP LOGS</h2>")
            AppendHtmlTable(sb, GetBackupLogs())
            sb.AppendLine("<br/>")
        End If

        If includeRestore Then
            sb.AppendLine("<h2>RESTORE LOGS</h2>")
            AppendHtmlTable(sb, GetRestoreLogs())
        End If

        sb.AppendLine("</body></html>")

        File.WriteAllText(filePath, sb.ToString())
    End Sub

    Private Sub AppendHtmlTable(sb As StringBuilder, table As DataTable)
        sb.AppendLine("<table border='1'>")
        sb.AppendLine("<tr>")

        For Each column As DataColumn In table.Columns
            sb.AppendLine("<th>" & column.ColumnName & "</th>")
        Next

        sb.AppendLine("</tr>")

        For Each row As DataRow In table.Rows
            sb.AppendLine("<tr>")
            For Each column As DataColumn In table.Columns
                sb.AppendLine("<td>" & row(column).ToString() & "</td>")
            Next
            sb.AppendLine("</tr>")
        Next

        sb.AppendLine("</table>")
    End Sub

End Class