Imports MySql.Data.MySqlClient

''' <summary>
''' Household Adding Logic - Handles all household and address business logic
''' Separated from UI form logic for reusability and maintainability
''' Works with Household and Address tables
''' </summary>
Public Class HouseholdAddingLogic

    ''' <summary>
    ''' Result class for household operations
    ''' </summary>
    Public Class HouseholdResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public Property ErrorCode As Integer
        Public Property HouseholdId As Integer
        Public Property AddressId As Integer

        Public Sub New()
            IsSuccess = False
            Message = ""
            ErrorCode = 0
            HouseholdId = -1
            AddressId = -1
        End Sub
    End Class

    ''' <summary>
    ''' Household Address Data Class
    ''' </summary>
    Public Class HouseholdAddressData
        Public Property HouseholdNumber As String
        Public Property HouseNumber As String
        Public Property BlockNumber As String
        Public Property LotNumber As String
        Public Property AreaNumber As String
        Public Property StreetName As String
        Public Property Village As String
        Public Property Subdivision As String
        Public Property Compound As String
        Public Property Barangay As String
        Public Property Municipality As String
        Public Property Province As String
    End Class

    ''' <summary>
    ''' Validate household and address input data
    ''' </summary>
    Public Function ValidateHouseholdData(data As HouseholdAddressData) As HouseholdResult
        Dim result As New HouseholdResult()

        ' === HOUSEHOLD NUMBER VALIDATION ===
        If String.IsNullOrWhiteSpace(data.HouseholdNumber) Then
            result.Message = "Household Number is required."
            result.ErrorCode = 1
            Return result
        End If

        ' === AT LEAST ONE ADDRESS FIELD REQUIRED ===
        If String.IsNullOrWhiteSpace(data.BlockNumber) AndAlso
           String.IsNullOrWhiteSpace(data.LotNumber) AndAlso
           String.IsNullOrWhiteSpace(data.AreaNumber) AndAlso
           String.IsNullOrWhiteSpace(data.StreetName) AndAlso
           String.IsNullOrWhiteSpace(data.Village) AndAlso
           String.IsNullOrWhiteSpace(data.Subdivision) AndAlso
           String.IsNullOrWhiteSpace(data.Compound) AndAlso
           String.IsNullOrWhiteSpace(data.Barangay) AndAlso
           String.IsNullOrWhiteSpace(data.Municipality) AndAlso
           String.IsNullOrWhiteSpace(data.Province) Then
            result.Message = "At least one address field must be filled."
            result.ErrorCode = 1
            Return result
        End If

        ' === REQUIRED ADDRESS FIELDS ===
        If String.IsNullOrWhiteSpace(data.BlockNumber) Then
            result.Message = "Block Number is required."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(data.LotNumber) Then
            result.Message = "Lot Number is required."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(data.StreetName) Then
            result.Message = "Street Name is required."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(data.Barangay) Then
            result.Message = "Barangay is required."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(data.Municipality) Then
            result.Message = "Municipality is required."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(data.Province) Then
            result.Message = "Province is required."
            result.ErrorCode = 1
            Return result
        End If

        result.IsSuccess = True
        result.Message = "Validation passed."
        result.ErrorCode = 0
        Return result
    End Function

    ''' <summary>
    ''' Check if household number already exists
    ''' </summary>
    Public Function DoesHouseholdNumberExist(householdNumber As String) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim query As String = "SELECT COUNT(*) FROM household WHERE HouseholdNumber = @HouseholdNumber"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@HouseholdNumber", householdNumber)
                Dim count As Integer = CInt(cmd.ExecuteScalar())
                Return count > 0
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error checking household number: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Add new household with address
    ''' Inserts into both Household and Address tables
    ''' </summary>
    Public Function AddHouseholdWithAddress(data As HouseholdAddressData) As HouseholdResult
        Dim result As New HouseholdResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATE DATA ===
            Dim validationResult = ValidateHouseholdData(data)
            If Not validationResult.IsSuccess Then
                Return validationResult
            End If

            ' === CHECK FOR DUPLICATE HOUSEHOLD NUMBER ===
            If DoesHouseholdNumberExist(data.HouseholdNumber) Then
                result.Message = "Household Number already exists. Please use a different number."
                result.ErrorCode = 2
                Return result
            End If

            ' === GET DATABASE CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            connection.Open()

            ' === START TRANSACTION ===
            Dim transaction As MySqlTransaction = connection.BeginTransaction()

            Try
                ' === INSERT INTO ADDRESS TABLE FIRST ===
                Dim addressQuery As String = "INSERT INTO address (HouseNumber, BlockNumber, LotNumber, AreaNumber, StreetName, Village, Subdivision, Compound, Barangay, Municipality, Province) " &
                                            "VALUES (@HouseNumber, @BlockNumber, @LotNumber, @AreaNumber, @StreetName, @Village, @Subdivision, @Compound, @Barangay, @Municipality, @Province)"

                Dim addressId As Long = 0

                Using addressCmd As New MySqlCommand(addressQuery, connection, transaction)
                    addressCmd.Parameters.AddWithValue("@HouseNumber", If(String.IsNullOrEmpty(data.HouseNumber), DBNull.Value, data.HouseNumber))
                    addressCmd.Parameters.AddWithValue("@BlockNumber", data.BlockNumber)
                    addressCmd.Parameters.AddWithValue("@LotNumber", data.LotNumber)
                    addressCmd.Parameters.AddWithValue("@AreaNumber", If(String.IsNullOrEmpty(data.AreaNumber), DBNull.Value, data.AreaNumber))
                    addressCmd.Parameters.AddWithValue("@StreetName", data.StreetName)
                    addressCmd.Parameters.AddWithValue("@Village", If(String.IsNullOrEmpty(data.Village), DBNull.Value, data.Village))
                    addressCmd.Parameters.AddWithValue("@Subdivision", If(String.IsNullOrEmpty(data.Subdivision), DBNull.Value, data.Subdivision))
                    addressCmd.Parameters.AddWithValue("@Compound", If(String.IsNullOrEmpty(data.Compound), DBNull.Value, data.Compound))
                    addressCmd.Parameters.AddWithValue("@Barangay", data.Barangay)
                    addressCmd.Parameters.AddWithValue("@Municipality", data.Municipality)
                    addressCmd.Parameters.AddWithValue("@Province", data.Province)

                    addressCmd.ExecuteNonQuery()
                    addressId = addressCmd.LastInsertedId
                End Using

                ' === INSERT INTO HOUSEHOLD TABLE ===
                Dim householdQuery As String = "INSERT INTO household (HouseholdNumber, AddressID) " &
                                              "VALUES (@HouseholdNumber, @AddressID)"

                Using householdCmd As New MySqlCommand(householdQuery, connection, transaction)
                    householdCmd.Parameters.AddWithValue("@HouseholdNumber", data.HouseholdNumber)
                    householdCmd.Parameters.AddWithValue("@AddressID", addressId)

                    householdCmd.ExecuteNonQuery()
                    result.HouseholdId = CInt(householdCmd.LastInsertedId)
                End Using

                ' === COMMIT TRANSACTION ===
                transaction.Commit()

                result.IsSuccess = True
                result.Message = "Household added successfully."
                result.ErrorCode = 0
                result.AddressId = CInt(addressId)

            Catch ex As Exception
                ' === ROLLBACK ON ERROR ===
                transaction.Rollback()
                result.Message = "Error adding household: " & ex.Message
                result.ErrorCode = 3
                Debug.WriteLine("Error in transaction: " & ex.Message)
            End Try

        Catch ex As MySqlException
            result.Message = "Database error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("MySQL error in AddHouseholdWithAddress: " & ex.Message)
        Catch ex As Exception
            result.Message = "An error occurred: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in AddHouseholdWithAddress: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Get household with address details by Household ID
    ''' </summary>
    Public Function GetHouseholdWithAddress(householdId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT h.HouseholdID, h.HouseholdNumber, a.AddressID, a.HouseNumber, a.BlockNumber, a.LotNumber, a.AreaNumber, " &
                                 "a.StreetName, a.Village, a.Subdivision, a.Compound, a.Barangay, a.Municipality, a.Province " &
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
            Debug.WriteLine("Error getting household: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Get all households with address
    ''' </summary>
    Public Function GetAllHouseholds() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT h.HouseholdID, h.HouseholdNumber, a.AddressID, a.HouseNumber, a.BlockNumber, a.LotNumber, a.AreaNumber, " &
                                 "a.StreetName, a.Village, a.Subdivision, a.Compound, a.Barangay, a.Municipality, a.Province " &
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
    ''' Search households by household number or address
    ''' </summary>
    Public Function SearchHouseholds(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT h.HouseholdID, h.HouseholdNumber, a.AddressID, a.HouseNumber, a.BlockNumber, a.LotNumber, a.AreaNumber, " &
                                 "a.StreetName, a.Village, a.Subdivision, a.Compound, a.Barangay, a.Municipality, a.Province " &
                                 "FROM household h " &
                                 "INNER JOIN address a ON h.AddressID = a.AddressID " &
                                 "WHERE h.HouseholdNumber LIKE @Search OR a.StreetName LIKE @Search OR a.Barangay LIKE @Search " &
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
    ''' Delete household (cascade deletes address)
    ''' </summary>
    Public Function DeleteHousehold(householdId As Integer) As HouseholdResult
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
                ' === GET ADDRESS ID FIRST ===
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
                Dim deleteHouseholdQuery As String = "DELETE FROM household WHERE HouseholdID = @HouseholdID"
                Using deleteCmd As New MySqlCommand(deleteHouseholdQuery, connection, transaction)
                    deleteCmd.Parameters.AddWithValue("@HouseholdID", householdId)
                    deleteCmd.ExecuteNonQuery()
                End Using

                ' === DELETE ADDRESS IF NO OTHER HOUSEHOLDS USE IT ===
                If addressId > -1 Then
                    Dim checkQuery As String = "SELECT COUNT(*) FROM household WHERE AddressID = @AddressID"
                    Using checkCmd As New MySqlCommand(checkQuery, connection, transaction)
                        checkCmd.Parameters.AddWithValue("@AddressID", addressId)
                        Dim count As Integer = CInt(checkCmd.ExecuteScalar())

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
                result.Message = "Household deleted successfully."
                result.ErrorCode = 0

            Catch ex As Exception
                transaction.Rollback()
                result.Message = "Error deleting household: " & ex.Message
                result.ErrorCode = 3
                Debug.WriteLine("Error in delete transaction: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in DeleteHousehold: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

End Class
