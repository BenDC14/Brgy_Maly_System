<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HouseholdFamilyResidents_Form
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
        Me.LinePnl4 = New System.Windows.Forms.Panel()
        Me.LinePnl3 = New System.Windows.Forms.Panel()
        Me.LinePnl5 = New System.Windows.Forms.Panel()
        Me.LinePnl2 = New System.Windows.Forms.Panel()
        Me.txtSuffix = New System.Windows.Forms.TextBox()
        Me.Suffixlbl = New System.Windows.Forms.Label()
        Me.txtMiddleName = New System.Windows.Forms.TextBox()
        Me.MiddleNamelbl = New System.Windows.Forms.Label()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.FirstNamelbl = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.LastNamelbl = New System.Windows.Forms.Label()
        Me.BasicInfolbl = New System.Windows.Forms.Label()
        Me.LinePnl1 = New System.Windows.Forms.Panel()
        Me.FamilyResidentlbl = New System.Windows.Forms.Label()
        Me.LinePnl6 = New System.Windows.Forms.Panel()
        Me.PersonalDetailslbl = New System.Windows.Forms.Label()
        Me.ContactInfolbl = New System.Windows.Forms.Label()
        Me.HouseInfolbl = New System.Windows.Forms.Label()
        Me.Categorylbl = New System.Windows.Forms.Label()
        Me.DTPDateofBirth = New System.Windows.Forms.DateTimePicker()
        Me.DateofBirthlbl = New System.Windows.Forms.Label()
        Me.cbSex = New System.Windows.Forms.ComboBox()
        Me.Sexlbl = New System.Windows.Forms.Label()
        Me.cbCivilStatus = New System.Windows.Forms.ComboBox()
        Me.CivilStatuslbl = New System.Windows.Forms.Label()
        Me.txtEmailAddress = New System.Windows.Forms.TextBox()
        Me.txtContactNum = New System.Windows.Forms.TextBox()
        Me.EmailAddresslbl = New System.Windows.Forms.Label()
        Me.ContactNumberlbl = New System.Windows.Forms.Label()
        Me.txtHouseholdNumber = New System.Windows.Forms.TextBox()
        Me.Householdbl = New System.Windows.Forms.Label()
        Me.txtAdditionalInfo = New System.Windows.Forms.TextBox()
        Me.AdditionalInforlbl = New System.Windows.Forms.Label()
        Me.cbIndigenousPeople = New System.Windows.Forms.CheckBox()
        Me.cbInhabitant = New System.Windows.Forms.CheckBox()
        Me.cbHead = New System.Windows.Forms.CheckBox()
        Me.cbOutofSchoolChildren = New System.Windows.Forms.CheckBox()
        Me.cbOFW = New System.Windows.Forms.CheckBox()
        Me.cbUnemployed = New System.Windows.Forms.CheckBox()
        Me.cbEmployed = New System.Windows.Forms.CheckBox()
        Me.cbSoloParent = New System.Windows.Forms.CheckBox()
        Me.cbStudent = New System.Windows.Forms.CheckBox()
        Me.cbPWD = New System.Windows.Forms.CheckBox()
        Me.cbSeniorCitizen = New System.Windows.Forms.CheckBox()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.FillPanel.SuspendLayout()
        Me.LinePnl3.SuspendLayout()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnBack)
        Me.FillPanel.Controls.Add(Me.btnSave)
        Me.FillPanel.Controls.Add(Me.txtAdditionalInfo)
        Me.FillPanel.Controls.Add(Me.AdditionalInforlbl)
        Me.FillPanel.Controls.Add(Me.cbIndigenousPeople)
        Me.FillPanel.Controls.Add(Me.cbInhabitant)
        Me.FillPanel.Controls.Add(Me.cbHead)
        Me.FillPanel.Controls.Add(Me.cbOutofSchoolChildren)
        Me.FillPanel.Controls.Add(Me.cbOFW)
        Me.FillPanel.Controls.Add(Me.cbUnemployed)
        Me.FillPanel.Controls.Add(Me.cbEmployed)
        Me.FillPanel.Controls.Add(Me.cbSoloParent)
        Me.FillPanel.Controls.Add(Me.cbStudent)
        Me.FillPanel.Controls.Add(Me.cbPWD)
        Me.FillPanel.Controls.Add(Me.cbSeniorCitizen)
        Me.FillPanel.Controls.Add(Me.txtHouseholdNumber)
        Me.FillPanel.Controls.Add(Me.Householdbl)
        Me.FillPanel.Controls.Add(Me.txtEmailAddress)
        Me.FillPanel.Controls.Add(Me.txtContactNum)
        Me.FillPanel.Controls.Add(Me.EmailAddresslbl)
        Me.FillPanel.Controls.Add(Me.ContactNumberlbl)
        Me.FillPanel.Controls.Add(Me.cbCivilStatus)
        Me.FillPanel.Controls.Add(Me.CivilStatuslbl)
        Me.FillPanel.Controls.Add(Me.cbSex)
        Me.FillPanel.Controls.Add(Me.Sexlbl)
        Me.FillPanel.Controls.Add(Me.DTPDateofBirth)
        Me.FillPanel.Controls.Add(Me.DateofBirthlbl)
        Me.FillPanel.Controls.Add(Me.Categorylbl)
        Me.FillPanel.Controls.Add(Me.HouseInfolbl)
        Me.FillPanel.Controls.Add(Me.ContactInfolbl)
        Me.FillPanel.Controls.Add(Me.PersonalDetailslbl)
        Me.FillPanel.Controls.Add(Me.LinePnl6)
        Me.FillPanel.Controls.Add(Me.LinePnl4)
        Me.FillPanel.Controls.Add(Me.LinePnl3)
        Me.FillPanel.Controls.Add(Me.LinePnl2)
        Me.FillPanel.Controls.Add(Me.txtSuffix)
        Me.FillPanel.Controls.Add(Me.Suffixlbl)
        Me.FillPanel.Controls.Add(Me.txtMiddleName)
        Me.FillPanel.Controls.Add(Me.MiddleNamelbl)
        Me.FillPanel.Controls.Add(Me.txtFirstName)
        Me.FillPanel.Controls.Add(Me.FirstNamelbl)
        Me.FillPanel.Controls.Add(Me.txtLastName)
        Me.FillPanel.Controls.Add(Me.LastNamelbl)
        Me.FillPanel.Controls.Add(Me.BasicInfolbl)
        Me.FillPanel.Controls.Add(Me.LinePnl1)
        Me.FillPanel.Controls.Add(Me.FamilyResidentlbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'LinePnl4
        '
        Me.LinePnl4.BackColor = System.Drawing.Color.Black
        Me.LinePnl4.Location = New System.Drawing.Point(0, 716)
        Me.LinePnl4.Name = "LinePnl4"
        Me.LinePnl4.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl4.TabIndex = 5
        '
        'LinePnl3
        '
        Me.LinePnl3.BackColor = System.Drawing.Color.Black
        Me.LinePnl3.Controls.Add(Me.LinePnl5)
        Me.LinePnl3.Location = New System.Drawing.Point(837, 417)
        Me.LinePnl3.Name = "LinePnl3"
        Me.LinePnl3.Size = New System.Drawing.Size(2, 300)
        Me.LinePnl3.TabIndex = 0
        '
        'LinePnl5
        '
        Me.LinePnl5.BackColor = System.Drawing.Color.Black
        Me.LinePnl5.Location = New System.Drawing.Point(-849, 501)
        Me.LinePnl5.Name = "LinePnl5"
        Me.LinePnl5.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl5.TabIndex = 1
        '
        'LinePnl2
        '
        Me.LinePnl2.BackColor = System.Drawing.Color.Black
        Me.LinePnl2.Location = New System.Drawing.Point(0, 415)
        Me.LinePnl2.Name = "LinePnl2"
        Me.LinePnl2.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl2.TabIndex = 0
        '
        'txtSuffix
        '
        Me.txtSuffix.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtSuffix.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuffix.Location = New System.Drawing.Point(44, 355)
        Me.txtSuffix.Name = "txtSuffix"
        Me.txtSuffix.Size = New System.Drawing.Size(1600, 29)
        Me.txtSuffix.TabIndex = 4
        '
        'Suffixlbl
        '
        Me.Suffixlbl.AutoSize = True
        Me.Suffixlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Suffixlbl.Location = New System.Drawing.Point(40, 333)
        Me.Suffixlbl.Name = "Suffixlbl"
        Me.Suffixlbl.Size = New System.Drawing.Size(53, 19)
        Me.Suffixlbl.TabIndex = 0
        Me.Suffixlbl.Text = "Suffix"
        '
        'txtMiddleName
        '
        Me.txtMiddleName.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtMiddleName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMiddleName.Location = New System.Drawing.Point(44, 291)
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.Size = New System.Drawing.Size(1600, 29)
        Me.txtMiddleName.TabIndex = 3
        '
        'MiddleNamelbl
        '
        Me.MiddleNamelbl.AutoSize = True
        Me.MiddleNamelbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MiddleNamelbl.Location = New System.Drawing.Point(40, 269)
        Me.MiddleNamelbl.Name = "MiddleNamelbl"
        Me.MiddleNamelbl.Size = New System.Drawing.Size(107, 19)
        Me.MiddleNamelbl.TabIndex = 0
        Me.MiddleNamelbl.Text = "Middle Name"
        '
        'txtFirstName
        '
        Me.txtFirstName.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtFirstName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstName.Location = New System.Drawing.Point(44, 224)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(1600, 29)
        Me.txtFirstName.TabIndex = 2
        '
        'FirstNamelbl
        '
        Me.FirstNamelbl.AutoSize = True
        Me.FirstNamelbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FirstNamelbl.Location = New System.Drawing.Point(40, 202)
        Me.FirstNamelbl.Name = "FirstNamelbl"
        Me.FirstNamelbl.Size = New System.Drawing.Size(91, 19)
        Me.FirstNamelbl.TabIndex = 0
        Me.FirstNamelbl.Text = "First Name"
        '
        'txtLastName
        '
        Me.txtLastName.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtLastName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastName.Location = New System.Drawing.Point(44, 158)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(1600, 29)
        Me.txtLastName.TabIndex = 1
        '
        'LastNamelbl
        '
        Me.LastNamelbl.AutoSize = True
        Me.LastNamelbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LastNamelbl.Location = New System.Drawing.Point(40, 136)
        Me.LastNamelbl.Name = "LastNamelbl"
        Me.LastNamelbl.Size = New System.Drawing.Size(90, 19)
        Me.LastNamelbl.TabIndex = 0
        Me.LastNamelbl.Text = "Last Name"
        '
        'BasicInfolbl
        '
        Me.BasicInfolbl.AutoSize = True
        Me.BasicInfolbl.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BasicInfolbl.Location = New System.Drawing.Point(748, 97)
        Me.BasicInfolbl.Name = "BasicInfolbl"
        Me.BasicInfolbl.Size = New System.Drawing.Size(187, 24)
        Me.BasicInfolbl.TabIndex = 0
        Me.BasicInfolbl.Text = "Basic Information"
        '
        'LinePnl1
        '
        Me.LinePnl1.BackColor = System.Drawing.Color.Black
        Me.LinePnl1.Location = New System.Drawing.Point(0, 75)
        Me.LinePnl1.Name = "LinePnl1"
        Me.LinePnl1.Size = New System.Drawing.Size(1700, 2)
        Me.LinePnl1.TabIndex = 0
        '
        'FamilyResidentlbl
        '
        Me.FamilyResidentlbl.AutoSize = True
        Me.FamilyResidentlbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FamilyResidentlbl.Location = New System.Drawing.Point(20, 30)
        Me.FamilyResidentlbl.Name = "FamilyResidentlbl"
        Me.FamilyResidentlbl.Size = New System.Drawing.Size(227, 32)
        Me.FamilyResidentlbl.TabIndex = 0
        Me.FamilyResidentlbl.Text = "Family Resident"
        '
        'LinePnl6
        '
        Me.LinePnl6.BackColor = System.Drawing.Color.Black
        Me.LinePnl6.Location = New System.Drawing.Point(837, 620)
        Me.LinePnl6.Name = "LinePnl6"
        Me.LinePnl6.Size = New System.Drawing.Size(860, 2)
        Me.LinePnl6.TabIndex = 6
        '
        'PersonalDetailslbl
        '
        Me.PersonalDetailslbl.AutoSize = True
        Me.PersonalDetailslbl.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PersonalDetailslbl.Location = New System.Drawing.Point(290, 434)
        Me.PersonalDetailslbl.Name = "PersonalDetailslbl"
        Me.PersonalDetailslbl.Size = New System.Drawing.Size(173, 24)
        Me.PersonalDetailslbl.TabIndex = 0
        Me.PersonalDetailslbl.Text = "Personal Details"
        '
        'ContactInfolbl
        '
        Me.ContactInfolbl.AutoSize = True
        Me.ContactInfolbl.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContactInfolbl.Location = New System.Drawing.Point(1165, 434)
        Me.ContactInfolbl.Name = "ContactInfolbl"
        Me.ContactInfolbl.Size = New System.Drawing.Size(210, 24)
        Me.ContactInfolbl.TabIndex = 0
        Me.ContactInfolbl.Text = "Contact Information"
        '
        'HouseInfolbl
        '
        Me.HouseInfolbl.AutoSize = True
        Me.HouseInfolbl.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HouseInfolbl.Location = New System.Drawing.Point(1150, 635)
        Me.HouseInfolbl.Name = "HouseInfolbl"
        Me.HouseInfolbl.Size = New System.Drawing.Size(239, 24)
        Me.HouseInfolbl.TabIndex = 0
        Me.HouseInfolbl.Text = "Household Information"
        '
        'Categorylbl
        '
        Me.Categorylbl.AutoSize = True
        Me.Categorylbl.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Categorylbl.Location = New System.Drawing.Point(748, 730)
        Me.Categorylbl.Name = "Categorylbl"
        Me.Categorylbl.Size = New System.Drawing.Size(101, 24)
        Me.Categorylbl.TabIndex = 0
        Me.Categorylbl.Text = "Category"
        '
        'DTPDateofBirth
        '
        Me.DTPDateofBirth.CalendarFont = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateofBirth.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.DTPDateofBirth.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDateofBirth.Location = New System.Drawing.Point(44, 488)
        Me.DTPDateofBirth.Name = "DTPDateofBirth"
        Me.DTPDateofBirth.Size = New System.Drawing.Size(769, 26)
        Me.DTPDateofBirth.TabIndex = 5
        '
        'DateofBirthlbl
        '
        Me.DateofBirthlbl.AutoSize = True
        Me.DateofBirthlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateofBirthlbl.Location = New System.Drawing.Point(40, 460)
        Me.DateofBirthlbl.Name = "DateofBirthlbl"
        Me.DateofBirthlbl.Size = New System.Drawing.Size(104, 19)
        Me.DateofBirthlbl.TabIndex = 0
        Me.DateofBirthlbl.Text = "Date of Birth"
        '
        'cbSex
        '
        Me.cbSex.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbSex.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSex.FormattingEnabled = True
        Me.cbSex.Items.AddRange(New Object() {"Male", "Female"})
        Me.cbSex.Location = New System.Drawing.Point(44, 558)
        Me.cbSex.Name = "cbSex"
        Me.cbSex.Size = New System.Drawing.Size(769, 26)
        Me.cbSex.TabIndex = 6
        '
        'Sexlbl
        '
        Me.Sexlbl.AutoSize = True
        Me.Sexlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Sexlbl.Location = New System.Drawing.Point(40, 535)
        Me.Sexlbl.Name = "Sexlbl"
        Me.Sexlbl.Size = New System.Drawing.Size(38, 19)
        Me.Sexlbl.TabIndex = 0
        Me.Sexlbl.Text = "Sex"
        '
        'cbCivilStatus
        '
        Me.cbCivilStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.cbCivilStatus.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCivilStatus.FormattingEnabled = True
        Me.cbCivilStatus.Items.AddRange(New Object() {"Single", "Married"})
        Me.cbCivilStatus.Location = New System.Drawing.Point(44, 634)
        Me.cbCivilStatus.Name = "cbCivilStatus"
        Me.cbCivilStatus.Size = New System.Drawing.Size(769, 26)
        Me.cbCivilStatus.TabIndex = 7
        '
        'CivilStatuslbl
        '
        Me.CivilStatuslbl.AutoSize = True
        Me.CivilStatuslbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CivilStatuslbl.Location = New System.Drawing.Point(40, 611)
        Me.CivilStatuslbl.Name = "CivilStatuslbl"
        Me.CivilStatuslbl.Size = New System.Drawing.Size(95, 19)
        Me.CivilStatuslbl.TabIndex = 0
        Me.CivilStatuslbl.Text = "Civil Status"
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtEmailAddress.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailAddress.Location = New System.Drawing.Point(871, 558)
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Size = New System.Drawing.Size(773, 26)
        Me.txtEmailAddress.TabIndex = 9
        '
        'txtContactNum
        '
        Me.txtContactNum.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtContactNum.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactNum.Location = New System.Drawing.Point(871, 488)
        Me.txtContactNum.Name = "txtContactNum"
        Me.txtContactNum.Size = New System.Drawing.Size(773, 26)
        Me.txtContactNum.TabIndex = 8
        '
        'EmailAddresslbl
        '
        Me.EmailAddresslbl.AutoSize = True
        Me.EmailAddresslbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmailAddresslbl.Location = New System.Drawing.Point(867, 536)
        Me.EmailAddresslbl.Name = "EmailAddresslbl"
        Me.EmailAddresslbl.Size = New System.Drawing.Size(118, 19)
        Me.EmailAddresslbl.TabIndex = 0
        Me.EmailAddresslbl.Text = "Email Address"
        '
        'ContactNumberlbl
        '
        Me.ContactNumberlbl.AutoSize = True
        Me.ContactNumberlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContactNumberlbl.Location = New System.Drawing.Point(867, 467)
        Me.ContactNumberlbl.Name = "ContactNumberlbl"
        Me.ContactNumberlbl.Size = New System.Drawing.Size(134, 19)
        Me.ContactNumberlbl.TabIndex = 0
        Me.ContactNumberlbl.Text = "Contact Number"
        '
        'txtHouseholdNumber
        '
        Me.txtHouseholdNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtHouseholdNumber.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHouseholdNumber.Location = New System.Drawing.Point(871, 675)
        Me.txtHouseholdNumber.Name = "txtHouseholdNumber"
        Me.txtHouseholdNumber.Size = New System.Drawing.Size(773, 26)
        Me.txtHouseholdNumber.TabIndex = 10
        '
        'Householdbl
        '
        Me.Householdbl.AutoSize = True
        Me.Householdbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Householdbl.Location = New System.Drawing.Point(867, 654)
        Me.Householdbl.Name = "Householdbl"
        Me.Householdbl.Size = New System.Drawing.Size(184, 19)
        Me.Householdbl.TabIndex = 19
        Me.Householdbl.Text = "Household Information"
        '
        'txtAdditionalInfo
        '
        Me.txtAdditionalInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtAdditionalInfo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdditionalInfo.Location = New System.Drawing.Point(44, 853)
        Me.txtAdditionalInfo.Name = "txtAdditionalInfo"
        Me.txtAdditionalInfo.Size = New System.Drawing.Size(1600, 26)
        Me.txtAdditionalInfo.TabIndex = 22
        '
        'AdditionalInforlbl
        '
        Me.AdditionalInforlbl.AutoSize = True
        Me.AdditionalInforlbl.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdditionalInforlbl.Location = New System.Drawing.Point(40, 832)
        Me.AdditionalInforlbl.Name = "AdditionalInforlbl"
        Me.AdditionalInforlbl.Size = New System.Drawing.Size(177, 19)
        Me.AdditionalInforlbl.TabIndex = 0
        Me.AdditionalInforlbl.Text = "Additional Information"
        '
        'cbIndigenousPeople
        '
        Me.cbIndigenousPeople.AutoSize = True
        Me.cbIndigenousPeople.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbIndigenousPeople.Location = New System.Drawing.Point(1495, 787)
        Me.cbIndigenousPeople.Name = "cbIndigenousPeople"
        Me.cbIndigenousPeople.Size = New System.Drawing.Size(171, 23)
        Me.cbIndigenousPeople.TabIndex = 21
        Me.cbIndigenousPeople.Text = "Indigenous People"
        Me.cbIndigenousPeople.UseVisualStyleBackColor = True
        '
        'cbInhabitant
        '
        Me.cbInhabitant.AutoSize = True
        Me.cbInhabitant.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbInhabitant.Location = New System.Drawing.Point(1360, 787)
        Me.cbInhabitant.Name = "cbInhabitant"
        Me.cbInhabitant.Size = New System.Drawing.Size(104, 23)
        Me.cbInhabitant.TabIndex = 20
        Me.cbInhabitant.Text = "Inhabitant"
        Me.cbInhabitant.UseVisualStyleBackColor = True
        '
        'cbHead
        '
        Me.cbHead.AutoSize = True
        Me.cbHead.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbHead.Location = New System.Drawing.Point(1243, 787)
        Me.cbHead.Name = "cbHead"
        Me.cbHead.Size = New System.Drawing.Size(68, 23)
        Me.cbHead.TabIndex = 19
        Me.cbHead.Text = "Head"
        Me.cbHead.UseVisualStyleBackColor = True
        '
        'cbOutofSchoolChildren
        '
        Me.cbOutofSchoolChildren.AutoSize = True
        Me.cbOutofSchoolChildren.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOutofSchoolChildren.Location = New System.Drawing.Point(997, 787)
        Me.cbOutofSchoolChildren.Name = "cbOutofSchoolChildren"
        Me.cbOutofSchoolChildren.Size = New System.Drawing.Size(201, 23)
        Me.cbOutofSchoolChildren.TabIndex = 18
        Me.cbOutofSchoolChildren.Text = "Out of School Children"
        Me.cbOutofSchoolChildren.UseVisualStyleBackColor = True
        '
        'cbOFW
        '
        Me.cbOFW.AutoSize = True
        Me.cbOFW.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOFW.Location = New System.Drawing.Point(900, 787)
        Me.cbOFW.Name = "cbOFW"
        Me.cbOFW.Size = New System.Drawing.Size(65, 23)
        Me.cbOFW.TabIndex = 17
        Me.cbOFW.Text = "OFW"
        Me.cbOFW.UseVisualStyleBackColor = True
        '
        'cbUnemployed
        '
        Me.cbUnemployed.AutoSize = True
        Me.cbUnemployed.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUnemployed.Location = New System.Drawing.Point(733, 787)
        Me.cbUnemployed.Name = "cbUnemployed"
        Me.cbUnemployed.Size = New System.Drawing.Size(125, 23)
        Me.cbUnemployed.TabIndex = 16
        Me.cbUnemployed.Text = "Unemployed"
        Me.cbUnemployed.UseVisualStyleBackColor = True
        '
        'cbEmployed
        '
        Me.cbEmployed.AutoSize = True
        Me.cbEmployed.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEmployed.Location = New System.Drawing.Point(600, 787)
        Me.cbEmployed.Name = "cbEmployed"
        Me.cbEmployed.Size = New System.Drawing.Size(105, 23)
        Me.cbEmployed.TabIndex = 15
        Me.cbEmployed.Text = "Employed"
        Me.cbEmployed.UseVisualStyleBackColor = True
        '
        'cbSoloParent
        '
        Me.cbSoloParent.AutoSize = True
        Me.cbSoloParent.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSoloParent.Location = New System.Drawing.Point(443, 787)
        Me.cbSoloParent.Name = "cbSoloParent"
        Me.cbSoloParent.Size = New System.Drawing.Size(117, 23)
        Me.cbSoloParent.TabIndex = 14
        Me.cbSoloParent.Text = "Solo Parent"
        Me.cbSoloParent.UseVisualStyleBackColor = True
        '
        'cbStudent
        '
        Me.cbStudent.AutoSize = True
        Me.cbStudent.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbStudent.Location = New System.Drawing.Point(310, 787)
        Me.cbStudent.Name = "cbStudent"
        Me.cbStudent.Size = New System.Drawing.Size(88, 23)
        Me.cbStudent.TabIndex = 13
        Me.cbStudent.Text = "Student"
        Me.cbStudent.UseVisualStyleBackColor = True
        '
        'cbPWD
        '
        Me.cbPWD.AutoSize = True
        Me.cbPWD.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPWD.Location = New System.Drawing.Point(202, 787)
        Me.cbPWD.Name = "cbPWD"
        Me.cbPWD.Size = New System.Drawing.Size(66, 23)
        Me.cbPWD.TabIndex = 12
        Me.cbPWD.Text = "PWD"
        Me.cbPWD.UseVisualStyleBackColor = True
        '
        'cbSeniorCitizen
        '
        Me.cbSeniorCitizen.AutoSize = True
        Me.cbSeniorCitizen.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSeniorCitizen.Location = New System.Drawing.Point(27, 787)
        Me.cbSeniorCitizen.Name = "cbSeniorCitizen"
        Me.cbSeniorCitizen.Size = New System.Drawing.Size(135, 23)
        Me.cbSeniorCitizen.TabIndex = 11
        Me.cbSeniorCitizen.Text = "Senior Citizen"
        Me.cbSeniorCitizen.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(871, 918)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(199, 44)
        Me.btnBack.TabIndex = 24
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(614, 918)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(199, 44)
        Me.btnSave.TabIndex = 23
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'HouseholdFamilyResidents_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "HouseholdFamilyResidents_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HouseholdFamilyResidents_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        Me.LinePnl3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents FamilyResidentlbl As Label
    Friend WithEvents BasicInfolbl As Label
    Friend WithEvents LinePnl1 As Panel
    Friend WithEvents txtSuffix As TextBox
    Friend WithEvents Suffixlbl As Label
    Friend WithEvents txtMiddleName As TextBox
    Friend WithEvents MiddleNamelbl As Label
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents FirstNamelbl As Label
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents LastNamelbl As Label
    Friend WithEvents LinePnl2 As Panel
    Friend WithEvents LinePnl3 As Panel
    Friend WithEvents LinePnl5 As Panel
    Friend WithEvents LinePnl4 As Panel
    Friend WithEvents LinePnl6 As Panel
    Friend WithEvents Categorylbl As Label
    Friend WithEvents HouseInfolbl As Label
    Friend WithEvents ContactInfolbl As Label
    Friend WithEvents PersonalDetailslbl As Label
    Friend WithEvents DTPDateofBirth As DateTimePicker
    Friend WithEvents DateofBirthlbl As Label
    Friend WithEvents cbSex As ComboBox
    Friend WithEvents Sexlbl As Label
    Friend WithEvents cbCivilStatus As ComboBox
    Friend WithEvents CivilStatuslbl As Label
    Friend WithEvents txtEmailAddress As TextBox
    Friend WithEvents txtContactNum As TextBox
    Friend WithEvents EmailAddresslbl As Label
    Friend WithEvents ContactNumberlbl As Label
    Friend WithEvents txtHouseholdNumber As TextBox
    Friend WithEvents Householdbl As Label
    Friend WithEvents txtAdditionalInfo As TextBox
    Friend WithEvents AdditionalInforlbl As Label
    Friend WithEvents cbIndigenousPeople As CheckBox
    Friend WithEvents cbInhabitant As CheckBox
    Friend WithEvents cbHead As CheckBox
    Friend WithEvents cbOutofSchoolChildren As CheckBox
    Friend WithEvents cbOFW As CheckBox
    Friend WithEvents cbUnemployed As CheckBox
    Friend WithEvents cbEmployed As CheckBox
    Friend WithEvents cbSoloParent As CheckBox
    Friend WithEvents cbStudent As CheckBox
    Friend WithEvents cbPWD As CheckBox
    Friend WithEvents cbSeniorCitizen As CheckBox
    Friend WithEvents btnBack As Button
    Friend WithEvents btnSave As Button
End Class
