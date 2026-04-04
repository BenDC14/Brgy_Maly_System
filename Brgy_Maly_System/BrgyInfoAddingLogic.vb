Imports MySql.Data.MySqlClient
Imports System.IO

''' <summary>
''' Barangay Info Adding Logic - Handles all barangay info business logic
''' Separated from UI form logic for reusability and maintainability
''' </summary>
Public Class BrgyInfoAddingLogic

    ''' <summary>
    ''' Result class for barangay info operations
    ''' </summary>
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

    ''' <summary>
    ''' Save or Update Barangay Information
    ''' </summary>
    Public Function SaveBarangayInfo(brgyName As String, mission As String, vision As String, logoPath As String) As BrgyInfoResult
        Dim result As New BrgyInfoResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATION ===
            If String.IsNullOrWhiteSpace(brgyName) Then
                result.Message = "Please enter barangay name."
                result.ErrorCode = 1
                Return result
            End If

            If String.IsNullOrWhiteSpace(mission) Then
                result.Message = "Please enter mission."
                result.ErrorCode = 1
                Return result
            End If

            If String.IsNullOrWhiteSpace(vision) Then
                result.Message = "Please enter vision."
                result.ErrorCode = 1
                Return result
            End If

            ' === GET DATABASE CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            ' === CONVERT LOGO TO BYTES ===
            Dim logoBytes As Byte() = Nothing
            If Not String.IsNullOrEmpty(logoPath) AndAlso File.Exists(logoPath) Then
                logoBytes = File.ReadAllBytes(logoPath)
            End If

            ' === CHECK IF RECORD EXISTS ===
            Dim checkQuery As String = "SELECT COUNT(*) FROM barangayinformation"
            Dim recordCount As Integer = 0

            Using checkCmd As New MySqlCommand(checkQuery, connection)
                recordCount = CInt(checkCmd.ExecuteScalar())
            End Using

            If recordCount > 0 Then
                ' === UPDATE EXISTING RECORD ===
                Dim updateQuery As String = "UPDATE barangayinformation SET BarangayName = @BarangayName, Mission = @Mission, Vision = @Vision"
                If logoBytes IsNot Nothing Then
                    updateQuery &= ", Logo = @Logo"
                End If
                updateQuery &= ", LastUpdatedBy = @LastUpdatedBy, LastUpdatedDate = NOW()"

                Using cmd As New MySqlCommand(updateQuery, connection)
                    cmd.Parameters.AddWithValue("@BarangayName", brgyName)
                    cmd.Parameters.AddWithValue("@Mission", mission)
                    cmd.Parameters.AddWithValue("@Vision", vision)
                    If logoBytes IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@Logo", logoBytes)
                    End If
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", LogInForm.CurrentUserID)

                    cmd.ExecuteNonQuery()
                    result.IsSuccess = True
                    result.Message = "Barangay information updated successfully."
                    result.ErrorCode = 0
                End Using
            Else
                ' === INSERT NEW RECORD ===
                Dim insertQuery As String = "INSERT INTO barangayinformation (BarangayName, Mission, Vision, Logo, LastUpdatedBy, LastUpdatedDate) " &
                                           "VALUES (@BarangayName, @Mission, @Vision, @Logo, @LastUpdatedBy, NOW())"

                Using cmd As New MySqlCommand(insertQuery, connection)
                    cmd.Parameters.AddWithValue("@BarangayName", brgyName)
                    cmd.Parameters.AddWithValue("@Mission", mission)
                    cmd.Parameters.AddWithValue("@Vision", vision)
                    cmd.Parameters.AddWithValue("@Logo", If(logoBytes IsNot Nothing, logoBytes, DBNull.Value))
                    cmd.Parameters.AddWithValue("@LastUpdatedBy", LogInForm.CurrentUserID)

                    cmd.ExecuteNonQuery()
                    result.IsSuccess = True
                    result.Message = "Barangay information saved successfully."
                    result.ErrorCode = 0
                End Using
            End If

        Catch ex As MySqlException
            result.Message = "Database error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Database error in SaveBarangayInfo: " & ex.Message)
        Catch ex As Exception
            result.Message = "An error occurred: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in SaveBarangayInfo: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Get all Barangay Officials (Active only)
    ''' </summary>
    Public Function GetAllBarangayOfficials() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT OfficialId, CONCAT(FirstName, ' ', LastName) AS FullName, Position, TermStart, TermEnd, PhotoPath " &
                                 "FROM barangayofficials " &
                                 "WHERE IsActive = 1 " &
                                 "ORDER BY Position, FirstName"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting barangay officials: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Search Barangay Officials by name or position (Active only)
    ''' </summary>
    Public Function SearchBarangayOfficials(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT OfficialId, CONCAT(FirstName, ' ', LastName) AS FullName, Position, TermStart, TermEnd, PhotoPath " &
                                 "FROM barangayofficials " &
                                 "WHERE IsActive = 1 AND (FirstName LIKE @Search OR LastName LIKE @Search OR Position LIKE @Search) " &
                                 "ORDER BY Position, FirstName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Search", "%" & searchTerm & "%")
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error searching officials: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Archive (Deactivate) Barangay Official - Soft Delete Pattern
    ''' </summary>
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

            Dim query As String = "UPDATE barangayofficials SET IsActive = 0, UpdatedAt = NOW() WHERE OfficialId = @OfficialId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@OfficialId", officialId)
                Dim rowsAffected = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    result.IsSuccess = True
                    result.Message = "Official archived successfully."
                    result.ErrorCode = 0
                Else
                    result.Message = "Failed to archive official."
                    result.ErrorCode = 1
                End If
            End Using

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error archiving official: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Get Barangay Information
    ''' </summary>
    Public Function GetBarangayInfo() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT BarangayInfoID, BarangayName, Mission, Vision, Logo, LastUpdatedBy, LastUpdatedDate FROM barangayinformation LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting barangay info: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

End Class
