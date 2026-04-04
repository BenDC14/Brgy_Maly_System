Imports System.Drawing.Drawing2D

Public Class ResidentMain_Form
    ' === Service Layer (Business Logic) ===
    Private residentLogic As New ResidentMainLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As ResidentMainResponsiveManager

    ' === UI State ===
    Private selectedResidentId As Integer = -1

    Private Sub ResidentMain_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        UIUtilities.ApplyGradient(fillpanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling - Using UIUtilities ===
        UIUtilities.RoundButtonCorners(btnSearch, 20)
        UIUtilities.RoundButtonCorners(btnAddNewResident, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New ResidentMainResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Configure DataGridView ===
        ConfigureDataGridView()

        ' === Load All Residents ===
        LoadAllResidents()
    End Sub

    ''' <summary>
    ''' Configure DataGridView with modern styling using DataGridViewHelper and single Action column
    ''' </summary>
    Private Sub ConfigureDataGridView()
        ' === Apply standard styling using DataGridViewHelper ===
        DataGridViewHelper.ApplyStandardStyling(dgvResidents)

        ' === Configure advanced properties ===
        dgvResidents.AllowUserToResizeRows = False
        dgvResidents.EnableHeadersVisualStyles = False
        dgvResidents.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvResidents.MultiSelect = False

        ' === Modern styling overrides ===
        With dgvResidents
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .GridColor = Color.FromArgb(220, 220, 220)
            .ColumnHeadersHeight = 40
            .RowTemplate.Height = 40
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        End With

        ' === Column Headers Styling ===
        With dgvResidents.ColumnHeadersDefaultCellStyle
            .BackColor = Color.FromArgb(60, 137, 66)
            .ForeColor = Color.White
            .Font = New Font("Arial", 11, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .Padding = New Padding(5)
            .SelectionBackColor = Color.FromArgb(60, 137, 66)
        End With

        ' === Row Styling ===
        With dgvResidents.DefaultCellStyle
            .Font = New Font("Arial", 10, FontStyle.Regular)
            .ForeColor = Color.FromArgb(64, 64, 64)
            .BackColor = Color.White
            .Alignment = DataGridViewContentAlignment.MiddleLeft
            .Padding = New Padding(5, 0, 5, 0)
            .SelectionBackColor = Color.FromArgb(200, 230, 201)
            .SelectionForeColor = Color.FromArgb(64, 64, 64)
        End With

        ' === Alternate Row Color ===
        With dgvResidents.AlternatingRowsDefaultCellStyle
            .BackColor = Color.FromArgb(248, 248, 248)
            .SelectionBackColor = Color.FromArgb(200, 230, 201)
        End With

        ' === ADD DATA COLUMNS USING DataGridViewHelper ===
        ' Hidden ID Column
        DataGridViewHelper.AddTextColumn(dgvResidents, "ResidentId", "", "ResidentId", 0, True)
        dgvResidents.Columns("ResidentId").Visible = False

        ' First Name
        DataGridViewHelper.AddTextColumn(dgvResidents, "FirstName", "First Name", "FirstName", 110, True)

        ' Last Name
        DataGridViewHelper.AddTextColumn(dgvResidents, "LastName", "Last Name", "LastName", 110, True)

        ' Sex/Gender
        DataGridViewHelper.AddTextColumn(dgvResidents, "Sex", "Gender", "Sex", 80, True)

        ' Civil Status
        DataGridViewHelper.AddTextColumn(dgvResidents, "CivilStatus", "Civil Status", "CivilStatus", 100, True)

        ' Contact Number
        DataGridViewHelper.AddTextColumn(dgvResidents, "ContactNumber", "Contact #", "ContactNumber", 120, True)

        ' Household Number
        DataGridViewHelper.AddTextColumn(dgvResidents, "HouseholdNumber", "Household #", "HouseholdNumber", 110, True)

        ' === SINGLE ACTION COLUMN with all actions ===
        Dim actionColumn As New DataGridViewButtonColumn With {
            .Name = "Actions",
            .HeaderText = "Actions",
            .Width = 180,
            .FlatStyle = FlatStyle.Flat,
            .Text = "▼ Select Action"
        }
        actionColumn.DefaultCellStyle.BackColor = Color.FromArgb(100, 181, 246)
        actionColumn.DefaultCellStyle.ForeColor = Color.White
        actionColumn.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
        actionColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvResidents.Columns.Add(actionColumn)

        ' === ADD CELLCLICK EVENT ===
        AddHandler dgvResidents.CellClick, AddressOf dgvResidents_CellClick
        AddHandler dgvResidents.CellMouseEnter, AddressOf dgvResidents_CellMouseEnter
        AddHandler dgvResidents.CellMouseLeave, AddressOf dgvResidents_CellMouseLeave
    End Sub

    ''' <summary>
    ''' Load all residents
    ''' </summary>
    Private Sub LoadAllResidents()
        Try
            Dim dataTable As DataTable = residentLogic.GetAllResidents()
            dgvResidents.DataSource = dataTable
            AutoAdjustColumnWidths()
        Catch ex As Exception
            MsgBox("Error loading residents: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Auto-adjust column widths to fit properly
    ''' </summary>
    Private Sub AutoAdjustColumnWidths()
        Try
            Dim totalWidth As Integer = 0
            For Each col In dgvResidents.Columns
                If col.Visible Then
                    totalWidth += col.Width
                End If
            Next

            ' Distribute remaining space to FirstName column
            If totalWidth < dgvResidents.Width Then
                Dim remainingWidth As Integer = dgvResidents.Width - totalWidth - 25
                If remainingWidth > 0 AndAlso dgvResidents.Columns.Contains(dgvResidents.Columns("FirstName")) Then
                    dgvResidents.Columns("FirstName").Width += remainingWidth
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine("Error adjusting column widths: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Add New Resident button click
    ''' </summary>
    Private Sub btnAddNewResident_Click(sender As Object, e As EventArgs) Handles btnAddNewResident.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim residentAddingForm As New ResidentAdding_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(residentAddingForm)
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Search button click
    ''' </summary>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadAllResidents()
            Else
                Dim dataTable As DataTable = residentLogic.SearchResidents(txtSearch.Text)
                dgvResidents.DataSource = dataTable
                AutoAdjustColumnWidths()
            End If
        Catch ex As Exception
            MsgBox("Error searching residents: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' DataGridView cell click event - Show action menu
    ''' </summary>
    Private Sub dgvResidents_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidents.CellClick
        Try
            If e.RowIndex < 0 Then Return

            ' === Get Resident ID ===
            selectedResidentId = CInt(dgvResidents.Rows(e.RowIndex).Cells("ResidentId").Value)

            ' === If Actions column clicked, show context menu ===
            If e.ColumnIndex = dgvResidents.Columns("Actions").Index Then
                ShowActionMenu(e)
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Show context menu with action options
    ''' </summary>
    Private Sub ShowActionMenu(e As DataGridViewCellEventArgs)
        Try
            ' === Create Context Menu ===
            Dim contextMenu As New ContextMenuStrip()
            contextMenu.Font = New Font("Arial", 10, FontStyle.Regular)
            contextMenu.BackColor = Color.White
            contextMenu.ForeColor = Color.Black

            ' === View Option ===
            Dim viewItem As New ToolStripMenuItem("👁️ View Details")
            viewItem.BackColor = Color.FromArgb(240, 240, 240)
            AddHandler viewItem.Click, Sub() HandleViewClick()
            contextMenu.Items.Add(viewItem)

            ' === Update Option ===
            Dim updateItem As New ToolStripMenuItem("✏️ Update Resident")
            updateItem.BackColor = Color.FromArgb(240, 240, 240)
            AddHandler updateItem.Click, Sub() HandleUpdateClick()
            contextMenu.Items.Add(updateItem)

            ' === Separator ===
            contextMenu.Items.Add(New ToolStripSeparator())

            ' === Archive Option ===
            Dim archiveItem As New ToolStripMenuItem("🗑️ Archive")
            archiveItem.BackColor = Color.FromArgb(255, 200, 200)
            archiveItem.ForeColor = Color.FromArgb(244, 67, 54)
            AddHandler archiveItem.Click, Sub() HandleArchiveClick()
            contextMenu.Items.Add(archiveItem)

            ' === Show menu at mouse position ===
            contextMenu.Show(dgvResidents, dgvResidents.PointToClient(MousePosition))

        Catch ex As Exception
            MsgBox("Error showing menu: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Highlight button on mouse enter
    ''' </summary>
    Private Sub dgvResidents_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidents.CellMouseEnter
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                Dim cell = dgvResidents.Rows(e.RowIndex).Cells(e.ColumnIndex)
                If e.ColumnIndex = dgvResidents.Columns("Actions").Index Then
                    ' Highlight action button
                    cell.Style.BackColor = Color.FromArgb(66, 165, 245)
                    dgvResidents.Cursor = Cursors.Hand
                End If
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Reset button color on mouse leave
    ''' </summary>
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

    ''' <summary>
    ''' Handle View button click - Open in view-only mode
    ''' </summary>
    Private Sub HandleViewClick()
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim residentAddingForm As New ResidentAdding_Form(selectedResidentId, True) ' True = View Only
                Dashboard_Layout.CurrentInstance.LoadContentPanel(residentAddingForm)
            End If
        Catch ex As Exception
            MsgBox("Error loading view form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Handle Update button click - Open in edit mode
    ''' </summary>
    Private Sub HandleUpdateClick()
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim residentAddingForm As New ResidentAdding_Form(selectedResidentId, False) ' False = Edit Mode
                Dashboard_Layout.CurrentInstance.LoadContentPanel(residentAddingForm)
            End If
        Catch ex As Exception
            MsgBox("Error loading update form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Handle Archive button click
    ''' </summary>
    Private Sub HandleArchiveClick()
        Try
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

    ''' <summary>
    ''' Cleanup when form closes
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If
        MyBase.OnFormClosing(e)
    End Sub

End Class