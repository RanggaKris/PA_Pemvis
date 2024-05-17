Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class TambahAkunArtist
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Ambil nilai dari TextBox
        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text
        Dim noTelepon As String = TextBox3.Text

        ' Validasi input tidak kosong
        If String.IsNullOrEmpty(username) Then
            MessageBox.Show("Username tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Return
        End If

        If String.IsNullOrEmpty(password) Then
            MessageBox.Show("Password tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Return
        End If

        If String.IsNullOrEmpty(noTelepon) Then
            MessageBox.Show("Nomor Telepon tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return
        End If

        koneksi()

        ' Query SQL untuk menyimpan data
        Dim query As String = "INSERT INTO akun (username, password, no_telepon, role) VALUES (@username, @password, @noTelepon, @role)"

        ' Membuat command SQL
        CMD = New MySqlCommand(query, CONN)
        ' Menambahkan parameter ke command
        CMD.Parameters.AddWithValue("@username", username)
        CMD.Parameters.AddWithValue("@password", password)
        CMD.Parameters.AddWithValue("@noTelepon", noTelepon)
        CMD.Parameters.AddWithValue("@role", "Artist") ' Atur role menjadi artist

        Try
            ' Menjalankan query
            CMD.ExecuteNonQuery()
            ' Menampilkan pesan sukses
            MessageBox.Show("Akun artist berhasil ditambahkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As MySqlException
            ' Menampilkan pesan error
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ManajemenArtist.TampilkanDataAkunArtist()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If Not String.IsNullOrEmpty(TextBox2.Text) Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If Not String.IsNullOrEmpty(TextBox3.Text) Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If (Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub
    Public Sub bersihkan()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        bersihkan()
    End Sub
End Class
