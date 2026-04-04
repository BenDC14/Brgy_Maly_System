Imports System.Drawing.Drawing2D

Public Class Dashboard_Layout
    Private activeButton As Button

    ' === STATIC REFERENCE TO CURRENT DASHBOARD INSTANCE ===
    Public Shared CurrentInstance As Dashboard_Layout = Nothing

    Private Sub Dashboard_Layout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === STORE REFERENCE TO THIS INSTANCE FIRST ===
        CurrentInstance = Me

        ' === WINDOW SETUP ===
        Me.WindowState = FormWindowState.Maximized

        ' === STYLING ===
        RoundPanel(LeftPanel, 20)
        ApplyGradient(TopPanel, "#EDFFE9", "#FFFFFF")
        ApplyTopPanelShadow(TopPanel)

        ' === SETUP ALL MENU BUTTONS ===
        SetupMenuButtons()

        ' === DISPLAY CURRENT USER NAME IN USERACCOUNT BUTTON ===
        DisplayCurrentUser()

        ' === SELECT INITIAL BUTTON ===
        SelectMenuButton(DashboardBtn)

        ' === LOAD INITIAL FORM ===
        Dim dashboardForm As New Dashboard1_Form()
        LoadContentPanel(dashboardForm)
    End Sub

    ''' <summary>
    ''' Display current logged-in user's username in UserAccountBtn
    ''' </summary>
    Private Sub DisplayCurrentUser()
        Try
            ' === GET USERNAME FROM LOGINFORM (stored during login) ===
            If Not String.IsNullOrEmpty(LogInForm.CurrentUsername) Then
                UserAccountBtn.Text = LogInForm.CurrentUsername
            Else
                UserAccountBtn.Text = "User Account"
            End If
        Catch ex As Exception
            UserAccountBtn.Text = "User Account"
            Debug.WriteLine("Error displaying current user: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Setup all menu buttons with styling ONLY
    ''' </summary>
    Private Sub SetupMenuButtons()
        Dim allButtons As Button() = {DashboardBtn, ResidentBtn, BtnHousehold, BtnAyudaProgram,
                                      BtnReports, BtnEditBarangayInfo, BtnAudit,
                                      BtnDatabaseBackup, BtnAccounts, BtnLogOut, UserAccountBtn,
                                      btnAyudaAdding, btnHouseholdAdding}

        For Each btn In allButtons
            btn.FlatStyle = FlatStyle.Flat
            btn.UseVisualStyleBackColor = False
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 186, 15)
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 186, 15)
            btn.Cursor = Cursors.Hand

            If btn IsNot UserAccountBtn Then
                btn.Font = New Font("Arial", 12.0F, FontStyle.Bold)
                btn.ForeColor = Color.White
            Else
                btn.Font = New Font("Arial", 15.75F, FontStyle.Regular)
                btn.ForeColor = Color.Black
            End If
        Next

        ' Setup menu button event handlers
        SetupMenuButtonEvents()
    End Sub

    ''' <summary>
    ''' Setup menu button event handlers
    ''' </summary>
    Private Sub SetupMenuButtonEvents()
        AddHandler DashboardBtn.Click, Sub() HandleMenuButtonClick(DashboardBtn, New Dashboard1_Form())
        AddHandler ResidentBtn.Click, Sub() HandleMenuButtonClick(ResidentBtn, New ResidentMain_Form())
        AddHandler BtnHousehold.Click, Sub() HandleMenuButtonClick(BtnHousehold, New HouseholdMain_Form())
        AddHandler BtnAyudaProgram.Click, Sub() HandleMenuButtonClick(BtnAyudaProgram, New AyudaMain_Form())
        AddHandler BtnReports.Click, Sub() HandleMenuButtonClick(BtnReports, New ReportsMain_Form())
        AddHandler BtnEditBarangayInfo.Click, Sub() HandleMenuButtonClick(BtnEditBarangayInfo, New BrgyInfoAdding_Form())
        AddHandler BtnAudit.Click, Sub() HandleMenuButtonClick(BtnAudit, New Audit_Form())
        AddHandler BtnDatabaseBackup.Click, Sub() HandleMenuButtonClick(BtnDatabaseBackup, New DatabaseBackupMain_Form())
        AddHandler BtnAccounts.Click, Sub() HandleMenuButtonClick(BtnAccounts, New ManageAllAccounts_Form())

        ' === USER ACCOUNT BUTTON - Load current user's account ===
        AddHandler UserAccountBtn.Click, Sub(sender, e)
                                             Try
                                                 SelectMenuButton(UserAccountBtn)
                                                 Dim manageAccountForm As New ManageUserAccount_Form()
                                                 LoadContentPanel(manageAccountForm)
                                             Catch ex As Exception
                                                 MsgBox("Error loading account form: " & ex.Message, MsgBoxStyle.Critical, "Error")
                                             End Try
                                         End Sub

        ' === LOGOUT BUTTON - DO NOT USE CLICK HANDLER, ONLY DIRECT EVENT ===
        ' This is handled separately in BtnLogOut_Click to avoid recursion
    End Sub

    ''' <summary>
    ''' Load any form into the CenterPanel
    ''' </summary>
    Public Sub LoadContentPanel(ByVal whichForm As Form)
        Try
            ' Ensure any existing form in the panel is closed and disposed of
            If CenterPanel.Controls.Count > 0 Then
                Dim existingForm As Form = CType(CenterPanel.Controls(0), Form)
                existingForm.Close()
                existingForm.Dispose()
                CenterPanel.Controls.Clear()
            End If

            ' Configure the new form to be hosted within the panel
            whichForm.TopLevel = False         ' Essential: Treats the form as a control
            whichForm.FormBorderStyle = FormBorderStyle.None ' Remove the form's border
            whichForm.Dock = DockStyle.Fill      ' Makes the form fill the panel

            ' Add the form to the panel's controls collection
            CenterPanel.Controls.Add(whichForm)

            ' Show the form
            whichForm.Show()
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadContentPanel Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Handle menu button click and load form
    ''' </summary>
    Private Sub HandleMenuButtonClick(btn As Button, form As Form)
        Try
            SelectMenuButton(btn)
            LoadContentPanel(form)
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Select and highlight the active menu button
    ''' </summary>
    Private Sub SelectMenuButton(btn As Button)
        Dim allButtons As Button() = {DashboardBtn, ResidentBtn, BtnHousehold, BtnAyudaProgram,
                                  BtnReports, BtnEditBarangayInfo, BtnAudit,
                                  BtnDatabaseBackup, BtnAccounts, BtnLogOut, UserAccountBtn,
                                  btnAyudaAdding, btnHouseholdAdding}

        ' Reset all buttons
        For Each b In allButtons
            b.BackColor = Color.Transparent

            If b IsNot UserAccountBtn Then
                ' Sidebar menu buttons
                b.ForeColor = Color.White
                b.Font = New Font("Arial", 12.0F, FontStyle.Regular)
            Else
                ' UserAccountBtn in TopPanel
                b.ForeColor = Color.Black
                b.Font = New Font("Arial", 15.75F, FontStyle.Regular)
            End If

            b.Region = Nothing
            b.Invalidate()
        Next

        ' Set active button (only if it's a sidebar button)
        If btn IsNot UserAccountBtn Then
            btn.BackColor = Color.FromArgb(0, 186, 15)
            btn.ForeColor = Color.White
            btn.Font = New Font("Arial", 12.0F, FontStyle.Bold)
            RoundButtonCorners(btn, 10)
        Else
            ' UserAccountBtn stays with default styling when active
            btn.BackColor = Color.Transparent
            btn.ForeColor = Color.Black
            btn.Font = New Font("Arial", 15.75F, FontStyle.Bold)
        End If

        activeButton = btn
        btn.Invalidate()
    End Sub

    ''' <summary>
    ''' Shared Paint handler for all menu buttons
    ''' </summary>
    Private Sub MenuButton_Paint(sender As Object, e As PaintEventArgs) _
    Handles DashboardBtn.Paint, ResidentBtn.Paint, BtnHousehold.Paint,
            BtnAyudaProgram.Paint, BtnReports.Paint, BtnEditBarangayInfo.Paint,
            BtnAudit.Paint, BtnDatabaseBackup.Paint, BtnAccounts.Paint, BtnLogOut.Paint, UserAccountBtn.Paint,
            btnAyudaAdding.Paint, btnHouseholdAdding.Paint

        Dim btn = DirectCast(sender, Button)

        If btn Is activeButton AndAlso btn IsNot UserAccountBtn Then
            Dim shadowPen As New Pen(Color.FromArgb(80, 0, 0, 0), 2)
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.DrawLine(shadowPen, btn.Width - 3, 5, btn.Width - 3, btn.Height - 5)
            e.Graphics.DrawLine(shadowPen, 5, btn.Height - 3, btn.Width - 5, btn.Height - 3)
        End If
    End Sub

    ''' <summary>
    ''' Round button corners
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
    ''' Round panel corners
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

        AddHandler pnl.Resize, Sub(sender, e)
                                   p = New GraphicsPath()
                                   p.AddArc(0, 0, radius, radius, 180, 90)
                                   p.AddArc(panelLocal.Width - radius, 0, radius, radius, 270, 90)
                                   p.AddArc(panelLocal.Width - radius, panelLocal.Height - radius, radius, radius, 0, 90)
                                   p.AddArc(0, panelLocal.Height - radius, radius, radius, 90, 90)
                                   p.CloseFigure()
                                   panelLocal.Region = New Region(p)
                               End Sub
    End Sub

    ''' <summary>
    ''' Apply gradient
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
    ''' Apply top panel shadow
    ''' </summary>
    Private Sub ApplyTopPanelShadow(card As Panel)
        card.BackColor = Color.White
        card.Padding = New Padding(0, 0, 3, 3)

        Dim cardLocal = card

        AddHandler cardLocal.Paint, Sub(sender, e)
                                        Dim shadowPen As New Pen(Color.FromArgb(100, 150, 150, 150), 3)
                                        e.Graphics.DrawLine(shadowPen, cardLocal.Width - 3, 5, cardLocal.Width - 3, cardLocal.Height - 5)
                                        e.Graphics.DrawLine(shadowPen, 5, cardLocal.Height - 3, cardLocal.Width - 5, cardLocal.Height - 3)
                                    End Sub
    End Sub

    ' ===== INDIVIDUAL BUTTON CLICK HANDLERS =====
    ' Note: Most buttons use the event handler setup in SetupMenuButtonEvents()
    ' Only unique handlers are defined here

    ''' <summary>
    ''' Logout button - Direct click handler to avoid recursion
    ''' </summary>
    Private Sub BtnLogOut_Click(sender As Object, e As EventArgs) Handles BtnLogOut.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm Logout") = MsgBoxResult.Yes Then
            ' === CLEAR USER INFO ===
            LogInForm.CurrentUserID = -1
            LogInForm.CurrentUsername = ""
            LogInForm.CurrentFirstName = ""
            LogInForm.CurrentLastName = ""
            LogInForm.CurrentUserRole = ""
            LogInForm.AccessibleForms.Clear()

            ' === CLOSE DASHBOARD ===
            Me.Close()

            ' === SHOW LOGIN FORM ===
            LogInForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' Ayuda Adding button click - Navigate to AyudaAdd_Form
    ''' </summary>
    Private Sub btnAyudaAdding_Click(sender As Object, e As EventArgs) Handles btnAyudaAdding.Click
        Dim addAyudaProgramForm As New AyudaAdd_Form()
        addAyudaProgramForm.ShowDialog()
    End Sub

    ''' <summary>
    ''' Household Adding button click - Navigate to HouseholdAdding_Form
    ''' </summary>
    Private Sub btnHouseholdAdding_Click(sender As Object, e As EventArgs) Handles btnHouseholdAdding.Click
        SelectMenuButton(btnHouseholdAdding)
        Dim householdAddingForm As New HouseholdAdding_Form()
        LoadContentPanel(householdAddingForm)
    End Sub

End Class