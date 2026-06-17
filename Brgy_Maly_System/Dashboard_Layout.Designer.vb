    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class Dashboard_Layout
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dashboard_Layout))
        Me.MenuPanel = New System.Windows.Forms.Panel()
        Me.LeftPanel = New System.Windows.Forms.Panel()
        Me.LogoTextPanel = New System.Windows.Forms.Panel()
        Me.BrgyMalySystemLbl = New System.Windows.Forms.Label()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.CenterPanel = New System.Windows.Forms.Panel()
        Me.btnNewRelationshipType = New System.Windows.Forms.Button()
        Me.btnNewCategoryAdding = New System.Windows.Forms.Button()
        Me.btnHouseholdAdding = New System.Windows.Forms.Button()
        Me.btnAyudaAdding = New System.Windows.Forms.Button()
        Me.BtnLogOut = New System.Windows.Forms.Button()
        Me.BtnAccounts = New System.Windows.Forms.Button()
        Me.BtnHousehold = New System.Windows.Forms.Button()
        Me.BtnReports = New System.Windows.Forms.Button()
        Me.BtnDatabaseBackup = New System.Windows.Forms.Button()
        Me.BtnAyudaProgram = New System.Windows.Forms.Button()
        Me.DashboardBtn = New System.Windows.Forms.Button()
        Me.BtnEditBarangayInfo = New System.Windows.Forms.Button()
        Me.BtnAudit = New System.Windows.Forms.Button()
        Me.ResidentBtn = New System.Windows.Forms.Button()
        Me.LogoPic = New System.Windows.Forms.PictureBox()
        Me.UserAccountBtn = New System.Windows.Forms.Button()
        Me.MenuPanel.SuspendLayout()
        Me.LeftPanel.SuspendLayout()
        Me.LogoTextPanel.SuspendLayout()
        Me.TopPanel.SuspendLayout()
        CType(Me.LogoPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuPanel
        '
        Me.MenuPanel.Controls.Add(Me.btnNewRelationshipType)
        Me.MenuPanel.Controls.Add(Me.btnNewCategoryAdding)
        Me.MenuPanel.Controls.Add(Me.btnHouseholdAdding)
        Me.MenuPanel.Controls.Add(Me.btnAyudaAdding)
        Me.MenuPanel.Controls.Add(Me.BtnLogOut)
        Me.MenuPanel.Controls.Add(Me.BtnAccounts)
        Me.MenuPanel.Controls.Add(Me.BtnHousehold)
        Me.MenuPanel.Controls.Add(Me.BtnReports)
        Me.MenuPanel.Controls.Add(Me.BtnDatabaseBackup)
        Me.MenuPanel.Controls.Add(Me.BtnAyudaProgram)
        Me.MenuPanel.Controls.Add(Me.DashboardBtn)
        Me.MenuPanel.Controls.Add(Me.BtnEditBarangayInfo)
        Me.MenuPanel.Controls.Add(Me.BtnAudit)
        Me.MenuPanel.Controls.Add(Me.ResidentBtn)
        Me.MenuPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MenuPanel.Location = New System.Drawing.Point(0, 0)
        Me.MenuPanel.Margin = New System.Windows.Forms.Padding(10, 15, 10, 10)
        Me.MenuPanel.Name = "MenuPanel"
        Me.MenuPanel.Size = New System.Drawing.Size(255, 985)
        Me.MenuPanel.TabIndex = 0
        '
        'LeftPanel
        '
        Me.LeftPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.LeftPanel.Controls.Add(Me.MenuPanel)
        Me.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.LeftPanel.Location = New System.Drawing.Point(0, 76)
        Me.LeftPanel.Name = "LeftPanel"
        Me.LeftPanel.Size = New System.Drawing.Size(255, 985)
        Me.LeftPanel.TabIndex = 1
        '
        'LogoTextPanel
        '
        Me.LogoTextPanel.BackColor = System.Drawing.Color.Transparent
        Me.LogoTextPanel.Controls.Add(Me.LogoPic)
        Me.LogoTextPanel.Controls.Add(Me.BrgyMalySystemLbl)
        Me.LogoTextPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.LogoTextPanel.Location = New System.Drawing.Point(0, 0)
        Me.LogoTextPanel.Name = "LogoTextPanel"
        Me.LogoTextPanel.Size = New System.Drawing.Size(300, 76)
        Me.LogoTextPanel.TabIndex = 6
        '
        'BrgyMalySystemLbl
        '
        Me.BrgyMalySystemLbl.AutoSize = True
        Me.BrgyMalySystemLbl.BackColor = System.Drawing.Color.Transparent
        Me.BrgyMalySystemLbl.Font = New System.Drawing.Font("Arial Narrow", 20.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyMalySystemLbl.Location = New System.Drawing.Point(98, 9)
        Me.BrgyMalySystemLbl.Name = "BrgyMalySystemLbl"
        Me.BrgyMalySystemLbl.Padding = New System.Windows.Forms.Padding(10)
        Me.BrgyMalySystemLbl.Size = New System.Drawing.Size(195, 51)
        Me.BrgyMalySystemLbl.TabIndex = 4
        Me.BrgyMalySystemLbl.Text = "Barangay Maly "
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.Transparent
        Me.TopPanel.Controls.Add(Me.LogoTextPanel)
        Me.TopPanel.Controls.Add(Me.UserAccountBtn)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(1920, 76)
        Me.TopPanel.TabIndex = 0
        '
        'CenterPanel
        '
        Me.CenterPanel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.CenterPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CenterPanel.Location = New System.Drawing.Point(255, 76)
        Me.CenterPanel.Name = "CenterPanel"
        Me.CenterPanel.Size = New System.Drawing.Size(1665, 985)
        Me.CenterPanel.TabIndex = 2
        '
        'btnNewRelationshipType
        '
        Me.btnNewRelationshipType.BackColor = System.Drawing.Color.Transparent
        Me.btnNewRelationshipType.FlatAppearance.BorderSize = 0
        Me.btnNewRelationshipType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewRelationshipType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewRelationshipType.ForeColor = System.Drawing.Color.White
        Me.btnNewRelationshipType.Image = Global.Brgy_Maly_System.My.Resources.Resources.family_relationship
        Me.btnNewRelationshipType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewRelationshipType.Location = New System.Drawing.Point(12, 643)
        Me.btnNewRelationshipType.Name = "btnNewRelationshipType"
        Me.btnNewRelationshipType.Padding = New System.Windows.Forms.Padding(10)
        Me.btnNewRelationshipType.Size = New System.Drawing.Size(243, 51)
        Me.btnNewRelationshipType.TabIndex = 14
        Me.btnNewRelationshipType.Text = "Relationship Adding"
        Me.btnNewRelationshipType.UseVisualStyleBackColor = False
        '
        'btnNewCategoryAdding
        '
        Me.btnNewCategoryAdding.BackColor = System.Drawing.Color.Transparent
        Me.btnNewCategoryAdding.FlatAppearance.BorderSize = 0
        Me.btnNewCategoryAdding.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewCategoryAdding.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewCategoryAdding.ForeColor = System.Drawing.Color.White
        Me.btnNewCategoryAdding.Image = Global.Brgy_Maly_System.My.Resources.Resources.demographic
        Me.btnNewCategoryAdding.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewCategoryAdding.Location = New System.Drawing.Point(19, 700)
        Me.btnNewCategoryAdding.Name = "btnNewCategoryAdding"
        Me.btnNewCategoryAdding.Padding = New System.Windows.Forms.Padding(10)
        Me.btnNewCategoryAdding.Size = New System.Drawing.Size(206, 51)
        Me.btnNewCategoryAdding.TabIndex = 13
        Me.btnNewCategoryAdding.Text = "Category Adding"
        Me.btnNewCategoryAdding.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNewCategoryAdding.UseVisualStyleBackColor = False
        '
        'btnHouseholdAdding
        '
        Me.btnHouseholdAdding.BackColor = System.Drawing.Color.Transparent
        Me.btnHouseholdAdding.FlatAppearance.BorderSize = 0
        Me.btnHouseholdAdding.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHouseholdAdding.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHouseholdAdding.ForeColor = System.Drawing.Color.White
        Me.btnHouseholdAdding.Image = Global.Brgy_Maly_System.My.Resources.Resources.family__1_
        Me.btnHouseholdAdding.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHouseholdAdding.Location = New System.Drawing.Point(12, 531)
        Me.btnHouseholdAdding.Name = "btnHouseholdAdding"
        Me.btnHouseholdAdding.Padding = New System.Windows.Forms.Padding(10)
        Me.btnHouseholdAdding.Size = New System.Drawing.Size(240, 51)
        Me.btnHouseholdAdding.TabIndex = 12
        Me.btnHouseholdAdding.Text = "Household Adding"
        Me.btnHouseholdAdding.UseVisualStyleBackColor = False
        '
        'btnAyudaAdding
        '
        Me.btnAyudaAdding.BackColor = System.Drawing.Color.Transparent
        Me.btnAyudaAdding.FlatAppearance.BorderSize = 0
        Me.btnAyudaAdding.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAyudaAdding.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAyudaAdding.ForeColor = System.Drawing.Color.White
        Me.btnAyudaAdding.Image = Global.Brgy_Maly_System.My.Resources.Resources.ayuda
        Me.btnAyudaAdding.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAyudaAdding.Location = New System.Drawing.Point(19, 588)
        Me.btnAyudaAdding.Name = "btnAyudaAdding"
        Me.btnAyudaAdding.Padding = New System.Windows.Forms.Padding(10)
        Me.btnAyudaAdding.Size = New System.Drawing.Size(194, 51)
        Me.btnAyudaAdding.TabIndex = 11
        Me.btnAyudaAdding.Text = "Ayuda Adding"
        Me.btnAyudaAdding.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAyudaAdding.UseVisualStyleBackColor = False
        '
        'BtnLogOut
        '
        Me.BtnLogOut.BackColor = System.Drawing.Color.Transparent
        Me.BtnLogOut.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnLogOut.FlatAppearance.BorderSize = 0
        Me.BtnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnLogOut.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLogOut.ForeColor = System.Drawing.Color.White
        Me.BtnLogOut.Image = Global.Brgy_Maly_System.My.Resources.Resources.logout
        Me.BtnLogOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnLogOut.Location = New System.Drawing.Point(0, 934)
        Me.BtnLogOut.Name = "BtnLogOut"
        Me.BtnLogOut.Padding = New System.Windows.Forms.Padding(10)
        Me.BtnLogOut.Size = New System.Drawing.Size(255, 51)
        Me.BtnLogOut.TabIndex = 10
        Me.BtnLogOut.Text = "Log-Out"
        Me.BtnLogOut.UseVisualStyleBackColor = False
        '
        'BtnAccounts
        '
        Me.BtnAccounts.BackColor = System.Drawing.Color.Transparent
        Me.BtnAccounts.FlatAppearance.BorderSize = 0
        Me.BtnAccounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAccounts.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAccounts.ForeColor = System.Drawing.Color.White
        Me.BtnAccounts.Image = Global.Brgy_Maly_System.My.Resources.Resources.accounts
        Me.BtnAccounts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnAccounts.Location = New System.Drawing.Point(19, 470)
        Me.BtnAccounts.Name = "BtnAccounts"
        Me.BtnAccounts.Padding = New System.Windows.Forms.Padding(10)
        Me.BtnAccounts.Size = New System.Drawing.Size(206, 51)
        Me.BtnAccounts.TabIndex = 9
        Me.BtnAccounts.Text = "Accounts"
        Me.BtnAccounts.UseVisualStyleBackColor = False
        '
        'BtnHousehold
        '
        Me.BtnHousehold.BackColor = System.Drawing.Color.Transparent
        Me.BtnHousehold.FlatAppearance.BorderSize = 0
        Me.BtnHousehold.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnHousehold.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHousehold.ForeColor = System.Drawing.Color.White
        Me.BtnHousehold.Image = Global.Brgy_Maly_System.My.Resources.Resources.family__1_
        Me.BtnHousehold.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnHousehold.Location = New System.Drawing.Point(19, 148)
        Me.BtnHousehold.Name = "BtnHousehold"
        Me.BtnHousehold.Padding = New System.Windows.Forms.Padding(10)
        Me.BtnHousehold.Size = New System.Drawing.Size(206, 51)
        Me.BtnHousehold.TabIndex = 3
        Me.BtnHousehold.Text = "Households"
        Me.BtnHousehold.UseVisualStyleBackColor = False
        '
        'BtnReports
        '
        Me.BtnReports.BackColor = System.Drawing.Color.Transparent
        Me.BtnReports.FlatAppearance.BorderSize = 0
        Me.BtnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnReports.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReports.ForeColor = System.Drawing.Color.White
        Me.BtnReports.Image = Global.Brgy_Maly_System.My.Resources.Resources.reports_1_
        Me.BtnReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnReports.Location = New System.Drawing.Point(19, 255)
        Me.BtnReports.Name = "BtnReports"
        Me.BtnReports.Padding = New System.Windows.Forms.Padding(10)
        Me.BtnReports.Size = New System.Drawing.Size(206, 51)
        Me.BtnReports.TabIndex = 5
        Me.BtnReports.Text = "Reports"
        Me.BtnReports.UseVisualStyleBackColor = False
        '
        'BtnDatabaseBackup
        '
        Me.BtnDatabaseBackup.BackColor = System.Drawing.Color.Transparent
        Me.BtnDatabaseBackup.FlatAppearance.BorderSize = 0
        Me.BtnDatabaseBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDatabaseBackup.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDatabaseBackup.ForeColor = System.Drawing.Color.White
        Me.BtnDatabaseBackup.Image = Global.Brgy_Maly_System.My.Resources.Resources.database_management
        Me.BtnDatabaseBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDatabaseBackup.Location = New System.Drawing.Point(19, 417)
        Me.BtnDatabaseBackup.Name = "BtnDatabaseBackup"
        Me.BtnDatabaseBackup.Padding = New System.Windows.Forms.Padding(10)
        Me.BtnDatabaseBackup.Size = New System.Drawing.Size(206, 51)
        Me.BtnDatabaseBackup.TabIndex = 8
        Me.BtnDatabaseBackup.Text = "Database Backup"
        Me.BtnDatabaseBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnDatabaseBackup.UseVisualStyleBackColor = False
        '
        'BtnAyudaProgram
        '
        Me.BtnAyudaProgram.BackColor = System.Drawing.Color.Transparent
        Me.BtnAyudaProgram.FlatAppearance.BorderSize = 0
        Me.BtnAyudaProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAyudaProgram.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAyudaProgram.ForeColor = System.Drawing.Color.White
        Me.BtnAyudaProgram.Image = Global.Brgy_Maly_System.My.Resources.Resources.ayuda
        Me.BtnAyudaProgram.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnAyudaProgram.Location = New System.Drawing.Point(19, 201)
        Me.BtnAyudaProgram.Name = "BtnAyudaProgram"
        Me.BtnAyudaProgram.Padding = New System.Windows.Forms.Padding(10)
        Me.BtnAyudaProgram.Size = New System.Drawing.Size(194, 51)
        Me.BtnAyudaProgram.TabIndex = 4
        Me.BtnAyudaProgram.Text = "Ayuda Program"
        Me.BtnAyudaProgram.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnAyudaProgram.UseVisualStyleBackColor = False
        '
        'DashboardBtn
        '
        Me.DashboardBtn.BackColor = System.Drawing.Color.Transparent
        Me.DashboardBtn.FlatAppearance.BorderSize = 0
        Me.DashboardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DashboardBtn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DashboardBtn.ForeColor = System.Drawing.Color.White
        Me.DashboardBtn.Image = CType(resources.GetObject("DashboardBtn.Image"), System.Drawing.Image)
        Me.DashboardBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DashboardBtn.Location = New System.Drawing.Point(19, 41)
        Me.DashboardBtn.Name = "DashboardBtn"
        Me.DashboardBtn.Padding = New System.Windows.Forms.Padding(10)
        Me.DashboardBtn.Size = New System.Drawing.Size(206, 51)
        Me.DashboardBtn.TabIndex = 1
        Me.DashboardBtn.Text = "Dashboard"
        Me.DashboardBtn.UseVisualStyleBackColor = False
        '
        'BtnEditBarangayInfo
        '
        Me.BtnEditBarangayInfo.BackColor = System.Drawing.Color.Transparent
        Me.BtnEditBarangayInfo.FlatAppearance.BorderSize = 0
        Me.BtnEditBarangayInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEditBarangayInfo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEditBarangayInfo.ForeColor = System.Drawing.Color.White
        Me.BtnEditBarangayInfo.Image = Global.Brgy_Maly_System.My.Resources.Resources.edit_barangay_info
        Me.BtnEditBarangayInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnEditBarangayInfo.Location = New System.Drawing.Point(19, 309)
        Me.BtnEditBarangayInfo.Name = "BtnEditBarangayInfo"
        Me.BtnEditBarangayInfo.Padding = New System.Windows.Forms.Padding(10)
        Me.BtnEditBarangayInfo.Size = New System.Drawing.Size(206, 51)
        Me.BtnEditBarangayInfo.TabIndex = 6
        Me.BtnEditBarangayInfo.Text = "Edit Barangay Info"
        Me.BtnEditBarangayInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnEditBarangayInfo.UseVisualStyleBackColor = False
        '
        'BtnAudit
        '
        Me.BtnAudit.BackColor = System.Drawing.Color.Transparent
        Me.BtnAudit.FlatAppearance.BorderSize = 0
        Me.BtnAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAudit.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAudit.ForeColor = System.Drawing.Color.White
        Me.BtnAudit.Image = Global.Brgy_Maly_System.My.Resources.Resources.audit
        Me.BtnAudit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnAudit.Location = New System.Drawing.Point(19, 363)
        Me.BtnAudit.Name = "BtnAudit"
        Me.BtnAudit.Padding = New System.Windows.Forms.Padding(10)
        Me.BtnAudit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnAudit.Size = New System.Drawing.Size(206, 51)
        Me.BtnAudit.TabIndex = 7
        Me.BtnAudit.Text = "Audit"
        Me.BtnAudit.UseVisualStyleBackColor = False
        '
        'ResidentBtn
        '
        Me.ResidentBtn.BackColor = System.Drawing.Color.Transparent
        Me.ResidentBtn.FlatAppearance.BorderSize = 0
        Me.ResidentBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ResidentBtn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResidentBtn.ForeColor = System.Drawing.Color.White
        Me.ResidentBtn.Image = CType(resources.GetObject("ResidentBtn.Image"), System.Drawing.Image)
        Me.ResidentBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ResidentBtn.Location = New System.Drawing.Point(19, 94)
        Me.ResidentBtn.Name = "ResidentBtn"
        Me.ResidentBtn.Padding = New System.Windows.Forms.Padding(10)
        Me.ResidentBtn.Size = New System.Drawing.Size(206, 51)
        Me.ResidentBtn.TabIndex = 2
        Me.ResidentBtn.Text = "Residents"
        Me.ResidentBtn.UseVisualStyleBackColor = False
        '
        'LogoPic
        '
        Me.LogoPic.Image = Global.Brgy_Maly_System.My.Resources.Resources.LogoForMaly
        Me.LogoPic.Location = New System.Drawing.Point(19, 4)
        Me.LogoPic.Name = "LogoPic"
        Me.LogoPic.Size = New System.Drawing.Size(67, 67)
        Me.LogoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoPic.TabIndex = 3
        Me.LogoPic.TabStop = False
        '
        'UserAccountBtn
        '
        Me.UserAccountBtn.BackColor = System.Drawing.Color.Transparent
        Me.UserAccountBtn.Dock = System.Windows.Forms.DockStyle.Right
        Me.UserAccountBtn.FlatAppearance.BorderSize = 0
        Me.UserAccountBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.UserAccountBtn.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserAccountBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.UserIcon1
        Me.UserAccountBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UserAccountBtn.Location = New System.Drawing.Point(1640, 0)
        Me.UserAccountBtn.Margin = New System.Windows.Forms.Padding(0, 10, 20, 10)
        Me.UserAccountBtn.Name = "UserAccountBtn"
        Me.UserAccountBtn.Size = New System.Drawing.Size(280, 76)
        Me.UserAccountBtn.TabIndex = 10
        Me.UserAccountBtn.Text = "Username"
        Me.UserAccountBtn.UseVisualStyleBackColor = False
        '
        'Dashboard_Layout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1920, 1061)
        Me.Controls.Add(Me.CenterPanel)
        Me.Controls.Add(Me.LeftPanel)
        Me.Controls.Add(Me.TopPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Dashboard_Layout"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard_Layout"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuPanel.ResumeLayout(False)
        Me.LeftPanel.ResumeLayout(False)
        Me.LogoTextPanel.ResumeLayout(False)
        Me.LogoTextPanel.PerformLayout()
        Me.TopPanel.ResumeLayout(False)
        CType(Me.LogoPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MenuPanel As Panel
        Friend WithEvents BtnLogOut As Button
        Friend WithEvents BtnAccounts As Button
    Public WithEvents BtnHousehold As Button
    Friend WithEvents BtnReports As Button
    Friend WithEvents BtnDatabaseBackup As Button
    Friend WithEvents BtnAyudaProgram As Button
    Public WithEvents DashboardBtn As Button
    Friend WithEvents BtnEditBarangayInfo As Button
    Friend WithEvents BtnAudit As Button
    Friend WithEvents ResidentBtn As Button
    Public WithEvents LeftPanel As Panel
    Friend WithEvents UserAccountBtn As Button
        Friend WithEvents LogoTextPanel As Panel
        Friend WithEvents LogoPic As PictureBox
        Friend WithEvents BrgyMalySystemLbl As Label
        Friend WithEvents TopPanel As Panel
        Friend WithEvents CenterPanel As Panel
    Public WithEvents btnAyudaAdding As Button
    Public WithEvents btnHouseholdAdding As Button
    Public WithEvents btnNewRelationshipType As Button
    Public WithEvents btnNewCategoryAdding As Button
End Class
