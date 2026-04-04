Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager for HouseholdMain_Form
''' Handles layout calculations and positioning with proper spacing
''' </summary>
Public Class HouseholdMainResponsiveManager

    ' === Original form dimensions ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As HouseholdMain_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As HouseholdMain_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior
    ''' </summary>
    Public Sub Initialize()
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Calculate layout ===
        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    ''' <summary>
    ''' Fires when Windows resolution changes
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
    ''' Calculate positions and apply layout using ClientSize (CRITICAL for resolution changes)
    ''' </summary>
    Private Sub CalculateAndApplyLayout()
        ' === Use form's actual client size (tracks resolution changes) ===
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' === Calculate scale factor ===
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        ' === TITLE SECTION - Designer: Location(20, 40) ===
        _form.HouseholdInfolbl.Location = New Point(CInt(20 * scaleFactor), CInt(20 * scaleFactor))
        _form.HouseholdInfolbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.HouseholdInfolbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' === SEARCH SECTION ===
        ' Search Label - Designer: Location(1152, 123)
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.68), CInt(panelHeight * 0.08))
        _form.lblSearch.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' Search TextBox - Designer: Location(1241, 119), Size(290, 29)
        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.73), CInt(panelHeight * 0.075))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.17), CInt(panelHeight * 0.035))
        _form.txtSearch.Font = New Font("Arial", 11.0F * scaleFactor, FontStyle.Regular)
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' Search Button - Designer: Location(1537, 119), Size(151, 29)
        _form.btnSearch.Location = New Point(CInt(panelWidth * 0.91), CInt(panelHeight * 0.075))
        _form.btnSearch.Size = New Size(CInt(panelWidth * 0.089), CInt(panelHeight * 0.035))
        _form.btnSearch.Font = New Font("Arial Narrow", 11.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' === DATAGRIDVIEW - Designer: Location(12, 162), Size(1676, 766) ===
        ' Positioned below search section with proper spacing
        Dim dgvTop As Integer = CInt(panelHeight * 0.13)
        Dim dgvHeight As Integer = panelHeight - dgvTop - CInt(panelHeight * 0.05)

        _form.dgvHouseholdInfo.Location = New Point(CInt(panelWidth * 0.01), dgvTop)
        _form.dgvHouseholdInfo.Size = New Size(CInt(panelWidth * 0.98), dgvHeight)
        _form.dgvHouseholdInfo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class