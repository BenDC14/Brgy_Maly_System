Imports System.Drawing.Drawing2D

Public Class DatabaseBackupMain_Form
    Private responsiveManager As DatabaseBackupMainResponsiveManager
    Private databaseMainLogic As New DatabaseBackupMainLogic()

    Private allLogsTable As DataTable = Nothing
    Private currentPage As Integer = 1
    Private pageSize As Integer = 15

    Private Sub DatabaseBackupMain_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGradient(fillpanel, "#EDFFE9", "#FFFFFF")

            RoundButtonCorners(btnBackupDB, 20)
            RoundButtonCorners(btnRestoreDB, 20)
            RoundButtonCorners(btnView, 20)
            RoundButtonCorners(btnDownloadLogs, 20)
            RoundButtonCorners(btnSearch, 20)
            RoundButtonCorners(btnPage1, 20)
            RoundButtonCorners(btnPage2, 20)
            RoundButtonCorners(btnPage3, 20)
            RoundButtonCorners(btnPageNext, 20)

            responsiveManager = New DatabaseBackupMainResponsiveManager(Me)
            responsiveManager.Initialize()

            ConfigureDataGridView()
            LoadAllLogs()

        Catch ex As Exception
            MsgBox("Error loading database maintenance form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ConfigureDataGridView()
        dgvDatabase.AutoGenerateColumns = True
        dgvDatabase.AllowUserToAddRows = False
        dgvDatabase.AllowUserToDeleteRows = False
        dgvDatabase.AllowUserToResizeRows = False
        dgvDatabase.ReadOnly = True
        dgvDatabase.RowHeadersVisible = False
        dgvDatabase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDatabase.MultiSelect = False
        dgvDatabase.EnableHeadersVisualStyles = False

        dgvDatabase.BackgroundColor = Color.FromArgb(220, 220, 220)
        dgvDatabase.GridColor = Color.FromArgb(180, 180, 180)
        dgvDatabase.ColumnHeadersHeight = 35
        dgvDatabase.RowTemplate.Height = 30

        dgvDatabase.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 137, 66)
        dgvDatabase.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvDatabase.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
        dgvDatabase.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgvDatabase.DefaultCellStyle.Font = New Font("Arial", 10)
        dgvDatabase.DefaultCellStyle.ForeColor = Color.Black
        dgvDatabase.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dgvDatabase.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 200, 120)
        dgvDatabase.DefaultCellStyle.SelectionForeColor = Color.Black

        dgvDatabase.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225)
    End Sub

    Private Sub LoadAllLogs()
        Try
            allLogsTable = databaseMainLogic.GetAllLogs()
            currentPage = 1
            LoadCurrentPage()
        Catch ex As Exception
            MsgBox("Error loading backup and restore logs: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub LoadCurrentPage()
        Try
            Dim pageTable As DataTable = databaseMainLogic.GetPage(allLogsTable, currentPage, pageSize)
            dgvDatabase.DataSource = pageTable

            FormatGridColumns()
            UpdatePageButtons()

        Catch ex As Exception
            MsgBox("Error loading page: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub FormatGridColumns()
        If dgvDatabase.Columns.Contains("LogId") Then dgvDatabase.Columns("LogId").Visible = False
        If dgvDatabase.Columns.Contains("BackupId") Then dgvDatabase.Columns("BackupId").Visible = False
        If dgvDatabase.Columns.Contains("PerformedById") Then dgvDatabase.Columns("PerformedById").Visible = False
        If dgvDatabase.Columns.Contains("ErrorMessage") Then dgvDatabase.Columns("ErrorMessage").Visible = False
        If dgvDatabase.Columns.Contains("Description") Then dgvDatabase.Columns("Description").Visible = False

        If dgvDatabase.Columns.Contains("LogType") Then dgvDatabase.Columns("LogType").HeaderText = "Log Type"
        If dgvDatabase.Columns.Contains("PerformedBy") Then dgvDatabase.Columns("PerformedBy").HeaderText = "Performed By"
        If dgvDatabase.Columns.Contains("DateAndTime") Then dgvDatabase.Columns("DateAndTime").HeaderText = "Date and Time"
        If dgvDatabase.Columns.Contains("FileName") Then dgvDatabase.Columns("FileName").HeaderText = "File Name"
        If dgvDatabase.Columns.Contains("FilePath") Then dgvDatabase.Columns("FilePath").HeaderText = "File Path"

        If dgvDatabase.Columns.Contains("FilePath") Then
            dgvDatabase.Columns("FilePath").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
    End Sub

    Private Sub UpdatePageButtons()
        Dim totalPages As Integer = databaseMainLogic.GetTotalPages(If(allLogsTable Is Nothing, 0, allLogsTable.Rows.Count), pageSize)

        btnPage1.Enabled = totalPages >= 1
        btnPage2.Enabled = totalPages >= 2
        btnPage3.Enabled = totalPages >= 3
        btnPageNext.Enabled = currentPage < totalPages

        btnPage1.BackColor = If(currentPage = 1, Color.FromArgb(100, 200, 120), Color.FromArgb(159, 190, 168))
        btnPage2.BackColor = If(currentPage = 2, Color.FromArgb(100, 200, 120), Color.FromArgb(159, 190, 168))
        btnPage3.BackColor = If(currentPage = 3, Color.FromArgb(100, 200, 120), Color.FromArgb(159, 190, 168))
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                allLogsTable = databaseMainLogic.GetAllLogs()
            Else
                allLogsTable = databaseMainLogic.SearchLogs(txtSearch.Text.Trim())
            End If

            currentPage = 1
            LoadCurrentPage()

        Catch ex As Exception
            MsgBox("Error searching logs: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnPage1_Click(sender As Object, e As EventArgs) Handles btnPage1.Click
        currentPage = 1
        LoadCurrentPage()
    End Sub

    Private Sub btnPage2_Click(sender As Object, e As EventArgs) Handles btnPage2.Click
        currentPage = 2
        LoadCurrentPage()
    End Sub

    Private Sub btnPage3_Click(sender As Object, e As EventArgs) Handles btnPage3.Click
        currentPage = 3
        LoadCurrentPage()
    End Sub

    Private Sub btnPageNext_Click(sender As Object, e As EventArgs) Handles btnPageNext.Click
        Dim totalPages As Integer = databaseMainLogic.GetTotalPages(If(allLogsTable Is Nothing, 0, allLogsTable.Rows.Count), pageSize)

        If currentPage < totalPages Then
            currentPage += 1
            LoadCurrentPage()
        End If
    End Sub

    Private Sub btnBackupDB_Click(sender As Object, e As EventArgs) Handles btnBackupDB.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim databaseBackupForm As New DatabaseBackup_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(databaseBackupForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading backup form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnRestoreDB_Click(sender As Object, e As EventArgs) Handles btnRestoreDB.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim databaseRestoreForm As New DatabaseRestore_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(databaseRestoreForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading restore form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            If dgvDatabase.SelectedRows.Count = 0 Then
                MsgBox("Please select a log from the table first.", MsgBoxStyle.Information, "Selection Required")
                Return
            End If

            Dim selectedRow As DataGridViewRow = dgvDatabase.SelectedRows(0)
            Dim logType As String = selectedRow.Cells("LogType").Value.ToString()
            Dim logId As Integer = CInt(selectedRow.Cells("LogId").Value)

            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim databaseViewForm As New DatabaseView_Form(logType, logId)
                Dashboard_Layout.CurrentInstance.LoadContentPanel(databaseViewForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If

        Catch ex As Exception
            MsgBox("Error loading log details: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnDownloadLogs_Click(sender As Object, e As EventArgs) Handles btnDownloadLogs.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim downloadLogsDialog As New DatabaseDownloadLogs_Form()
                downloadLogsDialog.ShowDialog()
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading download logs form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)

        AddHandler pnl.Paint,
            Sub(sender, e)
                Using brush As New LinearGradientBrush(New Point(0, 0), New Point(pnl.Width, 0), startColor, endColor)
                    e.Graphics.FillRectangle(brush, pnl.ClientRectangle)
                End Using
            End Sub
    End Sub

    Private Sub RoundButtonCorners(btn As Button, ByVal radius As Integer)
        ApplyButtonRoundedRegion(btn, radius)

        AddHandler btn.Resize,
            Sub(s, args)
                ApplyButtonRoundedRegion(btn, radius)
            End Sub
    End Sub

    Private Sub ApplyButtonRoundedRegion(btn As Button, radius As Integer)
        If btn Is Nothing Then Return
        If btn.Width <= 0 OrElse btn.Height <= 0 Then Return

        Using p As New GraphicsPath()
            p.AddArc(0, 0, radius, radius, 180, 90)
            p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
            p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
            p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
            p.CloseFigure()
            btn.Region = New Region(p)
        End Using
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If

        MyBase.OnFormClosing(e)
    End Sub

End Class