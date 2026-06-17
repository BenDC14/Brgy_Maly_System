Public Class HouseholdViewFamily_Form
    Private viewingHouseholdId As Integer = -1
    Private selectedFamilyId As Integer = -1
    Private responsiveManager As HouseholdViewFamilyResponsiveManager
    Private familyLogic As New HouseholdViewFamilyLogic()

    Public Sub New(Optional householdId As Integer = -1)
        InitializeComponent()
        viewingHouseholdId = householdId
    End Sub

    Private Sub HouseholdViewFamily_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            UIUtilities.ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            UIUtilities.RoundButtonCorners(btnEditFamily, 20)
            UIUtilities.RoundButtonCorners(btnBack, 20)

            responsiveManager = New HouseholdViewFamilyResponsiveManager(Me)
            responsiveManager.Initialize()

            ConfigureFamilyHeadsGrid()
            LoadFamiliesForHousehold()

        Catch ex As Exception
            MsgBox("Error loading view family form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ConfigureFamilyHeadsGrid()
        dgvFamilyHeads.AutoGenerateColumns = False
        dgvFamilyHeads.Columns.Clear()
        dgvFamilyHeads.AllowUserToAddRows = False
        dgvFamilyHeads.AllowUserToDeleteRows = False
        dgvFamilyHeads.AllowUserToResizeRows = False
        dgvFamilyHeads.RowHeadersVisible = False
        dgvFamilyHeads.ReadOnly = True
        dgvFamilyHeads.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFamilyHeads.MultiSelect = False
        dgvFamilyHeads.EnableHeadersVisualStyles = False
        dgvFamilyHeads.BackgroundColor = Color.FromArgb(220, 220, 220)
        dgvFamilyHeads.GridColor = Color.FromArgb(180, 180, 180)
        dgvFamilyHeads.ColumnHeadersHeight = 35
        dgvFamilyHeads.RowTemplate.Height = 32
        dgvFamilyHeads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        dgvFamilyHeads.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        dgvFamilyHeads.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvFamilyHeads.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
        dgvFamilyHeads.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgvFamilyHeads.DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Regular)
        dgvFamilyHeads.DefaultCellStyle.ForeColor = Color.Black
        dgvFamilyHeads.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dgvFamilyHeads.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        dgvFamilyHeads.DefaultCellStyle.SelectionForeColor = Color.Black

        dgvFamilyHeads.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)

        dgvFamilyHeads.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FamilyId",
            .HeaderText = "Family ID",
            .DataPropertyName = "FamilyId",
            .Width = 90
        })

        dgvFamilyHeads.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "ResidentId",
            .HeaderText = "Resident ID",
            .DataPropertyName = "ResidentId",
            .Visible = False
        })

        dgvFamilyHeads.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FamilyName",
            .HeaderText = "Family Name",
            .DataPropertyName = "FamilyName",
            .Width = 180
        })

        dgvFamilyHeads.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FamilyHead",
            .HeaderText = "Family Head",
            .DataPropertyName = "FamilyHead",
            .Width = 250
        })

        dgvFamilyHeads.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "HouseholdNumber",
            .HeaderText = "Household Number",
            .DataPropertyName = "HouseholdNumber",
            .Width = 160
        })

        dgvFamilyHeads.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "TotalFamilyMembers",
            .HeaderText = "Total Members",
            .DataPropertyName = "TotalFamilyMembers",
            .Width = 130
        })

        AddHandler dgvFamilyHeads.SelectionChanged, AddressOf dgvFamilyHeads_SelectionChanged
        AddHandler dgvFamilyHeads.CellDoubleClick, AddressOf dgvFamilyHeads_CellDoubleClick
    End Sub

    Private Sub LoadFamiliesForHousehold()
        Try
            Dim dataTable As DataTable = familyLogic.GetFamilyHeads(viewingHouseholdId)
            dgvFamilyHeads.DataSource = dataTable
            selectedFamilyId = -1

        Catch ex As Exception
            MsgBox("Error loading family heads: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub dgvFamilyHeads_SelectionChanged(sender As Object, e As EventArgs)
        Try
            If dgvFamilyHeads.SelectedRows.Count = 0 Then
                selectedFamilyId = -1
                Return
            End If

            selectedFamilyId = CInt(dgvFamilyHeads.SelectedRows(0).Cells("FamilyId").Value)

        Catch ex As Exception
            selectedFamilyId = -1
            Debug.WriteLine("dgvFamilyHeads_SelectionChanged Error: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvFamilyHeads_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return

        dgvFamilyHeads.ClearSelection()
        dgvFamilyHeads.Rows(e.RowIndex).Selected = True
        selectedFamilyId = CInt(dgvFamilyHeads.Rows(e.RowIndex).Cells("FamilyId").Value)

        OpenEditFamilyForm()
    End Sub

    Private Sub btnEditFamily_Click(sender As Object, e As EventArgs) Handles btnEditFamily.Click
        OpenEditFamilyForm()
    End Sub

    Private Sub OpenEditFamilyForm()
        Try
            If selectedFamilyId <= 0 Then
                MsgBox("Please select a family head first.", MsgBoxStyle.Information, "Selection Required")
                Return
            End If

            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdEditFamilyForm As New HouseholdEditFamily_Form(selectedFamilyId)
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdEditFamilyForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error loading edit family form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim householdMainForm As New HouseholdMain_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(householdMainForm)
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If

        MyBase.OnFormClosing(e)
    End Sub
End Class