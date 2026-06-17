' ================================================================================
' FILE: Dashboard1_Form.vb
' LAYER: Main Form UI Events Layer
'
' CONTROL MAP (from Designer):
'   Summary Cards:
'     ResidentPnl  → TotalResidentsLbl (number), LblResidents (title)
'     HouseholdPnl → TotalHouseholdLbl (number), LblHousehold (title)
'     StudentPnl   → TotalStudentLbl   (number), LblStudents  (title)
'     SeniorsPnl   → TotalSeniorLbl    (number), LblSenior    (title)
'     PwdPnl       → TotalPwdLbl       (number), LblPWD       (title)
'   Charts:
'     AgePnl  → AgeChart  (Column / vertical bar)
'     SexPnl  → SexChart  (Doughnut / pie)
' ================================================================================
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Dashboard1_Form

    ' ── Infrastructure ────────────────────────────────────────────────────────
    Private responsiveManager As Dashboard1_ResponsiveManager
    Private ReadOnly dashLogic As New Dashboard1_Logic()

    ' ============================================================================
    ' FORM LOAD
    ' ============================================================================
    Private Sub Dashboard1_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Visual polish
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            ApplyCardStyling(ResidentPnl)
            ApplyCardStyling(HouseholdPnl)
            ApplyCardStyling(StudentPnl)
            ApplyCardStyling(SeniorsPnl)
            ApplyCardStyling(PwdPnl)

            ApplyChartPanelStyling(AgePnl)
            ApplyChartPanelStyling(SexPnl)

            ' Responsive layout (must come before data so labels are sized)
            responsiveManager = New Dashboard1_ResponsiveManager(Me)
            responsiveManager.Initialize()

            ' ── Load summary counters ─────────────────────────────────────────
            LoadSummaryCounters()

            ' ── Load charts ──────────────────────────────────────────────────
            LoadAgeChart()
            LoadSexChart()

        Catch ex As Exception
            MsgBox("Error loading dashboard: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("Dashboard1_Form_Load Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================================
    ' SUMMARY COUNTERS
    ' Fetches each scalar total from the BLL and assigns to the number labels.
    ' ============================================================================
    Private Sub LoadSummaryCounters()
        Try
            TotalResidentsLbl.Text = dashLogic.GetTotalResidents().ToString()
            TotalHouseholdLbl.Text = dashLogic.GetTotalHouseholds().ToString()
            TotalStudentLbl.Text = dashLogic.GetTotalByCategory("student").ToString()
            TotalSeniorLbl.Text = dashLogic.GetTotalByCategory("senior").ToString()
            TotalPwdLbl.Text = dashLogic.GetTotalByCategory("pwd").ToString()
        Catch ex As Exception
            MsgBox("Error loading summary counts: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadSummaryCounters Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================================
    ' AGE CHART — Column (Vertical Bar) Chart
    ' Binds population counts grouped by age bracket.
    ' ============================================================================
    Private Sub LoadAgeChart()
        Try
            Dim dt As DataTable = dashLogic.GetPopulationByAge()

            ' ── Configure the chart ───────────────────────────────────────────
            AgeChart.Series.Clear()
            AgeChart.ChartAreas.Clear()
            AgeChart.Legends.Clear()

            Dim ca As New ChartArea("AgeArea")
            ca.BackColor = Color.White
            ca.AxisX.MajorGrid.Enabled = False
            ca.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
            ca.AxisX.LabelStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            ca.AxisY.LabelStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            ca.AxisX.LineColor = Color.FromArgb(180, 180, 180)
            ca.AxisY.LineColor = Color.FromArgb(180, 180, 180)
            AgeChart.ChartAreas.Add(ca)

            Dim series As New Series("Population")
            series.ChartType = SeriesChartType.Column
            series.ChartArea = "AgeArea"
            series.Color = Color.FromArgb(76, 175, 80)
            series.BorderColor = Color.FromArgb(56, 142, 60)
            series.BorderWidth = 1
            series.IsValueShownAsLabel = True
            series.LabelForeColor = Color.FromArgb(40, 40, 40)
            series.Font = New Font("Arial", 8, FontStyle.Bold)

            ' Bind rows: AgeBracket (X-axis label), Count (Y-axis value)
            For Each row As DataRow In dt.Rows
                Dim idx As Integer = series.Points.AddXY(
                    row("AgeBracket").ToString(),
                    CInt(row("Count")))
                ' Alternate bar colours for visual clarity
                series.Points(idx).Color = If(idx Mod 2 = 0,
                    Color.FromArgb(76, 175, 80),
                    Color.FromArgb(139, 195, 74))
            Next

            AgeChart.Series.Add(series)
            AgeChart.BackColor = Color.White
            AgeChart.BorderlineColor = Color.Transparent

        Catch ex As Exception
            Debug.WriteLine("LoadAgeChart Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================================
    ' SEX CHART — Doughnut Chart
    ' Binds Male vs Female population distribution.
    ' ============================================================================
    Private Sub LoadSexChart()
        Try
            Dim dt As DataTable = dashLogic.GetPopulationBySex()

            ' ── Configure the chart ───────────────────────────────────────────
            SexChart.Series.Clear()
            SexChart.ChartAreas.Clear()
            SexChart.Legends.Clear()

            Dim ca As New ChartArea("SexArea")
            ca.BackColor = Color.White
            SexChart.ChartAreas.Add(ca)

            Dim legend As New Legend("SexLegend")
            legend.Font = New Font("Arial", 9, FontStyle.Regular)
            legend.BackColor = Color.Transparent
            legend.BorderColor = Color.Transparent
            SexChart.Legends.Add(legend)

            Dim series As New Series("Distribution")
            series.ChartType = SeriesChartType.Doughnut
            series.ChartArea = "SexArea"
            series.Legend = "SexLegend"
            series.IsValueShownAsLabel = True
            series.LabelForeColor = Color.White
            series.Font = New Font("Arial", 10, FontStyle.Bold)
            series("DoughnutRadius") = "60"   ' inner hole size %
            series("PieStartAngle") = "270"  ' start from top

            ' Bind rows: Sex (legend label), Count (slice size)
            Dim sliceColors As Color() = {
                Color.FromArgb(33, 150, 243),    ' Male  → blue
                Color.FromArgb(233, 30, 99)      ' Female → pink/red
            }
            Dim colorIndex As Integer = 0
            For Each row As DataRow In dt.Rows
                Dim idx As Integer = series.Points.AddXY(
                    row("Sex").ToString(),
                    CInt(row("Count")))
                series.Points(idx).Label = $"{row("Sex")} ({row("Count")})"
                series.Points(idx).Color = If(colorIndex < sliceColors.Length,
                    sliceColors(colorIndex), Color.Gray)
                series.Points(idx).LegendText = row("Sex").ToString()
                colorIndex += 1
            Next

            SexChart.Series.Add(series)
            SexChart.BackColor = Color.White
            SexChart.BorderlineColor = Color.Transparent

        Catch ex As Exception
            Debug.WriteLine("LoadSexChart Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================================
    ' NAVIGATION
    ' ============================================================================
    Private Sub NextBtn_Click(sender As Object, e As EventArgs) Handles NextBtn.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dashboard_Layout.CurrentInstance.LoadContentPanel(New Dashboard2_Form())
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("NextBtn_Click Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================================
    ' VISUAL HELPERS
    ' ============================================================================
    Private Sub ApplyGradient(pnl As Control, startColorHex As String, endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)
        Dim panelLocal = pnl
        AddHandler panelLocal.Paint,
            Sub(s, e)
                Using brush As New LinearGradientBrush(
                    New Point(0, 0), New Point(panelLocal.Width, 0),
                    startColor, endColor)
                    e.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                End Using
            End Sub
    End Sub

    Private Sub ApplyCardStyling(card As Panel)
        card.BackColor = Color.FromArgb(204, 255, 204)
        RoundPanel(card, 20)
        ApplyDropShadow(card)
    End Sub

    Private Sub ApplyChartPanelStyling(chart As Panel)
        chart.BackColor = Color.White
        RoundPanel(chart, 20)
        ApplyChartShadow(chart)
    End Sub

    Private Sub RoundPanel(pnl As Control, radius As Integer)
        Dim panelLocal = pnl
        Dim applyRegion As Action =
            Sub()
                If panelLocal.Width <= 0 OrElse panelLocal.Height <= 0 Then Return
                Using p As New GraphicsPath()
                    p.AddArc(0, 0, radius, radius, 180, 90)
                    p.AddArc(panelLocal.Width - radius, 0, radius, radius, 270, 90)
                    p.AddArc(panelLocal.Width - radius, panelLocal.Height - radius, radius, radius, 0, 90)
                    p.AddArc(0, panelLocal.Height - radius, radius, radius, 90, 90)
                    p.CloseFigure()
                    panelLocal.Region = New Region(p)
                End Using
            End Sub
        applyRegion()
        AddHandler pnl.Resize, Sub(s, args) applyRegion()
    End Sub

    Private Sub ApplyDropShadow(card As Panel)
        card.Padding = New Padding(0, 0, 3, 3)
        Dim cardLocal = card
        AddHandler cardLocal.Paint,
            Sub(s, args)
                Using shadowPen As New Pen(Color.FromArgb(100, 150, 150, 150), 3)
                    args.Graphics.DrawLine(shadowPen, cardLocal.Width - 3, 5,
                                           cardLocal.Width - 3, cardLocal.Height - 5)
                    args.Graphics.DrawLine(shadowPen, 5, cardLocal.Height - 3,
                                           cardLocal.Width - 5, cardLocal.Height - 3)
                End Using
            End Sub
    End Sub

    Private Sub ApplyChartShadow(card As Panel)
        card.Padding = New Padding(0, 0, 3, 3)
        Dim cardLocal = card
        AddHandler cardLocal.Paint,
            Sub(s, args)
                Using shadowPen As New Pen(Color.FromArgb(80, 150, 150, 150), 2)
                    args.Graphics.DrawLine(shadowPen, cardLocal.Width - 2, 3,
                                           cardLocal.Width - 2, cardLocal.Height - 3)
                    args.Graphics.DrawLine(shadowPen, 3, cardLocal.Height - 2,
                                           cardLocal.Width - 3, cardLocal.Height - 2)
                End Using
            End Sub
    End Sub

    ' ============================================================================
    ' CLEANUP
    ' ============================================================================
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then responsiveManager.Cleanup()
        MyBase.OnFormClosing(e)
    End Sub

End Class
