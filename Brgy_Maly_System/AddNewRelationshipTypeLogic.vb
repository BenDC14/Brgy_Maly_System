Imports MySql.Data.MySqlClient

''' <summary>
''' Business Logic and parameterised ADO.NET data layer for the Relationship Type feature.
''' All SQL lives here. The UI layer must contain no database code.
'''
''' Database table  : relationship
''' Columns         : relationshipId  INTEGER  AUTO_INCREMENT PRIMARY KEY
'''                   relationship    VARCHAR(50)
''' </summary>
Public Class AddNewRelationshipTypeLogic

    ' ─────────────────────────────────────────────────────────────────────────────
    ' READ — fetch all relationship types
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Queries the <c>relationship</c> table and returns a DataTable containing
    ''' every row ordered alphabetically.  Columns: RelationshipId (Integer),
    ''' Relationship (String).
    '''
    ''' The form binds the returned rows to cbrelationship using Items.Add and
    ''' pre-pends its own sentinel at index 0 — this method returns only the
    ''' real database rows with no sentinel row injected.
    ''' </summary>
    Public Function GetRelationshipTypes() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        ' Pre-declare columns so the table is usable even if the query fails
        dataTable.Columns.Add("RelationshipId", GetType(Integer))
        dataTable.Columns.Add("Relationship", GetType(String))

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT RelationshipId, Relationship " &
                "FROM   relationship " &
                "ORDER BY Relationship ASC"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    dataTable.Clear()
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetRelationshipTypes Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    ' ─────────────────────────────────────────────────────────────────────────────
    ' READ — resolve a name to its PK
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Returns the <c>RelationshipId</c> for an exact name match.
    ''' Returns -1 when the name is not found or any error occurs.
    ''' Used by the form to resolve a ComboBox selection back to its primary key
    ''' so the form does not need to keep a full DataTable alive.
    ''' </summary>
    Public Function GetRelationshipIdByName(relationshipName As String) As Integer
        Dim connection As MySqlConnection = Nothing

        Try
            If String.IsNullOrWhiteSpace(relationshipName) Then Return -1

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1

            Dim query As String =
                "SELECT RelationshipId " &
                "FROM   relationship " &
                "WHERE  Relationship = @Relationship " &
                "LIMIT  1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Relationship", relationshipName.Trim())
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return CInt(result)
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetRelationshipIdByName Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return -1
    End Function

    ' ─────────────────────────────────────────────────────────────────────────────
    ' INSERT — add a new relationship type
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Inserts a new row into the <c>relationship</c> table using a parameterised
    ''' INSERT command.  The <c>relationshipId</c> AUTO_INCREMENT column is
    ''' deliberately omitted from the INSERT so the database handles indexing.
    '''
    ''' Returns:
    '''   > 0   the new (or existing duplicate) RelationshipId
    '''   -1    validation failure or database error
    ''' </summary>
    Public Function AddRelationshipType(relationshipName As String) As Integer
        Dim connection As MySqlConnection = Nothing

        Try
            If String.IsNullOrWhiteSpace(relationshipName) Then Return -1

            Dim trimmedName As String = relationshipName.Trim()

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1

            ' ── Duplicate guard ────────────────────────────────────────────────
            ' Return the existing id rather than raising an error so the caller
            ' can report "already exists" to the user gracefully.
            Using checkCmd As New MySqlCommand(
                    "SELECT RelationshipId " &
                    "FROM   relationship " &
                    "WHERE  Relationship = @Relationship " &
                    "LIMIT  1",
                    connection)
                checkCmd.Parameters.AddWithValue("@Relationship", trimmedName)
                Dim existing As Object = checkCmd.ExecuteScalar()
                If existing IsNot Nothing Then Return CInt(existing)
            End Using

            ' ── Parameterised INSERT — relationshipId intentionally omitted ────
            ' The database AUTO_INCREMENT assigns the new PK automatically.
            Using cmd As New MySqlCommand(
                    "INSERT INTO relationship (Relationship) " &
                    "VALUES (@Relationship); " &
                    "SELECT LAST_INSERT_ID();",
                    connection)
                cmd.Parameters.AddWithValue("@Relationship", trimmedName)
                Dim result As Object = cmd.ExecuteScalar()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("AddNewRelationshipType_Form", "ADD RELATIONSHIP TYPE",
                LogInForm.CurrentUsername & " added new relationship type: " & relationshipName)
                If result IsNot Nothing Then Return CInt(result)
            End Using

        Catch ex As Exception
            Debug.WriteLine("AddRelationshipType Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return -1
    End Function

    ' ─────────────────────────────────────────────────────────────────────────────
    ' UPDATE — rename an existing relationship type
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Updates the <c>Relationship</c> column for the row identified by
    ''' <paramref name="relationshipId"/> using a parameterised UPDATE command.
    '''
    ''' Returns True on success, False on validation failure or database error.
    ''' </summary>
    Public Function UpdateRelationshipType(relationshipId As Integer,
                                           newName As String) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            If relationshipId <= 0 OrElse String.IsNullOrWhiteSpace(newName) Then
                Return False
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Using cmd As New MySqlCommand(
                    "UPDATE relationship " &
                    "SET    Relationship = @Relationship " &
                    "WHERE  RelationshipId = @RelationshipId",
                    connection)
                cmd.Parameters.AddWithValue("@Relationship", newName.Trim())
                cmd.Parameters.AddWithValue("@RelationshipId", relationshipId)
                cmd.ExecuteNonQuery()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("AddNewRelationshipType_Form", "UPDATE RELATIONSHIP TYPE",
                LogInForm.CurrentUsername & " updated relationship type (ID: " & relationshipId & ")")

                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("UpdateRelationshipType Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

End Class
