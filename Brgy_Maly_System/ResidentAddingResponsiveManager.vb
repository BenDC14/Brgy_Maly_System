Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for ResidentAdding_Form
''' Handles all layout calculations, positioning, and font scaling for a complex multi-section form
''' </summary>
Public Class ResidentAddingResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As ResidentAdding_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As ResidentAdding_Form)
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

        ' === POSITION ALL SECTIONS ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)
        PositionPersonalInfoSection(panelWidth, panelHeight, scaleFactor)
        PositionDemographicInfoSection(panelWidth, panelHeight, scaleFactor)
        PositionContactInfoSection(panelWidth, panelHeight, scaleFactor)
        PositionAdditionalInfoSection(panelWidth, panelHeight, scaleFactor)
        PositionHouseholdInfoSection(panelWidth, panelHeight, scaleFactor)
        PositionCategorySection(panelWidth, panelHeight, scaleFactor)
        PositionAdditionalInfoFieldSection(panelWidth, panelHeight, scaleFactor)
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title section
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' ResidentInfolbl - Designer: Location(20, 30)
        _form.ResidentInfolbl.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.03))
        _form.ResidentInfolbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.ResidentInfolbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' LinePnl - Designer: Location(0, 75), Size(1700, 2)
        _form.LinePnl.Location = New Point(0, CInt(panelHeight * 0.075))
        _form.LinePnl.Size = New Size(panelWidth, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Personal Information section
    ''' </summary>
    Private Sub PositionPersonalInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.025)
        Dim leftFieldWidth As Integer = CInt(panelWidth * 0.389)
        Dim rightMargin As Integer = CInt(panelWidth * 0.537)
        Dim rightFieldWidth As Integer = CInt(panelWidth * 0.389)

        ' Section Title - Designer: Location(704, 80)
        _form.PersonalInfolbl.Location = New Point(CInt(panelWidth * 0.414), CInt(panelHeight * 0.08))
        _form.PersonalInfolbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' === LEFT COLUMN ===
        ' Last Name - Designer: Location(42, 118), txtLname(46, 141), Size(661, 26)
        _form.LastNamelbl.Location = New Point(leftMargin, CInt(panelHeight * 0.118))
        _form.LastNamelbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtLname.Location = New Point(leftMargin, CInt(panelHeight * 0.14))
        _form.txtLname.Size = New Size(leftFieldWidth, CInt(panelHeight * 0.026))
        _form.txtLname.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' First Name - Designer: Location(42, 177), txtFname(46, 200)
        _form.FirstNamelbl.Location = New Point(leftMargin, CInt(panelHeight * 0.176))
        _form.FirstNamelbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtFname.Location = New Point(leftMargin, CInt(panelHeight * 0.199))
        _form.txtFname.Size = New Size(leftFieldWidth, CInt(panelHeight * 0.026))
        _form.txtFname.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Middle Name - Designer: Location(42, 236), txtMname(46, 259), Size(580, 26)
        _form.MiddleNamelbl.Location = New Point(leftMargin, CInt(panelHeight * 0.235))
        _form.MiddleNamelbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtMname.Location = New Point(leftMargin, CInt(panelHeight * 0.258))
        _form.txtMname.Size = New Size(CInt(panelWidth * 0.341), CInt(panelHeight * 0.026))
        _form.txtMname.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Suffix - Designer: Location(628, 237), txtSuffix(632, 259), Size(75, 26)
        _form.Suffixlbl.Location = New Point(CInt(panelWidth * 0.37), CInt(panelHeight * 0.236))
        _form.Suffixlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtSuffix.Location = New Point(CInt(panelWidth * 0.372), CInt(panelHeight * 0.258))
        _form.txtSuffix.Size = New Size(CInt(panelWidth * 0.044), CInt(panelHeight * 0.026))
        _form.txtSuffix.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' === RIGHT COLUMN ===
        ' Date of Birth - Designer: Location(908, 118), DTPDateofBirth(912, 146), Size(661, 26)
        _form.DateofBirthlbl.Location = New Point(rightMargin, CInt(panelHeight * 0.118))
        _form.DateofBirthlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.DTPDateofBirth.Location = New Point(rightMargin, CInt(panelHeight * 0.145))
        _form.DTPDateofBirth.Size = New Size(rightFieldWidth, CInt(panelHeight * 0.026))
        _form.DTPDateofBirth.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Place of Birth - Designer: Location(908, 178), txtPlaceofBirth(912, 200)
        _form.PlaceofBirthlbl.Location = New Point(rightMargin, CInt(panelHeight * 0.177))
        _form.PlaceofBirthlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtPlaceofBirth.Location = New Point(rightMargin, CInt(panelHeight * 0.199))
        _form.txtPlaceofBirth.Size = New Size(rightFieldWidth, CInt(panelHeight * 0.026))
        _form.txtPlaceofBirth.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Sex - Designer: Location(908, 236), cbSex(912, 259)
        _form.Sexlbl.Location = New Point(rightMargin, CInt(panelHeight * 0.235))
        _form.Sexlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.cbSex.Location = New Point(rightMargin, CInt(panelHeight * 0.258))
        _form.cbSex.Size = New Size(rightFieldWidth, CInt(panelHeight * 0.026))
        _form.cbSex.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Divider - Designer: Location(0, 309), Size(1700, 2)
        _form.LinePnl2.Location = New Point(0, CInt(panelHeight * 0.308))
        _form.LinePnl2.Size = New Size(panelWidth, 2)
    End Sub

    ''' <summary>
    ''' Position Demographic Information section
    ''' </summary>
    Private Sub PositionDemographicInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.025)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.389)

        ' Section Title - Designer: Location(249, 314)
        _form.DemographicInfolbl.Location = New Point(CInt(panelWidth * 0.146), CInt(panelHeight * 0.313))
        _form.DemographicInfolbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' Civil Status - Designer: Location(42, 338), cbCivilStatus(46, 361), Size(661, 26)
        _form.CivilStatuslbl.Location = New Point(leftMargin, CInt(panelHeight * 0.337))
        _form.CivilStatuslbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.cbCivilStatus.Location = New Point(leftMargin, CInt(panelHeight * 0.36))
        _form.cbCivilStatus.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.cbCivilStatus.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Religion - Designer: Location(42, 396), txtReligion(46, 417)
        _form.Religionlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.395))
        _form.Religionlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtReligion.Location = New Point(leftMargin, CInt(panelHeight * 0.415))
        _form.txtReligion.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtReligion.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Citizenship - Designer: Location(42, 452), txtCitezenship(46, 475)
        _form.Citizenshiplbl.Location = New Point(leftMargin, CInt(panelHeight * 0.45))
        _form.Citizenshiplbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtCitezenship.Location = New Point(leftMargin, CInt(panelHeight * 0.473))
        _form.txtCitezenship.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtCitezenship.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Occupation - Designer: Location(42, 511), txtOccupation(46, 534)
        _form.Occupationlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.509))
        _form.Occupationlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtOccupation.Location = New Point(leftMargin, CInt(panelHeight * 0.532))
        _form.txtOccupation.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtOccupation.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Voter - Designer: Location(42, 570), CbYes(204, 585), CbNo(465, 585)
        _form.Voterlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.568))
        _form.Voterlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.CbYes.Location = New Point(CInt(panelWidth * 0.12), CInt(panelHeight * 0.583))
        _form.CbYes.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)
        _form.CbNo.Location = New Point(CInt(panelWidth * 0.274), CInt(panelHeight * 0.583))
        _form.CbNo.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Vertical Divider - Designer: Location(808, 309), Size(2, 307)
        _form.LinePnl3.Location = New Point(CInt(panelWidth * 0.475), CInt(panelHeight * 0.308))
        _form.LinePnl3.Size = New Size(2, CInt(panelHeight * 0.306))
    End Sub

    ''' <summary>
    ''' Position Contact Information section
    ''' </summary>
    Private Sub PositionContactInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim rightMargin As Integer = CInt(panelWidth * 0.496)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.429)

        ' Section Title - Designer: Location(1167, 314)
        _form.ContactInfolbl.Location = New Point(CInt(panelWidth * 0.687), CInt(panelHeight * 0.313))
        _form.ContactInfolbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' Contact Number - Designer: Location(840, 344), txtContactNum(844, 365), Size(729, 26)
        _form.ContactNumberlbl.Location = New Point(rightMargin, CInt(panelHeight * 0.343))
        _form.ContactNumberlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtContactNum.Location = New Point(rightMargin, CInt(panelHeight * 0.364))
        _form.txtContactNum.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtContactNum.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Email Address - Designer: Location(840, 396), txtEmailAddress(844, 418)
        _form.EmailAddresslbl.Location = New Point(rightMargin, CInt(panelHeight * 0.395))
        _form.EmailAddresslbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtEmailAddress.Location = New Point(rightMargin, CInt(panelHeight * 0.416))
        _form.txtEmailAddress.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.txtEmailAddress.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Horizontal Divider - Designer: Location(808, 464), Size(892, 2)
        _form.LinePnl4.Location = New Point(CInt(panelWidth * 0.475), CInt(panelHeight * 0.462))
        _form.LinePnl4.Size = New Size(CInt(panelWidth * 0.525), 2)
    End Sub

    ''' <summary>
    ''' Position Additional Information (Education) section
    ''' </summary>
    Private Sub PositionAdditionalInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim rightMargin As Integer = CInt(panelWidth * 0.496)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.429)

        ' Section Title - Designer: Location(1158, 474)
        _form.AdditionalInfolbl.Location = New Point(CInt(panelWidth * 0.681), CInt(panelHeight * 0.472))
        _form.AdditionalInfolbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' Education Level - Designer: Location(840, 496), CbEducationLevel(844, 519), Size(729, 26)
        _form.EducationLevellbl.Location = New Point(rightMargin, CInt(panelHeight * 0.494))
        _form.EducationLevellbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.CbEducationLevel.Location = New Point(rightMargin, CInt(panelHeight * 0.517))
        _form.CbEducationLevel.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.CbEducationLevel.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Educational Status - Designer: Location(840, 552), CbEducationalStatus(844, 575)
        _form.EducationalStatuslbl.Location = New Point(rightMargin, CInt(panelHeight * 0.55))
        _form.EducationalStatuslbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.CbEducationalStatus.Location = New Point(rightMargin, CInt(panelHeight * 0.573))
        _form.CbEducationalStatus.Size = New Size(fieldWidth, CInt(panelHeight * 0.026))
        _form.CbEducationalStatus.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Divider - Designer: Location(0, 614), Size(1700, 2)
        _form.LinePnl5.Location = New Point(0, CInt(panelHeight * 0.612))
        _form.LinePnl5.Size = New Size(panelWidth, 2)
    End Sub

    ''' <summary>
    ''' Position Household Information section
    ''' </summary>
    Private Sub PositionHouseholdInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.025)
        Dim fullWidth As Integer = CInt(panelWidth * 0.898)

        ' Section Title - Designer: Location(704, 619)
        _form.HouseholdInfolbl.Location = New Point(CInt(panelWidth * 0.414), CInt(panelHeight * 0.617))
        _form.HouseholdInfolbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' Household Number - Designer: Location(42, 644), txtHouseholdNumber(46, 665), Size(980, 26)
        _form.HouseholdNumlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.642))
        _form.HouseholdNumlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.cbHouseholdNum.Location = New Point(leftMargin, CInt(panelHeight * 0.663))
        _form.cbHouseholdNum.Size = New Size(CInt(panelWidth * 0.576), CInt(panelHeight * 0.026))
        _form.cbHouseholdNum.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Search Household - Designer: Location(1032, 644), txtSearch(1036, 666), Size(375, 26)
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.607), CInt(panelHeight * 0.642))
        _form.lblSearch.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.609), CInt(panelHeight * 0.664))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.221), CInt(panelHeight * 0.026))
        _form.txtSearch.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Search Button - Designer: Location(1417, 666), Size(156, 26)
        _form.btnSearch.Location = New Point(CInt(panelWidth * 0.833), CInt(panelHeight * 0.664))
        _form.btnSearch.Size = New Size(CInt(panelWidth * 0.092), CInt(panelHeight * 0.026))
        _form.btnSearch.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Cursor = Cursors.Hand

        ' Address Information - Designer: Location(42, 698), txtAddressInfo(46, 719), Size(1527, 26)
        _form.AddressInfolbl.Location = New Point(leftMargin, CInt(panelHeight * 0.695))
        _form.AddressInfolbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtAddressInfo.Location = New Point(leftMargin, CInt(panelHeight * 0.716))
        _form.txtAddressInfo.Size = New Size(fullWidth, CInt(panelHeight * 0.026))
        _form.txtAddressInfo.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)

        ' Divider - Designer: Location(0, 763), Size(1700, 2)
        _form.LinePnl6.Location = New Point(0, CInt(panelHeight * 0.76))
        _form.LinePnl6.Size = New Size(panelWidth, 2)
    End Sub

    ''' <summary>
    ''' Position Category checkboxes
    ''' </summary>
    Private Sub PositionCategorySection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim categoryY As Integer = CInt(panelHeight * 0.796)

        ' Section Title - Designer: Location(759, 768)
        _form.Categorylbl.Location = New Point(CInt(panelWidth * 0.447), CInt(panelHeight * 0.765))
        _form.Categorylbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)

        ' Category checkboxes (11 total) - Position horizontally
        _form.cbSeniorCitizen.Location = New Point(CInt(panelWidth * 0.017), categoryY)
        _form.cbSeniorCitizen.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbPWD.Location = New Point(CInt(panelWidth * 0.12), categoryY)
        _form.cbPWD.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbStudent.Location = New Point(CInt(panelWidth * 0.184), categoryY)
        _form.cbStudent.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbSoloParent.Location = New Point(CInt(panelWidth * 0.262), categoryY)
        _form.cbSoloParent.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbEmployed.Location = New Point(CInt(panelWidth * 0.354), categoryY)
        _form.cbEmployed.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbUnemployed.Location = New Point(CInt(panelWidth * 0.432), categoryY)
        _form.cbUnemployed.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbOFW.Location = New Point(CInt(panelWidth * 0.531), categoryY)
        _form.cbOFW.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbOutofSchoolChildren.Location = New Point(CInt(panelWidth * 0.588), categoryY)
        _form.cbOutofSchoolChildren.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbHead.Location = New Point(CInt(panelWidth * 0.733), categoryY)
        _form.cbHead.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbInhabitant.Location = New Point(CInt(panelWidth * 0.801), categoryY)
        _form.cbInhabitant.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbIndigenousPeople.Location = New Point(CInt(panelWidth * 0.881), categoryY)
        _form.cbIndigenousPeople.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        ' Divider - Designer: Location(0, 900), Size(1700, 2)
        _form.LinePnl7.Location = New Point(0, CInt(panelHeight * 0.896))
        _form.LinePnl7.Size = New Size(panelWidth, 2)
    End Sub

    ''' <summary>
    ''' Position Additional Information field (bottom)
    ''' </summary>
    Private Sub PositionAdditionalInfoFieldSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.025)
        Dim fullWidth As Integer = CInt(panelWidth * 0.898)

        ' Additional Information - Designer: Location(42, 834), txtAdditionalInfo(46, 855), Size(1527, 26)
        _form.AdditionalInforlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.831))
        _form.AdditionalInforlbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.txtAdditionalInfo.Location = New Point(leftMargin, CInt(panelHeight * 0.852))
        _form.txtAdditionalInfo.Size = New Size(fullWidth, CInt(panelHeight * 0.026))
        _form.txtAdditionalInfo.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position action buttons (Add Resident, Back to Main)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.117)
        Dim btnHeight As Integer = CInt(panelHeight * 0.044)
        Dim btnY As Integer = CInt(panelHeight * 0.932)

        ' Add Resident Button - Designer: Location(465, 936), Size(199, 44)
        _form.BtnAddResident.Location = New Point(CInt(panelWidth * 0.274), btnY)
        _form.BtnAddResident.Size = New Size(btnWidth, btnHeight)
        _form.BtnAddResident.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.BtnAddResident.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
        _form.BtnAddResident.Cursor = Cursors.Hand

        ' Back to Main Button - Designer: Location(902, 936), Size(199, 44)
        _form.btnBack.Location = New Point(CInt(panelWidth * 0.531), btnY)
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
