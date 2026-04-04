Imports System.Drawing.Drawing2D

Public Class HouseholdViewFamily_Form
    ' === UI State ===
    Private viewingHouseholdId As Integer = -1

    ' === Responsive Manager Instance ===
    Private responsiveManager As HouseholdViewFamilyResponsiveManager

    ''' <summary>
    ''' Constructor - Accept household ID for viewing families
    ''' </summary>
    Public Sub New(Optional householdId As Integer = -1)
        InitializeComponent()
        viewingHouseholdId = householdId
    End Sub

    Private Sub HouseholdViewFamily_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        UIUtilities.ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        UIUtilities.RoundButtonCorners(btnEditFamily, 20)
        UIUtilities.RoundButtonCorners(btnBack, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New HouseholdViewFamilyResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Load Families for this Household ===
        LoadFamiliesForHousehold()
    End Sub

    ''' <summary>
    ''' Load families for the selected household
    ''' </summary>
    Private Sub LoadFamiliesForHousehold()
        Try
            If viewingHouseholdId > 0 Then
                ' TODO: Call logic to load families for this household
                ' Dim families = familyLogic.GetFamiliesByHousehold(viewingHouseholdId)
                ' dgvFamilies.DataSource = families
            Else
                MsgBox("No household selected.", MsgBoxStyle.Information, "Info")
            End If
        Catch ex As Exception
            MsgBox("Error loading families: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
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
    ''' Edit Family button click
    ''' </summary>
    Private Sub btnEditFamily_Click(sender As Object, e As EventArgs) Handles btnEditFamily.Click
        ' TODO: Implement edit family functionality
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
    ' - Load family/household information from database
    ' - Load family members/heads into dgvFamilyHeads
    ' - Display family details based on selected household
    ' - Handle DataGridView row selection
    ' - Edit Family button:
    '   - Navigate to HouseholdEditFamily_Form
    '   - Pass selected family ID/information
    ' - Back to Main button:
    '   - Navigate to HouseholdMain_Form
    ' - Refresh DataGridView after edits
    ' - DataGridView columns: ID, Name, Relationship, Status, Date Added, etc.
    ' ========================================

End Class