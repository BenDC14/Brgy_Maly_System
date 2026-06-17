Imports System.Drawing.Drawing2D

Public Class DatabaseDownloadLogs_Form
    Private responsiveManager As DatabaseDownloadLogsResponsiveManager
    Private downloadLogic As New DatabaseDownloadLogsLogic()
    Private isChangingChecks As Boolean = False

    Public Sub New()
        InitializeComponent()
        Me.DoubleBuffered = True
    End Sub

    Private Sub DatabaseDownloadLogs_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")
        RoundButtonCorners(btnDownload, 20)

        responsiveManager = New DatabaseDownloadLogsResponsiveManager(Me)
        responsiveManager.Initialize()
    End Sub

    Private Sub cbBackupLogs_CheckedChanged(sender As Object, e As EventArgs) Handles cbBackupLogs.CheckedChanged
        If isChangingChecks Then Return

        If cbBackupLogs.Checked Then
            isChangingChecks = True
            cbBoth.Checked = False
            isChangingChecks = False
        End If
    End Sub

    Private Sub cbRestoreLogs_CheckedChanged(sender As Object, e As EventArgs) Handles cbRestoreLogs.CheckedChanged
        If isChangingChecks Then Return

        If cbRestoreLogs.Checked Then
            isChangingChecks = True
            cbBoth.Checked = False
            isChangingChecks = False
        End If
    End Sub

    Private Sub cbBoth_CheckedChanged(sender As Object, e As EventArgs) Handles cbBoth.CheckedChanged
        If isChangingChecks Then Return

        If cbBoth.Checked Then
            isChangingChecks = True
            cbBackupLogs.Checked = False
            cbRestoreLogs.Checked = False
            isChangingChecks = False
        End If
    End Sub

    Private Sub cbCSV_CheckedChanged(sender As Object, e As EventArgs) Handles cbCSV.CheckedChanged
        If isChangingChecks Then Return

        If cbCSV.Checked Then
            isChangingChecks = True
            cbExcel.Checked = False
            isChangingChecks = False
        End If
    End Sub

    Private Sub cbExcel_CheckedChanged(sender As Object, e As EventArgs) Handles cbExcel.CheckedChanged
        If isChangingChecks Then Return

        If cbExcel.Checked Then
            isChangingChecks = True
            cbCSV.Checked = False
            isChangingChecks = False
        End If
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Try
            Dim includeBackup As Boolean = cbBackupLogs.Checked OrElse cbBoth.Checked
            Dim includeRestore As Boolean = cbRestoreLogs.Checked OrElse cbBoth.Checked
            Dim selectedFormat As String = ""

            If cbCSV.Checked Then selectedFormat = "CSV"
            If cbExcel.Checked Then selectedFormat = "Excel"

            Dim validationError As String = downloadLogic.ValidateDownloadSelection(includeBackup, includeRestore, selectedFormat)

            If Not String.IsNullOrWhiteSpace(validationError) Then
                MsgBox(validationError, MsgBoxStyle.Information, "Validation")
                Return
            End If

            Using saveDialog As New SaveFileDialog()
                saveDialog.FileName = downloadLogic.BuildDefaultFileName(includeBackup, includeRestore, selectedFormat)

                If selectedFormat = "Excel" Then
                    saveDialog.Filter = "Excel File (*.xls)|*.xls"
                Else
                    saveDialog.Filter = "CSV File (*.csv)|*.csv"
                End If

                If saveDialog.ShowDialog() = DialogResult.OK Then
                    downloadLogic.ExportLogs(saveDialog.FileName, includeBackup, includeRestore, selectedFormat)
                    MsgBox("Logs downloaded successfully.", MsgBoxStyle.Information, "Success")
                    Me.Close()
                End If
            End Using

        Catch ex As Exception
            MsgBox("Error downloading logs: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) Handles ExitBtn.Click
        Me.Close()
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

        Using p As New Drawing2D.GraphicsPath()
            p.AddArc(0, 0, radius, radius, 180, 90)
            p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
            p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
            p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
            p.CloseFigure()
            btn.Region = New Region(p)
        End Using
    End Sub

End Class