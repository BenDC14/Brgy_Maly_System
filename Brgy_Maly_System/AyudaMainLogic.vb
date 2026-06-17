Imports MySql.Data.MySqlClient
Imports System.Data

''' <summary>
''' Resident Main Logic - Handles resident viewing and management business logic
''' Separated from UI form logic for reusability and maintainability
''' DATABASE: barangay_maly
''' </summary>
Public Class AyudaMainLogic

    ''' <summary>
    ''' Result class for resident operations
    ''' </summary>
    Public Class AyudaResult
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
    ''' Get all resident ayudas with essential information
    ''' Joins residentaid, residents, and barangayaid tables
    ''' </summary>
    Public Function GetAllResidentAids() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            ' SQL Query to join tables for readable data
            Dim query As String = "SELECT ra.ResidentAidId, CONCAT(r.FirstName, ' ', r.LastName) AS ResidentName, " &
                                 "ba.program_title AS ProgramTitle, ba.assistance_type AS AssistanceType, " &
                                 "ra.AidDate, ra.Quantity, ra.Description " &
                                 "FROM residentaid ra " &
                                 "INNER JOIN residents r ON ra.ResidentId = r.ResidentId " &
                                 "INNER JOIN barangayaid ba ON ra.AidId = ba.AidId " &
                                 "ORDER BY ra.AidDate DESC"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAllResidentAids Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Search resident ayudas by Resident Name or Program Title
    ''' </summary>
    Public Function SearchResidentAids(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT ra.ResidentAidId, CONCAT(r.FirstName, ' ', r.LastName) AS ResidentName, " &
                                 "ba.program_title AS ProgramTitle, ba.assistance_type AS AssistanceType, " &
                                 "ra.AidDate, ra.Quantity, ra.Description " &
                                 "FROM residentaid ra " &
                                 "INNER JOIN residents r ON ra.ResidentId = r.ResidentId " &
                                 "INNER JOIN barangayaid ba ON ra.AidId = ba.AidId " &
                                 "WHERE CONCAT(r.FirstName, ' ', r.LastName) LIKE @SearchTerm " &
                                 "OR ba.program_title LIKE @SearchTerm " &
                                 "ORDER BY ra.AidDate DESC"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@SearchTerm", "%" & searchTerm & "%")
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("SearchResidentAids Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Archive (mark as archived) resident aid record
    ''' </summary>
    Public Function ArchiveResidentAid(residentAidId As Integer) As AyudaResult
        Dim result As New AyudaResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            ' Since residentaid doesn't have an Is_Archived column, we perform a hard delete
            Dim query As String = "DELETE FROM residentaid WHERE ResidentAidId = @ResidentAidId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ResidentAidId", residentAidId)
                Dim rowsAffected = cmd.ExecuteNonQuery()

                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("AyudaMain_Form", "ARCHIVE AID CLAIM",
                LogInForm.CurrentUsername & " archived resident aid claim (ResidentAidId: " & residentAidId & ")")

                If rowsAffected > 0 Then
                    result.IsSuccess = True
                    result.Message = "Ayuda record archived successfully."
                    result.ErrorCode = 0
                Else
                    result.Message = "Failed to archive record."
                    result.ErrorCode = 1
                End If
            End Using

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("ArchiveResidentAid Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

End Class