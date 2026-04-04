Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for HouseholdViewFamily_Form
''' Handles all layout calculations, positioning, and font scaling
''' Simple layout: Title + Large DataGridView + Action Buttons
''' </summary>
Public Class HouseholdViewFamilyResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As HouseholdViewFamily_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As HouseholdViewFamily_Form)
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

        ' === POSITION ALL SECTIONS ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)
        PositionDataGridView(panelWidth, panelHeight)
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title section
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' ViewFamilylbl - Designer: Location(12, 27)
        _form.ViewFamilylbl.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.027))
        _form.ViewFamilylbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.ViewFamilylbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position DataGridView for family members/heads
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        ' dgvFamilyHeads - Designer: Location(12, 92), Size(1676, 793)
        _form.dgvFamilyHeads.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.092))
        _form.dgvFamilyHeads.Size = New Size(CInt(panelWidth * 0.986), CInt(panelHeight * 0.789))
        _form.dgvFamilyHeads.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position action buttons (Edit Family, Back to Main)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.117)
        Dim btnHeight As Integer = CInt(panelHeight * 0.044)
        Dim btnY As Integer = CInt(panelHeight * 0.907)

        ' Edit Family Button - Designer: Location(270, 911)
        _form.btnEditFamily.Location = New Point(CInt(panelWidth * 0.159), btnY)
        _form.btnEditFamily.Size = New Size(btnWidth, btnHeight)
        _form.btnEditFamily.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnEditFamily.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.btnEditFamily.Cursor = Cursors.Hand

        ' Back to Main Button - Designer: Location(1208, 911)
        _form.btnBack.Location = New Point(CInt(panelWidth * 0.71), btnY)
        _form.btnBack.Size = New Size(btnWidth, btnHeight)
        _form.btnBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnBack.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
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
