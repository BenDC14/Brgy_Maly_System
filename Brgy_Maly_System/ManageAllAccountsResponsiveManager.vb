Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for ManageAllAccounts_Form.
''' This version matches the updated design that uses dgvFormsToAccess
''' instead of separate form-access checkboxes.
''' </summary>
Public Class ManageAllAccountsResponsiveManager

    ' Original Designer size.
    ' All control positions are scaled from this base resolution.
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' Reference to the form being resized.
    Private ReadOnly _form As ManageAllAccounts_Form

    ' Timer used to avoid recalculating layout too many times during resize.
    Private resizeTimer As New System.Windows.Forms.Timer()

    ' Prevents resize logic from running before first layout is complete.
    Private isLayoutCalculated As Boolean = False

    Public Sub New(form As ManageAllAccounts_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Starts responsive behavior.
    ''' Call this once in ManageAllAccounts_Form_Load.
    ''' </summary>
    Public Sub Initialize()
        ' Make FillPanel occupy the whole form.
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.FillPanel.Location = New Point(0, 0)
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' Debounce resize events.
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' Recalculate layout when form is resized.
        AddHandler _form.Resize, AddressOf Form_Resize

        ' Recalculate layout when Windows display resolution changes.
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' First layout calculation.
        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()

        isLayoutCalculated = True
    End Sub

    ''' <summary>
    ''' Runs when Windows screen/display settings change.
    ''' </summary>
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Runs when the form is resized.
    ''' Timer is used so layout updates after resizing pauses.
    ''' </summary>
    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Exit Sub

        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    ''' <summary>
    ''' Runs after resize pause.
    ''' </summary>
    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Main layout method.
    ''' Calculates scale, then positions every control.
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 OrElse panelHeight < 100 Then Exit Sub

        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        _form.FillPanel.Size = New Size(panelWidth, panelHeight)
        _form.FillPanel.Location = New Point(0, 0)

        PositionTitleSection(panelWidth, panelHeight, scaleFactor)
        PositionSearchSection(panelWidth, panelHeight, scaleFactor)
        PositionAccountsGrid(panelWidth, panelHeight)
        PositionFormFields(panelWidth, panelHeight, scaleFactor)
        PositionRoleSection(panelWidth, panelHeight, scaleFactor)
        PositionActionButtons(panelWidth, panelHeight, scaleFactor)
        PositionPermissionsGrid(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Positions the main title.
    ''' Designer: AccountManagementLbl Location(710, 14)
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        _form.AccountManagementLbl.Location = New Point(CInt(panelWidth * 0.418), CInt(panelHeight * 0.014))
        _form.AccountManagementLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.AccountManagementLbl.Font = New Font("Arial", 26.25F * scaleFactor, FontStyle.Bold)
        _form.AccountManagementLbl.AutoSize = True
    End Sub

    ''' <summary>
    ''' Positions search label, search textbox, and search button.
    ''' </summary>
    Private Sub PositionSearchSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.031), CInt(panelHeight * 0.082))
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblSearch.AutoSize = True

        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.034), CInt(panelHeight * 0.107))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.853), CInt(panelHeight * 0.032))
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.txtSearch.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        _form.BtnSearch.Location = New Point(CInt(panelWidth * 0.89), CInt(panelHeight * 0.107))
        _form.BtnSearch.Size = New Size(CInt(panelWidth * 0.075), CInt(panelHeight * 0.032))
        _form.BtnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.BtnSearch.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.BtnSearch.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Positions the accounts DataGridView.
    ''' Designer: dgvAccounts Location(57, 145), Size(1583, 270)
    ''' </summary>
    Private Sub PositionAccountsGrid(panelWidth As Integer, panelHeight As Integer)
        _form.dgvAccounts.Location = New Point(CInt(panelWidth * 0.034), CInt(panelHeight * 0.144))
        _form.dgvAccounts.Size = New Size(CInt(panelWidth * 0.931), CInt(panelHeight * 0.269))
        _form.dgvAccounts.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Positions First Name, Last Name, Username, and Password fields.
    ''' These positions match your updated Designer.
    ''' </summary>
    Private Sub PositionFormFields(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftX As Integer = CInt(panelWidth * 0.034)
        Dim rightX As Integer = CInt(panelWidth * 0.49)
        Dim fieldWidth As Integer = CInt(panelWidth * 0.36)
        Dim fieldHeight As Integer = CInt(panelHeight * 0.032)

        ' First Name
        _form.lblFname.Location = New Point(leftX, CInt(panelHeight * 0.434))
        _form.lblFname.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblFname.AutoSize = True

        _form.txtFname.Location = New Point(leftX, CInt(panelHeight * 0.459))
        _form.txtFname.Size = New Size(fieldWidth, fieldHeight)
        _form.txtFname.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        ' Last Name
        _form.lblLname.Location = New Point(rightX, CInt(panelHeight * 0.434))
        _form.lblLname.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblLname.AutoSize = True

        _form.txtLname.Location = New Point(rightX, CInt(panelHeight * 0.459))
        _form.txtLname.Size = New Size(fieldWidth, fieldHeight)
        _form.txtLname.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        ' Username
        _form.lblUname.Location = New Point(leftX, CInt(panelHeight * 0.52))
        _form.lblUname.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblUname.AutoSize = True

        _form.txtUname.Location = New Point(leftX, CInt(panelHeight * 0.545))
        _form.txtUname.Size = New Size(fieldWidth, fieldHeight)
        _form.txtUname.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)

        ' Password
        _form.lblPass.Location = New Point(rightX, CInt(panelHeight * 0.52))
        _form.lblPass.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblPass.AutoSize = True

        _form.txtPass.Location = New Point(rightX, CInt(panelHeight * 0.545))
        _form.txtPass.Size = New Size(fieldWidth, fieldHeight)
        _form.txtPass.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Positions Role label and Role ComboBox.
    ''' Designer: lblRole Location(57, 598), cbRole Location(57, 624)
    ''' </summary>
    Private Sub PositionRoleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim leftX As Integer = CInt(panelWidth * 0.034)

        _form.lblRole.Location = New Point(leftX, CInt(panelHeight * 0.596))
        _form.lblRole.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.lblRole.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblRole.AutoSize = True

        _form.cbRole.Location = New Point(leftX, CInt(panelHeight * 0.622))
        _form.cbRole.Size = New Size(CInt(panelWidth * 0.816), CInt(panelHeight * 0.035))
        _form.cbRole.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        _form.cbRole.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Positions Add, Edit, and Archive account buttons.
    ''' Designer:
    ''' btnAdd Location(1473, 482)
    ''' btnEdit Location(1473, 547)
    ''' btnArchieve Location(1473, 612)
    ''' </summary>
    Private Sub PositionActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Dim btnWidth As Integer = CInt(panelWidth * 0.102)
        Dim btnHeight As Integer = CInt(panelHeight * 0.032)
        Dim btnX As Integer = CInt(panelWidth * 0.867)

        _form.btnAdd.Location = New Point(btnX, CInt(panelHeight * 0.48))
        _form.btnAdd.Size = New Size(btnWidth, btnHeight)
        _form.btnAdd.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnAdd.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnAdd.Cursor = Cursors.Hand

        _form.btnEdit.Location = New Point(btnX, CInt(panelHeight * 0.545))
        _form.btnEdit.Size = New Size(btnWidth, btnHeight)
        _form.btnEdit.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnEdit.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnEdit.Cursor = Cursors.Hand

        _form.btnArchieve.Location = New Point(btnX, CInt(panelHeight * 0.61))
        _form.btnArchieve.Size = New Size(btnWidth, btnHeight)
        _form.btnArchieve.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnArchieve.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnArchieve.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Positions the permissions DataGridView.
    ''' This replaces the old Forms to Access checkbox section.
    ''' Designer: dgvFormsToAccess Location(57, 688), Size(1583, 270)
    ''' </summary>
    Private Sub PositionPermissionsGrid(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        _form.dgvFormsToAccess.Location = New Point(CInt(panelWidth * 0.034), CInt(panelHeight * 0.685))
        _form.dgvFormsToAccess.Size = New Size(CInt(panelWidth * 0.931), CInt(panelHeight * 0.269))
        _form.dgvFormsToAccess.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        _form.dgvFormsToAccess.Font = New Font("Arial", 10.0F * scaleFactor, FontStyle.Regular)
    End Sub

    ''' <summary>
    ''' Removes event handlers when form closes.
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()

        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
        RemoveHandler _form.Resize, AddressOf Form_Resize
        RemoveHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        resizeTimer.Dispose()
    End Sub

End Class
