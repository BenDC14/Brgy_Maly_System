Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for ManageUserAccount_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class ManageUserAccountResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As ManageUserAccount_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As ManageUserAccount_Form)
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

        ' === FORM FIELDS SECTION ===
        PositionFormFields(panelWidth, panelHeight, scaleFactor)

        ' === SAVE BUTTON ===
        PositionSaveButton(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title label at top left
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(20, 40) on 1700x1004
        _form.ManageAccountLbl.Location = New Point(CInt(20 * scaleFactor), CInt(40 * scaleFactor))
        _form.ManageAccountLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.ManageAccountLbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position all form input fields in a vertical layout
    ''' </summary>
    Private Sub PositionFormFields(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Common dimensions
        Dim fieldMarginLeft As Integer = CInt(78 * scaleFactor)
        Dim fieldWidth As Integer = CInt(1506 * scaleFactor)
        Dim fieldHeight As Integer = CInt(35 * scaleFactor)

        ' === FIRST NAME ===
        ' Label - Designer: Location(73, 145)
        _form.FNameLbl.Location = New Point(CInt(73 * scaleFactor), CInt(145 * scaleFactor))
        _form.FNameLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.FNameLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' TextBox - Designer: Location(78, 173)
        _form.txtFname.Location = New Point(fieldMarginLeft, CInt(173 * scaleFactor))
        _form.txtFname.Size = New Size(fieldWidth, fieldHeight)
        _form.txtFname.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtFname.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Regular)

        ' === LAST NAME ===
        ' Label - Designer: Location(73, 266)
        _form.LnameLbl.Location = New Point(CInt(73 * scaleFactor), CInt(266 * scaleFactor))
        _form.LnameLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.LnameLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' TextBox - Designer: Location(78, 292)
        _form.txtLname.Location = New Point(fieldMarginLeft, CInt(292 * scaleFactor))
        _form.txtLname.Size = New Size(fieldWidth, fieldHeight)
        _form.txtLname.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtLname.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Regular)

        ' === USERNAME ===
        ' Label - Designer: Location(73, 398)
        _form.UnameLbL.Location = New Point(CInt(73 * scaleFactor), CInt(398 * scaleFactor))
        _form.UnameLbL.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.UnameLbL.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' TextBox - Designer: Location(78, 426)
        _form.txtUname.Location = New Point(fieldMarginLeft, CInt(426 * scaleFactor))
        _form.txtUname.Size = New Size(fieldWidth, fieldHeight)
        _form.txtUname.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtUname.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Regular)
        _form.txtUname.ReadOnly = True ' Username should not be editable

        ' === PASSWORD ===
        ' Label - Designer: Location(73, 516)
        _form.PassLbl.Location = New Point(CInt(73 * scaleFactor), CInt(516 * scaleFactor))
        _form.PassLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.PassLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' TextBox - Designer: Location(78, 544)
        _form.txtPass.Location = New Point(fieldMarginLeft, CInt(544 * scaleFactor))
        _form.txtPass.Size = New Size(fieldWidth, fieldHeight)
        _form.txtPass.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtPass.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Regular)

        ' === SEE PASSWORD BUTTON 1 - Designer: Location(1545, 544), Size(39, 35) ===
        _form.SeePassBtn.Location = New Point(CInt(1545 * scaleFactor), CInt(544 * scaleFactor))
        _form.SeePassBtn.Size = New Size(CInt(39 * scaleFactor), CInt(35 * scaleFactor))
        _form.SeePassBtn.SizeMode = PictureBoxSizeMode.StretchImage
        _form.SeePassBtn.Cursor = Cursors.Hand
        _form.SeePassBtn.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' === CONFIRM PASSWORD ===
        ' Label - Designer: Location(73, 634)
        _form.ConfirmPassLbl.Location = New Point(CInt(73 * scaleFactor), CInt(634 * scaleFactor))
        _form.ConfirmPassLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.ConfirmPassLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' TextBox - Designer: Location(78, 662)
        _form.txtConfirmPass.Location = New Point(fieldMarginLeft, CInt(662 * scaleFactor))
        _form.txtConfirmPass.Size = New Size(fieldWidth, fieldHeight)
        _form.txtConfirmPass.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtConfirmPass.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Regular)

        ' === SEE PASSWORD BUTTON 2 - Designer: Location(1545, 662), Size(39, 35) ===
        _form.SeePassBtn2.Location = New Point(CInt(1545 * scaleFactor), CInt(662 * scaleFactor))
        _form.SeePassBtn2.Size = New Size(CInt(39 * scaleFactor), CInt(35 * scaleFactor))
        _form.SeePassBtn2.SizeMode = PictureBoxSizeMode.StretchImage
        _form.SeePassBtn2.Cursor = Cursors.Hand
        _form.SeePassBtn2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position save button centered at bottom
    ''' </summary>
    Private Sub PositionSaveButton(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(741, 864), Size(260, 76) on 1700x1004
        Dim btnWidth As Integer = CInt(260 * scaleFactor)
        Dim btnHeight As Integer = CInt(76 * scaleFactor)

        _form.btnSaveInfo.Location = New Point(CInt(741 * scaleFactor), CInt(864 * scaleFactor))
        _form.btnSaveInfo.Size = New Size(btnWidth, btnHeight)
        _form.btnSaveInfo.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnSaveInfo.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)
        _form.btnSaveInfo.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
