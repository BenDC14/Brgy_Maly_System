<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatabaseBackup_Form
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
        Me.lblBackupDatabase = New System.Windows.Forms.Label()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.txtFilename = New System.Windows.Forms.TextBox()
        Me.txtFileLocation = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.BackupNotesRtxt = New System.Windows.Forms.RichTextBox()
        Me.lblBackupNotes = New System.Windows.Forms.Label()
        Me.btnBacktoMain = New System.Windows.Forms.Button()
        Me.btnStartBackup = New System.Windows.Forms.Button()
        Me.FillPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnBacktoMain)
        Me.FillPanel.Controls.Add(Me.btnStartBackup)
        Me.FillPanel.Controls.Add(Me.lblBackupNotes)
        Me.FillPanel.Controls.Add(Me.BackupNotesRtxt)
        Me.FillPanel.Controls.Add(Me.btnBrowse)
        Me.FillPanel.Controls.Add(Me.txtFileLocation)
        Me.FillPanel.Controls.Add(Me.Label1)
        Me.FillPanel.Controls.Add(Me.txtFilename)
        Me.FillPanel.Controls.Add(Me.lblFileName)
        Me.FillPanel.Controls.Add(Me.lblBackupDatabase)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'lblBackupDatabase
        '
        Me.lblBackupDatabase.AutoSize = True
        Me.lblBackupDatabase.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupDatabase.Location = New System.Drawing.Point(38, 33)
        Me.lblBackupDatabase.Name = "lblBackupDatabase"
        Me.lblBackupDatabase.Size = New System.Drawing.Size(242, 32)
        Me.lblBackupDatabase.TabIndex = 0
        Me.lblBackupDatabase.Text = "Backup Database"
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileName.Location = New System.Drawing.Point(66, 169)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(100, 22)
        Me.lblFileName.TabIndex = 0
        Me.lblFileName.Text = "File Name"
        '
        'txtFilename
        '
        Me.txtFilename.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFilename.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilename.Location = New System.Drawing.Point(70, 194)
        Me.txtFilename.Name = "txtFilename"
        Me.txtFilename.Size = New System.Drawing.Size(1435, 29)
        Me.txtFilename.TabIndex = 1
        '
        'txtFileLocation
        '
        Me.txtFileLocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFileLocation.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileLocation.Location = New System.Drawing.Point(70, 346)
        Me.txtFileLocation.Name = "txtFileLocation"
        Me.txtFileLocation.Size = New System.Drawing.Size(1435, 29)
        Me.txtFileLocation.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(66, 321)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "File Location"
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBrowse.FlatAppearance.BorderSize = 0
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(1546, 347)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(119, 29)
        Me.btnBrowse.TabIndex = 3
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'BackupNotesRtxt
        '
        Me.BackupNotesRtxt.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.BackupNotesRtxt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupNotesRtxt.Location = New System.Drawing.Point(70, 505)
        Me.BackupNotesRtxt.Name = "BackupNotesRtxt"
        Me.BackupNotesRtxt.Size = New System.Drawing.Size(1435, 230)
        Me.BackupNotesRtxt.TabIndex = 4
        Me.BackupNotesRtxt.Text = ""
        '
        'lblBackupNotes
        '
        Me.lblBackupNotes.AutoSize = True
        Me.lblBackupNotes.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupNotes.Location = New System.Drawing.Point(66, 480)
        Me.lblBackupNotes.Name = "lblBackupNotes"
        Me.lblBackupNotes.Size = New System.Drawing.Size(234, 22)
        Me.lblBackupNotes.TabIndex = 0
        Me.lblBackupNotes.Text = "Backup Notes (Optional)"
        '
        'btnBacktoMain
        '
        Me.btnBacktoMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBacktoMain.FlatAppearance.BorderSize = 0
        Me.btnBacktoMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBacktoMain.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBacktoMain.Location = New System.Drawing.Point(926, 781)
        Me.btnBacktoMain.Name = "btnBacktoMain"
        Me.btnBacktoMain.Size = New System.Drawing.Size(189, 42)
        Me.btnBacktoMain.TabIndex = 6
        Me.btnBacktoMain.Text = "Back To Main"
        Me.btnBacktoMain.UseVisualStyleBackColor = False
        '
        'btnStartBackup
        '
        Me.btnStartBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnStartBackup.FlatAppearance.BorderSize = 0
        Me.btnStartBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStartBackup.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartBackup.Location = New System.Drawing.Point(384, 781)
        Me.btnStartBackup.Name = "btnStartBackup"
        Me.btnStartBackup.Size = New System.Drawing.Size(189, 42)
        Me.btnStartBackup.TabIndex = 5
        Me.btnStartBackup.Text = "Start Backup"
        Me.btnStartBackup.UseVisualStyleBackColor = False
        '
        'DatabaseBackup_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DatabaseBackup_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DatabaseBackup_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents lblBackupDatabase As Label
    Friend WithEvents lblFileName As Label
    Friend WithEvents txtFilename As TextBox
    Friend WithEvents txtFileLocation As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnBrowse As Button
    Friend WithEvents lblBackupNotes As Label
    Friend WithEvents BackupNotesRtxt As RichTextBox
    Friend WithEvents btnBacktoMain As Button
    Friend WithEvents btnStartBackup As Button
End Class
