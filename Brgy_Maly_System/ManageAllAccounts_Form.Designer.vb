<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManageAllAccounts_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.FillPanel = New System.Windows.Forms.Panel()
        Me.cbReports = New System.Windows.Forms.CheckBox()
        Me.cbAyuda = New System.Windows.Forms.CheckBox()
        Me.cbHousehold = New System.Windows.Forms.CheckBox()
        Me.cbResident = New System.Windows.Forms.CheckBox()
        Me.btnArchieve = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblFormsToAccess = New System.Windows.Forms.Label()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.cbRole = New System.Windows.Forms.ComboBox()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.lblPass = New System.Windows.Forms.Label()
        Me.txtUname = New System.Windows.Forms.TextBox()
        Me.lblUname = New System.Windows.Forms.Label()
        Me.txtLname = New System.Windows.Forms.TextBox()
        Me.txtFname = New System.Windows.Forms.TextBox()
        Me.lblLname = New System.Windows.Forms.Label()
        Me.lblFname = New System.Windows.Forms.Label()
        Me.dgvAccounts = New System.Windows.Forms.DataGridView()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.AccountManagementLbl = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        CType(Me.dgvAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.cbReports)
        Me.FillPanel.Controls.Add(Me.cbAyuda)
        Me.FillPanel.Controls.Add(Me.cbHousehold)
        Me.FillPanel.Controls.Add(Me.cbResident)
        Me.FillPanel.Controls.Add(Me.btnArchieve)
        Me.FillPanel.Controls.Add(Me.btnEdit)
        Me.FillPanel.Controls.Add(Me.btnAdd)
        Me.FillPanel.Controls.Add(Me.lblFormsToAccess)
        Me.FillPanel.Controls.Add(Me.lblRole)
        Me.FillPanel.Controls.Add(Me.cbRole)
        Me.FillPanel.Controls.Add(Me.txtPass)
        Me.FillPanel.Controls.Add(Me.lblPass)
        Me.FillPanel.Controls.Add(Me.txtUname)
        Me.FillPanel.Controls.Add(Me.lblUname)
        Me.FillPanel.Controls.Add(Me.txtLname)
        Me.FillPanel.Controls.Add(Me.txtFname)
        Me.FillPanel.Controls.Add(Me.lblLname)
        Me.FillPanel.Controls.Add(Me.lblFname)
        Me.FillPanel.Controls.Add(Me.dgvAccounts)
        Me.FillPanel.Controls.Add(Me.lblSearch)
        Me.FillPanel.Controls.Add(Me.BtnSearch)
        Me.FillPanel.Controls.Add(Me.txtSearch)
        Me.FillPanel.Controls.Add(Me.AccountManagementLbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'cbReports
        '
        Me.cbReports.AutoSize = True
        Me.cbReports.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReports.Location = New System.Drawing.Point(1312, 893)
        Me.cbReports.Name = "cbReports"
        Me.cbReports.Size = New System.Drawing.Size(133, 23)
        Me.cbReports.TabIndex = 13
        Me.cbReports.Text = "Reports Form"
        Me.cbReports.UseVisualStyleBackColor = True
        '
        'cbAyuda
        '
        Me.cbAyuda.AutoSize = True
        Me.cbAyuda.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAyuda.Location = New System.Drawing.Point(902, 893)
        Me.cbAyuda.Name = "cbAyuda"
        Me.cbAyuda.Size = New System.Drawing.Size(120, 23)
        Me.cbAyuda.TabIndex = 12
        Me.cbAyuda.Text = "Ayuda Form"
        Me.cbAyuda.UseVisualStyleBackColor = True
        '
        'cbHousehold
        '
        Me.cbHousehold.AutoSize = True
        Me.cbHousehold.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbHousehold.Location = New System.Drawing.Point(479, 893)
        Me.cbHousehold.Name = "cbHousehold"
        Me.cbHousehold.Size = New System.Drawing.Size(156, 23)
        Me.cbHousehold.TabIndex = 11
        Me.cbHousehold.Text = "Household Form"
        Me.cbHousehold.UseVisualStyleBackColor = True
        '
        'cbResident
        '
        Me.cbResident.AutoSize = True
        Me.cbResident.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbResident.Location = New System.Drawing.Point(90, 893)
        Me.cbResident.Name = "cbResident"
        Me.cbResident.Size = New System.Drawing.Size(149, 23)
        Me.cbResident.TabIndex = 10
        Me.cbResident.Text = "Residents Form"
        Me.cbResident.UseVisualStyleBackColor = True
        '
        'btnArchieve
        '
        Me.btnArchieve.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnArchieve.FlatAppearance.BorderSize = 0
        Me.btnArchieve.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnArchieve.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnArchieve.Location = New System.Drawing.Point(1473, 735)
        Me.btnArchieve.Name = "btnArchieve"
        Me.btnArchieve.Size = New System.Drawing.Size(174, 32)
        Me.btnArchieve.TabIndex = 16
        Me.btnArchieve.Text = "Archieve Account"
        Me.btnArchieve.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnEdit.FlatAppearance.BorderSize = 0
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.Location = New System.Drawing.Point(1473, 670)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(174, 32)
        Me.btnEdit.TabIndex = 15
        Me.btnEdit.Text = "Edit Account"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(1473, 605)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(174, 32)
        Me.btnAdd.TabIndex = 14
        Me.btnAdd.Text = "Add Account"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'lblFormsToAccess
        '
        Me.lblFormsToAccess.AutoSize = True
        Me.lblFormsToAccess.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormsToAccess.Location = New System.Drawing.Point(57, 858)
        Me.lblFormsToAccess.Name = "lblFormsToAccess"
        Me.lblFormsToAccess.Size = New System.Drawing.Size(164, 22)
        Me.lblFormsToAccess.TabIndex = 0
        Me.lblFormsToAccess.Text = "Forms to access"
        '
        'lblRole
        '
        Me.lblRole.AutoSize = True
        Me.lblRole.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRole.Location = New System.Drawing.Point(57, 721)
        Me.lblRole.Name = "lblRole"
        Me.lblRole.Size = New System.Drawing.Size(51, 22)
        Me.lblRole.TabIndex = 0
        Me.lblRole.Text = "Role"
        '
        'cbRole
        '
        Me.cbRole.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbRole.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRole.FormattingEnabled = True
        Me.cbRole.Items.AddRange(New Object() {"Super Admin", "Admin"})
        Me.cbRole.Location = New System.Drawing.Point(57, 747)
        Me.cbRole.Name = "cbRole"
        Me.cbRole.Size = New System.Drawing.Size(1388, 35)
        Me.cbRole.TabIndex = 9
        '
        'txtPass
        '
        Me.txtPass.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtPass.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPass.Location = New System.Drawing.Point(833, 670)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPass.Size = New System.Drawing.Size(612, 32)
        Me.txtPass.TabIndex = 8
        '
        'lblPass
        '
        Me.lblPass.AutoSize = True
        Me.lblPass.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPass.Location = New System.Drawing.Point(829, 645)
        Me.lblPass.Name = "lblPass"
        Me.lblPass.Size = New System.Drawing.Size(103, 22)
        Me.lblPass.TabIndex = 0
        Me.lblPass.Text = "Password"
        '
        'txtUname
        '
        Me.txtUname.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtUname.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUname.Location = New System.Drawing.Point(57, 670)
        Me.txtUname.Name = "txtUname"
        Me.txtUname.Size = New System.Drawing.Size(612, 32)
        Me.txtUname.TabIndex = 6
        '
        'lblUname
        '
        Me.lblUname.AutoSize = True
        Me.lblUname.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUname.Location = New System.Drawing.Point(57, 645)
        Me.lblUname.Name = "lblUname"
        Me.lblUname.Size = New System.Drawing.Size(104, 22)
        Me.lblUname.TabIndex = 0
        Me.lblUname.Text = "Username"
        '
        'txtLname
        '
        Me.txtLname.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtLname.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLname.Location = New System.Drawing.Point(833, 584)
        Me.txtLname.Name = "txtLname"
        Me.txtLname.Size = New System.Drawing.Size(612, 32)
        Me.txtLname.TabIndex = 5
        '
        'txtFname
        '
        Me.txtFname.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFname.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFname.Location = New System.Drawing.Point(57, 584)
        Me.txtFname.Name = "txtFname"
        Me.txtFname.Size = New System.Drawing.Size(612, 32)
        Me.txtFname.TabIndex = 4
        '
        'lblLname
        '
        Me.lblLname.AutoSize = True
        Me.lblLname.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLname.Location = New System.Drawing.Point(829, 559)
        Me.lblLname.Name = "lblLname"
        Me.lblLname.Size = New System.Drawing.Size(107, 22)
        Me.lblLname.TabIndex = 0
        Me.lblLname.Text = "Last Name"
        '
        'lblFname
        '
        Me.lblFname.AutoSize = True
        Me.lblFname.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFname.Location = New System.Drawing.Point(57, 559)
        Me.lblFname.Name = "lblFname"
        Me.lblFname.Size = New System.Drawing.Size(109, 22)
        Me.lblFname.TabIndex = 0
        Me.lblFname.Text = "First Name"
        '
        'dgvAccounts
        '
        Me.dgvAccounts.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAccounts.Location = New System.Drawing.Point(57, 145)
        Me.dgvAccounts.Name = "dgvAccounts"
        Me.dgvAccounts.Size = New System.Drawing.Size(1583, 373)
        Me.dgvAccounts.TabIndex = 3
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(53, 82)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(135, 22)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search Users"
        '
        'BtnSearch
        '
        Me.BtnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnSearch.FlatAppearance.BorderSize = 0
        Me.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSearch.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(1513, 107)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(127, 32)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "Search"
        Me.BtnSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(57, 107)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(1450, 32)
        Me.txtSearch.TabIndex = 1
        '
        'AccountManagementLbl
        '
        Me.AccountManagementLbl.AutoSize = True
        Me.AccountManagementLbl.Font = New System.Drawing.Font("Arial", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AccountManagementLbl.Location = New System.Drawing.Point(710, 14)
        Me.AccountManagementLbl.Name = "AccountManagementLbl"
        Me.AccountManagementLbl.Size = New System.Drawing.Size(371, 41)
        Me.AccountManagementLbl.TabIndex = 0
        Me.AccountManagementLbl.Text = "Account Management"
        '
        'ManageAllAccounts_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ManageAllAccounts_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.dgvAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents AccountManagementLbl As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents BtnSearch As Button
    Friend WithEvents dgvAccounts As DataGridView
    Friend WithEvents lblSearch As Label
    Friend WithEvents lblUname As Label
    Friend WithEvents txtLname As TextBox
    Friend WithEvents txtFname As TextBox
    Friend WithEvents lblLname As Label
    Friend WithEvents lblFname As Label
    Friend WithEvents txtPass As TextBox
    Friend WithEvents lblPass As Label
    Friend WithEvents txtUname As TextBox
    Friend WithEvents cbRole As ComboBox
    Friend WithEvents lblRole As Label
    Friend WithEvents lblFormsToAccess As Label
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnArchieve As Button
    Friend WithEvents cbHousehold As CheckBox
    Friend WithEvents cbResident As CheckBox
    Friend WithEvents cbReports As CheckBox
    Friend WithEvents cbAyuda As CheckBox
End Class
