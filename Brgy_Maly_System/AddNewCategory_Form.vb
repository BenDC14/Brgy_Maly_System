Imports System.Drawing.Drawing2D

Public Class AddNewCategory_form

    ' ── state ──────────────────────────────────────────────────────────────────
    Private _logic As New AddNewCategoryLogic()
    Private _responsiveManager As AddNewCategoryResponsiveManager
    Private _editingCategoryId As Integer = -1   ' -1 = adding new
    Private _isSuppressingComboEvent As Boolean = False

    ' ── constants ──────────────────────────────────────────────────────────────
    Private Const ADD_NEW_SENTINEL As String = "[Add New Category]"

    ' ── initialisation ─────────────────────────────────────────────────────────
    Public Sub New()
        InitializeComponent()
        Me.DoubleBuffered = True
    End Sub

    Private Sub AddNewCategory_form_Load(sender As Object, e As EventArgs) _
            Handles MyBase.Load

        _responsiveManager = New AddNewCategoryResponsiveManager(Me)
        _responsiveManager.Initialize()

        ApplyVisualStyling()
        ResetEditorState()
        LoadComboBox()
    End Sub

    ' ── combo loading ───────────────────────────────────────────────────────────
    ''' <summary>
    ''' Fills the combo with a hardcoded sentinel at the top, then all DB rows.
    ''' Uses Items directly (no DataSource binding) so the sentinel is always
    ''' first without touching the DataTable.
    ''' </summary>
    Private Sub LoadComboBox()
        _isSuppressingComboEvent = True

        Try
            cbcategory.Items.Clear()
            cbcategory.Items.Add(ADD_NEW_SENTINEL)   ' sentinel always first

            Dim dt As DataTable = _logic.GetCategories()
            For Each row As DataRow In dt.Rows
                cbcategory.Items.Add(row("Category").ToString())
            Next

            cbcategory.SelectedIndex = -1

        Finally
            _isSuppressingComboEvent = False
        End Try
    End Sub

    ' ── combo selection changed ─────────────────────────────────────────────────
    Private Sub cbcategory_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles cbcategory.SelectedIndexChanged

        If _isSuppressingComboEvent Then Return
        If cbcategory.SelectedIndex < 0 Then Return

        Dim selected As String = cbcategory.SelectedItem.ToString()

        If selected = ADD_NEW_SENTINEL Then
            ' ── NEW MODE ──────────────────────────────────────────────
            _editingCategoryId = -1
            txtCategory.Text = ""
            txtCategory.Enabled = True
            txtCategory.BackColor = Color.White
            txtCategory.Focus()

            btnAddNewCategory.Enabled = True
            btnEditCategory.Enabled = False

            lblCategory.Text = "New Category Name:"
        Else
            ' ── EDIT MODE: look up the matching ID from the DB ────────
            Dim id As Integer = _logic.GetCategoryIdByName(selected)

            If id = -1 Then
                ' something went wrong; fall back to reset
                ResetEditorState()
                Return
            End If

            _editingCategoryId = id
            txtCategory.Text = selected
            txtCategory.Enabled = True
            txtCategory.BackColor = Color.White
            txtCategory.Focus()
            txtCategory.SelectAll()   ' highlight text so user can type straight away

            btnAddNewCategory.Enabled = False
            btnEditCategory.Enabled = True

            lblCategory.Text = "Edit Category Name:"
        End If
    End Sub

    ' ── ADD button ──────────────────────────────────────────────────────────────
    Private Sub btnAddNewCategory_Click(sender As Object, e As EventArgs) _
            Handles btnAddNewCategory.Click

        Dim newName As String = txtCategory.Text.Trim()

        If String.IsNullOrWhiteSpace(newName) Then
            FlashInvalid(txtCategory, "Please type a category name.")
            Return
        End If

        Dim result As Integer = _logic.AddCategory(newName)

        If result > 0 Then
            ShowSuccess("Category """ & newName & """ added successfully.")
            ResetEditorState()
            LoadComboBox()
        ElseIf result = 0 Then
            ' 0 means duplicate detected inside logic
            FlashInvalid(txtCategory, """" & newName & """ already exists.")
        Else
            MsgBox("Failed to save. Check your database connection.", MsgBoxStyle.Exclamation, "Save Error")
        End If
    End Sub

    ' ── EDIT button ─────────────────────────────────────────────────────────────
    Private Sub btnEditCategory_Click(sender As Object, e As EventArgs) _
            Handles btnEditCategory.Click

        If _editingCategoryId <= 0 Then
            ResetEditorState()
            Return
        End If

        Dim newName As String = txtCategory.Text.Trim()

        If String.IsNullOrWhiteSpace(newName) Then
            FlashInvalid(txtCategory, "Category name cannot be empty.")
            Return
        End If

        Dim success As Boolean = _logic.UpdateCategory(_editingCategoryId, newName)

        If success Then
            ShowSuccess("Category updated to """ & newName & """.")
            ResetEditorState()
            LoadComboBox()
        Else
            MsgBox("Update failed. Check your database connection.", MsgBoxStyle.Exclamation, "Update Error")
        End If
    End Sub

    ' ── EXIT ────────────────────────────────────────────────────────────────────
    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) _
            Handles ExitBtn.Click
        Me.Close()
    End Sub

    ' ── state helpers ───────────────────────────────────────────────────────────
    ''' <summary>
    ''' Puts the form back to its default idle state after every successful
    ''' operation or when nothing is selected.
    ''' </summary>
    Private Sub ResetEditorState()
        _isSuppressingComboEvent = True
        Try
            _editingCategoryId = -1
            cbcategory.SelectedIndex = -1
        Finally
            _isSuppressingComboEvent = False
        End Try

        txtCategory.Text = ""
        txtCategory.Enabled = False
        txtCategory.BackColor = Color.FromArgb(237, 237, 237)

        btnAddNewCategory.Enabled = False
        btnEditCategory.Enabled = False

        lblCategory.Text = "Category:"
    End Sub

    ' ── UX feedback helpers ─────────────────────────────────────────────────────
    ''' <summary>
    ''' Briefly flashes the textbox red and shows a tooltip-style label instead
    ''' of a blocking MsgBox, keeping user flow smooth.
    ''' </summary>
    Private Sub FlashInvalid(tb As TextBox, message As String)
        Dim original As Color = tb.BackColor
        tb.BackColor = Color.FromArgb(255, 200, 200)
        tb.Focus()

        Dim tip As New ToolTip()
        tip.Show(message, tb, 0, -20, 2500)

        ' Restore colour after 600 ms without blocking the UI thread
        Dim restoreTimer As New System.Windows.Forms.Timer()
        restoreTimer.Interval = 600
        AddHandler restoreTimer.Tick,
            Sub(s, ev)
                tb.BackColor = Color.White
                restoreTimer.Stop()
                restoreTimer.Dispose()
            End Sub
        restoreTimer.Start()
    End Sub

    ''' <summary>
    ''' Briefly turns the textbox green to confirm success — no popup needed.
    ''' </summary>
    Private Sub ShowSuccess(message As String)
        txtCategory.BackColor = Color.FromArgb(200, 255, 200)

        Dim tip As New ToolTip()
        tip.Show(message, txtCategory, 0, -20, 2500)

        Dim restoreTimer As New System.Windows.Forms.Timer()
        restoreTimer.Interval = 800
        AddHandler restoreTimer.Tick,
            Sub(s, ev)
                txtCategory.BackColor = Color.FromArgb(237, 237, 237)
                restoreTimer.Stop()
                restoreTimer.Dispose()
            End Sub
        restoreTimer.Start()
    End Sub

    ' ── visual styling ──────────────────────────────────────────────────────────
    Private Sub ApplyVisualStyling()
        ' Gradient background on FillPanel
        AddHandler FillPanel.Paint,
            Sub(s, pe)
                Dim c1 As Color = Color.FromArgb(220, 255, 225)
                Dim c2 As Color = Color.White
                Using br As New LinearGradientBrush(
                        FillPanel.ClientRectangle, c1, c2,
                        LinearGradientMode.ForwardDiagonal)
                    pe.Graphics.FillRectangle(br, FillPanel.ClientRectangle)
                End Using
            End Sub

        ' Rounded corners on both action buttons
        ApplyRoundedButton(btnAddNewCategory, 18)
        ApplyRoundedButton(btnEditCategory, 18)

        ' Hand cursor everywhere interactive
        cbcategory.Cursor = Cursors.Hand
        btnAddNewCategory.Cursor = Cursors.Hand
        btnEditCategory.Cursor = Cursors.Hand
        ExitBtn.Cursor = Cursors.Hand

        ' Subtle border on the textbox
        txtCategory.BorderStyle = BorderStyle.FixedSingle
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

    ' ── cleanup ─────────────────────────────────────────────────────────────────
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        _responsiveManager?.Cleanup()
        MyBase.OnFormClosing(e)
    End Sub

End Class
