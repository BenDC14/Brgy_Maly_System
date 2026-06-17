Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive Layout Manager for Dashboard3_Form.
''' Derives every coordinate and font size as a proportional percentage of the
''' original 1700×1004 designer canvas so the profile card scales cleanly
''' from 1366×768 laptop displays up to 1080p+ monitors.
'''
''' A single uniform scale factor (minimum of the x and y axis ratios) is used
''' so that no label ever overflows its panel or is clipped horizontally.
''' Text labels are further constrained by MaximumSize so long Mission/Vision
''' paragraphs wrap naturally rather than running off-screen.
''' </summary>
Public Class Dashboard3ResponsiveManager

    ' =========================================================================
    '  DESIGNER CANVAS — read from Dashboard3_Form.Designer.vb
    '  ClientSize: 1700 × 1004
    ' =========================================================================
    Private Const ORIG_W As Integer = 1700
    Private Const ORIG_H As Integer = 1004

    ' Original control bounds {x, y, w, h} — taken directly from Designer.vb
    ' TitleLbl           Location(698, 18)  Font 27 Bold
    Private Const D_TitleX As Integer = 698
    Private Const D_TitleY As Integer = 18
    Private Const D_TitleFont As Single = 27.0F

    ' BrgyLogoPic        Location(772, 89)  Size(230, 218)
    Private Shared ReadOnly D_Logo As Integer() = {772, 89, 230, 218}

    ' BrgyMalyNameLbl    Location(785, 319) Font 20.25 Bold
    Private Const D_NameX As Integer = 785
    Private Const D_NameY As Integer = 319
    Private Const D_NameFont As Single = 20.25F

    ' TitleMissionLbl    Location(130, 397) Font 26.25 Bold
    Private Const D_MsnTitleX As Integer = 130
    Private Const D_MsnTitleY As Integer = 397
    Private Const D_MsnTitleFont As Single = 26.25F

    ' MissionInfoLbl     Location(201, 450) Font 24 Bold+Italic  MaxW ≈ 80% panel
    Private Const D_MsnInfoX As Integer = 201
    Private Const D_MsnInfoY As Integer = 450
    Private Const D_MsnInfoFont As Single = 24.0F

    ' VisionLbl          Location(130, 690) Font 26.25 Bold
    Private Const D_VsnTitleX As Integer = 130
    Private Const D_VsnTitleY As Integer = 690
    Private Const D_VsnTitleFont As Single = 26.25F

    ' VisionInfoLbl      Location(201, 749) Font 24 Bold+Italic  MaxW ≈ 80% panel
    Private Const D_VsnInfoX As Integer = 201
    Private Const D_VsnInfoY As Integer = 749
    Private Const D_VsnInfoFont As Single = 24.0F

    ' LeftButtonPB       Location(12, 431)  Size(100, 76)
    Private Shared ReadOnly D_LeftBtn As Integer() = {12, 431, 100, 76}

    ' =========================================================================
    '  MINIMUM READABLE FONT FLOORS
    ' =========================================================================
    Private Const MIN_TITLE_FONT As Single = 9.0F
    Private Const MIN_NAME_FONT As Single = 8.0F
    Private Const MIN_SECTION_FONT As Single = 8.0F
    Private Const MIN_CONTENT_FONT As Single = 7.5F

    ' =========================================================================
    '  STATE
    ' =========================================================================
    Private ReadOnly _form As Dashboard3_Form
    Private ReadOnly _resizeTimer As New System.Windows.Forms.Timer()
    Private _layoutReady As Boolean = False

    Public Sub New(form As Dashboard3_Form)
        _form = form
    End Sub

    ' =========================================================================
    '  PUBLIC API
    ' =========================================================================

    ''' <summary>
    ''' Call once from Form_Load after InitializeComponent() and data binding.
    ''' Wires resize/display-change handlers, applies the gradient, and runs
    ''' the first layout pass.
    ''' </summary>
    Public Sub Initialize()
        ' FillPanel must fill the host panel completely
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.FillPanel.Location = New Point(0, 0)
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                                    AnchorStyles.Left Or AnchorStyles.Right

        ApplyGradient(_form.FillPanel, "#EDFFE9", "#FFFFFF")

        ' Debounce timer — recalculates layout 250 ms after last resize event
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
    ''' Forces an immediate layout recalculation.
    ''' Call after data binding updates label text so centering is recalculated
    ''' against the new AutoSize width.
    ''' </summary>
    Public Sub RefreshLayout()
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Must be called from OnFormClosing to prevent handler/memory leaks.
    ''' </summary>
    Public Sub Cleanup()
        _resizeTimer.Stop()
        _resizeTimer.Dispose()
        RemoveHandler _form.Resize, AddressOf OnFormResize
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf OnDisplaySettingsChanged
    End Sub

    ' =========================================================================
    '  RESIZE EVENT CHAIN
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
    '
    '  Scale strategy:
    '  • One uniform scale factor = Min(clientW / ORIG_W, clientH / ORIG_H)
    '    so content is always proportional and never clips outside the panel.
    '  • Each control is positioned using sf * original_designer_value.
    '  • Text labels that hold multi-line content (Mission, Vision) receive a
    '    MaximumSize width constraint = 82% of client width so they wrap
    '    instead of expanding horizontally beyond the visible area.
    '  • BrgyLogoPic and BrgyMalyNameLbl are horizontally centred after sizing
    '    to stay anchored in the middle of the profile card at every resolution.
    ' =========================================================================
    Private Sub CalculateAndApplyLayout()
        Dim cw As Integer = _form.ClientSize.Width
        Dim ch As Integer = _form.ClientSize.Height
        If cw < 200 Or ch < 150 Then Exit Sub

        ' Sync FillPanel
        _form.FillPanel.Size = New Size(cw, ch)
        _form.FillPanel.Location = New Point(0, 0)

        ' Uniform scale factor — constrained by the tighter dimension
        Dim sx As Single = CSng(cw) / ORIG_W
        Dim sy As Single = CSng(ch) / ORIG_H
        Dim sf As Single = Math.Min(sx, sy)

        ' ------------------------------------------------------------------
        '  TITLE LABEL  — "Barangay Information"
        '  Centred horizontally by reading AutoSize width after font change.
        ' ------------------------------------------------------------------
        _form.TitleLbl.Font = New Font("Arial",
                                            SafeFont(D_TitleFont * sf, MIN_TITLE_FONT),
                                            FontStyle.Bold)
        _form.TitleLbl.AutoSize = True
        _form.TitleLbl.Location = New Point(
            CInt((cw - _form.TitleLbl.Width) / 2),
            CInt(D_TitleY * sf))
        _form.TitleLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' ------------------------------------------------------------------
        '  BARANGAY LOGO PICTUREBOX
        '  Sized proportionally; centred horizontally so it stays in the
        '  middle of the card at every resolution.
        ' ------------------------------------------------------------------
        Dim logoW As Integer = CInt(D_Logo(2) * sf)
        Dim logoH As Integer = CInt(D_Logo(3) * sf)
        _form.BrgyLogoPic.Size = New Size(logoW, logoH)
        _form.BrgyLogoPic.Location = New Point(
            CInt((cw - logoW) / 2),
            CInt(D_Logo(1) * sf))
        _form.BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage
        _form.BrgyLogoPic.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' ------------------------------------------------------------------
        '  BARANGAY NAME LABEL  — e.g. "Barangay Maly"
        '  Centred horizontally after AutoSize reflows.
        ' ------------------------------------------------------------------
        _form.BrgyMalyNameLbl.Font = New Font("Arial",
                                                    SafeFont(D_NameFont * sf, MIN_NAME_FONT),
                                                    FontStyle.Bold)
        _form.BrgyMalyNameLbl.AutoSize = True
        _form.BrgyMalyNameLbl.Location = New Point(
            CInt((cw - _form.BrgyMalyNameLbl.Width) / 2),
            CInt(D_NameY * sf))
        _form.BrgyMalyNameLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' ------------------------------------------------------------------
        '  MISSION SECTION TITLE  — "Mission"
        ' ------------------------------------------------------------------
        _form.TitleMissionLbl.Font = New Font("Arial",
                                                    SafeFont(D_MsnTitleFont * sf, MIN_SECTION_FONT),
                                                    FontStyle.Bold)
        _form.TitleMissionLbl.AutoSize = True
        _form.TitleMissionLbl.Location = New Point(
            CInt(D_MsnTitleX * sf),
            CInt(D_MsnTitleY * sf))
        _form.TitleMissionLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' ------------------------------------------------------------------
        '  MISSION INFO LABEL  — multi-line paragraph content
        '  MaximumSize width = 82% of client width so the text wraps and
        '  does not overflow the right edge at any resolution.
        ' ------------------------------------------------------------------
        _form.MissionInfoLbl.Font = New Font("Yu Gothic Medium",
                                                      SafeFont(D_MsnInfoFont * sf, MIN_CONTENT_FONT),
                                                      FontStyle.Bold Or FontStyle.Italic)
        _form.MissionInfoLbl.AutoSize = True
        _form.MissionInfoLbl.MaximumSize = New Size(CInt(cw * 0.82), 0)
        _form.MissionInfoLbl.Location = New Point(
            CInt(D_MsnInfoX * sf),
            CInt(D_MsnInfoY * sf))
        _form.MissionInfoLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' ------------------------------------------------------------------
        '  VISION SECTION TITLE  — "Vision"
        ' ------------------------------------------------------------------
        _form.VisionLbl.Font = New Font("Arial",
                                             SafeFont(D_VsnTitleFont * sf, MIN_SECTION_FONT),
                                             FontStyle.Bold)
        _form.VisionLbl.AutoSize = True
        _form.VisionLbl.Location = New Point(
            CInt(D_VsnTitleX * sf),
            CInt(D_VsnTitleY * sf))
        _form.VisionLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' ------------------------------------------------------------------
        '  VISION INFO LABEL  — multi-line paragraph content
        '  Same MaximumSize constraint as MissionInfoLbl.
        ' ------------------------------------------------------------------
        _form.VisionInfoLbl.Font = New Font("Yu Gothic Medium",
                                                     SafeFont(D_VsnInfoFont * sf, MIN_CONTENT_FONT),
                                                     FontStyle.Bold Or FontStyle.Italic)
        _form.VisionInfoLbl.AutoSize = True
        _form.VisionInfoLbl.MaximumSize = New Size(CInt(cw * 0.82), 0)
        _form.VisionInfoLbl.Location = New Point(
            CInt(D_VsnInfoX * sf),
            CInt(D_VsnInfoY * sf))
        _form.VisionInfoLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' ------------------------------------------------------------------
        '  LEFT NAVIGATION ARROW  — vertically centred, inset from left edge
        ' ------------------------------------------------------------------
        _form.LeftButtonPB.Location = New Point(
            CInt(D_LeftBtn(0) * sf),
            CInt(D_LeftBtn(1) * sf))
        _form.LeftButtonPB.Size = New Size(
            CInt(D_LeftBtn(2) * sf),
            CInt(D_LeftBtn(3) * sf))
        _form.LeftButtonPB.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.LeftButtonPB.Cursor = Cursors.Hand
    End Sub

    ' =========================================================================
    '  VISUAL STYLING HELPERS
    ' =========================================================================

    ''' <summary>
    ''' Paints a horizontal linear gradient over the given panel.
    ''' </summary>
    Friend Sub ApplyGradient(pnl As Control,
                              startColorHex As String,
                              endColorHex As String)
        Dim sc = ColorTranslator.FromHtml(startColorHex)
        Dim ec = ColorTranslator.FromHtml(endColorHex)
        Dim brush As New LinearGradientBrush(
            New Point(0, 0), New Point(Math.Max(pnl.Width, 1), 0), sc, ec)
        Dim local = pnl
        AddHandler local.Paint, Sub(s, ev)
                                    ev.Graphics.FillRectangle(brush, local.ClientRectangle)
                                End Sub
    End Sub

    ' =========================================================================
    '  UTILITY
    ' =========================================================================

    ''' <summary>
    ''' Returns the larger of <paramref name="size"/> and
    ''' <paramref name="minimum"/> to prevent fonts from becoming unreadable.
    ''' </summary>
    Private Shared Function SafeFont(size As Single, minimum As Single) As Single
        Return Math.Max(size, minimum)
    End Function

End Class
