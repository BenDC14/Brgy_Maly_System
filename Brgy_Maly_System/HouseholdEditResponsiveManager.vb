Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for HouseholdEdit_Form
''' Handles all layout calculations, positioning, and font scaling
''' Two-column layout: Left (address fields) | Right (DataGridViews with searches)
''' </summary>
Public Class HouseholdEditResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As HouseholdEdit_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As HouseholdEdit_Form)
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
        PositionLeftColumnFields(panelWidth, panelHeight, scaleFactor)
        PositionVerticalDivider(panelWidth, panelHeight)
        PositionFamilyHeadsSection(panelWidth, panelHeight, scaleFactor)
        PositionHorizontalDivider(panelWidth, panelHeight)
        PositionResidentsSection(panelWidth, panelHeight, scaleFactor)
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title section
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' AddHouseholdlbl - Designer: Location(20, 40)
        _form.AddHouseholdlbl.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.04))
        _form.AddHouseholdlbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.AddHouseholdlbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position LEFT COLUMN fields (Household Number through Province)
    ''' </summary>
    Private Sub PositionLeftColumnFields(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.031)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.226)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 12.0F * scaleFactor
        Dim rowHeight As Single = panelHeight / 14.0F

        ' === HOUSEHOLD NUMBER ===
        _form.HouseholdNumlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.107))
        _form.HouseholdNumlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtHouseholdNumber.Location = New Point(leftMargin, CInt(panelHeight * 0.13))
        _form.txtHouseholdNumber.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtHouseholdNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === HOUSE NUMBER ===
        _form.HouseNumberlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.17))
        _form.HouseNumberlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtHouseNumber.Location = New Point(leftMargin, CInt(panelHeight * 0.193))
        _form.txtHouseNumber.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtHouseNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === BLOCK NUMBER ===
        _form.BlockNumberlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.239))
        _form.BlockNumberlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtBlockNumber.Location = New Point(leftMargin, CInt(panelHeight * 0.262))
        _form.txtBlockNumber.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtBlockNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === LOT NUMBER ===
        _form.LotNumberlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.302))
        _form.LotNumberlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtLotNumber.Location = New Point(leftMargin, CInt(panelHeight * 0.325))
        _form.txtLotNumber.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtLotNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === AREA NUMBER ===
        _form.AreaNumberlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.369))
        _form.AreaNumberlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtAreaNumber.Location = New Point(leftMargin, CInt(panelHeight * 0.392))
        _form.txtAreaNumber.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtAreaNumber.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === STREET NAME ===
        _form.StreetNamelbl.Location = New Point(leftMargin, CInt(panelHeight * 0.432))
        _form.StreetNamelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txStreetName.Location = New Point(leftMargin, CInt(panelHeight * 0.455))
        _form.txStreetName.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txStreetName.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === VILLAGE ===
        _form.Villagelbl.Location = New Point(leftMargin, CInt(panelHeight * 0.498))
        _form.Villagelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtVillage.Location = New Point(leftMargin, CInt(panelHeight * 0.521))
        _form.txtVillage.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtVillage.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === SUBDIVISION ===
        _form.Subdivisionlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.567))
        _form.Subdivisionlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtSubdivision.Location = New Point(leftMargin, CInt(panelHeight * 0.59))
        _form.txtSubdivision.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtSubdivision.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === COMPOUND ===
        _form.Compoundlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.632))
        _form.Compoundlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtCompound.Location = New Point(leftMargin, CInt(panelHeight * 0.655))
        _form.txtCompound.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtCompound.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === BARANGAY ===
        _form.Barangaylbl.Location = New Point(leftMargin, CInt(panelHeight * 0.698))
        _form.Barangaylbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtBarangay.Location = New Point(leftMargin, CInt(panelHeight * 0.721))
        _form.txtBarangay.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtBarangay.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === MUNICIPALITY ===
        _form.Municipalitylbl.Location = New Point(leftMargin, CInt(panelHeight * 0.765))
        _form.Municipalitylbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtMunicipality.Location = New Point(leftMargin, CInt(panelHeight * 0.788))
        _form.txtMunicipality.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtMunicipality.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === PROVINCE ===
        _form.Provincelbl.Location = New Point(leftMargin, CInt(panelHeight * 0.831))
        _form.Provincelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtProvince.Location = New Point(leftMargin, CInt(panelHeight * 0.854))
        _form.txtProvince.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtProvince.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position vertical divider line (between left and right columns)
    ''' </summary>
    Private Sub PositionVerticalDivider(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl1 - Designer: Location(500, 0), Size(2, 1004)
        _form.LinePnl1.Location = New Point(CInt(panelWidth * 0.294), 0)
        _form.LinePnl1.Size = New Size(2, panelHeight)
        _form.LinePnl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position Family Heads section (top right)
    ''' </summary>
    Private Sub PositionFamilyHeadsSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim rightMargin As Integer = CInt(panelWidth * 0.313)
        Dim dataGridWidth As Integer = CInt(panelWidth * 0.673)
        Dim dataGridHeight As Integer = CInt(panelHeight * 0.38)
        Dim searchBoxWidth As Integer = CInt(panelWidth * 0.184)

        ' === TITLE ===
        _form.FamilyHeadsInTheHouseholdlbl.Location = New Point(rightMargin, CInt(panelHeight * 0.049))
        _form.FamilyHeadsInTheHouseholdlbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.FamilyHeadsInTheHouseholdlbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        ' === SEARCH LABEL ===
        _form.lblSearchFamHead.Location = New Point(CInt(panelWidth * 0.727), CInt(panelHeight * 0.024))
        _form.lblSearchFamHead.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblSearchFamHead.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' === SEARCH TEXTBOX ===
        _form.txtSearchFamilyHeads.Location = New Point(CInt(panelWidth * 0.729), CInt(panelHeight * 0.049))
        _form.txtSearchFamilyHeads.Size = New Size(searchBoxWidth, CInt(panelHeight * 0.029))
        _form.txtSearchFamilyHeads.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
        _form.txtSearchFamilyHeads.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' === SEARCH BUTTON ===
        _form.btnSearchHeads.Location = New Point(CInt(panelWidth * 0.924), CInt(panelHeight * 0.049))
        _form.btnSearchHeads.Size = New Size(CInt(panelWidth * 0.062), CInt(panelHeight * 0.029))
        _form.btnSearchHeads.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearchHeads.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnSearchHeads.Cursor = Cursors.Hand

        ' === DATAGRIDVIEW ===
        _form.FamilyHeadsDGV.Location = New Point(rightMargin, CInt(panelHeight * 0.084))
        _form.FamilyHeadsDGV.Size = New Size(dataGridWidth, dataGridHeight)
        _form.FamilyHeadsDGV.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position horizontal divider line (between top and bottom sections)
    ''' </summary>
    Private Sub PositionHorizontalDivider(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl2 - Designer: Location(500, 493), Size(1200, 2)
        _form.LinePnl2.Location = New Point(CInt(panelWidth * 0.294), CInt(panelHeight * 0.49))
        _form.LinePnl2.Size = New Size(CInt(panelWidth * 0.706), 2)
        _form.LinePnl2.Anchor = AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Residents section (bottom right)
    ''' </summary>
    Private Sub PositionResidentsSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim rightMargin As Integer = CInt(panelWidth * 0.313)
        Dim dataGridWidth As Integer = CInt(panelWidth * 0.673)
        Dim dataGridHeight As Integer = CInt(panelHeight * 0.38)
        Dim searchBoxWidth As Integer = CInt(panelWidth * 0.184)

        ' === TITLE ===
        _form.ResidentsInTheHouseholdlbl.Location = New Point(rightMargin, CInt(panelHeight * 0.554))
        _form.ResidentsInTheHouseholdlbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.ResidentsInTheHouseholdlbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        ' === SEARCH LABEL ===
        _form.SearchResidentsLbl.Location = New Point(CInt(panelWidth * 0.727), CInt(panelHeight * 0.531))
        _form.SearchResidentsLbl.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.SearchResidentsLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' === SEARCH TEXTBOX ===
        _form.TxtSearchResidents.Location = New Point(CInt(panelWidth * 0.729), CInt(panelHeight * 0.557))
        _form.TxtSearchResidents.Size = New Size(searchBoxWidth, CInt(panelHeight * 0.029))
        _form.TxtSearchResidents.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
        _form.TxtSearchResidents.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' === SEARCH BUTTON ===
        _form.BtnSearchResident.Location = New Point(CInt(panelWidth * 0.924), CInt(panelHeight * 0.557))
        _form.BtnSearchResident.Size = New Size(CInt(panelWidth * 0.062), CInt(panelHeight * 0.029))
        _form.BtnSearchResident.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.BtnSearchResident.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.BtnSearchResident.Cursor = Cursors.Hand

        ' === DATAGRIDVIEW ===
        _form.ResidentInHouseholdDGV.Location = New Point(rightMargin, CInt(panelHeight * 0.592))
        _form.ResidentInHouseholdDGV.Size = New Size(dataGridWidth, dataGridHeight)
        _form.ResidentInHouseholdDGV.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position action buttons (Edit Household, Back to Main)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.117)
        Dim btnHeight As Integer = CInt(panelHeight * 0.044)
        Dim btnY As Integer = CInt(panelHeight * 0.926)

        ' Edit Household Button - Designer: Location(26, 930)
        _form.BtnEditHousehold.Location = New Point(CInt(panelWidth * 0.015), btnY)
        _form.BtnEditHousehold.Size = New Size(btnWidth, btnHeight)
        _form.BtnEditHousehold.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.BtnEditHousehold.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.BtnEditHousehold.Cursor = Cursors.Hand

        ' Back to Main Button - Designer: Location(272, 930)
        _form.btnBack.Location = New Point(CInt(panelWidth * 0.16), btnY)
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
