Imports System.IO
Imports MySql.Data.MySqlClient

Public Class Lagu
    Public Sub LoadData(ByVal idLagu As Integer)
        ' Query SQL untuk mendapatkan detail lagu dari database berdasarkan ID
        Dim query As String = "SELECT cover, judul_lagu, nama_artist, source FROM lagu WHERE id_lagu = @idLagu"

        Try
            koneksi() ' Buka koneksi ke database
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@idLagu", idLagu)

            RD = CMD.ExecuteReader()

            If RD.Read() Then
                Dim coverPath As String = RD("cover").ToString()
                Dim judulLagu As String = RD("judul_lagu").ToString()
                Dim artistName As String = RD("nama_artist").ToString()
                Dim sourceLagu As String = RD("source").ToString()

                ' Mengatur cover lagu
                If File.Exists(PATH_COVER & coverPath) Then
                    PictureBox1.Image = Image.FromFile(PATH_COVER & coverPath)
                End If

                ' Mengatur judul lagu dan nama artist
                LinkLabel1.Text = judulLagu
                Label1.Text = artistName

                ' Mengatur ID lagu di TextBox tersembunyi
                id_song.Text = idLagu.ToString()
            End If
        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadForm(sender As Object, e As EventArgs) Handles MyBase.Load
        LinkLabel1.LinkBehavior = LinkBehavior.NeverUnderline
        PictureBox2.Visible = False
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox2.Visible = True
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox2.Visible = False
    End Sub

    Private Sub LinkLabel1_MouseEnter(sender As Object, e As EventArgs) Handles LinkLabel1.MouseEnter
        LinkLabel1.LinkColor = Color.Red
    End Sub

    Private Sub LinkLabel1_MouseLeave(sender As Object, e As EventArgs) Handles LinkLabel1.MouseLeave
        LinkLabel1.LinkColor = Color.Black
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' Ambil ID lagu dari TextBox
        Dim idLagu As String = id_song.Text

        ' Panggil fungsi untuk memutar lagu
        PlaySong(idLagu)
        HalamanQueue.DataGridView1.Rows.Add(idLagu)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim idLagu As String = id_song.Text
        PlaySong(idLagu)
    End Sub

    ' Fungsi untuk memutar lagu berdasarkan ID
    Private Sub PlaySong(ByVal idLagu As String)
        koneksi()

        Dim query As String = "SELECT source FROM lagu WHERE id_lagu = @idLagu"
        CMD = New MySqlCommand(query, CONN)
        CMD.Parameters.AddWithValue("@idLagu", idLagu)

        Try
            RD = CMD.ExecuteReader()

            If RD.Read() Then
                Dim sourceLagu As String = RD("source").ToString()
                Dim pathLagu As String = PATH_SONG & sourceLagu

                ' Memeriksa apakah file lagu ada
                If File.Exists(pathLagu) Then
                    ' Atur source lagu ke kontrol MediaPlayer
                    Homapage.AxWindowsMediaPlayer1.URL = pathLagu
                    ' Memutar lagu
                    Homapage.AxWindowsMediaPlayer1.Ctlcontrols.play()
                    HalamanQueue.DataGridView1.Rows.Add(sourceLagu)
                Else
                    MessageBox.Show("File lagu tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

            RD.Close()
        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If RD IsNot Nothing AndAlso Not RD.IsClosed Then
                RD.Close()
            End If
            If CONN IsNot Nothing AndAlso CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub
End Class
