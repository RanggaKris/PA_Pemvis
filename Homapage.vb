Imports MySql.Data.MySqlClient

Public Class Homapage
    Private Sub Homepage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilUsername()
        StatusLogin()
        AxWindowsMediaPlayer1.Ctlcontrols.play()
        TampilkanKartuLagu()
    End Sub

    Sub content(ByVal nama_panel As Form)
        nama_panel.TopLevel = False
        nama_panel.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        nama_panel.Dock = DockStyle.Fill
        nama_panel.Show()
    End Sub

    Private Sub StatusLogin()
        If isLogin Then
            ' Jika sudah login
            btnislogin.Text = "Logout"
            AxWindowsMediaPlayer1.Visible = True
        Else
            btnislogin.Text = "Login"
            HomesFlow.Visible = False
            AxWindowsMediaPlayer1.Visible = False
        End If
    End Sub

    Private Sub TampilUsername()
        If isLogin Then
            welcome.Text = "Welcome, " & loggedInUsername
        Else
            welcome.Text = "Welcome, login dulu untuk memainkan musik!"
        End If
    End Sub

    Private Sub btnLoginLogout_Click(sender As Object, e As EventArgs) Handles btnislogin.Click
        If isLogin Then
            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                ' Proses logout disini
                MessageBox.Show("Berhasil Logout.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                isLogin = False
                AxWindowsMediaPlayer1.Ctlcontrols.stop()
                loggedInUsername = ""
            End If
        Else
            ' Proses login disini
            isLogin = True
            LoginPage.Show()
            Me.Close()
        End If

        StatusLogin()
        TampilUsername()
    End Sub

    Private Sub TampilkanKartuLagu()
        Dim dt As DataTable = GetSongs()
        For Each row As DataRow In dt.Rows
            Dim tc As New Lagu()
            tc.LoadData(row)
            HomesFlow.Controls.Add(tc)
        Next
    End Sub


End Class
