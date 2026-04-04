Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for Audit_Form
''' Handles all layout calculations, positioning, and font scaling
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

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As Audit_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior
    ''' </summary>
    Public Sub Initialize()
        ' === CRITICAL: Override Designer's fixed size on fillpanel ===
        _form.fillpanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.fillpanel.Location = New Point(0, 0)

        ' === MAIN CONTAINER - FILL AVAILABLE SPACE ===
        _form.fillpanel.Dock = DockStyle.Fill
        _form.fillpanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event to recalculate layout when window resizes ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === CRITICAL: Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Calculate and apply layout for the first time ===
        _form.fillpanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    ''' <summary>
    ''' CRITICAL: Fires when Windows resolution changes
    ''' </summary>
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Fires when form window resizes
    ''' </summary>
    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Exit Sub
        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    ''' <summary>
    ''' Timer tick - recalculates layout ONCE after resize stops
    ''' </summary>
    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Calculate positions and apply layout based on current form size
    ''' Uses PERCENTAGES for positioning and sizing
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        ' === Use form's actual client size ===
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' === Calculate scale factor for font sizing ===
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        ' === Update fillpanel ===
        _form.fillpanel.Size = New Size(panelWidth, panelHeight)
        _form.fillpanel.Location = New Point(0, 0)

        ' === TITLE SECTION ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)

        ' === DIVIDER LINE 1 ===
        PositionDividerLine1(panelWidth, panelHeight)

        ' === SEARCH SECTION ===
        PositionSearchSection(panelWidth, panelHeight, scaleFactor)

        ' === FILTER SECTION ===
        PositionFilterSection(panelWidth, panelHeight, scaleFactor)

        ' === DIVIDER LINE 2 ===
        PositionDividerLine2(panelWidth, panelHeight)

        ' === DATA GRID VIEW ===
        PositionDataGridView(panelWidth, panelHeight)

        ' === PAGINATION SECTION ===
        PositionPaginationSection(panelWidth, panelHeight, scaleFactor)

        ' === EXPORT BUTTONS ===
        PositionExportButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position "Audit Logs" title
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(20, 19) on 1700x1004
        _form.lblAuditLogs.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.019))
        _form.lblAuditLogs.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblAuditLogs.Font = New Font("Arial", 21.75F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position first horizontal divider line
    ''' </summary>
    Private Sub PositionDividerLine1(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(0, 70), Size(1700, 2)
        _form.LinePnl.Location = New Point(0, CInt(panelHeight * 0.07))
        _form.LinePnl.Size = New Size(panelWidth, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position search section (label, textbox, search button)
    ''' </summary>
    Private Sub PositionSearchSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Search Label - Designer: Location(103, 110)
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.061), CInt(panelHeight * 0.11))
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Search TextBox - Designer: Location(240, 106), Size(1177, 32)
        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.141), CInt(panelHeight * 0.106))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.692), CInt(panelHeight * 0.032))
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtSearch.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        ' Search Button - Designer: Location(1423, 106), Size(151, 32)
        _form.btnSearch.Location = New Point(CInt(panelWidth * 0.837), CInt(panelHeight * 0.106))
        _form.btnSearch.Size = New Size(CInt(panelWidth * 0.089), CInt(panelHeight * 0.032))
        _form.btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnSearch.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position filter section (Action Type, Date Range, Forms, Remove Filter)
    ''' </summary>
    Private Sub PositionFilterSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftLabelMargin As Integer = CInt(panelWidth * 0.061)
        Dim leftFieldMargin As Integer = CInt(panelWidth * 0.141)
        Dim leftFieldWidth As Integer = CInt(panelWidth * 0.264)

        Dim rightLabelMargin As Integer = CInt(panelWidth * 0.434)
        Dim rightFieldMargin As Integer = CInt(panelWidth * 0.509)
        Dim rightFieldWidth As Integer = CInt(panelWidth * 0.18)

        ' === ACTION TYPE ===
        ' Label - Designer: Location(103, 181)
        _form.lblActionType.Location = New Point(leftLabelMargin, CInt(panelHeight * 0.18))
        _form.lblActionType.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblActionType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' ComboBox - Designer: Location(240, 178), Size(449, 31)
        _form.cbActionType.Location = New Point(leftFieldMargin, CInt(panelHeight * 0.177))
        _form.cbActionType.Size = New Size(leftFieldWidth, CInt(panelHeight * 0.031))
        _form.cbActionType.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.cbActionType.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === DATE RANGE ===
        ' Label - Designer: Location(737, 182)
        _form.lblDateRange.Location = New Point(rightLabelMargin, CInt(panelHeight * 0.181))
        _form.lblDateRange.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblDateRange.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' From DateTimePicker - Designer: Location(866, 178), Size(306, 29)
        _form.dtpFrom.Location = New Point(rightFieldMargin, CInt(panelHeight * 0.177))
        _form.dtpFrom.Size = New Size(rightFieldWidth, CInt(panelHeight * 0.029))
        _form.dtpFrom.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.dtpFrom.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' To Label - Designer: Location(1178, 181)
        _form.lblTo.Location = New Point(CInt(panelWidth * 0.693), CInt(panelHeight * 0.18))
        _form.lblTo.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblTo.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' To DateTimePicker - Designer: Location(1218, 178), Size(306, 29)
        _form.dtpLatest.Location = New Point(CInt(panelWidth * 0.716), CInt(panelHeight * 0.177))
        _form.dtpLatest.Size = New Size(rightFieldWidth, CInt(panelHeight * 0.029))
        _form.dtpLatest.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.dtpLatest.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === FORMS ===
        ' Label - Designer: Location(109, 249)
        _form.lblForms.Location = New Point(leftLabelMargin, CInt(panelHeight * 0.248))
        _form.lblForms.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblForms.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' ComboBox - Designer: Location(240, 245), Size(449, 31)
        _form.cbForms.Location = New Point(leftFieldMargin, CInt(panelHeight * 0.244))
        _form.cbForms.Size = New Size(leftFieldWidth, CInt(panelHeight * 0.031))
        _form.cbForms.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.cbForms.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === REMOVE FILTER BUTTON ===
        ' Designer: Location(941, 244), Size(476, 32)
        _form.btnRemoveFilter.Location = New Point(CInt(panelWidth * 0.554), CInt(panelHeight * 0.243))
        _form.btnRemoveFilter.Size = New Size(CInt(panelWidth * 0.28), CInt(panelHeight * 0.032))
        _form.btnRemoveFilter.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnRemoveFilter.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnRemoveFilter.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position second horizontal divider line
    ''' </summary>
    Private Sub PositionDividerLine2(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(0, 315), Size(1700, 2)
        _form.LinePnl2.Location = New Point(0, CInt(panelHeight * 0.314))
        _form.LinePnl2.Size = New Size(panelWidth, 2)
        _form.LinePnl2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position DataGridView for audit logs
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(26, 337), Size(1650, 528)
        _form.dgvAudit.Location = New Point(CInt(panelWidth * 0.015), CInt(panelHeight * 0.336))
        _form.dgvAudit.Size = New Size(CInt(panelWidth * 0.971), CInt(panelHeight * 0.526))
        _form.dgvAudit.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position pagination section (Pages label and page buttons)
    ''' </summary>
    Private Sub PositionPaginationSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnY As Integer = CInt(panelHeight * 0.865)
        Dim btnHeight As Integer = CInt(panelHeight * 0.033)

        ' Pages Label - Designer: Location(22, 872)
        _form.lblPages.Location = New Point(CInt(panelWidth * 0.013), CInt(panelHeight * 0.869))
        _form.lblPages.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.lblPages.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Page 1 Button - Designer: Location(103, 868), Size(46, 33)
        _form.btnPage1.Location = New Point(CInt(panelWidth * 0.061), btnY)
        _form.btnPage1.Size = New Size(CInt(panelWidth * 0.027), btnHeight)
        _form.btnPage1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPage1.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPage1.Cursor = Cursors.Hand

        ' Page 2 Button - Designer: Location(155, 868), Size(46, 33)
        _form.btnPage2.Location = New Point(CInt(panelWidth * 0.091), btnY)
        _form.btnPage2.Size = New Size(CInt(panelWidth * 0.027), btnHeight)
        _form.btnPage2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPage2.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPage2.Cursor = Cursors.Hand

        ' Page 3 Button - Designer: Location(207, 868), Size(46, 33)
        _form.btnPage3.Location = New Point(CInt(panelWidth * 0.122), btnY)
        _form.btnPage3.Size = New Size(CInt(panelWidth * 0.027), btnHeight)
        _form.btnPage3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPage3.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPage3.Cursor = Cursors.Hand

        ' Next Button - Designer: Location(262, 868), Size(77, 33)
        _form.btnPageNext.Location = New Point(CInt(panelWidth * 0.154), btnY)
        _form.btnPageNext.Size = New Size(CInt(panelWidth * 0.045), btnHeight)
        _form.btnPageNext.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPageNext.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPageNext.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position export buttons (PDF, Excel, CSV)
    ''' </summary>
    Private Sub PositionExportButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.111)
        Dim btnHeight As Integer = CInt(panelHeight * 0.033)
        Dim btnY As Integer = CInt(panelHeight * 0.929)

        ' Export PDF Button - Designer: Location(124, 933), Size(189, 33)
        _form.btnExportPdf.Location = New Point(CInt(panelWidth * 0.073), btnY)
        _form.btnExportPdf.Size = New Size(btnWidth, btnHeight)
        _form.btnExportPdf.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnExportPdf.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnExportPdf.Cursor = Cursors.Hand

        ' Export Excel Button - Designer: Location(741, 933), Size(189, 33)
        _form.btnExportExcel.Location = New Point(CInt(panelWidth * 0.436), btnY)
        _form.btnExportExcel.Size = New Size(btnWidth, btnHeight)
        _form.btnExportExcel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnExportExcel.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnExportExcel.Cursor = Cursors.Hand

        ' Export CSV Button - Designer: Location(1363, 933), Size(189, 33)
        _form.btnExportCSV.Location = New Point(CInt(panelWidth * 0.802), btnY)
        _form.btnExportCSV.Size = New Size(btnWidth, btnHeight)
        _form.btnExportCSV.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnExportCSV.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnExportCSV.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
