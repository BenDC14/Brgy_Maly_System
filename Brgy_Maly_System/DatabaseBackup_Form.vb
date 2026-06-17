Imports System.Drawing.Drawing2D

Public Class DatabaseBackup_Form
    Private responsiveManager As DatabaseBackupResponsiveManager
    Private backupLogic As New DatabaseBackupLogic()

    Private Sub DatabaseBackup_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

            RoundButtonCorners(btnBacktoMain, 20)
            RoundButtonCorners(btnStartBackup, 20)
            RoundButtonCorners(btnBrowse, 20)

            responsiveManager = New DatabaseBackupResponsiveManager(Me)
            responsiveManager.Initialize()

            txtFileLocation.ReadOnly = True

        Catch ex As Exception
            MsgBox("Error loading backup form: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            Using folderDialog As New FolderBrowserDialog()
                folderDialog.Description = "Select folder where the database backup will be saved"
                folderDialog.ShowNewFolderButton = True

                If folderDialog.ShowDialog() = DialogResult.OK Then
                    txtFileLocation.Text = folderDialog.SelectedPath
                End If
            End Using

        Catch ex As Exception
            MsgBox("Error selecting folder: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnStartBackup_Click(sender As Object, e As EventArgs) Handles btnStartBackup.Click
        Try
            Dim validationError As String = backupLogic.ValidateBackupInput(txtFilename.Text, txtFileLocation.Text)

            If Not String.IsNullOrWhiteSpace(validationError) Then
                MsgBox(validationError, MsgBoxStyle.Information, "Validation")
                Return
            End If

            If MsgBox("Start database backup now?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Confirm Backup") = MsgBoxResult.No Then
                Return
            End If

            btnStartBackup.Enabled = False
            btnBrowse.Enabled = False
            Application.DoEvents()

            Dim result As DatabaseBackupLogic.BackupResult = backupLogic.StartBackup(
                txtFilename.Text.Trim(),
                txtFileLocation.Text.Trim(),
                BackupNotesRtxt.Text.Trim(),
                LogInForm.CurrentUserID
            )

            btnStartBackup.Enabled = True
            btnBrowse.Enabled = True

            If result.IsSuccess Then
                MsgBox("Database backup completed successfully." & vbCrLf &
                       "File saved to: " & result.FilePath,
                       MsgBoxStyle.Information, "Backup Successful")

                txtFilename.Clear()
                txtFileLocation.Clear()
                BackupNotesRtxt.Clear()
            Else
                MsgBox("Database backup failed." & vbCrLf &
                       "Reason: " & result.ErrorMessage,
                       MsgBoxStyle.Critical, "Backup Failed")
            End If

        Catch ex As Exception
            btnStartBackup.Enabled = True
            btnBrowse.Enabled = True
            MsgBox("Error starting backup: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
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