Public Class AddNewCategoryResponsiveManager

    ' ── reference dimensions (match designer ClientSize exactly) ───────────────
    Private Const REF_W As Integer = 470
    Private Const REF_H As Integer = 245
    Private Const REF_SCREEN_W As Integer = 1920
    Private Const REF_SCREEN_H As Integer = 1080

    Private ReadOnly _form As AddNewCategory_form
    Private _scale As Single = 1.0F

    Public Sub New(form As AddNewCategory_form)
        _form = form
    End Sub

    Public Sub Initialize()
        _form.AutoScaleMode = AutoScaleMode.None

        ' ── compute scale factor ────────────────────────────────────────────────
        Dim screen As Screen = Screen.FromControl(_form)
        Dim sw As Integer = screen.WorkingArea.Width
        Dim sh As Integer = screen.WorkingArea.Height

        If sw < REF_SCREEN_W OrElse sh < REF_SCREEN_H Then
            _scale = Math.Min(CSng(sw) / REF_SCREEN_W,
                              CSng(sh) / REF_SCREEN_H)
        Else
            _scale = 1.0F
        End If

        ' ── resize the form ─────────────────────────────────────────────────────
        Dim fw As Integer = CInt(REF_W * _scale)
        Dim fh As Integer = CInt(REF_H * _scale)
        _form.ClientSize = New Size(fw, fh)
        _form.FillPanel.Size = New Size(fw, fh)
        _form.FillPanel.Location = New Point(0, 0)

        ' ── centre on screen ────────────────────────────────────────────────────
        _form.StartPosition = FormStartPosition.Manual
        _form.Location = New Point((sw - fw) \ 2, (sh - fh) \ 2)

        ScaleControls()
    End Sub

    Private Sub ScaleControls()
        Dim s As Single = _scale

        ' ExitBtn — designer: (439, 0) / (31, 30)
        _form.ExitBtn.Location = New Point(CInt(439 * s), CInt(0 * s))
        _form.ExitBtn.Size = New Size(CInt(31 * s), CInt(30 * s))
        _form.ExitBtn.Cursor = Cursors.Hand

        ' lblAddCategory — designer: (92, 20)
        _form.lblAddCategory.Location = New Point(CInt(92 * s), CInt(20 * s))
        _form.lblAddCategory.Font = New Font("Arial", 20.25F * s, FontStyle.Bold)
        _form.lblAddCategory.AutoSize = True

        ' lblCategory — designer: (20, 79)
        _form.lblCategory.Location = New Point(CInt(20 * s), CInt(79 * s))
        _form.lblCategory.Font = New Font("Arial", 14.25F * s, FontStyle.Bold)
        _form.lblCategory.AutoSize = True

        ' cbcategory — designer: (24, 104) / (248, 30)
        _form.cbcategory.Location = New Point(CInt(24 * s), CInt(104 * s))
        _form.cbcategory.Size = New Size(CInt(248 * s), CInt(30 * s))
        _form.cbcategory.Font = New Font("Arial", 14.25F * s, FontStyle.Regular)

        ' txtCategory — designer: (278, 104) / (150, 29)
        _form.txtCategory.Location = New Point(CInt(278 * s), CInt(104 * s))
        _form.txtCategory.Size = New Size(CInt(150 * s), CInt(29 * s))
        _form.txtCategory.Font = New Font("Arial", 14.25F * s, FontStyle.Regular)

        ' btnAddNewCategory — designer: (24, 167) / (199, 47)
        _form.btnAddNewCategory.Location = New Point(CInt(24 * s), CInt(167 * s))
        _form.btnAddNewCategory.Size = New Size(CInt(199 * s), CInt(47 * s))
        _form.btnAddNewCategory.Font = New Font("Arial Narrow", 14.25F * s,
                                                 FontStyle.Bold Or FontStyle.Italic)

        ' btnEditCategory — designer: (245, 167) / (199, 47)
        _form.btnEditCategory.Location = New Point(CInt(245 * s), CInt(167 * s))
        _form.btnEditCategory.Size = New Size(CInt(199 * s), CInt(47 * s))
        _form.btnEditCategory.Font = New Font("Arial Narrow", 14.25F * s,
                                               FontStyle.Bold Or FontStyle.Italic)
    End Sub

    Public Sub Cleanup()
        ' reserved for future event handler removal if needed
    End Sub

End Class
