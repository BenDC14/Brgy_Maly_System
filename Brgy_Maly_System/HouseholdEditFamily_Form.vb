Imports System.Drawing.Drawing2D

Public Class HouseholdEditFamily_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As HouseholdEditFamilyResponsiveManager

    ' === Store selected resident ID for editing ===
    Private selectedResidentID As Integer = -1

    Private Sub HouseholdEditFamily_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling (Once - never reapply) ===
        RoundButtonCorners(btnSaveChanges, 20)
        RoundButtonCorners(btnAddNewResident, 20)
        RoundButtonCorners(btnEditMember, 20)
        RoundButtonCorners(btnEditPosition, 20)
        RoundButtonCorners(btnBack, 20)
        RoundButtonCorners(btnAddNewFamilyMember, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New HouseholdEditFamilyResponsiveManager(Me)
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
    ''' Back button click - Navigate to HouseholdMain_Form
    ''' </summary>
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdMainForm As New HouseholdMain_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdMainForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Add New Resident button click - Navigate to HouseholdFamilyResidents_Form (Add mode)
    ''' </summary>
    Private Sub btnAddNewResident_Click(sender As Object, e As EventArgs) Handles btnAddNewResident.Click
        Dim householdFamilyResidentsForm As New HouseholdFamilyResidents_Form()
        ' Pass -1 to indicate "Add New" mode
        householdFamilyResidentsForm.SetFormMode(-1, Nothing)
        Dashboard_Layout.CurrentInstance.LoadContentPanel(householdFamilyResidentsForm)
    End Sub

    ''' <summary>
    ''' Edit Member button click - TEMPORARY BUTTON
    ''' Will be replaced with DataGridViewButtonColumn in the future
    ''' </summary>
    Private Sub btnEditMember_Click(sender As Object, e As EventArgs) Handles btnEditMember.Click
        ' TODO: REMOVE THIS BUTTON WHEN DATABASE IS CONNECTED
        ' This button will be replaced with a DataGridViewButtonColumn
        ' that will be added to FamilyMembersDGV for per-row editing

        MsgBox("Edit Member functionality will be available when backend is connected." & vbCrLf &
               "This button will be replaced with Edit buttons in the DataGridView.",
               MsgBoxStyle.Information, "Future Implementation")
    End Sub

    ''' <summary>
    ''' Edit Position button click (backend placeholder)
    ''' </summary>
    Private Sub btnEditPosition_Click(sender As Object, e As EventArgs) Handles btnEditPosition.Click
        ' TODO: Implement when backend is ready
        ' - Check if row is selected in DataGridView
        ' - Allow user to select relationship type
        ' - Update position/relationship in database
        ' - Refresh DataGridView
    End Sub

    ''' <summary>
    ''' Save Changes button click (backend placeholder)
    ''' </summary>
    Private Sub btnSaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
        ' TODO: Implement when backend is ready
        ' - Validate all required fields
        ' - Save changes to database
        ' - Show success message
    End Sub

    ''' <summary>
    ''' Add New Member button click - Navigate to HouseholdFamilyResidents_Form (Add mode)
    ''' </summary>
    Private Sub btnAddNewFamilyMember_Click(sender As Object, e As EventArgs) Handles btnAddNewFamilyMember.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdFamilyResidentsForm As New HouseholdFamilyResidents_Form()
                ' Pass -1 to indicate "Add New" mode
                householdFamilyResidentsForm.SetFormMode(-1, Nothing)
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdFamilyResidentsForm)
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

    ' ========================================
    ' TODO: Add your business logic methods here
    ' ========================================
    ' - Load family information into top fields (Family ID, Name, Household, Head, Total Members)
    ' - Load family members from database into FamilyMembersDGV
    ' - Populate cbResidents with available residents
    ' - Populate cbRelationships with relationship types (Head, Mother, Son, Daughter, etc.)
    ' 
    ' - Add New Member (btnAddNewFamilyMember):
    '   - Navigate to HouseholdFamilyResidents_Form (Add mode) ✓ IMPLEMENTED
    ' 
    ' - Save Changes button implementation:
    '   - Validate changes
    '   - Update family information in database
    '   - Show success/error messages
    ' 
    ' - Edit Member button:
    '   - TEMPORARY BUTTON - Will be replaced with DataGridViewButtonColumn
    '   - Currently shows info message
    '   - In the future: Add Edit button column to DataGridView for per-row editing
    ' 
    ' - Edit Position button:
    '   - Get selected DataGridView row
    '   - Allow user to change relationship type
    '   - Update relationship in database
    '   - Refresh DataGridView
    ' 
    ' - Handle member selection in DataGridView
    ' - Refresh DataGridView after CRUD operations
    ' ========================================

End Class