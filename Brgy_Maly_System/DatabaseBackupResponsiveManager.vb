Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for DatabaseBackup_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class DatabaseBackupResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As DatabaseBackup_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As DatabaseBackup_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior
    ''' </summary>
    Public Sub Initialize()
        ' === CRITICAL: Override Designer's fixed size on FillPanel ===
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.FillPanel.Location = New Point(0, 0)

        ' === MAIN CONTAINER - FILL AVAILABLE SPACE ===
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event to recalculate layout when window resizes ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === CRITICAL: Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Calculate and apply layout for the first time ===
        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    ''' <summary>
    ''' CRITICAL: Fires when Windows resolution changes
    ''' </summary>
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
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
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Calculate positions and apply layout based on current form size
    ''' Uses PERCENTAGES for positioning and sizing
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        ' === Use form's actual client size ===
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' === Calculate scale factor for font sizing ===
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        ' === Update FillPanel ===
        _form.FillPanel.Size = New Size(panelWidth, panelHeight)
        _form.FillPanel.Location = New Point(0, 0)

        ' === TITLE SECTION ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)

        ' === FILE NAME SECTION ===
        PositionFileNameSection(panelWidth, panelHeight, scaleFactor)

        ' === FILE LOCATION SECTION ===
        PositionFileLocationSection(panelWidth, panelHeight, scaleFactor)

        ' === BACKUP NOTES SECTION ===
        PositionBackupNotesSection(panelWidth, panelHeight, scaleFactor)

        ' === ACTION BUTTONS ===
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title label at top
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(38, 33) on 1700x1004 = 2.2% from left, 3.3% from top
        _form.lblBackupDatabase.Location = New Point(CInt(panelWidth * 0.022), CInt(panelHeight * 0.033))
        _form.lblBackupDatabase.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblBackupDatabase.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position file name label and textbox
    ''' </summary>
    Private Sub PositionFileNameSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' File Name Label - Designer: Location(66, 169) = 3.9% from left, 16.8% from top
        _form.lblFileName.Location = New Point(CInt(panelWidth * 0.039), CInt(panelHeight * 0.168))
        _form.lblFileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblFileName.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' File Name TextBox - Designer: Location(70, 194), Size(1435, 29)
        _form.txtFilename.Location = New Point(CInt(panelWidth * 0.041), CInt(panelHeight * 0.193))
        _form.txtFilename.Size = New Size(CInt(panelWidth * 0.844), CInt(panelHeight * 0.029))
        _form.txtFilename.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtFilename.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position file location label, textbox, and browse button
    ''' </summary>
    Private Sub PositionFileLocationSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' File Location Label - Designer: Location(66, 321) = 3.9% from left, 32% from top
        _form.Label1.Location = New Point(CInt(panelWidth * 0.039), CInt(panelHeight * 0.32))
        _form.Label1.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.Label1.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' File Location TextBox - Designer: Location(70, 346), Size(1435, 29)
        _form.txtFileLocation.Location = New Point(CInt(panelWidth * 0.041), CInt(panelHeight * 0.345))
        _form.txtFileLocation.Size = New Size(CInt(panelWidth * 0.844), CInt(panelHeight * 0.029))
        _form.txtFileLocation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtFileLocation.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Browse Button - Designer: Location(1546, 347), Size(119, 29)
        _form.btnBrowse.Location = New Point(CInt(panelWidth * 0.909), CInt(panelHeight * 0.346))
        _form.btnBrowse.Size = New Size(CInt(panelWidth * 0.07), CInt(panelHeight * 0.029))
        _form.btnBrowse.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnBrowse.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnBrowse.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position backup notes label and rich textbox
    ''' </summary>
    Private Sub PositionBackupNotesSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Backup Notes Label - Designer: Location(66, 480) = 3.9% from left, 47.8% from top
        _form.lblBackupNotes.Location = New Point(CInt(panelWidth * 0.039), CInt(panelHeight * 0.478))
        _form.lblBackupNotes.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblBackupNotes.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Backup Notes RichTextBox - Designer: Location(70, 505), Size(1435, 230)
        _form.BackupNotesRtxt.Location = New Point(CInt(panelWidth * 0.041), CInt(panelHeight * 0.503))
        _form.BackupNotesRtxt.Size = New Size(CInt(panelWidth * 0.844), CInt(panelHeight * 0.229))
        _form.BackupNotesRtxt.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.BackupNotesRtxt.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position action buttons (Start Backup, Back to Main)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.111) ' 189/1700 ≈ 11.1%
        Dim btnHeight As Integer = CInt(panelHeight * 0.042) ' 42/1004 ≈ 4.2%
        Dim btnY As Integer = CInt(panelHeight * 0.778) ' 781/1004 ≈ 77.8%

        ' Start Backup Button - Designer: Location(384, 781), Size(189, 42)
        _form.btnStartBackup.Location = New Point(CInt(panelWidth * 0.226), btnY)
        _form.btnStartBackup.Size = New Size(btnWidth, btnHeight)
        _form.btnStartBackup.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnStartBackup.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnStartBackup.Cursor = Cursors.Hand

        ' Back to Main Button - Designer: Location(926, 781), Size(189, 42)
        _form.btnBacktoMain.Location = New Point(CInt(panelWidth * 0.545), btnY)
        _form.btnBacktoMain.Size = New Size(btnWidth, btnHeight)
        _form.btnBacktoMain.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnBacktoMain.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnBacktoMain.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
