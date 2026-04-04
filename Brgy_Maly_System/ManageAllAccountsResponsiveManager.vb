Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for ManageAllAccounts_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class ManageAllAccountsResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As ManageAllAccounts_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As ManageAllAccounts_Form)
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

        ' === SEARCH SECTION ===
        PositionSearchSection(panelWidth, panelHeight, scaleFactor)

        ' === DATA GRID VIEW ===
        PositionDataGridView(panelWidth, panelHeight)

        ' === FORM FIELDS SECTION ===
        PositionFormFields(panelWidth, panelHeight, scaleFactor)

        ' === ROLE SECTION ===
        PositionRoleSection(panelWidth, panelHeight, scaleFactor)

        ' === FORMS TO ACCESS SECTION ===
        PositionFormsAccessSection(panelWidth, panelHeight, scaleFactor)

        ' === ACTION BUTTONS ===
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position title label at top center
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Designer: Location(710, 14) on 1700x1004 = 41.8% from left, 1.4% from top
        _form.AccountManagementLbl.Location = New Point(CInt(panelWidth * 0.418), CInt(panelHeight * 0.014))
        _form.AccountManagementLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.AccountManagementLbl.Font = New Font("Arial", 26.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position search label, textbox, and button
    ''' </summary>
    Private Sub PositionSearchSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Search Label - Designer: Location(53, 82)
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.031), CInt(panelHeight * 0.082))
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Search Textbox - Designer: Location(57, 107), Size(1450, 32)
        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.034), CInt(panelHeight * 0.107))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.853), CInt(panelHeight * 0.032))
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtSearch.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        ' Search Button - Designer: Location(1513, 107), Size(127, 32)
        _form.BtnSearch.Location = New Point(CInt(panelWidth * 0.89), CInt(panelHeight * 0.107))
        _form.BtnSearch.Size = New Size(CInt(panelWidth * 0.075), CInt(panelHeight * 0.032))
        _form.BtnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.BtnSearch.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.BtnSearch.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position DataGridView for accounts table
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        ' Designer: Location(57, 145), Size(1583, 373)
        _form.dgvAccounts.Location = New Point(CInt(panelWidth * 0.034), CInt(panelHeight * 0.144))
        _form.dgvAccounts.Size = New Size(CInt(panelWidth * 0.931), CInt(panelHeight * 0.371))
        _form.dgvAccounts.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position form input fields (First Name, Last Name, Username, Password)
    ''' </summary>
    Private Sub PositionFormFields(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim fieldMarginLeft As Integer = CInt(panelWidth * 0.034)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.36)
        Dim fieldHeight As Integer = CInt(panelHeight * 0.032)
        Dim fieldRightMargin As Integer = CInt(panelWidth * 0.49)

        ' === FIRST NAME ===
        _form.lblFname.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.557))
        _form.lblFname.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblFname.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        _form.txtFname.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.582))
        _form.txtFname.Size = New Size(fieldWidth, fieldHeight)
        _form.txtFname.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.txtFname.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        ' === LAST NAME ===
        _form.lblLname.Location = New Point(fieldRightMargin, CInt(panelHeight * 0.557))
        _form.lblLname.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblLname.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        _form.txtLname.Location = New Point(fieldRightMargin, CInt(panelHeight * 0.582))
        _form.txtLname.Size = New Size(fieldWidth, fieldHeight)
        _form.txtLname.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.txtLname.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        ' === USERNAME ===
        _form.lblUname.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.642))
        _form.lblUname.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblUname.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        _form.txtUname.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.667))
        _form.txtUname.Size = New Size(fieldWidth, fieldHeight)
        _form.txtUname.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.txtUname.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        ' === PASSWORD ===
        _form.lblPass.Location = New Point(fieldRightMargin, CInt(panelHeight * 0.642))
        _form.lblPass.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblPass.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        _form.txtPass.Location = New Point(fieldRightMargin, CInt(panelHeight * 0.667))
        _form.txtPass.Size = New Size(fieldWidth, fieldHeight)
        _form.txtPass.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.txtPass.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position role label and combobox
    ''' </summary>
    Private Sub PositionRoleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim fieldMarginLeft As Integer = CInt(panelWidth * 0.034)

        ' Role Label - Designer: Location(57, 721)
        _form.lblRole.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.718))
        _form.lblRole.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblRole.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Role ComboBox - Designer: Location(57, 747), Size(1388, 35)
        _form.cbRole.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.744))
        _form.cbRole.Size = New Size(CInt(panelWidth * 0.816), CInt(panelHeight * 0.035))
        _form.cbRole.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.cbRole.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Position forms to access label and checkboxes
    ''' </summary>
    Private Sub PositionFormsAccessSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim fieldMarginLeft As Integer = CInt(panelWidth * 0.034)

        ' Forms to Access Label - Designer: Location(57, 858)
        _form.lblFormsToAccess.Location = New Point(fieldMarginLeft, CInt(panelHeight * 0.854))
        _form.lblFormsToAccess.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblFormsToAccess.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Checkboxes - Designer: Resident(90, 893), Household(479, 893), Ayuda(902, 893), Reports(1312, 893)
        _form.cbResident.Location = New Point(CInt(panelWidth * 0.053), CInt(panelHeight * 0.889))
        _form.cbResident.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.cbResident.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbHousehold.Location = New Point(CInt(panelWidth * 0.282), CInt(panelHeight * 0.889))
        _form.cbHousehold.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.cbHousehold.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbAyuda.Location = New Point(CInt(panelWidth * 0.531), CInt(panelHeight * 0.889))
        _form.cbAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.cbAyuda.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)

        _form.cbReports.Location = New Point(CInt(panelWidth * 0.772), CInt(panelHeight * 0.889))
        _form.cbReports.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.cbReports.Font = New Font("Arial", 12.0F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position action buttons (Add, Edit, Archive) on the right side
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.102)
        Dim btnHeight As Integer = CInt(panelHeight * 0.032)
        Dim btnX As Integer = CInt(panelWidth * 0.867)

        ' Add Button - Designer: Location(1473, 605), Size(174, 32)
        _form.btnAdd.Location = New Point(btnX, CInt(panelHeight * 0.603))
        _form.btnAdd.Size = New Size(btnWidth, btnHeight)
        _form.btnAdd.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnAdd.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnAdd.Cursor = Cursors.Hand

        ' Edit Button - Designer: Location(1473, 670), Size(174, 32)
        _form.btnEdit.Location = New Point(btnX, CInt(panelHeight * 0.667))
        _form.btnEdit.Size = New Size(btnWidth, btnHeight)
        _form.btnEdit.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnEdit.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnEdit.Cursor = Cursors.Hand

        ' Archive Button - Designer: Location(1473, 735), Size(174, 32)
        _form.btnArchieve.Location = New Point(btnX, CInt(panelHeight * 0.732))
        _form.btnArchieve.Size = New Size(btnWidth, btnHeight)
        _form.btnArchieve.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnArchieve.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnArchieve.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
