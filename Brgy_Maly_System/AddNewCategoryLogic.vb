Imports MySql.Data.MySqlClient

Public Class AddNewCategoryLogic

    Public Function GetCategoriesWithAddNew() As DataTable
        Dim dataTable As DataTable = GetCategories()
        Dim addNewRow As DataRow = dataTable.NewRow()
        addNewRow("CategoryId") = -1
        addNewRow("Category") = "-- Add New Category --"
        dataTable.Rows.Add(addNewRow)
        Return dataTable
    End Function

    Public Function GetCategories() As DataTable
        Dim dataTable As New DataTable()
        Dim connection As MySqlConnection = Nothing
        dataTable.Columns.Add("CategoryId", GetType(Integer))
        dataTable.Columns.Add("Category", GetType(String))
        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return dataTable
            Dim query As String = "SELECT CategoryId, Category FROM categories ORDER BY Category"
            Using cmd As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(cmd)
                    dataTable.Clear()
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetCategories Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
        Return dataTable
    End Function

    Public Function AddCategory(categoryName As String) As Integer
        Dim connection As MySqlConnection = Nothing
        Try
            If String.IsNullOrWhiteSpace(categoryName) Then Return -1
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1
            ' Check for duplicate
            Using checkCmd As New MySqlCommand("SELECT CategoryId FROM categories WHERE Category = @Category LIMIT 1", connection)
                checkCmd.Parameters.AddWithValue("@Category", categoryName.Trim())
                Dim existingId = checkCmd.ExecuteScalar()
                If existingId IsNot Nothing Then Return CInt(existingId)
            End Using
            ' Insert new
            Using cmd As New MySqlCommand("INSERT INTO categories (Category) VALUES (@Category); SELECT LAST_INSERT_ID();", connection)
                cmd.Parameters.AddWithValue("@Category", categoryName.Trim())
                Dim result = cmd.ExecuteScalar()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("AddNewCategory_Form", "ADD CATEGORY",
                LogInForm.CurrentUsername & " added new resident category: " & categoryName)

                If result IsNot Nothing Then Return CInt(result)
            End Using
        Catch ex As Exception
            Debug.WriteLine("AddCategory Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then connection.Close()
        End Try
        Return -1
    End Function

    Public Function UpdateCategory(categoryId As Integer, newName As String) As Boolean
        Dim connection As MySqlConnection = Nothing
        Try
            If categoryId <= 0 OrElse String.IsNullOrWhiteSpace(newName) Then Return False
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False
            Using cmd As New MySqlCommand("UPDATE categories SET Category = @Category WHERE CategoryId = @CategoryId", connection)
                cmd.Parameters.AddWithValue("@Category", newName.Trim())
                cmd.Parameters.AddWithValue("@CategoryId", categoryId)
                cmd.ExecuteNonQuery()
                ' === LOG AUDIT TRAIL ===
                GlobalAuditLogger.Log("AddNewCategory_Form", "UPDATE CATEGORY",
                LogInForm.CurrentUsername & " updated resident category (ID: " & categoryId & ") to: " & newName)

                Return True
            End Using
        Catch ex As Exception
            Debug.WriteLine("UpdateCategory Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then connection.Close()
        End Try
    End Function
    ''' <summary>
    ''' Returns the CategoryId for an exact name match, or -1 if not found.
    ''' Used by the form to resolve the selected combo item back to its PK.
    ''' </summary>
    Public Function GetCategoryIdByName(categoryName As String) As Integer
        Dim connection As MySqlConnection = Nothing
        Try
            If String.IsNullOrWhiteSpace(categoryName) Then Return -1
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return -1

            Using cmd As New MySqlCommand(
                    "SELECT CategoryId FROM categories WHERE Category = @Category LIMIT 1",
                    connection)
                cmd.Parameters.AddWithValue("@Category", categoryName.Trim())
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return CInt(result)
            End Using
        Catch ex As Exception
            Debug.WriteLine("GetCategoryIdByName Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then connection.Close()
        End Try
        Return -1
    End Function

End Class
