Imports MySql.Data.MySqlClient
Imports System.IO

Public Class BrgyInfoAddingLogic

    Public Class BrgyInfoResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public Property ErrorCode As Integer
        Public Property BrgyInfoId As Integer

        Public Sub New()
            IsSuccess = False
            Message = ""
            ErrorCode = 0
            BrgyInfoId = -1
        End Sub
    End Class

    ' ============================================================================
    ' READ — Fetch the single master barangayinformation row
    ' Returns all columns including the LONGBLOB Logo for the form to bind.
    ' ============================================================================
    ''' <summary>
    ''' Retrieves the single master row from the barangayinformation table.
    ''' Returns an empty DataTable if the table has no rows yet.
    ''' </summary>
    Public Function GetBarangayInfo() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing
        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable
            Dim query As String =
            "SELECT BarangayInfoID, BarangayName, Mission, Vision, " &
            "       Logo, LastUpdatedBy, LastUpdatedDate " &
            "FROM   barangayinformation " &
            "ORDER BY BarangayInfoID ASC " &
            "LIMIT  1"
            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetBarangayInfo Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
           connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
        Return dataTable
    End Function



    ' ============================================================================
    ' WRITE — Smart INSERT or UPDATE (determined at runtime)
    ' Logo is read from disk path, downscaled, and saved as raw PNG bytes.
    ' ============================================================================
    ''' <summary>
    ''' Saves barangay information.
    ''' If a row already exists → parameterized UPDATE.
    ''' If the table is empty   → parameterized INSERT.
    ''' The Logo blob is written only when a new file path is provided;
    ''' it is set to NULL only when <paramref name="removeLogo"/> is True.
    ''' </summary>
    Public Function SaveBarangayInfo(brgyName As String,
                                 mission As String,
                                 vision As String,
                                 logoPath As String,
                                 removeLogo As Boolean) As BrgyInfoResult
        Dim result As New BrgyInfoResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing
        Try
            ' ── Field validation ─────────────────────────────────────────────────
            If String.IsNullOrWhiteSpace(brgyName) Then
                result.Message = "Please enter barangay name."
                result.ErrorCode = 1 : Return result
            End If
            If String.IsNullOrWhiteSpace(mission) Then
                result.Message = "Please enter the mission statement."
                result.ErrorCode = 1 : Return result
            End If
            If String.IsNullOrWhiteSpace(vision) Then
                result.Message = "Please enter the vision statement."
                result.ErrorCode = 1 : Return result
            End If
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3 : Return result
            End If
            ' ── Prepare logo bytes (downscale + re-encode as PNG to reduce size) ─
            Dim logoBytes As Byte() = Nothing
            If Not String.IsNullOrWhiteSpace(logoPath) AndAlso
           System.IO.File.Exists(logoPath) Then
                ' Load the original, resize to max 512×512 maintaining aspect ratio,
                ' then encode as PNG bytes for the LONGBLOB column.
                Using original As Image = Image.FromFile(logoPath)
                    Dim maxSize As Integer = 512
                    Dim ratio As Double = Math.Min(
                    CDbl(maxSize) / original.Width,
                    CDbl(maxSize) / original.Height)
                    Dim newW As Integer = CInt(original.Width * ratio)
                    Dim newH As Integer = CInt(original.Height * ratio)
                    Using scaled As New Bitmap(newW, newH)
                        Using g As Graphics = Graphics.FromImage(scaled)
                            g.InterpolationMode =
                            System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                            g.DrawImage(original, 0, 0, newW, newH)
                        End Using
                        Using ms As New System.IO.MemoryStream()
                            scaled.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                            logoBytes = ms.ToArray()
                        End Using
                    End Using
                End Using
            End If
            ' ── Determine operation: UPDATE existing row or INSERT new row ────────
            Dim barangayInfoId As Integer = GetExistingBarangayInfoId(connection)
            If barangayInfoId > 0 Then
                ' ── UPDATE path ──────────────────────────────────────────────────
                Dim updateQuery As String =
                "UPDATE barangayinformation SET " &
                "    BarangayName    = @BarangayName, " &
                "    Mission         = @Mission, " &
                "    Vision          = @Vision, " &
                "    LastUpdatedBy   = @LastUpdatedBy, " &
                "    LastUpdatedDate = NOW()"
                ' Only touch the Logo column if a new file was provided or
                ' the user explicitly clicked Remove.
                If logoBytes IsNot Nothing Then
                    updateQuery &= ", Logo = @Logo"
                ElseIf removeLogo Then
                    updateQuery &= ", Logo = NULL"
                End If
                updateQuery &= " WHERE BarangayInfoID = @BarangayInfoID"
                Using cmd As New MySqlCommand(updateQuery, connection)
                    cmd.Parameters.AddWithValue("@BarangayName", brgyName.Trim())
                    cmd.Parameters.AddWithValue("@Mission", mission.Trim())
                    cmd.Parameters.AddWithValue("@Vision", vision.Trim())
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", LogInForm.CurrentUserID)
                    cmd.Parameters.AddWithValue("@BarangayInfoID", barangayInfoId)
                    If logoBytes IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@Logo", logoBytes)
                    End If
                    ' === LOG AUDIT TRAIL ===
                    GlobalAuditLogger.Log("BrgyInfoAdding_Form", "SAVE BARANGAY INFO",
                    LogInForm.CurrentUsername & " saved/updated barangay information record.",
                    connection, transaction)

                    cmd.ExecuteNonQuery()
                End Using
                result.IsSuccess = True
                result.Message = "Barangay information updated successfully."
                result.BrgyInfoId = barangayInfoId
                Return result
            End If
            ' ── INSERT path (table was empty) ────────────────────────────────────
            Dim insertQuery As String =
            "INSERT INTO barangayinformation " &
            "    (BarangayName, Mission, Vision, Logo, LastUpdatedBy, LastUpdatedDate) " &
            "VALUES " &
            "    (@BarangayName, @Mission, @Vision, @Logo, @LastUpdatedBy, NOW()); " &
            "SELECT LAST_INSERT_ID();"
            Using cmd As New MySqlCommand(insertQuery, connection)
                cmd.Parameters.AddWithValue("@BarangayName", brgyName.Trim())
                cmd.Parameters.AddWithValue("@Mission", mission.Trim())
                cmd.Parameters.AddWithValue("@Vision", vision.Trim())
                cmd.Parameters.AddWithValue("@Logo",
                If(logoBytes IsNot Nothing, CObj(logoBytes), DBNull.Value))
                cmd.Parameters.AddWithValue("@LastUpdatedBy", LogInForm.CurrentUserID)
                Dim newId As Object = cmd.ExecuteScalar()
                If newId IsNot Nothing Then
                    result.BrgyInfoId = CInt(newId)
                End If
            End Using
            result.IsSuccess = True
            result.Message = "Barangay information saved successfully."
        Catch ex As MySqlException
            result.Message = "Database error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("SaveBarangayInfo Database Error: " & ex.Message)
        Catch ex As Exception
            result.Message = "An error occurred: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("SaveBarangayInfo Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
           connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
        Return result
    End Function

    ' ============================================================================
    ' PRIVATE HELPER — check if a record already exists (reused by SaveBarangayInfo)
    ' ============================================================================
    ''' <summary>
    ''' Returns the BarangayInfoID of the first existing row, or -1 if the table is empty.
    ''' Accepts an open connection so it can be called without opening a second one.
    ''' </summary>
    Private Function GetExistingBarangayInfoId(connection As MySqlConnection) As Integer
        Using cmd As New MySqlCommand(
            "SELECT BarangayInfoID " &
            "FROM   barangayinformation " &
            "ORDER BY BarangayInfoID ASC LIMIT 1",
            connection)
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return CInt(result)
            End If
        End Using
        Return -1
    End Function

    Public Function GetAllBarangayOfficials() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT OfficialId, CONCAT(FirstName, ' ', LastName) AS FullName, Position, TermStart, TermEnd, PhotoPath " &
                "FROM barangayofficials " &
                "WHERE IsActive = 1 " &
                "ORDER BY Position, FirstName"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAllBarangayOfficials Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function SearchBarangayOfficials(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT OfficialId, CONCAT(FirstName, ' ', LastName) AS FullName, Position, TermStart, TermEnd, PhotoPath " &
                "FROM barangayofficials " &
                "WHERE IsActive = 1 " &
                "AND (FirstName LIKE @Search OR LastName LIKE @Search OR Position LIKE @Search) " &
                "ORDER BY Position, FirstName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Search", "%" & searchTerm.Trim() & "%")

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("SearchBarangayOfficials Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function ArchiveOfficial(officialId As Integer) As BrgyInfoResult
        Dim result As New BrgyInfoResult()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            Dim query As String =
                "UPDATE barangayofficials " &
                "SET IsActive = 0, UpdatedAt = NOW() " &
                "WHERE OfficialId = @OfficialId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@OfficialId", officialId)

                Dim rowsAffected = cmd.ExecuteNonQuery()

                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("BrgyInfoAdding_Form", "ARCHIVE OFFICIAL",
                LogInForm.CurrentUsername & " archived barangay official (ID: " & officialId & ")")


                If rowsAffected > 0 Then
                    result.IsSuccess = True
                    result.Message = "Official archived successfully."
                Else
                    result.Message = "Failed to archive official."
                    result.ErrorCode = 1
                End If
            End Using

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("ArchiveOfficial Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

End Class