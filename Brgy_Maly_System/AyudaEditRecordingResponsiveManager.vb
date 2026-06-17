Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for AyudaEditRecording_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class AyudaEditRecordingResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As AyudaEditRecording_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As AyudaEditRecording_Form)
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
        PositionAyudaInfoSection(panelWidth, panelHeight, scaleFactor)
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title section
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' EditResidentAyudaLbl - Designer: Location(20, 30)
        _form.EditResidentAyudaLbl.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.03))
        _form.EditResidentAyudaLbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
        _form.EditResidentAyudaLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position Resident Information section.
    ''' UPDATED: Re-applies read-only visual lock state on every layout pass
    ''' so locked fields never accidentally revert after a resolution change.
    ''' </summary>
    Private Sub PositionResidentInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.05)
        Dim leftFieldW As Integer = CInt(panelWidth * 0.364)
        Dim rightMargin As Integer = CInt(panelWidth * 0.529)
        Dim rightFieldW As Integer = CInt(panelWidth * 0.364)

        ' ── Section Title ─────────────────────────────────────────────
        _form.ResidentInformationLbl.Location = New Point(
            CInt(panelWidth * 0.026), CInt(panelHeight * 0.105))
        _form.ResidentInformationLbl.Font = New Font(
            "Arial", 18.0F * scaleFactor, FontStyle.Bold)

        ' ── LEFT COLUMN ───────────────────────────────────────────────

        ' Resident Name label + field
        _form.ResidentNameLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.145))
        _form.ResidentNameLbl.Font = New Font(
            "Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        _form.txtResidentName.Location = New Point(leftMargin, CInt(panelHeight * 0.172))
        _form.txtResidentName.Size = New Size(leftFieldW, CInt(panelHeight * 0.029))
        _form.txtResidentName.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Age label + field
        _form.AgeLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.22))
        _form.AgeLbl.Font = New Font(
            "Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        _form.txtAge.Location = New Point(leftMargin, CInt(panelHeight * 0.246))
        _form.txtAge.Size = New Size(leftFieldW, CInt(panelHeight * 0.029))
        _form.txtAge.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' ── RIGHT COLUMN ──────────────────────────────────────────────

        ' Sex label + field
        _form.SexLbl.Location = New Point(rightMargin, CInt(panelHeight * 0.145))
        _form.SexLbl.Font = New Font(
            "Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        _form.txtSex.Location = New Point(rightMargin, CInt(panelHeight * 0.172))
        _form.txtSex.Size = New Size(rightFieldW, CInt(panelHeight * 0.029))
        _form.txtSex.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Category label + field
        _form.CategoryLbl.Location = New Point(rightMargin, CInt(panelHeight * 0.22))
        _form.CategoryLbl.Font = New Font(
            "Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        _form.txtCategory.Location = New Point(rightMargin, CInt(panelHeight * 0.246))
        _form.txtCategory.Size = New Size(rightFieldW, CInt(panelHeight * 0.029))
        _form.txtCategory.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' ── CENTRE ROW — Household ────────────────────────────────────
        Dim householdX As Integer = CInt(panelWidth * 0.298)
        Dim householdW As Integer = CInt(panelWidth * 0.364)

        _form.HouseholdLbl.Location = New Point(householdX, CInt(panelHeight * 0.294))
        _form.HouseholdLbl.Font = New Font(
            "Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        _form.txtHousehold.Location = New Point(householdX, CInt(panelHeight * 0.32))
        _form.txtHousehold.Size = New Size(householdW, CInt(panelHeight * 0.029))
        _form.txtHousehold.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' ── RE-ASSERT LOCK STATE after every layout recalculation ─────
        ' Prevents any edge case where a resolution change could reset colors.
        Dim lockedColor As Color = Color.FromArgb(220, 220, 220)
        Dim lockedFore As Color = Color.FromArgb(90, 90, 90)

        For Each lockedCtrl As Control In New Control() {
                _form.txtResidentName,
                _form.txtAge,
                _form.txtSex,
                _form.txtCategory,
                _form.txtHousehold}
            If TypeOf lockedCtrl Is TextBox Then
                Dim tb As TextBox = CType(lockedCtrl, TextBox)
                tb.ReadOnly = True
                tb.BackColor = lockedColor
                tb.ForeColor = lockedFore
                tb.TabStop = False
                tb.Cursor = Cursors.Default
            End If
        Next
    End Sub


    ''' <summary>
    ''' Position horizontal divider line
    ''' </summary>
    Private Sub PositionDividerLine(panelWidth As Integer, panelHeight As Integer)
        ' LinePnl - Designer: Location(0, 415), Size(1700, 2)
        _form.LinePnl.Location = New Point(0, CInt(panelHeight * 0.413))
        _form.LinePnl.Size = New Size(panelWidth, 2)
        _form.LinePnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position Ayuda Information section
    ''' </summary>
    Private Sub PositionAyudaInfoSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftMargin As Integer = CInt(panelWidth * 0.05)
        Dim fullWidth As Integer = CInt(panelWidth * 0.843)

        ' Section Title - Designer: Location(36, 425)
        _form.lblAyudaInformation.Location = New Point(CInt(panelWidth * 0.021), CInt(panelHeight * 0.423))
        _form.lblAyudaInformation.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Bold)

        ' Ayuda ComboBox - Designer: Location(80, 482), cbAyuda(85, 509), Size(1433, 30)
        _form.AyudaLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.48))
        _form.AyudaLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.cbAyuda.Location = New Point(leftMargin, CInt(panelHeight * 0.507))
        _form.cbAyuda.Size = New Size(fullWidth, CInt(panelHeight * 0.03))
        _form.cbAyuda.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Quantity - Designer: Location(80, 563), txtQuantity(85, 589)
        _form.QuantityLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.561))
        _form.QuantityLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.txtQuantity.Location = New Point(leftMargin, CInt(panelHeight * 0.587))
        _form.txtQuantity.Size = New Size(fullWidth, CInt(panelHeight * 0.029))
        _form.txtQuantity.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Date - Designer: Location(80, 640), AyudaDateDTP(85, 668)
        _form.DateLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.638))
        _form.DateLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.AyudaDateDTP.Location = New Point(leftMargin, CInt(panelHeight * 0.665))
        _form.AyudaDateDTP.Size = New Size(fullWidth, CInt(panelHeight * 0.029))
        _form.AyudaDateDTP.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Description - Designer: Location(80, 719), DescriptionRtxt(85, 747), Size(1433, 101)
        _form.DescriptionLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.716))
        _form.DescriptionLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.DescriptionRtxt.Location = New Point(leftMargin, CInt(panelHeight * 0.744))
        _form.DescriptionRtxt.Size = New Size(fullWidth, CInt(panelHeight * 0.101))
        _form.DescriptionRtxt.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position action buttons (Save Changes, Back to Main Page)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.133)
        Dim btnHeight As Integer = CInt(panelHeight * 0.045)
        Dim btnY As Integer = CInt(panelHeight * 0.9)

        ' Save Changes Button - Designer: Location(527, 904), Size(226, 45)
        _form.btnSaveChanges.Location = New Point(CInt(panelWidth * 0.31), btnY)
        _form.btnSaveChanges.Size = New Size(btnWidth, btnHeight)
        _form.btnSaveChanges.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        _form.btnSaveChanges.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSaveChanges.Cursor = Cursors.Hand

        ' Back to Main Page Button - Designer: Location(862, 904), Size(226, 45)
        _form.btnBackToMainPage.Location = New Point(CInt(panelWidth * 0.507), btnY)
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
