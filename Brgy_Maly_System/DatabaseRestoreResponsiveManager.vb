Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for DatabaseRestore_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class DatabaseRestoreResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As DatabaseRestore_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As DatabaseRestore_Form)
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

        ' === SELECT BACKUP FILE SECTION ===
        PositionSelectBackupFileSection(panelWidth, panelHeight, scaleFactor)

        ' === FILE DETAILS SECTION ===
        PositionFileDetailsSection(panelWidth, panelHeight, scaleFactor)

        ' === DIVIDER LINE 1 ===
        PositionDividerLine1(panelWidth, panelHeight)

        ' === WARNING SECTION ===
        PositionWarningSection(panelWidth, panelHeight, scaleFactor)

        ' === DIVIDER LINE 2 ===
        PositionDividerLine2(panelWidth, panelHeight)

        ' === ACTION BUTTONS ===
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title label at top
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(38, 33) on 1700x1004 = 2.2% from left, 3.3% from top
        _form.lblRestoreDatabase.Location = New Point(CInt(panelWidth * 0.022), CInt(panelHeight * 0.033))
        _form.lblRestoreDatabase.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblRestoreDatabase.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position select backup file label, textbox, and browse button
    ''' </summary>
    Private Sub PositionSelectBackupFileSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Select Backup File Label - Designer: Location(66, 134) = 3.9% from left, 13.3% from top
        _form.lblSelectBackupFile.Location = New Point(CInt(panelWidth * 0.039), CInt(panelHeight * 0.133))
        _form.lblSelectBackupFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblSelectBackupFile.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Select Backup File TextBox - Designer: Location(70, 159), Size(1435, 29)
        _form.txtSelectBackupFile.Location = New Point(CInt(panelWidth * 0.041), CInt(panelHeight * 0.158))
        _form.txtSelectBackupFile.Size = New Size(CInt(panelWidth * 0.844), CInt(panelHeight * 0.029))
        _form.txtSelectBackupFile.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtSelectBackupFile.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Browse Button - Designer: Location(1544, 159), Size(119, 29)
        _form.btnBrowse.Location = New Point(CInt(panelWidth * 0.908), CInt(panelHeight * 0.158))
        _form.btnBrowse.Size = New Size(CInt(panelWidth * 0.07), CInt(panelHeight * 0.029))
        _form.btnBrowse.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnBrowse.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnBrowse.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position file name, backup date, and backup status sections
    ''' </summary>
    Private Sub PositionFileDetailsSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' === FILE NAME SECTION ===
        ' File Name Label - Designer: Location(66, 251) = 3.9% from left, 25% from top
        _form.lblFileName.Location = New Point(CInt(panelWidth * 0.039), CInt(panelHeight * 0.25))
        _form.lblFileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblFileName.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' File Name TextBox - Designer: Location(70, 276), Size(1435, 29)
        _form.txtFileName.Location = New Point(CInt(panelWidth * 0.041), CInt(panelHeight * 0.275))
        _form.txtFileName.Size = New Size(CInt(panelWidth * 0.844), CInt(panelHeight * 0.029))
        _form.txtFileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtFileName.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === BACKUP DATE SECTION ===
        ' Backup Date Label - Designer: Location(66, 376) = 3.9% from left, 37.5% from top
        _form.lblBackupDate.Location = New Point(CInt(panelWidth * 0.039), CInt(panelHeight * 0.375))
        _form.lblBackupDate.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblBackupDate.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Backup Date TextBox - Designer: Location(70, 401), Size(1435, 29)
        _form.txtBackupDate.Location = New Point(CInt(panelWidth * 0.041), CInt(panelHeight * 0.399))
        _form.txtBackupDate.Size = New Size(CInt(panelWidth * 0.844), CInt(panelHeight * 0.029))
        _form.txtBackupDate.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtBackupDate.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === BACKUP STATUS SECTION ===
        ' Backup Status Label - Designer: Location(66, 509) = 3.9% from left, 50.7% from top
        _form.lblBackupStatus.Location = New Point(CInt(panelWidth * 0.039), CInt(panelHeight * 0.507))
        _form.lblBackupStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblBackupStatus.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Backup Status TextBox - Designer: Location(70, 534), Size(1435, 29)
        _form.txtBackupStatus.Location = New Point(CInt(panelWidth * 0.041), CInt(panelHeight * 0.532))
        _form.txtBackupStatus.Size = New Size(CInt(panelWidth * 0.844), CInt(panelHeight * 0.029))
        _form.txtBackupStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtBackupStatus.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position first horizontal divider line
    ''' </summary>
    Private Sub PositionDividerLine1(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(0, 595), Size(1700, 2)
        _form.LinePnl1.Location = New Point(0, CInt(panelHeight * 0.593))
        _form.LinePnl1.Size = New Size(panelWidth, 2)
        _form.LinePnl1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position warning section (title and message)
    ''' </summary>
    Private Sub PositionWarningSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Warning Title - Designer: Location(64, 621) = 3.8% from left, 61.9% from top
        _form.lblWarning.Location = New Point(CInt(panelWidth * 0.038), CInt(panelHeight * 0.619))
        _form.lblWarning.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblWarning.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)

        ' Warning Message - Designer: Location(176, 683) = 10.4% from left, 68% from top
        _form.lblWarningMessage.Location = New Point(CInt(panelWidth * 0.104), CInt(panelHeight * 0.68))
        _form.lblWarningMessage.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblWarningMessage.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.lblWarningMessage.MaximumSize = New Size(CInt(panelWidth * 0.85), 0) ' Allow text wrapping at 85% width
    End Sub

    ''' <summary>
    ''' Position second horizontal divider line
    ''' </summary>
    Private Sub PositionDividerLine2(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(0, 813), Size(1700, 2)
        _form.LinePanel2.Location = New Point(0, CInt(panelHeight * 0.81))
        _form.LinePanel2.Size = New Size(panelWidth, 2)
        _form.LinePanel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position action buttons (Restore Now, Back to Main)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.111) ' 189/1700 ≈ 11.1%
        Dim btnHeight As Integer = CInt(panelHeight * 0.042) ' 42/1004 ≈ 4.2%
        Dim btnY As Integer = CInt(panelHeight * 0.867) ' 870/1004 ≈ 86.7%

        ' Restore Now Button - Designer: Location(485, 870), Size(189, 42)
        _form.btnRestoreNow.Location = New Point(CInt(panelWidth * 0.285), btnY)
        _form.btnRestoreNow.Size = New Size(btnWidth, btnHeight)
        _form.btnRestoreNow.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnRestoreNow.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnRestoreNow.Cursor = Cursors.Hand

        ' Back to Main Button - Designer: Location(1027, 870), Size(189, 42)
        _form.btnBacktoMain.Location = New Point(CInt(panelWidth * 0.604), btnY)
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
