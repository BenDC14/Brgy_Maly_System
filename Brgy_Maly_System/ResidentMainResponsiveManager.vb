Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for ResidentMain_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class ResidentMainResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As ResidentMain_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As ResidentMain_Form)
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

        ' === ADD NEW RESIDENT BUTTON ===
        PositionAddNewResidentButton(panelWidth, panelHeight, scaleFactor)

        ' === SEARCH SECTION ===
        PositionSearchSection(panelWidth, panelHeight, scaleFactor)

        ' === DATA GRID VIEW ===
        PositionDataGridView(panelWidth, panelHeight)

    End Sub

    ''' <summary>
    ''' Position "Resident Information" title
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(20, 40) on 1700x1004
        _form.ResidentInfolbl.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.04))
        _form.ResidentInfolbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.ResidentInfolbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position "Add New Resident" button
    ''' </summary>
    Private Sub PositionAddNewResidentButton(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(40, 103), Size(204, 45)
        _form.btnAddNewResident.Location = New Point(CInt(panelWidth * 0.024), CInt(panelHeight * 0.103))
        _form.btnAddNewResident.Size = New Size(CInt(panelWidth * 0.12), CInt(panelHeight * 0.045))
        _form.btnAddNewResident.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnAddNewResident.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnAddNewResident.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position search section (label, textbox, search button)
    ''' </summary>
    Private Sub PositionSearchSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Search Label - Designer: Location(1152, 123)
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.678), CInt(panelHeight * 0.123))
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Search TextBox - Designer: Location(1241, 119), Size(290, 29)
        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.73), CInt(panelHeight * 0.119))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.171), CInt(panelHeight * 0.029))
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.txtSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Search Button - Designer: Location(1537, 119), Size(151, 29)
        _form.btnSearch.Location = New Point(CInt(panelWidth * 0.904), CInt(panelHeight * 0.119))
        _form.btnSearch.Size = New Size(CInt(panelWidth * 0.089), CInt(panelHeight * 0.029))
        _form.btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnSearch.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position DataGridView for residents
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(12, 162), Size(1676, 766)
        _form.dgvResidents.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.161))
        _form.dgvResidents.Size = New Size(CInt(panelWidth * 0.986), CInt(panelHeight * 0.763))
        _form.dgvResidents.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
