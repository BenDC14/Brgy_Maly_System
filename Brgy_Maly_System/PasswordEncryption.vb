Imports System.Security.Cryptography
Imports System.Text

''' <summary>
''' Password encryption and verification utility
''' Uses SHA256 hashing for secure password storage
''' </summary>
Public Class PasswordEncryption

    ''' <summary>
    ''' Hash password using SHA256
    ''' </summary>
    Public Shared Function HashPassword(password As String) As String
        Using sha256 As New SHA256Managed()
            Dim hashBytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Return Convert.ToBase64String(hashBytes)
        End Using
    End Function

    ''' <summary>
    ''' Verify password against hash
    ''' </summary>
    Public Shared Function VerifyPassword(password As String, hash As String) As Boolean
        Try
            Dim hashOfInput As String = HashPassword(password)
            Return hashOfInput.Equals(hash, StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try
    End Function

End Class
