Imports System.Drawing.Drawing2D

Public Class Dashboard1_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As Dashboard1ResponsiveManager

    Private Sub Dashboard1_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Rounded Corners and Shadows to Cards ===
        ApplyCardStyling(ResidentPnl)
        ApplyCardStyling(HouseholdPnl)
        ApplyCardStyling(StudentPnl)
        ApplyCardStyling(SeniorsPnl)
        ApplyCardStyling(PwdPnl)

        ' === Apply Rounded Corners and Shadows to Chart Panels ===
        ApplyChartPanelStyling(AgePnl)
        ApplyChartPanelStyling(SexPnl)

        ' === Initialize Responsive Manager ===
        responsiveManager = New Dashboard1ResponsiveManager(Me)
        responsiveManager.Initialize()
    End Sub

    ''' <summary>
    ''' Apply gradient background to panel
    ''' </summary>
    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)

        Dim brush As New LinearGradientBrush(
            New Point(0, 0),
            New Point(pnl.Width, 0),
            startColor,
            endColor
        )

        Dim panelLocal = pnl

        AddHandler panelLocal.Paint, Sub(sender, e)
                                         e.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                                     End Sub
    End Sub

    ''' <summary>
    ''' Apply rounded corners and drop shadow to summary cards
    ''' </summary>
    Private Sub ApplyCardStyling(card As Panel)
        card.BackColor = Color.FromArgb(204, 255, 204)
        RoundPanel(card, 20)
        ApplyDropShadow(card)
    End Sub

    ''' <summary>
    ''' Apply rounded corners and drop shadow to chart panels
    ''' </summary>
    Private Sub ApplyChartPanelStyling(chart As Panel)
        chart.BackColor = Color.White
        RoundPanel(chart, 20)
        ApplyChartShadow(chart)
    End Sub

    ''' <summary>
    ''' Round panel corners (with resize handler)
    ''' </summary>
    Private Sub RoundPanel(ByRef pnl As Control, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(pnl.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(pnl.Width - radius, pnl.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, pnl.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        pnl.Region = New Region(p)

        Dim panelLocal = pnl

        AddHandler pnl.Resize, Sub(s, args)
                                   Dim newPath As New GraphicsPath()
                                   newPath.AddArc(0, 0, radius, radius, 180, 90)
                                   newPath.AddArc(panelLocal.Width - radius, 0, radius, radius, 270, 90)
                                   newPath.AddArc(panelLocal.Width - radius, panelLocal.Height - radius, radius, radius, 0, 90)
                                   newPath.AddArc(0, panelLocal.Height - radius, radius, radius, 90, 90)
                                   newPath.CloseFigure()
                                   panelLocal.Region = New Region(newPath)
                               End Sub
    End Sub

    ''' <summary>
    ''' Apply drop shadow to summary cards
    ''' </summary>
    Private Sub ApplyDropShadow(card As Panel)
        card.Padding = New Padding(0, 0, 3, 3)

        Dim cardLocal = card

        AddHandler cardLocal.Paint, Sub(s, args)
                                        Dim shadowPen As New Pen(Color.FromArgb(100, 150, 150, 150), 3)
                                        args.Graphics.DrawLine(shadowPen, cardLocal.Width - 3, 5, cardLocal.Width - 3, cardLocal.Height - 5)
                                        args.Graphics.DrawLine(shadowPen, 5, cardLocal.Height - 3, cardLocal.Width - 5, cardLocal.Height - 3)
                                    End Sub
    End Sub

    ''' <summary>
    ''' Apply drop shadow to chart panels
    ''' </summary>
    Private Sub ApplyChartShadow(card As Panel)
        card.Padding = New Padding(0, 0, 3, 3)

        Dim cardLocal = card

        AddHandler cardLocal.Paint, Sub(s, args)
                                        Dim shadowPen As New Pen(Color.FromArgb(80, 150, 150, 150), 2)
                                        args.Graphics.DrawLine(shadowPen, cardLocal.Width - 2, 3, cardLocal.Width - 2, cardLocal.Height - 3)
                                        args.Graphics.DrawLine(shadowPen, 3, cardLocal.Height - 2, cardLocal.Width - 3, cardLocal.Height - 2)
                                    End Sub
    End Sub

    ''' <summary>
    ''' Navigate to Dashboard 2
    ''' </summary>
    Private Sub NextBtn_Click(sender As Object, e As EventArgs) Handles NextBtn.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim dashboard2_form As New Dashboard2_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(dashboard2_form)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Cleanup when form closes
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If
        MyBase.OnFormClosing(e)
    End Sub

End Class