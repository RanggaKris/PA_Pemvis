Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class ArtistEditLagu
    Public songFilePath As String = ""
    Public coverFilePath As String = ""
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim judulLagu As String = TextBox1.Text
        Dim namaArtist As String = TextBox2.Text
        Dim deskripsi As String = TextBox3.Text
        Dim genre As String
        Dim sourceLagu As String = TextBox4.Text
        Dim coverLagu As String = TextBox5.Text

        If String.IsNullOrEmpty(deskripsi) Or String.IsNullOrEmpty(judulLagu) Or String.IsNullOrEmpty(namaArtist) Or String.IsNullOrEmpty(sourceLagu) Or String.IsNullOrEmpty(coverLagu) Then
            MessageBox.Show("Semua field harus diisi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If ComboBox1.SelectedItem Is Nothing Then
            MessageBox.Show("Genre harus dipilih.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            genre = ComboBox1.SelectedItem.ToString()
        End If

        koneksi()

        Dim query As String = "UPDATE lagu SET judul_lagu = @judul, nama_artist = @artist, deskripsi = @deskripsi, genre = @genre, cover = @cover, source = @source WHERE id_lagu = @idLagu"
        CMD = New MySqlCommand(query, CONN)
        CMD.Parameters.AddWithValue("@judul", judulLagu)
        CMD.Parameters.AddWithValue("@artist", namaArtist)
        CMD.Parameters.AddWithValue("@deskripsi", deskripsi)
        CMD.Parameters.AddWithValue("@genre", genre)
        CMD.Parameters.AddWithValue("@source", sourceLagu)
        CMD.Parameters.AddWithValue("@cover", coverLagu)
        CMD.Parameters.AddWithValue("@idLagu", txtID.Text)

        Try
            If Not String.IsNullOrEmpty(coverFilePath) Then
                Dim oldCoverPath As String = System.IO.Path.Combine(PATH_COVER, coverLagu)
                If System.IO.File.Exists(oldCoverPath) Then
                    System.IO.File.Delete(oldCoverPath)
                End If
            End If

            If Not String.IsNullOrEmpty(songFilePath) Then
                Dim oldSongPath As String = System.IO.Path.Combine(PATH_SONG, System.IO.Path.GetFileName(sourceLagu))
                If System.IO.File.Exists(oldSongPath) Then
                    System.IO.File.Delete(oldSongPath)
                End If
            End If

            If Not String.IsNullOrEmpty(coverFilePath) Then
                Dim newCoverDestination As String = System.IO.Path.Combine(PATH_COVER, coverLagu)
                System.IO.File.Copy(coverFilePath, newCoverDestination, True)
            End If

            If Not String.IsNullOrEmpty(songFilePath) Then
                Dim newSongDestination As String = System.IO.Path.Combine(PATH_SONG, System.IO.Path.GetFileName(sourceLagu))
                System.IO.File.Copy(songFilePath, newSongDestination, True)
            End If

            CMD.ExecuteNonQuery()
            MessageBox.Show("Data lagu berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Gagal memperbarui data lagu: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try
        ArtistHome.TampilkanData()
    End Sub






    Public Sub TampilkanDataEdit(ByVal idLagu As String)
        ' Koneksi ke database
        koneksi()

        ' SQL Query untuk mengambil data lagu berdasarkan ID
        Dim query As String = "SELECT * FROM lagu WHERE id_lagu = @idLagu"

        ' Buat command untuk eksekusi query
        CMD = New MySqlCommand(query, CONN)
        CMD.Parameters.AddWithValue("@idLagu", idLagu)

        Try
            Dim reader As MySqlDataReader = CMD.ExecuteReader()

            ' Jika data ditemukan, tampilkan informasi lagu di form
            If reader.Read() Then
                TextBox1.Text = reader("judul_lagu").ToString()
                TextBox2.Text = reader("nama_artist").ToString()
                TextBox3.Text = reader("deskripsi").ToString()
                ComboBox1.SelectedItem = reader("genre").ToString()
                TextBox4.Text = reader("source").ToString()
                TextBox5.Text = reader("cover").ToString()

                ' Load gambar dari file cover
                Dim coverFileName As String = reader("cover").ToString()
                If Not String.IsNullOrEmpty(coverFileName) Then
                    PictureBox1.Image = Image.FromFile(System.IO.Path.Combine(PATH_COVER, coverFileName))
                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                    Panel6.Visible = True ' Tampilkan panel jika ada gambar
                End If
            End If

            ' Tutup reader dan koneksi
            reader.Close()
        Catch ex As Exception
            ' Tampilkan pesan kesalahan jika terjadi error
            MessageBox.Show("Gagal menampilkan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close() ' Pastikan untuk selalu menutup koneksi setelah digunakan
        End Try

    End Sub

    Public Sub bersihkan()
        TextBox1.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        PictureBox1.Image = Nothing
        ComboBox1.SelectedIndex = -1
        Panel6.Visible = False
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

    Private Sub ArtistEditLagu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanDataEdit(txtID.Text)
    End Sub
End Class