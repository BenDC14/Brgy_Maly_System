Imports MySql.Data.MySqlClient
Imports System.Data

''' <summary>
''' Business logic for AyudaAudit_Form
''' Handles KPI calculations, reconciliation, and audit trail display
''' </summary>
Public Class AyudaAuditLogic

    ''' <summary>
    ''' KPI Results Class
    ''' </summary>
    Public Class KPIResults
        Public Property TotalResidentsServed As Integer
        Public Property TotalCashReleased As Decimal
        Public Property TotalPacksReleased As Decimal
    End Class

    ''' <summary>
    ''' Results class for totals calculation
    ''' </summary>
    Public Class TotalsResult
        Public Property TotalCash As Decimal
        Public Property TotalPacks As Decimal
    End Class

    ''' <summary>
    ''' Calculate KPIs based on date range and selected program
    ''' Uses LIKE for flexible assistance_type matching
    ''' </summary>
    Public Function CalculateKPIs(startDate As Date, endDate As Date, aidId As Integer) As KPIResults
        Try
            Dim result As New KPIResults()
            Dim connection As MySqlConnection = Nothing

            Try
                connection = ConnectDB_Module.GetDatabaseConnection()
                If connection Is Nothing Then Return result

                ' === TOTAL RESIDENTS SERVED ===
                Dim residentsQuery As String = "SELECT COUNT(DISTINCT ra.ResidentId) " &
                                              "FROM residentaid ra " &
                                              "WHERE ra.AidId = @AidId " &
                                              "AND ra.AidDate >= @StartDate " &
                                              "AND ra.AidDate < DATE_ADD(@EndDate, INTERVAL 1 DAY)"

                Using cmd As New MySqlCommand(residentsQuery, connection)
                    cmd.Parameters.AddWithValue("@AidId", aidId)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Date)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Date)
                    Dim residentsResult = cmd.ExecuteScalar()
                    If residentsResult IsNot Nothing AndAlso Not IsDBNull(residentsResult) Then
                        result.TotalResidentsServed = CInt(residentsResult)
                    End If
                End Using

                ' === TOTAL CASH RELEASED - USE LIKE FOR FLEXIBLE MATCHING ===
                Dim cashQuery As String = "SELECT COALESCE(SUM(ra.Quantity), 0) " &
                                         "FROM residentaid ra " &
                                         "INNER JOIN barangayaid ba ON ra.AidId = ba.AidId " &
                                         "WHERE ra.AidId = @AidId " &
                                         "AND LOWER(ba.assistance_type) LIKE '%cash%' " &
                                         "AND ra.AidDate >= @StartDate " &
                                         "AND ra.AidDate < DATE_ADD(@EndDate, INTERVAL 1 DAY)"

                Using cmd As New MySqlCommand(cashQuery, connection)
                    cmd.Parameters.AddWithValue("@AidId", aidId)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Date)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Date)
                    Dim cashResult = cmd.ExecuteScalar()
                    If cashResult IsNot Nothing AndAlso Not IsDBNull(cashResult) Then
                        result.TotalCashReleased = CDec(cashResult)
                    End If
                End Using

                ' === TOTAL PACKS RELEASED - USE LIKE FOR FLEXIBLE MATCHING ===
                Dim packsQuery As String = "SELECT COALESCE(SUM(ra.Quantity), 0) " &
                                          "FROM residentaid ra " &
                                          "INNER JOIN barangayaid ba ON ra.AidId = ba.AidId " &
                                          "WHERE ra.AidId = @AidId " &
                                          "AND (LOWER(ba.assistance_type) LIKE '%pack%' " &
                                          "     OR LOWER(ba.assistance_type) LIKE '%food%' " &
                                          "     OR LOWER(ba.assistance_type) LIKE '%in-kind%') " &
                                          "AND ra.AidDate >= @StartDate " &
                                          "AND ra.AidDate < DATE_ADD(@EndDate, INTERVAL 1 DAY)"

                Using cmd As New MySqlCommand(packsQuery, connection)
                    cmd.Parameters.AddWithValue("@AidId", aidId)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Date)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Date)
                    Dim packsResult = cmd.ExecuteScalar()
                    If packsResult IsNot Nothing AndAlso Not IsDBNull(packsResult) Then
                        result.TotalPacksReleased = CDec(packsResult)
                    End If
                End Using

            Catch ex As Exception
                Debug.WriteLine("Error calculating KPIs: " & ex.Message)
            Finally
                If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

            Return result

        Catch ex As Exception
            Debug.WriteLine("CalculateKPIs Error: " & ex.Message)
            Return New KPIResults()
        End Try
    End Function

    ''' <summary>
    ''' Calculate totals based on assistance type, date range, and AidId
    ''' Uses dynamic SUM with CASE WHEN to handle "Cash" vs "In-Kind"/"Food Pack"
    ''' </summary>
    Public Function CalculateTotals(startDate As Date, endDate As Date, aidId As Integer) As TotalsResult
        Try
            Dim result As New TotalsResult()
            Dim connection As MySqlConnection = Nothing

            Try
                connection = ConnectDB_Module.GetDatabaseConnection()
                If connection Is Nothing Then Return result

                ' === SINGLE QUERY WITH CONDITIONAL SUMS ===
                ' Groups by assistance_type and sums quantities accordingly
                Dim query As String = "SELECT " &
                                     "COALESCE(SUM(CASE WHEN ba.assistance_type = 'Cash' THEN ra.Quantity ELSE 0 END), 0) AS TotalCash, " &
                                     "COALESCE(SUM(CASE WHEN ba.assistance_type IN ('In-Kind', 'Food Pack') THEN ra.Quantity ELSE 0 END), 0) AS TotalPacks " &
                                     "FROM residentaid ra " &
                                     "INNER JOIN barangayaid ba ON ra.AidId = ba.AidId " &
                                     "WHERE ra.AidDate >= @StartDate " &
                                     "AND ra.AidDate < DATE_ADD(@EndDate, INTERVAL 1 DAY) " &
                                     "AND ra.AidId = @AidId"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Date)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Date)
                    cmd.Parameters.AddWithValue("@AidId", aidId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            result.TotalCash = If(IsDBNull(reader("TotalCash")), 0D, CDec(reader("TotalCash")))
                            result.TotalPacks = If(IsDBNull(reader("TotalPacks")), 0D, CDec(reader("TotalPacks")))
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Debug.WriteLine("Error calculating totals: " & ex.Message)
            Finally
                If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

            Return result

        Catch ex As Exception
            Debug.WriteLine("CalculateTotals Error: " & ex.Message)
            Return New TotalsResult()
        End Try
    End Function

    ''' <summary>
    ''' Calculate totals for ALL programs (when "All Programs" is selected)
    ''' </summary>
    Public Function CalculateTotalsAllPrograms(startDate As Date, endDate As Date) As TotalsResult
        Try
            Dim result As New TotalsResult()
            Dim connection As MySqlConnection = Nothing

            Try
                connection = ConnectDB_Module.GetDatabaseConnection()
                If connection Is Nothing Then Return result

                ' === SUM ALL PROGRAMS ===
                Dim query As String = "SELECT " &
                                     "COALESCE(SUM(CASE WHEN ba.assistance_type = 'Cash' THEN ra.Quantity ELSE 0 END), 0) AS TotalCash, " &
                                     "COALESCE(SUM(CASE WHEN ba.assistance_type IN ('In-Kind', 'Food Pack') THEN ra.Quantity ELSE 0 END), 0) AS TotalPacks " &
                                     "FROM residentaid ra " &
                                     "INNER JOIN barangayaid ba ON ra.AidId = ba.AidId " &
                                     "WHERE ra.AidDate >= @StartDate " &
                                     "AND ra.AidDate < DATE_ADD(@EndDate, INTERVAL 1 DAY)"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Date)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Date)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            result.TotalCash = If(IsDBNull(reader("TotalCash")), 0D, CDec(reader("TotalCash")))
                            result.TotalPacks = If(IsDBNull(reader("TotalPacks")), 0D, CDec(reader("TotalPacks")))
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Debug.WriteLine("Error calculating totals for all programs: " & ex.Message)
            Finally
                If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

            Return result

        Catch ex As Exception
            Debug.WriteLine("CalculateTotalsAllPrograms Error: " & ex.Message)
            Return New TotalsResult()
        End Try
    End Function

    ''' <summary>
    ''' Get Ayuda Information with reconciliation data
    ''' Shows: Program Name, Allocated Slots, Claimed Count, Remaining Slots, Status
    ''' </summary>
    Public Function GetAyudaInformation(aidId As Integer) As DataTable
        Try
            Dim dataTable As New DataTable()
            Dim connection As MySqlConnection = Nothing

            Try
                connection = ConnectDB_Module.GetDatabaseConnection()
                If connection Is Nothing Then Return dataTable

                ' MODIFIED: Used GREATEST(..., 0) to ensure remaining slots never show negative values
                Dim query As String = "SELECT " &
                                     "ba.AidId, " &
                                     "ba.program_title AS 'Program Name', " &
                                     "ba.max_slots AS 'Allocated Slots', " &
                                     "COALESCE(COUNT(ra.ResidentAidId), 0) AS 'Claimed', " &
                                     "GREATEST(ba.max_slots - COALESCE(COUNT(ra.ResidentAidId), 0), 0) AS 'Remaining', " &
                                     "ba.end_date, " &
                                     "CASE " &
                                     "  WHEN ba.end_date < CURDATE() THEN 'LOCKED (Expired)' " &
                                     "  WHEN ba.max_slots - COALESCE(COUNT(ra.ResidentAidId), 0) <= 0 THEN 'FULL' " &
                                     "  ELSE 'ACTIVE' " &
                                     "END AS 'Status' " &
                                     "FROM barangayaid ba " &
                                     "LEFT JOIN residentaid ra ON ba.AidId = ra.AidId " &
                                     "WHERE ba.AidId = @AidId " &
                                     "GROUP BY ba.AidId, ba.program_title, ba.max_slots, ba.end_date"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@AidId", aidId)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dataTable)
                    End Using
                End Using

            Catch ex As Exception
                Debug.WriteLine("Error getting ayuda information: " & ex.Message)
            Finally
                If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

            Return dataTable

        Catch ex As Exception
            Debug.WriteLine("GetAyudaInformation Error: " & ex.Message)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Get Ayuda Audit Information - Detailed log of claims
    ''' Shows: Resident Name, Date Claimed, Quantity, Description
    ''' </summary>
    Public Function GetAyudaAuditInformation(aidId As Integer, startDate As Date, endDate As Date) As DataTable
        Try
            Dim dataTable As New DataTable()
            Dim connection As MySqlConnection = Nothing

            Try
                connection = ConnectDB_Module.GetDatabaseConnection()
                If connection Is Nothing Then Return dataTable

                Dim query As String = "SELECT " &
                                     "CONCAT(r.FirstName, ' ', r.LastName) AS 'Resident Name', " &
                                     "ra.AidDate AS 'Date Claimed', " &
                                     "ra.Quantity, " &
                                     "ra.Description " &
                                     "FROM residentaid ra " &
                                     "INNER JOIN residents r ON ra.ResidentId = r.ResidentId " &
                                     "WHERE ra.AidId = @AidId " &
                                     "AND ra.AidDate >= @StartDate " &
                                     "AND ra.AidDate <= @EndDate " &
                                     "ORDER BY ra.AidDate DESC"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@AidId", aidId)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Date)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddSeconds(-1))
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dataTable)
                    End Using
                End Using

            Catch ex As Exception
                Debug.WriteLine("Error getting ayuda audit information: " & ex.Message)
            Finally
                If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

            Return dataTable

        Catch ex As Exception
            Debug.WriteLine("GetAyudaAuditInformation Error: " & ex.Message)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Get all active ayuda programs for ComboBox
    ''' </summary>
    Public Function GetAllAyudaPrograms() As DataTable
        Try
            Dim dataTable As New DataTable()
            Dim connection As MySqlConnection = Nothing

            Try
                connection = ConnectDB_Module.GetDatabaseConnection()
                If connection Is Nothing Then Return dataTable

                Dim query As String = "SELECT AidId, program_title, " &
                                     "CONCAT(program_title, ' (', DATE_FORMAT(start_date, '%M %d, %Y'), ' - ', DATE_FORMAT(end_date, '%M %d, %Y'), ')') AS DisplayText " &
                                     "FROM barangayaid " &
                                     "WHERE is_active = 1 " &
                                     "ORDER BY start_date DESC"

                Using cmd As New MySqlCommand(query, connection)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dataTable)
                    End Using
                End Using

            Catch ex As Exception
                Debug.WriteLine("Error getting ayuda programs: " & ex.Message)
            Finally
                If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

            Return dataTable

        Catch ex As Exception
            Debug.WriteLine("GetAllAyudaPrograms Error: " & ex.Message)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Get assistance_type for a specific Ayuda program
    ''' </summary>
    Public Function GetAssistanceType(aidId As Integer) As String
        Try
            Dim connection As MySqlConnection = Nothing

            Try
                connection = ConnectDB_Module.GetDatabaseConnection()
                If connection Is Nothing Then Return ""

                Dim query As String = "SELECT assistance_type FROM barangayaid WHERE AidId = @AidId"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@AidId", aidId)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        Return result.ToString().Trim()
                    End If
                End Using

            Catch ex As Exception
                Debug.WriteLine("Error getting assistance type: " & ex.Message)
            Finally
                If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

            Return ""

        Catch ex As Exception
            Debug.WriteLine("GetAssistanceType Error: " & ex.Message)
            Return ""
        End Try
    End Function
    ' ════════════════════════════════════════════════════════════════
    '  REPORT EXPORT — Data Transfer Types
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Carries all filter/summary values captured from the form's controls
    ''' at the moment the user clicks Print Audit.
    ''' The form layer populates this; the logic layer reads it.
    ''' </summary>
    Public Class ReportFilterData
        Public Property StartDate As Date
        Public Property EndDate As Date
        Public Property AyudaProgram As String   ' "All Programs" when none selected
        Public Property TotalResidentsServed As String
        Public Property TotalCashReleased As String
        Public Property TotalPacksReleased As String
        Public Property GeneratedBy As String
        Public Property GeneratedOn As Date
    End Class

    ''' <summary>
    ''' A plain-data snapshot of a DataGridView — headers and cell strings.
    ''' Decouples the logic layer from the WinForms DataGridView type entirely.
    ''' </summary>
    Public Class GridExportData
        Public Property SectionTitle As String
        Public Property Headers As List(Of String)
        Public Property Rows As List(Of List(Of String))

        Public Sub New(title As String)
            SectionTitle = title
            Headers = New List(Of String)()
            Rows = New List(Of List(Of String))()
        End Sub

        Public ReadOnly Property HasData As Boolean
            Get
                Return Headers.Count > 0 AndAlso Rows.Count > 0
            End Get
        End Property
    End Class

    ' ════════════════════════════════════════════════════════════════
    '  REPORT EXPORT — HTML Builder Methods
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Assembles the complete HTML report document as a String.
    ''' Accepts plain data only — no WinForms or UI dependencies.
    ''' </summary>
    Public Function BuildAyudaReportHtml(filter As ReportFilterData,
                                         ayudaInfoGrid As GridExportData,
                                         auditTransactionGrid As GridExportData) As String
        Dim sb As New System.Text.StringBuilder()

        ' ── HTML Shell ───────────────────────────────────────────────
        sb.AppendLine("<!DOCTYPE html>")
        sb.AppendLine("<html>")
        sb.AppendLine("<head>")
        sb.AppendLine("  <meta charset=""UTF-8"">")
        sb.AppendLine("  <title>Ayuda Audit Report</title>")
        sb.AppendLine("  <style>")
        sb.AppendLine("    body { font-family: Arial, sans-serif; font-size: 13px; margin: 24px; color: #1a1a2e; }")
        sb.AppendLine("    h1   { font-size: 22px; color: #2c5f2e; margin-bottom: 4px; }")
        sb.AppendLine("    h2   { font-size: 16px; color: #2c5f2e; margin-top: 28px; margin-bottom: 8px;")
        sb.AppendLine("           border-bottom: 2px solid #2c5f2e; padding-bottom: 4px; }")
        sb.AppendLine("    .meta-card { background-color: #f0fff4; border: 1px solid #a8d5b5;")
        sb.AppendLine("                 border-radius: 6px; padding: 16px 20px; margin-bottom: 24px; }")
        sb.AppendLine("    .meta-card table { border-collapse: collapse; width: 100%; }")
        sb.AppendLine("    .meta-card td       { padding: 6px 12px; font-size: 13px; }")
        sb.AppendLine("    .meta-card td.lbl   { font-weight: bold; color: #2c5f2e; width: 230px; white-space: nowrap; }")
        sb.AppendLine("    .meta-card td.val   { color: #333; }")
        sb.AppendLine("    .data-table         { border-collapse: collapse; width: 100%; margin-bottom: 32px; }")
        sb.AppendLine("    .data-table th      { background-color: #2c5f2e; color: #fff; padding: 8px 10px;")
        sb.AppendLine("                          text-align: left; font-size: 12px; border: 1px solid #1a3d1e; }")
        sb.AppendLine("    .data-table td      { padding: 6px 10px; border: 1px solid #b2d8b2; font-size: 12px; }")
        sb.AppendLine("    .data-table tr:nth-child(even) { background-color: #f0fff4; }")
        sb.AppendLine("    .no-data { color: #888; font-style: italic; padding: 10px 0; }")
        sb.AppendLine("    .footer  { margin-top: 32px; font-size: 11px; color: #888;")
        sb.AppendLine("               border-top: 1px solid #ccc; padding-top: 8px; }")
        sb.AppendLine("  </style>")
        sb.AppendLine("</head>")
        sb.AppendLine("<body>")

        ' ── Report Title ─────────────────────────────────────────────
        sb.AppendLine("  <h1>&#127807; Barangay Maly — Ayuda Audit Report</h1>")
        sb.AppendLine("  <p style=""margin-top:0; color:#555; font-size:12px;"">")
        sb.AppendLine("    Generated on: " & Format(filter.GeneratedOn, "MMMM dd, yyyy  hh:mm:ss tt"))
        sb.AppendLine("    &nbsp;|&nbsp; Exported by: <strong>" &
                      System.Net.WebUtility.HtmlEncode(filter.GeneratedBy) & "</strong>")
        sb.AppendLine("  </p>")

        ' ── Filter Criteria Metadata Card ────────────────────────────
        sb.AppendLine("  <div class=""meta-card"">")
        sb.AppendLine("    <table>")
        sb.AppendLine("      <tr>")
        sb.AppendLine("        <td class=""lbl"">&#128197; Date Range (From):</td>")
        sb.AppendLine("        <td class=""val"">" &
                      System.Net.WebUtility.HtmlEncode(filter.StartDate.ToString("MMMM dd, yyyy")) & "</td>")
        sb.AppendLine("        <td class=""lbl"">&#128197; Date Range (To):</td>")
        sb.AppendLine("        <td class=""val"">" &
                      System.Net.WebUtility.HtmlEncode(filter.EndDate.ToString("MMMM dd, yyyy")) & "</td>")
        sb.AppendLine("      </tr>")
        sb.AppendLine("      <tr>")
        sb.AppendLine("        <td class=""lbl"">&#127873; Ayuda Program Filter:</td>")
        sb.AppendLine("        <td class=""val"">" &
                      System.Net.WebUtility.HtmlEncode(filter.AyudaProgram) & "</td>")
        sb.AppendLine("        <td class=""lbl""></td><td class=""val""></td>")
        sb.AppendLine("      </tr>")
        sb.AppendLine("      <tr>")
        sb.AppendLine("        <td class=""lbl"">&#128100; Total Residents Served:</td>")
        sb.AppendLine("        <td class=""val"">" &
                      System.Net.WebUtility.HtmlEncode(filter.TotalResidentsServed) & "</td>")
        sb.AppendLine("        <td class=""lbl"">&#128181; Total Cash Released:</td>")
        sb.AppendLine("        <td class=""val"">" &
                      System.Net.WebUtility.HtmlEncode(filter.TotalCashReleased) & "</td>")
        sb.AppendLine("      </tr>")
        sb.AppendLine("      <tr>")
        sb.AppendLine("        <td class=""lbl"">&#128230; Total Packs Released:</td>")
        sb.AppendLine("        <td class=""val"">" &
                      System.Net.WebUtility.HtmlEncode(filter.TotalPacksReleased) & "</td>")
        sb.AppendLine("        <td class=""lbl""></td><td class=""val""></td>")
        sb.AppendLine("      </tr>")
        sb.AppendLine("    </table>")
        sb.AppendLine("  </div>")

        ' ── Grid 1: Master Ayuda Program Metadata ────────────────────
        sb.AppendLine("  <h2>&#128203; Master Ayuda Program Metadata</h2>")
        sb.AppendLine(BuildGridHtml(ayudaInfoGrid,
                                    "No Ayuda program records found for the selected filters."))

        ' ── Grid 2: Distribution Transaction Log ─────────────────────
        sb.AppendLine("  <h2>&#128200; Distribution Transaction Log</h2>")
        sb.AppendLine(BuildGridHtml(auditTransactionGrid,
                                    "No distribution transaction records found for the selected filters."))

        ' ── Footer ───────────────────────────────────────────────────
        sb.AppendLine("  <div class=""footer"">")
        sb.AppendLine("    Barangay Maly Management System &mdash; Confidential Document")
        sb.AppendLine("    &nbsp;|&nbsp; Printed by: " &
                      System.Net.WebUtility.HtmlEncode(filter.GeneratedBy))
        sb.AppendLine("    &nbsp;|&nbsp; " & Format(filter.GeneratedOn, "yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine("  </div>")
        sb.AppendLine("</body>")
        sb.AppendLine("</html>")

        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Converts a GridExportData snapshot into an HTML table string.
    ''' Pure string logic — no WinForms dependency.
    ''' </summary>
    Private Function BuildGridHtml(grid As GridExportData, emptyMessage As String) As String
        If Not grid.HasData Then
            Return "  <p class=""no-data"">" &
                   System.Net.WebUtility.HtmlEncode(emptyMessage) & "</p>"
        End If

        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine("  <table class=""data-table"" border=""1"" cellpadding=""6"" cellspacing=""0"">")

        ' Header row
        sb.AppendLine("    <thead><tr>")
        For Each header As String In grid.Headers
            sb.AppendLine("      <th>" & System.Net.WebUtility.HtmlEncode(header) & "</th>")
        Next
        sb.AppendLine("    </tr></thead>")

        ' Data rows
        sb.AppendLine("    <tbody>")
        For Each row As List(Of String) In grid.Rows
            sb.AppendLine("    <tr>")
            For Each cell As String In row
                sb.AppendLine("      <td>" & System.Net.WebUtility.HtmlEncode(cell) & "</td>")
            Next
            sb.AppendLine("    </tr>")
        Next
        sb.AppendLine("    </tbody>")
        sb.AppendLine("  </table>")

        Return sb.ToString()
    End Function

End Class