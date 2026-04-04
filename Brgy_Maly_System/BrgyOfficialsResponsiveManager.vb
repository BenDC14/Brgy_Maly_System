Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager for BrgyOfficials_Form
''' Handles all layout calculations and positioning
''' </summary>
Public Class BrgyOfficialsResponsiveManager

    ' === Original form dimensions ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As BrgyOfficials_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(form As BrgyOfficials_Form)
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

        ' === BARANGAY OFFICIAL TITLE - Designer: Location(23, 25), Size(352, 37) ===
        _form.BrgyOfficialLbl.Location = New Point(CInt(23 * scaleFactor), CInt(25 * scaleFactor))
        _form.BrgyOfficialLbl.Font = New Font("Arial", 24.0F * scaleFactor, FontStyle.Bold)

        ' === LINE PANEL - Designer: Location(0, 81), Size(1700, 2) ===
        _form.LinePnl.Location = New Point(0, CInt(81 * scaleFactor))
        _form.LinePnl.Height = 2
        _form.LinePnl.Width = panelWidth

        ' === OFFICIAL PHOTO LABEL - Designer: Location(47, 129), Size(141, 22) ===
        _form.BrgyLogoLbl.Location = New Point(CInt(47 * scaleFactor), CInt(129 * scaleFactor))
        _form.BrgyLogoLbl.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' === OFFICIAL PHOTO PICTURE - Designer: Location(245, 129), Size(117, 104) ===
        _form.BrgyLogoPic.Location = New Point(CInt(245 * scaleFactor), CInt(129 * scaleFactor))
        _form.BrgyLogoPic.Size = New Size(CInt(117 * scaleFactor), CInt(104 * scaleFactor))
        _form.BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage

        ' === UPLOAD BUTTON - Designer: Location(434, 160), Size(199, 44) ===
        _form.BtnUpload.Location = New Point(CInt(434 * scaleFactor), CInt(160 * scaleFactor))
        _form.BtnUpload.Size = New Size(CInt(199 * scaleFactor), CInt(44 * scaleFactor))
        _form.BtnUpload.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)

        ' === REMOVE BUTTON - Designer: Location(709, 160), Size(199, 44) ===
        _form.BtnRemove.Location = New Point(CInt(709 * scaleFactor), CInt(160 * scaleFactor))
        _form.BtnRemove.Size = New Size(CInt(199 * scaleFactor), CInt(44 * scaleFactor))
        _form.BtnRemove.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)

        ' === FIRST NAME LABEL - Designer: Location(46, 290), Size(109, 25) ===
        _form.FNameLbl.Location = New Point(CInt(46 * scaleFactor), CInt(290 * scaleFactor))
        _form.FNameLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === FIRST NAME TEXTBOX - Designer: Location(245, 290), Size(1308, 35) ===
        _form.txtFname.Location = New Point(CInt(245 * scaleFactor), CInt(290 * scaleFactor))
        _form.txtFname.Size = New Size(CInt(1308 * scaleFactor), CInt(35 * scaleFactor))
        _form.txtFname.Font = New Font("Arial", 18.0F * scaleFactor)

        ' === LAST NAME LABEL - Designer: Location(46, 402), Size(107, 25) ===
        _form.Lnamelbl.Location = New Point(CInt(46 * scaleFactor), CInt(402 * scaleFactor))
        _form.Lnamelbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === LAST NAME TEXTBOX - Designer: Location(245, 402), Size(1308, 35) ===
        _form.txtLname.Location = New Point(CInt(245 * scaleFactor), CInt(402 * scaleFactor))
        _form.txtLname.Size = New Size(CInt(1308 * scaleFactor), CInt(35 * scaleFactor))
        _form.txtLname.Font = New Font("Arial", 18.0F * scaleFactor)

        ' === POSITION LABEL - Designer: Location(46, 513), Size(88, 25) ===
        _form.PositionLbl.Location = New Point(CInt(46 * scaleFactor), CInt(513 * scaleFactor))
        _form.PositionLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === POSITION COMBOBOX - Designer: Location(245, 513), Size(1308, 35) ===
        _form.cbPosition.Location = New Point(CInt(245 * scaleFactor), CInt(513 * scaleFactor))
        _form.cbPosition.Size = New Size(CInt(1308 * scaleFactor), CInt(35 * scaleFactor))
        _form.cbPosition.Font = New Font("Arial", 18.0F * scaleFactor)

        ' === TERM START LABEL - Designer: Location(46, 621), Size(106, 25) ===
        _form.TermStartLbl.Location = New Point(CInt(46 * scaleFactor), CInt(621 * scaleFactor))
        _form.TermStartLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === TERM START DATETIMEPICKER - Designer: Location(245, 621), Size(1308, 35) ===
        _form.TermStartDTP.Location = New Point(CInt(245 * scaleFactor), CInt(621 * scaleFactor))
        _form.TermStartDTP.Size = New Size(CInt(1308 * scaleFactor), CInt(35 * scaleFactor))
        _form.TermStartDTP.Font = New Font("Arial", 18.0F * scaleFactor)

        ' === TERM END LABEL - Designer: Location(46, 733), Size(99, 25) ===
        _form.TermEndLbl.Location = New Point(CInt(46 * scaleFactor), CInt(733 * scaleFactor))
        _form.TermEndLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)

        ' === TERM END DATETIMEPICKER - Designer: Location(245, 733), Size(1308, 35) ===
        _form.TermEndDTP.Location = New Point(CInt(245 * scaleFactor), CInt(733 * scaleFactor))
        _form.TermEndDTP.Size = New Size(CInt(1308 * scaleFactor), CInt(35 * scaleFactor))
        _form.TermEndDTP.Font = New Font("Arial", 18.0F * scaleFactor)

        ' === SAVE OFFICIAL BUTTON - Designer: Location(506, 883), Size(284, 44) ===
        _form.BtnSaveOfficial.Location = New Point(CInt(506 * scaleFactor), CInt(883 * scaleFactor))
        _form.BtnSaveOfficial.Size = New Size(CInt(284 * scaleFactor), CInt(44 * scaleFactor))
        _form.BtnSaveOfficial.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)

        ' === BACK TO MAIN BUTTON - Designer: Location(1007, 883), Size(284, 44) ===
        _form.BtnBackToMain.Location = New Point(CInt(1007 * scaleFactor), CInt(883 * scaleFactor))
        _form.BtnBackToMain.Size = New Size(CInt(284 * scaleFactor), CInt(44 * scaleFactor))
        _form.BtnBackToMain.Font = New Font("Arial Narrow", 11.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
