Imports System.Drawing.Drawing2D

Public Class AyudaMain_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As AyudaMainResponsiveManager

    Private Sub AyudaMain_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling (Once - never reapply) ===
        RoundButtonCorners(btnRecordNewAyuda, 20)
        RoundButtonCorners(btnSearch, 20)
        RoundButtonCorners(btnEdit, 20)
        RoundButtonCorners(btnArchieve, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New AyudaMainResponsiveManager(Me)
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
    ''' Cleanup when form closes
    ''' </summary>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If responsiveManager IsNot Nothing Then
            responsiveManager.Cleanup()
        End If
        MyBase.OnFormClosing(e)
    End Sub


    Private Sub btnRecordNewAyuda_Click(sender As Object, e As EventArgs) Handles btnRecordNewAyuda.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim recordAyudaProgramForm As New AyudaRecording_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(recordAyudaProgramForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim editayudaProgramForm As New AyudaEditRecording_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(editayudaProgramForm)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try
    End Sub

    ' ========================================
    ' TODO: Add your business logic methods here
    ' ========================================
    ' - Add New Ayuda Program button click (btnAddNewAyudaProgram)
    ' - Record New Ayuda button click (btnRecordNewAyuda)
    ' - Search button click (btnSearch) - Filter ayuda records by search text
    ' - Load ayuda records from database into DataGridView
    ' - Edit button click (btnEdit) - TEMPORARY: Will be DataGridView button column
    ' - Archive button click (btnArchieve) - TEMPORARY: Will be DataGridView button column
    ' 
    ' FUTURE IMPLEMENTATION:
    ' - Add DataGridViewButtonColumn for "Edit" in dgvResidentAyudas
    ' - Add DataGridViewButtonColumn for "Archive" in dgvResidentAyudas
    ' - Handle CellClick event for button columns
    ' - Remove btnEdit, btnArchieve from form
    ' 
    ' - DataGridView columns: Ayuda ID, Program Name, Resident Name, Date Received, Amount/Type, Status, Actions
    ' - Handle ayuda record selection
    ' - Navigate to ayuda details form
    ' - Refresh DataGridView after CRUD operations
    ' ========================================

End Class