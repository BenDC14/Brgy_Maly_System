Imports System.Drawing.Drawing2D

Public Class LogInForm
    ' === Responsive Manager Instance ===
    Private responsiveManager As LogInResponsiveManager
    Private passwordVisible As Boolean = False

    ' === Service Layer (handles all authentication logic) ===
    Private loginLogic As New LoginFormLogic()

    ' === Store user info for later use ===
    Public Shared CurrentUserRole As String = ""
    Public Shared CurrentUsername As String = ""
    Public Shared CurrentUserID As Integer = -1
    Public Shared CurrentFirstName As String = ""
    Public Shared CurrentLastName As String = ""
    Public Shared AccessibleForms As New List(Of String)

    Private Sub LogInForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Rounded Corners to Button ===
        RoundButtonCorners(LogInBtn, 15)

        ' === Initialize Responsive Manager ===
        responsiveManager = New LogInResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Set Tab Order ===
        UnameTxtBox.TabIndex = 0
        PassTxtbox.TabIndex = 1
        LogInBtn.TabIndex = 2

        ' === CLEAR ALL FIELDS WHEN FORM LOADS ===
        UnameTxtBox.Clear()
        PassTxtbox.Clear()
        UnameTxtBox.Focus()
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
    ''' Login button click handler
    ''' Delegates authentication to LoginFormLogic service
    ''' </summary>
    Private Sub LogInBtn_Click(sender As Object, e As EventArgs) Handles LogInBtn.Click
        ' === GET CREDENTIALS FROM UI ===
        Dim username As String = UnameTxtBox.Text.Trim()
        Dim password As String = PassTxtbox.Text

        ' === AUTHENTICATE USER (Service handles all logic) ===
        Dim authResult As LoginFormLogic.AuthenticationResult = loginLogic.AuthenticateUser(username, password)

        ' === CHECK AUTHENTICATION RESULT ===
        If authResult.IsSuccess Then
            ' === STORE USER INFO ===
            CurrentUserID = authResult.UserId
            CurrentUsername = authResult.Username
            CurrentFirstName = authResult.FirstName
            CurrentLastName = authResult.LastName
            CurrentUserRole = authResult.Role
            AccessibleForms = authResult.AccessibleForms

            ' === SHOW DASHBOARD ===
            LoginSuccess(authResult.Role)
            UnameTxtBox.Clear()
            PassTxtbox.Clear()
        Else
            ' === SHOW ERROR MESSAGE ===
            MsgBox(authResult.ErrorMessage, MsgBoxStyle.Critical, "Login Failed")
            PassTxtbox.Clear()
            PassTxtbox.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handle successful login and show Dashboard with role-based access
    ''' </summary>
    Private Sub LoginSuccess(userRole As String)
        Me.Hide()

        ' Create new dashboard instance
        Dim dashboard As New Dashboard_Layout()

        ' Apply role-based button visibility
        ApplyRoleBasedAccess(dashboard, userRole)

        ' Show dashboard
        dashboard.Show()
    End Sub

    ''' <summary>
    ''' Apply role-based button visibility to Dashboard
    ''' Super Admin: All buttons visible
    ''' Admin: Only assigned forms visible (from UserPermissions table)
    ''' </summary>
    Private Sub ApplyRoleBasedAccess(dashboard As Dashboard_Layout, userRole As String)
        Select Case userRole.ToLower()
            Case "super admin"
                ' === SUPER ADMIN: SHOW ALL BUTTONS ===
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
                ' === ADMIN: Show only assigned forms ===
                dashboard.DashboardBtn.Visible = True ' Dashboard always visible
                dashboard.ResidentBtn.Visible = AccessibleForms.Contains("ResidentMain_Form")
                dashboard.BtnHousehold.Visible = AccessibleForms.Contains("HouseholdMain_Form")
                dashboard.BtnAyudaProgram.Visible = AccessibleForms.Contains("AyudaMain_Form")
                dashboard.BtnReports.Visible = AccessibleForms.Contains("ReportsMain_Form")

                ' Hide admin/system buttons (never show to Admin)
                dashboard.BtnEditBarangayInfo.Visible = False
                dashboard.BtnAudit.Visible = False
                dashboard.BtnDatabaseBackup.Visible = False
                dashboard.BtnAccounts.Visible = False
                dashboard.btnAyudaAdding.Visible = False
                dashboard.btnHouseholdAdding.Visible = False

            Case Else
                ' === DEFAULT: SHOW MINIMAL ACCESS ===
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

    ''' <summary>
    ''' Toggle password visibility - SAME LOGIC AS FORGETPASS.VB
    ''' </summary>
    Private Sub SeePassBtn_Click(sender As Object, e As EventArgs) Handles SeePassBtn.Click
        passwordVisible = Not passwordVisible

        If passwordVisible Then
            SeePassBtn.Image = My.Resources.eyesopen
            PassTxtbox.PasswordChar = ChrW(0)  ' Show password
        Else
            SeePassBtn.Image = My.Resources.closeeye
            PassTxtbox.PasswordChar = ChrW(42) ' Hide password (asterisk)
        End If
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