Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for AyudaAdd_Form (Modal Dialog)
''' Uses scale factor approach for modal/popup forms
''' </summary>
Public Class AyudaAddResponsiveUIManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 563
    Private Const ORIGINAL_HEIGHT As Integer = 203

    ' === Reference to the form ===
    Private ReadOnly _form As AyudaAdd_Form

    ' === Scale factor ===
    Private scaleFactor As Single = 1.0F

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As AyudaAdd_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior for modal dialog
    ''' </summary>
    Public Sub Initialize()
        ' === FORM SETUP ===
        _form.AutoScaleMode = AutoScaleMode.None
        _form.FormBorderStyle = FormBorderStyle.None

        ' === Get current screen size ===
        Dim currentScreen As Screen = Screen.FromControl(_form)
        Dim screenWidth As Integer = currentScreen.WorkingArea.Width
        Dim screenHeight As Integer = currentScreen.WorkingArea.Height

        ' === REFERENCE RESOLUTION: 1920x1080 ===
        Dim referenceWidth As Integer = 1920
        Dim referenceHeight As Integer = 1080

        ' === CALCULATE SCALE FACTOR ===
        ' Only scale DOWN if screen is smaller than reference
        ' Don't scale UP if screen is larger
        If screenWidth < referenceWidth OrElse screenHeight < referenceHeight Then
            Dim widthScale As Single = CSng(screenWidth) / CSng(referenceWidth)
            Dim heightScale As Single = CSng(screenHeight) / CSng(referenceHeight)
            scaleFactor = Math.Min(widthScale, heightScale)
        Else
            ' Screen is larger than reference - keep optimal size
            scaleFactor = 1.0F
        End If

        ' === SET FORM SIZE ===
        Dim formWidth As Integer = CInt(ORIGINAL_WIDTH * scaleFactor)
        Dim formHeight As Integer = CInt(ORIGINAL_HEIGHT * scaleFactor)
        _form.ClientSize = New Size(formWidth, formHeight)

        ' === CRITICAL: Set StartPosition AFTER setting size ===
        _form.StartPosition = FormStartPosition.Manual

        ' === Manually center the form on screen ===
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
        ' === EXIT BUTTON ===
        ' Designer: Location(531, 1), Size(31, 30)
        _form.ExitBtn.Location = New Point(CInt(531 * scaleFactor), CInt(1 * scaleFactor))
        _form.ExitBtn.Size = New Size(CInt(31 * scaleFactor), CInt(30 * scaleFactor))
        _form.ExitBtn.Cursor = Cursors.Hand

        ' === TITLE LABEL ===
        ' Designer: Location(214, 21)
        _form.lblNewAyuda.Location = New Point(CInt(214 * scaleFactor), CInt(21 * scaleFactor))
        _form.lblNewAyuda.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Bold)
        _form.lblNewAyuda.AutoSize = True

        ' === ADD NEW AYUDA SECTION ===
        ' Label - Designer: Location(38, 69)
        _form.lblAddNewAyuda.Location = New Point(CInt(38 * scaleFactor), CInt(69 * scaleFactor))
        _form.lblAddNewAyuda.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblAddNewAyuda.AutoSize = True

        ' TextBox - Designer: Location(42, 94), Size(479, 29)
        _form.txtAddNewAyuda.Location = New Point(CInt(42 * scaleFactor), CInt(94 * scaleFactor))
        _form.txtAddNewAyuda.Size = New Size(CInt(479 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtAddNewAyuda.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Save Button - Designer: Location(161, 141), Size(245, 46)
        _form.btnSave.Location = New Point(CInt(161 * scaleFactor), CInt(141 * scaleFactor))
        _form.btnSave.Size = New Size(CInt(245 * scaleFactor), CInt(46 * scaleFactor))
        _form.btnSave.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSave.Cursor = Cursors.Hand
    End Sub

End Class
