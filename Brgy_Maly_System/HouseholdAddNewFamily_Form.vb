' ================================================================================
' FILE: HouseholdAddNewFamily_Form.vb
' LAYER: Main Form UI Events Layer
'
' DESIGNER CONTROL MAP:
'   txtHousehold      → displays HouseholdNumber (read-only)
'   txtFamilyName     → user-entered family name
'   txtFamilyHead     → HIDDEN at runtime; replaced by cbFamilyHead (dynamic CB)
'   cbCivilStatus     → relationship type selector (labeled "Relationship Type:")
'   BtnSaveRelationship → save the edited relationship (starts disabled)
'   btnSaveFamily     → save new family record
'   btnBack           → navigate back to HouseholdMain_Form
'   FamilyMembersDGV  → shows members after family is saved; has "Edit Relationship" btn column
' ================================================================================
Imports System.Drawing.Drawing2D

Public Class HouseholdAddNewFamily_Form

    ' ── Infrastructure ───────────────────────────────────────────────────────────
    Private responsiveManager As HouseholdAddNewFamilyResponsiveManager
    Private ReadOnly familyLogic As New HouseholdAddingNewFamilyLogic()

    ' ── Dynamic ComboBox for Family Head (designer has only a TextBox here) ──────
    Friend cbFamilyHead As New ComboBox()   ' overlaid over txtFamilyHead position

    ' ── Form-level state ─────────────────────────────────────────────────────────
    Private householdIdForFamily As Integer = -1   ' injected by caller
    Private currentFamilyId As Integer = -1   ' set after Save Family succeeds
    Private editingMemberId As Integer = -1   ' FamilyMemberId being rel-edited

    ' ============================================================================
    ' CONSTRUCTOR
    ' ============================================================================
    Public Sub New(Optional householdId As Integer = -1)
        InitializeComponent()
        householdIdForFamily = householdId
    End Sub

    ' ============================================================================
    ' FORM LOAD
    ' ============================================================================
    Private Sub HouseholdAddNewFamily_Form_Load(sender As Object, e As EventArgs) _
            Handles MyBase.Load
        Try
            ' Visual polish
            UIUtilities.ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")
            UIUtilities.RoundButtonCorners(btnSaveFamily, 20)
            UIUtilities.RoundButtonCorners(BtnSaveRelationship, 20)
            UIUtilities.RoundButtonCorners(btnBack, 20)

            ' Hide the designer TextBox; the dynamic CB will sit in its place
            familyheadcb.Visible = True

            ' Build and register the dynamic Family Head combobox
            InitialiseFamilyHeadComboBox()

            ' Responsive layout (must come before data loads so CB is sized)
            responsiveManager = New HouseholdAddNewFamilyResponsiveManager(Me)
            responsiveManager.Initialize()

            ' Configure grid columns
            ConfigureFamilyMembersGrid()

            ' Set relationship controls to disabled until a row is being edited
            cbRelationshipType.Enabled = False
            BtnSaveRelationship.Enabled = False

            ' Validate household
            If householdIdForFamily <= 0 Then
                MsgBox("No household selected. Please go back and select a household.",
                       MsgBoxStyle.Information, "Info")
                Return
            End If

            ' Load data
            LoadHouseholdDisplay()
            PopulateRelationshipComboBox()
            PopulateFamilyHeadComboBox()

        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' DYNAMIC COMBOBOX — Family Head
    ' Overlaid over the designer's txtFamilyHead TextBox.
    ' The responsive manager will reposition it in sync with txtFamilyHead.
    ' ============================================================================
    Private Sub InitialiseFamilyHeadComboBox()
        With familyheadcb
            .Name = "cbFamilyHead"
            .DropDownStyle = ComboBoxStyle.DropDownList
            .BackColor = Color.FromArgb(237, 237, 237)
            .Font = New Font("Arial", 12, FontStyle.Regular)
            .Location = familyheadcb.Location
            .Size = familyheadcb.Size
            .Enabled = True
        End With
        FillPanel.Controls.Add(familyheadcb)
        familyheadcb.BringToFront()
    End Sub

    ' ============================================================================
    ' DATA LOAD HELPERS
    ' ============================================================================

    Private Sub LoadHouseholdDisplay()
        Try
            Dim hhNumber As String =
                familyLogic.GetHouseholdNumber(householdIdForFamily)
            txtHousehold.Text = hhNumber
            txtHousehold.ReadOnly = True
            txtHousehold.BackColor = Color.FromArgb(225, 225, 225)
        Catch ex As Exception
            MsgBox("Error loading household: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Fills cbFamilyHead with all non-archived residents of the household
    ''' who are not already a family head in another family within this household.
    ''' </summary>
    Private Sub PopulateFamilyHeadComboBox()
        Try
            familyheadcb.Items.Clear()
            ' ── Call the BLL to get eligible residents for this household ──
            Dim dt As DataTable = familyLogic.GetEligibleFamilyHeads(householdIdForFamily)
            For Each row As DataRow In dt.Rows
                ' Wrap each row: DisplayText = FullName, ValueKey = ResidentId
                familyheadcb.Items.Add(New ResidentItem(
                CInt(row("ResidentId")),
                row("FullName").ToString()))
            Next
            ' Default-select first item; show blank if no eligible residents found
            If familyheadcb.Items.Count > 0 Then
                familyheadcb.SelectedIndex = 0
            Else
                familyheadcb.SelectedIndex = -1
            End If
        Catch ex As Exception
            MsgBox("Error loading potential family heads: " & ex.Message,
               MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Fills cbCivilStatus (the "Relationship Type" combobox) from the
    ''' relationship table. Clears any static Designer items first.
    ''' </summary>
    Private Sub PopulateRelationshipComboBox()
        Try
            cbRelationshipType.Items.Clear()
            Dim dt As DataTable = familyLogic.GetRelationshipTypes()
            For Each row As DataRow In dt.Rows
                cbRelationshipType.Items.Add(New RelationshipItem(
                    CInt(row("RelationshipId")), row("Relationship").ToString()))
            Next
            If cbRelationshipType.Items.Count > 0 Then
                cbRelationshipType.SelectedIndex = 0
            End If
        Catch ex As Exception
            MsgBox("Error loading relationship types: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Loads (or refreshes) FamilyMembersDGV with the members of the
    ''' currently saved family (currentFamilyId).
    ''' </summary>
    Private Sub LoadFamilyMembers()
        Try
            If currentFamilyId <= 0 Then Return
            FamilyMembersDGV.DataSource = familyLogic.GetFamilyMembers(currentFamilyId)
        Catch ex As Exception
            MsgBox("Error loading family members: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' GRID CONFIGURATION
    ' ============================================================================
    Private Sub ConfigureFamilyMembersGrid()
        With FamilyMembersDGV
            .AutoGenerateColumns = False
            .Columns.Clear()
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .RowHeadersVisible = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .EnableHeadersVisualStyles = False
            .BackgroundColor = Color.FromArgb(220, 220, 220)
            .GridColor = Color.FromArgb(180, 180, 180)
            .ColumnHeadersHeight = 35
            .RowTemplate.Height = 32
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Regular)
            .DefaultCellStyle.ForeColor = Color.Black
            .DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)
        End With

        ' Hidden key column
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FamilyMemberId", .HeaderText = "FamilyMemberId",
            .DataPropertyName = "FamilyMemberId", .Visible = False
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "ResidentId", .HeaderText = "ResidentId",
            .DataPropertyName = "ResidentId", .Visible = False
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "IsHead", .HeaderText = "IsHead",
            .DataPropertyName = "IsHead", .Visible = False
        })

        ' Visible columns
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FullName", .HeaderText = "Full Name",
            .DataPropertyName = "FullName", .FillWeight = 35
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "RelationshipType", .HeaderText = "Relationship",
            .DataPropertyName = "RelationshipType", .FillWeight = 20
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Sex", .HeaderText = "Sex",
            .DataPropertyName = "Sex", .FillWeight = 10
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "CivilStatus", .HeaderText = "Civil Status",
            .DataPropertyName = "CivilStatus", .FillWeight = 15
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "ContactNumber", .HeaderText = "Contact",
            .DataPropertyName = "ContactNumber", .FillWeight = 15
        })

        ' "Edit Relationship" button column
        Dim colEditRel As New DataGridViewButtonColumn With {
            .Name = "colEditRelationship",
            .HeaderText = "Edit Relationship",
            .Text = "Edit Relationship",
            .UseColumnTextForButtonValue = True,
            .FlatStyle = FlatStyle.Flat,
            .FillWeight = 15
        }
        colEditRel.DefaultCellStyle.BackColor = Color.FromArgb(74, 144, 226)
        colEditRel.DefaultCellStyle.ForeColor = Color.White
        colEditRel.DefaultCellStyle.Font = New Font("Arial Narrow", 10, FontStyle.Bold)
        colEditRel.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        FamilyMembersDGV.Columns.Add(colEditRel)
    End Sub

    ' ============================================================================
    ' BUTTON: Save Family
    ' ============================================================================
    Private Sub btnSaveFamily_Click(sender As Object, e As EventArgs) _
            Handles btnSaveFamily.Click
        Try
            ' Validation
            If String.IsNullOrWhiteSpace(txtFamilyName.Text) Then
                MsgBox("Please enter a Family Name.", MsgBoxStyle.Exclamation, "Validation")
                txtFamilyName.Focus() : Return
            End If
            If familyheadcb.SelectedIndex < 0 OrElse familyheadcb.SelectedItem Is Nothing Then
                MsgBox("Please select a Family Head.", MsgBoxStyle.Exclamation, "Validation")
                familyheadcb.Focus() : Return
            End If
            If householdIdForFamily <= 0 Then
                MsgBox("No household is linked to this form.",
                       MsgBoxStyle.Exclamation, "Validation")
                Return
            End If

            Dim selectedHead As ResidentItem =
                DirectCast(familyheadcb.SelectedItem, ResidentItem)

            Dim result As HouseholdAddingNewFamilyLogic.SaveResult =
                familyLogic.InsertFamily(
                    householdIdForFamily,
                    txtFamilyName.Text.Trim(),
                    selectedHead.ResidentId)

            If result.IsSuccess Then
                currentFamilyId = result.FamilyId
                MsgBox("Family saved successfully.", MsgBoxStyle.Information, "Success")
                LoadFamilyMembers()
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error saving family: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' DGV: "Edit Relationship" button click
    ' Unlock the relationship controls and track which member is being edited.
    ' ============================================================================
    Private Sub FamilyMembersDGV_CellContentClick(sender As Object,
                                                   e As DataGridViewCellEventArgs) _
            Handles FamilyMembersDGV.CellContentClick
        If e.RowIndex < 0 Then Return
        If FamilyMembersDGV.Columns(e.ColumnIndex).Name <> "colEditRelationship" Then Return

        Dim row As DataGridViewRow = FamilyMembersDGV.Rows(e.RowIndex)

        ' Guard: Family Head row is managed elsewhere
        Dim isHead As Boolean = CBool(If(row.Cells("IsHead").Value, False))
        If isHead Then
            MsgBox("The Family Head's relationship cannot be changed here.",
                   MsgBoxStyle.Information, "Info")
            Return
        End If

        ' Track which member is being edited
        editingMemberId = CInt(row.Cells("FamilyMemberId").Value)

        ' Pre-select the current relationship in the combobox
        Dim currentRel As String = row.Cells("RelationshipType").Value?.ToString()
        Dim foundAt As Integer = -1
        For i As Integer = 0 To cbRelationshipType.Items.Count - 1
            Dim item As RelationshipItem = TryCast(cbRelationshipType.Items(i), RelationshipItem)
            If item IsNot Nothing AndAlso
               String.Equals(item.ToString(), currentRel, StringComparison.OrdinalIgnoreCase) Then
                foundAt = i : Exit For
            End If
        Next
        cbRelationshipType.SelectedIndex = If(foundAt >= 0, foundAt,
                                          If(cbRelationshipType.Items.Count > 0, 0, -1))

        ' Unlock relationship controls
        cbRelationshipType.Enabled = True
        BtnSaveRelationship.Enabled = True

    End Sub

    ' ============================================================================
    ' BUTTON: Save Relationship
    ' ============================================================================
    Private Sub BtnSaveRelationship_Click(sender As Object, e As EventArgs) _
            Handles BtnSaveRelationship.Click
        Try
            If editingMemberId <= 0 Then
                MsgBox("No member selected for relationship edit.",
                       MsgBoxStyle.Information, "Info")
                Return
            End If
            If cbRelationshipType.SelectedIndex < 0 Then
                MsgBox("Please select a relationship type.",
                       MsgBoxStyle.Exclamation, "Validation")
                Return
            End If

            Dim selectedRel As RelationshipItem =
                DirectCast(cbRelationshipType.SelectedItem, RelationshipItem)

            Dim result As HouseholdAddingNewFamilyLogic.SaveResult =
                familyLogic.UpdateMemberRelationship(editingMemberId, selectedRel.ToString())

            If result.IsSuccess Then
                MsgBox("Relationship updated successfully.", MsgBoxStyle.Information, "Success")
                ' Reset state and lock controls
                editingMemberId = -1
                cbRelationshipType.Enabled = False
                BtnSaveRelationship.Enabled = False
                cbRelationshipType.SelectedIndex = -1
                LoadFamilyMembers()
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error saving relationship: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' BUTTON: Back
    ' ============================================================================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dashboard_Layout.CurrentInstance.LoadContentPanel(New HouseholdMain_Form())
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error navigating back: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' CLEANUP
    ' ============================================================================
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then responsiveManager.Cleanup()
        MyBase.OnFormClosing(e)
    End Sub

    ' ============================================================================
    ' PRIVATE HELPER TYPES
    ' ============================================================================

    Private Class ResidentItem
        Public ReadOnly ResidentId As Integer
        Private ReadOnly _name As String
        Public Sub New(id As Integer, name As String)
            ResidentId = id : _name = name
        End Sub
        Public Overrides Function ToString() As String
            Return _name
        End Function
    End Class

    Private Class RelationshipItem
        Public ReadOnly RelationshipId As Integer
        Private ReadOnly _name As String
        Public Sub New(id As Integer, name As String)
            RelationshipId = id : _name = name
        End Sub
        Public Overrides Function ToString() As String
            Return _name
        End Function
    End Class

End Class
