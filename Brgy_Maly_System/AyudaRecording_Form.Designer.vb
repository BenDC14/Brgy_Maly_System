<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AyudaRecording_Form
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
        Me.btnBackToMainPage = New System.Windows.Forms.Button()
        Me.DescriptionLbl = New System.Windows.Forms.Label()
        Me.DescriptionRtxt = New System.Windows.Forms.RichTextBox()
        Me.AyudaDateDTP = New System.Windows.Forms.DateTimePicker()
        Me.cbAyuda = New System.Windows.Forms.ComboBox()
        Me.DateLbl = New System.Windows.Forms.Label()
        Me.AyudaLbl = New System.Windows.Forms.Label()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.QuantityLbl = New System.Windows.Forms.Label()
        Me.txtResidentType = New System.Windows.Forms.TextBox()
        Me.ResidentTypeLbl = New System.Windows.Forms.Label()
        Me.LinePnl = New System.Windows.Forms.Panel()
        Me.cbResidentType = New System.Windows.Forms.ComboBox()
        Me.SearchResidentTypeLbl = New System.Windows.Forms.Label()
        Me.dgvResidents = New System.Windows.Forms.DataGridView()
        Me.ResidentInformationLbl = New System.Windows.Forms.Label()
        Me.AyudaRecordinglbl = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        CType(Me.dgvResidents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnBackToMainPage)
        Me.FillPanel.Controls.Add(Me.DescriptionLbl)
        Me.FillPanel.Controls.Add(Me.DescriptionRtxt)
        Me.FillPanel.Controls.Add(Me.AyudaDateDTP)
        Me.FillPanel.Controls.Add(Me.cbAyuda)
        Me.FillPanel.Controls.Add(Me.DateLbl)
        Me.FillPanel.Controls.Add(Me.AyudaLbl)
        Me.FillPanel.Controls.Add(Me.txtQuantity)
        Me.FillPanel.Controls.Add(Me.QuantityLbl)
        Me.FillPanel.Controls.Add(Me.txtResidentType)
        Me.FillPanel.Controls.Add(Me.ResidentTypeLbl)
        Me.FillPanel.Controls.Add(Me.LinePnl)
        Me.FillPanel.Controls.Add(Me.cbResidentType)
        Me.FillPanel.Controls.Add(Me.SearchResidentTypeLbl)
        Me.FillPanel.Controls.Add(Me.dgvResidents)
        Me.FillPanel.Controls.Add(Me.ResidentInformationLbl)
        Me.FillPanel.Controls.Add(Me.AyudaRecordinglbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'btnBackToMainPage
        '
        Me.btnBackToMainPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBackToMainPage.FlatAppearance.BorderSize = 0
        Me.btnBackToMainPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackToMainPage.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackToMainPage.Location = New System.Drawing.Point(635, 910)
        Me.btnBackToMainPage.Name = "btnBackToMainPage"
        Me.btnBackToMainPage.Size = New System.Drawing.Size(226, 45)
        Me.btnBackToMainPage.TabIndex = 9
        Me.btnBackToMainPage.Text = "Back To Main Page"
        Me.btnBackToMainPage.UseVisualStyleBackColor = False
        '
        'DescriptionLbl
        '
        Me.DescriptionLbl.AutoSize = True
        Me.DescriptionLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DescriptionLbl.Location = New System.Drawing.Point(43, 714)
        Me.DescriptionLbl.Name = "DescriptionLbl"
        Me.DescriptionLbl.Size = New System.Drawing.Size(110, 25)
        Me.DescriptionLbl.TabIndex = 0
        Me.DescriptionLbl.Text = "Description"
        '
        'DescriptionRtxt
        '
        Me.DescriptionRtxt.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.DescriptionRtxt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DescriptionRtxt.Location = New System.Drawing.Point(48, 742)
        Me.DescriptionRtxt.Name = "DescriptionRtxt"
        Me.DescriptionRtxt.Size = New System.Drawing.Size(1640, 108)
        Me.DescriptionRtxt.TabIndex = 7
        Me.DescriptionRtxt.Text = ""
        '
        'AyudaDateDTP
        '
        Me.AyudaDateDTP.CalendarFont = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaDateDTP.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.AyudaDateDTP.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaDateDTP.Location = New System.Drawing.Point(1070, 643)
        Me.AyudaDateDTP.Name = "AyudaDateDTP"
        Me.AyudaDateDTP.Size = New System.Drawing.Size(618, 29)
        Me.AyudaDateDTP.TabIndex = 6
        Me.AyudaDateDTP.Value = New Date(2026, 1, 24, 16, 53, 38, 0)
        '
        'cbAyuda
        '
        Me.cbAyuda.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbAyuda.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAyuda.FormattingEnabled = True
        Me.cbAyuda.Items.AddRange(New Object() {"Educational Assistance", "Family Ayuda", "Senior Citizen Ayuda"})
        Me.cbAyuda.Location = New System.Drawing.Point(1070, 543)
        Me.cbAyuda.Name = "cbAyuda"
        Me.cbAyuda.Size = New System.Drawing.Size(618, 30)
        Me.cbAyuda.TabIndex = 4
        '
        'DateLbl
        '
        Me.DateLbl.AutoSize = True
        Me.DateLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateLbl.Location = New System.Drawing.Point(1065, 615)
        Me.DateLbl.Name = "DateLbl"
        Me.DateLbl.Size = New System.Drawing.Size(50, 25)
        Me.DateLbl.TabIndex = 0
        Me.DateLbl.Text = "Date"
        '
        'AyudaLbl
        '
        Me.AyudaLbl.AutoSize = True
        Me.AyudaLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaLbl.Location = New System.Drawing.Point(1065, 516)
        Me.AyudaLbl.Name = "AyudaLbl"
        Me.AyudaLbl.Size = New System.Drawing.Size(66, 25)
        Me.AyudaLbl.TabIndex = 0
        Me.AyudaLbl.Text = "Ayuda"
        '
        'txtQuantity
        '
        Me.txtQuantity.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQuantity.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantity.ForeColor = System.Drawing.Color.Black
        Me.txtQuantity.Location = New System.Drawing.Point(48, 643)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(618, 29)
        Me.txtQuantity.TabIndex = 5
        '
        'QuantityLbl
        '
        Me.QuantityLbl.AutoSize = True
        Me.QuantityLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QuantityLbl.Location = New System.Drawing.Point(43, 617)
        Me.QuantityLbl.Name = "QuantityLbl"
        Me.QuantityLbl.Size = New System.Drawing.Size(84, 25)
        Me.QuantityLbl.TabIndex = 0
        Me.QuantityLbl.Text = "Quantity"
        '
        'txtResidentType
        '
        Me.txtResidentType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtResidentType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResidentType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResidentType.ForeColor = System.Drawing.Color.Black
        Me.txtResidentType.Location = New System.Drawing.Point(48, 543)
        Me.txtResidentType.Name = "txtResidentType"
        Me.txtResidentType.Size = New System.Drawing.Size(618, 29)
        Me.txtResidentType.TabIndex = 3
        '
        'ResidentTypeLbl
        '
        Me.ResidentTypeLbl.AutoSize = True
        Me.ResidentTypeLbl.Font = New System.Drawing.Font("Arial Narrow", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResidentTypeLbl.Location = New System.Drawing.Point(43, 515)
        Me.ResidentTypeLbl.Name = "ResidentTypeLbl"
        Me.ResidentTypeLbl.Size = New System.Drawing.Size(134, 25)
        Me.ResidentTypeLbl.TabIndex = 0
        Me.ResidentTypeLbl.Text = "Resident Type"
        '
        'LinePnl
        '
        Me.LinePnl.BackColor = System.Drawing.Color.Black
        Me.LinePnl.Location = New System.Drawing.Point(0, 501)
        Me.LinePnl.Name = "LinePnl"
        Me.LinePnl.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl.TabIndex = 0
        '
        'cbResidentType
        '
        Me.cbResidentType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbResidentType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbResidentType.FormattingEnabled = True
        Me.cbResidentType.Items.AddRange(New Object() {"Senior Citizen", "PWD", "Student", "Solo Parent", "Head", "Inhabitant"})
        Me.cbResidentType.Location = New System.Drawing.Point(1483, 142)
        Me.cbResidentType.Name = "cbResidentType"
        Me.cbResidentType.Size = New System.Drawing.Size(212, 30)
        Me.cbResidentType.TabIndex = 2
        '
        'SearchResidentTypeLbl
        '
        Me.SearchResidentTypeLbl.AutoSize = True
        Me.SearchResidentTypeLbl.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchResidentTypeLbl.Location = New System.Drawing.Point(1483, 119)
        Me.SearchResidentTypeLbl.Name = "SearchResidentTypeLbl"
        Me.SearchResidentTypeLbl.Size = New System.Drawing.Size(213, 22)
        Me.SearchResidentTypeLbl.TabIndex = 0
        Me.SearchResidentTypeLbl.Text = "Search Resident Type"
        '
        'dgvResidents
        '
        Me.dgvResidents.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvResidents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResidents.Location = New System.Drawing.Point(48, 142)
        Me.dgvResidents.Name = "dgvResidents"
        Me.dgvResidents.Size = New System.Drawing.Size(1429, 325)
        Me.dgvResidents.TabIndex = 1
        '
        'ResidentInformationLbl
        '
        Me.ResidentInformationLbl.AutoSize = True
        Me.ResidentInformationLbl.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResidentInformationLbl.Location = New System.Drawing.Point(43, 110)
        Me.ResidentInformationLbl.Name = "ResidentInformationLbl"
        Me.ResidentInformationLbl.Size = New System.Drawing.Size(254, 29)
        Me.ResidentInformationLbl.TabIndex = 0
        Me.ResidentInformationLbl.Text = "Resident Information"
        '
        'AyudaRecordinglbl
        '
        Me.AyudaRecordinglbl.AutoSize = True
        Me.AyudaRecordinglbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AyudaRecordinglbl.Location = New System.Drawing.Point(20, 40)
        Me.AyudaRecordinglbl.Name = "AyudaRecordinglbl"
        Me.AyudaRecordinglbl.Size = New System.Drawing.Size(241, 32)
        Me.AyudaRecordinglbl.TabIndex = 0
        Me.AyudaRecordinglbl.Text = "Ayuda Recording"
        '
        'AyudaRecording_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AyudaRecording_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AyudaRecording_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.dgvResidents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents ResidentInformationLbl As Label
    Friend WithEvents AyudaRecordinglbl As Label
    Friend WithEvents SearchResidentTypeLbl As Label
    Friend WithEvents dgvResidents As DataGridView
    Friend WithEvents cbResidentType As ComboBox
    Friend WithEvents LinePnl As Panel
    Friend WithEvents cbAyuda As ComboBox
    Friend WithEvents DateLbl As Label
    Friend WithEvents AyudaLbl As Label
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents QuantityLbl As Label
    Friend WithEvents txtResidentType As TextBox
    Friend WithEvents ResidentTypeLbl As Label
    Friend WithEvents AyudaDateDTP As DateTimePicker
    Friend WithEvents DescriptionLbl As Label
    Friend WithEvents DescriptionRtxt As RichTextBox
    Friend WithEvents btnBackToMainPage As Button
End Class
