Imports System.Drawing.Drawing2D

''' <summary>
''' UI Event Layer for AyudaEditRecording_Form.
''' Accepts a ResidentAidId from AyudaMain_Form, populates all fields,
''' locks resident identity controls, and delegates all DB work to
''' AyudaEditRecordingLogic.
''' </summary>
Public Class AyudaEditRecording_Form

    ' ── Service Layer ────────────────────────────────────────────────
    Private _logic As New AyudaEditRecordingLogic()

    ' ── Responsive Manager ───────────────────────────────────────────
    Private _responsiveManager As AyudaEditRecordingResponsiveManager

    ' ── Record Identifiers passed from AyudaMain_Form ─────────────────
    ''' <summary>
    ''' The primary key of the residentaid row being edited.
    ''' Set by AyudaMain_Form before LoadContentPanel() is called.
    ''' </summary>
    Public Property TargetResidentAidId As Integer = -1

    ' ════════════════════════════════════════════════════════════════
    '  FORM LOAD
    ' ════════════════════════════════════════════════════════════════
    Private Sub AyudaEditRecording_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' ── Visual setup ─────────────────────────────────────────
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")
            RoundButtonCorners(btnSaveChanges, 20)
            RoundButtonCorners(btnBackToMainPage, 20)

            ' ── Responsive layout ────────────────────────────────────
            _responsiveManager = New AyudaEditRecordingResponsiveManager(Me)
            _responsiveManager.Initialize()

            ' ── Lock all resident identity fields immediately ─────────
            LockResidentFields()

            ' ── Guard: ensure a valid ID was passed in ───────────────
            If TargetResidentAidId <= 0 Then
                MsgBox("No record selected. Please select a record from the list and try again.",
                       MsgBoxStyle.Exclamation, "No Record Selected")
                Return
            End If

            ' ── Populate Ayuda program ComboBox from DB ───────────────
            LoadAyudaProgramsComboBox()

            ' ── Fetch and populate all form fields ───────────────────
            PopulateFormFields()

        Catch ex As Exception
            MsgBox("Error initializing form: " & ex.Message, MsgBoxStyle.Critical, "Load Error")
            Debug.WriteLine("AyudaEditRecording_Form_Load Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  SECURITY: Lock resident identity controls
    '  These fields are display-only — user MUST NOT be able to edit.
    ' ════════════════════════════════════════════════════════════════
    Private Sub LockResidentFields()
        ' Set ReadOnly and apply a visual cue (slightly darker background)
        Dim lockedColor As Color = Color.FromArgb(220, 220, 220)
        Dim lockedFore As Color = Color.FromArgb(90, 90, 90)

        For Each ctrl As Control In New Control() {
                txtResidentName, txtAge, txtSex, txtCategory, txtHousehold}
            If TypeOf ctrl Is TextBox Then
                Dim tb As TextBox = CType(ctrl, TextBox)
                tb.ReadOnly = True
                tb.BackColor = lockedColor
                tb.ForeColor = lockedFore
                tb.TabStop = False
                tb.Cursor = Cursors.Default
            End If
        Next
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  LOAD AYUDA PROGRAMS INTO COMBOBOX
    ' ════════════════════════════════════════════════════════════════
    Private Sub LoadAyudaProgramsComboBox()
        Try
            Dim programs As DataTable = _logic.GetAllAyudaPrograms()
            cbAyuda.DataSource = programs
            cbAyuda.DisplayMember = "DisplayText"
            cbAyuda.ValueMember = "AidId"
            cbAyuda.SelectedIndex = -1
        Catch ex As Exception
            Debug.WriteLine("LoadAyudaProgramsComboBox Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  POPULATE ALL FORM FIELDS from the database record
    ' ════════════════════════════════════════════════════════════════
    Private Sub PopulateFormFields()
        Try
            Dim dbErrorMsg As String = ""
            Dim record As AyudaEditRecordingLogic.AyudaRecordData =
                _logic.GetRecordById(TargetResidentAidId, dbErrorMsg)

            If record Is Nothing Then
                ' Now shows the ACTUAL reason (column error, no row, etc.)
                MsgBox(If(String.IsNullOrWhiteSpace(dbErrorMsg),
                          "Could not load record data.",
                          dbErrorMsg),
                       MsgBoxStyle.Exclamation, "Record Not Found")
                Return
            End If

            ' ── Locked: Resident Identity ─────────────────────────────
            txtResidentName.Text = record.ResidentFullName
            txtAge.Text = record.Age.ToString()
            txtSex.Text = record.Sex
            txtCategory.Text = record.Category
            txtHousehold.Text = record.HouseholdNumber

            ' ── Editable: Ayuda Parameters ────────────────────────────
            If cbAyuda.DataSource IsNot Nothing Then
                cbAyuda.SelectedValue = record.AidId
            End If

            txtQuantity.Text = record.Quantity.ToString()
            AyudaDateDTP.Value = If(record.AidDate = Date.MinValue, Now, record.AidDate)
            DescriptionRtxt.Text = record.Description

        Catch ex As Exception
            MsgBox("Error populating fields: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("PopulateFormFields Error: " & ex.Message)
        End Try
    End Sub


    ' ════════════════════════════════════════════════════════════════
    '  SAVE CHANGES
    ' ════════════════════════════════════════════════════════════════
    Private Sub btnSaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
        Try
            ' ── Validate quantity is numeric ──────────────────────────
            Dim quantity As Integer
            If Not Integer.TryParse(txtQuantity.Text.Trim(), quantity) OrElse quantity <= 0 Then
                MsgBox("Please enter a valid numeric quantity greater than zero.",
                       MsgBoxStyle.Exclamation, "Invalid Quantity")
                txtQuantity.Focus()
                Return
            End If

            ' ── Validate an Ayuda program is selected ─────────────────
            If cbAyuda.SelectedValue Is Nothing OrElse cbAyuda.SelectedIndex < 0 Then
                MsgBox("Please select an Ayuda program.",
                       MsgBoxStyle.Exclamation, "No Program Selected")
                cbAyuda.Focus()
                Return
            End If

            ' ── Confirm before saving ─────────────────────────────────
            Dim confirm As MsgBoxResult = MsgBox(
                "Are you sure you want to save these changes to the Ayuda record?",
                MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm Save")
            If confirm = MsgBoxResult.No Then Return

            ' ── Package updated values and call Logic layer ───────────
            Dim updatedData As New AyudaEditRecordingLogic.AyudaUpdateData With {
                .ResidentAidId = TargetResidentAidId,
                .AidId = CInt(cbAyuda.SelectedValue),
                .quantity = quantity,
                .AidDate = AyudaDateDTP.Value,
                .Description = DescriptionRtxt.Text.Trim()
            }

            Dim result As AyudaEditRecordingLogic.EditResult = _logic.UpdateAyudaRecord(updatedData)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Saved Successfully")

                ' ── Navigate back to AyudaMain and refresh ────────────
                If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                    Dim mainForm As New AyudaMain_Form()
                    Dashboard_Layout.CurrentInstance.LoadContentPanel(mainForm)
                End If
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Save Failed")
            End If

        Catch ex As Exception
            MsgBox("An unexpected error occurred: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnSaveChanges_Click Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  BACK TO MAIN PAGE
    ' ════════════════════════════════════════════════════════════════
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

    ' ════════════════════════════════════════════════════════════════
    '  FORM CLOSING — cleanup
    ' ════════════════════════════════════════════════════════════════
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        Try
            If _responsiveManager IsNot Nothing Then _responsiveManager.Cleanup()
            MyBase.OnFormClosing(e)
        Catch ex As Exception
            Debug.WriteLine("OnFormClosing Error: " & ex.Message)
        End Try
    End Sub

    ' ════════════════════════════════════════════════════════════════
    '  VISUAL HELPERS  (identical pattern to all other forms)
    ' ════════════════════════════════════════════════════════════════
    Private Sub ApplyGradient(pnl As Control, startColorHex As String, endColorHex As String)
        Try
            Dim startColor = ColorTranslator.FromHtml(startColorHex)
            Dim endColor = ColorTranslator.FromHtml(endColorHex)
            Dim brush As New LinearGradientBrush(
                New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
            Dim panelLocal = pnl
            AddHandler panelLocal.Paint, Sub(s, ev)
                                             ev.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                                         End Sub
        Catch ex As Exception
            Debug.WriteLine("ApplyGradient Error: " & ex.Message)
        End Try
    End Sub

    Private Sub RoundButtonCorners(btn As Button, radius As Integer)
        Try
            If btn Is Nothing Then Return
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

End Class
