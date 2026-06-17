Imports System.Drawing.Drawing2D

''' <summary>
''' UI Events Layer for Dashboard2_Form.
''' Responsibility: wire Form_Load, delegate data loading to Dashboard2Logic,
''' bind results to controls, and delegate all layout to Dashboard2ResponsiveManager.
''' This class contains ZERO SQL and ZERO pixel coordinates.
''' </summary>
Public Class Dashboard2_Form

    ' =========================================================================
    '  SERVICE AND MANAGER INSTANCES
    ' =========================================================================
    Private ReadOnly _logic As New Dashboard2Logic()
    Private _responsive As Dashboard2ResponsiveManager  ' set in Form_Load

    ' =========================================================================
    '  FORM LOAD
    ' =========================================================================
    Private Sub Dashboard2_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' --- Step 1: Initialize the Responsive Manager ---
        ' Must happen before data binding so the first layout pass sees the
        ' correctly sized FillPanel.
        _responsive = New Dashboard2ResponsiveManager(Me)
        _responsive.Initialize()

        ' --- Step 2: Fetch officials from the database ---
        Dim officials As Dashboard2Logic.DashboardOfficials = _logic.GetActiveDashboardOfficials()

        ' --- Step 3: Bind data to all card panels ---
        BindOfficialCards(officials)

        ' --- Step 4: Refresh layout so labels re-centre after text is set ---
        _responsive.RefreshLayout()
    End Sub

    ' =========================================================================
    '  CARD BINDING — delegates one record per slot to PopulateCard()
    '
    '  Explicit control references are used (not string lookups) so the
    '  compiler catches any Designer name mismatches at build time.
    '  The off-by-one PictureBox names in the Designer are handled here:
    '    SkKagawad4Pnl → SkKagawad5Pic
    '    SkKagawad5Pnl → SkKagawad6Pic
    '    SkKagawad6Pnl → SkKagawad7Pic
    '    SkKagawad7Pnl → SkKagawad8Pic
    '    Kagawad9Pnl   → PictureBox9BrgyKagawad9Pic  (not BrgyKagawad9Pic)
    ' =========================================================================
    Private Sub BindOfficialCards(officials As Dashboard2Logic.DashboardOfficials)

        ' --- Executive: Barangay Chairman ---
        PopulateCard(ChairmanPic,
                     ChairmanName,
                     ChairmanPosition,
                     officials.Chairman,
                     "Barangay Chairman")

        ' --- Legislative: Barangay Kagawad slots 1-8 ---
        PopulateCard(BrgyKagawad1Pic, BrgyKagawad1Name, Kagawad1Position,
                     officials.Kagawads(0), "Barangay Kagawad")
        PopulateCard(BrgyKagawad2Pic, BrgyKagawad2Name, Kagawad2Position,
                     officials.Kagawads(1), "Barangay Kagawad")
        PopulateCard(BrgyKagawad3Pic, BrgyKagawad3Name, Kagawad3Position,
                     officials.Kagawads(2), "Barangay Kagawad")
        PopulateCard(BrgyKagawad4Pic, BrgyKagawad4Name, Kagawad4Position,
                     officials.Kagawads(3), "Barangay Kagawad")
        PopulateCard(BrgyKagawad5Pic, BrgyKagawad5Name, Kagawad5Position,
                     officials.Kagawads(4), "Barangay Kagawad")
        PopulateCard(BrgyKagawad6Pic, BrgyKagawad6Name, Kagawad6Position,
                     officials.Kagawads(5), "Barangay Kagawad")
        PopulateCard(BrgyKagawad7Pic, BrgyKagawad7Name, Kagawad7Position,
                     officials.Kagawads(6), "Barangay Kagawad")
        PopulateCard(BrgyKagawad8Pic, BrgyKagawad8Name, Kagawad8Position,
                     officials.Kagawads(7), "Barangay Kagawad")

        ' --- Appointed Officials: re-routed slots 9-11 ---
        ' Kagawad9Pnl's PictureBox is named PictureBox9BrgyKagawad9Pic in the Designer
        PopulateCard(PictureBox9BrgyKagawad9Pic, BrgyKagawad9Name, Kagawad9Position,
                     officials.Secretary, "Barangay Secretary")
        PopulateCard(BrgyKagawad10Pic, BrgyKagawad10Name, Kagawad10Position,
                     officials.Treasurer, "Barangay Treasurer")
        PopulateCard(BrgyKagawad11Pic, BrgyKagawad11Name, Kagawad11Position,
                     officials.Administrator, "Barangay Administrator")

        ' --- SK Section: SK Chairman ---
        PopulateCard(SkChairmanPic,
                     SkChairmanName,
                     SkChairmanPosition,
                     officials.SkChairman,
                     "Barangay SK Chairman")

        ' --- SK Kagawad slots 1-7 ---
        ' Note: SkKagawad1-3Pnl have correctly numbered Pics.
        ' SkKagawad4-7Pnl have pics named one index higher (Designer quirk).
        PopulateCard(SkKagawad1Pic, SkKagawad1Name, BarangaySkKagawad1,
                     officials.SkKagawads(0), "Barangay SK Kagawad")
        PopulateCard(SkKagawad2Pic, SkKagawad2Name, BarangaySkKagawad2,
                     officials.SkKagawads(1), "Barangay SK Kagawad")
        PopulateCard(SkKagawad3Pic, SkKagawad3Name, BarangaySkKagawad3,
                     officials.SkKagawads(2), "Barangay SK Kagawad")
        PopulateCard(SkKagawad5Pic, SkKagawad4Name, BarangaySkKagawad4,  ' SkKagawad4Pnl → SkKagawad5Pic
                     officials.SkKagawads(3), "Barangay SK Kagawad")
        PopulateCard(SkKagawad6Pic, SkKagawad5Name, BarangaySkKagawad5,  ' SkKagawad5Pnl → SkKagawad6Pic
                     officials.SkKagawads(4), "Barangay SK Kagawad")
        PopulateCard(SkKagawad7Pic, SkKagawad6Name, BarangaySkKagawad6,  ' SkKagawad6Pnl → SkKagawad7Pic
                     officials.SkKagawads(5), "Barangay SK Kagawad")
        PopulateCard(SkKagawad8Pic, SkKagawad7Name, BarangaySkKagawad7,  ' SkKagawad7Pnl → SkKagawad8Pic
                     officials.SkKagawads(6), "Barangay SK Kagawad")
    End Sub

    ' =========================================================================
    '  POPULATE CARD HELPER
    '  Writes name/position text and loads the photo (or default avatar).
    '  All image conversion is delegated to Dashboard2Logic.BytesToImage().
    ' =========================================================================
    Private Sub PopulateCard(picBox As PictureBox,
                              nameLbl As Label,
                              posLbl As Label,
                              data As Dashboard2Logic.OfficialData,
                              displayTitle As String)
        ' Set label text
        nameLbl.Text = data.FullName       ' returns "Vacant" when IsVacant = True
        posLbl.Text = displayTitle

        ' Load photo
        Dim photo As Image = Nothing
        If Not data.IsVacant Then
            photo = _logic.BytesToImage(data.PhotoBytes)
        End If

        ' Fall back to embedded default avatar when no stored photo exists
        picBox.Image = If(photo IsNot Nothing, photo, My.Resources.picforofficials)
        picBox.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    ' =========================================================================
    '  NAVIGATION BUTTONS
    ' =========================================================================
    Private Sub LeftButtonPB_Click(sender As Object, e As EventArgs) Handles LeftButtonPB.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dashboard_Layout.CurrentInstance.LoadContentPanel(New Dashboard1_Form())
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LeftButtonPB_Click Error: " & ex.Message)
        End Try
    End Sub

    Private Sub NextBtn_Click(sender As Object, e As EventArgs) Handles NextBtn.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dashboard_Layout.CurrentInstance.LoadContentPanel(New Dashboard3_Form())
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("NextBtn_Click Error: " & ex.Message)
        End Try
    End Sub

    ' =========================================================================
    '  CLEANUP
    ' =========================================================================
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If _responsive IsNot Nothing Then
            _responsive.Cleanup()
        End If
        MyBase.OnFormClosing(e)
    End Sub

End Class
