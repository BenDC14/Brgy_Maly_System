<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BrgyInfoAdding_Form
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
        Me.SearchOfficialLbl = New System.Windows.Forms.Label()
        Me.BtnArchieveSelected = New System.Windows.Forms.Button()
        Me.BtnEditSelected = New System.Windows.Forms.Button()
        Me.BtnAddNewOfficial = New System.Windows.Forms.Button()
        Me.BrgyOfficialsDGV = New System.Windows.Forms.DataGridView()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.TxtSearchOfficial = New System.Windows.Forms.TextBox()
        Me.LinePnl2 = New System.Windows.Forms.Panel()
        Me.BtnSaveInfo = New System.Windows.Forms.Button()
        Me.BtnRemove = New System.Windows.Forms.Button()
        Me.BtnUpload = New System.Windows.Forms.Button()
        Me.BrgyLogoPic = New System.Windows.Forms.PictureBox()
        Me.BrgyLogoLbl = New System.Windows.Forms.Label()
        Me.BrgyVisionRtxt = New System.Windows.Forms.RichTextBox()
        Me.BrgyVisionLbl = New System.Windows.Forms.Label()
        Me.BrgyMissionRtxt = New System.Windows.Forms.RichTextBox()
        Me.BrgyMissionLbl = New System.Windows.Forms.Label()
        Me.txtBrgyName = New System.Windows.Forms.TextBox()
        Me.BrgyNameLbl = New System.Windows.Forms.Label()
        Me.LinePnl = New System.Windows.Forms.Panel()
        Me.BrgyDetailsLbl = New System.Windows.Forms.Label()
        Me.BrgyInfoLbl = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        CType(Me.BrgyOfficialsDGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BrgyLogoPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.SearchOfficialLbl)
        Me.FillPanel.Controls.Add(Me.BtnArchieveSelected)
        Me.FillPanel.Controls.Add(Me.BtnEditSelected)
        Me.FillPanel.Controls.Add(Me.BtnAddNewOfficial)
        Me.FillPanel.Controls.Add(Me.BrgyOfficialsDGV)
        Me.FillPanel.Controls.Add(Me.BtnSearch)
        Me.FillPanel.Controls.Add(Me.TxtSearchOfficial)
        Me.FillPanel.Controls.Add(Me.LinePnl2)
        Me.FillPanel.Controls.Add(Me.BtnSaveInfo)
        Me.FillPanel.Controls.Add(Me.BtnRemove)
        Me.FillPanel.Controls.Add(Me.BtnUpload)
        Me.FillPanel.Controls.Add(Me.BrgyLogoPic)
        Me.FillPanel.Controls.Add(Me.BrgyLogoLbl)
        Me.FillPanel.Controls.Add(Me.BrgyVisionRtxt)
        Me.FillPanel.Controls.Add(Me.BrgyVisionLbl)
        Me.FillPanel.Controls.Add(Me.BrgyMissionRtxt)
        Me.FillPanel.Controls.Add(Me.BrgyMissionLbl)
        Me.FillPanel.Controls.Add(Me.txtBrgyName)
        Me.FillPanel.Controls.Add(Me.BrgyNameLbl)
        Me.FillPanel.Controls.Add(Me.LinePnl)
        Me.FillPanel.Controls.Add(Me.BrgyDetailsLbl)
        Me.FillPanel.Controls.Add(Me.BrgyInfoLbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'SearchOfficialLbl
        '
        Me.SearchOfficialLbl.AutoSize = True
        Me.SearchOfficialLbl.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchOfficialLbl.Location = New System.Drawing.Point(40, 635)
        Me.SearchOfficialLbl.Name = "SearchOfficialLbl"
        Me.SearchOfficialLbl.Size = New System.Drawing.Size(152, 22)
        Me.SearchOfficialLbl.TabIndex = 0
        Me.SearchOfficialLbl.Text = "Search Official:"
        '
        'BtnArchieveSelected
        '
        Me.BtnArchieveSelected.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnArchieveSelected.FlatAppearance.BorderSize = 0
        Me.BtnArchieveSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnArchieveSelected.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnArchieveSelected.Location = New System.Drawing.Point(1340, 950)
        Me.BtnArchieveSelected.Name = "BtnArchieveSelected"
        Me.BtnArchieveSelected.Size = New System.Drawing.Size(190, 29)
        Me.BtnArchieveSelected.TabIndex = 12
        Me.BtnArchieveSelected.Text = "Archieve Selected"
        Me.BtnArchieveSelected.UseVisualStyleBackColor = False
        '
        'BtnEditSelected
        '
        Me.BtnEditSelected.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnEditSelected.FlatAppearance.BorderSize = 0
        Me.BtnEditSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEditSelected.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEditSelected.Location = New System.Drawing.Point(751, 950)
        Me.BtnEditSelected.Name = "BtnEditSelected"
        Me.BtnEditSelected.Size = New System.Drawing.Size(190, 29)
        Me.BtnEditSelected.TabIndex = 11
        Me.BtnEditSelected.Text = "Edit Selected"
        Me.BtnEditSelected.UseVisualStyleBackColor = False
        '
        'BtnAddNewOfficial
        '
        Me.BtnAddNewOfficial.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnAddNewOfficial.FlatAppearance.BorderSize = 0
        Me.BtnAddNewOfficial.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddNewOfficial.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddNewOfficial.Location = New System.Drawing.Point(178, 950)
        Me.BtnAddNewOfficial.Name = "BtnAddNewOfficial"
        Me.BtnAddNewOfficial.Size = New System.Drawing.Size(190, 29)
        Me.BtnAddNewOfficial.TabIndex = 10
        Me.BtnAddNewOfficial.Text = "Add New Official"
        Me.BtnAddNewOfficial.UseVisualStyleBackColor = False
        '
        'BrgyOfficialsDGV
        '
        Me.BrgyOfficialsDGV.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.BrgyOfficialsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BrgyOfficialsDGV.Location = New System.Drawing.Point(44, 660)
        Me.BrgyOfficialsDGV.Name = "BrgyOfficialsDGV"
        Me.BrgyOfficialsDGV.Size = New System.Drawing.Size(1611, 267)
        Me.BrgyOfficialsDGV.TabIndex = 9
        '
        'BtnSearch
        '
        Me.BtnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnSearch.FlatAppearance.BorderSize = 0
        Me.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSearch.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(1550, 628)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(105, 29)
        Me.BtnSearch.TabIndex = 8
        Me.BtnSearch.Text = "Search"
        Me.BtnSearch.UseVisualStyleBackColor = False
        '
        'TxtSearchOfficial
        '
        Me.TxtSearchOfficial.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.TxtSearchOfficial.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearchOfficial.Location = New System.Drawing.Point(210, 628)
        Me.TxtSearchOfficial.Name = "TxtSearchOfficial"
        Me.TxtSearchOfficial.Size = New System.Drawing.Size(1320, 29)
        Me.TxtSearchOfficial.TabIndex = 7
        '
        'LinePnl2
        '
        Me.LinePnl2.BackColor = System.Drawing.Color.Black
        Me.LinePnl2.Location = New System.Drawing.Point(0, 568)
        Me.LinePnl2.Name = "LinePnl2"
        Me.LinePnl2.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl2.TabIndex = 0
        '
        'BtnSaveInfo
        '
        Me.BtnSaveInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnSaveInfo.FlatAppearance.BorderSize = 0
        Me.BtnSaveInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveInfo.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveInfo.Location = New System.Drawing.Point(661, 516)
        Me.BtnSaveInfo.Name = "BtnSaveInfo"
        Me.BtnSaveInfo.Size = New System.Drawing.Size(392, 44)
        Me.BtnSaveInfo.TabIndex = 6
        Me.BtnSaveInfo.Text = "Save Barangay Information"
        Me.BtnSaveInfo.UseVisualStyleBackColor = False
        '
        'BtnRemove
        '
        Me.BtnRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnRemove.FlatAppearance.BorderSize = 0
        Me.BtnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRemove.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRemove.Location = New System.Drawing.Point(435, 467)
        Me.BtnRemove.Name = "BtnRemove"
        Me.BtnRemove.Size = New System.Drawing.Size(199, 44)
        Me.BtnRemove.TabIndex = 5
        Me.BtnRemove.Text = "Remove"
        Me.BtnRemove.UseVisualStyleBackColor = False
        '
        'BtnUpload
        '
        Me.BtnUpload.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnUpload.FlatAppearance.BorderSize = 0
        Me.BtnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpload.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpload.Location = New System.Drawing.Point(435, 407)
        Me.BtnUpload.Name = "BtnUpload"
        Me.BtnUpload.Size = New System.Drawing.Size(199, 44)
        Me.BtnUpload.TabIndex = 4
        Me.BtnUpload.Text = "Upload"
        Me.BtnUpload.UseVisualStyleBackColor = False
        '
        'BrgyLogoPic
        '
        Me.BrgyLogoPic.Image = Global.Brgy_Maly_System.My.Resources.Resources.LogoForMaly
        Me.BrgyLogoPic.Location = New System.Drawing.Point(251, 407)
        Me.BrgyLogoPic.Name = "BrgyLogoPic"
        Me.BrgyLogoPic.Size = New System.Drawing.Size(117, 104)
        Me.BrgyLogoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BrgyLogoPic.TabIndex = 4
        Me.BrgyLogoPic.TabStop = False
        '
        'BrgyLogoLbl
        '
        Me.BrgyLogoLbl.AutoSize = True
        Me.BrgyLogoLbl.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyLogoLbl.Location = New System.Drawing.Point(40, 407)
        Me.BrgyLogoLbl.Name = "BrgyLogoLbl"
        Me.BrgyLogoLbl.Size = New System.Drawing.Size(160, 22)
        Me.BrgyLogoLbl.TabIndex = 0
        Me.BrgyLogoLbl.Text = "Barangay Logo:"
        '
        'BrgyVisionRtxt
        '
        Me.BrgyVisionRtxt.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.BrgyVisionRtxt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyVisionRtxt.Location = New System.Drawing.Point(210, 303)
        Me.BrgyVisionRtxt.Name = "BrgyVisionRtxt"
        Me.BrgyVisionRtxt.Size = New System.Drawing.Size(1320, 85)
        Me.BrgyVisionRtxt.TabIndex = 3
        Me.BrgyVisionRtxt.Text = ""
        '
        'BrgyVisionLbl
        '
        Me.BrgyVisionLbl.AutoSize = True
        Me.BrgyVisionLbl.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyVisionLbl.Location = New System.Drawing.Point(40, 303)
        Me.BrgyVisionLbl.Name = "BrgyVisionLbl"
        Me.BrgyVisionLbl.Size = New System.Drawing.Size(75, 22)
        Me.BrgyVisionLbl.TabIndex = 0
        Me.BrgyVisionLbl.Text = "Vision:"
        '
        'BrgyMissionRtxt
        '
        Me.BrgyMissionRtxt.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.BrgyMissionRtxt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyMissionRtxt.Location = New System.Drawing.Point(210, 202)
        Me.BrgyMissionRtxt.Name = "BrgyMissionRtxt"
        Me.BrgyMissionRtxt.Size = New System.Drawing.Size(1320, 85)
        Me.BrgyMissionRtxt.TabIndex = 2
        Me.BrgyMissionRtxt.Text = ""
        '
        'BrgyMissionLbl
        '
        Me.BrgyMissionLbl.AutoSize = True
        Me.BrgyMissionLbl.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyMissionLbl.Location = New System.Drawing.Point(40, 202)
        Me.BrgyMissionLbl.Name = "BrgyMissionLbl"
        Me.BrgyMissionLbl.Size = New System.Drawing.Size(90, 22)
        Me.BrgyMissionLbl.TabIndex = 0
        Me.BrgyMissionLbl.Text = "Mission:"
        '
        'txtBrgyName
        '
        Me.txtBrgyName.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtBrgyName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBrgyName.Location = New System.Drawing.Point(210, 149)
        Me.txtBrgyName.Name = "txtBrgyName"
        Me.txtBrgyName.Size = New System.Drawing.Size(1320, 29)
        Me.txtBrgyName.TabIndex = 1
        '
        'BrgyNameLbl
        '
        Me.BrgyNameLbl.AutoSize = True
        Me.BrgyNameLbl.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyNameLbl.Location = New System.Drawing.Point(40, 149)
        Me.BrgyNameLbl.Name = "BrgyNameLbl"
        Me.BrgyNameLbl.Size = New System.Drawing.Size(164, 22)
        Me.BrgyNameLbl.TabIndex = 0
        Me.BrgyNameLbl.Text = "Barangay Name:"
        '
        'LinePnl
        '
        Me.LinePnl.BackColor = System.Drawing.Color.Black
        Me.LinePnl.Location = New System.Drawing.Point(0, 81)
        Me.LinePnl.Name = "LinePnl"
        Me.LinePnl.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl.TabIndex = 0
        '
        'BrgyDetailsLbl
        '
        Me.BrgyDetailsLbl.AutoSize = True
        Me.BrgyDetailsLbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyDetailsLbl.Location = New System.Drawing.Point(745, 96)
        Me.BrgyDetailsLbl.Name = "BrgyDetailsLbl"
        Me.BrgyDetailsLbl.Size = New System.Drawing.Size(237, 32)
        Me.BrgyDetailsLbl.TabIndex = 0
        Me.BrgyDetailsLbl.Text = "Barangay Details"
        '
        'BrgyInfoLbl
        '
        Me.BrgyInfoLbl.AutoSize = True
        Me.BrgyInfoLbl.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyInfoLbl.Location = New System.Drawing.Point(23, 25)
        Me.BrgyInfoLbl.Name = "BrgyInfoLbl"
        Me.BrgyInfoLbl.Size = New System.Drawing.Size(345, 37)
        Me.BrgyInfoLbl.TabIndex = 0
        Me.BrgyInfoLbl.Text = "Barangay Information"
        '
        'BrgyInfoAdding_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "BrgyInfoAdding_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BrgyInfoAdding_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.BrgyOfficialsDGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BrgyLogoPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents BrgyInfoLbl As Label
    Friend WithEvents LinePnl As Panel
    Friend WithEvents BrgyDetailsLbl As Label
    Friend WithEvents txtBrgyName As TextBox
    Friend WithEvents BrgyNameLbl As Label
    Friend WithEvents BrgyMissionRtxt As RichTextBox
    Friend WithEvents BrgyMissionLbl As Label
    Friend WithEvents BrgyLogoLbl As Label
    Friend WithEvents BrgyVisionRtxt As RichTextBox
    Friend WithEvents BrgyVisionLbl As Label
    Friend WithEvents BrgyLogoPic As PictureBox
    Friend WithEvents BtnRemove As Button
    Friend WithEvents BtnUpload As Button
    Friend WithEvents BtnSaveInfo As Button
    Friend WithEvents LinePnl2 As Panel
    Friend WithEvents TxtSearchOfficial As TextBox
    Friend WithEvents BrgyOfficialsDGV As DataGridView
    Friend WithEvents BtnSearch As Button
    Friend WithEvents BtnArchieveSelected As Button
    Friend WithEvents BtnEditSelected As Button
    Friend WithEvents BtnAddNewOfficial As Button
    Friend WithEvents SearchOfficialLbl As Label
End Class
