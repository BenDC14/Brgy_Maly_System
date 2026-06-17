''' <summary>
''' Universal Responsive Layout Manager for AddNewRelationshipType_Form.
'''
''' Resolution-agnostic design principle
''' ─────────────────────────────────────
''' This class stores the ORIGINAL control bounds exactly as authored in the
''' Designer file (ClientSize 470×245).  On every layout pass it derives two
''' independent scale factors:
'''
'''     scaleX = CurrentFormWidth  / OriginalFormWidth
'''     scaleY = CurrentFormHeight / OriginalFormHeight
'''
''' Every control's X, Y, Width, and Height are then multiplied by the
''' appropriate axis factor so the layout maps perfectly to ANY resolution
''' without hardcoding a specific target screen size.
'''
''' The form is also resized to fill a configurable percentage of the current
''' screen's working area (default 35 % of screen width) so it never appears
''' tiny on a 4K display or oversized on a 768-pixel-tall laptop.
''' </summary>
Public Class AddNewRelationshipTypeResponsiveManager

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Designer baseline — taken directly from InitializeComponent
    ' ─────────────────────────────────────────────────────────────────────────────

    ' Form / FillPanel canvas size (ClientSize in Designer)
    Private Const ORIG_W As Integer = 470
    Private Const ORIG_H As Integer = 245

    ' ── Control baselines: (X, Y, W, H) from the Designer ───────────────────────

    ' lblAddRelationshipType — Location(44, 23)   AutoSize=True (no scaling of size needed)
    Private Const LBL_TITLE_X As Integer = 44
    Private Const LBL_TITLE_Y As Integer = 23

    ' lblRelationship — Location(20, 79)           AutoSize=True
    Private Const LBL_REL_X As Integer = 20
    Private Const LBL_REL_Y As Integer = 79

    ' cbrelationship — Location(24, 104)  Size(248, 30)
    Private Const CB_X As Integer = 24
    Private Const CB_Y As Integer = 104
    Private Const CB_W As Integer = 248
    Private Const CB_H As Integer = 30

    ' txtRelationship — Location(278, 104)  Size(150, 29)
    Private Const TXT_X As Integer = 278
    Private Const TXT_Y As Integer = 104
    Private Const TXT_W As Integer = 150
    Private Const TXT_H As Integer = 29

    ' btnAddNewRelationshipType — Location(24, 168)  Size(202, 47)
    Private Const BTN_ADD_X As Integer = 24
    Private Const BTN_ADD_Y As Integer = 168
    Private Const BTN_ADD_W As Integer = 202
    Private Const BTN_ADD_H As Integer = 47

    ' btnEditRelationship — Location(245, 168)  Size(202, 47)
    Private Const BTN_EDIT_X As Integer = 245
    Private Const BTN_EDIT_Y As Integer = 168
    Private Const BTN_EDIT_W As Integer = 202
    Private Const BTN_EDIT_H As Integer = 47

    ' ExitBtn — Location(439, 0)  Size(31, 30)
    Private Const EXIT_X As Integer = 439
    Private Const EXIT_Y As Integer = 0
    Private Const EXIT_W As Integer = 31
    Private Const EXIT_H As Integer = 30

    ' ── Original font sizes from the Designer ────────────────────────────────────
    Private Const ORIG_FONT_TITLE As Single = 20.25F   ' lblAddRelationshipType
    Private Const ORIG_FONT_LABEL As Single = 14.25F   ' lblRelationship
    Private Const ORIG_FONT_INPUT As Single = 14.25F   ' cbrelationship / txtRelationship
    Private Const ORIG_FONT_BUTTON As Single = 14.25F   ' both action buttons

    ' ── Target form size as a fraction of the screen's working area ──────────────
    ' 0.30 = the form will be 30 % of the screen width.
    ' The height is derived from the original aspect ratio so the form never
    ' distorts even if the user switches monitors mid-session.
    Private Const TARGET_SCREEN_FRACTION As Single = 0.3F

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Instance state
    ' ─────────────────────────────────────────────────────────────────────────────

    Private ReadOnly _form As AddNewRelationshipType_Form

    ''' <summary>Scale factors computed at Initialize time.</summary>
    Private _scaleX As Single = 1.0F
    Private _scaleY As Single = 1.0F

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Constructor
    ' ─────────────────────────────────────────────────────────────────────────────

    Public Sub New(form As AddNewRelationshipType_Form)
        _form = form
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Public entry points
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Called once from Form_Load.  Computes the target form size from the
    ''' current screen's working area, resizes the form, derives axis scale
    ''' factors from the new size vs. the original designer size, then positions
    ''' every control proportionally.
    ''' </summary>
    Public Sub Initialize()
        ' Disable the WinForms auto-scaling so our manual math is authoritative
        _form.AutoScaleMode = AutoScaleMode.None

        ' ── 1. Determine target form size from the current screen ────────────
        Dim screen As Screen = Screen.FromControl(_form)
        Dim workW As Integer = screen.WorkingArea.Width
        Dim workH As Integer = screen.WorkingArea.Height

        ' Target width is a fraction of the screen width; height preserves ratio
        Dim targetW As Integer = CInt(workW * TARGET_SCREEN_FRACTION)
        Dim targetH As Integer = CInt(targetW * (CSng(ORIG_H) / CSng(ORIG_W)))

        ' Clamp: never go below the original designer size or above the screen
        targetW = Math.Max(ORIG_W, Math.Min(targetW, workW - 40))
        targetH = Math.Max(ORIG_H, Math.Min(targetH, workH - 40))

        ' ── 2. Apply the new form client size ────────────────────────────────
        _form.ClientSize = New Size(targetW, targetH)
        _form.FillPanel.Size = New Size(targetW, targetH)
        _form.FillPanel.Location = New Point(0, 0)

        ' ── 3. Centre on the working area ────────────────────────────────────
        _form.StartPosition = FormStartPosition.Manual
        _form.Location = New Point(
            screen.WorkingArea.Left + (workW - targetW) \ 2,
            screen.WorkingArea.Top + (workH - targetH) \ 2)

        ' ── 4. Derive resolution-agnostic scale factors ───────────────────────
        '     scaleX = new width  / original designer width
        '     scaleY = new height / original designer height
        '  These are computed from the FORM's own dimensions — not the screen's —
        '  so the math stays correct if the form is ever resized or used on a
        '  monitor with a different DPI/resolution than where it was authored.
        _scaleX = CSng(targetW) / CSng(ORIG_W)
        _scaleY = CSng(targetH) / CSng(ORIG_H)

        ' ── 5. Position every control using the computed scale factors ────────
        ApplyControlLayout()

        ' ── 6. Hook into Resize so layout stays correct if the dialog is resized
        AddHandler _form.Resize, AddressOf OnFormResize
    End Sub

    ''' <summary>
    ''' Re-runs layout whenever the form is resized (e.g. user drags the window).
    ''' Recomputes scale factors from the current ClientSize every time.
    ''' </summary>
    Public Sub RecalculateLayout()
        Dim currentW As Integer = _form.ClientSize.Width
        Dim currentH As Integer = _form.ClientSize.Height

        If currentW < 10 OrElse currentH < 10 Then Exit Sub

        _scaleX = CSng(currentW) / CSng(ORIG_W)
        _scaleY = CSng(currentH) / CSng(ORIG_H)

        ' Keep FillPanel covering the full client area
        _form.FillPanel.Size = New Size(currentW, currentH)
        _form.FillPanel.Location = New Point(0, 0)

        ApplyControlLayout()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Resize event handler
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub OnFormResize(sender As Object, e As EventArgs)
        RecalculateLayout()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Core layout engine
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Positions and sizes every control by multiplying its original Designer
    ''' coordinates by the current (_scaleX, _scaleY) factors.
    '''
    ''' X and Width  are multiplied by _scaleX (horizontal axis).
    ''' Y and Height are multiplied by _scaleY (vertical axis).
    ''' Fonts use the smaller of the two factors (uniform font scale) to prevent
    ''' text from becoming wider than its container.
    ''' </summary>
    Private Sub ApplyControlLayout()
        Dim sx As Single = _scaleX
        Dim sy As Single = _scaleY
        Dim sf As Single = Math.Min(sx, sy)   ' uniform font scale factor

        ' ── ExitBtn ────────────────────────────────────────────────────────────
        ' Pinned to the top-right corner: X scales with form width,
        ' Y stays at 0.
        _form.ExitBtn.Location = New Point(CInt(_form.ClientSize.Width) - CInt(EXIT_W * sx) - 1, EXIT_Y)
        _form.ExitBtn.Size = New Size(CInt(EXIT_W * sx), CInt(EXIT_H * sy))
        _form.ExitBtn.Cursor = Cursors.Hand

        ' ── Title label ────────────────────────────────────────────────────────
        _form.lblAddRelationshipType.Location = New Point(CInt(LBL_TITLE_X * sx),
                                                          CInt(LBL_TITLE_Y * sy))
        _form.lblAddRelationshipType.Font = New Font("Arial",
                                                          ORIG_FONT_TITLE * sf,
                                                          FontStyle.Bold)
        _form.lblAddRelationshipType.AutoSize = True

        ' ── Field label ────────────────────────────────────────────────────────
        _form.lblRelationship.Location = New Point(CInt(LBL_REL_X * sx),
                                                    CInt(LBL_REL_Y * sy))
        _form.lblRelationship.Font = New Font("Arial",
                                                   ORIG_FONT_LABEL * sf,
                                                   FontStyle.Bold)
        _form.lblRelationship.AutoSize = True

        ' ── ComboBox ───────────────────────────────────────────────────────────
        _form.cbrelationship.Location = New Point(CInt(CB_X * sx), CInt(CB_Y * sy))
        _form.cbrelationship.Size = New Size(CInt(CB_W * sx), CInt(CB_H * sy))
        _form.cbrelationship.Font = New Font("Arial", ORIG_FONT_INPUT * sf, FontStyle.Regular)

        ' ── Editing side-TextBox ───────────────────────────────────────────────
        _form.txtRelationship.Location = New Point(CInt(TXT_X * sx), CInt(TXT_Y * sy))
        _form.txtRelationship.Size = New Size(CInt(TXT_W * sx), CInt(TXT_H * sy))
        _form.txtRelationship.Font = New Font("Arial", ORIG_FONT_INPUT * sf, FontStyle.Regular)

        ' ── Add New Relationship button ─────────────────────────────────────────
        _form.btnAddNewRelationshipType.Location = New Point(CInt(BTN_ADD_X * sx),
                                                              CInt(BTN_ADD_Y * sy))
        _form.btnAddNewRelationshipType.Size = New Size(CInt(BTN_ADD_W * sx),
                                                             CInt(BTN_ADD_H * sy))
        _form.btnAddNewRelationshipType.Font = New Font("Arial Narrow",
                                                             ORIG_FONT_BUTTON * sf,
                                                             FontStyle.Bold Or FontStyle.Italic)

        ' ── Edit Relationship button ────────────────────────────────────────────
        _form.btnEditRelationship.Location = New Point(CInt(BTN_EDIT_X * sx),
                                                        CInt(BTN_EDIT_Y * sy))
        _form.btnEditRelationship.Size = New Size(CInt(BTN_EDIT_W * sx),
                                                       CInt(BTN_EDIT_H * sy))
        _form.btnEditRelationship.Font = New Font("Arial Narrow",
                                                       ORIG_FONT_BUTTON * sf,
                                                       FontStyle.Bold Or FontStyle.Italic)
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Cleanup
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Removes event handlers to prevent memory leaks.
    ''' Called from the form's OnFormClosing override.
    ''' </summary>
    Public Sub Cleanup()
        RemoveHandler _form.Resize, AddressOf OnFormResize
    End Sub

End Class
