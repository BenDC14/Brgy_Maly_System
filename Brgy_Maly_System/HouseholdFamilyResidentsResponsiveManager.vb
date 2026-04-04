Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for HouseholdFamilyResidents_Form
''' Handles all layout calculations, positioning, and font scaling
''' Multi-section form: Basic Info | Personal Details + Contact Info | House Info + Categories
''' </summary>
Public Class HouseholdFamilyResidentsResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As HouseholdFamilyResidents_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As HouseholdFamilyResidents_Form)
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
        PositionFirstDivider(panelWidth, panelHeight)
        PositionBasicInfoSection(panelWidth, panelHeight, scaleFactor)
        PositionSecondDivider(panelWidth, panelHeight)
        PositionPersonalAndContactSections(panelWidth, panelHeight, scaleFactor)
        PositionThirdDivider(panelWidth, panelHeight)
        PositionHouseAndCategorySections(panelWidth, panelHeight, scaleFactor)
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title section
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' FamilyResidentlbl - Designer: Location(20, 30)
        _form.FamilyResidentlbl.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.03))
        _form.FamilyResidentlbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.FamilyResidentlbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position first horizontal divider
    ''' </summary>
    Private Sub PositionFirstDivider(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl1 - Designer: Location(0, 75), Size(1700, 2)
        _form.LinePnl1.Location = New Point(0, CInt(panelHeight * 0.075))
        _form.LinePnl1.Size = New Size(panelWidth, 2)
        _form.LinePnl1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Basic Information section (Last Name, First Name, Middle Name, Suffix)
    ''' </summary>
    Private Sub PositionBasicInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.026)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.94)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 14.25F * scaleFactor

        ' === SECTION TITLE ===
        _form.BasicInfolbl.Location = New Point(CInt(panelWidth * 0.44), CInt(panelHeight * 0.097))
        _form.BasicInfolbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' === LAST NAME ===
        _form.LastNamelbl.Location = New Point(leftMargin, CInt(panelHeight * 0.136))
        _form.LastNamelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtLastName.Location = New Point(leftMargin, CInt(panelHeight * 0.158))
        _form.txtLastName.Size = New Size(fieldWidth, CInt(panelHeight * 0.029))
        _form.txtLastName.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === FIRST NAME ===
        _form.FirstNamelbl.Location = New Point(leftMargin, CInt(panelHeight * 0.202))
        _form.FirstNamelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtFirstName.Location = New Point(leftMargin, CInt(panelHeight * 0.224))
        _form.txtFirstName.Size = New Size(fieldWidth, CInt(panelHeight * 0.029))
        _form.txtFirstName.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === MIDDLE NAME ===
        _form.MiddleNamelbl.Location = New Point(leftMargin, CInt(panelHeight * 0.269))
        _form.MiddleNamelbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtMiddleName.Location = New Point(leftMargin, CInt(panelHeight * 0.291))
        _form.txtMiddleName.Size = New Size(fieldWidth, CInt(panelHeight * 0.029))
        _form.txtMiddleName.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)

        ' === SUFFIX ===
        _form.Suffixlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.333))
        _form.Suffixlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtSuffix.Location = New Point(leftMargin, CInt(panelHeight * 0.355))
        _form.txtSuffix.Size = New Size(fieldWidth, CInt(panelHeight * 0.029))
        _form.txtSuffix.Font = New Font("Arial", fieldFontSize, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position second horizontal divider
    ''' </summary>
    Private Sub PositionSecondDivider(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl2 - Designer: Location(0, 415), Size(1700, 2)
        _form.LinePnl2.Location = New Point(0, CInt(panelHeight * 0.413))
        _form.LinePnl2.Size = New Size(panelWidth, 2)
        _form.LinePnl2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Personal Details and Contact Information sections (left and right columns)
    ''' </summary>
    Private Sub PositionPersonalAndContactSections(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.026)
        Dim rightMargin As Integer = CInt(panelWidth * 0.512)
        Dim leftWidth As Integer = CInt(panelWidth * 0.453)
        Dim rightWidth As Integer = CInt(panelWidth * 0.456)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 12.0F * scaleFactor

        ' === LEFT COLUMN: PERSONAL DETAILS ===
        ' Section Title
        _form.PersonalDetailslbl.Location = New Point(CInt(panelWidth * 0.17), CInt(panelHeight * 0.434))
        _form.PersonalDetailslbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' Date of Birth
        _form.DateofBirthlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.46))
        _form.DateofBirthlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.DTPDateofBirth.Location = New Point(leftMargin, CInt(panelHeight * 0.488))
        _form.DTPDateofBirth.Size = New Size(leftWidth, CInt(panelHeight * 0.026))
        _form.DTPDateofBirth.Font = New Font("Arial", labelFontSize, FontStyle.Regular)

        ' Sex
        _form.Sexlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.535))
        _form.Sexlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.cbSex.Location = New Point(leftMargin, CInt(panelHeight * 0.558))
        _form.cbSex.Size = New Size(leftWidth, CInt(panelHeight * 0.026))
        _form.cbSex.Font = New Font("Arial", labelFontSize, FontStyle.Regular)

        ' Civil Status
        _form.CivilStatuslbl.Location = New Point(leftMargin, CInt(panelHeight * 0.611))
        _form.CivilStatuslbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.cbCivilStatus.Location = New Point(leftMargin, CInt(panelHeight * 0.634))
        _form.cbCivilStatus.Size = New Size(leftWidth, CInt(panelHeight * 0.026))
        _form.cbCivilStatus.Font = New Font("Arial", labelFontSize, FontStyle.Regular)

        ' === RIGHT COLUMN: CONTACT INFORMATION ===
        ' Section Title
        _form.ContactInfolbl.Location = New Point(CInt(panelWidth * 0.685), CInt(panelHeight * 0.434))
        _form.ContactInfolbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' Contact Number
        _form.ContactNumberlbl.Location = New Point(rightMargin, CInt(panelHeight * 0.467))
        _form.ContactNumberlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtContactNum.Location = New Point(rightMargin, CInt(panelHeight * 0.488))
        _form.txtContactNum.Size = New Size(rightWidth, CInt(panelHeight * 0.026))
        _form.txtContactNum.Font = New Font("Arial", labelFontSize, FontStyle.Regular)

        ' Email Address
        _form.EmailAddresslbl.Location = New Point(rightMargin, CInt(panelHeight * 0.536))
        _form.EmailAddresslbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtEmailAddress.Location = New Point(rightMargin, CInt(panelHeight * 0.558))
        _form.txtEmailAddress.Size = New Size(rightWidth, CInt(panelHeight * 0.026))
        _form.txtEmailAddress.Font = New Font("Arial", labelFontSize, FontStyle.Regular)

        ' === VERTICAL DIVIDER (Between columns) ===
        _form.LinePnl3.Location = New Point(CInt(panelWidth * 0.492), CInt(panelHeight * 0.417))
        _form.LinePnl3.Size = New Size(2, CInt(panelHeight * 0.3))
        _form.LinePnl3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position third horizontal divider
    ''' </summary>
    Private Sub PositionThirdDivider(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl4 - Designer: Location(0, 716), Size(1700, 2)
        _form.LinePnl4.Location = New Point(0, CInt(panelHeight * 0.713))
        _form.LinePnl4.Size = New Size(panelWidth, 2)
        _form.LinePnl4.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position House Information and Category checkboxes
    ''' </summary>
    Private Sub PositionHouseAndCategorySections(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim rightMargin As Integer = CInt(panelWidth * 0.512)
        Dim rightWidth As Integer = CInt(panelWidth * 0.456)
        Dim labelFontSize As Single = 12.0F * scaleFactor
        Dim fieldFontSize As Single = 12.0F * scaleFactor
        Dim checkboxFontSize As Single = 12.0F * scaleFactor

        ' === HOUSEHOLD INFORMATION (Right column) ===
        ' Section Title
        _form.HouseInfolbl.Location = New Point(CInt(panelWidth * 0.677), CInt(panelHeight * 0.635))
        _form.HouseInfolbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' Household Number
        _form.Householdbl.Location = New Point(rightMargin, CInt(panelHeight * 0.654))
        _form.Householdbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtHouseholdNumber.Location = New Point(rightMargin, CInt(panelHeight * 0.675))
        _form.txtHouseholdNumber.Size = New Size(rightWidth, CInt(panelHeight * 0.026))
        _form.txtHouseholdNumber.Font = New Font("Arial", labelFontSize, FontStyle.Regular)

        ' === CATEGORY SECTION (Full width, bottom) ===
        ' Section Title - Designer: Location(748, 730)
        _form.Categorylbl.Location = New Point(CInt(panelWidth * 0.44), CInt(panelHeight * 0.728))
        _form.Categorylbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' === CATEGORY CHECKBOXES (Horizontal layout) ===
        Dim checkboxY As Integer = CInt(panelHeight * 0.785)
        Dim checkboxStartX As Integer = CInt(panelWidth * 0.015)
        Dim checkboxSpacing As Integer = CInt(panelWidth * 0.125)

        ' Senior Citizen
        _form.cbSeniorCitizen.Location = New Point(checkboxStartX, checkboxY)
        _form.cbSeniorCitizen.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' PWD
        _form.cbPWD.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 1), checkboxY)
        _form.cbPWD.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' Student
        _form.cbStudent.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 1.8), checkboxY)
        _form.cbStudent.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' Solo Parent
        _form.cbSoloParent.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 2.6), checkboxY)
        _form.cbSoloParent.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' Employed
        _form.cbEmployed.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 3.4), checkboxY)
        _form.cbEmployed.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' Unemployed
        _form.cbUnemployed.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 4.2), checkboxY)
        _form.cbUnemployed.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' OFW
        _form.cbOFW.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 5.0), checkboxY)
        _form.cbOFW.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' Out of School Children
        _form.cbOutofSchoolChildren.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 5.8), checkboxY)
        _form.cbOutofSchoolChildren.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' Head
        _form.cbHead.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 6.6), checkboxY)
        _form.cbHead.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' Inhabitant
        _form.cbInhabitant.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 7.4), checkboxY)
        _form.cbInhabitant.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' Indigenous People
        _form.cbIndigenousPeople.Location = New Point(checkboxStartX + CInt(checkboxSpacing * 8.2), checkboxY)
        _form.cbIndigenousPeople.Font = New Font("Arial", checkboxFontSize, FontStyle.Bold)

        ' === ADDITIONAL INFORMATION ===
        _form.AdditionalInforlbl.Location = New Point(CInt(panelWidth * 0.026), CInt(panelHeight * 0.832))
        _form.AdditionalInforlbl.Font = New Font("Arial", labelFontSize, FontStyle.Bold)
        _form.txtAdditionalInfo.Location = New Point(CInt(panelWidth * 0.026), CInt(panelHeight * 0.853))
        _form.txtAdditionalInfo.Size = New Size(CInt(panelWidth * 0.94), CInt(panelHeight * 0.026))
        _form.txtAdditionalInfo.Font = New Font("Arial", labelFontSize, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position action buttons (Save, Back)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.117)
        Dim btnHeight As Integer = CInt(panelHeight * 0.044)
        Dim btnY As Integer = CInt(panelHeight * 0.914)

        ' Save Button - Designer: Location(614, 918)
        _form.btnSave.Location = New Point(CInt(panelWidth * 0.361), btnY)
        _form.btnSave.Size = New Size(btnWidth, btnHeight)
        _form.btnSave.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnSave.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.btnSave.Cursor = Cursors.Hand

        ' Back Button - Designer: Location(871, 918)
        _form.btnBack.Location = New Point(CInt(panelWidth * 0.512), btnY)
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