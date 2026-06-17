Imports MySql.Data.MySqlClient
Imports System.Data

''' <summary>
''' Business Logic Layer for Audit_Form.
''' Provides parameterized SQL pagination, distinct-value metadata loaders,
''' and a full-dataset fetch for exports.
''' Table: audittrail  Columns: AuditId, Timestamp, Username, Form, Action, Description
''' </summary>
Public Class AuditLogic

    ' ════════════════════════════════════════════════════════════════
    '  CONSTANTS
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>Number of rows per page.</summary>
    Public Const PAGE_SIZE As Integer = 20

    ' ════════════════════════════════════════════════════════════════
    '  DATA TRANSFER — Filter DTO
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Carries the active filter state from the form to every query.
    ''' Empty strings mean "no filter" — the WHERE clause will skip them.
    ''' </summary>
    Public Class AuditFilter
        Public Property SearchText As String = ""
        Public Property ActionType As String = ""
        Public Property FormName As String = ""
        Public Property DateFrom As Date = Date.MinValue
        Public Property DateTo As Date = Date.MaxValue
    End Class

    ' ════════════════════════════════════════════════════════════════
    '  METADATA DROPDOWN BUILDERS
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Returns every distinct Action value in audittrail, sorted A→Z.
    ''' Excludes NULL and empty-string rows.
    ''' Used to populate cbActionType dynamically at runtime.
    ''' </summary>
    Public Function GetDistinctActions() As List(Of String)
        Return GetDistinctColumn("Action")
    End Function

    ''' <summary>
    ''' Returns every distinct Form value in audittrail, sorted A→Z.
    ''' Excludes NULL and empty-string rows.
    ''' Used to populate cbForms dynamically at runtime.
    ''' </summary>
    Public Function GetDistinctForms() As List(Of String)
        Return GetDistinctColumn("Form")
    End Function

    ''' <summary>
    ''' Generic helper — SELECT DISTINCT a single column from audittrail.
    ''' The AND column &lt;&gt; '' guard ensures blank/whitespace rows
    ''' never pollute the dropdown even if the DB contains them.
    ''' NOTE: columnName is an internal constant string — never user input.
    ''' </summary>
    Private Function GetDistinctColumn(columnName As String) As List(Of String)
        Dim results As New List(Of String)()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return results

            ' UPDATED: Added AND column <> '' to exclude empty string rows
            Dim query As String =
                "SELECT DISTINCT " & columnName & " " &
                "FROM audittrail " &
                "WHERE " & columnName & " IS NOT NULL " &
                "  AND " & columnName & " <> '' " &
                "ORDER BY " & columnName & " ASC"

            Using cmd As New MySqlCommand(query, connection)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        If Not IsDBNull(reader(0)) Then
                            Dim val As String = reader(0).ToString().Trim()
                            If val.Length > 0 Then
                                results.Add(val)
                            End If
                        End If
                    End While
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("AuditLogic.GetDistinctColumn(" & columnName & ") Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return results
    End Function


    ' ════════════════════════════════════════════════════════════════
    '  PAGINATION ENGINE — Total Page Count
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Returns the total number of pages for the current filter set.
    ''' Equation: CEIL( total_matching_rows / PAGE_SIZE )
    ''' </summary>
    Public Function GetTotalPages(filter As AuditFilter) As Integer
        Dim totalRows As Integer = GetTotalRowCount(filter)
        If totalRows = 0 Then Return 1
        Return CInt(Math.Ceiling(totalRows / PAGE_SIZE))
    End Function

    ''' <summary>
    ''' Returns the raw total matching row count for the given filter.
    ''' </summary>
    Public Function GetTotalRowCount(filter As AuditFilter) As Integer
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return 0

            Dim whereClause As String = BuildWhereClause(filter)
            Dim query As String = "SELECT COUNT(*) FROM audittrail " & whereClause

            Using cmd As New MySqlCommand(query, connection)
                ApplyFilterParameters(cmd, filter)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return CInt(result)
                End If
            End Using

        Catch ex As Exception
            Debug.WriteLine("AuditLogic.GetTotalRowCount Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return 0
    End Function

    ' ════════════════════════════════════════════════════════════════
    '  PAGINATION ENGINE — Paged Row Fetch
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Fetches exactly PAGE_SIZE rows for the requested page number,
    ''' applying all active filter parameters via LIMIT / OFFSET.
    ''' pageNumber is 1-based.
    ''' </summary>
    Public Function GetPagedAuditLogs(filter As AuditFilter,
                                       pageNumber As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            If pageNumber < 1 Then pageNumber = 1
            Dim offset As Integer = (pageNumber - 1) * PAGE_SIZE

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim whereClause As String = BuildWhereClause(filter)
            Dim query As String =
                "SELECT AuditId, Timestamp, Username, Form, Action, Description " &
                "FROM audittrail " &
                whereClause &
                " ORDER BY Timestamp DESC " &
                " LIMIT @PageSize OFFSET @Offset"

            Using cmd As New MySqlCommand(query, connection)
                ApplyFilterParameters(cmd, filter)
                cmd.Parameters.AddWithValue("@PageSize", PAGE_SIZE)
                cmd.Parameters.AddWithValue("@Offset", offset)

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("AuditLogic.GetPagedAuditLogs Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return dataTable
    End Function

    ' ════════════════════════════════════════════════════════════════
    '  FULL DATASET — for exports (no page limit)
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Returns ALL matching rows for the current filter — used by export buttons.
    ''' No LIMIT applied. Ordered newest first.
    ''' </summary>
    Public Function GetAllFilteredAuditLogs(filter As AuditFilter) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim whereClause As String = BuildWhereClause(filter)
            Dim query As String =
                "SELECT AuditId, Timestamp, Username, Form, Action, Description " &
                "FROM audittrail " &
                whereClause &
                " ORDER BY Timestamp DESC"

            Using cmd As New MySqlCommand(query, connection)
                ApplyFilterParameters(cmd, filter)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("AuditLogic.GetAllFilteredAuditLogs Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try

        Return dataTable
    End Function

    ' ════════════════════════════════════════════════════════════════
    '  PRIVATE HELPERS — WHERE clause and parameter binding
    ' ════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Builds the WHERE clause string dynamically based on which filters are active.
    ''' All conditions use named parameters — no string concatenation of user input.
    ''' </summary>
    Private Function BuildWhereClause(filter As AuditFilter) As String
        Dim conditions As New List(Of String)()

        ' Full-text search across Description and Username
        If Not String.IsNullOrWhiteSpace(filter.SearchText) Then
            conditions.Add("(Description LIKE @Search OR Username LIKE @Search)")
        End If

        ' Action type filter
        If Not String.IsNullOrWhiteSpace(filter.ActionType) Then
            conditions.Add("Action LIKE @ActionType")
        End If

        ' Form name filter
        If Not String.IsNullOrWhiteSpace(filter.FormName) Then
            conditions.Add("Form LIKE @FormName")
        End If

        ' Date range filter
        If filter.DateFrom <> Date.MinValue Then
            conditions.Add("Timestamp >= @DateFrom")
        End If
        If filter.DateTo <> Date.MaxValue Then
            conditions.Add("Timestamp <= @DateTo")
        End If

        If conditions.Count = 0 Then Return ""
        Return " WHERE " & String.Join(" AND ", conditions)
    End Function

    ''' <summary>
    ''' Binds filter parameter values to the command.
    ''' Must be called AFTER BuildWhereClause so the parameters match.
    ''' </summary>
    Private Sub ApplyFilterParameters(cmd As MySqlCommand, filter As AuditFilter)
        If Not String.IsNullOrWhiteSpace(filter.SearchText) Then
            cmd.Parameters.AddWithValue("@Search", "%" & filter.SearchText & "%")
        End If

        If Not String.IsNullOrWhiteSpace(filter.ActionType) Then
            cmd.Parameters.AddWithValue("@ActionType", "%" & filter.ActionType & "%")
        End If

        If Not String.IsNullOrWhiteSpace(filter.FormName) Then
            cmd.Parameters.AddWithValue("@FormName", "%" & filter.FormName & "%")
        End If

        If filter.DateFrom <> Date.MinValue Then
            cmd.Parameters.AddWithValue("@DateFrom", filter.DateFrom)
        End If

        If filter.DateTo <> Date.MaxValue Then
            cmd.Parameters.AddWithValue("@DateTo", filter.DateTo)
        End If
    End Sub

End Class
