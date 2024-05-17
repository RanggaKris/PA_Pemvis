Imports MySql.Data.MySqlClient

Public Class LoginHpPage
    Private Sub txthp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txthp.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub txthp_TextChanged(sender As Object, e As EventArgs) Handles txthp.TextChanged
        Dim charCount As Integer = txthp.TextLength

        If charCount >= 12 Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If
    End Sub

    Private Sub LoginHpPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LinkLabel2.LinkBehavior = LinkBehavior.NeverUnderline
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        LoginPage.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim hp As String = txthp.Text
        Dim password As String = txtpassword.Text

        If String.IsNullOrEmpty(hp) OrElse String.IsNullOrEmpty(password) Then
            MessageBox.Show("Nomor telepon dan password tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        koneksi()

        Try
            Dim query As String = $"SELECT username, role FROM akun WHERE no_telepon = '{hp}' AND password = '{password}'"
            CMD = New MySqlCommand(query, CONN)
            Dim reader As MySqlDataReader = CMD.ExecuteReader()

            If reader.Read() Then
                isLogin = True
                loggedInUsername = reader("username").ToString()
                Dim role As String = reader("role").ToString()

                MessageBox.Show("Login berhasil.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                If role = "Admin" Then
                    AdminHome.Show()
                ElseIf role = "Pendengar" Then
                    Homapage.Show()
                ElseIf role = "Artist" Then
                    ArtistHome.Show()
                Else
                    MessageBox.Show("Role tidak dikenal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                Me.Close()
            Else
                MessageBox.Show("Nomor telepon atau password salah.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Login gagal: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try
    End Sub




    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked Then
            txtpassword.PasswordChar = Char.MinValue
        Else
            txtpassword.PasswordChar = "•" ' 
        End If
    End Sub
End Class
