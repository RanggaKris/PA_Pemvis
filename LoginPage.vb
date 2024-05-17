Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports MySql.Data.MySqlClient

Public Class LoginPage
    Sub bersihkan()
        txtusername.Text = ""
        txtpassword.Text = ""
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtusername.TextChanged

        Dim charCount As Integer = txtusername.TextLength

        If charCount >= 1 Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If
    End Sub


    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtpassword.TextChanged
        Dim charCount As Integer = txtpassword.TextLength

        If charCount >= 8 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        DaftarPage.Show()
        Me.Close()
    End Sub

    Private Sub LoginPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LinkLabel2.LinkBehavior = LinkBehavior.NeverUnderline
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = txtusername.Text
        Dim password As String = txtpassword.Text

        If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(password) Then
            MessageBox.Show("Username dan Password tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        koneksi()

        Try
            ' Directly embedding the variables into the SQL query
            Dim query As String = $"SELECT username, role FROM akun WHERE username = '{username}' AND password = '{password}'"
            CMD = New MySqlCommand(query, CONN)
            Dim reader As MySqlDataReader = CMD.ExecuteReader()

            If reader.Read() Then
                isLogin = True
                loggedInUsername = reader("username").ToString()
                Dim role As String = reader("role").ToString()

                MessageBox.Show("Login berhasil.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                bersihkan()

                ' Redirect based on role
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
                MessageBox.Show("Username atau Password salah.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Login gagal: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CONN.Close()
        End Try
    End Sub



    Private Sub btnLoginPhone_Click(sender As Object, e As EventArgs) Handles btnLoginPhone.Click
        LoginHpPage.Show()
        Me.Close()
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked Then
            txtpassword.PasswordChar = Char.MinValue
        Else
            txtpassword.PasswordChar = "•" ' 
        End If
    End Sub

    Private Sub btnLoginGoogle_Click(sender As Object, e As EventArgs) Handles btnLoginGoogle.Click
        MessageBox.Show("Mohon maaf fungsi ini masih dalam maintenance.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
End Class
