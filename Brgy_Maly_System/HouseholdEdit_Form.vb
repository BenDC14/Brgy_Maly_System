Imports System.Drawing.Drawing2D

Public Class HouseholdEdit_Form
    ' === Service Layer (Business Logic) ===
    Private householdEditLogic As New HouseholdEditLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As HouseholdEditResponsiveManager

    ' === UI State ===
    Private editingHouseholdId As Integer = -1
    Private householdData As HouseholdEditLogic.HouseholdAddressData

    ''' <summary>
    ''' Constructor - Accept household ID for editing
    ''' </summary>
    Public Sub New(Optional householdId As Integer = -1)
        InitializeComponent()
        editingHouseholdId = householdId
    End Sub

    Private Sub HouseholdEdit_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        RoundButtonCorners(BtnEditHousehold, 20)
        RoundButtonCorners(btnBack, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New HouseholdEditResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Load Household Data ===
        LoadHouseholdData()

        ' === TODO: Load Family Heads (later implementation) ===
        ' LoadFamilyHeads()

        ' === TODO: Load Residents (later implementation) ===
        ' LoadResidents()
    End Sub

    ''' <summary>
    ''' Load household and address data
    ''' </summary>
    Private Sub LoadHouseholdData()
        Try
            If editingHouseholdId <= 0 Then
                MsgBox("Invalid Household ID.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            householdData = householdEditLogic.GetHouseholdById(editingHouseholdId)

            If householdData Is Nothing OrElse householdData.HouseholdID = 0 Then
                MsgBox("Household not found.", MsgBoxStyle.Critical, "Error")
                Return
            End If

            ' === POPULATE FORM FIELDS ===
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
    ''' Apply rounded corners to button
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
    ''' Edit Household button click
    ''' </summary>
    Private Sub BtnEditHousehold_Click(sender As Object, e As EventArgs) Handles BtnEditHousehold.Click
        Try
            ' === GET FORM DATA ===
            Dim updatedData As New HouseholdEditLogic.HouseholdAddressData With {
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

            ' === CALL SERVICE ===
            Dim result As HouseholdEditLogic.HouseholdEditResult = householdEditLogic.UpdateHousehold(updatedData)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")

                ' === NAVIGATE BACK TO HOUSEHOLD MAIN ===
                If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                    Dim householdMainForm As New HouseholdMain_Form()
                    Dashboard_Layout.CurrentInstance.LoadContentPanel(householdMainForm)
                End If
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("BtnEditHousehold_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Back to Main button click
    ''' </summary>
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

    ''' <summary>
    ''' Cleanup when form closes
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If
        MyBase.OnFormClosing(e)
    End Sub

    ' ========================================
    ' TODO: For Future Implementation
    ' ========================================
    ' 1. btnSearchHeads_Click - Search family heads in household
    ' 2. BtnSearchResident_Click - Search residents in household
    ' 3. LoadFamilyHeads() - Load all family heads from GetFamilyHeadsByHousehold
    ' 4. LoadResidents() - Load all residents from GetResidentsByHousehold
    ' 5. ConfigureFamilyHeadsDGV() - Setup DataGridView styling for family heads
    ' 6. ConfigureResidentsDGV() - Setup DataGridView styling for residents
    ' 7. Handle button columns in DataGridView for family heads actions
    ' 8. Handle button columns in DataGridView for residents actions
    ' ========================================

End Class