Imports System.Drawing.Drawing2D

Public Class ReportsMain_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As ReportsMainResponsiveManager

    Private Sub ReportsMain_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling (Once - never reapply) ===
        RoundButtonCorners(btnNewReportType, 20)
        RoundButtonCorners(btnSearch, 20)
        RoundButtonCorners(btnGenerate, 20)
        RoundButtonCorners(btnViewGeneratedReports, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New ReportsMainResponsiveManager(Me)
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
    ''' Apply rounded corners to button (with resize handler)
    ''' </summary>
    Private Sub RoundButtonCorners(ByRef btn As Button, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        btn.Region = New Region(p)

        Dim btnLocal = btn

        AddHandler btn.Resize, Sub(s, args)
                                   Dim newPath As New GraphicsPath()
                                   newPath.AddArc(0, 0, radius, radius, 180, 90)
                                   newPath.AddArc(btnLocal.Width - radius, 0, radius, radius, 270, 90)
                                   newPath.AddArc(btnLocal.Width - radius, btnLocal.Height - radius, radius, radius, 0, 90)
                                   newPath.AddArc(0, btnLocal.Height - radius, radius, radius, 90, 90)
                                   newPath.CloseFigure()
                                   btnLocal.Region = New Region(newPath)
                               End Sub
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

    Private Sub btnViewGeneratedReports_Click(sender As Object, e As EventArgs) Handles btnViewGeneratedReports.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim ViewReport As New ViewGeneratedReports_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(ViewReport)

            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnNewReportType_Click(sender As Object, e As EventArgs) Handles btnNewReportType.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim NewReportTypeDialog As New NewReportType_Form()
                NewReportTypeDialog.ShowDialog()
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try

    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

    End Sub

    ' ========================================
    ' TODO: Add your business logic methods here
    ' ========================================
    ' - New Report Type button click handler (open NewReportType_Form modal)
    ' - Report Type combobox selection changed handler
    ' - Report Sub Type combobox selection changed handler
    ' - Filter sub types based on report type selection
    ' - Date range validation (from <= to)
    ' - Search button click handler
    ' - Filter DataGridView by search criteria
    ' - Generate button click handler
    ' - Validate selections before generating
    ' - Generate report based on type, sub type, date range
    ' - Export to Excel/PDF based on download type
    ' - View Generated Reports button click handler
    ' - Load existing reports from database
    ' - DataGridView data loading and refresh
    ' - Show progress during report generation
    ' ========================================
End Class