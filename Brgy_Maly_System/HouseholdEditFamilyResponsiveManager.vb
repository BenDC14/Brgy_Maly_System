' ================================================================================
' FILE: HouseholdEditFamilyResponsiveManager.vb
' LAYER: Universal Responsive Scaling Layer
' PURPOSE: Translates the Designer's fixed 1700×1004 pixel layout into
'          percentage-based coordinates so every control scales smoothly
'          from 1366×768 upward to any higher resolution.
'
' Layout zones (Y% of total height):
'   Title              0 – 30 %
'   Members DGV       30 – 72 %
'   Add Members       72 – 92 %
'   Action buttons    92 – 100 %
' ================================================================================
Imports Microsoft.Win32

''' <summary>
''' Responsive UI manager for HouseholdEditFamily_Form.
''' Recalculates all control positions and font sizes whenever the host window
''' or system display settings change.
''' </summary>
Public Class HouseholdEditFamilyResponsiveManager

    ' ── Designer baseline ────────────────────────────────────────────────────────
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' ── Floor dimensions (1366×768 viewport minimum) ─────────────────────────────
    Private Const MIN_WIDTH As Integer = 640
    Private Const MIN_HEIGHT As Integer = 400

    ' ── Form reference ───────────────────────────────────────────────────────────
    Private ReadOnly _form As HouseholdEditFamily_Form

    ' ── Debounce timer ───────────────────────────────────────────────────────────
    Private ReadOnly _resizeTimer As New System.Windows.Forms.Timer With {.Interval = 280}
    Private _layoutDone As Boolean = False

    ' ============================================================================
    ' CONSTRUCTOR
    ' ============================================================================
    Public Sub New(form As HouseholdEditFamily_Form)
        _form = form
    End Sub

    ' ============================================================================
    ' INITIALIZE
    ' ============================================================================
    Public Sub Initialize()
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Location = New Point(0, 0)
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)

        AddHandler _resizeTimer.Tick, AddressOf ResizeTimer_Tick
        AddHandler _form.Resize, AddressOf Form_Resize
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplay_Changed

        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        _layoutDone = True
    End Sub

    ' ============================================================================
    ' EVENT HANDLERS
    ' ============================================================================

    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not _layoutDone Then Return
        _resizeTimer.Stop()
        _resizeTimer.Start()
    End Sub

    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        _resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    Private Sub SystemDisplay_Changed(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    ' ============================================================================
    ' MASTER LAYOUT ORCHESTRATOR
    ' ============================================================================

    ''' <summary>
    ''' Recalculates all control positions relative to the form's current client
    ''' area.  Called on load, resize, and display-setting changes.
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        Dim w As Integer = Math.Max(_form.ClientSize.Width, MIN_WIDTH)
        Dim h As Integer = Math.Max(_form.ClientSize.Height, MIN_HEIGHT)
        If w < 100 OrElse h < 100 Then Return

        Dim wScale As Single = CSng(w) / ORIGINAL_WIDTH
        Dim hScale As Single = CSng(h) / ORIGINAL_HEIGHT
        Dim scale As Single = Math.Min(wScale, hScale)

        _form.FillPanel.Size = New Size(w, h)
        _form.FillPanel.Location = New Point(0, 0)

        PositionTitleSection(w, h, scale)
        PositionFamilyInfoSection(w, h, scale)
        PositionFirstDivider(w, h)
        PositionMembersSection(w, h, scale)
        PositionSecondDivider(w, h)
        PositionAddMembersSection(w, h, scale)
        PositionActionButtons(w, h, scale)
    End Sub

    ' ============================================================================
    ' SECTION: Title
    '   EditFamilylbl @ Designer (30, 30) → 1.76 % × 2.99 %
    ' ============================================================================
    Private Sub PositionTitleSection(w As Integer, h As Integer, scale As Single)
        _form.EditFamilylbl.Location = New Point(CInt(w * 0.0176), CInt(h * 0.0299))
        _form.EditFamilylbl.Font = New Font("Arial", Math.Max(9, 20.25F * scale), FontStyle.Bold)
        _form.EditFamilylbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ' ============================================================================
    ' SECTION: Family Info (IDs, names, household, head, total members)
    '   Row 1 (y≈11.8 %): FamilyID @ 23.8 %,  Household @ 54.9 %
    '   Row 2 (y≈19.0 %): FamilyName @ 9.2 %, FamilyHead @ 38.3 %, TotalMembers @ 67.5 %
    '   Field width: 384 px → 22.6 %
    ' ============================================================================
    Private Sub PositionFamilyInfoSection(w As Integer, h As Integer, scale As Single)
        Dim lbFont As Single = Math.Max(7, 12.0F * scale)
        Dim txFont As Single = Math.Max(7, 12.0F * scale)
        Dim fldH As Integer = CInt(h * 0.026)
        Dim fldW As Integer = CInt(w * 0.226)

        ' Row 1 — labels
        Dim lbY1 As Integer = CInt(h * 0.118)
        SetLabel(_form.FamilyIDlbl, CInt(w * 0.238), lbY1, lbFont)
        SetLabel(_form.Householdlbl, CInt(w * 0.549), lbY1, lbFont)

        ' Row 1 — textboxes
        Dim txY1 As Integer = CInt(h * 0.141)
        SetTextBox(_form.txtFamilyID, CInt(w * 0.238), txY1, fldW, fldH, txFont)
        SetTextBox(_form.txtHousehold, CInt(w * 0.549), txY1, fldW, fldH, txFont)

        ' Row 2 — labels
        Dim lbY2 As Integer = CInt(h * 0.19)
        SetLabel(_form.FamilyNamelbl, CInt(w * 0.092), lbY2, lbFont)
        SetLabel(_form.FamilyHeadlbl, CInt(w * 0.383), lbY2, lbFont)
        SetLabel(_form.TotalFamilyMemberslbl, CInt(w * 0.675), lbY2, lbFont)

        ' Row 2 — textboxes
        Dim txY2 As Integer = CInt(h * 0.213)
        SetTextBox(_form.txtFamilyName, CInt(w * 0.092), txY2, fldW, fldH, txFont)
        SetTextBox(_form.txtFamilyHead, CInt(w * 0.383), txY2, fldW, fldH, txFont)
        SetTextBox(_form.txtTotalFamilyMembers, CInt(w * 0.675), txY2, fldW, fldH, txFont)
    End Sub

    ' ============================================================================
    ' SECTION: First divider — LinePnl @ y=302 → 30.1 %
    ' ============================================================================
    Private Sub PositionFirstDivider(w As Integer, h As Integer)
        _form.LinePnl.Location = New Point(0, CInt(h * 0.301))
        _form.LinePnl.Size = New Size(w, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ' ============================================================================
    ' SECTION: Members — title + DataGridView
    '   Memberslbl        @ Designer (30, 329)  → 1.76 % × 32.8 %
    '   FamilyMembersDGV  @ Designer (36, 374)  → 2.1  % × 37.3 %
    '   DGV width: 1633 px → 96.1 %  |  height: 326 px → 32.5 %
    ' ============================================================================
    Private Sub PositionMembersSection(w As Integer, h As Integer, scale As Single)
        Dim lm As Integer = CInt(w * 0.021)

        _form.Memberslbl.Location = New Point(lm, CInt(h * 0.328))
        _form.Memberslbl.Font = New Font("Arial", Math.Max(9, 20.25F * scale), FontStyle.Bold)
        _form.Memberslbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        _form.FamilyMembersDGV.Location = New Point(lm, CInt(h * 0.373))
        _form.FamilyMembersDGV.Size = New Size(CInt(w * 0.961), CInt(h * 0.325))
        _form.FamilyMembersDGV.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        _form.FamilyMembersDGV.ColumnHeadersHeight = Math.Max(24, CInt(35 * scale))
        _form.FamilyMembersDGV.RowTemplate.Height = Math.Max(20, CInt(32 * scale))
        _form.FamilyMembersDGV.DefaultCellStyle.Font =
            New Font("Arial", Math.Max(7, 10.0F * scale), FontStyle.Regular)
        _form.FamilyMembersDGV.ColumnHeadersDefaultCellStyle.Font =
            New Font("Arial", Math.Max(7, 11.0F * scale), FontStyle.Bold)
    End Sub

    ' ============================================================================
    ' SECTION: Second divider — LinePnl2 @ y=725 → 72.2 %
    ' ============================================================================
    Private Sub PositionSecondDivider(w As Integer, h As Integer)
        _form.LinePnl2.Location = New Point(0, CInt(h * 0.722))
        _form.LinePnl2.Size = New Size(w, 2)
        _form.LinePnl2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ' ============================================================================
    ' SECTION: Add New Members — title + two combobox pairs
    '   AddNewMemberslbl    @ (32,  743) → 1.9  % × 74.0 %
    '   SelectResidentslbl  @ (156, 787) → 9.2  % × 78.4 %
    '   cbResidents         @ (160, 809) → 9.4  % × 80.6 %  w=24.1 %
    '   RelationshipTypelbl @ (1147,787) → 67.5 % × 78.4 %
    '   cbRelationships     @ (1151,809) → 67.7 % × 80.6 %  w=24.1 %
    ' ============================================================================
    Private Sub PositionAddMembersSection(w As Integer, h As Integer, scale As Single)
        Dim lbFont As Single = Math.Max(7, 12.0F * scale)
        Dim fieldFont As Single = Math.Max(7, 14.25F * scale)
        Dim cbH As Integer = Math.Max(20, CInt(h * 0.032))
        Dim cbW As Integer = CInt(w * 0.241)
        Dim cbX1 As Integer = CInt(w * 0.094)
        Dim cbX2 As Integer = CInt(w * 0.677)

        _form.AddNewMemberslbl.Location = New Point(CInt(w * 0.019), CInt(h * 0.743))
        _form.AddNewMemberslbl.Font = New Font("Arial", Math.Max(8, 14.25F * scale), FontStyle.Bold)

        SetLabel(_form.SelectResidentslbl, cbX1, CInt(h * 0.785), lbFont)
        SetComboBox(_form.cbResidents, cbX1, CInt(h * 0.809), cbW, cbH, fieldFont)
        _form.cbResidents.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

        SetLabel(_form.RelationshipTypelbl, cbX2, CInt(h * 0.785), lbFont)
        SetComboBox(_form.cbRelationships, cbX2, CInt(h * 0.809), cbW, cbH, fieldFont)
        _form.cbRelationships.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
    End Sub

    ' ============================================================================
    ' SECTION: Action buttons  (four along the bottom row)
    '   btnAddNewFamilyMember @ (160,  921) → 9.4  % × 91.7 %  w=11.7 %
    '   btnSaveChanges        @ (582,  921) → 34.2 % × 91.7 %
    '   btnAddNewResident     @ (980,  921) → 57.6 % × 91.7 %
    '   btnBack               @ (1362, 921) → 80.1 % × 91.7 %
    '   Button height: 44 px → 4.4 %
    ' ============================================================================
    Private Sub PositionActionButtons(w As Integer, h As Integer, scale As Single)
        Dim btnW As Integer = CInt(w * 0.117)
        Dim btnH As Integer = Math.Max(28, CInt(h * 0.044))
        Dim btnY As Integer = CInt(h * 0.917)
        Dim font As Single = Math.Max(7, 11.25F * scale)

        SetButton(_form.btnAddNewFamilyMember, CInt(w * 0.094), btnY, btnW, btnH, font,
                  AnchorStyles.Bottom Or AnchorStyles.Left)
        SetButton(_form.btnSaveChanges, CInt(w * 0.342), btnY, btnW, btnH, font,
                  AnchorStyles.Bottom Or AnchorStyles.Left)
        SetButton(_form.btnAddNewResident, CInt(w * 0.576), btnY, btnW, btnH, font,
                  AnchorStyles.Bottom Or AnchorStyles.Left)
        SetButton(_form.btnBack, CInt(w * 0.801), btnY, btnW, btnH, font,
                  AnchorStyles.Bottom Or AnchorStyles.Right)
    End Sub

    ' ============================================================================
    ' PRIVATE MICRO-HELPERS
    ' ============================================================================

    Private Shared Sub SetLabel(lbl As Label, x As Integer, y As Integer, fontSize As Single)
        lbl.Location = New Point(x, y)
        lbl.Font = New Font("Arial", fontSize, FontStyle.Bold)
    End Sub

    Private Shared Sub SetTextBox(txt As TextBox, x As Integer, y As Integer,
                                  width As Integer, height As Integer, fontSize As Single)
        txt.Location = New Point(x, y)
        txt.Size = New Size(width, height)
        txt.Font = New Font("Arial", fontSize, FontStyle.Regular)
    End Sub

    Private Shared Sub SetComboBox(cb As ComboBox, x As Integer, y As Integer,
                                   width As Integer, height As Integer, fontSize As Single)
        cb.Location = New Point(x, y)
        cb.Size = New Size(width, height)
        cb.Font = New Font("Arial", fontSize, FontStyle.Regular)
    End Sub

    Private Shared Sub SetButton(btn As Button, x As Integer, y As Integer,
                                 width As Integer, height As Integer,
                                 fontSize As Single, anchor As AnchorStyles)
        btn.Location = New Point(x, y)
        btn.Size = New Size(width, height)
        btn.Font = New Font("Arial Narrow", fontSize, FontStyle.Bold)
        btn.Anchor = anchor
        btn.Cursor = Cursors.Hand
    End Sub

    ' ============================================================================
    ' CLEANUP
    ' ============================================================================
    Public Sub Cleanup()
        _resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplay_Changed
    End Sub

End Class
