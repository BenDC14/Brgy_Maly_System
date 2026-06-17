Imports MySql.Data.MySqlClient

''' <summary>
''' Business logic for AyudaAdd_Form.
''' This file handles database operations for adding, editing, loading,
''' and searching ayuda programs from the barangayaid table.
''' </summary>
Public Class AyudaAddLogic

    ''' <summary>
    ''' Standard result object used by AddAyuda and UpdateAyuda.
    ''' This lets the form know if the operation succeeded and what message to show.
    ''' </summary>
    Public Class AyudaResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public Property ErrorCode As Integer
        Public Property AidId As Integer

        Public Sub New()
            IsSuccess = False
            Message = ""
            ErrorCode = 0
            AidId = -1
        End Sub
    End Class

    ''' <summary>
    ''' Container for ayuda information from the form.
    ''' This is passed from AyudaAdd_Form into the database logic.
    ''' </summary>
    Public Class AyudaData
        Public Property AidId As Integer

        ' This is the selected resident category for the ayuda.
        ' It will be saved into barangayaid.CategoryId.
        Public Property CategoryId As Integer

        Public Property source_agency As String
        Public Property program_title As String
        Public Property assistance_type As String
        Public Property provision_details As String
        Public Property max_slots As Integer
        Public Property start_date As Date
        Public Property end_date As Date
        Public Property is_active As Boolean
    End Class

    ''' <summary>
    ''' Loads all categories from the categories table.
    ''' These values are used as the DataSource of cbcategories.
    ''' </summary>
    Public Function GetAllCategories() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String = "SELECT CategoryId, Category FROM categories ORDER BY Category"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAllCategories Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Loads all ayuda records.
    ''' LEFT JOIN is used so the grid can display the category name from categories.Category.
    ''' </summary>
    Public Function GetAllAyudas() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT ba.AidId, ba.CategoryId, c.Category, ba.source_agency, " &
                "ba.program_title, ba.assistance_type, ba.provision_details, " &
                "ba.max_slots, ba.start_date, ba.end_date, ba.is_active " &
                "FROM barangayaid ba " &
                "LEFT JOIN categories c ON ba.CategoryId = c.CategoryId " &
                "ORDER BY ba.program_title"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAllAyudas Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Searches ayuda records by program, giver, assistance type, or category name.
    ''' </summary>
    Public Function SearchAyudas(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT ba.AidId, ba.CategoryId, c.Category, ba.source_agency, " &
                "ba.program_title, ba.assistance_type, ba.provision_details, " &
                "ba.max_slots, ba.start_date, ba.end_date, ba.is_active " &
                "FROM barangayaid ba " &
                "LEFT JOIN categories c ON ba.CategoryId = c.CategoryId " &
                "WHERE ba.program_title LIKE @Search " &
                "OR ba.source_agency LIKE @Search " &
                "OR ba.assistance_type LIKE @Search " &
                "OR c.Category LIKE @Search " &
                "ORDER BY ba.program_title"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Search", "%" & searchTerm & "%")

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("SearchAyudas Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Validates ayuda data before saving or updating.
    ''' This prevents incomplete or invalid records from being saved.
    ''' </summary>
    Public Function ValidateAyudaData(data As AyudaData) As AyudaResult
        Dim result As New AyudaResult()

        Try
            If data.CategoryId <= 0 Then
                result.Message = "Resident Category is required."
                result.ErrorCode = 1
                Return result
            End If

            If String.IsNullOrWhiteSpace(data.source_agency) Then
                result.Message = "Source Agency / Giver is required."
                result.ErrorCode = 1
                Return result
            End If

            If String.IsNullOrWhiteSpace(data.program_title) Then
                result.Message = "Program Name is required."
                result.ErrorCode = 1
                Return result
            End If

            If String.IsNullOrWhiteSpace(data.assistance_type) Then
                result.Message = "Assistance Type is required."
                result.ErrorCode = 1
                Return result
            End If

            If String.IsNullOrWhiteSpace(data.provision_details) Then
                result.Message = "Assistance Provision is required."
                result.ErrorCode = 1
                Return result
            End If

            If data.max_slots <= 0 Then
                result.Message = "Target Slots must be greater than 0."
                result.ErrorCode = 1
                Return result
            End If

            If data.start_date.Date > data.end_date.Date Then
                result.Message = "Term Start must not be later than Term End."
                result.ErrorCode = 1
                Return result
            End If

            result.IsSuccess = True
            result.Message = "Validation passed."
            result.ErrorCode = 0

        Catch ex As Exception
            result.Message = "Validation error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("ValidateAyudaData Error: " & ex.Message)
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Checks if a program name already exists.
    ''' excludedAidId is used during editing so the current record does not count as duplicate.
    ''' </summary>
    Public Function DoesProgramTitleExist(programTitle As String, Optional excludedAidId As Integer = -1) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim query As String =
                "SELECT COUNT(*) FROM barangayaid " &
                "WHERE program_title = @ProgramTitle " &
                "AND (@ExcludedAidId <= 0 OR AidId <> @ExcludedAidId)"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ProgramTitle", programTitle)
                cmd.Parameters.AddWithValue("@ExcludedAidId", excludedAidId)

                Dim count As Integer = CInt(cmd.ExecuteScalar())
                Return count > 0
            End Using

        Catch ex As Exception
            Debug.WriteLine("DoesProgramTitleExist Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Inserts a new ayuda program into barangayaid.
    ''' CategoryId is saved so the ayuda is linked to the selected resident category.
    ''' </summary>
    Public Function AddAyuda(data As AyudaData) As AyudaResult
        Dim result As New AyudaResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing
        Try
            Dim validationResult As AyudaResult = ValidateAyudaData(data)
            If Not validationResult.IsSuccess Then Return validationResult

            If DoesProgramTitleExist(data.program_title) Then
                result.Message = "Program Name already exists. Please use a different name."
                result.ErrorCode = 2
                Return result
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            Dim insertQuery As String =
                "INSERT INTO barangayaid " &
                "(source_agency, program_title, assistance_type, provision_details, max_slots, start_date, end_date, is_active, CategoryId) " &
                "VALUES " &
                "(@SourceAgency, @ProgramTitle, @AssistanceType, @ProvisionDetails, @MaxSlots, @StartDate, @EndDate, @IsActive, @CategoryId)"

            Using cmd As New MySqlCommand(insertQuery, connection)
                cmd.Parameters.AddWithValue("@SourceAgency", data.source_agency)
                cmd.Parameters.AddWithValue("@ProgramTitle", data.program_title)
                cmd.Parameters.AddWithValue("@AssistanceType", data.assistance_type)
                cmd.Parameters.AddWithValue("@ProvisionDetails", data.provision_details)
                cmd.Parameters.AddWithValue("@MaxSlots", data.max_slots)
                cmd.Parameters.AddWithValue("@StartDate", data.start_date.Date)
                cmd.Parameters.AddWithValue("@EndDate", data.end_date.Date)
                cmd.Parameters.AddWithValue("@IsActive", If(data.is_active, 1, 0))
                cmd.Parameters.AddWithValue("@CategoryId", data.CategoryId)

                cmd.ExecuteNonQuery()
                result.AidId = CInt(cmd.LastInsertedId)
            End Using

            result.IsSuccess = True
            result.Message = "Ayuda program added successfully."
            result.ErrorCode = 0

            ' === LOG AUDIT TRAIL ===
            GlobalAuditLogger.Log("AyudaAdd_Form", "ADD AYUDA PROGRAM",
                    LogInForm.CurrentUsername & " created new ayuda program: " & data.program_title,
                    connection, transaction)


        Catch ex As MySqlException
            result.Message = "Database error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("AddAyuda MySQL Error: " & ex.Message)
        Catch ex As Exception
            result.Message = "Error adding ayuda: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("AddAyuda Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Updates an existing ayuda program.
    ''' This also updates CategoryId if the user changes the selected resident category.
    ''' </summary>
    Public Function UpdateAyuda(data As AyudaData) As AyudaResult
        Dim result As New AyudaResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            Dim validationResult As AyudaResult = ValidateAyudaData(data)
            If Not validationResult.IsSuccess Then Return validationResult

            If data.AidId <= 0 Then
                result.Message = "Invalid ayuda selected."
                result.ErrorCode = 1
                Return result
            End If

            If DoesProgramTitleExist(data.program_title, data.AidId) Then
                result.Message = "Program Name already exists. Please use a different name."
                result.ErrorCode = 2
                Return result
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                result.Message = "Unable to connect to database."
                result.ErrorCode = 3
                Return result
            End If

            Dim updateQuery As String =
                "UPDATE barangayaid SET " &
                "source_agency = @SourceAgency, " &
                "program_title = @ProgramTitle, " &
                "assistance_type = @AssistanceType, " &
                "provision_details = @ProvisionDetails, " &
                "max_slots = @MaxSlots, " &
                "start_date = @StartDate, " &
                "end_date = @EndDate, " &
                "is_active = @IsActive, " &
                "CategoryId = @CategoryId " &
                "WHERE AidId = @AidId"

            Using cmd As New MySqlCommand(updateQuery, connection)
                cmd.Parameters.AddWithValue("@SourceAgency", data.source_agency)
                cmd.Parameters.AddWithValue("@ProgramTitle", data.program_title)
                cmd.Parameters.AddWithValue("@AssistanceType", data.assistance_type)
                cmd.Parameters.AddWithValue("@ProvisionDetails", data.provision_details)
                cmd.Parameters.AddWithValue("@MaxSlots", data.max_slots)
                cmd.Parameters.AddWithValue("@StartDate", data.start_date.Date)
                cmd.Parameters.AddWithValue("@EndDate", data.end_date.Date)
                cmd.Parameters.AddWithValue("@IsActive", If(data.is_active, 1, 0))
                cmd.Parameters.AddWithValue("@CategoryId", data.CategoryId)
                cmd.Parameters.AddWithValue("@AidId", data.AidId)

                cmd.ExecuteNonQuery()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("AyudaAdd_Form", "UPDATE AYUDA PROGRAM",
                    LogInForm.CurrentUsername & " updated ayuda program (ID: " & data.AidId & "): " & data.program_title,
                    connection, transaction)

            End Using

            result.IsSuccess = True
            result.Message = "Ayuda program updated successfully."
            result.ErrorCode = 0
            result.AidId = data.AidId

        Catch ex As MySqlException
            result.Message = "Database error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("UpdateAyuda MySQL Error: " & ex.Message)
        Catch ex As Exception
            result.Message = "Error updating ayuda: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("UpdateAyuda Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

End Class
