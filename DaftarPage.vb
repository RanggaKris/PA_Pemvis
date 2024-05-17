Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class DaftarPage

    Sub bersihkan()
        txtUsername.Text = ""
        txthp.Text = ""
        txtPassword.Text = ""
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LinkLabel2.LinkBehavior = LinkBehavior.NeverUnderline
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        Dim charCount As Integer = txtUsername.TextLength

        ' Lebih dari 5 logo checkmark.
        If charCount >= 5 Then
            Panel1.Visible = True
        Else

            Panel1.Visible = False
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthp.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles txthp.TextChanged


        Dim charCount As Integer = txthp.TextLength

        If charCount >= 12 Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If



    End Sub

    Private Function cekNoTelepon(noTelepon As String) As Boolean
        Dim isExists As Boolean = False
        koneksi()
        Try
            Dim query As String = "SELECT COUNT(*) FROM akun WHERE no_telepon = @no_telepon"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@no_telepon", noTelepon)
            Dim count As Integer = Convert.ToInt32(CMD.ExecuteScalar())
            If count > 0 Then
                isExists = True
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try
        Return isExists
    End Function

    Private Function cekUsername(username As String) As Boolean
        Dim isExists As Boolean = False
        koneksi()
        Try
            Dim query As String = "SELECT COUNT(*) FROM akun WHERE username = @username"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@username", username)
            Dim count As Integer = Convert.ToInt32(CMD.ExecuteScalar())
            If count > 0 Then
                isExists = True
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try
        Return isExists
    End Function



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text
        Dim hp As String = txthp.Text
        Dim charPanjang = txtUsername.TextLength

        If charPanjang < 5 Then
            MessageBox.Show("Masukkan minimal 5 karakter..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If


        If String.IsNullOrEmpty(username) Then
            MessageBox.Show("Username tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If cekUsername(username) Then
            MessageBox.Show("Username sudah ada silahkan gunakan username yang lain.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If String.IsNullOrEmpty(hp) OrElse Not Regex.IsMatch(hp, "^[0-9]{12}$") Then
            MessageBox.Show("Nomor telepon harus terdiri dari 12 digit angka.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If cekNoTelepon(hp) Then
            MessageBox.Show("Nomor Telepon Sudah Digunakan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim charCount As Integer = password.Length
        Dim hasUpperCase As Boolean = Regex.IsMatch(password, "[A-Z]")
        Dim hasNumber As Boolean = Regex.IsMatch(password, "\d")

        If charCount < 8 OrElse Not hasUpperCase OrElse Not hasNumber Then
            txterror.Text = "Password harus minimal 8 karakter, mengandung huruf kapital, dan angka."
            txterror.Visible = True
            Return
        End If

        koneksi()
        Try
            Dim query As String = "INSERT INTO akun (username, password, no_telepon, role) VALUES (@username, @password, @no_telepon, @role)"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@username", username)
            CMD.Parameters.AddWithValue("@password", password)
            CMD.Parameters.AddWithValue("@no_telepon", hp)
            CMD.Parameters.AddWithValue("@role", "Pendengar")
            CMD.ExecuteNonQuery()

            MessageBox.Show("Registrasi berhasil.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txterror.Visible = False
            bersihkan()
        Catch ex As Exception
            MessageBox.Show("Registrasi gagal: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked Then
            txtPassword.PasswordChar = Char.MinValue
        Else
            txtPassword.PasswordChar = "•" ' 
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        LoginPage.Show()
        Me.Close()
    End Sub
End Class