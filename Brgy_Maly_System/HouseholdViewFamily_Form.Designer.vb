<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HouseholdViewFamily_Form
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
        Me.btnEditFamily = New System.Windows.Forms.Button()
        Me.dgvFamilyHeads = New System.Windows.Forms.DataGridView()
        Me.ViewFamilylbl = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout()
        CType(Me.dgvFamilyHeads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FillPanel
        '
        Me.FillPanel.BackColor = System.Drawing.Color.Transparent
        Me.FillPanel.Controls.Add(Me.btnBack)
        Me.FillPanel.Controls.Add(Me.btnEditFamily)
        Me.FillPanel.Controls.Add(Me.dgvFamilyHeads)
        Me.FillPanel.Controls.Add(Me.ViewFamilylbl)
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
        Me.btnBack.Location = New System.Drawing.Point(1208, 911)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(199, 44)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "Back To Main"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnEditFamily
        '
        Me.btnEditFamily.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnEditFamily.FlatAppearance.BorderSize = 0
        Me.btnEditFamily.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditFamily.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditFamily.Location = New System.Drawing.Point(270, 911)
        Me.btnEditFamily.Name = "btnEditFamily"
        Me.btnEditFamily.Size = New System.Drawing.Size(199, 44)
        Me.btnEditFamily.TabIndex = 2
        Me.btnEditFamily.Text = "Edit Family"
        Me.btnEditFamily.UseVisualStyleBackColor = False
        '
        'dgvFamilyHeads
        '
        Me.dgvFamilyHeads.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.dgvFamilyHeads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFamilyHeads.Location = New System.Drawing.Point(12, 92)
        Me.dgvFamilyHeads.Name = "dgvFamilyHeads"
        Me.dgvFamilyHeads.Size = New System.Drawing.Size(1676, 793)
        Me.dgvFamilyHeads.TabIndex = 1
        '
        'ViewFamilylbl
        '
        Me.ViewFamilylbl.AutoSize = True
        Me.ViewFamilylbl.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewFamilylbl.Location = New System.Drawing.Point(12, 27)
        Me.ViewFamilylbl.Name = "ViewFamilylbl"
        Me.ViewFamilylbl.Size = New System.Drawing.Size(173, 32)
        Me.ViewFamilylbl.TabIndex = 0
        Me.ViewFamilylbl.Text = "View Family"
        '
        'HouseholdViewFamily_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1700, 1004)
        Me.Controls.Add(Me.FillPanel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "HouseholdViewFamily_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HouseholdViewFamily_Form"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.dgvFamilyHeads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FillPanel As Panel
    Friend WithEvents ViewFamilylbl As Label
    Friend WithEvents dgvFamilyHeads As DataGridView
    Friend WithEvents btnBack As Button
    Friend WithEvents btnEditFamily As Button
End Class
