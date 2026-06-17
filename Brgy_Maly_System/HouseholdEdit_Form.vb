Imports System.Data
Imports System.Drawing.Drawing2D

Public Class HouseholdEdit_Form

    ' === Service / Logic layer instance ===
    Private householdEditLogic As New HouseholdEdit_Logic()

    ' === Responsive layout manager ===
    Private responsiveManager As HouseholdEditResponsiveManager

    ' === State ===
    Private editingHouseholdId As Integer = -1

    Private householdData As HouseholdEdit_Logic.HouseholdAddressData = Nothing

    ' ─── Constructor ─────────────────────────────────────────────────────────────

    Public Sub New(Optional householdId As Integer = -1)
        InitializeComponent()
        editingHouseholdId = householdId
    End Sub

    ' ─── Form Load ───────────────────────────────────────────────────────────────

    Private Sub HouseholdEdit_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            RoundButtonCorners(BtnEditHousehold, 20)
            RoundButtonCorners(btnBack, 20)
            RoundButtonCorners(btnSearchHeads, 16)
            RoundButtonCorners(BtnSearchResident, 16)

            responsiveManager = New HouseholdEditResponsiveManager(Me)
            responsiveManager.Initialize()

            ConfigureResidentGrid(FamilyHeadsDGV)
            ConfigureResidentGrid(ResidentInHouseholdDGV)

            LoadHouseholdData()
            LoadFamilyHeads()
            LoadNonHeadResidents()

        Catch ex As Exception
            MsgBox("Error loading household edit form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("HouseholdEdit_Form_Load Error: " & ex.Message)
        End Try
    End Sub

    ' ─── Address / household fields ──────────────────────────────────────────────

    Private Sub LoadHouseholdData()
        Try
            If editingHouseholdId <= 0 Then
                MsgBox("Invalid household selected.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            ' Call through the INSTANCE variable, not the class name
            householdData = householdEditLogic.GetHouseholdById(editingHouseholdId)

            If householdData Is Nothing OrElse householdData.HouseholdID <= 0 Then
                MsgBox("Household not found.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            txtHouseholdNumber.Text = householdData.HouseholdNumber
            txtHouseNumber.Text = householdData.HouseNumber
            txtBlockNumber.Text = householdData.BlockNumber
            txtLotNumber.Text = householdData.LotNumber
            txtAreaNumber.Text = householdData.AreaNumber
            txStreetName.Text = householdData.StreetName
            txtVillage.Text = householdData.Village
            txtSubdivision.Text = householdData.Subdivision
            txtCompound.Text = householdData.Compound
            txtBarangay.Text = householdData.Barangay
            txtMunicipality.Text = householdData.Municipality
            txtProvince.Text = householdData.Province

        Catch ex As Exception
            MsgBox("Error loading household data: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadHouseholdData Error: " & ex.Message)
        End Try
    End Sub

    ' ─── Family Heads DataGridView ────────────────────────────────────────────────

    Private Sub LoadFamilyHeads()
        Try
            If editingHouseholdId <= 0 Then Return

            ' ✅ Use the instance variable "householdEditLogic", NOT the class name
            FamilyHeadsDGV.DataSource = householdEditLogic.GetFamilyHeadsByHousehold(
    editingHouseholdId,
    txtSearchFamilyHeads.Text.Trim()
)

            FormatResidentGridColumns(FamilyHeadsDGV)

        Catch ex As Exception
            MsgBox("Error loading family heads: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadFamilyHeads Error: " & ex.Message)
        End Try
    End Sub

    ' ─── Non-Head Residents DataGridView ─────────────────────────────────────────

    Private Sub LoadNonHeadResidents()
        Try
            If editingHouseholdId <= 0 Then Return

            ' ✅ Use the instance variable "householdEditLogic", NOT the class name
            ResidentInHouseholdDGV.DataSource = householdEditLogic.GetNonHeadResidentsByHousehold(
                editingHouseholdId,
                TxtSearchResidents.Text.Trim()
            )

            FormatResidentGridColumns(ResidentInHouseholdDGV)

        Catch ex As Exception
            MsgBox("Error loading residents: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LoadNonHeadResidents Error: " & ex.Message)
        End Try
    End Sub

    ' ─── DataGridView Configuration ──────────────────────────────────────────────

    Private Sub ConfigureResidentGrid(dgv As DataGridView)
        dgv.AutoGenerateColumns = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.AllowUserToResizeRows = False
        dgv.RowHeadersVisible = False
        dgv.ReadOnly = False
        dgv.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.EnableHeadersVisualStyles = False
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        dgv.BackgroundColor = Color.FromArgb(220, 220, 220)
        dgv.GridColor = Color.FromArgb(180, 180, 180)

        dgv.ColumnHeadersHeight = 35
        dgv.RowTemplate.Height = 30

        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold)
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgv.DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Regular)
        dgv.DefaultCellStyle.ForeColor = Color.Black
        dgv.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        dgv.DefaultCellStyle.SelectionForeColor = Color.Black
        dgv.DefaultCellStyle.Padding = New Padding(4)

        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black
    End Sub

    Private Sub FormatResidentGridColumns(dgv As DataGridView)
        If dgv.Columns.Contains("ResidentId") Then
            dgv.Columns("ResidentId").Visible = False
        End If

        SetColumnHeader(dgv, "FullName", "Full Name", 180)
        SetColumnHeader(dgv, "FirstName", "First Name", 120)
        SetColumnHeader(dgv, "MiddleName", "Middle Name", 120)
        SetColumnHeader(dgv, "LastName", "Last Name", 120)
        SetColumnHeader(dgv, "Sex", "Sex", 70)
        SetColumnHeader(dgv, "CivilStatus", "Civil Status", 100)
        SetColumnHeader(dgv, "ContactNumber", "Contact Number", 130)
        SetColumnHeader(dgv, "HouseholdNumber", "Household Number", 140)
    End Sub

    Private Sub SetColumnHeader(dgv As DataGridView, columnName As String,
                                headerText As String, width As Integer)
        If dgv.Columns.Contains(columnName) Then
            dgv.Columns(columnName).HeaderText = headerText
            dgv.Columns(columnName).Width = width
        End If
    End Sub

    ' ─── Search event handlers ────────────────────────────────────────────────────

    Private Sub btnSearchHeads_Click(sender As Object, e As EventArgs) Handles btnSearchHeads.Click
        LoadFamilyHeads()
    End Sub

    Private Sub BtnSearchResident_Click(sender As Object, e As EventArgs) Handles BtnSearchResident.Click
        LoadNonHeadResidents()
    End Sub

    Private Sub txtSearchFamilyHeads_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchFamilyHeads.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadFamilyHeads()
        End If
    End Sub

    Private Sub TxtSearchResidents_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSearchResidents.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadNonHeadResidents()
        End If
    End Sub

    ' ─── Save (Edit Household) ────────────────────────────────────────────────────

    Private Sub BtnEditHousehold_Click(sender As Object, e As EventArgs) Handles BtnEditHousehold.Click
        Try
            If householdData Is Nothing OrElse householdData.AddressID <= 0 Then
                MsgBox("No valid household address was loaded.", MsgBoxStyle.Exclamation, "Error")
                Return
            End If

            FamilyHeadsDGV.EndEdit()
            ResidentInHouseholdDGV.EndEdit()

            ' ✅ Type qualifier uses the CLASS name "HouseholdEditLogic" (no underscore)
            Dim updatedData As New HouseholdEdit_Logic.HouseholdAddressData With {
                .HouseholdID = editingHouseholdId,
                .HouseholdNumber = txtHouseholdNumber.Text.Trim(),
                .AddressID = householdData.AddressID,
                .HouseNumber = txtHouseNumber.Text.Trim(),
                .BlockNumber = txtBlockNumber.Text.Trim(),
                .LotNumber = txtLotNumber.Text.Trim(),
                .AreaNumber = txtAreaNumber.Text.Trim(),
                .StreetName = txStreetName.Text.Trim(),
                .Village = txtVillage.Text.Trim(),
                .Subdivision = txtSubdivision.Text.Trim(),
                .Compound = txtCompound.Text.Trim(),
                .Barangay = txtBarangay.Text.Trim(),
                .Municipality = txtMunicipality.Text.Trim(),
                .Province = txtProvince.Text.Trim()
            }

            ' ✅ Call through the INSTANCE variable; result type uses the CLASS name
            Dim result As HouseholdEdit_Logic.HouseholdEditResult =
    householdEditLogic.UpdateAddress(updatedData)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                NavigateBackToHouseholdMain()
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("BtnEditHousehold_Click Error: " & ex.Message)
        End Try
    End Sub

    ' ─── Navigation ──────────────────────────────────────────────────────────────

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        NavigateBackToHouseholdMain()
    End Sub

    Private Sub NavigateBackToHouseholdMain()
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdMainForm As New HouseholdMain_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdMainForm)
            End If
        Catch ex As Exception
            MsgBox("Error loading household main form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("NavigateBackToHouseholdMain Error: " & ex.Message)
        End Try
    End Sub

    ' ─── Visual Helpers ──────────────────────────────────────────────────────────

    Private Sub ApplyGradient(pnl As Control, startColorHex As String, endColorHex As String)
        Try
            Dim startColor = ColorTranslator.FromHtml(startColorHex)
            Dim endColor = ColorTranslator.FromHtml(endColorHex)

            AddHandler pnl.Paint,
                Sub(s, ev)
                    Using brush As New LinearGradientBrush(
                        New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
                        ev.Graphics.FillRectangle(brush, pnl.ClientRectangle)
                    End Using
                End Sub

        Catch ex As Exception
            Debug.WriteLine("ApplyGradient Error: " & ex.Message)
        End Try
    End Sub

    Private Sub RoundButtonCorners(btn As Button, radius As Integer)
        Try
            If btn Is Nothing Then Return
            ApplyButtonRoundedRegion(btn, radius)
            AddHandler btn.Resize, Sub(s, ev) ApplyButtonRoundedRegion(btn, radius)
        Catch ex As Exception
            Debug.WriteLine("RoundButtonCorners Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ApplyButtonRoundedRegion(btn As Button, radius As Integer)
        Try
            If btn Is Nothing OrElse btn.Width <= 0 OrElse btn.Height <= 0 Then Return

            Dim safeRadius As Integer = Math.Min(radius, Math.Min(btn.Width, btn.Height))

            Using p As New GraphicsPath()
                p.AddArc(0, 0, safeRadius, safeRadius, 180, 90)
                p.AddArc(btn.Width - safeRadius, 0, safeRadius, safeRadius, 270, 90)
                p.AddArc(btn.Width - safeRadius, btn.Height - safeRadius, safeRadius, safeRadius, 0, 90)
                p.AddArc(0, btn.Height - safeRadius, safeRadius, safeRadius, 90, 90)
                p.CloseFigure()
                btn.Region = New Region(p)
            End Using

        Catch ex As Exception
            Debug.WriteLine("ApplyButtonRoundedRegion Error: " & ex.Message)
        End Try
    End Sub

    ' ─── Cleanup ─────────────────────────────────────────────────────────────────

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        Try
            If responsiveManager IsNot Nothing Then responsiveManager.Cleanup()
        Catch ex As Exception
            Debug.WriteLine("OnFormClosing Cleanup Error: " & ex.Message)
        End Try
        MyBase.OnFormClosing(e)
    End Sub

End Class
