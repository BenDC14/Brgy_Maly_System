Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive Layout Manager for Dashboard2_Form.
''' All coordinates are derived as proportional percentages of the original
''' 1700×1004 designer canvas so the org-chart scales cleanly from
''' 1366×768 up to 1080p+ without any hardcoded pixel offsets.
''' </summary>
Public Class Dashboard2ResponsiveManager

    ' =========================================================================
    '  DESIGNER CANVAS CONSTANTS
    '  Source: Dashboard2_Form.Designer.vb — ClientSize (1700, 1004)
    '  Every coordinate below was read directly from the Designer file and
    '  converted to a proportional percentage of these two constants.
    ' =========================================================================
    Private Const ORIG_W As Integer = 1700
    Private Const ORIG_H As Integer = 1004

    ' =========================================================================
    '  DESIGNER PANEL BOUNDS  (x, y, w, h) — read from Designer.vb
    '  Stored as Single arrays so the scale math stays in one place.
    ' =========================================================================
    ' {origX, origY, origW, origH}
    Private Shared ReadOnly D_ChairmanPnl As Integer() = {743, 68, 241, 125}

    Private Shared ReadOnly D_KagawadPnls()() As Integer = {
    New Integer() {220, 199, 241, 125},
    New Integer() {480, 199, 241, 125},
    New Integer() {743, 199, 241, 125},
    New Integer() {1002, 199, 241, 125},
    New Integer() {1262, 199, 241, 125},
    New Integer() {118, 334, 241, 125},
    New Integer() {376, 334, 241, 125},
    New Integer() {633, 334, 241, 125},
    New Integer() {892, 334, 241, 125},
    New Integer() {1149, 334, 241, 125},
    New Integer() {1405, 334, 214, 125}
}

    Private Shared ReadOnly D_SkChairmanPnl As Integer() = {743, 530, 241, 125}

    Private Shared ReadOnly D_SkKagawadPnls()() As Integer = {
        New Integer() {480, 699, 241, 125},   ' SkKagawad1Pnl
        New Integer() {743, 699, 241, 125},   ' SkKagawad2Pnl
        New Integer() {1005, 699, 241, 125},   ' SkKagawad3Pnl
        New Integer() {376, 835, 241, 125},   ' SkKagawad4Pnl
        New Integer() {636, 835, 241, 125},   ' SkKagawad5Pnl
        New Integer() {896, 835, 241, 125},   ' SkKagawad6Pnl
        New Integer() {1158, 835, 241, 125}    ' SkKagawad7Pnl
    }

    ' TitleLbl original anchor
    Private Const D_TitleX As Integer = 698
    Private Const D_TitleY As Integer = 18

    ' Nav arrow original bounds
    Private Shared ReadOnly D_LeftBtn As Integer() = {12, 484, 100, 76}
    Private Shared ReadOnly D_NextBtn As Integer() = {1598, 484, 100, 76}

    ' =========================================================================
    '  FORM REFERENCE AND TIMER
    ' =========================================================================
    Private ReadOnly _form As Dashboard2_Form
    Private ReadOnly _resizeTimer As New System.Windows.Forms.Timer()
    Private _layoutReady As Boolean = False

    Public Sub New(form As Dashboard2_Form)
        _form = form
    End Sub

    ' =========================================================================
    '  PUBLIC API
    ' =========================================================================

    ''' <summary>
    ''' Call once from Form_Load after InitializeComponent() and data binding.
    ''' Applies all visual styling and wires resize/display-change handlers.
    ''' </summary>
    Public Sub Initialize()
        ' FillPanel must cover the entire host area
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                                  AnchorStyles.Left Or AnchorStyles.Right

        ' Reset any docks the Designer may have applied to the card panels
        ResetCardDocks()

        ' Visual styling — applied once; rounded corners re-fire on Resize
        ApplyGradient(_form.FillPanel, "#EDFFE9", "#FFFFFF")
        ApplyCardStyling()

        ' Debounce timer — recalculates layout 250 ms after the last resize
        _resizeTimer.Interval = 250
        AddHandler _resizeTimer.Tick, AddressOf OnResizeTimerTick

        AddHandler _form.Resize, AddressOf OnFormResize
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged

        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        _layoutReady = True
    End Sub

    ''' <summary>
    ''' Recalculates the entire layout immediately.
    ''' Can be called externally (e.g. after data is reloaded).
    ''' </summary>
    Public Sub RefreshLayout()
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Must be called from OnFormClosing to prevent memory/handler leaks.
    ''' </summary>
    Public Sub Cleanup()
        _resizeTimer.Stop()
        _resizeTimer.Dispose()
        RemoveHandler _form.Resize, AddressOf OnFormResize
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged
    End Sub

    ' =========================================================================
    '  RESIZE EVENTS
    ' =========================================================================
    Private Sub OnDisplaySettingsChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    Private Sub OnFormResize(sender As Object, e As EventArgs)
        If Not _layoutReady Then Exit Sub
        _resizeTimer.Stop()
        _resizeTimer.Start()
    End Sub

    Private Sub OnResizeTimerTick(sender As Object, e As EventArgs)
        _resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ' =========================================================================
    '  CORE LAYOUT ENGINE
    '  Derives a single uniform scale factor from the ratio of the current
    '  client size to the original 1700×1004 designer canvas.
    '  The minimum of the two axis scales is used so content is never cropped.
    ' =========================================================================
    Private Sub CalculateAndApplyLayout()
        Dim cw As Integer = _form.ClientSize.Width
        Dim ch As Integer = _form.ClientSize.Height
        If cw < 200 Or ch < 150 Then Exit Sub

        ' Sync FillPanel to host area
        _form.FillPanel.Size = New Size(cw, ch)
        _form.FillPanel.Location = New Point(0, 0)

        ' Uniform scale factor — constrained by the tighter axis
        Dim sx As Single = CSng(cw) / ORIG_W
        Dim sy As Single = CSng(ch) / ORIG_H
        Dim sf As Single = Math.Min(sx, sy)

        ' ------------------------------------------------------------------
        '  TITLE LABEL
        '  Font scales with sf; minimum 8 pt to remain readable.
        ' ------------------------------------------------------------------
        _form.TitleLbl.Font = New Font("Arial",
                                            Math.Max(27.0F * sf, 8.0F),
                                            FontStyle.Bold)
        _form.TitleLbl.AutoSize = True
        ' Re-centre after AutoSize recalculates width
        _form.TitleLbl.Location = New Point(
            CInt((cw - _form.TitleLbl.Width) / 2),
            CInt(D_TitleY * sf))

        ' ------------------------------------------------------------------
        '  EXECUTIVE PANEL — Barangay Chairman
        ' ------------------------------------------------------------------
        ScaleAndPositionPanel(_form.ChairmanPnl, D_ChairmanPnl, sf, cw)

        ' ------------------------------------------------------------------
        '  LEGISLATIVE PANELS — Kagawad 1-11
        ' ------------------------------------------------------------------
        Dim kPanels As Panel() = {
            _form.Kagawad1Pnl, _form.Kagawad2Pnl, _form.Kagawad3Pnl,
            _form.Kagawad4Pnl, _form.Kagawad5Pnl, _form.Kagawad6Pnl,
            _form.Kagawad7Pnl, _form.Kagawad8Pnl, _form.Kagawad9Pnl,
            _form.Kagawad10Pnl, _form.Kagawad11Pnl
        }
        For i As Integer = 0 To 10
            ScaleAndPositionPanel(kPanels(i), D_KagawadPnls(i), sf, cw)
        Next

        ' ------------------------------------------------------------------
        '  SK CHAIRMAN PANEL
        ' ------------------------------------------------------------------
        ScaleAndPositionPanel(_form.SkChairmanPnl, D_SkChairmanPnl, sf, cw)

        ' ------------------------------------------------------------------
        '  SK KAGAWAD PANELS 1-7
        ' ------------------------------------------------------------------
        Dim skPanels As Panel() = {
            _form.SkKagawad1Pnl, _form.SkKagawad2Pnl, _form.SkKagawad3Pnl,
            _form.SkKagawad4Pnl, _form.SkKagawad5Pnl, _form.SkKagawad6Pnl,
            _form.SkKagawad7Pnl
        }
        For i As Integer = 0 To 6
            ScaleAndPositionPanel(skPanels(i), D_SkKagawadPnls(i), sf, cw)
        Next

        ' ------------------------------------------------------------------
        '  NAV ARROWS — left and right
        ' ------------------------------------------------------------------
        ScaleControl(_form.LeftButtonPB, D_LeftBtn, sf)
        _form.LeftButtonPB.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.LeftButtonPB.Cursor = Cursors.Hand

        ScaleControl(_form.NextBtn, D_NextBtn, sf)
        _form.NextBtn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.NextBtn.Cursor = Cursors.Hand
    End Sub

    ' =========================================================================
    '  PANEL SCALING HELPER
    '  Scales the panel itself, then proportionally repositions every child
    '  control (PictureBox + two Labels) within it.
    ' =========================================================================
    Private Sub ScaleAndPositionPanel(pnl As Panel,
                                       orig As Integer(),
                                       sf As Single,
                                       hostWidth As Integer)
        Dim x As Integer = CInt(orig(0) * sf)
        Dim y As Integer = CInt(orig(1) * sf)
        Dim w As Integer = CInt(orig(2) * sf)
        Dim h As Integer = CInt(orig(3) * sf)

        pnl.Location = New Point(x, y)
        pnl.Size = New Size(w, h)
        pnl.Dock = DockStyle.None
        pnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' Child control proportions (relative to card size)
        Dim picW As Integer = CInt(w * 0.42)
        Dim picH As Integer = CInt(h * 0.58)
        Dim picTop As Integer = CInt(h * 0.03)
        Dim picLeft As Integer = CInt((w - picW) / 2)

        Dim nameTop As Integer = CInt(h * 0.64)
        Dim posTop As Integer = CInt(h * 0.81)

        ' Label font — scaled, floored at 6 pt
        Dim fontSize As Single = Math.Max(9.75F * sf, 6.0F)
        Dim labelFont As Font = New Font("Arial", fontSize, FontStyle.Bold)

        For Each ctrl As Control In pnl.Controls
            ctrl.MaximumSize = New Size(w - 4, 0)  ' prevent overflow past card edge

            If TypeOf ctrl Is PictureBox Then
                ctrl.Size = New Size(picW, picH)
                ctrl.Location = New Point(picLeft, picTop)
                CType(ctrl, PictureBox).SizeMode = PictureBoxSizeMode.StretchImage

            ElseIf TypeOf ctrl Is Label Then
                ctrl.Font = labelFont
                ctrl.AutoSize = True

                ' Re-centre horizontally after AutoSize reflows the label width
                If ctrl.Name.IndexOf("Name", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    ctrl.Location = New Point(CInt((w - ctrl.Width) / 2), nameTop)
                Else
                    ctrl.Location = New Point(CInt((w - ctrl.Width) / 2), posTop)
                End If
            End If
        Next
    End Sub

    ''' <summary>Scales a non-panel control using the same sf and orig bounds.</summary>
    Private Sub ScaleControl(ctrl As Control, orig As Integer(), sf As Single)
        ctrl.Location = New Point(CInt(orig(0) * sf), CInt(orig(1) * sf))
        ctrl.Size = New Size(CInt(orig(2) * sf), CInt(orig(3) * sf))
    End Sub

    ' =========================================================================
    '  VISUAL STYLING
    ' =========================================================================

    Private Sub ApplyCardStyling()
        RoundPanel(_form.ChairmanPnl, 20)
        ApplyDropShadow(_form.ChairmanPnl)

        For i As Integer = 1 To 11
            Dim pnl As Panel = CType(_form.FillPanel.Controls("Kagawad" & i & "Pnl"), Panel)
            RoundPanel(pnl, 20)
            ApplyDropShadow(pnl)
        Next

        RoundPanel(_form.SkChairmanPnl, 20)
        ApplyDropShadow(_form.SkChairmanPnl)

        For i As Integer = 1 To 7
            Dim pnl As Panel = CType(_form.FillPanel.Controls("SkKagawad" & i & "Pnl"), Panel)
            RoundPanel(pnl, 20)
            ApplyDropShadow(pnl)
        Next
    End Sub

    Friend Sub ApplyGradient(pnl As Control,
                              startColorHex As String,
                              endColorHex As String)
        Dim sc = ColorTranslator.FromHtml(startColorHex)
        Dim ec = ColorTranslator.FromHtml(endColorHex)
        Dim brush As New LinearGradientBrush(
            New Point(0, 0), New Point(pnl.Width, 0), sc, ec)
        Dim local = pnl
        AddHandler local.Paint, Sub(s, ev)
                                    ev.Graphics.FillRectangle(brush, local.ClientRectangle)
                                End Sub
    End Sub

    Private Sub RoundPanel(pnl As Control, radius As Integer)
        Dim buildPath As Func(Of Control, GraphicsPath) =
            Function(c As Control)
                Dim p As New GraphicsPath()
                p.AddArc(0, 0, radius, radius, 180, 90)
                p.AddArc(c.Width - radius, 0, radius, radius, 270, 90)
                p.AddArc(c.Width - radius, c.Height - radius, radius, radius, 0, 90)
                p.AddArc(0, c.Height - radius, radius, radius, 90, 90)
                p.CloseFigure()
                Return p
            End Function

        pnl.Region = New Region(buildPath(pnl))
        Dim local = pnl
        AddHandler pnl.Resize, Sub(s, ev)
                                   local.Region = New Region(buildPath(local))
                               End Sub
    End Sub

    Private Sub ApplyDropShadow(card As Panel)
        card.BackColor = Color.White
        card.Padding = New Padding(0, 0, 3, 3)
        Dim local = card
        AddHandler local.Paint, Sub(s, ev)
                                    Using pen As New Pen(Color.FromArgb(100, 150, 150, 150), 2)
                                        ev.Graphics.DrawLine(pen,
                                            local.Width - 2, 3,
                                            local.Width - 2, local.Height - 3)
                                        ev.Graphics.DrawLine(pen,
                                            3, local.Height - 2,
                                            local.Width - 3, local.Height - 2)
                                    End Using
                                End Sub
    End Sub

    ' =========================================================================
    '  SETUP HELPERS
    ' =========================================================================

    Private Sub ResetCardDocks()
        _form.ChairmanPnl.Dock = DockStyle.None
        For i As Integer = 1 To 11
            CType(_form.FillPanel.Controls("Kagawad" & i & "Pnl"), Panel).Dock = DockStyle.None
        Next
        _form.SkChairmanPnl.Dock = DockStyle.None
        For i As Integer = 1 To 7
            CType(_form.FillPanel.Controls("SkKagawad" & i & "Pnl"), Panel).Dock = DockStyle.None
        Next
    End Sub

End Class
