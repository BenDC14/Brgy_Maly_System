<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Audit_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Audit_Form))
        Me.fillpanel = New System.Windows.Forms.Panel()
        Me.btnExportCSV = New System.Windows.Forms.Button()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.btnExportPdf = New System.Windows.Forms.Button()
        Me.btnPageNext = New System.Windows.Forms.Button()
        Me.btnPage3 = New System.Windows.Forms.Button()
        Me.btnPage2 = New System.Windows.Forms.Button()
        Me.btnPage1 = New System.Windows.Forms.Button()
        Me.lblPages = New System.Windows.Forms.Label()
        Me.dgvAudit = New System.Windows.Forms.DataGridView()
        Me.LinePnl2 = New System.Windows.Forms.Panel()
        Me.btnRemoveFilter = New System.Windows.Forms.Button()
        Me.dtpLatest = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblDateRange = New System.Windows.Forms.Label()
        Me.cbForms = New System.Windows.Forms.ComboBox()
        Me.lblForms = New System.Windows.Forms.Label()
        Me.cbActionType = New System.Windows.Forms.ComboBox()
        Me.lblActionType = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.LinePnl = New System.Windows.Forms.Panel()
        Me.lblAuditLogs = New System.Windows.Forms.Label()
        Me.PrintDocuReports = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialogReports = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDialogReports = New System.Windows.Forms.PrintDialog()
        Me.fillpanel.SuspendLayout()
        CType(Me.dgvAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fillpanel
        '
        Me.fillpanel.BackColor = System.Drawing.Color.Transparent
        Me.fillpanel.Controls.Add(Me.btnExportCSV)
        Me.fillpanel.Controls.Add(Me.btnExportExcel)
        Me.fillpanel.Controls.Add(Me.btnExportPdf)
        Me.fillpanel.Controls.Add(Me.btnPageNext)
        Me.fillpanel.Controls.Add(Me.btnPage3)
        Me.fillpanel.Controls.Add(Me.btnPage2)
        Me.fillpanel.Controls.Add(Me.btnPage1)
        Me.fillpanel.Controls.Add(Me.lblPages)
        Me.fillpanel.Controls.Add(Me.dgvAudit)
        Me.fillpanel.Controls.Add(Me.LinePnl2)
        Me.fillpanel.Controls.Add(Me.btnRemoveFilter)
        Me.fillpanel.Controls.Add(Me.dtpLatest)
        Me.fillpanel.Controls.Add(Me.lblTo)
        Me.fillpanel.Controls.Add(Me.dtpFrom)
        Me.fillpanel.Controls.Add(Me.lblDateRange)
        Me.fillpanel.Controls.Add(Me.cbForms)
        Me.fillpanel.Controls.Add(Me.lblForms)
        Me.fillpanel.Controls.Add(Me.cbActionType)
        Me.fillpanel.Controls.Add(Me.lblActionType)
        Me.fillpanel.Controls.Add(Me.btnSearch)
        Me.fillpanel.Controls.Add(Me.txtSearch)
        Me.fillpanel.Controls.Add(Me.lblSearch)
        Me.fillpanel.Controls.Add(Me.LinePnl)
        Me.fillpanel.Controls.Add(Me.lblAuditLogs)
        Me.fillpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fillpanel.Location = New System.Drawing.Point(0, 0)
        Me.fillpanel.Name = "fillpanel"
        Me.fillpanel.Size = New System.Drawing.Size(1700, 1004)
        Me.fillpanel.TabIndex = 0
        '
        'btnExportCSV
        '
        Me.btnExportCSV.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnExportCSV.FlatAppearance.BorderSize = 0
        Me.btnExportCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportCSV.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportCSV.Location = New System.Drawing.Point(1363, 933)
        Me.btnExportCSV.Name = "btnExportCSV"
        Me.btnExportCSV.Size = New System.Drawing.Size(189, 33)
        Me.btnExportCSV.TabIndex = 15
        Me.btnExportCSV.Text = "Export CSV"
        Me.btnExportCSV.UseVisualStyleBackColor = False
        '
        'btnExportExcel
        '
        Me.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnExportExcel.FlatAppearance.BorderSize = 0
        Me.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportExcel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportExcel.Location = New System.Drawing.Point(741, 933)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(189, 33)
        Me.btnExportExcel.TabIndex = 14
        Me.btnExportExcel.Text = "Export Excel"
        Me.btnExportExcel.UseVisualStyleBackColor = False
        '
        'btnExportPdf
        '
        Me.btnExportPdf.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnExportPdf.FlatAppearance.BorderSize = 0
        Me.btnExportPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportPdf.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportPdf.Location = New System.Drawing.Point(124, 933)
        Me.btnExportPdf.Name = "btnExportPdf"
        Me.btnExportPdf.Size = New System.Drawing.Size(189, 33)
        Me.btnExportPdf.TabIndex = 13
        Me.btnExportPdf.Text = "Export PDF"
        Me.btnExportPdf.UseVisualStyleBackColor = False
        '
        'btnPageNext
        '
        Me.btnPageNext.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnPageNext.FlatAppearance.BorderSize = 0
        Me.btnPageNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPageNext.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPageNext.Location = New System.Drawing.Point(262, 868)
        Me.btnPageNext.Name = "btnPageNext"
        Me.btnPageNext.Size = New System.Drawing.Size(77, 33)
        Me.btnPageNext.TabIndex = 12
        Me.btnPageNext.Text = "Next"
        Me.btnPageNext.UseVisualStyleBackColor = False
        '
        'btnPage3
        '
        Me.btnPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnPage3.FlatAppearance.BorderSize = 0
        Me.btnPage3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPage3.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPage3.Location = New System.Drawing.Point(207, 868)
        Me.btnPage3.Name = "btnPage3"
        Me.btnPage3.Size = New System.Drawing.Size(46, 33)
        Me.btnPage3.TabIndex = 11
        Me.btnPage3.Text = "3"
        Me.btnPage3.UseVisualStyleBackColor = False
        '
        'btnPage2
        '
        Me.btnPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnPage2.FlatAppearance.BorderSize = 0
        Me.btnPage2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPage2.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPage2.Location = New System.Drawing.Point(155, 868)
        Me.btnPage2.Name = "btnPage2"
        Me.btnPage2.Size = New System.Drawing.Size(46, 33)
        Me.btnPage2.TabIndex = 10
        Me.btnPage2.Text = "2"
        Me.btnPage2.UseVisualStyleBackColor = False
        '
        'btnPage1
        '
        Me.btnPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnPage1.FlatAppearance.BorderSize = 0
        Me.btnPage1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPage1.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPage1.Location = New System.Drawing.Point(103, 868)
        Me.btnPage1.Name = "btnPage1"
        Me.btnPage1.Size = New System.Drawing.Size(46, 33)
        Me.btnPage1.TabIndex = 9
        Me.btnPage1.Text = "1"
        Me.btnPage1.UseVisualStyleBackColor = False
        '
        'lblPages
        '
        Me.lblPages.AutoSize = True
        Me.lblPages.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPages.Location = New System.Drawing.Point(22, 872)
        Me.lblPages.Name = "lblPages"
        Me.lblPages.Size = New System.Drawing.Size(75, 22)
        Me.lblPages.TabIndex = 0
        Me.lblPages.Text = "Pages:"
        '
        'dgvAudit
        '
        Me.dgvAudit.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAudit.Location = New System.Drawing.Point(26, 337)
        Me.dgvAudit.Name = "dgvAudit"
        Me.dgvAudit.Size = New System.Drawing.Size(1650, 528)
        Me.dgvAudit.TabIndex = 8
        '
        'LinePnl2
        '
        Me.LinePnl2.BackColor = System.Drawing.Color.Black
        Me.LinePnl2.Location = New System.Drawing.Point(0, 315)
        Me.LinePnl2.Name = "LinePnl2"
        Me.LinePnl2.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl2.TabIndex = 0
        '
        'btnRemoveFilter
        '
        Me.btnRemoveFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnRemoveFilter.FlatAppearance.BorderSize = 0
        Me.btnRemoveFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveFilter.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveFilter.Location = New System.Drawing.Point(941, 244)
        Me.btnRemoveFilter.Name = "btnRemoveFilter"
        Me.btnRemoveFilter.Size = New System.Drawing.Size(476, 32)
        Me.btnRemoveFilter.TabIndex = 7
        Me.btnRemoveFilter.Text = "Remove Filter"
        Me.btnRemoveFilter.UseVisualStyleBackColor = False
        '
        'dtpLatest
        '
        Me.dtpLatest.CalendarFont = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLatest.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dtpLatest.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLatest.Location = New System.Drawing.Point(1218, 178)
        Me.dtpLatest.Name = "dtpLatest"
        Me.dtpLatest.Size = New System.Drawing.Size(306, 29)
        Me.dtpLatest.TabIndex = 5
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(1178, 181)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(34, 22)
        Me.lblTo.TabIndex = 0
        Me.lblTo.Text = "To"
        '
        'dtpFrom
        '
        Me.dtpFrom.CalendarFont = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dtpFrom.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Location = New System.Drawing.Point(866, 178)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(306, 29)
        Me.dtpFrom.TabIndex = 4
        '
        'lblDateRange
        '
        Me.lblDateRange.AutoSize = True
        Me.lblDateRange.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateRange.Location = New System.Drawing.Point(737, 182)
        Me.lblDateRange.Name = "lblDateRange"
        Me.lblDateRange.Size = New System.Drawing.Size(123, 22)
        Me.lblDateRange.TabIndex = 0
        Me.lblDateRange.Text = "Date Range:"
        '
        'cbForms
        '
        Me.cbForms.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbForms.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbForms.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbForms.FormattingEnabled = True
        Me.cbForms.Items.AddRange(New Object() {"Residents Form", "Household Form", "Family Form", "Ayuda Form", "Reports Form", "Barangay Information Form", "Audit Form", "Accounts Form"})
        Me.cbForms.Location = New System.Drawing.Point(240, 245)
        Me.cbForms.Name = "cbForms"
        Me.cbForms.Size = New System.Drawing.Size(449, 31)
        Me.cbForms.TabIndex = 6
        '
        'lblForms
        '
        Me.lblForms.AutoSize = True
        Me.lblForms.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForms.Location = New System.Drawing.Point(109, 249)
        Me.lblForms.Name = "lblForms"
        Me.lblForms.Size = New System.Drawing.Size(77, 22)
        Me.lblForms.TabIndex = 0
        Me.lblForms.Text = "Forms:"
        '
        'cbActionType
        '
        Me.cbActionType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbActionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbActionType.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbActionType.FormattingEnabled = True
        Me.cbActionType.Items.AddRange(New Object() {"Create", "Retrieve", "Update", "Archieve"})
        Me.cbActionType.Location = New System.Drawing.Point(240, 178)
        Me.cbActionType.Name = "cbActionType"
        Me.cbActionType.Size = New System.Drawing.Size(449, 31)
        Me.cbActionType.TabIndex = 3
        '
        'lblActionType
        '
        Me.lblActionType.AutoSize = True
        Me.lblActionType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActionType.Location = New System.Drawing.Point(103, 181)
        Me.lblActionType.Name = "lblActionType"
        Me.lblActionType.Size = New System.Drawing.Size(127, 22)
        Me.lblActionType.TabIndex = 0
        Me.lblActionType.Text = "Action Type:"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1423, 106)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(151, 32)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(240, 106)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(1177, 32)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(103, 110)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(83, 22)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'LinePnl
        '
        Me.LinePnl.BackColor = System.Drawing.Color.Black
        Me.LinePnl.Location = New System.Drawing.Point(0, 70)
        Me.LinePnl.Name = "LinePnl"
        Me.LinePnl.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl.TabIndex = 0
        '
        'lblAuditLogs
        '
        Me.lblAuditLogs.AutoSize = True
        Me.lblAuditLogs.Font = New System.Drawing.Font("Arial", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuditLogs.Location = New System.Drawing.Point(20, 19)
        Me.lblAuditLogs.Name = "lblAuditLogs"
        Me.lblAuditLogs.Size = New System.Drawing.Size(166, 34)
        Me.lblAuditLogs.TabIndex = 0
        Me.lblAuditLogs.Text = "Audit Logs"
        '
        'PrintPreviewDialogReports
        '
        Me.PrintPreviewDialogReports.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialogReports.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialogReports.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialogReports.Enabled = True
        Me.PrintPreviewDialogReports.Icon = CType(resources.GetObject("PrintPreviewDialogReports.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialogReports.Name = "PrintPreviewDialogReports"
        Me.PrintPreviewDialogReports.Visible = False
        '
        'PrintDialogReports
        '
        Me.PrintDialogReports.UseEXDialog = True
        '
        'Audit_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.fillpanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Audit_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Audit_Form"
        Me.fillpanel.ResumeLayout(False)
        Me.fillpanel.PerformLayout()
        CType(Me.dgvAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents fillpanel As Panel
    Friend WithEvents lblAuditLogs As Label
    Friend WithEvents lblSearch As Label
    Friend WithEvents LinePnl As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents cbActionType As ComboBox
    Friend WithEvents lblActionType As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents cbForms As ComboBox
    Friend WithEvents lblForms As Label
    Friend WithEvents dtpLatest As DateTimePicker
    Friend WithEvents lblTo As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents lblDateRange As Label
    Friend WithEvents LinePnl2 As Panel
    Friend WithEvents btnRemoveFilter As Button
    Friend WithEvents dgvAudit As DataGridView
    Friend WithEvents btnPage2 As Button
    Friend WithEvents btnPage1 As Button
    Friend WithEvents lblPages As Label
    Friend WithEvents btnExportPdf As Button
    Friend WithEvents btnPageNext As Button
    Friend WithEvents btnPage3 As Button
    Friend WithEvents btnExportCSV As Button
    Friend WithEvents btnExportExcel As Button
    Friend WithEvents PrintDocuReports As Printing.PrintDocument
    Friend WithEvents PrintPreviewDialogReports As PrintPreviewDialog
    Friend WithEvents PrintDialogReports As PrintDialog
End Class
