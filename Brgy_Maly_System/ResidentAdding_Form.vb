Imports System.Drawing.Drawing2D

''' <summary>
''' UI Event Layer for ResidentAdding_Form.
''' Responsibilities: wiring events, reading/writing controls, delegating all
''' business logic and database calls to ResidentAddingLogic.
''' No SQL or direct DB code belongs in this file.
''' </summary>
Public Class ResidentAdding_Form

    Private Const RESIDENT_FORM_CLASS As String = "ResidentMain_Form"

    Private _logic As New ResidentAddingLogic()
    Private _responsiveManager As ResidentAddingResponsiveManager

    Private _editingResidentId As Integer = -1
    Private _isViewOnly As Boolean = False
    Private _currentData As ResidentAddingLogic.ResidentData

    ' ─── Constructor ──────────────────────────────────────────────────────────────

    Public Sub New(Optional residentId As Integer = -1, Optional viewOnly As Boolean = False)
        InitializeComponent()
        _editingResidentId = residentId
        _isViewOnly = viewOnly
    End Sub

    ' ─── Form Load ────────────────────────────────────────────────────────────────

    Private Sub ResidentAdding_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Visual polish
        UIUtilities.ApplyGradient(fillpanel, "#EDFFE9", "#FFFFFF")
        UIUtilities.RoundButtonCorners(btnBack, 20)
        UIUtilities.RoundButtonCorners(BtnAddResident, 20)
        UIUtilities.RoundButtonCorners(btnSearch, 20)

        ' Responsive layout
        _responsiveManager = New ResidentAddingResponsiveManager(Me)
        _responsiveManager.Initialize()

        ' Data bindings
        LoadHouseholdComboBox()
        LoadCategoriesCheckedListBox()   ' Task 2 requirement: bind CLB from DB

        ' Mode setup
        If _isViewOnly Then
            SetupViewOnlyMode()
        ElseIf _editingResidentId > 0 Then
            SetupEditMode()
        Else
            SetupAddMode()
        End If
    End Sub

    ' ─── Data loading helpers ────────────────────────────────────────────────────

    ''' <summary>
    ''' Populates cbHouseholdNum from the database via the Logic layer.
    ''' </summary>
    Private Sub LoadHouseholdComboBox()
        Try
            Dim households As DataTable = _logic.GetAllHouseholds()
            cbHouseholdNum.DataSource = households
            cbHouseholdNum.DisplayMember = "HouseholdNumber"
            cbHouseholdNum.ValueMember = "HouseholdID"
        Catch ex As Exception
            MsgBox("Error loading households: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Calls the Logic layer to fetch the categories DataTable and binds it to
    ''' CategoriesCLB using CategoryId as the value member and Category as the
    ''' display member.  This replaces the old hardcoded checkboxes.
    ''' </summary>
    Private Sub LoadCategoriesCheckedListBox()
        Try
            Dim categoriesTable As DataTable = _logic.GetCategoriesTable()

            ' A CheckedListBox does not natively support ValueMember/DisplayMember
            ' the same way a ComboBox does, so we use a DataSource trick:
            ' bind via DataSource and rely on DisplayMember for the label text,
            ' then retrieve the ValueMember (CategoryId) when reading checked items.
            CategoriesCLB.DataSource = categoriesTable
            CategoriesCLB.DisplayMember = "Category"
            CategoriesCLB.ValueMember = "CategoryId"

        Catch ex As Exception
            MsgBox("Error loading categories: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ─── ComboBox events ─────────────────────────────────────────────────────────

    Private Sub cbHouseholdNum_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles cbHouseholdNum.SelectedIndexChanged
        Try
            If cbHouseholdNum.SelectedIndex >= 0 AndAlso cbHouseholdNum.ValueMember <> "" Then
                Dim householdId As Integer = CInt(cbHouseholdNum.SelectedValue)
                txtAddressInfo.Text = _logic.GetHouseholdAddress(householdId)
            End If
        Catch ex As Exception
            Debug.WriteLine("Household selection error: " & ex.Message)
        End Try
    End Sub

    ' ─── Mode setup ──────────────────────────────────────────────────────────────

    Private Sub SetupViewOnlyMode()
        Try
            _currentData = _logic.GetResidentById(_editingResidentId)
            PopulateFormFields(_currentData)
            DisableAllInputFields()
            BtnAddResident.Visible = False
            btnBack.Visible = True
        Catch ex As Exception
            MsgBox("Error loading resident data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub SetupEditMode()
        Try
            If Not LogInForm.CanEdit(RESIDENT_FORM_CLASS) Then
                _isViewOnly = True
                SetupViewOnlyMode()
                Return
            End If
            _currentData = _logic.GetResidentById(_editingResidentId)
            PopulateFormFields(_currentData)
            EnableAllInputFields()
            BtnAddResident.Text = "Update Resident"
        Catch ex As Exception
            MsgBox("Error loading resident data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub SetupAddMode()
        If Not LogInForm.CanAdd(RESIDENT_FORM_CLASS) Then
            MsgBox("You do not have permission to add residents.", MsgBoxStyle.Exclamation, "Access Denied")
            NavigateBackToResidentMain()
            Return
        End If
        EnableAllInputFields()
        BtnAddResident.Text = "Add Resident"
    End Sub

    ' ─── Field population / read ─────────────────────────────────────────────────

    ''' <summary>
    ''' Fills all form controls from a ResidentData object retrieved from the DB.
    ''' For the CLB, checks items whose CategoryId is in data.SelectedCategoryIds.
    ''' </summary>
    Private Sub PopulateFormFields(data As ResidentAddingLogic.ResidentData)
        Try
            txtLname.Text = data.LastName
            txtFname.Text = data.FirstName
            txtMname.Text = data.MiddleName
            txtSuffix.Text = data.Suffix
            DTPDateofBirth.Value = data.DateOfBirth
            txtPlaceofBirth.Text = data.PlaceOfBirth
            cbSex.Text = data.Sex
            cbCivilStatus.Text = data.CivilStatus
            txtReligion.Text = data.Religion
            txtCitezenship.Text = data.Citizenship
            txtOccupation.Text = data.Occupation
            txtContactNum.Text = data.ContactNumber
            txtEmailAddress.Text = data.EmailAddress
            CbYes.Checked = data.Voter
            CbNo.Checked = Not data.Voter
            CbEducationLevel.Text = data.EducationLevel
            CbEducationalStatus.Text = data.EducationalStatus
            txtAdditionalInfo.Text = data.AdditionalInfo

            If data.HouseholdId > 0 Then
                cbHouseholdNum.SelectedValue = data.HouseholdId
            End If

            ' Restore checked state for each category item in the CLB
            CheckCategoriesByIds(data.SelectedCategoryIds)

        Catch ex As Exception
            Debug.WriteLine("Error populating fields: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Iterates the CLB items and checks those whose CategoryId is in the supplied list.
    ''' </summary>
    Private Sub CheckCategoriesByIds(categoryIds As List(Of Integer))
        For i As Integer = 0 To CategoriesCLB.Items.Count - 1
            Dim rowView As DataRowView = TryCast(CategoriesCLB.Items(i), DataRowView)
            If rowView Is Nothing Then Continue For

            Dim itemId As Integer = CInt(rowView("CategoryId"))
            CategoriesCLB.SetItemChecked(i, categoryIds.Contains(itemId))
        Next
    End Sub

    ''' <summary>
    ''' Reads all checked items from CategoriesCLB and returns their CategoryIds.
    ''' No SQL here – just reading the bound DataRowView objects.
    ''' </summary>
    Private Function GetSelectedCategoryIds() As List(Of Integer)
        Dim ids As New List(Of Integer)()

        For Each checkedItem As Object In CategoriesCLB.CheckedItems
            Dim rowView As DataRowView = TryCast(checkedItem, DataRowView)
            If rowView IsNot Nothing Then
                ids.Add(CInt(rowView("CategoryId")))
            End If
        Next

        Return ids
    End Function

    ''' <summary>
    ''' Assembles a ResidentData object from the current form state.
    ''' Passes the list of selected Category IDs (not names) to the Logic layer.
    ''' </summary>
    Private Function GetFormData() As ResidentAddingLogic.ResidentData
        Return New ResidentAddingLogic.ResidentData With {
            .ResidentId = _editingResidentId,
            .FirstName = txtFname.Text.Trim(),
            .LastName = txtLname.Text.Trim(),
            .MiddleName = txtMname.Text.Trim(),
            .Suffix = txtSuffix.Text.Trim(),
            .DateOfBirth = DTPDateofBirth.Value,
            .PlaceOfBirth = txtPlaceofBirth.Text.Trim(),
            .Sex = cbSex.Text,
            .CivilStatus = cbCivilStatus.Text,
            .Religion = txtReligion.Text.Trim(),
            .Citizenship = txtCitezenship.Text.Trim(),
            .Occupation = txtOccupation.Text.Trim(),
            .ContactNumber = txtContactNum.Text.Trim(),
            .EmailAddress = txtEmailAddress.Text.Trim(),
            .Voter = CbYes.Checked,
            .HouseholdId = CInt(cbHouseholdNum.SelectedValue),
            .EducationLevel = CbEducationLevel.Text,
            .EducationalStatus = CbEducationalStatus.Text,
            .AdditionalInfo = txtAdditionalInfo.Text.Trim(),
            .SelectedCategoryIds = GetSelectedCategoryIds()   ' ← IDs only, no SQL here
        }
    End Function

    ' ─── Enable / Disable helpers ─────────────────────────────────────────────────

    Private Sub DisableAllInputFields()
        txtFname.ReadOnly = True
        txtLname.ReadOnly = True
        txtMname.ReadOnly = True
        txtSuffix.ReadOnly = True
        DTPDateofBirth.Enabled = False
        txtPlaceofBirth.ReadOnly = True
        cbSex.Enabled = False
        cbCivilStatus.Enabled = False
        txtReligion.ReadOnly = True
        txtCitezenship.ReadOnly = True
        txtOccupation.ReadOnly = True
        txtContactNum.ReadOnly = True
        txtEmailAddress.ReadOnly = True
        CbYes.Enabled = False
        CbNo.Enabled = False
        cbHouseholdNum.Enabled = False
        CbEducationLevel.Enabled = False
        CbEducationalStatus.Enabled = False
        CategoriesCLB.Enabled = False   ' replaces all old individual cb.Enabled = False
        txtAdditionalInfo.ReadOnly = True
    End Sub

    Private Sub EnableAllInputFields()
        txtFname.ReadOnly = False
        txtLname.ReadOnly = False
        txtMname.ReadOnly = False
        txtSuffix.ReadOnly = False
        DTPDateofBirth.Enabled = True
        txtPlaceofBirth.ReadOnly = False
        cbSex.Enabled = True
        cbCivilStatus.Enabled = True
        txtReligion.ReadOnly = False
        txtCitezenship.ReadOnly = False
        txtOccupation.ReadOnly = False
        txtContactNum.ReadOnly = False
        txtEmailAddress.ReadOnly = False
        CbYes.Enabled = True
        CbNo.Enabled = True
        cbHouseholdNum.Enabled = True
        CbEducationLevel.Enabled = True
        CbEducationalStatus.Enabled = True
        CategoriesCLB.Enabled = True    ' replaces all old individual cb.Enabled = True
        txtAdditionalInfo.ReadOnly = False
    End Sub

    ' ─── Button events ───────────────────────────────────────────────────────────

    ''' <summary>
    ''' Save / Update button.  Reads the form via GetFormData() – which includes
    ''' GetSelectedCategoryIds() – then hands everything to the Logic layer.
    ''' No SQL in this file.
    ''' </summary>
    Private Sub BtnAddResident_Click(sender As Object, e As EventArgs) Handles BtnAddResident.Click
        Try
            If _isViewOnly Then Return

            If _editingResidentId > 0 AndAlso Not LogInForm.CanEdit(RESIDENT_FORM_CLASS) Then
                MsgBox("You do not have permission to edit residents.", MsgBoxStyle.Exclamation, "Access Denied")
                Return
            End If

            If _editingResidentId <= 0 AndAlso Not LogInForm.CanAdd(RESIDENT_FORM_CLASS) Then
                MsgBox("You do not have permission to add residents.", MsgBoxStyle.Exclamation, "Access Denied")
                Return
            End If

            Dim formData As ResidentAddingLogic.ResidentData = GetFormData()
            Dim result As ResidentAddingLogic.ResidentOperationResult

            If _editingResidentId > 0 Then
                result = _logic.UpdateResident(formData)
            Else
                result = _logic.AddResident(formData)
            End If

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                NavigateBackToResidentMain()
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Unexpected error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        NavigateBackToResidentMain()
    End Sub

    ' ─── Navigation ──────────────────────────────────────────────────────────────

    Private Sub NavigateBackToResidentMain()
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dashboard_Layout.CurrentInstance.LoadContentPanel(New ResidentMain_Form())
            End If
        Catch ex As Exception
            MsgBox("Navigation error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ─── Cleanup ─────────────────────────────────────────────────────────────────

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If _responsiveManager IsNot Nothing Then
            _responsiveManager.Cleanup()
        End If
        MyBase.OnFormClosing(e)
    End Sub

End Class
