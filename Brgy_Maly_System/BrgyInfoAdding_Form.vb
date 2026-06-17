Imports System.Drawing.Drawing2D
Imports System.IO

Public Class BrgyInfoAdding_Form
    Private brgyInfoLogic As New BrgyInfoAddingLogic()
    Private responsiveManager As BrgyInfoAddingResponsiveManager

    Private logoFilePath As String = ""
    Private logoRemoved As Boolean = False
    Private selectedOfficialId As Integer = -1

    ' ============================================================================
    ' FORM LOAD — Auto-populate all fields from the single barangayinformation row
    ' ============================================================================
    Private Sub BrgyInfoAdding_Form_Load(sender As Object, e As EventArgs) _
        Handles MyBase.Load
        Try
            ' Visual polish
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")
            RoundButtonCorners(BtnUpload, 20)
            RoundButtonCorners(BtnRemove, 20)
            RoundButtonCorners(BtnSaveInfo, 20)
            RoundButtonCorners(BtnSearch, 20)
            RoundButtonCorners(BtnAddNewOfficial, 20)
            RoundButtonCorners(BtnEditSelected, 20)
            RoundButtonCorners(BtnArchieveSelected, 20)
            responsiveManager = New BrgyInfoAddingResponsiveManager(Me)
            responsiveManager.Initialize()
            ConfigureOfficialsDGV()
            ' ── AUTO-LOAD: pull the master configuration row and bind all controls ──
            LoadBarangayInfo()
            LoadAllOfficials()
        Catch ex As Exception
            MsgBox("Error loading barangay information form: " & ex.Message,
               MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ConfigureOfficialsDGV()
        BrgyOfficialsDGV.AutoGenerateColumns = False
        BrgyOfficialsDGV.Columns.Clear()
        BrgyOfficialsDGV.ReadOnly = True
        BrgyOfficialsDGV.AllowUserToAddRows = False
        BrgyOfficialsDGV.AllowUserToDeleteRows = False
        BrgyOfficialsDGV.RowHeadersVisible = False
        BrgyOfficialsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        BrgyOfficialsDGV.MultiSelect = False
        BrgyOfficialsDGV.EnableHeadersVisualStyles = False

        BrgyOfficialsDGV.BackgroundColor = Color.FromArgb(220, 220, 220)
        BrgyOfficialsDGV.GridColor = Color.FromArgb(180, 180, 180)
        BrgyOfficialsDGV.ColumnHeadersHeight = 35
        BrgyOfficialsDGV.RowTemplate.Height = 30

        BrgyOfficialsDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        BrgyOfficialsDGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        BrgyOfficialsDGV.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
        BrgyOfficialsDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        BrgyOfficialsDGV.DefaultCellStyle.Font = New Font("Arial", 10)
        BrgyOfficialsDGV.DefaultCellStyle.ForeColor = Color.Black
        BrgyOfficialsDGV.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        BrgyOfficialsDGV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        BrgyOfficialsDGV.DefaultCellStyle.Padding = New Padding(5)
        BrgyOfficialsDGV.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        BrgyOfficialsDGV.DefaultCellStyle.SelectionForeColor = Color.Black

        BrgyOfficialsDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)
        BrgyOfficialsDGV.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black

        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "OfficialId",
            .DataPropertyName = "OfficialId",
            .HeaderText = "ID",
            .Width = 50,
            .Visible = True
        })

        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FullName",
            .HeaderText = "Full Name",
            .DataPropertyName = "FullName",
            .Width = 150
        })

        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Position",
            .HeaderText = "Position",
            .DataPropertyName = "Position",
            .Width = 150
        })

        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "TermStart",
            .HeaderText = "Term Start",
            .DataPropertyName = "TermStart",
            .Width = 120
        })

        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "TermEnd",
            .HeaderText = "Term End",
            .DataPropertyName = "TermEnd",
            .Width = 120,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        })
    End Sub

    ' ============================================================================
    ' DATA LOAD — Pulls single master row; binds text fields + PictureBox
    ' ============================================================================
    Private Sub LoadBarangayInfo()
        Try
            ' Reset controls to neutral state first
            LoadNeutralBarangayInfo()
            Dim dataTable As DataTable = brgyInfoLogic.GetBarangayInfo()
            If dataTable.Rows.Count = 0 Then Return   ' table is empty — keep defaults
            Dim row As DataRow = dataTable.Rows(0)
            ' ── Bind text fields ──────────────────────────────────────────────────
            txtBrgyName.Text = If(IsDBNull(row("BarangayName")), "", row("BarangayName").ToString())
            BrgyMissionRtxt.Text = If(IsDBNull(row("Mission")), "", row("Mission").ToString())
            BrgyVisionRtxt.Text = If(IsDBNull(row("Vision")), "", row("Vision").ToString())
            ' ── Bind Logo blob → PictureBox ──────────────────────────────────────
            ' The Logo column is LONGBLOB (raw uncompressed bytes).
            ' We read it into a MemoryStream and let GDI+ auto-detect the format.
            If Not IsDBNull(row("Logo")) Then
                Dim logoBytes As Byte() = CType(row("Logo"), Byte())
                If logoBytes IsNot Nothing AndAlso logoBytes.Length > 0 Then
                    Using ms As New MemoryStream(logoBytes)
                        ' New Bitmap(stream) decodes JPEG/PNG/BMP automatically.
                        ' We create a fresh Bitmap copy so the stream can be disposed.
                        BrgyLogoPic.Image = New Bitmap(ms)
                    End Using
                    BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage
                End If
            End If
            ' Clear the transient flags so Save knows no new file was chosen
            logoFilePath = ""
            logoRemoved = False
            BtnRemove.Enabled = BrgyLogoPic.Image IsNot Nothing
        Catch ex As Exception
            Debug.WriteLine("LoadBarangayInfo Error: " & ex.Message)
            LoadNeutralBarangayInfo()   ' graceful fallback
        End Try
    End Sub


    Private Sub LoadNeutralBarangayInfo()
        txtBrgyName.Clear()
        BrgyMissionRtxt.Clear()
        BrgyVisionRtxt.Clear()
        BrgyLogoPic.Image = My.Resources.LogoForMaly
        BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage
        logoFilePath = ""
        logoRemoved = False
        BtnRemove.Enabled = True
    End Sub

    Private Sub LoadAllOfficials()
        Try
            BrgyOfficialsDGV.DataSource = brgyInfoLogic.GetAllBarangayOfficials()
        Catch ex As Exception
            MsgBox("Error loading officials: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click
        Try
            Using openFileDialog As New OpenFileDialog()
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
                openFileDialog.Title = "Select Barangay Logo"

                If openFileDialog.ShowDialog() = DialogResult.OK Then
                    logoFilePath = openFileDialog.FileName
                    logoRemoved = False

                    If BrgyLogoPic.Image IsNot Nothing Then
                        BrgyLogoPic.Image.Dispose()
                    End If

                    Using tempImage As Image = Image.FromFile(logoFilePath)
                        BrgyLogoPic.Image = New Bitmap(tempImage)
                    End Using

                    BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage
                    BtnRemove.Enabled = True
                    MsgBox("Logo loaded successfully.", MsgBoxStyle.Information, "Success")
                End If
            End Using

        Catch ex As Exception
            MsgBox("Error loading image: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub BtnRemove_Click(sender As Object, e As EventArgs) Handles BtnRemove.Click
        If BrgyLogoPic.Image IsNot Nothing Then
            BrgyLogoPic.Image.Dispose()
        End If

        BrgyLogoPic.Image = My.Resources.LogoForMaly
        BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage

        logoFilePath = ""
        logoRemoved = True
        BtnRemove.Enabled = True
    End Sub

    ' ============================================================================
    ' BUTTON: Save / Update Barangay Information
    ' Delegates INSERT vs UPDATE decision entirely to the BLL.
    ' ============================================================================
    Private Sub BtnSaveInfo_Click(sender As Object, e As EventArgs) _
        Handles BtnSaveInfo.Click
        Try
            ' Pass field values and logo state to the BLL.
            ' The BLL determines whether to UPDATE the existing row or INSERT a new one.
            Dim result As BrgyInfoAddingLogic.BrgyInfoResult =
            brgyInfoLogic.SaveBarangayInfo(
                txtBrgyName.Text.Trim(),
                BrgyMissionRtxt.Text.Trim(),
                BrgyVisionRtxt.Text.Trim(),
                logoFilePath,
                logoRemoved
            )
            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                logoFilePath = ""
                logoRemoved = False
                LoadBarangayInfo()   ' refresh form controls from DB
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Try
            If String.IsNullOrWhiteSpace(TxtSearchOfficial.Text) Then
                LoadAllOfficials()
            Else
                BrgyOfficialsDGV.DataSource = brgyInfoLogic.SearchBarangayOfficials(TxtSearchOfficial.Text.Trim())
            End If
        Catch ex As Exception
            MsgBox("Error searching officials: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub BtnAddNewOfficial_Click(sender As Object, e As EventArgs) Handles BtnAddNewOfficial.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim brgyOfficialsForm As New BrgyOfficials_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(brgyOfficialsForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub BtnEditSelected_Click(sender As Object, e As EventArgs) Handles BtnEditSelected.Click
        Try
            If BrgyOfficialsDGV.SelectedRows.Count = 0 Then
                MsgBox("Please select an official to edit.", MsgBoxStyle.Information, "Selection Required")
                Return
            End If

            Dim selectedCellValue = BrgyOfficialsDGV.SelectedRows(0).Cells("OfficialId").Value

            If selectedCellValue Is Nothing OrElse IsDBNull(selectedCellValue) Then
                MsgBox("Error: Could not retrieve official ID. Please try again.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            selectedOfficialId = CInt(selectedCellValue)

            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim brgyOfficialsForm As New BrgyOfficials_Form(selectedOfficialId)
                Dashboard_Layout.CurrentInstance.LoadContentPanel(brgyOfficialsForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub BtnArchieveSelected_Click(sender As Object, e As EventArgs) Handles BtnArchieveSelected.Click
        Try
            If BrgyOfficialsDGV.SelectedRows.Count = 0 Then
                MsgBox("Please select an official to archive.", MsgBoxStyle.Information, "Selection Required")
                Return
            End If

            Dim selectedCellValue = BrgyOfficialsDGV.SelectedRows(0).Cells("OfficialId").Value

            If selectedCellValue Is Nothing OrElse IsDBNull(selectedCellValue) Then
                MsgBox("Error: Could not retrieve official ID. Please try again.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            selectedOfficialId = CInt(selectedCellValue)

            Dim officialName As String = BrgyOfficialsDGV.SelectedRows(0).Cells("FullName").Value.ToString()

            If MsgBox("Archive " & officialName & "? They will not appear in the list.", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.No Then
                Return
            End If

            Dim result As BrgyInfoAddingLogic.BrgyInfoResult = brgyInfoLogic.ArchiveOfficial(selectedOfficialId)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                LoadAllOfficials()
                selectedOfficialId = -1
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)

        AddHandler pnl.Paint,
            Sub(sender, e)
                Using brush As New LinearGradientBrush(New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
                    e.Graphics.FillRectangle(brush, pnl.ClientRectangle)
                End Using
            End Sub
    End Sub

    Private Sub RoundButtonCorners(btn As Button, ByVal radius As Integer)
        ApplyButtonRoundedRegion(btn, radius)

        AddHandler btn.Resize,
            Sub(s, args)
                ApplyButtonRoundedRegion(btn, radius)
            End Sub
    End Sub

    Private Sub ApplyButtonRoundedRegion(btn As Button, radius As Integer)
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
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If

        MyBase.OnFormClosing(e)
    End Sub

End Class