Imports Microsoft.Win32

Public Class HouseholdEdit_ResponsiveManager
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    Private ReadOnly _form As HouseholdEdit_Form
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    Public Sub New(form As HouseholdEdit_Form)
        _form = form
    End Sub

    Public Sub Initialize()
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.FillPanel.Location = New Point(0, 0)

        resizeTimer.Interval = 250
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick
        AddHandler _form.Resize, AddressOf Form_Resize
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Return
        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    Public Sub CalculateAndApplyLayout()
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 OrElse panelHeight < 100 Then Return

        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        _form.FillPanel.Size = New Size(panelWidth, panelHeight)
        _form.FillPanel.Location = New Point(0, 0)

        PositionLeftPanel(panelWidth, panelHeight, scaleFactor)
        PositionRightPanel(panelWidth, panelHeight, scaleFactor)
    End Sub

    Private Sub PositionLeftPanel(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftX As Integer = CInt(panelWidth * 0.031)
        Dim labelFont As Single = Math.Max(8.5F, 12.0F * scaleFactor)
        Dim titleFont As Single = Math.Max(13.0F, 20.25F * scaleFactor)
        Dim inputFont As Single = Math.Max(8.5F, 12.0F * scaleFactor)

        Dim leftWidth As Integer = CInt(panelWidth * 0.226)
        Dim inputHeight As Integer = Math.Max(22, CInt(panelHeight * 0.026))

        _form.AddHouseholdlbl.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.04))
        _form.AddHouseholdlbl.Font = New Font("Arial", titleFont, FontStyle.Bold)

        PositionLabelAndText(_form.HouseholdNumlbl, _form.txtHouseholdNumber, leftX, panelHeight, 0.107, 0.13, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.HouseNumberlbl, _form.txtHouseNumber, leftX, panelHeight, 0.17, 0.193, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.BlockNumberlbl, _form.txtBlockNumber, leftX, panelHeight, 0.239, 0.262, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.LotNumberlbl, _form.txtLotNumber, leftX, panelHeight, 0.302, 0.325, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.AreaNumberlbl, _form.txtAreaNumber, leftX, panelHeight, 0.369, 0.392, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.StreetNamelbl, _form.txStreetName, leftX, panelHeight, 0.432, 0.455, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.Villagelbl, _form.txtVillage, leftX, panelHeight, 0.498, 0.521, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.Subdivisionlbl, _form.txtSubdivision, leftX, panelHeight, 0.567, 0.59, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.Compoundlbl, _form.txtCompound, leftX, panelHeight, 0.632, 0.655, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.Barangaylbl, _form.txtBarangay, leftX, panelHeight, 0.698, 0.721, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.Municipalitylbl, _form.txtMunicipality, leftX, panelHeight, 0.765, 0.788, leftWidth, inputHeight, labelFont, inputFont)
        PositionLabelAndText(_form.Provincelbl, _form.txtProvince, leftX, panelHeight, 0.831, 0.854, leftWidth, inputHeight, labelFont, inputFont)

        _form.BtnEditHousehold.Location = New Point(CInt(panelWidth * 0.015), CInt(panelHeight * 0.926))
        _form.BtnEditHousehold.Size = New Size(CInt(panelWidth * 0.117), Math.Max(34, CInt(panelHeight * 0.044)))
        _form.BtnEditHousehold.Font = New Font("Arial Narrow", Math.Max(8.5F, 11.25F * scaleFactor), FontStyle.Bold)
        _form.BtnEditHousehold.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom

        _form.btnBack.Location = New Point(CInt(panelWidth * 0.16), CInt(panelHeight * 0.926))
        _form.btnBack.Size = New Size(CInt(panelWidth * 0.117), Math.Max(34, CInt(panelHeight * 0.044)))
        _form.btnBack.Font = New Font("Arial Narrow", Math.Max(8.5F, 11.25F * scaleFactor), FontStyle.Bold)
        _form.btnBack.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom

        _form.LinePnl1.Location = New Point(CInt(panelWidth * 0.294), 0)
        _form.LinePnl1.Size = New Size(2, panelHeight)
        _form.LinePnl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
    End Sub

    Private Sub PositionLabelAndText(lbl As Label, txt As TextBox, leftX As Integer, panelHeight As Integer, labelYRatio As Double, textYRatio As Double, width As Integer, height As Integer, labelFont As Single, textFont As Single)
        lbl.Location = New Point(leftX, CInt(panelHeight * labelYRatio))
        lbl.Font = New Font("Arial", labelFont, FontStyle.Bold)

        txt.Location = New Point(leftX, CInt(panelHeight * textYRatio))
        txt.Size = New Size(width, height)
        txt.Font = New Font("Arial", textFont, FontStyle.Regular)
        txt.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    Private Sub PositionRightPanel(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim rightX As Integer = CInt(panelWidth * 0.313)
        Dim rightWidth As Integer = CInt(panelWidth * 0.673)
        Dim gridHeight As Integer = Math.Max(190, CInt(panelHeight * 0.38))

        Dim titleFont As Single = Math.Max(13.0F, 20.25F * scaleFactor)
        Dim labelFont As Single = Math.Max(9.0F, 14.25F * scaleFactor)
        Dim inputFont As Single = Math.Max(9.0F, 14.25F * scaleFactor)
        Dim buttonFont As Single = Math.Max(8.5F, 12.0F * scaleFactor)

        Dim searchX As Integer = CInt(panelWidth * 0.729)
        Dim searchWidth As Integer = CInt(panelWidth * 0.184)
        Dim searchButtonX As Integer = CInt(panelWidth * 0.924)
        Dim searchButtonWidth As Integer = Math.Max(70, CInt(panelWidth * 0.062))
        Dim searchHeight As Integer = Math.Max(24, CInt(panelHeight * 0.029))

        _form.FamilyHeadsInTheHouseholdlbl.Location = New Point(rightX, CInt(panelHeight * 0.049))
        _form.FamilyHeadsInTheHouseholdlbl.Font = New Font("Arial", titleFont, FontStyle.Bold)

        _form.lblSearchFamHead.Location = New Point(CInt(panelWidth * 0.727), CInt(panelHeight * 0.024))
        _form.lblSearchFamHead.Font = New Font("Arial", labelFont, FontStyle.Bold)

        _form.txtSearchFamilyHeads.Location = New Point(searchX, CInt(panelHeight * 0.049))
        _form.txtSearchFamilyHeads.Size = New Size(searchWidth, searchHeight)
        _form.txtSearchFamilyHeads.Font = New Font("Arial", inputFont, FontStyle.Regular)

        _form.btnSearchHeads.Location = New Point(searchButtonX, CInt(panelHeight * 0.049))
        _form.btnSearchHeads.Size = New Size(searchButtonWidth, searchHeight)
        _form.btnSearchHeads.Font = New Font("Arial Narrow", buttonFont, FontStyle.Bold Or FontStyle.Italic)

        _form.FamilyHeadsDGV.Location = New Point(rightX, CInt(panelHeight * 0.084))
        _form.FamilyHeadsDGV.Size = New Size(rightWidth, gridHeight)
        _form.FamilyHeadsDGV.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        _form.LinePnl2.Location = New Point(CInt(panelWidth * 0.294), CInt(panelHeight * 0.49))
        _form.LinePnl2.Size = New Size(CInt(panelWidth * 0.706), 2)
        _form.LinePnl2.Anchor = AnchorStyles.Left Or AnchorStyles.Right

        _form.ResidentsInTheHouseholdlbl.Location = New Point(rightX, CInt(panelHeight * 0.554))
        _form.ResidentsInTheHouseholdlbl.Font = New Font("Arial", titleFont, FontStyle.Bold)

        _form.SearchResidentsLbl.Location = New Point(CInt(panelWidth * 0.727), CInt(panelHeight * 0.531))
        _form.SearchResidentsLbl.Font = New Font("Arial", labelFont, FontStyle.Bold)

        _form.TxtSearchResidents.Location = New Point(searchX, CInt(panelHeight * 0.557))
        _form.TxtSearchResidents.Size = New Size(searchWidth, searchHeight)
        _form.TxtSearchResidents.Font = New Font("Arial", inputFont, FontStyle.Regular)

        _form.BtnSearchResident.Location = New Point(searchButtonX, CInt(panelHeight * 0.557))
        _form.BtnSearchResident.Size = New Size(searchButtonWidth, searchHeight)
        _form.BtnSearchResident.Font = New Font("Arial Narrow", buttonFont, FontStyle.Bold Or FontStyle.Italic)

        _form.ResidentInHouseholdDGV.Location = New Point(rightX, CInt(panelHeight * 0.592))
        _form.ResidentInHouseholdDGV.Size = New Size(rightWidth, gridHeight)
        _form.ResidentInHouseholdDGV.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class