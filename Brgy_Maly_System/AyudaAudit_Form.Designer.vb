<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AyudaAudit_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AyudaAudit_Form))
        Me.FillPanel = New System.Windows.Forms.Panel()
        Me.btnPrintAudit = New System.Windows.Forms.Button()
        Me.dgvAyudaAuditInformation = New System.Windows.Forms.DataGridView()
        Me.AyudaAuditInformationlbl = New System.Windows.Forms.Label()
        Me.LnPnl2 = New System.Windows.Forms.Panel()
        Me.dgvAyudaInfo = New System.Windows.Forms.DataGridView()
        Me.AyudaInformationLbl = New System.Windows.Forms.Label()
        Me.LinePnl = New System.Windows.Forms.Panel()
        Me.txtPacksReleased = New System.Windows.Forms.TextBox()
        Me.txtCashReleased = New System.Windows.Forms.TextBox()
        Me.txtResidentServed = New System.Windows.Forms.TextBox()
        Me.lblTotalPacksReleased = New System.Windows.Forms.Label()
        Me.lblTotalCashReleased = New System.Windows.Forms.Label()
        Me.lblTotalResidentsServed = New System.Windows.Forms.Label()
        Me.AyudaLbl = New System.Windows.Forms.Label()
        Me.EndDateLbl = New System.Windows.Forms.Label()
        Me.StartDateLbl = New System.Windows.Forms.Label()
        Me.lblAyudaAudit = New System.Windows.Forms.Label()
        Me.cbAyuda = New System.Windows.Forms.ComboBox()
        Me.endDTP = New System.Windows.Forms.DateTimePicker()
        Me.startDTP = New System.Windows.Forms.DateTimePicker()
        Me.PrintDialogReports = New System.Windows.Forms.PrintDialog()
        Me.PrintPreviewDialogReports = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDocuReports = New System.Drawing.Printing.PrintDocument()
        Me.FillPanel.SuspendLayout()
        CType(Me.dgvAyudaAuditInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAyudaInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.Controls.Add(Me.btnPrintAudit)
        Me.FillPanel.Controls.Add(Me.dgvAyudaAuditInformation)
        Me.FillPanel.Controls.Add(Me.AyudaAuditInformationlbl)
        Me.FillPanel.Controls.Add(Me.LnPnl2)
        Me.FillPanel.Controls.Add(Me.dgvAyudaInfo)
        Me.FillPanel.Controls.Add(Me.AyudaInformationLbl)
        Me.FillPanel.Controls.Add(Me.LinePnl)
        Me.FillPanel.Controls.Add(Me.txtPacksReleased)
        Me.FillPanel.Controls.Add(Me.txtCashReleased)
        Me.FillPanel.Controls.Add(Me.txtResidentServed)
        Me.FillPanel.Controls.Add(Me.lblTotalPacksReleased)
        Me.FillPanel.Controls.Add(Me.lblTotalCashReleased)
        Me.FillPanel.Controls.Add(Me.lblTotalResidentsServed)
        Me.FillPanel.Controls.Add(Me.AyudaLbl)
        Me.FillPanel.Controls.Add(Me.EndDateLbl)
        Me.FillPanel.Controls.Add(Me.StartDateLbl)
        Me.FillPanel.Controls.Add(Me.lblAyudaAudit)
        Me.FillPanel.Controls.Add(Me.cbAyuda)
        Me.FillPanel.Controls.Add(Me.endDTP)
        Me.FillPanel.Controls.Add(Me.startDTP)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'btnPrintAudit
        '
        Me.btnPrintAudit.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnPrintAudit.FlatAppearance.BorderSize = 0
        Me.btnPrintAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintAudit.Font = New System.Drawing.Font("Arial Narrow", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintAudit.Location = New System.Drawing.Point(761, 904)
        Me.btnPrintAudit.Name = "btnPrintAudit"
        Me.btnPrintAudit.Size = New System.Drawing.Size(163, 46)
        Me.btnPrintAudit.TabIndex = 10
        Me.btnPrintAudit.Text = "Print Audit"
        Me.btnPrintAudit.UseVisualStyleBackColor = False
        '
        'dgvAyudaAuditInformation
        '
        Me.dgvAyudaAuditInformation.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvAyudaAuditInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAyudaAuditInformation.Location = New System.Drawing.Point(18, 666)
        Me.dgvAyudaAuditInformation.Name = "dgvAyudaAuditInformation"
        Me.dgvAyudaAuditInformation.Size = New System.Drawing.Size(1670, 198)
        Me.dgvAyudaAuditInformation.TabIndex = 8
        '
        'AyudaAuditInformationlbl
        '
        Me.AyudaAuditInformationlbl.AutoSize = True
        Me.AyudaAuditInformationlbl.BackColor = System.Drawing.Color.Transparent
        Me.AyudaAuditInformationlbl.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaAuditInformationlbl.Location = New System.Drawing.Point(13, 634)
        Me.AyudaAuditInformationlbl.Name = "AyudaAuditInformationlbl"
        Me.AyudaAuditInformationlbl.Size = New System.Drawing.Size(293, 29)
        Me.AyudaAuditInformationlbl.TabIndex = 0
        Me.AyudaAuditInformationlbl.Text = "Ayuda Audit Information"
        '
        'LnPnl2
        '
        Me.LnPnl2.BackColor = System.Drawing.Color.Black
        Me.LnPnl2.Location = New System.Drawing.Point(0, 609)
        Me.LnPnl2.Name = "LnPnl2"
        Me.LnPnl2.Size = New System.Drawing.Size(1700, 2)
        Me.LnPnl2.TabIndex = 0
        '
        'dgvAyudaInfo
        '
        Me.dgvAyudaInfo.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvAyudaInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAyudaInfo.Location = New System.Drawing.Point(18, 366)
        Me.dgvAyudaInfo.Name = "dgvAyudaInfo"
        Me.dgvAyudaInfo.Size = New System.Drawing.Size(1670, 198)
        Me.dgvAyudaInfo.TabIndex = 7
        '
        'AyudaInformationLbl
        '
        Me.AyudaInformationLbl.AutoSize = True
        Me.AyudaInformationLbl.BackColor = System.Drawing.Color.Transparent
        Me.AyudaInformationLbl.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaInformationLbl.Location = New System.Drawing.Point(13, 334)
        Me.AyudaInformationLbl.Name = "AyudaInformationLbl"
        Me.AyudaInformationLbl.Size = New System.Drawing.Size(225, 29)
        Me.AyudaInformationLbl.TabIndex = 0
        Me.AyudaInformationLbl.Text = "Ayuda Information"
        '
        'LinePnl
        '
        Me.LinePnl.BackColor = System.Drawing.Color.Black
        Me.LinePnl.Location = New System.Drawing.Point(0, 311)
        Me.LinePnl.Name = "LinePnl"
        Me.LinePnl.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl.TabIndex = 0
        '
        'txtPacksReleased
        '
        Me.txtPacksReleased.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPacksReleased.Location = New System.Drawing.Point(1089, 244)
        Me.txtPacksReleased.Name = "txtPacksReleased"
        Me.txtPacksReleased.ReadOnly = True
        Me.txtPacksReleased.Size = New System.Drawing.Size(454, 29)
        Me.txtPacksReleased.TabIndex = 6
        '
        'txtCashReleased
        '
        Me.txtCashReleased.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashReleased.Location = New System.Drawing.Point(614, 244)
        Me.txtCashReleased.Name = "txtCashReleased"
        Me.txtCashReleased.ReadOnly = True
        Me.txtCashReleased.Size = New System.Drawing.Size(323, 29)
        Me.txtCashReleased.TabIndex = 5
        '
        'txtResidentServed
        '
        Me.txtResidentServed.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResidentServed.Location = New System.Drawing.Point(152, 244)
        Me.txtResidentServed.Name = "txtResidentServed"
        Me.txtResidentServed.ReadOnly = True
        Me.txtResidentServed.Size = New System.Drawing.Size(323, 29)
        Me.txtResidentServed.TabIndex = 4
        '
        'lblTotalPacksReleased
        '
        Me.lblTotalPacksReleased.AutoSize = True
        Me.lblTotalPacksReleased.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalPacksReleased.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPacksReleased.Location = New System.Drawing.Point(1085, 196)
        Me.lblTotalPacksReleased.Name = "lblTotalPacksReleased"
        Me.lblTotalPacksReleased.Size = New System.Drawing.Size(232, 24)
        Me.lblTotalPacksReleased.TabIndex = 0
        Me.lblTotalPacksReleased.Text = "Total Packs Released:"
        '
        'lblTotalCashReleased
        '
        Me.lblTotalCashReleased.AutoSize = True
        Me.lblTotalCashReleased.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalCashReleased.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCashReleased.Location = New System.Drawing.Point(610, 196)
        Me.lblTotalCashReleased.Name = "lblTotalCashReleased"
        Me.lblTotalCashReleased.Size = New System.Drawing.Size(222, 24)
        Me.lblTotalCashReleased.TabIndex = 0
        Me.lblTotalCashReleased.Text = "Total Cash Released:"
        '
        'lblTotalResidentsServed
        '
        Me.lblTotalResidentsServed.AutoSize = True
        Me.lblTotalResidentsServed.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalResidentsServed.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalResidentsServed.Location = New System.Drawing.Point(148, 196)
        Me.lblTotalResidentsServed.Name = "lblTotalResidentsServed"
        Me.lblTotalResidentsServed.Size = New System.Drawing.Size(250, 24)
        Me.lblTotalResidentsServed.TabIndex = 0
        Me.lblTotalResidentsServed.Text = "Total Residents Served:"
        '
        'AyudaLbl
        '
        Me.AyudaLbl.AutoSize = True
        Me.AyudaLbl.BackColor = System.Drawing.Color.Transparent
        Me.AyudaLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaLbl.Location = New System.Drawing.Point(1084, 89)
        Me.AyudaLbl.Name = "AyudaLbl"
        Me.AyudaLbl.Size = New System.Drawing.Size(66, 25)
        Me.AyudaLbl.TabIndex = 0
        Me.AyudaLbl.Text = "Ayuda"
        '
        'EndDateLbl
        '
        Me.EndDateLbl.AutoSize = True
        Me.EndDateLbl.BackColor = System.Drawing.Color.Transparent
        Me.EndDateLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EndDateLbl.Location = New System.Drawing.Point(609, 87)
        Me.EndDateLbl.Name = "EndDateLbl"
        Me.EndDateLbl.Size = New System.Drawing.Size(40, 25)
        Me.EndDateLbl.TabIndex = 0
        Me.EndDateLbl.Text = "To:"
        '
        'StartDateLbl
        '
        Me.StartDateLbl.AutoSize = True
        Me.StartDateLbl.BackColor = System.Drawing.Color.Transparent
        Me.StartDateLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StartDateLbl.Location = New System.Drawing.Point(147, 87)
        Me.StartDateLbl.Name = "StartDateLbl"
        Me.StartDateLbl.Size = New System.Drawing.Size(62, 25)
        Me.StartDateLbl.TabIndex = 0
        Me.StartDateLbl.Text = "From:"
        '
        'lblAyudaAudit
        '
        Me.lblAyudaAudit.AutoSize = True
        Me.lblAyudaAudit.BackColor = System.Drawing.Color.Transparent
        Me.lblAyudaAudit.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAyudaAudit.Location = New System.Drawing.Point(12, 9)
        Me.lblAyudaAudit.Name = "lblAyudaAudit"
        Me.lblAyudaAudit.Size = New System.Drawing.Size(218, 32)
        Me.lblAyudaAudit.TabIndex = 0
        Me.lblAyudaAudit.Text = "Audit for Ayuda"
        '
        'cbAyuda
        '
        Me.cbAyuda.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbAyuda.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAyuda.FormattingEnabled = True
        Me.cbAyuda.Location = New System.Drawing.Point(1089, 117)
        Me.cbAyuda.Name = "cbAyuda"
        Me.cbAyuda.Size = New System.Drawing.Size(454, 30)
        Me.cbAyuda.TabIndex = 3
        '
        'endDTP
        '
        Me.endDTP.CalendarFont = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endDTP.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endDTP.Location = New System.Drawing.Point(614, 115)
        Me.endDTP.Name = "endDTP"
        Me.endDTP.Size = New System.Drawing.Size(323, 29)
        Me.endDTP.TabIndex = 2
        '
        'startDTP
        '
        Me.startDTP.CalendarFont = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startDTP.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startDTP.Location = New System.Drawing.Point(152, 115)
        Me.startDTP.Name = "startDTP"
        Me.startDTP.Size = New System.Drawing.Size(323, 29)
        Me.startDTP.TabIndex = 1
        '
        'PrintDialogReports
        '
        Me.PrintDialogReports.UseEXDialog = True
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
        'AyudaAudit_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AyudaAudit_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AyudaAudit_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.dgvAyudaAuditInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAyudaInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents endDTP As DateTimePicker
    Friend WithEvents startDTP As DateTimePicker
    Friend WithEvents cbAyuda As ComboBox
    Friend WithEvents lblAyudaAudit As Label
    Friend WithEvents AyudaLbl As Label
    Friend WithEvents EndDateLbl As Label
    Friend WithEvents StartDateLbl As Label
    Friend WithEvents lblTotalPacksReleased As Label
    Friend WithEvents lblTotalCashReleased As Label
    Friend WithEvents lblTotalResidentsServed As Label
    Friend WithEvents txtPacksReleased As TextBox
    Friend WithEvents txtCashReleased As TextBox
    Friend WithEvents txtResidentServed As TextBox
    Friend WithEvents LinePnl As Panel
    Friend WithEvents dgvAyudaAuditInformation As DataGridView
    Friend WithEvents AyudaAuditInformationlbl As Label
    Friend WithEvents LnPnl2 As Panel
    Friend WithEvents dgvAyudaInfo As DataGridView
    Friend WithEvents AyudaInformationLbl As Label
    Friend WithEvents btnPrintAudit As Button
    Friend WithEvents PrintDialogReports As PrintDialog
    Friend WithEvents PrintPreviewDialogReports As PrintPreviewDialog
    Friend WithEvents PrintDocuReports As Printing.PrintDocument
End Class
