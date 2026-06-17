Imports MySql.Data.MySqlClient

''' <summary>
''' Resident Main Logic - Handles resident viewing and management business logic
''' Separated from UI form logic for reusability and maintainability
''' DATABASE: barangay_maly
''' </summary>
Public Class ResidentMainLogic

    ''' <summary>
    ''' Result class for resident operations
    ''' </summary>
    Public Class ResidentResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public Property ErrorCode As Integer

        Public Sub New()
            IsSuccess = False
            Message = ""
            ErrorCode = 0
        End Sub
    End Class

    ''' <summary>
    ''' Get all residents with essential information
    ''' Displays: Resident ID, FirstName, LastName, Sex, CivilStatus, ContactNumber
    ''' </summary>
    Public Function GetAllResidents() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            ' === CORRECTED QUERY - ALIGNED WITH ACTUAL DATABASE ===
            Dim query As String = "SELECT r.ResidentId, r.FirstName, r.LastName, r.Sex, r.CivilStatus, " &
                                 "r.ContactNumber, h.HouseholdNumber " &
                                 "FROM residents r " &
                                 "LEFT JOIN household h ON r.HouseholdId = h.HouseholdID " &
                                 "WHERE r.Is_Archived = 0 OR r.Is_Archived IS NULL " &
                                 "ORDER BY r.FirstName, r.LastName"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting all residents: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Search residents by name, contact, or household
    ''' </summary>
    Public Function SearchResidents(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            ' === CORRECTED SEARCH QUERY ===
            Dim query As String = "SELECT r.ResidentId, r.FirstName, r.LastName, r.Sex, r.CivilStatus, " &
                                 "r.ContactNumber, h.HouseholdNumber " &
                                 "FROM residents r " &
                                 "LEFT JOIN household h ON r.HouseholdId = h.HouseholdID " &
                                 "WHERE (r.Is_Archived = 0 OR r.Is_Archived IS NULL) AND " &
                                 "(r.FirstName LIKE @Search OR r.LastName LIKE @Search OR " &
                                 "r.ContactNumber LIKE @Search OR h.HouseholdNumber LIKE @Search) " &
                                 "ORDER BY r.FirstName, r.LastName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Search", "%" & searchTerm & "%")
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error searching residents: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Get resident by ID with full details
    ''' </summary>
    Public Function GetResidentById(residentId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT * FROM residents WHERE ResidentId = @ResidentId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting resident by ID: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Archive (mark as archived) resident
    ''' </summary>
    Public Function ArchiveResident(residentId As Integer) As ResidentResult
        Dim result As New ResidentResult()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            ' === CORRECTED - Using Is_Archived instead of IsActive ===
            Dim query As String = "UPDATE residents SET Is_Archived = 1 WHERE ResidentId = @ResidentId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)
                Dim rowsAffected = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    result.IsSuccess = True
                    result.Message = "Resident archived successfully."
                    result.ErrorCode = 0
                    ' === LOG AUDIT TRAIL ===
                    GlobalAuditLogger.Log("ResidentMain_Form", "ARCHIVE RESIDENT",
                    LogInForm.CurrentUsername & " archived resident (ID: " & residentId & ")")

                Else
                    result.Message = "Failed to archive resident."
                    result.ErrorCode = 1
                End If
            End Using

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error archiving resident: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

End Class
