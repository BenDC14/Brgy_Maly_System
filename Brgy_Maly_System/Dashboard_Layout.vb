Imports System.Drawing.Drawing2D

Public Class Dashboard_Layout
    Private activeButton As Button

    ' === STATIC REFERENCE TO CURRENT DASHBOARD INSTANCE ===
    Public Shared CurrentInstance As Dashboard_Layout = Nothing

    ' ─── Designer baseline constants ───────────────────────────────────────────
    ' The designer was authored against a 1920x1061 maximised window.
    ' LeftPanel width = 255, MenuPanel height = 985.
    ' We use these as the reference to derive proportional Y-positions.
    Private Const ORIGINAL_MENU_HEIGHT As Integer = 985
    Private Const MENU_BUTTON_HEIGHT As Integer = 51
    Private Const MENU_PANEL_WIDTH As Integer = 255

    ' ─── Ordered list of ALL sidebar nav buttons ───────────────────────────────
    ' This is the single source-of-truth for the sidebar button stack.
    ' BtnLogOut is NOT in this list; it is always pinned to the bottom.
    Private _navButtons As Button()

    ' ─── Form Load ─────────────────────────────────────────────────────────────

    Private Sub Dashboard_Layout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CurrentInstance = Me

        ' === WINDOW SETUP ===
        Me.WindowState = FormWindowState.Maximized

        ' === STYLING ===
        RoundPanel(LeftPanel, 20)
        ApplyGradient(TopPanel, "#EDFFE9", "#FFFFFF")
        ApplyTopPanelShadow(TopPanel)

        ' === BUILD ORDERED NAV BUTTON LIST ===
        ' Order matches the intended visual top-to-bottom sequence in the sidebar.
        _navButtons = New Button() {
            DashboardBtn,
            ResidentBtn,
            BtnHousehold,
            BtnAyudaProgram,
            BtnReports,
            BtnEditBarangayInfo,
            BtnAudit,
            BtnDatabaseBackup,
            BtnAccounts,
            btnAyudaAdding,
            btnHouseholdAdding,
            btnNewRelationshipType,
            btnNewCategoryAdding
        }

        ' === SETUP ALL MENU BUTTONS (style + events) ===
        SetupMenuButtons()

        ' === APPLY RESPONSIVE LAYOUT FOR THE FIRST TIME ===
        ApplySidebarLayout()

        ' === HOOK RESIZE TO REFLOW SIDEBAR ON EVERY SIZE CHANGE ===
        AddHandler Me.Resize, AddressOf Dashboard_Resize

        ' === DISPLAY CURRENT USER NAME IN USERACCOUNT BUTTON ===
        DisplayCurrentUser()

        ' === SELECT INITIAL BUTTON ===
        SelectMenuButton(DashboardBtn)

        ' === LOAD INITIAL FORM ===
        Dim dashboardForm As New Dashboard1_Form()
        LoadContentPanel(dashboardForm)
    End Sub

    ' ─── Resize handler ────────────────────────────────────────────────────────

    Private Sub Dashboard_Resize(sender As Object, e As EventArgs)
        ApplySidebarLayout()
    End Sub

    ' ─── Core responsive sidebar layout engine ─────────────────────────────────

    ''' <summary>
    ''' Recalculates and applies the Y-position of every sidebar nav button so
    ''' they all fit inside MenuPanel at any screen height.
    '''
    ''' Strategy:
    '''   1. BtnLogOut is always reserved at the very bottom (Dock = Bottom stays).
    '''   2. The remaining vertical space is divided proportionally among the nav
    '''      buttons using a uniform slot height.
    '''   3. If the calculated slot height would be smaller than a minimum (36 px),
    '''      the buttons are still positioned sequentially — the panel becomes
    '''      scrollable in that edge case.
    '''   4. All buttons are left-aligned and fill the panel width minus 12 px padding.
    ''' </summary>
    Private Sub ApplySidebarLayout()
        ' Safety guard — panel may not be ready yet at design time
        If MenuPanel Is Nothing OrElse _navButtons Is Nothing Then Exit Sub

        Dim panelH As Integer = MenuPanel.Height   ' current height of the docked menu panel
        Dim panelW As Integer = MenuPanel.Width    ' current width (normally = LeftPanel.Width)

        If panelH < 10 OrElse panelW < 10 Then Exit Sub

        ' ── Reserve space for BtnLogOut at the bottom ──────────────────────────
        ' BtnLogOut keeps Dock = Bottom, so it auto-sits at the bottom.
        ' We must not overlap it, so subtract its height from usable space.
        Dim reservedBottom As Integer = MENU_BUTTON_HEIGHT + 4   ' button height + a small gap
        Dim usableHeight As Integer = panelH - reservedBottom

        ' ── How many nav buttons are there? ────────────────────────────────────
        Dim count As Integer = _navButtons.Length
        If count = 0 Then Exit Sub

        ' ── Calculate a uniform slot height for each button ─────────────────────
        ' A "slot" is the vertical space allocated to one button including its gap.
        ' We add a small top margin (the first button starts a little below Y=0).
        Dim topMargin As Integer = CInt(panelH * 0.04)    ' ~4% top padding
        Dim available As Integer = usableHeight - topMargin
        Dim slotHeight As Integer = CInt(available / count)

        ' Enforce minimum slot so text is not crushed; clamp to button height
        Const MIN_SLOT As Integer = 40
        If slotHeight < MIN_SLOT Then slotHeight = MIN_SLOT

        ' ── Button width: fill the panel with a left margin ────────────────────
        Dim btnX As Integer = 8
        Dim btnWidth As Integer = panelW - btnX - 4   ' leave 4 px right gap

        ' ── Position each nav button ────────────────────────────────────────────
        Dim currentY As Integer = topMargin

        For Each btn As Button In _navButtons
            ' Vertically centre the button inside its slot
            Dim centreOffset As Integer = CInt((slotHeight - MENU_BUTTON_HEIGHT) / 2)
            Dim btnY As Integer = currentY + centreOffset

            btn.Location = New Point(btnX, btnY)
            btn.Size = New Size(btnWidth, MENU_BUTTON_HEIGHT)
            ' Pin to top-left of MenuPanel so future resizes do not drift them
            btn.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

            currentY += slotHeight
        Next

        ' ── Ensure BtnLogOut keeps its Dock = Bottom and correct width ──────────
        ' Dock handles Y automatically.  We only correct Width here because
        ' the Dock = Bottom style already stretches it horizontally by default.
        BtnLogOut.Dock = DockStyle.Bottom
        BtnLogOut.Height = MENU_BUTTON_HEIGHT
    End Sub

    ' ─── Current user display ──────────────────────────────────────────────────

    ''' <summary>
    ''' Display current logged-in user's username in UserAccountBtn.
    ''' </summary>
    Private Sub DisplayCurrentUser()
        Try
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

    ' ─── Button setup ──────────────────────────────────────────────────────────

    ''' <summary>
    ''' Applies uniform styling to every sidebar + top button, then wires events.
    ''' </summary>
    Private Sub SetupMenuButtons()
        ' Include BtnLogOut and UserAccountBtn in the styling pass
        Dim allButtons As Button() = New Button() {
            DashboardBtn, ResidentBtn, BtnHousehold, BtnAyudaProgram,
            BtnReports, BtnEditBarangayInfo, BtnAudit,
            BtnDatabaseBackup, BtnAccounts, BtnLogOut, UserAccountBtn,
            btnAyudaAdding, btnHouseholdAdding,
            btnNewRelationshipType, btnNewCategoryAdding
        }

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

        SetupMenuButtonEvents()
    End Sub

    ''' <summary>
    ''' Wires click handlers for all menu buttons.
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

        ' User Account button — loads current user's management form
        AddHandler UserAccountBtn.Click,
            Sub(sender, e)
                Try
                    SelectMenuButton(UserAccountBtn)
                    LoadContentPanel(New ManageUserAccount_Form())
                Catch ex As Exception
                    MsgBox("Error loading account form: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
            End Sub

        ' Relationship Type Adding — opens as popup dialog
        AddHandler btnNewRelationshipType.Click,
            Sub(sender, e)
                Try
                    SelectMenuButton(btnNewRelationshipType)
                    Using frm As New AddNewRelationshipType_Form()
                        frm.ShowDialog(Me)
                    End Using
                Catch ex As Exception
                    MsgBox("Error opening Relationship Type form: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
            End Sub

        ' Category Adding — opens as popup dialog
        AddHandler btnNewCategoryAdding.Click,
            Sub(sender, e)
                Try
                    SelectMenuButton(btnNewCategoryAdding)
                    Using frm As New AddNewCategory_form()
                        frm.ShowDialog(Me)
                    End Using
                Catch ex As Exception
                    MsgBox("Error opening Category form: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
            End Sub

        ' NOTE: BtnLogOut uses a direct Handles event (BtnLogOut_Click below)
        '       to avoid double-firing from both AddHandler and Handles.
    End Sub

    ' ─── Content panel loading ─────────────────────────────────────────────────

    ''' <summary>
    ''' Load any form into CenterPanel as a hosted child form.
    ''' </summary>
    Public Sub LoadContentPanel(ByVal whichForm As Form)
        Try
            If CenterPanel.Controls.Count > 0 Then
                Dim existingForm As Form = TryCast(CenterPanel.Controls(0), Form)
                If existingForm IsNot Nothing Then
                    existingForm.Close()
                    existingForm.Dispose()
                End If
                CenterPanel.Controls.Clear()
            End If

            whichForm.TopLevel = False
            whichForm.FormBorderStyle = FormBorderStyle.None
            whichForm.Dock = DockStyle.Fill

            CenterPanel.Controls.Add(whichForm)
            whichForm.Show()

        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadContentPanel Error: " & ex.Message)
        End Try
    End Sub

    ' ─── Menu button selection / highlight ────────────────────────────────────

    Private Sub HandleMenuButtonClick(btn As Button, form As Form)
        Try
            SelectMenuButton(btn)
            LoadContentPanel(form)
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Highlights the active sidebar button and resets all others.
    ''' </summary>
    Private Sub SelectMenuButton(btn As Button)
        Dim allButtons As Button() = New Button() {
            DashboardBtn, ResidentBtn, BtnHousehold, BtnAyudaProgram,
            BtnReports, BtnEditBarangayInfo, BtnAudit,
            BtnDatabaseBackup, BtnAccounts, BtnLogOut, UserAccountBtn,
            btnAyudaAdding, btnHouseholdAdding,
            btnNewRelationshipType, btnNewCategoryAdding
        }

        ' Reset every button
        For Each b In allButtons
            b.BackColor = Color.Transparent
            If b IsNot UserAccountBtn Then
                b.ForeColor = Color.White
                b.Font = New Font("Arial", 12.0F, FontStyle.Regular)
            Else
                b.ForeColor = Color.Black
                b.Font = New Font("Arial", 15.75F, FontStyle.Regular)
            End If
            b.Region = Nothing
            b.Invalidate()
        Next

        ' Highlight the clicked button
        If btn IsNot UserAccountBtn Then
            btn.BackColor = Color.FromArgb(0, 186, 15)
            btn.ForeColor = Color.White
            btn.Font = New Font("Arial", 12.0F, FontStyle.Bold)
            RoundButtonCorners(btn, 10)
        Else
            btn.BackColor = Color.Transparent
            btn.ForeColor = Color.Black
            btn.Font = New Font("Arial", 15.75F, FontStyle.Bold)
        End If

        activeButton = btn
        btn.Invalidate()
    End Sub

    ' ─── Paint handler for active-button shadow ───────────────────────────────

    Private Sub MenuButton_Paint(sender As Object, e As PaintEventArgs) _
        Handles DashboardBtn.Paint, ResidentBtn.Paint, BtnHousehold.Paint,
                BtnAyudaProgram.Paint, BtnReports.Paint, BtnEditBarangayInfo.Paint,
                BtnAudit.Paint, BtnDatabaseBackup.Paint, BtnAccounts.Paint,
                BtnLogOut.Paint, UserAccountBtn.Paint,
                btnAyudaAdding.Paint, btnHouseholdAdding.Paint,
                btnNewRelationshipType.Paint, btnNewCategoryAdding.Paint

        Dim btn = DirectCast(sender, Button)

        If btn Is activeButton AndAlso btn IsNot UserAccountBtn Then
            Dim shadowPen As New Pen(Color.FromArgb(80, 0, 0, 0), 2)
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.DrawLine(shadowPen, btn.Width - 3, 5, btn.Width - 3, btn.Height - 5)
            e.Graphics.DrawLine(shadowPen, 5, btn.Height - 3, btn.Width - 5, btn.Height - 3)
        End If
    End Sub

    ' ─── Drawing / styling helpers ────────────────────────────────────────────

    Private Sub RoundButtonCorners(ByRef btn As Button, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        btn.Region = New Region(p)
    End Sub

    Private Sub RoundPanel(ByRef pnl As Control, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(pnl.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(pnl.Width - radius, pnl.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, pnl.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        pnl.Region = New Region(p)

        Dim panelLocal = pnl
        AddHandler pnl.Resize,
            Sub(sender, e)
                Dim q As New GraphicsPath()
                q.AddArc(0, 0, radius, radius, 180, 90)
                q.AddArc(panelLocal.Width - radius, 0, radius, radius, 270, 90)
                q.AddArc(panelLocal.Width - radius, panelLocal.Height - radius, radius, radius, 0, 90)
                q.AddArc(0, panelLocal.Height - radius, radius, radius, 90, 90)
                q.CloseFigure()
                panelLocal.Region = New Region(q)
            End Sub
    End Sub

    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)

        Dim brush As New LinearGradientBrush(
            New Point(0, 0),
            New Point(pnl.Width, 0),
            startColor,
            endColor)

        Dim panelLocal = pnl
        AddHandler panelLocal.Paint,
            Sub(sender, e)
                e.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
            End Sub
    End Sub

    Private Sub ApplyTopPanelShadow(card As Panel)
        card.BackColor = Color.White
        card.Padding = New Padding(0, 0, 3, 3)

        Dim cardLocal = card
        AddHandler cardLocal.Paint,
            Sub(sender, e)
                Dim shadowPen As New Pen(Color.FromArgb(100, 150, 150, 150), 3)
                e.Graphics.DrawLine(shadowPen, cardLocal.Width - 3, 5, cardLocal.Width - 3, cardLocal.Height - 5)
                e.Graphics.DrawLine(shadowPen, 5, cardLocal.Height - 3, cardLocal.Width - 5, cardLocal.Height - 3)
            End Sub
    End Sub

    ' ─── Individual button click handlers ─────────────────────────────────────
    ' (Only buttons that need direct Handles wiring remain here.
    '  All others are AddHandler-wired in SetupMenuButtonEvents.)

    ''' <summary>
    ''' Logout button — uses direct Handles to avoid double-firing.
    ''' </summary>
    Private Sub BtnLogOut_Click(sender As Object, e As EventArgs) Handles BtnLogOut.Click
        If MsgBox("Are you sure you want to logout?",
                  MsgBoxStyle.Question Or MsgBoxStyle.YesNo,
                  "Confirm Logout") = MsgBoxResult.Yes Then

            LogInForm.CurrentUserID = -1
            LogInForm.CurrentUsername = ""
            LogInForm.CurrentFirstName = ""
            LogInForm.CurrentLastName = ""
            LogInForm.CurrentUserRole = ""
            LogInForm.AccessibleForms.Clear()

            Me.Close()
            LogInForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' Ayuda Adding — uses direct Handles for LoadContentPanel navigation.
    ''' </summary>
    Private Sub btnAyudaAdding_Click(sender As Object, e As EventArgs) Handles btnAyudaAdding.Click
        SelectMenuButton(btnAyudaAdding)
        LoadContentPanel(New AyudaAdd_Form())
    End Sub

    ''' <summary>
    ''' Household Adding — uses direct Handles for LoadContentPanel navigation.
    ''' </summary>
    Private Sub btnHouseholdAdding_Click(sender As Object, e As EventArgs) Handles btnHouseholdAdding.Click
        SelectMenuButton(btnHouseholdAdding)
        LoadContentPanel(New HouseholdAdding_Form())
    End Sub

    ''' <summary>
    ''' Relationship Type Adding — direct Handles; opens as a modal dialog only.
    ''' (AddHandler in SetupMenuButtonEvents also handles this button —
    '''  remove one of the two wiring points if double-firing occurs.)
    ''' </summary>
    Private Sub btnNewRelationshipType_Click(sender As Object, e As EventArgs) Handles btnNewRelationshipType.Click
        SelectMenuButton(btnNewRelationshipType)
        Using frm As New AddNewRelationshipType_Form()
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Category Adding — direct Handles; opens as a modal dialog only.
    ''' </summary>
    Private Sub btnNewCategoryAdding_Click(sender As Object, e As EventArgs) Handles btnNewCategoryAdding.Click
        SelectMenuButton(btnNewCategoryAdding)
        Using frm As New AddNewCategory_form()
            frm.ShowDialog(Me)
        End Using
    End Sub

End Class
