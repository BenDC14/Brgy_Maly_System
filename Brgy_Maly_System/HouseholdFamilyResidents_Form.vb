Imports System.Drawing.Drawing2D

Public Class HouseholdFamilyResidents_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As HouseholdFamilyResidentsResponsiveManager

    ' === Form Mode: -1 = Add New, > 0 = Edit Existing ===
    Private formMode As Integer = -1
    Private residentID As Integer = -1
    Private residentData As Dictionary(Of String, Object)

    Private Sub HouseholdFamilyResidents_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling (Once - never reapply) ===
        RoundButtonCorners(btnSave, 20)
        RoundButtonCorners(btnBack, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New HouseholdFamilyResidentsResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Load resident data if in edit mode ===
        If formMode > -1 Then
            LoadResidentDataIntoForm()
        End If
    End Sub

    ''' <summary>
    ''' Set form mode (Add New = -1, Edit = ResidentID)
    ''' Called from HouseholdEditFamily_Form
    ''' </summary>
    Public Sub SetFormMode(residentIDParam As Integer, residentDataParam As Dictionary(Of String, Object))
        residentID = residentIDParam
        residentData = residentDataParam
        formMode = residentIDParam
    End Sub

    ''' <summary>
    ''' Load resident data into form controls (Edit mode)
    ''' </summary>
    Private Sub LoadResidentDataIntoForm()
        ' TODO: When database is connected, implement this method
        ' This will populate form fields with existing resident data

        ' Example implementation:
        ' If residentData IsNot Nothing Then
        '     txtLastName.Text = residentData("LastName").ToString()
        '     txtFirstName.Text = residentData("FirstName").ToString()
        '     txtMiddleName.Text = residentData("MiddleName").ToString()
        '     txtSuffix.Text = residentData("Suffix").ToString()
        '     DTPDateofBirth.Value = CDate(residentData("DateOfBirth"))
        '     cbSex.SelectedIndex = cbSex.Items.IndexOf(residentData("Sex").ToString())
        '     cbCivilStatus.SelectedIndex = cbCivilStatus.Items.IndexOf(residentData("CivilStatus").ToString())
        '     txtContactNum.Text = residentData("ContactNumber").ToString()
        '     txtEmailAddress.Text = residentData("EmailAddress").ToString()
        '     txtHouseholdNumber.Text = residentData("HouseholdNumber").ToString()
        '     txtAdditionalInfo.Text = residentData("AdditionalInfo").ToString()
        '     
        '     ' Load category checkboxes
        '     cbSeniorCitizen.Checked = CBool(residentData("IsSeniorCitizen"))
        '     cbPWD.Checked = CBool(residentData("IsPWD"))
        '     ' ... and so on for other categories
        ' End If
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
    ''' Back button click - Navigate to HouseholdEditFamily_Form
    ''' </summary>
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdEditFamilyForm As New HouseholdEditFamily_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdEditFamilyForm)

            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Save button click (backend placeholder)
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validate required fields
        If String.IsNullOrWhiteSpace(txtLastName.Text) Then
            MsgBox("Please enter Last Name.", MsgBoxStyle.Exclamation, "Validation Error")
            txtLastName.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            MsgBox("Please enter First Name.", MsgBoxStyle.Exclamation, "Validation Error")
            txtFirstName.Focus()
            Return
        End If

        ' TODO: Implement when backend is ready
        ' - Validate all required fields
        ' - Calculate age from Date of Birth
        ' - Save resident information to database (Add or Update based on formMode)
        ' - Show success message
        ' - Navigate back to HouseholdEditFamily_Form
        ' - Refresh HouseholdEditFamily_Form DataGridView

        MsgBox("Resident information saved successfully!", MsgBoxStyle.Information, "Success")
        Dim householdEditFamilyForm As New HouseholdEditFamily_Form()
        Dashboard_Layout.LoadContentPanel(householdEditFamilyForm)
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
    ' - Validate required fields:
    '   - Last Name (required)
    '   - First Name (required)
    '   - Date of Birth (required for age calculation)
    ' - Auto-calculate age from Date of Birth
    ' - Validate email address format
    ' - Validate contact number format (digits only)
    ' - Save family resident information to database (INSERT for add, UPDATE for edit)
    ' - Handle category checkboxes (multiple selections allowed)
    ' - Populate form with resident data when in edit mode (SetFormMode called)
    ' - Handle navigation back to HouseholdEditFamily_Form
    ' - Refresh HouseholdEditFamily_Form DataGridView after save
    ' ========================================

End Class