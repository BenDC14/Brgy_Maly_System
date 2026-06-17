Imports System.Drawing.Drawing2D

Public Class DatabaseRestore_Form
    Private responsiveManager As DatabaseRestoreResponsiveManager
    Private restoreLogic As New DatabaseRestoreLogic()

    Private selectedSqlFilePath As String = ""

    Private Sub DatabaseRestore_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            RoundButtonCorners(btnBacktoMain, 20)
            RoundButtonCorners(btnRestoreNow, 20)
            RoundButtonCorners(btnBrowse, 20)

            responsiveManager = New DatabaseRestoreResponsiveManager(Me)
            responsiveManager.Initialize()

            txtSelectBackupFile.ReadOnly = True
            txtFileName.ReadOnly = True
            txtBackupDate.ReadOnly = True
            txtBackupStatus.ReadOnly = True
            btnRestoreNow.Enabled = False

        Catch ex As Exception
            MsgBox("Error loading restore form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            Using openDialog As New OpenFileDialog()
                openDialog.Title = "Select SQL Backup File"
                openDialog.Filter = "SQL Backup Files (*.sql)|*.sql"
                openDialog.Multiselect = False

                If openDialog.ShowDialog() = DialogResult.OK Then
                    selectedSqlFilePath = openDialog.FileName

                    Dim validationError As String = restoreLogic.ValidateSqlFile(selectedSqlFilePath)

                    If Not String.IsNullOrWhiteSpace(validationError) Then
                        MsgBox(validationError, MsgBoxStyle.Exclamation, "Invalid SQL File")
                        ClearSelectedFile()
                        Return
                    End If

                    Dim backupInfo As DatabaseRestoreLogic.BackupFileInfo = restoreLogic.GetBackupFileInfo(selectedSqlFilePath)

                    txtSelectBackupFile.Text = selectedSqlFilePath
                    txtFileName.Text = backupInfo.FileName
                    txtBackupDate.Text = backupInfo.BackupDateTime
                    txtBackupStatus.Text = backupInfo.BackupStatus

                    btnRestoreNow.Enabled = True
                End If
            End Using

        Catch ex As Exception
            MsgBox("Error selecting backup file: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnRestoreNow_Click(sender As Object, e As EventArgs) Handles btnRestoreNow.Click
        Try
            If String.IsNullOrWhiteSpace(selectedSqlFilePath) Then
                MsgBox("Please select a SQL backup file first.", MsgBoxStyle.Information, "Selection Required")
                Return
            End If

            If MsgBox("Restoring will overwrite current database data. Continue?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Confirm Restore") = MsgBoxResult.No Then
                Return
            End If

            btnRestoreNow.Enabled = False
            btnBrowse.Enabled = False
            Application.DoEvents()

            Dim result As DatabaseRestoreLogic.RestoreResult = restoreLogic.RestoreDatabase(selectedSqlFilePath, LogInForm.CurrentUserID)

            btnBrowse.Enabled = True
            btnRestoreNow.Enabled = True

            If result.IsSuccess Then
                MsgBox("Database restored successfully.", MsgBoxStyle.Information, "Restore Successful")
                ClearSelectedFile()
            Else
                MsgBox("Database restore failed." & vbCrLf &
                       "Reason: " & result.ErrorMessage,
                       MsgBoxStyle.Critical, "Restore Failed")
            End If

        Catch ex As Exception
            btnBrowse.Enabled = True
            btnRestoreNow.Enabled = True
            MsgBox("Error restoring database: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ClearSelectedFile()
        selectedSqlFilePath = ""
        txtSelectBackupFile.Clear()
        txtFileName.Clear()
        txtBackupDate.Clear()
        txtBackupStatus.Clear()
        btnRestoreNow.Enabled = False
    End Sub

    Private Sub btnBacktoMain_Click(sender As Object, e As EventArgs) Handles btnBacktoMain.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim databaseBackupMainForm As New DatabaseBackupMain_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(databaseBackupMainForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
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