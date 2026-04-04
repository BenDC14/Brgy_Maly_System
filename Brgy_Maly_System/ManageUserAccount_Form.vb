Imports System.Drawing.Drawing2D

Public Class ManageUserAccount_Form
    ' === Service Layer (Business Logic) ===
    Private userAccountLogic As New ManageUserAccountLogic()

    ' === Responsive Manager Instance ===
    Private responsiveManager As ManageUserAccountResponsiveManager

    ' === UI State ===
    Private currentUsername As String = ""
    Private currentUserId As Integer = -1
    Private passwordVisible As Boolean = False
    Private confirmPasswordVisible As Boolean = False

    Private Sub ManageUserAccount_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        UIUtilities.ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling ===
        UIUtilities.RoundButtonCorners(btnSaveInfo, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New ManageUserAccountResponsiveManager(Me)
        responsiveManager.Initialize()

        ' === Setup password fields as MASKED (never show actual password) ===
        txtPass.PasswordChar = "*"c
        txtConfirmPass.PasswordChar = "*"c

        ' === CLEAR ALL FIELDS WHEN FORM LOADS ===
        ClearAllFields()

        ' === Get current logged-in user from LogInForm ===
        currentUsername = LogInForm.CurrentUsername
        currentUserId = LogInForm.CurrentUserID

        If Not String.IsNullOrEmpty(currentUsername) Then
            LoadUserAccount(currentUsername)
        Else
            MsgBox("Error: No user is currently logged in.", MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    ''' <summary>
    ''' Clear all input fields
    ''' </summary>
    Private Sub ClearAllFields()
        txtFname.Clear()
        txtLname.Clear()
        txtUname.Clear()
        txtPass.Clear()
        txtConfirmPass.Clear()
        passwordVisible = False
        confirmPasswordVisible = False
        txtPass.PasswordChar = "*"c
        txtConfirmPass.PasswordChar = "*"c
    End Sub

    ''' <summary>
    ''' Load user account information from database
    ''' NOTE: Password fields are NOT filled - users must enter new password to change
    ''' </summary>
    Private Sub LoadUserAccount(username As String)
        Try
            Dim result = userAccountLogic.GetUserByUsername(username)

            If result.IsSuccess Then
                Dim userData = CType(result.Data, ManageUserAccountLogic.UserAccountData)

                ' === POPULATE NON-PASSWORD FIELDS ===
                currentUserId = userData.UserId
                txtFname.Text = userData.FirstName
                txtLname.Text = userData.LastName
                txtUname.Text = userData.Username

                ' === MAKE USERNAME READ-ONLY ===
                txtUname.ReadOnly = True

                ' === LEAVE PASSWORD FIELDS EMPTY - Users must enter new password ===
                ' This prevents accidental password display
                txtPass.Clear()
                txtConfirmPass.Clear()

                ' === Show info message ===
                MsgBox("Account loaded successfully." & vbCrLf & "Leave password fields empty if you don't want to change your password.", MsgBoxStyle.Information, "Account Loaded")

            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error loading user account: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Toggle password visibility - Button 1 (Password field)
    ''' </summary>
    Private Sub SeePassBtn_Click(sender As Object, e As EventArgs) Handles SeePassBtn.Click
        Try
            passwordVisible = Not passwordVisible

            If passwordVisible Then
                txtPass.PasswordChar = ChrW(0)  ' Show password
                SeePassBtn.Image = My.Resources.eyesopen
            Else
                txtPass.PasswordChar = ChrW(42)  ' Hide password (asterisk)
                SeePassBtn.Image = My.Resources.closeeye
            End If

        Catch ex As Exception
            Debug.WriteLine("Error in SeePassBtn_Click: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Toggle password visibility - Button 2 (Confirm Password field)
    ''' </summary>
    Private Sub SeePassBtn2_Click(sender As Object, e As EventArgs) Handles SeePassBtn2.Click
        Try
            confirmPasswordVisible = Not confirmPasswordVisible

            If confirmPasswordVisible Then
                txtConfirmPass.PasswordChar = ChrW(0)  ' Show password
                SeePassBtn2.Image = My.Resources.eyesopen
            Else
                txtConfirmPass.PasswordChar = ChrW(42)  ' Hide password (asterisk)
                SeePassBtn2.Image = My.Resources.closeeye
            End If

        Catch ex As Exception
            Debug.WriteLine("Error in SeePassBtn2_Click: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Save/Update user account information
    ''' If password fields are empty, only update name fields
    ''' If password fields are filled, validate and update password too
    ''' </summary>
    Private Sub btnSaveInfo_Click(sender As Object, e As EventArgs) Handles btnSaveInfo.Click
        Try
            ' === CHECK IF PASSWORD IS BEING CHANGED ===
            Dim isChangingPassword As Boolean = Not String.IsNullOrWhiteSpace(txtPass.Text)

            ' === IF CHANGING PASSWORD, VALIDATE ===
            If isChangingPassword Then
                ' === VALIDATE PASSWORDS MATCH ===
                If txtPass.Text <> txtConfirmPass.Text Then
                    MsgBox("Passwords do not match. Please try again.", MsgBoxStyle.Exclamation, "Validation Error")
                    txtPass.Clear()
                    txtConfirmPass.Clear()
                    txtPass.Focus()
                    Return
                End If

                ' === VALIDATE PASSWORD LENGTH ===
                If txtPass.Text.Length < 6 Then
                    MsgBox("Password must be at least 6 characters long.", MsgBoxStyle.Exclamation, "Validation Error")
                    txtPass.Focus()
                    Return
                End If
            End If

            ' === PREPARE USER DATA FOR UPDATE ===
            Dim userData As New ManageUserAccountLogic.UserAccountData With {
                .UserId = currentUserId,
                .FirstName = txtFname.Text.Trim(),
                .LastName = txtLname.Text.Trim(),
                .Username = txtUname.Text.Trim(),
                .Password = If(isChangingPassword, txtPass.Text, "") ' Empty string = don't change password
            }

            ' === VALIDATE NON-PASSWORD FIELDS ===
            Dim validationResult = userAccountLogic.ValidateUserDataForSave(userData, isChangingPassword)

            If Not validationResult.IsSuccess Then
                MsgBox(validationResult.Message, MsgBoxStyle.Exclamation, "Validation Error")
                Return
            End If

            ' === UPDATE USER ACCOUNT IN DATABASE ===
            Dim result = userAccountLogic.UpdateUserAccount(userData)

            If result.IsSuccess Then
                MsgBox(result.Message, MsgBoxStyle.Information, "Success")

                ' === UPDATE DASHBOARD BUTTON ===
                If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                    Dashboard_Layout.CurrentInstance.UserAccountBtn.Text = LogInForm.CurrentUsername
                End If

                ' === RELOAD UPDATED DATA ===
                LoadUserAccount(currentUsername)
            Else
                MsgBox(result.Message, MsgBoxStyle.Exclamation, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Cleanup when form closes
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        ' === CLEAR FIELDS WHEN FORM CLOSES ===
        ClearAllFields()

        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If
        MyBase.OnFormClosing(e)
    End Sub

End Class