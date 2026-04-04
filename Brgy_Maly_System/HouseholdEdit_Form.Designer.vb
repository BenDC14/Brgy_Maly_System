<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HouseholdEdit_Form
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
        Me.btnBack = New System.Windows.Forms.Button()
        Me.BtnEditHousehold = New System.Windows.Forms.Button()
        Me.lblSearchFamHead = New System.Windows.Forms.Label()
        Me.btnSearchHeads = New System.Windows.Forms.Button()
        Me.txtSearchFamilyHeads = New System.Windows.Forms.TextBox()
        Me.SearchResidentsLbl = New System.Windows.Forms.Label()
        Me.BtnSearchResident = New System.Windows.Forms.Button()
        Me.TxtSearchResidents = New System.Windows.Forms.TextBox()
        Me.ResidentsInTheHouseholdlbl = New System.Windows.Forms.Label()
        Me.FamilyHeadsInTheHouseholdlbl = New System.Windows.Forms.Label()
        Me.ResidentInHouseholdDGV = New System.Windows.Forms.DataGridView()
        Me.FamilyHeadsDGV = New System.Windows.Forms.DataGridView()
        Me.LinePnl2 = New System.Windows.Forms.Panel()
        Me.LinePnl1 = New System.Windows.Forms.Panel()
        Me.LinePnl5 = New System.Windows.Forms.Panel()
        Me.txtProvince = New System.Windows.Forms.TextBox()
        Me.Provincelbl = New System.Windows.Forms.Label()
        Me.txtMunicipality = New System.Windows.Forms.TextBox()
        Me.Municipalitylbl = New System.Windows.Forms.Label()
        Me.txtBarangay = New System.Windows.Forms.TextBox()
        Me.Barangaylbl = New System.Windows.Forms.Label()
        Me.txtCompound = New System.Windows.Forms.TextBox()
        Me.Compoundlbl = New System.Windows.Forms.Label()
        Me.txtSubdivision = New System.Windows.Forms.TextBox()
        Me.Subdivisionlbl = New System.Windows.Forms.Label()
        Me.txtVillage = New System.Windows.Forms.TextBox()
        Me.Villagelbl = New System.Windows.Forms.Label()
        Me.txStreetName = New System.Windows.Forms.TextBox()
        Me.StreetNamelbl = New System.Windows.Forms.Label()
        Me.txtAreaNumber = New System.Windows.Forms.TextBox()
        Me.AreaNumberlbl = New System.Windows.Forms.Label()
        Me.txtLotNumber = New System.Windows.Forms.TextBox()
        Me.LotNumberlbl = New System.Windows.Forms.Label()
        Me.txtBlockNumber = New System.Windows.Forms.TextBox()
        Me.BlockNumberlbl = New System.Windows.Forms.Label()
        Me.txtHouseNumber = New System.Windows.Forms.TextBox()
        Me.HouseNumberlbl = New System.Windows.Forms.Label()
        Me.txtHouseholdNumber = New System.Windows.Forms.TextBox()
        Me.HouseholdNumlbl = New System.Windows.Forms.Label()
        Me.AddHouseholdlbl = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        CType(Me.ResidentInHouseholdDGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FamilyHeadsDGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LinePnl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnBack)
        Me.FillPanel.Controls.Add(Me.BtnEditHousehold)
        Me.FillPanel.Controls.Add(Me.lblSearchFamHead)
        Me.FillPanel.Controls.Add(Me.btnSearchHeads)
        Me.FillPanel.Controls.Add(Me.txtSearchFamilyHeads)
        Me.FillPanel.Controls.Add(Me.SearchResidentsLbl)
        Me.FillPanel.Controls.Add(Me.BtnSearchResident)
        Me.FillPanel.Controls.Add(Me.TxtSearchResidents)
        Me.FillPanel.Controls.Add(Me.ResidentsInTheHouseholdlbl)
        Me.FillPanel.Controls.Add(Me.FamilyHeadsInTheHouseholdlbl)
        Me.FillPanel.Controls.Add(Me.ResidentInHouseholdDGV)
        Me.FillPanel.Controls.Add(Me.FamilyHeadsDGV)
        Me.FillPanel.Controls.Add(Me.LinePnl2)
        Me.FillPanel.Controls.Add(Me.LinePnl1)
        Me.FillPanel.Controls.Add(Me.txtProvince)
        Me.FillPanel.Controls.Add(Me.Provincelbl)
        Me.FillPanel.Controls.Add(Me.txtMunicipality)
        Me.FillPanel.Controls.Add(Me.Municipalitylbl)
        Me.FillPanel.Controls.Add(Me.txtBarangay)
        Me.FillPanel.Controls.Add(Me.Barangaylbl)
        Me.FillPanel.Controls.Add(Me.txtCompound)
        Me.FillPanel.Controls.Add(Me.Compoundlbl)
        Me.FillPanel.Controls.Add(Me.txtSubdivision)
        Me.FillPanel.Controls.Add(Me.Subdivisionlbl)
        Me.FillPanel.Controls.Add(Me.txtVillage)
        Me.FillPanel.Controls.Add(Me.Villagelbl)
        Me.FillPanel.Controls.Add(Me.txStreetName)
        Me.FillPanel.Controls.Add(Me.StreetNamelbl)
        Me.FillPanel.Controls.Add(Me.txtAreaNumber)
        Me.FillPanel.Controls.Add(Me.AreaNumberlbl)
        Me.FillPanel.Controls.Add(Me.txtLotNumber)
        Me.FillPanel.Controls.Add(Me.LotNumberlbl)
        Me.FillPanel.Controls.Add(Me.txtBlockNumber)
        Me.FillPanel.Controls.Add(Me.BlockNumberlbl)
        Me.FillPanel.Controls.Add(Me.txtHouseNumber)
        Me.FillPanel.Controls.Add(Me.HouseNumberlbl)
        Me.FillPanel.Controls.Add(Me.txtHouseholdNumber)
        Me.FillPanel.Controls.Add(Me.HouseholdNumlbl)
        Me.FillPanel.Controls.Add(Me.AddHouseholdlbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(272, 930)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(199, 44)
        Me.btnBack.TabIndex = 14
        Me.btnBack.Text = "Back To Main"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'BtnEditHousehold
        '
        Me.BtnEditHousehold.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnEditHousehold.FlatAppearance.BorderSize = 0
        Me.BtnEditHousehold.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEditHousehold.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEditHousehold.Location = New System.Drawing.Point(26, 930)
        Me.BtnEditHousehold.Name = "BtnEditHousehold"
        Me.BtnEditHousehold.Size = New System.Drawing.Size(199, 44)
        Me.BtnEditHousehold.TabIndex = 13
        Me.BtnEditHousehold.Text = "Edit Household"
        Me.BtnEditHousehold.UseVisualStyleBackColor = False
        '
        'lblSearchFamHead
        '
        Me.lblSearchFamHead.AutoSize = True
        Me.lblSearchFamHead.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchFamHead.Location = New System.Drawing.Point(1235, 24)
        Me.lblSearchFamHead.Name = "lblSearchFamHead"
        Me.lblSearchFamHead.Size = New System.Drawing.Size(212, 22)
        Me.lblSearchFamHead.TabIndex = 0
        Me.lblSearchFamHead.Text = "Search Family Heads:"
        '
        'btnSearchHeads
        '
        Me.btnSearchHeads.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSearchHeads.FlatAppearance.BorderSize = 0
        Me.btnSearchHeads.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchHeads.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchHeads.Location = New System.Drawing.Point(1571, 49)
        Me.btnSearchHeads.Name = "btnSearchHeads"
        Me.btnSearchHeads.Size = New System.Drawing.Size(105, 29)
        Me.btnSearchHeads.TabIndex = 16
        Me.btnSearchHeads.Text = "Search"
        Me.btnSearchHeads.UseVisualStyleBackColor = False
        '
        'txtSearchFamilyHeads
        '
        Me.txtSearchFamilyHeads.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSearchFamilyHeads.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchFamilyHeads.Location = New System.Drawing.Point(1239, 49)
        Me.txtSearchFamilyHeads.Name = "txtSearchFamilyHeads"
        Me.txtSearchFamilyHeads.Size = New System.Drawing.Size(312, 29)
        Me.txtSearchFamilyHeads.TabIndex = 15
        '
        'SearchResidentsLbl
        '
        Me.SearchResidentsLbl.AutoSize = True
        Me.SearchResidentsLbl.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchResidentsLbl.Location = New System.Drawing.Point(1235, 532)
        Me.SearchResidentsLbl.Name = "SearchResidentsLbl"
        Me.SearchResidentsLbl.Size = New System.Drawing.Size(180, 22)
        Me.SearchResidentsLbl.TabIndex = 0
        Me.SearchResidentsLbl.Text = "Search Residents:"
        '
        'BtnSearchResident
        '
        Me.BtnSearchResident.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.BtnSearchResident.FlatAppearance.BorderSize = 0
        Me.BtnSearchResident.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSearchResident.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSearchResident.Location = New System.Drawing.Point(1571, 557)
        Me.BtnSearchResident.Name = "BtnSearchResident"
        Me.BtnSearchResident.Size = New System.Drawing.Size(105, 29)
        Me.BtnSearchResident.TabIndex = 19
        Me.BtnSearchResident.Text = "Search"
        Me.BtnSearchResident.UseVisualStyleBackColor = False
        '
        'TxtSearchResidents
        '
        Me.TxtSearchResidents.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.TxtSearchResidents.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearchResidents.Location = New System.Drawing.Point(1239, 557)
        Me.TxtSearchResidents.Name = "TxtSearchResidents"
        Me.TxtSearchResidents.Size = New System.Drawing.Size(312, 29)
        Me.TxtSearchResidents.TabIndex = 18
        '
        'ResidentsInTheHouseholdlbl
        '
        Me.ResidentsInTheHouseholdlbl.AutoSize = True
        Me.ResidentsInTheHouseholdlbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResidentsInTheHouseholdlbl.Location = New System.Drawing.Point(525, 554)
        Me.ResidentsInTheHouseholdlbl.Name = "ResidentsInTheHouseholdlbl"
        Me.ResidentsInTheHouseholdlbl.Size = New System.Drawing.Size(378, 32)
        Me.ResidentsInTheHouseholdlbl.TabIndex = 0
        Me.ResidentsInTheHouseholdlbl.Text = "Residents in the Household"
        '
        'FamilyHeadsInTheHouseholdlbl
        '
        Me.FamilyHeadsInTheHouseholdlbl.AutoSize = True
        Me.FamilyHeadsInTheHouseholdlbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FamilyHeadsInTheHouseholdlbl.Location = New System.Drawing.Point(525, 49)
        Me.FamilyHeadsInTheHouseholdlbl.Name = "FamilyHeadsInTheHouseholdlbl"
        Me.FamilyHeadsInTheHouseholdlbl.Size = New System.Drawing.Size(426, 32)
        Me.FamilyHeadsInTheHouseholdlbl.TabIndex = 0
        Me.FamilyHeadsInTheHouseholdlbl.Text = "Family Heads in the Household"
        '
        'ResidentInHouseholdDGV
        '
        Me.ResidentInHouseholdDGV.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.ResidentInHouseholdDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ResidentInHouseholdDGV.Location = New System.Drawing.Point(531, 592)
        Me.ResidentInHouseholdDGV.Name = "ResidentInHouseholdDGV"
        Me.ResidentInHouseholdDGV.Size = New System.Drawing.Size(1145, 382)
        Me.ResidentInHouseholdDGV.TabIndex = 20
        '
        'FamilyHeadsDGV
        '
        Me.FamilyHeadsDGV.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.FamilyHeadsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FamilyHeadsDGV.Location = New System.Drawing.Point(531, 84)
        Me.FamilyHeadsDGV.Name = "FamilyHeadsDGV"
        Me.FamilyHeadsDGV.Size = New System.Drawing.Size(1145, 382)
        Me.FamilyHeadsDGV.TabIndex = 17
        '
        'LinePnl2
        '
        Me.LinePnl2.BackColor = System.Drawing.Color.Black
        Me.LinePnl2.Location = New System.Drawing.Point(500, 493)
        Me.LinePnl2.Name = "LinePnl2"
        Me.LinePnl2.Size = New System.Drawing.Size(1200, 2)
        Me.LinePnl2.TabIndex = 0
        '
        'LinePnl1
        '
        Me.LinePnl1.BackColor = System.Drawing.Color.Black
        Me.LinePnl1.Controls.Add(Me.LinePnl5)
        Me.LinePnl1.Location = New System.Drawing.Point(500, 0)
        Me.LinePnl1.Name = "LinePnl1"
        Me.LinePnl1.Size = New System.Drawing.Size(2, 1004)
        Me.LinePnl1.TabIndex = 0
        '
        'LinePnl5
        '
        Me.LinePnl5.BackColor = System.Drawing.Color.Black
        Me.LinePnl5.Location = New System.Drawing.Point(-849, 501)
        Me.LinePnl5.Name = "LinePnl5"
        Me.LinePnl5.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl5.TabIndex = 1
        '
        'txtProvince
        '
        Me.txtProvince.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtProvince.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProvince.Location = New System.Drawing.Point(52, 854)
        Me.txtProvince.Name = "txtProvince"
        Me.txtProvince.Size = New System.Drawing.Size(384, 26)
        Me.txtProvince.TabIndex = 12
        '
        'Provincelbl
        '
        Me.Provincelbl.AutoSize = True
        Me.Provincelbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Provincelbl.Location = New System.Drawing.Point(48, 831)
        Me.Provincelbl.Name = "Provincelbl"
        Me.Provincelbl.Size = New System.Drawing.Size(77, 19)
        Me.Provincelbl.TabIndex = 0
        Me.Provincelbl.Text = "Province"
        '
        'txtMunicipality
        '
        Me.txtMunicipality.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtMunicipality.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMunicipality.Location = New System.Drawing.Point(52, 788)
        Me.txtMunicipality.Name = "txtMunicipality"
        Me.txtMunicipality.Size = New System.Drawing.Size(384, 26)
        Me.txtMunicipality.TabIndex = 11
        '
        'Municipalitylbl
        '
        Me.Municipalitylbl.AutoSize = True
        Me.Municipalitylbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Municipalitylbl.Location = New System.Drawing.Point(48, 765)
        Me.Municipalitylbl.Name = "Municipalitylbl"
        Me.Municipalitylbl.Size = New System.Drawing.Size(100, 19)
        Me.Municipalitylbl.TabIndex = 0
        Me.Municipalitylbl.Text = "Municipality"
        '
        'txtBarangay
        '
        Me.txtBarangay.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtBarangay.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarangay.Location = New System.Drawing.Point(52, 721)
        Me.txtBarangay.Name = "txtBarangay"
        Me.txtBarangay.Size = New System.Drawing.Size(384, 26)
        Me.txtBarangay.TabIndex = 10
        '
        'Barangaylbl
        '
        Me.Barangaylbl.AutoSize = True
        Me.Barangaylbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barangaylbl.Location = New System.Drawing.Point(48, 698)
        Me.Barangaylbl.Name = "Barangaylbl"
        Me.Barangaylbl.Size = New System.Drawing.Size(83, 19)
        Me.Barangaylbl.TabIndex = 0
        Me.Barangaylbl.Text = "Barangay"
        '
        'txtCompound
        '
        Me.txtCompound.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtCompound.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompound.Location = New System.Drawing.Point(52, 655)
        Me.txtCompound.Name = "txtCompound"
        Me.txtCompound.Size = New System.Drawing.Size(384, 26)
        Me.txtCompound.TabIndex = 9
        '
        'Compoundlbl
        '
        Me.Compoundlbl.AutoSize = True
        Me.Compoundlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Compoundlbl.Location = New System.Drawing.Point(48, 632)
        Me.Compoundlbl.Name = "Compoundlbl"
        Me.Compoundlbl.Size = New System.Drawing.Size(95, 19)
        Me.Compoundlbl.TabIndex = 0
        Me.Compoundlbl.Text = "Compound"
        '
        'txtSubdivision
        '
        Me.txtSubdivision.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSubdivision.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubdivision.Location = New System.Drawing.Point(52, 590)
        Me.txtSubdivision.Name = "txtSubdivision"
        Me.txtSubdivision.Size = New System.Drawing.Size(384, 26)
        Me.txtSubdivision.TabIndex = 8
        '
        'Subdivisionlbl
        '
        Me.Subdivisionlbl.AutoSize = True
        Me.Subdivisionlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Subdivisionlbl.Location = New System.Drawing.Point(48, 567)
        Me.Subdivisionlbl.Name = "Subdivisionlbl"
        Me.Subdivisionlbl.Size = New System.Drawing.Size(100, 19)
        Me.Subdivisionlbl.TabIndex = 0
        Me.Subdivisionlbl.Text = "Subdivision"
        '
        'txtVillage
        '
        Me.txtVillage.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtVillage.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVillage.Location = New System.Drawing.Point(52, 521)
        Me.txtVillage.Name = "txtVillage"
        Me.txtVillage.Size = New System.Drawing.Size(384, 26)
        Me.txtVillage.TabIndex = 7
        '
        'Villagelbl
        '
        Me.Villagelbl.AutoSize = True
        Me.Villagelbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Villagelbl.Location = New System.Drawing.Point(48, 498)
        Me.Villagelbl.Name = "Villagelbl"
        Me.Villagelbl.Size = New System.Drawing.Size(60, 19)
        Me.Villagelbl.TabIndex = 0
        Me.Villagelbl.Text = "Village"
        '
        'txStreetName
        '
        Me.txStreetName.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txStreetName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txStreetName.Location = New System.Drawing.Point(52, 455)
        Me.txStreetName.Name = "txStreetName"
        Me.txStreetName.Size = New System.Drawing.Size(384, 26)
        Me.txStreetName.TabIndex = 6
        '
        'StreetNamelbl
        '
        Me.StreetNamelbl.AutoSize = True
        Me.StreetNamelbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StreetNamelbl.Location = New System.Drawing.Point(48, 432)
        Me.StreetNamelbl.Name = "StreetNamelbl"
        Me.StreetNamelbl.Size = New System.Drawing.Size(102, 19)
        Me.StreetNamelbl.TabIndex = 0
        Me.StreetNamelbl.Text = "Street Name"
        '
        'txtAreaNumber
        '
        Me.txtAreaNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtAreaNumber.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAreaNumber.Location = New System.Drawing.Point(52, 392)
        Me.txtAreaNumber.Name = "txtAreaNumber"
        Me.txtAreaNumber.Size = New System.Drawing.Size(384, 26)
        Me.txtAreaNumber.TabIndex = 5
        '
        'AreaNumberlbl
        '
        Me.AreaNumberlbl.AutoSize = True
        Me.AreaNumberlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AreaNumberlbl.Location = New System.Drawing.Point(48, 369)
        Me.AreaNumberlbl.Name = "AreaNumberlbl"
        Me.AreaNumberlbl.Size = New System.Drawing.Size(109, 19)
        Me.AreaNumberlbl.TabIndex = 0
        Me.AreaNumberlbl.Text = "Area Number"
        '
        'txtLotNumber
        '
        Me.txtLotNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtLotNumber.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLotNumber.Location = New System.Drawing.Point(52, 325)
        Me.txtLotNumber.Name = "txtLotNumber"
        Me.txtLotNumber.Size = New System.Drawing.Size(384, 26)
        Me.txtLotNumber.TabIndex = 4
        '
        'LotNumberlbl
        '
        Me.LotNumberlbl.AutoSize = True
        Me.LotNumberlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LotNumberlbl.Location = New System.Drawing.Point(48, 302)
        Me.LotNumberlbl.Name = "LotNumberlbl"
        Me.LotNumberlbl.Size = New System.Drawing.Size(99, 19)
        Me.LotNumberlbl.TabIndex = 0
        Me.LotNumberlbl.Text = "Lot Number"
        '
        'txtBlockNumber
        '
        Me.txtBlockNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtBlockNumber.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBlockNumber.Location = New System.Drawing.Point(52, 262)
        Me.txtBlockNumber.Name = "txtBlockNumber"
        Me.txtBlockNumber.Size = New System.Drawing.Size(384, 26)
        Me.txtBlockNumber.TabIndex = 3
        '
        'BlockNumberlbl
        '
        Me.BlockNumberlbl.AutoSize = True
        Me.BlockNumberlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlockNumberlbl.Location = New System.Drawing.Point(48, 239)
        Me.BlockNumberlbl.Name = "BlockNumberlbl"
        Me.BlockNumberlbl.Size = New System.Drawing.Size(118, 19)
        Me.BlockNumberlbl.TabIndex = 0
        Me.BlockNumberlbl.Text = "Block Number"
        '
        'txtHouseNumber
        '
        Me.txtHouseNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtHouseNumber.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHouseNumber.Location = New System.Drawing.Point(52, 193)
        Me.txtHouseNumber.Name = "txtHouseNumber"
        Me.txtHouseNumber.Size = New System.Drawing.Size(384, 26)
        Me.txtHouseNumber.TabIndex = 2
        '
        'HouseNumberlbl
        '
        Me.HouseNumberlbl.AutoSize = True
        Me.HouseNumberlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HouseNumberlbl.Location = New System.Drawing.Point(48, 170)
        Me.HouseNumberlbl.Name = "HouseNumberlbl"
        Me.HouseNumberlbl.Size = New System.Drawing.Size(124, 19)
        Me.HouseNumberlbl.TabIndex = 0
        Me.HouseNumberlbl.Text = "House Number"
        '
        'txtHouseholdNumber
        '
        Me.txtHouseholdNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtHouseholdNumber.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHouseholdNumber.Location = New System.Drawing.Point(52, 130)
        Me.txtHouseholdNumber.Name = "txtHouseholdNumber"
        Me.txtHouseholdNumber.Size = New System.Drawing.Size(384, 26)
        Me.txtHouseholdNumber.TabIndex = 1
        '
        'HouseholdNumlbl
        '
        Me.HouseholdNumlbl.AutoSize = True
        Me.HouseholdNumlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HouseholdNumlbl.Location = New System.Drawing.Point(48, 107)
        Me.HouseholdNumlbl.Name = "HouseholdNumlbl"
        Me.HouseholdNumlbl.Size = New System.Drawing.Size(158, 19)
        Me.HouseholdNumlbl.TabIndex = 0
        Me.HouseholdNumlbl.Text = "Household Number"
        '
        'AddHouseholdlbl
        '
        Me.AddHouseholdlbl.AutoSize = True
        Me.AddHouseholdlbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddHouseholdlbl.Location = New System.Drawing.Point(20, 40)
        Me.AddHouseholdlbl.Name = "AddHouseholdlbl"
        Me.AddHouseholdlbl.Size = New System.Drawing.Size(218, 32)
        Me.AddHouseholdlbl.TabIndex = 0
        Me.AddHouseholdlbl.Text = "Add Household"
        '
        'HouseholdEdit_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "HouseholdEdit_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HouseholdEdit_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.ResidentInHouseholdDGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FamilyHeadsDGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LinePnl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents AddHouseholdlbl As Label
    Friend WithEvents txtProvince As TextBox
    Friend WithEvents Provincelbl As Label
    Friend WithEvents txtMunicipality As TextBox
    Friend WithEvents Municipalitylbl As Label
    Friend WithEvents txtBarangay As TextBox
    Friend WithEvents Barangaylbl As Label
    Friend WithEvents txtCompound As TextBox
    Friend WithEvents Compoundlbl As Label
    Friend WithEvents txtSubdivision As TextBox
    Friend WithEvents Subdivisionlbl As Label
    Friend WithEvents txtVillage As TextBox
    Friend WithEvents Villagelbl As Label
    Friend WithEvents txStreetName As TextBox
    Friend WithEvents StreetNamelbl As Label
    Friend WithEvents txtAreaNumber As TextBox
    Friend WithEvents AreaNumberlbl As Label
    Friend WithEvents txtLotNumber As TextBox
    Friend WithEvents LotNumberlbl As Label
    Friend WithEvents txtBlockNumber As TextBox
    Friend WithEvents BlockNumberlbl As Label
    Friend WithEvents txtHouseNumber As TextBox
    Friend WithEvents HouseNumberlbl As Label
    Friend WithEvents txtHouseholdNumber As TextBox
    Friend WithEvents HouseholdNumlbl As Label
    Friend WithEvents LinePnl1 As Panel
    Friend WithEvents LinePnl2 As Panel
    Friend WithEvents LinePnl5 As Panel
    Friend WithEvents ResidentsInTheHouseholdlbl As Label
    Friend WithEvents FamilyHeadsInTheHouseholdlbl As Label
    Friend WithEvents ResidentInHouseholdDGV As DataGridView
    Friend WithEvents FamilyHeadsDGV As DataGridView
    Friend WithEvents lblSearchFamHead As Label
    Friend WithEvents btnSearchHeads As Button
    Friend WithEvents txtSearchFamilyHeads As TextBox
    Friend WithEvents SearchResidentsLbl As Label
    Friend WithEvents BtnSearchResident As Button
    Friend WithEvents TxtSearchResidents As TextBox
    Friend WithEvents btnBack As Button
    Friend WithEvents BtnEditHousehold As Button
End Class
