Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager for ForgetPass Form
''' Handles all layout calculations and positioning with resolution change detection
''' </summary>
Public Class ForgetPassResponsiveUIManager

    ' === Original form dimensions ===
    Private Const ORIGINAL_WIDTH As Integer = 563
    Private Const ORIGINAL_HEIGHT As Integer = 350

    ' === Reference to the form ===
    Private _form As ForgetPass

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(form As ForgetPass)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior with event listeners
    ''' </summary>
    Public Sub InitializeResponsive()
        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        isLayoutCalculated = True
    End Sub

    ''' <summary>
    ''' Fires when Windows resolution changes
    ''' </summary>
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        RecalculateLayout()
    End Sub

    ''' <summary>
    ''' Fires when form window resizes
    ''' </summary>
    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Exit Sub
        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    ''' <summary>
    ''' Timer tick - recalculates layout ONCE after resize stops
    ''' </summary>
    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        RecalculateLayout()
    End Sub

    ''' <summary>
    ''' Recalculate scale factor based on current screen resolution
    ''' </summary>
    Private Sub RecalculateLayout()
        ' === REFERENCE RESOLUTION: 1920x1080 ===
        Dim referenceWidth As Integer = 1920
        Dim referenceHeight As Integer = 1080

        ' Get current screen size
        Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height

        ' === CALCULATE SCALE FACTOR ===
        Dim scaleFactor As Single = 1.0F
        If screenWidth < referenceWidth OrElse screenHeight < referenceHeight Then
            Dim widthScale As Single = CSng(screenWidth) / referenceWidth
            Dim heightScale As Single = CSng(screenHeight) / referenceHeight
            scaleFactor = Math.Min(widthScale, heightScale)
        Else
            scaleFactor = 1.0F
        End If

        ' === SET FORM SIZE ===
        Dim formWidth As Integer = CInt(563 * scaleFactor)
        Dim formHeight As Integer = CInt(350 * scaleFactor)
        _form.ClientSize = New Size(formWidth, formHeight)

        ' === RE-SCALE ALL CONTROLS ===
        ScaleAllControls(scaleFactor)

        ' === RE-CENTER ON SCREEN ===
        _form.StartPosition = FormStartPosition.CenterScreen
    End Sub

    ''' <summary>
    ''' Apply button styling with hover effects
    ''' </summary>
    Public Shared Sub ApplyButtonStyle(btn As Button, normalColor As Color, hoverColor As Color, textColor As Color)
        btn.BackColor = normalColor
        btn.ForeColor = textColor
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.UseVisualStyleBackColor = False
        btn.Cursor = Cursors.Hand

        AddHandler btn.MouseEnter, Sub(s, e)
                                       btn.BackColor = hoverColor
                                   End Sub

        AddHandler btn.MouseLeave, Sub(s, e)
                                       btn.BackColor = normalColor
                                   End Sub
    End Sub

    ''' <summary>
    ''' Apply textbox styling with focus effect
    ''' </summary>
    Public Shared Sub ApplyTextBoxStyling(textBox As TextBox, Optional focusBackColor As Color = Nothing, Optional normalBackColor As Color = Nothing)
        If normalBackColor = Nothing Then normalBackColor = Color.White
        If focusBackColor = Nothing Then focusBackColor = Color.FromArgb(240, 255, 240)

        textBox.BorderStyle = BorderStyle.FixedSingle
        textBox.BackColor = normalBackColor
        textBox.ForeColor = Color.Black

        AddHandler textBox.GotFocus, Sub(s, e)
                                         textBox.BackColor = focusBackColor
                                     End Sub

        AddHandler textBox.LostFocus, Sub(s, e)
                                          textBox.BackColor = normalBackColor
                                      End Sub
    End Sub

    ''' <summary>
    ''' Apply panel styling with optional rounded corners
    ''' </summary>
    Public Shared Sub ApplyPanelStyling(panel As Panel, backColor As Color, Optional radius As Integer = 0)
        panel.BackColor = backColor

        If radius > 0 Then
            ApplyRoundedCorners(panel, radius)
        End If
    End Sub

    ''' <summary>
    ''' Apply rounded corners to panel dynamically
    ''' </summary>
    Private Shared Sub ApplyRoundedCorners(panel As Panel, radius As Integer)
        Dim p As New GraphicsPath()
        CreateRoundedPath(p, panel.Width, panel.Height, radius)
        panel.Region = New Region(p)

        Dim panelLocal = panel
        Dim radiusLocal = radius

        AddHandler panel.Resize, Sub(sender, e)
                                     Dim newPath As New GraphicsPath()
                                     CreateRoundedPath(newPath, panelLocal.Width, panelLocal.Height, radiusLocal)
                                     panelLocal.Region = New Region(newPath)
                                 End Sub
    End Sub

    ''' <summary>
    ''' Helper to create rounded rectangle path
    ''' </summary>
    Private Shared Sub CreateRoundedPath(p As GraphicsPath, width As Integer, height As Integer, radius As Integer)
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(width - radius, 0, radius, radius, 270, 90)
        p.AddArc(width - radius, height - radius, radius, radius, 0, 90)
        p.AddArc(0, height - radius, radius, radius, 90, 90)
        p.CloseFigure()
    End Sub

    ''' <summary>
    ''' Scale all control positions and sizes by the scale factor
    ''' </summary>
    Public Sub ScaleAllControls(scaleFactor As Single)
        ' === TOP PANEL ===
        _form.TopPanel.Height = CInt(45 * scaleFactor)

        ' ForgetLbl - Title
        _form.ForgetLbl.Location = New Point(CInt(36 * scaleFactor), CInt(12 * scaleFactor))
        _form.ForgetLbl.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.ForgetLbl.ForeColor = Color.Black

        ' LogInBtn - Top button (Back to Login)
        _form.LogInBtn.Location = New Point(CInt(399 * scaleFactor), CInt(10 * scaleFactor))
        _form.LogInBtn.Size = New Size(CInt(70 * scaleFactor), CInt(25 * scaleFactor))
        _form.LogInBtn.Font = New Font("Arial Narrow", 9.75F * scaleFactor, FontStyle.Bold)
        _form.LogInBtn.ForeColor = Color.Black

        ' BtnClose - Top button
        _form.BtnClose.Location = New Point(CInt(487 * scaleFactor), CInt(10 * scaleFactor))
        _form.BtnClose.Size = New Size(CInt(70 * scaleFactor), CInt(25 * scaleFactor))
        _form.BtnClose.Font = New Font("Arial Narrow", 9.75F * scaleFactor, FontStyle.Bold)
        _form.BtnClose.ForeColor = Color.Black

        ' === FILL PANEL ===
        _form.FillPanel.Location = New Point(0, _form.TopPanel.Height)

        ' Username Label
        _form.UsernameLbl.Location = New Point(CInt(42 * scaleFactor), CInt(45 * scaleFactor))
        _form.UsernameLbl.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Bold)
        _form.UsernameLbl.ForeColor = Color.Black

        ' Username Textbox
        _form.UnameTxtBox.Location = New Point(CInt(45 * scaleFactor), CInt(62 * scaleFactor))
        _form.UnameTxtBox.Size = New Size(CInt(475 * scaleFactor), CInt(22 * scaleFactor))
        _form.UnameTxtBox.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Regular)

        ' === SEARCH BUTTON (PictureBox) - Designer: Location(497, 62), Size(23, 24) ===
        _form.btnSearch.Location = New Point(CInt(497 * scaleFactor), CInt(62 * scaleFactor))
        _form.btnSearch.Size = New Size(CInt(23 * scaleFactor), CInt(24 * scaleFactor))
        _form.btnSearch.SizeMode = PictureBoxSizeMode.StretchImage
        _form.btnSearch.Cursor = Cursors.Hand

        ' New Password Label
        _form.NewPassword.Location = New Point(CInt(42 * scaleFactor), CInt(105 * scaleFactor))
        _form.NewPassword.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Bold)
        _form.NewPassword.ForeColor = Color.Black

        ' New Password Textbox
        _form.PassTxtbox.Location = New Point(CInt(45 * scaleFactor), CInt(123 * scaleFactor))
        _form.PassTxtbox.Size = New Size(CInt(475 * scaleFactor), CInt(22 * scaleFactor))
        _form.PassTxtbox.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Regular)

        ' === SEE PASSWORD BUTTON 1 (PictureBox) - Designer: Location(500, 123), Size(23, 24) ===
        Dim seePassMargin As Integer = CInt(2 * scaleFactor)
        _form.SeePassBtn.Location = New Point(CInt(497 * scaleFactor) - seePassMargin, CInt(123 * scaleFactor))
        _form.SeePassBtn.Size = New Size(CInt(23 * scaleFactor), CInt(24 * scaleFactor))
        _form.SeePassBtn.SizeMode = PictureBoxSizeMode.StretchImage
        _form.SeePassBtn.Cursor = Cursors.Hand

        ' Confirm Password Label
        _form.ConfirmPass.Location = New Point(CInt(42 * scaleFactor), CInt(165 * scaleFactor))
        _form.ConfirmPass.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Bold)
        _form.ConfirmPass.ForeColor = Color.Black

        ' Confirm Password Textbox
        _form.ConfirmPassTxt.Location = New Point(CInt(45 * scaleFactor), CInt(183 * scaleFactor))
        _form.ConfirmPassTxt.Size = New Size(CInt(475 * scaleFactor), CInt(22 * scaleFactor))
        _form.ConfirmPassTxt.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Regular)

        ' === SEE PASSWORD BUTTON 2 (PictureBox) - Designer: Location(500, 183), Size(23, 24) ===
        _form.SeePassBtn2.Location = New Point(CInt(497 * scaleFactor) - seePassMargin, CInt(183 * scaleFactor))
        _form.SeePassBtn2.Size = New Size(CInt(23 * scaleFactor), CInt(24 * scaleFactor))
        _form.SeePassBtn2.SizeMode = PictureBoxSizeMode.StretchImage
        _form.SeePassBtn2.Cursor = Cursors.Hand

        ' Save Button
        _form.SaveBtn.Location = New Point(CInt(243 * scaleFactor), CInt(229 * scaleFactor))
        _form.SaveBtn.Size = New Size(CInt(80 * scaleFactor), CInt(30 * scaleFactor))
        _form.SaveBtn.Font = New Font("Arial Narrow", 9.75F * scaleFactor, FontStyle.Bold)
        _form.SaveBtn.ForeColor = Color.Black
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class