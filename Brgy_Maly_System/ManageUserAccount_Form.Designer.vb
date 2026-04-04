<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManageUserAccount_Form
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
        Me.SeePassBtn2 = New System.Windows.Forms.PictureBox()
        Me.SeePassBtn = New System.Windows.Forms.PictureBox()
        Me.btnSaveInfo = New System.Windows.Forms.Button()
        Me.txtConfirmPass = New System.Windows.Forms.TextBox()
        Me.ConfirmPassLbl = New System.Windows.Forms.Label()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.PassLbl = New System.Windows.Forms.Label()
        Me.txtUname = New System.Windows.Forms.TextBox()
        Me.UnameLbL = New System.Windows.Forms.Label()
        Me.txtLname = New System.Windows.Forms.TextBox()
        Me.LnameLbl = New System.Windows.Forms.Label()
        Me.txtFname = New System.Windows.Forms.TextBox()
        Me.FNameLbl = New System.Windows.Forms.Label()
        Me.ManageAccountLbl = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        CType(Me.SeePassBtn2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SeePassBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.SeePassBtn2)
        Me.FillPanel.Controls.Add(Me.SeePassBtn)
        Me.FillPanel.Controls.Add(Me.btnSaveInfo)
        Me.FillPanel.Controls.Add(Me.txtConfirmPass)
        Me.FillPanel.Controls.Add(Me.ConfirmPassLbl)
        Me.FillPanel.Controls.Add(Me.txtPass)
        Me.FillPanel.Controls.Add(Me.PassLbl)
        Me.FillPanel.Controls.Add(Me.txtUname)
        Me.FillPanel.Controls.Add(Me.UnameLbL)
        Me.FillPanel.Controls.Add(Me.txtLname)
        Me.FillPanel.Controls.Add(Me.LnameLbl)
        Me.FillPanel.Controls.Add(Me.txtFname)
        Me.FillPanel.Controls.Add(Me.FNameLbl)
        Me.FillPanel.Controls.Add(Me.ManageAccountLbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'SeePassBtn2
        '
        Me.SeePassBtn2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SeePassBtn2.Image = Global.Brgy_Maly_System.My.Resources.Resources.closeeye
        Me.SeePassBtn2.Location = New System.Drawing.Point(1545, 662)
        Me.SeePassBtn2.Name = "SeePassBtn2"
        Me.SeePassBtn2.Size = New System.Drawing.Size(39, 35)
        Me.SeePassBtn2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SeePassBtn2.TabIndex = 34
        Me.SeePassBtn2.TabStop = False
        '
        'SeePassBtn
        '
        Me.SeePassBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SeePassBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.closeeye1
        Me.SeePassBtn.Location = New System.Drawing.Point(1545, 544)
        Me.SeePassBtn.Name = "SeePassBtn"
        Me.SeePassBtn.Size = New System.Drawing.Size(39, 35)
        Me.SeePassBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SeePassBtn.TabIndex = 31
        Me.SeePassBtn.TabStop = False
        '
        'btnSaveInfo
        '
        Me.btnSaveInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSaveInfo.FlatAppearance.BorderSize = 0
        Me.btnSaveInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveInfo.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveInfo.Location = New System.Drawing.Point(741, 864)
        Me.btnSaveInfo.Name = "btnSaveInfo"
        Me.btnSaveInfo.Size = New System.Drawing.Size(260, 76)
        Me.btnSaveInfo.TabIndex = 7
        Me.btnSaveInfo.Text = "Save Information"
        Me.btnSaveInfo.UseVisualStyleBackColor = False
        '
        'txtConfirmPass
        '
        Me.txtConfirmPass.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtConfirmPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConfirmPass.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPass.ForeColor = System.Drawing.Color.Black
        Me.txtConfirmPass.Location = New System.Drawing.Point(78, 662)
        Me.txtConfirmPass.Name = "txtConfirmPass"
        Me.txtConfirmPass.Size = New System.Drawing.Size(1506, 35)
        Me.txtConfirmPass.TabIndex = 6
        '
        'ConfirmPassLbl
        '
        Me.ConfirmPassLbl.AutoSize = True
        Me.ConfirmPassLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfirmPassLbl.Location = New System.Drawing.Point(73, 634)
        Me.ConfirmPassLbl.Name = "ConfirmPassLbl"
        Me.ConfirmPassLbl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ConfirmPassLbl.Size = New System.Drawing.Size(167, 25)
        Me.ConfirmPassLbl.TabIndex = 0
        Me.ConfirmPassLbl.Text = "Confirm Password"
        '
        'txtPass
        '
        Me.txtPass.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPass.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPass.ForeColor = System.Drawing.Color.Black
        Me.txtPass.Location = New System.Drawing.Point(78, 544)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.Size = New System.Drawing.Size(1506, 35)
        Me.txtPass.TabIndex = 5
        '
        'PassLbl
        '
        Me.PassLbl.AutoSize = True
        Me.PassLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassLbl.Location = New System.Drawing.Point(73, 516)
        Me.PassLbl.Name = "PassLbl"
        Me.PassLbl.Size = New System.Drawing.Size(95, 25)
        Me.PassLbl.TabIndex = 0
        Me.PassLbl.Text = "Password"
        '
        'txtUname
        '
        Me.txtUname.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtUname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUname.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUname.ForeColor = System.Drawing.Color.Black
        Me.txtUname.Location = New System.Drawing.Point(78, 426)
        Me.txtUname.Name = "txtUname"
        Me.txtUname.Size = New System.Drawing.Size(1506, 35)
        Me.txtUname.TabIndex = 4
        '
        'UnameLbL
        '
        Me.UnameLbL.AutoSize = True
        Me.UnameLbL.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UnameLbL.Location = New System.Drawing.Point(73, 398)
        Me.UnameLbL.Name = "UnameLbL"
        Me.UnameLbL.Size = New System.Drawing.Size(97, 25)
        Me.UnameLbL.TabIndex = 0
        Me.UnameLbL.Text = "Username"
        '
        'txtLname
        '
        Me.txtLname.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtLname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLname.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLname.ForeColor = System.Drawing.Color.Black
        Me.txtLname.Location = New System.Drawing.Point(78, 292)
        Me.txtLname.Name = "txtLname"
        Me.txtLname.Size = New System.Drawing.Size(1506, 35)
        Me.txtLname.TabIndex = 2
        '
        'LnameLbl
        '
        Me.LnameLbl.AutoSize = True
        Me.LnameLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LnameLbl.Location = New System.Drawing.Point(73, 266)
        Me.LnameLbl.Name = "LnameLbl"
        Me.LnameLbl.Size = New System.Drawing.Size(101, 25)
        Me.LnameLbl.TabIndex = 0
        Me.LnameLbl.Text = "Last Name"
        '
        'txtFname
        '
        Me.txtFname.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFname.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFname.ForeColor = System.Drawing.Color.Black
        Me.txtFname.Location = New System.Drawing.Point(78, 173)
        Me.txtFname.Name = "txtFname"
        Me.txtFname.Size = New System.Drawing.Size(1506, 35)
        Me.txtFname.TabIndex = 1
        '
        'FNameLbl
        '
        Me.FNameLbl.AutoSize = True
        Me.FNameLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FNameLbl.Location = New System.Drawing.Point(73, 145)
        Me.FNameLbl.Name = "FNameLbl"
        Me.FNameLbl.Size = New System.Drawing.Size(103, 25)
        Me.FNameLbl.TabIndex = 0
        Me.FNameLbl.Text = "First Name"
        '
        'ManageAccountLbl
        '
        Me.ManageAccountLbl.AutoSize = True
        Me.ManageAccountLbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ManageAccountLbl.Location = New System.Drawing.Point(20, 40)
        Me.ManageAccountLbl.Name = "ManageAccountLbl"
        Me.ManageAccountLbl.Size = New System.Drawing.Size(230, 32)
        Me.ManageAccountLbl.TabIndex = 0
        Me.ManageAccountLbl.Text = "Manage Account"
        '
        'ManageUserAccount_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ManageUserAccount_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ManageUserAccount_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.SeePassBtn2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SeePassBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents ManageAccountLbl As Label
    Friend WithEvents FNameLbl As Label
    Friend WithEvents txtUname As TextBox
    Friend WithEvents UnameLbL As Label
    Friend WithEvents txtLname As TextBox
    Friend WithEvents LnameLbl As Label
    Friend WithEvents txtFname As TextBox
    Friend WithEvents txtConfirmPass As TextBox
    Friend WithEvents ConfirmPassLbl As Label
    Friend WithEvents txtPass As TextBox
    Friend WithEvents PassLbl As Label
    Friend WithEvents btnSaveInfo As Button
    Friend WithEvents SeePassBtn As PictureBox
    Friend WithEvents SeePassBtn2 As PictureBox
End Class
