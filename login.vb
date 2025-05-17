Imports System.Data.Odbc

Public Class login
    Public Shared NamaPenggunaLogin As String ' Untuk menyimpan nama user ke global
    Public Shared KodeAdminLogin As String    ' Untuk menyimpan kode admin ke global

    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Kode Admin dan Password Tidak Boleh Kosong!")
        Else
            Call Koneksi()
            Cmd = New OdbcCommand("SELECT * FROM admin WHERE kodeadmin='" & TextBox1.Text & "' AND passwordadmin='" & TextBox2.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                Dim level As String = Rd("leveladmin").ToString().ToLower()
                Dim username As String = Rd("kodeadmin").ToString()
                Dim name As String = Rd("namaadmin").ToString()

                ' Simpan informasi login
                NamaPenggunaLogin = name
                KodeAdminLogin = username ' Simpan kode admin juga

                ' Buka form utama dan set label
                Me.Hide()
                Form1.Label1User.Text = name
                Form1.Label2.Text = level

                ' Panggil metode sesuai level
                If level = "admin" Then
                    Form1.Terbuka()
                ElseIf level = "kasir" Then
                    Form1.Terbuka2()
                Else
                    MsgBox("Level user tidak dikenali.")
                    Call KondisiAwal()
                    Exit Sub
                End If

                ' Tampilkan Form1 dan langsung load ucDasbor
                Form1.Show()
                Form1.LoadKontenKontrol(New ucDasbor()) ' ← tampilkan dasbor langsung

            Else
                MsgBox("Username atau Password Salah!")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown, TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub
End Class
