Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for Audit_Form.
''' Handles all layout calculations, positioning, and font scaling.
''' UPDATED: PositionDataGridView now computes its top edge from
''' the live filter section bottom, preventing overlap at 768p.
''' </summary>
Public Class AuditResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As Audit_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    Public Sub New(form As Audit_Form)
        _form = form
    End Sub

    Public Sub Initialize()
        _form.fillpanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.fillpanel.Location = New Point(0, 0)
        _form.fillpanel.Dock = DockStyle.Fill
        _form.fillpanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                                   AnchorStyles.Left Or AnchorStyles.Right

        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick
        AddHandler _form.Resize, AddressOf Form_Resize
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        _form.fillpanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Exit Sub
        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Master layout pass — calculates all control positions using
    ''' resolution-agnostic percentage coordinates.
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        _form.fillpanel.Size = New Size(panelWidth, panelHeight)
        _form.fillpanel.Location = New Point(0, 0)

        PositionTitleSection(panelWidth, panelHeight, scaleFactor)
        PositionDividerLine1(panelWidth, panelHeight)
        PositionSearchSection(panelWidth, panelHeight, scaleFactor)
        PositionFilterSection(panelWidth, panelHeight, scaleFactor)
        PositionDividerLine2(panelWidth, panelHeight)
        PositionDataGridView(panelWidth, panelHeight)
        PositionPaginationSection(panelWidth, panelHeight, scaleFactor)
        PositionExportButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ' ── Title ────────────────────────────────────────────────────────
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        _form.lblAuditLogs.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.019))
        _form.lblAuditLogs.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblAuditLogs.Font = New Font("Arial", 21.75F * scaleFactor, FontStyle.Bold)
    End Sub

    ' ── Divider 1 ────────────────────────────────────────────────────
    Private Sub PositionDividerLine1(panelWidth As Integer, panelHeight As Integer)
        _form.LinePnl.Location = New Point(0, CInt(panelHeight * 0.07))
        _form.LinePnl.Size = New Size(panelWidth, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ' ── Search row ───────────────────────────────────────────────────
    Private Sub PositionSearchSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.061), CInt(panelHeight * 0.11))
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.141), CInt(panelHeight * 0.106))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.692), CInt(panelHeight * 0.032))
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtSearch.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        _form.btnSearch.Location = New Point(CInt(panelWidth * 0.837), CInt(panelHeight * 0.106))
        _form.btnSearch.Size = New Size(CInt(panelWidth * 0.089), CInt(panelHeight * 0.032))
        _form.btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnSearch.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Cursor = Cursors.Hand
    End Sub

    ' ── Filter row (Action Type | Date Range | Forms | Remove) ───────
    Private Sub PositionFilterSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftLabelMargin As Integer = CInt(panelWidth * 0.061)
        Dim leftFieldMargin As Integer = CInt(panelWidth * 0.141)
        Dim leftFieldWidth As Integer = CInt(panelWidth * 0.264)

        Dim rightLabelMargin As Integer = CInt(panelWidth * 0.434)
        Dim rightFieldMargin As Integer = CInt(panelWidth * 0.509)
        Dim rightFieldWidth As Integer = CInt(panelWidth * 0.18)

        ' ── Row 1 : Action Type + Date Range ─────────────────────────
        _form.lblActionType.Location = New Point(leftLabelMargin, CInt(panelHeight * 0.18))
        _form.lblActionType.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblActionType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        _form.cbActionType.Location = New Point(leftFieldMargin, CInt(panelHeight * 0.177))
        _form.cbActionType.Size = New Size(leftFieldWidth, CInt(panelHeight * 0.031))
        _form.cbActionType.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.cbActionType.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        _form.lblDateRange.Location = New Point(rightLabelMargin, CInt(panelHeight * 0.181))
        _form.lblDateRange.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblDateRange.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        _form.dtpFrom.Location = New Point(rightFieldMargin, CInt(panelHeight * 0.177))
        _form.dtpFrom.Size = New Size(rightFieldWidth, CInt(panelHeight * 0.029))
        _form.dtpFrom.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.dtpFrom.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        _form.lblTo.Location = New Point(CInt(panelWidth * 0.693), CInt(panelHeight * 0.18))
        _form.lblTo.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblTo.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        _form.dtpLatest.Location = New Point(CInt(panelWidth * 0.716), CInt(panelHeight * 0.177))
        _form.dtpLatest.Size = New Size(rightFieldWidth, CInt(panelHeight * 0.029))
        _form.dtpLatest.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.dtpLatest.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' ── Row 2 : Forms + Remove Filter ────────────────────────────
        _form.lblForms.Location = New Point(leftLabelMargin, CInt(panelHeight * 0.248))
        _form.lblForms.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblForms.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        _form.cbForms.Location = New Point(leftFieldMargin, CInt(panelHeight * 0.244))
        _form.cbForms.Size = New Size(leftFieldWidth, CInt(panelHeight * 0.031))
        _form.cbForms.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.cbForms.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' Remove Filter spans the right half of row 2
        _form.btnRemoveFilter.Location = New Point(CInt(panelWidth * 0.554), CInt(panelHeight * 0.243))
        _form.btnRemoveFilter.Size = New Size(CInt(panelWidth * 0.28), CInt(panelHeight * 0.032))
        _form.btnRemoveFilter.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnRemoveFilter.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnRemoveFilter.Cursor = Cursors.Hand
    End Sub

    ' ── Divider 2 ────────────────────────────────────────────────────
    Private Sub PositionDividerLine2(panelWidth As Integer, panelHeight As Integer)
        _form.LinePnl2.Location = New Point(0, CInt(panelHeight * 0.314))
        _form.LinePnl2.Size = New Size(panelWidth, 2)
        _form.LinePnl2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' UPDATED: DataGridView top edge is derived from LinePnl2's bottom + margin.
    ''' Bottom edge is pinned 12% above the form bottom to leave room for pagination
    ''' and export buttons without overlapping at 768p height.
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        ' Top edge: just below divider 2 with a small gap
        Dim gridTop As Integer = CInt(panelHeight * 0.314) + 10

        ' Bottom edge: leave 14% of height for pagination (2%) + gap + export row (3.5%)
        ' Total reservation = ~13% from bottom = 87% of height as bottom edge
        Dim gridBottom As Integer = CInt(panelHeight * 0.862)
        Dim gridHeight As Integer = Math.Max(60, gridBottom - gridTop)

        _form.dgvAudit.Location = New Point(CInt(panelWidth * 0.015), gridTop)
        _form.dgvAudit.Size = New Size(CInt(panelWidth * 0.971), gridHeight)
        _form.dgvAudit.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                                   AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ' ── Pagination row ───────────────────────────────────────────────
    Private Sub PositionPaginationSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnY As Integer = CInt(panelHeight * 0.868)
        Dim btnHeight As Integer = CInt(panelHeight * 0.033)

        _form.lblPages.Location = New Point(CInt(panelWidth * 0.013), CInt(panelHeight * 0.872))
        _form.lblPages.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.lblPages.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Page 1
        _form.btnPage1.Location = New Point(CInt(panelWidth * 0.061), btnY)
        _form.btnPage1.Size = New Size(CInt(panelWidth * 0.027), btnHeight)
        _form.btnPage1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPage1.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPage1.Cursor = Cursors.Hand

        ' Page 2
        _form.btnPage2.Location = New Point(CInt(panelWidth * 0.091), btnY)
        _form.btnPage2.Size = New Size(CInt(panelWidth * 0.027), btnHeight)
        _form.btnPage2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPage2.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPage2.Cursor = Cursors.Hand

        ' Page 3
        _form.btnPage3.Location = New Point(CInt(panelWidth * 0.122), btnY)
        _form.btnPage3.Size = New Size(CInt(panelWidth * 0.027), btnHeight)
        _form.btnPage3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPage3.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPage3.Cursor = Cursors.Hand

        ' Next
        _form.btnPageNext.Location = New Point(CInt(panelWidth * 0.154), btnY)
        _form.btnPageNext.Size = New Size(CInt(panelWidth * 0.045), btnHeight)
        _form.btnPageNext.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPageNext.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPageNext.Cursor = Cursors.Hand
    End Sub

    ' ── Export buttons row ───────────────────────────────────────────
    Private Sub PositionExportButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.111)
        Dim btnHeight As Integer = CInt(panelHeight * 0.033)
        Dim btnY As Integer = CInt(panelHeight * 0.929)

        _form.btnExportPdf.Location = New Point(CInt(panelWidth * 0.073), btnY)
        _form.btnExportPdf.Size = New Size(btnWidth, btnHeight)
        _form.btnExportPdf.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnExportPdf.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnExportPdf.Cursor = Cursors.Hand

        _form.btnExportExcel.Location = New Point(CInt(panelWidth * 0.436), btnY)
        _form.btnExportExcel.Size = New Size(btnWidth, btnHeight)
        _form.btnExportExcel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnExportExcel.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnExportExcel.Cursor = Cursors.Hand

        _form.btnExportCSV.Location = New Point(CInt(panelWidth * 0.802), btnY)
        _form.btnExportCSV.Size = New Size(btnWidth, btnHeight)
        _form.btnExportCSV.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnExportCSV.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnExportCSV.Cursor = Cursors.Hand
    End Sub

    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
