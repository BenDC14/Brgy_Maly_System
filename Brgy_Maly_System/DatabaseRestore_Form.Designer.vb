<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatabaseRestore_Form
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
        Me.lblRestoreDatabase = New System.Windows.Forms.Label()
        Me.btnBacktoMain = New System.Windows.Forms.Button()
        Me.btnRestoreNow = New System.Windows.Forms.Button()
        Me.lblWarningMessage = New System.Windows.Forms.Label()
        Me.lblWarning = New System.Windows.Forms.Label()
        Me.LinePanel2 = New System.Windows.Forms.Panel()
        Me.LinePnl1 = New System.Windows.Forms.Panel()
        Me.txtBackupStatus = New System.Windows.Forms.TextBox()
        Me.lblBackupStatus = New System.Windows.Forms.Label()
        Me.txtBackupDate = New System.Windows.Forms.TextBox()
        Me.lblBackupDate = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtSelectBackupFile = New System.Windows.Forms.TextBox()
        Me.lblSelectBackupFile = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.lblRestoreDatabase)
        Me.FillPanel.Controls.Add(Me.btnBacktoMain)
        Me.FillPanel.Controls.Add(Me.btnRestoreNow)
        Me.FillPanel.Controls.Add(Me.lblWarningMessage)
        Me.FillPanel.Controls.Add(Me.lblWarning)
        Me.FillPanel.Controls.Add(Me.LinePanel2)
        Me.FillPanel.Controls.Add(Me.LinePnl1)
        Me.FillPanel.Controls.Add(Me.txtBackupStatus)
        Me.FillPanel.Controls.Add(Me.lblBackupStatus)
        Me.FillPanel.Controls.Add(Me.txtBackupDate)
        Me.FillPanel.Controls.Add(Me.lblBackupDate)
        Me.FillPanel.Controls.Add(Me.txtFileName)
        Me.FillPanel.Controls.Add(Me.lblFileName)
        Me.FillPanel.Controls.Add(Me.btnBrowse)
        Me.FillPanel.Controls.Add(Me.txtSelectBackupFile)
        Me.FillPanel.Controls.Add(Me.lblSelectBackupFile)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'lblRestoreDatabase
        '
        Me.lblRestoreDatabase.AutoSize = True
        Me.lblRestoreDatabase.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRestoreDatabase.Location = New System.Drawing.Point(38, 33)
        Me.lblRestoreDatabase.Name = "lblRestoreDatabase"
        Me.lblRestoreDatabase.Size = New System.Drawing.Size(245, 32)
        Me.lblRestoreDatabase.TabIndex = 8
        Me.lblRestoreDatabase.Text = "Restore Database"
        '
        'btnBacktoMain
        '
        Me.btnBacktoMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBacktoMain.FlatAppearance.BorderSize = 0
        Me.btnBacktoMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBacktoMain.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBacktoMain.Location = New System.Drawing.Point(1027, 870)
        Me.btnBacktoMain.Name = "btnBacktoMain"
        Me.btnBacktoMain.Size = New System.Drawing.Size(189, 42)
        Me.btnBacktoMain.TabIndex = 7
        Me.btnBacktoMain.Text = "Back To Main"
        Me.btnBacktoMain.UseVisualStyleBackColor = False
        '
        'btnRestoreNow
        '
        Me.btnRestoreNow.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnRestoreNow.FlatAppearance.BorderSize = 0
        Me.btnRestoreNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestoreNow.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestoreNow.Location = New System.Drawing.Point(485, 870)
        Me.btnRestoreNow.Name = "btnRestoreNow"
        Me.btnRestoreNow.Size = New System.Drawing.Size(189, 42)
        Me.btnRestoreNow.TabIndex = 6
        Me.btnRestoreNow.Text = "Restore Now"
        Me.btnRestoreNow.UseVisualStyleBackColor = False
        '
        'lblWarningMessage
        '
        Me.lblWarningMessage.AutoSize = True
        Me.lblWarningMessage.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWarningMessage.Location = New System.Drawing.Point(176, 683)
        Me.lblWarningMessage.Name = "lblWarningMessage"
        Me.lblWarningMessage.Size = New System.Drawing.Size(1313, 32)
        Me.lblWarningMessage.TabIndex = 0
        Me.lblWarningMessage.Text = "Restoring will overwrite all current data. Only authorized super admins should pe" &
    "rform this action."
        '
        'lblWarning
        '
        Me.lblWarning.AutoSize = True
        Me.lblWarning.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWarning.Location = New System.Drawing.Point(64, 621)
        Me.lblWarning.Name = "lblWarning"
        Me.lblWarning.Size = New System.Drawing.Size(131, 32)
        Me.lblWarning.TabIndex = 0
        Me.lblWarning.Text = "Warning!"
        '
        'LinePanel2
        '
        Me.LinePanel2.BackColor = System.Drawing.Color.Black
        Me.LinePanel2.Location = New System.Drawing.Point(0, 813)
        Me.LinePanel2.Name = "LinePanel2"
        Me.LinePanel2.Size = New System.Drawing.Size(1700, 2)
        Me.LinePanel2.TabIndex = 0
        '
        'LinePnl1
        '
        Me.LinePnl1.BackColor = System.Drawing.Color.Black
        Me.LinePnl1.Location = New System.Drawing.Point(0, 595)
        Me.LinePnl1.Name = "LinePnl1"
        Me.LinePnl1.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl1.TabIndex = 0
        '
        'txtBackupStatus
        '
        Me.txtBackupStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtBackupStatus.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackupStatus.Location = New System.Drawing.Point(70, 534)
        Me.txtBackupStatus.Name = "txtBackupStatus"
        Me.txtBackupStatus.Size = New System.Drawing.Size(1435, 29)
        Me.txtBackupStatus.TabIndex = 5
        '
        'lblBackupStatus
        '
        Me.lblBackupStatus.AutoSize = True
        Me.lblBackupStatus.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupStatus.Location = New System.Drawing.Point(66, 509)
        Me.lblBackupStatus.Name = "lblBackupStatus"
        Me.lblBackupStatus.Size = New System.Drawing.Size(145, 22)
        Me.lblBackupStatus.TabIndex = 5
        Me.lblBackupStatus.Text = "Backup Status"
        '
        'txtBackupDate
        '
        Me.txtBackupDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtBackupDate.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackupDate.Location = New System.Drawing.Point(70, 401)
        Me.txtBackupDate.Name = "txtBackupDate"
        Me.txtBackupDate.Size = New System.Drawing.Size(1435, 29)
        Me.txtBackupDate.TabIndex = 4
        Me.txtBackupDate.Text = " "
        '
        'lblBackupDate
        '
        Me.lblBackupDate.AutoSize = True
        Me.lblBackupDate.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupDate.Location = New System.Drawing.Point(66, 376)
        Me.lblBackupDate.Name = "lblBackupDate"
        Me.lblBackupDate.Size = New System.Drawing.Size(128, 22)
        Me.lblBackupDate.TabIndex = 0
        Me.lblBackupDate.Text = "Backup Date"
        '
        'txtFileName
        '
        Me.txtFileName.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFileName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileName.Location = New System.Drawing.Point(70, 276)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(1435, 29)
        Me.txtFileName.TabIndex = 3
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileName.Location = New System.Drawing.Point(66, 251)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(100, 22)
        Me.lblFileName.TabIndex = 0
        Me.lblFileName.Text = "File Name"
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBrowse.FlatAppearance.BorderSize = 0
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(1544, 159)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(119, 29)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'txtSelectBackupFile
        '
        Me.txtSelectBackupFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSelectBackupFile.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelectBackupFile.Location = New System.Drawing.Point(70, 159)
        Me.txtSelectBackupFile.Name = "txtSelectBackupFile"
        Me.txtSelectBackupFile.Size = New System.Drawing.Size(1435, 29)
        Me.txtSelectBackupFile.TabIndex = 1
        '
        'lblSelectBackupFile
        '
        Me.lblSelectBackupFile.AutoSize = True
        Me.lblSelectBackupFile.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectBackupFile.Location = New System.Drawing.Point(66, 134)
        Me.lblSelectBackupFile.Name = "lblSelectBackupFile"
        Me.lblSelectBackupFile.Size = New System.Drawing.Size(181, 22)
        Me.lblSelectBackupFile.TabIndex = 0
        Me.lblSelectBackupFile.Text = "Select Backup File"
        '
        'DatabaseRestore_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DatabaseRestore_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DatabaseRestore_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents lblSelectBackupFile As Label
    Friend WithEvents txtSelectBackupFile As TextBox
    Friend WithEvents txtFileName As TextBox
    Friend WithEvents lblFileName As Label
    Friend WithEvents btnBrowse As Button
    Friend WithEvents txtBackupDate As TextBox
    Friend WithEvents lblBackupDate As Label
    Friend WithEvents txtBackupStatus As TextBox
    Friend WithEvents lblBackupStatus As Label
    Friend WithEvents lblWarningMessage As Label
    Friend WithEvents lblWarning As Label
    Friend WithEvents LinePanel2 As Panel
    Friend WithEvents LinePnl1 As Panel
    Friend WithEvents btnBacktoMain As Button
    Friend WithEvents btnRestoreNow As Button
    Friend WithEvents lblRestoreDatabase As Label
End Class
