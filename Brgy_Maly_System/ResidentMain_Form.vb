Imports System.Drawing.Drawing2D

Public Class ResidentMain_Form
    Private Const RESIDENT_FORM_CLASS As String = "ResidentMain_Form"

    Private residentLogic As New ResidentMainLogic()
    Private responsiveManager As ResidentMainResponsiveManager
    Private selectedResidentId As Integer = -1

    Private Sub ResidentMain_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UIUtilities.ApplyGradient(fillpanel, "#EDFFE9", "#FFFFFF")
        UIUtilities.RoundButtonCorners(btnSearch, 20)
        UIUtilities.RoundButtonCorners(btnAddNewResident, 20)

        responsiveManager = New ResidentMainResponsiveManager(Me)
        responsiveManager.Initialize()

        ConfigureDataGridView()
        LoadAllResidents()
        ApplyPermissions()
    End Sub

    Private Sub ApplyPermissions()
        btnAddNewResident.Visible = LogInForm.CanAdd(RESIDENT_FORM_CLASS)
    End Sub

    Private Sub ConfigureDataGridView()
        DataGridViewHelper.ApplyStandardStyling(dgvResidents)

        dgvResidents.AllowUserToResizeRows = False
        dgvResidents.EnableHeadersVisualStyles = False
        dgvResidents.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvResidents.MultiSelect = False

        With dgvResidents
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .GridColor = Color.FromArgb(220, 220, 220)
            .ColumnHeadersHeight = 40
            .RowTemplate.Height = 40
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        End With

        With dgvResidents.ColumnHeadersDefaultCellStyle
            .BackColor = Color.FromArgb(60, 137, 66)
            .ForeColor = Color.White
            .Font = New Font("Arial", 11, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .Padding = New Padding(5)
            .SelectionBackColor = Color.FromArgb(60, 137, 66)
        End With

        With dgvResidents.DefaultCellStyle
            .Font = New Font("Arial", 10, FontStyle.Regular)
            .ForeColor = Color.FromArgb(64, 64, 64)
            .BackColor = Color.White
            .Alignment = DataGridViewContentAlignment.MiddleLeft
            .Padding = New Padding(5, 0, 5, 0)
            .SelectionBackColor = Color.FromArgb(200, 230, 201)
            .SelectionForeColor = Color.FromArgb(64, 64, 64)
        End With

        With dgvResidents.AlternatingRowsDefaultCellStyle
            .BackColor = Color.FromArgb(248, 248, 248)
            .SelectionBackColor = Color.FromArgb(200, 230, 201)
        End With

        DataGridViewHelper.AddTextColumn(dgvResidents, "ResidentId", "", "ResidentId", 0, True)
        dgvResidents.Columns("ResidentId").Visible = False

        DataGridViewHelper.AddTextColumn(dgvResidents, "FirstName", "First Name", "FirstName", 110, True)
        DataGridViewHelper.AddTextColumn(dgvResidents, "LastName", "Last Name", "LastName", 110, True)
        DataGridViewHelper.AddTextColumn(dgvResidents, "Sex", "Gender", "Sex", 80, True)
        DataGridViewHelper.AddTextColumn(dgvResidents, "CivilStatus", "Civil Status", "CivilStatus", 100, True)
        DataGridViewHelper.AddTextColumn(dgvResidents, "ContactNumber", "Contact #", "ContactNumber", 120, True)
        DataGridViewHelper.AddTextColumn(dgvResidents, "HouseholdNumber", "Household #", "HouseholdNumber", 110, True)

        Dim actionColumn As New DataGridViewButtonColumn With {
            .Name = "Actions",
            .HeaderText = "Actions",
            .Width = 180,
            .FlatStyle = FlatStyle.Flat,
            .Text = "Select Action",
            .UseColumnTextForButtonValue = True
        }

        actionColumn.DefaultCellStyle.BackColor = Color.FromArgb(100, 181, 246)
        actionColumn.DefaultCellStyle.ForeColor = Color.White
        actionColumn.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
        actionColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvResidents.Columns.Add(actionColumn)

        AddHandler dgvResidents.CellClick, AddressOf dgvResidents_CellClick
        AddHandler dgvResidents.CellMouseEnter, AddressOf dgvResidents_CellMouseEnter
        AddHandler dgvResidents.CellMouseLeave, AddressOf dgvResidents_CellMouseLeave
    End Sub

    Private Sub LoadAllResidents()
        Try
            Dim dataTable As DataTable = residentLogic.GetAllResidents()
            dgvResidents.DataSource = dataTable
            AutoAdjustColumnWidths()
        Catch ex As Exception
            MsgBox("Error loading residents: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub AutoAdjustColumnWidths()
        Try
            Dim totalWidth As Integer = 0

            For Each col As DataGridViewColumn In dgvResidents.Columns
                If col.Visible Then
                    totalWidth += col.Width
                End If
            Next

            If totalWidth < dgvResidents.Width AndAlso dgvResidents.Columns.Contains("FirstName") Then
                Dim remainingWidth As Integer = dgvResidents.Width - totalWidth - 25

                If remainingWidth > 0 Then
                    dgvResidents.Columns("FirstName").Width += remainingWidth
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine("Error adjusting column widths: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAddNewResident_Click(sender As Object, e As EventArgs) Handles btnAddNewResident.Click
        Try
            If Not LogInForm.CanAdd(RESIDENT_FORM_CLASS) Then
                MsgBox("You do not have permission to add residents.", MsgBoxStyle.Exclamation, "Access Denied")
                Return
            End If

            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim residentAddingForm As New ResidentAdding_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(residentAddingForm)
            End If

        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadAllResidents()
            Else
                Dim dataTable As DataTable = residentLogic.SearchResidents(txtSearch.Text.Trim())
                dgvResidents.DataSource = dataTable
                AutoAdjustColumnWidths()
            End If

        Catch ex As Exception
            MsgBox("Error searching residents: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub dgvResidents_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidents.CellClick
        Try
            If e.RowIndex < 0 Then Return

            selectedResidentId = CInt(dgvResidents.Rows(e.RowIndex).Cells("ResidentId").Value)

            If e.ColumnIndex = dgvResidents.Columns("Actions").Index Then
                ShowActionMenu(e)
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ShowActionMenu(e As DataGridViewCellEventArgs)
        Try
            Dim contextMenu As New ContextMenuStrip()
            contextMenu.Font = New Font("Arial", 10, FontStyle.Regular)
            contextMenu.BackColor = Color.White
            contextMenu.ForeColor = Color.Black

            Dim viewItem As New ToolStripMenuItem("View Details")
            viewItem.BackColor = Color.FromArgb(240, 240, 240)
            AddHandler viewItem.Click, Sub() HandleViewClick()
            contextMenu.Items.Add(viewItem)

            If LogInForm.CanEdit(RESIDENT_FORM_CLASS) Then
                Dim updateItem As New ToolStripMenuItem("Update Resident")
                updateItem.BackColor = Color.FromArgb(240, 240, 240)
                AddHandler updateItem.Click, Sub() HandleUpdateClick()
                contextMenu.Items.Add(updateItem)
            End If

            If LogInForm.CanDelete(RESIDENT_FORM_CLASS) Then
                contextMenu.Items.Add(New ToolStripSeparator())

                Dim archiveItem As New ToolStripMenuItem("Archive")
                archiveItem.BackColor = Color.FromArgb(255, 200, 200)
                archiveItem.ForeColor = Color.FromArgb(244, 67, 54)
                AddHandler archiveItem.Click, Sub() HandleArchiveClick()
                contextMenu.Items.Add(archiveItem)
            End If

            contextMenu.Show(dgvResidents, dgvResidents.PointToClient(MousePosition))

        Catch ex As Exception
            MsgBox("Error showing menu: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub dgvResidents_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidents.CellMouseEnter
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                If e.ColumnIndex = dgvResidents.Columns("Actions").Index Then
                    dgvResidents.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.FromArgb(66, 165, 245)
                    dgvResidents.Cursor = Cursors.Hand
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub dgvResidents_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidents.CellMouseLeave
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                If e.ColumnIndex = dgvResidents.Columns("Actions").Index Then
                    dgvResidents.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.FromArgb(100, 181, 246)
                    dgvResidents.Cursor = Cursors.Default
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub HandleViewClick()
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim residentAddingForm As New ResidentAdding_Form(selectedResidentId, True)
                Dashboard_Layout.CurrentInstance.LoadContentPanel(residentAddingForm)
            End If

        Catch ex As Exception
            MsgBox("Error loading view form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub HandleUpdateClick()
        Try
            If Not LogInForm.CanEdit(RESIDENT_FORM_CLASS) Then
                MsgBox("You do not have permission to edit residents.", MsgBoxStyle.Exclamation, "Access Denied")
                Return
            End If

            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim residentAddingForm As New ResidentAdding_Form(selectedResidentId, False)
                Dashboard_Layout.CurrentInstance.LoadContentPanel(residentAddingForm)
            End If

        Catch ex As Exception
            MsgBox("Error loading update form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub HandleArchiveClick()
        Try
            If Not LogInForm.CanDelete(RESIDENT_FORM_CLASS) Then
                MsgBox("You do not have permission to archive residents.", MsgBoxStyle.Exclamation, "Access Denied")
                Return
            End If

            Dim residentName As String = dgvResidents.SelectedRows(0).Cells("FirstName").Value.ToString() & " " &
                                        dgvResidents.SelectedRows(0).Cells("LastName").Value.ToString()

            If MsgBox("Archive " & residentName & "? This action cannot be undone.", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.No Then
                Return
            End If

            Dim result As ResidentMainLogic.ResidentResult = residentLogic.ArchiveResident(selectedResidentId)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                LoadAllResidents()
                selectedResidentId = -1
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If

        MyBase.OnFormClosing(e)
    End Sub

End Class
