Imports System.Drawing.Drawing2D

Public Class NewReportType_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As NewReportTypeResponsiveManager

    ''' <summary>
    ''' Constructor - Enable double buffering
    ''' </summary>
    Public Sub New()
        InitializeComponent()

        ' === Enable double buffering (must be set here, it's Protected) ===
        Me.DoubleBuffered = True
    End Sub

    Private Sub NewReportType_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling (Once - never reapply) ===
        RoundButtonCorners(btnAddNewReportType, 20)
        RoundButtonCorners(btnAddNewReportSubType, 20)

        ' === Apply TextBox Styling ===
        ApplyTextBoxStyling(txtReportType)
        ApplyTextBoxStyling(txtReportSubType)

        ' === Initialize Responsive Manager (Modal Pattern) ===
        responsiveManager = New NewReportTypeResponsiveManager(Me)
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
    ''' Apply textbox styling with focus effect
    ''' </summary>
    Private Sub ApplyTextBoxStyling(textBox As TextBox)
        Dim normalBackColor As Color = Color.FromArgb(237, 237, 237)
        Dim focusBackColor As Color = Color.FromArgb(220, 255, 220)

        textBox.BorderStyle = BorderStyle.FixedSingle
        textBox.BackColor = normalBackColor
        textBox.ForeColor = Color.Black

        AddHandler textBox.GotFocus, Sub(s, e)
                                         textBox.BackColor = focusBackColor
                                     End Sub

        AddHandler textBox.LostFocus, Sub(s, e)
                                          textBox.BackColor = normalBackColor
                                      End Sub
    End Sub

    ''' <summary>
    ''' Exit button - close the dialog
    ''' </summary>
    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) Handles ExitBtn.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Add New Report Type button click
    ''' </summary>
    Private Sub btnAddNewReportType_Click(sender As Object, e As EventArgs) Handles btnAddNewReportType.Click
        If String.IsNullOrWhiteSpace(txtReportType.Text) Then
            MsgBox("Please enter a Report Type.", MsgBoxStyle.Information, "Validation Error")
            txtReportType.Focus()
            Exit Sub
        End If

        ' TODO: Add database logic to save new report type
        MsgBox("New Report Type: " & txtReportType.Text & vbCrLf & "Save functionality to be implemented.",
               MsgBoxStyle.Information, "Add Report Type")

        txtReportType.Clear()
        txtReportType.Focus()
    End Sub

    ''' <summary>
    ''' Add New Report Sub Type button click
    ''' </summary>
    Private Sub btnAddNewReportSubType_Click(sender As Object, e As EventArgs) Handles btnAddNewReportSubType.Click
        If String.IsNullOrWhiteSpace(txtReportSubType.Text) Then
            MsgBox("Please enter a Report Sub Type.", MsgBoxStyle.Information, "Validation Error")
            txtReportSubType.Focus()
            Exit Sub
        End If

        ' TODO: Add database logic to save new report sub type
        MsgBox("New Report Sub Type: " & txtReportSubType.Text & vbCrLf & "Save functionality to be implemented.",
               MsgBoxStyle.Information, "Add Report Sub Type")

        txtReportSubType.Clear()
        txtReportSubType.Focus()
    End Sub

End Class