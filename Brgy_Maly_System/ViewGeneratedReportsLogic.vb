Imports MySql.Data.MySqlClient

Public Class ViewGeneratedReportsLogic

    Public Function GetGeneratedReports() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT gr.ReportTypeId, gr.ReportsSubTypeId, " &
                "rt.ReportTypeName, rst.ReportSubTypeName, " &
                "gr.GeneratedBy, ua.Username AS GeneratedByUsername, " &
                "gr.`Date`, gr.FileName, gr.FilePath, gr.Status " &
                "FROM generatedreports gr " &
                "LEFT JOIN reporttype rt ON gr.ReportTypeId = rt.ReportTypeId " &
                "LEFT JOIN reportsubtype rst ON gr.ReportsSubTypeId = rst.ReportsSubTypeId " &
                "LEFT JOIN UserAccounts ua ON gr.GeneratedBy = ua.UserId " &
                "ORDER BY gr.`Date` DESC"

            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetGeneratedReports Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

    Public Function SearchGeneratedReports(searchTerm As String) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT gr.ReportTypeId, gr.ReportsSubTypeId, " &
                "rt.ReportTypeName, rst.ReportSubTypeName, " &
                "gr.GeneratedBy, ua.Username AS GeneratedByUsername, " &
                "gr.`Date`, gr.FileName, gr.FilePath, gr.Status " &
                "FROM generatedreports gr " &
                "LEFT JOIN reporttype rt ON gr.ReportTypeId = rt.ReportTypeId " &
                "LEFT JOIN reportsubtype rst ON gr.ReportsSubTypeId = rst.ReportsSubTypeId " &
                "LEFT JOIN UserAccounts ua ON gr.GeneratedBy = ua.UserId " &
                "WHERE rt.ReportTypeName LIKE @Search " &
                "OR rst.ReportSubTypeName LIKE @Search " &
                "OR ua.Username LIKE @Search " &
                "OR gr.FileName LIKE @Search " &
                "OR gr.Status LIKE @Search " &
                "ORDER BY gr.`Date` DESC"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Search", "%" & searchTerm & "%")

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("SearchGeneratedReports Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

End Class