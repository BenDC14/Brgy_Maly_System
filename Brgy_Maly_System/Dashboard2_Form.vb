Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox
Imports Microsoft.Win32

Public Class Dashboard2_Form
    ' === Timer to debounce resize events ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    Private Sub Dashboard2_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === CRITICAL: Override Designer's fixed size on FillPanel ===
        FillPanel.Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height)
        FillPanel.Location = New Point(0, 0)

        ' === RESET ALL PANEL DOCKS TO NONE ===
        ChairmanPnl.Dock = DockStyle.None
        For i As Integer = 1 To 11
            CType(FillPanel.Controls("Kagawad" & i & "Pnl"), Panel).Dock = DockStyle.None
        Next
        SkChairmanPnl.Dock = DockStyle.None
        For i As Integer = 1 To 7
            CType(FillPanel.Controls("SkKagawad" & i & "Pnl"), Panel).Dock = DockStyle.None
        Next

        ' === MAIN CONTAINER - FILL AVAILABLE SPACE ===
        FillPanel.Dock = DockStyle.Fill
        FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event to recalculate layout when window resizes ===
        AddHandler Me.Resize, AddressOf Dashboard2_Form_Resize

        ' === CRITICAL: Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Apply Styling ONCE - Never reapply ===
        RoundPanel(ChairmanPnl, 30)
        ApplyDropShadow(ChairmanPnl)

        For i As Integer = 1 To 11
            Dim kagawadPnl As Panel = CType(FillPanel.Controls("Kagawad" & i & "Pnl"), Panel)
            RoundPanel(kagawadPnl, 30)
            ApplyDropShadow(kagawadPnl)
        Next

        RoundPanel(SkChairmanPnl, 30)
        ApplyDropShadow(SkChairmanPnl)

        For i As Integer = 1 To 7
            Dim skKagawadPnl As Panel = CType(FillPanel.Controls("SkKagawad" & i & "Pnl"), Panel)
            RoundPanel(skKagawadPnl, 30)
            ApplyDropShadow(skKagawadPnl)
        Next

        ' === Calculate and apply layout for the first time ===
        FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    ''' <summary>
    ''' CRITICAL: Fires when Windows resolution changes
    ''' </summary>
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Fires when form window resizes
    ''' </summary>
    Private Sub Dashboard2_Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Exit Sub
        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    ''' <summary>
    ''' Timer tick - recalculates layout ONCE after resize stops
    ''' </summary>
    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Calculate positions and apply layout based on current form size
    ''' Uses PERCENTAGES for positioning and sizing - NOT font scaling!
    ''' </summary>
    Private Sub CalculateAndApplyLayout()
        ' === Use form's actual client size ===
        Dim panelWidth As Integer = Me.ClientSize.Width
        Dim panelHeight As Integer = Me.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' === Update FillPanel ===
        FillPanel.Size = New Size(panelWidth, panelHeight)
        FillPanel.Location = New Point(0, 0)

        ' === CARD POSITIONING - Uses percentages ===
        Dim cardMarginTop As Integer = CInt(panelHeight * 0.068)
        Dim cardWidth As Integer = CInt(panelWidth * 0.142)
        Dim cardHeight As Integer = CInt(panelHeight * 0.124)
        Dim cardSpacing As Integer = CInt(panelWidth * 0.015)
        Dim rowSpacing As Integer = CInt(panelHeight * 0.02)

        ' === TITLE LABEL ===
        TitleLbl.Location = New Point(CInt(panelWidth * 0.35), CInt(panelHeight * 0.018))
        TitleLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' === CHAIRMAN (Centered) ===
        Dim chairmanX As Integer = CInt((panelWidth - cardWidth) / 2)
        Dim currentY As Integer = cardMarginTop
        PositionCard(ChairmanPnl, chairmanX, currentY, cardWidth, cardHeight)

        ' === ROW 1: BARANGAY KAGAWAD 1-5 ===
        currentY += cardHeight + rowSpacing
        Dim row1TotalWidth As Integer = (5 * cardWidth) + (4 * cardSpacing)
        Dim row1StartX As Integer = CInt((panelWidth - row1TotalWidth) / 2)

        PositionCard(CType(FillPanel.Controls("Kagawad1Pnl"), Panel), row1StartX, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("Kagawad2Pnl"), Panel), row1StartX + cardWidth + cardSpacing, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("Kagawad3Pnl"), Panel), row1StartX + (cardWidth + cardSpacing) * 2, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("Kagawad4Pnl"), Panel), row1StartX + (cardWidth + cardSpacing) * 3, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("Kagawad5Pnl"), Panel), row1StartX + (cardWidth + cardSpacing) * 4, currentY, cardWidth, cardHeight)

        ' === ROW 2: BARANGAY KAGAWAD 6-11 ===
        currentY += cardHeight + rowSpacing
        Dim row2TotalWidth As Integer = (6 * cardWidth) + (5 * cardSpacing)
        Dim row2StartX As Integer = CInt((panelWidth - row2TotalWidth) / 2)

        PositionCard(CType(FillPanel.Controls("Kagawad6Pnl"), Panel), row2StartX, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("Kagawad7Pnl"), Panel), row2StartX + cardWidth + cardSpacing, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("Kagawad8Pnl"), Panel), row2StartX + (cardWidth + cardSpacing) * 2, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("Kagawad9Pnl"), Panel), row2StartX + (cardWidth + cardSpacing) * 3, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("Kagawad10Pnl"), Panel), row2StartX + (cardWidth + cardSpacing) * 4, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("Kagawad11Pnl"), Panel), row2StartX + (cardWidth + cardSpacing) * 5, currentY, cardWidth, cardHeight)

        ' === SK CHAIRMAN (Centered) ===
        currentY += cardHeight + CInt(rowSpacing * 1.5)
        PositionCard(SkChairmanPnl, chairmanX, currentY, cardWidth, cardHeight)

        ' === ROW 3: SK KAGAWAD 1-3 ===
        currentY += cardHeight + rowSpacing
        Dim skRow1TotalWidth As Integer = (3 * cardWidth) + (2 * cardSpacing)
        Dim skRow1StartX As Integer = CInt((panelWidth - skRow1TotalWidth) / 2)

        PositionCard(CType(FillPanel.Controls("SkKagawad1Pnl"), Panel), skRow1StartX, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("SkKagawad2Pnl"), Panel), skRow1StartX + cardWidth + cardSpacing, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("SkKagawad3Pnl"), Panel), skRow1StartX + (cardWidth + cardSpacing) * 2, currentY, cardWidth, cardHeight)

        ' === ROW 4: SK KAGAWAD 4-7 ===
        currentY += cardHeight + rowSpacing
        Dim skRow2TotalWidth As Integer = (4 * cardWidth) + (3 * cardSpacing)
        Dim skRow2StartX As Integer = CInt((panelWidth - skRow2TotalWidth) / 2)

        PositionCard(CType(FillPanel.Controls("SkKagawad4Pnl"), Panel), skRow2StartX, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("SkKagawad5Pnl"), Panel), skRow2StartX + cardWidth + cardSpacing, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("SkKagawad6Pnl"), Panel), skRow2StartX + (cardWidth + cardSpacing) * 2, currentY, cardWidth, cardHeight)
        PositionCard(CType(FillPanel.Controls("SkKagawad7Pnl"), Panel), skRow2StartX + (cardWidth + cardSpacing) * 3, currentY, cardWidth, cardHeight)

        ' === LEFT BUTTON ===
        LeftButtonPB.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.48))
        LeftButtonPB.Size = New Size(CInt(panelWidth * 0.059), CInt(panelHeight * 0.076))
        LeftButtonPB.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        LeftButtonPB.Cursor = Cursors.Hand

        ' === NEXT BUTTON ===
        NextBtn.Location = New Point(CInt(panelWidth * 0.933), CInt(panelHeight * 0.48))
        NextBtn.Size = New Size(CInt(panelWidth * 0.059), CInt(panelHeight * 0.076))
        NextBtn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        NextBtn.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Helper: Position a card panel and its children
    ''' DYNAMICALLY RESIZES AND REPOSITIONS ALL CHILD CONTROLS
    ''' </summary>
    Private Sub PositionCard(pnl As Panel, x As Integer, y As Integer, w As Integer, h As Integer)
        pnl.Location = New Point(x, y)
        pnl.Size = New Size(w, h)
        pnl.Dock = DockStyle.None
        pnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' === Calculate responsive sizes for child controls ===
        Dim picWidth As Integer = CInt(w * 0.39)  ' PictureBox is 39% of panel width
        Dim picHeight As Integer = CInt(h * 0.59) ' PictureBox is 59% of panel height
        Dim picTop As Integer = CInt(h * 0.024)   ' Top margin
        Dim picLeft As Integer = CInt((w - picWidth) / 2) ' Center horizontally

        Dim nameTop As Integer = CInt(h * 0.64)   ' Name label at 64% height
        Dim positionTop As Integer = CInt(h * 0.79) ' Position label at 79% height

        ' === Find and reposition child controls based on panel size ===
        For Each ctrl As Control In pnl.Controls
            If TypeOf ctrl Is PictureBox Then
                ' Resize and reposition picture box
                ctrl.Size = New Size(picWidth, picHeight)
                ctrl.Location = New Point(picLeft, picTop)
                CType(ctrl, PictureBox).SizeMode = PictureBoxSizeMode.StretchImage

            ElseIf TypeOf ctrl Is Label Then
                ' Position labels below picture box
                If ctrl.Name.Contains("Name") Then
                    ' Name label - center horizontally
                    ctrl.AutoSize = True
                    ctrl.Location = New Point(CInt((w - ctrl.Width) / 2), nameTop)

                ElseIf ctrl.Name.Contains("Position") OrElse ctrl.Name.Contains("Barangay") OrElse ctrl.Name.Contains("Chairman") OrElse ctrl.Name.Contains("Kagawad") Then
                    ' Position/Role label - center horizontally
                    ctrl.AutoSize = True
                    ctrl.Location = New Point(CInt((w - ctrl.Width) / 2), positionTop)
                End If
            End If
        Next
    End Sub

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

        AddHandler panelLocal.Paint, Sub(s, args)
                                         args.Graphics.FillRectangle(brush, panelLocal.ClientRectangle)
                                     End Sub
    End Sub

    Private Sub RoundPanel(ByRef pnl As Control, ByVal radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(pnl.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(pnl.Width - radius, pnl.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, pnl.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        pnl.Region = New Region(p)

        Dim panelLocal = pnl

        AddHandler pnl.Resize, Sub(s, args)
                                   p = New GraphicsPath()
                                   p.AddArc(0, 0, radius, radius, 180, 90)
                                   p.AddArc(panelLocal.Width - radius, 0, radius, radius, 270, 90)
                                   p.AddArc(panelLocal.Width - radius, panelLocal.Height - radius, radius, radius, 0, 90)
                                   p.AddArc(0, panelLocal.Height - radius, radius, radius, 90, 90)
                                   p.CloseFigure()
                                   panelLocal.Region = New Region(p)
                               End Sub
    End Sub

    Private Sub ApplyDropShadow(card As Panel)
        card.BackColor = Color.White
        card.Padding = New Padding(0, 0, 3, 3)

        Dim cardLocal = card

        AddHandler cardLocal.Paint, Sub(s, args)
                                        Dim shadowPen As New Pen(Color.FromArgb(100, 150, 150, 150), 2)
                                        args.Graphics.DrawLine(shadowPen, cardLocal.Width - 2, 3, cardLocal.Width - 2, cardLocal.Height - 3)
                                        args.Graphics.DrawLine(shadowPen, 3, cardLocal.Height - 2, cardLocal.Width - 3, cardLocal.Height - 2)
                                    End Sub
    End Sub

    Private Sub LeftButtonPB_Click(sender As Object, e As EventArgs) Handles LeftButtonPB.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim dashboard1_form As New Dashboard1_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(dashboard1_form)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try
    End Sub

    Private Sub NextBtn_Click(sender As Object, e As EventArgs) Handles NextBtn.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim dashboard3_form As New Dashboard3_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(dashboard3_form)
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("btnView_Click Error: " & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
        MyBase.OnFormClosing(e)
    End Sub
End Class