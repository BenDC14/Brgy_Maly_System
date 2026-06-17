Imports System.Drawing.Drawing2D
Imports Microsoft.Win32

''' <summary>
''' Responsive UI Manager specifically for AyudaAudit_Form
''' Handles all layout calculations, positioning, and font scaling
''' </summary>
Public Class AyudaAuditResponsiveManager
    ' === Store original dimensions from Designer ===
    Private Const ORIGINAL_WIDTH As Integer = 1700
    Private Const ORIGINAL_HEIGHT As Integer = 1004

    ' === Reference to the form ===
    Private ReadOnly _form As AyudaAudit_Form

    ' === Timer for debouncing ===
    Private resizeTimer As New System.Windows.Forms.Timer()
    Private isLayoutCalculated As Boolean = False

    ''' <summary>
    ''' Constructor - Initialize with form reference
    ''' </summary>
    Public Sub New(form As AyudaAudit_Form)
        _form = form
    End Sub

    ''' <summary>
    ''' Initialize responsive behavior
    ''' </summary>
    Public Sub Initialize()
        Try
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

        Catch ex As Exception
            Debug.WriteLine("Initialize Error: " & ex.Message)
        End Try
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
    ''' </summary>
    Public Sub CalculateAndApplyLayout()
        Try
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

            ' === POSITION ALL SECTIONS ===
            PositionTitleSection(panelWidth, panelHeight, scaleFactor)
            PositionFilterSection(panelWidth, panelHeight, scaleFactor)
            PositionStatisticsSection(panelWidth, panelHeight, scaleFactor)
            PositionFirstDividerLine(panelWidth, panelHeight)
            PositionAyudaInformationSection(panelWidth, panelHeight, scaleFactor)
            PositionSecondDividerLine(panelWidth, panelHeight)
            PositionAuditInformationSection(panelWidth, panelHeight, scaleFactor)
            PositionActionButton(panelWidth, panelHeight, scaleFactor)

        Catch ex As Exception
            Debug.WriteLine("CalculateAndApplyLayout Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Position title section
    ''' </summary>
    Private Sub PositionTitleSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Try
            If _form.lblAyudaAudit IsNot Nothing Then
                _form.lblAyudaAudit.Location = New Point(CInt(panelWidth * 0.007), CInt(panelHeight * 0.009))
                _form.lblAyudaAudit.Font = New Font("Arial", 20.25F * scaleFactor, FontStyle.Bold)
                _form.lblAyudaAudit.Anchor = AnchorStyles.Top Or AnchorStyles.Left
            End If
        Catch ex As Exception
            Debug.WriteLine("PositionTitleSection Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Position filter section
    ''' </summary>
    Private Sub PositionFilterSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Try
            Dim leftMargin As Integer = CInt(panelWidth * 0.089)
            Dim centerMargin As Integer = CInt(panelWidth * 0.361)
            Dim rightMargin As Integer = CInt(panelWidth * 0.638)
            Dim fieldWidth As Integer = CInt(panelWidth * 0.267)

            If _form.StartDateLbl IsNot Nothing Then
                _form.StartDateLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.087))
                _form.StartDateLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
            End If

            If _form.startDTP IsNot Nothing Then
                _form.startDTP.Location = New Point(leftMargin, CInt(panelHeight * 0.114))
                _form.startDTP.Size = New Size(fieldWidth, CInt(panelHeight * 0.029))
                _form.startDTP.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
            End If

            If _form.EndDateLbl IsNot Nothing Then
                _form.EndDateLbl.Location = New Point(centerMargin, CInt(panelHeight * 0.087))
                _form.EndDateLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
            End If

            If _form.endDTP IsNot Nothing Then
                _form.endDTP.Location = New Point(centerMargin, CInt(panelHeight * 0.114))
                _form.endDTP.Size = New Size(fieldWidth, CInt(panelHeight * 0.029))
                _form.endDTP.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
            End If

            If _form.AyudaLbl IsNot Nothing Then
                _form.AyudaLbl.Location = New Point(rightMargin, CInt(panelHeight * 0.089))
                _form.AyudaLbl.Font = New Font("Arial Narrow", 15.75F * scaleFactor, FontStyle.Bold Or FontStyle.Italic)
            End If

            If _form.cbAyuda IsNot Nothing Then
                _form.cbAyuda.Location = New Point(rightMargin, CInt(panelHeight * 0.116))
                _form.cbAyuda.Size = New Size(fieldWidth, CInt(panelHeight * 0.03))
                _form.cbAyuda.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
                _form.cbAyuda.BackColor = Color.FromArgb(237, 237, 237)
            End If

        Catch ex As Exception
            Debug.WriteLine("PositionFilterSection Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Position statistics section
    ''' </summary>
    Private Sub PositionStatisticsSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Try
            Dim leftMargin As Integer = CInt(panelWidth * 0.089)
            Dim centerMargin As Integer = CInt(panelWidth * 0.361)
            Dim rightMargin As Integer = CInt(panelWidth * 0.638)
            Dim fieldWidth As Integer = CInt(panelWidth * 0.267)

            If _form.lblTotalResidentsServed IsNot Nothing Then
                _form.lblTotalResidentsServed.Location = New Point(leftMargin, CInt(panelHeight * 0.195))
                _form.lblTotalResidentsServed.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)
            End If

            If _form.txtResidentServed IsNot Nothing Then
                _form.txtResidentServed.Location = New Point(leftMargin, CInt(panelHeight * 0.243))
                _form.txtResidentServed.Size = New Size(fieldWidth, CInt(panelHeight * 0.029))
                _form.txtResidentServed.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
            End If

            If _form.lblTotalCashReleased IsNot Nothing Then
                _form.lblTotalCashReleased.Location = New Point(centerMargin, CInt(panelHeight * 0.195))
                _form.lblTotalCashReleased.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)
            End If

            If _form.txtCashReleased IsNot Nothing Then
                _form.txtCashReleased.Location = New Point(centerMargin, CInt(panelHeight * 0.243))
                _form.txtCashReleased.Size = New Size(fieldWidth, CInt(panelHeight * 0.029))
                _form.txtCashReleased.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
            End If

            If _form.lblTotalPacksReleased IsNot Nothing Then
                _form.lblTotalPacksReleased.Location = New Point(rightMargin, CInt(panelHeight * 0.195))
                _form.lblTotalPacksReleased.Font = New Font("Arial", 15.75F * scaleFactor, FontStyle.Bold)
            End If

            If _form.txtPacksReleased IsNot Nothing Then
                _form.txtPacksReleased.Location = New Point(rightMargin, CInt(panelHeight * 0.243))
                _form.txtPacksReleased.Size = New Size(fieldWidth, CInt(panelHeight * 0.029))
                _form.txtPacksReleased.Font = New Font("Arial", 14.25F * scaleFactor, FontStyle.Regular)
            End If

        Catch ex As Exception
            Debug.WriteLine("PositionStatisticsSection Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Position first divider line
    ''' </summary>
    Private Sub PositionFirstDividerLine(panelWidth As Integer, panelHeight As Integer)
        Try
            If _form.LinePnl IsNot Nothing Then
                _form.LinePnl.Location = New Point(0, CInt(panelHeight * 0.309))
                _form.LinePnl.Size = New Size(panelWidth, 2)
            End If
        Catch ex As Exception
            Debug.WriteLine("PositionFirstDividerLine Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Position Ayuda Information section
    ''' </summary>
    Private Sub PositionAyudaInformationSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Try
            Dim leftMargin As Integer = CInt(panelWidth * 0.011)
            Dim dgvWidth As Integer = CInt(panelWidth * 0.983)
            Dim dgvHeight As Integer = CInt(panelHeight * 0.197)

            If _form.AyudaInformationLbl IsNot Nothing Then
                _form.AyudaInformationLbl.Location = New Point(leftMargin, CInt(panelHeight * 0.333))
                _form.AyudaInformationLbl.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Bold)
            End If

            If _form.dgvAyudaInfo IsNot Nothing Then
                _form.dgvAyudaInfo.Location = New Point(leftMargin, CInt(panelHeight * 0.364))
                _form.dgvAyudaInfo.Size = New Size(dgvWidth, dgvHeight)
            End If

        Catch ex As Exception
            Debug.WriteLine("PositionAyudaInformationSection Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Position second divider line
    ''' </summary>
    Private Sub PositionSecondDividerLine(panelWidth As Integer, panelHeight As Integer)
        Try
            If _form.LnPnl2 IsNot Nothing Then
                _form.LnPnl2.Location = New Point(0, CInt(panelHeight * 0.607))
                _form.LnPnl2.Size = New Size(panelWidth, 2)
            End If
        Catch ex As Exception
            Debug.WriteLine("PositionSecondDividerLine Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Position Audit Information section
    ''' </summary>
    Private Sub PositionAuditInformationSection(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Try
            Dim leftMargin As Integer = CInt(panelWidth * 0.011)
            Dim dgvWidth As Integer = CInt(panelWidth * 0.983)
            Dim dgvHeight As Integer = CInt(panelHeight * 0.197)

            If _form.AyudaAuditInformationlbl IsNot Nothing Then
                _form.AyudaAuditInformationlbl.Location = New Point(leftMargin, CInt(panelHeight * 0.631))
                _form.AyudaAuditInformationlbl.Font = New Font("Arial", 18.0F * scaleFactor, FontStyle.Bold)
            End If

            If _form.dgvAyudaAuditInformation IsNot Nothing Then
                _form.dgvAyudaAuditInformation.Location = New Point(leftMargin, CInt(panelHeight * 0.663))
                _form.dgvAyudaAuditInformation.Size = New Size(dgvWidth, dgvHeight)
            End If

        Catch ex As Exception
            Debug.WriteLine("PositionAuditInformationSection Error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Position action button
    ''' </summary>
    Private Sub PositionActionButton(panelWidth As Integer, panelHeight As Integer, scaleFactor As Single)
        Try
            If _form.btnPrintAudit IsNot Nothing Then
                Dim btnWidth As Integer = CInt(panelWidth * 0.096)
                Dim btnHeight As Integer = CInt(panelHeight * 0.046)
                Dim btnY As Integer = CInt(panelHeight * 0.9)

                _form.btnPrintAudit.Location = New Point(CInt(panelWidth * 0.448), btnY)
                _form.btnPrintAudit.Size = New Size(btnWidth, btnHeight)
                _form.btnPrintAudit.Font = New Font("Arial Narrow", 20.25F * scaleFactor, FontStyle.Bold)
                _form.btnPrintAudit.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            End If

        Catch ex As Exception
            Debug.WriteLine("PositionActionButton Error: " & ex.Message)
        End Try
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
