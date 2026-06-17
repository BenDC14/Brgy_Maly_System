Imports System.Drawing.Drawing2D

Public Class LogInForm
    Private responsiveManager As LogInResponsiveManager
    Private passwordVisible As Boolean = False

    Private loginLogic As New LoginFormLogic()

    Public Shared CurrentUserRole As String = ""
    Public Shared CurrentUsername As String = ""
    Public Shared CurrentUserID As Integer = -1
    Public Shared CurrentFirstName As String = ""
    Public Shared CurrentLastName As String = ""
    Public Shared AccessibleForms As New List(Of String)

    ' Stores full permissions per form:
    ' CanView, CanAdd, CanEdit, CanDelete
    Public Shared CurrentPermissions As New Dictionary(Of String, LoginFormLogic.UserPermissionData)

    Private Sub LogInForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RoundButtonCorners(LogInBtn, 15)

        responsiveManager = New LogInResponsiveManager(Me)
        responsiveManager.Initialize()

        UnameTxtBox.TabIndex = 0
        PassTxtbox.TabIndex = 1
        LogInBtn.TabIndex = 2

        UnameTxtBox.Clear()
        PassTxtbox.Clear()
        UnameTxtBox.Focus()
    End Sub

    Private Sub RoundButtonCorners(ByRef btn As Button, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        btn.Region = New Region(p)
    End Sub

    Private Sub LogInBtn_Click(sender As Object, e As EventArgs) Handles LogInBtn.Click
        Dim username As String = UnameTxtBox.Text.Trim()
        Dim password As String = PassTxtbox.Text

        Dim authResult As LoginFormLogic.AuthenticationResult = loginLogic.AuthenticateUser(username, password)

        If authResult.IsSuccess Then
            CurrentUserID = authResult.UserId
            CurrentUsername = authResult.Username
            CurrentFirstName = authResult.FirstName
            CurrentLastName = authResult.LastName
            CurrentUserRole = authResult.Role
            AccessibleForms = authResult.AccessibleForms
            CurrentPermissions = authResult.Permissions



            LoginSuccess(authResult.Role)

            UnameTxtBox.Clear()
            PassTxtbox.Clear()
        Else
            MsgBox(authResult.ErrorMessage, MsgBoxStyle.Critical, "Login Failed")
            PassTxtbox.Clear()
            PassTxtbox.Focus()
        End If
    End Sub

    Private Sub LoginSuccess(userRole As String)
        Me.Hide()

        Dim dashboard As New Dashboard_Layout()

        ApplyRoleBasedAccess(dashboard, userRole)

        dashboard.Show()
    End Sub

    Private Sub ApplyRoleBasedAccess(dashboard As Dashboard_Layout, userRole As String)
        Select Case userRole.ToLower()
            Case "super admin"
                dashboard.DashboardBtn.Visible = True
                dashboard.ResidentBtn.Visible = True
                dashboard.BtnHousehold.Visible = True
                dashboard.BtnAyudaProgram.Visible = True
                dashboard.BtnReports.Visible = True
                dashboard.BtnEditBarangayInfo.Visible = True
                dashboard.BtnAudit.Visible = True
                dashboard.BtnDatabaseBackup.Visible = True
                dashboard.BtnAccounts.Visible = True
                dashboard.btnAyudaAdding.Visible = True
                dashboard.btnHouseholdAdding.Visible = True

            Case "admin"
                dashboard.DashboardBtn.Visible = True

                dashboard.ResidentBtn.Visible = CanView("ResidentMain_Form")
                dashboard.BtnHousehold.Visible = CanView("HouseholdMain_Form")
                dashboard.BtnAyudaProgram.Visible = CanView("AyudaMain_Form")
                dashboard.BtnReports.Visible = CanView("ReportsMain_Form")

                dashboard.BtnEditBarangayInfo.Visible = False
                dashboard.BtnAudit.Visible = False
                dashboard.BtnDatabaseBackup.Visible = False
                dashboard.BtnAccounts.Visible = False

                ' These are add-only shortcut pages.
                dashboard.btnAyudaAdding.Visible = CanAdd("AyudaAdd_Form")
                dashboard.btnHouseholdAdding.Visible = CanAdd("HouseholdAdding_Form")

            Case Else
                dashboard.DashboardBtn.Visible = True
                dashboard.ResidentBtn.Visible = False
                dashboard.BtnHousehold.Visible = False
                dashboard.BtnAyudaProgram.Visible = False
                dashboard.BtnReports.Visible = False
                dashboard.BtnEditBarangayInfo.Visible = False
                dashboard.BtnAudit.Visible = False
                dashboard.BtnDatabaseBackup.Visible = False
                dashboard.BtnAccounts.Visible = False
                dashboard.btnAyudaAdding.Visible = False
                dashboard.btnHouseholdAdding.Visible = False
        End Select
    End Sub

    Public Shared Function CanView(formClass As String) As Boolean
        If CurrentUserRole.ToLower() = "super admin" Then Return True

        Return CurrentPermissions.ContainsKey(formClass) AndAlso
               CurrentPermissions(formClass).CanView
    End Function

    Public Shared Function CanAdd(formClass As String) As Boolean
        If CurrentUserRole.ToLower() = "super admin" Then Return True

        Return CurrentPermissions.ContainsKey(formClass) AndAlso
               CurrentPermissions(formClass).CanAdd
    End Function

    Public Shared Function CanEdit(formClass As String) As Boolean
        If CurrentUserRole.ToLower() = "super admin" Then Return True

        Return CurrentPermissions.ContainsKey(formClass) AndAlso
               CurrentPermissions(formClass).CanEdit
    End Function

    Public Shared Function CanDelete(formClass As String) As Boolean
        If CurrentUserRole.ToLower() = "super admin" Then Return True

        Return CurrentPermissions.ContainsKey(formClass) AndAlso
               CurrentPermissions(formClass).CanDelete
    End Function

    Private Sub SeePassBtn_Click(sender As Object, e As EventArgs) Handles SeePassBtn.Click
        passwordVisible = Not passwordVisible

        If passwordVisible Then
            SeePassBtn.Image = My.Resources.eyesopen
            PassTxtbox.PasswordChar = ChrW(0)
        Else
            SeePassBtn.Image = My.Resources.closeeye
            PassTxtbox.PasswordChar = ChrW(42)
        End If
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If

        MyBase.OnFormClosing(e)
    End Sub

    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) Handles ExitBtn.Click
        If MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub ForgetPassLbl_Click(sender As Object, e As EventArgs) Handles ForgetPassLbl.Click
        Me.Hide()
        ForgetPass.Show()
    End Sub

End Class
