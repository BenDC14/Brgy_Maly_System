' ================================================================================
' FILE: HouseholdAddingNewFamilyLogic.vb
' LAYER: Business Logic & Parameterized ADO.NET Data Layer
'
' Operations:
'   GetHouseholdNumber        — display-only HouseholdNumber for the header field
'   GetEligibleFamilyHeads    — residents in the household not already a head
'   GetRelationshipTypes       — populates cbCivilStatus
'   InsertFamily              — creates familyhead + links resident; returns FamilyId
'   GetFamilyMembers          — SELECT for the DGV (UNION head + members)
'   UpdateMemberRelationship  — parameterized UPDATE on familymembers
' ================================================================================
Imports MySql.Data.MySqlClient
Imports System.Data

Public Class HouseholdAddingNewFamilyLogic

    ' ============================================================================
    ' RESULT DTO
    ' ============================================================================
    Public Class SaveResult
        Public Property IsSuccess As Boolean = False
        Public Property Message As String = ""
        Public Property FamilyId As Integer = -1
    End Class

    ' ============================================================================
    ' READ — Household Number (for header display)
    ' ============================================================================

    ''' <summary>
    ''' Returns the HouseholdNumber string for a given HouseholdId.
    ''' </summary>
    Public Function GetHouseholdNumber(householdId As Integer) As String
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
            Debug.WriteLine("GetHouseholdNumber Error: " & ex.Message)
            Return ""
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ' ============================================================================
    ' READ — Eligible Family Heads
    ' Columns returned: ResidentId (Integer), FullName (String)
    ' Excludes: archived residents, residents already in familyhead for this HH
    ' ============================================================================

    ''' <summary>
    ''' Returns all residents in <paramref name="householdId"/> who are eligible
    ''' to be designated as a new Family Head.
    ''' A resident is excluded if they already appear as a head in the familyhead
    ''' table AND their household matches the one being worked on.
    ''' </summary>
    Public Function GetEligibleFamilyHeads(householdId As Integer) As DataTable
        Dim dt As New DataTable()
        Dim connection As MySqlConnection = Nothing

        dt.Columns.Add("ResidentId", GetType(Integer))
        dt.Columns.Add("FullName", GetType(String))

        Try
            If householdId <= 0 Then Return dt

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dt

            ' Build full name; trim double-space when MiddleName is empty.
            Dim query As String =
            "SELECT r.ResidentId, " &
            "TRIM(CONCAT(r.FirstName, ' ', " &
            "            IFNULL(NULLIF(r.MiddleName,''), ''), ' ', " &
            "            r.LastName)) AS FullName " &
            "FROM residents r " &
            "WHERE r.HouseholdId = @HouseholdId " &
            "  AND (r.Is_Archived IS NULL OR r.Is_Archived = 0) " &
            "  AND r.ResidentId NOT IN ( " &
            "        SELECT fh.ResidentId " &
            "        FROM   familyhead fh " &
            "        INNER JOIN residents rh ON fh.ResidentId = rh.ResidentId " &
            "        WHERE  rh.HouseholdId = @HouseholdId " &
            "      ) " &
            "ORDER BY r.LastName, r.FirstName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@HouseholdId", householdId)
                Using adapter As New MySqlDataAdapter(cmd)
                    dt.Clear()
                    adapter.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetEligibleFamilyHeads Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
           connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dt
    End Function


    ' ============================================================================
    ' READ — Relationship Types (for cbCivilStatus / "Relationship Type")
    ' Columns: RelationshipId (Integer), Relationship (String)
    ' ============================================================================

    ''' <summary>Returns all relationship types ordered alphabetically.</summary>
    Public Function GetRelationshipTypes() As DataTable
        Dim dt As New DataTable()
        Dim connection As MySqlConnection = Nothing

        dt.Columns.Add("RelationshipId", GetType(Integer))
        dt.Columns.Add("Relationship", GetType(String))

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dt

            Using cmd As New MySqlCommand(
                    "SELECT RelationshipId, Relationship " &
                    "FROM relationship ORDER BY Relationship ASC",
                    connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    dt.Clear() : adapter.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetRelationshipTypes Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dt
    End Function

    ' ============================================================================
    ' INSERT — Create New Family  (with automatic residentcategory sync)
    ' ============================================================================

    ''' <summary>
    ''' Creates a new family record inside a single transaction:
    '''   1. Duplicate-head guard.
    '''   2. INSERT INTO familyhead (ResidentId, FamilyName).
    '''   3. Checks residentcategory for the "Family Head" category pair;
    '''      inserts it only if it does not already exist (no UNIQUE key required).
    ''' Returns a <see cref="SaveResult"/> with FamilyId set on success.
    ''' </summary>
    Public Function InsertFamily(householdId As Integer,
                                 familyName As String,
                                 headResidentId As Integer) As SaveResult

        Dim result As New SaveResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            ' ── Pre-flight validation ─────────────────────────────────────────
            If householdId <= 0 Then
                result.Message = "Invalid household ID." : Return result
            End If
            If String.IsNullOrWhiteSpace(familyName) Then
                result.Message = "Family name is required." : Return result
            End If
            If headResidentId <= 0 Then
                result.Message = "A valid family head must be selected." : Return result
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database." : Return result
            End If

            transaction = connection.BeginTransaction()

            Try
                ' ── 1. Duplicate-head guard ───────────────────────────────────
                Using chk As New MySqlCommand(
                        "SELECT COUNT(*) FROM familyhead WHERE ResidentId = @ResidentId",
                        connection, transaction)
                    chk.Parameters.AddWithValue("@ResidentId", headResidentId)
                    If CInt(chk.ExecuteScalar()) > 0 Then
                        transaction.Rollback()
                        result.Message = "This resident is already a family head."
                        Return result
                    End If
                End Using

                ' ── 2. INSERT INTO familyhead ─────────────────────────────────
                Using cmd As New MySqlCommand(
                        "INSERT INTO familyhead (ResidentId, FamilyName) " &
                        "VALUES (@ResidentId, @FamilyName)",
                        connection, transaction)
                    cmd.Parameters.AddWithValue("@ResidentId", headResidentId)
                    cmd.Parameters.AddWithValue("@FamilyName", familyName.Trim())
                    cmd.ExecuteNonQuery()
                    result.FamilyId = CInt(cmd.LastInsertedId)
                End Using

                ' ── 3. Sync residentcategory — tag resident as "Family Head" ──
                Dim familyHeadCatId As Integer =
                    GetFamilyHeadCategoryId(connection, transaction)

                If familyHeadCatId > 0 Then
                    ' Guard: only insert if this (ResidentId, CategoryId) pair
                    ' does NOT already exist — no UNIQUE key needed.
                    Dim alreadyTagged As Integer = 0
                    Using chkCat As New MySqlCommand(
                            "SELECT COUNT(*) " &
                            "FROM   residentcategory " &
                            "WHERE  ResidentId = @ResidentId " &
                            "  AND  CategoryId = @CategoryId",
                            connection, transaction)
                        chkCat.Parameters.AddWithValue("@ResidentId", headResidentId)
                        chkCat.Parameters.AddWithValue("@CategoryId", familyHeadCatId)
                        alreadyTagged = CInt(chkCat.ExecuteScalar())
                    End Using

                    If alreadyTagged = 0 Then
                        Using catCmd As New MySqlCommand(
                                "INSERT INTO residentcategory " &
                                "    (ResidentId, CategoryId, AdditionalInfo) " &
                                "VALUES (@ResidentId, @CategoryId, NULL)",
                                connection, transaction)
                            catCmd.Parameters.AddWithValue("@ResidentId", headResidentId)
                            catCmd.Parameters.AddWithValue("@CategoryId", familyHeadCatId)
                            catCmd.ExecuteNonQuery()
                        End Using
                    End If
                Else
                    Debug.WriteLine("InsertFamily Warning: 'Family Head' category " &
                                    "not found in categories table. " &
                                    "residentcategory was NOT updated.")
                End If
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("HouseholdAddNewFamily_Form", "ADD FAMILY",
                    LogInForm.CurrentUsername & " added new family to Household ID: " & householdId,
                    connection, transaction)

                transaction.Commit()
                result.IsSuccess = True
                result.Message = "Family saved successfully."

            Catch ex As Exception
                If transaction IsNot Nothing Then transaction.Rollback()
                result.Message = "Error saving family: " & ex.Message
                Debug.WriteLine("InsertFamily transaction error: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Database error: " & ex.Message
            Debug.WriteLine("InsertFamily error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ' ============================================================================
    ' PRIVATE HELPER — Resolve "Family Head" CategoryId
    ' ============================================================================

    ''' <summary>
    ''' Returns the CategoryId whose Category name matches 'head', 'family head',
    ''' or 'family heads' (case-insensitive). Returns -1 if not found.
    ''' </summary>
    Private Function GetFamilyHeadCategoryId(connection As MySqlConnection,
                                              transaction As MySqlTransaction) As Integer
        Try
            Using cmd As New MySqlCommand(
                    "SELECT CategoryId " &
                    "FROM   categories " &
                    "WHERE  LOWER(Category) IN ('head','family head','family heads') " &
                    "LIMIT  1",
                    connection, transaction)
                Dim res As Object = cmd.ExecuteScalar()
                If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                    Return CInt(res)
                End If
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetFamilyHeadCategoryId Error: " & ex.Message)
        End Try
        Return -1
    End Function






    ' ============================================================================
    ' READ — Family Members DGV
    ' UNION: head row first (FamilyMemberId = -1, IsHead = 1), then members.
    ' ============================================================================

    ''' <summary>
    ''' Returns all rows for FamilyMembersDGV for the given FamilyId.
    ''' The head row carries FamilyMemberId = -1 and IsHead = 1 so the UI
    ''' can block "Edit Relationship" on it.
    ''' </summary>
    Public Function GetFamilyMembers(familyId As Integer) As DataTable
        Dim dt As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            If familyId <= 0 Then Return dt
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dt

            Dim query As String =
                "SELECT " &
                "  -1 AS FamilyMemberId, " &
                "  r.ResidentId, " &
                "  CONCAT(r.FirstName,' ',IFNULL(NULLIF(r.MiddleName,''),''),' ',r.LastName) AS FullName, " &
                "  'Head' AS RelationshipType, " &
                "  r.Sex, r.CivilStatus, r.ContactNumber, " &
                "  1 AS IsHead " &
                "FROM familyhead fh " &
                "INNER JOIN residents r ON fh.ResidentId = r.ResidentId " &
                "WHERE fh.FamilyId = @FamilyId " &
                "UNION ALL " &
                "SELECT " &
                "  fm.FamilyMemberId, " &
                "  r.ResidentId, " &
                "  CONCAT(r.FirstName,' ',IFNULL(NULLIF(r.MiddleName,''),''),' ',r.LastName) AS FullName, " &
                "  fm.RelationshipType, " &
                "  r.Sex, r.CivilStatus, r.ContactNumber, " &
                "  0 AS IsHead " &
                "FROM familymembers fm " &
                "INNER JOIN residents r ON fm.ResidentId = r.ResidentId " &
                "WHERE fm.FamilyId = @FamilyId " &
                "ORDER BY IsHead DESC, RelationshipType, FullName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@FamilyId", familyId)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetFamilyMembers Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dt
    End Function

    ' ============================================================================
    ' UPDATE — Member Relationship
    ' ============================================================================

    ''' <summary>
    ''' Updates the RelationshipType for a specific familymembers row
    ''' identified by its PK (<paramref name="familyMemberId"/>).
    ''' </summary>
    Public Function UpdateMemberRelationship(familyMemberId As Integer,
                                             newRelationship As String) As SaveResult
        Dim result As New SaveResult()
        Dim connection As MySqlConnection = Nothing

        Try
            If familyMemberId <= 0 Then
                result.Message = "Invalid member ID." : Return result
            End If
            If String.IsNullOrWhiteSpace(newRelationship) Then
                result.Message = "Relationship type cannot be blank." : Return result
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database." : Return result
            End If

            Using cmd As New MySqlCommand(
                    "UPDATE familymembers " &
                    "SET RelationshipType = @RelationshipType " &
                    "WHERE FamilyMemberId = @FamilyMemberId",
                    connection)
                cmd.Parameters.AddWithValue("@RelationshipType", newRelationship.Trim())
                cmd.Parameters.AddWithValue("@FamilyMemberId", familyMemberId)
                Dim rows As Integer = cmd.ExecuteNonQuery()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("HouseholdAddNewFamily_Form", "UPDATE RELATIONSHIP",
                    LogInForm.CurrentUsername & " updated relationship for Family Member ID: " & familyMemberId)

                If rows > 0 Then
                    result.IsSuccess = True
                    result.Message = "Relationship updated."
                Else
                    result.Message = "No record was updated. The member may not exist."
                End If
            End Using

        Catch ex As MySqlException
            result.Message = "Database error: " & ex.Message
            Debug.WriteLine("UpdateMemberRelationship MySqlException: " & ex.Message)
        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            Debug.WriteLine("UpdateMemberRelationship Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

End Class
