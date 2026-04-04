Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for LogInForm (Modal Login Dialog)
''' Uses scale factor approach and ensures proper centering
''' Detects system resolution changes
''' </summary>
Public Class LogInResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 470
    Private Const ORIGINAL_HEIGHT As Integer = 566

    ' === Reference to the form ===
    Private ReadOnly _form As LogInForm

    ' === Scale factor ===
    Private scaleFactor As Single = 1.0F

    ' === Timer for resize debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As LogInForm)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior for login dialog
    ''' </summary>
    Public Sub Initialize()
        ' === FORM SETUP ===
        _form.AutoScaleMode = AutoScaleMode.None
        _form.FormBorderStyle = FormBorderStyle.None

        ' === Setup timer for resize debouncing ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Initial layout calculation ===
        RecalculateLayout()
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
    ''' Recalculate layout based on current resolution
    ''' </summary>
    Private Sub RecalculateLayout()
        ' === GET CURRENT SCREEN SIZE ===
        Dim currentScreen As Screen = Screen.FromControl(_form)
        Dim screenWidth As Integer = currentScreen.WorkingArea.Width
        Dim screenHeight As Integer = currentScreen.WorkingArea.Height

        ' === REFERENCE RESOLUTION: 1920x1080 ===
        Dim referenceWidth As Integer = 1920
        Dim referenceHeight As Integer = 1080

        ' === CALCULATE SCALE FACTOR ===
        If screenWidth < referenceWidth OrElse screenHeight < referenceHeight Then
            Dim widthScale As Single = CSng(screenWidth) / CSng(referenceWidth)
            Dim heightScale As Single = CSng(screenHeight) / CSng(referenceHeight)
            scaleFactor = Math.Min(widthScale, heightScale)
        Else
            scaleFactor = 1.0F
        End If

        ' === SET FORM SIZE ===
        Dim formWidth As Integer = CInt(ORIGINAL_WIDTH * scaleFactor)
        Dim formHeight As Integer = CInt(ORIGINAL_HEIGHT * scaleFactor)
        _form.ClientSize = New Size(formWidth, formHeight)

        ' === CRITICAL: Center on screen AFTER setting size ===
        _form.StartPosition = FormStartPosition.Manual
        Dim centerX As Integer = (screenWidth - formWidth) \ 2
        Dim centerY As Integer = (screenHeight - formHeight) \ 2
        _form.Location = New Point(centerX, centerY)

        ' === Scale all controls ===
        ScaleAllControls()
    End Sub

    ''' <summary>
    ''' Scale all control positions and sizes by the scale factor
    ''' </summary>
    Private Sub ScaleAllControls()
        ' === LEFT PANEL (Green sidebar) ===
        ' Designer: Size(148, 566)
        _form.LeftPanel.Width = CInt(148 * scaleFactor)

        ' === LOGO ===
        ' Designer: Location(115, 21), Size(113, 113)
        _form.LogoPic.Location = New Point(CInt(115 * scaleFactor), CInt(21 * scaleFactor))
        _form.LogoPic.Size = New Size(CInt(113 * scaleFactor), CInt(113 * scaleFactor))

        ' === TITLE LABEL ===
        ' Designer: Location(85, 137)
        _form.BrgyMalySystemLbl.Location = New Point(CInt(85 * scaleFactor), CInt(137 * scaleFactor))
        _form.BrgyMalySystemLbl.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)
        _form.BrgyMalySystemLbl.ForeColor = Color.FromArgb(60, 137, 66)

        ' === USERNAME SECTION ===
        ' Label - Designer: Location(68, 308)
        _form.UsernameLbl.Location = New Point(CInt(68 * scaleFactor), CInt(308 * scaleFactor))
        _form.UsernameLbl.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Bold)

        ' TextBox - Designer: Location(71, 325), Size(181, 22)
        _form.UnameTxtBox.Location = New Point(CInt(71 * scaleFactor), CInt(325 * scaleFactor))
        _form.UnameTxtBox.Size = New Size(CInt(181 * scaleFactor), CInt(22 * scaleFactor))
        _form.UnameTxtBox.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Regular)

        ' === PASSWORD SECTION ===
        ' Label - Designer: Location(68, 352)
        _form.Password.Location = New Point(CInt(68 * scaleFactor), CInt(352 * scaleFactor))
        _form.Password.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Bold)

        ' TextBox - Designer: Location(71, 370), Size(181, 22)
        _form.PassTxtbox.Location = New Point(CInt(71 * scaleFactor), CInt(370 * scaleFactor))
        _form.PassTxtbox.Size = New Size(CInt(181 * scaleFactor), CInt(22 * scaleFactor))
        _form.PassTxtbox.Font = New Font("Arial", 9.75F * scaleFactor, FontStyle.Regular)

        ' See Password Button - Designer: Location(258, 370), Size(23, 24)
        _form.SeePassBtn.Location = New Point(CInt(258 * scaleFactor), CInt(370 * scaleFactor))
        _form.SeePassBtn.Size = New Size(CInt(23 * scaleFactor), CInt(24 * scaleFactor))
        _form.SeePassBtn.Cursor = Cursors.Hand

        ' === FORGOT PASSWORD LINK ===
        ' Designer: Location(155, 395)
        _form.ForgetPassLbl.Location = New Point(CInt(155 * scaleFactor), CInt(395 * scaleFactor))
        _form.ForgetPassLbl.Font = New Font("Arial", 8.25F * scaleFactor, FontStyle.Underline)
        _form.ForgetPassLbl.ForeColor = Color.FromArgb(26, 30, 255)
        _form.ForgetPassLbl.Cursor = Cursors.Hand

        ' === LOGIN BUTTON ===
        ' Designer: Location(115, 435), Size(110, 30)
        _form.LogInBtn.Location = New Point(CInt(115 * scaleFactor), CInt(435 * scaleFactor))
        _form.LogInBtn.Size = New Size(CInt(110 * scaleFactor), CInt(30 * scaleFactor))
        _form.LogInBtn.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
        _form.LogInBtn.Cursor = Cursors.Hand

        ' === EXIT BUTTON (Top Right) ===
        ' Position relative to FillPanel width
        Dim fillPanelWidth As Integer = _form.ClientSize.Width - CInt(148 * scaleFactor)
        _form.ExitBtn.Location = New Point(fillPanelWidth - CInt(33 * scaleFactor), CInt(1 * scaleFactor))
        _form.ExitBtn.Size = New Size(CInt(31 * scaleFactor), CInt(30 * scaleFactor))
        _form.ExitBtn.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        Try
            ' === Stop and dispose timer ===
            If resizeTimer IsNot Nothing Then
                resizeTimer.Stop()
                RemoveHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick
                resizeTimer.Dispose()
            End If

            ' === Remove event handlers ===
            RemoveHandler _form.Resize, AddressOf Form_Resize
            RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
        Catch ex As Exception
            Debug.WriteLine("Error during cleanup: " & ex.Message)
        End Try
    End Sub

End Class
