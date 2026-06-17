' ================================================================================
' FILE: Dashboard1_Logic.vb
' LAYER: Business Logic & Parameterized SQL Dashboard Aggregator
'
' Operations:
'   GetTotalResidents    — COUNT(*) from residents (non-archived)
'   GetTotalHouseholds   — COUNT(*) from household
'   GetTotalByCategory   — COUNT via residentcategory JOIN categories (by keyword)
'   GetPopulationByAge   — CASE WHEN age bracket grouping from residents.DateOfBirth
'   GetPopulationBySex   — GROUP BY Sex count from residents (non-archived)
' ================================================================================
Imports MySql.Data.MySqlClient
Imports System.Data

Public Class Dashboard1_Logic

    ' ============================================================================
    ' SCALAR — Total Residents (excludes archived)
    ' ============================================================================

    ''' <summary>
    ''' Returns the total count of non-archived residents.
    ''' </summary>
    Public Function GetTotalResidents() As Integer
        Dim connection As MySqlConnection = Nothing
        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return 0

            Using cmd As New MySqlCommand(
                    "SELECT COUNT(*) " &
                    "FROM   residents " &
                    "WHERE  (Is_Archived IS NULL OR Is_Archived = 0)",
                    connection)
                Dim result As Object = cmd.ExecuteScalar()
                Return If(result IsNot Nothing AndAlso Not IsDBNull(result), CInt(result), 0)
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetTotalResidents Error: " & ex.Message)
            Return 0
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ' ============================================================================
    ' SCALAR — Total Households
    ' ============================================================================

    ''' <summary>
    ''' Returns the total count of households.
    ''' </summary>
    Public Function GetTotalHouseholds() As Integer
        Dim connection As MySqlConnection = Nothing
        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return 0

            Using cmd As New MySqlCommand(
                    "SELECT COUNT(*) FROM household",
                    connection)
                Dim result As Object = cmd.ExecuteScalar()
                Return If(result IsNot Nothing AndAlso Not IsDBNull(result), CInt(result), 0)
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetTotalHouseholds Error: " & ex.Message)
            Return 0
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ' ============================================================================
    ' SCALAR — Total by Category (Student / Senior / PWD)
    ' Matches by LOWER(Category) keyword so exact DB spelling doesn't matter.
    ' keyword values: "student", "senior", "pwd"
    ' ============================================================================

    ''' <summary>
    ''' Returns the count of non-archived residents tagged with a given category
    ''' keyword. The keyword is matched loosely via LIKE against the Category name.
    ''' </summary>
    ''' <param name="categoryKeyword">
    ''' One of: "student", "senior", "pwd" — matched with LIKE %keyword%
    ''' </param>
    Public Function GetTotalByCategory(categoryKeyword As String) As Integer
        Dim connection As MySqlConnection = Nothing
        Try
            If String.IsNullOrWhiteSpace(categoryKeyword) Then Return 0

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return 0

            ' JOIN residentcategory → categories → residents
            ' Filters: category name LIKE keyword, resident not archived
            Dim query As String =
                "SELECT COUNT(DISTINCT rc.ResidentId) " &
                "FROM   residentcategory rc " &
                "INNER JOIN categories c  ON rc.CategoryId  = c.CategoryId " &
                "INNER JOIN residents  r  ON rc.ResidentId  = r.ResidentId " &
                "WHERE  LOWER(c.Category) LIKE @Keyword " &
                "  AND  (r.Is_Archived IS NULL OR r.Is_Archived = 0)"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Keyword", "%" & categoryKeyword.Trim().ToLower() & "%")
                Dim result As Object = cmd.ExecuteScalar()
                Return If(result IsNot Nothing AndAlso Not IsDBNull(result), CInt(result), 0)
            End Using

        Catch ex As Exception
            Debug.WriteLine($"GetTotalByCategory({categoryKeyword}) Error: " & ex.Message)
            Return 0
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    ' ============================================================================
    ' DATASET — Population by Age Bracket (for AgeChart Column Chart)
    ' Uses CASE WHEN on TIMESTAMPDIFF(YEAR, DateOfBirth, CURDATE()) to
    ' bucket each resident into a labelled age group.
    ' Columns: AgeBracket (String), Count (Integer)
    ' ============================================================================

    ''' <summary>
    ''' Returns population counts grouped into age brackets for the bar chart.
    ''' Brackets: 0-12 Child | 13-17 Teen | 18-35 Young Adult |
    '''           36-59 Adult | 60+ Senior
    ''' Only non-archived residents with a non-null DateOfBirth are counted.
    ''' </summary>
    Public Function GetPopulationByAge() As DataTable
        Dim dt As New DataTable()
        Dim connection As MySqlConnection = Nothing

        dt.Columns.Add("AgeBracket", GetType(String))
        dt.Columns.Add("Count", GetType(Integer))

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dt

            Dim query As String =
                "SELECT " &
                "  CASE " &
                "    WHEN TIMESTAMPDIFF(YEAR, DateOfBirth, CURDATE()) BETWEEN 0  AND 12 THEN '0-12 Child' " &
                "    WHEN TIMESTAMPDIFF(YEAR, DateOfBirth, CURDATE()) BETWEEN 13 AND 17 THEN '13-17 Teen' " &
                "    WHEN TIMESTAMPDIFF(YEAR, DateOfBirth, CURDATE()) BETWEEN 18 AND 35 THEN '18-35 Young Adult' " &
                "    WHEN TIMESTAMPDIFF(YEAR, DateOfBirth, CURDATE()) BETWEEN 36 AND 59 THEN '36-59 Adult' " &
                "    WHEN TIMESTAMPDIFF(YEAR, DateOfBirth, CURDATE()) >= 60             THEN '60+ Senior' " &
                "    ELSE 'Unknown' " &
                "  END AS AgeBracket, " &
                "  COUNT(*) AS Count " &
                "FROM  residents " &
                "WHERE (Is_Archived IS NULL OR Is_Archived = 0) " &
                "  AND  DateOfBirth IS NOT NULL " &
                "GROUP BY AgeBracket " &
                "ORDER BY " &
                "  CASE AgeBracket " &
                "    WHEN '0-12 Child'        THEN 1 " &
                "    WHEN '13-17 Teen'        THEN 2 " &
                "    WHEN '18-35 Young Adult' THEN 3 " &
                "    WHEN '36-59 Adult'       THEN 4 " &
                "    WHEN '60+ Senior'        THEN 5 " &
                "    ELSE 6 " &
                "  END"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    dt.Clear()
                    adapter.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetPopulationByAge Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dt
    End Function

    ' ============================================================================
    ' DATASET — Population by Sex (for SexChart Doughnut Chart)
    ' Groups non-archived residents by their Sex value.
    ' Columns: Sex (String), Count (Integer)
    ' ============================================================================

    ''' <summary>
    ''' Returns population counts grouped by Sex for the doughnut chart.
    ''' Only non-archived residents with a non-null Sex value are counted.
    ''' </summary>
    Public Function GetPopulationBySex() As DataTable
        Dim dt As New DataTable()
        Dim connection As MySqlConnection = Nothing

        dt.Columns.Add("Sex", GetType(String))
        dt.Columns.Add("Count", GetType(Integer))

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dt

            Dim query As String =
                "SELECT Sex, COUNT(*) AS Count " &
                "FROM   residents " &
                "WHERE  (Is_Archived IS NULL OR Is_Archived = 0) " &
                "  AND   Sex IS NOT NULL " &
                "  AND   TRIM(Sex) <> '' " &
                "GROUP BY Sex " &
                "ORDER BY Sex ASC"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    dt.Clear()
                    adapter.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetPopulationBySex Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dt
    End Function

End Class
