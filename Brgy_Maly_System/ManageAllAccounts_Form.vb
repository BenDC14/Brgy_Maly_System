Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class ManageAllAccounts_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As ManageAllAccountsResponsiveManager

    ' === Service Layer (Business Logic) ===
    Private accountLogic As New ManageAllAccountsLogic()

    ' === UI State ===
    Private selectedUserId As Integer = -1
    Private isEditMode As Boolean = False
    Private allFormsData As DataTable = Nothing

    Private Sub ManageAllAccounts_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Setup password textbox ===
        txtPass.PasswordChar = "*"c

        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        RoundButtonCorners(BtnSearch, 20)
        RoundButtonCorners(btnAdd, 20)
        RoundButtonCorners(btnEdit, 20)
        RoundButtonCorners(btnArchieve, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New ManageAllAccountsResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Setup DataGridView (Professional UI) ===
        ConfigureDataGridView()

        ' === Load all available forms ===
        allFormsData = accountLogic.GetAllAvailableForms()

        ' === Setup role combobox ===
        cbRole.SelectedIndex = 1

        ' === Load all admin accounts ===
        LoadAllAdminAccounts()

        ' === Setup events ===
        AddHandler dgvAccounts.SelectionChanged, AddressOf dgvAccounts_SelectionChanged
        AddHandler dgvAccounts.CellDoubleClick, AddressOf dgvAccounts_CellDoubleClick

        ' === Clear form fields ===
        ClearForm()
    End Sub

    ''' <summary>
    ''' Configure DataGridView with professional styling and soft gray background
    ''' </summary>
    Private Sub ConfigureDataGridView()
        dgvAccounts.AutoGenerateColumns = False
        dgvAccounts.Columns.Clear()
        dgvAccounts.ReadOnly = True
        dgvAccounts.AllowUserToAddRows = False
        dgvAccounts.AllowUserToDeleteRows = False
        dgvAccounts.AllowUserToResizeRows = False
        dgvAccounts.RowHeadersVisible = False

        ' === STYLING - SOFT GRAY BACKGROUND ===
        dgvAccounts.BackgroundColor = Color.FromArgb(220, 220, 220) ' Soft gray
        dgvAccounts.GridColor = Color.FromArgb(180, 180, 180) ' Medium gray for grid lines
        dgvAccounts.ColumnHeadersHeight = 35
        dgvAccounts.RowTemplate.Height = 30

        ' === COLUMN HEADERS STYLING ===
        dgvAccounts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        dgvAccounts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvAccounts.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
        dgvAccounts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' === ROW STYLING - BLACK TEXT ON SOFT GRAY BACKGROUND ===
        dgvAccounts.DefaultCellStyle.Font = New Font("Arial", 10)
        dgvAccounts.DefaultCellStyle.ForeColor = Color.Black
        dgvAccounts.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240) ' Very light gray
        dgvAccounts.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgvAccounts.DefaultCellStyle.Padding = New Padding(5)

        ' === ALTERNATE ROW COLOR - SLIGHTLY DARKER GRAY ===
        dgvAccounts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)
        dgvAccounts.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black

        ' === SELECTION STYLING ===
        dgvAccounts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        dgvAccounts.DefaultCellStyle.SelectionForeColor = Color.Black

        ' === ADD COLUMNS ===
        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "UserId",
            .DataPropertyName = "UserId",
            .Visible = False
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FirstName",
            .HeaderText = "First Name",
            .DataPropertyName = "FirstName",
            .Width = 120,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "LastName",
            .HeaderText = "Last Name",
            .DataPropertyName = "LastName",
            .Width = 120,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Username",
            .HeaderText = "Username",
            .DataPropertyName = "Username",
            .Width = 130,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Role",
            .HeaderText = "Role",
            .DataPropertyName = "Role",
            .Width = 80,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        })

        dgvAccounts.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "IsActive",
            .HeaderText = "Active",
            .DataPropertyName = "IsActive",
            .Width = 70,
            .ReadOnly = True,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "CreatedAt",
            .HeaderText = "Created Date",
            .DataPropertyName = "CreatedAt",
            .Width = 130,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        })
    End Sub

    ''' <summary>
    ''' Load all admin accounts (excluding SuperAdmin)
    ''' </summary>
    Private Sub LoadAllAdminAccounts()
        Try
            Dim dataTable As DataTable = accountLogic.GetAllAdminAccounts()
            dgvAccounts.DataSource = dataTable
        Catch ex As Exception
            MsgBox("Error loading accounts: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Apply gradient background
    ''' </summary>
    Private Sub ApplyGradient(pnl As Control, startColorHex As String, endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)
        Dim brush As New LinearGradientBrush(New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
        Dim panelLocal = pnl
        AddHandler panelLocal.Paint, Sub(sender, e) e.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
    End Sub

    ''' <summary>
    ''' Apply rounded corners to button
    ''' </summary>
    Private Sub RoundButtonCorners(btn As Button, radius As Integer)
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
    ''' Search button click
    ''' </summary>
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadAllAdminAccounts()
            Else
                Dim dataTable As DataTable = accountLogic.SearchAdminAccounts(txtSearch.Text)
                dgvAccounts.DataSource = dataTable
            End If
        Catch ex As Exception
            MsgBox("Error searching accounts: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Add Account button click
    ''' </summary>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ' === VALIDATE ===
        Dim validationError As String = accountLogic.ValidateAccountData(txtFname.Text, txtLname.Text, txtUname.Text, txtPass.Text, cbRole.SelectedItem, True)
        If Not String.IsNullOrEmpty(validationError) Then
            MsgBox(validationError, MsgBoxStyle.Information, "Validation")
            Return
        End If

        ' === GET SELECTED FORMS ===
        Dim selectedFormIds As List(Of Integer) = accountLogic.GetSelectedFormIds(cbResident.Checked, cbHousehold.Checked, cbAyuda.Checked, cbReports.Checked, allFormsData)

        ' === ADD ACCOUNT ===
        Try
            Dim newUserId As Integer = accountLogic.AddAdminAccount(txtFname.Text, txtLname.Text, txtUname.Text, txtPass.Text, cbRole.SelectedItem.ToString())

            If newUserId > 0 Then
                ' === ASSIGN FORMS ===
                If selectedFormIds.Count > 0 Then
                    accountLogic.AssignFormsToUser(newUserId, selectedFormIds, LogInForm.CurrentUserID)
                End If

                MsgBox("Account created successfully!", MsgBoxStyle.Information, "Success")
                ClearForm()
                LoadAllAdminAccounts()
            Else
                MsgBox("Failed to create account. Username may already exist.", MsgBoxStyle.Exclamation, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Edit Account button click
    ''' </summary>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If selectedUserId = -1 Then
            MsgBox("Please select an account to edit.", MsgBoxStyle.Information, "Selection Required")
            Return
        End If

        Dim validationError As String = accountLogic.ValidateAccountData(txtFname.Text, txtLname.Text, txtUname.Text, txtPass.Text, cbRole.SelectedItem, False)
        If Not String.IsNullOrEmpty(validationError) Then
            MsgBox(validationError, MsgBoxStyle.Information, "Validation")
            Return
        End If

        Dim selectedFormIds As List(Of Integer) = accountLogic.GetSelectedFormIds(cbResident.Checked, cbHousehold.Checked, cbAyuda.Checked, cbReports.Checked, allFormsData)

        Try
            If accountLogic.UpdateAdminAccount(selectedUserId, txtFname.Text, txtLname.Text, txtUname.Text, txtPass.Text) Then
                accountLogic.AssignFormsToUser(selectedUserId, selectedFormIds, LogInForm.CurrentUserID)
                MsgBox("Account updated successfully!", MsgBoxStyle.Information, "Success")
                ClearForm()
                LoadAllAdminAccounts()
                isEditMode = False
                selectedUserId = -1
            Else
                MsgBox("Failed to update account.", MsgBoxStyle.Exclamation, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Archive Account button click
    ''' </summary>
    Private Sub btnArchieve_Click(sender As Object, e As EventArgs) Handles btnArchieve.Click
        If selectedUserId = -1 Then
            MsgBox("Please select an account to archive.", MsgBoxStyle.Information, "Selection Required")
            Return
        End If

        If MsgBox("Archive this account? The user will not be able to login.", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm") = DialogResult.Yes Then
            Try
                If accountLogic.ArchiveAccount(selectedUserId) Then
                    MsgBox("Account archived successfully!", MsgBoxStyle.Information, "Success")
                    ClearForm()
                    LoadAllAdminAccounts()
                    selectedUserId = -1
                Else
                    MsgBox("Failed to archive account.", MsgBoxStyle.Exclamation, "Error")
                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    ''' <summary>
    ''' DataGridView row selection changed - Load selected account data
    ''' </summary>
    Private Sub dgvAccounts_SelectionChanged(sender As Object, e As EventArgs)
        If dgvAccounts.SelectedRows.Count > 0 Then
            LoadSelectedAccountData(dgvAccounts.SelectedRows(0))
        End If
    End Sub

    ''' <summary>
    ''' DataGridView cell double-click - Select row and load all data
    ''' </summary>
    Private Sub dgvAccounts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        ' === ONLY PROCESS IF CLICKING ON A DATA CELL (NOT HEADER) ===
        If e.RowIndex < 0 Then Exit Sub

        ' === GET COLUMN NAME THAT WAS DOUBLE-CLICKED ===
        Dim columnName As String = dgvAccounts.Columns(e.ColumnIndex).Name

        ' === ONLY ALLOW DOUBLE-CLICK ON EDITABLE COLUMNS ===
        Select Case columnName
            Case "FirstName", "LastName", "Username", "Role"
                ' === SELECT THE ROW ===
                dgvAccounts.ClearSelection()
                dgvAccounts.Rows(e.RowIndex).Selected = True

                ' === LOAD ALL ACCOUNT DATA ===
                LoadSelectedAccountData(dgvAccounts.Rows(e.RowIndex))

                ' === FOCUS ON THE CORRESPONDING TEXTBOX ===
                Select Case columnName
                    Case "FirstName"
                        txtFname.Focus()
                        txtFname.SelectAll()
                    Case "LastName"
                        txtLname.Focus()
                        txtLname.SelectAll()
                    Case "Username"
                        txtUname.Focus()
                        txtUname.SelectAll()
                    Case "Role"
                        cbRole.Focus()
                End Select

            Case "CreatedAt"
                ' === SHOW INFO ABOUT CREATED DATE ===
                Dim cellValue As String = dgvAccounts.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
                MsgBox("Created Date: " & cellValue, MsgBoxStyle.Information, "Account Information")
        End Select
    End Sub

    ''' <summary>
    ''' Load all selected account data from a DataGridView row
    ''' </summary>
    Private Sub LoadSelectedAccountData(selectedRow As DataGridViewRow)
        ' === EXTRACT USER ID ===
        selectedUserId = CInt(selectedRow.Cells("UserId").Value)

        ' === POPULATE ALL FORM FIELDS ===
        txtFname.Text = selectedRow.Cells("FirstName").Value.ToString()
        txtLname.Text = selectedRow.Cells("LastName").Value.ToString()
        txtUname.Text = selectedRow.Cells("Username").Value.ToString()
        cbRole.SelectedItem = selectedRow.Cells("Role").Value.ToString()

        ' === LOAD PASSWORD FROM SERVICE ===
        Try
            Dim encryptedPassword As String = accountLogic.GetUserPassword(selectedUserId)
            txtPass.Text = encryptedPassword
        Catch ex As Exception
            Debug.WriteLine("Error loading password: " & ex.Message)
            txtPass.Text = ""
        End Try

        ' === LOAD ASSIGNED FORMS ===
        Dim formStates As Dictionary(Of String, Boolean) = accountLogic.GetAssignedFormCheckStates(selectedUserId, allFormsData)
        cbResident.Checked = formStates("Resident")
        cbHousehold.Checked = formStates("Household")
        cbAyuda.Checked = formStates("Ayuda")
        cbReports.Checked = formStates("Reports")

        isEditMode = True
    End Sub

    ''' <summary>
    ''' Clear form fields
    ''' </summary>
    Private Sub ClearForm()
        txtFname.Clear()
        txtLname.Clear()
        txtUname.Clear()
        txtPass.Clear()
        cbRole.SelectedIndex = 1
        cbResident.Checked = False
        cbHousehold.Checked = False
        cbAyuda.Checked = False
        cbReports.Checked = False
        selectedUserId = -1
        isEditMode = False
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

    Private Sub FillPanel_Paint(sender As Object, e As PaintEventArgs) Handles FillPanel.Paint

    End Sub
End Class