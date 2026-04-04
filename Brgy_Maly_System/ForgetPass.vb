Imports System.Drawing.Drawing2D

Public Class ForgetPass
    ' === Service Layer (Business Logic) ===
    Private forgetPassLogic As New ForgetPassLogic()

    ' === Responsive Manager ===
    Private responsiveManager As ForgetPassResponsiveUIManager

    ' === UI State ===
    Private isInitialized As Boolean = False
    Private passwordVisible As Boolean = False
    Private confirmPasswordVisible As Boolean = False
    Private userFound As Boolean = False

    ' === Store original sizes for scaling ===
    Private originalFormWidth As Integer = 563
    Private originalFormHeight As Integer = 350
    Private scaleFactor As Single = 1.0F

    Private Sub ForgetPass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === FORM SETUP ===
        Me.DoubleBuffered = True
        Me.AutoScaleMode = AutoScaleMode.None
        Me.FormBorderStyle = FormBorderStyle.None
        Me.BackColor = Color.White

        ' === REFERENCE RESOLUTION: 1920x1080 ===
        Dim referenceWidth As Integer = 1920
        Dim referenceHeight As Integer = 1080

        ' Get current screen size
        Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height

        ' === CALCULATE SCALE FACTOR ===
        If screenWidth < referenceWidth OrElse screenHeight < referenceHeight Then
            Dim widthScale As Single = CSng(screenWidth) / referenceWidth
            Dim heightScale As Single = CSng(screenHeight) / referenceHeight
            scaleFactor = Math.Min(widthScale, heightScale)
        Else
            scaleFactor = 1.0F
        End If

        ' === SET FORM SIZE ===
        Dim formWidth As Integer = CInt(originalFormWidth * scaleFactor)
        Dim formHeight As Integer = CInt(originalFormHeight * scaleFactor)
        Me.ClientSize = New Size(formWidth, formHeight)

        ' === ALWAYS CENTER ON SCREEN ===
        Me.StartPosition = FormStartPosition.CenterScreen

        ' === APPLY STYLING ===
        ForgetPassResponsiveUIManager.ApplyPanelStyling(TopPanel, Color.FromArgb(60, 137, 66))
        ForgetPassResponsiveUIManager.ApplyPanelStyling(FillPanel, Color.White)

        ' === BUTTON STYLING ===
        ForgetPassResponsiveUIManager.ApplyButtonStyle(LogInBtn, Color.FromArgb(159, 190, 168), Color.FromArgb(0, 186, 15), Color.White)
        ForgetPassResponsiveUIManager.ApplyButtonStyle(BtnClose, Color.FromArgb(159, 190, 168), Color.FromArgb(0, 186, 15), Color.White)
        ForgetPassResponsiveUIManager.ApplyButtonStyle(SaveBtn, Color.FromArgb(159, 190, 168), Color.FromArgb(0, 186, 15), Color.White)

        ' === TEXTBOX STYLING ===
        ForgetPassResponsiveUIManager.ApplyTextBoxStyling(UnameTxtBox, Color.FromArgb(240, 255, 240), Color.White)
        ForgetPassResponsiveUIManager.ApplyTextBoxStyling(PassTxtbox, Color.FromArgb(240, 255, 240), Color.White)
        ForgetPassResponsiveUIManager.ApplyTextBoxStyling(ConfirmPassTxt, Color.FromArgb(240, 255, 240), Color.White)

        ' === INITIALIZE RESPONSIVE MANAGER ===
        responsiveManager = New ForgetPassResponsiveUIManager(Me)
        responsiveManager.ScaleAllControls(scaleFactor)

        ' === PASSWORD VISIBILITY ===
        PassTxtbox.PasswordChar = "*"c
        ConfirmPassTxt.PasswordChar = "*"c

        ' === DISABLE FIELDS INITIALLY ===
        DisablePasswordFields()

        isInitialized = True
    End Sub

    ''' <summary>
    ''' Disable password input fields
    ''' </summary>
    Private Sub DisablePasswordFields()
        PassTxtbox.Enabled = False
        ConfirmPassTxt.Enabled = False
        SaveBtn.Enabled = False
        SeePassBtn.Enabled = False
        SeePassBtn2.Enabled = False
    End Sub

    ''' <summary>
    ''' Enable password input fields
    ''' </summary>
    Private Sub EnablePasswordFields()
        PassTxtbox.Enabled = True
        ConfirmPassTxt.Enabled = True
        SaveBtn.Enabled = True
        SeePassBtn.Enabled = True
        SeePassBtn2.Enabled = True
    End Sub

    ''' <summary>
    ''' Clear all input fields
    ''' </summary>
    Private Sub ClearFields()
        UnameTxtBox.Clear()
        PassTxtbox.Clear()
        ConfirmPassTxt.Clear()
        passwordVisible = False
        confirmPasswordVisible = False
        PassTxtbox.PasswordChar = "*"c
        ConfirmPassTxt.PasswordChar = "*"c

        userFound = False
    End Sub


    ''' <summary>
    ''' Search Button Click - Search for user by username
    ''' </summary>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If String.IsNullOrWhiteSpace(UnameTxtBox.Text) Then
                MsgBox("Please enter a username to search.", MsgBoxStyle.Information, "Input Required")
                UnameTxtBox.Focus()
                Return
            End If

            ' === DISABLE BUTTON DURING SEARCH ===
            btnSearch.Enabled = False
            Application.DoEvents()

            ' === CALL SERVICE TO SEARCH USER ===
            Dim searchResult As ForgetPassLogic.PasswordResetResult = forgetPassLogic.SearchUser(UnameTxtBox.Text.Trim())

            ' === ENABLE BUTTON AFTER SEARCH ===
            btnSearch.Enabled = True

            If searchResult.IsSuccess Then
                MsgBox("User found! Please enter your new password.", MsgBoxStyle.Information, "User Found")
                UnameTxtBox.ReadOnly = True
                userFound = True
                EnablePasswordFields()
                PassTxtbox.Focus()
            Else
                MsgBox(searchResult.Message, MsgBoxStyle.Exclamation, "Search Failed")
                UnameTxtBox.Clear()
                UnameTxtBox.Focus()
                userFound = False
            End If

        Catch ex As Exception
            btnSearch.Enabled = True
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Log In Button Click - Go back to Login Form
    ''' </summary>
    Private Sub LogInBtn_Click(sender As Object, e As EventArgs) Handles LogInBtn.Click
        ClearFields()
        DisablePasswordFields()
        UnameTxtBox.ReadOnly = False
        LogInForm.Show()
        Me.Hide()
    End Sub

    ''' <summary>
    ''' Close button click
    ''' </summary>
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to exit the application?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Exit Application")

        If result = MsgBoxResult.Yes Then
            Application.Exit()
        End If
    End Sub

    ''' <summary>
    ''' Toggle new password visibility (PictureBox click)
    ''' </summary>
    Private Sub SeePassBtn_Click(sender As Object, e As EventArgs) Handles SeePassBtn.Click
        passwordVisible = Not passwordVisible

        If passwordVisible Then
            PassTxtbox.PasswordChar = ChrW(0)   ' Show password
            SeePassBtn.Image = My.Resources.eyesopen
        Else
            PassTxtbox.PasswordChar = ChrW(42)  ' Hide password
            SeePassBtn.Image = My.Resources.closeeye
        End If

    End Sub

    ''' <summary>
    ''' Toggle confirm password visibility (PictureBox click)
    ''' </summary>
    Private Sub SeePassBtn2_Click(sender As Object, e As EventArgs) Handles SeePassBtn2.Click
        confirmPasswordVisible = Not confirmPasswordVisible

        If confirmPasswordVisible Then
            ConfirmPassTxt.PasswordChar = ChrW(0)     ' Show password
            SeePassBtn.Image = My.Resources.eyesopen
        Else
            ConfirmPassTxt.PasswordChar = ChrW(42)    ' Hide password
            SeePassBtn.Image = My.Resources.closeeye
        End If

    End Sub

    ''' <summary>
    ''' Save button click - Update password
    ''' </summary>
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        Try
            ' === CHECK IF USER WAS FOUND ===
            If Not userFound Then
                MsgBox("Please search for a user first using the search button.", MsgBoxStyle.Information, "User Not Found")
                Return
            End If

            ' === VALIDATE PASSWORDS ===
            Dim validationResult As ForgetPassLogic.PasswordResetResult = forgetPassLogic.ValidatePasswords(PassTxtbox.Text, ConfirmPassTxt.Text)

            If Not validationResult.IsSuccess Then
                MsgBox(validationResult.Message, MsgBoxStyle.Exclamation, "Validation Failed")
                PassTxtbox.Focus()
                Return
            End If

            ' === CONFIRM PASSWORD CHANGE ===
            If MsgBox("Are you sure you want to change your password?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.No Then
                Return
            End If

            ' === DISABLE BUTTON DURING UPDATE ===
            SaveBtn.Enabled = False
            Application.DoEvents()

            ' === CALL SERVICE TO UPDATE PASSWORD ===
            Dim updateResult As ForgetPassLogic.PasswordResetResult = forgetPassLogic.UpdatePassword(UnameTxtBox.Text, PassTxtbox.Text)

            ' === ENABLE BUTTON AFTER UPDATE ===
            SaveBtn.Enabled = True

            If updateResult.IsSuccess Then
                MsgBox(updateResult.Message & vbCrLf & "Click OK to return to login.", MsgBoxStyle.Information, "Success")
                ClearFields()
                DisablePasswordFields()
                UnameTxtBox.ReadOnly = False
                LogInForm.Show()
                Me.Hide()
            Else
                MsgBox(updateResult.Message, MsgBoxStyle.Critical, "Update Failed")
            End If

        Catch ex As Exception
            SaveBtn.Enabled = True
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Cleanup when form closes
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
    End Sub

End Class