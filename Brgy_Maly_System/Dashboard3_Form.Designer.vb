<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Dashboard3_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dashboard3_Form))
        Me.FillPanel = New System.Windows.Forms.Panel()
        Me.LeftButtonPB = New System.Windows.Forms.PictureBox()
        Me.VisionInfoLbl = New System.Windows.Forms.Label()
        Me.VisionLbl = New System.Windows.Forms.Label()
        Me.MissionInfoLbl = New System.Windows.Forms.Label()
        Me.BrgyMalyNameLbl = New System.Windows.Forms.Label()
        Me.BrgyLogoPic = New System.Windows.Forms.PictureBox()
        Me.TitleMissionLbl = New System.Windows.Forms.Label()
        Me.TitleLbl = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        CType(Me.LeftButtonPB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BrgyLogoPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.LeftButtonPB)
        Me.FillPanel.Controls.Add(Me.VisionInfoLbl)
        Me.FillPanel.Controls.Add(Me.VisionLbl)
        Me.FillPanel.Controls.Add(Me.MissionInfoLbl)
        Me.FillPanel.Controls.Add(Me.BrgyMalyNameLbl)
        Me.FillPanel.Controls.Add(Me.BrgyLogoPic)
        Me.FillPanel.Controls.Add(Me.TitleMissionLbl)
        Me.FillPanel.Controls.Add(Me.TitleLbl)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'LeftButtonPB
        '
        Me.LeftButtonPB.Image = Global.Brgy_Maly_System.My.Resources.Resources.arrowLeft
        Me.LeftButtonPB.Location = New System.Drawing.Point(12, 431)
        Me.LeftButtonPB.Name = "LeftButtonPB"
        Me.LeftButtonPB.Size = New System.Drawing.Size(100, 76)
        Me.LeftButtonPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LeftButtonPB.TabIndex = 20
        Me.LeftButtonPB.TabStop = False
        '
        'VisionInfoLbl
        '
        Me.VisionInfoLbl.AutoSize = True
        Me.VisionInfoLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.VisionInfoLbl.Font = New System.Drawing.Font("Yu Gothic Medium", 24.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VisionInfoLbl.Location = New System.Drawing.Point(201, 749)
        Me.VisionInfoLbl.Name = "VisionInfoLbl"
        Me.VisionInfoLbl.Size = New System.Drawing.Size(1215, 84)
        Me.VisionInfoLbl.TabIndex = 7
        Me.VisionInfoLbl.Text = "A healthy, empowered and environemental sustainable barangay where the " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "youth ar" &
    "e active partners in buildinga vibrant and resilient community."
        '
        'VisionLbl
        '
        Me.VisionLbl.AutoSize = True
        Me.VisionLbl.Font = New System.Drawing.Font("Arial", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VisionLbl.Location = New System.Drawing.Point(130, 690)
        Me.VisionLbl.Name = "VisionLbl"
        Me.VisionLbl.Size = New System.Drawing.Size(118, 41)
        Me.VisionLbl.TabIndex = 6
        Me.VisionLbl.Text = "Vision"
        '
        'MissionInfoLbl
        '
        Me.MissionInfoLbl.AutoSize = True
        Me.MissionInfoLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MissionInfoLbl.Font = New System.Drawing.Font("Yu Gothic Medium", 24.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MissionInfoLbl.Location = New System.Drawing.Point(201, 450)
        Me.MissionInfoLbl.Name = "MissionInfoLbl"
        Me.MissionInfoLbl.Size = New System.Drawing.Size(1362, 168)
        Me.MissionInfoLbl.TabIndex = 5
        Me.MissionInfoLbl.Text = resources.GetString("MissionInfoLbl.Text")
        '
        'BrgyMalyNameLbl
        '
        Me.BrgyMalyNameLbl.AutoSize = True
        Me.BrgyMalyNameLbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrgyMalyNameLbl.Location = New System.Drawing.Point(785, 319)
        Me.BrgyMalyNameLbl.Name = "BrgyMalyNameLbl"
        Me.BrgyMalyNameLbl.Size = New System.Drawing.Size(206, 32)
        Me.BrgyMalyNameLbl.TabIndex = 4
        Me.BrgyMalyNameLbl.Text = "Barangay Maly"
        '
        'BrgyLogoPic
        '
        Me.BrgyLogoPic.Image = Global.Brgy_Maly_System.My.Resources.Resources.LogoForMaly
        Me.BrgyLogoPic.Location = New System.Drawing.Point(772, 89)
        Me.BrgyLogoPic.Name = "BrgyLogoPic"
        Me.BrgyLogoPic.Size = New System.Drawing.Size(230, 218)
        Me.BrgyLogoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.BrgyLogoPic.TabIndex = 3
        Me.BrgyLogoPic.TabStop = False
        '
        'TitleMissionLbl
        '
        Me.TitleMissionLbl.AutoSize = True
        Me.TitleMissionLbl.Font = New System.Drawing.Font("Arial", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleMissionLbl.Location = New System.Drawing.Point(130, 397)
        Me.TitleMissionLbl.Name = "TitleMissionLbl"
        Me.TitleMissionLbl.Size = New System.Drawing.Size(144, 41)
        Me.TitleMissionLbl.TabIndex = 2
        Me.TitleMissionLbl.Text = "Mission"
        '
        'TitleLbl
        '
        Me.TitleLbl.AutoSize = True
        Me.TitleLbl.Font = New System.Drawing.Font("Arial", 27.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLbl.ForeColor = System.Drawing.Color.Black
        Me.TitleLbl.Location = New System.Drawing.Point(698, 18)
        Me.TitleLbl.Name = "TitleLbl"
        Me.TitleLbl.Size = New System.Drawing.Size(392, 43)
        Me.TitleLbl.TabIndex = 1
        Me.TitleLbl.Text = "Barangay Information"
        '
        'Dashboard3_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Dashboard3_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard3_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.LeftButtonPB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BrgyLogoPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents TitleLbl As Label
    Friend WithEvents BrgyMalyNameLbl As Label
    Friend WithEvents BrgyLogoPic As PictureBox
    Friend WithEvents TitleMissionLbl As Label
    Friend WithEvents MissionInfoLbl As Label
    Friend WithEvents VisionInfoLbl As Label
    Friend WithEvents VisionLbl As Label
    Friend WithEvents LeftButtonPB As PictureBox
End Class
