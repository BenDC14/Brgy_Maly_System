<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AyudaMain_Form
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
        Me.dgvResidentAyudas = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.btnRecordNewAyuda = New System.Windows.Forms.Button()
        Me.AyudaInfolbl = New System.Windows.Forms.Label()
        Me.btnAudit = New System.Windows.Forms.Button()
        Me.FillPanel.SuspendLayout()
        CType(Me.dgvResidentAyudas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnAudit)
        Me.FillPanel.Controls.Add(Me.dgvResidentAyudas)
        Me.FillPanel.Controls.Add(Me.btnSearch)
        Me.FillPanel.Controls.Add(Me.txtSearch)
        Me.FillPanel.Controls.Add(Me.lblSearch)
        Me.FillPanel.Controls.Add(Me.btnRecordNewAyuda)
        Me.FillPanel.Controls.Add(Me.AyudaInfolbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'dgvResidentAyudas
        '
        Me.dgvResidentAyudas.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvResidentAyudas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResidentAyudas.Location = New System.Drawing.Point(12, 139)
        Me.dgvResidentAyudas.Name = "dgvResidentAyudas"
        Me.dgvResidentAyudas.Size = New System.Drawing.Size(1676, 830)
        Me.dgvResidentAyudas.TabIndex = 5
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1537, 96)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(151, 29)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(1241, 96)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(290, 29)
        Me.txtSearch.TabIndex = 3
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1152, 100)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(83, 22)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'btnRecordNewAyuda
        '
        Me.btnRecordNewAyuda.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnRecordNewAyuda.FlatAppearance.BorderSize = 0
        Me.btnRecordNewAyuda.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRecordNewAyuda.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRecordNewAyuda.Location = New System.Drawing.Point(12, 88)
        Me.btnRecordNewAyuda.Name = "btnRecordNewAyuda"
        Me.btnRecordNewAyuda.Size = New System.Drawing.Size(226, 45)
        Me.btnRecordNewAyuda.TabIndex = 1
        Me.btnRecordNewAyuda.Text = "Record New Ayuda"
        Me.btnRecordNewAyuda.UseVisualStyleBackColor = False
        '
        'AyudaInfolbl
        '
        Me.AyudaInfolbl.AutoSize = True
        Me.AyudaInfolbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaInfolbl.Location = New System.Drawing.Point(20, 20)
        Me.AyudaInfolbl.Name = "AyudaInfolbl"
        Me.AyudaInfolbl.Size = New System.Drawing.Size(258, 32)
        Me.AyudaInfolbl.TabIndex = 0
        Me.AyudaInfolbl.Text = "Ayuda Information"
        '
        'btnAudit
        '
        Me.btnAudit.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnAudit.FlatAppearance.BorderSize = 0
        Me.btnAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAudit.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAudit.Location = New System.Drawing.Point(279, 88)
        Me.btnAudit.Name = "btnAudit"
        Me.btnAudit.Size = New System.Drawing.Size(226, 45)
        Me.btnAudit.TabIndex = 2
        Me.btnAudit.Text = "Audit Ayuda"
        Me.btnAudit.UseVisualStyleBackColor = False
        '
        'AyudaMain_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AyudaMain_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AyudaMain_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.dgvResidentAyudas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents AyudaInfolbl As Label
    Friend WithEvents btnRecordNewAyuda As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents dgvResidentAyudas As DataGridView
    Friend WithEvents btnAudit As Button
End Class
