Imports MySql.Data.MySqlClient
Imports System.Data

Public Class HouseholdEdit_Logic

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

    Public Class HouseholdAddressData
        Public Property HouseholdID As Integer
        Public Property HouseholdNumber As String
        Public Property AddressID As Integer
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

    Public Function GetHouseholdById(householdId As Integer) As HouseholdAddressData
        Dim data As New HouseholdAddressData()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return data

            Dim query As String =
                "SELECT h.HouseholdID, h.HouseholdNumber, a.AddressID, " &
                "a.HouseNumber, a.BlockNumber, a.LotNumber, a.AreaNumber, " &
                "a.StreetName, a.Village, a.Subdivision, a.Compound, " &
                "a.Barangay, a.Municipality, a.Province " &
                "FROM household h " &
                "INNER JOIN address a ON h.AddressID = a.AddressID " &
                "WHERE h.HouseholdID = @HouseholdID LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@HouseholdID", householdId)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        data.HouseholdID = SafeInt(reader("HouseholdID"))
                        data.HouseholdNumber = SafeString(reader("HouseholdNumber"))
                        data.AddressID = SafeInt(reader("AddressID"))
                        data.HouseNumber = SafeString(reader("HouseNumber"))
                        data.BlockNumber = SafeString(reader("BlockNumber"))
                        data.LotNumber = SafeString(reader("LotNumber"))
                        data.AreaNumber = SafeString(reader("AreaNumber"))
                        data.StreetName = SafeString(reader("StreetName"))
                        data.Village = SafeString(reader("Village"))
                        data.Subdivision = SafeString(reader("Subdivision"))
                        data.Compound = SafeString(reader("Compound"))
                        data.Barangay = SafeString(reader("Barangay"))
                        data.Municipality = SafeString(reader("Municipality"))
                        data.Province = SafeString(reader("Province"))
                    End If
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetHouseholdById Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return data
    End Function

    Public Function GetFamilyHeadsByHousehold(householdId As Integer, Optional searchTerm As String = "") As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT r.ResidentId, " &
                "CONCAT(r.FirstName, ' ', IFNULL(NULLIF(r.MiddleName, ''), ''), ' ', r.LastName) AS FullName, " &
                "r.FirstName, r.MiddleName, r.LastName, r.Sex, r.CivilStatus, r.ContactNumber, h.HouseholdNumber " &
                "FROM residents r " &
                "INNER JOIN household h ON r.HouseholdId = h.HouseholdID " &
                "WHERE r.HouseholdId = @HouseholdID " &
                "AND (r.Is_Archived = 0 OR r.Is_Archived IS NULL) " &
                "AND (" &
                "EXISTS (" &
                "SELECT 1 FROM residentcategory rc " &
                "INNER JOIN categories c ON rc.CategoryId = c.CategoryId " &
                "WHERE rc.ResidentId = r.ResidentId " &
                "AND LOWER(c.Category) IN ('head', 'family head', 'family heads')" &
                ") " &
                "OR EXISTS (" &
                "SELECT 1 FROM familyhead fh WHERE fh.ResidentId = r.ResidentId" &
                ")" &
                ") "

            If Not String.IsNullOrWhiteSpace(searchTerm) Then
                query &= "AND (r.FirstName LIKE @Search OR r.MiddleName LIKE @Search OR r.LastName LIKE @Search OR r.ContactNumber LIKE @Search) "
            End If

            query &= "ORDER BY r.LastName, r.FirstName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@HouseholdID", householdId)

                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    cmd.Parameters.AddWithValue("@Search", "%" & searchTerm.Trim() & "%")
                End If

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetFamilyHeadsByHousehold Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function GetNonHeadResidentsByHousehold(householdId As Integer, Optional searchTerm As String = "") As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT r.ResidentId, " &
                "CONCAT(r.FirstName, ' ', IFNULL(NULLIF(r.MiddleName, ''), ''), ' ', r.LastName) AS FullName, " &
                "r.FirstName, r.MiddleName, r.LastName, r.Sex, r.CivilStatus, r.ContactNumber, h.HouseholdNumber " &
                "FROM residents r " &
                "INNER JOIN household h ON r.HouseholdId = h.HouseholdID " &
                "WHERE r.HouseholdId = @HouseholdID " &
                "AND (r.Is_Archived = 0 OR r.Is_Archived IS NULL) " &
                "AND NOT EXISTS (" &
                "SELECT 1 FROM residentcategory rc " &
                "INNER JOIN categories c ON rc.CategoryId = c.CategoryId " &
                "WHERE rc.ResidentId = r.ResidentId " &
                "AND LOWER(c.Category) IN ('head', 'family head', 'family heads')" &
                ") " &
                "AND NOT EXISTS (" &
                "SELECT 1 FROM familyhead fh WHERE fh.ResidentId = r.ResidentId" &
                ") "

            If Not String.IsNullOrWhiteSpace(searchTerm) Then
                query &= "AND (r.FirstName LIKE @Search OR r.MiddleName LIKE @Search OR r.LastName LIKE @Search OR r.ContactNumber LIKE @Search) "
            End If

            query &= "ORDER BY r.LastName, r.FirstName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@HouseholdID", householdId)

                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    cmd.Parameters.AddWithValue("@Search", "%" & searchTerm.Trim() & "%")
                End If

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetNonHeadResidentsByHousehold Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function UpdateAddress(data As HouseholdAddressData) As HouseholdEditResult
        Dim result As New HouseholdEditResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            If data Is Nothing Then
                result.Message = "No household data was provided."
                result.ErrorCode = 1
                Return result
            End If

            If data.AddressID <= 0 Then
                result.Message = "Invalid address ID."
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

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            transaction = connection.BeginTransaction()

            Dim query As String =
                "UPDATE address SET " &
                "HouseNumber = @HouseNumber, " &
                "BlockNumber = @BlockNumber, " &
                "LotNumber = @LotNumber, " &
                "AreaNumber = @AreaNumber, " &
                "StreetName = @StreetName, " &
                "Village = @Village, " &
                "Subdivision = @Subdivision, " &
                "Compound = @Compound, " &
                "Barangay = @Barangay, " &
                "Municipality = @Municipality, " &
                "Province = @Province " &
                "WHERE AddressID = @AddressID"

            Using cmd As New MySqlCommand(query, connection, transaction)
                cmd.Parameters.AddWithValue("@HouseNumber", DbValue(data.HouseNumber))
                cmd.Parameters.AddWithValue("@BlockNumber", DbValue(data.BlockNumber))
                cmd.Parameters.AddWithValue("@LotNumber", DbValue(data.LotNumber))
                cmd.Parameters.AddWithValue("@AreaNumber", DbValue(data.AreaNumber))
                cmd.Parameters.AddWithValue("@StreetName", DbValue(data.StreetName))
                cmd.Parameters.AddWithValue("@Village", DbValue(data.Village))
                cmd.Parameters.AddWithValue("@Subdivision", DbValue(data.Subdivision))
                cmd.Parameters.AddWithValue("@Compound", DbValue(data.Compound))
                cmd.Parameters.AddWithValue("@Barangay", data.Barangay.Trim())
                cmd.Parameters.AddWithValue("@Municipality", data.Municipality.Trim())
                cmd.Parameters.AddWithValue("@Province", data.Province.Trim())
                cmd.Parameters.AddWithValue("@AddressID", data.AddressID)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected <= 0 Then
                    transaction.Rollback()
                    result.Message = "No address record was updated."
                    result.ErrorCode = 1
                    Return result
                End If
            End Using

            ' === LOG AUDIT TRAIL ===
            GlobalAuditLogger.Log("HouseholdEdit_Form", "UPDATE ADDRESS",
                    LogInForm.CurrentUsername & " updated household address details (Household ID: " & data.HouseholdID & ")",
                    connection, transaction)

            transaction.Commit()

            result.IsSuccess = True
            result.Message = "Household address updated successfully."
            result.ErrorCode = 0

        Catch ex As Exception
            Try
                If transaction IsNot Nothing Then transaction.Rollback()
            Catch rollbackEx As Exception
                Debug.WriteLine("UpdateAddress Rollback Error: " & rollbackEx.Message)
            End Try

            result.IsSuccess = False
            result.Message = "Error updating household address: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("UpdateAddress Error: " & ex.Message)

        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    Public Function UpdateHousehold(data As HouseholdAddressData) As HouseholdEditResult
        Return UpdateAddress(data)
    End Function

    Private Function DbValue(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If

        Return value.Trim()
    End Function

    Private Function SafeString(value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then
            Return ""
        End If

        Return value.ToString()
    End Function

    Private Function SafeInt(value As Object) As Integer
        If value Is Nothing OrElse IsDBNull(value) Then
            Return 0
        End If

        Return CInt(value)
    End Function

End Class