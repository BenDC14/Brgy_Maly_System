Imports MySql.Data.MySqlClient
Imports System.IO

''' <summary>
''' Business Logic Layer for Dashboard2_Form.
''' Handles all parameterized ADO.NET queries and safe image conversion.
''' No UI references — completely decoupled from the form.
''' </summary>
Public Class Dashboard2Logic

    ' =========================================================================
    '  DATA TRANSFER OBJECTS
    ' =========================================================================

    ''' <summary>
    ''' Represents one official's data as returned from the database.
    ''' </summary>
    Public Class OfficialData
        Public Property FirstName As String = String.Empty
        Public Property LastName As String = String.Empty
        Public Property Position As String = String.Empty
        Public Property PhotoBytes As Byte() = Nothing
        Public Property IsVacant As Boolean = False

        ''' <summary>Combined display name — "Vacant" when no record exists.</summary>
        Public ReadOnly Property FullName As String
            Get
                If IsVacant Then Return "Vacant"
                Dim fn As String = FirstName.Trim()
                Dim ln As String = LastName.Trim()
                If String.IsNullOrWhiteSpace(fn) AndAlso String.IsNullOrWhiteSpace(ln) Then
                    Return "Vacant"
                End If
                Return (fn & " " & ln).Trim()
            End Get
        End Property
    End Class

    ''' <summary>
    ''' Aggregated result holding all positions needed by Dashboard2_Form.
    ''' Kagawads has exactly 8 items; SkKagawads has exactly 7 items —
    ''' vacant slots are pre-filled so the form never needs a bounds check.
    ''' </summary>
    Public Class DashboardOfficials
        Public Property Chairman As OfficialData = New OfficialData() With {.IsVacant = True, .Position = "Barangay Captain"}
        Public Property Secretary As OfficialData = New OfficialData() With {.IsVacant = True, .Position = "Barangay Secretary"}
        Public Property Treasurer As OfficialData = New OfficialData() With {.IsVacant = True, .Position = "Barangay Treasurer"}
        Public Property Administrator As OfficialData = New OfficialData() With {.IsVacant = True, .Position = "Barangay Adminstrator"}
        Public Property SkChairman As OfficialData = New OfficialData() With {.IsVacant = True, .Position = "SK Chairman"}

        ''' <summary>Always 8 items — index 0 = Kagawad 1 … index 7 = Kagawad 8.</summary>
        Public Property Kagawads As New List(Of OfficialData)()

        ''' <summary>Always 7 items — index 0 = SK Kagawad 1 … index 6 = SK Kagawad 7.</summary>
        Public Property SkKagawads As New List(Of OfficialData)()
    End Class

    ' =========================================================================
    '  MAIN QUERY METHOD
    ' =========================================================================

    ''' <summary>
    ''' Executes a single parameterized SELECT to retrieve all active officials
    ''' and distributes them into the DashboardOfficials model.
    ''' Returns a fully initialised (possibly all-vacant) model on any error.
    ''' </summary>
    Public Function GetActiveDashboardOfficials() As DashboardOfficials
        Dim officials As New DashboardOfficials()
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then
                Debug.WriteLine("Dashboard2Logic: Unable to obtain DB connection.")
                Return BuildVacantResult()
            End If

            ' ------------------------------------------------------------------
            '  Single parameterized query — IsActive = @IsActive prevents
            '  SQL injection even though the value is a literal.
            '  ORDER BY ensures Kagawad records come back in a consistent
            '  insertion order so slot 1 always maps to the earliest entry.
            ' ------------------------------------------------------------------
            Const query As String =
                "SELECT FirstName, LastName, Position, PhotoPath " &
                "FROM barangayofficials " &
                "WHERE IsActive = @IsActive " &
                "ORDER BY Position, OfficialId ASC"

            ' Bucket each position into its own list before mapping to slots
            Dim buckets As New Dictionary(Of String, List(Of OfficialData))(
                StringComparer.OrdinalIgnoreCase) From {
                {"Barangay Captain", New List(Of OfficialData)()},
                {"Barangay Kagawad", New List(Of OfficialData)()},
                {"Barangay Secretary", New List(Of OfficialData)()},
                {"Barangay Treasurer", New List(Of OfficialData)()},
                {"Barangay Adminstrator", New List(Of OfficialData)()},
                {"SK Chairman", New List(Of OfficialData)()},
                {"Barangay SK Kagawad", New List(Of OfficialData)()}
            }

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@IsActive", 1)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim pos As String = reader("Position").ToString().Trim()
                        If Not buckets.ContainsKey(pos) Then Continue While

                        Dim data As New OfficialData() With {
                            .FirstName = reader("FirstName").ToString(),
                            .LastName = reader("LastName").ToString(),
                            .Position = pos,
                            .IsVacant = False
                        }

                        ' Safely read the BLOB photo column
                        Dim photoOrdinal As Integer = reader.GetOrdinal("PhotoPath")
                        If Not reader.IsDBNull(photoOrdinal) Then
                            data.PhotoBytes = CType(reader(photoOrdinal), Byte())
                        End If

                        buckets(pos).Add(data)
                    End While
                End Using
            End Using

            ' ------------------------------------------------------------------
            '  Map buckets → result fields
            ' ------------------------------------------------------------------
            officials.Chairman = SlotOrVacant(buckets("Barangay Captain"), 0, "Barangay Captain")
            officials.Secretary = SlotOrVacant(buckets("Barangay Secretary"), 0, "Barangay Secretary")
            officials.Treasurer = SlotOrVacant(buckets("Barangay Treasurer"), 0, "Barangay Treasurer")
            officials.Administrator = SlotOrVacant(buckets("Barangay Adminstrator"), 0, "Barangay Adminstrator")
            officials.SkChairman = SlotOrVacant(buckets("SK Chairman"), 0, "SK Chairman")

            ' Exactly 8 Kagawad slots
            For i As Integer = 0 To 7
                officials.Kagawads.Add(SlotOrVacant(buckets("Barangay Kagawad"), i, "Barangay Kagawad"))
            Next

            ' Exactly 7 SK Kagawad slots
            For i As Integer = 0 To 6
                officials.SkKagawads.Add(SlotOrVacant(buckets("Barangay SK Kagawad"), i, "Barangay SK Kagawad"))
            Next

        Catch ex As MySqlException
            Debug.WriteLine("Dashboard2Logic — MySQL error: " & ex.Message)
            Return BuildVacantResult()
        Catch ex As Exception
            Debug.WriteLine("Dashboard2Logic — General error: " & ex.Message)
            Return BuildVacantResult()
        Finally
            If connection IsNot Nothing AndAlso
               connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return officials
    End Function

    ' =========================================================================
    '  IMAGE CONVERSION
    ' =========================================================================

    ''' <summary>
    ''' Safely converts a byte array (stored BLOB) back to a System.Drawing.Image.
    ''' Returns Nothing when the array is null/empty or the bytes are not a
    ''' valid image — the caller should substitute the default avatar.
    ''' IMPORTANT: The underlying MemoryStream is intentionally NOT disposed
    ''' here because GDI+ requires it to remain open for the lifetime of the
    ''' returned Image object.
    ''' </summary>
    Public Function BytesToImage(photoBytes As Byte()) As Image
        If photoBytes Is Nothing OrElse photoBytes.Length = 0 Then Return Nothing
        Try
            Return Image.FromStream(New IO.MemoryStream(photoBytes))
        Catch ex As Exception
            Debug.WriteLine("Dashboard2Logic.BytesToImage error: " & ex.Message)
            Return Nothing
        End Try
    End Function

    ' =========================================================================
    '  PRIVATE HELPERS
    ' =========================================================================

    ''' <summary>
    ''' Returns the item at <paramref name="index"/> from <paramref name="list"/>,
    ''' or a pre-built vacant placeholder when the index is out of range.
    ''' </summary>
    Private Function SlotOrVacant(list As List(Of OfficialData),
                                   index As Integer,
                                   position As String) As OfficialData
        If index >= 0 AndAlso index < list.Count Then
            Return list(index)
        End If
        Return New OfficialData() With {
            .IsVacant = True,
            .Position = position
        }
    End Function

    ''' <summary>
    ''' Returns a DashboardOfficials instance where every position is vacant.
    ''' Used as the safe fallback when the database is unreachable.
    ''' </summary>
    Private Function BuildVacantResult() As DashboardOfficials
        Dim d As New DashboardOfficials()
        For i As Integer = 0 To 7
            d.Kagawads.Add(New OfficialData() With {.IsVacant = True, .Position = "Barangay Kagawad"})
        Next
        For i As Integer = 0 To 6
            d.SkKagawads.Add(New OfficialData() With {.IsVacant = True, .Position = "Barangay SK Kagawad"})
        Next
        Return d
    End Function

End Class
