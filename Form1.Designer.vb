<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1User = New System.Windows.Forms.Label()
        Me.txtCari = New System.Windows.Forms.RichTextBox()
        Me.btnCari = New System.Windows.Forms.Button()
        Me.DirectorySearcher1 = New System.DirectoryServices.DirectorySearcher()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.buttonDasbor = New System.Windows.Forms.Button()
        Me.buttonuser = New System.Windows.Forms.Button()
        Me.buttonlogout = New System.Windows.Forms.Button()
        Me.buttonlaporan = New System.Windows.Forms.Button()
        Me.buttontransaksi = New System.Windows.Forms.Button()
        Me.buttonproduk = New System.Windows.Forms.Button()
        Me.PanelMain = New System.Windows.Forms.Panel()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1User)
        Me.Panel1.Controls.Add(Me.PictureBox3)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.txtCari)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.btnCari)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1181, 104)
        Me.Panel1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1007, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Kasir"
        '
        'Label1User
        '
        Me.Label1User.AutoSize = True
        Me.Label1User.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1User.ForeColor = System.Drawing.Color.Black
        Me.Label1User.Location = New System.Drawing.Point(1007, 29)
        Me.Label1User.Name = "Label1User"
        Me.Label1User.Size = New System.Drawing.Size(77, 17)
        Me.Label1User.TabIndex = 6
        Me.Label1User.Text = "Nama Kasir"
        '
        'txtCari
        '
        Me.txtCari.BackColor = System.Drawing.Color.White
        Me.txtCari.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCari.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCari.ForeColor = System.Drawing.Color.Gray
        Me.txtCari.Location = New System.Drawing.Point(256, 41)
        Me.txtCari.Margin = New System.Windows.Forms.Padding(6)
        Me.txtCari.MaxLength = 20
        Me.txtCari.Name = "txtCari"
        Me.txtCari.Size = New System.Drawing.Size(169, 28)
        Me.txtCari.TabIndex = 2
        Me.txtCari.Text = ""
        '
        'btnCari
        '
        Me.btnCari.BackColor = System.Drawing.Color.Red
        Me.btnCari.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCari.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnCari.FlatAppearance.BorderSize = 0
        Me.btnCari.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCari.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCari.ForeColor = System.Drawing.Color.White
        Me.btnCari.Location = New System.Drawing.Point(441, 37)
        Me.btnCari.Name = "btnCari"
        Me.btnCari.Size = New System.Drawing.Size(52, 28)
        Me.btnCari.TabIndex = 1
        Me.btnCari.Text = "Cari"
        Me.btnCari.UseVisualStyleBackColor = False
        '
        'DirectorySearcher1
        '
        Me.DirectorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01")
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.Controls.Add(Me.PictureBox9)
        Me.Panel2.Controls.Add(Me.buttonDasbor)
        Me.Panel2.Controls.Add(Me.PictureBox8)
        Me.Panel2.Controls.Add(Me.buttonuser)
        Me.Panel2.Controls.Add(Me.PictureBox7)
        Me.Panel2.Controls.Add(Me.PictureBox6)
        Me.Panel2.Controls.Add(Me.PictureBox5)
        Me.Panel2.Controls.Add(Me.PictureBox4)
        Me.Panel2.Controls.Add(Me.buttonlogout)
        Me.Panel2.Controls.Add(Me.buttonlaporan)
        Me.Panel2.Controls.Add(Me.buttontransaksi)
        Me.Panel2.Controls.Add(Me.buttonproduk)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 104)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(188, 535)
        Me.Panel2.TabIndex = 1
        '
        'buttonDasbor
        '
        Me.buttonDasbor.BackColor = System.Drawing.Color.Transparent
        Me.buttonDasbor.FlatAppearance.BorderSize = 0
        Me.buttonDasbor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonDasbor.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonDasbor.Location = New System.Drawing.Point(78, 21)
        Me.buttonDasbor.Name = "buttonDasbor"
        Me.buttonDasbor.Size = New System.Drawing.Size(95, 34)
        Me.buttonDasbor.TabIndex = 9
        Me.buttonDasbor.Text = "Dasbor"
        Me.buttonDasbor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.buttonDasbor.UseVisualStyleBackColor = False
        '
        'buttonuser
        '
        Me.buttonuser.BackColor = System.Drawing.Color.Transparent
        Me.buttonuser.FlatAppearance.BorderSize = 0
        Me.buttonuser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonuser.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonuser.Location = New System.Drawing.Point(78, 426)
        Me.buttonuser.Name = "buttonuser"
        Me.buttonuser.Size = New System.Drawing.Size(95, 34)
        Me.buttonuser.TabIndex = 7
        Me.buttonuser.Text = "Kelola"
        Me.buttonuser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.buttonuser.UseVisualStyleBackColor = False
        '
        'buttonlogout
        '
        Me.buttonlogout.BackColor = System.Drawing.Color.Transparent
        Me.buttonlogout.FlatAppearance.BorderSize = 0
        Me.buttonlogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonlogout.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonlogout.Location = New System.Drawing.Point(78, 463)
        Me.buttonlogout.Name = "buttonlogout"
        Me.buttonlogout.Size = New System.Drawing.Size(95, 34)
        Me.buttonlogout.TabIndex = 3
        Me.buttonlogout.Text = "Logout"
        Me.buttonlogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.buttonlogout.UseVisualStyleBackColor = False
        '
        'buttonlaporan
        '
        Me.buttonlaporan.BackColor = System.Drawing.Color.Transparent
        Me.buttonlaporan.FlatAppearance.BorderSize = 0
        Me.buttonlaporan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonlaporan.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonlaporan.Location = New System.Drawing.Point(78, 141)
        Me.buttonlaporan.Name = "buttonlaporan"
        Me.buttonlaporan.Size = New System.Drawing.Size(95, 34)
        Me.buttonlaporan.TabIndex = 2
        Me.buttonlaporan.Text = "Laporan"
        Me.buttonlaporan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.buttonlaporan.UseVisualStyleBackColor = False
        '
        'buttontransaksi
        '
        Me.buttontransaksi.BackColor = System.Drawing.Color.Transparent
        Me.buttontransaksi.FlatAppearance.BorderSize = 0
        Me.buttontransaksi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttontransaksi.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttontransaksi.Location = New System.Drawing.Point(78, 101)
        Me.buttontransaksi.Name = "buttontransaksi"
        Me.buttontransaksi.Size = New System.Drawing.Size(95, 34)
        Me.buttontransaksi.TabIndex = 1
        Me.buttontransaksi.Text = "Transaksi"
        Me.buttontransaksi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.buttontransaksi.UseVisualStyleBackColor = False
        '
        'buttonproduk
        '
        Me.buttonproduk.BackColor = System.Drawing.Color.Transparent
        Me.buttonproduk.FlatAppearance.BorderSize = 0
        Me.buttonproduk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonproduk.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonproduk.Location = New System.Drawing.Point(78, 61)
        Me.buttonproduk.Name = "buttonproduk"
        Me.buttonproduk.Size = New System.Drawing.Size(95, 34)
        Me.buttonproduk.TabIndex = 0
        Me.buttonproduk.Text = "Produk"
        Me.buttonproduk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.buttonproduk.UseVisualStyleBackColor = False
        '
        'PanelMain
        '
        Me.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMain.Location = New System.Drawing.Point(188, 104)
        Me.PanelMain.Name = "PanelMain"
        Me.PanelMain.Size = New System.Drawing.Size(993, 535)
        Me.PanelMain.TabIndex = 2
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = Global.TR_POS_App.My.Resources.Resources.dashboard
        Me.PictureBox9.Location = New System.Drawing.Point(49, 27)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(24, 23)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox9.TabIndex = 10
        Me.PictureBox9.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = Global.TR_POS_App.My.Resources.Resources.manage
        Me.PictureBox8.Location = New System.Drawing.Point(49, 432)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(24, 23)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox8.TabIndex = 8
        Me.PictureBox8.TabStop = False
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.TR_POS_App.My.Resources.Resources.logout
        Me.PictureBox7.Location = New System.Drawing.Point(49, 469)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(24, 23)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox7.TabIndex = 6
        Me.PictureBox7.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.TR_POS_App.My.Resources.Resources.chart
        Me.PictureBox6.Location = New System.Drawing.Point(49, 147)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(24, 23)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox6.TabIndex = 5
        Me.PictureBox6.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.TR_POS_App.My.Resources.Resources.transaction
        Me.PictureBox5.Location = New System.Drawing.Point(49, 107)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(24, 23)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 4
        Me.PictureBox5.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.TR_POS_App.My.Resources.Resources.t_shirt
        Me.PictureBox4.Location = New System.Drawing.Point(49, 67)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(24, 23)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 2
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.TR_POS_App.My.Resources.Resources.user
        Me.PictureBox3.Location = New System.Drawing.Point(960, 28)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(35, 40)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 5
        Me.PictureBox3.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Image = Global.TR_POS_App.My.Resources.Resources.logo
        Me.PictureBox1.Location = New System.Drawing.Point(39, 21)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(122, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.TR_POS_App.My.Resources.Resources.sc2
        Me.PictureBox2.Location = New System.Drawing.Point(240, 26)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(262, 50)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 4
        Me.PictureBox2.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Snow
        Me.ClientSize = New System.Drawing.Size(1181, 639)
        Me.Controls.Add(Me.PanelMain)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS Tampilan Rakyat"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents DirectorySearcher1 As DirectoryServices.DirectorySearcher
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents btnCari As Button
    Friend WithEvents txtCari As RichTextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label1User As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents buttonproduk As Button
    Friend WithEvents buttonlogout As Button
    Friend WithEvents buttonlaporan As Button
    Friend WithEvents buttontransaksi As Button
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents buttonuser As Button
    Friend WithEvents PanelMain As Panel
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents buttonDasbor As Button
End Class
