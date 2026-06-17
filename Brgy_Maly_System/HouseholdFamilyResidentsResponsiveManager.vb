' ================================================================================
' FILE: HouseholdFamilyResidentsResponsiveManager.vb
' LAYER: Universal Responsive Scaling Layer
' CHANGE (this revision):
'   PART 1 FIX — PositionBottomDivider and PositionActionButtons Y-coordinates
'   pushed down so they sit cleanly below txtAdditionalInfo on all resolutions.
'
'   Root cause: the old button Y (0.914) was calculated against the fixed designer
'   baseline but did not account for the dynamic CLB height + Additional Info
'   stack that can push txtAdditionalInfo as low as ~Y = 0.923 on standard
'   1004 px height.  The divider now trails the Additional Info field by a
'   fixed gap derived from ph, and buttons sit one row below the divider.
' ================================================================================
Imports Microsoft.Win32

Public Class HouseholdFamilyResidentsResponsiveManager

    ' ── Designer baseline ────────────────────────────────────────────────────────
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' ── Floor (1366×768 minimum) ─────────────────────────────────────────────────
    Private Const MIN_WIDTH As Integer = 640
    Private Const MIN_HEIGHT As Integer = 400

    Private ReadOnly _form As HouseholdFamilyResidents_Form

    Private ReadOnly _resizeTimer As New System.Windows.Forms.Timer With {.Interval = 300}
    Private _layoutReady As Boolean = False

    ' ============================================================================
    ' CONSTRUCTOR
    ' ============================================================================
    Public Sub New(form As HouseholdFamilyResidents_Form)
        _form = form
    End Sub

    ' ============================================================================
    ' INITIALIZE
    ' ============================================================================
    Public Sub Initialize()
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Location = New Point(0, 0)
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)

        AddHandler _resizeTimer.Tick, AddressOf OnResizeTimerTick
        AddHandler _form.Resize, AddressOf OnFormResize
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged

        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        _layoutReady = True
    End Sub

    ' ============================================================================
    ' EVENT HANDLERS
    ' ============================================================================

    Private Sub OnDisplaySettingsChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    Private Sub OnFormResize(sender As Object, e As EventArgs)
        If Not _layoutReady Then Return
        _resizeTimer.Stop()
        _resizeTimer.Start()
    End Sub

    Private Sub OnResizeTimerTick(sender As Object, e As EventArgs)
        _resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ' ============================================================================
    ' MASTER LAYOUT ORCHESTRATOR
    ' ============================================================================
    Public Sub CalculateAndApplyLayout()
        Dim pw As Integer = Math.Max(_form.ClientSize.Width, MIN_WIDTH)
        Dim ph As Integer = Math.Max(_form.ClientSize.Height, MIN_HEIGHT)
        If pw < 100 OrElse ph < 100 Then Return

        Dim sf As Single = Math.Min(CSng(pw) / ORIGINAL_WIDTH,
                                    CSng(ph) / ORIGINAL_HEIGHT)

        _form.FillPanel.Size = New Size(pw, ph)
        _form.FillPanel.Location = New Point(0, 0)

        PositionTitleSection(pw, ph, sf)
        PositionFirstDivider(pw, ph)
        PositionBasicInfoSection(pw, ph, sf)
        PositionSecondDivider(pw, ph)
        PositionPersonalAndContactSection(pw, ph, sf)
        PositionThirdDivider(pw, ph)

        ' --- Returns the bottom Y of txtAdditionalInfo so the divider/buttons
        '     can be anchored dynamically below it. ---
        Dim addInfoBottomY As Integer =
            PositionHouseholdAndCategorySection(pw, ph, sf)

        PositionBottomDividerDynamic(pw, ph, addInfoBottomY)
        PositionActionButtonsDynamic(pw, ph, sf, addInfoBottomY)
    End Sub

    ' ============================================================================
    ' SECTION: Title
    ' ============================================================================
    Private Sub PositionTitleSection(pw As Integer, ph As Integer, sf As Single)
        _form.FamilyResidentlbl.Location = New Point(CInt(pw * 0.012), CInt(ph * 0.03))
        _form.FamilyResidentlbl.Font = New Font("Arial",
                                                     Math.Max(9, 20.25F * sf),
                                                     FontStyle.Bold)
        _form.FamilyResidentlbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ' ============================================================================
    ' SECTION: First divider — LinePnl1 @ Designer (0, 75, 1700×2)
    ' ============================================================================
    Private Sub PositionFirstDivider(pw As Integer, ph As Integer)
        _form.LinePnl1.Location = New Point(0, CInt(ph * 0.075))
        _form.LinePnl1.Size = New Size(pw, 2)
        _form.LinePnl1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ' ============================================================================
    ' SECTION: Basic Information — LastName / FirstName / MiddleName / Suffix
    ' ============================================================================
    Private Sub PositionBasicInfoSection(pw As Integer, ph As Integer, sf As Single)
        Dim lm As Integer = CInt(pw * 0.026)
        Dim fw As Integer = CInt(pw * 0.941)
        Dim fh As Integer = CInt(ph * 0.029)
        Dim lfs As Single = Math.Max(7, 12.0F * sf)
        Dim tfs As Single = Math.Max(7, 14.25F * sf)

        _form.BasicInfolbl.Location = New Point(CInt(pw * 0.44), CInt(ph * 0.097))
        _form.BasicInfolbl.Font = New Font("Arial", Math.Max(9, 15.75F * sf), FontStyle.Bold)

        _form.LastNamelbl.Location = New Point(lm, CInt(ph * 0.136))
        _form.LastNamelbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.txtLastName.Location = New Point(lm, CInt(ph * 0.158))
        _form.txtLastName.Size = New Size(fw, fh)
        _form.txtLastName.Font = New Font("Arial", tfs, FontStyle.Regular)

        _form.FirstNamelbl.Location = New Point(lm, CInt(ph * 0.202))
        _form.FirstNamelbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.txtFirstName.Location = New Point(lm, CInt(ph * 0.224))
        _form.txtFirstName.Size = New Size(fw, fh)
        _form.txtFirstName.Font = New Font("Arial", tfs, FontStyle.Regular)

        _form.MiddleNamelbl.Location = New Point(lm, CInt(ph * 0.269))
        _form.MiddleNamelbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.txtMiddleName.Location = New Point(lm, CInt(ph * 0.291))
        _form.txtMiddleName.Size = New Size(fw, fh)
        _form.txtMiddleName.Font = New Font("Arial", tfs, FontStyle.Regular)

        _form.Suffixlbl.Location = New Point(lm, CInt(ph * 0.333))
        _form.Suffixlbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.txtSuffix.Location = New Point(lm, CInt(ph * 0.355))
        _form.txtSuffix.Size = New Size(fw, fh)
        _form.txtSuffix.Font = New Font("Arial", tfs, FontStyle.Regular)
    End Sub

    ' ============================================================================
    ' SECTION: Second divider — LinePnl2 @ Designer (0, 415, 1700×2)
    ' ============================================================================
    Private Sub PositionSecondDivider(pw As Integer, ph As Integer)
        _form.LinePnl2.Location = New Point(0, CInt(ph * 0.413))
        _form.LinePnl2.Size = New Size(pw, 2)
        _form.LinePnl2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ' ============================================================================
    ' SECTION: Personal (left) + Contact (right)
    ' ============================================================================
    Private Sub PositionPersonalAndContactSection(pw As Integer, ph As Integer, sf As Single)
        Dim lm As Integer = CInt(pw * 0.026)
        Dim lfw As Integer = CInt(pw * 0.453)
        Dim rm As Integer = CInt(pw * 0.512)
        Dim rfw As Integer = CInt(pw * 0.456)
        Dim fh As Integer = CInt(ph * 0.026)
        Dim lfs As Single = Math.Max(7, 12.0F * sf)

        _form.PersonalDetailslbl.Location = New Point(CInt(pw * 0.171), CInt(ph * 0.434))
        _form.PersonalDetailslbl.Font = New Font("Arial", Math.Max(9, 15.75F * sf), FontStyle.Bold)

        _form.DateofBirthlbl.Location = New Point(lm, CInt(ph * 0.46))
        _form.DateofBirthlbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.DTPDateofBirth.Location = New Point(lm, CInt(ph * 0.488))
        _form.DTPDateofBirth.Size = New Size(lfw, fh)
        _form.DTPDateofBirth.Font = New Font("Arial", lfs, FontStyle.Regular)

        _form.Sexlbl.Location = New Point(lm, CInt(ph * 0.535))
        _form.Sexlbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.cbSex.Location = New Point(lm, CInt(ph * 0.558))
        _form.cbSex.Size = New Size(lfw, fh)
        _form.cbSex.Font = New Font("Arial", lfs, FontStyle.Regular)

        _form.CivilStatuslbl.Location = New Point(lm, CInt(ph * 0.611))
        _form.CivilStatuslbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.cbCivilStatus.Location = New Point(lm, CInt(ph * 0.634))
        _form.cbCivilStatus.Size = New Size(lfw, fh)
        _form.cbCivilStatus.Font = New Font("Arial", lfs, FontStyle.Regular)

        _form.ContactInfolbl.Location = New Point(CInt(pw * 0.685), CInt(ph * 0.434))
        _form.ContactInfolbl.Font = New Font("Arial", Math.Max(9, 15.75F * sf), FontStyle.Bold)

        _form.ContactNumberlbl.Location = New Point(rm, CInt(ph * 0.467))
        _form.ContactNumberlbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.txtContactNum.Location = New Point(rm, CInt(ph * 0.488))
        _form.txtContactNum.Size = New Size(rfw, fh)
        _form.txtContactNum.Font = New Font("Arial", lfs, FontStyle.Regular)

        _form.EmailAddresslbl.Location = New Point(rm, CInt(ph * 0.536))
        _form.EmailAddresslbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.txtEmailAddress.Location = New Point(rm, CInt(ph * 0.558))
        _form.txtEmailAddress.Size = New Size(rfw, fh)
        _form.txtEmailAddress.Font = New Font("Arial", lfs, FontStyle.Regular)

        _form.LinePnl3.Location = New Point(CInt(pw * 0.492), CInt(ph * 0.417))
        _form.LinePnl3.Size = New Size(2, CInt(ph * 0.3))
        _form.LinePnl3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
    End Sub

    ' ============================================================================
    ' SECTION: Third divider — LinePnl4
    ' ============================================================================
    Private Sub PositionThirdDivider(pw As Integer, ph As Integer)
        _form.LinePnl4.Location = New Point(0, CInt(ph * 0.713))
        _form.LinePnl4.Size = New Size(pw, 2)
        _form.LinePnl4.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ' ============================================================================
    ' SECTION: Household + Category + Additional Info
    ' Returns the bottom Y pixel of txtAdditionalInfo so callers can anchor
    ' the bottom divider and buttons below it dynamically.
    ' ============================================================================
    Private Function PositionHouseholdAndCategorySection(pw As Integer,
                                                         ph As Integer,
                                                         sf As Single) As Integer
        Dim rm As Integer = CInt(pw * 0.512)
        Dim rfw As Integer = CInt(pw * 0.456)
        Dim lm As Integer = CInt(pw * 0.026)
        Dim fw As Integer = CInt(pw * 0.941)
        Dim fh As Integer = CInt(ph * 0.026)
        Dim lfs As Single = Math.Max(7, 12.0F * sf)

        ' Household (right column)
        _form.HouseInfolbl.Location = New Point(CInt(pw * 0.677), CInt(ph * 0.632))
        _form.HouseInfolbl.Font = New Font("Arial", Math.Max(9, 15.75F * sf), FontStyle.Bold)

        _form.Householdbl.Location = New Point(rm, CInt(ph * 0.651))
        _form.Householdbl.Font = New Font("Arial", lfs, FontStyle.Bold)

        _form.txtHouseholdNumber.Location = New Point(rm, CInt(ph * 0.672))
        _form.txtHouseholdNumber.Size = New Size(rfw, fh)
        _form.txtHouseholdNumber.Font = New Font("Arial", lfs, FontStyle.Regular)

        ' Right-half sub-divider
        _form.LinePnl6.Location = New Point(CInt(pw * 0.492), CInt(ph * 0.617))
        _form.LinePnl6.Size = New Size(CInt(pw * 0.508), 2)
        _form.LinePnl6.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        ' Category heading + CLB
        _form.Categorylbl.Location = New Point(CInt(pw * 0.44), CInt(ph * 0.728))
        _form.Categorylbl.Font = New Font("Arial", Math.Max(9, 15.75F * sf), FontStyle.Bold)

        Dim clbY As Integer = CInt(ph * 0.752)
        Dim clbH As Integer = Math.Max(80, CInt(ph * 0.103))
        _form.CategoriesCLB.Location = New Point(lm, clbY)
        _form.CategoriesCLB.Size = New Size(fw, clbH)
        _form.CategoriesCLB.Font = New Font("Arial", Math.Max(7, 11.25F * sf), FontStyle.Regular)

        ' Additional Info — anchored below CLB with a 1.5 % gap
        Dim addGap As Integer = CInt(ph * 0.015)
        Dim addRowGap As Integer = CInt(ph * 0.022)
        Dim addLblY As Integer = clbY + clbH + addGap
        Dim addTxtY As Integer = addLblY + addRowGap

        _form.AdditionalInforlbl.Location = New Point(lm, addLblY)
        _form.AdditionalInforlbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.txtAdditionalInfo.Location = New Point(lm, addTxtY)
        _form.txtAdditionalInfo.Size = New Size(fw, fh)
        _form.txtAdditionalInfo.Font = New Font("Arial", lfs, FontStyle.Regular)

        ' Return the pixel coordinate of the BOTTOM EDGE of txtAdditionalInfo
        Return addTxtY + fh
    End Function

    ' ============================================================================
    ' SECTION: Bottom divider — placed 2.0 % of ph BELOW txtAdditionalInfo
    ' Replaces the old fixed-percentage PositionBottomDivider.
    ' ============================================================================
    Private Sub PositionBottomDividerDynamic(pw As Integer, ph As Integer,
                                              addInfoBottomY As Integer)
        Dim divY As Integer = addInfoBottomY + CInt(ph * 0.02)
        _form.LinePnl5.Location = New Point(0, divY)
        _form.LinePnl5.Size = New Size(pw, 2)
        _form.LinePnl5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ' ============================================================================
    ' SECTION: Action buttons — placed 1.0 % below the bottom divider
    ' Replaces the old fixed-percentage PositionActionButtons.
    ' ============================================================================
    Private Sub PositionActionButtonsDynamic(pw As Integer, ph As Integer, sf As Single,
                                              addInfoBottomY As Integer)
        ' Divider sits at addInfoBottomY + 2.0 %; buttons sit 1.0 % below that
        Dim divY As Integer = addInfoBottomY + CInt(ph * 0.02)
        Dim by As Integer = divY + CInt(ph * 0.01)
        Dim bw As Integer = CInt(pw * 0.117)
        Dim bh As Integer = Math.Max(28, CInt(ph * 0.044))
        Dim font As Single = Math.Max(7, 11.25F * sf)

        ' Clamp: if the computed Y would push buttons off-screen, pull it back
        If by + bh > ph - 2 Then by = ph - bh - 2

        ' Save — left of centre (designer X = 614 / 1700 = 36.1 %)
        _form.btnSave.Location = New Point(CInt(pw * 0.361), by)
        _form.btnSave.Size = New Size(bw, bh)
        _form.btnSave.Font = New Font("Arial Narrow", font, FontStyle.Bold)
        _form.btnSave.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnSave.Cursor = Cursors.Hand

        ' Back — right of centre (designer X = 871 / 1700 = 51.2 %)
        _form.btnBack.Location = New Point(CInt(pw * 0.512), by)
        _form.btnBack.Size = New Size(bw, bh)
        _form.btnBack.Font = New Font("Arial Narrow", font, FontStyle.Bold)
        _form.btnBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnBack.Cursor = Cursors.Hand
    End Sub

    ' ============================================================================
    ' CLEANUP
    ' ============================================================================
    Public Sub Cleanup()
        _resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged
    End Sub

End Class
