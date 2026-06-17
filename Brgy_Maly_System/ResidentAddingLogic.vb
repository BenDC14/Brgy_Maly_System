Imports MySql.Data.MySqlClient
Imports System.Data

Public Class ResidentAddingLogic

    Public Class ResidentOperationResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public Property ErrorCode As Integer
        Public Property ResidentId As Integer

        Public Sub New()
            IsSuccess = False
            Message = ""
            ErrorCode = 0
            ResidentId = -1
        End Sub
    End Class

    Public Class ResidentData
        Public Property ResidentId As Integer
        Public Property FirstName As String
        Public Property LastName As String
        Public Property MiddleName As String
        Public Property Suffix As String
        Public Property DateOfBirth As Date
        Public Property PlaceOfBirth As String
        Public Property Sex As String
        Public Property CivilStatus As String
        Public Property Religion As String
        Public Property Citizenship As String
        Public Property Occupation As String
        Public Property ContactNumber As String
        Public Property EmailAddress As String
        Public Property Voter As Boolean
        Public Property HouseholdId As Integer
        Public Property EducationLevel As String
        Public Property EducationalStatus As String
        Public Property AdditionalInfo As String
        Public Property SelectedCategoryIds As List(Of Integer)

        Public Sub New()
            SelectedCategoryIds = New List(Of Integer)()
            AdditionalInfo = ""
        End Sub
    End Class

    Public Function GetCategoriesTable() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        dataTable.Columns.Add("CategoryId", GetType(Integer))
        dataTable.Columns.Add("Category", GetType(String))

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT CategoryId, Category " &
                "FROM categories " &
                "ORDER BY Category ASC"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    dataTable.Clear()
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetCategoriesTable Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function GetAllHouseholds() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT h.HouseholdID, h.HouseholdNumber, " &
                "CONCAT(a.BlockNumber, ' Block, ', a.LotNumber, ' Lot, ', " &
                "a.StreetName, ', ', a.Barangay, ', ', " &
                "a.Municipality, ', ', a.Province) AS FullAddress " &
                "FROM household h " &
                "INNER JOIN address a ON h.AddressID = a.AddressID " &
                "ORDER BY h.HouseholdNumber"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAllHouseholds Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function GetHouseholdAddress(householdId As Integer) As String
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return ""

            Dim query As String =
                "SELECT CONCAT(a.BlockNumber, ' Block, ', a.LotNumber, ' Lot, ', " &
                "a.StreetName, ', ', a.Barangay, ', ', " &
                "a.Municipality, ', ', a.Province) AS FullAddress " &
                "FROM household h " &
                "INNER JOIN address a ON h.AddressID = a.AddressID " &
                "WHERE h.HouseholdID = @HouseholdID"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@HouseholdID", householdId)
                Dim result = cmd.ExecuteScalar()

                If result IsNot Nothing Then
                    Return result.ToString()
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetHouseholdAddress Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return ""
    End Function

    Public Function GetResidentById(residentId As Integer) As ResidentData
        Dim data As New ResidentData()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return data

            Using cmd As New MySqlCommand("SELECT * FROM residents WHERE ResidentId = @ResidentId", connection)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        data.ResidentId = CInt(reader("ResidentId"))
                        data.FirstName = DBNullToString(reader("FirstName"))
                        data.LastName = DBNullToString(reader("LastName"))
                        data.MiddleName = DBNullToString(reader("MiddleName"))
                        data.Suffix = DBNullToString(reader("Suffix"))
                        data.DateOfBirth = If(IsDBNull(reader("DateOfBirth")), Today, CDate(reader("DateOfBirth")))
                        data.PlaceOfBirth = DBNullToString(reader("PlaceOfBirth"))
                        data.Sex = DBNullToString(reader("Sex"))
                        data.CivilStatus = DBNullToString(reader("CivilStatus"))
                        data.Religion = DBNullToString(reader("Religion"))
                        data.Citizenship = DBNullToString(reader("Citizenship"))
                        data.Occupation = DBNullToString(reader("Occupation"))
                        data.ContactNumber = DBNullToString(reader("ContactNumber"))
                        data.EmailAddress = DBNullToString(reader("EmailAddress"))
                        data.Voter = If(IsDBNull(reader("Voter")), False, CBool(reader("Voter")))
                        data.HouseholdId = If(IsDBNull(reader("HouseholdId")), -1, CInt(reader("HouseholdId")))
                    End If
                End Using
            End Using

            Using cmd As New MySqlCommand(
                "SELECT EducationLevel, EducationStatus " &
                "FROM education " &
                "WHERE ResidentID = @ResidentID LIMIT 1", connection)

                cmd.Parameters.AddWithValue("@ResidentID", residentId)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        data.EducationLevel = DBNullToString(reader("EducationLevel"))
                        data.EducationalStatus = DBNullToString(reader("EducationStatus"))
                    End If
                End Using
            End Using

            Using cmd As New MySqlCommand(
                "SELECT CategoryId, AdditionalInfo " &
                "FROM residentcategory " &
                "WHERE ResidentId = @ResidentId", connection)

                cmd.Parameters.AddWithValue("@ResidentId", residentId)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    data.SelectedCategoryIds = New List(Of Integer)()

                    While reader.Read()
                        data.SelectedCategoryIds.Add(CInt(reader("CategoryId")))

                        If String.IsNullOrWhiteSpace(data.AdditionalInfo) AndAlso Not IsDBNull(reader("AdditionalInfo")) Then
                            data.AdditionalInfo = reader("AdditionalInfo").ToString()
                        End If
                    End While
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetResidentById Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return data
    End Function

    Public Function ValidateResidentData(data As ResidentData) As ResidentOperationResult
        Dim result As New ResidentOperationResult()

        If String.IsNullOrWhiteSpace(data.FirstName) Then
            result.Message = "First name is required."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(data.LastName) Then
            result.Message = "Last name is required."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(data.Sex) Then
            result.Message = "Sex is required."
            result.ErrorCode = 1
            Return result
        End If

        If data.HouseholdId <= 0 Then
            result.Message = "Please select a valid household."
            result.ErrorCode = 1
            Return result
        End If

        result.IsSuccess = True
        result.Message = "Validation passed."
        result.ErrorCode = 0
        Return result
    End Function

    Public Function AddResident(data As ResidentData) As ResidentOperationResult
        Dim result As New ResidentOperationResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            Dim validationResult As ResidentOperationResult = ValidateResidentData(data)
            If Not validationResult.IsSuccess Then
                Return validationResult
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to the database."
                result.ErrorCode = 3
                Return result
            End If

            transaction = connection.BeginTransaction()

            Try
                Dim residentId As Long

                Dim insertResidentSql As String =
                    "INSERT INTO residents " &
                    "(FirstName, LastName, MiddleName, Suffix, DateOfBirth, PlaceOfBirth, " &
                    "Sex, CivilStatus, Religion, Citizenship, Occupation, ContactNumber, " &
                    "EmailAddress, Voter, HouseholdId, Is_Archived) " &
                    "VALUES " &
                    "(@FirstName, @LastName, @MiddleName, @Suffix, @DateOfBirth, @PlaceOfBirth, " &
                    "@Sex, @CivilStatus, @Religion, @Citizenship, @Occupation, @ContactNumber, " &
                    "@EmailAddress, @Voter, @HouseholdId, 0)"

                Using cmd As New MySqlCommand(insertResidentSql, connection, transaction)
                    AddResidentParameters(cmd, data)
                    cmd.ExecuteNonQuery()
                    residentId = cmd.LastInsertedId
                End Using

                SaveEducation(connection, transaction, CInt(residentId), data)
                SaveCategoriesByIds(connection, transaction, CInt(residentId), data)
                SaveFamilyHeadIfNeeded(connection, transaction, CInt(residentId), data)

                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("ResidentAdding_Form", "ADD RESIDENT",
                    LogInForm.CurrentUsername & " Added new resident: " & data.FirstName & " " & data.LastName,
                    connection, transaction)

                transaction.Commit()

                result.IsSuccess = True
                result.Message = "Resident added successfully."
                result.ErrorCode = 0
                result.ResidentId = CInt(residentId)

            Catch ex As Exception
                If transaction IsNot Nothing Then transaction.Rollback()

                result.Message = "Error adding resident: " & ex.Message
                result.ErrorCode = 3
                Debug.WriteLine("AddResident transaction error: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("AddResident error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    Public Function UpdateResident(data As ResidentData) As ResidentOperationResult
        Dim result As New ResidentOperationResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            Dim validationResult As ResidentOperationResult = ValidateResidentData(data)
            If Not validationResult.IsSuccess Then
                Return validationResult
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to the database."
                result.ErrorCode = 3
                Return result
            End If

            transaction = connection.BeginTransaction()

            Try
                Dim updateResidentSql As String =
                    "UPDATE residents SET " &
                    "FirstName = @FirstName, " &
                    "LastName = @LastName, " &
                    "MiddleName = @MiddleName, " &
                    "Suffix = @Suffix, " &
                    "DateOfBirth = @DateOfBirth, " &
                    "PlaceOfBirth = @PlaceOfBirth, " &
                    "Sex = @Sex, " &
                    "CivilStatus = @CivilStatus, " &
                    "Religion = @Religion, " &
                    "Citizenship = @Citizenship, " &
                    "Occupation = @Occupation, " &
                    "ContactNumber = @ContactNumber, " &
                    "EmailAddress = @EmailAddress, " &
                    "Voter = @Voter, " &
                    "HouseholdId = @HouseholdId " &
                    "WHERE ResidentId = @ResidentId"

                Using cmd As New MySqlCommand(updateResidentSql, connection, transaction)
                    AddResidentParameters(cmd, data)
                    cmd.Parameters.AddWithValue("@ResidentId", data.ResidentId)
                    cmd.ExecuteNonQuery()
                End Using

                SaveEducation(connection, transaction, data.ResidentId, data)

                Using deleteCmd As New MySqlCommand(
                    "DELETE FROM residentcategory WHERE ResidentId = @ResidentId",
                    connection, transaction)

                    deleteCmd.Parameters.AddWithValue("@ResidentId", data.ResidentId)
                    deleteCmd.ExecuteNonQuery()
                End Using

                SaveCategoriesByIds(connection, transaction, data.ResidentId, data)
                SaveFamilyHeadIfNeeded(connection, transaction, data.ResidentId, data)

                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("ResidentAdding_Form", "UPDATE RESIDENT",
                    LogInForm.CurrentUsername & " updated resident record (ID: " & data.ResidentId & "): " & data.FirstName & " " & data.LastName,
                    connection, transaction)

                transaction.Commit()


                result.IsSuccess = True
                result.Message = "Resident updated successfully."
                result.ErrorCode = 0
                result.ResidentId = data.ResidentId

            Catch ex As Exception
                If transaction IsNot Nothing Then transaction.Rollback()

                result.Message = "Error updating resident: " & ex.Message
                result.ErrorCode = 3
                Debug.WriteLine("UpdateResident transaction error: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("UpdateResident error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    Private Sub AddResidentParameters(cmd As MySqlCommand, data As ResidentData)
        cmd.Parameters.AddWithValue("@FirstName", data.FirstName.Trim())
        cmd.Parameters.AddWithValue("@LastName", data.LastName.Trim())
        cmd.Parameters.AddWithValue("@MiddleName", NullIfBlank(data.MiddleName))
        cmd.Parameters.AddWithValue("@Suffix", NullIfBlank(data.Suffix))
        cmd.Parameters.AddWithValue("@DateOfBirth", data.DateOfBirth)
        cmd.Parameters.AddWithValue("@PlaceOfBirth", NullIfBlank(data.PlaceOfBirth))
        cmd.Parameters.AddWithValue("@Sex", data.Sex.Trim())
        cmd.Parameters.AddWithValue("@CivilStatus", NullIfBlank(data.CivilStatus))
        cmd.Parameters.AddWithValue("@Religion", NullIfBlank(data.Religion))
        cmd.Parameters.AddWithValue("@Citizenship", NullIfBlank(data.Citizenship))
        cmd.Parameters.AddWithValue("@Occupation", NullIfBlank(data.Occupation))
        cmd.Parameters.AddWithValue("@ContactNumber", NullIfBlank(data.ContactNumber))
        cmd.Parameters.AddWithValue("@EmailAddress", NullIfBlank(data.EmailAddress))
        cmd.Parameters.AddWithValue("@Voter", data.Voter)
        cmd.Parameters.AddWithValue("@HouseholdId", data.HouseholdId)
    End Sub

    Private Sub SaveEducation(connection As MySqlConnection,
                              transaction As MySqlTransaction,
                              residentId As Integer,
                              data As ResidentData)

        Dim educationExists As Integer

        Using checkCmd As New MySqlCommand(
            "SELECT COUNT(*) FROM education WHERE ResidentID = @ResidentID",
            connection, transaction)

            checkCmd.Parameters.AddWithValue("@ResidentID", residentId)
            educationExists = CInt(checkCmd.ExecuteScalar())
        End Using

        If educationExists > 0 Then
            Using cmd As New MySqlCommand(
                "UPDATE education " &
                "SET EducationLevel = @EducationLevel, EducationStatus = @EducationStatus " &
                "WHERE ResidentID = @ResidentID",
                connection, transaction)

                cmd.Parameters.AddWithValue("@EducationLevel", NullIfBlank(data.EducationLevel))
                cmd.Parameters.AddWithValue("@EducationStatus", NullIfBlank(data.EducationalStatus))
                cmd.Parameters.AddWithValue("@ResidentID", residentId)
                cmd.ExecuteNonQuery()
            End Using
        Else
            Using cmd As New MySqlCommand(
                "INSERT INTO education (ResidentID, EducationLevel, EducationStatus) " &
                "VALUES (@ResidentID, @EducationLevel, @EducationStatus)",
                connection, transaction)

                cmd.Parameters.AddWithValue("@ResidentID", residentId)
                cmd.Parameters.AddWithValue("@EducationLevel", NullIfBlank(data.EducationLevel))
                cmd.Parameters.AddWithValue("@EducationStatus", NullIfBlank(data.EducationalStatus))
                cmd.ExecuteNonQuery()
            End Using
        End If
    End Sub

    Private Sub SaveCategoriesByIds(connection As MySqlConnection,
                                    transaction As MySqlTransaction,
                                    residentId As Integer,
                                    data As ResidentData)

        If data.SelectedCategoryIds Is Nothing OrElse data.SelectedCategoryIds.Count = 0 Then
            Return
        End If

        Dim insertSql As String =
            "INSERT INTO residentcategory (ResidentId, CategoryId, AdditionalInfo) " &
            "VALUES (@ResidentId, @CategoryId, @AdditionalInfo)"

        For Each categoryId As Integer In data.SelectedCategoryIds
            Using cmd As New MySqlCommand(insertSql, connection, transaction)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)
                cmd.Parameters.AddWithValue("@CategoryId", categoryId)
                cmd.Parameters.AddWithValue("@AdditionalInfo", NullIfBlank(data.AdditionalInfo))
                cmd.ExecuteNonQuery()
            End Using
        Next
    End Sub

    Private Sub SaveFamilyHeadIfNeeded(connection As MySqlConnection,
                                       transaction As MySqlTransaction,
                                       residentId As Integer,
                                       data As ResidentData)

        Dim isHead As Boolean = IsHeadCategorySelected(connection, transaction, data.SelectedCategoryIds)

        If Not isHead Then
            Using deleteCmd As New MySqlCommand(
                "DELETE FROM familyhead WHERE ResidentId = @ResidentId",
                connection, transaction)

                deleteCmd.Parameters.AddWithValue("@ResidentId", residentId)
                deleteCmd.ExecuteNonQuery()
            End Using

            Return
        End If

        Dim existingCount As Integer

        Using checkCmd As New MySqlCommand(
            "SELECT COUNT(*) FROM familyhead WHERE ResidentId = @ResidentId",
            connection, transaction)

            checkCmd.Parameters.AddWithValue("@ResidentId", residentId)
            existingCount = CInt(checkCmd.ExecuteScalar())
        End Using

        If existingCount > 0 Then
            Using updateCmd As New MySqlCommand(
                "UPDATE familyhead SET FamilyName = @FamilyName WHERE ResidentId = @ResidentId",
                connection, transaction)

                updateCmd.Parameters.AddWithValue("@FamilyName", data.LastName.Trim())
                updateCmd.Parameters.AddWithValue("@ResidentId", residentId)
                updateCmd.ExecuteNonQuery()
            End Using
        Else
            Using insertCmd As New MySqlCommand(
                "INSERT INTO familyhead (ResidentId, FamilyName) VALUES (@ResidentId, @FamilyName)",
                connection, transaction)

                insertCmd.Parameters.AddWithValue("@ResidentId", residentId)
                insertCmd.Parameters.AddWithValue("@FamilyName", data.LastName.Trim())
                insertCmd.ExecuteNonQuery()
            End Using
        End If
    End Sub

    Private Function IsHeadCategorySelected(connection As MySqlConnection,
                                            transaction As MySqlTransaction,
                                            selectedCategoryIds As List(Of Integer)) As Boolean

        If selectedCategoryIds Is Nothing OrElse selectedCategoryIds.Count = 0 Then
            Return False
        End If

        Dim query As String =
            "SELECT COUNT(*) " &
            "FROM categories " &
            "WHERE CategoryId = @CategoryId " &
            "AND LOWER(Category) IN ('head', 'family head', 'family heads')"

        For Each categoryId As Integer In selectedCategoryIds
            Using cmd As New MySqlCommand(query, connection, transaction)
                cmd.Parameters.AddWithValue("@CategoryId", categoryId)

                If CInt(cmd.ExecuteScalar()) > 0 Then
                    Return True
                End If
            End Using
        Next

        Return False
    End Function

    Private Function NullIfBlank(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If

        Return value.Trim()
    End Function

    Private Function DBNullToString(value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then
            Return ""
        End If

        Return value.ToString()
    End Function

End Class