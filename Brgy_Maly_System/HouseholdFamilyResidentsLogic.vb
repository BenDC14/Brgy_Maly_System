' ================================================================================
' FILE: HouseholdFamilyResidentsLogic.vb
' LAYER: Business Logic & Parameterized ADO.NET Data Layer
' PURPOSE: Handles all database operations for HouseholdFamilyResidents_Form.
'          Uses only the minimum resident columns needed for a family-context
'          add/edit (no full ResidentAdding audit trail or education table needed).
'
' Minimal columns written:
'   residents : LastName, FirstName, MiddleName, Suffix, DateOfBirth,
'               Sex, CivilStatus, ContactNumber, EmailAddress,
'               HouseholdId, Is_Archived
'   residentcategory : ResidentId, CategoryId, AdditionalInfo
'   familymembers    : FamilyId, ResidentId, RelationshipType  (ADD_MODE only)
' ================================================================================
Imports MySql.Data.MySqlClient
Imports System.Data

Public Class HouseholdFamilyResidentsLogic

    ' ============================================================================
    ' DTOs
    ' ============================================================================

    ''' <summary>
    ''' The absolute minimum resident data set required by the residents table.
    ''' Matches the form's fields exactly — nothing more, nothing less.
    ''' </summary>
    Public Class MinimalResidentData
        Public Property ResidentId As Integer = -1
        Public Property LastName As String = ""
        Public Property FirstName As String = ""
        Public Property MiddleName As String = ""
        Public Property Suffix As String = ""
        Public Property DateOfBirth As Date = Date.Today
        Public Property Sex As String = ""
        Public Property CivilStatus As String = ""
        Public Property ContactNumber As String = ""
        Public Property EmailAddress As String = ""
        Public Property AdditionalInfo As String = ""
        Public Property HouseholdId As Integer = -1
        Public Property HouseholdNumber As String = ""
        Public Property SelectedCategoryIds As New List(Of Integer)()
    End Class

    ''' <summary>Operation outcome returned to the UI layer.</summary>
    Public Class SaveResult
        Public Property IsSuccess As Boolean = False
        Public Property Message As String = ""
        Public Property ResidentId As Integer = -1
    End Class

    ' ============================================================================
    ' READ — Load minimal profile for EDIT_MODE
    ' ============================================================================

    ''' <summary>
    ''' Returns the minimal resident data required to pre-populate the form
    ''' fields in EDIT_MODE.  Also fetches the HouseholdNumber for display
    ''' and the list of checked CategoryIds.
    ''' </summary>
    Public Function GetMinimalResidentById(residentId As Integer) As MinimalResidentData
        Dim data As New MinimalResidentData()
        Dim connection As MySqlConnection = Nothing

        Try
            If residentId <= 0 Then Return data

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return data

            ' ── Core resident row ─────────────────────────────────────────────────
            Dim resQuery As String =
                "SELECT r.ResidentId, r.LastName, r.FirstName, r.MiddleName, r.Suffix, " &
                "r.DateOfBirth, r.Sex, r.CivilStatus, r.ContactNumber, r.EmailAddress, " &
                "r.HouseholdId, h.HouseholdNumber " &
                "FROM residents r " &
                "LEFT JOIN household h ON r.HouseholdId = h.HouseholdID " &
                "WHERE r.ResidentId = @ResidentId " &
                "LIMIT 1"

            Using cmd As New MySqlCommand(resQuery, connection)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        data.ResidentId = CInt(reader("ResidentId"))
                        data.LastName = DbStr(reader("LastName"))
                        data.FirstName = DbStr(reader("FirstName"))
                        data.MiddleName = DbStr(reader("MiddleName"))
                        data.Suffix = DbStr(reader("Suffix"))
                        data.DateOfBirth = If(IsDBNull(reader("DateOfBirth")),
                                                  Date.Today, CDate(reader("DateOfBirth")))
                        data.Sex = DbStr(reader("Sex"))
                        data.CivilStatus = DbStr(reader("CivilStatus"))
                        data.ContactNumber = DbStr(reader("ContactNumber"))
                        data.EmailAddress = DbStr(reader("EmailAddress"))
                        data.HouseholdId = If(IsDBNull(reader("HouseholdId")),
                                                  -1, CInt(reader("HouseholdId")))
                        data.HouseholdNumber = DbStr(reader("HouseholdNumber"))
                    End If
                End Using
            End Using

            If data.ResidentId <= 0 Then Return data  ' not found

            ' ── Category IDs ──────────────────────────────────────────────────────
            Using cmd As New MySqlCommand(
                    "SELECT CategoryId, AdditionalInfo " &
                    "FROM residentcategory WHERE ResidentId = @ResidentId",
                    connection)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    data.SelectedCategoryIds = New List(Of Integer)()
                    While reader.Read()
                        data.SelectedCategoryIds.Add(CInt(reader("CategoryId")))
                        ' Take the first non-blank AdditionalInfo found
                        If String.IsNullOrWhiteSpace(data.AdditionalInfo) AndAlso
                           Not IsDBNull(reader("AdditionalInfo")) Then
                            data.AdditionalInfo = reader("AdditionalInfo").ToString()
                        End If
                    End While
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetMinimalResidentById Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return data
    End Function

    ' ============================================================================
    ' READ — HouseholdNumber by HouseholdId  (for ADD_MODE pre-fill)
    ' ============================================================================

    ''' <summary>
    ''' Returns the HouseholdNumber string for a given HouseholdId.
    ''' Used in ADD_MODE to pre-fill the read-only household display field.
    ''' </summary>
    Public Function GetHouseholdNumberById(householdId As Integer) As String
        Dim connection As MySqlConnection = Nothing
        Try
            If householdId <= 0 Then Return ""
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return ""
            Using cmd As New MySqlCommand(
                    "SELECT HouseholdNumber FROM household WHERE HouseholdID = @Id LIMIT 1",
                    connection)
                cmd.Parameters.AddWithValue("@Id", householdId)
                Dim result As Object = cmd.ExecuteScalar()
                Return If(result IsNot Nothing, result.ToString(), "")
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetHouseholdNumberById Error: " & ex.Message)
            Return ""
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ' ============================================================================
    ' READ — Categories CheckedListBox population
    ' ============================================================================

    ''' <summary>Returns all rows from the categories table ordered alphabetically.</summary>
    Public Function GetCategories() As DataTable
        Dim dt As New DataTable()
        Dim connection As MySqlConnection = Nothing
        dt.Columns.Add("CategoryId", GetType(Integer))
        dt.Columns.Add("Category", GetType(String))
        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dt
            Using cmd As New MySqlCommand(
                    "SELECT CategoryId, Category FROM categories ORDER BY Category ASC",
                    connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    dt.Clear() : adapter.Fill(dt)
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetCategories Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
        Return dt
    End Function

    ' ============================================================================
    ' INSERT — ADD_MODE
    ' Inserts the resident, saves categories, then links them to the family.
    ' ============================================================================

    ''' <summary>
    ''' Inserts a new row into <c>residents</c>, saves <c>residentcategory</c>
    ''' rows, and inserts a row into <c>familymembers</c> with RelationshipType
    ''' defaulting to "Member" (the caller can later use Edit-Position to refine).
    ''' All three writes share a single transaction.
    ''' </summary>
    Public Function InsertResident(data As MinimalResidentData,
                                   familyId As Integer) As SaveResult
        Dim result As New SaveResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            ' Validate minimum required fields
            Dim valMsg As String = ValidateMinimal(data)
            If valMsg <> "" Then
                result.Message = valMsg
                Return result
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                Return result
            End If

            transaction = connection.BeginTransaction()

            Try
                ' ── INSERT residents ──────────────────────────────────────────────
                Dim insertSql As String =
                    "INSERT INTO residents " &
                    "(LastName, FirstName, MiddleName, Suffix, DateOfBirth, " &
                    " Sex, CivilStatus, ContactNumber, EmailAddress, HouseholdId, Is_Archived) " &
                    "VALUES " &
                    "(@LastName, @FirstName, @MiddleName, @Suffix, @DateOfBirth, " &
                    " @Sex, @CivilStatus, @ContactNumber, @EmailAddress, @HouseholdId, 0)"

                Dim newResidentId As Long

                Using cmd As New MySqlCommand(insertSql, connection, transaction)
                    BindMinimalParams(cmd, data)
                    cmd.ExecuteNonQuery()
                    newResidentId = cmd.LastInsertedId
                End Using

                ' ── INSERT residentcategory rows ──────────────────────────────────
                SaveCategories(connection, transaction, CInt(newResidentId),
                               data.SelectedCategoryIds, data.AdditionalInfo)

                ' ── INSERT familymembers link (only if a valid familyId given) ────
                If familyId > 0 Then
                    ' Duplicate guard
                    Using chk As New MySqlCommand(
                            "SELECT COUNT(*) FROM familymembers " &
                            "WHERE FamilyId=@F AND ResidentId=@R",
                            connection, transaction)
                        chk.Parameters.AddWithValue("@F", familyId)
                        chk.Parameters.AddWithValue("@R", CInt(newResidentId))
                        If CInt(chk.ExecuteScalar()) = 0 Then
                            Using lnk As New MySqlCommand(
                                    "INSERT INTO familymembers " &
                                    "(FamilyId, ResidentId, RelationshipType) " &
                                    "VALUES (@FamilyId, @ResidentId, 'Member')",
                                    connection, transaction)
                                lnk.Parameters.AddWithValue("@FamilyId", familyId)
                                lnk.Parameters.AddWithValue("@ResidentId", CInt(newResidentId))
                                lnk.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                End If


                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("HouseholdFamilyResidents_Form", "ADD RESIDENT TO HOUSEHOLD",
                    LogInForm.CurrentUsername & " added resident to household (Household ID: " & data.HouseholdId & "): " & data.FirstName & " " & data.LastName,
                    connection, transaction)



                transaction.Commit()

                result.IsSuccess = True
                result.Message = "Resident added and linked to family successfully."
                result.ResidentId = CInt(newResidentId)

            Catch ex As Exception
                If transaction IsNot Nothing Then transaction.Rollback()
                result.Message = "Error saving resident: " & ex.Message
                Debug.WriteLine("InsertResident transaction error: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Database error: " & ex.Message
            Debug.WriteLine("InsertResident error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ' ============================================================================
    ' UPDATE — EDIT_MODE
    ' Updates the minimal resident fields and refreshes category assignments.
    ' ============================================================================

    ''' <summary>
    ''' Updates only the minimal columns for an existing resident and re-saves
    ''' their category links (delete-then-insert pattern).
    ''' HouseholdId is intentionally NOT updated here — use the full
    ''' ResidentAdding form or a dedicated household-transfer workflow.
    ''' </summary>
    Public Function UpdateResident(data As MinimalResidentData) As SaveResult
        Dim result As New SaveResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            If data.ResidentId <= 0 Then
                result.Message = "Invalid resident ID for update."
                Return result
            End If

            Dim valMsg As String = ValidateMinimal(data)
            If valMsg <> "" Then
                result.Message = valMsg
                Return result
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                Return result
            End If

            transaction = connection.BeginTransaction()

            Try
                ' ── UPDATE residents (minimal fields only) ────────────────────────
                Dim updateSql As String =
                    "UPDATE residents SET " &
                    "LastName       = @LastName, " &
                    "FirstName      = @FirstName, " &
                    "MiddleName     = @MiddleName, " &
                    "Suffix         = @Suffix, " &
                    "DateOfBirth    = @DateOfBirth, " &
                    "Sex            = @Sex, " &
                    "CivilStatus    = @CivilStatus, " &
                    "ContactNumber  = @ContactNumber, " &
                    "EmailAddress   = @EmailAddress " &
                    "WHERE ResidentId = @ResidentId"

                Using cmd As New MySqlCommand(updateSql, connection, transaction)
                    BindMinimalParams(cmd, data)
                    cmd.Parameters.AddWithValue("@ResidentId", data.ResidentId)
                    cmd.ExecuteNonQuery()
                End Using

                ' ── Refresh residentcategory (delete-then-insert) ─────────────────
                Using del As New MySqlCommand(
                        "DELETE FROM residentcategory WHERE ResidentId = @ResidentId",
                        connection, transaction)
                    del.Parameters.AddWithValue("@ResidentId", data.ResidentId)
                    del.ExecuteNonQuery()
                End Using

                SaveCategories(connection, transaction, data.ResidentId,
                               data.SelectedCategoryIds, data.AdditionalInfo)

                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("HouseholdFamilyResidents_Form", "UPDATE HOUSEHOLD RESIDENT",
                    LogInForm.CurrentUsername & " updated household resident (ID: " & data.ResidentId & "): " & data.FirstName & " " & data.LastName,
                    connection, transaction)

                transaction.Commit()

                result.IsSuccess = True
                result.Message = "Resident updated successfully."
                result.ResidentId = data.ResidentId

            Catch ex As Exception
                If transaction IsNot Nothing Then transaction.Rollback()
                result.Message = "Error updating resident: " & ex.Message
                Debug.WriteLine("UpdateResident transaction error: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Database error: " & ex.Message
            Debug.WriteLine("UpdateResident error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ' ============================================================================
    ' PRIVATE HELPERS
    ' ============================================================================

    ''' <summary>Validates the absolute minimum fields. Returns "" on pass.</summary>
    Private Function ValidateMinimal(data As MinimalResidentData) As String
        If String.IsNullOrWhiteSpace(data.LastName) Then Return "Last Name is required."
        If String.IsNullOrWhiteSpace(data.FirstName) Then Return "First Name is required."
        If String.IsNullOrWhiteSpace(data.Sex) Then Return "Sex is required."
        If data.HouseholdId <= 0 Then Return "A valid Household ID is required."
        Return ""
    End Function

    ''' <summary>Binds all minimal INSERT/UPDATE parameters to a command.</summary>
    Private Sub BindMinimalParams(cmd As MySqlCommand, data As MinimalResidentData)
        cmd.Parameters.AddWithValue("@LastName", data.LastName.Trim())
        cmd.Parameters.AddWithValue("@FirstName", data.FirstName.Trim())
        cmd.Parameters.AddWithValue("@MiddleName", NullIfBlank(data.MiddleName))
        cmd.Parameters.AddWithValue("@Suffix", NullIfBlank(data.Suffix))
        cmd.Parameters.AddWithValue("@DateOfBirth", data.DateOfBirth)
        cmd.Parameters.AddWithValue("@Sex", data.Sex.Trim())
        cmd.Parameters.AddWithValue("@CivilStatus", NullIfBlank(data.CivilStatus))
        cmd.Parameters.AddWithValue("@ContactNumber", NullIfBlank(data.ContactNumber))
        cmd.Parameters.AddWithValue("@EmailAddress", NullIfBlank(data.EmailAddress))
        cmd.Parameters.AddWithValue("@HouseholdId", data.HouseholdId)
    End Sub

    ''' <summary>
    ''' Inserts one residentcategory row per CategoryId.
    ''' Safe to call with an empty list (no-op).
    ''' </summary>
    Private Sub SaveCategories(connection As MySqlConnection,
                               transaction As MySqlTransaction,
                               residentId As Integer,
                               categoryIds As List(Of Integer),
                               additionalInfo As String)
        If categoryIds Is Nothing OrElse categoryIds.Count = 0 Then Return

        Dim sql As String =
            "INSERT INTO residentcategory (ResidentId, CategoryId, AdditionalInfo) " &
            "VALUES (@ResidentId, @CategoryId, @AdditionalInfo)"

        For Each catId As Integer In categoryIds
            Using cmd As New MySqlCommand(sql, connection, transaction)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)
                cmd.Parameters.AddWithValue("@CategoryId", catId)
                cmd.Parameters.AddWithValue("@AdditionalInfo", NullIfBlank(additionalInfo))
                cmd.ExecuteNonQuery()
            End Using
        Next
    End Sub

    Private Shared Function NullIfBlank(value As String) As Object
        Return If(String.IsNullOrWhiteSpace(value), CObj(DBNull.Value), value.Trim())
    End Function

    Private Shared Function DbStr(value As Object) As String
        Return If(value Is Nothing OrElse IsDBNull(value), "", value.ToString())
    End Function

End Class
