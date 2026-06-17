' ================================================================================
' FILE: HouseholdFamilyResidents_Form.vb
' LAYER: Main Form UI Events Layer
'
' MODE ROUTING
' ──────────────────────────────────────────────────────────────────────────────
'  Caller sets mode via:
'    residentsForm.SetMode(FormMode.ADD_MODE,  -1,          householdId, familyId)
'    residentsForm.SetMode(FormMode.EDIT_MODE, residentId,  householdId, familyId)
'
'  On Form_Load:
'    ADD_MODE  → clear fields, pre-fill HouseholdNumber (read-only display)
'    EDIT_MODE → load existing minimal profile from DB into fields
'
'  btnSave_Click branches on currentMode:
'    ADD_MODE  → INSERT new resident, link to family via familymembers table
'    EDIT_MODE → UPDATE existing resident's minimal fields
' ================================================================================
Imports System.Drawing.Drawing2D
Public Class HouseholdFamilyResidents_Form

    ' ============================================================================
    ' MODE ENUM — replaces the old ambiguous Integer flag
    ' ============================================================================
    Public Enum FormMode
        ADD_MODE
        EDIT_MODE
    End Enum

    ' ── Infrastructure ───────────────────────────────────────────────────────────
    Private responsiveManager As HouseholdFamilyResidentsResponsiveManager
    Private ReadOnly residentLogic As New HouseholdFamilyResidentsLogic()

    ' ── Form-level state ─────────────────────────────────────────────────────────
    Private currentMode As FormMode = FormMode.ADD_MODE
    Private currentResidentId As Integer = -1   ' > 0 only in EDIT_MODE
    Private currentHouseholdId As Integer = -1  ' always injected by caller
    Private currentFamilyId As Integer = -1    ' used when linking after INSERT

    ' ============================================================================
    ' PUBLIC ENTRY POINT — called by HouseholdEditFamily_Form BEFORE Load
    ' ============================================================================
    ''' <summary>
    ''' Sets the operating mode and all context identifiers for this form.
    ''' Must be called before the form is shown so Form_Load picks up the values.
    ''' </summary>
    Public Sub SetMode(mode As FormMode,
                       residentId As Integer,
                       householdId As Integer,
                       familyId As Integer)
        currentMode = mode
        currentResidentId = residentId
        currentHouseholdId = householdId
        currentFamilyId = familyId
    End Sub

    ' ── LEGACY shim — keeps any older callers that still use SetFormMode working ─
    ''' <summary>Backward-compatible shim. Prefer SetMode().</summary>
    Public Sub SetFormMode(residentIDParam As Integer,
                           residentDataParam As Object)
        If residentIDParam > 0 Then
            currentMode = FormMode.EDIT_MODE
            currentResidentId = residentIDParam
        Else
            currentMode = FormMode.ADD_MODE
            currentResidentId = -1
        End If
    End Sub

    ' ============================================================================
    ' FORM LOAD
    ' ============================================================================
    Private Sub HouseholdFamilyResidents_Form_Load(sender As Object, e As EventArgs) _
            Handles MyBase.Load
        Try
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")
            RoundButtonCorners(btnSave, 20)
            RoundButtonCorners(btnBack, 20)

            responsiveManager = New HouseholdFamilyResidentsResponsiveManager(Me)
            responsiveManager.Initialize()

            ' Populate the categories CheckedListBox
            LoadCategoriesIntoCheckedListBox()

            Select Case currentMode

                Case FormMode.ADD_MODE
                    ConfigureForAddMode()

                Case FormMode.EDIT_MODE
                    ConfigureForEditMode()

            End Select

        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' MODE CONFIGURATION
    ' ============================================================================

    ''' <summary>
    ''' ADD_MODE: clear all inputs; pre-fill household number (read-only) if
    ''' we have a householdId. Update form title to reflect the mode.
    ''' </summary>
    Private Sub ConfigureForAddMode()
        FamilyResidentlbl.Text = "Add New Resident"

        ' Clear all fields
        txtLastName.Clear()
        txtFirstName.Clear()
        txtMiddleName.Clear()
        txtSuffix.Clear()
        DTPDateofBirth.Value = DateTime.Today
        cbSex.SelectedIndex = -1
        cbCivilStatus.SelectedIndex = -1
        txtContactNum.Clear()
        txtEmailAddress.Clear()
        txtAdditionalInfo.Clear()

        ' Pre-fill and lock the household display field
        If currentHouseholdId > 0 Then
            Dim hhNumber As String =
                residentLogic.GetHouseholdNumberById(currentHouseholdId)
            txtHouseholdNumber.Text = hhNumber
            txtHouseholdNumber.ReadOnly = True
            txtHouseholdNumber.BackColor = Color.FromArgb(225, 225, 225)
        Else
            txtHouseholdNumber.Clear()
            txtHouseholdNumber.ReadOnly = False
        End If

        ' Uncheck all categories
        For i As Integer = 0 To CategoriesCLB.Items.Count - 1
            CategoriesCLB.SetItemChecked(i, False)
        Next
    End Sub

    ''' <summary>
    ''' EDIT_MODE: load the resident's minimal profile from the DB and populate
    ''' all fields. The household number field is locked (read-only).
    ''' </summary>
    Private Sub ConfigureForEditMode()
        FamilyResidentlbl.Text = "Edit Resident"

        If currentResidentId <= 0 Then
            MsgBox("Invalid resident ID for edit mode.", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        Try
            Dim data As HouseholdFamilyResidentsLogic.MinimalResidentData =
                residentLogic.GetMinimalResidentById(currentResidentId)

            If data Is Nothing OrElse data.ResidentId <= 0 Then
                MsgBox("Resident record not found.", MsgBoxStyle.Exclamation, "Not Found")
                Return
            End If

            ' Populate fields
            txtLastName.Text = data.LastName
            txtFirstName.Text = data.FirstName
            txtMiddleName.Text = data.MiddleName
            txtSuffix.Text = data.Suffix

            DTPDateofBirth.Value = If(data.DateOfBirth = Date.MinValue,
                                     DateTime.Today, data.DateOfBirth)

            ' Sex combobox
            Dim sexIdx As Integer = cbSex.FindStringExact(data.Sex)
            cbSex.SelectedIndex = If(sexIdx >= 0, sexIdx, -1)

            ' Civil Status combobox
            Dim csIdx As Integer = cbCivilStatus.FindStringExact(data.CivilStatus)
            cbCivilStatus.SelectedIndex = If(csIdx >= 0, csIdx, -1)

            txtContactNum.Text = data.ContactNumber
            txtEmailAddress.Text = data.EmailAddress
            txtAdditionalInfo.Text = data.AdditionalInfo

            ' Household Number — locked in edit mode
            txtHouseholdNumber.Text = data.HouseholdNumber
            txtHouseholdNumber.ReadOnly = True
            txtHouseholdNumber.BackColor = Color.FromArgb(225, 225, 225)

            ' Restore checked categories
            For i As Integer = 0 To CategoriesCLB.Items.Count - 1
                Dim item As CategoryComboItem =
                    TryCast(CategoriesCLB.Items(i), CategoryComboItem)
                If item IsNot Nothing Then
                    CategoriesCLB.SetItemChecked(i,
                        data.SelectedCategoryIds.Contains(item.CategoryId))
                End If
            Next

        Catch ex As Exception
            MsgBox("Error loading resident data: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' CATEGORIES CHECKEDLISTBOX
    ' ============================================================================
    Private Sub LoadCategoriesIntoCheckedListBox()
        Try
            CategoriesCLB.Items.Clear()
            Dim dt As DataTable = residentLogic.GetCategories()
            For Each row As DataRow In dt.Rows
                CategoriesCLB.Items.Add(New CategoryComboItem(
                    CInt(row("CategoryId")), row("Category").ToString()))
            Next
        Catch ex As Exception
            Debug.WriteLine("LoadCategoriesIntoCheckedListBox Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Collects checked CategoryIds from CategoriesCLB.
    ''' </summary>
    Private Function GetCheckedCategoryIds() As List(Of Integer)
        Dim ids As New List(Of Integer)()
        For Each item As Object In CategoriesCLB.CheckedItems
            Dim cat As CategoryComboItem = TryCast(item, CategoryComboItem)
            If cat IsNot Nothing Then ids.Add(cat.CategoryId)
        Next
        Return ids
    End Function

    ' ============================================================================
    ' BUTTON: Save
    ' Routes to INSERT (ADD_MODE) or UPDATE (EDIT_MODE) in the Logic layer.
    ' ============================================================================
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' ── Shared validation ────────────────────────────────────────────────
            If String.IsNullOrWhiteSpace(txtLastName.Text) Then
                MsgBox("Last Name is required.", MsgBoxStyle.Exclamation, "Validation")
                txtLastName.Focus() : Return
            End If
            If String.IsNullOrWhiteSpace(txtFirstName.Text) Then
                MsgBox("First Name is required.", MsgBoxStyle.Exclamation, "Validation")
                txtFirstName.Focus() : Return
            End If
            If cbSex.SelectedIndex < 0 Then
                MsgBox("Sex is required.", MsgBoxStyle.Exclamation, "Validation")
                cbSex.Focus() : Return
            End If
            If currentHouseholdId <= 0 Then
                MsgBox("No household is associated with this entry." &
                       Environment.NewLine &
                       "Please ensure a valid Household ID was passed to this form.",
                       MsgBoxStyle.Exclamation, "Validation")
                Return
            End If

            ' ── Build the minimal data object ────────────────────────────────────
            Dim data As New HouseholdFamilyResidentsLogic.MinimalResidentData With {
                .ResidentId = currentResidentId,
                .LastName = txtLastName.Text.Trim(),
                .FirstName = txtFirstName.Text.Trim(),
                .MiddleName = txtMiddleName.Text.Trim(),
                .Suffix = txtSuffix.Text.Trim(),
                .DateOfBirth = DTPDateofBirth.Value.Date,
                .Sex = cbSex.SelectedItem.ToString(),
                .CivilStatus = If(cbCivilStatus.SelectedIndex >= 0,
                                        cbCivilStatus.SelectedItem.ToString(), ""),
                .ContactNumber = txtContactNum.Text.Trim(),
                .EmailAddress = txtEmailAddress.Text.Trim(),
                .AdditionalInfo = txtAdditionalInfo.Text.Trim(),
                .HouseholdId = currentHouseholdId,
                .SelectedCategoryIds = GetCheckedCategoryIds()
            }

            ' ── Route by mode ────────────────────────────────────────────────────
            Dim result As HouseholdFamilyResidentsLogic.SaveResult

            Select Case currentMode

                Case FormMode.ADD_MODE
                    result = residentLogic.InsertResident(data, currentFamilyId)

                Case FormMode.EDIT_MODE
                    result = residentLogic.UpdateResident(data)

                Case Else
                    MsgBox("Unknown form mode.", MsgBoxStyle.Critical, "Error")
                    Return

            End Select

            ' ── Handle result ────────────────────────────────────────────────────
            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")
                NavigateBackToEditFamily()
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Unexpected error while saving: " & ex.Message,
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ============================================================================
    ' BUTTON: Back
    ' ============================================================================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            NavigateBackToEditFamily()
        Catch ex As Exception
            MsgBox("Error navigating back: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Returns to HouseholdEditFamily_Form, passing the FamilyId so the grid
    ''' re-loads with the current family context.
    ''' </summary>
    Private Sub NavigateBackToEditFamily()
        If Dashboard_Layout.CurrentInstance IsNot Nothing Then
            Dim editForm As New HouseholdEditFamily_Form(currentFamilyId)
            Dashboard_Layout.CurrentInstance.LoadContentPanel(editForm)
        Else
            MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
        End If
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
    ' PRIVATE HELPER TYPE — CLB item wrapper
    ' ============================================================================
    Private Class CategoryComboItem
        Public ReadOnly CategoryId As Integer
        Private ReadOnly _name As String
        Public Sub New(id As Integer, name As String)
            CategoryId = id : _name = name
        End Sub
        Public Overrides Function ToString() As String
            Return _name
        End Function
    End Class

End Class
