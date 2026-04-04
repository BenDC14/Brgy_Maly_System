Imports System.Drawing.Drawing2D

Public Class HouseholdAdding_Form
    ' === Service Layer (Business Logic) ===
    Private householdLogic As New HouseholdAddingLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As HouseholdAddingResponsiveManager

    Private Sub HouseholdAdding_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        RoundButtonCorners(BtnAddNewHousehold, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New HouseholdAddingResponsiveManager(Me)
        responsiveManager.Initialize()
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
    ''' Get form input data as HouseholdAddressData object
    ''' </summary>
    Private Function GetFormData() As HouseholdAddingLogic.HouseholdAddressData
        Dim data As New HouseholdAddingLogic.HouseholdAddressData With {
            .HouseholdNumber = txtHouseholdNumber.Text.Trim(),
            .HouseNumber = txtHouseNumber.Text.Trim(),
            .BlockNumber = txtBlockNumber.Text.Trim(),
            .LotNumber = txtLotNumber.Text.Trim(),
            .AreaNumber = txtAreaNumber.Text.Trim(),
            .StreetName = txtStreetName.Text.Trim(),
            .Village = txtVillage.Text.Trim(),
            .Subdivision = txtSubdivision.Text.Trim(),
            .Compound = txtCompound.Text.Trim(),
            .Barangay = txtBarangay.Text.Trim(),
            .Municipality = txtMunicipality.Text.Trim(),
            .Province = txtProvince.Text.Trim()
        }
        Return data
    End Function

    ''' <summary>
    ''' Clear all input fields
    ''' </summary>
    Private Sub ClearAllFields()
        txtHouseholdNumber.Clear()
        txtHouseNumber.Clear()
        txtBlockNumber.Clear()
        txtLotNumber.Clear()
        txtAreaNumber.Clear()
        txtStreetName.Clear()
        txtVillage.Clear()
        txtSubdivision.Clear()
        txtCompound.Clear()
        txtBarangay.Clear()
        txtMunicipality.Clear()
        txtProvince.Clear()
    End Sub

    ''' <summary>
    ''' Add New Household button click
    ''' </summary>
    Private Sub BtnAddNewHousehold_Click(sender As Object, e As EventArgs) Handles BtnAddNewHousehold.Click
        Try
            ' === GET FORM DATA ===
            Dim formData As HouseholdAddingLogic.HouseholdAddressData = GetFormData()

            ' === CALL SERVICE TO ADD HOUSEHOLD ===
            Dim result As HouseholdAddingLogic.HouseholdResult = householdLogic.AddHouseholdWithAddress(formData)

            If result.IsSuccess Then
                MsgBox(result.Message & vbCrLf & "Household ID: " & result.HouseholdId, MsgBoxStyle.Information, "Success")
                ClearAllFields()

                ' === NAVIGATE BACK TO HOUSEHOLD MAIN ===
                If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                    Dim householdMainForm As New HouseholdMain_Form()
                    Dashboard_Layout.CurrentInstance.LoadContentPanel(householdMainForm)
                End If
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")

                ' === FOCUS ON FIRST INVALID FIELD ===
                If result.ErrorCode = 1 Then
                    If String.IsNullOrWhiteSpace(txtHouseholdNumber.Text) Then
                        txtHouseholdNumber.Focus()
                    Else
                        txtBlockNumber.Focus()
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("BtnAddNewHousehold_Click Error: " & ex.Message)
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