Imports System.Drawing.Drawing2D

Public Class AyudaAdd_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As AyudaAddResponsiveUIManager

    Private Sub AyudaAdd_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        RoundButtonCorners(btnSave, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New AyudaAddResponsiveUIManager(Me)
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
    ''' Apply rounded corners to button
    ''' </summary>
    Private Sub RoundButtonCorners(ByRef btn As Button, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        btn.Region = New Region(p)
    End Sub

    ''' <summary>
    ''' Exit button click - Close modal
    ''' </summary>
    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) Handles ExitBtn.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Save button click (backend placeholder)
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' TODO: Implement when backend is ready
        ' - Validate txtAddNewAyuda is not empty
        ' - Save new ayuda program to database
        ' - Show success message
        ' - Close modal and refresh AyudaMain_Form
    End Sub

    ' ========================================
    ' TODO: Add your business logic methods here
    ' ========================================
    ' - Validate ayuda name (required, no duplicates)
    ' - Save new ayuda program to database
    ' - Return DialogResult.OK on success
    ' - Handle errors gracefully
    ' ========================================

End Class