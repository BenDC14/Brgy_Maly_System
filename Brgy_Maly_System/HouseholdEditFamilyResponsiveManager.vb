Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for HouseholdEditFamily_Form
''' Handles all layout calculations, positioning, and font scaling
''' Three-row layout: Top (Family Info) | Middle (Members DataGridView) | Bottom (Add New Members + Action Buttons)
''' </summary>
Public Class HouseholdEditFamilyResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As HouseholdEditFamily_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As HouseholdEditFamily_Form)
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
        PositionFamilyInfoSection(panelWidth, panelHeight, scaleFactor)
        PositionFirstDividerLine(panelWidth, panelHeight)
        PositionMembersSection(panelWidth, panelHeight, scaleFactor)
        PositionSecondDividerLine(panelWidth, panelHeight)
        PositionAddNewMembersSection(panelWidth, panelHeight, scaleFactor)
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title section
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' EditFamilylbl - Designer: Location(30, 30)
        _form.EditFamilylbl.Location = New Point(CInt(panelWidth * 0.018), CInt(panelHeight * 0.03))
        _form.EditFamilylbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.EditFamilylbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position Family Information fields (Family ID, Family Name, Household, Family Head, Total Members)
    ''' </summary>
    Private Sub PositionFamilyInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim margin1 As Integer = CInt(panelWidth * 0.241)
        Dim margin2 As Integer = CInt(panelWidth * 0.095)
        Dim margin3 As Integer = CInt(panelWidth * 0.385)
        Dim margin4 As Integer = CInt(panelWidth * 0.385)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.226)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 12.0F * scaleFactor

        ' === FAMILY ID ===
        _form.FamilyIDlbl.Location = New Point(margin2, CInt(panelHeight * 0.118))
        _form.FamilyIDlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtFamilyID.Location = New Point(margin2, CInt(panelHeight * 0.141))
        _form.txtFamilyID.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtFamilyID.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === FAMILY NAME ===
        _form.FamilyNamelbl.Location = New Point(margin2, CInt(panelHeight * 0.19))
        _form.FamilyNamelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtFamilyName.Location = New Point(margin2, CInt(panelHeight * 0.213))
        _form.txtFamilyName.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtFamilyName.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === HOUSEHOLD ===
        _form.Householdlbl.Location = New Point(margin3, CInt(panelHeight * 0.118))
        _form.Householdlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtHousehold.Location = New Point(margin3, CInt(panelHeight * 0.141))
        _form.txtHousehold.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtHousehold.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === FAMILY HEAD ===
        _form.FamilyHeadlbl.Location = New Point(margin4, CInt(panelHeight * 0.19))
        _form.FamilyHeadlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtFamilyHead.Location = New Point(margin4, CInt(panelHeight * 0.213))
        _form.txtFamilyHead.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtFamilyHead.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === TOTAL FAMILY MEMBERS ===
        _form.TotalFamilyMemberslbl.Location = New Point(CInt(panelWidth * 0.675), CInt(panelHeight * 0.19))
        _form.TotalFamilyMemberslbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtTotalFamilyMembers.Location = New Point(CInt(panelWidth * 0.675), CInt(panelHeight * 0.213))
        _form.txtTotalFamilyMembers.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtTotalFamilyMembers.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position first horizontal divider
    ''' </summary>
    Private Sub PositionFirstDividerLine(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl - Designer: Location(0, 302), Size(1700, 2)
        _form.LinePnl.Location = New Point(0, CInt(panelHeight * 0.301))
        _form.LinePnl.Size = New Size(panelWidth, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Members section (title, DataGridView, action buttons)
    ''' </summary>
    Private Sub PositionMembersSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.021)
        Dim dataGridWidth As Integer = CInt(panelWidth * 0.96)
        Dim buttonWidth As Integer = CInt(panelWidth * 0.08)
        Dim buttonHeight As Integer = CInt(panelHeight * 0.044)

        ' === MEMBERS TITLE ===
        _form.Memberslbl.Location = New Point(leftMargin, CInt(panelHeight * 0.329))
        _form.Memberslbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.Memberslbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' === DATAGRIDVIEW ===
        _form.FamilyMembersDGV.Location = New Point(leftMargin, CInt(panelHeight * 0.374))
        _form.FamilyMembersDGV.Size = New Size(dataGridWidth, CInt(panelHeight * 0.325))
        _form.FamilyMembersDGV.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        ' === EDIT MEMBER BUTTON ===
        _form.btnEditMember.Location = New Point(CInt(panelWidth * 0.804), CInt(panelHeight * 0.387))
        _form.btnEditMember.Size = New Size(CInt(panelWidth * 0.08), buttonHeight)
        _form.btnEditMember.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnEditMember.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.btnEditMember.Cursor = Cursors.Hand

        ' === EDIT POSITION BUTTON ===
        _form.btnEditPosition.Location = New Point(CInt(panelWidth * 0.896), CInt(panelHeight * 0.387))
        _form.btnEditPosition.Size = New Size(CInt(panelWidth * 0.08), buttonHeight)
        _form.btnEditPosition.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnEditPosition.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.btnEditPosition.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position second horizontal divider
    ''' </summary>
    Private Sub PositionSecondDividerLine(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl2 - Designer: Location(0, 725), Size(1700, 2)
        _form.LinePnl2.Location = New Point(0, CInt(panelHeight * 0.722))
        _form.LinePnl2.Size = New Size(panelWidth, 2)
        _form.LinePnl2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Add New Members section (title, comboboxes, selects)
    ''' </summary>
    Private Sub PositionAddNewMembersSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.021)
        Dim comboMargin1 As Integer = CInt(panelWidth * 0.095)
        Dim comboMargin2 As Integer = CInt(panelWidth * 0.677)
        Dim comboWidth As Integer = CInt(panelWidth * 0.241)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 14.25F * scaleFactor

        ' === ADD NEW MEMBERS TITLE ===
        _form.AddNewMemberslbl.Location = New Point(leftMargin, CInt(panelHeight * 0.743))
        _form.AddNewMemberslbl.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' === SELECT RESIDENTS LABEL ===
        _form.SelectResidentslbl.Location = New Point(comboMargin1, CInt(panelHeight * 0.785))
        _form.SelectResidentslbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)

        ' === SELECT RESIDENTS COMBOBOX ===
        _form.cbResidents.Location = New Point(comboMargin1, CInt(panelHeight * 0.809))
        _form.cbResidents.Size = New Size(comboWidth, CInt(panelHeight * 0.03))
        _form.cbResidents.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)
        _form.cbResidents.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

        ' === RELATIONSHIP TYPE LABEL ===
        _form.RelationshipTypelbl.Location = New Point(comboMargin2, CInt(panelHeight * 0.785))
        _form.RelationshipTypelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)

        ' === RELATIONSHIP TYPE COMBOBOX ===
        _form.cbRelationships.Location = New Point(comboMargin2, CInt(panelHeight * 0.809))
        _form.cbRelationships.Size = New Size(comboWidth, CInt(panelHeight * 0.03))
        _form.cbRelationships.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)
        _form.cbRelationships.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position action buttons (Add New Member, Save Changes, Add New Resident, Back)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.117)
        Dim btnHeight As Integer = CInt(panelHeight * 0.044)
        Dim btnY As Integer = CInt(panelHeight * 0.917)

        ' Add New Member Button - Designer: Location(160, 921)
        _form.btnAddNewFamilyMember.Location = New Point(CInt(panelWidth * 0.094), btnY)
        _form.btnAddNewFamilyMember.Size = New Size(btnWidth, btnHeight)
        _form.btnAddNewFamilyMember.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnAddNewFamilyMember.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.btnAddNewFamilyMember.Cursor = Cursors.Hand

        ' Save Changes Button - Designer: Location(582, 921)
        _form.btnSaveChanges.Location = New Point(CInt(panelWidth * 0.342), btnY)
        _form.btnSaveChanges.Size = New Size(btnWidth, btnHeight)
        _form.btnSaveChanges.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnSaveChanges.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.btnSaveChanges.Cursor = Cursors.Hand

        ' Add New Resident Button - Designer: Location(980, 921)
        _form.btnAddNewResident.Location = New Point(CInt(panelWidth * 0.576), btnY)
        _form.btnAddNewResident.Size = New Size(btnWidth, btnHeight)
        _form.btnAddNewResident.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnAddNewResident.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.btnAddNewResident.Cursor = Cursors.Hand

        ' Back Button - Designer: Location(1362, 921)
        _form.btnBack.Location = New Point(CInt(panelWidth * 0.801), btnY)
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
