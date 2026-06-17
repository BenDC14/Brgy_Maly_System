''' <summary>
''' Universal Responsive Layout Manager for NewReportType_Form.
'''
''' Resolution-agnostic design principle
''' ─────────────────────────────────────
''' All controls are described in a shared ControlDescriptor array that stores
''' exact Designer coordinates.  On every layout pass, two axis scale factors
''' are derived purely from the form's own ClientSize:
'''
'''     scaleX = CurrentFormWidth  / ORIG_W   (470)
'''     scaleY = CurrentFormHeight / ORIG_H   (485)
'''
''' The layout engine iterates the descriptor array and multiplies each
''' control's (X, Y, W, H) by the matching axis factor.  Fonts use
''' Math.Min(scaleX, scaleY) for uniform readability.
'''
''' No screen resolution is ever hardcoded — the math adapts to 1366x768,
''' 1920x1080, 4K, or any other display automatically.
''' </summary>
Public Class NewReportTypeResponsiveManager

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Designer baseline (taken directly from InitializeComponent)
    ' ─────────────────────────────────────────────────────────────────────────────
    Private Const ORIG_W As Integer = 470
    Private Const ORIG_H As Integer = 485

    ''' <summary>
    ''' How much of the screen working-area width the form should occupy.
    ''' 0.28 = 28% on 1920-wide -> ~538 px; on 1366 -> ~382 px.
    ''' Height is derived from the original aspect ratio.
    ''' </summary>
    Private Const TARGET_FRACTION As Single = 0.28F

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Control descriptor — one record per designer control
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Captures everything the layout engine needs to position one control.
    ''' NOTE: The font-style field is named "Style" (not "FontStyle") to avoid
    '''       a VB.NET naming conflict with the System.Drawing.FontStyle enum type.
    ''' </summary>
    Private Class ControlDescriptor
        Public ReadOnly Ctrl As Control
        Public ReadOnly OrigX As Integer   ' designer Location.X
        Public ReadOnly OrigY As Integer   ' designer Location.Y
        Public ReadOnly OrigW As Integer   ' designer Size.Width  (0 = AutoSize)
        Public ReadOnly OrigH As Integer   ' designer Size.Height (0 = AutoSize)
        Public ReadOnly OrigFontSz As Single    ' designer Font size in points
        Public ReadOnly Family As String    ' "Arial" or "Arial Narrow"
        Public ReadOnly Style As FontStyle ' ← renamed from FontStyle to Style
        Public ReadOnly IsAutoSize As Boolean   ' True = only position, not resize

        Public Sub New(ctrl As Control,
                       origX As Integer,
                       origY As Integer,
                       origW As Integer,
                       origH As Integer,
                       origFontSz As Single,
                       family As String,
                       style As FontStyle,
                       isAutoSize As Boolean)
            Me.Ctrl = ctrl
            Me.OrigX = origX
            Me.OrigY = origY
            Me.OrigW = origW
            Me.OrigH = origH
            Me.OrigFontSz = origFontSz
            Me.Family = family
            Me.Style = style
            Me.IsAutoSize = isAutoSize
        End Sub
    End Class

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Instance state
    ' ─────────────────────────────────────────────────────────────────────────────

    Private ReadOnly _form As NewReportType_Form
    Private _descriptors As List(Of ControlDescriptor)
    Private _scaleX As Single = 1.0F
    Private _scaleY As Single = 1.0F

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Constructor
    ' ─────────────────────────────────────────────────────────────────────────────

    Public Sub New(form As NewReportType_Form)
        _form = form
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Public entry point
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Called once from Form_Load.
    ''' Builds the descriptor table, sizes the form to a fraction of the screen,
    ''' computes scale factors, positions all controls, then hooks Resize.
    ''' </summary>
    Public Sub Initialize()
        _form.AutoScaleMode = AutoScaleMode.None

        ' ── 1. Pre-declare FontStyle values as locals ──────────────────────────────
        ' This avoids any VB.NET ambiguity between the FontStyle enum type and the
        ' Style field name inside ControlDescriptor when building the descriptor list.
        Dim fsReg As FontStyle = FontStyle.Regular
        Dim fsBold As FontStyle = FontStyle.Bold
        Dim fsBoldItal As FontStyle = FontStyle.Bold Or FontStyle.Italic

        ' ── 2. Build the descriptor list — one Add per line, no line continuation ──
        ' Each call is a single logical line so VB.NET never loses continuation context.
        _descriptors = New List(Of ControlDescriptor)()

        ' ExitBtn — pinned top-right; handled specially in ApplyControlLayout
        _descriptors.Add(New ControlDescriptor(_form.ExitBtn, 436, 1, 31, 30, 0.0F, "Arial", fsReg, False))

        ' Title label
        _descriptors.Add(New ControlDescriptor(_form.lblAddReportTypes, 103, 29, 0, 0, 20.25F, "Arial", fsBold, True))

        ' Report Type section
        _descriptors.Add(New ControlDescriptor(_form.lblReportType, 26, 137, 0, 0, 14.25F, "Arial", fsBold, True))
        _descriptors.Add(New ControlDescriptor(_form.cbreporttype, 30, 162, 248, 30, 14.25F, "Arial", fsReg, False))
        _descriptors.Add(New ControlDescriptor(_form.txtReportType, 284, 162, 150, 29, 14.25F, "Arial", fsReg, False))
        _descriptors.Add(New ControlDescriptor(_form.btnAddNewReportType, 30, 212, 215, 47, 14.25F, "Arial Narrow", fsBoldItal, False))
        _descriptors.Add(New ControlDescriptor(_form.btnEditReportType, 251, 212, 207, 47, 14.25F, "Arial Narrow", fsBoldItal, False))

        ' Report Sub-Type section
        _descriptors.Add(New ControlDescriptor(_form.lblReportSubType, 26, 287, 0, 0, 14.25F, "Arial", fsBold, True))
        _descriptors.Add(New ControlDescriptor(_form.cbreportsubtype, 30, 312, 248, 30, 14.25F, "Arial", fsReg, False))
        _descriptors.Add(New ControlDescriptor(_form.txtReportSubType, 284, 312, 150, 29, 14.25F, "Arial", fsReg, False))
        _descriptors.Add(New ControlDescriptor(_form.btnAddNewReportSubType, 30, 357, 229, 47, 14.25F, "Arial Narrow", fsBoldItal, False))
        _descriptors.Add(New ControlDescriptor(_form.btnEditReportSubType, 265, 357, 193, 47, 14.25F, "Arial Narrow", fsBoldItal, False))

        ' ── 3. Calculate target form size from the screen's working area ──────────
        Dim screen As Screen = Screen.FromControl(_form)
        Dim workW As Integer = screen.WorkingArea.Width
        Dim workH As Integer = screen.WorkingArea.Height

        Dim targetW As Integer = CInt(workW * TARGET_FRACTION)
        Dim targetH As Integer = CInt(targetW * (CSng(ORIG_H) / CSng(ORIG_W)))

        targetW = Math.Max(ORIG_W, Math.Min(targetW, workW - 40))
        targetH = Math.Max(ORIG_H, Math.Min(targetH, workH - 40))

        ' ── 4. Apply form and FillPanel size ─────────────────────────────────────
        _form.ClientSize = New Size(targetW, targetH)
        _form.FillPanel.Size = New Size(targetW, targetH)
        _form.FillPanel.Location = New Point(0, 0)

        ' ── 5. Centre on working area ─────────────────────────────────────────────
        _form.StartPosition = FormStartPosition.Manual
        _form.Location = New Point(screen.WorkingArea.Left + (workW - targetW) \ 2,
                               screen.WorkingArea.Top + (workH - targetH) \ 2)

        ' ── 6. Derive scale factors (form-relative, not screen-relative) ──────────
        _scaleX = CSng(targetW) / CSng(ORIG_W)
        _scaleY = CSng(targetH) / CSng(ORIG_H)

        ' ── 7. Apply layout and hook Resize ───────────────────────────────────────
        ApplyControlLayout()
        AddHandler _form.Resize, AddressOf OnFormResize
    End Sub




    ''' <summary>
    ''' Re-runs layout whenever the form is resized.
    ''' Recomputes both scale factors from the new ClientSize.
    ''' </summary>
    Public Sub RecalculateLayout()
        Dim cw As Integer = _form.ClientSize.Width
        Dim ch As Integer = _form.ClientSize.Height

        If cw < 10 OrElse ch < 10 Then Exit Sub

        _scaleX = CSng(cw) / CSng(ORIG_W)
        _scaleY = CSng(ch) / CSng(ORIG_H)

        _form.FillPanel.Size = New Size(cw, ch)
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
    ' Core layout engine — iterates the descriptor array
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Applies position, size, and font to every control in the descriptor table.
    '''
    ''' Axis rules:
    '''   X, Width  -> multiplied by _scaleX
    '''   Y, Height -> multiplied by _scaleY
    '''   Font size -> multiplied by Math.Min(_scaleX, _scaleY)  (uniform scale)
    '''
    ''' Special case — ExitBtn:
    '''   Always pinned to the top-right corner regardless of form stretch.
    ''' </summary>
    Private Sub ApplyControlLayout()
        Dim sx As Single = _scaleX
        Dim sy As Single = _scaleY
        Dim sf As Single = Math.Min(sx, sy)   ' uniform font scale factor

        For Each d As ControlDescriptor In _descriptors

            ' ── Special: ExitBtn always pinned top-right ─────────────────────
            If d.Ctrl Is _form.ExitBtn Then
                Dim ew As Integer = CInt(d.OrigW * sx)
                Dim eh As Integer = CInt(d.OrigH * sy)
                d.Ctrl.Location = New Point(_form.ClientSize.Width - ew - 1, 0)
                d.Ctrl.Size = New Size(ew, eh)
                d.Ctrl.Cursor = Cursors.Hand
                Continue For
            End If

            ' ── Position (applied to every control) ───────────────────────────
            d.Ctrl.Location = New Point(CInt(d.OrigX * sx), CInt(d.OrigY * sy))

            ' ── Size (non-AutoSize controls only) ────────────────────────────
            If Not d.IsAutoSize AndAlso d.OrigW > 0 AndAlso d.OrigH > 0 Then
                d.Ctrl.Size = New Size(CInt(d.OrigW * sx), CInt(d.OrigH * sy))
            End If

            ' ── Font (controls with a defined font size) ──────────────────────
            If d.OrigFontSz > 0 Then
                ' Uses d.Style (renamed field) — no longer conflicts with enum type
                d.Ctrl.Font = New Font(d.Family, d.OrigFontSz * sf, d.Style)
            End If

            ' ── Re-enable AutoSize after font change so labels self-measure ───
            If d.IsAutoSize AndAlso TypeOf d.Ctrl Is Label Then
                DirectCast(d.Ctrl, Label).AutoSize = True
            End If

        Next
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Cleanup
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Removes the Resize handler to prevent memory leaks.
    ''' Called from the form's OnFormClosing override.
    ''' </summary>
    Public Sub Cleanup()
        RemoveHandler _form.Resize, AddressOf OnFormResize
    End Sub

End Class
