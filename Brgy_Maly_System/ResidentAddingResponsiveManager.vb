Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager for ResidentAdding_Form.
''' Handles all layout calculations, control positioning, and font scaling.
''' Designed against a 1700x1004 designer canvas and scales proportionally
''' to any lower (or higher) monitor resolution without touching the Designer file.
''' </summary>
Public Class ResidentAddingResponsiveManager

    ' === Original designer canvas dimensions ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    Private ReadOnly _form As ResidentAdding_Form

    ' === Debounce timer for resize events ===
    Private _resizeTimer As New System.Windows.Forms.Timer()
    Private _layoutCalculated As Boolean = False

    Public Sub New(form As ResidentAdding_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Call once from Form_Load after InitializeComponent.
    ''' Wires up all resize listeners and runs the first layout pass.
    ''' </summary>
    Public Sub Initialize()
        ' Force fill panel to occupy the full client area
        _form.fillpanel.Dock = DockStyle.Fill
        _form.fillpanel.Location = New Point(0, 0)
        _form.fillpanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)

        ' Debounce: wait 300 ms after the last resize event before recalculating
        _resizeTimer.Interval = 300
        AddHandler _resizeTimer.Tick, AddressOf OnResizeTimerTick

        AddHandler _form.Resize, AddressOf OnFormResize
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged

        ' First-pass layout
        _form.fillpanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        _layoutCalculated = True
    End Sub

    ' ─── Event handlers ──────────────────────────────────────────────────────────

    Private Sub OnDisplaySettingsChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    Private Sub OnFormResize(sender As Object, e As EventArgs)
        If Not _layoutCalculated Then Exit Sub
        _resizeTimer.Stop()
        _resizeTimer.Start()
    End Sub

    Private Sub OnResizeTimerTick(sender As Object, e As EventArgs)
        _resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ' ─── Core layout engine ──────────────────────────────────────────────────────

    ''' <summary>
    ''' Recalculates and applies every control's position/size/font based on
    ''' the current form client area.  Safe to call from any resolution.
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        Dim pw As Integer = _form.ClientSize.Width   ' panel width
        Dim ph As Integer = _form.ClientSize.Height  ' panel height

        ' Guard against minimised / degenerate states
        If pw < 100 OrElse ph < 100 Then Exit Sub

        ' Uniform scale factor – use the smaller axis so nothing is clipped
        Dim scaleW As Single = CSng(pw) / ORIGINAL_WIDTH
        Dim scaleH As Single = CSng(ph) / ORIGINAL_HEIGHT
        Dim sf As Single = Math.Min(scaleW, scaleH)

        _form.fillpanel.Size = New Size(pw, ph)
        _form.fillpanel.Location = New Point(0, 0)

        PositionTitleSection(pw, ph, sf)
        PositionPersonalInfoSection(pw, ph, sf)
        PositionDemographicInfoSection(pw, ph, sf)
        PositionContactInfoSection(pw, ph, sf)
        PositionEducationInfoSection(pw, ph, sf)
        PositionHouseholdInfoSection(pw, ph, sf)
        PositionCategorySection(pw, ph, sf)
        PositionAdditionalInfoFieldSection(pw, ph, sf)
        PositionActionButtons(pw, ph, sf)
    End Sub

    ' ─── Section positioners ─────────────────────────────────────────────────────

    ''' <summary>
    ''' Title bar: "Resident Information" label + horizontal divider.
    ''' Designer: ResidentInfolbl(20,30), LinePnl(0,75,1700×2)
    ''' </summary>
    Private Sub PositionTitleSection(pw As Integer, ph As Integer, sf As Single)
        _form.ResidentInfolbl.Location = New Point(CInt(pw * 0.012), CInt(ph * 0.03))
        _form.ResidentInfolbl.Font = New Font("Arial", 20.25F * sf, FontStyle.Bold)

        _form.LinePnl.Location = New Point(0, CInt(ph * 0.075))
        _form.LinePnl.Size = New Size(pw, 2)
    End Sub

    ''' <summary>
    ''' Personal Information section (left + right columns).
    ''' Designer reference: panel top at Y≈0.08, divider LinePnl2 at Y=309.
    ''' Left column: Last/First/Middle/Suffix  |  Right column: DOB/PlaceOfBirth/Sex
    ''' </summary>
    Private Sub PositionPersonalInfoSection(pw As Integer, ph As Integer, sf As Single)
        Dim lm As Integer = CInt(pw * 0.025)  ' left margin  (≈42 px)
        Dim lfw As Integer = CInt(pw * 0.389)  ' left field width  (≈661 px)
        Dim rm As Integer = CInt(pw * 0.537)  ' right margin  (≈912 px)
        Dim rfw As Integer = CInt(pw * 0.389)  ' right field width (≈661 px)
        Dim fh As Integer = CInt(ph * 0.026)  ' field height  (≈26 px)

        ' Section title – Designer: (704, 80)
        _form.PersonalInfolbl.Location = New Point(CInt(pw * 0.414), CInt(ph * 0.08))
        _form.PersonalInfolbl.Font = New Font("Arial", 15.75F * sf, FontStyle.Bold)

        ' Last Name – Designer label(42,118) / field(46,141)
        _form.LastNamelbl.Location = New Point(lm, CInt(ph * 0.118))
        _form.LastNamelbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtLname.Location = New Point(lm, CInt(ph * 0.14))
        _form.txtLname.Size = New Size(lfw, fh)
        _form.txtLname.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' First Name – Designer label(42,177) / field(46,200)
        _form.FirstNamelbl.Location = New Point(lm, CInt(ph * 0.176))
        _form.FirstNamelbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtFname.Location = New Point(lm, CInt(ph * 0.199))
        _form.txtFname.Size = New Size(lfw, fh)
        _form.txtFname.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Middle Name – Designer label(42,236) / field(46,259) / size(580,26)
        Dim mnw As Integer = CInt(pw * 0.341)  ' ~580 px
        _form.MiddleNamelbl.Location = New Point(lm, CInt(ph * 0.235))
        _form.MiddleNamelbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtMname.Location = New Point(lm, CInt(ph * 0.258))
        _form.txtMname.Size = New Size(mnw, fh)
        _form.txtMname.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Suffix – Designer label(628,237) / field(632,259) / size(75,26)
        _form.Suffixlbl.Location = New Point(CInt(pw * 0.37), CInt(ph * 0.236))
        _form.Suffixlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtSuffix.Location = New Point(CInt(pw * 0.372), CInt(ph * 0.258))
        _form.txtSuffix.Size = New Size(CInt(pw * 0.044), fh)
        _form.txtSuffix.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Date of Birth – Designer label(908,118) / field(912,146)
        _form.DateofBirthlbl.Location = New Point(rm, CInt(ph * 0.118))
        _form.DateofBirthlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.DTPDateofBirth.Location = New Point(rm, CInt(ph * 0.145))
        _form.DTPDateofBirth.Size = New Size(rfw, fh)
        _form.DTPDateofBirth.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Place of Birth – Designer label(908,178) / field(912,200)
        _form.PlaceofBirthlbl.Location = New Point(rm, CInt(ph * 0.177))
        _form.PlaceofBirthlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtPlaceofBirth.Location = New Point(rm, CInt(ph * 0.199))
        _form.txtPlaceofBirth.Size = New Size(rfw, fh)
        _form.txtPlaceofBirth.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Sex – Designer label(908,236) / field(912,259)
        _form.Sexlbl.Location = New Point(rm, CInt(ph * 0.235))
        _form.Sexlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.cbSex.Location = New Point(rm, CInt(ph * 0.258))
        _form.cbSex.Size = New Size(rfw, fh)
        _form.cbSex.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Full-width horizontal divider – Designer: (0,309,1700×2)
        _form.LinePnl2.Location = New Point(0, CInt(ph * 0.308))
        _form.LinePnl2.Size = New Size(pw, 2)
    End Sub

    ''' <summary>
    ''' Demographic Information section (left half).
    ''' Designer: DemographicInfolbl(249,314), divider LinePnl3(808,309,2×307)
    ''' </summary>
    Private Sub PositionDemographicInfoSection(pw As Integer, ph As Integer, sf As Single)
        Dim lm As Integer = CInt(pw * 0.025)
        Dim fw As Integer = CInt(pw * 0.389)
        Dim fh As Integer = CInt(ph * 0.026)

        ' Section title – Designer: (249, 314)
        _form.DemographicInfolbl.Location = New Point(CInt(pw * 0.146), CInt(ph * 0.313))
        _form.DemographicInfolbl.Font = New Font("Arial", 15.75F * sf, FontStyle.Bold)

        ' Civil Status – label(42,338) / field(46,361)
        _form.CivilStatuslbl.Location = New Point(lm, CInt(ph * 0.337))
        _form.CivilStatuslbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.cbCivilStatus.Location = New Point(lm, CInt(ph * 0.36))
        _form.cbCivilStatus.Size = New Size(fw, fh)
        _form.cbCivilStatus.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Religion – label(42,396) / field(46,417)
        _form.Religionlbl.Location = New Point(lm, CInt(ph * 0.395))
        _form.Religionlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtReligion.Location = New Point(lm, CInt(ph * 0.415))
        _form.txtReligion.Size = New Size(fw, fh)
        _form.txtReligion.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Citizenship – label(42,452) / field(46,475)
        _form.Citizenshiplbl.Location = New Point(lm, CInt(ph * 0.45))
        _form.Citizenshiplbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtCitezenship.Location = New Point(lm, CInt(ph * 0.473))
        _form.txtCitezenship.Size = New Size(fw, fh)
        _form.txtCitezenship.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Occupation – label(42,511) / field(46,534)
        _form.Occupationlbl.Location = New Point(lm, CInt(ph * 0.509))
        _form.Occupationlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtOccupation.Location = New Point(lm, CInt(ph * 0.532))
        _form.txtOccupation.Size = New Size(fw, fh)
        _form.txtOccupation.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Voter – label(42,570) / CbYes(204,585) / CbNo(465,585)
        _form.Voterlbl.Location = New Point(lm, CInt(ph * 0.568))
        _form.Voterlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.CbYes.Location = New Point(CInt(pw * 0.12), CInt(ph * 0.583))
        _form.CbYes.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)
        _form.CbNo.Location = New Point(CInt(pw * 0.274), CInt(ph * 0.583))
        _form.CbNo.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Vertical centre divider – Designer: (808,309,2×307)
        _form.LinePnl3.Location = New Point(CInt(pw * 0.475), CInt(ph * 0.308))
        _form.LinePnl3.Size = New Size(2, CInt(ph * 0.306))
    End Sub

    ''' <summary>
    ''' Contact Information section (right half, upper).
    ''' Designer: ContactInfolbl(1167,314), LinePnl4(808,464,892×2)
    ''' </summary>
    Private Sub PositionContactInfoSection(pw As Integer, ph As Integer, sf As Single)
        Dim rm As Integer = CInt(pw * 0.496)  ' right-half start (~844 px)
        Dim fw As Integer = CInt(pw * 0.429)  ' field width (~729 px)
        Dim fh As Integer = CInt(ph * 0.026)

        ' Section title – Designer: (1167, 314)
        _form.ContactInfolbl.Location = New Point(CInt(pw * 0.687), CInt(ph * 0.313))
        _form.ContactInfolbl.Font = New Font("Arial", 15.75F * sf, FontStyle.Bold)

        ' Contact Number – label(840,344) / field(844,365)
        _form.ContactNumberlbl.Location = New Point(rm, CInt(ph * 0.343))
        _form.ContactNumberlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtContactNum.Location = New Point(rm, CInt(ph * 0.364))
        _form.txtContactNum.Size = New Size(fw, fh)
        _form.txtContactNum.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Email Address – label(840,396) / field(844,418)
        _form.EmailAddresslbl.Location = New Point(rm, CInt(ph * 0.395))
        _form.EmailAddresslbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtEmailAddress.Location = New Point(rm, CInt(ph * 0.416))
        _form.txtEmailAddress.Size = New Size(fw, fh)
        _form.txtEmailAddress.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Horizontal sub-divider – Designer: (808,464,892×2)
        _form.LinePnl4.Location = New Point(CInt(pw * 0.475), CInt(ph * 0.462))
        _form.LinePnl4.Size = New Size(CInt(pw * 0.525), 2)
    End Sub

    ''' <summary>
    ''' Additional / Education Information section (right half, lower).
    ''' Designer: AdditionalInfolbl(1158,474), LinePnl5(0,614,1700×2)
    ''' </summary>
    Private Sub PositionEducationInfoSection(pw As Integer, ph As Integer, sf As Single)
        Dim rm As Integer = CInt(pw * 0.496)
        Dim fw As Integer = CInt(pw * 0.429)
        Dim fh As Integer = CInt(ph * 0.026)

        ' Section title – Designer: (1158, 474)
        _form.AdditionalInfolbl.Location = New Point(CInt(pw * 0.681), CInt(ph * 0.472))
        _form.AdditionalInfolbl.Font = New Font("Arial", 15.75F * sf, FontStyle.Bold)

        ' Education Level – label(840,496) / field(844,519)
        _form.EducationLevellbl.Location = New Point(rm, CInt(ph * 0.494))
        _form.EducationLevellbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.CbEducationLevel.Location = New Point(rm, CInt(ph * 0.517))
        _form.CbEducationLevel.Size = New Size(fw, fh)
        _form.CbEducationLevel.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Educational Status – label(840,552) / field(844,575)
        _form.EducationalStatuslbl.Location = New Point(rm, CInt(ph * 0.55))
        _form.EducationalStatuslbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.CbEducationalStatus.Location = New Point(rm, CInt(ph * 0.573))
        _form.CbEducationalStatus.Size = New Size(fw, fh)
        _form.CbEducationalStatus.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Full-width horizontal divider – Designer: (0,614,1700×2)
        _form.LinePnl5.Location = New Point(0, CInt(ph * 0.612))
        _form.LinePnl5.Size = New Size(pw, 2)
    End Sub

    ''' <summary>
    ''' Household Information section (Search + ComboBox + Address).
    ''' Designer: HouseholdInfolbl(216,621), cbHouseholdNum(46,753,705×26)
    ''' </summary>
    Private Sub PositionHouseholdInfoSection(pw As Integer, ph As Integer, sf As Single)
        Dim lm As Integer = CInt(pw * 0.025)
        Dim fh As Integer = CInt(ph * 0.026)

        ' Section title – Designer: (216, 621)
        _form.HouseholdInfolbl.Location = New Point(CInt(pw * 0.127), CInt(ph * 0.617))
        _form.HouseholdInfolbl.Font = New Font("Arial", 15.75F * sf, FontStyle.Bold)

        ' Search label + textbox + button
        ' Designer: lblSearch(42,660) / txtSearch(46,682,564×26) / btnSearch(630,682,119×26)
        _form.lblSearch.Location = New Point(lm, CInt(ph * 0.657))
        _form.lblSearch.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtSearch.Location = New Point(lm, CInt(ph * 0.679))
        _form.txtSearch.Size = New Size(CInt(pw * 0.332), fh)
        _form.txtSearch.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)
        _form.btnSearch.Location = New Point(CInt(pw * 0.371), CInt(ph * 0.679))
        _form.btnSearch.Size = New Size(CInt(pw * 0.07), fh)
        _form.btnSearch.Font = New Font("Arial Narrow", 12.0F * sf, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Cursor = Cursors.Hand

        ' Household Number ComboBox – label(42,731) / field(46,753,705×26)
        _form.HouseholdNumlbl.Location = New Point(lm, CInt(ph * 0.728))
        _form.HouseholdNumlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.cbHouseholdNum.Location = New Point(lm, CInt(ph * 0.749))
        _form.cbHouseholdNum.Size = New Size(CInt(pw * 0.415), fh)
        _form.cbHouseholdNum.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)

        ' Address Information – label(42,798) / field(46,831,705×26)
        _form.AddressInfolbl.Location = New Point(lm, CInt(ph * 0.795))
        _form.AddressInfolbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtAddressInfo.Location = New Point(lm, CInt(ph * 0.827))
        _form.txtAddressInfo.Size = New Size(CInt(pw * 0.415), fh)
        _form.txtAddressInfo.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Category section: vertical divider Panel1, "Category" label, and CategoriesCLB.
    ''' Designer: Panel1(808,615,2×284) / Categorylbl(1222,619) / CategoriesCLB(844,644,824×184)
    ''' The CLB is sized to fill the right column and show all items without overlap.
    ''' </summary>
    Private Sub PositionCategorySection(pw As Integer, ph As Integer, sf As Single)
        ' Vertical divider between Household (left) and Category (right)
        ' Designer: Panel1(808,615,2×284)
        Dim dividerX As Integer = CInt(pw * 0.475)   ' ~808/1700
        Dim dividerY As Integer = CInt(ph * 0.612)   ' ~615/1004
        _form.Panel1.Location = New Point(dividerX, dividerY)
        _form.Panel1.Size = New Size(2, CInt(ph * 0.283))  ' ~284/1004

        ' "Category" section label – Designer: (1222, 619)
        _form.Categorylbl.Location = New Point(CInt(pw * 0.719), CInt(ph * 0.617))
        _form.Categorylbl.Font = New Font("Arial", 15.75F * sf, FontStyle.Bold)

        ' CategoriesCLB – Designer: Location(844,644) / Size(824,184)
        ' Right column starts just after the divider.  Give it generous height
        ' so all checkboxes are visible and items are not cut off.
        Dim clbX As Integer = dividerX + CInt(pw * 0.021)   ' ~844/1700 → divider + small gap
        Dim clbY As Integer = CInt(ph * 0.641)              ' ~644/1004
        Dim clbWidth As Integer = CInt(pw * 0.925) - clbX       ' fill to near right edge
        Dim clbHeight As Integer = CInt(ph * 0.183)              ' ~184/1004; expands proportionally

        ' Enforce a sensible minimum height so items aren't invisible on tiny screens
        If clbHeight < 80 Then clbHeight = 80

        _form.CategoriesCLB.Location = New Point(clbX, clbY)
        _form.CategoriesCLB.Size = New Size(clbWidth, clbHeight)
        _form.CategoriesCLB.Font = New Font("Arial", 11.25F * sf, FontStyle.Regular)

        ' Bottom divider of the whole form – Designer: LinePnl7(0,900,1700×2)
        _form.LinePnl7.Location = New Point(0, CInt(ph * 0.896))
        _form.LinePnl7.Size = New Size(pw, 2)
    End Sub

    ''' <summary>
    ''' Additional Information text field (bottom strip, spans full width).
    ''' Designer: AdditionalInforlbl(840,831) / txtAdditionalInfo(844,853,824×26)
    ''' </summary>
    Private Sub PositionAdditionalInfoFieldSection(pw As Integer, ph As Integer, sf As Single)
        Dim lm As Integer = CInt(pw * 0.496)  ' aligns with right-column start (~844)
        Dim fw As Integer = CInt(pw * 0.925) - lm
        Dim fh As Integer = CInt(ph * 0.026)

        _form.AdditionalInforlbl.Location = New Point(lm, CInt(ph * 0.828))
        _form.AdditionalInforlbl.Font = New Font("Arial", 12.0F * sf, FontStyle.Bold)
        _form.txtAdditionalInfo.Location = New Point(lm, CInt(ph * 0.849))
        _form.txtAdditionalInfo.Size = New Size(fw, fh)
        _form.txtAdditionalInfo.Font = New Font("Arial", 12.0F * sf, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Action buttons: "Save Information" and "Back To Main".
    ''' Designer: BtnAddResident(465,936,199×44) / btnBack(902,936,199×44)
    ''' </summary>
    Private Sub PositionActionButtons(pw As Integer, ph As Integer, sf As Single)
        Dim bw As Integer = CInt(pw * 0.117)   ' ~199/1700
        Dim bh As Integer = CInt(ph * 0.044)   ' ~44/1004
        Dim by As Integer = CInt(ph * 0.932)   ' ~936/1004

        _form.BtnAddResident.Location = New Point(CInt(pw * 0.274), by)
        _form.BtnAddResident.Size = New Size(bw, bh)
        _form.BtnAddResident.Font = New Font("Arial Narrow", 11.25F * sf, FontStyle.Bold)
        _form.BtnAddResident.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.BtnAddResident.Cursor = Cursors.Hand

        _form.btnBack.Location = New Point(CInt(pw * 0.531), by)
        _form.btnBack.Size = New Size(bw, bh)
        _form.btnBack.Font = New Font("Arial Narrow", 11.25F * sf, FontStyle.Bold)
        _form.btnBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnBack.Cursor = Cursors.Hand
    End Sub

    ' ─── Cleanup ─────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Remove event listeners to prevent memory leaks when the form closes.
    ''' Call from OnFormClosing.
    ''' </summary>
    Public Sub Cleanup()
        _resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged
    End Sub

End Class
