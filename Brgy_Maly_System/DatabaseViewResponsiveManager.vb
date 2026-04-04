Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for DatabaseView_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class DatabaseViewResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As DatabaseView_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As DatabaseView_Form)
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

        ' === LOG DETAIL FIELDS (7 fields in vertical layout) ===
        PositionLogDetailsFields(panelWidth, panelHeight, scaleFactor)

        ' === BACK TO MAIN BUTTON ===
        PositionBackButton(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title label at top
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(38, 33) on 1700x1004 = 2.2% from left, 3.3% from top
        _form.lblLogDetails.Location = New Point(CInt(panelWidth * 0.022), CInt(panelHeight * 0.033))
        _form.lblLogDetails.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblLogDetails.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position all 7 log detail fields in vertical layout
    ''' </summary>
    Private Sub PositionLogDetailsFields(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim fieldMarginLeft As Integer = CInt(panelWidth * 0.052) ' 89/1700 ≈ 5.2%
        Dim fieldWidth As Integer = CInt(panelWidth * 0.844) ' 1435/1700 ≈ 84.4%
        Dim fieldHeight As Integer = CInt(panelHeight * 0.029) ' 29/1004 ≈ 2.9%
        Dim labelMarginLeft As Integer = CInt(panelWidth * 0.05) ' 85/1700 ≈ 5%

        ' === 1. LOG TYPE ===
        ' Label - Designer: Location(85, 124) = 5% from left, 12.4% from top
        _form.lblLogType.Location = New Point(labelMarginLeft, CInt(panelHeight * 0.124))
        _form.lblLogType.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblLogType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' TextBox - Designer: Location(89, 149), Size(1435, 29)
        _form.txtLogType.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.148))
        _form.txtLogType.Size = New Size(fieldWidth, fieldHeight)
        _form.txtLogType.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtLogType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === 2. STATUS ===
        ' Label - Designer: Location(85, 229) = 5% from left, 22.8% from top
        _form.lblStatus.Location = New Point(labelMarginLeft, CInt(panelHeight * 0.228))
        _form.lblStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblStatus.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' TextBox - Designer: Location(89, 254), Size(1435, 29)
        _form.txtStatus.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.253))
        _form.txtStatus.Size = New Size(fieldWidth, fieldHeight)
        _form.txtStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtStatus.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === 3. DATE AND TIME ===
        ' Label - Designer: Location(85, 345) = 5% from left, 34.4% from top
        _form.lblDateTime.Location = New Point(labelMarginLeft, CInt(panelHeight * 0.344))
        _form.lblDateTime.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblDateTime.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' TextBox - Designer: Location(89, 370), Size(1435, 29)
        _form.txtDateAndTime.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.369))
        _form.txtDateAndTime.Size = New Size(fieldWidth, fieldHeight)
        _form.txtDateAndTime.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtDateAndTime.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === 4. PERFORMED BY ===
        ' Label - Designer: Location(85, 459) = 5% from left, 45.7% from top
        _form.lblPerformedBy.Location = New Point(labelMarginLeft, CInt(panelHeight * 0.457))
        _form.lblPerformedBy.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblPerformedBy.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' TextBox - Designer: Location(89, 484), Size(1435, 29)
        _form.txtPerformedBy.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.482))
        _form.txtPerformedBy.Size = New Size(fieldWidth, fieldHeight)
        _form.txtPerformedBy.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtPerformedBy.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === 5. FILE NAME ===
        ' Label - Designer: Location(85, 573) = 5% from left, 57.1% from top
        _form.lblFileName.Location = New Point(labelMarginLeft, CInt(panelHeight * 0.571))
        _form.lblFileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblFileName.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' TextBox - Designer: Location(89, 598), Size(1435, 29)
        _form.TextBox2.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.596))
        _form.TextBox2.Size = New Size(fieldWidth, fieldHeight)
        _form.TextBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.TextBox2.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === 6. FILE PATH ===
        ' Label - Designer: Location(85, 689) = 5% from left, 68.6% from top
        _form.lblFilePath.Location = New Point(labelMarginLeft, CInt(panelHeight * 0.686))
        _form.lblFilePath.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblFilePath.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' TextBox - Designer: Location(89, 714), Size(1435, 29)
        _form.TextBox1.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.711))
        _form.TextBox1.Size = New Size(fieldWidth, fieldHeight)
        _form.TextBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.TextBox1.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === 7. ERROR MESSAGE ===
        ' Label - Designer: Location(85, 805) = 5% from left, 80.2% from top
        _form.lblErrorMessage.Location = New Point(labelMarginLeft, CInt(panelHeight * 0.802))
        _form.lblErrorMessage.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblErrorMessage.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' TextBox - Designer: Location(89, 830), Size(1435, 29)
        _form.txtErrorMessage.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.827))
        _form.txtErrorMessage.Size = New Size(fieldWidth, fieldHeight)
        _form.txtErrorMessage.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtErrorMessage.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position back to main button at bottom center
    ''' </summary>
    Private Sub PositionBackButton(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.111) ' 189/1700 ≈ 11.1%
        Dim btnHeight As Integer = CInt(panelHeight * 0.042) ' 42/1004 ≈ 4.2%

        ' Back to Main Button - Designer: Location(753, 924), Size(189, 42)
        ' Centered horizontally: 753/1700 ≈ 44.3%, 924/1004 ≈ 92%
        _form.btnBacktoMain.Location = New Point(CInt(panelWidth * 0.443), CInt(panelHeight * 0.92))
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
