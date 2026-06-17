<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewReportType_Form
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
        Me.cbreportsubtype = New System.Windows.Forms.ComboBox()
        Me.cbreporttype = New System.Windows.Forms.ComboBox()
        Me.btnAddNewReportSubType = New System.Windows.Forms.Button()
        Me.txtReportSubType = New System.Windows.Forms.TextBox()
        Me.lblReportSubType = New System.Windows.Forms.Label()
        Me.btnAddNewReportType = New System.Windows.Forms.Button()
        Me.txtReportType = New System.Windows.Forms.TextBox()
        Me.lblReportType = New System.Windows.Forms.Label()
        Me.lblAddReportTypes = New System.Windows.Forms.Label()
        Me.ExitBtn = New System.Windows.Forms.PictureBox()
        Me.btnEditReportType = New System.Windows.Forms.Button()
        Me.btnEditReportSubType = New System.Windows.Forms.Button()
        Me.FillPanel.SuspendLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnEditReportSubType)
        Me.FillPanel.Controls.Add(Me.btnEditReportType)
        Me.FillPanel.Controls.Add(Me.cbreportsubtype)
        Me.FillPanel.Controls.Add(Me.cbreporttype)
        Me.FillPanel.Controls.Add(Me.btnAddNewReportSubType)
        Me.FillPanel.Controls.Add(Me.txtReportSubType)
        Me.FillPanel.Controls.Add(Me.lblReportSubType)
        Me.FillPanel.Controls.Add(Me.btnAddNewReportType)
        Me.FillPanel.Controls.Add(Me.txtReportType)
        Me.FillPanel.Controls.Add(Me.lblReportType)
        Me.FillPanel.Controls.Add(Me.lblAddReportTypes)
        Me.FillPanel.Controls.Add(Me.ExitBtn)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(470, 485)
        Me.FillPanel.TabIndex = 0
        '
        'cbreportsubtype
        '
        Me.cbreportsubtype.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbreportsubtype.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbreportsubtype.FormattingEnabled = True
        Me.cbreportsubtype.Location = New System.Drawing.Point(30, 312)
        Me.cbreportsubtype.Name = "cbreportsubtype"
        Me.cbreportsubtype.Size = New System.Drawing.Size(248, 30)
        Me.cbreportsubtype.TabIndex = 5
        '
        'cbreporttype
        '
        Me.cbreporttype.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbreporttype.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbreporttype.FormattingEnabled = True
        Me.cbreporttype.Location = New System.Drawing.Point(30, 162)
        Me.cbreporttype.Name = "cbreporttype"
        Me.cbreporttype.Size = New System.Drawing.Size(248, 30)
        Me.cbreporttype.TabIndex = 1
        '
        'btnAddNewReportSubType
        '
        Me.btnAddNewReportSubType.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnAddNewReportSubType.FlatAppearance.BorderSize = 0
        Me.btnAddNewReportSubType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNewReportSubType.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNewReportSubType.Location = New System.Drawing.Point(30, 357)
        Me.btnAddNewReportSubType.Name = "btnAddNewReportSubType"
        Me.btnAddNewReportSubType.Size = New System.Drawing.Size(229, 47)
        Me.btnAddNewReportSubType.TabIndex = 7
        Me.btnAddNewReportSubType.Text = "Add New Report Sub Type"
        Me.btnAddNewReportSubType.UseVisualStyleBackColor = False
        '
        'txtReportSubType
        '
        Me.txtReportSubType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtReportSubType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportSubType.Location = New System.Drawing.Point(284, 312)
        Me.txtReportSubType.Name = "txtReportSubType"
        Me.txtReportSubType.Size = New System.Drawing.Size(150, 29)
        Me.txtReportSubType.TabIndex = 6
        '
        'lblReportSubType
        '
        Me.lblReportSubType.AutoSize = True
        Me.lblReportSubType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportSubType.Location = New System.Drawing.Point(26, 287)
        Me.lblReportSubType.Name = "lblReportSubType"
        Me.lblReportSubType.Size = New System.Drawing.Size(172, 22)
        Me.lblReportSubType.TabIndex = 0
        Me.lblReportSubType.Text = "Report Sub Type:"
        '
        'btnAddNewReportType
        '
        Me.btnAddNewReportType.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnAddNewReportType.FlatAppearance.BorderSize = 0
        Me.btnAddNewReportType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNewReportType.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNewReportType.Location = New System.Drawing.Point(30, 212)
        Me.btnAddNewReportType.Name = "btnAddNewReportType"
        Me.btnAddNewReportType.Size = New System.Drawing.Size(215, 47)
        Me.btnAddNewReportType.TabIndex = 3
        Me.btnAddNewReportType.Text = "Add New Report Type"
        Me.btnAddNewReportType.UseVisualStyleBackColor = False
        '
        'txtReportType
        '
        Me.txtReportType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtReportType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportType.Location = New System.Drawing.Point(284, 162)
        Me.txtReportType.Name = "txtReportType"
        Me.txtReportType.Size = New System.Drawing.Size(150, 29)
        Me.txtReportType.TabIndex = 2
        '
        'lblReportType
        '
        Me.lblReportType.AutoSize = True
        Me.lblReportType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportType.Location = New System.Drawing.Point(26, 137)
        Me.lblReportType.Name = "lblReportType"
        Me.lblReportType.Size = New System.Drawing.Size(130, 22)
        Me.lblReportType.TabIndex = 0
        Me.lblReportType.Text = "Report Type:"
        '
        'lblAddReportTypes
        '
        Me.lblAddReportTypes.AutoSize = True
        Me.lblAddReportTypes.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddReportTypes.Location = New System.Drawing.Point(103, 29)
        Me.lblAddReportTypes.Name = "lblAddReportTypes"
        Me.lblAddReportTypes.Size = New System.Drawing.Size(250, 32)
        Me.lblAddReportTypes.TabIndex = 0
        Me.lblAddReportTypes.Text = "Add Report Types"
        '
        'ExitBtn
        '
        Me.ExitBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExitBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.ExitButton
        Me.ExitBtn.Location = New System.Drawing.Point(436, 1)
        Me.ExitBtn.Name = "ExitBtn"
        Me.ExitBtn.Size = New System.Drawing.Size(31, 30)
        Me.ExitBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ExitBtn.TabIndex = 2
        Me.ExitBtn.TabStop = False
        '
        'btnEditReportType
        '
        Me.btnEditReportType.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnEditReportType.FlatAppearance.BorderSize = 0
        Me.btnEditReportType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditReportType.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditReportType.Location = New System.Drawing.Point(251, 212)
        Me.btnEditReportType.Name = "btnEditReportType"
        Me.btnEditReportType.Size = New System.Drawing.Size(207, 47)
        Me.btnEditReportType.TabIndex = 4
        Me.btnEditReportType.Text = "Edit Report Type"
        Me.btnEditReportType.UseVisualStyleBackColor = False
        '
        'btnEditReportSubType
        '
        Me.btnEditReportSubType.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnEditReportSubType.FlatAppearance.BorderSize = 0
        Me.btnEditReportSubType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditReportSubType.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditReportSubType.Location = New System.Drawing.Point(265, 357)
        Me.btnEditReportSubType.Name = "btnEditReportSubType"
        Me.btnEditReportSubType.Size = New System.Drawing.Size(193, 47)
        Me.btnEditReportSubType.TabIndex = 8
        Me.btnEditReportSubType.Text = "Edit Report Sub Type"
        Me.btnEditReportSubType.UseVisualStyleBackColor = False
        '
        'NewReportType_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(470, 485)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "NewReportType_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NewReportType_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents ExitBtn As PictureBox
    Friend WithEvents lblAddReportTypes As Label
    Friend WithEvents lblReportType As Label
    Friend WithEvents txtReportType As TextBox
    Friend WithEvents btnAddNewReportSubType As Button
    Friend WithEvents lblReportSubType As Label
    Friend WithEvents btnAddNewReportType As Button
    Friend WithEvents cbreportsubtype As ComboBox
    Friend WithEvents cbreporttype As ComboBox
    Friend WithEvents txtReportSubType As TextBox
    Friend WithEvents btnEditReportSubType As Button
    Friend WithEvents btnEditReportType As Button
End Class
