Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for DatabaseBackupMain_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class DatabaseBackupMainResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As DatabaseBackupMain_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As DatabaseBackupMain_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior
    ''' </summary>
    Public Sub Initialize()
        ' === CRITICAL: Override Designer's fixed size on FillPanel ===
        _form.fillpanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.fillpanel.Location = New Point(0, 0)

        ' === MAIN CONTAINER - FILL AVAILABLE SPACE ===
        _form.fillpanel.Dock = DockStyle.Fill
        _form.fillpanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event to recalculate layout when window resizes ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === CRITICAL: Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Calculate and apply layout for the first time ===
        _form.fillpanel.PerformLayout()
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
        _form.fillpanel.Size = New Size(panelWidth, panelHeight)
        _form.fillpanel.Location = New Point(0, 0)

        ' === TITLE SECTION ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)

        ' === ACTION BUTTONS (Top Row) ===
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)

        ' === DIVIDER LINE ===
        PositionDividerLine(panelWidth, panelHeight)

        ' === BACKUP/RESTORE LOGS SECTION ===
        PositionLogsSection(panelWidth, panelHeight, scaleFactor)

        ' === SEARCH SECTION ===
        PositionSearchSection(panelWidth, panelHeight, scaleFactor)

        ' === DATA GRID VIEW ===
        PositionDataGridView(panelWidth, panelHeight)

        ' === PAGINATION SECTION ===
        PositionPaginationSection(panelWidth, panelHeight, scaleFactor)

        ' === VIEW BUTTON ===
        PositionViewButton(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title label at top
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(19, 39) on 1700x1004 = 1.1% from left, 3.9% from top
        _form.lblDatabaseMaintenance.Location = New Point(CInt(panelWidth * 0.011), CInt(panelHeight * 0.039))
        _form.lblDatabaseMaintenance.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblDatabaseMaintenance.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position action buttons (Backup, Restore, Download Logs)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnHeight As Integer = CInt(panelHeight * 0.033) ' 33/1004 ≈ 3.3%
        Dim btnY As Integer = CInt(panelHeight * 0.11) ' 110/1004 ≈ 11%

        ' Backup Database Button - Designer: Location(104, 110), Size(189, 33)
        _form.btnBackupDB.Location = New Point(CInt(panelWidth * 0.061), btnY)
        _form.btnBackupDB.Size = New Size(CInt(panelWidth * 0.111), btnHeight)
        _form.btnBackupDB.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnBackupDB.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnBackupDB.Cursor = Cursors.Hand

        ' Restore Database Button - Designer: Location(721, 110), Size(189, 33)
        _form.btnRestoreDB.Location = New Point(CInt(panelWidth * 0.424), btnY)
        _form.btnRestoreDB.Size = New Size(CInt(panelWidth * 0.111), btnHeight)
        _form.btnRestoreDB.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnRestoreDB.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnRestoreDB.Cursor = Cursors.Hand

        ' Download Logs Button - Designer: Location(1343, 110), Size(189, 33)
        _form.btnDownloadLogs.Location = New Point(CInt(panelWidth * 0.79), btnY)
        _form.btnDownloadLogs.Size = New Size(CInt(panelWidth * 0.111), btnHeight)
        _form.btnDownloadLogs.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnDownloadLogs.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnDownloadLogs.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position horizontal divider line
    ''' </summary>
    Private Sub PositionDividerLine(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(0, 161), Size(1700, 2)
        _form.LinePnl2.Location = New Point(0, CInt(panelHeight * 0.16))
        _form.LinePnl2.Size = New Size(panelWidth, 2)
        _form.LinePnl2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position backup and restore logs label
    ''' </summary>
    Private Sub PositionLogsSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(21, 217) on 1700x1004 = 1.2% from left, 21.6% from top
        _form.lblBackupAndRestoreLogs.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.216))
        _form.lblBackupAndRestoreLogs.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblBackupAndRestoreLogs.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position search label, textbox, and button
    ''' </summary>
    Private Sub PositionSearchSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim searchY As Integer = CInt(panelHeight * 0.281) ' 282/1004 ≈ 28.1%

        ' Search Label - Designer: Location(21, 288)
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.287))
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Search Textbox - Designer: Location(104, 282), Size(1414, 32)
        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.061), searchY)
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.832), CInt(panelHeight * 0.032))
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtSearch.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        ' Search Button - Designer: Location(1524, 282), Size(151, 32)
        _form.btnSearch.Location = New Point(CInt(panelWidth * 0.896), searchY)
        _form.btnSearch.Size = New Size(CInt(panelWidth * 0.089), CInt(panelHeight * 0.032))
        _form.btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnSearch.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position DataGridView for database logs
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(25, 320), Size(1650, 528)
        _form.dgvDatabase.Location = New Point(CInt(panelWidth * 0.015), CInt(panelHeight * 0.319))
        _form.dgvDatabase.Size = New Size(CInt(panelWidth * 0.971), CInt(panelHeight * 0.526))
        _form.dgvDatabase.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position pagination buttons (1, 2, 3, Next) and label
    ''' </summary>
    Private Sub PositionPaginationSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim pageY As Integer = CInt(panelHeight * 0.853) ' 856/1004 ≈ 85.3%
        Dim pageBtnWidth As Integer = CInt(panelWidth * 0.027) ' 46/1700 ≈ 2.7%
        Dim pageBtnHeight As Integer = CInt(panelHeight * 0.033) ' 33/1004 ≈ 3.3%

        ' Pages Label - Designer: Location(21, 860)
        _form.lblPages.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.857))
        _form.lblPages.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.lblPages.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Page 1 Button - Designer: Location(102, 856), Size(46, 33)
        _form.btnPage1.Location = New Point(CInt(panelWidth * 0.06), pageY)
        _form.btnPage1.Size = New Size(pageBtnWidth, pageBtnHeight)
        _form.btnPage1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPage1.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPage1.Cursor = Cursors.Hand

        ' Page 2 Button - Designer: Location(154, 856), Size(46, 33)
        _form.btnPage2.Location = New Point(CInt(panelWidth * 0.091), pageY)
        _form.btnPage2.Size = New Size(pageBtnWidth, pageBtnHeight)
        _form.btnPage2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPage2.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPage2.Cursor = Cursors.Hand

        ' Page 3 Button - Designer: Location(206, 856), Size(46, 33)
        _form.btnPage3.Location = New Point(CInt(panelWidth * 0.121), pageY)
        _form.btnPage3.Size = New Size(pageBtnWidth, pageBtnHeight)
        _form.btnPage3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPage3.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPage3.Cursor = Cursors.Hand

        ' Next Button - Designer: Location(261, 856), Size(77, 33)
        _form.btnPageNext.Location = New Point(CInt(panelWidth * 0.154), pageY)
        _form.btnPageNext.Size = New Size(CInt(panelWidth * 0.045), pageBtnHeight)
        _form.btnPageNext.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnPageNext.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnPageNext.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position view button at bottom right
    ''' </summary>
    Private Sub PositionViewButton(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(1538, 860), Size(128, 32)
        _form.btnView.Location = New Point(CInt(panelWidth * 0.905), CInt(panelHeight * 0.857))
        _form.btnView.Size = New Size(CInt(panelWidth * 0.075), CInt(panelHeight * 0.032))
        _form.btnView.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        _form.btnView.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnView.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
