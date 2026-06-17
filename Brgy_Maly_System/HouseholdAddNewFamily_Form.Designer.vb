<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HouseholdAddNewFamily_Form
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
        Me.AddNewFamilylbl = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.BtnSaveRelationship = New System.Windows.Forms.Button()
        Me.cbRelationshipType = New System.Windows.Forms.ComboBox()
        Me.CivilStatuslbl = New System.Windows.Forms.Label()
        Me.FamilyMembersDGV = New System.Windows.Forms.DataGridView()
        Me.Familieslbl = New System.Windows.Forms.Label()
        Me.LinePnl = New System.Windows.Forms.Panel()
        Me.btnSaveFamily = New System.Windows.Forms.Button()
        Me.txtFamilyName = New System.Windows.Forms.TextBox()
        Me.FamilyNamelbl = New System.Windows.Forms.Label()
        Me.Householdlbl = New System.Windows.Forms.Label()
        Me.txtHousehold = New System.Windows.Forms.TextBox()
        Me.FamilyHeadlbl = New System.Windows.Forms.Label()
        Me.familyheadcb = New System.Windows.Forms.ComboBox()
        Me.FillPanel.SuspendLayout()
        CType(Me.FamilyMembersDGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.familyheadcb)
        Me.FillPanel.Controls.Add(Me.AddNewFamilylbl)
        Me.FillPanel.Controls.Add(Me.btnBack)
        Me.FillPanel.Controls.Add(Me.BtnSaveRelationship)
        Me.FillPanel.Controls.Add(Me.cbRelationshipType)
        Me.FillPanel.Controls.Add(Me.CivilStatuslbl)
        Me.FillPanel.Controls.Add(Me.FamilyMembersDGV)
        Me.FillPanel.Controls.Add(Me.Familieslbl)
        Me.FillPanel.Controls.Add(Me.LinePnl)
        Me.FillPanel.Controls.Add(Me.btnSaveFamily)
        Me.FillPanel.Controls.Add(Me.txtFamilyName)
        Me.FillPanel.Controls.Add(Me.FamilyNamelbl)
        Me.FillPanel.Controls.Add(Me.Householdlbl)
        Me.FillPanel.Controls.Add(Me.txtHousehold)
        Me.FillPanel.Controls.Add(Me.FamilyHeadlbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'AddNewFamilylbl
        '
        Me.AddNewFamilylbl.AutoSize = True
        Me.AddNewFamilylbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddNewFamilylbl.Location = New System.Drawing.Point(30, 30)
        Me.AddNewFamilylbl.Name = "AddNewFamilylbl"
        Me.AddNewFamilylbl.Size = New System.Drawing.Size(228, 32)
        Me.AddNewFamilylbl.TabIndex = 0
        Me.AddNewFamilylbl.Text = "Add New Family"
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(1468, 926)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(199, 44)
        Me.btnBack.TabIndex = 37
        Me.btnBack.Text = "Back To Main"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'BtnSaveRelationship
        '
        Me.BtnSaveRelationship.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnSaveRelationship.FlatAppearance.BorderSize = 0
        Me.BtnSaveRelationship.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveRelationship.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveRelationship.Location = New System.Drawing.Point(1223, 926)
        Me.BtnSaveRelationship.Name = "BtnSaveRelationship"
        Me.BtnSaveRelationship.Size = New System.Drawing.Size(199, 44)
        Me.BtnSaveRelationship.TabIndex = 36
        Me.BtnSaveRelationship.Text = "Save Relationship"
        Me.BtnSaveRelationship.UseVisualStyleBackColor = False
        '
        'cbRelationshipType
        '
        Me.cbRelationshipType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbRelationshipType.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRelationshipType.FormattingEnabled = True
        Me.cbRelationshipType.Items.AddRange(New Object() {"Head", "Mother", "Father", "Deceased"})
        Me.cbRelationshipType.Location = New System.Drawing.Point(188, 849)
        Me.cbRelationshipType.Name = "cbRelationshipType"
        Me.cbRelationshipType.Size = New System.Drawing.Size(509, 26)
        Me.cbRelationshipType.TabIndex = 10
        '
        'CivilStatuslbl
        '
        Me.CivilStatuslbl.AutoSize = True
        Me.CivilStatuslbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CivilStatuslbl.Location = New System.Drawing.Point(30, 852)
        Me.CivilStatuslbl.Name = "CivilStatuslbl"
        Me.CivilStatuslbl.Size = New System.Drawing.Size(152, 19)
        Me.CivilStatuslbl.TabIndex = 9
        Me.CivilStatuslbl.Text = "Relationship Type:"
        '
        'FamilyMembersDGV
        '
        Me.FamilyMembersDGV.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.FamilyMembersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FamilyMembersDGV.Location = New System.Drawing.Point(34, 297)
        Me.FamilyMembersDGV.Name = "FamilyMembersDGV"
        Me.FamilyMembersDGV.Size = New System.Drawing.Size(1633, 518)
        Me.FamilyMembersDGV.TabIndex = 5
        '
        'Familieslbl
        '
        Me.Familieslbl.AutoSize = True
        Me.Familieslbl.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Familieslbl.Location = New System.Drawing.Point(32, 270)
        Me.Familieslbl.Name = "Familieslbl"
        Me.Familieslbl.Size = New System.Drawing.Size(93, 24)
        Me.Familieslbl.TabIndex = 0
        Me.Familieslbl.Text = "Families"
        '
        'LinePnl
        '
        Me.LinePnl.BackColor = System.Drawing.Color.Black
        Me.LinePnl.Location = New System.Drawing.Point(0, 250)
        Me.LinePnl.Name = "LinePnl"
        Me.LinePnl.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl.TabIndex = 0
        '
        'btnSaveFamily
        '
        Me.btnSaveFamily.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSaveFamily.FlatAppearance.BorderSize = 0
        Me.btnSaveFamily.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveFamily.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveFamily.Location = New System.Drawing.Point(203, 172)
        Me.btnSaveFamily.Name = "btnSaveFamily"
        Me.btnSaveFamily.Size = New System.Drawing.Size(1232, 36)
        Me.btnSaveFamily.TabIndex = 4
        Me.btnSaveFamily.Text = "Save Family"
        Me.btnSaveFamily.UseVisualStyleBackColor = False
        '
        'txtFamilyName
        '
        Me.txtFamilyName.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFamilyName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFamilyName.Location = New System.Drawing.Point(623, 129)
        Me.txtFamilyName.Name = "txtFamilyName"
        Me.txtFamilyName.Size = New System.Drawing.Size(384, 26)
        Me.txtFamilyName.TabIndex = 2
        '
        'FamilyNamelbl
        '
        Me.FamilyNamelbl.AutoSize = True
        Me.FamilyNamelbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FamilyNamelbl.Location = New System.Drawing.Point(619, 106)
        Me.FamilyNamelbl.Name = "FamilyNamelbl"
        Me.FamilyNamelbl.Size = New System.Drawing.Size(107, 19)
        Me.FamilyNamelbl.TabIndex = 0
        Me.FamilyNamelbl.Text = "Family Name"
        '
        'Householdlbl
        '
        Me.Householdlbl.AutoSize = True
        Me.Householdlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Householdlbl.Location = New System.Drawing.Point(199, 106)
        Me.Householdlbl.Name = "Householdlbl"
        Me.Householdlbl.Size = New System.Drawing.Size(93, 19)
        Me.Householdlbl.TabIndex = 0
        Me.Householdlbl.Text = "Household"
        '
        'txtHousehold
        '
        Me.txtHousehold.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtHousehold.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHousehold.Location = New System.Drawing.Point(203, 129)
        Me.txtHousehold.Name = "txtHousehold"
        Me.txtHousehold.Size = New System.Drawing.Size(384, 26)
        Me.txtHousehold.TabIndex = 1
        '
        'FamilyHeadlbl
        '
        Me.FamilyHeadlbl.AutoSize = True
        Me.FamilyHeadlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FamilyHeadlbl.Location = New System.Drawing.Point(1047, 106)
        Me.FamilyHeadlbl.Name = "FamilyHeadlbl"
        Me.FamilyHeadlbl.Size = New System.Drawing.Size(103, 19)
        Me.FamilyHeadlbl.TabIndex = 0
        Me.FamilyHeadlbl.Text = "Family Head"
        '
        'familyheadcb
        '
        Me.familyheadcb.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.familyheadcb.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.familyheadcb.FormattingEnabled = True
        Me.familyheadcb.Location = New System.Drawing.Point(1051, 129)
        Me.familyheadcb.Name = "familyheadcb"
        Me.familyheadcb.Size = New System.Drawing.Size(384, 26)
        Me.familyheadcb.TabIndex = 3
        '
        'HouseholdAddNewFamily_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "HouseholdAddNewFamily_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HouseholdAddNewFamily_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.FamilyMembersDGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents txtFamilyName As TextBox
    Friend WithEvents FamilyNamelbl As Label
    Friend WithEvents Householdlbl As Label
    Friend WithEvents txtHousehold As TextBox
    Friend WithEvents FamilyHeadlbl As Label
    Friend WithEvents btnSaveFamily As Button
    Friend WithEvents Familieslbl As Label
    Friend WithEvents LinePnl As Panel
    Friend WithEvents FamilyMembersDGV As DataGridView
    Friend WithEvents cbRelationshipType As ComboBox
    Friend WithEvents CivilStatuslbl As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents BtnSaveRelationship As Button
    Friend WithEvents AddNewFamilylbl As Label
    Friend WithEvents familyheadcb As ComboBox
End Class
