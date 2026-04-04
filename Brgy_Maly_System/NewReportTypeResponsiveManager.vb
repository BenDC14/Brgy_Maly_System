Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for NewReportType_Form (Modal Dialog)
''' Uses scale factor approach for modal/popup forms
''' </summary>
Public Class NewReportTypeResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 470
    Private Const ORIGINAL_HEIGHT As Integer = 485

    ' === Reference to the form ===
    Private ReadOnly _form As NewReportType_Form

    ' === Scale factor ===
    Private scaleFactor As Single = 1.0F

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As NewReportType_Form)
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
        ' Designer: Location(436, 1), Size(31, 30)
        Dim exitBtnMarginRight As Integer = CInt(2 * scaleFactor)
        Dim exitBtnMarginTop As Integer = CInt(1 * scaleFactor)
        _form.ExitBtn.Location = New Point(CInt(436 * scaleFactor) - exitBtnMarginRight, CInt(1 * scaleFactor) + exitBtnMarginTop)
        _form.ExitBtn.Size = New Size(CInt(31 * scaleFactor), CInt(30 * scaleFactor))
        _form.ExitBtn.Cursor = Cursors.Hand

        ' === TITLE LABEL ===
        ' Designer: Location(103, 29)
        _form.lblAddReportTypes.Location = New Point(CInt(103 * scaleFactor), CInt(29 * scaleFactor))
        _form.lblAddReportTypes.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.lblAddReportTypes.AutoSize = True

        ' === REPORT TYPE SECTION ===
        ' Label - Designer: Location(26, 137)
        _form.lblReportType.Location = New Point(CInt(26 * scaleFactor), CInt(137 * scaleFactor))
        _form.lblReportType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblReportType.AutoSize = True

        ' TextBox - Designer: Location(30, 162), Size(404, 29)
        _form.txtReportType.Location = New Point(CInt(30 * scaleFactor), CInt(162 * scaleFactor))
        _form.txtReportType.Size = New Size(CInt(404 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtReportType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Button - Designer: Location(110, 214), Size(245, 47)
        _form.btnAddNewReportType.Location = New Point(CInt(110 * scaleFactor), CInt(214 * scaleFactor))
        _form.btnAddNewReportType.Size = New Size(CInt(245 * scaleFactor), CInt(47 * scaleFactor))
        _form.btnAddNewReportType.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnAddNewReportType.Cursor = Cursors.Hand

        ' === REPORT SUB TYPE SECTION ===
        ' Label - Designer: Location(26, 287)
        _form.lblReportSubType.Location = New Point(CInt(26 * scaleFactor), CInt(287 * scaleFactor))
        _form.lblReportSubType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblReportSubType.AutoSize = True

        ' TextBox - Designer: Location(30, 312), Size(404, 29)
        _form.txtReportSubType.Location = New Point(CInt(30 * scaleFactor), CInt(312 * scaleFactor))
        _form.txtReportSubType.Size = New Size(CInt(404 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtReportSubType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Button - Designer: Location(110, 364), Size(245, 47)
        _form.btnAddNewReportSubType.Location = New Point(CInt(110 * scaleFactor), CInt(364 * scaleFactor))
        _form.btnAddNewReportSubType.Size = New Size(CInt(245 * scaleFactor), CInt(47 * scaleFactor))
        _form.btnAddNewReportSubType.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnAddNewReportSubType.Cursor = Cursors.Hand
    End Sub

End Class