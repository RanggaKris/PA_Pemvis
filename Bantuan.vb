Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Module Bantuan

    Public isLogin As Boolean = False
    Public loggedInUsername As String = ""
    Public PATH_COVER As String = CurDir() & "\Cover\"
    Public PATH_ARTIST As String = CurDir() & "\Artist\"
    Public PATH_SONG As String = CurDir() & "\Song\"







    Public CONN As MySqlConnection
    Public CMD As MySqlCommand
    Public RD As MySqlDataReader
    Public DA As MySqlDataAdapter
    Public DS As DataSet
    Public STR As String

    Public Function GetUniqueNumber() As Integer
        Static rng As New Random()
        Return rng.Next(1, 1000)
    End Function

    Sub koneksi()
        Try
            Dim STR As String =
            "server=localhost;userid=root;password=;database=pa"
            'Ganti nama database sesuaikan dengan nama database kalian
            CONN = New MySqlConnection(STR)
            If CONN.State = ConnectionState.Closed Then
                CONN.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Module