<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatabaseBackupMain_Form
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
        Me.fillpanel = New System.Windows.Forms.Panel()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnPageNext = New System.Windows.Forms.Button()
        Me.btnPage3 = New System.Windows.Forms.Button()
        Me.btnPage2 = New System.Windows.Forms.Button()
        Me.btnPage1 = New System.Windows.Forms.Button()
        Me.lblPages = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.lblBackupAndRestoreLogs = New System.Windows.Forms.Label()
        Me.dgvDatabase = New System.Windows.Forms.DataGridView()
        Me.btnDownloadLogs = New System.Windows.Forms.Button()
        Me.LinePnl2 = New System.Windows.Forms.Panel()
        Me.btnRestoreDB = New System.Windows.Forms.Button()
        Me.btnBackupDB = New System.Windows.Forms.Button()
        Me.lblDatabaseMaintenance = New System.Windows.Forms.Label()
        Me.fillpanel.SuspendLayout()
        CType(Me.dgvDatabase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fillpanel
        '
        Me.fillpanel.BackColor = System.Drawing.Color.Transparent
        Me.fillpanel.Controls.Add(Me.btnView)
        Me.fillpanel.Controls.Add(Me.btnPageNext)
        Me.fillpanel.Controls.Add(Me.btnPage3)
        Me.fillpanel.Controls.Add(Me.btnPage2)
        Me.fillpanel.Controls.Add(Me.btnPage1)
        Me.fillpanel.Controls.Add(Me.lblPages)
        Me.fillpanel.Controls.Add(Me.btnSearch)
        Me.fillpanel.Controls.Add(Me.txtSearch)
        Me.fillpanel.Controls.Add(Me.lblSearch)
        Me.fillpanel.Controls.Add(Me.lblBackupAndRestoreLogs)
        Me.fillpanel.Controls.Add(Me.dgvDatabase)
        Me.fillpanel.Controls.Add(Me.btnDownloadLogs)
        Me.fillpanel.Controls.Add(Me.LinePnl2)
        Me.fillpanel.Controls.Add(Me.btnRestoreDB)
        Me.fillpanel.Controls.Add(Me.btnBackupDB)
        Me.fillpanel.Controls.Add(Me.lblDatabaseMaintenance)
        Me.fillpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fillpanel.Location = New System.Drawing.Point(0, 0)
        Me.fillpanel.Name = "fillpanel"
        Me.fillpanel.Size = New System.Drawing.Size(1700, 1004)
        Me.fillpanel.TabIndex = 0
        '
        'btnView
        '
        Me.btnView.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnView.FlatAppearance.BorderSize = 0
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnView.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnView.Location = New System.Drawing.Point(1538, 860)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(128, 32)
        Me.btnView.TabIndex = 11
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = False
        '
        'btnPageNext
        '
        Me.btnPageNext.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnPageNext.FlatAppearance.BorderSize = 0
        Me.btnPageNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPageNext.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPageNext.Location = New System.Drawing.Point(261, 856)
        Me.btnPageNext.Name = "btnPageNext"
        Me.btnPageNext.Size = New System.Drawing.Size(77, 33)
        Me.btnPageNext.TabIndex = 10
        Me.btnPageNext.Text = "Next"
        Me.btnPageNext.UseVisualStyleBackColor = False
        '
        'btnPage3
        '
        Me.btnPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnPage3.FlatAppearance.BorderSize = 0
        Me.btnPage3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPage3.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPage3.Location = New System.Drawing.Point(206, 856)
        Me.btnPage3.Name = "btnPage3"
        Me.btnPage3.Size = New System.Drawing.Size(46, 33)
        Me.btnPage3.TabIndex = 9
        Me.btnPage3.Text = "3"
        Me.btnPage3.UseVisualStyleBackColor = False
        '
        'btnPage2
        '
        Me.btnPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnPage2.FlatAppearance.BorderSize = 0
        Me.btnPage2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPage2.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPage2.Location = New System.Drawing.Point(154, 856)
        Me.btnPage2.Name = "btnPage2"
        Me.btnPage2.Size = New System.Drawing.Size(46, 33)
        Me.btnPage2.TabIndex = 8
        Me.btnPage2.Text = "2"
        Me.btnPage2.UseVisualStyleBackColor = False
        '
        'btnPage1
        '
        Me.btnPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnPage1.FlatAppearance.BorderSize = 0
        Me.btnPage1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPage1.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPage1.Location = New System.Drawing.Point(102, 856)
        Me.btnPage1.Name = "btnPage1"
        Me.btnPage1.Size = New System.Drawing.Size(46, 33)
        Me.btnPage1.TabIndex = 7
        Me.btnPage1.Text = "1"
        Me.btnPage1.UseVisualStyleBackColor = False
        '
        'lblPages
        '
        Me.lblPages.AutoSize = True
        Me.lblPages.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPages.Location = New System.Drawing.Point(21, 860)
        Me.lblPages.Name = "lblPages"
        Me.lblPages.Size = New System.Drawing.Size(75, 22)
        Me.lblPages.TabIndex = 0
        Me.lblPages.Text = "Pages:"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1524, 282)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(151, 32)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(104, 282)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(1414, 32)
        Me.txtSearch.TabIndex = 4
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(21, 288)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(83, 22)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'lblBackupAndRestoreLogs
        '
        Me.lblBackupAndRestoreLogs.AutoSize = True
        Me.lblBackupAndRestoreLogs.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupAndRestoreLogs.Location = New System.Drawing.Point(21, 217)
        Me.lblBackupAndRestoreLogs.Name = "lblBackupAndRestoreLogs"
        Me.lblBackupAndRestoreLogs.Size = New System.Drawing.Size(273, 24)
        Me.lblBackupAndRestoreLogs.TabIndex = 0
        Me.lblBackupAndRestoreLogs.Text = "Backup and Restore Logs"
        '
        'dgvDatabase
        '
        Me.dgvDatabase.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvDatabase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDatabase.Location = New System.Drawing.Point(25, 320)
        Me.dgvDatabase.Name = "dgvDatabase"
        Me.dgvDatabase.Size = New System.Drawing.Size(1650, 528)
        Me.dgvDatabase.TabIndex = 6
        '
        'btnDownloadLogs
        '
        Me.btnDownloadLogs.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnDownloadLogs.FlatAppearance.BorderSize = 0
        Me.btnDownloadLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDownloadLogs.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownloadLogs.Location = New System.Drawing.Point(1343, 110)
        Me.btnDownloadLogs.Name = "btnDownloadLogs"
        Me.btnDownloadLogs.Size = New System.Drawing.Size(189, 33)
        Me.btnDownloadLogs.TabIndex = 3
        Me.btnDownloadLogs.Text = "Download Logs"
        Me.btnDownloadLogs.UseVisualStyleBackColor = False
        '
        'LinePnl2
        '
        Me.LinePnl2.BackColor = System.Drawing.Color.Black
        Me.LinePnl2.Location = New System.Drawing.Point(0, 161)
        Me.LinePnl2.Name = "LinePnl2"
        Me.LinePnl2.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl2.TabIndex = 0
        '
        'btnRestoreDB
        '
        Me.btnRestoreDB.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnRestoreDB.FlatAppearance.BorderSize = 0
        Me.btnRestoreDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestoreDB.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestoreDB.Location = New System.Drawing.Point(721, 110)
        Me.btnRestoreDB.Name = "btnRestoreDB"
        Me.btnRestoreDB.Size = New System.Drawing.Size(189, 33)
        Me.btnRestoreDB.TabIndex = 2
        Me.btnRestoreDB.Text = "Restore Database"
        Me.btnRestoreDB.UseVisualStyleBackColor = False
        '
        'btnBackupDB
        '
        Me.btnBackupDB.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBackupDB.FlatAppearance.BorderSize = 0
        Me.btnBackupDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackupDB.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackupDB.Location = New System.Drawing.Point(104, 110)
        Me.btnBackupDB.Name = "btnBackupDB"
        Me.btnBackupDB.Size = New System.Drawing.Size(189, 33)
        Me.btnBackupDB.TabIndex = 1
        Me.btnBackupDB.Text = "Backup Database"
        Me.btnBackupDB.UseVisualStyleBackColor = False
        '
        'lblDatabaseMaintenance
        '
        Me.lblDatabaseMaintenance.AutoSize = True
        Me.lblDatabaseMaintenance.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatabaseMaintenance.Location = New System.Drawing.Point(19, 39)
        Me.lblDatabaseMaintenance.Name = "lblDatabaseMaintenance"
        Me.lblDatabaseMaintenance.Size = New System.Drawing.Size(307, 32)
        Me.lblDatabaseMaintenance.TabIndex = 0
        Me.lblDatabaseMaintenance.Text = "Database Maintenance"
        '
        'DatabaseBackupMain_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.fillpanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DatabaseBackupMain_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DatabaseBackupMain_Form"
        Me.fillpanel.ResumeLayout(False)
        Me.fillpanel.PerformLayout()
        CType(Me.dgvDatabase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents fillpanel As Panel
    Friend WithEvents lblDatabaseMaintenance As Label
    Friend WithEvents LinePnl2 As Panel
    Friend WithEvents btnDownloadLogs As Button
    Friend WithEvents btnRestoreDB As Button
    Friend WithEvents btnBackupDB As Button
    Friend WithEvents lblBackupAndRestoreLogs As Label
    Friend WithEvents dgvDatabase As DataGridView
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents btnView As Button
    Friend WithEvents btnPageNext As Button
    Friend WithEvents btnPage3 As Button
    Friend WithEvents btnPage2 As Button
    Friend WithEvents btnPage1 As Button
    Friend WithEvents lblPages As Label
End Class
