<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddNewCategory_form
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
        Me.cbcategory = New System.Windows.Forms.ComboBox()
        Me.btnAddNewCategory = New System.Windows.Forms.Button()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.lblAddCategory = New System.Windows.Forms.Label()
        Me.ExitBtn = New System.Windows.Forms.PictureBox()
        Me.btnEditCategory = New System.Windows.Forms.Button()
        Me.FillPanel.SuspendLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnEditCategory)
        Me.FillPanel.Controls.Add(Me.cbcategory)
        Me.FillPanel.Controls.Add(Me.btnAddNewCategory)
        Me.FillPanel.Controls.Add(Me.txtCategory)
        Me.FillPanel.Controls.Add(Me.lblCategory)
        Me.FillPanel.Controls.Add(Me.lblAddCategory)
        Me.FillPanel.Controls.Add(Me.ExitBtn)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(470, 245)
        Me.FillPanel.TabIndex = 1
        '
        'cbcategory
        '
        Me.cbcategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbcategory.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbcategory.FormattingEnabled = True
        Me.cbcategory.Location = New System.Drawing.Point(24, 104)
        Me.cbcategory.Name = "cbcategory"
        Me.cbcategory.Size = New System.Drawing.Size(248, 30)
        Me.cbcategory.TabIndex = 1
        '
        'btnAddNewCategory
        '
        Me.btnAddNewCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnAddNewCategory.FlatAppearance.BorderSize = 0
        Me.btnAddNewCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNewCategory.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNewCategory.Location = New System.Drawing.Point(24, 167)
        Me.btnAddNewCategory.Name = "btnAddNewCategory"
        Me.btnAddNewCategory.Size = New System.Drawing.Size(199, 47)
        Me.btnAddNewCategory.TabIndex = 3
        Me.btnAddNewCategory.Text = "Add New Category"
        Me.btnAddNewCategory.UseVisualStyleBackColor = False
        '
        'txtCategory
        '
        Me.txtCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtCategory.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.Location = New System.Drawing.Point(278, 104)
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(150, 29)
        Me.txtCategory.TabIndex = 2
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.Location = New System.Drawing.Point(20, 79)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(102, 22)
        Me.lblCategory.TabIndex = 0
        Me.lblCategory.Text = "Category:"
        '
        'lblAddCategory
        '
        Me.lblAddCategory.AutoSize = True
        Me.lblAddCategory.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCategory.Location = New System.Drawing.Point(92, 20)
        Me.lblAddCategory.Name = "lblAddCategory"
        Me.lblAddCategory.Size = New System.Drawing.Size(258, 32)
        Me.lblAddCategory.TabIndex = 0
        Me.lblAddCategory.Text = "Add New Category"
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
        'btnEditCategory
        '
        Me.btnEditCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnEditCategory.FlatAppearance.BorderSize = 0
        Me.btnEditCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditCategory.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditCategory.Location = New System.Drawing.Point(245, 167)
        Me.btnEditCategory.Name = "btnEditCategory"
        Me.btnEditCategory.Size = New System.Drawing.Size(199, 47)
        Me.btnEditCategory.TabIndex = 4
        Me.btnEditCategory.Text = "Edit Category"
        Me.btnEditCategory.UseVisualStyleBackColor = False
        '
        'AddNewCategory_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(5.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(470, 245)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "AddNewCategory_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AddNewCategory"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents cbcategory As ComboBox
    Friend WithEvents btnAddNewCategory As Button
    Friend WithEvents txtCategory As TextBox
    Friend WithEvents lblCategory As Label
    Friend WithEvents lblAddCategory As Label
    Friend WithEvents ExitBtn As PictureBox
    Friend WithEvents btnEditCategory As Button
End Class
