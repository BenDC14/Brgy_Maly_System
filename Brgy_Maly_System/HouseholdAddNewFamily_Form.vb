Imports System.Drawing.Drawing2D

Public Class HouseholdAddNewFamily_Form
    ' === UI State ===
    Private householdIdForFamily As Integer = -1

    ' === Responsive Manager Instance ===
    Private responsiveManager As HouseholdAddNewFamilyResponsiveManager

    ''' <summary>
    ''' Constructor - Accept household ID for adding family
    ''' </summary>
    Public Sub New(Optional householdId As Integer = -1)
        InitializeComponent()
        householdIdForFamily = householdId
    End Sub

    Private Sub HouseholdAddNewFamily_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        UIUtilities.ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        UIUtilities.RoundButtonCorners(btnSaveFamily, 20)
        UIUtilities.RoundButtonCorners(BtnSaveRelationship, 20)
        UIUtilities.RoundButtonCorners(btnBack, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New HouseholdAddNewFamilyResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Validate Household ID ===
        If householdIdForFamily <= 0 Then
            MsgBox("No household selected.", MsgBoxStyle.Information, "Info")
        End If
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
    ''' Save Family button click
    ''' </summary>
    Private Sub btnSaveFamily_Click(sender As Object, e As EventArgs) Handles btnSaveFamily.Click
        ' TODO: Implement save family functionality
    End Sub

    ''' <summary>
    ''' Save Relationship button click
    ''' </summary>
    Private Sub BtnSaveRelationship_Click(sender As Object, e As EventArgs) Handles BtnSaveRelationship.Click
        ' TODO: Implement save relationship functionality
    End Sub

    ''' <summary>
    ''' Back button click
    ''' </summary>
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdMainForm As New HouseholdMain_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdMainForm)
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
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

    ' ========================================
    ' TODO: Add your business logic methods here
    ' ========================================
    ' - Load families from database for selected household
    ' - Load family members into DataGridView (with columns: Name, Age, Relationship, Status)
    ' - Save Family button implementation:
    '   - Validate inputs (Household, Family Name, Family Head)
    '   - Check for duplicate families in household
    '   - Insert family record into database
    '   - Refresh DataGridView
    ' - Save Relationship button implementation:
    '   - Get selected DataGridView row
    '   - Validate Relationship Type is selected
    '   - Update family member relationship/role in database
    '   - Refresh DataGridView
    ' - Handle family member selection in DataGridView
    ' - Add/Remove family member functionality (future enhancement)
    ' ========================================

End Class