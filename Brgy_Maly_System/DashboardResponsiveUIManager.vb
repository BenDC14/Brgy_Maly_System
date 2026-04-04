Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager for Dashboard_Layout (Fullscreen Forms)
''' Handles scaling for sidebar, buttons, fonts, padding, and DPI awareness
''' </summary>
Public Class DashboardResponsiveUIManager

    ' ========== DPI & SCALING ==========
    ''' <summary>
    ''' Get scaled font size based on DPI
    ''' </summary>
    Public Shared Function GetScaledFontSize(baseSize As Single) As Single
        Try
            Using g = Graphics.FromHwnd(IntPtr.Zero)
                Return baseSize * (g.DpiY / 96.0F)
            End Using
        Catch
            Return baseSize
        End Try
    End Function

    ''' <summary>
    ''' Scale any pixel value for current DPI
    ''' </summary>
    Public Shared Function ScaleDpi(pixels As Integer) As Integer
        Try
            Using g = Graphics.FromHwnd(IntPtr.Zero)
                Return CInt(pixels * (g.DpiY / 96.0F))
            End Using
        Catch
            Return pixels
        End Try
    End Function

    ''' <summary>
    ''' Calculate sidebar width based on screen size
    ''' Reference: 220px for 1920x1080 resolution
    ''' </summary>
    Public Shared Function GetSidebarWidth(screenWidth As Integer) As Integer
        ' Sidebar should be ~11.5% of screen width
        Dim baseWidth As Integer = 220
        Dim referenceWidth As Integer = 1920
        Dim scaledWidth As Integer = CInt(baseWidth * (CSng(screenWidth) / referenceWidth))

        ' Minimum 180px, Maximum 280px
        If scaledWidth < 180 Then Return 180
        If scaledWidth > 280 Then Return 280
        Return scaledWidth
    End Function

    ''' <summary>
    ''' Calculate button height based on DPI
    ''' Reference: 50px for standard DPI
    ''' </summary>
    Public Shared Function GetButtonHeight() As Integer
        Return ScaleDpi(50)
    End Function

    ''' <summary>
    ''' Calculate button margin/spacing based on DPI
    ''' Reference: 10px for standard DPI
    ''' </summary>
    Public Shared Function GetButtonMargin() As Integer
        Return ScaleDpi(10)
    End Function

    ''' <summary>
    ''' Calculate padding based on DPI
    ''' </summary>
    Public Shared Function GetPadding(pixels As Integer) As Integer
        Return ScaleDpi(pixels)
    End Function

    ' ========== BUTTON STYLING ==========
    ''' <summary>
    ''' Apply styling to menu buttons with hover effects
    ''' </summary>
    Public Shared Sub ApplyMenuButtonStyle(btn As Button, normalColor As Color, hoverColor As Color)
        btn.BackColor = normalColor
        btn.ForeColor = Color.White
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.UseVisualStyleBackColor = False
        btn.Cursor = Cursors.Hand
        btn.ImageAlign = ContentAlignment.MiddleLeft
        btn.TextImageRelation = TextImageRelation.ImageBeforeText
        btn.Padding = New Padding(GetPadding(10))

        AddHandler btn.MouseEnter, Sub(s, e)
                                       btn.BackColor = hoverColor
                                   End Sub

        AddHandler btn.MouseLeave, Sub(s, e)
                                       btn.BackColor = normalColor
                                   End Sub
    End Sub

    ''' <summary>
    ''' Apply rounded corners to button
    ''' </summary>
    Public Shared Sub RoundButtonCorners(btn As Button, radius As Integer)
        Dim scaledRadius As Integer = ScaleDpi(radius)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, scaledRadius, scaledRadius, 180, 90)
        p.AddArc(btn.Width - scaledRadius, 0, scaledRadius, scaledRadius, 270, 90)
        p.AddArc(btn.Width - scaledRadius, btn.Height - scaledRadius, scaledRadius, scaledRadius, 0, 90)
        p.AddArc(0, btn.Height - scaledRadius, scaledRadius, scaledRadius, 90, 90)
        p.CloseFigure()
        btn.Region = New Region(p)

        Dim btnLocal = btn
        Dim radiusLocal = scaledRadius

        AddHandler btn.Resize, Sub(sender, e)
                                   Dim newPath As New GraphicsPath()
                                   newPath.AddArc(0, 0, radiusLocal, radiusLocal, 180, 90)
                                   newPath.AddArc(btnLocal.Width - radiusLocal, 0, radiusLocal, radiusLocal, 270, 90)
                                   newPath.AddArc(btnLocal.Width - radiusLocal, btnLocal.Height - radiusLocal, radiusLocal, radiusLocal, 0, 90)
                                   newPath.AddArc(0, btnLocal.Height - radiusLocal, radiusLocal, radiusLocal, 90, 90)
                                   newPath.CloseFigure()
                                   btnLocal.Region = New Region(newPath)
                               End Sub
    End Sub

    ' ========== PANEL STYLING ==========
    ''' <summary>
    ''' Apply rounded corners to panel
    ''' </summary>
    Public Shared Sub RoundPanelCorners(pnl As Control, radius As Integer)
        Dim scaledRadius As Integer = ScaleDpi(radius)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, scaledRadius, scaledRadius, 180, 90)
        p.AddArc(pnl.Width - scaledRadius, 0, scaledRadius, scaledRadius, 270, 90)
        p.AddArc(pnl.Width - scaledRadius, pnl.Height - scaledRadius, scaledRadius, scaledRadius, 0, 90)
        p.AddArc(0, pnl.Height - scaledRadius, scaledRadius, scaledRadius, 90, 90)
        p.CloseFigure()
        pnl.Region = New Region(p)

        Dim panelLocal = pnl
        Dim radiusLocal = scaledRadius

        AddHandler pnl.Resize, Sub(sender, e)
                                   Dim newPath As New GraphicsPath()
                                   newPath.AddArc(0, 0, radiusLocal, radiusLocal, 180, 90)
                                   newPath.AddArc(panelLocal.Width - radiusLocal, 0, radiusLocal, radiusLocal, 270, 90)
                                   newPath.AddArc(panelLocal.Width - radiusLocal, panelLocal.Height - radiusLocal, radiusLocal, radiusLocal, 0, 90)
                                   newPath.AddArc(0, panelLocal.Height - radiusLocal, radiusLocal, radiusLocal, 90, 90)
                                   newPath.CloseFigure()
                                   panelLocal.Region = New Region(newPath)
                               End Sub
    End Sub

    ' ========== GRADIENT & EFFECTS ==========
    ''' <summary>
    ''' Apply gradient background to panel
    ''' </summary>
    Public Shared Sub ApplyGradient(pnl As Control, startColorHex As String, endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)

        Dim brush As New LinearGradientBrush(
            New Point(0, 0),
            New Point(pnl.Width, 0),
            startColor,
            endColor
        )

        Dim panelLocal = pnl

        AddHandler panelLocal.Paint, Sub(s, args)
                                         args.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                                     End Sub
    End Sub

    ''' <summary>
    ''' Apply shadow effect to top panel
    ''' </summary>
    Public Shared Sub ApplyTopPanelShadow(card As Panel)
        card.BackColor = Color.White
        card.Padding = New Padding(0, 0, ScaleDpi(3), ScaleDpi(3))

        Dim cardLocal = card

        AddHandler cardLocal.Paint, Sub(s, e)
                                        Dim shadowPen As New Pen(Color.FromArgb(100, 150, 150, 150), 3)
                                        e.Graphics.DrawLine(shadowPen, cardLocal.Width - 3, 5, cardLocal.Width - 3, cardLocal.Height - 5)
                                        e.Graphics.DrawLine(shadowPen, 5, cardLocal.Height - 3, cardLocal.Width - 5, cardLocal.Height - 3)
                                    End Sub
    End Sub

    ''' <summary>
    ''' Apply shadow effect to active button
    ''' </summary>
    Public Shared Sub ApplyButtonShadow(btn As Button)
        AddHandler btn.Paint, Sub(s, e)
                                  Dim shadowPen As New Pen(Color.FromArgb(80, 0, 0, 0), 2)
                                  e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                                  e.Graphics.DrawLine(shadowPen, btn.Width - 3, 5, btn.Width - 3, btn.Height - 5)
                                  e.Graphics.DrawLine(shadowPen, 5, btn.Height - 3, btn.Width - 5, btn.Height - 3)
                              End Sub
    End Sub

End Class