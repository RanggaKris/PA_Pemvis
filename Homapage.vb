Imports MySql.Data.MySqlClient

Public Class Homapage
    Dim testcards As New List(Of Lagu)

    Private Sub Homepage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        content(New HalamanQueue)
        Dim laguInstance As New Lagu()

        TampilUsername()
        StatusLogin()
        AxWindowsMediaPlayer1.Ctlcontrols.play()
        laguInstance.PictureBox1.Visible = False
        laguInstance.PictureBox2.Visible = False
        ' Tampilkan kartu lagu sesuai dengan jumlah lagu di database
        TampilkanKartuLagu()
    End Sub

    Sub content(ByVal nama_panel As Form)
        queue.Controls.Clear()
        nama_panel.TopLevel = False
        nama_panel.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        nama_panel.Dock = DockStyle.Fill
        queue.Controls.Add(nama_panel)
        nama_panel.Show()
    End Sub

    Private Sub StatusLogin()
        If isLogin Then
            'Jika sudah Login
            btnislogin.Text = "Logout"
            AxWindowsMediaPlayer1.Visible = True

        Else


            btnislogin.Text = "Login"
            AxWindowsMediaPlayer1.Visible = False
        End If
    End Sub

    Private Sub TampilUsername()
        If isLogin Then
            welcome.Text = "Welcome, " & loggedInUsername
        Else
            welcome.Text = "Welcome, login dulu untuk memainkan musik! "
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

    Private Sub Homapage_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        End
    End Sub

    Private Sub TampilkanKartuLagu()
        koneksi()

        Dim query As String = "SELECT id_lagu FROM lagu"
        CMD = New MySqlCommand(query, CONN)

        Try
            RD = CMD.ExecuteReader()

            While RD.Read()
                Dim idLagu As Integer = RD("id_lagu")

                Dim tc As New Lagu()
                tc.Size = New Size(214, 213)

                tc.LoadData(idLagu)

                Me.HomesFlow.Controls.Add(tc)
            End While

        Catch ex As MySqlException
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            RD.Close() ' Tutup reader setelah membaca semua data
            CONN.Close() ' Pastikan koneksi ditutup setelah selesai
        End Try
    End Sub


End Class
