Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing

Public Class ReportsMain_Form
    Private responsiveManager As ReportsMainResponsiveManager
    Private reportLogic As New ReportsMainLogic()

    Private currentReportData As DataTable = Nothing
    Private printRowIndex As Integer = 0
    Private selectedReportTypeName As String = ""
    Private selectedReportSubTypeName As String = ""

    Private Sub ReportsMain_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            RoundButtonCorners(btnNewReportType, 20)
            RoundButtonCorners(btnSearch, 20)
            RoundButtonCorners(btnGenerate, 20)
            RoundButtonCorners(btnViewGeneratedReports, 20)

            responsiveManager = New ReportsMainResponsiveManager(Me)
            responsiveManager.Initialize()

            ConfigureDataGridView()
            ConfigureComboBoxes()
            LoadReportTypes()

        Catch ex As Exception
            MsgBox("Error loading reports form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ConfigureComboBoxes()
        cbReportType.DropDownStyle = ComboBoxStyle.DropDownList
        cbReportSubType.DropDownStyle = ComboBoxStyle.DropDownList
        cbDownloadType.DropDownStyle = ComboBoxStyle.DropDownList

        cbDownloadType.Items.Clear()
        cbDownloadType.Items.Add("Excel")
        cbDownloadType.Items.Add("PDF")
        cbDownloadType.SelectedIndex = 0
    End Sub

    Private Sub ConfigureDataGridView()
        dgvReports.AutoGenerateColumns = True
        dgvReports.AllowUserToAddRows = False
        dgvReports.AllowUserToDeleteRows = False
        dgvReports.AllowUserToResizeRows = False
        dgvReports.ReadOnly = True
        dgvReports.RowHeadersVisible = False
        dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReports.MultiSelect = False
        dgvReports.EnableHeadersVisualStyles = False

        dgvReports.BackgroundColor = Color.FromArgb(220, 220, 220)
        dgvReports.GridColor = Color.FromArgb(180, 180, 180)
        dgvReports.ColumnHeadersHeight = 35
        dgvReports.RowTemplate.Height = 30

        dgvReports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        dgvReports.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvReports.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
        dgvReports.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgvReports.DefaultCellStyle.Font = New Font("Arial", 10)
        dgvReports.DefaultCellStyle.ForeColor = Color.Black
        dgvReports.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dgvReports.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        dgvReports.DefaultCellStyle.SelectionForeColor = Color.Black

        dgvReports.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)
    End Sub

    Private Sub LoadReportTypes()
        Try
            ' Reset DataSource before binding to avoid "Cannot bind to new display member" error
            cbReportType.DataSource = Nothing
            cbReportType.Items.Clear()
            cbReportType.DisplayMember = ""
            cbReportType.ValueMember = ""

            Dim dataTable As DataTable = reportLogic.GetReportTypes()

            ' Only bind if the datatable actually has the expected columns
            If dataTable IsNot Nothing AndAlso dataTable.Columns.Contains("ReportTypeName") Then
                cbReportType.DataSource = dataTable
                cbReportType.DisplayMember = "ReportTypeName"
                cbReportType.ValueMember = "ReportTypeId"
                cbReportType.SelectedIndex = -1
            End If

            ' Always reset sub type combo
            cbReportSubType.DataSource = Nothing
            cbReportSubType.Items.Clear()
            cbReportSubType.DisplayMember = ""
            cbReportSubType.ValueMember = ""

        Catch ex As Exception
            MsgBox("Error loading report types: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub cbReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbReportType.SelectedIndexChanged
        Try
            ' Reset sub type combo first to avoid display member binding errors
            cbReportSubType.DataSource = Nothing
            cbReportSubType.Items.Clear()
            cbReportSubType.DisplayMember = ""
            cbReportSubType.ValueMember = ""

            If cbReportType.SelectedIndex < 0 OrElse cbReportType.SelectedValue Is Nothing Then Return
            If TypeOf cbReportType.SelectedValue Is DataRowView Then Return

            Dim reportTypeId As Integer = CInt(cbReportType.SelectedValue)
            Dim dataTable As DataTable = reportLogic.GetReportSubTypes(reportTypeId)

            If dataTable IsNot Nothing AndAlso dataTable.Columns.Contains("ReportSubTypeName") Then
                cbReportSubType.DataSource = dataTable
                cbReportSubType.DisplayMember = "ReportSubTypeName"
                cbReportSubType.ValueMember = "ReportsSubTypeId"
                cbReportSubType.SelectedIndex = -1
            End If

        Catch ex As Exception
            Debug.WriteLine("cbReportType_SelectedIndexChanged Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadPreviewData()
    End Sub

    Private Sub LoadPreviewData()
        Try
            Dim validationError As String = ValidateSelections()

            If Not String.IsNullOrWhiteSpace(validationError) Then
                MsgBox(validationError, MsgBoxStyle.Information, "Validation")
                Return
            End If

            Dim reportTypeId As Integer = CInt(cbReportType.SelectedValue)
            Dim reportSubTypeId As Integer = CInt(cbReportSubType.SelectedValue)

            currentReportData = reportLogic.GetReportPreview(
                reportTypeId,
                reportSubTypeId,
                dtpFrom.Value.Date,
                dtpLatest.Value.Date,
                txtSearch.Text.Trim()
            )

            dgvReports.DataSource = currentReportData
            FormatPreviewGrid()

        Catch ex As Exception
            MsgBox("Error loading report preview: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub FormatPreviewGrid()
        If dgvReports.Columns.Contains("ResidentId") Then
            dgvReports.Columns("ResidentId").Visible = False
        End If

        If dgvReports.Columns.Contains("FirstName") Then dgvReports.Columns("FirstName").HeaderText = "First Name"
        If dgvReports.Columns.Contains("LastName") Then dgvReports.Columns("LastName").HeaderText = "Last Name"
        If dgvReports.Columns.Contains("CivilStatus") Then dgvReports.Columns("CivilStatus").HeaderText = "Civil Status"
        If dgvReports.Columns.Contains("ContactNumber") Then dgvReports.Columns("ContactNumber").HeaderText = "Contact Number"
        If dgvReports.Columns.Contains("HouseholdNumber") Then dgvReports.Columns("HouseholdNumber").HeaderText = "Household Number"
    End Sub

    Private Function ValidateSelections() As String
        Dim hasReportType As Boolean =
            cbReportType.SelectedIndex >= 0 AndAlso
            cbReportType.SelectedValue IsNot Nothing AndAlso
            Not TypeOf cbReportType.SelectedValue Is DataRowView

        Dim hasReportSubType As Boolean =
            cbReportSubType.SelectedIndex >= 0 AndAlso
            cbReportSubType.SelectedValue IsNot Nothing AndAlso
            Not TypeOf cbReportSubType.SelectedValue Is DataRowView

        Dim downloadType As String = If(cbDownloadType.SelectedItem Is Nothing, "", cbDownloadType.SelectedItem.ToString())

        Return reportLogic.ValidateReportSelection(
            hasReportType,
            hasReportSubType,
            downloadType,
            dtpFrom.Value.Date,
            dtpLatest.Value.Date
        )
    End Function

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Try
            Dim validationError As String = ValidateSelections()

            If Not String.IsNullOrWhiteSpace(validationError) Then
                MsgBox(validationError, MsgBoxStyle.Information, "Validation")
                Return
            End If

            LoadPreviewData()

            If currentReportData Is Nothing OrElse currentReportData.Rows.Count = 0 Then
                MsgBox("No records found for this report.", MsgBoxStyle.Information, "No Data")
                Return
            End If

            selectedReportTypeName = cbReportType.Text
            selectedReportSubTypeName = cbReportSubType.Text
            printRowIndex = 0

            PrintDialogReports.Document = PrintDocuReports

            If PrintDialogReports.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            PrintPreviewDialogReports.Document = PrintDocuReports
            PrintPreviewDialogReports.Width = 1000
            PrintPreviewDialogReports.Height = 700
            PrintPreviewDialogReports.ShowDialog()

            Dim downloadType As String = cbDownloadType.SelectedItem.ToString()
            Dim fileName As String = reportLogic.BuildFileName(selectedReportTypeName, selectedReportSubTypeName, downloadType)
            Dim filePath As String = reportLogic.BuildFilePath(fileName, downloadType)

            If downloadType.ToLower() = "excel" Then
                reportLogic.SaveCsvFile(filePath, currentReportData)
            End If

            Dim status As String = If(downloadType.ToLower() = "excel", "Generated", "Previewed / Printed")

            reportLogic.SaveGeneratedReport(
                CInt(cbReportType.SelectedValue),
                CInt(cbReportSubType.SelectedValue),
                LogInForm.CurrentUserID,
                fileName,
                filePath,
                status
            )

            MsgBox("Report generated successfully.", MsgBoxStyle.Information, "Success")

        Catch ex As Exception
            MsgBox("Error generating report: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub PrintDocuReports_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocuReports.PrintPage
        Dim fontTitle As New Font("Arial", 14, FontStyle.Bold)
        Dim fontHeader As New Font("Arial", 10, FontStyle.Bold)
        Dim fontBody As New Font("Arial", 9, FontStyle.Regular)

        Dim y As Integer = 50
        Dim leftMargin As Integer = 50

        e.Graphics.DrawString("Barangay Maly Report", fontTitle, Brushes.Black, leftMargin, y)
        y += 30
        e.Graphics.DrawString("Report Type: " & selectedReportTypeName, fontHeader, Brushes.Black, leftMargin, y)
        y += 22
        e.Graphics.DrawString("Report Sub Type: " & selectedReportSubTypeName, fontHeader, Brushes.Black, leftMargin, y)
        y += 22
        e.Graphics.DrawString("Date Range: " & dtpFrom.Value.ToString("MMMM dd, yyyy") & " to " & dtpLatest.Value.ToString("MMMM dd, yyyy"), fontHeader, Brushes.Black, leftMargin, y)
        y += 35

        e.Graphics.DrawString("First Name", fontHeader, Brushes.Black, leftMargin, y)
        e.Graphics.DrawString("Last Name", fontHeader, Brushes.Black, leftMargin + 140, y)
        e.Graphics.DrawString("Sex", fontHeader, Brushes.Black, leftMargin + 280, y)
        e.Graphics.DrawString("Civil Status", fontHeader, Brushes.Black, leftMargin + 350, y)
        e.Graphics.DrawString("Household #", fontHeader, Brushes.Black, leftMargin + 480, y)

        y += 25

        While printRowIndex < currentReportData.Rows.Count
            Dim row As DataRow = currentReportData.Rows(printRowIndex)

            e.Graphics.DrawString(row("FirstName").ToString(), fontBody, Brushes.Black, leftMargin, y)
            e.Graphics.DrawString(row("LastName").ToString(), fontBody, Brushes.Black, leftMargin + 140, y)
            e.Graphics.DrawString(row("Sex").ToString(), fontBody, Brushes.Black, leftMargin + 280, y)
            e.Graphics.DrawString(row("CivilStatus").ToString(), fontBody, Brushes.Black, leftMargin + 350, y)
            e.Graphics.DrawString(row("HouseholdNumber").ToString(), fontBody, Brushes.Black, leftMargin + 480, y)

            y += 22
            printRowIndex += 1

            If y > e.MarginBounds.Bottom Then
                e.HasMorePages = True
                Return
            End If
        End While

        e.HasMorePages = False
        printRowIndex = 0
    End Sub

    Private Sub btnViewGeneratedReports_Click(sender As Object, e As EventArgs) Handles btnViewGeneratedReports.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim viewReport As New ViewGeneratedReports_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(viewReport)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnNewReportType_Click(sender As Object, e As EventArgs) Handles btnNewReportType.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim newReportTypeDialog As New NewReportType_Form()
                newReportTypeDialog.ShowDialog()
                LoadReportTypes()
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)

        AddHandler pnl.Paint,
            Sub(sender, e)
                Using brush As New LinearGradientBrush(New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
                    e.Graphics.FillRectangle(brush, pnl.ClientRectangle)
                End Using
            End Sub
    End Sub

    Private Sub RoundButtonCorners(btn As Button, ByVal radius As Integer)
        ApplyButtonRoundedRegion(btn, radius)

        AddHandler btn.Resize,
        Sub(s, args)
            ApplyButtonRoundedRegion(btn, radius)
        End Sub
    End Sub

    Private Sub ApplyButtonRoundedRegion(btn As Button, radius As Integer)
        If btn Is Nothing Then Return
        If btn.Width <= 0 OrElse btn.Height <= 0 Then Return

        Using p As New GraphicsPath()
            p.AddArc(0, 0, radius, radius, 180, 90)
            p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
            p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
            p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
            p.CloseFigure()

            btn.Region = New Region(p)
        End Using
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If

        MyBase.OnFormClosing(e)
    End Sub

End Class