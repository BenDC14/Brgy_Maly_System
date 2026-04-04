Imports MySql.Data.MySqlClient

''' <summary>
''' Household Main Logic - Handles all household viewing and management business logic
''' Separated from UI form logic for reusability and maintainability
''' </summary>
Public Class HouseholdMainLogic

    ''' <summary>
    ''' Result class for household operations
    ''' </summary>
    Public Class HouseholdResult
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
    ''' Get all households with essential address information
    ''' </summary>
    Public Function GetAllHouseholds() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT h.HouseholdID, h.HouseholdNumber, " &
                                 "a.BlockNumber, a.LotNumber, a.StreetName, a.Barangay, a.Municipality " &
                                 "FROM household h " &
                                 "INNER JOIN address a ON h.AddressID = a.AddressID " &
                                 "ORDER BY h.HouseholdNumber"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting all households: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Search households by household number, street, barangay, or municipality
    ''' </summary>
    Public Function SearchHouseholds(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT h.HouseholdID, h.HouseholdNumber, " &
                                 "a.BlockNumber, a.LotNumber, a.StreetName, a.Barangay, a.Municipality " &
                                 "FROM household h " &
                                 "INNER JOIN address a ON h.AddressID = a.AddressID " &
                                 "WHERE h.HouseholdNumber LIKE @Search OR " &
                                 "      a.StreetName LIKE @Search OR " &
                                 "      a.Barangay LIKE @Search OR " &
                                 "      a.Municipality LIKE @Search " &
                                 "ORDER BY h.HouseholdNumber"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Search", "%" & searchTerm & "%")
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error searching households: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Get full household details by ID
    ''' </summary>
    Public Function GetHouseholdById(householdId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT h.HouseholdID, h.HouseholdNumber, a.AddressID, a.HouseNumber, a.BlockNumber, " &
                                 "a.LotNumber, a.AreaNumber, a.StreetName, a.Village, a.Subdivision, a.Compound, " &
                                 "a.Barangay, a.Municipality, a.Province " &
                                 "FROM household h " &
                                 "INNER JOIN address a ON h.AddressID = a.AddressID " &
                                 "WHERE h.HouseholdID = @HouseholdID"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@HouseholdID", householdId)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting household by ID: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Archive (delete) household and related data
    ''' </summary>
    Public Function ArchiveHousehold(householdId As Integer) As HouseholdResult
        Dim result As New HouseholdResult()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            connection.Open()
            Dim transaction As MySqlTransaction = connection.BeginTransaction()

            Try
                ' === CHECK IF HOUSEHOLD HAS FAMILIES ===
                Dim checkQuery As String = "SELECT COUNT(*) FROM familyhead WHERE HouseholdId = @HouseholdID"
                Dim familyCount As Integer = 0

                Using checkCmd As New MySqlCommand(checkQuery, connection, transaction)
                    checkCmd.Parameters.AddWithValue("@HouseholdID", householdId)
                    familyCount = CInt(checkCmd.ExecuteScalar())
                End Using

                If familyCount > 0 Then
                    result.Message = "Cannot delete household. It has " & familyCount & " family/families. Delete families first."
                    result.ErrorCode = 2
                    Return result
                End If

                ' === GET ADDRESS ID ===
                Dim getAddressQuery As String = "SELECT AddressID FROM household WHERE HouseholdID = @HouseholdID"
                Dim addressId As Integer = -1

                Using getCmd As New MySqlCommand(getAddressQuery, connection, transaction)
                    getCmd.Parameters.AddWithValue("@HouseholdID", householdId)
                    Dim reader = getCmd.ExecuteReader()
                    If reader.Read() Then
                        addressId = CInt(reader("AddressID"))
                    End If
                    reader.Close()
                End Using

                ' === DELETE HOUSEHOLD ===
                Dim deleteQuery As String = "DELETE FROM household WHERE HouseholdID = @HouseholdID"
                Using deleteCmd As New MySqlCommand(deleteQuery, connection, transaction)
                    deleteCmd.Parameters.AddWithValue("@HouseholdID", householdId)
                    deleteCmd.ExecuteNonQuery()
                End Using

                ' === DELETE ADDRESS IF NO OTHER HOUSEHOLDS USE IT ===
                If addressId > -1 Then
                    Dim checkAddressQuery As String = "SELECT COUNT(*) FROM household WHERE AddressID = @AddressID"
                    Using checkAddressCmd As New MySqlCommand(checkAddressQuery, connection, transaction)
                        checkAddressCmd.Parameters.AddWithValue("@AddressID", addressId)
                        Dim count As Integer = CInt(checkAddressCmd.ExecuteScalar())

                        If count = 0 Then
                            Dim deleteAddressQuery As String = "DELETE FROM address WHERE AddressID = @AddressID"
                            Using deleteAddressCmd As New MySqlCommand(deleteAddressQuery, connection, transaction)
                                deleteAddressCmd.Parameters.AddWithValue("@AddressID", addressId)
                                deleteAddressCmd.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                End If

                transaction.Commit()
                result.IsSuccess = True
                result.Message = "Household archived successfully."
                result.ErrorCode = 0

            Catch ex As Exception
                transaction.Rollback()
                result.Message = "Error archiving household: " & ex.Message
                result.ErrorCode = 3
                Debug.WriteLine("Error in archive transaction: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in ArchiveHousehold: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

End Class
