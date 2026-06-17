Imports System.Drawing.Drawing2D

''' <summary>
''' Form used for viewing and managing main Ayuda records.
''' Follows the standard Barangay Maly layout with a DataGridView and search functionality.
''' Matches the ResidentMain_Form pattern for action buttons and context menus.
''' </summary>
Public Class AyudaMain_Form
    ' === Service Layer (Business Logic) ===
    Private ayudaLogic As New AyudaMainLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As AyudaMainResponsiveManager

    ' === UI State ===
    Private selectedResidentAidId As Integer = -1

    Private Sub AyudaMain_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' === Apply Gradient ===
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            ' === Apply Button Styling (Once - never reapply) ===
            RoundButtonCorners(btnRecordNewAyuda, 20)
            RoundButtonCorners(btnSearch, 20)
            RoundButtonCorners(btnAudit, 20)

            ' === Initialize Responsive Manager ===
            responsiveManager = New AyudaMainResponsiveManager(Me)
            responsiveManager.Initialize()

            ' === Configure DataGridView ===
            ConfigureDataGridView()

            ' === Load Initial Data ===
            LoadAllResidentAids()

        Catch ex As Exception
            MsgBox("Error initializing form: " & ex.Message, MsgBoxStyle.Critical, "Initialization Error")
            Debug.WriteLine("AyudaMain_Form_Load Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Configures the columns and styling for the Resident Ayuda DataGridView.
    ''' Matches ResidentMain pattern with a single Action Button column and full-width layout.
    ''' </summary>
    Private Sub ConfigureDataGridView()
        Try
            ' === Apply standard styling using DataGridViewHelper ===
            DataGridViewHelper.ApplyStandardStyling(dgvResidentAyudas)

            dgvResidentAyudas.AllowUserToResizeRows = False
            dgvResidentAyudas.EnableHeadersVisualStyles = False
            dgvResidentAyudas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvResidentAyudas.MultiSelect = False

            With dgvResidentAyudas
                .BackgroundColor = Color.White
                .BorderStyle = BorderStyle.None
                .GridColor = Color.FromArgb(220, 220, 220)
                .ColumnHeadersHeight = 45
                .RowTemplate.Height = 40
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With

            ' Clear existing columns to prevent duplicates
            dgvResidentAyudas.Columns.Clear()

            ' 1. ID Column (Hidden)
            DataGridViewHelper.AddTextColumn(dgvResidentAyudas, "ResidentAidId", "", "ResidentAidId", 0, True)
            dgvResidentAyudas.Columns("ResidentAidId").Visible = False

            ' 2. Resident Name
            DataGridViewHelper.AddTextColumn(dgvResidentAyudas, "ResidentName", "Resident Full Name", "ResidentName", 200, True)

            ' 3. Program Title
            DataGridViewHelper.AddTextColumn(dgvResidentAyudas, "ProgramTitle", "Ayuda Program Title", "ProgramTitle", 200, True)

            ' 4. Assistance Type
            DataGridViewHelper.AddTextColumn(dgvResidentAyudas, "AssistanceType", "Assistance Type", "AssistanceType", 110, True)

            ' 5. Quantity Distributed
            DataGridViewHelper.AddTextColumn(dgvResidentAyudas, "Quantity", "Quantity Distributed", "Quantity", 130, True)

            ' 6. Date Received - Includes Timestamp
            DataGridViewHelper.AddTextColumn(dgvResidentAyudas, "AidDate", "Date and Time Distributed", "AidDate", 180, True)
            dgvResidentAyudas.Columns("AidDate").DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt"

            ' 7. Remarks
            DataGridViewHelper.AddTextColumn(dgvResidentAyudas, "Description", "Remarks/Notes", "Description", 150, True)

            ' === SINGLE ACTION COLUMN (Matches ResidentMain) ===
            Dim actionColumn As New DataGridViewButtonColumn With {
                .Name = "Actions",
                .HeaderText = "Actions",
                .Width = 150,
                .FlatStyle = FlatStyle.Flat,
                .Text = "Select Action",
                .UseColumnTextForButtonValue = True
            }

            actionColumn.DefaultCellStyle.BackColor = Color.FromArgb(100, 181, 246)
            actionColumn.DefaultCellStyle.ForeColor = Color.White
            actionColumn.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
            actionColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvResidentAyudas.Columns.Add(actionColumn)

            ' Attach standard event handlers
            AddHandler dgvResidentAyudas.CellClick, AddressOf dgvResidentAyudas_CellClick
            AddHandler dgvResidentAyudas.CellMouseEnter, AddressOf dgvResidentAyudas_CellMouseEnter
            AddHandler dgvResidentAyudas.CellMouseLeave, AddressOf dgvResidentAyudas_CellMouseLeave

        Catch ex As Exception
            Debug.WriteLine("ConfigureDataGridView Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Loads all resident aid records from the database.
    ''' </summary>
    Private Sub LoadAllResidentAids()
        Try
            Dim dataTable As DataTable = ayudaLogic.GetAllResidentAids()
            dgvResidentAyudas.DataSource = dataTable
        Catch ex As Exception
            MsgBox("Error loading records: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadAllResidentAids Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Handle DataGridView CellClick to trigger Action Menu
    ''' </summary>
    Private Sub dgvResidentAyudas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidentAyudas.CellClick
        Try
            ' Ignore header clicks
            If e.RowIndex < 0 Then Return

            ' Store selected ID from the row
            selectedResidentAidId = CInt(dgvResidentAyudas.Rows(e.RowIndex).Cells("ResidentAidId").Value)

            ' If the Action button was clicked, show the context menu
            If e.ColumnIndex = dgvResidentAyudas.Columns("Actions").Index Then
                ShowActionMenu(e)
            End If

        Catch ex As Exception
            MsgBox("Error handling cell click: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Shows the Action Menu (View, Update, Archive) at the mouse position.
    ''' </summary>
    Private Sub ShowActionMenu(e As DataGridViewCellEventArgs)
        Try
            Dim contextMenu As New ContextMenuStrip()
            contextMenu.Font = New Font("Arial", 10, FontStyle.Regular)
            contextMenu.BackColor = Color.White
            contextMenu.ForeColor = Color.Black

            ' 1. View Details
            Dim viewItem As New ToolStripMenuItem("View Details")
            AddHandler viewItem.Click, Sub() HandleViewClick()
            contextMenu.Items.Add(viewItem)

            ' 2. Update Entry
            Dim updateItem As New ToolStripMenuItem("Update Record")
            AddHandler updateItem.Click, Sub() HandleUpdateClick()
            contextMenu.Items.Add(updateItem)

            ' 3. Archive/Delete (Red color like ResidentMain)
            contextMenu.Items.Add(New ToolStripSeparator())
            Dim archiveItem As New ToolStripMenuItem("Archive")
            archiveItem.BackColor = Color.FromArgb(255, 200, 200)
            archiveItem.ForeColor = Color.FromArgb(244, 67, 54)
            AddHandler archiveItem.Click, Sub() HandleArchiveClick()
            contextMenu.Items.Add(archiveItem)

            ' Show menu relative to the current mouse position
            contextMenu.Show(dgvResidentAyudas, dgvResidentAyudas.PointToClient(MousePosition))

        Catch ex As Exception
            Debug.WriteLine("ShowActionMenu Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Handle View logic - To be implemented
    ''' </summary>
    Private Sub HandleViewClick()
        MsgBox("Detailed view for Aid ID: " & selectedResidentAidId & " is coming soon.", MsgBoxStyle.Information, "View Info")
    End Sub

    ''' <summary>
    ''' Handle Update logic - Navigate to Edit form and pass the selected record ID
    ''' </summary>
    Private Sub HandleUpdateClick()
        Try
            If selectedResidentAidId <= 0 Then
                MsgBox("Please select a record first.", MsgBoxStyle.Exclamation, "No Selection")
                Return
            End If

            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim editAyudaForm As New AyudaEditRecording_Form()
                editAyudaForm.TargetResidentAidId = selectedResidentAidId
                Dashboard_Layout.CurrentInstance.LoadContentPanel(editAyudaForm)
            End If
        Catch ex As Exception
            MsgBox("Error loading update form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    ''' <summary>
    ''' Handle Archive logic - Confirmation and DB update
    ''' </summary>
    Private Sub HandleArchiveClick()
        Try
            Dim resName As String = dgvResidentAyudas.CurrentRow.Cells("ResidentName").Value.ToString()

            If MsgBox("Are you sure you want to archive record for " & resName & "?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm Archive") = MsgBoxResult.Yes Then
                Dim result = ayudaLogic.ArchiveResidentAid(selectedResidentAidId)
                If result.IsSuccess Then
                    MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                    LoadAllResidentAids()
                Else
                    MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
                End If
            End If
        Catch ex As Exception
            MsgBox("Error during archive process: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Search button click handler.
    ''' </summary>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim searchTerm As String = txtSearch.Text.Trim()
            If String.IsNullOrEmpty(searchTerm) Then
                LoadAllResidentAids()
            Else
                Dim searchResults As DataTable = ayudaLogic.SearchResidentAids(searchTerm)
                dgvResidentAyudas.DataSource = searchResults
            End If
        Catch ex As Exception
            MsgBox("Search error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' === MOUSE HOVER EFFECTS (Matches ResidentMain) ===

    Private Sub dgvResidentAyudas_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = dgvResidentAyudas.Columns("Actions").Index Then
            dgvResidentAyudas.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.FromArgb(66, 165, 245)
            dgvResidentAyudas.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub dgvResidentAyudas_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = dgvResidentAyudas.Columns("Actions").Index Then
            dgvResidentAyudas.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.FromArgb(100, 181, 246)
            dgvResidentAyudas.Cursor = Cursors.Default
        End If
    End Sub

    ' === GRADIENT AND ROUNDING HELPERS ===

    ''' <summary>
    ''' Apply gradient background to panel
    ''' </summary>
    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Try
            Dim startColor = ColorTranslator.FromHtml(startColorHex)
            Dim endColor = ColorTranslator.FromHtml(endColorHex)
            Dim brush As New LinearGradientBrush(New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
            Dim panelLocal = pnl
            AddHandler panelLocal.Paint, Sub(sender, e)
                                             e.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                                         End Sub
        Catch ex As Exception
            Debug.WriteLine("ApplyGradient Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Apply rounded corners to button (with resize handler)
    ''' </summary>
    Private Sub RoundButtonCorners(ByVal btn As Button, ByVal radius As Integer)
        Try
            If btn Is Nothing Then Return

            ' Create local reference to fix the 'ByRef' Lambda error
            Dim btnLocal = btn
            ApplyButtonRoundedRegion(btnLocal, radius)

            AddHandler btnLocal.Resize, Sub(s, args)
                                            ApplyButtonRoundedRegion(btnLocal, radius)
                                        End Sub

        Catch ex As Exception
            Debug.WriteLine("RoundButtonCorners Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Applies the actual rounded region to the control.
    ''' </summary>
    Private Sub ApplyButtonRoundedRegion(btn As Button, radius As Integer)
        Try
            If btn Is Nothing OrElse btn.Width <= 0 OrElse btn.Height <= 0 Then Return

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

    ''' <summary>
    ''' Cleanup when form closes
    ''' </summary>
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

    ' === BUTTON NAVIGATION ===

    Private Sub btnRecordNewAyuda_Click(sender As Object, e As EventArgs) Handles btnRecordNewAyuda.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim recordAyudaProgramForm As New AyudaRecording_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(recordAyudaProgramForm)
            End If
        Catch ex As Exception
            Debug.WriteLine("btnRecordNewAyuda_Click Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAudit_Click(sender As Object, e As EventArgs) Handles btnAudit.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim auditForm As New AyudaAudit_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(auditForm)
            End If
        Catch ex As Exception
            Debug.WriteLine("btnAudit_Click Error: " & ex.Message)
        End Try
    End Sub

End Class