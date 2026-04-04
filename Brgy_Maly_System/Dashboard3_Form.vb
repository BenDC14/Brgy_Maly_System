Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

Public Class Dashboard3_Form
    ' === Timer to debounce resize events ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ' === Store original font sizes from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    Private Sub Dashboard3_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === CRITICAL: Override Designer's fixed size on FillPanel ===
        FillPanel.Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height)
        FillPanel.Location = New Point(0, 0)

        ' === MAIN CONTAINER - FILL AVAILABLE SPACE ===
        FillPanel.Dock = DockStyle.Fill
        FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ApplyGradient(FillPanel, "#EDFFE9", "#FFFFFF")

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event to recalculate layout when window resizes ===
        AddHandler Me.Resize, AddressOf Dashboard3_Form_Resize

        ' === CRITICAL: Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

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
    Private Sub Dashboard3_Form_Resize(sender As Object, e As EventArgs)
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
    ''' Uses PERCENTAGES for positioning and sizing - respects Designer proportions
    ''' </summary>
    Private Sub CalculateAndApplyLayout()
        ' === Use form's actual client size ===
        Dim panelWidth As Integer = Me.ClientSize.Width
        Dim panelHeight As Integer = Me.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' === Calculate scale factor for font sizing ===
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale) ' Use smaller scale to prevent overflow

        ' === Update FillPanel ===
        FillPanel.Size = New Size(panelWidth, panelHeight)
        FillPanel.Location = New Point(0, 0)

        ' === TITLE LABEL (Top center) ===
        ' Designer: Location(698, 18) on 1700x1004 = 41% from left, 1.8% from top
        TitleLbl.Location = New Point(CInt(panelWidth * 0.41), CInt(panelHeight * 0.018))
        TitleLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        TitleLbl.Font = New Font("Arial", 27.0F * scaleFactor, FontStyle.Bold)

        ' === BARANGAY LOGO (Center top) ===
        ' Designer: Location(772, 89), Size(230, 218) on 1700x1004
        Dim logoWidth As Integer = CInt(panelWidth * 0.135)
        Dim logoHeight As Integer = CInt(panelHeight * 0.217)
        BrgyLogoPic.Size = New Size(logoWidth, logoHeight)
        BrgyLogoPic.Location = New Point(CInt(panelWidth * 0.454), CInt(panelHeight * 0.089))
        BrgyLogoPic.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage

        ' === BARANGAY NAME LABEL (Below logo) ===
        ' Designer: Location(785, 319) on 1700x1004 = 46.2% from left, 31.8% from top
        BrgyMalyNameLbl.Location = New Point(CInt(panelWidth * 0.462), CInt(panelHeight * 0.318))
        BrgyMalyNameLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        BrgyMalyNameLbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)

        ' === MISSION TITLE LABEL (Left side) ===
        ' Designer: Location(130, 397) on 1700x1004 = 7.6% from left, 39.5% from top
        TitleMissionLbl.Location = New Point(CInt(panelWidth * 0.076), CInt(panelHeight * 0.395))
        TitleMissionLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        TitleMissionLbl.Font = New Font("Arial", 26.25F * scaleFactor, FontStyle.Bold)

        ' === MISSION INFO LABEL (Content) ===
        ' Designer: Location(201, 450) on 1700x1004 = 11.8% from left, 44.8% from top
        MissionInfoLbl.Location = New Point(CInt(panelWidth * 0.118), CInt(panelHeight * 0.448))
        MissionInfoLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        MissionInfoLbl.MaximumSize = New Size(CInt(panelWidth * 0.8), 0) ' 80% width max, auto height
        MissionInfoLbl.Font = New Font("Yu Gothic Medium", 24.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === VISION TITLE LABEL (Left side) ===
        ' Designer: Location(130, 690) on 1700x1004 = 7.6% from left, 68.7% from top
        VisionLbl.Location = New Point(CInt(panelWidth * 0.076), CInt(panelHeight * 0.687))
        VisionLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        VisionLbl.Font = New Font("Arial", 26.25F * scaleFactor, FontStyle.Bold)

        ' === VISION INFO LABEL (Content) ===
        ' Designer: Location(201, 749) on 1700x1004 = 11.8% from left, 74.6% from top
        VisionInfoLbl.Location = New Point(CInt(panelWidth * 0.118), CInt(panelHeight * 0.746))
        VisionInfoLbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        VisionInfoLbl.MaximumSize = New Size(CInt(panelWidth * 0.8), 0) ' 80% width max, auto height
        VisionInfoLbl.Font = New Font("Yu Gothic Medium", 24.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === LEFT BUTTON (Navigation) ===
        ' Designer: Location(12, 431), Size(100, 76) on 1700x1004
        LeftButtonPB.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.43))
        LeftButtonPB.Size = New Size(CInt(panelWidth * 0.059), CInt(panelHeight * 0.076))
        LeftButtonPB.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        LeftButtonPB.Cursor = Cursors.Hand
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

    Private Sub LeftButtonPB_Click(sender As Object, e As EventArgs) Handles LeftButtonPB.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dim dashboard2_form As New Dashboard2_Form()
                Dashboard_Layout.CurrentInstance.LoadContentPanel(dashboard2_form)
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