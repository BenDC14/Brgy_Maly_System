Imports System.Drawing.Drawing2D

''' <summary>
''' UI Event Layer for NewReportType_Form.
''' Handles dual cascading ComboBoxes:
'''   cbReportType  → parent (reporttype table)
'''   cbreportsubtype → child filtered by selected ReportTypeId (report_sub_type table)
'''
''' No SQL or ADO.NET code belongs in this file.
''' All database operations are delegated to NewReportTypeLogic.
''' </summary>
Public Class NewReportType_Form

    ' ── Dependencies ────────────────────────────────────────────────────────────
    Private _logic As New NewReportTypeLogic()
    Private _responsiveManager As NewReportTypeResponsiveManager

    ' ── State: active editing PKs (-1 = Add-New mode) ───────────────────────────
    Private _editingReportTypeId As Integer = -1
    Private _editingSubTypeId As Integer = -1
    Private _selectedParentTypeId As Integer = -1   ' tracks the currently chosen parent

    ' ── Event suppression flags (prevent re-entrant combo events) ───────────────
    Private _suppressTypeEvent As Boolean = False
    Private _suppressSubTypeEvent As Boolean = False

    ' ── Sentinel strings pinned at index 0 of each ComboBox ─────────────────────
    Private Const SENTINEL_TYPE As String = "[Add New Type]"
    Private Const SENTINEL_SUB_TYPE As String = "[Add New Sub-Type]"

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Construction
    ' ─────────────────────────────────────────────────────────────────────────────

    Public Sub New()
        InitializeComponent()
        Me.DoubleBuffered = True
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Form Load
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub NewReportType_Form_Load(sender As Object, e As EventArgs) _
            Handles MyBase.Load

        ' 1. Responsive layout first so all control positions are correct
        _responsiveManager = New NewReportTypeResponsiveManager(Me)
        _responsiveManager.Initialize()

        ' 2. Visual polish
        ApplyVisualStyling()

        ' 3. Start both sides in idle state
        ResetTypeEditorState()
        ResetSubTypeEditorState()

        ' 4. Populate parent ComboBox from the database
        LoadReportTypeCombo()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' ComboBox population
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Fills cbreporttype via Items.Add.
    ''' SENTINEL_TYPE is always pinned at index 0.
    ''' Existing DB rows follow in alphabetical order (returned by logic).
    ''' </summary>
    Private Sub LoadReportTypeCombo()
        _suppressTypeEvent = True
        Try
            cbreporttype.Items.Clear()
            cbreporttype.Items.Add(SENTINEL_TYPE)           ' index 0 — always first

            Dim dt As DataTable = _logic.GetReportTypes()
            For Each row As DataRow In dt.Rows
                cbreporttype.Items.Add(New ReportTypeItem(
                    CInt(row("ReportTypeId")),
                    row("ReportTypeName").ToString()))
            Next

            cbreporttype.SelectedIndex = -1
        Finally
            _suppressTypeEvent = False
        End Try

        ' Reset child combo whenever parent reloads
        ResetSubTypeCombo()
    End Sub

    ''' <summary>
    ''' Fills cbreportsubtype for a given parent ReportTypeId.
    ''' SENTINEL_SUB_TYPE is always pinned at index 0.
    ''' Only rows that belong to the selected parent are loaded (cascading filter).
    ''' </summary>
    Private Sub LoadSubTypeCombo(parentTypeId As Integer)
        _suppressSubTypeEvent = True
        Try
            cbreportsubtype.Items.Clear()
            cbreportsubtype.Items.Add(SENTINEL_SUB_TYPE)    ' index 0 — always first

            Dim dt As DataTable = _logic.GetReportSubTypes(parentTypeId)
            For Each row As DataRow In dt.Rows
                cbreportsubtype.Items.Add(New ReportSubTypeItem(
                    CInt(row("ReportsSubTypeId")),
                    CInt(row("ReportTypeId")),
                    row("ReportSubTypeName").ToString()))
            Next

            cbreportsubtype.SelectedIndex = -1
            cbreportsubtype.Enabled = True
        Finally
            _suppressSubTypeEvent = False
        End Try
    End Sub

    ''' <summary>
    ''' Clears and disables the child combo when no valid parent is selected.
    ''' </summary>
    Private Sub ResetSubTypeCombo()
        _suppressSubTypeEvent = True
        Try
            cbreportsubtype.Items.Clear()
            cbreportsubtype.Enabled = False
        Finally
            _suppressSubTypeEvent = False
        End Try
        ResetSubTypeEditorState()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' cbreporttype — SelectedIndexChanged (PARENT / cascading trigger)
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Branches on whether SENTINEL_TYPE or an existing report type was selected.
    '''
    ''' • Sentinel → ADD mode for report type:
    '''     Clear txtReportType and enable it.  Child combo is cleared.
    '''
    ''' • Existing type → EDIT mode for report type:
    '''     Auto-populate txtReportType instantly with the selected name.
    '''     Filter cbreportsubtype to only show children of this parent.
    '''     No MsgBox is shown.
    ''' </summary>
    Private Sub cbreporttype_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles cbreporttype.SelectedIndexChanged

        If _suppressTypeEvent Then Return
        If cbreporttype.SelectedIndex < 0 Then Return

        Dim selected As Object = cbreporttype.SelectedItem

        ' ── Sentinel selected → ADD NEW TYPE mode ────────────────────────────
        If TypeOf selected Is String AndAlso
           DirectCast(selected, String) = SENTINEL_TYPE Then

            _editingReportTypeId = -1
            _selectedParentTypeId = -1

            txtReportType.Clear()
            txtReportType.Enabled = True
            txtReportType.BackColor = Color.White
            txtReportType.Focus()

            btnAddNewReportType.Enabled = True
            btnEditReportType.Enabled = False

            lblReportType.Text = "New Report Type:"

            ' Child combo must be cleared when no real parent is selected
            ResetSubTypeCombo()
            Return
        End If

        ' ── Existing type selected → EDIT mode + cascade filter ──────────────
        If TypeOf selected Is ReportTypeItem Then
            Dim item As ReportTypeItem = DirectCast(selected, ReportTypeItem)

            _editingReportTypeId = item.Id
            _selectedParentTypeId = item.Id

            ' Auto-populate the editing side-TextBox instantly — no popup
            txtReportType.Text = item.Name
            txtReportType.Enabled = True
            txtReportType.BackColor = Color.White
            txtReportType.Focus()
            txtReportType.SelectAll()

            btnAddNewReportType.Enabled = False
            btnEditReportType.Enabled = True

            lblReportType.Text = "Edit Report Type:"

            ' ── Cascade: filter child combo to this parent's sub-types ───────
            ResetSubTypeEditorState()
            LoadSubTypeCombo(item.Id)
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' cbreportsubtype — SelectedIndexChanged (CHILD)
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Branches on whether SENTINEL_SUB_TYPE or an existing sub-type was selected.
    '''
    ''' • Sentinel → ADD mode for sub-type:
    '''     Clear txtReportSubType and enable it.
    '''
    ''' • Existing sub-type → EDIT mode for sub-type:
    '''     Auto-populate txtReportSubType instantly with the selected name.
    '''     No MsgBox is shown.
    ''' </summary>
    Private Sub cbreportsubtype_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles cbreportsubtype.SelectedIndexChanged

        If _suppressSubTypeEvent Then Return
        If cbreportsubtype.SelectedIndex < 0 Then Return

        Dim selected As Object = cbreportsubtype.SelectedItem

        ' ── Sentinel selected → ADD NEW SUB-TYPE mode ────────────────────────
        If TypeOf selected Is String AndAlso
           DirectCast(selected, String) = SENTINEL_SUB_TYPE Then

            _editingSubTypeId = -1

            txtReportSubType.Clear()
            txtReportSubType.Enabled = True
            txtReportSubType.BackColor = Color.White
            txtReportSubType.Focus()

            btnAddNewReportSubType.Enabled = True
            btnEditReportSubType.Enabled = False

            lblReportSubType.Text = "New Sub-Type:"
            Return
        End If

        ' ── Existing sub-type selected → EDIT mode ────────────────────────────
        If TypeOf selected Is ReportSubTypeItem Then
            Dim item As ReportSubTypeItem = DirectCast(selected, ReportSubTypeItem)

            _editingSubTypeId = item.Id

            ' Auto-populate the editing side-TextBox instantly — no popup
            txtReportSubType.Text = item.Name
            txtReportSubType.Enabled = True
            txtReportSubType.BackColor = Color.White
            txtReportSubType.Focus()
            txtReportSubType.SelectAll()

            btnAddNewReportSubType.Enabled = False
            btnEditReportSubType.Enabled = True

            lblReportSubType.Text = "Edit Sub-Type:"
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Add Report Type button
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub btnAddNewReportType_Click(sender As Object, e As EventArgs) _
            Handles btnAddNewReportType.Click

        Dim newName As String = txtReportType.Text.Trim()

        If String.IsNullOrWhiteSpace(newName) Then
            FlashInvalid(txtReportType, "Please enter a report type name.")
            Return
        End If

        Dim result As Integer = _logic.ExecuteNonQuery(newName)

        If result > 0 Then
            ShowSuccess(txtReportType, """" & newName & """ saved.")
            ResetTypeEditorState()
            LoadReportTypeCombo()
        Else
            MsgBox("Failed to save report type. Check your database connection.",
                   MsgBoxStyle.Exclamation, "Save Error")
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Edit Report Type button
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub btnEditReportType_Click(sender As Object, e As EventArgs) _
            Handles btnEditReportType.Click

        If _editingReportTypeId <= 0 Then
            ResetTypeEditorState()
            Return
        End If

        Dim updatedName As String = txtReportType.Text.Trim()

        If String.IsNullOrWhiteSpace(updatedName) Then
            FlashInvalid(txtReportType, "Report type name cannot be empty.")
            Return
        End If

        Dim success As Boolean = _logic.UpdateReportType(_editingReportTypeId, updatedName)

        If success Then
            ShowSuccess(txtReportType, "Updated to """ & updatedName & """.")
            ResetTypeEditorState()
            LoadReportTypeCombo()
        Else
            MsgBox("Update failed. Check your database connection.",
                   MsgBoxStyle.Exclamation, "Update Error")
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Add Report Sub-Type button
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub btnAddNewReportSubType_Click(sender As Object, e As EventArgs) _
            Handles btnAddNewReportSubType.Click

        If _selectedParentTypeId <= 0 Then
            FlashInvalid(txtReportSubType, "Select a parent Report Type first.")
            Return
        End If

        Dim newName As String = txtReportSubType.Text.Trim()

        If String.IsNullOrWhiteSpace(newName) Then
            FlashInvalid(txtReportSubType, "Please enter a sub-type name.")
            Return
        End If

        Dim result As Integer = _logic.AddReportSubType(_selectedParentTypeId, newName)

        If result > 0 Then
            ShowSuccess(txtReportSubType, """" & newName & """ saved.")
            ResetSubTypeEditorState()
            LoadSubTypeCombo(_selectedParentTypeId)
        Else
            MsgBox("Failed to save sub-type. Check your database connection.",
                   MsgBoxStyle.Exclamation, "Save Error")
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Edit Report Sub-Type button
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub btnEditReportSubType_Click(sender As Object, e As EventArgs) _
            Handles btnEditReportSubType.Click

        If _editingSubTypeId <= 0 Then
            ResetSubTypeEditorState()
            Return
        End If

        Dim updatedName As String = txtReportSubType.Text.Trim()

        If String.IsNullOrWhiteSpace(updatedName) Then
            FlashInvalid(txtReportSubType, "Sub-type name cannot be empty.")
            Return
        End If

        Dim success As Boolean = _logic.UpdateReportSubType(_editingSubTypeId, updatedName)

        If success Then
            ShowSuccess(txtReportSubType, "Updated to """ & updatedName & """.")
            ResetSubTypeEditorState()
            If _selectedParentTypeId > 0 Then LoadSubTypeCombo(_selectedParentTypeId)
        Else
            MsgBox("Update failed. Check your database connection.",
                   MsgBoxStyle.Exclamation, "Update Error")
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Exit
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) Handles ExitBtn.Click
        Me.Close()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' State reset helpers
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub ResetTypeEditorState()
        _suppressTypeEvent = True
        Try
            _editingReportTypeId = -1
            cbreporttype.SelectedIndex = -1
        Finally
            _suppressTypeEvent = False
        End Try

        txtReportType.Text = ""
        txtReportType.Enabled = False
        txtReportType.BackColor = Color.FromArgb(237, 237, 237)

        btnAddNewReportType.Enabled = False
        btnEditReportType.Enabled = False
        lblReportType.Text = "Report Type:"

        ResetSubTypeCombo()
    End Sub

    Private Sub ResetSubTypeEditorState()
        _suppressSubTypeEvent = True
        Try
            _editingSubTypeId = -1
            If cbreportsubtype.Items.Count > 0 Then
                cbreportsubtype.SelectedIndex = -1
            End If
        Finally
            _suppressSubTypeEvent = False
        End Try

        txtReportSubType.Text = ""
        txtReportSubType.Enabled = False
        txtReportSubType.BackColor = Color.FromArgb(237, 237, 237)

        btnAddNewReportSubType.Enabled = False
        btnEditReportSubType.Enabled = False
        lblReportSubType.Text = "Report Sub-Type:"
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' UX feedback helpers — no blocking MsgBox for validation / success
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub FlashInvalid(tb As TextBox, message As String)
        tb.BackColor = Color.FromArgb(255, 200, 200)
        tb.Focus()

        Dim tip As New ToolTip()
        tip.Show(message, tb, 0, -24, 2500)

        Dim t As New System.Windows.Forms.Timer() With {.Interval = 650}
        AddHandler t.Tick,
            Sub(s, ev)
                tb.BackColor = If(tb.Enabled, Color.White, Color.FromArgb(237, 237, 237))
                t.Stop()
                t.Dispose()
            End Sub
        t.Start()
    End Sub

    Private Sub ShowSuccess(tb As TextBox, message As String)
        tb.BackColor = Color.FromArgb(200, 255, 210)

        Dim tip As New ToolTip()
        tip.Show(message, tb, 0, -24, 2500)

        Dim t As New System.Windows.Forms.Timer() With {.Interval = 800}
        AddHandler t.Tick,
            Sub(s, ev)
                tb.BackColor = Color.FromArgb(237, 237, 237)
                t.Stop()
                t.Dispose()
            End Sub
        t.Start()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Visual styling
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub ApplyVisualStyling()
        ' Diagonal gradient on FillPanel
        AddHandler FillPanel.Paint,
            Sub(s, pe)
                Using br As New LinearGradientBrush(
                        FillPanel.ClientRectangle,
                        Color.FromArgb(220, 255, 225),
                        Color.White,
                        LinearGradientMode.ForwardDiagonal)
                    pe.Graphics.FillRectangle(br, FillPanel.ClientRectangle)
                End Using
            End Sub

        ' Rounded action buttons
        ApplyRoundedButton(btnAddNewReportType, 18)
        ApplyRoundedButton(btnEditReportType, 18)
        ApplyRoundedButton(btnAddNewReportSubType, 18)
        ApplyRoundedButton(btnEditReportSubType, 18)

        ' Hand cursors
        cbreporttype.Cursor = Cursors.Hand
        cbreportsubtype.Cursor = Cursors.Hand
        btnAddNewReportType.Cursor = Cursors.Hand
        btnEditReportType.Cursor = Cursors.Hand
        btnAddNewReportSubType.Cursor = Cursors.Hand
        btnEditReportSubType.Cursor = Cursors.Hand
        ExitBtn.Cursor = Cursors.Hand

        ' Clean single-line borders on text inputs
        txtReportType.BorderStyle = BorderStyle.FixedSingle
        txtReportSubType.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub ApplyRoundedButton(btn As Button, radius As Integer)
        AddHandler btn.Paint,
            Sub(s, pe)
                pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                Using path As New GraphicsPath()
                    path.AddArc(0, 0, radius, radius, 180, 90)
                    path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
                    path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
                    path.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
                    path.CloseFigure()
                    btn.Region = New Region(path)
                End Using
            End Sub
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Lightweight ComboBox item wrappers
    ' (Stored as items so we can retrieve Id + Name without keeping the DataTable)
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Class ReportTypeItem
        Public ReadOnly Id As Integer
        Public ReadOnly Name As String

        Public Sub New(id As Integer, name As String)
            Me.Id = id
            Me.Name = name
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Class ReportSubTypeItem
        Public ReadOnly Id As Integer
        Public ReadOnly ParentTypeId As Integer
        Public ReadOnly Name As String

        Public Sub New(id As Integer, parentTypeId As Integer, name As String)
            Me.Id = id
            Me.ParentTypeId = parentTypeId
            Me.Name = name
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Cleanup
    ' ─────────────────────────────────────────────────────────────────────────────

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        _responsiveManager?.Cleanup()
        MyBase.OnFormClosing(e)
    End Sub

End Class
