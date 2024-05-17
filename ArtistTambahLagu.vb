Imports System.ComponentModel
Imports MySql.Data.MySqlClient

Public Class ArtistTambahLagu

    Public Sub bersihkan()
        TextBox1.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        PictureBox1.Image = Nothing
        ComboBox1.SelectedIndex = -1
        Panel6.Visible = False
    End Sub
    ' PATH_SONG = Tempat Simpan lagu
    Public PATH_SONG As String = CurDir() & "\Song\"
    Public PATH_COVER As String = CurDir() & "\Cover\"
    Public songFilePath As String = ""
    Public coverFilePath As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Mngambil nilai dari inputan
        Dim judulLagu As String = TextBox1.Text
        Dim namaArtist As String = TextBox2.Text
        Dim deskripsi As String = TextBox3.Text
        Dim genre As String
        Dim sourceLagu As String = TextBox4.Text
        Dim coverLagu As String = TextBox5.Text

        ' Salin file lagu ke direktori PATH_SONG
        Dim songDestination As String = System.IO.Path.Combine(PATH_SONG, System.IO.Path.GetFileName(sourceLagu))
        If Not String.IsNullOrEmpty(songFilePath) Then
            System.IO.File.Copy(songFilePath, songDestination, True)
        End If

        ' Salin file cover ke direktori PATH_COVER
        Dim coverDestination As String = System.IO.Path.Combine(PATH_COVER, "CoverLagu" & GetUniqueNumber() & System.IO.Path.GetExtension(coverLagu))
        If Not String.IsNullOrEmpty(coverFilePath) Then
            System.IO.File.Copy(coverFilePath, coverDestination, True)
        End If

        ' Validasi input
        If String.IsNullOrEmpty(judulLagu) Or String.IsNullOrEmpty(namaArtist) Or String.IsNullOrEmpty(deskripsi) Or String.IsNullOrEmpty(sourceLagu) Or String.IsNullOrEmpty(coverLagu) Then
            MessageBox.Show("Semua field harus diisi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If ComboBox1.SelectedItem Is Nothing Then
            MessageBox.Show("Genre harus dipilih.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            genre = ComboBox1.SelectedItem.ToString()
        End If



        ' Koneksi ke database
        koneksi()

        ' SQL Query untuk menyimpan data
        Dim query As String = "INSERT INTO lagu (nama_artist, judul_lagu, deskripsi, cover, genre, source) VALUES (@nama_artist, @judul, @deskripsi, @cover, @genre, @source)"
        CMD = New MySqlCommand(query, CONN)
        CMD.Parameters.AddWithValue("@nama_artist", namaArtist)
        CMD.Parameters.AddWithValue("@judul", judulLagu)
        CMD.Parameters.AddWithValue("@deskripsi", deskripsi)
        CMD.Parameters.AddWithValue("@genre", genre)
        CMD.Parameters.AddWithValue("@source", System.IO.Path.GetFileName(sourceLagu)) ' Menggunakan hanya nama file, bukan path lengkap
        CMD.Parameters.AddWithValue("@cover", System.IO.Path.GetFileName(coverDestination)) ' Menggunakan hanya nama file, bukan path lengkap

        Try
            CMD.ExecuteNonQuery()
            MessageBox.Show("Lagu berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan lagu: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try
        bersihkan()
        ArtistHome.TampilkanData()
    End Sub




    Private Sub TextBox4_Click(sender As Object, e As EventArgs) Handles TextBox4.Click
        ' Membuat instance dari OpenFileDialog
        Dim openFileDialog As New OpenFileDialog()

        ' Mengatur filter untuk file MP3
        openFileDialog.Filter = "MP3 Files (*.mp3)|*.mp3|All Files (*.*)|*.*"
        openFileDialog.Title = "Pilih file lagu"

        ' Menampilkan OpenFileDialog dan mengambil hasilnya
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Mendapatkan nama file saja (tanpa path) dan menampilkannya di TextBox4
            TextBox4.Text = System.IO.Path.GetFileName(openFileDialog.FileName)
            songFilePath = openFileDialog.FileName
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' Membuat instance dari OpenFileDialog
        Dim openFileDialog As New OpenFileDialog()

        ' Mengatur filter untuk file gambar
        openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png|All Files (*.*)|*.*"
        openFileDialog.Title = "Pilih gambar"

        ' Menampilkan OpenFileDialog dan mengambil hasilnya
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Menampilkan gambar yang dipilih di PictureBox
            PictureBox1.Image = Image.FromFile(openFileDialog.FileName)
            ' Mengatur PictureBox untuk menampilkan gambar dengan ukuran sesuai
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            ' Menampilkan nama file beserta ekstensinya di TextBox5
            TextBox5.Text = System.IO.Path.GetFileName(openFileDialog.FileName)
            coverFilePath = openFileDialog.FileName
            ' Mengecek apakah gambar kosong
            Panel6.Visible = True
        End If



    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Panel2.Visible = Not String.IsNullOrEmpty(TextBox1.Text)
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Panel1.Visible = Not String.IsNullOrEmpty(TextBox2.Text)
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Panel3.Visible = Not String.IsNullOrEmpty(TextBox3.Text)
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Panel4.Visible = Not String.IsNullOrEmpty(TextBox4.Text)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Panel5.Visible = Not String.IsNullOrEmpty(ComboBox1.SelectedItem)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        bersihkan()

    End Sub





End Class
