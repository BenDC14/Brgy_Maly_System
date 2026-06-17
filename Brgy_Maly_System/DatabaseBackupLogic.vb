Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.IO

Public Class DatabaseBackupLogic

    Private Const DB_HOST As String = "localhost"
    Private Const DB_USER As String = "root"
    Private Const DB_PASSWORD As String = ""
    Private Const DB_NAME As String = "barangay_maly"

    Public Class BackupResult
        Public Property IsSuccess As Boolean
        Public Property FileName As String
        Public Property FilePath As String
        Public Property Status As String
        Public Property ErrorMessage As String

        Public Sub New()
            IsSuccess = False
            FileName = ""
            FilePath = ""
            Status = "Failed"
            ErrorMessage = ""
        End Sub
    End Class

    Public Function ValidateBackupInput(fileName As String, folderPath As String) As String
        If String.IsNullOrWhiteSpace(fileName) Then
            Return "Please enter a file name."
        End If

        For Each invalidChar As Char In Path.GetInvalidFileNameChars()
            If fileName.Contains(invalidChar) Then
                Return "File name contains invalid characters."
            End If
        Next

        If String.IsNullOrWhiteSpace(folderPath) Then
            Return "Please select a backup folder."
        End If

        If Not Directory.Exists(folderPath) Then
            Return "Selected backup folder does not exist."
        End If

        Return ""
    End Function

    Public Function BuildBackupFilePath(folderPath As String, fileName As String) As String
        Dim cleanFileName As String = fileName.Trim()

        If Not cleanFileName.ToLower().EndsWith(".sql") Then
            cleanFileName &= ".sql"
        End If

        Return Path.Combine(folderPath, cleanFileName)
    End Function

    Public Function StartBackup(fileName As String, folderPath As String, description As String, backupBy As Integer) As BackupResult
        Dim result As New BackupResult()
        Dim backupFilePath As String = BuildBackupFilePath(folderPath, fileName)

        result.FileName = Path.GetFileName(backupFilePath)
        result.FilePath = backupFilePath

        Try
            Dim validationError As String = ValidateBackupInput(fileName, folderPath)

            If Not String.IsNullOrWhiteSpace(validationError) Then
                result.Status = "Failed"
                result.ErrorMessage = validationError
                SaveBackupLog(backupBy, result.FilePath, result.FileName, description, result.Status, result.ErrorMessage)
                Return result
            End If

            Dim mysqldumpPath As String = FindMySqlDumpPath()

            If String.IsNullOrWhiteSpace(mysqldumpPath) Then
                result.Status = "Failed"
                result.ErrorMessage = "mysqldump.exe was not found. Please install MySQL/XAMPP or add mysqldump to PATH."
                SaveBackupLog(backupBy, result.FilePath, result.FileName, description, result.Status, result.ErrorMessage)
                Return result
            End If

            Dim arguments As String =
                "--host=" & DB_HOST & " " &
                "--user=" & DB_USER & " " &
                If(String.IsNullOrEmpty(DB_PASSWORD), "", "--password=" & DB_PASSWORD & " ") &
                "--databases " & DB_NAME & " " &
                "--result-file=""" & backupFilePath & """"

            Dim processInfo As New ProcessStartInfo()
            processInfo.FileName = mysqldumpPath
            processInfo.Arguments = arguments
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = True
            processInfo.RedirectStandardError = True
            processInfo.RedirectStandardOutput = True

            Using process As Process = Process.Start(processInfo)
                Dim errorOutput As String = process.StandardError.ReadToEnd()
                process.WaitForExit()

                If process.ExitCode = 0 AndAlso File.Exists(backupFilePath) Then
                    result.IsSuccess = True
                    result.Status = "Success"
                    result.ErrorMessage = ""
                Else
                    result.IsSuccess = False
                    result.Status = "Failed"
                    result.ErrorMessage = If(String.IsNullOrWhiteSpace(errorOutput), "Backup process failed.", errorOutput)
                End If
            End Using

            SaveBackupLog(backupBy, result.FilePath, result.FileName, description, result.Status, result.ErrorMessage)

        Catch ex As Exception
            result.IsSuccess = False
            result.Status = "Failed"
            result.ErrorMessage = ex.Message
            SaveBackupLog(backupBy, result.FilePath, result.FileName, description, result.Status, result.ErrorMessage)
        End Try

        Return result
    End Function

    Private Function FindMySqlDumpPath() As String
        Dim commonPaths As String() = {
            "C:\xampp\mysql\bin\mysqldump.exe",
            "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe",
            "C:\Program Files\MySQL\MySQL Server 5.7\bin\mysqldump.exe"
        }

        For Each path As String In commonPaths
            If File.Exists(path) Then
                Return path
            End If
        Next

        Return "mysqldump.exe"
    End Function

    Public Function SaveBackupLog(backupBy As Integer, filePath As String, fileName As String, description As String, backupStatus As String, errorMessage As String) As Boolean
        Dim connection As MySqlConnection = Nothing

        Try
            connection = ConnectDB_Module.GetDatabaseConnection()
            If connection Is Nothing Then Return False

            Dim query As String =
                "INSERT INTO backuplogs " &
                "(BackupBy, FilePath, FileName, Description, BackupDateTime, BackupStatus, ErrorMessage) " &
                "VALUES " &
                "(@BackupBy, @FilePath, @FileName, @Description, NOW(), @BackupStatus, @ErrorMessage)"

            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@BackupBy", backupBy)
                cmd.Parameters.AddWithValue("@FilePath", filePath)
                cmd.Parameters.AddWithValue("@FileName", fileName)
                cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrWhiteSpace(description), DBNull.Value, description))
                cmd.Parameters.AddWithValue("@BackupStatus", backupStatus)
                cmd.Parameters.AddWithValue("@ErrorMessage", If(String.IsNullOrWhiteSpace(errorMessage), DBNull.Value, errorMessage))

                cmd.ExecuteNonQuery()
                ' === OPTIONAL: Mirror to global audit trail ===
                GlobalAuditLogger.Log("DatabaseBackup_Form", "DATABASE BACKUP",
                LogInForm.CurrentUsername & " performed a database backup: " & fileName & " [Status: " & backupStatus & "]")

                Return True
            End Using

        Catch ex As Exception
            Debug.WriteLine("SaveBackupLog Error: " & ex.Message)
            Return False
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

End Class