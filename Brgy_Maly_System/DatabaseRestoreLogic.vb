Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.IO

Public Class DatabaseRestoreLogic

    Private Const DB_HOST As String = "localhost"
    Private Const DB_USER As String = "root"
    Private Const DB_PASSWORD As String = ""
    Private Const DB_NAME As String = "barangay_maly"

    Public Class BackupFileInfo
        Public Property BackupId As Integer
        Public Property FileName As String
        Public Property FilePath As String
        Public Property BackupDateTime As String
        Public Property BackupStatus As String

        Public Sub New()
            BackupId = -1
            FileName = ""
            FilePath = ""
            BackupDateTime = ""
            BackupStatus = ""
        End Sub
    End Class

    Public Class RestoreResult
        Public Property IsSuccess As Boolean
        Public Property BackupId As Integer
        Public Property RestoreFileName As String
        Public Property RestoreFilePath As String
        Public Property RestoreStatus As String
        Public Property ErrorMessage As String

        Public Sub New()
            IsSuccess = False
            BackupId = -1
            RestoreFileName = ""
            RestoreFilePath = ""
            RestoreStatus = "Failed"
            ErrorMessage = ""
        End Sub
    End Class

    Public Function ValidateSqlFile(filePath As String) As String
        If String.IsNullOrWhiteSpace(filePath) Then
            Return "Please select a backup SQL file."
        End If

        If Not File.Exists(filePath) Then
            Return "Selected file does not exist."
        End If

        If Path.GetExtension(filePath).ToLower() <> ".sql" Then
            Return "Invalid file type. Please select a .sql backup file only."
        End If

        Dim fileInfo As New FileInfo(filePath)

        If fileInfo.Length <= 0 Then
            Return "Selected SQL file is empty."
        End If

        Dim firstText As String = ""

        Using reader As New StreamReader(filePath)
            Dim bufferLength As Integer = CInt(Math.Min(5000, fileInfo.Length))
            Dim buffer(bufferLength - 1) As Char
            reader.Read(buffer, 0, bufferLength)
            firstText = New String(buffer)
        End Using

        If Not firstText.ToLower().Contains("create database") AndAlso
           Not firstText.ToLower().Contains("create table") AndAlso
           Not firstText.ToLower().Contains("insert into") Then
            Return "The selected file does not look like a valid SQL backup file."
        End If

        Return ""
    End Function

    Public Function GetBackupFileInfo(filePath As String) As BackupFileInfo
        Dim info As New BackupFileInfo()
        Dim connection As MySqlConnection = Nothing

        Try
            info.FilePath = filePath
            info.FileName = Path.GetFileName(filePath)

            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return info

            Dim query As String =
                "SELECT BackupId, FileName, FilePath, BackupDateTime, BackupStatus " &
                "FROM backuplogs " &
                "WHERE FilePath = @FilePath OR FileName = @FileName " &
                "ORDER BY BackupDateTime DESC " &
                "LIMIT 1"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@FilePath", filePath)
                cmd.Parameters.AddWithValue("@FileName", info.FileName)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        info.BackupId = CInt(reader("BackupId"))
                        info.FileName = reader("FileName").ToString()
                        info.FilePath = reader("FilePath").ToString()
                        info.BackupDateTime = If(IsDBNull(reader("BackupDateTime")), "", CDate(reader("BackupDateTime")).ToString("yyyy-MM-dd hh:mm tt"))
                        info.BackupStatus = If(IsDBNull(reader("BackupStatus")), "", reader("BackupStatus").ToString())
                    Else
                        info.BackupDateTime = File.GetCreationTime(filePath).ToString("yyyy-MM-dd hh:mm tt")
                        info.BackupStatus = "Unknown / External SQL File"
                    End If
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine("GetBackupFileInfo Error: " & ex.Message)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

        Return info
    End Function

    Public Function RestoreDatabase(filePath As String, restoredBy As Integer) As RestoreResult
        Dim result As New RestoreResult()
        result.RestoreFilePath = filePath
        result.RestoreFileName = Path.GetFileName(filePath)

        Try
            Dim validationError As String = ValidateSqlFile(filePath)

            If Not String.IsNullOrWhiteSpace(validationError) Then
                result.RestoreStatus = "Failed"
                result.ErrorMessage = validationError
                SaveRestoreLog(result.BackupId, restoredBy, result.RestoreFileName, result.RestoreFilePath, result.RestoreStatus, result.ErrorMessage)
                Return result
            End If

            Dim backupInfo As BackupFileInfo = GetBackupFileInfo(filePath)
            result.BackupId = backupInfo.BackupId

            Dim mysqlPath As String = FindMySqlPath()

            If String.IsNullOrWhiteSpace(mysqlPath) Then
                result.RestoreStatus = "Failed"
                result.ErrorMessage = "mysql.exe was not found. Please install MySQL/XAMPP or add mysql.exe to PATH."
                SaveRestoreLog(result.BackupId, restoredBy, result.RestoreFileName, result.RestoreFilePath, result.RestoreStatus, result.ErrorMessage)
                Return result
            End If

            Dim arguments As String =
                "--host=" & DB_HOST & " " &
                "--user=" & DB_USER & " " &
                If(String.IsNullOrEmpty(DB_PASSWORD), "", "--password=" & DB_PASSWORD & " ") &
                DB_NAME

            Dim processInfo As New ProcessStartInfo()
            processInfo.FileName = mysqlPath
            processInfo.Arguments = arguments
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = True
            processInfo.RedirectStandardInput = True
            processInfo.RedirectStandardError = True
            processInfo.RedirectStandardOutput = True

            Using process As Process = Process.Start(processInfo)
                Dim sqlText As String = File.ReadAllText(filePath)
                process.StandardInput.WriteLine(sqlText)
                process.StandardInput.Close()

                Dim errorOutput As String = process.StandardError.ReadToEnd()
                process.WaitForExit()

                If process.ExitCode = 0 Then
                    result.IsSuccess = True
                    result.RestoreStatus = "Success"
                    result.ErrorMessage = ""
                Else
                    result.IsSuccess = False
                    result.RestoreStatus = "Failed"
                    result.ErrorMessage = If(String.IsNullOrWhiteSpace(errorOutput), "Restore process failed.", errorOutput)
                End If
            End Using

            SaveRestoreLog(result.BackupId, restoredBy, result.RestoreFileName, result.RestoreFilePath, result.RestoreStatus, result.ErrorMessage)

        Catch ex As Exception
            result.IsSuccess = False
            result.RestoreStatus = "Failed"
            result.ErrorMessage = ex.Message
            SaveRestoreLog(result.BackupId, restoredBy, result.RestoreFileName, result.RestoreFilePath, result.RestoreStatus, result.ErrorMessage)
        End Try

        Return result
    End Function

    Private Function FindMySqlPath() As String
        Dim commonPaths As String() = {
            "C:\xampp\mysql\bin\mysql.exe",
            "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe",
            "C:\Program Files\MySQL\MySQL Server 5.7\bin\mysql.exe"
        }

        For Each path As String In commonPaths
            If File.Exists(path) Then
                Return path
            End If
        Next

        Return "mysql.exe"
    End Function

    Public Function SaveRestoreLog(backupId As Integer, restoredBy As Integer, restoreFileName As String, restoreFilePath As String, restoreStatus As String, errorMessage As String) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim query As String =
                "INSERT INTO restorelogs " &
                "(BackupId, RestoredBy, RestoreDateTime, RestoreFileName, RestoreFilePath, RestoreStatus, ErrorMessage) " &
                "VALUES " &
                "(@BackupId, @RestoredBy, NOW(), @RestoreFileName, @RestoreFilePath, @RestoreStatus, @ErrorMessage)"

            Using cmd As New MySqlCommand(query, connection)
                If backupId > 0 Then
                    cmd.Parameters.AddWithValue("@BackupId", backupId)
                Else
                    cmd.Parameters.AddWithValue("@BackupId", DBNull.Value)
                End If

                cmd.Parameters.AddWithValue("@RestoredBy", restoredBy)
                cmd.Parameters.AddWithValue("@RestoreFileName", restoreFileName)
                cmd.Parameters.AddWithValue("@RestoreFilePath", restoreFilePath)
                cmd.Parameters.AddWithValue("@RestoreStatus", restoreStatus)
                cmd.Parameters.AddWithValue("@ErrorMessage", If(String.IsNullOrWhiteSpace(errorMessage), DBNull.Value, errorMessage))

                cmd.ExecuteNonQuery()
                ' === OPTIONAL: Mirror to global audit trail ===
                GlobalAuditLogger.Log("DatabaseRestore_Form", "DATABASE RESTORE",
                LogInForm.CurrentUsername & " restored database from: " & restoreFileName & " [Status: " & restoreStatus & "]")

                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("SaveRestoreLog Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

End Class