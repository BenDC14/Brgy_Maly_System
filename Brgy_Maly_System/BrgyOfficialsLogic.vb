Imports MySql.Data.MySqlClient
Imports System.IO

''' <summary>
''' Barangay Officials Logic - Handles all official-related business logic
''' Separated from UI form logic for reusability and maintainability
''' </summary>
Public Class BrgyOfficialsLogic

    ''' <summary>
    ''' Result class for official operations
    ''' </summary>
    Public Class OfficialResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public Property ErrorCode As Integer
        Public Property OfficialId As Integer

        Public Sub New()
            IsSuccess = False
            Message = ""
            ErrorCode = 0
            OfficialId = -1
        End Sub
    End Class

    ' ==========================================================================
    '  ACTIVE QUOTA MAP
    '  Key   = Position value stored in DB (exact match, case-sensitive)
    '  Value = Maximum number of simultaneously active records allowed
    ' ==========================================================================
    Private Shared ReadOnly ActiveQuota As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase) From {
        {"Barangay Captain", 1},
        {"Barangay Kagawad", 8},
        {"SK Chairman", 1},
        {"Barangay Secretary", 1},
        {"Barangay Treasurer", 1},
        {"Barangay Adminstrator", 1},
        {"Barangay SK Kagawad", 7}
    }

    ''' <summary>
    ''' Returns the maximum number of active officials allowed for a given position.
    ''' Returns -1 when the position has no defined quota (unlimited).
    ''' </summary>
    Public Function GetQuotaForPosition(position As String) As Integer
        Dim limit As Integer
        If ActiveQuota.TryGetValue(position, limit) Then
            Return limit
        End If
        Return -1
    End Function

    ''' <summary>
    ''' Counts how many active records currently exist for the specified position.
    ''' Uses a parameterized query to prevent SQL injection.
    ''' Pass excludeOfficialId > 0 when editing an existing record so it is not
    ''' counted against itself.
    ''' </summary>
    Public Function GetActiveCountByPosition(position As String,
                                             Optional excludeOfficialId As Integer = -1) As Integer
        Dim connection As MySqlConnection = Nothing
        Dim activeCount As Integer = 0

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return 0

            Dim query As String
            If excludeOfficialId > 0 Then
                ' Editing mode: exclude the record being edited from the count
                query = "SELECT COUNT(*) FROM barangayofficials " &
                        "WHERE Position = @Position AND IsActive = 1 " &
                        "AND OfficialId <> @ExcludeId"
            Else
                ' Add mode: count all active records for this position
                query = "SELECT COUNT(*) FROM barangayofficials " &
                        "WHERE Position = @Position AND IsActive = 1"
            End If

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Position", position)
                If excludeOfficialId > 0 Then
                    cmd.Parameters.AddWithValue("@ExcludeId", excludeOfficialId)
                End If
                activeCount = CInt(cmd.ExecuteScalar())
            End Using

        Catch ex As MySqlException
            Debug.WriteLine("DB error in GetActiveCountByPosition: " & ex.Message)
        Catch ex As Exception
            Debug.WriteLine("Error in GetActiveCountByPosition: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return activeCount
    End Function

    ''' <summary>
    ''' Checks whether adding or keeping an official active for the given position
    ''' would violate the quota.  Returns an OfficialResult with IsSuccess=False
    ''' and the blocking message when the quota is already met.
    ''' </summary>
    Public Function CheckActiveQuota(position As String,
                                     Optional excludeOfficialId As Integer = -1) As OfficialResult
        Dim result As New OfficialResult()
        Dim limit As Integer = GetQuotaForPosition(position)

        ' No defined quota → always allowed
        If limit < 0 Then
            result.IsSuccess = True
            result.Message = "Quota check passed (no limit defined)."
            Return result
        End If

        Dim currentCount As Integer = GetActiveCountByPosition(position, excludeOfficialId)

        If currentCount >= limit Then
            result.IsSuccess = False
            result.Message = "You cannot add new active officials for this position. The limit has already been reached."
            result.ErrorCode = 2
        Else
            result.IsSuccess = True
            result.Message = "Quota check passed."
            result.ErrorCode = 0
        End If

        Return result
    End Function

    ''' <summary>
    ''' Validate official input data
    ''' </summary>
    Public Function ValidateOfficialData(firstName As String, lastName As String,
                                         position As String,
                                         termStart As DateTime,
                                         termEnd As DateTime) As OfficialResult
        Dim result As New OfficialResult()

        ' === VALIDATION ===
        If String.IsNullOrWhiteSpace(firstName) Then
            result.Message = "Please enter first name."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(lastName) Then
            result.Message = "Please enter last name."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(position) Then
            result.Message = "Please select a position."
            result.ErrorCode = 1
            Return result
        End If

        If termStart >= termEnd Then
            result.Message = "Term start date must be before term end date."
            result.ErrorCode = 1
            Return result
        End If

        result.IsSuccess = True
        result.Message = "Validation passed."
        result.ErrorCode = 0
        Return result
    End Function

    ''' <summary>
    ''' Add new Barangay Official — enforces active-quota before inserting
    ''' </summary>
    Public Function AddBarangayOfficial(firstName As String, lastName As String,
                                        position As String,
                                        termStart As DateTime, termEnd As DateTime,
                                        photoPath As String) As OfficialResult
        Dim result As New OfficialResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            ' === STEP 1: FIELD VALIDATION ===
            Dim validationResult = ValidateOfficialData(firstName, lastName, position, termStart, termEnd)
            If Not validationResult.IsSuccess Then Return validationResult

            ' === STEP 2: ACTIVE QUOTA CHECK ===
            ' New records are always inserted with IsActive = 1, so we must
            ' verify the slot is still available before hitting the database.
            Dim quotaResult = CheckActiveQuota(position)
            If Not quotaResult.IsSuccess Then Return quotaResult

            ' === STEP 3: DATABASE CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            ' === STEP 4: CONVERT PHOTO TO BYTES ===
            Dim photoBytes As Byte() = Nothing
            If Not String.IsNullOrEmpty(photoPath) AndAlso File.Exists(photoPath) Then
                photoBytes = File.ReadAllBytes(photoPath)
            End If

            ' === STEP 5: INSERT NEW OFFICIAL ===
            Dim insertQuery As String =
                "INSERT INTO barangayofficials " &
                "  (FirstName, LastName, Position, TermStart, TermEnd, PhotoPath, IsActive) " &
                "VALUES (@FirstName, @LastName, @Position, @TermStart, @TermEnd, @PhotoPath, 1)"

            Using cmd As New MySqlCommand(insertQuery, connection)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Position", position)
                cmd.Parameters.AddWithValue("@TermStart", termStart)
                cmd.Parameters.AddWithValue("@TermEnd", termEnd)
                cmd.Parameters.AddWithValue("@PhotoPath",
                    If(photoBytes IsNot Nothing, CObj(photoBytes), DBNull.Value))

                cmd.ExecuteNonQuery()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("BrgyOfficials_Form", "ADD OFFICIAL",
                    LogInForm.CurrentUsername & " added barangay official: " & firstName & " " & lastName,
                    connection, transaction)

                result.IsSuccess = True
                result.Message = "Official added successfully."
                result.ErrorCode = 0
            End Using

        Catch ex As MySqlException
            result.Message = "Database error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Database error in AddBarangayOfficial: " & ex.Message)
        Catch ex As Exception
            result.Message = "An error occurred: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in AddBarangayOfficial: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Get official data by ID
    ''' </summary>
    Public Function GetOfficialById(officialId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT OfficialId, FirstName, LastName, Position, " &
                "       TermStart, TermEnd, PhotoPath, IsActive " &
                "FROM barangayofficials " &
                "WHERE OfficialId = @OfficialId LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@OfficialId", officialId)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting official: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Update existing Barangay Official.
    ''' When the record being edited is still active (IsActive = 1), the quota
    ''' check excludes its own row so it is not blocked by itself.
    ''' </summary>
    Public Function UpdateBarangayOfficial(officialId As Integer,
                                           firstName As String, lastName As String,
                                           position As String,
                                           termStart As DateTime, termEnd As DateTime,
                                           photoPath As String,
                                           Optional isActive As Boolean = True) As OfficialResult
        Dim result As New OfficialResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            ' === STEP 1: FIELD VALIDATION ===
            Dim validationResult = ValidateOfficialData(firstName, lastName, position, termStart, termEnd)
            If Not validationResult.IsSuccess Then Return validationResult

            ' === STEP 2: ACTIVE QUOTA CHECK (only when record is/will be active) ===
            ' Pass the current record's ID so the count query excludes it from
            ' the tally — an edit should never block itself.
            If isActive Then
                Dim quotaResult = CheckActiveQuota(position, excludeOfficialId:=officialId)
                If Not quotaResult.IsSuccess Then Return quotaResult
            End If

            ' === STEP 3: DATABASE CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            ' === STEP 4: CONVERT PHOTO TO BYTES ===
            Dim photoBytes As Byte() = Nothing
            If Not String.IsNullOrEmpty(photoPath) AndAlso File.Exists(photoPath) Then
                photoBytes = File.ReadAllBytes(photoPath)
            End If

            ' === STEP 5: BUILD & EXECUTE UPDATE QUERY ===
            Dim updateQuery As String =
                "UPDATE barangayofficials " &
                "SET FirstName = @FirstName, LastName = @LastName, " &
                "    Position  = @Position,  TermStart = @TermStart, " &
                "    TermEnd   = @TermEnd"

            If photoBytes IsNot Nothing Then
                updateQuery &= ", PhotoPath = @PhotoPath"
            End If

            updateQuery &= ", UpdatedAt = NOW() WHERE OfficialId = @OfficialId"

            Using cmd As New MySqlCommand(updateQuery, connection)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Position", position)
                cmd.Parameters.AddWithValue("@TermStart", termStart)
                cmd.Parameters.AddWithValue("@TermEnd", termEnd)
                If photoBytes IsNot Nothing Then
                    cmd.Parameters.AddWithValue("@PhotoPath", photoBytes)
                End If
                cmd.Parameters.AddWithValue("@OfficialId", officialId)

                Dim rowsAffected = cmd.ExecuteNonQuery()

                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("BrgyOfficials_Form", "UPDATE OFFICIAL",
                    LogInForm.CurrentUsername & " updated barangay official (ID: " & officialId & ")",
                    connection, transaction)


                If rowsAffected > 0 Then
                    result.IsSuccess = True
                    result.Message = "Official updated successfully."
                    result.ErrorCode = 0
                Else
                    result.Message = "Failed to update official. Record may no longer exist."
                    result.ErrorCode = 1
                End If
            End Using

        Catch ex As MySqlException
            result.Message = "Database error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Database error in UpdateBarangayOfficial: " & ex.Message)
        Catch ex As Exception
            result.Message = "An error occurred: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in UpdateBarangayOfficial: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

End Class
