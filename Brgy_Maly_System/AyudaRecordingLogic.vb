Imports MySql.Data.MySqlClient
Imports System.Data

''' <summary>
''' Business logic for AyudaRecording_Form.
''' Handles loading categories, loading ayudas by category,
''' loading qualified residents, and recording ayuda claims.
''' </summary>
Public Class AyudaRecordingLogic

    Public Class RecordingResult
        Public Property IsSuccess As Boolean
        Public Property Message As String
        Public Property ErrorCode As Integer
        Public Property RecordId As Integer

        Public Sub New()
            IsSuccess = False
            Message = ""
            ErrorCode = 0
            RecordId = -1
        End Sub
    End Class

    Public Class ResidentRecordingData
        Public Property ResidentAidId As Integer
        Public Property ResidentId As Integer
        Public Property AidId As Integer
        Public Property AidDate As Date
        Public Property Quantity As Integer
        Public Property Description As String
    End Class

    ''' <summary>
    ''' Loads all resident categories.
    ''' These are shown in cbResidentType.
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
    ''' Gets the category name using CategoryId.
    ''' Used to display the selected resident type in txtResidentType.
    ''' </summary>
    Public Function GetCategoryNameById(categoryId As Integer) As String
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return ""

            Dim query As String = "SELECT Category FROM categories WHERE CategoryId = @CategoryId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@CategoryId", categoryId)

                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return result.ToString()
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetCategoryNameById Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return ""
    End Function

    ''' <summary>
    ''' Loads only available ayuda programs for the selected resident category.
    ''' This reduces cbAyuda choices after the resident type is selected.
    ''' Rules:
    ''' - CategoryId must match selected resident type.
    ''' - Ayuda must be active.
    ''' - Ayuda must not be expired.
    ''' - UPDATED: Removed max_slots > 0 so user can see missing residents even if full.
    ''' </summary>
    Public Function GetAyudaProgramsByCategory(categoryId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            ' MODIFIED: Removed "AND max_slots > 0"
            Dim query As String =
                "SELECT AidId, program_title, source_agency, assistance_type, " &
                "provision_details, start_date, end_date, max_slots, CategoryId, " &
                "CONCAT(program_title, ' (Slots: ', max_slots, ')') AS DisplayText " &
                "FROM barangayaid " &
                "WHERE CategoryId = @CategoryId " &
                "AND is_active = 1 " &
                "AND end_date >= CURDATE() " &
                "ORDER BY start_date DESC"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@CategoryId", categoryId)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAyudaProgramsByCategory Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Loads residents under the selected category who have not claimed the selected ayuda.
    ''' This is called only after resident type and ayuda are both selected.
    ''' </summary>
    Public Function GetResidentsByCategory(categoryId As Integer, aidId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT DISTINCT " &
                "r.ResidentId, " &
                "CONCAT(r.FirstName, ' ', r.LastName) AS FullName, " &
                "r.ContactNumber, " &
                "c.Category " &
                "FROM residents r " &
                "INNER JOIN residentcategory rc ON r.ResidentId = rc.ResidentId " &
                "INNER JOIN categories c ON rc.CategoryId = c.CategoryId " &
                "WHERE rc.CategoryId = @CategoryId " &
                "AND IFNULL(r.Is_Archived, 0) = 0 " &
                "AND NOT EXISTS ( " &
                "    SELECT 1 FROM residentaid ra " &
                "    WHERE ra.ResidentId = r.ResidentId " &
                "    AND ra.AidId = @AidId " &
                ") " &
                "ORDER BY r.FirstName, r.LastName"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@CategoryId", categoryId)
                cmd.Parameters.AddWithValue("@AidId", aidId)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetResidentsByCategory Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Gets provision details from the selected ayuda.
    ''' This is shown in txtQuantity.
    ''' </summary>
    Public Function GetAyudaProvisionDetails(aidId As Integer) As String
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return ""

            Dim query As String = "SELECT provision_details FROM barangayaid WHERE AidId = @AidId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@AidId", aidId)

                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return result.ToString()
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAyudaProvisionDetails Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return ""
    End Function

    ''' <summary>
    ''' Extracts a numeric quantity from provision details.
    ''' Handles comma-formatted values like "5,000 pesos" correctly.
    ''' Examples:
    ''' "5,000 pesos" = 5000
    ''' "5000 pesos" = 5000
    ''' "10kg Rice" = 10
    ''' "Food Pack" = 1
    ''' </summary>
    Public Function ExtractQuantityFromProvisionDetails(provisionDetails As String) As Integer
        Try
            If String.IsNullOrWhiteSpace(provisionDetails) Then Return 1

            Dim numberString As String = ""

            For Each c As Char In provisionDetails
                If Char.IsDigit(c) Then
                    numberString &= c
                ElseIf c = ","c AndAlso numberString.Length > 0 Then
                    Continue For
                ElseIf numberString.Length > 0 Then
                    Exit For
                End If
            Next

            Dim quantity As Integer
            If Integer.TryParse(numberString, quantity) AndAlso quantity > 0 Then
                Return quantity
            End If

            Return 1

        Catch ex As Exception
            Debug.WriteLine("ExtractQuantityFromProvisionDetails Error: " & ex.Message)
            Return 1
        End Try
    End Function


    ''' <summary>
    ''' Checks if resident already claimed the selected ayuda.
    ''' </summary>
    Public Function HasResidentClaimedAyuda(residentId As Integer, aidId As Integer) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim query As String = "SELECT COUNT(*) FROM residentaid WHERE ResidentId = @ResidentId AND AidId = @AidId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)
                cmd.Parameters.AddWithValue("@AidId", aidId)

                Return CInt(cmd.ExecuteScalar()) > 0
            End Using

        Catch ex As Exception
            Debug.WriteLine("HasResidentClaimedAyuda Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Records a resident's ayuda claim.
    ''' Uses a secure transaction to update slots and record the claim simultaneously.
    ''' </summary>
    Public Function RecordResidentAidClaim(data As ResidentRecordingData) As RecordingResult
        Dim result As New RecordingResult()
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing

        Try
            If data.ResidentId <= 0 Then
                result.Message = "Invalid resident selected."
                result.ErrorCode = 1
                Return result
            End If

            If data.AidId <= 0 Then
                result.Message = "Invalid ayuda selected."
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

            If HasResidentClaimedAyudaInTransaction(data.ResidentId, data.AidId, connection, transaction) Then
                transaction.Rollback()
                result.Message = "This resident has already claimed this ayuda."
                result.ErrorCode = 2
                Return result
            End If

            If Not IsResidentQualifiedForAyuda(data.ResidentId, data.AidId, connection, transaction) Then
                transaction.Rollback()
                result.Message = "This resident is not qualified for the selected ayuda category."
                result.ErrorCode = 5
                Return result
            End If

            ' Decrease slots only if the ayuda is still valid.
            ' If rowsAffected is 0, it means max slots reached, expired, inactive, or invalid ayuda.
            Dim updateQuery As String =
                "UPDATE barangayaid " &
                "SET max_slots = max_slots - 1 " &
                "WHERE AidId = @AidId " &
                "AND max_slots > 0 " &
                "AND is_active = 1 " &
                "AND end_date >= CURDATE()"

            Using updateCmd As New MySqlCommand(updateQuery, connection, transaction)
                updateCmd.Parameters.AddWithValue("@AidId", data.AidId)

                Dim rowsAffected As Integer = updateCmd.ExecuteNonQuery()

                If rowsAffected = 0 Then
                    transaction.Rollback()
                    result.Message = "This ayuda is no longer available. It may be full, expired, or inactive."
                    result.ErrorCode = 4
                    Return result
                End If
            End Using

            Dim insertQuery As String =
                "INSERT INTO residentaid (ResidentId, AidId, AidDate, Quantity, Description) " &
                "VALUES (@ResidentId, @AidId, @AidDate, @Quantity, @Description)"

            Using insertCmd As New MySqlCommand(insertQuery, connection, transaction)
                insertCmd.Parameters.AddWithValue("@ResidentId", data.ResidentId)
                insertCmd.Parameters.AddWithValue("@AidId", data.AidId)
                insertCmd.Parameters.AddWithValue("@AidDate", DateTime.Now)
                insertCmd.Parameters.AddWithValue("@Quantity", data.Quantity)
                insertCmd.Parameters.AddWithValue("@Description", If(String.IsNullOrWhiteSpace(data.Description), DBNull.Value, data.Description))

                insertCmd.ExecuteNonQuery()
                result.RecordId = CInt(insertCmd.LastInsertedId)
            End Using

            LogAuditTrail(data.ResidentId, data.AidId, connection, transaction)

            transaction.Commit()

            result.IsSuccess = True
            result.Message = "Ayuda claimed successfully. Remaining slots updated."
            result.ErrorCode = 0

        Catch ex As MySqlException
            If transaction IsNot Nothing Then transaction.Rollback()
            result.Message = "Database error: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("RecordResidentAidClaim MySQL Error: " & ex.Message)
        Catch ex As Exception
            If transaction IsNot Nothing Then transaction.Rollback()
            result.Message = "Error recording ayuda claim: " & ex.Message
            result.ErrorCode = 3
            Debug.WriteLine("RecordResidentAidClaim Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return result
    End Function

    Private Function HasResidentClaimedAyudaInTransaction(residentId As Integer, aidId As Integer, connection As MySqlConnection, transaction As MySqlTransaction) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM residentaid WHERE ResidentId = @ResidentId AND AidId = @AidId"

        Using cmd As New MySqlCommand(query, connection, transaction)
            cmd.Parameters.AddWithValue("@ResidentId", residentId)
            cmd.Parameters.AddWithValue("@AidId", aidId)

            Return CInt(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Function IsResidentQualifiedForAyuda(residentId As Integer, aidId As Integer, connection As MySqlConnection, transaction As MySqlTransaction) As Boolean
        Dim query As String =
            "SELECT COUNT(*) " &
            "FROM residentcategory rc " &
            "INNER JOIN barangayaid ba ON rc.CategoryId = ba.CategoryId " &
            "WHERE rc.ResidentId = @ResidentId " &
            "AND ba.AidId = @AidId"

        Using cmd As New MySqlCommand(query, connection, transaction)
            cmd.Parameters.AddWithValue("@ResidentId", residentId)
            cmd.Parameters.AddWithValue("@AidId", aidId)

            Return CInt(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Sub LogAuditTrail(residentId As Integer, aidId As Integer, connection As MySqlConnection, transaction As MySqlTransaction)
        Try
            Dim userId As Integer = LogInForm.CurrentUserID
            Dim username As String = LogInForm.CurrentUsername
            Dim residentName As String = GetResidentName(residentId)
            Dim aidName As String = GetAidName(aidId)

            Dim description As String = username & " distributed the ayuda of " & residentName & " (" & aidName & ")"

            Dim auditQuery As String =
                "INSERT INTO audittrail (UserId, Username, Form, Action, Description, Timestamp) " &
                "VALUES (@UserId, @Username, @Form, @Action, @Description, NOW())"

            Using auditCmd As New MySqlCommand(auditQuery, connection, transaction)
                auditCmd.Parameters.AddWithValue("@UserId", userId)
                auditCmd.Parameters.AddWithValue("@Username", username)
                auditCmd.Parameters.AddWithValue("@Form", "AyudaRecording_Form")
                auditCmd.Parameters.AddWithValue("@Action", "Claim Ayuda")
                auditCmd.Parameters.AddWithValue("@Description", description)
                auditCmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Debug.WriteLine("LogAuditTrail Error: " & ex.Message)
        End Try
    End Sub

    Private Function GetResidentName(residentId As Integer) As String
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return "Unknown"

            Dim query As String = "SELECT CONCAT(FirstName, ' ', LastName) FROM residents WHERE ResidentId = @ResidentId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)

                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return result.ToString()
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetResidentName Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return "Unknown"
    End Function

    Private Function GetAidName(aidId As Integer) As String
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return "Unknown"

            Dim query As String = "SELECT program_title FROM barangayaid WHERE AidId = @AidId"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@AidId", aidId)

                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return result.ToString()
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetAidName Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return "Unknown"
    End Function

End Class