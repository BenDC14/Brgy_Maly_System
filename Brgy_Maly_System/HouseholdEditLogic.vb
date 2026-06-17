Imports MySql.Data.MySqlClient

''' <summary>
''' Household Edit Logic - Handles household editing business logic
''' Separated from UI form logic for reusability and maintainability
''' </summary>
Public Class HouseholdEditLogic

    ''' <summary>
    ''' Result class for household operations
    ''' </summary>
    Public Class HouseholdEditResult
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
    ''' Household Address Data Class
    ''' </summary>
    Public Class HouseholdAddressData
        Public Property HouseholdID As Integer
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
        Public Property AddressID As Integer
    End Class

    ''' <summary>
    ''' Get household with address details by ID
    ''' </summary>
    Public Function GetHouseholdById(householdId As Integer) As HouseholdAddressData
        Dim data As New HouseholdAddressData()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return data

            Dim query As String = "SELECT h.HouseholdID, h.HouseholdNumber, a.AddressID, a.HouseNumber, a.BlockNumber, " &
                                 "a.LotNumber, a.AreaNumber, a.StreetName, a.Village, a.Subdivision, a.Compound, " &
                                 "a.Barangay, a.Municipality, a.Province " &
                                 "FROM household h " &
                                 "INNER JOIN address a ON h.AddressID = a.AddressID " &
                                 "WHERE h.HouseholdID = @HouseholdID"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@HouseholdID", householdId)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        data.HouseholdID = CInt(reader("HouseholdID"))
                        data.HouseholdNumber = reader("HouseholdNumber").ToString()
                        data.AddressID = CInt(reader("AddressID"))
                        data.HouseNumber = If(IsDBNull(reader("HouseNumber")), "", reader("HouseNumber").ToString())
                        data.BlockNumber = reader("BlockNumber").ToString()
                        data.LotNumber = reader("LotNumber").ToString()
                        data.AreaNumber = If(IsDBNull(reader("AreaNumber")), "", reader("AreaNumber").ToString())
                        data.StreetName = reader("StreetName").ToString()
                        data.Village = If(IsDBNull(reader("Village")), "", reader("Village").ToString())
                        data.Subdivision = If(IsDBNull(reader("Subdivision")), "", reader("Subdivision").ToString())
                        data.Compound = If(IsDBNull(reader("Compound")), "", reader("Compound").ToString())
                        data.Barangay = reader("Barangay").ToString()
                        data.Municipality = reader("Municipality").ToString()
                        data.Province = reader("Province").ToString()
                    End If
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting household by ID: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return data
    End Function

    ''' <summary>
    ''' Update household and address information
    ''' </summary>
    Public Function UpdateHousehold(data As HouseholdAddressData) As HouseholdEditResult
        Dim result As New HouseholdEditResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATION ===
            If String.IsNullOrWhiteSpace(data.HouseholdNumber) Then
                result.Message = "Household Number is required."
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

            connection.Open()
            Dim transaction As MySqlTransaction = connection.BeginTransaction()

            Try
                ' === UPDATE ADDRESS TABLE ===
                Dim addressQuery As String = "UPDATE address SET HouseNumber = @HouseNumber, BlockNumber = @BlockNumber, " &
                                            "LotNumber = @LotNumber, AreaNumber = @AreaNumber, StreetName = @StreetName, " &
                                            "Village = @Village, Subdivision = @Subdivision, Compound = @Compound, " &
                                            "Barangay = @Barangay, Municipality = @Municipality, Province = @Province " &
                                            "WHERE AddressID = @AddressID"

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
                    addressCmd.Parameters.AddWithValue("@AddressID", data.AddressID)

                    addressCmd.ExecuteNonQuery()
                End Using

                ' === UPDATE HOUSEHOLD TABLE ===
                Dim householdQuery As String = "UPDATE household SET HouseholdNumber = @HouseholdNumber WHERE HouseholdID = @HouseholdID"

                Using householdCmd As New MySqlCommand(householdQuery, connection, transaction)
                    householdCmd.Parameters.AddWithValue("@HouseholdNumber", data.HouseholdNumber)
                    householdCmd.Parameters.AddWithValue("@HouseholdID", data.HouseholdID)

                    householdCmd.ExecuteNonQuery()
                End Using

                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("HouseholdEdit_Form", "UPDATE HOUSEHOLD",
                    LogInForm.CurrentUsername & " updated household address (Household ID: " & data.HouseholdID & ")",
                    connection, transaction)

                transaction.Commit()


                result.IsSuccess = True
                result.Message = "Household updated successfully."
                result.ErrorCode = 0

            Catch ex As Exception
                transaction.Rollback()
                result.Message = "Error updating household: " & ex.Message
                result.ErrorCode = 3
                Debug.WriteLine("Error in update transaction: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in UpdateHousehold: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ' ========================================
    ' TODO: For Future Implementation
    ' ========================================
    ' - GetFamilyHeadsByHousehold(householdId) - Load all family heads in this household
    ' - SearchFamilyHeads(householdId, searchTerm) - Search family heads by name
    ' - GetResidentsByHousehold(householdId) - Load all residents in this household
    ' - SearchResidents(householdId, searchTerm) - Search residents by name
    ' - Get counts of families and residents in household
    ' ========================================

End Class
