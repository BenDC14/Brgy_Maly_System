Imports MySql.Data.MySqlClient
Imports System.Configuration

''' <summary>
''' Database Connection Module with Enhanced Security
''' </summary>
Module ConnectDB_Module
    ''' <summary>
    ''' Get secure database connection with timeout and pooling
    ''' </summary>
    Public Function GetDatabaseConnection() As MySqlConnection
        Try
            ' === CONNECTION STRING WITH SECURITY OPTIONS ===
            Dim connectionString As String = "server=localhost;" &
                                            "userid=root;" &
                                            "password=;" &
                                            "database=barangay_maly;" &
                                            "Connection Timeout=30;" &
                                            "Pooling=True;" &
                                            "MinimumPoolSize=5;" &
                                            "MaximumPoolSize=10;" &
                                            "SSL Mode=Preferred"

            Dim connection As New MySqlConnection(connectionString)
            connection.Open()
            Return connection

        Catch ex As MySqlException
            MessageBox.Show("Database connection failed: " & ex.Message,
                          "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred: " & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Close database connection safely
    ''' </summary>
    Public Sub CloseConnection(connection As MySqlConnection)
        If connection IsNot Nothing Then
            Try
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
                connection.Dispose()
            Catch ex As Exception
                ' Log error but don't throw
                Debug.WriteLine("Error closing connection: " & ex.Message)
            End Try
        End If
    End Sub

End Module
