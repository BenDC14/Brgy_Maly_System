' ================================================================================
' FILE: HouseholdAddNewFamilyResponsiveManager.vb
' LAYER: Universal Responsive Scaling Layer
' BASELINE: Designer canvas 1700 × 1004 px  (HouseholdAddNewFamily_Form.Designer.vb)
'
' Layout zones (Y% of 1004 baseline):
'   Title + subtitle label    0   –  7.4 %   (Y=30,  AddNewFamilylbl)
'   Divider                   (implicit, no LinePnl in Designer for top)
'   Header fields row         10  – 22.0 %   (Y=106–155, Household/FamilyName/FamilyHead)
'   Save Family button        17  – 21.0 %   (Y=172–208, btnSaveFamily)
'   Divider                   24.9 %         (Y=250, LinePnl)
'   Families label            26.9 %         (Y=270, Familieslbl)
'   FamilyMembersDGV          29.6 – 81.1 % (Y=297–815, size=1633×518)
'   Relationship type row     84.6 %         (Y=849–875, CivilStatuslbl + cbCivilStatus)
'   Buttons                   92.2 %         (Y=926, BtnSaveRelationship + btnBack)
' ================================================================================
Imports Microsoft.Win32

Public Class HouseholdAddNewFamilyResponsiveManager

    ' ── Designer baseline ────────────────────────────────────────────────────────
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' ── Floor (1366×768 minimum) ─────────────────────────────────────────────────
    Private Const MIN_WIDTH As Integer = 640
    Private Const MIN_HEIGHT As Integer = 400

    Private ReadOnly _form As HouseholdAddNewFamily_Form

    Private ReadOnly _resizeTimer As New System.Windows.Forms.Timer With {.Interval = 280}
    Private _layoutReady As Boolean = False

    ' ============================================================================
    ' CONSTRUCTOR
    ' ============================================================================
    Public Sub New(form As HouseholdAddNewFamily_Form)
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
        PositionHeaderFieldsRow(pw, ph, sf)
        PositionSaveFamilyButton(pw, ph, sf)
        PositionDivider(pw, ph)
        PositionFamiliesLabel(pw, ph, sf)
        PositionFamilyMembersGrid(pw, ph, sf)
        PositionRelationshipRow(pw, ph, sf)
        PositionActionButtons(pw, ph, sf)
    End Sub

    ' ============================================================================
    ' SECTION: Title — "Add New Family"
    '   AddNewFamilylbl @ Designer (30, 30) → 1.76 % × 2.99 %
    '   Font: Arial 20.25 pt Bold
    ' ============================================================================
    Private Sub PositionTitleSection(pw As Integer, ph As Integer, sf As Single)
        _form.AddNewFamilylbl.Location = New Point(CInt(pw * 0.0176), CInt(ph * 0.0299))
        _form.AddNewFamilylbl.Font = New Font("Arial",
                                                   Math.Max(9, 20.25F * sf),
                                                   FontStyle.Bold)
        _form.AddNewFamilylbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ' ============================================================================
    ' SECTION: Header Fields Row — Household | FamilyName | FamilyHead (CB)
    '   Three equal-width fields across the full usable width.
    '   Designer refs:
    '     txtHousehold  (203, 106/129, w=384)  → X=11.9 %, Y=10.6/12.9 %
    '     txtFamilyName (623, 106/129, w=384)  → X=36.6 %, Y=10.6/12.9 %
    '     txtFamilyHead (1051,106/129, w=384)  → X=61.8 %, Y=10.6/12.9 %
    '   Field width 384/1700 = 22.6 %, height 26/1004 = 2.6 %
    ' ============================================================================
    Private Sub PositionHeaderFieldsRow(pw As Integer, ph As Integer, sf As Single)
        Dim lfs As Single = Math.Max(7, 12.0F * sf)
        Dim fh As Integer = Math.Max(20, CInt(ph * 0.026))
        Dim fw As Integer = CInt(pw * 0.226)  ' ≈ 384 px at baseline

        Dim lblY As Integer = CInt(ph * 0.106)
        Dim fldY As Integer = CInt(ph * 0.129)

        ' Column X origins (left edges)
        Dim x1 As Integer = CInt(pw * 0.119)   ' Household  ≈ 203 px
        Dim x2 As Integer = CInt(pw * 0.366)   ' FamilyName ≈ 623 px
        Dim x3 As Integer = CInt(pw * 0.618)   ' FamilyHead ≈ 1051 px

        ' Household (read-only)
        _form.Householdlbl.Location = New Point(x1, lblY)
        _form.Householdlbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.txtHousehold.Location = New Point(x1, fldY)
        _form.txtHousehold.Size = New Size(fw, fh)
        _form.txtHousehold.Font = New Font("Arial", lfs, FontStyle.Regular)

        ' Family Name
        _form.FamilyNamelbl.Location = New Point(x2, lblY)
        _form.FamilyNamelbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.txtFamilyName.Location = New Point(x2, fldY)
        _form.txtFamilyName.Size = New Size(fw, fh)
        _form.txtFamilyName.Font = New Font("Arial", lfs, FontStyle.Regular)

        ' Family Head (dynamic ComboBox overlays hidden txtFamilyHead)
        _form.FamilyHeadlbl.Location = New Point(x3, lblY)
        _form.FamilyHeadlbl.Font = New Font("Arial", lfs, FontStyle.Bold)

        ' Keep designer TextBox hidden; resize/reposition the dynamic CB
        _form.familyheadcb.Location = New Point(x3, fldY)
        _form.familyheadcb.Size = New Size(fw, fh)

        If _form.cbFamilyHead IsNot Nothing Then
            _form.cbFamilyHead.Location = New Point(x3, fldY)
            _form.cbFamilyHead.Size = New Size(fw, fh)
            _form.cbFamilyHead.Font = New Font("Arial", lfs, FontStyle.Regular)
        End If
    End Sub

    ' ============================================================================
    ' SECTION: Save Family button
    '   btnSaveFamily @ Designer (203, 172, w=1232, h=36) → 11.9/17.1 %, w=72.5 %, h=3.6 %
    ' ============================================================================
    Private Sub PositionSaveFamilyButton(pw As Integer, ph As Integer, sf As Single)
        Dim x As Integer = CInt(pw * 0.119)
        Dim y As Integer = CInt(ph * 0.171)
        Dim bw As Integer = CInt(pw * 0.725)   ' spans all three field columns
        Dim bh As Integer = Math.Max(28, CInt(ph * 0.036))
        Dim fs As Single = Math.Max(7, 11.25F * sf)

        _form.btnSaveFamily.Location = New Point(x, y)
        _form.btnSaveFamily.Size = New Size(bw, bh)
        _form.btnSaveFamily.Font = New Font("Arial Narrow", fs, FontStyle.Bold)
        _form.btnSaveFamily.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.btnSaveFamily.Cursor = Cursors.Hand
    End Sub

    ' ============================================================================
    ' SECTION: Full-width horizontal divider
    '   LinePnl @ Designer (0, 250, 1700×2) → Y = 24.9 %
    ' ============================================================================
    Private Sub PositionDivider(pw As Integer, ph As Integer)
        _form.LinePnl.Location = New Point(0, CInt(ph * 0.249))
        _form.LinePnl.Size = New Size(pw, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ' ============================================================================
    ' SECTION: "Families" section label
    '   Familieslbl @ Designer (32, 270) → 1.88 % × 26.9 %
    ' ============================================================================
    Private Sub PositionFamiliesLabel(pw As Integer, ph As Integer, sf As Single)
        _form.Familieslbl.Location = New Point(CInt(pw * 0.019), CInt(ph * 0.269))
        _form.Familieslbl.Font = New Font("Arial", Math.Max(9, 15.75F * sf), FontStyle.Bold)
        _form.Familieslbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ' ============================================================================
    ' SECTION: FamilyMembersDGV
    '   Designer: (34, 297, 1633×518) → X=2.0 %, Y=29.6 %, W=96.1 %, H=51.6 %
    ' ============================================================================
    Private Sub PositionFamilyMembersGrid(pw As Integer, ph As Integer, sf As Single)
        Dim lm As Integer = CInt(pw * 0.02)
        Dim gw As Integer = CInt(pw * 0.961)
        ' DGV height: from Y≈29.6 % down to Y≈81.0 % (leave room for relationship row + buttons)
        Dim gy As Integer = CInt(ph * 0.296)
        Dim gh As Integer = CInt(ph * 0.515)   ' ≈ 517 px at 1004 baseline
        If gh < 80 Then gh = 80   ' minimum visible

        _form.FamilyMembersDGV.Location = New Point(lm, gy)
        _form.FamilyMembersDGV.Size = New Size(gw, gh)
        _form.FamilyMembersDGV.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or
                                          AnchorStyles.Right Or AnchorStyles.Bottom

        ' Scale header/row fonts with the form
        _form.FamilyMembersDGV.ColumnHeadersHeight = Math.Max(24, CInt(35 * sf))
        _form.FamilyMembersDGV.RowTemplate.Height = Math.Max(20, CInt(32 * sf))
        _form.FamilyMembersDGV.DefaultCellStyle.Font =
            New Font("Arial", Math.Max(7, 10.0F * sf), FontStyle.Regular)
        _form.FamilyMembersDGV.ColumnHeadersDefaultCellStyle.Font =
            New Font("Arial", Math.Max(7, 11.0F * sf), FontStyle.Bold)
    End Sub

    ' ============================================================================
    ' SECTION: Relationship row — label + cbCivilStatus
    '   Designer:
    '     CivilStatuslbl (30, 852, "Relationship Type:") → 1.76 % × 84.9 %
    '     cbCivilStatus  (188, 849, w=509, h=26)         → 11.1 % × 84.6 %
    '                                                       w = 29.9 %,  h = 2.6 %
    ' ============================================================================
    Private Sub PositionRelationshipRow(pw As Integer, ph As Integer, sf As Single)
        Dim lfs As Single = Math.Max(7, 12.0F * sf)
        Dim fh As Integer = Math.Max(20, CInt(ph * 0.026))
        Dim lbX As Integer = CInt(pw * 0.0176)
        Dim cbX As Integer = CInt(pw * 0.111)
        Dim cbW As Integer = CInt(pw * 0.299)
        Dim rowY As Integer = CInt(ph * 0.849)

        _form.CivilStatuslbl.Location = New Point(lbX, rowY + CInt(ph * 0.003))
        _form.CivilStatuslbl.Font = New Font("Arial", lfs, FontStyle.Bold)
        _form.CivilStatuslbl.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

        _form.cbRelationshipType.Location = New Point(cbX, rowY)
        _form.cbRelationshipType.Size = New Size(cbW, fh)
        _form.cbRelationshipType.Font = New Font("Arial", lfs, FontStyle.Regular)
        _form.cbRelationshipType.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
    End Sub

    ' ============================================================================
    ' SECTION: Action Buttons — BtnSaveRelationship (left) + btnBack (right)
    '   Designer:
    '     BtnSaveRelationship (1223, 926, 199×44) → 71.9 % × 92.2 %
    '     btnBack             (1468, 926, 199×44) → 86.4 % × 92.2 %
    '   Button width 199/1700 = 11.7 %, height 44/1004 = 4.4 %
    ' ============================================================================
    Private Sub PositionActionButtons(pw As Integer, ph As Integer, sf As Single)
        Dim bw As Integer = CInt(pw * 0.117)
        Dim bh As Integer = Math.Max(28, CInt(ph * 0.044))
        Dim by As Integer = CInt(ph * 0.922)
        Dim fs As Single = Math.Max(7, 11.25F * sf)

        ' Clamp so buttons never fall below the form bottom
        If by + bh > ph - 2 Then by = ph - bh - 2

        ' Save Relationship (left-positioned, disabled by default)
        _form.BtnSaveRelationship.Location = New Point(CInt(pw * 0.719), by)
        _form.BtnSaveRelationship.Size = New Size(bw, bh)
        _form.BtnSaveRelationship.Font = New Font("Arial Narrow", fs, FontStyle.Bold)
        _form.BtnSaveRelationship.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        _form.BtnSaveRelationship.Cursor = Cursors.Hand

        ' Back
        _form.btnBack.Location = New Point(CInt(pw * 0.864), by)
        _form.btnBack.Size = New Size(bw, bh)
        _form.btnBack.Font = New Font("Arial Narrow", fs, FontStyle.Bold)
        _form.btnBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
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
