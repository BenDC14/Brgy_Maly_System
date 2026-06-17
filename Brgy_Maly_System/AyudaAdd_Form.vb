Imports System.Data
Imports System.Drawing.Drawing2D

''' <summary>
''' Form for adding and editing barangay ayuda programs.
''' This form loads categories, displays ayuda records, and saves selected category to barangayaid.CategoryId.
''' </summary>
Public Class AyudaAdd_Form

    ' Handles all database/business logic for this form.
    Private ayudaLogic As New AyudaAddLogic()

    ' Handles resizing and repositioning of controls.
    Private responsiveManager As AyudaAddResponsiveUIManager

    ' Stores the AidId of the row currently being edited.
    ' -1 means no selected row for editing.
    Private currentEditingAidId As Integer = -1

    ''' <summary>
    ''' Runs when the form opens.
    ''' Initializes UI styling, responsive layout, ComboBoxes, and DataGridView.
    ''' </summary>
    Private Sub AyudaAdd_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            RoundButtonCorners(btnSave, 20)
            RoundButtonCorners(btnEdit, 20)

            responsiveManager = New AyudaAddResponsiveUIManager(Me)
            responsiveManager.Initialize()

            ConfigureAyudasDGV()
            SetupAssistanceTypeComboBox()
            LoadCategories()
            LoadAllAyudas()

            rbIsActiveYes.Checked = True
            btnEdit.Enabled = False
            txtOthers.Enabled = False

        Catch ex As Exception
            MsgBox("Error initializing form: " & ex.Message, MsgBoxStyle.Critical, "Initialization Error")
            Debug.WriteLine("AyudaAdd_Form_Load Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Loads categories from the categories table into cbcategories.
    ''' DisplayMember is what the user sees.
    ''' ValueMember is the hidden CategoryId saved to barangayaid.
    ''' </summary>
    Private Sub LoadCategories()
        Try
            Dim dataTable As DataTable = ayudaLogic.GetAllCategories()

            cbcategories.DataSource = Nothing
            cbcategories.Items.Clear()
            cbcategories.DropDownStyle = ComboBoxStyle.DropDownList
            cbcategories.DisplayMember = "Category"
            cbcategories.ValueMember = "CategoryId"

            If dataTable.Rows.Count > 0 Then
                cbcategories.DataSource = dataTable
                cbcategories.SelectedIndex = -1
            End If

        Catch ex As Exception
            MsgBox("Error loading categories: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadCategories Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Sets predefined assistance types.
    ''' If user selects Others, txtOthers becomes enabled.
    ''' </summary>
    Private Sub SetupAssistanceTypeComboBox()
        Try
            cbAssistanceType.Items.Clear()
            cbAssistanceType.Items.Add("Cash Assistance")
            cbAssistanceType.Items.Add("Food Assistance")
            cbAssistanceType.Items.Add("Medical Assistance")
            cbAssistanceType.Items.Add("Educational Assistance")
            cbAssistanceType.Items.Add("Livelihood Assistance")
            cbAssistanceType.Items.Add("Shelter Assistance")
            cbAssistanceType.Items.Add("Others")
            cbAssistanceType.DropDownStyle = ComboBoxStyle.DropDownList

        Catch ex As Exception
            Debug.WriteLine("SetupAssistanceTypeComboBox Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Enables txtOthers only when Assistance Type is Others.
    ''' </summary>
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAssistanceType.SelectedIndexChanged
        Try
            If cbAssistanceType.SelectedItem Is Nothing Then Return

            Dim selectedValue As String = cbAssistanceType.SelectedItem.ToString()

            If selectedValue.Equals("Others", StringComparison.OrdinalIgnoreCase) Then
                txtOthers.Enabled = True
                txtOthers.Clear()
                txtOthers.Focus()
            Else
                txtOthers.Enabled = False
                txtOthers.Clear()
            End If

        Catch ex As Exception
            Debug.WriteLine("ComboBox1_SelectedIndexChanged Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Builds the DataGridView columns manually.
    ''' CategoryId is hidden.
    ''' Category is visible so users see the resident category name.
    ''' </summary>
    Private Sub ConfigureAyudasDGV()
        Try
            dgvAyudas.AutoGenerateColumns = False
            dgvAyudas.Columns.Clear()
            dgvAyudas.ReadOnly = True
            dgvAyudas.AllowUserToAddRows = False
            dgvAyudas.AllowUserToDeleteRows = False
            dgvAyudas.RowHeadersVisible = False
            dgvAyudas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvAyudas.MultiSelect = False

            dgvAyudas.BackgroundColor = Color.FromArgb(220, 220, 220)
            dgvAyudas.GridColor = Color.FromArgb(180, 180, 180)
            dgvAyudas.ColumnHeadersHeight = 35
            dgvAyudas.RowTemplate.Height = 30

            dgvAyudas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
            dgvAyudas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvAyudas.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            dgvAyudas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            dgvAyudas.DefaultCellStyle.Font = New Font("Arial", 10)
            dgvAyudas.DefaultCellStyle.ForeColor = Color.Black
            dgvAyudas.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
            dgvAyudas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgvAyudas.DefaultCellStyle.Padding = New Padding(5)
            dgvAyudas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)

            dgvAyudas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "AidId",
                .DataPropertyName = "AidId",
                .HeaderText = "AidId",
                .Width = 0,
                .Visible = False
            })

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "CategoryId",
                .DataPropertyName = "CategoryId",
                .HeaderText = "CategoryId",
                .Width = 0,
                .Visible = False
            })

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "Category",
                .DataPropertyName = "Category",
                .HeaderText = "Resident Category",
                .Width = 160,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "source_agency",
                .DataPropertyName = "source_agency",
                .HeaderText = "Giver",
                .Width = 150,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "program_title",
                .DataPropertyName = "program_title",
                .HeaderText = "Program Name",
                .Width = 200,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "assistance_type",
                .DataPropertyName = "assistance_type",
                .HeaderText = "Assistance Type",
                .Width = 150,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "provision_details",
                .DataPropertyName = "provision_details",
                .HeaderText = "Assistance Provision",
                .Width = 190,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "max_slots",
                .DataPropertyName = "max_slots",
                .HeaderText = "Target Slots",
                .Width = 100,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "start_date",
                .DataPropertyName = "start_date",
                .HeaderText = "Term Start",
                .Width = 110,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudas.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "end_date",
                .DataPropertyName = "end_date",
                .HeaderText = "Term End",
                .Width = 110,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudas.Columns.Add(New DataGridViewCheckBoxColumn With {
                .Name = "is_active",
                .DataPropertyName = "is_active",
                .HeaderText = "Available",
                .Width = 90,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                .ReadOnly = True
            })

        Catch ex As Exception
            MsgBox("Error configuring DataGridView: " & ex.Message, MsgBoxStyle.Critical, "Configuration Error")
            Debug.WriteLine("ConfigureAyudasDGV Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Loads all ayuda records into dgvAyudas.
    ''' </summary>
    Private Sub LoadAllAyudas()
        Try
            Dim dataTable As DataTable = ayudaLogic.GetAllAyudas()
            dgvAyudas.DataSource = dataTable

        Catch ex As Exception
            MsgBox("Error loading ayudas: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadAllAyudas Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Searches ayudas while typing.
    ''' Empty search reloads all records.
    ''' </summary>
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadAllAyudas()
            Else
                dgvAyudas.DataSource = ayudaLogic.SearchAyudas(txtSearch.Text.Trim())
            End If

        Catch ex As Exception
            MsgBox("Error searching ayudas: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("txtSearch_TextChanged Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Double-clicking a row loads the selected ayuda into the form fields for editing.
    ''' </summary>
    Private Sub dgvAyudas_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAyudas.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Return

            Dim selectedRow As DataGridViewRow = dgvAyudas.Rows(e.RowIndex)

            currentEditingAidId = Convert.ToInt32(selectedRow.Cells("AidId").Value)

            If selectedRow.Cells("CategoryId").Value IsNot Nothing AndAlso Not IsDBNull(selectedRow.Cells("CategoryId").Value) Then
                cbcategories.SelectedValue = Convert.ToInt32(selectedRow.Cells("CategoryId").Value)
            Else
                cbcategories.SelectedIndex = -1
            End If

            txtResidentType.Text = selectedRow.Cells("source_agency").Value.ToString()
            txtQuantity.Text = selectedRow.Cells("program_title").Value.ToString()

            Dim assistanceType As String = selectedRow.Cells("assistance_type").Value.ToString()
            txtAssistanceProvision.Text = selectedRow.Cells("provision_details").Value.ToString()
            txtTargetSlots.Text = selectedRow.Cells("max_slots").Value.ToString()
            AyudaStartDTP.Value = CDate(selectedRow.Cells("start_date").Value)
            AyudaEndDTP.Value = CDate(selectedRow.Cells("end_date").Value)

            If cbAssistanceType.Items.Contains(assistanceType) Then
                cbAssistanceType.SelectedItem = assistanceType
                txtOthers.Clear()
                txtOthers.Enabled = False
            Else
                cbAssistanceType.SelectedItem = "Others"
                txtOthers.Text = assistanceType
                txtOthers.Enabled = True
            End If

            Dim isActive As Boolean = Convert.ToBoolean(selectedRow.Cells("is_active").Value)
            rbIsActiveYes.Checked = isActive
            rbIsActiveNo.Checked = Not isActive

            btnEdit.Enabled = True
            btnSave.Text = "Save New"

        Catch ex As Exception
            MsgBox("Error loading ayuda data: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("dgvAyudas_CellDoubleClick Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saves a new ayuda program.
    ''' The selected category is saved to barangayaid.CategoryId.
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not TryParseNumeric(txtTargetSlots.Text) Then
                MsgBox("Target Slots must be a valid number.", MsgBoxStyle.Exclamation, "Validation Error")
                txtTargetSlots.Focus()
                Return
            End If

            Dim assistanceType As String = GetAssistanceType()
            If String.IsNullOrWhiteSpace(assistanceType) Then
                MsgBox("Please select or enter an Assistance Type.", MsgBoxStyle.Exclamation, "Validation Error")
                cbAssistanceType.Focus()
                Return
            End If

            Dim categoryId As Integer = GetSelectedCategoryId()
            If categoryId <= 0 Then
                MsgBox("Please select a Resident Category.", MsgBoxStyle.Exclamation, "Validation Error")
                cbcategories.Focus()
                Return
            End If

            Dim ayudaData As New AyudaAddLogic.AyudaData With {
                .CategoryId = categoryId,
                .source_agency = txtResidentType.Text.Trim(),
                .program_title = txtQuantity.Text.Trim(),
                .assistance_type = assistanceType,
                .provision_details = txtAssistanceProvision.Text.Trim(),
                .max_slots = CInt(txtTargetSlots.Text),
                .start_date = AyudaStartDTP.Value,
                .end_date = AyudaEndDTP.Value,
                .is_active = rbIsActiveYes.Checked
            }

            Dim result As AyudaAddLogic.AyudaResult = ayudaLogic.AddAyuda(ayudaData)

            If result.IsSuccess Then
                MsgBox(result.Message & vbCrLf & "Ayuda ID: " & result.AidId, MsgBoxStyle.Information, "Success")
                ClearAllFields()
                LoadAllAyudas()
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnSave_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Updates the selected ayuda program.
    ''' User must double-click a row first before editing.
    ''' </summary>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            If currentEditingAidId <= 0 Then
                MsgBox("Please select an ayuda to edit by double-clicking a row.", MsgBoxStyle.Information, "Selection Required")
                Return
            End If

            If Not TryParseNumeric(txtTargetSlots.Text) Then
                MsgBox("Target Slots must be a valid number.", MsgBoxStyle.Exclamation, "Validation Error")
                txtTargetSlots.Focus()
                Return
            End If

            Dim assistanceType As String = GetAssistanceType()
            If String.IsNullOrWhiteSpace(assistanceType) Then
                MsgBox("Please select or enter an Assistance Type.", MsgBoxStyle.Exclamation, "Validation Error")
                cbAssistanceType.Focus()
                Return
            End If

            Dim categoryId As Integer = GetSelectedCategoryId()
            If categoryId <= 0 Then
                MsgBox("Please select a Resident Category.", MsgBoxStyle.Exclamation, "Validation Error")
                cbcategories.Focus()
                Return
            End If

            Dim ayudaData As New AyudaAddLogic.AyudaData With {
                .AidId = currentEditingAidId,
                .CategoryId = categoryId,
                .source_agency = txtResidentType.Text.Trim(),
                .program_title = txtQuantity.Text.Trim(),
                .assistance_type = assistanceType,
                .provision_details = txtAssistanceProvision.Text.Trim(),
                .max_slots = CInt(txtTargetSlots.Text),
                .start_date = AyudaStartDTP.Value,
                .end_date = AyudaEndDTP.Value,
                .is_active = rbIsActiveYes.Checked
            }

            Dim result As AyudaAddLogic.AyudaResult = ayudaLogic.UpdateAyuda(ayudaData)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                ClearAllFields()
                LoadAllAyudas()
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnEdit_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Returns the selected assistance type.
    ''' If Others is selected, it returns the text from txtOthers.
    ''' </summary>
    Private Function GetAssistanceType() As String
        Try
            If cbAssistanceType.SelectedItem Is Nothing Then Return ""

            Dim selectedValue As String = cbAssistanceType.SelectedItem.ToString()

            If selectedValue.Equals("Others", StringComparison.OrdinalIgnoreCase) Then
                Return txtOthers.Text.Trim()
            End If

            Return selectedValue

        Catch ex As Exception
            Debug.WriteLine("GetAssistanceType Error: " & ex.Message)
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Gets the hidden CategoryId from cbcategories.
    ''' This is the value saved in barangayaid.CategoryId.
    ''' </summary>
    Private Function GetSelectedCategoryId() As Integer
        Try
            If cbcategories.SelectedValue Is Nothing Then Return -1

            If TypeOf cbcategories.SelectedValue Is DataRowView Then
                Dim rowView As DataRowView = DirectCast(cbcategories.SelectedValue, DataRowView)
                Return Convert.ToInt32(rowView("CategoryId"))
            End If

            Dim categoryId As Integer
            If Integer.TryParse(cbcategories.SelectedValue.ToString(), categoryId) Then
                Return categoryId
            End If

            Return -1

        Catch ex As Exception
            Debug.WriteLine("GetSelectedCategoryId Error: " & ex.Message)
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Clears the form after saving or editing.
    ''' </summary>
    Private Sub ClearAllFields()
        Try
            txtResidentType.Clear()
            txtQuantity.Clear()
            cbcategories.SelectedIndex = -1
            cbAssistanceType.SelectedIndex = -1
            txtOthers.Clear()
            txtOthers.Enabled = False
            txtAssistanceProvision.Clear()
            txtTargetSlots.Clear()
            AyudaStartDTP.Value = Today
            AyudaEndDTP.Value = Today.AddMonths(1)
            rbIsActiveYes.Checked = True
            rbIsActiveNo.Checked = False
            currentEditingAidId = -1
            btnEdit.Enabled = False
            btnSave.Text = "Save"

        Catch ex As Exception
            Debug.WriteLine("ClearAllFields Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Checks if the target slots value is a positive whole number.
    ''' </summary>
    Private Function TryParseNumeric(value As String) As Boolean
        Try
            If String.IsNullOrWhiteSpace(value) Then Return False

            Dim result As Integer
            Return Integer.TryParse(value, result) AndAlso result > 0

        Catch ex As Exception
            Debug.WriteLine("TryParseNumeric Error: " & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Paints a left-to-right gradient background on the panel.
    ''' </summary>
    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Try
            Dim startColor = ColorTranslator.FromHtml(startColorHex)
            Dim endColor = ColorTranslator.FromHtml(endColorHex)

            AddHandler pnl.Paint,
                Sub(sender, e)
                    Using brush As New LinearGradientBrush(
                        New Point(0, 0),
                        New Point(pnl.Width, 0),
                        startColor,
                        endColor
                    )
                        e.Graphics.FillRectangle(brush, pnl.ClientRectangle)
                    End Using
                End Sub

        Catch ex As Exception
            Debug.WriteLine("ApplyGradient Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Makes buttons rounded.
    ''' The region is recalculated when the button resizes.
    ''' </summary>
    Private Sub RoundButtonCorners(ByRef btn As Button, ByVal radius As Integer)
        Try
            If btn Is Nothing Then Return

            Dim btnLocal As Button = btn

            ApplyButtonRoundedRegion(btnLocal, radius)

            AddHandler btnLocal.Resize,
            Sub(s, args)
                ApplyButtonRoundedRegion(btnLocal, radius)
            End Sub

        Catch ex As Exception
            Debug.WriteLine("RoundButtonCorners Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Applies the actual rounded shape to the button.
    ''' This is separated so the resize event can reuse it safely.
    ''' </summary>
    Private Sub ApplyButtonRoundedRegion(btn As Button, radius As Integer)
        Try
            If btn Is Nothing Then Return
            If btn.Width <= 0 OrElse btn.Height <= 0 Then Return

            Dim p As New GraphicsPath()
            p.AddArc(0, 0, radius, radius, 180, 90)
            p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
            p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
            p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
            p.CloseFigure()

            btn.Region = New Region(p)

        Catch ex As Exception
            Debug.WriteLine("ApplyButtonRoundedRegion Error: " & ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Cleans up the responsive manager when the form closes.
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

End Class
