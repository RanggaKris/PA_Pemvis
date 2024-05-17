Imports MySql.Data.MySqlClient

Public Class ManajemenArtist

    Public Sub TampilkanDataAkunArtist()
        Try
            koneksi()
            Dim query As String = "SELECT id_akun, username, password, no_telepon, role FROM akun WHERE role = 'Artist'"
            CMD = New MySqlCommand(query, CONN)
            RD = CMD.ExecuteReader()

            ' Bersihkan DataGridView sebelum menambahkan data baru
            DataGridView1.Rows.Clear()

            ' Loop melalui hasil query dan tambahkan ke DataGridView
            While RD.Read()
                Dim idAkun As String = RD("id_akun").ToString()
                Dim username As String = RD("username").ToString()
                Dim password As String = RD("password").ToString()
                Dim noTelepon As String = RD("no_telepon").ToString()
                Dim role As String = RD("role").ToString()

                DataGridView1.Rows.Add(idAkun, username, password, noTelepon, role)
            End While

            RD.Close()
        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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

    Private Sub btnislogin_Click(sender As Object, e As EventArgs) Handles btnislogin.Click
        AdminHome.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        content(TambahAkunArtist)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim idAkun As String = DataGridView1.CurrentRow.Cells("id_akun").Value.ToString()

            EditAkunArtist.txtID.Text = idAkun
            content(EditAkunArtist)
            EditAkunArtist.TampilkanDataAkun(idAkun)
        Else
            MessageBox.Show("Pilih lagu yang akan diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ManajemenArtist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanDataAkunArtist()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim idAkun As String = selectedRow.Cells("id_akun").Value.ToString()

            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus akun ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Try
                    koneksi()
                    Dim query As String = "DELETE FROM akun WHERE id_akun = @idAkun AND role = 'Artist'"
                    CMD = New MySqlCommand(query, CONN)
                    CMD.Parameters.AddWithValue("@idAkun", idAkun)

                    CMD.ExecuteNonQuery()
                    MessageBox.Show("Akun admin berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Refresh data grid view setelah penghapusan
                    TampilkanDataAkunArtist()
                Catch ex As MySqlException
                    MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Else
            MessageBox.Show("Pilih akun yang akan dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim confirmation As DialogResult = MessageBox.Show("Anda yakin ingin melakukan refresh? Semua perubahan yang belum disimpan akan hilang.", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
            TambahAkunArtist.bersihkan()
            EditAkunArtist.bersihkan()
            Panel1.Controls.Clear()
        End If
    End Sub

End Class