Imports System.Drawing.Drawing2D

Public Class HouseholdMain_Form
    ' === Service Layer (Business Logic) ===
    Private householdLogic As New HouseholdMainLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As HouseholdMainResponsiveManager

    ' === UI State ===
    Private selectedHouseholdId As Integer = -1
    Private selectedActionType As String = "" ' Track which action is clicked

    Private Sub HouseholdMain_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        UIUtilities.ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling - Using UIUtilities ===
        UIUtilities.RoundButtonCorners(btnSearch, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New HouseholdMainResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Configure DataGridView ===
        ConfigureDataGridView()

        ' === Load All Households ===
        LoadAllHouseholds()
    End Sub

    ''' <summary>
    ''' Configure DataGridView with modern styling and single Action column
    ''' </summary>
    Private Sub ConfigureDataGridView()
        ' === Apply standard styling using DataGridViewHelper ===
        DataGridViewHelper.ApplyStandardStyling(dgvHouseholdInfo)

        ' === Configure advanced properties ===
        dgvHouseholdInfo.AllowUserToResizeRows = False
        dgvHouseholdInfo.EnableHeadersVisualStyles = False
        dgvHouseholdInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHouseholdInfo.MultiSelect = False

        ' === Modern styling overrides ===
        With dgvHouseholdInfo
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .GridColor = Color.FromArgb(220, 220, 220)
            .ColumnHeadersHeight = 40
            .RowTemplate.Height = 45
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        End With

        ' === Column Headers Styling ===
        With dgvHouseholdInfo.ColumnHeadersDefaultCellStyle
            .BackColor = Color.FromArgb(60, 137, 66)
            .ForeColor = Color.White
            .Font = New Font("Arial", 11, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .Padding = New Padding(5)
            .SelectionBackColor = Color.FromArgb(60, 137, 66)
        End With

        ' === Row Styling ===
        With dgvHouseholdInfo.DefaultCellStyle
            .Font = New Font("Arial", 10, FontStyle.Regular)
            .ForeColor = Color.FromArgb(64, 64, 64)
            .BackColor = Color.White
            .Alignment = DataGridViewContentAlignment.MiddleLeft
            .Padding = New Padding(5, 0, 5, 0)
            .SelectionBackColor = Color.FromArgb(200, 230, 201)
            .SelectionForeColor = Color.FromArgb(64, 64, 64)
        End With

        ' === Alternate Row Color ===
        With dgvHouseholdInfo.AlternatingRowsDefaultCellStyle
            .BackColor = Color.FromArgb(248, 248, 248)
            .SelectionBackColor = Color.FromArgb(200, 230, 201)
        End With

        ' === ADD DATA COLUMNS USING DataGridViewHelper ===
        ' Hidden ID Column
        DataGridViewHelper.AddTextColumn(dgvHouseholdInfo, "HouseholdID", "", "HouseholdID", 0, True)
        dgvHouseholdInfo.Columns("HouseholdID").Visible = False

        ' Household Number
        DataGridViewHelper.AddTextColumn(dgvHouseholdInfo, "HouseholdNumber", "Household #", "HouseholdNumber", 100, True)

        ' Block Number
        DataGridViewHelper.AddTextColumn(dgvHouseholdInfo, "BlockNumber", "Block", "BlockNumber", 70, True)

        ' Lot Number
        DataGridViewHelper.AddTextColumn(dgvHouseholdInfo, "LotNumber", "Lot", "LotNumber", 70, True)

        ' Street Name
        DataGridViewHelper.AddTextColumn(dgvHouseholdInfo, "StreetName", "Street", "StreetName", 140, True)

        ' Barangay
        DataGridViewHelper.AddTextColumn(dgvHouseholdInfo, "Barangay", "Barangay", "Barangay", 110, True)

        ' Municipality
        DataGridViewHelper.AddTextColumn(dgvHouseholdInfo, "Municipality", "Municipality", "Municipality", 120, True)

        ' === SINGLE ACTION COLUMN with all actions ===
        Dim actionColumn As New DataGridViewButtonColumn With {
            .Name = "Actions",
            .HeaderText = "Actions",
            .Width = 220,
            .FlatStyle = FlatStyle.Flat,
            .Text = "▼ Select Action"
        }
        actionColumn.DefaultCellStyle.BackColor = Color.FromArgb(100, 181, 246)
        actionColumn.DefaultCellStyle.ForeColor = Color.White
        actionColumn.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
        actionColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvHouseholdInfo.Columns.Add(actionColumn)

        ' === ADD CELLCLICK EVENT ===
        AddHandler dgvHouseholdInfo.CellClick, AddressOf dgvHouseholdInfo_CellClick
        AddHandler dgvHouseholdInfo.CellMouseEnter, AddressOf dgvHouseholdInfo_CellMouseEnter
        AddHandler dgvHouseholdInfo.CellMouseLeave, AddressOf dgvHouseholdInfo_CellMouseLeave
    End Sub

    ''' <summary>
    ''' Load all households
    ''' </summary>
    Private Sub LoadAllHouseholds()
        Try
            Dim dataTable As DataTable = householdLogic.GetAllHouseholds()
            dgvHouseholdInfo.DataSource = dataTable
            AutoAdjustColumnWidths()
        Catch ex As Exception
            MsgBox("Error loading households: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Auto-adjust column widths to fit properly
    ''' </summary>
    Private Sub AutoAdjustColumnWidths()
        Try
            Dim totalWidth As Integer = 0
            For Each col In dgvHouseholdInfo.Columns
                If col.Visible Then
                    totalWidth += col.Width
                End If
            Next

            ' Distribute remaining space to StreetName column
            If totalWidth < dgvHouseholdInfo.Width Then
                Dim remainingWidth As Integer = dgvHouseholdInfo.Width - totalWidth - 25
                If remainingWidth > 0 AndAlso dgvHouseholdInfo.Columns.Contains(dgvHouseholdInfo.Columns("StreetName")) Then
                    dgvHouseholdInfo.Columns("StreetName").Width += remainingWidth
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine("Error adjusting column widths: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Search button click
    ''' </summary>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadAllHouseholds()
            Else
                Dim dataTable As DataTable = householdLogic.SearchHouseholds(txtSearch.Text)
                dgvHouseholdInfo.DataSource = dataTable
                AutoAdjustColumnWidths()
            End If
        Catch ex As Exception
            MsgBox("Error searching households: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' DataGridView cell click event - Show action menu
    ''' </summary>
    Private Sub dgvHouseholdInfo_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHouseholdInfo.CellClick
        Try
            If e.RowIndex < 0 Then Return

            ' === Get Household ID ===
            selectedHouseholdId = CInt(dgvHouseholdInfo.Rows(e.RowIndex).Cells("HouseholdID").Value)

            ' === If Actions column clicked, show context menu ===
            If e.ColumnIndex = dgvHouseholdInfo.Columns("Actions").Index Then
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

            ' === Edit Option ===
            Dim editItem As New ToolStripMenuItem("✏️ Edit Household")
            editItem.BackColor = Color.FromArgb(240, 240, 240)
            AddHandler editItem.Click, Sub() HandleEditClick()
            contextMenu.Items.Add(editItem)

            ' === View Family Option ===
            Dim viewItem As New ToolStripMenuItem("👥 View Family")
            viewItem.BackColor = Color.FromArgb(240, 240, 240)
            AddHandler viewItem.Click, Sub() HandleViewFamilyClick()
            contextMenu.Items.Add(viewItem)

            ' === Add Family Option ===
            Dim addItem As New ToolStripMenuItem("➕ Add Family")
            addItem.BackColor = Color.FromArgb(240, 240, 240)
            AddHandler addItem.Click, Sub() HandleAddFamilyClick()
            contextMenu.Items.Add(addItem)

            ' === Separator ===
            contextMenu.Items.Add(New ToolStripSeparator())

            ' === Archive Option ===
            Dim archiveItem As New ToolStripMenuItem("🗑️ Archive")
            archiveItem.BackColor = Color.FromArgb(255, 200, 200)
            archiveItem.ForeColor = Color.FromArgb(244, 67, 54)
            AddHandler archiveItem.Click, Sub() HandleArchiveClick()
            contextMenu.Items.Add(archiveItem)

            ' === Show menu at mouse position ===
            contextMenu.Show(dgvHouseholdInfo, dgvHouseholdInfo.PointToClient(MousePosition))

        Catch ex As Exception
            MsgBox("Error showing menu: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Highlight button on mouse enter
    ''' </summary>
    Private Sub dgvHouseholdInfo_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHouseholdInfo.CellMouseEnter
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                Dim cell = dgvHouseholdInfo.Rows(e.RowIndex).Cells(e.ColumnIndex)
                If e.ColumnIndex = dgvHouseholdInfo.Columns("Actions").Index Then
                    ' Highlight action button
                    cell.Style.BackColor = Color.FromArgb(66, 165, 245)
                    dgvHouseholdInfo.Cursor = Cursors.Hand
                End If
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Reset button color on mouse leave
    ''' </summary>
    Private Sub dgvHouseholdInfo_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHouseholdInfo.CellMouseLeave
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                If e.ColumnIndex = dgvHouseholdInfo.Columns("Actions").Index Then
                    dgvHouseholdInfo.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.FromArgb(100, 181, 246)
                    dgvHouseholdInfo.Cursor = Cursors.Default
                End If
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Handle Edit button click
    ''' </summary>
    Private Sub HandleEditClick()
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdEditForm As New HouseholdEdit_Form(selectedHouseholdId)
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdEditForm)
            End If
        Catch ex As Exception
            MsgBox("Error loading edit form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Handle View Family button click
    ''' </summary>
    Private Sub HandleViewFamilyClick()
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdViewFamilyForm As New HouseholdViewFamily_Form(selectedHouseholdId)
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdViewFamilyForm)
            End If
        Catch ex As Exception
            MsgBox("Error loading view family form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Handle Add Family button click
    ''' </summary>
    Private Sub HandleAddFamilyClick()
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdAddFamilyForm As New HouseholdAddNewFamily_Form(selectedHouseholdId)
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdAddFamilyForm)
            End If
        Catch ex As Exception
            MsgBox("Error loading add family form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Handle Archive button click
    ''' </summary>
    Private Sub HandleArchiveClick()
        Try
            Dim householdNumber As String = dgvHouseholdInfo.Rows.Cast(Of DataGridViewRow)().FirstOrDefault(Function(r) CInt(r.Cells("HouseholdID").Value) = selectedHouseholdId)?.Cells("HouseholdNumber").Value.ToString()

            If MsgBox("Archive Household #" & householdNumber & "? This action cannot be undone.", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.No Then
                Return
            End If

            Dim result As HouseholdMainLogic.HouseholdResult = householdLogic.ArchiveHousehold(selectedHouseholdId)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                LoadAllHouseholds()
                selectedHouseholdId = -1
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