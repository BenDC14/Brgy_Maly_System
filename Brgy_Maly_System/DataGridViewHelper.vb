Imports System.Windows.Forms

''' <summary>
''' Helper class for DataGridView configuration
''' Reduces code duplication across forms
''' </summary>
Public Class DataGridViewHelper

    ''' <summary>
    ''' Apply standard styling to DataGridView
    ''' </summary>
    Public Shared Sub ApplyStandardStyling(dgv As DataGridView)
        dgv.AutoGenerateColumns = False
        dgv.Columns.Clear()
        dgv.ReadOnly = False
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.RowHeadersVisible = False

        ' === STYLING ===
        dgv.BackgroundColor = Color.FromArgb(181, 255, 124)
        dgv.GridColor = Color.FromArgb(180, 180, 180)
        dgv.ColumnHeadersHeight = 35
        dgv.RowTemplate.Height = 40

        ' === COLUMN HEADERS STYLING ===
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' === ROW STYLING ===
        dgv.DefaultCellStyle.Font = New Font("Arial", 10)
        dgv.DefaultCellStyle.ForeColor = Color.Black
        dgv.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgv.DefaultCellStyle.Padding = New Padding(5)

        ' === ALTERNATE ROW COLOR ===
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)

        ' === SELECTION STYLING ===
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        dgv.DefaultCellStyle.SelectionForeColor = Color.Black
    End Sub

    ''' <summary>
    ''' Add a text column to DataGridView
    ''' </summary>
    Public Shared Sub AddTextColumn(dgv As DataGridView, name As String, headerText As String,
                                    dataPropertyName As String, width As Integer, Optional isReadOnly As Boolean = True)
        dgv.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = name,
            .HeaderText = headerText,
            .DataPropertyName = dataPropertyName,
            .Width = width,
            .ReadOnly = isReadOnly
        })
    End Sub

    ''' <summary>
    ''' Add a button column to DataGridView
    ''' </summary>
    Public Shared Sub AddButtonColumn(dgv As DataGridView, name As String, headerText As String,
                                      buttonText As String, width As Integer)
        Dim btnColumn As New DataGridViewButtonColumn With {
            .Name = name,
            .HeaderText = headerText,
            .Text = buttonText,
            .UseColumnTextForButtonValue = True,
            .Width = width
        }
        dgv.Columns.Add(btnColumn)
    End Sub

End Class
