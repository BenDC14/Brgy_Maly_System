Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager for BrgyOfficials_Form.
''' Scales all controls proportionally from the 1700×1004 designer canvas
''' down to the 1366×768 deployment target (and any other resolution).
''' </summary>
Public Class BrgyOfficialsResponsiveManager

    ' ==========================================================================
    '  Designer canvas dimensions — must match the Designer.vb ClientSize
    ' ==========================================================================
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' Minimum readable font sizes (in scaled points)
    Private Const MIN_LABEL_FONT As Single = 7.5F
    Private Const MIN_INPUT_FONT As Single = 8.0F
    Private Const MIN_BTN_FONT As Single = 7.5F
    Private Const MIN_TITLE_FONT As Single = 10.0F

    ' === Reference to the form ===
    Private ReadOnly _form As BrgyOfficials_Form

    ' === Debounce timer ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>Constructor</summary>
    Public Sub New(form As BrgyOfficials_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behaviour — call once from Form_Load after
    ''' InitializeComponent().
    ''' </summary>
    Public Sub Initialize()
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                                 AnchorStyles.Left Or AnchorStyles.Right

        ' Debounce resize at 250 ms to avoid continuous layout thrashing
        resizeTimer.Interval = 250
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        AddHandler _form.Resize, AddressOf Form_Resize
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()
        isLayoutCalculated = True
    End Sub

    ''' <summary>Fires when Windows display settings change (resolution switch).</summary>
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>Fires when the host panel/form is resized.</summary>
    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Exit Sub
        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    ''' <summary>One-shot after resize settles — recalculates layout exactly once.</summary>
    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ' --------------------------------------------------------------------------
    '  Helper: clamp a font size so it never goes below a readable floor
    ' --------------------------------------------------------------------------
    Private Shared Function SafeFont(ByVal size As Single, ByVal minimum As Single) As Single
        Return Math.Max(size, minimum)
    End Function

    ''' <summary>
    ''' Core layout engine.  All coordinates and sizes are derived from the
    ''' original designer values multiplied by a uniform scale factor calculated
    ''' from the current client area.
    '''
    ''' At 1366×768 the scale factor is ≈ 0.7653 (width-constrained), which
    ''' brings all controls cleanly inside the visible area with balanced margins.
    ''' </summary>
    Private Sub CalculateAndApplyLayout()
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        ' Guard against collapsed / minimised state
        If panelWidth < 200 Or panelHeight < 150 Then Exit Sub

        ' Uniform scale — constrained by the tighter dimension so nothing clips
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim sf As Single = Math.Min(widthScale, heightScale)

        ' ------------------------------------------------------------------
        '  TITLE LABEL  — Designer: Location(23,25) Font 24 Bold
        ' ------------------------------------------------------------------
        _form.BrgyOfficialLbl.Location = New Point(CInt(23 * sf), CInt(25 * sf))
        _form.BrgyOfficialLbl.Font = New Font("Arial",
                                                   SafeFont(24.0F * sf, MIN_TITLE_FONT),
                                                   FontStyle.Bold)

        ' ------------------------------------------------------------------
        '  SEPARATOR LINE  — Designer: Location(0,81) Size(1700,2)
        ' ------------------------------------------------------------------
        _form.LinePnl.Location = New Point(0, CInt(81 * sf))
        _form.LinePnl.Height = 2
        _form.LinePnl.Width = panelWidth

        ' ------------------------------------------------------------------
        '  OFFICIAL PHOTO LABEL  — Designer: Location(47,129)
        ' ------------------------------------------------------------------
        _form.BrgyLogoLbl.Location = New Point(CInt(47 * sf), CInt(129 * sf))
        _form.BrgyLogoLbl.Font = New Font("Arial",
                                               SafeFont(14.25F * sf, MIN_LABEL_FONT),
                                               FontStyle.Bold)

        ' ------------------------------------------------------------------
        '  OFFICIAL PHOTO PICTURE  — Designer: Location(245,129) Size(117,104)
        ' ------------------------------------------------------------------
        _form.BrgyLogoPic.Location = New Point(CInt(245 * sf), CInt(129 * sf))
        _form.BrgyLogoPic.Size = New Size(CInt(117 * sf), CInt(104 * sf))
        _form.BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage

        ' ------------------------------------------------------------------
        '  UPLOAD BUTTON  — Designer: Location(434,160) Size(199,44)
        ' ------------------------------------------------------------------
        _form.BtnUpload.Location = New Point(CInt(434 * sf), CInt(160 * sf))
        _form.BtnUpload.Size = New Size(CInt(199 * sf), CInt(44 * sf))
        _form.BtnUpload.Font = New Font("Arial Narrow",
                                             SafeFont(11.25F * sf, MIN_BTN_FONT),
                                             FontStyle.Bold)

        ' ------------------------------------------------------------------
        '  REMOVE BUTTON  — Designer: Location(709,160) Size(199,44)
        ' ------------------------------------------------------------------
        _form.BtnRemove.Location = New Point(CInt(709 * sf), CInt(160 * sf))
        _form.BtnRemove.Size = New Size(CInt(199 * sf), CInt(44 * sf))
        _form.BtnRemove.Font = New Font("Arial Narrow",
                                             SafeFont(11.25F * sf, MIN_BTN_FONT),
                                             FontStyle.Bold)

        ' ------------------------------------------------------------------
        '  FIRST NAME  — Label: Location(46,290)  TextBox: Location(245,290) Size(1308,35)
        ' ------------------------------------------------------------------
        _form.FNameLbl.Location = New Point(CInt(46 * sf), CInt(290 * sf))
        _form.FNameLbl.Font = New Font("Arial Narrow",
                                            SafeFont(15.75F * sf, MIN_LABEL_FONT),
                                            FontStyle.Bold Or FontStyle.Italic)

        _form.txtFname.Location = New Point(CInt(245 * sf), CInt(290 * sf))
        _form.txtFname.Size = New Size(CInt(1308 * sf), CInt(35 * sf))
        _form.txtFname.Font = New Font("Arial",
                                            SafeFont(18.0F * sf, MIN_INPUT_FONT))

        ' ------------------------------------------------------------------
        '  LAST NAME  — Label: Location(46,402)  TextBox: Location(245,402) Size(1308,35)
        ' ------------------------------------------------------------------
        _form.Lnamelbl.Location = New Point(CInt(46 * sf), CInt(402 * sf))
        _form.Lnamelbl.Font = New Font("Arial Narrow",
                                            SafeFont(15.75F * sf, MIN_LABEL_FONT),
                                            FontStyle.Bold Or FontStyle.Italic)

        _form.txtLname.Location = New Point(CInt(245 * sf), CInt(402 * sf))
        _form.txtLname.Size = New Size(CInt(1308 * sf), CInt(35 * sf))
        _form.txtLname.Font = New Font("Arial",
                                            SafeFont(18.0F * sf, MIN_INPUT_FONT))

        ' ------------------------------------------------------------------
        '  POSITION  — Label: Location(46,513)  ComboBox: Location(245,513) Size(1308,35)
        ' ------------------------------------------------------------------
        _form.PositionLbl.Location = New Point(CInt(46 * sf), CInt(513 * sf))
        _form.PositionLbl.Font = New Font("Arial Narrow",
                                               SafeFont(15.75F * sf, MIN_LABEL_FONT),
                                               FontStyle.Bold Or FontStyle.Italic)

        _form.cbPosition.Location = New Point(CInt(245 * sf), CInt(513 * sf))
        _form.cbPosition.Size = New Size(CInt(1308 * sf), CInt(35 * sf))
        _form.cbPosition.Font = New Font("Arial",
                                              SafeFont(18.0F * sf, MIN_INPUT_FONT))

        ' ------------------------------------------------------------------
        '  TERM START  — Label: Location(46,621)  DTP: Location(245,621) Size(1308,35)
        ' ------------------------------------------------------------------
        _form.TermStartLbl.Location = New Point(CInt(46 * sf), CInt(621 * sf))
        _form.TermStartLbl.Font = New Font("Arial Narrow",
                                                SafeFont(15.75F * sf, MIN_LABEL_FONT),
                                                FontStyle.Bold Or FontStyle.Italic)

        _form.TermStartDTP.Location = New Point(CInt(245 * sf), CInt(621 * sf))
        _form.TermStartDTP.Size = New Size(CInt(1308 * sf), CInt(35 * sf))
        _form.TermStartDTP.Font = New Font("Arial",
                                                SafeFont(18.0F * sf, MIN_INPUT_FONT))

        ' ------------------------------------------------------------------
        '  TERM END  — Label: Location(46,733)  DTP: Location(245,733) Size(1308,35)
        ' ------------------------------------------------------------------
        _form.TermEndLbl.Location = New Point(CInt(46 * sf), CInt(733 * sf))
        _form.TermEndLbl.Font = New Font("Arial Narrow",
                                              SafeFont(15.75F * sf, MIN_LABEL_FONT),
                                              FontStyle.Bold Or FontStyle.Italic)

        _form.TermEndDTP.Location = New Point(CInt(245 * sf), CInt(733 * sf))
        _form.TermEndDTP.Size = New Size(CInt(1308 * sf), CInt(35 * sf))
        _form.TermEndDTP.Font = New Font("Arial",
                                              SafeFont(18.0F * sf, MIN_INPUT_FONT))

        ' ------------------------------------------------------------------
        '  SAVE BUTTON  — Designer: Location(506,883) Size(284,44)
        ' ------------------------------------------------------------------
        _form.BtnSaveOfficial.Location = New Point(CInt(506 * sf), CInt(883 * sf))
        _form.BtnSaveOfficial.Size = New Size(CInt(284 * sf), CInt(44 * sf))
        _form.BtnSaveOfficial.Font = New Font("Arial Narrow",
                                                    SafeFont(11.25F * sf, MIN_BTN_FONT),
                                                    FontStyle.Bold)

        ' ------------------------------------------------------------------
        '  BACK TO MAIN BUTTON  — Designer: Location(1007,883) Size(284,44)
        ' ------------------------------------------------------------------
        _form.BtnBackToMain.Location = New Point(CInt(1007 * sf), CInt(883 * sf))
        _form.BtnBackToMain.Size = New Size(CInt(284 * sf), CInt(44 * sf))
        _form.BtnBackToMain.Font = New Font("Arial Narrow",
                                                  SafeFont(11.25F * sf, MIN_BTN_FONT),
                                                  FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Cleanup — must be called from OnFormClosing to prevent memory leaks.
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()
        resizeTimer.Dispose()
        RemoveHandler _form.Resize, AddressOf Form_Resize
        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
    End Sub

End Class
