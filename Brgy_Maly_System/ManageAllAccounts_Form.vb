Imports System.Data
Imports System.Drawing.Drawing2D

Public Class ManageAllAccounts_Form
    Private responsiveManager As ManageAllAccountsResponsiveManager
    Private accountLogic As New ManageAllAccountsLogic()

    Private selectedUserId As Integer = -1
    Private isEditMode As Boolean = False
    Private allFormsData As DataTable = Nothing

    Private Sub ManageAllAccounts_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtPass.PasswordChar = "*"c

            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            RoundButtonCorners(BtnSearch, 20)
            RoundButtonCorners(btnAdd, 20)
            RoundButtonCorners(btnEdit, 20)
            RoundButtonCorners(btnArchieve, 20)

            responsiveManager = New ManageAllAccountsResponsiveManager(Me)
            responsiveManager.Initialize()

            ConfigureDataGridView()
            ConfigureFormsToAccessDataGridView()

            allFormsData = accountLogic.GetAllAvailableForms()
            LoadFormsToAccessGrid()

            cbRole.SelectedIndex = 1

            LoadAllAdminAccounts()

            AddHandler dgvAccounts.SelectionChanged, AddressOf dgvAccounts_SelectionChanged
            AddHandler dgvAccounts.CellDoubleClick, AddressOf dgvAccounts_CellDoubleClick

            ClearForm()

        Catch ex As Exception
            MsgBox("Error initializing account management form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ConfigureDataGridView()
        dgvAccounts.AutoGenerateColumns = False
        dgvAccounts.Columns.Clear()
        dgvAccounts.ReadOnly = True
        dgvAccounts.AllowUserToAddRows = False
        dgvAccounts.AllowUserToDeleteRows = False
        dgvAccounts.AllowUserToResizeRows = False
        dgvAccounts.RowHeadersVisible = False
        dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAccounts.MultiSelect = False

        dgvAccounts.BackgroundColor = Color.FromArgb(220, 220, 220)
        dgvAccounts.GridColor = Color.FromArgb(180, 180, 180)
        dgvAccounts.ColumnHeadersHeight = 35
        dgvAccounts.RowTemplate.Height = 30

        dgvAccounts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        dgvAccounts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvAccounts.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
        dgvAccounts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgvAccounts.DefaultCellStyle.Font = New Font("Arial", 10)
        dgvAccounts.DefaultCellStyle.ForeColor = Color.Black
        dgvAccounts.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dgvAccounts.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgvAccounts.DefaultCellStyle.Padding = New Padding(5)
        dgvAccounts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        dgvAccounts.DefaultCellStyle.SelectionForeColor = Color.Black

        dgvAccounts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)
        dgvAccounts.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "UserId",
            .DataPropertyName = "UserId",
            .Visible = False
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FirstName",
            .HeaderText = "First Name",
            .DataPropertyName = "FirstName",
            .Width = 120
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "LastName",
            .HeaderText = "Last Name",
            .DataPropertyName = "LastName",
            .Width = 120
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Username",
            .HeaderText = "Username",
            .DataPropertyName = "Username",
            .Width = 130
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Role",
            .HeaderText = "Role",
            .DataPropertyName = "Role",
            .Width = 80
        })

        dgvAccounts.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "IsActive",
            .HeaderText = "Active",
            .DataPropertyName = "IsActive",
            .Width = 70,
            .ReadOnly = True
        })

        dgvAccounts.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "CreatedAt",
            .HeaderText = "Created Date",
            .DataPropertyName = "CreatedAt",
            .Width = 130,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        })
    End Sub

    Private Sub ConfigureFormsToAccessDataGridView()
        dgvFormsToAccess.AutoGenerateColumns = False
        dgvFormsToAccess.Columns.Clear()
        dgvFormsToAccess.AllowUserToAddRows = False
        dgvFormsToAccess.AllowUserToDeleteRows = False
        dgvFormsToAccess.AllowUserToResizeRows = False
        dgvFormsToAccess.RowHeadersVisible = False
        dgvFormsToAccess.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFormsToAccess.MultiSelect = False
        dgvFormsToAccess.EditMode = DataGridViewEditMode.EditOnEnter

        dgvFormsToAccess.BackgroundColor = Color.FromArgb(220, 220, 220)
        dgvFormsToAccess.GridColor = Color.FromArgb(180, 180, 180)
        dgvFormsToAccess.ColumnHeadersHeight = 35
        dgvFormsToAccess.RowTemplate.Height = 30

        dgvFormsToAccess.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        dgvFormsToAccess.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvFormsToAccess.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
        dgvFormsToAccess.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgvFormsToAccess.DefaultCellStyle.Font = New Font("Arial", 10)
        dgvFormsToAccess.DefaultCellStyle.ForeColor = Color.Black
        dgvFormsToAccess.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dgvFormsToAccess.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        dgvFormsToAccess.DefaultCellStyle.SelectionForeColor = Color.Black

        dgvFormsToAccess.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FormID",
            .DataPropertyName = "FormID",
            .Visible = False
        })

        dgvFormsToAccess.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FormName",
            .HeaderText = "Form / Module",
            .DataPropertyName = "FormName",
            .ReadOnly = True,
            .Width = 350,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        })

        dgvFormsToAccess.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "CanView",
            .HeaderText = "View",
            .DataPropertyName = "CanView",
            .Width = 110
        })

        dgvFormsToAccess.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "CanAdd",
            .HeaderText = "Add",
            .DataPropertyName = "CanAdd",
            .Width = 110
        })

        dgvFormsToAccess.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "CanEdit",
            .HeaderText = "Edit",
            .DataPropertyName = "CanEdit",
            .Width = 110
        })

        dgvFormsToAccess.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "CanDelete",
            .HeaderText = "Delete / Archive",
            .DataPropertyName = "CanDelete",
            .Width = 150
        })

        AddHandler dgvFormsToAccess.CurrentCellDirtyStateChanged, AddressOf dgvFormsToAccess_CurrentCellDirtyStateChanged
        AddHandler dgvFormsToAccess.CellValueChanged, AddressOf dgvFormsToAccess_CellValueChanged
    End Sub

    Private Sub LoadFormsToAccessGrid()
        Try
            Dim permissionTable As New DataTable()
            permissionTable.Columns.Add("FormID", GetType(Integer))
            permissionTable.Columns.Add("FormName", GetType(String))
            permissionTable.Columns.Add("CanView", GetType(Boolean))
            permissionTable.Columns.Add("CanAdd", GetType(Boolean))
            permissionTable.Columns.Add("CanEdit", GetType(Boolean))
            permissionTable.Columns.Add("CanDelete", GetType(Boolean))

            If allFormsData IsNot Nothing Then
                For Each row As DataRow In allFormsData.Rows
                    permissionTable.Rows.Add(
                        CInt(row("FormID")),
                        row("FormName").ToString(),
                        False,
                        False,
                        False,
                        False
                    )
                Next
            End If

            dgvFormsToAccess.DataSource = permissionTable

        Catch ex As Exception
            MsgBox("Error loading form permissions: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub LoadAllAdminAccounts()
        Try
            dgvAccounts.DataSource = accountLogic.GetAllAdminAccounts()
        Catch ex As Exception
            MsgBox("Error loading accounts: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadAllAdminAccounts()
            Else
                dgvAccounts.DataSource = accountLogic.SearchAdminAccounts(txtSearch.Text.Trim())
            End If
        Catch ex As Exception
            MsgBox("Error searching accounts: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim selectedRole As String = If(cbRole.SelectedItem Is Nothing, "", cbRole.SelectedItem.ToString())

        Dim validationError As String = accountLogic.ValidateAccountData(
            txtFname.Text,
            txtLname.Text,
            txtUname.Text,
            txtPass.Text,
            selectedRole,
            True
        )

        If Not String.IsNullOrEmpty(validationError) Then
            MsgBox(validationError, MsgBoxStyle.Information, "Validation")
            Return
        End If

        Dim selectedPermissions As List(Of ManageAllAccountsLogic.FormPermissionData) = GetSelectedPermissionsFromGrid()

        Try
            Dim newUserId As Integer = accountLogic.AddAdminAccount(
                txtFname.Text.Trim(),
                txtLname.Text.Trim(),
                txtUname.Text.Trim(),
                txtPass.Text,
                selectedRole
            )

            If newUserId > 0 Then
                accountLogic.AssignFormPermissionsToUser(newUserId, selectedPermissions, LogInForm.CurrentUserID)

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

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If selectedUserId = -1 Then
            MsgBox("Please select an account to edit.", MsgBoxStyle.Information, "Selection Required")
            Return
        End If

        Dim selectedRole As String = If(cbRole.SelectedItem Is Nothing, "", cbRole.SelectedItem.ToString())

        Dim validationError As String = accountLogic.ValidateAccountData(
            txtFname.Text,
            txtLname.Text,
            txtUname.Text,
            txtPass.Text,
            selectedRole,
            False
        )

        If Not String.IsNullOrEmpty(validationError) Then
            MsgBox(validationError, MsgBoxStyle.Information, "Validation")
            Return
        End If

        Dim selectedPermissions As List(Of ManageAllAccountsLogic.FormPermissionData) = GetSelectedPermissionsFromGrid()

        Try
            If accountLogic.UpdateAdminAccount(
                selectedUserId,
                txtFname.Text.Trim(),
                txtLname.Text.Trim(),
                txtUname.Text.Trim(),
                txtPass.Text,
                selectedRole
            ) Then
                accountLogic.AssignFormPermissionsToUser(selectedUserId, selectedPermissions, LogInForm.CurrentUserID)

                MsgBox("Account updated successfully!", MsgBoxStyle.Information, "Success")
                ClearForm()
                LoadAllAdminAccounts()
            Else
                MsgBox("Failed to update account. Username may already exist.", MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnArchieve_Click(sender As Object, e As EventArgs) Handles btnArchieve.Click
        If selectedUserId = -1 Then
            MsgBox("Please select an account to archive.", MsgBoxStyle.Information, "Selection Required")
            Return
        End If

        If MsgBox("Archive this account? The user will not be able to login.", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
            Try
                If accountLogic.ArchiveAccount(selectedUserId) Then
                    MsgBox("Account archived successfully!", MsgBoxStyle.Information, "Success")
                    ClearForm()
                    LoadAllAdminAccounts()
                Else
                    MsgBox("Failed to archive account.", MsgBoxStyle.Exclamation, "Error")
                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub dgvAccounts_SelectionChanged(sender As Object, e As EventArgs)
        If dgvAccounts.SelectedRows.Count > 0 Then
            LoadSelectedAccountData(dgvAccounts.SelectedRows(0))
        End If
    End Sub

    Private Sub dgvAccounts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Exit Sub

        dgvAccounts.ClearSelection()
        dgvAccounts.Rows(e.RowIndex).Selected = True
        LoadSelectedAccountData(dgvAccounts.Rows(e.RowIndex))
    End Sub

    Private Sub LoadSelectedAccountData(selectedRow As DataGridViewRow)
        selectedUserId = CInt(selectedRow.Cells("UserId").Value)

        txtFname.Text = selectedRow.Cells("FirstName").Value.ToString()
        txtLname.Text = selectedRow.Cells("LastName").Value.ToString()
        txtUname.Text = selectedRow.Cells("Username").Value.ToString()
        cbRole.SelectedItem = selectedRow.Cells("Role").Value.ToString()

        ' Leave password blank during edit.
        ' If blank, password will not be changed.
        txtPass.Clear()

        LoadUserPermissionsToGrid(selectedUserId)

        isEditMode = True
    End Sub

    Private Function GetSelectedPermissionsFromGrid() As List(Of ManageAllAccountsLogic.FormPermissionData)
        Dim permissions As New List(Of ManageAllAccountsLogic.FormPermissionData)

        dgvFormsToAccess.EndEdit()

        For Each row As DataGridViewRow In dgvFormsToAccess.Rows
            If row.IsNewRow Then Continue For

            Dim canView As Boolean = GetBooleanCellValue(row, "CanView")
            Dim canAdd As Boolean = GetBooleanCellValue(row, "CanAdd")
            Dim canEdit As Boolean = GetBooleanCellValue(row, "CanEdit")
            Dim canDelete As Boolean = GetBooleanCellValue(row, "CanDelete")

            If canAdd OrElse canEdit OrElse canDelete Then
                canView = True
            End If

            If canView OrElse canAdd OrElse canEdit OrElse canDelete Then
                permissions.Add(New ManageAllAccountsLogic.FormPermissionData With {
                    .FormID = CInt(row.Cells("FormID").Value),
                    .canView = canView,
                    .canAdd = canAdd,
                    .canEdit = canEdit,
                    .canDelete = canDelete
                })
            End If
        Next

        Return permissions
    End Function

    Private Sub LoadUserPermissionsToGrid(userId As Integer)
        Try
            Dim savedPermissions As Dictionary(Of Integer, ManageAllAccountsLogic.FormPermissionData) =
                accountLogic.GetUserPermissionRows(userId)

            For Each row As DataGridViewRow In dgvFormsToAccess.Rows
                If row.IsNewRow Then Continue For

                Dim formId As Integer = CInt(row.Cells("FormID").Value)

                row.Cells("CanView").Value = False
                row.Cells("CanAdd").Value = False
                row.Cells("CanEdit").Value = False
                row.Cells("CanDelete").Value = False

                If savedPermissions.ContainsKey(formId) Then
                    Dim permission = savedPermissions(formId)

                    row.Cells("CanView").Value = permission.CanView
                    row.Cells("CanAdd").Value = permission.CanAdd
                    row.Cells("CanEdit").Value = permission.CanEdit
                    row.Cells("CanDelete").Value = permission.CanDelete
                End If
            Next

        Catch ex As Exception
            MsgBox("Error loading user permissions: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ClearPermissionGrid()
        For Each row As DataGridViewRow In dgvFormsToAccess.Rows
            If row.IsNewRow Then Continue For

            row.Cells("CanView").Value = False
            row.Cells("CanAdd").Value = False
            row.Cells("CanEdit").Value = False
            row.Cells("CanDelete").Value = False
        Next
    End Sub

    Private Sub dgvFormsToAccess_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs)
        If dgvFormsToAccess.IsCurrentCellDirty Then
            dgvFormsToAccess.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub dgvFormsToAccess_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return

        Dim columnName As String = dgvFormsToAccess.Columns(e.ColumnIndex).Name
        Dim row As DataGridViewRow = dgvFormsToAccess.Rows(e.RowIndex)

        If columnName = "CanAdd" OrElse columnName = "CanEdit" OrElse columnName = "CanDelete" Then
            If GetBooleanCellValue(row, columnName) Then
                row.Cells("CanView").Value = True
            End If
        End If

        If columnName = "CanView" AndAlso Not GetBooleanCellValue(row, "CanView") Then
            row.Cells("CanAdd").Value = False
            row.Cells("CanEdit").Value = False
            row.Cells("CanDelete").Value = False
        End If
    End Sub

    Private Function GetBooleanCellValue(row As DataGridViewRow, columnName As String) As Boolean
        If row.Cells(columnName).Value Is Nothing OrElse IsDBNull(row.Cells(columnName).Value) Then
            Return False
        End If

        Return CBool(row.Cells(columnName).Value)
    End Function

    Private Sub ClearForm()
        txtFname.Clear()
        txtLname.Clear()
        txtUname.Clear()
        txtPass.Clear()

        If cbRole.Items.Count > 1 Then
            cbRole.SelectedIndex = 1
        End If

        ClearPermissionGrid()

        selectedUserId = -1
        isEditMode = False
    End Sub

    Private Sub ApplyGradient(pnl As Control, startColorHex As String, endColorHex As String)
        Try
            Dim startColor = ColorTranslator.FromHtml(startColorHex)
            Dim endColor = ColorTranslator.FromHtml(endColorHex)

            AddHandler pnl.Paint,
                Sub(sender, e)
                    Using brush As New LinearGradientBrush(New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
                        e.Graphics.FillRectangle(brush, pnl.ClientRectangle)
                    End Using
                End Sub

        Catch ex As Exception
            Debug.WriteLine("ApplyGradient Error: " & ex.Message)
        End Try
    End Sub

    Private Sub RoundButtonCorners(btn As Button, radius As Integer)
        Try
            If btn Is Nothing Then Return

            ApplyButtonRoundedRegion(btn, radius)

            AddHandler btn.Resize,
                Sub(s, args)
                    ApplyButtonRoundedRegion(btn, radius)
                End Sub

        Catch ex As Exception
            Debug.WriteLine("RoundButtonCorners Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ApplyButtonRoundedRegion(btn As Button, radius As Integer)
        Try
            If btn Is Nothing Then Return
            If btn.Width <= 0 OrElse btn.Height <= 0 Then Return

            Using p As New GraphicsPath()
                p.AddArc(0, 0, radius, radius, 180, 90)
                p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
                p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
                p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
                p.CloseFigure()

                btn.Region = New Region(p)
            End Using

        Catch ex As Exception
            Debug.WriteLine("ApplyButtonRoundedRegion Error: " & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        Try
            If responsiveManager IsNot Nothing Then
                responsiveManager.Cleanup()
            End If

            MyBase.OnFormClosing(e)

        Catch ex As Exception
            Debug.WriteLine("OnFormClosing Error: " & ex.Message)
        End Try
    End Sub

End Class
