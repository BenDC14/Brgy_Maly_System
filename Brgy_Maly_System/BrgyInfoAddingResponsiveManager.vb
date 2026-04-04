Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager for BrgyInfoAdding_Form
''' Handles all layout calculations and positioning
''' </summary>
Public Class BrgyInfoAddingResponsiveManager

    ' === Original form dimensions ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As BrgyInfoAdding_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(form As BrgyInfoAdding_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior
    ''' </summary>
    Public Sub Initialize()
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Calculate layout ===
        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    ''' <summary>
    ''' Fires when Windows resolution changes
    ''' </summary>
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Fires when form window resizes
    ''' </summary>
    Private Sub Form_Resize(sender As Object, e As EventArgs)
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
    ''' Calculate positions and apply layout using ClientSize (CRITICAL for resolution changes)
    ''' </summary>
    Private Sub CalculateAndApplyLayout()
        ' === Use form's actual client size (tracks resolution changes) ===
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' === Calculate scale factor ===
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        ' === BARANGAY INFO TITLE - Designer: Location(23, 25), Size(345, 37) ===
        _form.BrgyInfoLbl.Location = New Point(CInt(23 * scaleFactor), CInt(25 * scaleFactor))
        _form.BrgyInfoLbl.Font = New Font("Arial", 24.0F * scaleFactor, FontStyle.Bold)

        ' === LINE PANEL - Designer: Location(0, 81), Size(1700, 2) ===
        _form.LinePnl.Location = New Point(0, CInt(81 * scaleFactor))
        _form.LinePnl.Height = 2
        _form.LinePnl.Width = panelWidth

        ' === BARANGAY DETAILS LABEL - Designer: Location(745, 96), Size(237, 32) ===
        _form.BrgyDetailsLbl.Location = New Point(CInt(745 * scaleFactor), CInt(96 * scaleFactor))
        _form.BrgyDetailsLbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)

        ' === BARANGAY NAME LABEL - Designer: Location(40, 149), Size(164, 22) ===
        _form.BrgyNameLbl.Location = New Point(CInt(40 * scaleFactor), CInt(149 * scaleFactor))
        _form.BrgyNameLbl.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' === BARANGAY NAME TEXTBOX - Designer: Location(210, 149), Size(1320, 29) ===
        _form.txtBrgyName.Location = New Point(CInt(210 * scaleFactor), CInt(149 * scaleFactor))
        _form.txtBrgyName.Size = New Size(CInt(1320 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtBrgyName.Font = New Font("Arial", 14.25F * scaleFactor)

        ' === BARANGAY MISSION LABEL - Designer: Location(40, 202), Size(90, 22) ===
        _form.BrgyMissionLbl.Location = New Point(CInt(40 * scaleFactor), CInt(202 * scaleFactor))
        _form.BrgyMissionLbl.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' === BARANGAY MISSION RICHTEXTBOX - Designer: Location(210, 202), Size(1320, 85) ===
        _form.BrgyMissionRtxt.Location = New Point(CInt(210 * scaleFactor), CInt(202 * scaleFactor))
        _form.BrgyMissionRtxt.Size = New Size(CInt(1320 * scaleFactor), CInt(85 * scaleFactor))
        _form.BrgyMissionRtxt.Font = New Font("Arial", 14.25F * scaleFactor)

        ' === BARANGAY VISION LABEL - Designer: Location(40, 303), Size(75, 22) ===
        _form.BrgyVisionLbl.Location = New Point(CInt(40 * scaleFactor), CInt(303 * scaleFactor))
        _form.BrgyVisionLbl.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' === BARANGAY VISION RICHTEXTBOX - Designer: Location(210, 303), Size(1320, 85) ===
        _form.BrgyVisionRtxt.Location = New Point(CInt(210 * scaleFactor), CInt(303 * scaleFactor))
        _form.BrgyVisionRtxt.Size = New Size(CInt(1320 * scaleFactor), CInt(85 * scaleFactor))
        _form.BrgyVisionRtxt.Font = New Font("Arial", 14.25F * scaleFactor)

        ' === BARANGAY LOGO LABEL - Designer: Location(40, 407), Size(160, 22) ===
        _form.BrgyLogoLbl.Location = New Point(CInt(40 * scaleFactor), CInt(407 * scaleFactor))
        _form.BrgyLogoLbl.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' === BARANGAY LOGO PICTURE - Designer: Location(251, 407), Size(117, 104) ===
        _form.BrgyLogoPic.Location = New Point(CInt(251 * scaleFactor), CInt(407 * scaleFactor))
        _form.BrgyLogoPic.Size = New Size(CInt(117 * scaleFactor), CInt(104 * scaleFactor))
        _form.BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage

        ' === UPLOAD BUTTON - Designer: Location(435, 407), Size(199, 44) ===
        _form.BtnUpload.Location = New Point(CInt(435 * scaleFactor), CInt(407 * scaleFactor))
        _form.BtnUpload.Size = New Size(CInt(199 * scaleFactor), CInt(44 * scaleFactor))
        _form.BtnUpload.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)

        ' === REMOVE BUTTON - Designer: Location(435, 467), Size(199, 44) ===
        _form.BtnRemove.Location = New Point(CInt(435 * scaleFactor), CInt(467 * scaleFactor))
        _form.BtnRemove.Size = New Size(CInt(199 * scaleFactor), CInt(44 * scaleFactor))
        _form.BtnRemove.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)

        ' === SAVE INFO BUTTON - Designer: Location(661, 516), Size(392, 44) ===
        _form.BtnSaveInfo.Location = New Point(CInt(661 * scaleFactor), CInt(516 * scaleFactor))
        _form.BtnSaveInfo.Size = New Size(CInt(392 * scaleFactor), CInt(44 * scaleFactor))
        _form.BtnSaveInfo.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)

        ' === SEPARATOR LINE 2 - Designer: Location(0, 568), Size(1700, 2) ===
        _form.LinePnl2.Location = New Point(0, CInt(568 * scaleFactor))
        _form.LinePnl2.Height = 2
        _form.LinePnl2.Width = panelWidth

        ' === SEARCH OFFICIAL LABEL - Designer: Location(40, 635), Size(152, 22) ===
        _form.SearchOfficialLbl.Location = New Point(CInt(40 * scaleFactor), CInt(635 * scaleFactor))
        _form.SearchOfficialLbl.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' === SEARCH TEXTBOX - Designer: Location(210, 628), Size(1320, 29) ===
        _form.TxtSearchOfficial.Location = New Point(CInt(210 * scaleFactor), CInt(628 * scaleFactor))
        _form.TxtSearchOfficial.Size = New Size(CInt(1320 * scaleFactor), CInt(29 * scaleFactor))
        _form.TxtSearchOfficial.Font = New Font("Arial", 14.25F * scaleFactor)

        ' === SEARCH BUTTON - Designer: Location(1550, 628), Size(105, 29) ===
        _form.BtnSearch.Location = New Point(CInt(1550 * scaleFactor), CInt(628 * scaleFactor))
        _form.BtnSearch.Size = New Size(CInt(105 * scaleFactor), CInt(29 * scaleFactor))
        _form.BtnSearch.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === DATAGRIDVIEW - Designer: Location(44, 660), Size(1611, 267) ===
        _form.BrgyOfficialsDGV.Location = New Point(CInt(44 * scaleFactor), CInt(660 * scaleFactor))
        _form.BrgyOfficialsDGV.Size = New Size(CInt(1611 * scaleFactor), CInt(267 * scaleFactor))

        ' === ADD NEW OFFICIAL BUTTON - Designer: Location(178, 950), Size(190, 29) ===
        _form.BtnAddNewOfficial.Location = New Point(CInt(178 * scaleFactor), CInt(950 * scaleFactor))
        _form.BtnAddNewOfficial.Size = New Size(CInt(190 * scaleFactor), CInt(29 * scaleFactor))
        _form.BtnAddNewOfficial.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === EDIT SELECTED BUTTON - Designer: Location(751, 950), Size(190, 29) ===
        _form.BtnEditSelected.Location = New Point(CInt(751 * scaleFactor), CInt(950 * scaleFactor))
        _form.BtnEditSelected.Size = New Size(CInt(190 * scaleFactor), CInt(29 * scaleFactor))
        _form.BtnEditSelected.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === ARCHIVE SELECTED BUTTON - Designer: Location(1340, 950), Size(190, 29) ===
        _form.BtnArchieveSelected.Location = New Point(CInt(1340 * scaleFactor), CInt(950 * scaleFactor))
        _form.BtnArchieveSelected.Size = New Size(CInt(190 * scaleFactor), CInt(29 * scaleFactor))
        _form.BtnArchieveSelected.Font = New Font("Arial Narrow", 12.0F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
