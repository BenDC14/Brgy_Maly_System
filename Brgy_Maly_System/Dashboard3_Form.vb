Imports System.Drawing.Drawing2D

''' <summary>
''' UI Events Layer for Dashboard3_Form.
''' Responsibilities:
'''   • Orchestrate Form_Load: init responsive manager → fetch data → bind controls.
'''   • Map BarangayInfo fields to the exact Designer-declared control names.
'''   • Provide safe fallbacks for all nullable fields so no null-pointer crash
'''     can reach the UI.
''' This class contains ZERO SQL, ZERO pixel values, and ZERO layout math.
''' </summary>
Public Class Dashboard3_Form

    ' =========================================================================
    '  SERVICE AND MANAGER INSTANCES
    ' =========================================================================
    Private ReadOnly _logic As New Dashboard3Logic()
    Private _responsive As Dashboard3ResponsiveManager

    ' =========================================================================
    '  FORM LOAD
    ' =========================================================================
    Private Sub Dashboard3_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' --- Step 1: Boot the Responsive Manager first ---
        ' The manager sizes FillPanel and wires resize handlers before any
        ' data is bound, so the first layout pass fires against a correctly
        ' sized panel rather than the Designer's 1700×1004 fixed size.
        _responsive = New Dashboard3ResponsiveManager(Me)
        _responsive.Initialize()

        ' --- Step 2: Fetch barangay information from the database ---
        Dim info As Dashboard3Logic.BarangayInfo = _logic.GetBarangayInformation()

        ' --- Step 3: Bind data to controls ---
        BindBarangayProfile(info)

        ' --- Step 4: Refresh layout so centred labels recalculate
        '             their x-position after AutoSize reflows new text ---
        _responsive.RefreshLayout()
    End Sub

    ' =========================================================================
    '  DATA BINDING
    '  Maps each field of BarangayInfo to its target control.
    '  Every assignment is guarded against an empty/whitespace value so a
    '  partially populated database row never leaves a label blank.
    ' =========================================================================
    Private Sub BindBarangayProfile(info As Dashboard3Logic.BarangayInfo)

        ' --- Barangay Name Header ---
        BrgyMalyNameLbl.Text =
            If(Not String.IsNullOrWhiteSpace(info.BarangayName),
               info.BarangayName,
               "Barangay Maly")          ' hard-coded fallback matches Designer default

        ' --- Mission Text ---
        MissionInfoLbl.Text =
            If(Not String.IsNullOrWhiteSpace(info.Mission),
               info.Mission,
               "Mission statement not yet configured.")

        ' --- Vision Text ---
        VisionInfoLbl.Text =
            If(Not String.IsNullOrWhiteSpace(info.Vision),
               info.Vision,
               "Vision statement not yet configured.")

        ' --- Logo Image ---
        ' Attempt byte-array → Image conversion via the Logic layer.
        ' Falls back to the embedded LogoForMaly resource when:
        '   • No logo is stored in the database, OR
        '   • The byte array is not a valid image format.
        Dim logoImage As Image = _logic.BytesToImage(info.LogoBytes)

        If logoImage IsNot Nothing Then
            BrgyLogoPic.Image = logoImage
            BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage
        Else
            ' Restore the Designer's default resource image as the safe fallback
            BrgyLogoPic.Image = My.Resources.LogoForMaly
            BrgyLogoPic.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub

    ' =========================================================================
    '  NAVIGATION
    ' =========================================================================
    Private Sub LeftButtonPB_Click(sender As Object, e As EventArgs) Handles LeftButtonPB.Click
        Try
            If Dashboard_Layout.CurrentInstance IsNot Nothing Then
                Dashboard_Layout.CurrentInstance.LoadContentPanel(New Dashboard2_Form())
            Else
                MsgBox("Error: Dashboard not initialized.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error loading form: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Debug.WriteLine("LeftButtonPB_Click Error: " & ex.Message)
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
