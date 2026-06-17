Imports MySql.Data.MySqlClient

''' <summary>
''' Business Logic and parameterised ADO.NET data layer for the Report Type feature.
''' All SQL lives here. The UI layer must contain no database code.
'''
''' Tables used:
'''   reporttype      — columns: ReportTypeId (INT AUTO_INCREMENT PK), ReportTypeName (VARCHAR)
'''   reportsubtype   — columns: ReportsSubTypeId (INT AUTO_INCREMENT PK),
'''                              ReportTypeId (INT FK → reporttype),
'''                              ReportSubTypeName (VARCHAR)
''' </summary>
Public Class NewReportTypeLogic

    ' ─────────────────────────────────────────────────────────────────────────────
    ' READ — Report Types (parent table)
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Returns all rows from <c>reporttype</c> ordered alphabetically.
    ''' Columns: ReportTypeId (Integer), ReportTypeName (String).
    ''' The sentinel "[Add New Type]" is NOT injected here; the form prepends it.
    ''' </summary>
    Public Function GetReportTypes() As DataTable
        Dim dt As New DataTable()
        Dim connection As MySqlConnection = Nothing

        dt.Columns.Add("ReportTypeId", GetType(Integer))
        dt.Columns.Add("ReportTypeName", GetType(String))

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dt

            Dim query As String =
                "SELECT ReportTypeId, ReportTypeName " &
                "FROM   reporttype " &
                "ORDER BY ReportTypeName ASC"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    dt.Clear()
                    adapter.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetReportTypes Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return dt
    End Function

    ''' <summary>
    ''' Returns the <c>ReportTypeId</c> for an exact name match, or -1 if not found.
    ''' Used by the form to resolve a display name back to its PK without keeping
    ''' the full DataTable alive.
    ''' </summary>
    Public Function GetReportTypeIdByName(reportTypeName As String) As Integer
        Dim connection As MySqlConnection = Nothing

        Try
            If String.IsNullOrWhiteSpace(reportTypeName) Then Return -1

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1

            Using cmd As New MySqlCommand(
                    "SELECT ReportTypeId FROM reporttype " &
                    "WHERE  ReportTypeName = @ReportTypeName LIMIT 1",
                    connection)
                cmd.Parameters.AddWithValue("@ReportTypeName", reportTypeName.Trim())
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return CInt(result)
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetReportTypeIdByName Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return -1
    End Function

    ' ─────────────────────────────────────────────────────────────────────────────
    ' READ — Report Sub-Types (child table, filtered by parent)
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Returns all sub-types that belong to <paramref name="reportTypeId"/>,
    ''' ordered alphabetically.
    ''' Columns: ReportsSubTypeId (Integer), ReportTypeId (Integer),
    '''          ReportSubTypeName (String).
    ''' The sentinel "[Add New Sub-Type]" is NOT injected here; the form prepends it.
    ''' </summary>
    Public Function GetReportSubTypes(reportTypeId As Integer) As DataTable
        Dim dt As New DataTable()
        Dim connection As MySqlConnection = Nothing

        dt.Columns.Add("ReportsSubTypeId", GetType(Integer))
        dt.Columns.Add("ReportTypeId", GetType(Integer))
        dt.Columns.Add("ReportSubTypeName", GetType(String))

        Try
            If reportTypeId <= 0 Then Return dt

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dt

            Dim query As String =
                "SELECT ReportsSubTypeId, ReportTypeId, ReportSubTypeName " &
                "FROM   reportsubtype " &
                "WHERE  ReportTypeId = @ReportTypeId " &
                "ORDER BY ReportSubTypeName ASC"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    cmd.Parameters.AddWithValue("@ReportTypeId", reportTypeId)
                    dt.Clear()
                    adapter.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetReportSubTypes Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return dt
    End Function

    ''' <summary>
    ''' Returns the <c>ReportsSubTypeId</c> for an exact name + parent match, or -1.
    ''' </summary>
    Public Function GetSubTypeIdByName(reportTypeId As Integer,
                                       subTypeName As String) As Integer
        Dim connection As MySqlConnection = Nothing

        Try
            If reportTypeId <= 0 OrElse String.IsNullOrWhiteSpace(subTypeName) Then Return -1

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1

            Using cmd As New MySqlCommand(
                    "SELECT ReportsSubTypeId FROM reportsubtype " &
                    "WHERE  ReportTypeId = @ReportTypeId " &
                    "AND    ReportSubTypeName = @ReportSubTypeName " &
                    "LIMIT  1",
                    connection)
                cmd.Parameters.AddWithValue("@ReportTypeId", reportTypeId)
                cmd.Parameters.AddWithValue("@ReportSubTypeName", subTypeName.Trim())
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return CInt(result)
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetSubTypeIdByName Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return -1
    End Function

    ' ─────────────────────────────────────────────────────────────────────────────
    ' INSERT — Add a new Report Type
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Inserts a new row into <c>reporttype</c>.
    ''' The auto-increment <c>ReportTypeId</c> column is intentionally omitted
    ''' from the INSERT so the database assigns the PK.
    '''
    ''' Returns the new (or existing duplicate) ReportTypeId on success, -1 on error.
    ''' </summary>
    Public Function ExecuteNonQuery(reportTypeName As String) As Integer
        Dim connection As MySqlConnection = Nothing

        Try
            If String.IsNullOrWhiteSpace(reportTypeName) Then Return -1

            Dim trimmed As String = reportTypeName.Trim()

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1

            ' ── Duplicate guard ────────────────────────────────────────────────
            Using checkCmd As New MySqlCommand(
                    "SELECT ReportTypeId FROM reporttype " &
                    "WHERE  ReportTypeName = @ReportTypeName LIMIT 1",
                    connection)
                checkCmd.Parameters.AddWithValue("@ReportTypeName", trimmed)
                Dim existing As Object = checkCmd.ExecuteScalar()
                If existing IsNot Nothing Then Return CInt(existing)
            End Using

            ' ── Parameterised INSERT (PK column omitted — AUTO_INCREMENT) ──────
            Using cmd As New MySqlCommand(
                    "INSERT INTO reporttype (ReportTypeName) " &
                    "VALUES (@ReportTypeName); " &
                    "SELECT LAST_INSERT_ID();",
                    connection)
                cmd.Parameters.AddWithValue("@ReportTypeName", trimmed)
                Dim result As Object = cmd.ExecuteScalar()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("NewReportType_Form", "ADD REPORT TYPE",
                LogInForm.CurrentUsername & " added new report type: " & reportTypeName)

                If result IsNot Nothing Then Return CInt(result)
            End Using

        Catch ex As Exception
            Debug.WriteLine("AddReportType Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return -1
    End Function

    ' ─────────────────────────────────────────────────────────────────────────────
    ' INSERT — Add a new Report Sub-Type
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Inserts a new row into <c>reportsubtype</c> linked to the given parent.
    ''' The auto-increment PK is omitted from the INSERT.
    '''
    ''' Returns the new (or existing duplicate) ReportsSubTypeId on success, -1 on error.
    ''' </summary>
    Public Function AddReportSubType(reportTypeId As Integer,
                                     reportSubTypeName As String) As Integer
        Dim connection As MySqlConnection = Nothing

        Try
            If reportTypeId <= 0 Then Return -1
            If String.IsNullOrWhiteSpace(reportSubTypeName) Then Return -1

            Dim trimmed As String = reportSubTypeName.Trim()

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1

            ' ── Duplicate guard (scoped to the same parent) ────────────────────
            Using checkCmd As New MySqlCommand(
                    "SELECT ReportsSubTypeId FROM reportsubtype " &
                    "WHERE  ReportTypeId = @ReportTypeId " &
                    "AND    ReportSubTypeName = @ReportSubTypeName " &
                    "LIMIT  1",
                    connection)
                checkCmd.Parameters.AddWithValue("@ReportTypeId", reportTypeId)
                checkCmd.Parameters.AddWithValue("@ReportSubTypeName", trimmed)
                Dim existing As Object = checkCmd.ExecuteScalar()
                If existing IsNot Nothing Then Return CInt(existing)
            End Using

            ' ── Parameterised INSERT (PK column omitted — AUTO_INCREMENT) ──────
            Using cmd As New MySqlCommand(
                    "INSERT INTO reportsubtype (ReportTypeId, ReportSubTypeName) " &
                    "VALUES (@ReportTypeId, @ReportSubTypeName); " &
                    "SELECT LAST_INSERT_ID();",
                    connection)
                cmd.Parameters.AddWithValue("@ReportTypeId", reportTypeId)
                cmd.Parameters.AddWithValue("@ReportSubTypeName", trimmed)
                Dim result As Object = cmd.ExecuteScalar()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("NewReportType_Form", "ADD REPORT SUBTYPE",
                LogInForm.CurrentUsername & " added new report sub-type under Report Type ID: " & reportTypeId)

                If result IsNot Nothing Then Return CInt(result)
            End Using

        Catch ex As Exception
            Debug.WriteLine("AddReportSubType Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return -1
    End Function

    ' ─────────────────────────────────────────────────────────────────────────────
    ' UPDATE — Rename a Report Type
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Updates <c>ReportTypeName</c> for the given PK.
    ''' Returns True on success, False on validation failure or DB error.
    ''' </summary>
    Public Function UpdateReportType(reportTypeId As Integer,
                                     newTypeName As String) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            If reportTypeId <= 0 OrElse String.IsNullOrWhiteSpace(newTypeName) Then
                Return False
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Using cmd As New MySqlCommand(
                    "UPDATE reporttype " &
                    "SET    ReportTypeName = @ReportTypeName " &
                    "WHERE  ReportTypeId   = @ReportTypeId",
                    connection)
                cmd.Parameters.AddWithValue("@ReportTypeName", newTypeName.Trim())
                cmd.Parameters.AddWithValue("@ReportTypeId", reportTypeId)
                cmd.ExecuteNonQuery()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("NewReportType_Form", "UPDATE REPORT TYPE",
                LogInForm.CurrentUsername & " updated report type (ID: " & reportTypeId & ")")

                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("UpdateReportType Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try
    End Function

    ' ─────────────────────────────────────────────────────────────────────────────
    ' UPDATE — Rename a Report Sub-Type
    ' ─────────────────────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Updates <c>ReportSubTypeName</c> for the given PK.
    ''' Returns True on success, False on validation failure or DB error.
    ''' </summary>
    Public Function UpdateReportSubType(reportsSubTypeId As Integer,
                                        newSubTypeName As String) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            If reportsSubTypeId <= 0 OrElse String.IsNullOrWhiteSpace(newSubTypeName) Then
                Return False
            End If

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Using cmd As New MySqlCommand(
                    "UPDATE reportsubtype " &
                    "SET    ReportSubTypeName = @ReportSubTypeName " &
                    "WHERE  ReportsSubTypeId  = @ReportsSubTypeId",
                    connection)
                cmd.Parameters.AddWithValue("@ReportSubTypeName", newSubTypeName.Trim())
                cmd.Parameters.AddWithValue("@ReportsSubTypeId", reportsSubTypeId)
                cmd.ExecuteNonQuery()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("NewReportType_Form", "UPDATE REPORT SUBTYPE",
                LogInForm.CurrentUsername & " updated report sub-type (ID: " & reportsSubTypeId & ")")

                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("UpdateReportSubType Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try
    End Function

End Class
