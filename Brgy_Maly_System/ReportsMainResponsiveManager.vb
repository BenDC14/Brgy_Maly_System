Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for ReportsMain_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class ReportsMainResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As ReportsMain_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As ReportsMain_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior
    ''' </summary>
    Public Sub Initialize()
        ' === CRITICAL: Override Designer's fixed size on FillPanel ===
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.FillPanel.Location = New Point(0, 0)

        ' === MAIN CONTAINER - FILL AVAILABLE SPACE ===
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event to recalculate layout when window resizes ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === CRITICAL: Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Calculate and apply layout for the first time ===
        _form.FillPanel.PerformLayout()
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

        ' === Update FillPanel ===
        _form.FillPanel.Size = New Size(panelWidth, panelHeight)
        _form.FillPanel.Location = New Point(0, 0)

        ' === TITLE SECTION ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)

        ' === DIVIDER LINE ===
        PositionDividerLine(panelWidth, panelHeight)

        ' === REPORT TYPE SECTION ===
        PositionReportTypeSection(panelWidth, panelHeight, scaleFactor)

        ' === REPORT SUB TYPE SECTION ===
        PositionReportSubTypeSection(panelWidth, panelHeight, scaleFactor)

        ' === DATE RANGE SECTION ===
        PositionDateRangeSection(panelWidth, panelHeight, scaleFactor)

        ' === SEARCH SECTION ===
        PositionSearchSection(panelWidth, panelHeight, scaleFactor)

        ' === DATA GRID VIEW ===
        PositionDataGridView(panelWidth, panelHeight)

        ' === DOWNLOAD TYPE SECTION ===
        PositionDownloadTypeSection(panelWidth, panelHeight, scaleFactor)

        ' === ACTION BUTTONS ===
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title label and divider
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(20, 20) on 1700x1004 = 1.2% from left, 2% from top
        _form.lblGenerateReports.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.02))
        _form.lblGenerateReports.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblGenerateReports.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position horizontal divider line
    ''' </summary>
    Private Sub PositionDividerLine(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(0, 70), Size(1700, 2)
        _form.LinePnl.Location = New Point(0, CInt(panelHeight * 0.07))
        _form.LinePnl.Size = New Size(panelWidth, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position report type label, combobox, and new report type button
    ''' </summary>
    Private Sub PositionReportTypeSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Report Type Label - Designer: Location(22, 96) = 1.3% from left, 9.6% from top
        _form.lblReportType.Location = New Point(CInt(panelWidth * 0.013), CInt(panelHeight * 0.096))
        _form.lblReportType.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblReportType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Report Type ComboBox - Designer: Location(203, 93), Size(1319, 30)
        _form.cbReportType.Location = New Point(CInt(panelWidth * 0.119), CInt(panelHeight * 0.093))
        _form.cbReportType.Size = New Size(CInt(panelWidth * 0.776), CInt(panelHeight * 0.03))
        _form.cbReportType.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.cbReportType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' New Report Type Button - Designer: Location(1537, 93), Size(151, 30)
        _form.btnNewReportType.Location = New Point(CInt(panelWidth * 0.904), CInt(panelHeight * 0.093))
        _form.btnNewReportType.Size = New Size(CInt(panelWidth * 0.089), CInt(panelHeight * 0.03))
        _form.btnNewReportType.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnNewReportType.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnNewReportType.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position report sub type label and combobox
    ''' </summary>
    Private Sub PositionReportSubTypeSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Report Sub Type Label - Designer: Location(22, 185) = 1.3% from left, 18.4% from top
        _form.lblReportSubType.Location = New Point(CInt(panelWidth * 0.013), CInt(panelHeight * 0.184))
        _form.lblReportSubType.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblReportSubType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Report Sub Type ComboBox - Designer: Location(203, 182), Size(1319, 30)
        _form.cbReportSubType.Location = New Point(CInt(panelWidth * 0.119), CInt(panelHeight * 0.181))
        _form.cbReportSubType.Size = New Size(CInt(panelWidth * 0.776), CInt(panelHeight * 0.03))
        _form.cbReportSubType.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.cbReportSubType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position date range label and date pickers
    ''' </summary>
    Private Sub PositionDateRangeSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Date Range Label - Designer: Location(22, 271) = 1.3% from left, 27% from top
        _form.lblDateRange.Location = New Point(CInt(panelWidth * 0.013), CInt(panelHeight * 0.27))
        _form.lblDateRange.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblDateRange.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' From DateTimePicker - Designer: Location(203, 266), Size(627, 29)
        _form.dtpFrom.Location = New Point(CInt(panelWidth * 0.119), CInt(panelHeight * 0.265))
        _form.dtpFrom.Size = New Size(CInt(panelWidth * 0.369), CInt(panelHeight * 0.029))
        _form.dtpFrom.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.dtpFrom.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' To Label - Designer: Location(846, 271) = 49.8% from left
        _form.lblTo.Location = New Point(CInt(panelWidth * 0.498), CInt(panelHeight * 0.27))
        _form.lblTo.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblTo.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' To DateTimePicker - Designer: Location(895, 266), Size(627, 29)
        _form.dtpLatest.Location = New Point(CInt(panelWidth * 0.526), CInt(panelHeight * 0.265))
        _form.dtpLatest.Size = New Size(CInt(panelWidth * 0.369), CInt(panelHeight * 0.029))
        _form.dtpLatest.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.dtpLatest.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position search label, textbox, and button
    ''' </summary>
    Private Sub PositionSearchSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Search Label - Designer: Location(22, 367) = 1.3% from left, 36.6% from top
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.013), CInt(panelHeight * 0.366))
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Search TextBox - Designer: Location(203, 364), Size(1328, 29)
        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.119), CInt(panelHeight * 0.363))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.781), CInt(panelHeight * 0.029))
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Search Button - Designer: Location(1537, 364), Size(151, 29)
        _form.btnSearch.Location = New Point(CInt(panelWidth * 0.904), CInt(panelHeight * 0.363))
        _form.btnSearch.Size = New Size(CInt(panelWidth * 0.089), CInt(panelHeight * 0.029))
        _form.btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnSearch.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position DataGridView for reports
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(12, 400), Size(1676, 442)
        _form.dgvReports.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.398))
        _form.dgvReports.Size = New Size(CInt(panelWidth * 0.986), CInt(panelHeight * 0.44))
        _form.dgvReports.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position download type label and combobox
    ''' </summary>
    Private Sub PositionDownloadTypeSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Download Type Label - Designer: Location(22, 851) = 1.3% from left, 84.8% from top
        _form.lblDownloadType.Location = New Point(CInt(panelWidth * 0.013), CInt(panelHeight * 0.848))
        _form.lblDownloadType.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.lblDownloadType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Download Type ComboBox - Designer: Location(203, 848), Size(1485, 30)
        _form.cbDownloadType.Location = New Point(CInt(panelWidth * 0.119), CInt(panelHeight * 0.845))
        _form.cbDownloadType.Size = New Size(CInt(panelWidth * 0.874), CInt(panelHeight * 0.03))
        _form.cbDownloadType.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        _form.cbDownloadType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position action buttons (Generate, View Generated Reports)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.133) ' 226/1700 ≈ 13.3%
        Dim btnHeight As Integer = CInt(panelHeight * 0.045) ' 45/1004 ≈ 4.5%
        Dim btnY As Integer = CInt(panelHeight * 0.919) ' 923/1004 ≈ 91.9%

        ' Generate Button - Designer: Location(474, 923), Size(226, 45)
        _form.btnGenerate.Location = New Point(CInt(panelWidth * 0.279), btnY)
        _form.btnGenerate.Size = New Size(btnWidth, btnHeight)
        _form.btnGenerate.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnGenerate.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnGenerate.Cursor = Cursors.Hand

        ' View Generated Reports Button - Designer: Location(998, 923), Size(226, 45)
        _form.btnViewGeneratedReports.Location = New Point(CInt(panelWidth * 0.587), btnY)
        _form.btnViewGeneratedReports.Size = New Size(btnWidth, btnHeight)
        _form.btnViewGeneratedReports.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnViewGeneratedReports.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnViewGeneratedReports.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
