Imports System.Drawing.Drawing2D

Public Class Audit_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As AuditResponsiveManager

    Private Sub Audit_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(fillpanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling (Once - never reapply) ===
        RoundButtonCorners(btnSearch, 20)
        RoundButtonCorners(btnRemoveFilter, 20)
        RoundButtonCorners(btnPage1, 20)
        RoundButtonCorners(btnPage2, 20)
        RoundButtonCorners(btnPage3, 20)
        RoundButtonCorners(btnPageNext, 20)
        RoundButtonCorners(btnExportPdf, 20)
        RoundButtonCorners(btnExportExcel, 20)
        RoundButtonCorners(btnExportCSV, 20)

        ' === Initialize Responsive Manager ===
        responsiveManager = New AuditResponsiveManager(Me)
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

    ' ========================================
    ' TODO: Add your business logic methods here
    ' ========================================
    ' - Search button click (btnSearch) - Filter audit logs by search text
    ' - Action Type filter (cbActionType) - Filter by CRUD operations
    ' - Forms filter (cbForms) - Filter by form name
    ' - Date Range filter (dtpFrom, dtpLatest) - Filter by date
    ' - Remove Filter button (btnRemoveFilter) - Clear all filters
    ' - Load audit logs from database
    ' - Populate DataGridView with audit data
    ' - Pagination buttons (btnPage1, btnPage2, btnPage3, btnPageNext)
    ' - Export PDF button (btnExportPdf) - Generate PDF report
    ' - Export Excel button (btnExportExcel) - Generate Excel file
    ' - Export CSV button (btnExportCSV) - Generate CSV file
    ' - Handle DataGridView selection
    ' - Format audit log display (timestamp, user, action, details)
    ' ========================================

End Class