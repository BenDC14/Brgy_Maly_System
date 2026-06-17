Imports System.Data
Imports System.Drawing.Drawing2D

''' <summary>
''' Form used for recording ayuda claims.
''' Workflow:
''' 1. User selects resident type/category.
''' 2. System loads only available ayuda programs for that category.
''' 3. User selects a specific ayuda.
''' 4. System loads only residents under that category who have not claimed that ayuda.
''' UPDATED: Residents remain visible and Claim button disabled if limit is reached.
''' UPDATED: Selection preserved after successful claim to allow continuous recording.
''' </summary>
Public Class AyudaRecording_Form

    ' Handles database and business logic.
    Private recordingLogic As New AyudaRecordingLogic()

    ' Handles responsive layout.
    Private responsiveManager As AyudaRecordingResponsiveManager

    ' Stores currently selected ayuda and category.
    Private currentAidId As Integer = -1
    Private currentCategoryId As Integer = -1

    ' MODIFIED: Tracks if current ayuda is full.
    Private currentAyudaIsFull As Boolean = False

    ' Prevents ComboBox events from running while the form is still loading.
    Private isInitializing As Boolean = True

    ' Prevents cbAyuda event from running while cbAyuda is being rebound.
    Private isLoadingAyudas As Boolean = False

    Private Sub AyudaRecording_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")
            RoundButtonCorners(btnBackToMainPage, 20)

            responsiveManager = New AyudaRecordingResponsiveManager(Me)
            responsiveManager.Initialize()

            ConfigureResidentsDataGridView()

            ' Load resident categories first.
            ' Ayuda programs are not loaded yet.
            LoadCategories()

            ' Keep ayuda list and resident grid empty until user selects a resident type.
            cbAyuda.DataSource = Nothing
            dgvResidents.DataSource = Nothing
            txtResidentType.Clear()
            txtQuantity.Clear()
            DescriptionRtxt.Clear()

            isInitializing = False

        Catch ex As Exception
            MsgBox("Error initializing form: " & ex.Message, MsgBoxStyle.Critical, "Initialization Error")
            Debug.WriteLine("AyudaRecording_Form_Load Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Builds and styles the DataGridView columns.
    ''' The Claim button is added as the last column.
    ''' </summary>
    Private Sub ConfigureResidentsDataGridView()
        Try
            dgvResidents.AutoGenerateColumns = False
            dgvResidents.Columns.Clear()
            dgvResidents.ReadOnly = False
            dgvResidents.AllowUserToAddRows = False
            dgvResidents.AllowUserToDeleteRows = False
            dgvResidents.RowHeadersVisible = False
            dgvResidents.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvResidents.MultiSelect = False

            dgvResidents.BackgroundColor = Color.FromArgb(220, 220, 220)
            dgvResidents.GridColor = Color.FromArgb(180, 180, 180)
            dgvResidents.ColumnHeadersHeight = 35
            dgvResidents.RowTemplate.Height = 30

            dgvResidents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
            dgvResidents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvResidents.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            dgvResidents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            dgvResidents.DefaultCellStyle.Font = New Font("Arial", 10)
            dgvResidents.DefaultCellStyle.ForeColor = Color.Black
            dgvResidents.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
            dgvResidents.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgvResidents.DefaultCellStyle.WrapMode = DataGridViewTriState.False
            dgvResidents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)

            dgvResidents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)

            dgvResidents.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "ResidentId",
                .DataPropertyName = "ResidentId",
                .HeaderText = "ResidentId",
                .Visible = False,
                .Width = 0
            })

            dgvResidents.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "FullName",
                .DataPropertyName = "FullName",
                .HeaderText = "Full Name",
                .Width = 300,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                .ReadOnly = True
            })

            dgvResidents.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "ContactNumber",
                .DataPropertyName = "ContactNumber",
                .HeaderText = "Contact",
                .Width = 150,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                .ReadOnly = True
            })

            dgvResidents.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "Category",
                .DataPropertyName = "Category",
                .HeaderText = "Category",
                .Width = 150,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                .ReadOnly = True
            })

            ' MODIFIED: UseColumnTextForButtonValue set to False to allow dynamic "Limit Reached" text.
            Dim claimButtonColumn As New DataGridViewButtonColumn With {
                .Name = "Action",
                .HeaderText = "Action",
                .Text = "Claim",
                .UseColumnTextForButtonValue = False,
                .Width = 100,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                .ReadOnly = False
            }

            dgvResidents.Columns.Add(claimButtonColumn)

        Catch ex As Exception
            MsgBox("Error configuring DataGridView: " & ex.Message, MsgBoxStyle.Critical, "Configuration Error")
            Debug.WriteLine("ConfigureResidentsDataGridView Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Loads all resident categories into cbResidentType.
    ''' </summary>
    Private Sub LoadCategories()
        Try
            Dim dataTable As DataTable = recordingLogic.GetAllCategories()

            cbResidentType.DataSource = Nothing
            cbResidentType.DisplayMember = "Category"
            cbResidentType.ValueMember = "CategoryId"

            If dataTable.Rows.Count > 0 Then
                cbResidentType.DataSource = dataTable
                cbResidentType.SelectedIndex = -1
            Else
                MsgBox("No categories found.", MsgBoxStyle.Information, "No Data")
            End If

        Catch ex As Exception
            MsgBox("Error loading categories: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadCategories Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Runs when user selects a resident type.
    ''' This only loads the matching ayuda programs.
    ''' It does not load residents yet.
    ''' </summary>
    Private Sub cbResidentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbResidentType.SelectedIndexChanged
        Try
            If isInitializing Then Return

            dgvResidents.DataSource = Nothing
            cbAyuda.DataSource = Nothing
            txtQuantity.Clear()
            DescriptionRtxt.Clear()
            currentAidId = -1
            currentCategoryId = -1

            Dim selectedCategoryId As Integer = GetSelectedComboBoxIntegerValue(cbResidentType)
            If selectedCategoryId <= 0 Then Return

            currentCategoryId = selectedCategoryId

            Dim categoryName As String = recordingLogic.GetCategoryNameById(currentCategoryId)
            txtResidentType.Text = categoryName

            ' Load only ayuda programs intended for the selected resident type.
            LoadAyudaProgramsByCategory(currentCategoryId)

        Catch ex As Exception
            Debug.WriteLine("cbResidentType_SelectedIndexChanged Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Loads only active, not expired, not full ayuda programs for the selected category.
    ''' UPDATED: Logic to preserve current selection during refresh.
    ''' </summary>
    Private Sub LoadAyudaProgramsByCategory(categoryId As Integer)
        Try
            isLoadingAyudas = True

            ' Store current aid ID to re-select it after reload
            Dim savedAidId As Integer = currentAidId

            Dim dataTable As DataTable = recordingLogic.GetAyudaProgramsByCategory(categoryId)

            cbAyuda.DataSource = Nothing
            cbAyuda.DisplayMember = "DisplayText"
            cbAyuda.ValueMember = "AidId"

            If dataTable.Rows.Count > 0 Then
                cbAyuda.DataSource = dataTable

                ' Re-select previous ayuda if it exists
                If savedAidId > 0 Then
                    cbAyuda.SelectedValue = savedAidId
                Else
                    cbAyuda.SelectedIndex = -1
                End If
            Else
                dgvResidents.DataSource = Nothing
                ' Only show message if we are not in the middle of a continuous claim process
                If savedAidId <= 0 Then
                    MsgBox("No available ayuda programs found for this resident type.", MsgBoxStyle.Information, "No Ayuda Available")
                End If
            End If

        Catch ex As Exception
            MsgBox("Error loading ayuda programs: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadAyudaProgramsByCategory Error: " & ex.Message)
        Finally
            isLoadingAyudas = False
        End Try
    End Sub

    ''' <summary>
    ''' Runs when user selects a specific ayuda.
    ''' Residents are loaded only after both resident type and ayuda are selected.
    ''' </summary>
    Private Sub cbAyuda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAyuda.SelectedIndexChanged
        Try
            If isInitializing OrElse isLoadingAyudas Then Return

            dgvResidents.DataSource = Nothing
            ' Removed txtQuantity.Clear() to prevent clearing during continuous claims
            currentAidId = -1
            currentAyudaIsFull = False ' Reset state

            If currentCategoryId <= 0 Then Return

            Dim selectedAidId As Integer = GetSelectedComboBoxIntegerValue(cbAyuda)
            If selectedAidId <= 0 Then Return

            currentAidId = selectedAidId

            ' MODIFIED: Check if current ayuda is full and show MsgBox.
            Dim drv As DataRowView = DirectCast(cbAyuda.SelectedItem, DataRowView)
            If drv IsNot Nothing Then
                Dim maxSlots As Integer = CInt(drv("max_slots"))
                If maxSlots <= 0 Then
                    currentAyudaIsFull = True
                    MsgBox("The limit is reached. Following resident's cannot get the specific ayuda", MsgBoxStyle.Information, "Limit Reached")
                End If
            End If

            Dim provisionDetails As String = recordingLogic.GetAyudaProvisionDetails(currentAidId)
            txtQuantity.Text = provisionDetails

            ' Now load only qualified residents who have not claimed this ayuda.
            LoadResidentsByCategory(currentCategoryId, currentAidId)

        Catch ex As Exception
            Debug.WriteLine("cbAyuda_SelectedIndexChanged Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Loads residents under the selected category who have not claimed the selected ayuda.
    ''' </summary>
    Private Sub LoadResidentsByCategory(categoryId As Integer, aidId As Integer)
        Try
            If categoryId <= 0 OrElse aidId <= 0 Then
                dgvResidents.DataSource = Nothing
                Return
            End If

            Dim dataTable As DataTable = recordingLogic.GetResidentsByCategory(categoryId, aidId)

            If dataTable.Rows.Count > 0 Then
                dgvResidents.DataSource = dataTable
            Else
                dgvResidents.DataSource = Nothing
                ' Removed MsgBox here to prevent annoyance during continuous claims
            End If

        Catch ex As Exception
            MsgBox("Error loading residents: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadResidentsByCategory Error: " & ex.Message)
        End Try
    End Sub

    ' MODIFIED: Added DataBindingComplete to change button text to "Limit Reached"
    Private Sub dgvResidents_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvResidents.DataBindingComplete
        Try
            For Each row As DataGridViewRow In dgvResidents.Rows
                Dim btnCell As DataGridViewButtonCell = DirectCast(row.Cells("Action"), DataGridViewButtonCell)
                If currentAyudaIsFull Then
                    btnCell.Value = "Limit Reached"
                    btnCell.Style.ForeColor = Color.Gray
                Else
                    btnCell.Value = "Claim"
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine("dgvResidents_DataBindingComplete Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Handles Claim button click inside the DataGridView.
    ''' UPDATED: Logic to preserve ayuda selection and quantity after success.
    ''' </summary>
    Private Sub dgvResidents_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidents.CellClick
        Try
            If e.RowIndex < 0 Then Return
            If dgvResidents.Columns("Action") Is Nothing Then Return
            If e.ColumnIndex <> dgvResidents.Columns("Action").Index Then Return

            ' MODIFIED: Block click and show message if full.
            If currentAyudaIsFull Then
                MsgBox("the limit is reached following resident's cannot get the specific ayuda", MsgBoxStyle.Exclamation, "Cannot Process")
                Return
            End If

            If currentCategoryId <= 0 Then
                MsgBox("Please select a resident type first.", MsgBoxStyle.Exclamation, "Validation Error")
                Return
            End If

            If currentAidId <= 0 Then
                MsgBox("Please select an ayuda first.", MsgBoxStyle.Exclamation, "Validation Error")
                Return
            End If

            If LogInForm.CurrentUserID <= 0 Then
                MsgBox("Error: No user logged in. Please login first.", MsgBoxStyle.Critical, "Authentication Error")
                Return
            End If

            Dim selectedRow As DataGridViewRow = dgvResidents.Rows(e.RowIndex)
            Dim residentId As Integer = CInt(selectedRow.Cells("ResidentId").Value)
            Dim fullName As String = selectedRow.Cells("FullName").Value.ToString()

            ' Quantity is extracted from provision_details.
            Dim quantity As Integer = recordingLogic.ExtractQuantityFromProvisionDetails(txtQuantity.Text)

            Dim confirmResult As Integer = MsgBox("Record ayuda claim for " & fullName & "?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm Claim")
            If confirmResult <> vbYes Then Return

            Dim recordingData As New AyudaRecordingLogic.ResidentRecordingData With {
                .ResidentId = residentId,
                .AidId = currentAidId,
                .AidDate = AyudaDateDTP.Value,
                .Quantity = quantity,
                .Description = DescriptionRtxt.Text.Trim()
            }

            Dim result As AyudaRecordingLogic.RecordingResult = recordingLogic.RecordResidentAidClaim(recordingData)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")

                ' === REFRESH BACKGROUND DATA WITHOUT CLEARING SELECTION ===
                ' 1. Refresh residents (the one who claimed will be removed from list)
                LoadResidentsByCategory(currentCategoryId, currentAidId)

                ' 2. Refresh ayuda list to update slot count display in ComboBox
                LoadAyudaProgramsByCategory(currentCategoryId)

                ' 3. Description is usually specific to a claim, so it is okay to clear
                DescriptionRtxt.Clear()

                ' NOTE: cbAyuda and txtQuantity are NOT cleared here.
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Claim Failed")

                ' Refresh lists in case the ayuda became full or expired.
                LoadAyudaProgramsByCategory(currentCategoryId)
                dgvResidents.DataSource = Nothing
            End If

        Catch ex As Exception
            MsgBox("Error recording claim: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("dgvResidents_CellClick Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Safely gets an Integer value from a bound ComboBox.
    ''' This avoids errors when SelectedValue is temporarily a DataRowView.
    ''' </summary>
    Private Function GetSelectedComboBoxIntegerValue(comboBox As ComboBox) As Integer
        Try
            If comboBox Is Nothing OrElse comboBox.SelectedValue Is Nothing Then Return -1

            If TypeOf comboBox.SelectedValue Is DataRowView Then Return -1

            Dim selectedId As Integer
            If Integer.TryParse(comboBox.SelectedValue.ToString(), selectedId) Then
                Return selectedId
            End If

            Return -1

        Catch ex As Exception
            Debug.WriteLine("GetSelectedComboBoxIntegerValue Error: " & ex.Message)
            Return -1
        End Try
    End Function

    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
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

    ''' <summary>
    ''' Apply rounded corners to button (with resize handler)
    ''' UPDATED: Corrected 'ByRef' Parameter error for Lambda expressions.
    ''' </summary>
    Private Sub RoundButtonCorners(ByVal btn As Button, ByVal radius As Integer)
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
    ''' Helper to apply rounded region to a control.
    ''' </summary>
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

    ''' <summary>
    ''' Navigation back to main page.
    ''' </summary>
    Private Sub btnBackToMainPage_Click(sender As Object, e As EventArgs) Handles btnBackToMainPage.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim ayudaMainForm As New AyudaMain_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(ayudaMainForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnBackToMainPage_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Form closing cleanup.
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