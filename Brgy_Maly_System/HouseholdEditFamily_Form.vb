' ================================================================================
' FILE: HouseholdEditFamily_Form.vb
' LAYER: Main Form UI Events Layer
' ================================================================================
Imports System.Drawing.Drawing2D

Public Class HouseholdEditFamily_Form

    ' ── Infrastructure ───────────────────────────────────────────────────────────
    Private responsiveManager As HouseholdEditFamilyResponsiveManager
    Private ReadOnly familyLogic As New HouseholdEditFamilyLogic()

    ' ── Form-level state ─────────────────────────────────────────────────────────
    Private selectedFamilyId As Integer = -1
    Private selectedHouseholdId As Integer = -1
    Private editingMemberId As Integer = -1
    Private isEditingPosition As Boolean = False

    ' ============================================================================
    ' CONSTRUCTOR
    ' ============================================================================
    Public Sub New(Optional familyId As Integer = -1)
        InitializeComponent()
        selectedFamilyId = familyId
    End Sub

    ' ============================================================================
    ' FORM LOAD
    ' ============================================================================
    Private Sub HouseholdEditFamily_Form_Load(sender As Object, e As EventArgs) _
            Handles MyBase.Load
        Try
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")
            RoundButtonCorners(btnAddNewFamilyMember, 20)
            RoundButtonCorners(btnSaveChanges, 20)
            RoundButtonCorners(btnAddNewResident, 20)
            RoundButtonCorners(btnBack, 20)

            responsiveManager = New HouseholdEditFamilyResponsiveManager(Me)
            responsiveManager.Initialize()

            ConfigureFamilyMembersGrid()
            SetReadOnlyFamilyInfoFields()

            If selectedFamilyId > 0 Then
                LoadFamilyInformation()
                PopulateRelationshipsComboBox()
                LoadFamilyMembers()
            Else
                MsgBox("No family selected.", MsgBoxStyle.Information, "Info")
            End If

        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
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

        ' Hidden key columns
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FamilyMemberId", .HeaderText = "FamilyMemberId",
            .DataPropertyName = "FamilyMemberId", .Visible = False
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "ResidentId", .HeaderText = "Resident ID",
            .DataPropertyName = "ResidentId", .Visible = False
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "IsHead", .HeaderText = "IsHead",
            .DataPropertyName = "IsHead", .Visible = False
        })

        ' Visible data columns
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "FullName", .HeaderText = "Full Name",
            .DataPropertyName = "FullName", .FillWeight = 30
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "RelationshipType", .HeaderText = "Relationship",
            .DataPropertyName = "RelationshipType", .FillWeight = 15
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Sex", .HeaderText = "Sex",
            .DataPropertyName = "Sex", .FillWeight = 8
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "CivilStatus", .HeaderText = "Civil Status",
            .DataPropertyName = "CivilStatus", .FillWeight = 12
        })
        FamilyMembersDGV.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "ContactNumber", .HeaderText = "Contact Number",
            .DataPropertyName = "ContactNumber", .FillWeight = 15
        })

        ' Button column: Edit Member
        Dim colEditMember As New DataGridViewButtonColumn With {
            .Name = "colEditMember", .HeaderText = "Edit Member",
            .Text = "Edit Member", .UseColumnTextForButtonValue = True,
            .FlatStyle = FlatStyle.Flat, .FillWeight = 10
        }
        colEditMember.DefaultCellStyle.BackColor = Color.FromArgb(74, 144, 226)
        colEditMember.DefaultCellStyle.ForeColor = Color.White
        colEditMember.DefaultCellStyle.Font = New Font("Arial Narrow", 10, FontStyle.Bold)
        colEditMember.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        FamilyMembersDGV.Columns.Add(colEditMember)

        ' Button column: Edit Position
        Dim colEditPosition As New DataGridViewButtonColumn With {
            .Name = "colEditPosition", .HeaderText = "Edit Position",
            .Text = "Edit Position", .UseColumnTextForButtonValue = True,
            .FlatStyle = FlatStyle.Flat, .FillWeight = 10
        }
        colEditPosition.DefaultCellStyle.BackColor = Color.FromArgb(159, 190, 168)
        colEditPosition.DefaultCellStyle.ForeColor = Color.Black
        colEditPosition.DefaultCellStyle.Font = New Font("Arial Narrow", 10, FontStyle.Bold)
        colEditPosition.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        FamilyMembersDGV.Columns.Add(colEditPosition)
    End Sub

    ' ============================================================================
    ' DATA LOADING
    ' ============================================================================

    Private Sub SetReadOnlyFamilyInfoFields()
        For Each txt As TextBox In {txtFamilyID, txtHousehold, txtFamilyName,
                                    txtFamilyHead, txtTotalFamilyMembers}
            txt.ReadOnly = True
            txt.BackColor = Color.FromArgb(225, 225, 225)
        Next
    End Sub

    Private Sub LoadFamilyInformation()
        Try
            Dim info As HouseholdEditFamilyLogic.FamilyInformation =
                familyLogic.GetFamilyInformation(selectedFamilyId)

            If info Is Nothing OrElse info.FamilyId <= 0 Then
                MsgBox("Family information not found.", MsgBoxStyle.Exclamation, "Not Found")
                Return
            End If

            selectedHouseholdId = info.HouseholdId

            txtFamilyID.Text = info.FamilyId.ToString()
            txtHousehold.Text = info.HouseholdNumber
            txtFamilyName.Text = info.FamilyName
            txtFamilyHead.Text = info.FamilyHeadFullName
            txtTotalFamilyMembers.Text = info.TotalFamilyMembers.ToString()

            PopulateResidentsComboBox()

        Catch ex As Exception
            MsgBox("Error loading family information: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub LoadFamilyMembers()
        Try
            FamilyMembersDGV.DataSource = familyLogic.GetFamilyMembers(selectedFamilyId)
        Catch ex As Exception
            MsgBox("Error loading family members: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub PopulateRelationshipsComboBox()
        Try
            cbRelationships.Items.Clear()
            Dim dt As DataTable = familyLogic.GetRelationshipTypes()
            For Each row As DataRow In dt.Rows
                cbRelationships.Items.Add(row("Relationship").ToString())
            Next
            If cbRelationships.Items.Count > 0 Then cbRelationships.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Error loading relationship types: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub PopulateResidentsComboBox()
        Try
            cbResidents.Items.Clear()
            If selectedHouseholdId <= 0 Then Return
            Dim dt As DataTable = familyLogic.GetResidentsByHousehold(selectedHouseholdId)
            For Each row As DataRow In dt.Rows
                cbResidents.Items.Add(New ResidentComboItem(
                    CInt(row("ResidentId")), row("FullName").ToString()))
            Next
            cbResidents.SelectedIndex = -1
        Catch ex As Exception
            MsgBox("Error loading residents: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' DGV CELL CLICK — routes to the two handler methods
    ' ============================================================================
    Private Sub FamilyMembersDGV_CellContentClick(sender As Object,
                                                   e As DataGridViewCellEventArgs) _
            Handles FamilyMembersDGV.CellContentClick
        If e.RowIndex < 0 Then Return
        Dim row As DataGridViewRow = FamilyMembersDGV.Rows(e.RowIndex)
        Select Case FamilyMembersDGV.Columns(e.ColumnIndex).Name
            Case "colEditMember" : HandleEditMemberClick(row)
            Case "colEditPosition" : HandleEditPositionClick(row)
        End Select
    End Sub

    ' ────────────────────────────────────────────────────────────────────────────
    ' "Edit Member" — opens HouseholdFamilyResidents_Form in EDIT_MODE.
    ' Passes the ResidentId so the form can load the existing profile.
    ' ────────────────────────────────────────────────────────────────────────────
    Private Sub HandleEditMemberClick(row As DataGridViewRow)
        Try
            Dim residentId As Integer = CInt(row.Cells("ResidentId").Value)

            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim residentsForm As New HouseholdFamilyResidents_Form()

                ' EDIT_MODE: pass the positive ResidentId
                residentsForm.SetMode(
                    HouseholdFamilyResidents_Form.formMode.EDIT_MODE,
                    residentId,
                    selectedHouseholdId,
                    selectedFamilyId)

                Dashboard_Layout.CurrentInstance.LoadContentPanel(residentsForm)
            End If

        Catch ex As Exception
            MsgBox("Error opening resident editor: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ────────────────────────────────────────────────────────────────────────────
    ' "Edit Position" — populates comboboxes, locks cbResidents, tracks member PK
    ' ────────────────────────────────────────────────────────────────────────────
    Private Sub HandleEditPositionClick(row As DataGridViewRow)
        Try
            Dim isHead As Boolean = CBool(If(row.Cells("IsHead").Value, False))
            If isHead Then
                MsgBox("The Family Head's relationship cannot be changed here." &
                       Environment.NewLine &
                       "Use the dedicated Family Head management screen.",
                       MsgBoxStyle.Information, "Edit Position")
                Return
            End If

            Dim clickedMemberId As Integer = CInt(row.Cells("FamilyMemberId").Value)
            Dim clickedResidentId As Integer = CInt(row.Cells("ResidentId").Value)
            Dim currentRel As String = row.Cells("RelationshipType").Value?.ToString()

            ' Auto-select the resident in cbResidents
            Dim foundAt As Integer = -1
            For i As Integer = 0 To cbResidents.Items.Count - 1
                Dim item As ResidentComboItem = TryCast(cbResidents.Items(i), ResidentComboItem)
                If item IsNot Nothing AndAlso item.ResidentId = clickedResidentId Then
                    foundAt = i : Exit For
                End If
            Next
            cbResidents.SelectedIndex = foundAt   ' -1 if not found (edge case)

            ' Set current relationship
            Dim relIdx As Integer = cbRelationships.FindStringExact(currentRel)
            cbRelationships.SelectedIndex = If(relIdx >= 0, relIdx,
                                               If(cbRelationships.Items.Count > 0, 0, -1))

            ' Lock resident identity
            cbResidents.Enabled = False

            ' Track state
            editingMemberId = clickedMemberId
            isEditingPosition = True

        Catch ex As Exception
            MsgBox("Error preparing position edit: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' BUTTON: Add New Family Member (from the comboboxes)
    ' ============================================================================
    Private Sub btnAddNewFamilyMember_Click(sender As Object, e As EventArgs) _
            Handles btnAddNewFamilyMember.Click
        Try
            If isEditingPosition Then
                MsgBox("An 'Edit Position' session is active. Save or cancel it first.",
                       MsgBoxStyle.Exclamation, "Session Active")
                Return
            End If
            If cbResidents.SelectedIndex < 0 OrElse cbResidents.SelectedItem Is Nothing Then
                MsgBox("Please select a resident.", MsgBoxStyle.Exclamation, "Validation")
                Return
            End If
            If cbRelationships.SelectedIndex < 0 Then
                MsgBox("Please select a relationship type.", MsgBoxStyle.Exclamation, "Validation")
                Return
            End If

            Dim selectedResident As ResidentComboItem =
                DirectCast(cbResidents.SelectedItem, ResidentComboItem)
            Dim result As Boolean = familyLogic.AddFamilyMember(
                selectedFamilyId, selectedResident.ResidentId,
                cbRelationships.SelectedItem.ToString())

            If result Then
                MsgBox("Family member added successfully.", MsgBoxStyle.Information, "Success")
                cbResidents.SelectedIndex = -1
                LoadFamilyMembers()
                LoadFamilyInformation()
            Else
                MsgBox("Failed to add member. They may already be in this family.",
                       MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error adding family member: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' BUTTON: Save Changes (Edit Position session)
    ' ============================================================================
    Private Sub btnSaveChanges_Click(sender As Object, e As EventArgs) _
            Handles btnSaveChanges.Click
        Try
            If Not isEditingPosition OrElse editingMemberId <= 0 Then
                MsgBox("No position edit session is active. Click 'Edit Position' first.",
                       MsgBoxStyle.Information, "Nothing to Save")
                Return
            End If
            If cbRelationships.SelectedIndex < 0 Then
                MsgBox("Please select a relationship type.", MsgBoxStyle.Exclamation, "Validation")
                Return
            End If

            Dim result As Boolean = familyLogic.UpdateFamilyMemberRelationship(
                editingMemberId, cbRelationships.SelectedItem.ToString())

            If result Then
                MsgBox("Position updated successfully.", MsgBoxStyle.Information, "Success")
                editingMemberId = -1
                isEditingPosition = False
                cbResidents.SelectedIndex = -1
                cbResidents.Enabled = True
                LoadFamilyMembers()
                LoadFamilyInformation()
            Else
                MsgBox("Failed to update position.", MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error saving changes: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' BUTTON: Add New Resident — ADD_MODE, inject current HouseholdId
    ' ============================================================================
    Private Sub btnAddNewResident_Click(sender As Object, e As EventArgs) _
            Handles btnAddNewResident.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim residentsForm As New HouseholdFamilyResidents_Form()

                ' ADD_MODE: ResidentId = -1, householdId passed so it pre-fills
                residentsForm.SetMode(
                    HouseholdFamilyResidents_Form.formMode.ADD_MODE,
                    -1,
                    selectedHouseholdId,
                    selectedFamilyId)

                Dashboard_Layout.CurrentInstance.LoadContentPanel(residentsForm)
            End If
        Catch ex As Exception
            MsgBox("Error opening new resident form: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' BUTTON: Back
    ' ============================================================================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dashboard_Layout.CurrentInstance.LoadContentPanel(New HouseholdViewFamily_Form())
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error navigating back: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' VISUAL HELPERS
    ' ============================================================================

    Private Sub ApplyGradient(pnl As Control, startHex As String, endHex As String)
        Try
            Dim startColor = ColorTranslator.FromHtml(startHex)
            Dim endColor = ColorTranslator.FromHtml(endHex)
            AddHandler pnl.Paint,
                Sub(s, e)
                    Using brush As New LinearGradientBrush(
                            New Point(0, 0), New Point(pnl.Width, 0),
                            startColor, endColor)
                        e.Graphics.FillRectangle(brush, pnl.ClientRectangle)
                    End Using
                End Sub
        Catch ex As Exception
            Debug.WriteLine("ApplyGradient Error: " & ex.Message)
        End Try
    End Sub

    Private Sub RoundButtonCorners(btn As Button, radius As Integer)
        Try
            If btn Is Nothing Then Return
            ApplyRoundedRegion(btn, radius)
            AddHandler btn.Resize, Sub(s, e) ApplyRoundedRegion(btn, radius)
        Catch ex As Exception
            Debug.WriteLine("RoundButtonCorners Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ApplyRoundedRegion(btn As Button, radius As Integer)
        Try
            If btn Is Nothing OrElse btn.Width <= 0 OrElse btn.Height <= 0 Then Return
            Dim r As Integer = Math.Min(radius, Math.Min(btn.Width, btn.Height))
            Using p As New GraphicsPath()
                p.AddArc(0, 0, r, r, 180, 90)
                p.AddArc(btn.Width - r, 0, r, r, 270, 90)
                p.AddArc(btn.Width - r, btn.Height - r, r, r, 0, 90)
                p.AddArc(0, btn.Height - r, r, r, 90, 90)
                p.CloseFigure()
                btn.Region = New Region(p)
            End Using
        Catch ex As Exception
            Debug.WriteLine("ApplyRoundedRegion Error: " & ex.Message)
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
    ' PRIVATE HELPER TYPE
    ' ============================================================================
    Private Class ResidentComboItem
        Public ReadOnly ResidentId As Integer
        Private ReadOnly _fullName As String
        Public Sub New(id As Integer, fullName As String)
            ResidentId = id : _fullName = fullName
        End Sub
        Public Overrides Function ToString() As String
            Return _fullName
        End Function
    End Class

End Class
