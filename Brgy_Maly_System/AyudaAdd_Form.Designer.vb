<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AyudaAdd_Form
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
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtAddNewAyuda = New System.Windows.Forms.TextBox()
        Me.lblAddNewAyuda = New System.Windows.Forms.Label()
        Me.lblNewAyuda = New System.Windows.Forms.Label()
        Me.ExitBtn = New System.Windows.Forms.PictureBox()
        Me.FillPanel.SuspendLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnSave)
        Me.FillPanel.Controls.Add(Me.txtAddNewAyuda)
        Me.FillPanel.Controls.Add(Me.lblAddNewAyuda)
        Me.FillPanel.Controls.Add(Me.lblNewAyuda)
        Me.FillPanel.Controls.Add(Me.ExitBtn)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(563, 203)
        Me.FillPanel.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(161, 141)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(245, 46)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'txtAddNewAyuda
        '
        Me.txtAddNewAyuda.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtAddNewAyuda.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddNewAyuda.Location = New System.Drawing.Point(42, 94)
        Me.txtAddNewAyuda.Name = "txtAddNewAyuda"
        Me.txtAddNewAyuda.Size = New System.Drawing.Size(479, 29)
        Me.txtAddNewAyuda.TabIndex = 1
        '
        'lblAddNewAyuda
        '
        Me.lblAddNewAyuda.AutoSize = True
        Me.lblAddNewAyuda.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddNewAyuda.Location = New System.Drawing.Point(38, 69)
        Me.lblAddNewAyuda.Name = "lblAddNewAyuda"
        Me.lblAddNewAyuda.Size = New System.Drawing.Size(167, 22)
        Me.lblAddNewAyuda.TabIndex = 0
        Me.lblAddNewAyuda.Text = "ADD NEW AYUDA"
        '
        'lblNewAyuda
        '
        Me.lblNewAyuda.AutoSize = True
        Me.lblNewAyuda.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewAyuda.Location = New System.Drawing.Point(214, 21)
        Me.lblNewAyuda.Name = "lblNewAyuda"
        Me.lblNewAyuda.Size = New System.Drawing.Size(140, 29)
        Me.lblNewAyuda.TabIndex = 0
        Me.lblNewAyuda.Text = "New Ayuda"
        '
        'ExitBtn
        '
        Me.ExitBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExitBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.ExitButton
        Me.ExitBtn.Location = New System.Drawing.Point(531, 1)
        Me.ExitBtn.Name = "ExitBtn"
        Me.ExitBtn.Size = New System.Drawing.Size(31, 30)
        Me.ExitBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ExitBtn.TabIndex = 3
        Me.ExitBtn.TabStop = False
        '
        'AyudaAdd_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(563, 203)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AyudaAdd_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AyudaAdd_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents ExitBtn As PictureBox
    Friend WithEvents lblNewAyuda As Label
    Friend WithEvents txtAddNewAyuda As TextBox
    Friend WithEvents lblAddNewAyuda As Label
    Friend WithEvents btnSave As Button
End Class
