Imports System.Drawing.Drawing2D

''' <summary>
''' UI Event Layer for AddNewRelationshipType_Form.
''' Responsibilities: reacting to control events, reading/writing control state,
''' and delegating all database operations to AddNewRelationshipTypeLogic.
''' No SQL or ADO.NET code belongs in this file.
''' </summary>
Public Class AddNewRelationshipType_Form

    ' ── Dependencies ────────────────────────────────────────────────────────────
    Private _logic As New AddNewRelationshipTypeLogic()
    Private _responsiveManager As AddNewRelationshipTypeResponsiveManager

    ' ── State ───────────────────────────────────────────────────────────────────
    ''' <summary>
    ''' Holds the RelationshipId of the item currently selected for editing.
    ''' -1 means the form is in "Add New" mode.
    ''' </summary>
    Private _editingId As Integer = -1
    Private _suppressComboEvent As Boolean = False

    ' ── Sentinel ────────────────────────────────────────────────────────────────
    ''' <summary>
    ''' The fixed sentinel string pinned at ComboBox index 0.
    ''' Selecting it switches the form into Add-New mode.
    ''' </summary>
    Private Const SENTINEL As String = "[Add New Relationship Type]"

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

    Private Sub AddNewRelationshipType_Form_Load(sender As Object, e As EventArgs) _
            Handles MyBase.Load

        ' 1. Responsive layout — must run before anything else so controls have
        '    correct positions when visual styling is applied.
        _responsiveManager = New AddNewRelationshipTypeResponsiveManager(Me)
        _responsiveManager.Initialize()

        ' 2. Visual polish (gradients, rounded buttons, cursors).
        ApplyVisualStyling()

        ' 3. Reset UI to idle state.
        ResetEditorState()

        ' 4. Populate ComboBox from the database via the Logic layer.
        LoadComboBox()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' ComboBox population
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Fetches all relationship types from the Logic layer and populates
    ''' cbrelationship using Items.Add directly.  The sentinel is hardcoded at
    ''' index 0 so it always appears first, independent of database sort order.
    ''' </summary>
    Private Sub LoadComboBox()
        _suppressComboEvent = True
        Try
            cbrelationship.Items.Clear()
            cbrelationship.Items.Add(SENTINEL)          ' index 0 — always first

            ' Call the Logic layer — no SQL here
            Dim dt As DataTable = _logic.GetRelationshipTypes()
            For Each row As DataRow In dt.Rows
                cbrelationship.Items.Add(row("Relationship").ToString())
            Next

            cbrelationship.SelectedIndex = -1           ' start with nothing selected
        Finally
            _suppressComboEvent = False
        End Try
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' ComboBox — SelectedIndexChanged
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Branches on whether the sentinel or an existing relationship was selected.
    '''
    ''' • Sentinel selected → ADD mode: clear txtRelationship and enable it for input.
    ''' • Existing item selected → EDIT mode: auto-populate txtRelationship instantly
    '''   with the selected text so the user can edit in-place.  No MsgBox.
    ''' </summary>
    Private Sub cbrelationship_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles cbrelationship.SelectedIndexChanged

        If _suppressComboEvent Then Return
        If cbrelationship.SelectedIndex < 0 Then Return

        Dim selected As String = cbrelationship.SelectedItem.ToString()

        If selected = SENTINEL Then
            ' ── ADD MODE ──────────────────────────────────────────────────────
            _editingId = -1

            txtRelationship.Clear()
            txtRelationship.Enabled = True
            txtRelationship.BackColor = Color.White
            txtRelationship.Focus()

            btnAddNewRelationshipType.Enabled = True
            btnEditRelationship.Enabled = False

            lblRelationship.Text = "New Relationship:"

        Else
            ' ── EDIT MODE ─────────────────────────────────────────────────────
            ' Resolve the selected display text to its database primary key
            ' via the Logic layer (no SQL in this file).
            Dim id As Integer = _logic.GetRelationshipIdByName(selected)

            If id <= 0 Then
                ' Defensive: lookup failed — return to idle state silently
                ResetEditorState()
                Return
            End If

            _editingId = id

            ' Auto-populate the editing side-TextBox instantly — no popup
            txtRelationship.Text = selected
            txtRelationship.Enabled = True
            txtRelationship.BackColor = Color.White
            txtRelationship.Focus()
            txtRelationship.SelectAll()   ' pre-select so user can overtype immediately

            btnAddNewRelationshipType.Enabled = False
            btnEditRelationship.Enabled = True

            lblRelationship.Text = "Edit Relationship:"
        End If
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Add button
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub btnAddNewRelationshipType_Click(sender As Object, e As EventArgs) _
            Handles btnAddNewRelationshipType.Click

        Dim newName As String = txtRelationship.Text.Trim()

        ' ── Client-side validation (no DB call wasted on empty input) ─────────
        If String.IsNullOrWhiteSpace(newName) Then
            FlashInvalid(txtRelationship, "Please type a relationship type name.")
            Return
        End If

        If newName.Length > 50 Then
            FlashInvalid(txtRelationship, "Name must be 50 characters or fewer.")
            Return
        End If

        ' ── Delegate insert to the Logic layer ────────────────────────────────
        Dim result As Integer = _logic.AddRelationshipType(newName)

        Select Case result
            Case Is > 0
                ' Inserted (or already existed — logic returns existing id)
                ShowSuccess("""" & newName & """ saved successfully.")
                ResetEditorState()
                LoadComboBox()

            Case Else   ' -1 = DB error
                MsgBox("Failed to save. Please check your database connection.",
                       MsgBoxStyle.Exclamation, "Save Error")
        End Select
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Edit button
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub btnEditRelationship_Click(sender As Object, e As EventArgs) _
            Handles btnEditRelationship.Click

        If _editingId <= 0 Then
            ResetEditorState()
            Return
        End If

        Dim updatedName As String = txtRelationship.Text.Trim()

        If String.IsNullOrWhiteSpace(updatedName) Then
            FlashInvalid(txtRelationship, "Relationship name cannot be empty.")
            Return
        End If

        If updatedName.Length > 50 Then
            FlashInvalid(txtRelationship, "Name must be 50 characters or fewer.")
            Return
        End If

        ' ── Delegate update to the Logic layer ────────────────────────────────
        Dim success As Boolean = _logic.UpdateRelationshipType(_editingId, updatedName)

        If success Then
            ShowSuccess("Updated to """ & updatedName & """ successfully.")
            ResetEditorState()
            LoadComboBox()
        Else
            MsgBox("Update failed. Please check your database connection.",
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
    ' State helpers
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Returns every UI element to its idle/neutral state.
    ''' Called after every successful save/update and on form load.
    ''' </summary>
    Private Sub ResetEditorState()
        _suppressComboEvent = True
        Try
            _editingId = -1
            cbrelationship.SelectedIndex = -1
        Finally
            _suppressComboEvent = False
        End Try

        txtRelationship.Text = ""
        txtRelationship.Enabled = False
        txtRelationship.BackColor = Color.FromArgb(237, 237, 237)

        btnAddNewRelationshipType.Enabled = False
        btnEditRelationship.Enabled = False

        lblRelationship.Text = "Relationship:"
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' UX feedback helpers — no blocking MsgBox for validation/success
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Briefly highlights the TextBox in red and shows a floating tooltip message.
    ''' Restores the white background automatically after 650 ms.
    ''' </summary>
    Private Sub FlashInvalid(tb As TextBox, message As String)
        tb.BackColor = Color.FromArgb(255, 200, 200)
        tb.Focus()

        Dim tip As New ToolTip()
        tip.Show(message, tb, 0, -24, 2500)

        Dim t As New System.Windows.Forms.Timer() With {.Interval = 650}
        AddHandler t.Tick,
            Sub(s, ev)
                tb.BackColor = Color.White
                t.Stop()
                t.Dispose()
            End Sub
        t.Start()
    End Sub

    ''' <summary>
    ''' Briefly highlights the TextBox in green and shows a floating success tooltip.
    ''' </summary>
    Private Sub ShowSuccess(message As String)
        txtRelationship.BackColor = Color.FromArgb(200, 255, 210)

        Dim tip As New ToolTip()
        tip.Show(message, txtRelationship, 0, -24, 2500)

        Dim t As New System.Windows.Forms.Timer() With {.Interval = 800}
        AddHandler t.Tick,
            Sub(s, ev)
                txtRelationship.BackColor = Color.FromArgb(237, 237, 237)
                t.Stop()
                t.Dispose()
            End Sub
        t.Start()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Visual styling (gradients, rounded corners, cursors)
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub ApplyVisualStyling()
        ' Diagonal gradient painted on FillPanel
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

        ' Rounded corners on action buttons
        ApplyRoundedButton(btnAddNewRelationshipType, 18)
        ApplyRoundedButton(btnEditRelationship, 18)

        ' Hand cursors on all interactive controls
        cbrelationship.Cursor = Cursors.Hand
        btnAddNewRelationshipType.Cursor = Cursors.Hand
        btnEditRelationship.Cursor = Cursors.Hand
        ExitBtn.Cursor = Cursors.Hand

        ' Clean border on the text input
        txtRelationship.BorderStyle = BorderStyle.FixedSingle
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
    ' Cleanup
    ' ─────────────────────────────────────────────────────────────────────────────

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        _responsiveManager?.Cleanup()
        MyBase.OnFormClosing(e)
    End Sub

End Class
