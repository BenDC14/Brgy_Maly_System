<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HouseholdEditFamily_Form
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
        Me.EditFamilylbl = New System.Windows.Forms.Label()
        Me.btnEditPosition = New System.Windows.Forms.Button()
        Me.btnEditMember = New System.Windows.Forms.Button()
        Me.btnAddNewResident = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnSaveChanges = New System.Windows.Forms.Button()
        Me.RelationshipTypelbl = New System.Windows.Forms.Label()
        Me.SelectResidentslbl = New System.Windows.Forms.Label()
        Me.cbRelationships = New System.Windows.Forms.ComboBox()
        Me.cbResidents = New System.Windows.Forms.ComboBox()
        Me.AddNewMemberslbl = New System.Windows.Forms.Label()
        Me.LinePnl2 = New System.Windows.Forms.Panel()
        Me.FamilyMembersDGV = New System.Windows.Forms.DataGridView()
        Me.Memberslbl = New System.Windows.Forms.Label()
        Me.LinePnl = New System.Windows.Forms.Panel()
        Me.txtFamilyHead = New System.Windows.Forms.TextBox()
        Me.FamilyHeadlbl = New System.Windows.Forms.Label()
        Me.txtTotalFamilyMembers = New System.Windows.Forms.TextBox()
        Me.TotalFamilyMemberslbl = New System.Windows.Forms.Label()
        Me.txtHousehold = New System.Windows.Forms.TextBox()
        Me.Householdlbl = New System.Windows.Forms.Label()
        Me.txtFamilyName = New System.Windows.Forms.TextBox()
        Me.FamilyNamelbl = New System.Windows.Forms.Label()
        Me.txtFamilyID = New System.Windows.Forms.TextBox()
        Me.FamilyIDlbl = New System.Windows.Forms.Label()
        Me.btnAddNewFamilyMember = New System.Windows.Forms.Button()
        Me.FillPanel.SuspendLayout()
        CType(Me.FamilyMembersDGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnAddNewFamilyMember)
        Me.FillPanel.Controls.Add(Me.EditFamilylbl)
        Me.FillPanel.Controls.Add(Me.btnEditPosition)
        Me.FillPanel.Controls.Add(Me.btnEditMember)
        Me.FillPanel.Controls.Add(Me.btnAddNewResident)
        Me.FillPanel.Controls.Add(Me.btnBack)
        Me.FillPanel.Controls.Add(Me.btnSaveChanges)
        Me.FillPanel.Controls.Add(Me.RelationshipTypelbl)
        Me.FillPanel.Controls.Add(Me.SelectResidentslbl)
        Me.FillPanel.Controls.Add(Me.cbRelationships)
        Me.FillPanel.Controls.Add(Me.cbResidents)
        Me.FillPanel.Controls.Add(Me.AddNewMemberslbl)
        Me.FillPanel.Controls.Add(Me.LinePnl2)
        Me.FillPanel.Controls.Add(Me.FamilyMembersDGV)
        Me.FillPanel.Controls.Add(Me.Memberslbl)
        Me.FillPanel.Controls.Add(Me.LinePnl)
        Me.FillPanel.Controls.Add(Me.txtFamilyHead)
        Me.FillPanel.Controls.Add(Me.FamilyHeadlbl)
        Me.FillPanel.Controls.Add(Me.txtTotalFamilyMembers)
        Me.FillPanel.Controls.Add(Me.TotalFamilyMemberslbl)
        Me.FillPanel.Controls.Add(Me.txtHousehold)
        Me.FillPanel.Controls.Add(Me.Householdlbl)
        Me.FillPanel.Controls.Add(Me.txtFamilyName)
        Me.FillPanel.Controls.Add(Me.FamilyNamelbl)
        Me.FillPanel.Controls.Add(Me.txtFamilyID)
        Me.FillPanel.Controls.Add(Me.FamilyIDlbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'EditFamilylbl
        '
        Me.EditFamilylbl.AutoSize = True
        Me.EditFamilylbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditFamilylbl.Location = New System.Drawing.Point(30, 30)
        Me.EditFamilylbl.Name = "EditFamilylbl"
        Me.EditFamilylbl.Size = New System.Drawing.Size(163, 32)
        Me.EditFamilylbl.TabIndex = 0
        Me.EditFamilylbl.Text = "Edit Family"
        '
        'btnEditPosition
        '
        Me.btnEditPosition.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnEditPosition.FlatAppearance.BorderSize = 0
        Me.btnEditPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditPosition.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditPosition.Location = New System.Drawing.Point(1522, 389)
        Me.btnEditPosition.Name = "btnEditPosition"
        Me.btnEditPosition.Size = New System.Drawing.Size(136, 44)
        Me.btnEditPosition.TabIndex = 13
        Me.btnEditPosition.Text = "Edit Position"
        Me.btnEditPosition.UseVisualStyleBackColor = False
        '
        'btnEditMember
        '
        Me.btnEditMember.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnEditMember.FlatAppearance.BorderSize = 0
        Me.btnEditMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditMember.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditMember.Location = New System.Drawing.Point(1367, 389)
        Me.btnEditMember.Name = "btnEditMember"
        Me.btnEditMember.Size = New System.Drawing.Size(136, 44)
        Me.btnEditMember.TabIndex = 12
        Me.btnEditMember.Text = "Edit Member"
        Me.btnEditMember.UseVisualStyleBackColor = False
        '
        'btnAddNewResident
        '
        Me.btnAddNewResident.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnAddNewResident.FlatAppearance.BorderSize = 0
        Me.btnAddNewResident.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNewResident.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNewResident.Location = New System.Drawing.Point(980, 921)
        Me.btnAddNewResident.Name = "btnAddNewResident"
        Me.btnAddNewResident.Size = New System.Drawing.Size(199, 44)
        Me.btnAddNewResident.TabIndex = 11
        Me.btnAddNewResident.Text = "Add New Resident"
        Me.btnAddNewResident.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(1362, 921)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(199, 44)
        Me.btnBack.TabIndex = 12
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnSaveChanges
        '
        Me.btnSaveChanges.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSaveChanges.FlatAppearance.BorderSize = 0
        Me.btnSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveChanges.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveChanges.Location = New System.Drawing.Point(582, 921)
        Me.btnSaveChanges.Name = "btnSaveChanges"
        Me.btnSaveChanges.Size = New System.Drawing.Size(199, 44)
        Me.btnSaveChanges.TabIndex = 10
        Me.btnSaveChanges.Text = "Save Changes"
        Me.btnSaveChanges.UseVisualStyleBackColor = False
        '
        'RelationshipTypelbl
        '
        Me.RelationshipTypelbl.AutoSize = True
        Me.RelationshipTypelbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RelationshipTypelbl.Location = New System.Drawing.Point(1147, 787)
        Me.RelationshipTypelbl.Name = "RelationshipTypelbl"
        Me.RelationshipTypelbl.Size = New System.Drawing.Size(146, 19)
        Me.RelationshipTypelbl.TabIndex = 0
        Me.RelationshipTypelbl.Text = "Relationship Type"
        '
        'SelectResidentslbl
        '
        Me.SelectResidentslbl.AutoSize = True
        Me.SelectResidentslbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SelectResidentslbl.Location = New System.Drawing.Point(156, 787)
        Me.SelectResidentslbl.Name = "SelectResidentslbl"
        Me.SelectResidentslbl.Size = New System.Drawing.Size(137, 19)
        Me.SelectResidentslbl.TabIndex = 0
        Me.SelectResidentslbl.Text = "Select Residents"
        '
        'cbRelationships
        '
        Me.cbRelationships.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbRelationships.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRelationships.FormattingEnabled = True
        Me.cbRelationships.Items.AddRange(New Object() {"Head", "Mother", "Son", "Daugther"})
        Me.cbRelationships.Location = New System.Drawing.Point(1151, 809)
        Me.cbRelationships.Name = "cbRelationships"
        Me.cbRelationships.Size = New System.Drawing.Size(410, 30)
        Me.cbRelationships.TabIndex = 8
        '
        'cbResidents
        '
        Me.cbResidents.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbResidents.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbResidents.FormattingEnabled = True
        Me.cbResidents.Location = New System.Drawing.Point(160, 809)
        Me.cbResidents.Name = "cbResidents"
        Me.cbResidents.Size = New System.Drawing.Size(410, 30)
        Me.cbResidents.TabIndex = 7
        '
        'AddNewMemberslbl
        '
        Me.AddNewMemberslbl.AutoSize = True
        Me.AddNewMemberslbl.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddNewMemberslbl.Location = New System.Drawing.Point(32, 743)
        Me.AddNewMemberslbl.Name = "AddNewMemberslbl"
        Me.AddNewMemberslbl.Size = New System.Drawing.Size(183, 22)
        Me.AddNewMemberslbl.TabIndex = 0
        Me.AddNewMemberslbl.Text = "Add New Members"
        '
        'LinePnl2
        '
        Me.LinePnl2.BackColor = System.Drawing.Color.Black
        Me.LinePnl2.Location = New System.Drawing.Point(0, 725)
        Me.LinePnl2.Name = "LinePnl2"
        Me.LinePnl2.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl2.TabIndex = 0
        '
        'FamilyMembersDGV
        '
        Me.FamilyMembersDGV.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.FamilyMembersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FamilyMembersDGV.Location = New System.Drawing.Point(36, 374)
        Me.FamilyMembersDGV.Name = "FamilyMembersDGV"
        Me.FamilyMembersDGV.Size = New System.Drawing.Size(1633, 326)
        Me.FamilyMembersDGV.TabIndex = 6
        '
        'Memberslbl
        '
        Me.Memberslbl.AutoSize = True
        Me.Memberslbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Memberslbl.Location = New System.Drawing.Point(30, 329)
        Me.Memberslbl.Name = "Memberslbl"
        Me.Memberslbl.Size = New System.Drawing.Size(134, 32)
        Me.Memberslbl.TabIndex = 0
        Me.Memberslbl.Text = "Members"
        '
        'LinePnl
        '
        Me.LinePnl.BackColor = System.Drawing.Color.Black
        Me.LinePnl.Location = New System.Drawing.Point(0, 302)
        Me.LinePnl.Name = "LinePnl"
        Me.LinePnl.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl.TabIndex = 0
        '
        'txtFamilyHead
        '
        Me.txtFamilyHead.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFamilyHead.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFamilyHead.Location = New System.Drawing.Point(651, 213)
        Me.txtFamilyHead.Name = "txtFamilyHead"
        Me.txtFamilyHead.Size = New System.Drawing.Size(384, 26)
        Me.txtFamilyHead.TabIndex = 4
        '
        'FamilyHeadlbl
        '
        Me.FamilyHeadlbl.AutoSize = True
        Me.FamilyHeadlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FamilyHeadlbl.Location = New System.Drawing.Point(647, 190)
        Me.FamilyHeadlbl.Name = "FamilyHeadlbl"
        Me.FamilyHeadlbl.Size = New System.Drawing.Size(103, 19)
        Me.FamilyHeadlbl.TabIndex = 0
        Me.FamilyHeadlbl.Text = "Family Head"
        '
        'txtTotalFamilyMembers
        '
        Me.txtTotalFamilyMembers.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtTotalFamilyMembers.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalFamilyMembers.Location = New System.Drawing.Point(1151, 213)
        Me.txtTotalFamilyMembers.Name = "txtTotalFamilyMembers"
        Me.txtTotalFamilyMembers.Size = New System.Drawing.Size(384, 26)
        Me.txtTotalFamilyMembers.TabIndex = 5
        '
        'TotalFamilyMemberslbl
        '
        Me.TotalFamilyMemberslbl.AutoSize = True
        Me.TotalFamilyMemberslbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalFamilyMemberslbl.Location = New System.Drawing.Point(1147, 190)
        Me.TotalFamilyMemberslbl.Name = "TotalFamilyMemberslbl"
        Me.TotalFamilyMemberslbl.Size = New System.Drawing.Size(174, 19)
        Me.TotalFamilyMemberslbl.TabIndex = 7
        Me.TotalFamilyMemberslbl.Text = "Total Family Members"
        '
        'txtHousehold
        '
        Me.txtHousehold.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtHousehold.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHousehold.Location = New System.Drawing.Point(937, 141)
        Me.txtHousehold.Name = "txtHousehold"
        Me.txtHousehold.Size = New System.Drawing.Size(384, 26)
        Me.txtHousehold.TabIndex = 2
        '
        'Householdlbl
        '
        Me.Householdlbl.AutoSize = True
        Me.Householdlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Householdlbl.Location = New System.Drawing.Point(933, 118)
        Me.Householdlbl.Name = "Householdlbl"
        Me.Householdlbl.Size = New System.Drawing.Size(93, 19)
        Me.Householdlbl.TabIndex = 0
        Me.Householdlbl.Text = "Household"
        '
        'txtFamilyName
        '
        Me.txtFamilyName.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFamilyName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFamilyName.Location = New System.Drawing.Point(160, 213)
        Me.txtFamilyName.Name = "txtFamilyName"
        Me.txtFamilyName.Size = New System.Drawing.Size(384, 26)
        Me.txtFamilyName.TabIndex = 3
        '
        'FamilyNamelbl
        '
        Me.FamilyNamelbl.AutoSize = True
        Me.FamilyNamelbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FamilyNamelbl.Location = New System.Drawing.Point(156, 190)
        Me.FamilyNamelbl.Name = "FamilyNamelbl"
        Me.FamilyNamelbl.Size = New System.Drawing.Size(107, 19)
        Me.FamilyNamelbl.TabIndex = 0
        Me.FamilyNamelbl.Text = "Family Name"
        '
        'txtFamilyID
        '
        Me.txtFamilyID.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFamilyID.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFamilyID.Location = New System.Drawing.Point(409, 141)
        Me.txtFamilyID.Name = "txtFamilyID"
        Me.txtFamilyID.Size = New System.Drawing.Size(384, 26)
        Me.txtFamilyID.TabIndex = 1
        '
        'FamilyIDlbl
        '
        Me.FamilyIDlbl.AutoSize = True
        Me.FamilyIDlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FamilyIDlbl.Location = New System.Drawing.Point(405, 118)
        Me.FamilyIDlbl.Name = "FamilyIDlbl"
        Me.FamilyIDlbl.Size = New System.Drawing.Size(79, 19)
        Me.FamilyIDlbl.TabIndex = 0
        Me.FamilyIDlbl.Text = "Family ID"
        '
        'btnAddNewFamilyMember
        '
        Me.btnAddNewFamilyMember.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnAddNewFamilyMember.FlatAppearance.BorderSize = 0
        Me.btnAddNewFamilyMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNewFamilyMember.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNewFamilyMember.Location = New System.Drawing.Point(160, 921)
        Me.btnAddNewFamilyMember.Name = "btnAddNewFamilyMember"
        Me.btnAddNewFamilyMember.Size = New System.Drawing.Size(199, 44)
        Me.btnAddNewFamilyMember.TabIndex = 9
        Me.btnAddNewFamilyMember.Text = "Add New Member"
        Me.btnAddNewFamilyMember.UseVisualStyleBackColor = False
        '
        'HouseholdEditFamily_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "HouseholdEditFamily_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HouseholdEditFamily_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.FamilyMembersDGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents txtFamilyHead As TextBox
    Friend WithEvents FamilyHeadlbl As Label
    Friend WithEvents txtTotalFamilyMembers As TextBox
    Friend WithEvents TotalFamilyMemberslbl As Label
    Friend WithEvents txtHousehold As TextBox
    Friend WithEvents Householdlbl As Label
    Friend WithEvents txtFamilyName As TextBox
    Friend WithEvents FamilyNamelbl As Label
    Friend WithEvents txtFamilyID As TextBox
    Friend WithEvents FamilyIDlbl As Label
    Friend WithEvents Memberslbl As Label
    Friend WithEvents LinePnl As Panel
    Friend WithEvents AddNewMemberslbl As Label
    Friend WithEvents LinePnl2 As Panel
    Friend WithEvents FamilyMembersDGV As DataGridView
    Friend WithEvents cbRelationships As ComboBox
    Friend WithEvents cbResidents As ComboBox
    Friend WithEvents SelectResidentslbl As Label
    Friend WithEvents RelationshipTypelbl As Label
    Friend WithEvents btnAddNewResident As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents btnSaveChanges As Button
    Friend WithEvents btnEditPosition As Button
    Friend WithEvents btnEditMember As Button
    Friend WithEvents EditFamilylbl As Label
    Friend WithEvents btnAddNewFamilyMember As Button
End Class
