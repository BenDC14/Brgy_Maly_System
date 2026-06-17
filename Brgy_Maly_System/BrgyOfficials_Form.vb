Imports System.Drawing.Drawing2D

Public Class BrgyOfficials_Form
    ' === Service Layer (Business Logic) ===
    Private brgyOfficialsLogic As New BrgyOfficialsLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As BrgyOfficialsResponsiveManager

    ' === UI State ===
    Private photoFilePath As String = ""
    Private editingOfficialId As Integer = -1
    Private isEditMode As Boolean = False
    Private editingIsActive As Boolean = True   ' tracks IsActive of the record being edited

    ''' <summary>
    ''' Constructor - Accept optional official ID for editing
    ''' </summary>
    Public Sub New(Optional officialId As Integer = -1)
        InitializeComponent()
        editingOfficialId = officialId
        isEditMode = (officialId > 0)
    End Sub

    Private Sub BrgyOfficials_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        RoundButtonCorners(BtnUpload, 20)
        RoundButtonCorners(BtnRemove, 20)
        RoundButtonCorners(BtnSaveOfficial, 20)
        RoundButtonCorners(BtnBackToMain, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New BrgyOfficialsResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Load Position ComboBox ===
        LoadPositions()

        ' === Setup Form Mode ===
        If isEditMode Then
            BrgyOfficialLbl.Text = "Edit Official"
            LoadOfficialData(editingOfficialId)
        Else
            BrgyOfficialLbl.Text = "Add New Official"
            ClearForm()
        End If
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
        AddHandler panelLocal.Paint, Sub(s, ev)
                                         ev.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
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
    ''' Load positions into ComboBox
    ''' </summary>
    Private Sub LoadPositions()
        cbPosition.Items.Clear()
        cbPosition.Items.AddRange(New String() {
            "Barangay Captain",
            "Barangay Kagawad",
            "SK Chairman",
            "Barangay Secretary",
            "Barangay Treasurer",
            "Barangay Adminstrator"
        })
    End Sub

    ''' <summary>
    ''' Load official data for editing.
    ''' Also captures the IsActive flag so the quota check can exclude the
    ''' record's own slot when the position is changed during editing.
    ''' </summary>
    Private Sub LoadOfficialData(officialId As Integer)
        Try
            Dim dataTable As DataTable = brgyOfficialsLogic.GetOfficialById(officialId)
            If dataTable.Rows.Count > 0 Then
                Dim row As DataRow = dataTable.Rows(0)
                txtFname.Text = row("FirstName").ToString()
                txtLname.Text = row("LastName").ToString()
                cbPosition.SelectedItem = row("Position").ToString()
                TermStartDTP.Value = CDate(row("TermStart"))
                TermEndDTP.Value = CDate(row("TermEnd"))

                ' Capture the IsActive flag for quota-check logic
                editingIsActive = (Convert.ToInt32(row("IsActive")) = 1)

                ' === LOAD PHOTO ===
                If Not IsDBNull(row("PhotoPath")) Then
                    Dim photoBytes As Byte() = CType(row("PhotoPath"), Byte())
                    If photoBytes.Length > 0 Then
                        Using ms As New IO.MemoryStream(photoBytes)
                            BrgyLogoPic.Image = Image.FromStream(ms)
                        End Using
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Error loading official data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Clear form fields
    ''' </summary>
    Private Sub ClearForm()
        txtFname.Clear()
        txtLname.Clear()
        cbPosition.SelectedIndex = -1
        TermStartDTP.Value = DateTime.Now
        TermEndDTP.Value = DateTime.Now.AddYears(3)
        BrgyLogoPic.Image = Nothing
        photoFilePath = ""
        BtnRemove.Enabled = False
    End Sub

    ''' <summary>
    ''' Upload Photo button click
    ''' </summary>
    Private Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click
        Try
            Using openFileDialog As New OpenFileDialog()
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
                openFileDialog.Title = "Select Official Photo"

                If openFileDialog.ShowDialog() = DialogResult.OK Then
                    photoFilePath = openFileDialog.FileName
                    BrgyLogoPic.Image = Image.FromFile(photoFilePath)
                    BtnRemove.Enabled = True
                    MsgBox("Photo loaded successfully.", MsgBoxStyle.Information, "Success")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error loading image: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Remove Photo button click
    ''' </summary>
    Private Sub BtnRemove_Click(sender As Object, e As EventArgs) Handles BtnRemove.Click
        BrgyLogoPic.Image = Nothing
        photoFilePath = ""
        BtnRemove.Enabled = False
    End Sub

    ''' <summary>
    ''' Save Official button click.
    ''' Quota violations (ErrorCode = 2) are surfaced with the prescribed
    ''' blocking MessageBox before any database write occurs.
    ''' </summary>
    Private Sub BtnSaveOfficial_Click(sender As Object, e As EventArgs) Handles BtnSaveOfficial.Click
        Try
            ' === GET INPUTS ===
            Dim firstName As String = txtFname.Text.Trim()
            Dim lastName As String = txtLname.Text.Trim()
            Dim position As String = cbPosition.SelectedItem?.ToString()
            Dim termStart As DateTime = TermStartDTP.Value
            Dim termEnd As DateTime = TermEndDTP.Value

            ' === CALL SERVICE ===
            Dim result As BrgyOfficialsLogic.OfficialResult

            If isEditMode Then
                ' Pass editingIsActive so quota check knows whether to exclude
                ' this record's own active slot from the count.
                result = brgyOfficialsLogic.UpdateBarangayOfficial(
                             editingOfficialId, firstName, lastName,
                             position, termStart, termEnd,
                             photoFilePath, isActive:=editingIsActive)
            Else
                result = brgyOfficialsLogic.AddBarangayOfficial(
                             firstName, lastName,
                             position, termStart, termEnd,
                             photoFilePath)
            End If

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                ClearForm()
                photoFilePath = ""

                ' === NAVIGATE BACK TO MAIN ===
                If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                    Dim brgyInfoForm As New BrgyInfoAdding_Form()
                    Dashboard_Layout.CurrentInstance.LoadContentPanel(brgyInfoForm)
                End If

            ElseIf result.ErrorCode = 2 Then
                ' ---------------------------------------------------------------
                ' QUOTA LIMIT REACHED — show the prescribed blocking message box
                ' The save operation is already blocked inside the Logic layer.
                ' ---------------------------------------------------------------
                MessageBox.Show(
                    result.Message,
                    "Active Quota Limit Reached",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)
            Else
                ' General validation / DB errors
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Back To Main button click
    ''' </summary>
    Private Sub BtnBackToMain_Click(sender As Object, e As EventArgs) Handles BtnBackToMain.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim brgyInfoForm As New BrgyInfoAdding_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(brgyInfoForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("BtnBackToMain_Click Error: " & ex.Message)
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
