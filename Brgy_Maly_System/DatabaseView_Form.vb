Imports System.Drawing.Drawing2D

Public Class DatabaseView_Form
    Private responsiveManager As DatabaseViewResponsiveManager
    Private viewLogic As New DatabaseViewLogic()

    Private selectedLogType As String = ""
    Private selectedLogId As Integer = -1

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(logType As String, logId As Integer)
        InitializeComponent()
        selectedLogType = logType
        selectedLogId = logId
    End Sub

    Private Sub DatabaseView_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")
        RoundButtonCorners(btnBacktoMain, 20)

        responsiveManager = New DatabaseViewResponsiveManager(Me)
        responsiveManager.Initialize()

        LoadLogDetails()
    End Sub

    Private Sub LoadLogDetails()
        Try
            If selectedLogId <= 0 Then
                MsgBox("No log was selected.", MsgBoxStyle.Information, "Selection Required")
                Return
            End If

            Dim dataTable As DataTable = viewLogic.GetLogDetails(selectedLogType, selectedLogId)

            If dataTable.Rows.Count = 0 Then
                MsgBox("Selected log details were not found.", MsgBoxStyle.Information, "Not Found")
                Return
            End If

            Dim row As DataRow = dataTable.Rows(0)

            txtLogType.Text = row("LogType").ToString()
            txtStatus.Text = row("Status").ToString()
            txtDateAndTime.Text = If(IsDBNull(row("DateAndTime")), "", CDate(row("DateAndTime")).ToString("yyyy-MM-dd hh:mm tt"))
            txtPerformedBy.Text = row("PerformedBy").ToString()
            TextBox2.Text = row("FileName").ToString()
            TextBox1.Text = row("FilePath").ToString()
            txtErrorMessage.Text = If(IsDBNull(row("ErrorMessage")), "", row("ErrorMessage").ToString())

            If txtStatus.Text.ToLower().Contains("success") Then
                txtStatus.BackColor = Color.FromArgb(200, 255, 200)
            Else
                txtStatus.BackColor = Color.FromArgb(255, 210, 210)
            End If

        Catch ex As Exception
            MsgBox("Error loading log details: " & ex.Message, MsgBoxStyle.Critical, "Error")
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

        Using p As New Drawing2D.GraphicsPath()
            p.AddArc(0, 0, radius, radius, 180, 90)
            p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
            p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
            p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
            p.CloseFigure()
            btn.Region = New Region(p)
        End Using
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then responsiveManager.Cleanup()
        MyBase.OnFormClosing(e)
    End Sub

End Class