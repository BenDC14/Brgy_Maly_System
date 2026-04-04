Imports System.Drawing.Drawing2D

Public Class DatabaseDownloadLogs_Form
    ' === Responsive Manager Instance ===
    Private responsiveManager As DatabaseDownloadLogsResponsiveManager

    ''' <summary>
    ''' Constructor - Enable double buffering
    ''' </summary>
    Public Sub New()
        InitializeComponent()

        ' === Enable double buffering (must be set here, it's Protected) ===
        Me.DoubleBuffered = True
    End Sub

    Private Sub DatabaseDownloadLogs_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Apply Gradient ===
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Apply Button Styling (Once - never reapply) ===
        RoundButtonCorners(btnDownload, 20)

        ' === Initialize Responsive Manager (Modal Pattern) ===
        responsiveManager = New DatabaseDownloadLogsResponsiveManager(Me)
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
    ''' Exit button - close the dialog
    ''' </summary>
    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) Handles ExitBtn.Click
        Me.Close()
    End Sub

    ' ========================================
    ' TODO: Add your business logic methods here
    ' ========================================
    ' - Download button click handler
    ' - Validate selections (at least one log type and one format)
    ' - Handle checkbox mutual exclusivity (Both vs individual logs)
    ' - Generate CSV export from database logs
    ' - Generate Excel export (using EPPlus or ClosedXML)
    ' - Show SaveFileDialog for download location
    ' - Filter logs by type (Backup/Restore/Both)
    ' - Format data for export (headers, columns)
    ' - Progress indication during export
    ' - Success/Error message handling
    ' - Auto-close dialog after successful download
    ' ========================================

End Class