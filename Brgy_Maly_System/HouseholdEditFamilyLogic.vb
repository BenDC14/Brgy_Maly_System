' ================================================================================
' FILE: HouseholdEditFamilyLogic.vb
' LAYER: Business Logic & Parameterized ADO.NET Data Layer
' ================================================================================
Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient

Public Class HouseholdEditFamilyLogic

    Public Class FamilyInformation
        Public Property FamilyId As Integer
        Public Property HeadResidentId As Integer
        Public Property HouseholdId As Integer
        Public Property HouseholdNumber As String
        Public Property FamilyName As String
        Public Property FamilyHeadFullName As String
        Public Property TotalFamilyMembers As Integer
    End Class

    Public Function GetFamilyInformation(familyId As Integer) As FamilyInformation
        Dim info As New FamilyInformation()
        Dim connection As MySqlConnection = Nothing
        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return info
            Dim query As String =
                "SELECT fh.FamilyId, fh.ResidentId AS HeadResidentId, fh.FamilyName, " &
                "r.HouseholdId, h.HouseholdNumber, " &
                "CONCAT(r.FirstName,' ',IFNULL(NULLIF(r.MiddleName,''),''),' ',r.LastName) AS FamilyHeadFullName, " &
                "(1 + IFNULL((SELECT COUNT(*) FROM familymembers fm WHERE fm.FamilyId = fh.FamilyId),0)) AS TotalFamilyMembers " &
                "FROM familyhead fh " &
                "INNER JOIN residents r ON fh.ResidentId = r.ResidentId " &
                "LEFT JOIN household h ON r.HouseholdId = h.HouseholdID " &
                "WHERE fh.FamilyId = @FamilyId LIMIT 1"
            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@FamilyId", familyId)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        info.FamilyId = CInt(reader("FamilyId"))
                        info.HeadResidentId = CInt(reader("HeadResidentId"))
                        info.FamilyName = If(IsDBNull(reader("FamilyName")), "", reader("FamilyName").ToString())
                        info.HouseholdId = If(IsDBNull(reader("HouseholdId")), -1, CInt(reader("HouseholdId")))
                        info.HouseholdNumber = If(IsDBNull(reader("HouseholdNumber")), "", reader("HouseholdNumber").ToString())
                        info.FamilyHeadFullName = If(IsDBNull(reader("FamilyHeadFullName")), "", reader("FamilyHeadFullName").ToString())
                        info.TotalFamilyMembers = If(IsDBNull(reader("TotalFamilyMembers")), 1, CInt(reader("TotalFamilyMembers")))
                    End If
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetFamilyInformation Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
        Return info
    End Function

    Public Function GetFamilyMembers(familyId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing
        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable
            Dim query As String =
                "SELECT -1 AS FamilyMemberId, r.ResidentId, " &
                "CONCAT(r.FirstName,' ',IFNULL(NULLIF(r.MiddleName,''),''),' ',r.LastName) AS FullName, " &
                "'Head' AS RelationshipType, r.Sex, r.CivilStatus, r.ContactNumber, 1 AS IsHead " &
                "FROM familyhead fh INNER JOIN residents r ON fh.ResidentId = r.ResidentId " &
                "WHERE fh.FamilyId = @FamilyId " &
                "UNION ALL " &
                "SELECT fm.FamilyMemberId, r.ResidentId, " &
                "CONCAT(r.FirstName,' ',IFNULL(NULLIF(r.MiddleName,''),''),' ',r.LastName) AS FullName, " &
                "fm.RelationshipType, r.Sex, r.CivilStatus, r.ContactNumber, 0 AS IsHead " &
                "FROM familymembers fm INNER JOIN residents r ON fm.ResidentId = r.ResidentId " &
                "WHERE fm.FamilyId = @FamilyId " &
                "ORDER BY IsHead DESC, RelationshipType, FullName"
            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@FamilyId", familyId)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetFamilyMembers Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
        Return dataTable
    End Function

    Public Function GetRelationshipTypes() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing
        dataTable.Columns.Add("RelationshipId", GetType(Integer))
        dataTable.Columns.Add("Relationship", GetType(String))
        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable
            Using cmd As New MySqlCommand(
                    "SELECT RelationshipId, Relationship FROM relationship ORDER BY Relationship ASC",
                    connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    dataTable.Clear() : adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetRelationshipTypes Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
        Return dataTable
    End Function

    Public Function GetResidentsByHousehold(householdId As Integer) As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing
        dataTable.Columns.Add("ResidentId", GetType(Integer))
        dataTable.Columns.Add("FullName", GetType(String))
        Try
            If householdId <= 0 Then Return dataTable
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable
            Dim query As String =
                "SELECT r.ResidentId, " &
                "CONCAT(r.FirstName,' ',IFNULL(NULLIF(r.MiddleName,''),''),' ',r.LastName) AS FullName " &
                "FROM residents r " &
                "WHERE r.HouseholdId = @HouseholdId AND (r.Is_Archived IS NULL OR r.Is_Archived = 0) " &
                "ORDER BY r.LastName, r.FirstName"
            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@HouseholdId", householdId)
                Using adapter As New MySqlDataAdapter(cmd)
                    dataTable.Clear() : adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetResidentsByHousehold Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
        Return dataTable
    End Function

    Public Function AddFamilyMember(familyId As Integer,
                                    residentId As Integer,
                                    relationshipType As String) As Boolean
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing
        Try
            If familyId <= 0 OrElse residentId <= 0 Then Return False
            If String.IsNullOrWhiteSpace(relationshipType) Then Return False
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False
            Using cmd As New MySqlCommand(
                    "SELECT COUNT(*) FROM familyhead WHERE FamilyId=@F AND ResidentId=@R",
                    connection)
                cmd.Parameters.AddWithValue("@F", familyId)
                cmd.Parameters.AddWithValue("@R", residentId)
                If CInt(cmd.ExecuteScalar()) > 0 Then Return False
            End Using
            Using cmd As New MySqlCommand(
                    "SELECT COUNT(*) FROM familymembers WHERE FamilyId=@F AND ResidentId=@R",
                    connection)
                cmd.Parameters.AddWithValue("@F", familyId)
                cmd.Parameters.AddWithValue("@R", residentId)
                If CInt(cmd.ExecuteScalar()) > 0 Then Return False
            End Using
            Using cmd As New MySqlCommand(
                    "INSERT INTO familymembers (FamilyId, ResidentId, RelationshipType) " &
                    "VALUES (@FamilyId, @ResidentId, @RelationshipType)",
                    connection)
                cmd.Parameters.AddWithValue("@FamilyId", familyId)
                cmd.Parameters.AddWithValue("@ResidentId", residentId)
                cmd.Parameters.AddWithValue("@RelationshipType", relationshipType.Trim())
                Return cmd.ExecuteNonQuery() > 0
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("HouseholdEditFamily_Form", "ADD FAMILY MEMBER",
                    LogInForm.CurrentUsername & " added family member (Resident ID: " & residentId & ") to Family ID: " & familyId,
                    connection, transaction)

            End Using

        Catch ex As Exception
            Debug.WriteLine("AddFamilyMember Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    Public Function UpdateFamilyMemberRelationship(familyMemberId As Integer,
                                                   newRelationshipType As String) As Boolean
        Dim connection As MySqlConnection = Nothing
        Dim transaction As MySqlTransaction = Nothing
        Try
            If familyMemberId <= 0 Then Return False
            If String.IsNullOrWhiteSpace(newRelationshipType) Then Return False
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False
            Using cmd As New MySqlCommand(
                    "UPDATE familymembers SET RelationshipType=@R WHERE FamilyMemberId=@Id",
                    connection)
                cmd.Parameters.AddWithValue("@R", newRelationshipType.Trim())
                cmd.Parameters.AddWithValue("@Id", familyMemberId)
                Return cmd.ExecuteNonQuery() > 0
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("HouseholdEditFamily_Form", "UPDATE RELATIONSHIP",
                    LogInForm.CurrentUsername & " updated relationship type for Family Member ID: " & familyMemberId,
                    connection, transaction)
            End Using
        Catch ex As Exception
            Debug.WriteLine("UpdateFamilyMemberRelationship Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

End Class
