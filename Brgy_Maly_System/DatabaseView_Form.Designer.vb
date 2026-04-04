<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatabaseView_Form
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
        Me.btnBacktoMain = New System.Windows.Forms.Button()
        Me.txtErrorMessage = New System.Windows.Forms.TextBox()
        Me.lblErrorMessage = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.lblFilePath = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.txtPerformedBy = New System.Windows.Forms.TextBox()
        Me.lblPerformedBy = New System.Windows.Forms.Label()
        Me.txtDateAndTime = New System.Windows.Forms.TextBox()
        Me.lblDateTime = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtLogType = New System.Windows.Forms.TextBox()
        Me.lblLogType = New System.Windows.Forms.Label()
        Me.lblLogDetails = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnBacktoMain)
        Me.FillPanel.Controls.Add(Me.txtErrorMessage)
        Me.FillPanel.Controls.Add(Me.lblErrorMessage)
        Me.FillPanel.Controls.Add(Me.TextBox1)
        Me.FillPanel.Controls.Add(Me.lblFilePath)
        Me.FillPanel.Controls.Add(Me.TextBox2)
        Me.FillPanel.Controls.Add(Me.lblFileName)
        Me.FillPanel.Controls.Add(Me.txtPerformedBy)
        Me.FillPanel.Controls.Add(Me.lblPerformedBy)
        Me.FillPanel.Controls.Add(Me.txtDateAndTime)
        Me.FillPanel.Controls.Add(Me.lblDateTime)
        Me.FillPanel.Controls.Add(Me.txtStatus)
        Me.FillPanel.Controls.Add(Me.lblStatus)
        Me.FillPanel.Controls.Add(Me.txtLogType)
        Me.FillPanel.Controls.Add(Me.lblLogType)
        Me.FillPanel.Controls.Add(Me.lblLogDetails)
        Me.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FillPanel.Location = New System.Drawing.Point(0, 0)
        Me.FillPanel.Name = "FillPanel"
        Me.FillPanel.Size = New System.Drawing.Size(1700, 1004)
        Me.FillPanel.TabIndex = 0
        '
        'btnBacktoMain
        '
        Me.btnBacktoMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnBacktoMain.FlatAppearance.BorderSize = 0
        Me.btnBacktoMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBacktoMain.Font = New System.Drawing.Font("Arial Narrow", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBacktoMain.Location = New System.Drawing.Point(753, 924)
        Me.btnBacktoMain.Name = "btnBacktoMain"
        Me.btnBacktoMain.Size = New System.Drawing.Size(189, 42)
        Me.btnBacktoMain.TabIndex = 8
        Me.btnBacktoMain.Text = "Back To Main"
        Me.btnBacktoMain.UseVisualStyleBackColor = False
        '
        'txtErrorMessage
        '
        Me.txtErrorMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtErrorMessage.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtErrorMessage.Location = New System.Drawing.Point(89, 830)
        Me.txtErrorMessage.Name = "txtErrorMessage"
        Me.txtErrorMessage.ReadOnly = True
        Me.txtErrorMessage.Size = New System.Drawing.Size(1435, 29)
        Me.txtErrorMessage.TabIndex = 7
        '
        'lblErrorMessage
        '
        Me.lblErrorMessage.AutoSize = True
        Me.lblErrorMessage.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorMessage.Location = New System.Drawing.Point(85, 805)
        Me.lblErrorMessage.Name = "lblErrorMessage"
        Me.lblErrorMessage.Size = New System.Drawing.Size(148, 22)
        Me.lblErrorMessage.TabIndex = 0
        Me.lblErrorMessage.Text = "Error Message"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(89, 714)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(1435, 29)
        Me.TextBox1.TabIndex = 9
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePath.Location = New System.Drawing.Point(85, 689)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(90, 22)
        Me.lblFilePath.TabIndex = 0
        Me.lblFilePath.Text = "File Path"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.TextBox2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(89, 598)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(1435, 29)
        Me.TextBox2.TabIndex = 5
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileName.Location = New System.Drawing.Point(85, 573)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(100, 22)
        Me.lblFileName.TabIndex = 0
        Me.lblFileName.Text = "File Name"
        '
        'txtPerformedBy
        '
        Me.txtPerformedBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtPerformedBy.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPerformedBy.Location = New System.Drawing.Point(89, 484)
        Me.txtPerformedBy.Name = "txtPerformedBy"
        Me.txtPerformedBy.ReadOnly = True
        Me.txtPerformedBy.Size = New System.Drawing.Size(1435, 29)
        Me.txtPerformedBy.TabIndex = 4
        '
        'lblPerformedBy
        '
        Me.lblPerformedBy.AutoSize = True
        Me.lblPerformedBy.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerformedBy.Location = New System.Drawing.Point(85, 459)
        Me.lblPerformedBy.Name = "lblPerformedBy"
        Me.lblPerformedBy.Size = New System.Drawing.Size(138, 22)
        Me.lblPerformedBy.TabIndex = 0
        Me.lblPerformedBy.Text = "Performed By"
        '
        'txtDateAndTime
        '
        Me.txtDateAndTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtDateAndTime.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateAndTime.Location = New System.Drawing.Point(89, 370)
        Me.txtDateAndTime.Name = "txtDateAndTime"
        Me.txtDateAndTime.ReadOnly = True
        Me.txtDateAndTime.Size = New System.Drawing.Size(1435, 29)
        Me.txtDateAndTime.TabIndex = 3
        '
        'lblDateTime
        '
        Me.lblDateTime.AutoSize = True
        Me.lblDateTime.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateTime.Location = New System.Drawing.Point(85, 345)
        Me.lblDateTime.Name = "lblDateTime"
        Me.lblDateTime.Size = New System.Drawing.Size(143, 22)
        Me.lblDateTime.TabIndex = 0
        Me.lblDateTime.Text = "Date and Time"
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(89, 254)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(1435, 29)
        Me.txtStatus.TabIndex = 2
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(85, 229)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(69, 22)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Status"
        '
        'txtLogType
        '
        Me.txtLogType.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.txtLogType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLogType.Location = New System.Drawing.Point(89, 149)
        Me.txtLogType.Name = "txtLogType"
        Me.txtLogType.ReadOnly = True
        Me.txtLogType.Size = New System.Drawing.Size(1435, 29)
        Me.txtLogType.TabIndex = 1
        '
        'lblLogType
        '
        Me.lblLogType.AutoSize = True
        Me.lblLogType.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogType.Location = New System.Drawing.Point(85, 124)
        Me.lblLogType.Name = "lblLogType"
        Me.lblLogType.Size = New System.Drawing.Size(97, 22)
        Me.lblLogType.TabIndex = 0
        Me.lblLogType.Text = "Log Type"
        '
        'lblLogDetails
        '
        Me.lblLogDetails.AutoSize = True
        Me.lblLogDetails.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogDetails.Location = New System.Drawing.Point(38, 33)
        Me.lblLogDetails.Name = "lblLogDetails"
        Me.lblLogDetails.Size = New System.Drawing.Size(163, 32)
        Me.lblLogDetails.TabIndex = 0
        Me.lblLogDetails.Text = "Log Details"
        '
        'DatabaseView_Form
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DatabaseView_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DatabaseView_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents lblLogDetails As Label
    Friend WithEvents txtDateAndTime As TextBox
    Friend WithEvents lblDateTime As Label
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents txtLogType As TextBox
    Friend WithEvents lblLogType As Label
    Friend WithEvents txtErrorMessage As TextBox
    Friend WithEvents lblErrorMessage As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents lblFilePath As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents lblFileName As Label
    Friend WithEvents txtPerformedBy As TextBox
    Friend WithEvents lblPerformedBy As Label
    Friend WithEvents btnBacktoMain As Button
End Class
