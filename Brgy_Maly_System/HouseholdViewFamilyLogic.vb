Imports MySql.Data.MySqlClient
Imports System.Data

Public Class HouseholdViewFamilyLogic

    Public Function GetFamilyHeads(Optional householdId As Integer = -1) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable

            Dim query As String =
                "SELECT " &
                "fh.FamilyId, " &
                "fh.ResidentId, " &
                "fh.FamilyName, " &
                "CONCAT(r.FirstName, ' ', IFNULL(NULLIF(r.MiddleName, ''), ''), ' ', r.LastName) AS FamilyHead, " &
                "h.HouseholdNumber, " &
                "(1 + IFNULL((" &
                "SELECT COUNT(*) FROM familymembers fm WHERE fm.FamilyId = fh.FamilyId" &
                "), 0)) AS TotalFamilyMembers " &
                "FROM familyhead fh " &
                "INNER JOIN residents r ON fh.ResidentId = r.ResidentId " &
                "LEFT JOIN household h ON r.HouseholdId = h.HouseholdID " &
                "WHERE (r.Is_Archived = 0 OR r.Is_Archived IS NULL) "

            If householdId > 0 Then
                query &= "AND r.HouseholdId = @HouseholdId "
            End If

            query &= "ORDER BY fh.FamilyName, r.FirstName"

            Using cmd As New MySqlCommand(query, connection)
                If householdId > 0 Then
                    cmd.Parameters.AddWithValue("@HouseholdId", householdId)
                End If

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetFamilyHeads Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return dataTable
    End Function

End Class