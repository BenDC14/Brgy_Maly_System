Imports Microsoft.Win32

''' <summary>
''' Handles the responsive layout for AyudaAdd_Form.
''' This class resizes and repositions controls when the form size or screen resolution changes.
''' </summary>
Public Class AyudaAddResponsiveUIManager

    ' These are the original form dimensions from the Designer.
    ' All positions and sizes are scaled based on these values.
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' This stores the form being managed.
    Private ReadOnly _form As AyudaAdd_Form

    ' Timer used to avoid recalculating layout too many times while resizing.
    Private resizeTimer As New System.Windows.Forms.Timer()

    ' Prevents resize logic from running before the first layout is ready.
    Private isLayoutCalculated As Boolean = False

    Public Sub New(form As AyudaAdd_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Starts the responsive behavior.
    ''' Call this once from AyudaAdd_Form_Load.
    ''' </summary>
    Public Sub Initialize()
        ' Make sure FillPanel always fills the whole form.
        _form.FillPanel.Size = New Size(_form.ClientSize.Width, _form.ClientSize.Height)
        _form.FillPanel.Location = New Point(0, 0)
        _form.FillPanel.Dock = DockStyle.Fill
        _form.FillPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' Debounce timer: layout recalculates after resizing stops.
        resizeTimer.Interval = 300
        AddHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        ' Recalculate when the form itself is resized.
        AddHandler _form.Resize, AddressOf Form_Resize

        ' Recalculate when Windows display settings or resolution changes.
        AddHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged

        ' Apply the first layout immediately.
        _form.FillPanel.PerformLayout()
        Application.DoEvents()
        CalculateAndApplyLayout()

        isLayoutCalculated = True
    End Sub

    ''' <summary>
    ''' Runs when Windows display resolution changes.
    ''' </summary>
    Private Sub SystemDisplayChanged(sender As Object, e As EventArgs)
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Runs whenever the form is resized.
    ''' The timer prevents the layout from recalculating repeatedly while dragging.
    ''' </summary>
    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If Not isLayoutCalculated Then Exit Sub

        resizeTimer.Stop()
        resizeTimer.Start()
    End Sub

    ''' <summary>
    ''' Runs after resizing pauses.
    ''' </summary>
    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs)
        resizeTimer.Stop()
        CalculateAndApplyLayout()
    End Sub

    ''' <summary>
    ''' Calculates the current scale based on form size,
    ''' then applies scaled position, size, and font to every control.
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        Dim panelWidth As Integer = _form.ClientSize.Width
        Dim panelHeight As Integer = _form.ClientSize.Height

        ' Avoid layout errors if the form is minimized or too small.
        If panelWidth < 100 Or panelHeight < 100 Then Exit Sub

        ' Calculate width and height scale compared to the original Designer size.
        Dim widthScale As Single = CSng(panelWidth) / ORIGINAL_WIDTH
        Dim heightScale As Single = CSng(panelHeight) / ORIGINAL_HEIGHT

        ' Use the smaller scale so controls do not stretch unevenly.
        Dim scaleFactor As Single = Math.Min(widthScale, heightScale)

        ' Keep FillPanel aligned to the form.
        _form.FillPanel.Size = New Size(panelWidth, panelHeight)
        _form.FillPanel.Location = New Point(0, 0)

        ' Title
        _form.lblNewAyuda.Location = New Point(CInt(20 * scaleFactor), CInt(20 * scaleFactor))
        _form.lblNewAyuda.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Bold)
        _form.lblNewAyuda.AutoSize = True

        ' DataGridView list of ayuda programs
        _form.dgvAyudas.Location = New Point(CInt(12 * scaleFactor), CInt(87 * scaleFactor))
        _form.dgvAyudas.Size = New Size(CInt(1676 * scaleFactor), CInt(297 * scaleFactor))

        ' Search controls
        _form.lblSearch.Location = New Point(CInt(1309 * scaleFactor), CInt(56 * scaleFactor))
        _form.lblSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Bold)
        _form.lblSearch.AutoSize = True

        _form.txtSearch.Location = New Point(CInt(1398 * scaleFactor), CInt(52 * scaleFactor))
        _form.txtSearch.Size = New Size(CInt(290 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtSearch.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Giver / source agency
        _form.lblAyudaGiver.Location = New Point(CInt(20 * scaleFactor), CInt(430 * scaleFactor))
        _form.lblAyudaGiver.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.lblAyudaGiver.AutoSize = True

        _form.txtResidentType.Location = New Point(CInt(25 * scaleFactor), CInt(458 * scaleFactor))
        _form.txtResidentType.Size = New Size(CInt(618 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtResidentType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Program name
        _form.lblProgramName.Location = New Point(CInt(20 * scaleFactor), CInt(532 * scaleFactor))
        _form.lblProgramName.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.lblProgramName.AutoSize = True

        _form.txtQuantity.Location = New Point(CInt(25 * scaleFactor), CInt(558 * scaleFactor))
        _form.txtQuantity.Size = New Size(CInt(618 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtQuantity.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Assistance type
        _form.lblAssistanceType.Location = New Point(CInt(20 * scaleFactor), CInt(623 * scaleFactor))
        _form.lblAssistanceType.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.lblAssistanceType.AutoSize = True

        _form.cbAssistanceType.Location = New Point(CInt(25 * scaleFactor), CInt(651 * scaleFactor))
        _form.cbAssistanceType.Size = New Size(CInt(434 * scaleFactor), CInt(30 * scaleFactor))
        _form.cbAssistanceType.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Other assistance type textbox
        _form.txtOthers.Location = New Point(CInt(465 * scaleFactor), CInt(652 * scaleFactor))
        _form.txtOthers.Size = New Size(CInt(178 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtOthers.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Assistance provision
        _form.lblAssistanceProvision.Location = New Point(CInt(20 * scaleFactor), CInt(709 * scaleFactor))
        _form.lblAssistanceProvision.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.lblAssistanceProvision.AutoSize = True

        _form.txtAssistanceProvision.Location = New Point(CInt(25 * scaleFactor), CInt(737 * scaleFactor))
        _form.txtAssistanceProvision.Size = New Size(CInt(618 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtAssistanceProvision.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Resident category label and ComboBox
        ' These are the new controls you added in the Designer.
        _form.lblCategories.Location = New Point(CInt(20 * scaleFactor), CInt(778 * scaleFactor))
        _form.lblCategories.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.lblCategories.AutoSize = True

        _form.cbcategories.Location = New Point(CInt(25 * scaleFactor), CInt(806 * scaleFactor))
        _form.cbcategories.Size = New Size(CInt(1663 * scaleFactor), CInt(30 * scaleFactor))
        _form.cbcategories.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Active / available radio buttons
        _form.lblAvailOrActive.Location = New Point(CInt(1065 * scaleFactor), CInt(720 * scaleFactor))
        _form.lblAvailOrActive.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.lblAvailOrActive.AutoSize = True

        _form.rbIsActiveYes.Location = New Point(CInt(1070 * scaleFactor), CInt(748 * scaleFactor))
        _form.rbIsActiveYes.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
        _form.rbIsActiveYes.AutoSize = True

        _form.rbIsActiveNo.Location = New Point(CInt(1295 * scaleFactor), CInt(748 * scaleFactor))
        _form.rbIsActiveNo.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
        _form.rbIsActiveNo.AutoSize = True

        ' Term start date
        _form.AyudaStartLbl.Location = New Point(CInt(1065 * scaleFactor), CInt(530 * scaleFactor))
        _form.AyudaStartLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.AyudaStartLbl.AutoSize = True

        _form.AyudaStartDTP.Location = New Point(CInt(1070 * scaleFactor), CInt(558 * scaleFactor))
        _form.AyudaStartDTP.Size = New Size(CInt(618 * scaleFactor), CInt(29 * scaleFactor))
        _form.AyudaStartDTP.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Term end date
        _form.AyudaEndLbl.Location = New Point(CInt(1065 * scaleFactor), CInt(624 * scaleFactor))
        _form.AyudaEndLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.AyudaEndLbl.AutoSize = True

        _form.AyudaEndDTP.Location = New Point(CInt(1070 * scaleFactor), CInt(652 * scaleFactor))
        _form.AyudaEndDTP.Size = New Size(CInt(618 * scaleFactor), CInt(29 * scaleFactor))
        _form.AyudaEndDTP.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Target slots
        _form.lblTargetSlots.Location = New Point(CInt(1065 * scaleFactor), CInt(430 * scaleFactor))
        _form.lblTargetSlots.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.lblTargetSlots.AutoSize = True

        _form.txtTargetSlots.Location = New Point(CInt(1070 * scaleFactor), CInt(458 * scaleFactor))
        _form.txtTargetSlots.Size = New Size(CInt(618 * scaleFactor), CInt(29 * scaleFactor))
        _form.txtTargetSlots.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)

        ' Save button
        _form.btnSave.Location = New Point(CInt(518 * scaleFactor), CInt(899 * scaleFactor))
        _form.btnSave.Size = New Size(CInt(245 * scaleFactor), CInt(46 * scaleFactor))
        _form.btnSave.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnSave.Cursor = Cursors.Hand

        ' Edit button
        _form.btnEdit.Location = New Point(CInt(884 * scaleFactor), CInt(899 * scaleFactor))
        _form.btnEdit.Size = New Size(CInt(245 * scaleFactor), CInt(46 * scaleFactor))
        _form.btnEdit.Font = New Font("Arial Narrow", 14.25F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
        _form.btnEdit.Cursor = Cursors.Hand
    End Sub

    ''' <summary>
    ''' Removes event handlers when the form closes.
    ''' This helps avoid memory leaks or duplicate resize events.
    ''' </summary>
    Public Sub Cleanup()
        resizeTimer.Stop()

        RemoveHandler SystemEvents.DisplaySettingsChanged, AddressOf SystemDisplayChanged
        RemoveHandler _form.Resize, AddressOf Form_Resize
        RemoveHandler resizeTimer.Tick, AddressOf ResizeTimer_Tick

        resizeTimer.Dispose()
    End Sub

End Class
