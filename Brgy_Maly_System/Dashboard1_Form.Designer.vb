<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Dashboard1_Form
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
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend6 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.FillPanel = New System.Windows.Forms.Panel()
        Me.ResidentPnl = New System.Windows.Forms.Panel()
        Me.TotalResidentsLbl = New System.Windows.Forms.Label()
        Me.LblResidents = New System.Windows.Forms.Label()
        Me.SummaryDashboard = New System.Windows.Forms.Label()
        Me.HouseholdPnl = New System.Windows.Forms.Panel()
        Me.TotalHouseholdLbl = New System.Windows.Forms.Label()
        Me.LblHousehold = New System.Windows.Forms.Label()
        Me.StudentPnl = New System.Windows.Forms.Panel()
        Me.TotalStudentLbl = New System.Windows.Forms.Label()
        Me.LblStudents = New System.Windows.Forms.Label()
        Me.SeniorsPnl = New System.Windows.Forms.Panel()
        Me.TotalSeniorLbl = New System.Windows.Forms.Label()
        Me.LblSenior = New System.Windows.Forms.Label()
        Me.PwdPnl = New System.Windows.Forms.Panel()
        Me.TotalPwdLbl = New System.Windows.Forms.Label()
        Me.LblPWD = New System.Windows.Forms.Label()
        Me.NextBtn = New System.Windows.Forms.PictureBox()
        Me.AgePnl = New System.Windows.Forms.Panel()
        Me.PopulationAgeLbl = New System.Windows.Forms.Label()
        Me.AgeChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.SexPnl = New System.Windows.Forms.Panel()
        Me.PopulationSexLbl = New System.Windows.Forms.Label()
        Me.SexChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.PopulationStatistics = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        Me.ResidentPnl.SuspendLayout()
        Me.HouseholdPnl.SuspendLayout()
        Me.StudentPnl.SuspendLayout()
        Me.SeniorsPnl.SuspendLayout()
        Me.PwdPnl.SuspendLayout()
        CType(Me.NextBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AgePnl.SuspendLayout()
        CType(Me.AgeChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SexPnl.SuspendLayout()
        CType(Me.SexChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.ResidentPnl)
        Me.FillPanel.Controls.Add(Me.SummaryDashboard)
        Me.FillPanel.Controls.Add(Me.HouseholdPnl)
        Me.FillPanel.Controls.Add(Me.StudentPnl)
        Me.FillPanel.Controls.Add(Me.SeniorsPnl)
        Me.FillPanel.Controls.Add(Me.PwdPnl)
        Me.FillPanel.Controls.Add(Me.NextBtn)
        Me.FillPanel.Controls.Add(Me.AgePnl)
        Me.FillPanel.Controls.Add(Me.SexPnl)
        Me.FillPanel.Controls.Add(Me.PopulationStatistics)
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'ResidentPnl
        '
        Me.ResidentPnl.BackColor = System.Drawing.Color.SeaShell
        Me.ResidentPnl.Controls.Add(Me.TotalResidentsLbl)
        Me.ResidentPnl.Controls.Add(Me.LblResidents)
        Me.ResidentPnl.Location = New System.Drawing.Point(43, 145)
        Me.ResidentPnl.Name = "ResidentPnl"
        Me.ResidentPnl.Size = New System.Drawing.Size(256, 188)
        Me.ResidentPnl.TabIndex = 26
        '
        'TotalResidentsLbl
        '
        Me.TotalResidentsLbl.AutoSize = True
        Me.TotalResidentsLbl.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalResidentsLbl.Location = New System.Drawing.Point(91, 62)
        Me.TotalResidentsLbl.Name = "TotalResidentsLbl"
        Me.TotalResidentsLbl.Size = New System.Drawing.Size(78, 56)
        Me.TotalResidentsLbl.TabIndex = 1
        Me.TotalResidentsLbl.Text = "67"
        '
        'LblResidents
        '
        Me.LblResidents.AutoSize = True
        Me.LblResidents.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblResidents.Location = New System.Drawing.Point(30, 135)
        Me.LblResidents.Name = "LblResidents"
        Me.LblResidents.Size = New System.Drawing.Size(127, 29)
        Me.LblResidents.TabIndex = 0
        Me.LblResidents.Text = "Residents"
        '
        'SummaryDashboard
        '
        Me.SummaryDashboard.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SummaryDashboard.AutoSize = True
        Me.SummaryDashboard.BackColor = System.Drawing.Color.Transparent
        Me.SummaryDashboard.Font = New System.Drawing.Font("Arial Narrow", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SummaryDashboard.Location = New System.Drawing.Point(35, 54)
        Me.SummaryDashboard.Name = "SummaryDashboard"
        Me.SummaryDashboard.Size = New System.Drawing.Size(327, 43)
        Me.SummaryDashboard.TabIndex = 25
        Me.SummaryDashboard.Text = "Summary Dashboard"
        '
        'HouseholdPnl
        '
        Me.HouseholdPnl.BackColor = System.Drawing.Color.SeaShell
        Me.HouseholdPnl.Controls.Add(Me.TotalHouseholdLbl)
        Me.HouseholdPnl.Controls.Add(Me.LblHousehold)
        Me.HouseholdPnl.Location = New System.Drawing.Point(383, 145)
        Me.HouseholdPnl.Name = "HouseholdPnl"
        Me.HouseholdPnl.Size = New System.Drawing.Size(256, 188)
        Me.HouseholdPnl.TabIndex = 27
        '
        'TotalHouseholdLbl
        '
        Me.TotalHouseholdLbl.AutoSize = True
        Me.TotalHouseholdLbl.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalHouseholdLbl.Location = New System.Drawing.Point(94, 62)
        Me.TotalHouseholdLbl.Name = "TotalHouseholdLbl"
        Me.TotalHouseholdLbl.Size = New System.Drawing.Size(78, 56)
        Me.TotalHouseholdLbl.TabIndex = 2
        Me.TotalHouseholdLbl.Text = "67"
        '
        'LblHousehold
        '
        Me.LblHousehold.AutoSize = True
        Me.LblHousehold.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHousehold.Location = New System.Drawing.Point(34, 135)
        Me.LblHousehold.Name = "LblHousehold"
        Me.LblHousehold.Size = New System.Drawing.Size(138, 29)
        Me.LblHousehold.TabIndex = 1
        Me.LblHousehold.Text = "Household" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'StudentPnl
        '
        Me.StudentPnl.BackColor = System.Drawing.Color.SeaShell
        Me.StudentPnl.Controls.Add(Me.TotalStudentLbl)
        Me.StudentPnl.Controls.Add(Me.LblStudents)
        Me.StudentPnl.Location = New System.Drawing.Point(734, 145)
        Me.StudentPnl.Name = "StudentPnl"
        Me.StudentPnl.Size = New System.Drawing.Size(256, 188)
        Me.StudentPnl.TabIndex = 28
        '
        'TotalStudentLbl
        '
        Me.TotalStudentLbl.AutoSize = True
        Me.TotalStudentLbl.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalStudentLbl.Location = New System.Drawing.Point(92, 62)
        Me.TotalStudentLbl.Name = "TotalStudentLbl"
        Me.TotalStudentLbl.Size = New System.Drawing.Size(78, 56)
        Me.TotalStudentLbl.TabIndex = 3
        Me.TotalStudentLbl.Text = "67"
        '
        'LblStudents
        '
        Me.LblStudents.AutoSize = True
        Me.LblStudents.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStudents.Location = New System.Drawing.Point(18, 135)
        Me.LblStudents.Name = "LblStudents"
        Me.LblStudents.Size = New System.Drawing.Size(103, 29)
        Me.LblStudents.TabIndex = 2
        Me.LblStudents.Text = "Student"
        '
        'SeniorsPnl
        '
        Me.SeniorsPnl.BackColor = System.Drawing.Color.SeaShell
        Me.SeniorsPnl.Controls.Add(Me.TotalSeniorLbl)
        Me.SeniorsPnl.Controls.Add(Me.LblSenior)
        Me.SeniorsPnl.Location = New System.Drawing.Point(1074, 145)
        Me.SeniorsPnl.Name = "SeniorsPnl"
        Me.SeniorsPnl.Size = New System.Drawing.Size(256, 188)
        Me.SeniorsPnl.TabIndex = 29
        '
        'TotalSeniorLbl
        '
        Me.TotalSeniorLbl.AutoSize = True
        Me.TotalSeniorLbl.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalSeniorLbl.Location = New System.Drawing.Point(89, 62)
        Me.TotalSeniorLbl.Name = "TotalSeniorLbl"
        Me.TotalSeniorLbl.Size = New System.Drawing.Size(78, 56)
        Me.TotalSeniorLbl.TabIndex = 4
        Me.TotalSeniorLbl.Text = "67"
        '
        'LblSenior
        '
        Me.LblSenior.AutoSize = True
        Me.LblSenior.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSenior.Location = New System.Drawing.Point(22, 135)
        Me.LblSenior.Name = "LblSenior"
        Me.LblSenior.Size = New System.Drawing.Size(88, 29)
        Me.LblSenior.TabIndex = 3
        Me.LblSenior.Text = "Senior"
        '
        'PwdPnl
        '
        Me.PwdPnl.BackColor = System.Drawing.Color.SeaShell
        Me.PwdPnl.Controls.Add(Me.TotalPwdLbl)
        Me.PwdPnl.Controls.Add(Me.LblPWD)
        Me.PwdPnl.Location = New System.Drawing.Point(1409, 145)
        Me.PwdPnl.Name = "PwdPnl"
        Me.PwdPnl.Size = New System.Drawing.Size(256, 188)
        Me.PwdPnl.TabIndex = 30
        '
        'TotalPwdLbl
        '
        Me.TotalPwdLbl.AutoSize = True
        Me.TotalPwdLbl.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalPwdLbl.Location = New System.Drawing.Point(102, 62)
        Me.TotalPwdLbl.Name = "TotalPwdLbl"
        Me.TotalPwdLbl.Size = New System.Drawing.Size(78, 56)
        Me.TotalPwdLbl.TabIndex = 5
        Me.TotalPwdLbl.Text = "67"
        '
        'LblPWD
        '
        Me.LblPWD.AutoSize = True
        Me.LblPWD.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPWD.Location = New System.Drawing.Point(25, 135)
        Me.LblPWD.Name = "LblPWD"
        Me.LblPWD.Size = New System.Drawing.Size(69, 29)
        Me.LblPWD.TabIndex = 4
        Me.LblPWD.Text = "PWD"
        '
        'NextBtn
        '
        Me.NextBtn.Image = Global.Brgy_Maly_System.My.Resources.Resources.arrowRight
        Me.NextBtn.Location = New System.Drawing.Point(1588, 367)
        Me.NextBtn.Name = "NextBtn"
        Me.NextBtn.Size = New System.Drawing.Size(100, 76)
        Me.NextBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.NextBtn.TabIndex = 17
        Me.NextBtn.TabStop = False
        '
        'AgePnl
        '
        Me.AgePnl.BackColor = System.Drawing.Color.SeaShell
        Me.AgePnl.Controls.Add(Me.PopulationAgeLbl)
        Me.AgePnl.Controls.Add(Me.AgeChart)
        Me.AgePnl.Location = New System.Drawing.Point(119, 449)
        Me.AgePnl.Name = "AgePnl"
        Me.AgePnl.Size = New System.Drawing.Size(624, 535)
        Me.AgePnl.TabIndex = 13
        '
        'PopulationAgeLbl
        '
        Me.PopulationAgeLbl.AutoSize = True
        Me.PopulationAgeLbl.Font = New System.Drawing.Font("Arial", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PopulationAgeLbl.Location = New System.Drawing.Point(168, 433)
        Me.PopulationAgeLbl.Name = "PopulationAgeLbl"
        Me.PopulationAgeLbl.Size = New System.Drawing.Size(268, 34)
        Me.PopulationAgeLbl.TabIndex = 2
        Me.PopulationAgeLbl.Text = "Population by Age"
        '
        'AgeChart
        '
        ChartArea5.Name = "ChartArea1"
        Me.AgeChart.ChartAreas.Add(ChartArea5)
        Legend5.Name = "Legend1"
        Me.AgeChart.Legends.Add(Legend5)
        Me.AgeChart.Location = New System.Drawing.Point(86, 48)
        Me.AgeChart.Name = "AgeChart"
        Series5.ChartArea = "ChartArea1"
        Series5.Legend = "Legend1"
        Series5.Name = "Series1"
        Me.AgeChart.Series.Add(Series5)
        Me.AgeChart.Size = New System.Drawing.Size(480, 320)
        Me.AgeChart.TabIndex = 0
        Me.AgeChart.Text = "Chart1"
        '
        'SexPnl
        '
        Me.SexPnl.BackColor = System.Drawing.Color.SeaShell
        Me.SexPnl.Controls.Add(Me.PopulationSexLbl)
        Me.SexPnl.Controls.Add(Me.SexChart)
        Me.SexPnl.Location = New System.Drawing.Point(965, 449)
        Me.SexPnl.Name = "SexPnl"
        Me.SexPnl.Size = New System.Drawing.Size(624, 535)
        Me.SexPnl.TabIndex = 14
        '
        'PopulationSexLbl
        '
        Me.PopulationSexLbl.AutoSize = True
        Me.PopulationSexLbl.Font = New System.Drawing.Font("Arial", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PopulationSexLbl.Location = New System.Drawing.Point(178, 421)
        Me.PopulationSexLbl.Name = "PopulationSexLbl"
        Me.PopulationSexLbl.Size = New System.Drawing.Size(267, 34)
        Me.PopulationSexLbl.TabIndex = 3
        Me.PopulationSexLbl.Text = "Population by Sex"
        '
        'SexChart
        '
        ChartArea6.Name = "ChartArea1"
        Me.SexChart.ChartAreas.Add(ChartArea6)
        Legend6.Name = "Legend1"
        Me.SexChart.Legends.Add(Legend6)
        Me.SexChart.Location = New System.Drawing.Point(86, 48)
        Me.SexChart.Name = "SexChart"
        Series6.ChartArea = "ChartArea1"
        Series6.Legend = "Legend1"
        Series6.Name = "Series1"
        Me.SexChart.Series.Add(Series6)
        Me.SexChart.Size = New System.Drawing.Size(480, 320)
        Me.SexChart.TabIndex = 1
        Me.SexChart.Text = "Chart1"
        '
        'PopulationStatistics
        '
        Me.PopulationStatistics.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PopulationStatistics.AutoSize = True
        Me.PopulationStatistics.BackColor = System.Drawing.Color.Transparent
        Me.PopulationStatistics.Font = New System.Drawing.Font("Arial Narrow", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PopulationStatistics.Location = New System.Drawing.Point(35, 394)
        Me.PopulationStatistics.Name = "PopulationStatistics"
        Me.PopulationStatistics.Size = New System.Drawing.Size(319, 43)
        Me.PopulationStatistics.TabIndex = 12
        Me.PopulationStatistics.Text = "Population Statistics"
        '
        'Dashboard1_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Dashboard1_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard1_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        Me.ResidentPnl.ResumeLayout(False)
        Me.ResidentPnl.PerformLayout()
        Me.HouseholdPnl.ResumeLayout(False)
        Me.HouseholdPnl.PerformLayout()
        Me.StudentPnl.ResumeLayout(False)
        Me.StudentPnl.PerformLayout()
        Me.SeniorsPnl.ResumeLayout(False)
        Me.SeniorsPnl.PerformLayout()
        Me.PwdPnl.ResumeLayout(False)
        Me.PwdPnl.PerformLayout()
        CType(Me.NextBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AgePnl.ResumeLayout(False)
        Me.AgePnl.PerformLayout()
        CType(Me.AgeChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SexPnl.ResumeLayout(False)
        Me.SexPnl.PerformLayout()
        CType(Me.SexChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents AgePnl As Panel
    Friend WithEvents PopulationAgeLbl As Label
    Friend WithEvents AgeChart As DataVisualization.Charting.Chart
    Friend WithEvents SexPnl As Panel
    Friend WithEvents PopulationSexLbl As Label
    Friend WithEvents SexChart As DataVisualization.Charting.Chart
    Friend WithEvents PopulationStatistics As Label
    Friend WithEvents NextBtn As PictureBox
    Friend WithEvents ResidentPnl As Panel
    Friend WithEvents TotalResidentsLbl As Label
    Friend WithEvents LblResidents As Label
    Friend WithEvents SummaryDashboard As Label
    Friend WithEvents HouseholdPnl As Panel
    Friend WithEvents TotalHouseholdLbl As Label
    Friend WithEvents LblHousehold As Label
    Friend WithEvents StudentPnl As Panel
    Friend WithEvents TotalStudentLbl As Label
    Friend WithEvents LblStudents As Label
    Friend WithEvents SeniorsPnl As Panel
    Friend WithEvents TotalSeniorLbl As Label
    Friend WithEvents LblSenior As Label
    Friend WithEvents PwdPnl As Panel
    Friend WithEvents TotalPwdLbl As Label
    Friend WithEvents LblPWD As Label
End Class
