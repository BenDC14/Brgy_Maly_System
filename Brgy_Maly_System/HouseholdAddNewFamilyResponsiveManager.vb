Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for HouseholdAddNewFamily_Form
''' Handles all layout calculations, positioning, and font scaling
''' Three-column input form with DataGridView
''' </summary>
Public Class HouseholdAddNewFamilyResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As HouseholdAddNewFamily_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As HouseholdAddNewFamily_Form)
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
        PositionInputSection(panelWidth, panelHeight, scaleFactor)
        PositionDividerLine(panelWidth, panelHeight)
        PositionFamiliesSection(panelWidth, panelHeight, scaleFactor)
        PositionDataGridView(panelWidth, panelHeight)
        PositionRelationshipSection(panelWidth, panelHeight, scaleFactor)
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title section
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' AddNewFamilylbl - Designer: Location(30, 30)
        _form.AddNewFamilylbl.Location = New Point(CInt(panelWidth * 0.018), CInt(panelHeight * 0.03))
        _form.AddNewFamilylbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.AddNewFamilylbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position input section (Household, Family Name, Family Head)
    ''' </summary>
    Private Sub PositionInputSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim margin1 As Integer = CInt(panelWidth * 0.119)
        Dim margin2 As Integer = CInt(panelWidth * 0.366)
        Dim margin3 As Integer = CInt(panelWidth * 0.617)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.226)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 12.0F * scaleFactor

        ' === HOUSEHOLD ===
        _form.Householdlbl.Location = New Point(margin1, CInt(panelHeight * 0.106))
        _form.Householdlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtHousehold.Location = New Point(margin1, CInt(panelHeight * 0.129))
        _form.txtHousehold.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtHousehold.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === FAMILY NAME ===
        _form.FamilyNamelbl.Location = New Point(margin2, CInt(panelHeight * 0.106))
        _form.FamilyNamelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtFamilyName.Location = New Point(margin2, CInt(panelHeight * 0.129))
        _form.txtFamilyName.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtFamilyName.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === FAMILY HEAD ===
        _form.FamilyHeadlbl.Location = New Point(margin3, CInt(panelHeight * 0.106))
        _form.FamilyHeadlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtFamilyHead.Location = New Point(margin3, CInt(panelHeight * 0.129))
        _form.txtFamilyHead.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtFamilyHead.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === SAVE FAMILY BUTTON ===
        _form.btnSaveFamily.Location = New Point(margin1, CInt(panelHeight * 0.172))
        _form.btnSaveFamily.Size = New Size(CInt(panelWidth * 0.725), CInt(panelHeight * 0.036))
        _form.btnSaveFamily.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.btnSaveFamily.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.btnSaveFamily.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position horizontal divider line
    ''' </summary>
    Private Sub PositionDividerLine(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl - Designer: Location(0, 250), Size(1700, 2)
        _form.LinePnl.Location = New Point(0, CInt(panelHeight * 0.249))
        _form.LinePnl.Size = New Size(panelWidth, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Families section title
    ''' </summary>
    Private Sub PositionFamiliesSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Familieslbl - Designer: Location(32, 270)
        _form.Familieslbl.Location = New Point(CInt(panelWidth * 0.019), CInt(panelHeight * 0.269))
        _form.Familieslbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)
        _form.Familieslbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position DataGridView for family members
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(34, 297), Size(1633, 518)
        _form.FamilyMembersDGV.Location = New Point(CInt(panelWidth * 0.02), CInt(panelHeight * 0.296))
        _form.FamilyMembersDGV.Size = New Size(CInt(panelWidth * 0.96), CInt(panelHeight * 0.516))
        _form.FamilyMembersDGV.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Relationship section (Civil Status / Relationship Type)
    ''' </summary>
    Private Sub PositionRelationshipSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.018)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.3)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 12.0F * scaleFactor

        ' === RELATIONSHIP TYPE LABEL ===
        _form.CivilStatuslbl.Location = New Point(leftMargin, CInt(panelHeight * 0.848))
        _form.CivilStatuslbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)

        ' === RELATIONSHIP TYPE COMBOBOX ===
        _form.cbCivilStatus.Location = New Point(CInt(panelWidth * 0.111), CInt(panelHeight * 0.848))
        _form.cbCivilStatus.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.cbCivilStatus.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)
        _form.cbCivilStatus.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position action buttons (Save Relationship, Back to Main)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.117)
        Dim btnHeight As Integer = CInt(panelHeight * 0.044)
        Dim btnY As Integer = CInt(panelHeight * 0.922)

        ' Save Relationship Button - Designer: Location(1223, 926)
        _form.BtnSaveRelationship.Location = New Point(CInt(panelWidth * 0.719), btnY)
        _form.BtnSaveRelationship.Size = New Size(btnWidth, btnHeight)
        _form.BtnSaveRelationship.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        _form.BtnSaveRelationship.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.BtnSaveRelationship.Cursor = Cursors.Hand

        ' Back to Main Button - Designer: Location(1468, 926)
        _form.btnBack.Location = New Point(CInt(panelWidth * 0.864), btnY)
        _form.btnBack.Size = New Size(btnWidth, btnHeight)
        _form.btnBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
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
