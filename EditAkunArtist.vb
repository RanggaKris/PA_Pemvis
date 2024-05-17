Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports MySql.Data.MySqlClient

Public Class EditAkunArtist


    Public Sub TampilkanDataAkun(ByVal idAkun As String)
        Try
            koneksi()
            Dim query As String = "SELECT username, password, no_telepon FROM akun WHERE id_akun = @idAkun"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@idAkun", idAkun)
            RD = CMD.ExecuteReader()

            If RD.Read() Then
                TextBox1.Text = RD("username").ToString()
                TextBox2.Text = RD("password").ToString()
                TextBox3.Text = RD("no_telepon").ToString()
            End If

            RD.Close()
        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
    Public Sub UpdateDataAkun(ByVal idAkun As String, ByVal newUsername As String, ByVal newPassword As String, ByVal newNoTelepon As String)
        Try
            koneksi()
            Dim query As String = "UPDATE akun SET username = @newUsername, password = @newPassword, no_telepon = @newNoTelepon WHERE id_akun = @idAkun"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@newUsername", newUsername)
            CMD.Parameters.AddWithValue("@newPassword", newPassword)
            CMD.Parameters.AddWithValue("@newNoTelepon", newNoTelepon)
            CMD.Parameters.AddWithValue("@idAkun", idAkun)
            CMD.ExecuteNonQuery()
            MessageBox.Show("Data akun berhasil diperbarui.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ManajemenArtist.TampilkanDataAkunArtist()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim idAkun As String = txtID.Text
        Dim newUsername As String = TextBox1.Text
        Dim newPassword As String = TextBox2.Text
        Dim newNoTelepon As String = TextBox3.Text

        If String.IsNullOrEmpty(newUsername) Then
            MessageBox.Show("Username tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
            Return
        End If

        If String.IsNullOrEmpty(newPassword) Then
            MessageBox.Show("Password tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Focus()
            Return
        End If

        If String.IsNullOrEmpty(newNoTelepon) Then
            MessageBox.Show("Nomor Telepon tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Focus()
            Return
        End If
        UpdateDataAkun(idAkun, newUsername, newPassword, newNoTelepon)
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
