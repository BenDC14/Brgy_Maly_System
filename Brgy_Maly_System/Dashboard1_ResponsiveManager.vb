' ================================================================================
' FILE: Dashboard1_ResponsiveManager.vb
' LAYER: Universal Responsive Layout Manager for Dashboard1_Form
'
' Scales all panels and labels proportionally from the Designer baseline of
' 1700 × 1004 px down to 1366×768 and up to 1920×1080 without overlap.
'
' BUG FIX: PositionSummaryCards now passes arguments in the CORRECT order:
'   arg1 = the big NUMBER label  (TotalResidentsLbl, TotalHouseholdLbl, etc.)
'   arg2 = the static TITLE label (LblResidents, LblHousehold, etc.)
'
' PWD card special note: In the Designer, LblPWD has font 36pt (the number)
' and TotalPwdLbl has font 18pt (the title "PWD") — corrected in calls below.
' ================================================================================
Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

Public Class Dashboard1_ResponsiveManager

    ' ── Designer baseline dimensions ─────────────────────────────────────────
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' ── Form reference ────────────────────────────────────────────────────────
    Private ReadOnly _form As Dashboard1_Form

    ' ── Resize debounce timer ─────────────────────────────────────────────────
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ' ============================================================================
    ' CONSTRUCTOR
    ' ============================================================================
    Public Sub New(form As Dashboard1_Form)
        _form = form
    End Sub

    ' ============================================================================
    ' INITIALIZE
    ' ============================================================================
    Public Sub Initialize()
        ' Override Designer's fixed FillPanel size — dock to fill parent
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Location = New Point(0, 0)
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                                   AnchorStyles.Left Or AnchorStyles.Right

        ' Release any accidental Dock on the content panels
        For Each pnl As Panel In {_form.ResidentPnl, _form.HouseholdPnl,
                                   _form.StudentPnl, _form.SeniorsPnl,
                                   _form.PwdPnl, _form.AgePnl, _form.SexPnl}
            pnl.Dock = DockStyle.None
        Next

        ' Debounce timer — fires once after the user stops resizing
        resizeTimer.Interval = 250
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        AddHandler _form.Resize, AddressOf Form_Resize
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' Initial layout pass
        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    ' ============================================================================
    ' EVENT HANDLERS
    ' ============================================================================
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Return
        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ' ============================================================================
    ' MASTER LAYOUT CALCULATOR
    ' All measurements derived from percentage of actual client area.
    ' ============================================================================
    Public Sub CalculateAndApplyLayout()
        Dim W As Integer = _form.ClientSize.Width
        Dim H As Integer = _form.ClientSize.Height
        If W < 100 OrElse H < 100 Then Return

        ' Uniform scale factor drives font sizes
        Dim widthScale As Single = CSng(W) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(H) / ORIGINAL_HEIGHT
        Dim sf As Single = Math.Min(widthScale, heightScale)

        _form.FillPanel.Size = New Size(W, H)
        _form.FillPanel.Location = New Point(0, 0)

        PositionTitleSection(W, H, sf)
        PositionSummaryCards(W, H, sf)
        PositionPopulationSection(W, H, sf)
        PositionNextButton(W, H, sf)
        PositionChartPanels(W, H, sf)
    End Sub

    ' ============================================================================
    ' TITLE SECTION
    ' Designer baseline: Location(35, 54) on 1700×1004
    ' ============================================================================
    Private Sub PositionTitleSection(W As Integer, H As Integer, sf As Single)
        _form.SummaryDashboard.Location = New Point(CInt(W * 0.021), CInt(H * 0.054))
        _form.SummaryDashboard.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.SummaryDashboard.Font = New Font("Arial Narrow", Math.Max(10, 27.75F * sf), FontStyle.Bold)
    End Sub

    ' ============================================================================
    ' SUMMARY CARDS — 5 cards evenly distributed across the top row
    '
    ' ARGUMENT ORDER IS CRITICAL:
    '   PositionCard(panel, x, y, w, h,
    '                numberLbl  ← TotalXxxLbl  (big font, shows the count)
    '                titleLbl   ← LblXxx       (small font, shows the category name))
    '
    ' Designer baseline: y=145 (~14.4%), h=188 (~18.7%), first card x=43 (~2.5%)
    ' Cards are spaced with ~2% gap between them across the full width.
    ' ============================================================================
    Private Sub PositionSummaryCards(W As Integer, H As Integer, sf As Single)
        Dim cardW As Integer = CInt(W * 0.148)
        Dim cardH As Integer = CInt(H * 0.187)
        Dim cardY As Integer = CInt(H * 0.144)

        Dim x1 As Integer = CInt(W * 0.025)
        Dim x2 As Integer = CInt(W * 0.225)
        Dim x3 As Integer = CInt(W * 0.432)
        Dim x4 As Integer = CInt(W * 0.632)
        Dim x5 As Integer = CInt(W * 0.829)

        PositionCard(_form.ResidentPnl, x1, cardY, cardW, cardH, _form.TotalResidentsLbl, _form.LblResidents, sf)
        PositionCard(_form.HouseholdPnl, x2, cardY, cardW, cardH, _form.TotalHouseholdLbl, _form.LblHousehold, sf)
        PositionCard(_form.StudentPnl, x3, cardY, cardW, cardH, _form.TotalStudentLbl, _form.LblStudents, sf)
        PositionCard(_form.SeniorsPnl, x4, cardY, cardW, cardH, _form.TotalSeniorLbl, _form.LblSenior, sf)

        ' ── PWD — direct positioning (Designer labels are font-swapped vs others) ──
        _form.PwdPnl.Location = New Point(x5, cardY)
        _form.PwdPnl.Size = New Size(cardW, cardH)
        _form.PwdPnl.Dock = DockStyle.None
        _form.PwdPnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        _form.TotalPwdLbl.AutoSize = False
        _form.TotalPwdLbl.Location = New Point(0, CInt(cardH * 0.1))
        _form.TotalPwdLbl.Size = New Size(cardW, CInt(cardH * 0.55))
        _form.TotalPwdLbl.Font = New Font("Arial", Math.Max(12, 36.0F * sf), FontStyle.Bold)
        _form.TotalPwdLbl.TextAlign = ContentAlignment.MiddleCenter
        _form.TotalPwdLbl.ForeColor = Color.FromArgb(30, 110, 40)

        _form.LblPWD.AutoSize = False
        _form.LblPWD.Location = New Point(0, CInt(cardH * 0.65))
        _form.LblPWD.Size = New Size(cardW, CInt(cardH * 0.3))
        _form.LblPWD.Font = New Font("Arial", Math.Max(8, 18.0F * sf), FontStyle.Bold)
        _form.LblPWD.TextAlign = ContentAlignment.TopCenter
        _form.LblPWD.ForeColor = Color.FromArgb(60, 60, 60)
        _form.LblPWD.Text = "PWD"
    End Sub


    ' ============================================================================
    ' CARD HELPER
    ' numberLbl → big centered count (upper half of card)
    ' titleLbl  → smaller centered category name (lower portion of card)
    ' ============================================================================
    Private Sub PositionCard(pnl As Panel,
                             x As Integer, y As Integer,
                             w As Integer, h As Integer,
                             numberLbl As Label,
                             titleLbl As Label,
                             sf As Single)
        pnl.Location = New Point(x, y)
        pnl.Size = New Size(w, h)
        pnl.Dock = DockStyle.None
        pnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' ── Number label: big, centered, upper 55% of card ───────────────────
        numberLbl.AutoSize = False
        numberLbl.Location = New Point(0, CInt(h * 0.1))
        numberLbl.Size = New Size(w, CInt(h * 0.55))
        numberLbl.Font = New Font("Arial", Math.Max(12, 36.0F * sf), FontStyle.Bold)
        numberLbl.TextAlign = ContentAlignment.MiddleCenter
        numberLbl.ForeColor = Color.FromArgb(30, 110, 40)

        ' ── Title label: smaller, centered, lower 30% of card ────────────────
        titleLbl.AutoSize = False
        titleLbl.Location = New Point(0, CInt(h * 0.65))
        titleLbl.Size = New Size(w, CInt(h * 0.3))
        titleLbl.Font = New Font("Arial", Math.Max(8, 18.0F * sf), FontStyle.Bold)
        titleLbl.TextAlign = ContentAlignment.TopCenter
        titleLbl.ForeColor = Color.FromArgb(60, 60, 60)
    End Sub

    ' ============================================================================
    ' POPULATION STATISTICS SECTION TITLE
    ' Designer baseline: Location(35, 394) on 1700×1004 = 2.1%, 39.2%
    ' ============================================================================
    Private Sub PositionPopulationSection(W As Integer, H As Integer, sf As Single)
        _form.PopulationStatistics.Location = New Point(CInt(W * 0.021), CInt(H * 0.392))
        _form.PopulationStatistics.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.PopulationStatistics.Font = New Font("Arial Narrow", Math.Max(10, 27.75F * sf), FontStyle.Bold)
    End Sub

    ' ============================================================================
    ' NEXT BUTTON
    ' Designer baseline: Location(1588, 367), Size(100, 76)
    ' ============================================================================
    Private Sub PositionNextButton(W As Integer, H As Integer, sf As Single)
        _form.NextBtn.Location = New Point(CInt(W * 0.934), CInt(H * 0.366))
        _form.NextBtn.Size = New Size(CInt(W * 0.059), CInt(H * 0.076))
        _form.NextBtn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.NextBtn.Cursor = Cursors.Hand
    End Sub

    ' ============================================================================
    ' CHART PANELS
    ' Two equal-width panels side by side with a gap, filling ~86% of the width.
    ' Designer baseline: AgePnl at x=119, SexPnl at x=965, both size 624×535.
    ' ============================================================================
    Private Sub PositionChartPanels(W As Integer, H As Integer, sf As Single)
        Dim leftMargin As Integer = CInt(W * 0.065)   ' ≈ 6.5% left edge
        Dim gap As Integer = CInt(W * 0.025)   ' ≈ 2.5% gap between panels
        Dim totalW As Integer = W - (leftMargin * 2) - gap
        Dim chartPanelW As Integer = totalW \ 2
        Dim chartPanelH As Integer = CInt(H * 0.52)    ' ≈ 52% of height
        Dim chartTop As Integer = CInt(H * 0.447)   ' ≈ 44.7% from top

        PositionChartPanel(_form.AgePnl,
                           leftMargin, chartTop,
                           chartPanelW, chartPanelH,
                           _form.PopulationAgeLbl, _form.AgeChart, sf)

        PositionChartPanel(_form.SexPnl,
                           leftMargin + chartPanelW + gap, chartTop,
                           chartPanelW, chartPanelH,
                           _form.PopulationSexLbl, _form.SexChart, sf)
    End Sub

    ' ============================================================================
    ' CHART PANEL HELPER
    ' Title label sits at the bottom; chart fills the upper ~82% with padding.
    ' ============================================================================
    Private Sub PositionChartPanel(pnl As Panel,
                                   x As Integer, y As Integer,
                                   w As Integer, h As Integer,
                                   titleLbl As Label,
                                   chart As DataVisualization.Charting.Chart,
                                   sf As Single)
        pnl.Location = New Point(x, y)
        pnl.Size = New Size(w, h)
        pnl.BackColor = Color.White
        pnl.Dock = DockStyle.None
        pnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                         AnchorStyles.Left Or AnchorStyles.Right

        ' Title label — bottom strip
        titleLbl.AutoSize = False
        titleLbl.Location = New Point(0, CInt(h * 0.87))
        titleLbl.Size = New Size(w, CInt(h * 0.11))
        titleLbl.Font = New Font("Arial", Math.Max(8, 21.75F * sf), FontStyle.Bold)
        titleLbl.TextAlign = ContentAlignment.MiddleCenter
        titleLbl.ForeColor = Color.FromArgb(50, 50, 50)

        ' Chart — fills upper area with 4% horizontal padding, 3% vertical padding
        Dim hPad As Integer = CInt(w * 0.04)
        Dim vPad As Integer = CInt(h * 0.03)
        chart.Location = New Point(hPad, vPad)
        chart.Size = New Size(w - (hPad * 2), CInt(h * 0.82))
        chart.BackColor = Color.White
        chart.BorderlineColor = Color.Transparent
    End Sub

    ' ============================================================================
    ' CLEANUP
    ' ============================================================================
    Public Sub Cleanup()
        resizeTimer.Stop()
        resizeTimer.Dispose()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
