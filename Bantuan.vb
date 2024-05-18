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


    Public Function GetSongs() As DataTable
        Dim dt As New DataTable()
        Try
            koneksi()
            Dim query As String = "SELECT id_lagu, cover, judul_lagu, nama_artist, source FROM lagu"
            CMD = New MySqlCommand(query, CONN)
            RD = CMD.ExecuteReader()
            dt.Load(RD)
        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If RD IsNot Nothing AndAlso Not RD.IsClosed Then RD.Close()
            If CONN IsNot Nothing AndAlso CONN.State = ConnectionState.Open Then CONN.Close()
        End Try
        Return dt
    End Function

    Public Function GetSongById(ByVal idLagu As Integer) As DataRow
        Dim dt As New DataTable()
        Try
            koneksi()
            Dim query As String = "SELECT cover, judul_lagu, nama_artist, source FROM lagu WHERE id_lagu = @idLagu"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@idLagu", idLagu)
            RD = CMD.ExecuteReader()
            dt.Load(RD)
        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If RD IsNot Nothing AndAlso Not RD.IsClosed Then RD.Close()
            If CONN IsNot Nothing AndAlso CONN.State = ConnectionState.Open Then CONN.Close()
        End Try
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        Else
            Return Nothing
        End If
    End Function
End Module