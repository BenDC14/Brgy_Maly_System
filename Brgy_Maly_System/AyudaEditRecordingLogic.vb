Imports MySql.Data.MySqlClient
Imports System.Data

''' <summary>
''' Business Logic Layer for AyudaEditRecording_Form.
''' Handles all parameterized ADO.NET queries against the residentaid,
''' residents, barangayaid, categories, and familyhead tables.
''' No UI references — pure data operations only.
''' DATABASE: barangay_maly
'''
''' FIXED:
'''   - r.Birthdate → r.DateOfBirth  (matches ResidentAddingLogic.vb schema)
'''   - TIMESTAMPDIFF now uses correct column name DateOfBirth
'''   - Catch block now surfaces the real SQL error message to the caller
''' </summary>
Public Class AyudaEditRecordingLogic

    ' ════════════════════════════════════════════════════════════════
    '  DATA TRANSFER OBJECTS
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Full snapshot of a residentaid record including joined resident
    ''' and program details — used to populate the edit form on load.
    ''' </summary>
    Public Class AyudaRecordData
        ' ── Resident identity (read-only display fields) ──────────────
        Public Property ResidentId As Integer
        Public Property ResidentFullName As String = ""
        Public Property Age As Integer
        Public Property Sex As String = ""
        Public Property Category As String = ""
        Public Property HouseholdNumber As String = ""

        ' ── Editable Ayuda parameters ─────────────────────────────────
        Public Property ResidentAidId As Integer
        Public Property AidId As Integer
        Public Property ProgramTitle As String = ""
        Public Property Quantity As Integer
        Public Property AidDate As Date
        Public Property Description As String = ""
    End Class

    ''' <summary>
    ''' Carries only the fields the user is allowed to edit.
    ''' Passed from the form to UpdateAyudaRecord().
    ''' </summary>
    Public Class AyudaUpdateData
        Public Property ResidentAidId As Integer
        Public Property AidId As Integer
        Public Property Quantity As Integer
        Public Property AidDate As Date
        Public Property Description As String = ""
    End Class

    ''' <summary>
    ''' Standard operation result returned to the UI layer.
    ''' </summary>
    Public Class EditResult
        Public Property IsSuccess As Boolean = False
        Public Property Message As String = ""
        Public Property ErrorCode As Integer = 0
    End Class

    ' ════════════════════════════════════════════════════════════════
    '  SELECT — Load a single residentaid record by primary key
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Fetches all display and editable values for the given ResidentAidId.
    ''' BUG FIX: Column is DateOfBirth (not Birthdate) — confirmed from ResidentAddingLogic.vb.
    ''' Returns Nothing if the record does not exist or a DB error occurs.
    ''' The ErrorMessage property on the returned Nothing is surfaced to the UI.
    ''' </summary>
    Public Function GetRecordById(residentAidId As Integer,
                                  Optional ByRef errorMessage As String = "") As AyudaRecordData
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                errorMessage = "Unable to connect to the database."
                Return Nothing
            End If

            ' ── FIXED: DateOfBirth is the correct column name ─────────
            ' ── FIXED: TIMESTAMPDIFF now references DateOfBirth ───────
            ' ── LEFT JOINs ensure a row is always returned even if
            '    the resident has no category or no family head entry ───
            Dim query As String =
                "SELECT " &
                "  ra.ResidentAidId, " &
                "  ra.ResidentId, " &
                "  ra.AidId, " &
                "  CONCAT(r.FirstName, ' ', r.LastName) AS ResidentFullName, " &
                "  TIMESTAMPDIFF(YEAR, r.DateOfBirth, CURDATE()) AS Age, " &
                "  COALESCE(r.Sex, '') AS Sex, " &
                "  COALESCE(c.Category, '') AS Category, " &
                "  COALESCE(fh.FamilyName, 'N/A') AS HouseholdNumber, " &
                "  ba.program_title AS ProgramTitle, " &
                "  ra.Quantity, " &
                "  ra.AidDate, " &
                "  COALESCE(ra.Description, '') AS Description " &
                "FROM residentaid ra " &
                "INNER JOIN residents r  ON ra.ResidentId = r.ResidentId " &
                "INNER JOIN barangayaid ba ON ra.AidId = ba.AidId " &
                "LEFT  JOIN residentcategory rc ON r.ResidentId = rc.ResidentId " &
                "LEFT  JOIN categories c ON rc.CategoryId = c.CategoryId " &
                "LEFT  JOIN familyhead fh ON r.ResidentId = fh.ResidentId " &
                "WHERE ra.ResidentAidId = @ResidentAidId " &
                "LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ResidentAidId", residentAidId)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return New AyudaRecordData With {
                            .ResidentAidId = SafeInt(reader, "ResidentAidId"),
                            .ResidentId = SafeInt(reader, "ResidentId"),
                            .AidId = SafeInt(reader, "AidId"),
                            .ResidentFullName = SafeStr(reader, "ResidentFullName"),
                            .Age = SafeInt(reader, "Age"),
                            .Sex = SafeStr(reader, "Sex"),
                            .Category = SafeStr(reader, "Category"),
                            .HouseholdNumber = SafeStr(reader, "HouseholdNumber"),
                            .ProgramTitle = SafeStr(reader, "ProgramTitle"),
                            .Quantity = SafeInt(reader, "Quantity"),
                            .AidDate = If(IsDBNull(reader("AidDate")),
                                                   Now, CDate(reader("AidDate"))),
                            .Description = SafeStr(reader, "Description")
                        }
                    Else
                        ' Reader returned no rows — record does not exist
                        errorMessage = "No record found with ID: " & residentAidId &
                                       ". It may have already been deleted."
                        Return Nothing
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            ' Surface the real MySQL error so the user and developer can see it
            errorMessage = "Database error loading record: " & ex.Message
            Debug.WriteLine("AyudaEditRecordingLogic.GetRecordById MySQL Error: " & ex.Message)
            Return Nothing

        Catch ex As Exception
            errorMessage = "Unexpected error loading record: " & ex.Message
            Debug.WriteLine("AyudaEditRecordingLogic.GetRecordById Error: " & ex.Message)
            Return Nothing

        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return Nothing
    End Function

    ' ════════════════════════════════════════════════════════════════
    '  UPDATE — Save edited Ayuda parameters
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Updates only the editable Ayuda distribution columns.
    ''' Resident identity columns are NOT in this UPDATE statement by design.
    ''' Wrapped in a transaction for atomicity. Logs to GlobalAuditLogger on success.
    ''' </summary>
    Public Function UpdateAyudaRecord(data As AyudaUpdateData) As EditResult
        Dim result As New EditResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            ' ── Input guard ───────────────────────────────────────────
            If data Is Nothing OrElse data.ResidentAidId <= 0 Then
                result.Message = "Invalid record identifier."
                result.ErrorCode = 1
                Return result
            End If

            If data.Quantity <= 0 Then
                result.Message = "Quantity must be greater than zero."
                result.ErrorCode = 1
                Return result
            End If

            ' ── Open connection and begin transaction ─────────────────
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to the database."
                result.ErrorCode = 3
                Return result
            End If

            transaction = connection.BeginTransaction()

            Try
                ' ── Resolve ResidentId for the audit log ──────────────
                Dim targetResidentId As Integer = GetResidentIdForAudit(
                    data.ResidentAidId, connection, transaction)

                ' ── Parameterized UPDATE ──────────────────────────────
                Dim updateQuery As String =
                    "UPDATE residentaid SET " &
                    "  AidId       = @AidId,  " &
                    "  Quantity    = @Quantity, " &
                    "  AidDate     = @AidDate, " &
                    "  Description = @Description " &
                    "WHERE ResidentAidId = @ResidentAidId"

                Using cmd As New MySqlCommand(updateQuery, connection, transaction)
                    cmd.Parameters.AddWithValue("@AidId", data.AidId)
                    cmd.Parameters.AddWithValue("@Quantity", data.Quantity)
                    cmd.Parameters.AddWithValue("@AidDate", data.AidDate)
                    cmd.Parameters.AddWithValue("@Description",
                        If(String.IsNullOrWhiteSpace(data.Description),
                           CObj(DBNull.Value), CObj(data.Description)))
                    cmd.Parameters.AddWithValue("@ResidentAidId", data.ResidentAidId)

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected = 0 Then
                        transaction.Rollback()
                        result.Message = "No record was updated. It may have been deleted."
                        result.ErrorCode = 2
                        Return result
                    End If
                End Using

                ' ── Activity Tracking ─────────────────────────────────
                GlobalAuditLogger.Log(
                    "ayudaeditrecording_form",
                    "UPDATE AYUDA RECORD",
                    LogInForm.CurrentUsername &
                    " modified the Ayuda record details for Resident ID: " &
                    targetResidentId,
                    connection, transaction)

                transaction.Commit()

                result.IsSuccess = True
                result.Message = "Ayuda record updated successfully."
                result.ErrorCode = 0

            Catch ex As Exception
                If transaction IsNot Nothing Then transaction.Rollback()
                Throw   ' Re-throw to outer Catch for full logging
            End Try

        Catch ex As MySqlException
            result.Message = "Database error while saving: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("AyudaEditRecordingLogic.UpdateAyudaRecord MySQL Error: " & ex.Message)

        Catch ex As Exception
            result.Message = "Unexpected error while saving: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("AyudaEditRecordingLogic.UpdateAyudaRecord Error: " & ex.Message)

        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ' ════════════════════════════════════════════════════════════════
    '  SUPPORTING — Get all active Ayuda programs for the ComboBox
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Returns all active ayuda programs for binding to cbAyuda.
    ''' DisplayMember = "DisplayText", ValueMember = "AidId"
    ''' </summary>
    Public Function GetAllAyudaPrograms() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT AidId, program_title, " &
                "CONCAT(program_title, ' (', DATE_FORMAT(start_date, '%b %d %Y'), " &
                "       ' - ', DATE_FORMAT(end_date, '%b %d %Y'), ')') AS DisplayText " &
                "FROM barangayaid " &
                "WHERE is_active = 1 " &
                "ORDER BY start_date DESC"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("AyudaEditRecordingLogic.GetAllAyudaPrograms Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ' ════════════════════════════════════════════════════════════════
    '  PRIVATE HELPERS
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Fetches the ResidentId from within an open transaction for audit logging.
    ''' Returns -1 if not found.
    ''' </summary>
    Private Function GetResidentIdForAudit(residentAidId As Integer,
                                            connection As MySqlConnection,
                                            transaction As MySqlTransaction) As Integer
        Try
            Dim q As String =
                "SELECT ResidentId FROM residentaid " &
                "WHERE ResidentAidId = @ResidentAidId LIMIT 1"

            Using cmd As New MySqlCommand(q, connection, transaction)
                cmd.Parameters.AddWithValue("@ResidentAidId", residentAidId)
                Dim res = cmd.ExecuteScalar()
                If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                    Return CInt(res)
                End If
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetResidentIdForAudit Error: " & ex.Message)
        End Try
        Return -1
    End Function

    ''' <summary>Safe string reader — returns "" for DBNull.</summary>
    Private Function SafeStr(reader As MySqlDataReader, column As String) As String
        Return If(IsDBNull(reader(column)), "", reader(column).ToString().Trim())
    End Function

    ''' <summary>Safe int reader — returns 0 for DBNull.</summary>
    Private Function SafeInt(reader As MySqlDataReader, column As String) As Integer
        Return If(IsDBNull(reader(column)), 0, CInt(reader(column)))
    End Function

End Class
