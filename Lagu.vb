Imports System.IO
Imports MySql.Data.MySqlClient

Public Class Lagu
    Inherits UserControl
    Public laguQueue As New Queue(Of String)()

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub LoadData(ByVal row As DataRow)
        If row IsNot Nothing Then
            Dim coverPath As String = row("cover").ToString()
            Dim judulLagu As String = row("judul_lagu").ToString()
            Dim artistName As String = row("nama_artist").ToString()

            If File.Exists(PATH_COVER & coverPath) Then
                PictureBox1.Image = Image.FromFile(PATH_COVER & coverPath)
            End If

            LinkLabel1.Text = judulLagu
            Label1.Text = artistName
            id_song.Text = row("id_lagu").ToString()
        End If
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
        RiwayatPemutaran(idLagu)

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim idLagu As String = id_song.Text
        PlaySong(idLagu)
    End Sub

    ' Fungsi untuk memutar lagu berdasarkan ID
    Private Sub PlaySong(ByVal idLagu As String)
        Dim query As String = "SELECT source FROM lagu WHERE id_lagu = @idLagu"
        Try
            koneksi()
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@idLagu", idLagu)
            RD = CMD.ExecuteReader()

            If RD.Read() Then
                Dim sourceLagu As String = RD("source").ToString()
                Dim pathLagu As String = PATH_SONG & sourceLagu

                If File.Exists(pathLagu) Then
                    Homapage.AxWindowsMediaPlayer1.URL = pathLagu
                    Homapage.AxWindowsMediaPlayer1.Ctlcontrols.play()
                Else
                    MessageBox.Show("File lagu tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If RD IsNot Nothing AndAlso Not RD.IsClosed Then RD.Close()
            If CONN IsNot Nothing AndAlso CONN.State = ConnectionState.Open Then CONN.Close()
        End Try
    End Sub

    Private Sub RiwayatPemutaran(ByVal idLagu As String)
        Dim query As String = "SELECT judul_lagu FROM lagu WHERE id_lagu = @idLagu"
        Try
            koneksi()
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@idLagu", idLagu)
            RD = CMD.ExecuteReader()

            If RD.Read() Then
                Dim judulLagu As String = RD("judul_lagu").ToString()


                Homapage.DataGridView1.Rows.Add(judulLagu)
            End If

        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan saat menambahkan lagu ke queue: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If RD IsNot Nothing AndAlso Not RD.IsClosed Then RD.Close()
            If CONN IsNot Nothing AndAlso CONN.State = ConnectionState.Open Then CONN.Close()
        End Try
    End Sub




End Class
