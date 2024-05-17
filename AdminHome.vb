Imports AxWMPLib
Imports MySql.Data.MySqlClient

Public Class AdminHome
    Private printDocument As New Printing.PrintDocument
    Private printPreviewDialog As New PrintPreviewDialog
    Private Sub AdminHome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub btnislogin_Click(sender As Object, e As EventArgs) Handles btnislogin.Click
        If isLogin Then
            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                ' Proses logout disini
                MessageBox.Show("Berhasil Logout.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                isLogin = False
                Homapage.AxWindowsMediaPlayer1.Ctlcontrols.stop()
                loggedInUsername = ""
                LoginPage.Show()
                Me.Close()
            End If

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedId As String = DataGridView1.SelectedRows(0).Cells("id_lagu").Value.ToString()
            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus lagu ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                HapusData(selectedId)
            End If
        Else
            MessageBox.Show("Pilih lagu yang ingin dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub HapusData(idLagu As String)
        Try
            koneksi()
            Dim query As String = "DELETE FROM lagu WHERE id_lagu = @idLagu"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@idLagu", idLagu)
            CMD.ExecuteNonQuery()
            MessageBox.Show("Lagu berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TampilkanData() ' Refresh DataGridView setelah menghapus data
        Catch ex As Exception
            MessageBox.Show("Gagal menghapus data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ManajemenArtist.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Membuat konten yang akan dicetak berdasarkan data dari database
        Dim content As String = "Informasi Lagu:" & vbCrLf
        koneksi()

        Try
            Dim query As String = "SELECT judul_lagu, nama_artist FROM lagu"
            CMD = New MySqlCommand(query, CONN)
            Dim reader As MySqlDataReader = CMD.ExecuteReader()

            While reader.Read()
                Dim judulLagu As String = reader("judul_lagu").ToString()
                Dim namaArtist As String = reader("nama_artist").ToString()
                content &= "Judul Lagu: " & judulLagu & vbCrLf & "Nama Artist: " & namaArtist & vbCrLf & vbCrLf
            End While

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal mengambil data dari database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try

        ' Mengatur properti PrintDocument
        AddHandler printDocument.PrintPage, Sub(sender1 As Object, e1 As Printing.PrintPageEventArgs)
                                                e1.Graphics.DrawString(content, New Font("Arial", 12), Brushes.Black, New PointF(100, 100))
                                            End Sub

        ' Menampilkan konten dalam PrintPreviewDialog
        printPreviewDialog.Document = printDocument
        printPreviewDialog.ShowDialog()
    End Sub
End Class