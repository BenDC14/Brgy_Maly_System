<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportsMain_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportsMain_Form))
        Me.FillPanel = New System.Windows.Forms.Panel()
        Me.btnViewGeneratedReports = New System.Windows.Forms.Button()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.cbDownloadType = New System.Windows.Forms.ComboBox()
        Me.lblDownloadType = New System.Windows.Forms.Label()
        Me.btnNewReportType = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.dtpLatest = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblDateRange = New System.Windows.Forms.Label()
        Me.cbReportSubType = New System.Windows.Forms.ComboBox()
        Me.lblReportSubType = New System.Windows.Forms.Label()
        Me.cbReportType = New System.Windows.Forms.ComboBox()
        Me.lblReportType = New System.Windows.Forms.Label()
        Me.LinePnl = New System.Windows.Forms.Panel()
        Me.lblGenerateReports = New System.Windows.Forms.Label()
        Me.dgvReports = New System.Windows.Forms.DataGridView()
        Me.PrintDocuReports = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialogReports = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDialogReports = New System.Windows.Forms.PrintDialog()
        Me.FillPanel.SuspendLayout()
        CType(Me.dgvReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnViewGeneratedReports)
        Me.FillPanel.Controls.Add(Me.btnGenerate)
        Me.FillPanel.Controls.Add(Me.cbDownloadType)
        Me.FillPanel.Controls.Add(Me.lblDownloadType)
        Me.FillPanel.Controls.Add(Me.btnNewReportType)
        Me.FillPanel.Controls.Add(Me.btnSearch)
        Me.FillPanel.Controls.Add(Me.txtSearch)
        Me.FillPanel.Controls.Add(Me.lblSearch)
        Me.FillPanel.Controls.Add(Me.dtpLatest)
        Me.FillPanel.Controls.Add(Me.lblTo)
        Me.FillPanel.Controls.Add(Me.dtpFrom)
        Me.FillPanel.Controls.Add(Me.lblDateRange)
        Me.FillPanel.Controls.Add(Me.cbReportSubType)
        Me.FillPanel.Controls.Add(Me.lblReportSubType)
        Me.FillPanel.Controls.Add(Me.cbReportType)
        Me.FillPanel.Controls.Add(Me.lblReportType)
        Me.FillPanel.Controls.Add(Me.LinePnl)
        Me.FillPanel.Controls.Add(Me.lblGenerateReports)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'btnViewGeneratedReports
        '
        Me.btnViewGeneratedReports.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnViewGeneratedReports.FlatAppearance.BorderSize = 0
        Me.btnViewGeneratedReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewGeneratedReports.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewGeneratedReports.Location = New System.Drawing.Point(998, 923)
        Me.btnViewGeneratedReports.Name = "btnViewGeneratedReports"
        Me.btnViewGeneratedReports.Size = New System.Drawing.Size(226, 45)
        Me.btnViewGeneratedReports.TabIndex = 11
        Me.btnViewGeneratedReports.Text = "View Generated Reports"
        Me.btnViewGeneratedReports.UseVisualStyleBackColor = False
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnGenerate.FlatAppearance.BorderSize = 0
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.Location = New System.Drawing.Point(474, 923)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(226, 45)
        Me.btnGenerate.TabIndex = 10
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'cbDownloadType
        '
        Me.cbDownloadType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbDownloadType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDownloadType.FormattingEnabled = True
        Me.cbDownloadType.Items.AddRange(New Object() {"Excel", "PDF"})
        Me.cbDownloadType.Location = New System.Drawing.Point(203, 848)
        Me.cbDownloadType.Name = "cbDownloadType"
        Me.cbDownloadType.Size = New System.Drawing.Size(1485, 30)
        Me.cbDownloadType.TabIndex = 9
        '
        'lblDownloadType
        '
        Me.lblDownloadType.AutoSize = True
        Me.lblDownloadType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDownloadType.Location = New System.Drawing.Point(22, 851)
        Me.lblDownloadType.Name = "lblDownloadType"
        Me.lblDownloadType.Size = New System.Drawing.Size(161, 22)
        Me.lblDownloadType.TabIndex = 8
        Me.lblDownloadType.Text = "Download Type:"
        '
        'btnNewReportType
        '
        Me.btnNewReportType.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnNewReportType.FlatAppearance.BorderSize = 0
        Me.btnNewReportType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewReportType.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewReportType.Location = New System.Drawing.Point(1537, 93)
        Me.btnNewReportType.Name = "btnNewReportType"
        Me.btnNewReportType.Size = New System.Drawing.Size(151, 30)
        Me.btnNewReportType.TabIndex = 7
        Me.btnNewReportType.Text = "New Report Type"
        Me.btnNewReportType.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1537, 364)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(151, 29)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(203, 364)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(1328, 29)
        Me.txtSearch.TabIndex = 5
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(22, 367)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(83, 22)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'dtpLatest
        '
        Me.dtpLatest.CalendarFont = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLatest.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dtpLatest.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLatest.Location = New System.Drawing.Point(895, 266)
        Me.dtpLatest.Name = "dtpLatest"
        Me.dtpLatest.Size = New System.Drawing.Size(627, 29)
        Me.dtpLatest.TabIndex = 4
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(846, 271)
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
        Me.dtpFrom.Location = New System.Drawing.Point(203, 266)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(627, 29)
        Me.dtpFrom.TabIndex = 3
        '
        'lblDateRange
        '
        Me.lblDateRange.AutoSize = True
        Me.lblDateRange.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateRange.Location = New System.Drawing.Point(22, 271)
        Me.lblDateRange.Name = "lblDateRange"
        Me.lblDateRange.Size = New System.Drawing.Size(123, 22)
        Me.lblDateRange.TabIndex = 7
        Me.lblDateRange.Text = "Date Range:"
        '
        'cbReportSubType
        '
        Me.cbReportSubType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbReportSubType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReportSubType.FormattingEnabled = True
        Me.cbReportSubType.Items.AddRange(New Object() {"Senior Citizen", "PWD", "Student", "Solo Parent", "Employed", "Unemployed", "OFW", "Out of School Children", "Head", "Inhabitant", "Indigenous People (IP)"})
        Me.cbReportSubType.Location = New System.Drawing.Point(203, 182)
        Me.cbReportSubType.Name = "cbReportSubType"
        Me.cbReportSubType.Size = New System.Drawing.Size(1319, 30)
        Me.cbReportSubType.TabIndex = 2
        '
        'lblReportSubType
        '
        Me.lblReportSubType.AutoSize = True
        Me.lblReportSubType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportSubType.Location = New System.Drawing.Point(22, 185)
        Me.lblReportSubType.Name = "lblReportSubType"
        Me.lblReportSubType.Size = New System.Drawing.Size(172, 22)
        Me.lblReportSubType.TabIndex = 0
        Me.lblReportSubType.Text = "Report Sub Type:"
        '
        'cbReportType
        '
        Me.cbReportType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbReportType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReportType.FormattingEnabled = True
        Me.cbReportType.Items.AddRange(New Object() {"Registry of Barangay Inhabitants (RBI)", "Household List", "Demographic Report", "Special Category Report"})
        Me.cbReportType.Location = New System.Drawing.Point(203, 93)
        Me.cbReportType.Name = "cbReportType"
        Me.cbReportType.Size = New System.Drawing.Size(1319, 30)
        Me.cbReportType.TabIndex = 1
        '
        'lblReportType
        '
        Me.lblReportType.AutoSize = True
        Me.lblReportType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportType.Location = New System.Drawing.Point(22, 96)
        Me.lblReportType.Name = "lblReportType"
        Me.lblReportType.Size = New System.Drawing.Size(130, 22)
        Me.lblReportType.TabIndex = 0
        Me.lblReportType.Text = "Report Type:"
        '
        'LinePnl
        '
        Me.LinePnl.BackColor = System.Drawing.Color.Black
        Me.LinePnl.Location = New System.Drawing.Point(0, 70)
        Me.LinePnl.Name = "LinePnl"
        Me.LinePnl.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl.TabIndex = 0
        '
        'lblGenerateReports
        '
        Me.lblGenerateReports.AutoSize = True
        Me.lblGenerateReports.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGenerateReports.Location = New System.Drawing.Point(20, 20)
        Me.lblGenerateReports.Name = "lblGenerateReports"
        Me.lblGenerateReports.Size = New System.Drawing.Size(244, 32)
        Me.lblGenerateReports.TabIndex = 0
        Me.lblGenerateReports.Text = "Generate Reports"
        '
        'dgvReports
        '
        Me.dgvReports.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReports.Location = New System.Drawing.Point(12, 400)
        Me.dgvReports.Name = "dgvReports"
        Me.dgvReports.Size = New System.Drawing.Size(1676, 442)
        Me.dgvReports.TabIndex = 8
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
        'ReportsMain_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.dgvReports)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ReportsMain_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ReportsMain_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.dgvReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents lblGenerateReports As Label
    Friend WithEvents LinePnl As Panel
    Friend WithEvents cbReportType As ComboBox
    Friend WithEvents lblReportType As Label
    Friend WithEvents cbReportSubType As ComboBox
    Friend WithEvents lblReportSubType As Label
    Friend WithEvents dtpLatest As DateTimePicker
    Friend WithEvents lblTo As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents lblDateRange As Label
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnNewReportType As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents dgvReports As DataGridView
    Friend WithEvents btnViewGeneratedReports As Button
    Friend WithEvents btnGenerate As Button
    Friend WithEvents cbDownloadType As ComboBox
    Friend WithEvents lblDownloadType As Label
    Friend WithEvents PrintDocuReports As Printing.PrintDocument
    Friend WithEvents PrintPreviewDialogReports As PrintPreviewDialog
    Friend WithEvents PrintDialogReports As PrintDialog
End Class
