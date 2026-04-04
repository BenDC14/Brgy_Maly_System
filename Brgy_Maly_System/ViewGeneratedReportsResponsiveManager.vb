Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for ViewGeneratedReports_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class ViewGeneratedReportsResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As ViewGeneratedReports_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As ViewGeneratedReports_Form)
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

        ' === SEARCH SECTION ===
        PositionSearchSection(panelWidth, panelHeight, scaleFactor)

        ' === DATA GRID VIEW ===
        PositionDataGridView(panelWidth, panelHeight)

        ' === BACK BUTTON ===
        PositionBackButton(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title label
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(22, 19) on 1700x1004 = 1.3% from left, 1.9% from top
        _form.ViewGeneratedReportslbl.Location = New Point(CInt(panelWidth * 0.013), CInt(panelHeight * 0.019))
        _form.ViewGeneratedReportslbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.ViewGeneratedReportslbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
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
    ''' Position search label, textbox, and button
    ''' </summary>
    Private Sub PositionSearchSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Search Label - Designer: Location(22, 133) = 1.3% from left, 13.3% from top
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.013), CInt(panelHeight * 0.133))
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Search TextBox - Designer: Location(203, 130), Size(1328, 29)
        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.119), CInt(panelHeight * 0.13))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.781), CInt(panelHeight * 0.029))
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Search Button - Designer: Location(1537, 130), Size(151, 29)
        _form.btnSearch.Location = New Point(CInt(panelWidth * 0.904), CInt(panelHeight * 0.13))
        _form.btnSearch.Size = New Size(CInt(panelWidth * 0.089), CInt(panelHeight * 0.029))
        _form.btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnSearch.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position DataGridView for generated reports
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(12, 165), Size(1676, 737)
        _form.dgvReports.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.164))
        _form.dgvReports.Size = New Size(CInt(panelWidth * 0.986), CInt(panelHeight * 0.734))
        _form.dgvReports.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position back button at bottom center
    ''' </summary>
    Private Sub PositionBackButton(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.151) ' 257/1700 ≈ 15.1%
        Dim btnHeight As Integer = CInt(panelHeight * 0.045) ' 45/1004 ≈ 4.5%

        ' Back Button - Designer: Location(708, 928), Size(257, 45)
        ' Centered horizontally: 708/1700 ≈ 41.6%, 928/1004 ≈ 92.4%
        _form.btnBack.Location = New Point(CInt(panelWidth * 0.416), CInt(panelHeight * 0.924))
        _form.btnBack.Size = New Size(btnWidth, btnHeight)
        _form.btnBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnBack.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnBack.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
