Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text

Public Class ReportsMainLogic

    Public Function GetReportTypes() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT ReportTypeId, ReportTypeName " &
                "FROM reporttype " &
                "ORDER BY ReportTypeName"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetReportTypes Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function GetReportSubTypes(reportTypeId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT ReportsSubTypeId, ReportTypeId, ReportSubTypeName " &
                "FROM reportsubtype " &
                "WHERE ReportTypeId = @ReportTypeId " &
                "ORDER BY ReportSubTypeName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ReportTypeId", reportTypeId)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetReportSubTypes Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function GetReportPreview(reportTypeId As Integer, reportSubTypeId As Integer, fromDate As Date, toDate As Date, searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT r.ResidentId, r.FirstName, r.LastName, r.Sex, r.CivilStatus, " &
                "r.ContactNumber, h.HouseholdNumber " &
                "FROM residents r " &
                "LEFT JOIN household h ON r.HouseholdId = h.HouseholdID " &
                "WHERE (r.Is_Archived = 0 OR r.Is_Archived IS NULL) "

            If Not String.IsNullOrWhiteSpace(searchTerm) Then
                query &= "AND (r.FirstName LIKE @Search OR r.LastName LIKE @Search OR r.ContactNumber LIKE @Search OR h.HouseholdNumber LIKE @Search) "
            End If

            query &= "ORDER BY r.LastName, r.FirstName"

            Using cmd As New MySqlCommand(query, connection)
                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    cmd.Parameters.AddWithValue("@Search", "%" & searchTerm.Trim() & "%")
                End If

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetReportPreview Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function ValidateReportSelection(hasReportType As Boolean, hasReportSubType As Boolean, downloadType As String, fromDate As Date, toDate As Date) As String
        If Not hasReportType Then
            Return "Please select a report type."
        End If

        If Not hasReportSubType Then
            Return "Please select a report sub type."
        End If

        If String.IsNullOrWhiteSpace(downloadType) Then
            Return "Please select a download type."
        End If

        If fromDate.Date > toDate.Date Then
            Return "The From date cannot be later than the To date."
        End If

        Return ""
    End Function

    Public Function BuildFileName(reportTypeName As String, reportSubTypeName As String, downloadType As String) As String
        Dim extensionName As String = If(downloadType.ToLower() = "excel", ".csv", ".pdf")

        Dim cleanType As String = CleanFileName(reportTypeName)
        Dim cleanSubType As String = CleanFileName(reportSubTypeName)

        Return cleanType & "_" & cleanSubType & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & extensionName
    End Function

    Public Function BuildFilePath(fileName As String, downloadType As String) As String
        If downloadType.ToLower() = "pdf" Then
            Return "Printed or previewed through PrintDialog"
        End If

        Dim folderPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BrgyMalyGeneratedReports")

        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

        Return Path.Combine(folderPath, fileName)
    End Function

    Private Function CleanFileName(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return "Report"

        Dim cleaned As String = value.Trim()

        For Each invalidChar As Char In Path.GetInvalidFileNameChars()
            cleaned = cleaned.Replace(invalidChar, "_"c)
        Next

        cleaned = cleaned.Replace(" ", "_")
        cleaned = cleaned.Replace("/", "_")
        cleaned = cleaned.Replace("\", "_")

        Return cleaned
    End Function

    Public Sub SaveCsvFile(filePath As String, reportData As DataTable)
        If reportData Is Nothing Then Return

        Dim sb As New StringBuilder()

        For colIndex As Integer = 0 To reportData.Columns.Count - 1
            sb.Append(reportData.Columns(colIndex).ColumnName)

            If colIndex < reportData.Columns.Count - 1 Then
                sb.Append(",")
            End If
        Next

        sb.AppendLine()

        For Each row As DataRow In reportData.Rows
            For colIndex As Integer = 0 To reportData.Columns.Count - 1
                Dim value As String = row(colIndex).ToString().Replace("""", """""")
                sb.Append("""" & value & """")

                If colIndex < reportData.Columns.Count - 1 Then
                    sb.Append(",")
                End If
            Next

            sb.AppendLine()
        Next

        File.WriteAllText(filePath, sb.ToString())
    End Sub

    Public Function SaveGeneratedReport(reportTypeId As Integer, reportSubTypeId As Integer, generatedBy As Integer, fileName As String, filePath As String, status As String) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim query As String =
                "INSERT INTO generatedreports " &
                "(ReportTypeId, ReportsSubTypeId, GeneratedBy, Date, FileName, FilePath, Status) " &
                "VALUES " &
                "(@ReportTypeId, @ReportsSubTypeId, @GeneratedBy, NOW(), @FileName, @FilePath, @Status)"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ReportTypeId", reportTypeId)
                cmd.Parameters.AddWithValue("@ReportsSubTypeId", reportSubTypeId)
                cmd.Parameters.AddWithValue("@GeneratedBy", generatedBy)
                cmd.Parameters.AddWithValue("@FileName", fileName)
                cmd.Parameters.AddWithValue("@FilePath", filePath)
                cmd.Parameters.AddWithValue("@Status", status)

                cmd.ExecuteNonQuery()

                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("ReportsMain_Form", "GENERATE REPORT",
                LogInForm.CurrentUsername & " generated report: " & fileName & " (Type ID: " & reportTypeId & ")")


                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("SaveGeneratedReport Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

End Class