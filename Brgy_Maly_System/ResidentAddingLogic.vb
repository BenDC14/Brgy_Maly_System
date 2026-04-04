Imports MySql.Data.MySqlClient

''' <summary>
''' Resident Adding Logic - Handles resident add/update business logic
''' Separated from UI form logic for reusability and maintainability
''' DATABASE: barangay_maly
''' </summary>
Public Class ResidentAddingLogic

    ''' <summary>
    ''' Result class for resident operations
    ''' </summary>
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

    ''' <summary>
    ''' Resident Data Class
    ''' </summary>
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
        Public Property Categories As List(Of String) ' List of category names selected
    End Class

    ''' <summary>
    ''' Get all households for dropdown/combobox
    ''' </summary>
    Public Function GetAllHouseholds() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT h.HouseholdID, h.HouseholdNumber, " &
                                 "CONCAT(a.BlockNumber, ' Block, ', a.LotNumber, ' Lot, ', a.StreetName, ', ', " &
                                 "a.Barangay, ', ', a.Municipality, ', ', a.Province) AS FullAddress " &
                                 "FROM household h " &
                                 "INNER JOIN address a ON h.AddressID = a.AddressID " &
                                 "ORDER BY h.HouseholdNumber"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("Error getting households: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Get household address info by household ID
    ''' </summary>
    Public Function GetHouseholdAddress(householdId As Integer) As String
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return ""

            Dim query As String = "SELECT CONCAT(a.BlockNumber, ' Block, ', a.LotNumber, ' Lot, ', a.StreetName, ', ', " &
                                 "a.Barangay, ', ', a.Municipality, ', ', a.Province) AS FullAddress " &
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
            Debug.WriteLine("Error getting household address: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return ""
    End Function

    ''' <summary>
    ''' Get resident by ID with full details
    ''' </summary>
    Public Function GetResidentById(residentId As Integer) As ResidentData
        Dim data As New ResidentData()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return data

            Dim query As String = "SELECT * FROM residents WHERE ResidentId = @ResidentId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        data.ResidentId = CInt(reader("ResidentId"))
                        data.FirstName = If(IsDBNull(reader("FirstName")), "", reader("FirstName").ToString())
                        data.LastName = If(IsDBNull(reader("LastName")), "", reader("LastName").ToString())
                        data.MiddleName = If(IsDBNull(reader("MiddleName")), "", reader("MiddleName").ToString())
                        data.Suffix = If(IsDBNull(reader("Suffix")), "", reader("Suffix").ToString())
                        data.DateOfBirth = If(IsDBNull(reader("DateOfBirth")), Today, CDate(reader("DateOfBirth")))
                        data.PlaceOfBirth = If(IsDBNull(reader("PlaceOfBirth")), "", reader("PlaceOfBirth").ToString())
                        data.Sex = If(IsDBNull(reader("Sex")), "", reader("Sex").ToString())
                        data.CivilStatus = If(IsDBNull(reader("CivilStatus")), "", reader("CivilStatus").ToString())
                        data.Religion = If(IsDBNull(reader("Religion")), "", reader("Religion").ToString())
                        data.Citizenship = If(IsDBNull(reader("Citizenship")), "", reader("Citizenship").ToString())
                        data.Occupation = If(IsDBNull(reader("Occupation")), "", reader("Occupation").ToString())
                        data.ContactNumber = If(IsDBNull(reader("ContactNumber")), "", reader("ContactNumber").ToString())
                        data.EmailAddress = If(IsDBNull(reader("EmailAddress")), "", reader("EmailAddress").ToString())
                        data.Voter = If(IsDBNull(reader("Voter")), False, CBool(reader("Voter")))
                        data.HouseholdId = If(IsDBNull(reader("HouseholdId")), -1, CInt(reader("HouseholdId")))
                    End If
                End Using
            End Using

            ' === GET EDUCATION INFO ===
            GetEducationInfo(residentId, data, connection)

        Catch ex As Exception
            Debug.WriteLine("Error getting resident by ID: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return data
    End Function

    ''' <summary>
    ''' Get education information for resident
    ''' </summary>
    Private Sub GetEducationInfo(residentId As Integer, data As ResidentData, connection As MySqlConnection)
        Try
            Dim query As String = "SELECT EducationLevel, EducationStatus FROM education WHERE ResidentID = @ResidentID LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ResidentID", residentId)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        data.EducationLevel = If(IsDBNull(reader("EducationLevel")), "", reader("EducationLevel").ToString())
                        data.EducationalStatus = If(IsDBNull(reader("EducationStatus")), "", reader("EducationStatus").ToString())
                    End If
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("Error getting education info: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Validate resident data
    ''' </summary>
    Public Function ValidateResidentData(data As ResidentData) As ResidentOperationResult
        Dim result As New ResidentOperationResult()

        ' === REQUIRED FIELDS ===
        If String.IsNullOrWhiteSpace(data.FirstName) Then
            result.Message = "First Name is required."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(data.LastName) Then
            result.Message = "Last Name is required."
            result.ErrorCode = 1
            Return result
        End If

        If String.IsNullOrWhiteSpace(data.Sex) Then
            result.Message = "Gender/Sex is required."
            result.ErrorCode = 1
            Return result
        End If

        If data.HouseholdId <= 0 Then
            result.Message = "Household is required."
            result.ErrorCode = 1
            Return result
        End If

        result.IsSuccess = True
        result.Message = "Validation passed."
        result.ErrorCode = 0
        Return result
    End Function

    ''' <summary>
    ''' Add new resident to database
    ''' </summary>
    Public Function AddResident(data As ResidentData) As ResidentOperationResult
        Dim result As New ResidentOperationResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATE ===
            Dim validationResult = ValidateResidentData(data)
            If Not validationResult.IsSuccess Then
                Return validationResult
            End If

            ' === GET CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            connection.Open()
            Dim transaction As MySqlTransaction = connection.BeginTransaction()

            Try
                ' === INSERT INTO RESIDENTS TABLE ===
                Dim residentsQuery As String = "INSERT INTO residents (FirstName, LastName, MiddleName, Suffix, DateOfBirth, " &
                                              "PlaceOfBirth, Sex, CivilStatus, Religion, Citizenship, Occupation, ContactNumber, " &
                                              "EmailAddress, Voter, HouseholdId, Is_Archived) " &
                                              "VALUES (@FirstName, @LastName, @MiddleName, @Suffix, @DateOfBirth, " &
                                              "@PlaceOfBirth, @Sex, @CivilStatus, @Religion, @Citizenship, @Occupation, " &
                                              "@ContactNumber, @EmailAddress, @Voter, @HouseholdId, 0)"

                Dim residentId As Long = 0

                Using residentsCmd As New MySqlCommand(residentsQuery, connection, transaction)
                    residentsCmd.Parameters.AddWithValue("@FirstName", data.FirstName)
                    residentsCmd.Parameters.AddWithValue("@LastName", data.LastName)
                    residentsCmd.Parameters.AddWithValue("@MiddleName", If(String.IsNullOrEmpty(data.MiddleName), DBNull.Value, data.MiddleName))
                    residentsCmd.Parameters.AddWithValue("@Suffix", If(String.IsNullOrEmpty(data.Suffix), DBNull.Value, data.Suffix))
                    residentsCmd.Parameters.AddWithValue("@DateOfBirth", data.DateOfBirth)
                    residentsCmd.Parameters.AddWithValue("@PlaceOfBirth", If(String.IsNullOrEmpty(data.PlaceOfBirth), DBNull.Value, data.PlaceOfBirth))
                    residentsCmd.Parameters.AddWithValue("@Sex", data.Sex)
                    residentsCmd.Parameters.AddWithValue("@CivilStatus", If(String.IsNullOrEmpty(data.CivilStatus), DBNull.Value, data.CivilStatus))
                    residentsCmd.Parameters.AddWithValue("@Religion", If(String.IsNullOrEmpty(data.Religion), DBNull.Value, data.Religion))
                    residentsCmd.Parameters.AddWithValue("@Citizenship", If(String.IsNullOrEmpty(data.Citizenship), DBNull.Value, data.Citizenship))
                    residentsCmd.Parameters.AddWithValue("@Occupation", If(String.IsNullOrEmpty(data.Occupation), DBNull.Value, data.Occupation))
                    residentsCmd.Parameters.AddWithValue("@ContactNumber", If(String.IsNullOrEmpty(data.ContactNumber), DBNull.Value, data.ContactNumber))
                    residentsCmd.Parameters.AddWithValue("@EmailAddress", If(String.IsNullOrEmpty(data.EmailAddress), DBNull.Value, data.EmailAddress))
                    residentsCmd.Parameters.AddWithValue("@Voter", data.Voter)
                    residentsCmd.Parameters.AddWithValue("@HouseholdId", data.HouseholdId)

                    residentsCmd.ExecuteNonQuery()
                    residentId = residentsCmd.LastInsertedId
                End Using

                ' === INSERT INTO EDUCATION TABLE ===
                If Not String.IsNullOrEmpty(data.EducationLevel) OrElse Not String.IsNullOrEmpty(data.EducationalStatus) Then
                    Dim educationQuery As String = "INSERT INTO education (ResidentID, EducationLevel, EducationStatus) " &
                                                  "VALUES (@ResidentID, @EducationLevel, @EducationStatus)"

                    Using educationCmd As New MySqlCommand(educationQuery, connection, transaction)
                        educationCmd.Parameters.AddWithValue("@ResidentID", residentId)
                        educationCmd.Parameters.AddWithValue("@EducationLevel", If(String.IsNullOrEmpty(data.EducationLevel), DBNull.Value, data.EducationLevel))
                        educationCmd.Parameters.AddWithValue("@EducationStatus", If(String.IsNullOrEmpty(data.EducationalStatus), DBNull.Value, data.EducationalStatus))
                        educationCmd.ExecuteNonQuery()
                    End Using
                End If

                ' === INSERT CATEGORIES ===
                If data.Categories IsNot Nothing AndAlso data.Categories.Count > 0 Then
                    For Each categoryName In data.Categories
                        Dim getCategoryQuery As String = "SELECT CategoryId FROM categories WHERE Category = @Category LIMIT 1"
                        Dim categoryId As Integer = -1

                        Using getCatCmd As New MySqlCommand(getCategoryQuery, connection, transaction)
                            getCatCmd.Parameters.AddWithValue("@Category", categoryName)
                            Dim result_cat = getCatCmd.ExecuteScalar()
                            If result_cat IsNot Nothing Then
                                categoryId = CInt(result_cat)
                            End If
                        End Using

                        If categoryId > 0 Then
                            Dim insertCategoryQuery As String = "INSERT INTO residentcategory (ResidentId, CategoryId) VALUES (@ResidentId, @CategoryId)"
                            Using insertCatCmd As New MySqlCommand(insertCategoryQuery, connection, transaction)
                                insertCatCmd.Parameters.AddWithValue("@ResidentId", residentId)
                                insertCatCmd.Parameters.AddWithValue("@CategoryId", categoryId)
                                insertCatCmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next
                End If

                transaction.Commit()
                result.IsSuccess = True
                result.Message = "Resident added successfully."
                result.ErrorCode = 0
                result.ResidentId = CInt(residentId)

            Catch ex As Exception
                transaction.Rollback()
                result.Message = "Error adding resident: " & ex.Message
                result.ErrorCode = 3
                Debug.WriteLine("Error in transaction: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in AddResident: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Update existing resident
    ''' </summary>
    Public Function UpdateResident(data As ResidentData) As ResidentOperationResult
        Dim result As New ResidentOperationResult()
        Dim connection As MySqlConnection = Nothing

        Try
            ' === VALIDATE ===
            Dim validationResult = ValidateResidentData(data)
            If Not validationResult.IsSuccess Then
                Return validationResult
            End If

            ' === GET CONNECTION ===
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            connection.Open()
            Dim transaction As MySqlTransaction = connection.BeginTransaction()

            Try
                ' === UPDATE RESIDENTS TABLE ===
                Dim updateQuery As String = "UPDATE residents SET FirstName = @FirstName, LastName = @LastName, " &
                                           "MiddleName = @MiddleName, Suffix = @Suffix, DateOfBirth = @DateOfBirth, " &
                                           "PlaceOfBirth = @PlaceOfBirth, Sex = @Sex, CivilStatus = @CivilStatus, " &
                                           "Religion = @Religion, Citizenship = @Citizenship, Occupation = @Occupation, " &
                                           "ContactNumber = @ContactNumber, EmailAddress = @EmailAddress, Voter = @Voter, " &
                                           "HouseholdId = @HouseholdId WHERE ResidentId = @ResidentId"

                Using updateCmd As New MySqlCommand(updateQuery, connection, transaction)
                    updateCmd.Parameters.AddWithValue("@FirstName", data.FirstName)
                    updateCmd.Parameters.AddWithValue("@LastName", data.LastName)
                    updateCmd.Parameters.AddWithValue("@MiddleName", If(String.IsNullOrEmpty(data.MiddleName), DBNull.Value, data.MiddleName))
                    updateCmd.Parameters.AddWithValue("@Suffix", If(String.IsNullOrEmpty(data.Suffix), DBNull.Value, data.Suffix))
                    updateCmd.Parameters.AddWithValue("@DateOfBirth", data.DateOfBirth)
                    updateCmd.Parameters.AddWithValue("@PlaceOfBirth", If(String.IsNullOrEmpty(data.PlaceOfBirth), DBNull.Value, data.PlaceOfBirth))
                    updateCmd.Parameters.AddWithValue("@Sex", data.Sex)
                    updateCmd.Parameters.AddWithValue("@CivilStatus", If(String.IsNullOrEmpty(data.CivilStatus), DBNull.Value, data.CivilStatus))
                    updateCmd.Parameters.AddWithValue("@Religion", If(String.IsNullOrEmpty(data.Religion), DBNull.Value, data.Religion))
                    updateCmd.Parameters.AddWithValue("@Citizenship", If(String.IsNullOrEmpty(data.Citizenship), DBNull.Value, data.Citizenship))
                    updateCmd.Parameters.AddWithValue("@Occupation", If(String.IsNullOrEmpty(data.Occupation), DBNull.Value, data.Occupation))
                    updateCmd.Parameters.AddWithValue("@ContactNumber", If(String.IsNullOrEmpty(data.ContactNumber), DBNull.Value, data.ContactNumber))
                    updateCmd.Parameters.AddWithValue("@EmailAddress", If(String.IsNullOrEmpty(data.EmailAddress), DBNull.Value, data.EmailAddress))
                    updateCmd.Parameters.AddWithValue("@Voter", data.Voter)
                    updateCmd.Parameters.AddWithValue("@HouseholdId", data.HouseholdId)
                    updateCmd.Parameters.AddWithValue("@ResidentId", data.ResidentId)

                    updateCmd.ExecuteNonQuery()
                End Using

                ' === UPDATE EDUCATION TABLE ===
                Dim educationCheckQuery As String = "SELECT COUNT(*) FROM education WHERE ResidentID = @ResidentID"
                Dim educationExists As Integer = 0

                Using checkCmd As New MySqlCommand(educationCheckQuery, connection, transaction)
                    checkCmd.Parameters.AddWithValue("@ResidentID", data.ResidentId)
                    educationExists = CInt(checkCmd.ExecuteScalar())
                End Using

                If educationExists > 0 Then
                    Dim updateEducationQuery As String = "UPDATE education SET EducationLevel = @EducationLevel, " &
                                                        "EducationStatus = @EducationStatus WHERE ResidentID = @ResidentID"
                    Using educationCmd As New MySqlCommand(updateEducationQuery, connection, transaction)
                        educationCmd.Parameters.AddWithValue("@EducationLevel", If(String.IsNullOrEmpty(data.EducationLevel), DBNull.Value, data.EducationLevel))
                        educationCmd.Parameters.AddWithValue("@EducationStatus", If(String.IsNullOrEmpty(data.EducationalStatus), DBNull.Value, data.EducationalStatus))
                        educationCmd.Parameters.AddWithValue("@ResidentID", data.ResidentId)
                        educationCmd.ExecuteNonQuery()
                    End Using
                Else
                    Dim insertEducationQuery As String = "INSERT INTO education (ResidentID, EducationLevel, EducationStatus) " &
                                                        "VALUES (@ResidentID, @EducationLevel, @EducationStatus)"
                    Using educationCmd As New MySqlCommand(insertEducationQuery, connection, transaction)
                        educationCmd.Parameters.AddWithValue("@ResidentID", data.ResidentId)
                        educationCmd.Parameters.AddWithValue("@EducationLevel", If(String.IsNullOrEmpty(data.EducationLevel), DBNull.Value, data.EducationLevel))
                        educationCmd.Parameters.AddWithValue("@EducationStatus", If(String.IsNullOrEmpty(data.EducationalStatus), DBNull.Value, data.EducationalStatus))
                        educationCmd.ExecuteNonQuery()
                    End Using
                End If

                transaction.Commit()
                result.IsSuccess = True
                result.Message = "Resident updated successfully."
                result.ErrorCode = 0
                result.ResidentId = data.ResidentId

            Catch ex As Exception
                transaction.Rollback()
                result.Message = "Error updating resident: " & ex.Message
                result.ErrorCode = 3
                Debug.WriteLine("Error in transaction: " & ex.Message)
            End Try

        Catch ex As Exception
            result.Message = "Error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("Error in UpdateResident: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

End Class
