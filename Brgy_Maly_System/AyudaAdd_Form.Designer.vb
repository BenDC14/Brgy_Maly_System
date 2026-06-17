<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AyudaAdd_Form
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
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.lblAvailOrActive = New System.Windows.Forms.Label()
        Me.rbIsActiveNo = New System.Windows.Forms.RadioButton()
        Me.rbIsActiveYes = New System.Windows.Forms.RadioButton()
        Me.AyudaEndDTP = New System.Windows.Forms.DateTimePicker()
        Me.AyudaEndLbl = New System.Windows.Forms.Label()
        Me.AyudaStartDTP = New System.Windows.Forms.DateTimePicker()
        Me.AyudaStartLbl = New System.Windows.Forms.Label()
        Me.txtTargetSlots = New System.Windows.Forms.TextBox()
        Me.lblTargetSlots = New System.Windows.Forms.Label()
        Me.txtAssistanceProvision = New System.Windows.Forms.TextBox()
        Me.lblAssistanceProvision = New System.Windows.Forms.Label()
        Me.lblAssistanceType = New System.Windows.Forms.Label()
        Me.txtOthers = New System.Windows.Forms.TextBox()
        Me.cbAssistanceType = New System.Windows.Forms.ComboBox()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.lblProgramName = New System.Windows.Forms.Label()
        Me.txtResidentType = New System.Windows.Forms.TextBox()
        Me.lblAyudaGiver = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.dgvAyudas = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lblNewAyuda = New System.Windows.Forms.Label()
        Me.lblCategories = New System.Windows.Forms.Label()
        Me.cbcategories = New System.Windows.Forms.ComboBox()
        Me.FillPanel.SuspendLayout()
        CType(Me.dgvAyudas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.lblCategories)
        Me.FillPanel.Controls.Add(Me.cbcategories)
        Me.FillPanel.Controls.Add(Me.btnEdit)
        Me.FillPanel.Controls.Add(Me.lblAvailOrActive)
        Me.FillPanel.Controls.Add(Me.rbIsActiveNo)
        Me.FillPanel.Controls.Add(Me.rbIsActiveYes)
        Me.FillPanel.Controls.Add(Me.AyudaEndDTP)
        Me.FillPanel.Controls.Add(Me.AyudaEndLbl)
        Me.FillPanel.Controls.Add(Me.AyudaStartDTP)
        Me.FillPanel.Controls.Add(Me.AyudaStartLbl)
        Me.FillPanel.Controls.Add(Me.txtTargetSlots)
        Me.FillPanel.Controls.Add(Me.lblTargetSlots)
        Me.FillPanel.Controls.Add(Me.txtAssistanceProvision)
        Me.FillPanel.Controls.Add(Me.lblAssistanceProvision)
        Me.FillPanel.Controls.Add(Me.lblAssistanceType)
        Me.FillPanel.Controls.Add(Me.txtOthers)
        Me.FillPanel.Controls.Add(Me.cbAssistanceType)
        Me.FillPanel.Controls.Add(Me.txtQuantity)
        Me.FillPanel.Controls.Add(Me.lblProgramName)
        Me.FillPanel.Controls.Add(Me.txtResidentType)
        Me.FillPanel.Controls.Add(Me.lblAyudaGiver)
        Me.FillPanel.Controls.Add(Me.txtSearch)
        Me.FillPanel.Controls.Add(Me.lblSearch)
        Me.FillPanel.Controls.Add(Me.dgvAyudas)
        Me.FillPanel.Controls.Add(Me.Label1)
        Me.FillPanel.Controls.Add(Me.btnSave)
        Me.FillPanel.Controls.Add(Me.lblNewAyuda)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnEdit.FlatAppearance.BorderSize = 0
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.Location = New System.Drawing.Point(884, 899)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(245, 46)
        Me.btnEdit.TabIndex = 15
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'lblAvailOrActive
        '
        Me.lblAvailOrActive.AutoSize = True
        Me.lblAvailOrActive.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvailOrActive.Location = New System.Drawing.Point(1065, 720)
        Me.lblAvailOrActive.Name = "lblAvailOrActive"
        Me.lblAvailOrActive.Size = New System.Drawing.Size(158, 25)
        Me.lblAvailOrActive.TabIndex = 0
        Me.lblAvailOrActive.Text = "Available / Active"
        '
        'rbIsActiveNo
        '
        Me.rbIsActiveNo.AutoSize = True
        Me.rbIsActiveNo.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbIsActiveNo.Location = New System.Drawing.Point(1295, 748)
        Me.rbIsActiveNo.Name = "rbIsActiveNo"
        Me.rbIsActiveNo.Size = New System.Drawing.Size(52, 26)
        Me.rbIsActiveNo.TabIndex = 12
        Me.rbIsActiveNo.TabStop = True
        Me.rbIsActiveNo.Text = "No"
        Me.rbIsActiveNo.UseVisualStyleBackColor = True
        '
        'rbIsActiveYes
        '
        Me.rbIsActiveYes.AutoSize = True
        Me.rbIsActiveYes.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbIsActiveYes.Location = New System.Drawing.Point(1070, 748)
        Me.rbIsActiveYes.Name = "rbIsActiveYes"
        Me.rbIsActiveYes.Size = New System.Drawing.Size(59, 26)
        Me.rbIsActiveYes.TabIndex = 11
        Me.rbIsActiveYes.TabStop = True
        Me.rbIsActiveYes.Text = "Yes"
        Me.rbIsActiveYes.UseVisualStyleBackColor = True
        '
        'AyudaEndDTP
        '
        Me.AyudaEndDTP.CalendarFont = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaEndDTP.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaEndDTP.Location = New System.Drawing.Point(1070, 652)
        Me.AyudaEndDTP.Name = "AyudaEndDTP"
        Me.AyudaEndDTP.Size = New System.Drawing.Size(618, 29)
        Me.AyudaEndDTP.TabIndex = 10
        Me.AyudaEndDTP.Value = New Date(2026, 1, 24, 16, 53, 38, 0)
        '
        'AyudaEndLbl
        '
        Me.AyudaEndLbl.AutoSize = True
        Me.AyudaEndLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaEndLbl.Location = New System.Drawing.Point(1065, 624)
        Me.AyudaEndLbl.Name = "AyudaEndLbl"
        Me.AyudaEndLbl.Size = New System.Drawing.Size(99, 25)
        Me.AyudaEndLbl.TabIndex = 0
        Me.AyudaEndLbl.Text = "Term End:"
        '
        'AyudaStartDTP
        '
        Me.AyudaStartDTP.CalendarFont = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaStartDTP.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaStartDTP.Location = New System.Drawing.Point(1070, 558)
        Me.AyudaStartDTP.Name = "AyudaStartDTP"
        Me.AyudaStartDTP.Size = New System.Drawing.Size(618, 29)
        Me.AyudaStartDTP.TabIndex = 9
        Me.AyudaStartDTP.Value = New Date(2026, 1, 24, 16, 53, 38, 0)
        '
        'AyudaStartLbl
        '
        Me.AyudaStartLbl.AutoSize = True
        Me.AyudaStartLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaStartLbl.Location = New System.Drawing.Point(1065, 530)
        Me.AyudaStartLbl.Name = "AyudaStartLbl"
        Me.AyudaStartLbl.Size = New System.Drawing.Size(106, 25)
        Me.AyudaStartLbl.TabIndex = 0
        Me.AyudaStartLbl.Text = "Term Start:"
        '
        'txtTargetSlots
        '
        Me.txtTargetSlots.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtTargetSlots.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTargetSlots.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetSlots.ForeColor = System.Drawing.Color.Black
        Me.txtTargetSlots.Location = New System.Drawing.Point(1070, 458)
        Me.txtTargetSlots.Name = "txtTargetSlots"
        Me.txtTargetSlots.Size = New System.Drawing.Size(618, 29)
        Me.txtTargetSlots.TabIndex = 8
        '
        'lblTargetSlots
        '
        Me.lblTargetSlots.AutoSize = True
        Me.lblTargetSlots.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTargetSlots.Location = New System.Drawing.Point(1065, 430)
        Me.lblTargetSlots.Name = "lblTargetSlots"
        Me.lblTargetSlots.Size = New System.Drawing.Size(115, 25)
        Me.lblTargetSlots.TabIndex = 0
        Me.lblTargetSlots.Text = "Target Slots"
        '
        'txtAssistanceProvision
        '
        Me.txtAssistanceProvision.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtAssistanceProvision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAssistanceProvision.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssistanceProvision.ForeColor = System.Drawing.Color.Black
        Me.txtAssistanceProvision.Location = New System.Drawing.Point(25, 737)
        Me.txtAssistanceProvision.Name = "txtAssistanceProvision"
        Me.txtAssistanceProvision.Size = New System.Drawing.Size(618, 29)
        Me.txtAssistanceProvision.TabIndex = 7
        '
        'lblAssistanceProvision
        '
        Me.lblAssistanceProvision.AutoSize = True
        Me.lblAssistanceProvision.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssistanceProvision.Location = New System.Drawing.Point(20, 709)
        Me.lblAssistanceProvision.Name = "lblAssistanceProvision"
        Me.lblAssistanceProvision.Size = New System.Drawing.Size(192, 25)
        Me.lblAssistanceProvision.TabIndex = 17
        Me.lblAssistanceProvision.Text = "Assistance Provision"
        '
        'lblAssistanceType
        '
        Me.lblAssistanceType.AutoSize = True
        Me.lblAssistanceType.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssistanceType.Location = New System.Drawing.Point(20, 623)
        Me.lblAssistanceType.Name = "lblAssistanceType"
        Me.lblAssistanceType.Size = New System.Drawing.Size(153, 25)
        Me.lblAssistanceType.TabIndex = 0
        Me.lblAssistanceType.Text = "Assistance Type"
        '
        'txtOthers
        '
        Me.txtOthers.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtOthers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOthers.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthers.ForeColor = System.Drawing.Color.Black
        Me.txtOthers.Location = New System.Drawing.Point(465, 652)
        Me.txtOthers.Name = "txtOthers"
        Me.txtOthers.Size = New System.Drawing.Size(178, 29)
        Me.txtOthers.TabIndex = 6
        '
        'cbAssistanceType
        '
        Me.cbAssistanceType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbAssistanceType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAssistanceType.FormattingEnabled = True
        Me.cbAssistanceType.Items.AddRange(New Object() {"Financial / Cash", "Food Packs (In-Kind)", "Medical Assistance", "Educational Assistance", "Hygiene/Sleeping Kits", "Others"})
        Me.cbAssistanceType.Location = New System.Drawing.Point(25, 651)
        Me.cbAssistanceType.Name = "cbAssistanceType"
        Me.cbAssistanceType.Size = New System.Drawing.Size(434, 30)
        Me.cbAssistanceType.TabIndex = 5
        '
        'txtQuantity
        '
        Me.txtQuantity.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQuantity.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantity.ForeColor = System.Drawing.Color.Black
        Me.txtQuantity.Location = New System.Drawing.Point(25, 558)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(618, 29)
        Me.txtQuantity.TabIndex = 4
        '
        'lblProgramName
        '
        Me.lblProgramName.AutoSize = True
        Me.lblProgramName.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgramName.Location = New System.Drawing.Point(20, 532)
        Me.lblProgramName.Name = "lblProgramName"
        Me.lblProgramName.Size = New System.Drawing.Size(136, 25)
        Me.lblProgramName.TabIndex = 0
        Me.lblProgramName.Text = "Program Name"
        '
        'txtResidentType
        '
        Me.txtResidentType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtResidentType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResidentType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResidentType.ForeColor = System.Drawing.Color.Black
        Me.txtResidentType.Location = New System.Drawing.Point(25, 458)
        Me.txtResidentType.Name = "txtResidentType"
        Me.txtResidentType.Size = New System.Drawing.Size(618, 29)
        Me.txtResidentType.TabIndex = 3
        '
        'lblAyudaGiver
        '
        Me.lblAyudaGiver.AutoSize = True
        Me.lblAyudaGiver.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAyudaGiver.Location = New System.Drawing.Point(20, 430)
        Me.lblAyudaGiver.Name = "lblAyudaGiver"
        Me.lblAyudaGiver.Size = New System.Drawing.Size(57, 25)
        Me.lblAyudaGiver.TabIndex = 0
        Me.lblAyudaGiver.Text = "Giver"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(1398, 52)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(290, 29)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1309, 56)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(83, 22)
        Me.lblSearch.TabIndex = 7
        Me.lblSearch.Text = "Search:"
        '
        'dgvAyudas
        '
        Me.dgvAyudas.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvAyudas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAyudas.Location = New System.Drawing.Point(12, 87)
        Me.dgvAyudas.Name = "dgvAyudas"
        Me.dgvAyudas.Size = New System.Drawing.Size(1676, 297)
        Me.dgvAyudas.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(38, 212)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 22)
        Me.Label1.TabIndex = 4
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(518, 899)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(245, 46)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'lblNewAyuda
        '
        Me.lblNewAyuda.AutoSize = True
        Me.lblNewAyuda.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewAyuda.Location = New System.Drawing.Point(20, 20)
        Me.lblNewAyuda.Name = "lblNewAyuda"
        Me.lblNewAyuda.Size = New System.Drawing.Size(140, 29)
        Me.lblNewAyuda.TabIndex = 0
        Me.lblNewAyuda.Text = "New Ayuda"
        '
        'lblCategories
        '
        Me.lblCategories.AutoSize = True
        Me.lblCategories.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategories.Location = New System.Drawing.Point(20, 778)
        Me.lblCategories.Name = "lblCategories"
        Me.lblCategories.Size = New System.Drawing.Size(160, 25)
        Me.lblCategories.TabIndex = 0
        Me.lblCategories.Text = "Resident Category"
        '
        'cbcategories
        '
        Me.cbcategories.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbcategories.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbcategories.FormattingEnabled = True
        Me.cbcategories.Location = New System.Drawing.Point(25, 806)
        Me.cbcategories.Name = "cbcategories"
        Me.cbcategories.Size = New System.Drawing.Size(1663, 30)
        Me.cbcategories.TabIndex = 13
        '
        'AyudaAdd_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AyudaAdd_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AyudaAdd_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.dgvAyudas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents lblNewAyuda As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvAyudas As DataGridView
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents lblAssistanceType As Label
    Friend WithEvents txtOthers As TextBox
    Friend WithEvents cbAssistanceType As ComboBox
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents lblProgramName As Label
    Friend WithEvents txtResidentType As TextBox
    Friend WithEvents lblAyudaGiver As Label
    Friend WithEvents txtTargetSlots As TextBox
    Friend WithEvents lblTargetSlots As Label
    Friend WithEvents txtAssistanceProvision As TextBox
    Friend WithEvents lblAssistanceProvision As Label
    Friend WithEvents AyudaEndDTP As DateTimePicker
    Friend WithEvents AyudaEndLbl As Label
    Friend WithEvents AyudaStartDTP As DateTimePicker
    Friend WithEvents AyudaStartLbl As Label
    Friend WithEvents lblAvailOrActive As Label
    Friend WithEvents rbIsActiveNo As RadioButton
    Friend WithEvents rbIsActiveYes As RadioButton
    Friend WithEvents btnEdit As Button
    Friend WithEvents lblCategories As Label
    Friend WithEvents cbcategories As ComboBox
End Class
