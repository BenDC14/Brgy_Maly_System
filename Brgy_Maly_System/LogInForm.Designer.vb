<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogInForm
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
        Me.LeftPanel = New System.Windows.Forms.Panel()
        Me.LogInBtn = New System.Windows.Forms.Button()
        Me.ForgetPassLbl = New System.Windows.Forms.Label()
        Me.PassTxtbox = New System.Windows.Forms.TextBox()
        Me.Password = New System.Windows.Forms.Label()
        Me.UsernameLbl = New System.Windows.Forms.Label()
        Me.UnameTxtBox = New System.Windows.Forms.TextBox()
        Me.BrgyMalySystemLbl = New System.Windows.Forms.Label()
        Me.FillPanel = New System.Windows.Forms.Panel()
        Me.ExitBtn = New System.Windows.Forms.PictureBox()
        Me.SeePassBtn = New System.Windows.Forms.PictureBox()
        Me.LogoPic = New System.Windows.Forms.PictureBox()
        Me.FillPanel.SuspendLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SeePassBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LogoPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LeftPanel
        '
        Me.LeftPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.LeftPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftPanel.Name = "LeftPanel"
        Me.LeftPanel.Size = New System.Drawing.Size(148, 566)
        Me.LeftPanel.TabIndex = 0
        '
        'LogInBtn
        '
        Me.LogInBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LogInBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.LogInBtn.FlatAppearance.BorderSize = 0
        Me.LogInBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LogInBtn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogInBtn.Location = New System.Drawing.Point(115, 435)
        Me.LogInBtn.Name = "LogInBtn"
        Me.LogInBtn.Size = New System.Drawing.Size(110, 30)
        Me.LogInBtn.TabIndex = 4
        Me.LogInBtn.Text = "Log-In"
        Me.LogInBtn.UseVisualStyleBackColor = False
        '
        'ForgetPassLbl
        '
        Me.ForgetPassLbl.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ForgetPassLbl.AutoSize = True
        Me.ForgetPassLbl.BackColor = System.Drawing.Color.Transparent
        Me.ForgetPassLbl.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForgetPassLbl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ForgetPassLbl.Location = New System.Drawing.Point(155, 395)
        Me.ForgetPassLbl.Name = "ForgetPassLbl"
        Me.ForgetPassLbl.Size = New System.Drawing.Size(97, 14)
        Me.ForgetPassLbl.TabIndex = 3
        Me.ForgetPassLbl.Text = "Forgot Password?"
        '
        'PassTxtbox
        '
        Me.PassTxtbox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PassTxtbox.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassTxtbox.Location = New System.Drawing.Point(71, 370)
        Me.PassTxtbox.Name = "PassTxtbox"
        Me.PassTxtbox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PassTxtbox.Size = New System.Drawing.Size(181, 22)
        Me.PassTxtbox.TabIndex = 2
        '
        'Password
        '
        Me.Password.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Password.AutoSize = True
        Me.Password.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Password.Location = New System.Drawing.Point(68, 352)
        Me.Password.Name = "Password"
        Me.Password.Size = New System.Drawing.Size(68, 16)
        Me.Password.TabIndex = 0
        Me.Password.Text = "Password"
        '
        'UsernameLbl
        '
        Me.UsernameLbl.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.UsernameLbl.AutoSize = True
        Me.UsernameLbl.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameLbl.Location = New System.Drawing.Point(68, 308)
        Me.UsernameLbl.Name = "UsernameLbl"
        Me.UsernameLbl.Size = New System.Drawing.Size(71, 16)
        Me.UsernameLbl.TabIndex = 0
        Me.UsernameLbl.Text = "Username"
        '
        'UnameTxtBox
        '
        Me.UnameTxtBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.UnameTxtBox.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UnameTxtBox.Location = New System.Drawing.Point(71, 325)
        Me.UnameTxtBox.Name = "UnameTxtBox"
        Me.UnameTxtBox.Size = New System.Drawing.Size(181, 22)
        Me.UnameTxtBox.TabIndex = 1
        '
        'BrgyMalySystemLbl
        '
        Me.BrgyMalySystemLbl.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BrgyMalySystemLbl.AutoSize = True
        Me.BrgyMalySystemLbl.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyMalySystemLbl.Location = New System.Drawing.Point(85, 137)
        Me.BrgyMalySystemLbl.Name = "BrgyMalySystemLbl"
        Me.BrgyMalySystemLbl.Size = New System.Drawing.Size(193, 24)
        Me.BrgyMalySystemLbl.TabIndex = 0
        Me.BrgyMalySystemLbl.Text = "Brgy. Maly System"
        '
        'FillPanel
        '
        Me.FillPanel.Controls.Add(Me.LogInBtn)
        Me.FillPanel.Controls.Add(Me.ExitBtn)
        Me.FillPanel.Controls.Add(Me.ForgetPassLbl)
        Me.FillPanel.Controls.Add(Me.SeePassBtn)
        Me.FillPanel.Controls.Add(Me.PassTxtbox)
        Me.FillPanel.Controls.Add(Me.Password)
        Me.FillPanel.Controls.Add(Me.LogoPic)
        Me.FillPanel.Controls.Add(Me.BrgyMalySystemLbl)
        Me.FillPanel.Controls.Add(Me.UnameTxtBox)
        Me.FillPanel.Controls.Add(Me.UsernameLbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(148, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(322, 566)
        Me.FillPanel.TabIndex = 0
        '
        'ExitBtn
        '
        Me.ExitBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExitBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.ExitButton
        Me.ExitBtn.Location = New System.Drawing.Point(289, 1)
        Me.ExitBtn.Name = "ExitBtn"
        Me.ExitBtn.Size = New System.Drawing.Size(31, 30)
        Me.ExitBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ExitBtn.TabIndex = 1
        Me.ExitBtn.TabStop = False
        '
        'SeePassBtn
        '
        Me.SeePassBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SeePassBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.closeeye
        Me.SeePassBtn.Location = New System.Drawing.Point(258, 370)
        Me.SeePassBtn.Name = "SeePassBtn"
        Me.SeePassBtn.Size = New System.Drawing.Size(23, 24)
        Me.SeePassBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SeePassBtn.TabIndex = 9
        Me.SeePassBtn.TabStop = False
        '
        'LogoPic
        '
        Me.LogoPic.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LogoPic.Image = Global.Brgy_Maly_System.My.Resources.Resources.MALY_LOGO
        Me.LogoPic.Location = New System.Drawing.Point(115, 21)
        Me.LogoPic.Name = "LogoPic"
        Me.LogoPic.Size = New System.Drawing.Size(113, 113)
        Me.LogoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoPic.TabIndex = 2
        Me.LogoPic.TabStop = False
        '
        'LogInForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(470, 566)
        Me.Controls.Add(Me.FillPanel)
        Me.Controls.Add(Me.LeftPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LogInForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LogInForm"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SeePassBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LogoPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LeftPanel As Panel
    Friend WithEvents ExitBtn As PictureBox
    Friend WithEvents LogoPic As PictureBox
    Friend WithEvents BrgyMalySystemLbl As Label
    Friend WithEvents UsernameLbl As Label
    Friend WithEvents UnameTxtBox As TextBox
    Friend WithEvents Password As Label
    Friend WithEvents PassTxtbox As TextBox
    Friend WithEvents ForgetPassLbl As Label
    Friend WithEvents SeePassBtn As PictureBox
    Friend WithEvents LogInBtn As Button
    Friend WithEvents FillPanel As Panel
End Class
