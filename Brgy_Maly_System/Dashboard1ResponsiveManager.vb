Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for Dashboard1_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class Dashboard1ResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As Dashboard1_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As Dashboard1_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior
    ''' </summary>
    Public Sub Initialize()
        ' === CRITICAL: Override Designer's fixed size on FillPanel ===
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.FillPanel.Location = New Point(0, 0)

        ' === MAIN CONTAINER - FILL AVAILABLE SPACE ===
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' === RESET ALL PANEL DOCKS TO NONE (Important!) ===
        _form.ResidentPnl.Dock = DockStyle.None
        _form.HouseholdPnl.Dock = DockStyle.None
        _form.StudentPnl.Dock = DockStyle.None
        _form.SeniorsPnl.Dock = DockStyle.None
        _form.PwdPnl.Dock = DockStyle.None
        _form.AgePnl.Dock = DockStyle.None
        _form.SexPnl.Dock = DockStyle.None

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event to recalculate layout when window resizes ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === CRITICAL: Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Calculate and apply layout for the first time ===
        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    ''' <summary>
    ''' CRITICAL: Fires when Windows resolution changes
    ''' </summary>
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Fires when form window resizes
    ''' </summary>
    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Exit Sub
        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    ''' <summary>
    ''' Timer tick - recalculates layout ONCE after resize stops
    ''' </summary>
    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Calculate positions and apply layout based on current form size
    ''' Uses PERCENTAGES for positioning and sizing (from exact Designer values)
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        ' === Use form's actual client size ===
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' === Calculate scale factor for font sizing ===
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        ' === Update FillPanel ===
        _form.FillPanel.Size = New Size(panelWidth, panelHeight)
        _form.FillPanel.Location = New Point(0, 0)

        ' === TITLE SECTION ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)

        ' === SUMMARY CARDS (Top Row) ===
        PositionSummaryCards(panelWidth, panelHeight, scaleFactor)

        ' === POPULATION STATISTICS SECTION ===
        PositionPopulationSection(panelWidth, panelHeight, scaleFactor)

        ' === NEXT BUTTON ===
        PositionNextButton(panelWidth, panelHeight, scaleFactor)

        ' === CHART PANELS (Bottom Row) ===
        PositionChartPanels(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position "Summary Dashboard" title
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(35, 54) on 1700x1004 = 2.1% from left, 5.4% from top
        _form.SummaryDashboard.Location = New Point(CInt(panelWidth * 0.021), CInt(panelHeight * 0.054))
        _form.SummaryDashboard.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.SummaryDashboard.Font = New Font("Arial Narrow", 27.75F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position the 5 summary cards (Residents, Household, Student, Seniors, PWD)
    ''' </summary>
    Private Sub PositionSummaryCards(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' === RESIDENT CARD ===
        ' Designer: Location(43, 145), Size(256, 188)
        PositionCard(_form.ResidentPnl, CInt(panelWidth * 0.025), CInt(panelHeight * 0.144),
                    CInt(panelWidth * 0.151), CInt(panelHeight * 0.187),
                    _form.LblResidents, _form.TotalResidentsLbl, scaleFactor)

        ' === HOUSEHOLD CARD ===
        ' Designer: Location(383, 145), Size(256, 188)
        PositionCard(_form.HouseholdPnl, CInt(panelWidth * 0.225), CInt(panelHeight * 0.144),
                    CInt(panelWidth * 0.151), CInt(panelHeight * 0.187),
                    _form.LblHousehold, _form.TotalHouseholdLbl, scaleFactor)

        ' === STUDENT CARD ===
        ' Designer: Location(734, 145), Size(256, 188)
        PositionCard(_form.StudentPnl, CInt(panelWidth * 0.432), CInt(panelHeight * 0.144),
                    CInt(panelWidth * 0.151), CInt(panelHeight * 0.187),
                    _form.LblStudents, _form.TotalStudentLbl, scaleFactor)

        ' === SENIORS CARD ===
        ' Designer: Location(1074, 145), Size(256, 188)
        PositionCard(_form.SeniorsPnl, CInt(panelWidth * 0.632), CInt(panelHeight * 0.144),
                    CInt(panelWidth * 0.151), CInt(panelHeight * 0.187),
                    _form.LblSenior, _form.TotalSeniorLbl, scaleFactor)

        ' === PWD CARD ===
        ' Designer: Location(1409, 145), Size(256, 188)
        PositionCard(_form.PwdPnl, CInt(panelWidth * 0.829), CInt(panelHeight * 0.144),
                    CInt(panelWidth * 0.151), CInt(panelHeight * 0.187),
                    _form.LblPWD, _form.TotalPwdLbl, scaleFactor)
    End Sub

    ''' <summary>
    ''' Helper: Position a single summary card with its labels
    ''' </summary>
    Private Sub PositionCard(pnl As Panel, x As Integer, y As Integer, w As Integer, h As Integer,
                            numberLabel As Label, textLabel As Label, scaleFactor As Single)
        pnl.Location = New Point(x, y)
        pnl.Size = New Size(w, h)
        pnl.Dock = DockStyle.None
        pnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' Position number label (e.g., "67") - Centered
        numberLabel.Location = New Point(0, CInt(h * 0.25))
        numberLabel.Font = New Font("Arial", 36.0F * scaleFactor, FontStyle.Bold)
        numberLabel.AutoSize = False
        numberLabel.Size = New Size(w, CInt(h * 0.4))
        numberLabel.TextAlign = ContentAlignment.MiddleCenter

        ' Position text label (e.g., "Residents", "PWD", etc.) - Centered
        textLabel.Location = New Point(0, CInt(h * 0.65))
        textLabel.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Bold)
        textLabel.AutoSize = False
        textLabel.Size = New Size(w, CInt(h * 0.3))
        textLabel.TextAlign = ContentAlignment.TopCenter
    End Sub

    ''' <summary>
    ''' Position "Population Statistics" label
    ''' </summary>
    Private Sub PositionPopulationSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(35, 394) on 1700x1004 = 2.1% from left, 39.2% from top
        _form.PopulationStatistics.Location = New Point(CInt(panelWidth * 0.021), CInt(panelHeight * 0.392))
        _form.PopulationStatistics.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.PopulationStatistics.Font = New Font("Arial Narrow", 27.75F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position Next button (arrow button)
    ''' </summary>
    Private Sub PositionNextButton(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(1588, 367), Size(100, 76)
        _form.NextBtn.Location = New Point(CInt(panelWidth * 0.934), CInt(panelHeight * 0.366))
        _form.NextBtn.Size = New Size(CInt(panelWidth * 0.059), CInt(panelHeight * 0.076))
        _form.NextBtn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.NextBtn.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position chart panels (Population by Age, Population by Sex)
    ''' </summary>
    Private Sub PositionChartPanels(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Calculate proper spacing to avoid overlap
        Dim chartMargin As Integer = CInt(panelWidth * 0.07)  ' 7% left margin
        Dim chartGap As Integer = CInt(panelWidth * 0.025)     ' 2.5% gap between charts
        Dim totalChartWidth As Integer = panelWidth - (chartMargin * 2) - chartGap
        Dim singleChartWidth As Integer = totalChartWidth \ 2
        Dim chartHeight As Integer = CInt(panelHeight * 0.52)  ' 52% height
        Dim chartTop As Integer = CInt(panelHeight * 0.447)    ' 44.7% from top

        ' === AGE CHART PANEL ===
        PositionChartPanel(_form.AgePnl, chartMargin, chartTop,
                          singleChartWidth, chartHeight,
                          _form.PopulationAgeLbl, _form.AgeChart, scaleFactor)

        ' === SEX CHART PANEL ===
        PositionChartPanel(_form.SexPnl, chartMargin + singleChartWidth + chartGap, chartTop,
                          singleChartWidth, chartHeight,
                          _form.PopulationSexLbl, _form.SexChart, scaleFactor)
    End Sub

    ''' <summary>
    ''' Helper: Position a single chart panel with title and chart
    ''' </summary>
    Private Sub PositionChartPanel(pnl As Panel, x As Integer, y As Integer, w As Integer, h As Integer,
                                   titleLabel As Label, chart As DataVisualization.Charting.Chart, scaleFactor As Single)
        pnl.Location = New Point(x, y)
        pnl.Size = New Size(w, h)
        pnl.BackColor = Color.White
        pnl.Dock = DockStyle.None
        pnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' Position title label at bottom center
        titleLabel.Location = New Point(0, CInt(h * 0.88))
        titleLabel.Font = New Font("Arial", 21.75F * scaleFactor, FontStyle.Bold)
        titleLabel.AutoSize = False
        titleLabel.Size = New Size(w, CInt(h * 0.1))
        titleLabel.TextAlign = ContentAlignment.TopCenter

        ' Position chart with padding
        Dim chartPadding As Integer = CInt(w * 0.05)
        chart.Location = New Point(chartPadding, CInt(h * 0.05))
        chart.Size = New Size(w - (chartPadding * 2), CInt(h * 0.78))
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
