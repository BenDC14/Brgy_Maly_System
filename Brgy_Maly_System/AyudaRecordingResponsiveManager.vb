Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for AyudaRecording_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class AyudaRecordingResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As AyudaRecording_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As AyudaRecording_Form)
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

        ' === POSITION ALL SECTIONS ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)
        PositionResidentInfoSection(panelWidth, panelHeight, scaleFactor)
        PositionDividerLine(panelWidth, panelHeight)
        PositionAyudaRecordingFields(panelWidth, panelHeight, scaleFactor)
        PositionActionButton(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title section
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' AyudaRecordinglbl - Designer: Location(20, 40)
        _form.AyudaRecordinglbl.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.04))
        _form.AyudaRecordinglbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.AyudaRecordinglbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position Resident Information section (DataGridView and filter)
    ''' </summary>
    Private Sub PositionResidentInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.028)

        ' Section Title - Designer: Location(43, 110)
        _form.ResidentInformationLbl.Location = New Point(CInt(panelWidth * 0.025), CInt(panelHeight * 0.11))
        _form.ResidentInformationLbl.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Bold)

        ' DataGridView - Designer: Location(48, 142), Size(1429, 325)
        _form.dgvResidents.Location = New Point(leftMargin, CInt(panelHeight * 0.141))
        _form.dgvResidents.Size = New Size(CInt(panelWidth * 0.841), CInt(panelHeight * 0.324))
        _form.dgvResidents.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        ' Search Resident Type Filter - Designer: Location(1483, 119), cbResidentType(1483, 142), Size(212, 30)
        _form.SearchResidentTypeLbl.Location = New Point(CInt(panelWidth * 0.872), CInt(panelHeight * 0.119))
        _form.SearchResidentTypeLbl.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.SearchResidentTypeLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        _form.cbResidentType.Location = New Point(CInt(panelWidth * 0.872), CInt(panelHeight * 0.141))
        _form.cbResidentType.Size = New Size(CInt(panelWidth * 0.125), CInt(panelHeight * 0.03))
        _form.cbResidentType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
        _form.cbResidentType.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position horizontal divider line
    ''' </summary>
    Private Sub PositionDividerLine(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl - Designer: Location(0, 501), Size(1700, 2)
        _form.LinePnl.Location = New Point(0, CInt(panelHeight * 0.499))
        _form.LinePnl.Size = New Size(panelWidth, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Ayuda Recording fields section (UPDATED POSITIONS)
    ''' </summary>
    Private Sub PositionAyudaRecordingFields(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.028)
        Dim leftFieldWidth As Integer = CInt(panelWidth * 0.364)
        Dim rightMargin As Integer = CInt(panelWidth * 0.629)
        Dim rightFieldWidth As Integer = CInt(panelWidth * 0.364)
        Dim fullWidth As Integer = CInt(panelWidth * 0.965)

        ' === LEFT COLUMN ===
        ' Resident Type - Designer: Location(43, 515), txtResidentType(48, 543), Size(618, 29)
        _form.ResidentTypeLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.513))
        _form.ResidentTypeLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.txtResidentType.Location = New Point(leftMargin, CInt(panelHeight * 0.541))
        _form.txtResidentType.Size = New Size(leftFieldWidth, CInt(panelHeight * 0.029))
        _form.txtResidentType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Quantity - Designer: Location(43, 617), txtQuantity(48, 643), Size(618, 29)
        _form.QuantityLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.615))
        _form.QuantityLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.txtQuantity.Location = New Point(leftMargin, CInt(panelHeight * 0.64))
        _form.txtQuantity.Size = New Size(leftFieldWidth, CInt(panelHeight * 0.029))
        _form.txtQuantity.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === RIGHT COLUMN ===
        ' Ayuda - Designer: Location(1065, 516), cbAyuda(1070, 543), Size(618, 30)
        _form.AyudaLbl.Location = New Point(rightMargin, CInt(panelHeight * 0.514))
        _form.AyudaLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.cbAyuda.Location = New Point(rightMargin, CInt(panelHeight * 0.541))
        _form.cbAyuda.Size = New Size(rightFieldWidth, CInt(panelHeight * 0.03))
        _form.cbAyuda.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Date - Designer: Location(1065, 615), AyudaDateDTP(1070, 643), Size(618, 29)
        _form.DateLbl.Location = New Point(rightMargin, CInt(panelHeight * 0.613))
        _form.DateLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.AyudaDateDTP.Location = New Point(rightMargin, CInt(panelHeight * 0.64))
        _form.AyudaDateDTP.Size = New Size(rightFieldWidth, CInt(panelHeight * 0.029))
        _form.AyudaDateDTP.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' === FULL WIDTH ===
        ' Description - Designer: Location(43, 714), DescriptionRtxt(48, 742), Size(1640, 108)
        _form.DescriptionLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.711))
        _form.DescriptionLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.DescriptionRtxt.Location = New Point(leftMargin, CInt(panelHeight * 0.739))
        _form.DescriptionRtxt.Size = New Size(fullWidth, CInt(panelHeight * 0.108))
        _form.DescriptionRtxt.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position action button (Back to Main Page only - centered)
    ''' </summary>
    Private Sub PositionActionButton(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.133)
        Dim btnHeight As Integer = CInt(panelHeight * 0.045)
        Dim btnY As Integer = CInt(panelHeight * 0.906)

        ' Back to Main Page Button (Centered) - Designer: Location(635, 910), Size(226, 45)
        _form.btnBackToMainPage.Location = New Point(CInt(panelWidth * 0.374), btnY)
        _form.btnBackToMainPage.Size = New Size(btnWidth, btnHeight)
        _form.btnBackToMainPage.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnBackToMainPage.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnBackToMainPage.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
