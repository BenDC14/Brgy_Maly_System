<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DatabaseDownloadLogs_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.FillPanel = New System.Windows.Forms.Panel()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.cbExcel = New System.Windows.Forms.CheckBox()
        Me.cbCSV = New System.Windows.Forms.CheckBox()
        Me.lblFormat = New System.Windows.Forms.Label()
        Me.cbBoth = New System.Windows.Forms.CheckBox()
        Me.cbRestoreLogs = New System.Windows.Forms.CheckBox()
        Me.cbBackupLogs = New System.Windows.Forms.CheckBox()
        Me.lblSelectLogType = New System.Windows.Forms.Label()
        Me.lblDownloadBackupAndRestoreLogs = New System.Windows.Forms.Label()
        Me.ExitBtn = New System.Windows.Forms.PictureBox()
        Me.FillPanel.SuspendLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnDownload)
        Me.FillPanel.Controls.Add(Me.cbExcel)
        Me.FillPanel.Controls.Add(Me.cbCSV)
        Me.FillPanel.Controls.Add(Me.lblFormat)
        Me.FillPanel.Controls.Add(Me.cbBoth)
        Me.FillPanel.Controls.Add(Me.cbRestoreLogs)
        Me.FillPanel.Controls.Add(Me.cbBackupLogs)
        Me.FillPanel.Controls.Add(Me.lblSelectLogType)
        Me.FillPanel.Controls.Add(Me.lblDownloadBackupAndRestoreLogs)
        Me.FillPanel.Controls.Add(Me.ExitBtn)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(470, 566)
        Me.FillPanel.TabIndex = 0
        '
        'btnDownload
        '
        Me.btnDownload.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnDownload.FlatAppearance.BorderSize = 0
        Me.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDownload.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownload.Location = New System.Drawing.Point(165, 501)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(151, 33)
        Me.btnDownload.TabIndex = 6
        Me.btnDownload.Text = "Download"
        Me.btnDownload.UseVisualStyleBackColor = False
        '
        'cbExcel
        '
        Me.cbExcel.AutoSize = True
        Me.cbExcel.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExcel.Location = New System.Drawing.Point(92, 438)
        Me.cbExcel.Name = "cbExcel"
        Me.cbExcel.Size = New System.Drawing.Size(76, 27)
        Me.cbExcel.TabIndex = 0
        Me.cbExcel.Text = "Excel"
        Me.cbExcel.UseVisualStyleBackColor = True
        '
        'cbCSV
        '
        Me.cbCSV.AutoSize = True
        Me.cbCSV.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCSV.Location = New System.Drawing.Point(92, 379)
        Me.cbCSV.Name = "cbCSV"
        Me.cbCSV.Size = New System.Drawing.Size(69, 27)
        Me.cbCSV.TabIndex = 4
        Me.cbCSV.Text = "CSV"
        Me.cbCSV.UseVisualStyleBackColor = True
        '
        'lblFormat
        '
        Me.lblFormat.AutoSize = True
        Me.lblFormat.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormat.Location = New System.Drawing.Point(19, 345)
        Me.lblFormat.Name = "lblFormat"
        Me.lblFormat.Size = New System.Drawing.Size(89, 24)
        Me.lblFormat.TabIndex = 0
        Me.lblFormat.Text = "Format:"
        '
        'cbBoth
        '
        Me.cbBoth.AutoSize = True
        Me.cbBoth.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBoth.Location = New System.Drawing.Point(92, 289)
        Me.cbBoth.Name = "cbBoth"
        Me.cbBoth.Size = New System.Drawing.Size(69, 27)
        Me.cbBoth.TabIndex = 4
        Me.cbBoth.Text = "Both"
        Me.cbBoth.UseVisualStyleBackColor = True
        '
        'cbRestoreLogs
        '
        Me.cbRestoreLogs.AutoSize = True
        Me.cbRestoreLogs.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRestoreLogs.Location = New System.Drawing.Point(92, 230)
        Me.cbRestoreLogs.Name = "cbRestoreLogs"
        Me.cbRestoreLogs.Size = New System.Drawing.Size(148, 27)
        Me.cbRestoreLogs.TabIndex = 2
        Me.cbRestoreLogs.Text = "Restore Logs"
        Me.cbRestoreLogs.UseVisualStyleBackColor = True
        '
        'cbBackupLogs
        '
        Me.cbBackupLogs.AutoSize = True
        Me.cbBackupLogs.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBackupLogs.Location = New System.Drawing.Point(92, 171)
        Me.cbBackupLogs.Name = "cbBackupLogs"
        Me.cbBackupLogs.Size = New System.Drawing.Size(143, 27)
        Me.cbBackupLogs.TabIndex = 1
        Me.cbBackupLogs.Text = "Backup Logs"
        Me.cbBackupLogs.UseVisualStyleBackColor = True
        '
        'lblSelectLogType
        '
        Me.lblSelectLogType.AutoSize = True
        Me.lblSelectLogType.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectLogType.Location = New System.Drawing.Point(19, 122)
        Me.lblSelectLogType.Name = "lblSelectLogType"
        Me.lblSelectLogType.Size = New System.Drawing.Size(165, 24)
        Me.lblSelectLogType.TabIndex = 0
        Me.lblSelectLogType.Text = "Select log type:"
        '
        'lblDownloadBackupAndRestoreLogs
        '
        Me.lblDownloadBackupAndRestoreLogs.AutoSize = True
        Me.lblDownloadBackupAndRestoreLogs.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDownloadBackupAndRestoreLogs.Location = New System.Drawing.Point(18, 38)
        Me.lblDownloadBackupAndRestoreLogs.Name = "lblDownloadBackupAndRestoreLogs"
        Me.lblDownloadBackupAndRestoreLogs.Size = New System.Drawing.Size(432, 29)
        Me.lblDownloadBackupAndRestoreLogs.TabIndex = 0
        Me.lblDownloadBackupAndRestoreLogs.Text = "Download Backup and Restore Logs"
        '
        'ExitBtn
        '
        Me.ExitBtn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExitBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.ExitButton
        Me.ExitBtn.Location = New System.Drawing.Point(438, 1)
        Me.ExitBtn.Name = "ExitBtn"
        Me.ExitBtn.Size = New System.Drawing.Size(31, 30)
        Me.ExitBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ExitBtn.TabIndex = 2
        Me.ExitBtn.TabStop = False
        '
        'DatabaseDownloadLogs_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(470, 566)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DatabaseDownloadLogs_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DatabaseDownload_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.ExitBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents ExitBtn As PictureBox
    Friend WithEvents lblDownloadBackupAndRestoreLogs As Label
    Friend WithEvents cbExcel As CheckBox
    Friend WithEvents cbCSV As CheckBox
    Friend WithEvents lblFormat As Label
    Friend WithEvents cbBoth As CheckBox
    Friend WithEvents cbRestoreLogs As CheckBox
    Friend WithEvents cbBackupLogs As CheckBox
    Friend WithEvents lblSelectLogType As Label
    Friend WithEvents btnDownload As Button
End Class
