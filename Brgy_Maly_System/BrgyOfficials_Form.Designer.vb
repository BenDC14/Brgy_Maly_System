<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BrgyOfficials_Form
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
        Me.LinePnl = New System.Windows.Forms.Panel()
        Me.BtnBackToMain = New System.Windows.Forms.Button()
        Me.BtnSaveOfficial = New System.Windows.Forms.Button()
        Me.TermEndDTP = New System.Windows.Forms.DateTimePicker()
        Me.TermEndLbl = New System.Windows.Forms.Label()
        Me.TermStartDTP = New System.Windows.Forms.DateTimePicker()
        Me.TermStartLbl = New System.Windows.Forms.Label()
        Me.cbPosition = New System.Windows.Forms.ComboBox()
        Me.PositionLbl = New System.Windows.Forms.Label()
        Me.txtLname = New System.Windows.Forms.TextBox()
        Me.Lnamelbl = New System.Windows.Forms.Label()
        Me.txtFname = New System.Windows.Forms.TextBox()
        Me.FNameLbl = New System.Windows.Forms.Label()
        Me.BtnRemove = New System.Windows.Forms.Button()
        Me.BrgyOfficialLbl = New System.Windows.Forms.Label()
        Me.BtnUpload = New System.Windows.Forms.Button()
        Me.BrgyLogoLbl = New System.Windows.Forms.Label()
        Me.BrgyLogoPic = New System.Windows.Forms.PictureBox()
        Me.FillPanel.SuspendLayout()
        CType(Me.BrgyLogoPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.LinePnl)
        Me.FillPanel.Controls.Add(Me.BtnBackToMain)
        Me.FillPanel.Controls.Add(Me.BtnSaveOfficial)
        Me.FillPanel.Controls.Add(Me.TermEndDTP)
        Me.FillPanel.Controls.Add(Me.TermEndLbl)
        Me.FillPanel.Controls.Add(Me.TermStartDTP)
        Me.FillPanel.Controls.Add(Me.TermStartLbl)
        Me.FillPanel.Controls.Add(Me.cbPosition)
        Me.FillPanel.Controls.Add(Me.PositionLbl)
        Me.FillPanel.Controls.Add(Me.txtLname)
        Me.FillPanel.Controls.Add(Me.Lnamelbl)
        Me.FillPanel.Controls.Add(Me.txtFname)
        Me.FillPanel.Controls.Add(Me.FNameLbl)
        Me.FillPanel.Controls.Add(Me.BtnRemove)
        Me.FillPanel.Controls.Add(Me.BrgyOfficialLbl)
        Me.FillPanel.Controls.Add(Me.BtnUpload)
        Me.FillPanel.Controls.Add(Me.BrgyLogoPic)
        Me.FillPanel.Controls.Add(Me.BrgyLogoLbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'LinePnl
        '
        Me.LinePnl.BackColor = System.Drawing.Color.Black
        Me.LinePnl.Location = New System.Drawing.Point(0, 81)
        Me.LinePnl.Name = "LinePnl"
        Me.LinePnl.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl.TabIndex = 0
        '
        'BtnBackToMain
        '
        Me.BtnBackToMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnBackToMain.FlatAppearance.BorderSize = 0
        Me.BtnBackToMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBackToMain.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBackToMain.Location = New System.Drawing.Point(1007, 883)
        Me.BtnBackToMain.Name = "BtnBackToMain"
        Me.BtnBackToMain.Size = New System.Drawing.Size(284, 44)
        Me.BtnBackToMain.TabIndex = 9
        Me.BtnBackToMain.Text = "Back To Main"
        Me.BtnBackToMain.UseVisualStyleBackColor = False
        '
        'BtnSaveOfficial
        '
        Me.BtnSaveOfficial.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnSaveOfficial.FlatAppearance.BorderSize = 0
        Me.BtnSaveOfficial.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveOfficial.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveOfficial.Location = New System.Drawing.Point(506, 883)
        Me.BtnSaveOfficial.Name = "BtnSaveOfficial"
        Me.BtnSaveOfficial.Size = New System.Drawing.Size(284, 44)
        Me.BtnSaveOfficial.TabIndex = 8
        Me.BtnSaveOfficial.Text = "Save Barangay Official"
        Me.BtnSaveOfficial.UseVisualStyleBackColor = False
        '
        'TermEndDTP
        '
        Me.TermEndDTP.CalendarFont = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TermEndDTP.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TermEndDTP.Location = New System.Drawing.Point(245, 733)
        Me.TermEndDTP.Name = "TermEndDTP"
        Me.TermEndDTP.Size = New System.Drawing.Size(1308, 35)
        Me.TermEndDTP.TabIndex = 7
        Me.TermEndDTP.Value = New Date(2026, 1, 24, 16, 53, 38, 0)
        '
        'TermEndLbl
        '
        Me.TermEndLbl.AutoSize = True
        Me.TermEndLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TermEndLbl.Location = New System.Drawing.Point(46, 733)
        Me.TermEndLbl.Name = "TermEndLbl"
        Me.TermEndLbl.Size = New System.Drawing.Size(99, 25)
        Me.TermEndLbl.TabIndex = 9
        Me.TermEndLbl.Text = "Term End:"
        '
        'TermStartDTP
        '
        Me.TermStartDTP.CalendarFont = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TermStartDTP.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TermStartDTP.Location = New System.Drawing.Point(245, 621)
        Me.TermStartDTP.Name = "TermStartDTP"
        Me.TermStartDTP.Size = New System.Drawing.Size(1308, 35)
        Me.TermStartDTP.TabIndex = 6
        Me.TermStartDTP.Value = New Date(2026, 1, 24, 16, 53, 38, 0)
        '
        'TermStartLbl
        '
        Me.TermStartLbl.AutoSize = True
        Me.TermStartLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TermStartLbl.Location = New System.Drawing.Point(46, 621)
        Me.TermStartLbl.Name = "TermStartLbl"
        Me.TermStartLbl.Size = New System.Drawing.Size(106, 25)
        Me.TermStartLbl.TabIndex = 0
        Me.TermStartLbl.Text = "Term Start:"
        '
        'cbPosition
        '
        Me.cbPosition.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbPosition.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPosition.FormattingEnabled = True
        Me.cbPosition.Location = New System.Drawing.Point(245, 513)
        Me.cbPosition.Name = "cbPosition"
        Me.cbPosition.Size = New System.Drawing.Size(1308, 35)
        Me.cbPosition.TabIndex = 5
        '
        'PositionLbl
        '
        Me.PositionLbl.AutoSize = True
        Me.PositionLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PositionLbl.Location = New System.Drawing.Point(46, 513)
        Me.PositionLbl.Name = "PositionLbl"
        Me.PositionLbl.Size = New System.Drawing.Size(88, 25)
        Me.PositionLbl.TabIndex = 0
        Me.PositionLbl.Text = "Position:"
        '
        'txtLname
        '
        Me.txtLname.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtLname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLname.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLname.ForeColor = System.Drawing.Color.Black
        Me.txtLname.Location = New System.Drawing.Point(245, 402)
        Me.txtLname.Name = "txtLname"
        Me.txtLname.Size = New System.Drawing.Size(1308, 35)
        Me.txtLname.TabIndex = 4
        '
        'Lnamelbl
        '
        Me.Lnamelbl.AutoSize = True
        Me.Lnamelbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lnamelbl.Location = New System.Drawing.Point(46, 402)
        Me.Lnamelbl.Name = "Lnamelbl"
        Me.Lnamelbl.Size = New System.Drawing.Size(107, 25)
        Me.Lnamelbl.TabIndex = 0
        Me.Lnamelbl.Text = "Last Name:"
        '
        'txtFname
        '
        Me.txtFname.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFname.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFname.ForeColor = System.Drawing.Color.Black
        Me.txtFname.Location = New System.Drawing.Point(245, 290)
        Me.txtFname.Name = "txtFname"
        Me.txtFname.Size = New System.Drawing.Size(1308, 35)
        Me.txtFname.TabIndex = 3
        '
        'FNameLbl
        '
        Me.FNameLbl.AutoSize = True
        Me.FNameLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FNameLbl.Location = New System.Drawing.Point(46, 290)
        Me.FNameLbl.Name = "FNameLbl"
        Me.FNameLbl.Size = New System.Drawing.Size(109, 25)
        Me.FNameLbl.TabIndex = 0
        Me.FNameLbl.Text = "First Name:"
        '
        'BtnRemove
        '
        Me.BtnRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnRemove.FlatAppearance.BorderSize = 0
        Me.BtnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRemove.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRemove.Location = New System.Drawing.Point(709, 160)
        Me.BtnRemove.Name = "BtnRemove"
        Me.BtnRemove.Size = New System.Drawing.Size(199, 44)
        Me.BtnRemove.TabIndex = 2
        Me.BtnRemove.Text = "Remove"
        Me.BtnRemove.UseVisualStyleBackColor = False
        '
        'BrgyOfficialLbl
        '
        Me.BrgyOfficialLbl.AutoSize = True
        Me.BrgyOfficialLbl.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyOfficialLbl.Location = New System.Drawing.Point(23, 25)
        Me.BrgyOfficialLbl.Name = "BrgyOfficialLbl"
        Me.BrgyOfficialLbl.Size = New System.Drawing.Size(352, 37)
        Me.BrgyOfficialLbl.TabIndex = 0
        Me.BrgyOfficialLbl.Text = "Add Barangay Official"
        '
        'BtnUpload
        '
        Me.BtnUpload.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnUpload.FlatAppearance.BorderSize = 0
        Me.BtnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpload.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpload.Location = New System.Drawing.Point(434, 160)
        Me.BtnUpload.Name = "BtnUpload"
        Me.BtnUpload.Size = New System.Drawing.Size(199, 44)
        Me.BtnUpload.TabIndex = 1
        Me.BtnUpload.Text = "Upload"
        Me.BtnUpload.UseVisualStyleBackColor = False
        '
        'BrgyLogoLbl
        '
        Me.BrgyLogoLbl.AutoSize = True
        Me.BrgyLogoLbl.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyLogoLbl.Location = New System.Drawing.Point(47, 129)
        Me.BrgyLogoLbl.Name = "BrgyLogoLbl"
        Me.BrgyLogoLbl.Size = New System.Drawing.Size(141, 22)
        Me.BrgyLogoLbl.TabIndex = 0
        Me.BrgyLogoLbl.Text = "Official Photo:"
        '
        'BrgyLogoPic
        '
        Me.BrgyLogoPic.Image = Global.Brgy_Maly_System.My.Resources.Resources.userICON
        Me.BrgyLogoPic.Location = New System.Drawing.Point(245, 129)
        Me.BrgyLogoPic.Name = "BrgyLogoPic"
        Me.BrgyLogoPic.Size = New System.Drawing.Size(117, 104)
        Me.BrgyLogoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BrgyLogoPic.TabIndex = 8
        Me.BrgyLogoPic.TabStop = False
        '
        'BrgyOfficials_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "BrgyOfficials_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BrgyOfficials_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.BrgyLogoPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents BrgyOfficialLbl As Label
    Friend WithEvents BtnRemove As Button
    Friend WithEvents BtnUpload As Button
    Friend WithEvents BrgyLogoPic As PictureBox
    Friend WithEvents BrgyLogoLbl As Label
    Friend WithEvents txtFname As TextBox
    Friend WithEvents FNameLbl As Label
    Friend WithEvents PositionLbl As Label
    Friend WithEvents txtLname As TextBox
    Friend WithEvents Lnamelbl As Label
    Friend WithEvents cbPosition As ComboBox
    Friend WithEvents TermStartDTP As DateTimePicker
    Friend WithEvents TermStartLbl As Label
    Friend WithEvents TermEndDTP As DateTimePicker
    Friend WithEvents TermEndLbl As Label
    Friend WithEvents BtnBackToMain As Button
    Friend WithEvents BtnSaveOfficial As Button
    Friend WithEvents LinePnl As Panel
End Class
