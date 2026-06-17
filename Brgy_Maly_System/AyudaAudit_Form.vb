Imports System.Drawing.Drawing2D

''' <summary>
''' Form used for auditing help distribution.
''' Provides KPI stats and detailed claim history.
''' </summary>
Public Class AyudaAudit_Form
    ' === Service Layer (Business Logic) ===
    Private auditLogic As New AyudaAuditLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As AyudaAuditResponsiveManager

    ' === State Variables ===
    Private isInitializing As Boolean = True
    Private currentAidId As Integer = -1

    Private Sub AyudaAudit_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' === Apply Gradient ===
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            ' === Apply Button Styling ===
            RoundButtonCorners(btnPrintAudit, 20)

            ' === Initialize Responsive Manager ===
            responsiveManager = New AyudaAuditResponsiveManager(Me)
            responsiveManager.Initialize()

            ' === Configure DataGridViews ===
            ConfigureAyudaInformationGrid()
            ConfigureAyudaAuditInformationGrid()

            ' === Set default dates ===
            startDTP.Value = DateTime.Now.AddMonths(-1)
            endDTP.Value = DateTime.Now

            ' === Load Ayuda Programs ===
            LoadAyudaPrograms()

            ' === Set initialization flag ===
            isInitializing = False

            ' === Trigger initial load ===
            If cbAyuda.Items.Count > 0 Then
                cbAyuda.SelectedIndex = 0
            End If

        Catch ex As Exception
            MsgBox("Error initializing form: " & ex.Message, MsgBoxStyle.Critical, "Initialization Error")
            Debug.WriteLine("AyudaAudit_Form_Load Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Configure Ayuda Information DataGridView
    ''' </summary>
    Private Sub ConfigureAyudaInformationGrid()
        Try
            dgvAyudaInfo.AutoGenerateColumns = False
            dgvAyudaInfo.Columns.Clear()
            dgvAyudaInfo.ReadOnly = True
            dgvAyudaInfo.AllowUserToAddRows = False
            dgvAyudaInfo.AllowUserToDeleteRows = False
            dgvAyudaInfo.RowHeadersVisible = False
            dgvAyudaInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            ' === STYLING ===
            dgvAyudaInfo.BackgroundColor = Color.FromArgb(181, 255, 124)
            dgvAyudaInfo.GridColor = Color.FromArgb(150, 200, 100)
            dgvAyudaInfo.ColumnHeadersHeight = 35
            dgvAyudaInfo.RowTemplate.Height = 30

            ' === COLUMN HEADERS ===
            dgvAyudaInfo.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
            dgvAyudaInfo.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvAyudaInfo.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            dgvAyudaInfo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            ' === ROW STYLING ===
            dgvAyudaInfo.DefaultCellStyle.Font = New Font("Arial", 10)
            dgvAyudaInfo.DefaultCellStyle.ForeColor = Color.Black
            dgvAyudaInfo.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
            dgvAyudaInfo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            ' === ADD COLUMNS ===
            dgvAyudaInfo.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "AidId",
                .DataPropertyName = "AidId",
                .HeaderText = "AidId",
                .Visible = False
            })

            dgvAyudaInfo.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "ProgramName",
                .DataPropertyName = "Program Name",
                .HeaderText = "Program Name",
                .Width = 250,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            })

            dgvAyudaInfo.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "AllocatedSlots",
                .DataPropertyName = "Allocated Slots",
                .HeaderText = "Allocated Slots",
                .Width = 120,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudaInfo.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "Claimed",
                .DataPropertyName = "Claimed",
                .HeaderText = "Claimed",
                .Width = 100,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudaInfo.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "Remaining",
                .DataPropertyName = "Remaining",
                .HeaderText = "Remaining",
                .Width = 100,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudaInfo.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "EndDate",
                .DataPropertyName = "end_date",
                .HeaderText = "End Date",
                .Visible = False
            })

            dgvAyudaInfo.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "Status",
                .DataPropertyName = "Status",
                .HeaderText = "Status",
                .Width = 150,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            ' === CELL FORMATTING EVENT ===
            AddHandler dgvAyudaInfo.CellFormatting, AddressOf dgvAyudaInfo_CellFormatting

        Catch ex As Exception
            Debug.WriteLine("ConfigureAyudaInformationGrid Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Configure Ayuda Audit Information DataGridView
    ''' </summary>
    Private Sub ConfigureAyudaAuditInformationGrid()
        Try
            dgvAyudaAuditInformation.AutoGenerateColumns = False
            dgvAyudaAuditInformation.Columns.Clear()
            dgvAyudaAuditInformation.ReadOnly = True
            dgvAyudaAuditInformation.AllowUserToAddRows = False
            dgvAyudaAuditInformation.AllowUserToDeleteRows = False
            dgvAyudaAuditInformation.RowHeadersVisible = False
            dgvAyudaAuditInformation.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            ' === STYLING ===
            dgvAyudaAuditInformation.BackgroundColor = Color.FromArgb(181, 255, 124)
            dgvAyudaAuditInformation.GridColor = Color.FromArgb(150, 200, 100)
            dgvAyudaAuditInformation.ColumnHeadersHeight = 35
            dgvAyudaAuditInformation.RowTemplate.Height = 30

            ' === COLUMN HEADERS ===
            dgvAyudaAuditInformation.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
            dgvAyudaAuditInformation.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvAyudaAuditInformation.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            dgvAyudaAuditInformation.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            ' === ROW STYLING ===
            dgvAyudaAuditInformation.DefaultCellStyle.Font = New Font("Arial", 10)
            dgvAyudaAuditInformation.DefaultCellStyle.ForeColor = Color.Black
            dgvAyudaAuditInformation.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
            dgvAyudaAuditInformation.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            ' === ADD COLUMNS ===
            dgvAyudaAuditInformation.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "ResidentName",
                .DataPropertyName = "Resident Name",
                .HeaderText = "Resident Name",
                .Width = 250,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            })

            dgvAyudaAuditInformation.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "DateClaimed",
                .DataPropertyName = "Date Claimed",
                .HeaderText = "Date Claimed",
                .Width = 150,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudaAuditInformation.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "Quantity",
                .DataPropertyName = "Quantity",
                .HeaderText = "Quantity",
                .Width = 100,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            })

            dgvAyudaAuditInformation.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "Description",
                .DataPropertyName = "Description",
                .HeaderText = "Description",
                .Width = 300,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            })

        Catch ex As Exception
            Debug.WriteLine("ConfigureAyudaAuditInformationGrid Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Load all ayuda programs into ComboBox
    ''' </summary>
    Private Sub LoadAyudaPrograms()
        Try
            Dim dataTable As DataTable = auditLogic.GetAllAyudaPrograms()

            If dataTable.Rows.Count > 0 Then
                cbAyuda.DataSource = dataTable
                cbAyuda.DisplayMember = "DisplayText"
                cbAyuda.ValueMember = "AidId"
                cbAyuda.SelectedIndex = -1
            Else
                MsgBox("No ayuda programs found.", MsgBoxStyle.Information, "No Data")
            End If

        Catch ex As Exception
            Debug.WriteLine("LoadAyudaPrograms Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Load totals based on date range and selected program
    ''' </summary>
    Private Sub LoadTotals()
        Try
            If isInitializing Then Return
            If cbAyuda.SelectedValue Is Nothing Then Return

            Dim totals As AyudaAuditLogic.TotalsResult

            ' === CHECK IF "ALL PROGRAMS" OR SPECIFIC PROGRAM ===
            If currentAidId = -1 OrElse currentAidId = 0 Then
                totals = auditLogic.CalculateTotalsAllPrograms(startDTP.Value, endDTP.Value)
            Else
                totals = auditLogic.CalculateTotals(startDTP.Value, endDTP.Value, currentAidId)
            End If

            ' === FORMAT AND DISPLAY RESULTS ===
            txtCashReleased.Text = FormatCurrency(totals.TotalCash)
            txtPacksReleased.Text = totals.TotalPacks.ToString("N0")

            Debug.WriteLine("Loaded Totals - Cash: ₱" & totals.TotalCash.ToString("N2") & ", Packs: " & totals.TotalPacks.ToString("N0"))

        Catch ex As Exception
            MsgBox("Error loading totals: " & ex.Message, MsgBoxStyle.Exclamation, "Error")
            Debug.WriteLine("LoadTotals Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Load KPI data into TextBoxes
    ''' </summary>
    Private Sub LoadKPIData()
        Try
            If isInitializing Then Return

            ' === GET KPI RESULTS FROM DATABASE ===
            Dim kpis As AyudaAuditLogic.KPIResults = auditLogic.CalculateKPIs(startDTP.Value, endDTP.Value, currentAidId)

            ' === LOAD TOTAL RESIDENTS SERVED ===
            txtResidentServed.Text = kpis.TotalResidentsServed.ToString()

            ' === LOAD CASH RELEASED ===
            If kpis.TotalCashReleased > 0 Then
                txtCashReleased.Text = FormatCurrency(kpis.TotalCashReleased)
            Else
                txtCashReleased.Text = "₱0.00"
            End If

            ' === LOAD PACKS RELEASED ===
            If kpis.TotalPacksReleased > 0 Then
                txtPacksReleased.Text = kpis.TotalPacksReleased.ToString("N0")
            Else
                txtPacksReleased.Text = "0"
            End If

            Debug.WriteLine("LoadKPIData updated stats for Aid ID: " & currentAidId)

        Catch ex As Exception
            Debug.WriteLine("LoadKPIData Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Format currency to Philippine Peso format
    ''' </summary>
    Private Function FormatCurrency(amount As Decimal) As String
        Try
            Dim phecultureInfo As New System.Globalization.CultureInfo("en-PH")
            phecultureInfo.NumberFormat.CurrencySymbol = "₱"
            Return amount.ToString("C2", phecultureInfo)
        Catch ex As Exception
            Debug.WriteLine("FormatCurrency Error: " & ex.Message)
            Return "₱" & amount.ToString("N2")
        End Try
    End Function

    ''' <summary>
    ''' Refresh all data when parameters change
    ''' </summary>
    Private Sub RefreshAllData()
        Try
            If isInitializing Then Return
            If cbAyuda.SelectedValue Is Nothing Then Return

            ' === PARSE SELECTED AID ID ===
            If Not Integer.TryParse(cbAyuda.SelectedValue.ToString(), currentAidId) Then
                currentAidId = -1
                Return
            End If

            ' === LOAD ALL DATA ===
            LoadKPIData()
            LoadAyudaInformation()
            LoadAyudaAuditInformation()

        Catch ex As Exception
            Debug.WriteLine("RefreshAllData Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' DataGridView CellFormatting - Color code status column
    ''' </summary>
    Private Sub dgvAyudaInfo_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        Try
            If e.ColumnIndex = dgvAyudaInfo.Columns("Status").Index AndAlso e.Value IsNot Nothing Then
                Dim statusValue As String = e.Value.ToString()

                If statusValue.Contains("LOCKED") Then
                    e.CellStyle.BackColor = Color.FromArgb(255, 200, 200)
                    e.CellStyle.ForeColor = Color.FromArgb(139, 0, 0)
                ElseIf statusValue = "FULL" Then
                    e.CellStyle.BackColor = Color.FromArgb(255, 255, 150)
                    e.CellStyle.ForeColor = Color.FromArgb(139, 120, 0)
                ElseIf statusValue = "ACTIVE" Then
                    e.CellStyle.BackColor = Color.FromArgb(200, 255, 200)
                    e.CellStyle.ForeColor = Color.FromArgb(0, 100, 0)
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine("dgvAyudaInfo_CellFormatting Error: " & ex.Message)
        End Try
    End Sub

    Private Sub cbAyuda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAyuda.SelectedIndexChanged
        RefreshAllData()
    End Sub

    Private Sub startDTP_ValueChanged(sender As Object, e As EventArgs) Handles startDTP.ValueChanged
        RefreshAllData()
    End Sub

    Private Sub endDTP_ValueChanged(sender As Object, e As EventArgs) Handles endDTP.ValueChanged
        RefreshAllData()
    End Sub


    ' ════════════════════════════════════════════════════════════════
    '  btnPrintAudit_Click — Orchestrator (UI layer only)
    '  Flow: Guard → Format Choice → Snapshot → Build HTML →
    '        Temp File → Preview → SaveDialog → Write → AuditLog
    ' ════════════════════════════════════════════════════════════════
    Private Sub btnPrintAudit_Click(sender As Object, e As EventArgs) Handles btnPrintAudit.Click

        ' ── Guard: data must exist ────────────────────────────────────
        If dgvAyudaInfo.Rows.Count = 0 AndAlso dgvAyudaAuditInformation.Rows.Count = 0 Then
            MessageBox.Show("There is no data to export. Please apply your filters first.",
                            "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' ── Step 1: Choose format ────────────────────────────────────
        Dim chosenFormat As String = ShowFormatChoiceDialog()
        If chosenFormat Is Nothing Then Return

        ' ── Step 2: Package all form control values into the filter DTO
        Dim filter As New AyudaAuditLogic.ReportFilterData With {
            .StartDate = startDTP.Value,
            .EndDate = endDTP.Value,
            .AyudaProgram = If(cbAyuda.SelectedIndex < 0 OrElse
                                      String.IsNullOrWhiteSpace(cbAyuda.Text),
                                      "All Programs", cbAyuda.Text),
            .TotalResidentsServed = txtResidentServed.Text,
            .TotalCashReleased = txtCashReleased.Text,
            .TotalPacksReleased = txtPacksReleased.Text,
            .GeneratedBy = LogInForm.CurrentUsername,
            .GeneratedOn = Now
        }

        ' ── Step 3: Snapshot both DataGridViews into plain data ───────
        Dim ayudaInfoSnapshot As AyudaAuditLogic.GridExportData =
            ExtractGridSnapshot(dgvAyudaInfo, "Master Ayuda Program Metadata")

        Dim auditLogSnapshot As AyudaAuditLogic.GridExportData =
            ExtractGridSnapshot(dgvAyudaAuditInformation, "Distribution Transaction Log")

        ' ── Step 4: Call logic layer to build the HTML string ─────────
        Dim htmlContent As String
        Try
            Dim logic As New AyudaAuditLogic()
            htmlContent = logic.BuildAyudaReportHtml(filter, ayudaInfoSnapshot, auditLogSnapshot)
        Catch ex As Exception
            MessageBox.Show("Failed to build report content:" & Environment.NewLine & ex.Message,
                            "Build Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' ── Step 5: Write HTML to a temp file for the WebBrowser ──────
        Dim tempPath As String = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(),
            "AyudaAuditPreview_" & Format(Now, "HHmmss") & ".html")

        Try
            System.IO.File.WriteAllText(tempPath, htmlContent, System.Text.Encoding.UTF8)
        Catch ex As Exception
            MessageBox.Show("Could not write preview file:" & Environment.NewLine & ex.Message,
                            "Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' ── Step 6: Show preview — returns True if user clicks Save ───
        Dim confirmed As Boolean = ShowPrintPreviewForm(tempPath, chosenFormat)

        If confirmed Then
            Dim filterLabel As String = If(chosenFormat = "XLS",
                                           "Excel Workbook (*.xls)|*.xls",
                                           "Word Document (*.doc)|*.doc")
            Using sfd As New SaveFileDialog()
                sfd.Title = "Save Ayuda Audit Report"
                sfd.Filter = filterLabel
                sfd.FileName = "AyudaAuditReport_" & Format(Now, "yyyyMMdd_HHmmss")

                If sfd.ShowDialog() = DialogResult.OK Then
                    Try
                        System.IO.File.WriteAllText(sfd.FileName, htmlContent,
                                                    System.Text.Encoding.UTF8)

                        ' === AUDIT TRAIL ===
                        GlobalAuditLogger.Log(
                            "ayudaaudit_form",
                            "PRINT_EXPORT_REPORT",
                            LogInForm.CurrentUsername &
                            " generated a complete filtered report of Ayuda and distribution audit records.")

                        MessageBox.Show("Report saved successfully!" &
                                        Environment.NewLine & sfd.FileName,
                                        "Export Complete",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As Exception
                        MessageBox.Show("Failed to save file:" & Environment.NewLine & ex.Message,
                                        "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End If

        ' ── Cleanup temp file ─────────────────────────────────────────
        Try
            If System.IO.File.Exists(tempPath) Then System.IO.File.Delete(tempPath)
        Catch
            ' Non-critical
        End Try

    End Sub


    ' ════════════════════════════════════════════════════════════════
    '  ExtractGridSnapshot  (form-layer helper)
    '  Reads a DataGridView into a plain GridExportData object.
    '  Must stay in the form layer — touches the WinForms DGV type.
    ' ════════════════════════════════════════════════════════════════
    Private Function ExtractGridSnapshot(grid As DataGridView,
                                         title As String) As AyudaAuditLogic.GridExportData

        Dim snapshot As New AyudaAuditLogic.GridExportData(title)

        ' Collect visible column headers
        For Each col As DataGridViewColumn In grid.Columns
            If col.Visible Then snapshot.Headers.Add(col.HeaderText)
        Next

        ' Collect visible cell values row by row
        For Each row As DataGridViewRow In grid.Rows
            If row.IsNewRow Then Continue For
            Dim rowData As New List(Of String)()
            For Each col As DataGridViewColumn In grid.Columns
                If col.Visible Then
                    Dim cellVal As String = ""
                    If row.Cells(col.Index).Value IsNot Nothing AndAlso
                       Not IsDBNull(row.Cells(col.Index).Value) Then
                        cellVal = row.Cells(col.Index).Value.ToString()
                    End If
                    rowData.Add(cellVal)
                End If
            Next
            snapshot.Rows.Add(rowData)
        Next

        Return snapshot
    End Function


    ' ════════════════════════════════════════════════════════════════
    '  ShowFormatChoiceDialog
    '  Returns "XLS", "DOC", or Nothing (user cancelled)
    ' ════════════════════════════════════════════════════════════════
    Private Function ShowFormatChoiceDialog() As String

        Dim result As String = Nothing

        Using dlg As New Form()
            dlg.Text = "Choose Export Format"
            dlg.Size = New System.Drawing.Size(420, 220)
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog
            dlg.MaximizeBox = False
            dlg.MinimizeBox = False
            dlg.BackColor = System.Drawing.Color.FromArgb(240, 255, 244)
            dlg.Font = New System.Drawing.Font("Arial", 10)

            Dim lbl As New Label()
            lbl.Text = "Select the format you want to export the report in:"
            lbl.AutoSize = False
            lbl.Size = New System.Drawing.Size(380, 40)
            lbl.Location = New System.Drawing.Point(20, 18)
            lbl.Font = New System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            lbl.ForeColor = System.Drawing.Color.FromArgb(44, 95, 46)
            dlg.Controls.Add(lbl)

            Dim btnExcel As New Button()
            btnExcel.Text = "📊  Excel (.xls)"
            btnExcel.Size = New System.Drawing.Size(170, 60)
            btnExcel.Location = New System.Drawing.Point(20, 72)
            btnExcel.BackColor = System.Drawing.Color.FromArgb(44, 95, 46)
            btnExcel.ForeColor = System.Drawing.Color.White
            btnExcel.FlatStyle = FlatStyle.Flat
            btnExcel.FlatAppearance.BorderSize = 0
            btnExcel.Font = New System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold)
            btnExcel.Cursor = Cursors.Hand
            AddHandler btnExcel.Click, Sub(s, ev)
                                           result = "XLS"
                                           dlg.Close()
                                       End Sub
            dlg.Controls.Add(btnExcel)

            Dim btnWord As New Button()
            btnWord.Text = "📄  Word (.doc)"
            btnWord.Size = New System.Drawing.Size(170, 60)
            btnWord.Location = New System.Drawing.Point(210, 72)
            btnWord.BackColor = System.Drawing.Color.FromArgb(26, 61, 143)
            btnWord.ForeColor = System.Drawing.Color.White
            btnWord.FlatStyle = FlatStyle.Flat
            btnWord.FlatAppearance.BorderSize = 0
            btnWord.Font = New System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold)
            btnWord.Cursor = Cursors.Hand
            AddHandler btnWord.Click, Sub(s, ev)
                                          result = "DOC"
                                          dlg.Close()
                                      End Sub
            dlg.Controls.Add(btnWord)

            Dim lnkCancel As New LinkLabel()
            lnkCancel.Text = "Cancel"
            lnkCancel.AutoSize = True
            lnkCancel.Location = New System.Drawing.Point(180, 155)
            lnkCancel.Font = New System.Drawing.Font("Arial", 9)
            lnkCancel.LinkColor = System.Drawing.Color.FromArgb(44, 95, 46)
            AddHandler lnkCancel.LinkClicked, Sub(s, ev)
                                                  result = Nothing
                                                  dlg.Close()
                                              End Sub
            dlg.Controls.Add(lnkCancel)

            dlg.ShowDialog(Me)
        End Using

        Return result
    End Function


    ' ════════════════════════════════════════════════════════════════
    '  ShowPrintPreviewForm
    '  Full-screen preview using an embedded WebBrowser control.
    '  Returns True if user clicks "Save", False if they cancel.
    ' ════════════════════════════════════════════════════════════════
    Private Function ShowPrintPreviewForm(tempHtmlPath As String,
                                          formatLabel As String) As Boolean

        Dim userConfirmed As Boolean = False

        Using preview As New Form()
            preview.Text = "Print Preview — Ayuda Audit Report  [" & formatLabel & "]"
            preview.WindowState = FormWindowState.Maximized
            preview.StartPosition = FormStartPosition.CenterScreen
            preview.BackColor = System.Drawing.Color.FromArgb(240, 255, 244)
            preview.Font = New System.Drawing.Font("Arial", 10)

            ' ── Toolbar ───────────────────────────────────────────────
            Dim toolbar As New Panel()
            toolbar.Height = 56
            toolbar.Dock = DockStyle.Top
            toolbar.BackColor = System.Drawing.Color.FromArgb(44, 95, 46)
            toolbar.Padding = New Padding(8, 8, 8, 8)
            preview.Controls.Add(toolbar)

            Dim lblTitle As New Label()
            lblTitle.Text = "📋  Report Preview"
            lblTitle.Font = New System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold)
            lblTitle.ForeColor = System.Drawing.Color.White
            lblTitle.AutoSize = True
            lblTitle.Location = New System.Drawing.Point(16, 14)
            toolbar.Controls.Add(lblTitle)

            Dim btnSaveNow As New Button()
            btnSaveNow.Text = "💾  Save Report"
            btnSaveNow.Size = New System.Drawing.Size(160, 36)
            btnSaveNow.BackColor = System.Drawing.Color.White
            btnSaveNow.ForeColor = System.Drawing.Color.FromArgb(44, 95, 46)
            btnSaveNow.FlatStyle = FlatStyle.Flat
            btnSaveNow.FlatAppearance.BorderSize = 0
            btnSaveNow.Font = New System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            btnSaveNow.Cursor = Cursors.Hand
            btnSaveNow.Location = New System.Drawing.Point(preview.Width - 340, 10)
            AddHandler preview.Resize, Sub(s, ev)
                                           btnSaveNow.Location = New System.Drawing.Point(preview.ClientSize.Width - 340, 10)
                                       End Sub
            AddHandler btnSaveNow.Click, Sub(s, ev)
                                             userConfirmed = True
                                             preview.Close()
                                         End Sub
            toolbar.Controls.Add(btnSaveNow)

            Dim btnCancelPreview As New Button()
            btnCancelPreview.Text = "✖  Cancel"
            btnCancelPreview.Size = New System.Drawing.Size(130, 36)
            btnCancelPreview.BackColor = System.Drawing.Color.FromArgb(180, 60, 60)
            btnCancelPreview.ForeColor = System.Drawing.Color.White
            btnCancelPreview.FlatStyle = FlatStyle.Flat
            btnCancelPreview.FlatAppearance.BorderSize = 0
            btnCancelPreview.Font = New System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            btnCancelPreview.Cursor = Cursors.Hand
            btnCancelPreview.Location = New System.Drawing.Point(preview.Width - 180, 10)
            AddHandler preview.Resize, Sub(s, ev)
                                           btnCancelPreview.Location = New System.Drawing.Point(preview.ClientSize.Width - 180, 10)
                                       End Sub
            AddHandler btnCancelPreview.Click, Sub(s, ev)
                                                   userConfirmed = False
                                                   preview.Close()
                                               End Sub
            toolbar.Controls.Add(btnCancelPreview)

            ' ── WebBrowser fills the rest of the window ───────────────
            Dim wb As New WebBrowser()
            wb.Dock = DockStyle.Fill
            wb.ScrollBarsEnabled = True
            wb.IsWebBrowserContextMenuEnabled = False
            wb.WebBrowserShortcutsEnabled = False
            wb.ScriptErrorsSuppressed = True
            preview.Controls.Add(wb)

            AddHandler preview.Shown, Sub(s, ev)
                                          wb.Navigate(New Uri(tempHtmlPath))
                                      End Sub

            preview.ShowDialog(Me)
        End Using

        Return userConfirmed
    End Function


    ' ============================================================
    ' WriteAyudaAuditReport
    ' StreamWriter helper — builds and writes the full HTML report.
    ' Called by btnPrintAudit_Click with the user-chosen file path.
    ' ============================================================
    Private Sub WriteAyudaAuditReport(filePath As String)

        Using sw As New System.IO.StreamWriter(filePath, False, System.Text.Encoding.UTF8)

            ' ── 1. HTML SHELL ────────────────────────────────────────────
            sw.WriteLine("<!DOCTYPE html>")
            sw.WriteLine("<html>")
            sw.WriteLine("<head>")
            sw.WriteLine("  <meta charset=""UTF-8"">")
            sw.WriteLine("  <title>Ayuda Audit Report</title>")
            sw.WriteLine("  <style>")
            sw.WriteLine("    body { font-family: Arial, sans-serif; font-size: 13px; margin: 24px; color: #1a1a2e; }")
            sw.WriteLine("    h1 { font-size: 22px; color: #2c5f2e; margin-bottom: 4px; }")
            sw.WriteLine("    h2 { font-size: 16px; color: #2c5f2e; margin-top: 28px; margin-bottom: 8px; border-bottom: 2px solid #2c5f2e; padding-bottom: 4px; }")
            sw.WriteLine("    .meta-card { background-color: #f0fff4; border: 1px solid #a8d5b5; border-radius: 6px;")
            sw.WriteLine("                 padding: 16px 20px; margin-bottom: 24px; display: table; width: 100%; }")
            sw.WriteLine("    .meta-card table { border-collapse: collapse; width: 100%; }")
            sw.WriteLine("    .meta-card td { padding: 6px 12px; font-size: 13px; }")
            sw.WriteLine("    .meta-card td.label { font-weight: bold; color: #2c5f2e; width: 220px; white-space: nowrap; }")
            sw.WriteLine("    .meta-card td.value { color: #333; }")
            sw.WriteLine("    .data-table { border-collapse: collapse; width: 100%; margin-bottom: 32px; }")
            sw.WriteLine("    .data-table th { background-color: #2c5f2e; color: #ffffff; padding: 8px 10px;")
            sw.WriteLine("                     text-align: left; font-size: 12px; border: 1px solid #1a3d1e; }")
            sw.WriteLine("    .data-table td { padding: 6px 10px; border: 1px solid #b2d8b2; font-size: 12px; }")
            sw.WriteLine("    .data-table tr:nth-child(even) { background-color: #f0fff4; }")
            sw.WriteLine("    .data-table tr:hover { background-color: #d4edda; }")
            sw.WriteLine("    .no-data { color: #888; font-style: italic; padding: 10px 0; }")
            sw.WriteLine("    .footer { margin-top: 32px; font-size: 11px; color: #888; border-top: 1px solid #ccc; padding-top: 8px; }")
            sw.WriteLine("  </style>")
            sw.WriteLine("</head>")
            sw.WriteLine("<body>")

            ' ── 2. REPORT TITLE ─────────────────────────────────────────
            sw.WriteLine("  <h1>&#127807; Barangay Maly — Ayuda Audit Report</h1>")
            sw.WriteLine("  <p style=""margin-top:0; color:#555; font-size:12px;"">")
            sw.WriteLine("    Generated on: " & Format(Now, "MMMM dd, yyyy  hh:mm:ss tt"))
            sw.WriteLine("    &nbsp;|&nbsp; Exported by: <strong>" & System.Net.WebUtility.HtmlEncode(LogInForm.CurrentUsername) & "</strong>")
            sw.WriteLine("  </p>")

            ' ── 3. FILTER CRITERIA METADATA CARD ────────────────────────
            sw.WriteLine("  <div class=""meta-card"">")
            sw.WriteLine("    <table>")
            sw.WriteLine("      <tr>")
            sw.WriteLine("        <td class=""label"">&#128197; Date Range (From):</td>")
            sw.WriteLine("        <td class=""value"">" & System.Net.WebUtility.HtmlEncode(startDTP.Value.ToString("MMMM dd, yyyy")) & "</td>")
            sw.WriteLine("        <td class=""label"">&#128197; Date Range (To):</td>")
            sw.WriteLine("        <td class=""value"">" & System.Net.WebUtility.HtmlEncode(endDTP.Value.ToString("MMMM dd, yyyy")) & "</td>")
            sw.WriteLine("      </tr>")
            sw.WriteLine("      <tr>")
            sw.WriteLine("        <td class=""label"">&#127873; Ayuda Program Filter:</td>")
            sw.WriteLine("        <td class=""value"">" & System.Net.WebUtility.HtmlEncode(If(cbAyuda.SelectedIndex < 0 OrElse String.IsNullOrWhiteSpace(cbAyuda.Text), "All Programs", cbAyuda.Text)) & "</td>")
            sw.WriteLine("        <td class=""label""></td><td class=""value""></td>")
            sw.WriteLine("      </tr>")
            sw.WriteLine("      <tr>")
            sw.WriteLine("        <td class=""label"">&#128100; Total Residents Served:</td>")
            sw.WriteLine("        <td class=""value"">" & System.Net.WebUtility.HtmlEncode(txtResidentServed.Text) & "</td>")
            sw.WriteLine("        <td class=""label"">&#128181; Total Cash Released:</td>")
            sw.WriteLine("        <td class=""value"">" & System.Net.WebUtility.HtmlEncode(txtCashReleased.Text) & "</td>")
            sw.WriteLine("      </tr>")
            sw.WriteLine("      <tr>")
            sw.WriteLine("        <td class=""label"">&#128230; Total Packs Released:</td>")
            sw.WriteLine("        <td class=""value"">" & System.Net.WebUtility.HtmlEncode(txtPacksReleased.Text) & "</td>")
            sw.WriteLine("        <td class=""label""></td><td class=""value""></td>")
            sw.WriteLine("      </tr>")
            sw.WriteLine("    </table>")
            sw.WriteLine("  </div>")

            ' ── 4. GRID 1: MASTER AYUDA PROGRAM METADATA ────────────────
            sw.WriteLine("  <h2>&#128203; Master Ayuda Program Metadata</h2>")

            If dgvAyudaInfo.Columns.Count = 0 OrElse dgvAyudaInfo.Rows.Count = 0 Then
                sw.WriteLine("  <p class=""no-data"">No Ayuda program records found for the selected filters.</p>")
            Else
                sw.WriteLine("  <table class=""data-table"" border=""1"" cellpadding=""6"" cellspacing=""0"">")
                ' Header row
                sw.WriteLine("    <thead><tr>")
                For Each col As DataGridViewColumn In dgvAyudaInfo.Columns
                    If col.Visible Then
                        sw.WriteLine("      <th>" & System.Net.WebUtility.HtmlEncode(col.HeaderText) & "</th>")
                    End If
                Next
                sw.WriteLine("    </tr></thead>")
                ' Data rows
                sw.WriteLine("    <tbody>")
                For Each row As DataGridViewRow In dgvAyudaInfo.Rows
                    If row.IsNewRow Then Continue For
                    sw.WriteLine("    <tr>")
                    For Each col As DataGridViewColumn In dgvAyudaInfo.Columns
                        If col.Visible Then
                            Dim cellValue As String = ""
                            If row.Cells(col.Index).Value IsNot Nothing AndAlso
                               Not IsDBNull(row.Cells(col.Index).Value) Then
                                cellValue = row.Cells(col.Index).Value.ToString()
                            End If
                            sw.WriteLine("      <td>" & System.Net.WebUtility.HtmlEncode(cellValue) & "</td>")
                        End If
                    Next
                    sw.WriteLine("    </tr>")
                Next
                sw.WriteLine("    </tbody>")
                sw.WriteLine("  </table>")
            End If

            ' ── 5. GRID 2: DISTRIBUTION TRANSACTION LOG ─────────────────
            sw.WriteLine("  <h2>&#128200; Distribution Transaction Log</h2>")

            If dgvAyudaAuditInformation.Columns.Count = 0 OrElse dgvAyudaAuditInformation.Rows.Count = 0 Then
                sw.WriteLine("  <p class=""no-data"">No distribution transaction records found for the selected filters.</p>")
            Else
                sw.WriteLine("  <table class=""data-table"" border=""1"" cellpadding=""6"" cellspacing=""0"">")
                ' Header row
                sw.WriteLine("    <thead><tr>")
                For Each col As DataGridViewColumn In dgvAyudaAuditInformation.Columns
                    If col.Visible Then
                        sw.WriteLine("      <th>" & System.Net.WebUtility.HtmlEncode(col.HeaderText) & "</th>")
                    End If
                Next
                sw.WriteLine("    </tr></thead>")
                ' Data rows
                sw.WriteLine("    <tbody>")
                For Each row As DataGridViewRow In dgvAyudaAuditInformation.Rows
                    If row.IsNewRow Then Continue For
                    sw.WriteLine("    <tr>")
                    For Each col As DataGridViewColumn In dgvAyudaAuditInformation.Columns
                        If col.Visible Then
                            Dim cellValue As String = ""
                            If row.Cells(col.Index).Value IsNot Nothing AndAlso
                               Not IsDBNull(row.Cells(col.Index).Value) Then
                                cellValue = row.Cells(col.Index).Value.ToString()
                            End If
                            sw.WriteLine("      <td>" & System.Net.WebUtility.HtmlEncode(cellValue) & "</td>")
                        End If
                    Next
                    sw.WriteLine("    </tr>")
                Next
                sw.WriteLine("    </tbody>")
                sw.WriteLine("  </table>")
            End If

            ' ── 6. FOOTER ────────────────────────────────────────────────
            sw.WriteLine("  <div class=""footer"">")
            sw.WriteLine("    Barangay Maly Management System &mdash; Confidential Document")
            sw.WriteLine("    &nbsp;|&nbsp; Printed by: " & System.Net.WebUtility.HtmlEncode(LogInForm.CurrentUsername))
            sw.WriteLine("    &nbsp;|&nbsp; " & Format(Now, "yyyy-MM-dd HH:mm:ss"))
            sw.WriteLine("  </div>")

            sw.WriteLine("</body>")
            sw.WriteLine("</html>")

        End Using ' StreamWriter auto-flushes and closes here

    End Sub


    ''' <summary>
    ''' Apply gradient background to panel
    ''' </summary>
    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Try
            Dim startColor = ColorTranslator.FromHtml(startColorHex)
            Dim endColor = ColorTranslator.FromHtml(endColorHex)

            AddHandler pnl.Paint, Sub(sender, e)
                                      Using brush As New LinearGradientBrush(New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
                                          e.Graphics.FillRectangle(brush, pnl.ClientRectangle)
                                      End Using
                                  End Sub
        Catch ex As Exception
            Debug.WriteLine("ApplyGradient Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Apply rounded corners to button
    ''' </summary>
    Private Sub RoundButtonCorners(ByVal btn As Button, ByVal radius As Integer)
        Try
            If btn Is Nothing Then Return

            ' Fixed ByRef Parameter error for Lambda expressions by creating local reference
            Dim btnLocal = btn
            ApplyButtonRoundedRegion(btnLocal, radius)

            AddHandler btnLocal.Resize, Sub(s, args)
                                            ApplyButtonRoundedRegion(btnLocal, radius)
                                        End Sub

        Catch ex As Exception
            Debug.WriteLine("RoundButtonCorners Error: " & ex.Message)
        End Try
    End Sub

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

    ''' <summary>
    ''' Load Ayuda Information into DataGridView
    ''' </summary>
    Private Sub LoadAyudaInformation()
        Try
            If currentAidId <= 0 Then
                dgvAyudaInfo.DataSource = Nothing
                Return
            End If

            Dim dataTable As DataTable = auditLogic.GetAyudaInformation(currentAidId)
            dgvAyudaInfo.DataSource = dataTable
        Catch ex As Exception
            Debug.WriteLine("LoadAyudaInformation Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Load Ayuda Audit Information into DataGridView
    ''' </summary>
    Private Sub LoadAyudaAuditInformation()
        Try
            If currentAidId <= 0 Then
                dgvAyudaAuditInformation.DataSource = Nothing
                Return
            End If

            Dim dataTable As DataTable = auditLogic.GetAyudaAuditInformation(currentAidId, startDTP.Value, endDTP.Value)
            dgvAyudaAuditInformation.DataSource = dataTable
        Catch ex As Exception
            Debug.WriteLine("LoadAyudaAuditInformation Error: " & ex.Message)
        End Try
    End Sub


End Class