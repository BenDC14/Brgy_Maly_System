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

    ''' <summary>
    ''' Validate official input data
    ''' </summary>
    Public Function ValidateOfficialData(firstName As String, lastName As String, position As String, termStart As DateTime, termEnd As DateTime) As OfficialResult
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
    ''' Add new Barangay Official
    ''' </summary>
    Public Function AddBarangayOfficial(firstName As String, lastName As String, position As String, termStart As DateTime, termEnd As DateTime, photoPath As String) As OfficialResult
        Dim result As New OfficialResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATE ===
            Dim validationResult = ValidateOfficialData(firstName, lastName, position, termStart, termEnd)
            If Not validationResult.IsSuccess Then
                Return validationResult
            End If

            ' === GET DATABASE CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            ' === CONVERT PHOTO TO BYTES ===
            Dim photoBytes As Byte() = Nothing
            If Not String.IsNullOrEmpty(photoPath) AndAlso File.Exists(photoPath) Then
                photoBytes = File.ReadAllBytes(photoPath)
            End If

            ' === INSERT NEW OFFICIAL ===
            Dim insertQuery As String = "INSERT INTO barangayofficials (FirstName, LastName, Position, TermStart, TermEnd, PhotoPath, IsActive) " &
                                       "VALUES (@FirstName, @LastName, @Position, @TermStart, @TermEnd, @PhotoPath, 1)"

            Using cmd As New MySqlCommand(insertQuery, connection)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Position", position)
                cmd.Parameters.AddWithValue("@TermStart", termStart)
                cmd.Parameters.AddWithValue("@TermEnd", termEnd)
                cmd.Parameters.AddWithValue("@PhotoPath", If(photoBytes IsNot Nothing, photoBytes, DBNull.Value))

                cmd.ExecuteNonQuery()
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

            Dim query As String = "SELECT OfficialId, FirstName, LastName, Position, TermStart, TermEnd, PhotoPath, IsActive " &
                                 "FROM barangayofficials WHERE OfficialId = @OfficialId LIMIT 1"

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
    ''' Update existing Barangay Official
    ''' </summary>
    Public Function UpdateBarangayOfficial(officialId As Integer, firstName As String, lastName As String, position As String, termStart As DateTime, termEnd As DateTime, photoPath As String) As OfficialResult
        Dim result As New OfficialResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATE ===
            Dim validationResult = ValidateOfficialData(firstName, lastName, position, termStart, termEnd)
            If Not validationResult.IsSuccess Then
                Return validationResult
            End If

            ' === GET DATABASE CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            ' === CONVERT PHOTO TO BYTES ===
            Dim photoBytes As Byte() = Nothing
            If Not String.IsNullOrEmpty(photoPath) AndAlso File.Exists(photoPath) Then
                photoBytes = File.ReadAllBytes(photoPath)
            End If

            ' === UPDATE OFFICIAL ===
            Dim updateQuery As String = "UPDATE barangayofficials SET FirstName = @FirstName, LastName = @LastName, Position = @Position, " &
                                       "TermStart = @TermStart, TermEnd = @TermEnd"
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
                If rowsAffected > 0 Then
                    result.IsSuccess = True
                    result.Message = "Official updated successfully."
                    result.ErrorCode = 0
                Else
                    result.Message = "Failed to update official."
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
