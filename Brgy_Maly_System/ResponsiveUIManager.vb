Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Universal responsive UI manager for ALL forms in your project
''' Works with different colors, sizes, layouts - completely flexible!
''' </summary>
Public Class ResponsiveUIManager

    ' ========== PANEL STYLING ==========
    ''' <summary>
    ''' Apply styling to ANY panel - you choose the color!
    ''' </summary>
    Public Shared Sub ApplyPanelStyling(panel As Panel, backColor As Color, Optional radius As Integer = 0)
        panel.BackColor = backColor

        If radius > 0 Then
            ApplyRoundedCorners(panel, radius)
        End If
    End Sub

    ''' <summary>
    ''' Apply rounded corners to ANY panel
    ''' </summary>
    Private Shared Sub ApplyRoundedCorners(panel As Panel, radius As Integer)
        Dim p As New GraphicsPath()
        CreateRoundedPath(p, panel.Width, panel.Height, radius)
        panel.Region = New Region(p)

        Dim panelLocal = panel
        Dim radiusLocal = radius

        AddHandler panel.Resize, Sub(sender, e)
                                     Dim newPath As New GraphicsPath()
                                     CreateRoundedPath(newPath, panelLocal.Width, panelLocal.Height, radiusLocal)
                                     panelLocal.Region = New Region(newPath)
                                 End Sub
    End Sub

    Private Shared Sub CreateRoundedPath(p As GraphicsPath, width As Integer, height As Integer, radius As Integer)
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(width - radius, 0, radius, radius, 270, 90)
        p.AddArc(width - radius, height - radius, radius, radius, 0, 90)
        p.AddArc(0, height - radius, radius, radius, 90, 90)
        p.CloseFigure()
    End Sub

    ' ========== GRADIENT BACKGROUNDS ==========
    ''' <summary>
    ''' Apply gradient to ANY panel with custom colors
    ''' </summary>
    Public Shared Sub ApplyGradient(panel As Panel, startColorHex As String, endColorHex As String)
        Dim startColor = ColorTranslator.FromHtml(startColorHex)
        Dim endColor = ColorTranslator.FromHtml(endColorHex)

        Dim brush As New LinearGradientBrush(
            New Point(0, 0),
            New Point(panel.Width, 0),
            startColor,
            endColor
        )

        Dim panelLocal = panel

        AddHandler panelLocal.Paint, Sub(s, args)
                                         args.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                                     End Sub
    End Sub

    ' ========== BUTTON STYLING ==========
    ''' <summary>
    ''' Apply hover effects to ANY button with custom colors
    ''' </summary>
    Public Shared Sub ApplyButtonStyle(btn As Button, normalColor As Color, hoverColor As Color, textColor As Color)
        btn.BackColor = normalColor
        btn.ForeColor = textColor
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.UseVisualStyleBackColor = False
        btn.Cursor = Cursors.Hand

        AddHandler btn.MouseEnter, Sub(s, e)
                                       btn.BackColor = hoverColor
                                   End Sub

        AddHandler btn.MouseLeave, Sub(s, e)
                                       btn.BackColor = normalColor
                                   End Sub
    End Sub

    ' ========== TEXTBOX STYLING ==========
    ''' <summary>
    ''' Apply focus effect to ANY textbox
    ''' </summary>
    Public Shared Sub ApplyTextBoxStyling(textBox As TextBox, Optional focusBackColor As Color = Nothing, Optional normalBackColor As Color = Nothing)
        If normalBackColor = Nothing Then normalBackColor = Color.White
        If focusBackColor = Nothing Then focusBackColor = Color.FromArgb(240, 255, 240)

        textBox.BorderStyle = BorderStyle.FixedSingle
        textBox.BackColor = normalBackColor
        textBox.ForeColor = Color.Black

        AddHandler textBox.GotFocus, Sub(s, e)
                                         textBox.BackColor = focusBackColor
                                     End Sub

        AddHandler textBox.LostFocus, Sub(s, e)
                                          textBox.BackColor = normalBackColor
                                      End Sub
    End Sub

    ' ========== POSITIONING (PERCENTAGE-BASED) ==========
    ''' <summary>
    ''' Position any control using percentages - works on ANY resolution!
    ''' </summary>
    Public Shared Sub PositionControl(ctrl As Control, parentWidth As Integer, parentHeight As Integer,
                                       leftPercent As Double, topPercent As Double,
                                       widthPercent As Double, heightPercent As Double)
        ctrl.Location = New Point(CInt(parentWidth * leftPercent), CInt(parentHeight * topPercent))
        ctrl.Size = New Size(CInt(parentWidth * widthPercent), CInt(parentHeight * heightPercent))
        ctrl.Dock = DockStyle.None
        ctrl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    End Sub

    ''' <summary>
    ''' Position control on right side (for right-aligned elements)
    ''' </summary>
    Public Shared Sub PositionControlRight(ctrl As Control, parentWidth As Integer, parentHeight As Integer,
                                            rightPercent As Double, topPercent As Double,
                                            widthPercent As Double, heightPercent As Double)
        Dim left = parentWidth - CInt(parentWidth * (rightPercent + widthPercent))
        ctrl.Location = New Point(left, CInt(parentHeight * topPercent))
        ctrl.Size = New Size(CInt(parentWidth * widthPercent), CInt(parentHeight * heightPercent))
        ctrl.Dock = DockStyle.None
        ctrl.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    ' ========== FONT SCALING ==========
    ''' <summary>
    ''' Scale font size based on current DPI (for high-res monitors)
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

    ' ========== DROP SHADOW / CARD EFFECTS ==========
    ''' <summary>
    ''' Apply drop shadow effect to any panel (for card-style UI)
    ''' </summary>
    Public Shared Sub ApplyDropShadow(panel As Panel, Optional shadowColor As Color = Nothing, Optional shadowAlpha As Integer = 100)
        If shadowColor = Nothing Then shadowColor = Color.FromArgb(150, 150, 150)

        Dim panelLocal = panel

        AddHandler panelLocal.Paint, Sub(s, args)
                                         Dim shadowPen As New Pen(Color.FromArgb(shadowAlpha, shadowColor.R, shadowColor.G, shadowColor.B), 3)
                                         args.Graphics.DrawLine(shadowPen, panelLocal.Width - 3, 5, panelLocal.Width - 3, panelLocal.Height - 5)
                                         args.Graphics.DrawLine(shadowPen, 5, panelLocal.Height - 3, panelLocal.Width - 5, panelLocal.Height - 3)
                                     End Sub
    End Sub

    ' ========== CALCULATE PERCENTAGE ==========
    ''' <summary>
    ''' Helper: Calculate a value as percentage of parent size
    ''' </summary>
    Public Shared Function CalculatePercentage(parentSize As Integer, percentage As Double) As Integer
        Return CInt(parentSize * percentage)
    End Function

End Class