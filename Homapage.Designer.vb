<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Homapage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Homapage))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.welcome = New System.Windows.Forms.Label()
        Me.btnislogin = New System.Windows.Forms.Button()
        Me.HomesFlow = New System.Windows.Forms.FlowLayoutPanel()
        Me.queue = New System.Windows.Forms.Panel()
        Me.AxWindowsMediaPlayer1 = New AxWMPLib.AxWindowsMediaPlayer()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label1.Font = New System.Drawing.Font("Trebuchet MS", 25.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(442, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(232, 43)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "S P O T U B E"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Label2"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.welcome)
        Me.Panel1.Controls.Add(Me.btnislogin)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(0, -1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1083, 97)
        Me.Panel1.TabIndex = 9
        '
        'welcome
        '
        Me.welcome.AutoSize = True
        Me.welcome.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.welcome.Location = New System.Drawing.Point(50, 21)
        Me.welcome.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.welcome.Name = "welcome"
        Me.welcome.Size = New System.Drawing.Size(165, 13)
        Me.welcome.TabIndex = 4
        Me.welcome.Text = "Halo! login dan dengarkan musik!"
        '
        'btnislogin
        '
        Me.btnislogin.Font = New System.Drawing.Font("Trebuchet MS", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnislogin.Location = New System.Drawing.Point(975, 11)
        Me.btnislogin.Margin = New System.Windows.Forms.Padding(2)
        Me.btnislogin.Name = "btnislogin"
        Me.btnislogin.Size = New System.Drawing.Size(92, 31)
        Me.btnislogin.TabIndex = 3
        Me.btnislogin.Text = "Login"
        Me.btnislogin.UseVisualStyleBackColor = True
        '
        'HomesFlow
        '
        Me.HomesFlow.AutoScroll = True
        Me.HomesFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.HomesFlow.Location = New System.Drawing.Point(189, 100)
        Me.HomesFlow.Name = "HomesFlow"
        Me.HomesFlow.Size = New System.Drawing.Size(894, 519)
        Me.HomesFlow.TabIndex = 11
        '
        'queue
        '
        Me.queue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.queue.Location = New System.Drawing.Point(7, 100)
        Me.queue.Name = "queue"
        Me.queue.Size = New System.Drawing.Size(176, 519)
        Me.queue.TabIndex = 12
        '
        'AxWindowsMediaPlayer1
        '
        Me.AxWindowsMediaPlayer1.Enabled = True
        Me.AxWindowsMediaPlayer1.Location = New System.Drawing.Point(0, 624)
        Me.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1"
        Me.AxWindowsMediaPlayer1.OcxState = CType(resources.GetObject("AxWindowsMediaPlayer1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxWindowsMediaPlayer1.Size = New System.Drawing.Size(1086, 45)
        Me.AxWindowsMediaPlayer1.TabIndex = 10
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ProjectAkhir.My.Resources.Resources._217_2178237_open_eye_vector_show_hide_password_icon
        Me.PictureBox1.Location = New System.Drawing.Point(6, 8)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(39, 40)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Homapage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(1086, 667)
        Me.Controls.Add(Me.queue)
        Me.Controls.Add(Me.AxWindowsMediaPlayer1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.HomesFlow)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "Homapage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Home Page Spotube"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnislogin As Button
    Friend WithEvents welcome As Label
    Friend WithEvents AxWindowsMediaPlayer1 As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents HomesFlow As FlowLayoutPanel
    Friend WithEvents queue As Panel
    Friend WithEvents Timer1 As Timer
End Class
