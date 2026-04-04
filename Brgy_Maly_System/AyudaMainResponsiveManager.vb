Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for AyudaMain_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class AyudaMainResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As AyudaMain_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As AyudaMain_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior
    ''' </summary>
    Public Sub Initialize()
        ' === CRITICAL: Override Designer's fixed size on FillPanel ===
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.FillPanel.Location = New Point(0, 0)

        ' === MAIN CONTAINER - FILL AVAILABLE SPACE ===
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' === Setup timer to debounce resize events ===
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' === Add resize event to recalculate layout when window resizes ===
        AddHandler _form.Resize, AddressOf Form_Resize

        ' === CRITICAL: Listen for system resolution changes ===
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' === Calculate and apply layout for the first time ===
        _form.FillPanel.PerformLayout()
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
    ''' Calculate positions and apply layout based on current form size
    ''' Uses PERCENTAGES for positioning and sizing
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        ' === Use form's actual client size ===
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' === Calculate scale factor for font sizing ===
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        ' === Update FillPanel ===
        _form.FillPanel.Size = New Size(panelWidth, panelHeight)
        _form.FillPanel.Location = New Point(0, 0)

        ' === TITLE SECTION ===
        PositionTitleSection(panelWidth, panelHeight, scaleFactor)

        ' === ACTION BUTTONS (Top Left) ===
        PositionTopActionButtons(panelWidth, panelHeight, scaleFactor)

        ' === SEARCH SECTION ===
        PositionSearchSection(panelWidth, panelHeight, scaleFactor)

        ' === DATA GRID VIEW ===
        PositionDataGridView(panelWidth, panelHeight)

        ' === ROW ACTION BUTTONS (Edit, Archive) ===
        PositionRowActionButtons(panelWidth, panelHeight, scaleFactor)
    End Sub

    ''' <summary>
    ''' Position "Ayuda Information" title
    ''' Designer: Location(20, 20)
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        _form.AyudaInfolbl.Location = New Point(CInt(panelWidth * 0.012), CInt(panelHeight * 0.02))
        _form.AyudaInfolbl.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.AyudaInfolbl.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
    End Sub

    ''' <summary>
    ''' Position top action buttons
    ''' Designer: btnRecordNewAyuda Location(12, 88), Size(226, 45)
    ''' </summary>
    Private Sub PositionTopActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Record New Ayuda Button - Designer: Location(12, 88), Size(226, 45)
        _form.btnRecordNewAyuda.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.088))
        _form.btnRecordNewAyuda.Size = New Size(CInt(panelWidth * 0.133), CInt(panelHeight * 0.045))
        _form.btnRecordNewAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _form.btnRecordNewAyuda.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnRecordNewAyuda.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position search section (label, textbox, search button)
    ''' Designer: lblSearch Location(1152, 100), txtSearch Location(1241, 96) Size(290, 29), btnSearch Location(1537, 96) Size(151, 29)
    ''' </summary>
    Private Sub PositionSearchSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Search Label - Designer: Location(1152, 100)
        _form.lblSearch.Location = New Point(CInt(panelWidth * 0.678), CInt(panelHeight * 0.1))
        _form.lblSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)

        ' Search TextBox - Designer: Location(1241, 96), Size(290, 29)
        _form.txtSearch.Location = New Point(CInt(panelWidth * 0.73), CInt(panelHeight * 0.096))
        _form.txtSearch.Size = New Size(CInt(panelWidth * 0.171), CInt(panelHeight * 0.029))
        _form.txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.txtSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Search Button - Designer: Location(1537, 96), Size(151, 29)
        _form.btnSearch.Location = New Point(CInt(panelWidth * 0.904), CInt(panelHeight * 0.096))
        _form.btnSearch.Size = New Size(CInt(panelWidth * 0.089), CInt(panelHeight * 0.029))
        _form.btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnSearch.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSearch.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Position DataGridView
    ''' Designer: Location(12, 139), Size(1676, 830)
    ''' </summary>
    Private Sub PositionDataGridView(panelWidth As Integer, panelHeight As Integer)
        _form.dgvResidentAyudas.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.138))
        _form.dgvResidentAyudas.Size = New Size(CInt(panelWidth * 0.986), CInt(panelHeight * 0.826))
        _form.dgvResidentAyudas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End Sub

    ''' <summary>
    ''' Position row action buttons (Edit, Archive)
    ''' Designer: btnEdit Location(1465, 191) Size(85, 29), btnArchieve Location(1571, 191) Size(100, 29)
    ''' </summary>
    Private Sub PositionRowActionButtons(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        ' Edit Button - Designer: Location(1465, 191), Size(85, 29)
        _form.btnEdit.Location = New Point(CInt(panelWidth * 0.862), CInt(panelHeight * 0.191))
        _form.btnEdit.Size = New Size(CInt(panelWidth * 0.05), CInt(panelHeight * 0.029))
        _form.btnEdit.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnEdit.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnEdit.Cursor = Cursors.Hand

        ' Archive Button - Designer: Location(1571, 191), Size(100, 29)
        _form.btnArchieve.Location = New Point(CInt(panelWidth * 0.924), CInt(panelHeight * 0.191))
        _form.btnArchieve.Size = New Size(CInt(panelWidth * 0.059), CInt(panelHeight * 0.029))
        _form.btnArchieve.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _form.btnArchieve.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnArchieve.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Cleanup - remove event handlers to prevent memory leaks
    ''' </summary>
    Public Sub Cleanup()
        Try
            resizeTimer.Stop()
            RemoveHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick
            RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
            resizeTimer.Dispose()
        Catch ex As Exception
            Debug.WriteLine("Error during cleanup: " & ex.Message)
        End Try
    End Sub

End Class
