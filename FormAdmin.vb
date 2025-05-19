Imports System.Data.Odbc

Public Class FormAdmin
    Public ModeEdit As Boolean = False
    Public KodeAdminEdit As String = ""

    Private Sub FormAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ModeEdit Then
            Me.Text = "Edit User"
            Call Koneksi()
            Dim cmd As New OdbcCommand("SELECT * FROM admin WHERE kodeadmin = ?", Conn)
            cmd.Parameters.AddWithValue("@kode", KodeAdminEdit)
            Dim rd = cmd.ExecuteReader()
            If rd.Read() Then
                txtKode.Text = rd("kodeadmin").ToString()
                txtNama.Text = rd("namaadmin").ToString()
                txtPassword.Text = rd("passwordadmin").ToString()
                cmbLevel.Text = rd("leveladmin").ToString()
                txtKode.Enabled = False
            End If
            rd.Close()
        Else
            Me.Text = "Tambah User"
        End If

        ' Label Kode Admin
        Dim lblKode As New Label()
        lblKode.Text = "Username"
        lblKode.Location = New Point(20, 20)
        lblKode.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        lblKode.AutoSize = True
        Controls.Add(lblKode)

        txtKode.Location = New Point(150, 20)
        txtKode.Size = New Size(200, 25)
        txtKode.Font = New Font("Segoe UI", 10, FontStyle.Regular)

        ' Label Nama Admin
        Dim lblNama As New Label()
        lblNama.Text = "Nama"
        lblNama.Location = New Point(20, 60)
        lblNama.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        lblNama.AutoSize = True
        Controls.Add(lblNama)

        txtNama.Location = New Point(150, 60)
        txtNama.Size = New Size(200, 25)
        txtNama.Font = New Font("Segoe UI", 10, FontStyle.Regular)

        ' Label Password
        Dim lblPassword As New Label()
        lblPassword.Text = "Password"
        lblPassword.Location = New Point(20, 100)
        lblPassword.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        lblPassword.AutoSize = True
        Controls.Add(lblPassword)

        txtPassword.Location = New Point(150, 100)
        txtPassword.Size = New Size(200, 25)
        txtPassword.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        txtPassword.PasswordChar = "*"

        ' Label Level
        Dim lblLevel As New Label()
        lblLevel.Text = "Level"
        lblLevel.Location = New Point(20, 140)
        lblLevel.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        lblLevel.AutoSize = True
        Controls.Add(lblLevel)

        cmbLevel.Location = New Point(150, 140)
        cmbLevel.Size = New Size(200, 25)
        cmbLevel.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        cmbLevel.DropDownStyle = ComboBoxStyle.DropDownList
        cmbLevel.Items.Clear()
        cmbLevel.Items.AddRange(New String() {"admin", "kasir"})

    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If txtKode.Text = "" Or txtNama.Text = "" Or txtPassword.Text = "" Or cmbLevel.Text = "" Then
            MessageBox.Show("Semua field harus diisi!")
            Exit Sub
        End If

        Try
            Call Koneksi()
            If ModeEdit Then
                Dim cmd As New OdbcCommand("UPDATE admin SET namaadmin=?, passwordadmin=?, leveladmin=? WHERE kodeadmin=?", Conn)
                cmd.Parameters.AddWithValue("@nama", txtNama.Text)
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
                cmd.Parameters.AddWithValue("@level", cmbLevel.Text)
                cmd.Parameters.AddWithValue("@kode", txtKode.Text)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data admin berhasil diperbarui.")
            Else
                Dim cmd As New OdbcCommand("INSERT INTO admin VALUES (?, ?, ?, ?)", Conn)
                cmd.Parameters.AddWithValue("@kode", txtKode.Text)
                cmd.Parameters.AddWithValue("@nama", txtNama.Text)
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
                cmd.Parameters.AddWithValue("@level", cmbLevel.Text)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Admin baru berhasil ditambahkan.")
            End If
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan data: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.Close()
    End Sub
End Class
