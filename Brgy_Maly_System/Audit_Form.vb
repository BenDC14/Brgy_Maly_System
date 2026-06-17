Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text

''' <summary>
''' UI Event Layer for Audit_Form.
''' Handles all filter interactions, pagination state, export triggers,
''' and DGV configuration. All DB operations delegated to AuditLogic.
''' </summary>
Public Class Audit_Form

    ' ── Service Layer ────────────────────────────────────────────────
    Private _logic As New AuditLogic()

    ' ── Responsive Manager ───────────────────────────────────────────
    Private responsiveManager As AuditResponsiveManager

    ' ── Pagination State ─────────────────────────────────────────────
    ' _pageOffset is the base page number shown on btnPage1.
    ' e.g. when offset=0: buttons show 1,2,3 | offset=3: shows 4,5,6
    Private _pageOffset As Integer = 0
    Private _activePage As Integer = 1   ' the currently highlighted page

    ' ── Filter Suppress Flag ─────────────────────────────────────────
    ' Prevents filter-change events from firing during programmatic load
    Private _suppressFilterEvents As Boolean = False

    ' ════════════════════════════════════════════════════════════════
    '  FORM LOAD
    ' ════════════════════════════════════════════════════════════════
    Private Sub Audit_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _suppressFilterEvents = True

            ' ── Visual polish ────────────────────────────────────────
            ApplyGradient(fillpanel, "#EDFFE9", "#FFFFFF")
            RoundButtonCorners(btnSearch, 20)
            RoundButtonCorners(btnRemoveFilter, 20)
            RoundButtonCorners(btnPage1, 20)
            RoundButtonCorners(btnPage2, 20)
            RoundButtonCorners(btnPage3, 20)
            RoundButtonCorners(btnPageNext, 20)
            RoundButtonCorners(btnExportPdf, 20)
            RoundButtonCorners(btnExportExcel, 20)
            RoundButtonCorners(btnExportCSV, 20)

            ' ── Responsive layout ────────────────────────────────────
            responsiveManager = New AuditResponsiveManager(Me)
            responsiveManager.Initialize()

            ' ── Date range defaults: last 30 days up to today ────────
            dtpFrom.Value = Now.AddDays(-30)
            dtpLatest.Value = Now

            ' ── Populate filter dropdowns from DB ────────────────────
            LoadActionTypeComboBox()
            LoadFormsComboBox()

            ' ── Configure DGV columns ────────────────────────────────
            ConfigureDataGridView()

            ' ── Pagination defaults ───────────────────────────────────
            ResetPaginationToStart()

            _suppressFilterEvents = False

            ' ── Initial data load ────────────────────────────────────
            LoadPage(_activePage)

        Catch ex As Exception
            MsgBox("Error initializing Audit Log form: " & ex.Message,
                   MsgBoxStyle.Critical, "Load Error")
            Debug.WriteLine("Audit_Form_Load Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  DGV CONFIGURATION
    ' ════════════════════════════════════════════════════════════════
    Private Sub ConfigureDataGridView()
        DataGridViewHelper.ApplyStandardStyling(dgvAudit)

        dgvAudit.AllowUserToResizeRows = False
        dgvAudit.EnableHeadersVisualStyles = False
        dgvAudit.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAudit.MultiSelect = False
        dgvAudit.ReadOnly = True

        With dgvAudit
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .GridColor = Color.FromArgb(220, 220, 220)
            .ColumnHeadersHeight = 45
            .RowTemplate.Height = 38
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

        dgvAudit.Columns.Clear()
        DataGridViewHelper.AddTextColumn(dgvAudit, "AuditId", "", "AuditId", 0, True)
        dgvAudit.Columns("AuditId").Visible = False

        DataGridViewHelper.AddTextColumn(dgvAudit, "Timestamp", "Timestamp", "Timestamp", 160, True)
        DataGridViewHelper.AddTextColumn(dgvAudit, "Username", "Username", "Username", 130, True)
        DataGridViewHelper.AddTextColumn(dgvAudit, "Form", "Form", "Form", 160, True)
        DataGridViewHelper.AddTextColumn(dgvAudit, "Action", "Action", "Action", 160, True)
        DataGridViewHelper.AddTextColumn(dgvAudit, "Description", "Description", "Description", 300, True)
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  DROPDOWN LOADERS
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Populates cbActionType entirely from the audittrail table.
    ''' Index 0 is the sentinel "All Actions" — when selected,
    ''' GetCurrentFilters() returns "" so the WHERE clause is dropped.
    ''' </summary>
    Private Sub LoadActionTypeComboBox()
        Try
            cbActionType.Items.Clear()
            cbActionType.Items.Add("All Actions")     ' sentinel — index 0

            Dim actions As List(Of String) = _logic.GetDistinctActions()
            If actions.Count > 0 Then
                For Each action As String In actions
                    cbActionType.Items.Add(action)
                Next
            End If

            cbActionType.SelectedIndex = 0            ' default: All Actions

        Catch ex As Exception
            Debug.WriteLine("LoadActionTypeComboBox Error: " & ex.Message)
            ' Fallback: ensure at least the sentinel exists so the form doesn't break
            If cbActionType.Items.Count = 0 Then
                cbActionType.Items.Add("All Actions")
                cbActionType.SelectedIndex = 0
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Populates cbForms entirely from the audittrail table.
    ''' Index 0 is the sentinel "All Forms" — when selected,
    ''' GetCurrentFilters() returns "" so the WHERE clause is dropped.
    ''' New forms appear automatically the moment they write their first log.
    ''' </summary>
    Private Sub LoadFormsComboBox()
        Try
            cbForms.Items.Clear()
            cbForms.Items.Add("All Forms")            ' sentinel — index 0

            Dim forms As List(Of String) = _logic.GetDistinctForms()
            If forms.Count > 0 Then
                For Each frm As String In forms
                    cbForms.Items.Add(frm)
                Next
            End If

            cbForms.SelectedIndex = 0                 ' default: All Forms

        Catch ex As Exception
            Debug.WriteLine("LoadFormsComboBox Error: " & ex.Message)
            If cbForms.Items.Count = 0 Then
                cbForms.Items.Add("All Forms")
                cbForms.SelectedIndex = 0
            End If
        End Try
    End Sub


    ' ════════════════════════════════════════════════════════════════
    '  FILTER HELPERS — build current filter state
    ' ════════════════════════════════════════════════════════════════
    Private Function GetCurrentFilters() As AuditLogic.AuditFilter
        Dim f As New AuditLogic.AuditFilter()
        f.SearchText = txtSearch.Text.Trim()
        f.ActionType = If(cbActionType.SelectedIndex <= 0, "", cbActionType.Text)
        f.FormName = If(cbForms.SelectedIndex <= 0, "", cbForms.Text)
        f.DateFrom = dtpFrom.Value.Date
        f.DateTo = dtpLatest.Value.Date.AddDays(1).AddSeconds(-1)
        Return f
    End Function

    ' ════════════════════════════════════════════════════════════════
    '  CORE PAGINATION ENGINE
    ' ════════════════════════════════════════════════════════════════
    Private Sub ResetPaginationToStart()
        _pageOffset = 0
        _activePage = 1
        UpdatePageButtonLabels()
    End Sub

    ''' <summary>
    ''' Sets the face-value text on all three page buttons based on current offset.
    ''' Disables buttons that exceed the total page count.
    ''' </summary>
    Private Sub UpdatePageButtonLabels()
        Dim filters As AuditLogic.AuditFilter = GetCurrentFilters()
        Dim totalPages As Integer = _logic.GetTotalPages(filters)

        Dim p1 As Integer = _pageOffset + 1
        Dim p2 As Integer = _pageOffset + 2
        Dim p3 As Integer = _pageOffset + 3

        btnPage1.Text = p1.ToString()
        btnPage2.Text = p2.ToString()
        btnPage3.Text = p3.ToString()

        btnPage1.Enabled = (p1 <= totalPages)
        btnPage2.Enabled = (p2 <= totalPages)
        btnPage3.Enabled = (p3 <= totalPages)
        btnPageNext.Enabled = (p3 < totalPages)   ' only show Next if more pages exist beyond p3

        ' Highlight the active page button
        HighlightActivePage()
    End Sub

    Private Sub HighlightActivePage()
        Dim activeColor As Color = Color.FromArgb(44, 95, 46)
        Dim inactiveColor As Color = Color.FromArgb(159, 190, 168)

        For Each btn As Button In New Button() {btnPage1, btnPage2, btnPage3}
            If Integer.TryParse(btn.Text, Nothing) AndAlso CInt(btn.Text) = _activePage Then
                btn.BackColor = activeColor
                btn.ForeColor = Color.White
            Else
                btn.BackColor = inactiveColor
                btn.ForeColor = Color.Black
            End If
        Next
    End Sub

    ''' <summary>
    ''' Fetches the correct row subset for the given page number and binds to dgvAudit.
    ''' </summary>
    Private Sub LoadPage(pageNumber As Integer)
        Try
            _activePage = pageNumber
            Dim filters As AuditLogic.AuditFilter = GetCurrentFilters()
            Dim data As DataTable = _logic.GetPagedAuditLogs(filters, pageNumber)
            dgvAudit.DataSource = data

            ' Re-hide the ID column after rebind
            If dgvAudit.Columns.Contains("AuditId") Then
                dgvAudit.Columns("AuditId").Visible = False
            End If

            UpdatePageButtonLabels()

        Catch ex As Exception
            MsgBox("Error loading audit logs: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadPage Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  FILTER EVENTS — any change resets to page 1
    ' ════════════════════════════════════════════════════════════════
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressFilterEvents Then Return
        ResetAndReload()
    End Sub

    Private Sub cbActionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbActionType.SelectedIndexChanged
        If _suppressFilterEvents Then Return
        ResetAndReload()
    End Sub

    Private Sub cbForms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbForms.SelectedIndexChanged
        If _suppressFilterEvents Then Return
        ResetAndReload()
    End Sub

    Private Sub dtpFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged
        If _suppressFilterEvents Then Return
        ResetAndReload()
    End Sub

    Private Sub dtpLatest_ValueChanged(sender As Object, e As EventArgs) Handles dtpLatest.ValueChanged
        If _suppressFilterEvents Then Return
        ResetAndReload()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ResetAndReload()
    End Sub

    Private Sub ResetAndReload()
        _pageOffset = 0
        LoadPage(1)
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  REMOVE FILTER
    ' ════════════════════════════════════════════════════════════════
    Private Sub btnRemoveFilter_Click(sender As Object, e As EventArgs) Handles btnRemoveFilter.Click
        Try
            _suppressFilterEvents = True

            txtSearch.Text = ""
            cbActionType.SelectedIndex = 0
            cbForms.SelectedIndex = 0
            dtpFrom.Value = Now.AddDays(-30)
            dtpLatest.Value = Now

            _suppressFilterEvents = False

            ResetAndReload()

        Catch ex As Exception
            _suppressFilterEvents = False
            Debug.WriteLine("btnRemoveFilter_Click Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  PAGINATION BUTTON CLICKS
    ' ════════════════════════════════════════════════════════════════
    Private Sub btnPage1_Click(sender As Object, e As EventArgs) Handles btnPage1.Click
        Dim targetPage As Integer
        If Integer.TryParse(btnPage1.Text, targetPage) Then LoadPage(targetPage)
    End Sub

    Private Sub btnPage2_Click(sender As Object, e As EventArgs) Handles btnPage2.Click
        Dim targetPage As Integer
        If Integer.TryParse(btnPage2.Text, targetPage) Then LoadPage(targetPage)
    End Sub

    Private Sub btnPage3_Click(sender As Object, e As EventArgs) Handles btnPage3.Click
        Dim targetPage As Integer
        If Integer.TryParse(btnPage3.Text, targetPage) Then LoadPage(targetPage)
    End Sub

    ''' <summary>
    ''' Next: shifts the page window forward by 3.
    ''' e.g. [1,2,3] → [4,5,6] → [7,8,9]
    ''' </summary>
    Private Sub btnPageNext_Click(sender As Object, e As EventArgs) Handles btnPageNext.Click
        Try
            Dim filters As AuditLogic.AuditFilter = GetCurrentFilters()
            Dim totalPages As Integer = _logic.GetTotalPages(filters)

            Dim newOffset As Integer = _pageOffset + 3
            If newOffset + 1 <= totalPages Then
                _pageOffset = newOffset
                LoadPage(_pageOffset + 1)
            End If
        Catch ex As Exception
            Debug.WriteLine("btnPageNext_Click Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  EXPORT — EXCEL (.xls via HTML table stream)
    ' ════════════════════════════════════════════════════════════════
    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Try
            Dim filters As AuditLogic.AuditFilter = GetCurrentFilters()
            Dim allData As DataTable = _logic.GetAllFilteredAuditLogs(filters)
            If allData.Rows.Count = 0 Then
                MsgBox("No records to export.", MsgBoxStyle.Information, "Export Excel")
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Title = "Export Audit Log — Excel"
                sfd.Filter = "Excel Workbook (*.xls)|*.xls"
                sfd.FileName = "AuditLog_" & Format(Now, "yyyyMMdd_HHmmss")

                If sfd.ShowDialog() = DialogResult.OK Then
                    Dim html As New StringBuilder()
                    html.AppendLine("<html><head><meta charset=""UTF-8""></head><body>")
                    html.AppendLine("<h2>Barangay Maly — Audit Log Export</h2>")
                    html.AppendLine("<p>Exported: " & Format(Now, "MMMM dd, yyyy hh:mm tt") &
                                    " | By: " & LogInForm.CurrentUsername & "</p>")
                    html.AppendLine("<table border=""1"" cellpadding=""5"" cellspacing=""0"">")

                    ' Header
                    html.AppendLine("<thead><tr style=""background:#2c5f2e;color:#fff;"">")
                    For Each col As DataColumn In allData.Columns
                        html.AppendLine("<th>" & System.Net.WebUtility.HtmlEncode(col.ColumnName) & "</th>")
                    Next
                    html.AppendLine("</tr></thead><tbody>")

                    ' Rows
                    Dim rowNum As Integer = 0
                    For Each row As DataRow In allData.Rows
                        Dim bg As String = If(rowNum Mod 2 = 0, "#f0fff4", "#ffffff")
                        html.AppendLine("<tr style=""background:" & bg & ";"">")
                        For Each cell As Object In row.ItemArray
                            Dim cellVal As String = If(cell Is Nothing OrElse IsDBNull(cell), "", cell.ToString())
                            html.AppendLine("<td>" & System.Net.WebUtility.HtmlEncode(cellVal) & "</td>")
                        Next
                        html.AppendLine("</tr>")
                        rowNum += 1
                    Next

                    html.AppendLine("</tbody></table></body></html>")
                    File.WriteAllText(sfd.FileName, html.ToString(), Encoding.UTF8)

                    ' Audit log
                    GlobalAuditLogger.Log("audit_form", "EXPORT_AUDIT_LOGS",
                        LogInForm.CurrentUsername & " exported system audit history.")

                    MsgBox("Excel export saved successfully!" & Environment.NewLine & sfd.FileName,
                           MsgBoxStyle.Information, "Export Complete")
                End If
            End Using

        Catch ex As Exception
            MsgBox("Export failed: " & ex.Message, MsgBoxStyle.Critical, "Export Error")
            Debug.WriteLine("btnExportExcel_Click Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  EXPORT — CSV
    ' ════════════════════════════════════════════════════════════════
    Private Sub btnExportCSV_Click(sender As Object, e As EventArgs) Handles btnExportCSV.Click
        Try
            Dim filters As AuditLogic.AuditFilter = GetCurrentFilters()
            Dim allData As DataTable = _logic.GetAllFilteredAuditLogs(filters)
            If allData.Rows.Count = 0 Then
                MsgBox("No records to export.", MsgBoxStyle.Information, "Export CSV")
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Title = "Export Audit Log — CSV"
                sfd.Filter = "CSV File (*.csv)|*.csv"
                sfd.FileName = "AuditLog_" & Format(Now, "yyyyMMdd_HHmmss")

                If sfd.ShowDialog() = DialogResult.OK Then
                    Dim csv As New StringBuilder()

                    ' Header row
                    Dim headers As New List(Of String)()
                    For Each col As DataColumn In allData.Columns
                        headers.Add(EscapeCsvField(col.ColumnName))
                    Next
                    csv.AppendLine(String.Join(",", headers))

                    ' Data rows
                    For Each row As DataRow In allData.Rows
                        Dim fields As New List(Of String)()
                        For Each cell As Object In row.ItemArray
                            Dim cellVal As String = If(cell Is Nothing OrElse IsDBNull(cell), "", cell.ToString())
                            fields.Add(EscapeCsvField(cellVal))
                        Next
                        csv.AppendLine(String.Join(",", fields))
                    Next

                    File.WriteAllText(sfd.FileName, csv.ToString(), New UTF8Encoding(True))

                    GlobalAuditLogger.Log("audit_form", "EXPORT_AUDIT_LOGS",
                        LogInForm.CurrentUsername & " exported system audit history.")

                    MsgBox("CSV export saved successfully!" & Environment.NewLine & sfd.FileName,
                           MsgBoxStyle.Information, "Export Complete")
                End If
            End Using

        Catch ex As Exception
            MsgBox("Export failed: " & ex.Message, MsgBoxStyle.Critical, "Export Error")
            Debug.WriteLine("btnExportCSV_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>Wraps a CSV field in quotes if it contains commas, quotes, or newlines.</summary>
    Private Function EscapeCsvField(value As String) As String
        If value.Contains(",") OrElse value.Contains("""") OrElse value.Contains(Environment.NewLine) Then
            Return """" & value.Replace("""", """""") & """"
        End If
        Return value
    End Function

    ' ════════════════════════════════════════════════════════════════
    '  EXPORT — PDF (GDI+ PrintDocument drawing canvas)
    ' ════════════════════════════════════════════════════════════════
    Private _pdfRows As List(Of String())
    Private _pdfHeaders As List(Of String)
    Private _pdfCurrentRow As Integer
    Private _pdfPageNum As Integer

    Private Sub btnExportPdf_Click(sender As Object, e As EventArgs) Handles btnExportPdf.Click
        Try
            Dim filters As AuditLogic.AuditFilter = GetCurrentFilters()
            Dim allData As DataTable = _logic.GetAllFilteredAuditLogs(filters)
            If allData.Rows.Count = 0 Then
                MsgBox("No records to export.", MsgBoxStyle.Information, "Export PDF")
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Title = "Export Audit Log — PDF"
                sfd.Filter = "PDF File (*.pdf)|*.pdf"
                sfd.FileName = "AuditLog_" & Format(Now, "yyyyMMdd_HHmmss")

                If sfd.ShowDialog() = DialogResult.OK Then
                    ' Build row snapshot for the print handler
                    _pdfHeaders = New List(Of String)()
                    For Each col As DataColumn In allData.Columns
                        _pdfHeaders.Add(col.ColumnName)
                    Next

                    _pdfRows = New List(Of String())()
                    For Each row As DataRow In allData.Rows
                        Dim cells(allData.Columns.Count - 1) As String
                        For i As Integer = 0 To allData.Columns.Count - 1
                            cells(i) = If(row(i) Is Nothing OrElse IsDBNull(row(i)),
                                          "", row(i).ToString())
                        Next
                        _pdfRows.Add(cells)
                    Next

                    _pdfCurrentRow = 0
                    _pdfPageNum = 1

                    ' Use PrintDocument to produce a PDF-like document
                    ' rendered on a standard GDI+ canvas.
                    Dim pd As New PrintDocument()
                    pd.DefaultPageSettings.Landscape = True
                    pd.DefaultPageSettings.PaperSize =
                        New PaperSize("A4-Landscape", 1169, 827)  ' tenths of an inch

                    AddHandler pd.PrintPage, AddressOf PrintAuditPage

                    ' Save via XPS/PDF using the built-in Microsoft Print to PDF
                    ' printer if available, otherwise use PrintPreview for the user
                    Dim printerName As String = "Microsoft Print to PDF"
                    Dim printerAvailable As Boolean = False

                    For Each prn As String In PrinterSettings.InstalledPrinters
                        If prn = printerName Then
                            printerAvailable = True
                            Exit For
                        End If
                    Next

                    If printerAvailable Then
                        pd.PrinterSettings.PrinterName = printerName
                        pd.PrinterSettings.PrintToFile = True
                        pd.PrinterSettings.PrintFileName = sfd.FileName
                        pd.Print()
                    Else
                        ' Fallback: show print preview dialog
                        Dim ppd As New PrintPreviewDialog()
                        ppd.Document = pd
                        ppd.WindowState = FormWindowState.Maximized
                        ppd.ShowDialog()
                    End If

                    GlobalAuditLogger.Log("audit_form", "EXPORT_AUDIT_LOGS",
                        LogInForm.CurrentUsername & " exported system audit history.")

                    MsgBox("PDF export complete.", MsgBoxStyle.Information, "Export Complete")
                End If
            End Using

        Catch ex As Exception
            MsgBox("PDF export failed: " & ex.Message, MsgBoxStyle.Critical, "Export Error")
            Debug.WriteLine("btnExportPdf_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' GDI+ PrintPage handler — draws the audit table onto the print canvas.
    ''' Handles multi-page continuation via e.HasMorePages.
    ''' </summary>
    Private Sub PrintAuditPage(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim pageRect As Rectangle = e.MarginBounds

        Dim headerFont As New Font("Arial", 16, FontStyle.Bold)
        Dim subFont As New Font("Arial", 8, FontStyle.Regular)
        Dim colFont As New Font("Arial", 8, FontStyle.Bold)
        Dim cellFont As New Font("Arial", 7, FontStyle.Regular)
        Dim greenBrush As New SolidBrush(Color.FromArgb(44, 95, 46))
        Dim whiteBrush As New SolidBrush(Color.White)
        Dim blackBrush As New SolidBrush(Color.Black)
        Dim altBrush As New SolidBrush(Color.FromArgb(240, 255, 244))
        Dim linePen As New Pen(Color.FromArgb(180, 220, 180))

        Dim y As Integer = pageRect.Top

        ' ── Page header (first page only) ────────────────────────────
        If _pdfPageNum = 1 Then
            g.DrawString("Barangay Maly — Audit Log", headerFont, greenBrush,
                         New PointF(pageRect.Left, y))
            y += 22
            g.DrawString("Exported: " & Format(Now, "MMMM dd, yyyy hh:mm tt") &
                         "   |   By: " & LogInForm.CurrentUsername,
                         subFont, blackBrush, New PointF(pageRect.Left, y))
            y += 20
        End If

        ' ── Column header row ─────────────────────────────────────────
        Dim colCount As Integer = _pdfHeaders.Count
        Dim colWidth As Integer = pageRect.Width \ colCount
        Dim rowHeight As Integer = 18

        Dim hdrRect As New Rectangle(pageRect.Left, y, pageRect.Width, rowHeight)
        g.FillRectangle(greenBrush, hdrRect)
        For i As Integer = 0 To colCount - 1
            Dim cellRect As New Rectangle(pageRect.Left + i * colWidth, y, colWidth, rowHeight)
            g.DrawString(_pdfHeaders(i), colFont, whiteBrush,
                         New RectangleF(cellRect.X + 2, cellRect.Y + 2, cellRect.Width - 4, rowHeight))
        Next
        y += rowHeight

        ' ── Data rows ────────────────────────────────────────────────
        Dim rowIdx As Integer = _pdfCurrentRow
        Do While rowIdx < _pdfRows.Count
            If y + rowHeight > pageRect.Bottom Then Exit Do

            Dim cells() As String = _pdfRows(rowIdx)
            Dim rowBg As Brush = If(rowIdx Mod 2 = 0, altBrush, whiteBrush)
            Dim rowRect As New Rectangle(pageRect.Left, y, pageRect.Width, rowHeight)
            g.FillRectangle(rowBg, rowRect)

            For i As Integer = 0 To colCount - 1
                If i < cells.Length Then
                    Dim cellRect As New Rectangle(pageRect.Left + i * colWidth, y, colWidth, rowHeight)
                    g.DrawString(cells(i), cellFont, blackBrush,
                                 New RectangleF(cellRect.X + 2, cellRect.Y + 2, cellRect.Width - 4, rowHeight))
                    g.DrawRectangle(linePen, cellRect)
                End If
            Next
            y += rowHeight
            rowIdx += 1
        Loop

        _pdfCurrentRow = rowIdx
        _pdfPageNum += 1
        e.HasMorePages = (_pdfCurrentRow < _pdfRows.Count)

        ' Cleanup GDI objects
        headerFont.Dispose()
        subFont.Dispose()
        colFont.Dispose()
        cellFont.Dispose()
        greenBrush.Dispose()
        whiteBrush.Dispose()
        blackBrush.Dispose()
        altBrush.Dispose()
        linePen.Dispose()
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  FORM CLOSING — cleanup
    ' ════════════════════════════════════════════════════════════════
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        Try
            If responsiveManager IsNot Nothing Then responsiveManager.Cleanup()
            MyBase.OnFormClosing(e)
        Catch ex As Exception
            Debug.WriteLine("Audit_Form OnFormClosing Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  VISUAL HELPERS  (preserved from original file exactly)
    ' ════════════════════════════════════════════════════════════════
    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)
        Dim brush As New LinearGradientBrush(
            New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
        Dim panelLocal = pnl
        AddHandler panelLocal.Paint, Sub(s, ev)
                                         ev.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                                     End Sub
    End Sub

    Private Sub RoundButtonCorners(ByRef btn As Button, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        btn.Region = New Region(p)
        Dim btnLocal = btn
        AddHandler btn.Resize, Sub(s, args)
                                   Dim newPath As New GraphicsPath()
                                   newPath.AddArc(0, 0, radius, radius, 180, 90)
                                   newPath.AddArc(btnLocal.Width - radius, 0, radius, radius, 270, 90)
                                   newPath.AddArc(btnLocal.Width - radius, btnLocal.Height - radius, radius, radius, 0, 90)
                                   newPath.AddArc(0, btnLocal.Height - radius, radius, radius, 90, 90)
                                   newPath.CloseFigure()
                                   btnLocal.Region = New Region(newPath)
                               End Sub
    End Sub

End Class
