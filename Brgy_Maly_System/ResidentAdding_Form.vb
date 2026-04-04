Imports System.Drawing.Drawing2D

Public Class ResidentAdding_Form
    ' === Service Layer (Business Logic) ===
    Private residentLogic As New ResidentAddingLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As ResidentAddingResponsiveManager

    ' === UI State ===
    Private editingResidentId As Integer = -1
    Private isViewOnly As Boolean = False
    Private currentResidentData As ResidentAddingLogic.ResidentData

    ''' <summary>
    ''' Constructor - Accept optional resident ID and view-only mode
    ''' </summary>
    Public Sub New(Optional residentId As Integer = -1, Optional viewOnly As Boolean = False)
        InitializeComponent()
        editingResidentId = residentId
        isViewOnly = viewOnly
    End Sub

    Private Sub ResidentAdding_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        UIUtilities.ApplyGradient(fillpanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        UIUtilities.RoundButtonCorners(btnBack, 20)
        UIUtilities.RoundButtonCorners(BtnAddResident, 20)
        UIUtilities.RoundButtonCorners(btnSearch, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New ResidentAddingResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Load Household Dropdown ===
        LoadHouseholdComboBox()

        ' === Setup Form Mode ===
        If isViewOnly Then
            SetupViewOnlyMode()
        ElseIf editingResidentId > 0 Then
            SetupEditMode()
        Else
            SetupAddMode()
        End If
    End Sub

    ''' <summary>
    ''' Load all households into combobox
    ''' </summary>
    Private Sub LoadHouseholdComboBox()
        Try
            Dim households = residentLogic.GetAllHouseholds()
            cbHouseholdNum.DataSource = households
            cbHouseholdNum.DisplayMember = "HouseholdNumber"
            cbHouseholdNum.ValueMember = "HouseholdID"
        Catch ex As Exception
            MsgBox("Error loading households: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Handle household combobox selection change - Show address
    ''' </summary>
    Private Sub cbHouseholdNum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbHouseholdNum.SelectedIndexChanged
        Try
            If cbHouseholdNum.SelectedIndex >= 0 AndAlso cbHouseholdNum.ValueMember <> "" Then
                Dim householdId As Integer = CInt(cbHouseholdNum.SelectedValue)
                Dim address As String = residentLogic.GetHouseholdAddress(householdId)
                txtAddressInfo.Text = address
            End If
        Catch ex As Exception
            Debug.WriteLine("Error in household selection: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Setup form for view-only mode
    ''' </summary>
    Private Sub SetupViewOnlyMode()
        Try
            ' === LOAD RESIDENT DATA ===
            currentResidentData = residentLogic.GetResidentById(editingResidentId)

            ' === POPULATE ALL FIELDS ===
            PopulateFormFields(currentResidentData)

            ' === DISABLE ALL INPUT FIELDS ===
            DisableAllInputFields()

            ' === HIDE SAVE BUTTON ===
            BtnAddResident.Visible = False

            ' === SHOW BACK BUTTON ONLY ===
            btnBack.Visible = True

        Catch ex As Exception
            MsgBox("Error loading resident data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Setup form for edit mode
    ''' </summary>
    Private Sub SetupEditMode()
        Try
            ' === LOAD RESIDENT DATA ===
            currentResidentData = residentLogic.GetResidentById(editingResidentId)

            ' === POPULATE ALL FIELDS ===
            PopulateFormFields(currentResidentData)

            ' === ENABLE ALL INPUT FIELDS ===
            EnableAllInputFields()

            ' === CHANGE BUTTON TEXT ===
            BtnAddResident.Text = "Update Resident"

        Catch ex As Exception
            MsgBox("Error loading resident data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Setup form for add mode
    ''' </summary>
    Private Sub SetupAddMode()
        ' === ENABLE ALL INPUT FIELDS ===
        EnableAllInputFields()

        ' === BUTTON TEXT ALREADY SET ===
        BtnAddResident.Text = "Add Resident"
    End Sub

    ''' <summary>
    ''' Populate form fields with resident data
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
            cbHouseholdNum.SelectedValue = data.HouseholdId
            CbEducationLevel.Text = data.EducationLevel
            CbEducationalStatus.Text = data.EducationalStatus
        Catch ex As Exception
            Debug.WriteLine("Error populating fields: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Get form data as ResidentData object
    ''' </summary>
    Private Function GetFormData() As ResidentAddingLogic.ResidentData
        Dim data As New ResidentAddingLogic.ResidentData With {
            .ResidentId = editingResidentId,
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
            .Categories = GetSelectedCategories()
        }
        Return data
    End Function

    ''' <summary>
    ''' Get selected categories from checkboxes
    ''' </summary>
    Private Function GetSelectedCategories() As List(Of String)
        Dim categories As New List(Of String)()
        If cbSeniorCitizen.Checked Then categories.Add("Senior Citizen")
        If cbPWD.Checked Then categories.Add("PWD")
        If cbStudent.Checked Then categories.Add("Student")
        If cbSoloParent.Checked Then categories.Add("Solo Parent")
        If cbEmployed.Checked Then categories.Add("Employed")
        If cbUnemployed.Checked Then categories.Add("Unemployed")
        If cbOFW.Checked Then categories.Add("OFW")
        If cbOutofSchoolChildren.Checked Then categories.Add("Out of School Children")
        If cbHead.Checked Then categories.Add("Head")
        If cbInhabitant.Checked Then categories.Add("Inhabitant")
        If cbIndigenousPeople.Checked Then categories.Add("Indigenous People")
        Return categories
    End Function

    ''' <summary>
    ''' Disable all input fields for view-only mode
    ''' </summary>
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
        cbSeniorCitizen.Enabled = False
        cbPWD.Enabled = False
        cbStudent.Enabled = False
        cbSoloParent.Enabled = False
        cbEmployed.Enabled = False
        cbUnemployed.Enabled = False
        cbOFW.Enabled = False
        cbOutofSchoolChildren.Enabled = False
        cbHead.Enabled = False
        cbInhabitant.Enabled = False
        cbIndigenousPeople.Enabled = False
        txtAdditionalInfo.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Enable all input fields for add/edit mode
    ''' </summary>
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
        cbSeniorCitizen.Enabled = True
        cbPWD.Enabled = True
        cbStudent.Enabled = True
        cbSoloParent.Enabled = True
        cbEmployed.Enabled = True
        cbUnemployed.Enabled = True
        cbOFW.Enabled = True
        cbOutofSchoolChildren.Enabled = True
        cbHead.Enabled = True
        cbInhabitant.Enabled = True
        cbIndigenousPeople.Enabled = True
        txtAdditionalInfo.ReadOnly = False
    End Sub

    ''' <summary>
    ''' Add/Update Resident button click
    ''' </summary>
    Private Sub BtnAddResident_Click(sender As Object, e As EventArgs) Handles BtnAddResident.Click
        Try
            Dim formData = GetFormData()
            Dim result As ResidentAddingLogic.ResidentOperationResult

            If isViewOnly Then
                Return ' Should not happen
            ElseIf editingResidentId > 0 Then
                ' === UPDATE MODE ===
                result = residentLogic.UpdateResident(formData)
            Else
                ' === ADD MODE ===
                result = residentLogic.AddResident(formData)
            End If

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                NavigateBackToResidentMain()
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Back button click
    ''' </summary>
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        NavigateBackToResidentMain()
    End Sub

    ''' <summary>
    ''' Navigate back to Resident Main form
    ''' </summary>
    Private Sub NavigateBackToResidentMain()
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim residentMainForm As New ResidentMain_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(residentMainForm)
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
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