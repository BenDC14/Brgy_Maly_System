Imports System.Drawing.Drawing2D

Public Class BrgyInfoAdding_Form
    ' === Service Layer (Business Logic) ===
    Private brgyInfoLogic As New BrgyInfoAddingLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As BrgyInfoAddingResponsiveManager

    ' === UI State ===
    Private logoFilePath As String = ""
    Private selectedOfficialId As Integer = -1

    Private Sub BrgyInfoAdding_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        RoundButtonCorners(BtnUpload, 20)
        RoundButtonCorners(BtnRemove, 20)
        RoundButtonCorners(BtnSaveInfo, 20)
        RoundButtonCorners(BtnSearch, 20)
        RoundButtonCorners(BtnAddNewOfficial, 20)
        RoundButtonCorners(BtnEditSelected, 20)
        RoundButtonCorners(BtnArchieveSelected, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New BrgyInfoAddingResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Configure DataGridView ===
        ConfigureOfficialsDGV()

        ' === Load Barangay Info ===
        LoadBarangayInfo()

        ' === Load Officials ===
        LoadAllOfficials()
    End Sub

    ''' <summary>
    ''' Configure DataGridView with professional styling
    ''' </summary>
    Private Sub ConfigureOfficialsDGV()
        BrgyOfficialsDGV.AutoGenerateColumns = False
        BrgyOfficialsDGV.Columns.Clear()
        BrgyOfficialsDGV.ReadOnly = True
        BrgyOfficialsDGV.AllowUserToAddRows = False
        BrgyOfficialsDGV.AllowUserToDeleteRows = False
        BrgyOfficialsDGV.RowHeadersVisible = False

        ' === STYLING ===
        BrgyOfficialsDGV.BackgroundColor = Color.FromArgb(220, 220, 220)
        BrgyOfficialsDGV.GridColor = Color.FromArgb(180, 180, 180)
        BrgyOfficialsDGV.ColumnHeadersHeight = 35
        BrgyOfficialsDGV.RowTemplate.Height = 30

        ' === COLUMN HEADERS STYLING ===
        BrgyOfficialsDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        BrgyOfficialsDGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        BrgyOfficialsDGV.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
        BrgyOfficialsDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' === ROW STYLING ===
        BrgyOfficialsDGV.DefaultCellStyle.Font = New Font("Arial", 10)
        BrgyOfficialsDGV.DefaultCellStyle.ForeColor = Color.Black
        BrgyOfficialsDGV.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        BrgyOfficialsDGV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        BrgyOfficialsDGV.DefaultCellStyle.Padding = New Padding(5)

        ' === ALTERNATE ROW COLOR ===
        BrgyOfficialsDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)
        BrgyOfficialsDGV.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black

        ' === SELECTION STYLING ===
        BrgyOfficialsDGV.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        BrgyOfficialsDGV.DefaultCellStyle.SelectionForeColor = Color.Black

        ' === ADD COLUMNS ===
        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "OfficialId",
            .DataPropertyName = "OfficialId",
            .Visible = False
        })

        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FullName",
            .HeaderText = "Full Name",
            .DataPropertyName = "FullName",
            .Width = 200,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        })

        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Position",
            .HeaderText = "Position",
            .DataPropertyName = "Position",
            .Width = 200,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        })

        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "TermStart",
            .HeaderText = "Term Start",
            .DataPropertyName = "TermStart",
            .Width = 120,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        })

        BrgyOfficialsDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "TermEnd",
            .HeaderText = "Term End",
            .DataPropertyName = "TermEnd",
            .Width = 120,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        })
    End Sub

    ''' <summary>
    ''' Load barangay information
    ''' </summary>
    Private Sub LoadBarangayInfo()
        Try
            Dim dataTable As DataTable = brgyInfoLogic.GetBarangayInfo()
            If dataTable.Rows.Count > 0 Then
                Dim row As DataRow = dataTable.Rows(0)
                txtBrgyName.Text = row("BarangayName").ToString()
                BrgyMissionRtxt.Text = row("Mission").ToString()
                BrgyVisionRtxt.Text = row("Vision").ToString()

                ' === LOAD LOGO ===
                If Not IsDBNull(row("Logo")) Then
                    Dim logoBytes As Byte() = CType(row("Logo"), Byte())
                    If logoBytes.Length > 0 Then
                        Using ms As New IO.MemoryStream(logoBytes)
                            BrgyLogoPic.Image = Image.FromStream(ms)
                        End Using
                    End If
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine("Error loading barangay info: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Load all officials from database
    ''' </summary>
    Private Sub LoadAllOfficials()
        Try
            Dim dataTable As DataTable = brgyInfoLogic.GetAllBarangayOfficials()
            BrgyOfficialsDGV.DataSource = dataTable
        Catch ex As Exception
            MsgBox("Error loading officials: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Apply gradient background to panel
    ''' </summary>
    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)

        Dim brush As New LinearGradientBrush(
            New Point(0, 0),
            New Point(pnl.Width, 0),
            startColor,
            endColor
        )

        Dim panelLocal = pnl

        AddHandler panelLocal.Paint, Sub(sender, e)
                                         e.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                                     End Sub
    End Sub

    ''' <summary>
    ''' Apply rounded corners to button (with resize handler)
    ''' </summary>
    Private Sub RoundButtonCorners(ByRef btn As Button, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        btn.Region = New Region(p)

        Dim btnLocal = btn

        AddHandler btn.Resize, Sub(s, args)
                                   Dim newPath As New GraphicsPath()
                                   newPath.AddArc(0, 0, radius, radius, 180, 90)
                                   newPath.AddArc(btnLocal.Width - radius, 0, radius, radius, 270, 90)
                                   newPath.AddArc(btnLocal.Width - radius, btnLocal.Height - radius, radius, radius, 0, 90)
                                   newPath.AddArc(0, btnLocal.Height - radius, radius, radius, 90, 90)
                                   newPath.CloseFigure()
                                   btnLocal.Region = New Region(newPath)
                               End Sub
    End Sub

    ''' <summary>
    ''' Upload Logo button click
    ''' </summary>
    Private Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click
        Try
            Using openFileDialog As New OpenFileDialog()
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
                openFileDialog.Title = "Select Barangay Logo"

                If openFileDialog.ShowDialog() = DialogResult.OK Then
                    logoFilePath = openFileDialog.FileName
                    BrgyLogoPic.Image = Image.FromFile(logoFilePath)
                    BtnRemove.Enabled = True
                    MsgBox("Logo loaded successfully.", MsgBoxStyle.Information, "Success")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error loading image: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Remove Logo button click
    ''' </summary>
    Private Sub BtnRemove_Click(sender As Object, e As EventArgs) Handles BtnRemove.Click
        BrgyLogoPic.Image = Nothing
        logoFilePath = ""
        BtnRemove.Enabled = False
    End Sub

    ''' <summary>
    ''' Save Barangay Info button click
    ''' </summary>
    Private Sub BtnSaveInfo_Click(sender As Object, e As EventArgs) Handles BtnSaveInfo.Click
        Try
            ' === GET INPUTS ===
            Dim brgyName As String = txtBrgyName.Text.Trim()
            Dim mission As String = BrgyMissionRtxt.Text.Trim()
            Dim vision As String = BrgyVisionRtxt.Text.Trim()

            ' === CALL SERVICE ===
            Dim result As BrgyInfoAddingLogic.BrgyInfoResult = brgyInfoLogic.SaveBarangayInfo(brgyName, mission, vision, logoFilePath)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                ' === CLEAR LOGO PATH AFTER SAVING ===
                logoFilePath = ""
                BtnRemove.Enabled = False
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Search Officials button click
    ''' </summary>
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Try
            If String.IsNullOrWhiteSpace(TxtSearchOfficial.Text) Then
                LoadAllOfficials()
            Else
                Dim dataTable As DataTable = brgyInfoLogic.SearchBarangayOfficials(TxtSearchOfficial.Text)
                BrgyOfficialsDGV.DataSource = dataTable
            End If
        Catch ex As Exception
            MsgBox("Error searching officials: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Add New Official button click
    ''' </summary>
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
            Debug.WriteLine("BtnAddNewOfficial_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Edit Selected Official button click
    ''' </summary>
    Private Sub BtnEditSelected_Click(sender As Object, e As EventArgs) Handles BtnEditSelected.Click
        Try
            ' === CHECK IF OFFICIAL IS SELECTED ===
            If BrgyOfficialsDGV.SelectedRows.Count = 0 Then
                MsgBox("Please select an official to edit.", MsgBoxStyle.Information, "Selection Required")
                Return
            End If

            ' === GET SELECTED OFFICIAL ID ===
            selectedOfficialId = CInt(BrgyOfficialsDGV.SelectedRows(0).Cells("OfficialId").Value)

            ' === NAVIGATE TO EDIT FORM ===
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim brgyOfficialsForm As New BrgyOfficials_Form(selectedOfficialId) ' Pass ID to edit
                Dashboard_Layout.CurrentInstance.LoadContentPanel(brgyOfficialsForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("BtnEditSelected_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Archive Selected Official button click
    ''' </summary>
    Private Sub BtnArchieveSelected_Click(sender As Object, e As EventArgs) Handles BtnArchieveSelected.Click
        Try
            ' === CHECK IF OFFICIAL IS SELECTED ===
            If BrgyOfficialsDGV.SelectedRows.Count = 0 Then
                MsgBox("Please select an official to archive.", MsgBoxStyle.Information, "Selection Required")
                Return
            End If

            ' === GET SELECTED OFFICIAL ID ===
            selectedOfficialId = CInt(BrgyOfficialsDGV.SelectedRows(0).Cells("OfficialId").Value)
            Dim officialName As String = BrgyOfficialsDGV.SelectedRows(0).Cells("FullName").Value.ToString()

            ' === CONFIRM ARCHIVE ===
            If MsgBox("Archive " & officialName & "? They will not appear in the list.", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.No Then
                Return
            End If

            ' === CALL SERVICE ===
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
            Debug.WriteLine("BtnArchieveSelected_Click Error: " & ex.Message)
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