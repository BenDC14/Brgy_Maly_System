Imports System.Drawing.Drawing2D

Public Class DatabaseBackupMain_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As DatabaseBackupMainResponsiveManager

    Private Sub DatabaseBackupMain_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(fillpanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling (Once - never reapply) ===
        RoundButtonCorners(btnBackupDB, 20)
        RoundButtonCorners(btnRestoreDB, 20)
        RoundButtonCorners(btnView, 20)
        RoundButtonCorners(btnDownloadLogs, 20)
        RoundButtonCorners(btnSearch, 20)
        RoundButtonCorners(btnPage1, 20)
        RoundButtonCorners(btnPage2, 20)
        RoundButtonCorners(btnPage3, 20)
        RoundButtonCorners(btnPageNext, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New DatabaseBackupMainResponsiveManager(Me)
        responsiveManager.Initialize()
    End Sub

    ''' <summary>
    ''' Apply gradient background to panel
    ''' </summary>
    Private Sub ApplyGradient(pnl As Control, ByVal startColorHex As String, ByVal endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)

        Dim brush As New LinearGradientBrush(
            New Point(0, 0),
            New Point(pnl.Width, 0),
            startColor,
            endColor
        )

        Dim panelLocal = pnl

        AddHandler panelLocal.Paint, Sub(sender, e)
                                         e.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                                     End Sub
    End Sub

    ''' <summary>
    ''' Apply rounded corners to button (with resize handler)
    ''' </summary>
    Private Sub RoundButtonCorners(ByRef btn As Button, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        btn.Region = New Region(p)

        Dim btnLocal = btn

        AddHandler btn.Resize, Sub(s, args)
                                   Dim newPath As New GraphicsPath()
                                   newPath.AddArc(0, 0, radius, radius, 180, 90)
                                   newPath.AddArc(btnLocal.Width - radius, 0, radius, radius, 270, 90)
                                   newPath.AddArc(btnLocal.Width - radius, btnLocal.Height - radius, radius, radius, 0, 90)
                                   newPath.AddArc(0, btnLocal.Height - radius, radius, radius, 90, 90)
                                   newPath.CloseFigure()
                                   btnLocal.Region = New Region(newPath)
                               End Sub
    End Sub

    ''' <summary>
    ''' Navigate to Database Backup form
    ''' </summary>
    Private Sub btnBackupDB_Click(sender As Object, e As EventArgs) Handles btnBackupDB.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim databaseBackupForm As New DatabaseBackup_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(databaseBackupForm)

            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Navigate to Database Restore form
    ''' </summary>
    Private Sub btnRestoreDB_Click(sender As Object, e As EventArgs) Handles btnRestoreDB.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim databaseRestoreForm As New DatabaseRestore_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(databaseRestoreForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Navigate to Database View form
    ''' </summary>
    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim databaseRestoreForm As New DatabaseRestore_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(databaseRestoreForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try
        Dim databaseViewForm As New DatabaseView_Form()
        Dashboard_Layout.LoadContentPanel(databaseViewForm)
    End Sub

    ''' <summary>
    ''' Show Download Logs dialog
    ''' </summary>
    Private Sub btnDownloadLogs_Click(sender As Object, e As EventArgs) Handles btnDownloadLogs.Click
        ' === Create and show the modal dialog ===
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim downloadLogsDialog As New DatabaseDownloadLogs_Form()
                downloadLogsDialog.ShowDialog() ' ✅ Modal dialog, centered on screen
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Cleanup when form closes
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If
        MyBase.OnFormClosing(e)
    End Sub

    ' ========================================
    ' TODO: Add your business logic methods here
    ' ========================================
    ' - Search button click handler
    ' - Pagination button click handlers (Page 1, 2, 3, Next)
    ' - DataGridView data loading
    ' - DataGridView selection changed handler
    ' - Filter/Sort database logs
    ' - Refresh logs data
    ' ========================================

End Class