Imports MySql.Data.MySqlClient
Imports System.IO

''' <summary>
''' Business Logic & Data Access Layer for Dashboard3_Form.
''' Handles the parameterized SELECT against `barangayinformation`,
''' safe LONGBLOB → Image conversion, and structured error handling.
''' Contains zero UI references.
''' </summary>
Public Class Dashboard3Logic

    ' =========================================================================
    '  DATA TRANSFER OBJECT
    ' =========================================================================

    ''' <summary>
    ''' Carries the single barangay information row back to the UI layer.
    ''' All fields have safe defaults so the form never receives a null.
    ''' </summary>
    Public Class BarangayInfo
        Public Property BarangayName As String = "Barangay Maly"
        Public Property Mission As String = String.Empty
        Public Property Vision As String = String.Empty
        Public Property LogoBytes As Byte() = Nothing
        Public Property LoadedFromDb As Boolean = False
    End Class

    ' =========================================================================
    '  MAIN DATA ACCESS METHOD
    ' =========================================================================

    ''' <summary>
    ''' Executes a parameterized SELECT to fetch the first (and typically only)
    ''' active row from the `barangayinformation` table.
    ''' Returns a fully initialised BarangayInfo with safe defaults on any
    ''' error so the UI layer never has to handle a Nothing reference.
    ''' </summary>
    Public Function GetBarangayInformation() As BarangayInfo
        Dim info As New BarangayInfo()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                Debug.WriteLine("Dashboard3Logic: Cannot obtain DB connection.")
                Return info
            End If

            ' ------------------------------------------------------------------
            '  Parameterized query — LIMIT 1 because the table holds a single
            '  LGU profile row. Using @Limit as a parameter keeps the query
            '  consistent with the project's ADO.NET conventions.
            ' ------------------------------------------------------------------
            Const query As String =
                "SELECT BarangayName, Logo, Mission, Vision " &
                "FROM barangayinformation " &
                "LIMIT @Limit"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Limit", 1)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then

                        ' BarangayName
                        Dim nameOrdinal As Integer = reader.GetOrdinal("BarangayName")
                        If Not reader.IsDBNull(nameOrdinal) Then
                            Dim dbName As String = reader.GetString(nameOrdinal).Trim()
                            If Not String.IsNullOrWhiteSpace(dbName) Then
                                info.BarangayName = dbName
                            End If
                        End If

                        ' Mission
                        Dim missionOrdinal As Integer = reader.GetOrdinal("Mission")
                        If Not reader.IsDBNull(missionOrdinal) Then
                            info.Mission = reader.GetString(missionOrdinal).Trim()
                        End If

                        ' Vision
                        Dim visionOrdinal As Integer = reader.GetOrdinal("Vision")
                        If Not reader.IsDBNull(visionOrdinal) Then
                            info.Vision = reader.GetString(visionOrdinal).Trim()
                        End If

                        ' Logo LONGBLOB — read raw bytes; conversion happens in BytesToImage()
                        Dim logoOrdinal As Integer = reader.GetOrdinal("Logo")
                        If Not reader.IsDBNull(logoOrdinal) Then
                            Dim byteCount As Long = reader.GetBytes(logoOrdinal, 0, Nothing, 0, 0)
                            If byteCount > 0 Then
                                Dim buffer(CInt(byteCount) - 1) As Byte
                                reader.GetBytes(logoOrdinal, 0, buffer, 0, CInt(byteCount))
                                info.LogoBytes = buffer
                            End If
                        End If

                        info.LoadedFromDb = True
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            Debug.WriteLine("Dashboard3Logic — MySQL error: " & ex.Message)
        Catch ex As Exception
            Debug.WriteLine("Dashboard3Logic — General error: " & ex.Message)
        Finally
            ' Connection is ALWAYS closed, even when an exception is thrown
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return info
    End Function

    ' =========================================================================
    '  IMAGE CONVERSION UTILITY
    ' =========================================================================

    ''' <summary>
    ''' Safely converts a raw byte array (LONGBLOB) back to a
    ''' System.Drawing.Image suitable for direct assignment to a PictureBox.
    '''
    ''' IMPORTANT — GDI+ contract:
    ''' The MemoryStream underlying the returned Image must remain open and
    ''' undisposed for the entire lifetime of the Image object.  The stream
    ''' is therefore intentionally NOT wrapped in a Using block here.
    ''' The Image (and thus the stream) will be reclaimed by the GC when the
    ''' PictureBox image is replaced or the form is disposed.
    '''
    ''' Returns Nothing when the array is null, empty, or not a valid image
    ''' format — callers must substitute a default/fallback image.
    ''' </summary>
    Public Function BytesToImage(logoBytes As Byte()) As Image
        If logoBytes Is Nothing OrElse logoBytes.Length = 0 Then
            Return Nothing
        End If

        Try
            Dim ms As New MemoryStream(logoBytes)
            Return Image.FromStream(ms)
        Catch ex As ArgumentException
            Debug.WriteLine("Dashboard3Logic.BytesToImage — invalid image bytes: " & ex.Message)
            Return Nothing
        Catch ex As Exception
            Debug.WriteLine("Dashboard3Logic.BytesToImage — error: " & ex.Message)
            Return Nothing
        End Try
    End Function

End Class
