<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ResidentMain_Form
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
        Me.fillpanel = New System.Windows.Forms.Panel()
        Me.dgvResidents = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.btnAddNewResident = New System.Windows.Forms.Button()
        Me.ResidentInfolbl = New System.Windows.Forms.Label()
        Me.fillpanel.SuspendLayout()
        CType(Me.dgvResidents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fillpanel
        '
        Me.fillpanel.BackColor = System.Drawing.Color.Transparent
        Me.fillpanel.Controls.Add(Me.dgvResidents)
        Me.fillpanel.Controls.Add(Me.btnSearch)
        Me.fillpanel.Controls.Add(Me.txtSearch)
        Me.fillpanel.Controls.Add(Me.lblSearch)
        Me.fillpanel.Controls.Add(Me.btnAddNewResident)
        Me.fillpanel.Controls.Add(Me.ResidentInfolbl)
        Me.fillpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fillpanel.Location = New System.Drawing.Point(0, 0)
        Me.fillpanel.Name = "fillpanel"
        Me.fillpanel.Size = New System.Drawing.Size(1700, 1004)
        Me.fillpanel.TabIndex = 0
        '
        'dgvResidents
        '
        Me.dgvResidents.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvResidents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResidents.Location = New System.Drawing.Point(12, 162)
        Me.dgvResidents.Name = "dgvResidents"
        Me.dgvResidents.Size = New System.Drawing.Size(1676, 766)
        Me.dgvResidents.TabIndex = 4
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1537, 119)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(151, 29)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(1241, 119)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(290, 29)
        Me.txtSearch.TabIndex = 2
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1152, 123)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(83, 22)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'btnAddNewResident
        '
        Me.btnAddNewResident.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnAddNewResident.FlatAppearance.BorderSize = 0
        Me.btnAddNewResident.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNewResident.Font = New System.Drawing.Font("Arial Narrow", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNewResident.Location = New System.Drawing.Point(40, 103)
        Me.btnAddNewResident.Name = "btnAddNewResident"
        Me.btnAddNewResident.Size = New System.Drawing.Size(204, 45)
        Me.btnAddNewResident.TabIndex = 1
        Me.btnAddNewResident.Text = "Add New Resident"
        Me.btnAddNewResident.UseVisualStyleBackColor = False
        '
        'ResidentInfolbl
        '
        Me.ResidentInfolbl.AutoSize = True
        Me.ResidentInfolbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResidentInfolbl.Location = New System.Drawing.Point(20, 40)
        Me.ResidentInfolbl.Name = "ResidentInfolbl"
        Me.ResidentInfolbl.Size = New System.Drawing.Size(292, 32)
        Me.ResidentInfolbl.TabIndex = 0
        Me.ResidentInfolbl.Text = "Resident Information"
        '
        'ResidentMain_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.fillpanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ResidentMain_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ResidentMain_Form"
        Me.fillpanel.ResumeLayout(False)
        Me.fillpanel.PerformLayout()
        CType(Me.dgvResidents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents fillpanel As Panel
    Friend WithEvents dgvResidents As DataGridView
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents btnAddNewResident As Button
    Friend WithEvents ResidentInfolbl As Label
End Class
