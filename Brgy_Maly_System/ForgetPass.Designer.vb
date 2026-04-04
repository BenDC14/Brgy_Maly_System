<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ForgetPass
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
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.LogInBtn = New System.Windows.Forms.Button()
        Me.ForgetLbl = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.FillPanel = New System.Windows.Forms.Panel()
        Me.btnSearch = New System.Windows.Forms.PictureBox()
        Me.SaveBtn = New System.Windows.Forms.Button()
        Me.SeePassBtn2 = New System.Windows.Forms.PictureBox()
        Me.ConfirmPassTxt = New System.Windows.Forms.TextBox()
        Me.ConfirmPass = New System.Windows.Forms.Label()
        Me.SeePassBtn = New System.Windows.Forms.PictureBox()
        Me.PassTxtbox = New System.Windows.Forms.TextBox()
        Me.NewPassword = New System.Windows.Forms.Label()
        Me.UnameTxtBox = New System.Windows.Forms.TextBox()
        Me.UsernameLbl = New System.Windows.Forms.Label()
        Me.TopPanel.SuspendLayout()
        Me.FillPanel.SuspendLayout()
        CType(Me.btnSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SeePassBtn2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SeePassBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.TopPanel.Controls.Add(Me.BtnClose)
        Me.TopPanel.Controls.Add(Me.LogInBtn)
        Me.TopPanel.Controls.Add(Me.ForgetLbl)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(563, 45)
        Me.TopPanel.TabIndex = 0
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BtnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnClose.FlatAppearance.BorderSize = 0
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(487, 10)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnClose.Size = New System.Drawing.Size(70, 25)
        Me.BtnClose.TabIndex = 2
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'LogInBtn
        '
        Me.LogInBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LogInBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.LogInBtn.FlatAppearance.BorderSize = 0
        Me.LogInBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LogInBtn.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogInBtn.Location = New System.Drawing.Point(399, 10)
        Me.LogInBtn.Name = "LogInBtn"
        Me.LogInBtn.Size = New System.Drawing.Size(70, 25)
        Me.LogInBtn.TabIndex = 1
        Me.LogInBtn.Text = "Log-In"
        Me.LogInBtn.UseVisualStyleBackColor = False
        '
        'ForgetLbl
        '
        Me.ForgetLbl.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ForgetLbl.AutoSize = True
        Me.ForgetLbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForgetLbl.Location = New System.Drawing.Point(36, 12)
        Me.ForgetLbl.Name = "ForgetLbl"
        Me.ForgetLbl.Size = New System.Drawing.Size(174, 19)
        Me.ForgetLbl.TabIndex = 0
        Me.ForgetLbl.Text = "FORGET PASSWORD"
        '
        'FillPanel
        '
        Me.FillPanel.Controls.Add(Me.btnSearch)
        Me.FillPanel.Controls.Add(Me.SaveBtn)
        Me.FillPanel.Controls.Add(Me.SeePassBtn2)
        Me.FillPanel.Controls.Add(Me.ConfirmPassTxt)
        Me.FillPanel.Controls.Add(Me.ConfirmPass)
        Me.FillPanel.Controls.Add(Me.SeePassBtn)
        Me.FillPanel.Controls.Add(Me.PassTxtbox)
        Me.FillPanel.Controls.Add(Me.NewPassword)
        Me.FillPanel.Controls.Add(Me.UnameTxtBox)
        Me.FillPanel.Controls.Add(Me.UsernameLbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 45)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(563, 305)
        Me.FillPanel.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnSearch.Image = Global.Brgy_Maly_System.My.Resources.Resources.search
        Me.btnSearch.Location = New System.Drawing.Point(497, 62)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(23, 24)
        Me.btnSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnSearch.TabIndex = 34
        Me.btnSearch.TabStop = False
        '
        'SaveBtn
        '
        Me.SaveBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SaveBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.SaveBtn.FlatAppearance.BorderSize = 0
        Me.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveBtn.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveBtn.Location = New System.Drawing.Point(243, 229)
        Me.SaveBtn.Name = "SaveBtn"
        Me.SaveBtn.Size = New System.Drawing.Size(80, 30)
        Me.SaveBtn.TabIndex = 6
        Me.SaveBtn.Text = "Save"
        Me.SaveBtn.UseVisualStyleBackColor = False
        '
        'SeePassBtn2
        '
        Me.SeePassBtn2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SeePassBtn2.Image = Global.Brgy_Maly_System.My.Resources.Resources.closeeye
        Me.SeePassBtn2.Location = New System.Drawing.Point(497, 183)
        Me.SeePassBtn2.Name = "SeePassBtn2"
        Me.SeePassBtn2.Size = New System.Drawing.Size(23, 24)
        Me.SeePassBtn2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SeePassBtn2.TabIndex = 33
        Me.SeePassBtn2.TabStop = False
        '
        'ConfirmPassTxt
        '
        Me.ConfirmPassTxt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ConfirmPassTxt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfirmPassTxt.Location = New System.Drawing.Point(45, 183)
        Me.ConfirmPassTxt.Name = "ConfirmPassTxt"
        Me.ConfirmPassTxt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ConfirmPassTxt.Size = New System.Drawing.Size(475, 22)
        Me.ConfirmPassTxt.TabIndex = 5
        '
        'ConfirmPass
        '
        Me.ConfirmPass.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ConfirmPass.AutoSize = True
        Me.ConfirmPass.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfirmPass.Location = New System.Drawing.Point(42, 165)
        Me.ConfirmPass.Name = "ConfirmPass"
        Me.ConfirmPass.Size = New System.Drawing.Size(122, 16)
        Me.ConfirmPass.TabIndex = 0
        Me.ConfirmPass.Text = "Confirm Password"
        '
        'SeePassBtn
        '
        Me.SeePassBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SeePassBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.closeeye1
        Me.SeePassBtn.Location = New System.Drawing.Point(497, 123)
        Me.SeePassBtn.Name = "SeePassBtn"
        Me.SeePassBtn.Size = New System.Drawing.Size(23, 24)
        Me.SeePassBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SeePassBtn.TabIndex = 30
        Me.SeePassBtn.TabStop = False
        '
        'PassTxtbox
        '
        Me.PassTxtbox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PassTxtbox.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassTxtbox.Location = New System.Drawing.Point(45, 123)
        Me.PassTxtbox.Name = "PassTxtbox"
        Me.PassTxtbox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PassTxtbox.Size = New System.Drawing.Size(475, 22)
        Me.PassTxtbox.TabIndex = 4
        '
        'NewPassword
        '
        Me.NewPassword.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.NewPassword.AutoSize = True
        Me.NewPassword.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewPassword.Location = New System.Drawing.Point(42, 105)
        Me.NewPassword.Name = "NewPassword"
        Me.NewPassword.Size = New System.Drawing.Size(100, 16)
        Me.NewPassword.TabIndex = 0
        Me.NewPassword.Text = "New Password"
        '
        'UnameTxtBox
        '
        Me.UnameTxtBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.UnameTxtBox.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UnameTxtBox.Location = New System.Drawing.Point(45, 62)
        Me.UnameTxtBox.Name = "UnameTxtBox"
        Me.UnameTxtBox.Size = New System.Drawing.Size(475, 22)
        Me.UnameTxtBox.TabIndex = 3
        '
        'UsernameLbl
        '
        Me.UsernameLbl.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.UsernameLbl.AutoSize = True
        Me.UsernameLbl.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameLbl.Location = New System.Drawing.Point(42, 45)
        Me.UsernameLbl.Name = "UsernameLbl"
        Me.UsernameLbl.Size = New System.Drawing.Size(71, 16)
        Me.UsernameLbl.TabIndex = 0
        Me.UsernameLbl.Text = "Username"
        '
        'ForgetPass
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(563, 350)
        Me.Controls.Add(Me.FillPanel)
        Me.Controls.Add(Me.TopPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ForgetPass"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ForgetPass"
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.btnSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SeePassBtn2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SeePassBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TopPanel As Panel
    Friend WithEvents ForgetLbl As Label
    Friend WithEvents LogInBtn As Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BtnClose As Button
    Friend WithEvents FillPanel As Panel
    Friend WithEvents SaveBtn As Button
    Friend WithEvents SeePassBtn2 As PictureBox
    Friend WithEvents ConfirmPassTxt As TextBox
    Friend WithEvents ConfirmPass As Label
    Friend WithEvents SeePassBtn As PictureBox
    Friend WithEvents PassTxtbox As TextBox
    Friend WithEvents NewPassword As Label
    Friend WithEvents UnameTxtBox As TextBox
    Friend WithEvents UsernameLbl As Label
    Friend WithEvents btnSearch As PictureBox
End Class
