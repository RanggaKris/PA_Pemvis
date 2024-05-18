Imports MySql.Data.MySqlClient

Public Class ArtistHome

    Private ArtistTambahLagu As New ArtistTambahLagu()
    Private ArtistEditLagu As New ArtistEditLagu()

    Sub content(ByVal nama_panel As Form)
        If nama_panel IsNot Nothing Then

            Panel1.Controls.Clear()
            nama_panel.TopLevel = False
            nama_panel.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            nama_panel.Dock = DockStyle.Fill

            Panel1.Controls.Add(nama_panel)
            nama_panel.Show()
        Else
            MessageBox.Show("Form tidak valid.")
        End If
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        content(ArtistTambahLagu)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim idLagu As String = DataGridView1.CurrentRow.Cells("id_lagu").Value.ToString()

            ArtistEditLagu.txtID.Text = idLagu
            content(ArtistEditLagu)
            ArtistEditLagu.TampilkanDataEdit(idLagu)
        Else
            MessageBox.Show("Pilih lagu yang akan diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub




    Private Sub ArtistTambahLagu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanData()
    End Sub

    Public Sub TampilkanData()
        koneksi()
        DataGridView1.Rows.Clear()


        Dim query As String = "SELECT * FROM lagu"

        CMD = New MySqlCommand(query, CONN)

        Try
            Dim reader As MySqlDataReader = CMD.ExecuteReader()

            While reader.Read()
                Dim idLagu As String = reader("id_lagu").ToString
                Dim judulLagu As String = reader("judul_lagu").ToString()
                Dim namaArtist As String = reader("nama_artist").ToString()
                Dim deskripsi As String = reader("deskripsi").ToString()
                Dim genre As String = reader("genre").ToString()
                Dim sourceLagu As String = reader("source").ToString()
                Dim cover As String = reader("cover").ToString()

                DataGridView1.Rows.Add(idLagu, judulLagu, namaArtist, deskripsi, genre, sourceLagu, cover)
            End While

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        koneksi()
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus lagu ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Dim idLagu As String = DataGridView1.SelectedRows(0).Cells("id_lagu").Value.ToString()



                Dim query As String = "DELETE FROM lagu WHERE id_lagu = @idLagu"
                CMD = New MySqlCommand(query, CONN)
                CMD.Parameters.AddWithValue("@idLagu", idLagu)

                Try
                    CMD.ExecuteNonQuery()

                    MessageBox.Show("Lagu berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    TampilkanData()
                    ArtistEditLagu.bersihkan()
                    ArtistTambahLagu.bersihkan()
                Catch ex As Exception
                    MessageBox.Show("Gagal menghapus lagu: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    CONN.Close()
                End Try
            End If
        Else
            MessageBox.Show("Pilih lagu yang akan dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


    Public Sub Refreshs()
        Dim confirmResult As DialogResult = MessageBox.Show("Anda yakin ingin melakukan refresh? Semua perubahan yang belum disimpan akan hilang.", "Konfirmasi Refresh", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmResult = DialogResult.Yes Then
            Panel1.Controls.Clear()
            ArtistEditLagu.bersihkan()
            ArtistTambahLagu.bersihkan()
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Refreshs()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    End Sub

    Private Sub btnislogin_Click(sender As Object, e As EventArgs) Handles btnislogin.Click
        If isLogin Then
            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                ' Proses logout disini
                MessageBox.Show("Berhasil Logout.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                isLogin = False
                loggedInUsername = ""
                LoginPage.Show()
                Me.Close()
            End If

        End If
    End Sub
End Class
