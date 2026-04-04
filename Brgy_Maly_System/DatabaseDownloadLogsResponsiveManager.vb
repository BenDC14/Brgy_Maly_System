Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for DatabaseDownloadLogs_Form (Modal Dialog)
''' Uses scale factor approach for modal/popup forms
''' </summary>
Public Class DatabaseDownloadLogsResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 470
    Private Const ORIGINAL_HEIGHT As Integer = 566

    ' === Reference to the form ===
    Private ReadOnly _form As DatabaseDownloadLogs_Form

    ' === Scale factor ===
    Private scaleFactor As Single = 1.0F

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As DatabaseDownloadLogs_Form)
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
        ' Designer: Location(438, 1), Size(31, 30)
        Dim exitBtnMarginRight As Integer = CInt(2 * scaleFactor)
        Dim exitBtnMarginTop As Integer = CInt(1 * scaleFactor)
        _form.ExitBtn.Location = New Point(CInt(438 * scaleFactor) - exitBtnMarginRight, CInt(1 * scaleFactor) + exitBtnMarginTop)
        _form.ExitBtn.Size = New Size(CInt(31 * scaleFactor), CInt(30 * scaleFactor))
        _form.ExitBtn.Cursor = Cursors.Hand

        ' === TITLE LABEL ===
        ' Designer: Location(18, 38)
        _form.lblDownloadBackupAndRestoreLogs.Location = New Point(CInt(18 * scaleFactor), CInt(38 * scaleFactor))
        _form.lblDownloadBackupAndRestoreLogs.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Bold)
        _form.lblDownloadBackupAndRestoreLogs.AutoSize = True

        ' === SELECT LOG TYPE SECTION ===
        ' Label - Designer: Location(19, 122)
        _form.lblSelectLogType.Location = New Point(CInt(19 * scaleFactor), CInt(122 * scaleFactor))
        _form.lblSelectLogType.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)
        _form.lblSelectLogType.AutoSize = True

        ' Backup Logs Checkbox - Designer: Location(92, 171)
        _form.cbBackupLogs.Location = New Point(CInt(92 * scaleFactor), CInt(171 * scaleFactor))
        _form.cbBackupLogs.Font = New Font("Arial", 15.0F * scaleFactor, FontStyle.Regular)
        _form.cbBackupLogs.AutoSize = True

        ' Restore Logs Checkbox - Designer: Location(92, 230)
        _form.cbRestoreLogs.Location = New Point(CInt(92 * scaleFactor), CInt(230 * scaleFactor))
        _form.cbRestoreLogs.Font = New Font("Arial", 15.0F * scaleFactor, FontStyle.Regular)
        _form.cbRestoreLogs.AutoSize = True

        ' Both Checkbox - Designer: Location(92, 289)
        _form.cbBoth.Location = New Point(CInt(92 * scaleFactor), CInt(289 * scaleFactor))
        _form.cbBoth.Font = New Font("Arial", 15.0F * scaleFactor, FontStyle.Regular)
        _form.cbBoth.AutoSize = True

        ' === FORMAT SECTION ===
        ' Label - Designer: Location(19, 345)
        _form.lblFormat.Location = New Point(CInt(19 * scaleFactor), CInt(345 * scaleFactor))
        _form.lblFormat.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)
        _form.lblFormat.AutoSize = True

        ' CSV Checkbox - Designer: Location(92, 379)
        _form.cbCSV.Location = New Point(CInt(92 * scaleFactor), CInt(379 * scaleFactor))
        _form.cbCSV.Font = New Font("Arial", 15.0F * scaleFactor, FontStyle.Regular)
        _form.cbCSV.AutoSize = True

        ' Excel Checkbox - Designer: Location(92, 438)
        _form.cbExcel.Location = New Point(CInt(92 * scaleFactor), CInt(438 * scaleFactor))
        _form.cbExcel.Font = New Font("Arial", 15.0F * scaleFactor, FontStyle.Regular)
        _form.cbExcel.AutoSize = True

        ' === DOWNLOAD BUTTON ===
        ' Designer: Location(165, 501), Size(151, 33)
        _form.btnDownload.Location = New Point(CInt(165 * scaleFactor), CInt(501 * scaleFactor))
        _form.btnDownload.Size = New Size(CInt(151 * scaleFactor), CInt(33 * scaleFactor))
        _form.btnDownload.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnDownload.Cursor = Cursors.Hand
    End Sub

End Class
