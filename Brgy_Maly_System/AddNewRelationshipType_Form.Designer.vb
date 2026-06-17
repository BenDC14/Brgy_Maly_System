<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddNewRelationshipType_Form
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
        Me.cbrelationship = New System.Windows.Forms.ComboBox()
        Me.btnAddNewRelationshipType = New System.Windows.Forms.Button()
        Me.txtRelationship = New System.Windows.Forms.TextBox()
        Me.lblRelationship = New System.Windows.Forms.Label()
        Me.lblAddRelationshipType = New System.Windows.Forms.Label()
        Me.ExitBtn = New System.Windows.Forms.PictureBox()
        Me.btnEditRelationship = New System.Windows.Forms.Button()
        Me.FillPanel.SuspendLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnEditRelationship)
        Me.FillPanel.Controls.Add(Me.cbrelationship)
        Me.FillPanel.Controls.Add(Me.btnAddNewRelationshipType)
        Me.FillPanel.Controls.Add(Me.txtRelationship)
        Me.FillPanel.Controls.Add(Me.lblRelationship)
        Me.FillPanel.Controls.Add(Me.lblAddRelationshipType)
        Me.FillPanel.Controls.Add(Me.ExitBtn)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(470, 245)
        Me.FillPanel.TabIndex = 0
        '
        'cbrelationship
        '
        Me.cbrelationship.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbrelationship.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbrelationship.FormattingEnabled = True
        Me.cbrelationship.Location = New System.Drawing.Point(24, 104)
        Me.cbrelationship.Name = "cbrelationship"
        Me.cbrelationship.Size = New System.Drawing.Size(248, 30)
        Me.cbrelationship.TabIndex = 1
        '
        'btnAddNewRelationshipType
        '
        Me.btnAddNewRelationshipType.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnAddNewRelationshipType.FlatAppearance.BorderSize = 0
        Me.btnAddNewRelationshipType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNewRelationshipType.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNewRelationshipType.Location = New System.Drawing.Point(24, 168)
        Me.btnAddNewRelationshipType.Name = "btnAddNewRelationshipType"
        Me.btnAddNewRelationshipType.Size = New System.Drawing.Size(202, 47)
        Me.btnAddNewRelationshipType.TabIndex = 3
        Me.btnAddNewRelationshipType.Text = "Add New Relationship"
        Me.btnAddNewRelationshipType.UseVisualStyleBackColor = False
        '
        'txtRelationship
        '
        Me.txtRelationship.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtRelationship.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRelationship.Location = New System.Drawing.Point(278, 104)
        Me.txtRelationship.Name = "txtRelationship"
        Me.txtRelationship.Size = New System.Drawing.Size(150, 29)
        Me.txtRelationship.TabIndex = 2
        '
        'lblRelationship
        '
        Me.lblRelationship.AutoSize = True
        Me.lblRelationship.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRelationship.Location = New System.Drawing.Point(20, 79)
        Me.lblRelationship.Name = "lblRelationship"
        Me.lblRelationship.Size = New System.Drawing.Size(132, 22)
        Me.lblRelationship.TabIndex = 0
        Me.lblRelationship.Text = "Relationship:"
        '
        'lblAddRelationshipType
        '
        Me.lblAddRelationshipType.AutoSize = True
        Me.lblAddRelationshipType.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddRelationshipType.Location = New System.Drawing.Point(44, 23)
        Me.lblAddRelationshipType.Name = "lblAddRelationshipType"
        Me.lblAddRelationshipType.Size = New System.Drawing.Size(376, 32)
        Me.lblAddRelationshipType.TabIndex = 0
        Me.lblAddRelationshipType.Text = "Add New Relationship Type"
        '
        'ExitBtn
        '
        Me.ExitBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExitBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.ExitButton
        Me.ExitBtn.Location = New System.Drawing.Point(439, 0)
        Me.ExitBtn.Name = "ExitBtn"
        Me.ExitBtn.Size = New System.Drawing.Size(31, 30)
        Me.ExitBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ExitBtn.TabIndex = 12
        Me.ExitBtn.TabStop = False
        '
        'btnEditRelationship
        '
        Me.btnEditRelationship.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnEditRelationship.FlatAppearance.BorderSize = 0
        Me.btnEditRelationship.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditRelationship.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditRelationship.Location = New System.Drawing.Point(245, 168)
        Me.btnEditRelationship.Name = "btnEditRelationship"
        Me.btnEditRelationship.Size = New System.Drawing.Size(202, 47)
        Me.btnEditRelationship.TabIndex = 4
        Me.btnEditRelationship.Text = "Edit Relationship"
        Me.btnEditRelationship.UseVisualStyleBackColor = False
        '
        'AddNewRelationshipType_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(5.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(470, 245)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "AddNewRelationshipType_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AddNewRelationshipType"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents cbrelationship As ComboBox
    Friend WithEvents btnAddNewRelationshipType As Button
    Friend WithEvents txtRelationship As TextBox
    Friend WithEvents lblRelationship As Label
    Friend WithEvents lblAddRelationshipType As Label
    Friend WithEvents ExitBtn As PictureBox
    Friend WithEvents btnEditRelationship As Button
End Class
