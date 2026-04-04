Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for HouseholdAdding_Form
''' Handles all layout calculations, positioning, a     nd font scaling
''' Two-column address form layout with exact Designer positioning
''' </summary>
Public Class HouseholdAddingResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As HouseholdAdding_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As HouseholdAdding_Form)
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
    ''' Uses exact Designer coordinates scaled by resolution
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        ' === Use form's actual client size ===
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' === Calculate scale factor ===
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        ' === Update FillPanel ===
        _form.FillPanel.Size = New Size(panelWidth, panelHeight)
        _form.FillPanel.Location = New Point(0, 0)

        ' === POSITION ALL SECTIONS ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)
        PositionLeftColumnFields(panelWidth, panelHeight, scaleFactor)
        PositionRightColumnFields(panelWidth, panelHeight, scaleFactor)
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title section
    ''' Designer: AddHouseholdlbl Location(20, 40)
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        _form.AddHouseholdlbl.Location = New Point(CInt(20 * scaleFactor), CInt(40 * scaleFactor))
        _form.AddHouseholdlbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.AddHouseholdlbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position LEFT COLUMN fields
    ''' Designer: Left margin starts at X=53
    ''' </summary>
    Private Sub PositionLeftColumnFields(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(53 * scaleFactor)
        Dim fieldWidth As Integer = CInt(522 * scaleFactor)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 18.0F * scaleFactor

        ' === HOUSEHOLD NUMBER - Designer: (53, 155) ===
        _form.HouseholdNumlbl.Location = New Point(leftMargin, CInt(133 * scaleFactor))
        _form.HouseholdNumlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtHouseholdNumber.Location = New Point(leftMargin, CInt(155 * scaleFactor))
        _form.txtHouseholdNumber.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtHouseholdNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === BLOCK NUMBER - Designer: (53, 263) ===
        _form.BlockNumlbl.Location = New Point(leftMargin, CInt(241 * scaleFactor))
        _form.BlockNumlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtBlockNumber.Location = New Point(leftMargin, CInt(263 * scaleFactor))
        _form.txtBlockNumber.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtBlockNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === AREA NUMBER - Designer: (53, 381) ===
        _form.AreaNumberlbl.Location = New Point(leftMargin, CInt(359 * scaleFactor))
        _form.AreaNumberlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtAreaNumber.Location = New Point(leftMargin, CInt(381 * scaleFactor))
        _form.txtAreaNumber.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtAreaNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === VILLAGE - Designer: (53, 503) ===
        _form.Villagelbl.Location = New Point(leftMargin, CInt(481 * scaleFactor))
        _form.Villagelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtVillage.Location = New Point(leftMargin, CInt(503 * scaleFactor))
        _form.txtVillage.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtVillage.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === COMPOUND - Designer: (53, 631) ===
        _form.Compoundlbl.Location = New Point(leftMargin, CInt(609 * scaleFactor))
        _form.Compoundlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtCompound.Location = New Point(leftMargin, CInt(631 * scaleFactor))
        _form.txtCompound.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtCompound.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === MUNICIPALITY - Designer: (53, 754) ===
        _form.Municipalitylbl.Location = New Point(leftMargin, CInt(732 * scaleFactor))
        _form.Municipalitylbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtMunicipality.Location = New Point(leftMargin, CInt(754 * scaleFactor))
        _form.txtMunicipality.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtMunicipality.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position RIGHT COLUMN fields
    ''' Designer: Right margin starts at X=1105
    ''' </summary>
    Private Sub PositionRightColumnFields(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim rightMargin As Integer = CInt(1105 * scaleFactor)
        Dim fieldWidth As Integer = CInt(522 * scaleFactor)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 18.0F * scaleFactor

        ' === HOUSE NUMBER - Designer: (1105, 155) ===
        _form.HouseNumberlbl.Location = New Point(rightMargin, CInt(133 * scaleFactor))
        _form.HouseNumberlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtHouseNumber.Location = New Point(rightMargin, CInt(155 * scaleFactor))
        _form.txtHouseNumber.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtHouseNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === LOT NUMBER - Designer: (1105, 263) ===
        _form.LotNumberlbl.Location = New Point(rightMargin, CInt(241 * scaleFactor))
        _form.LotNumberlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtLotNumber.Location = New Point(rightMargin, CInt(263 * scaleFactor))
        _form.txtLotNumber.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtLotNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === STREET NAME - Designer: (1105, 381) ===
        _form.StreetNamelbl.Location = New Point(rightMargin, CInt(359 * scaleFactor))
        _form.StreetNamelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtStreetName.Location = New Point(rightMargin, CInt(381 * scaleFactor))
        _form.txtStreetName.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtStreetName.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === SUBDIVISION - Designer: (1105, 503) ===
        _form.Label8.Location = New Point(rightMargin, CInt(481 * scaleFactor))
        _form.Label8.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtSubdivision.Location = New Point(rightMargin, CInt(503 * scaleFactor))
        _form.txtSubdivision.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtSubdivision.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === BARANGAY - Designer: (1105, 631) ===
        _form.Barangaylbl.Location = New Point(rightMargin, CInt(609 * scaleFactor))
        _form.Barangaylbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtBarangay.Location = New Point(rightMargin, CInt(631 * scaleFactor))
        _form.txtBarangay.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtBarangay.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === PROVINCE - Designer: (1105, 754) ===
        _form.Provincelbl.Location = New Point(rightMargin, CInt(732 * scaleFactor))
        _form.Provincelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtProvince.Location = New Point(rightMargin, CInt(754 * scaleFactor))
        _form.txtProvince.Size = New Size(fieldWidth, CInt(35 * scaleFactor))
        _form.txtProvince.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position action buttons
    ''' Designer: BtnAddNewHousehold Location(739, 878), Size(199, 44)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' === ADD NEW HOUSEHOLD BUTTON - Designer: (739, 878), Size(199, 44) ===
        ' Center horizontally, position at bottom
        Dim btnWidth As Integer = CInt(199 * scaleFactor)
        Dim btnHeight As Integer = CInt(44 * scaleFactor)
        Dim btnX As Integer = CInt(739 * scaleFactor)
        Dim btnY As Integer = CInt(878 * scaleFactor)

        _form.BtnAddNewHousehold.Location = New Point(btnX, btnY)
        _form.BtnAddNewHousehold.Size = New Size(btnWidth, btnHeight)
        _form.BtnAddNewHousehold.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.BtnAddNewHousehold.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
