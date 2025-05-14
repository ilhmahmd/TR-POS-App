Imports System.Data.Odbc

Public Class FormBarang
    Private TableLayoutPanel1 As New TableLayoutPanel()
    Private txtKode As New TextBox()
    Private txtNama As New TextBox()
    Private txtHarga As New TextBox()
    Private txtJumlah As New TextBox()
    Private btnSimpan As New Button()
    Private btnBatal As New Button()

    Public ModeEdit As Boolean = False
    Public KodeBarangEdit As String = ""

    Private Sub FormBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = If(ModeEdit, "Edit Barang", "Tambah Barang")
        ' Me.BackColor = Color.FromArgb(255, 240, 240) ' Hapus pengaturan warna background
        Me.Font = New Font("Segoe UI", 9)
        Me.AutoSize = False
        Me.Size = New Size(300, 220) ' Ukuran form yang lebih kecil
        Me.StartPosition = FormStartPosition.CenterScreen ' Muncul di tengah layar

        ' Setup layout
        SetupLayout()

        ' Clear form
        ClearForm()

        ' Kalau mode edit, load data dari database
        If ModeEdit Then
            LoadDataBarang()
            txtKode.Enabled = False
        End If
    End Sub

    Private Sub SetupLayout()
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.RowCount = 5
        TableLayoutPanel1.Padding = New Padding(10)
        TableLayoutPanel1.AutoSize = True
        TableLayoutPanel1.MaximumSize = New Size(280, 0)
        TableLayoutPanel1.BackColor = Color.White
        TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None

        ' Ukuran kolom
        TableLayoutPanel1.ColumnStyles.Clear()
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(DockStyle.Fill))

        ' Ukuran baris
        TableLayoutPanel1.RowStyles.Clear()
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Kode Barang
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Nama Barang
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Harga Barang
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Jumlah Barang
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 40)) ' Baris Tombol dengan tinggi tetap

        ' Input fields
        AddInput("Kode Barang", txtKode, 0)
        AddInput("Nama Barang", txtNama, 1)
        AddInput("Harga Barang", txtHarga, 2)
        AddInput("Jumlah Barang", txtJumlah, 3)

        ' Tombol simpan & batal
        Dim panelTombol As New FlowLayoutPanel With {
            .FlowDirection = FlowDirection.RightToLeft,
            .Dock = DockStyle.Fill, ' Dock mengisi sel tabel
            .AutoSize = False, ' Jangan auto size
            .Height = 30 ' Atur tinggi panel tombol
        }

        btnSimpan.Text = "Simpan"
        btnSimpan.BackColor = Color.LightBlue
        btnSimpan.Font = New Font("Segoe UI", 9)
        btnSimpan.AutoSize = False
        btnSimpan.Size = New Size(75, 25) ' Ukuran tombol lebih kecil
        AddHandler btnSimpan.Click, AddressOf btnSimpan_Click

        btnBatal.Text = "Batal"
        btnBatal.BackColor = Color.LightGray
        btnBatal.Font = New Font("Segoe UI", 9)
        btnBatal.AutoSize = False
        btnBatal.Size = New Size(75, 25) ' Ukuran tombol lebih kecil
        AddHandler btnBatal.Click, Sub() Me.Close()

        panelTombol.Controls.Add(btnBatal)
        panelTombol.Controls.Add(btnSimpan)
        TableLayoutPanel1.Controls.Add(panelTombol, 0, 4)
        TableLayoutPanel1.SetColumnSpan(panelTombol, 2)

        Me.Controls.Add(TableLayoutPanel1)
    End Sub

    Private Sub AddInput(labelText As String, inputBox As TextBox, rowIndex As Integer)
        Dim lbl As New Label With {
            .Text = labelText,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI", 9),
            .Dock = DockStyle.Fill,
            .AutoSize = True
        }
        inputBox.Font = New Font("Segoe UI", 9)
        inputBox.Dock = DockStyle.Fill

        TableLayoutPanel1.Controls.Add(lbl, 0, rowIndex)
        TableLayoutPanel1.Controls.Add(inputBox, 1, rowIndex)
    End Sub

    Private Sub LoadDataBarang()
        Try
            Call Koneksi()
            Cmd = New OdbcCommand("SELECT * FROM barang WHERE kodebarang=?", Conn)
            Cmd.Parameters.AddWithValue("?", KodeBarangEdit)
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                txtKode.Text = Rd("kodebarang").ToString()
                txtNama.Text = Rd("namabarang").ToString()
                txtHarga.Text = Rd("hargabarang").ToString()
                txtJumlah.Text = Rd("jumlahbarang").ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Gagal memuat data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearForm()
        txtKode.Text = ""
        txtNama.Text = ""
        txtHarga.Text = ""
        txtJumlah.Text = ""
        txtKode.Enabled = True
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs)
        ' Simpan ke database bisa ditambahkan di sini
        If txtKode.Text = "" Or txtNama.Text = "" Or txtHarga.Text = "" Or txtJumlah.Text = "" Then
            MessageBox.Show("Lengkapi semua data terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Call Koneksi()
            If ModeEdit Then
                ' Update
                Cmd = New OdbcCommand("UPDATE barang SET namabarang=?, hargabarang=?, jumlahbarang=? WHERE kodebarang=?", Conn)
                Cmd.Parameters.AddWithValue("?", txtNama.Text)
                Cmd.Parameters.AddWithValue("?", txtHarga.Text)
                Cmd.Parameters.AddWithValue("?", txtJumlah.Text)
                Cmd.Parameters.AddWithValue("?", txtKode.Text)
                Cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil diperbarui.")
            Else
                ' Insert
                Cmd = New OdbcCommand("INSERT INTO barang VALUES (?, ?, ?, ?)", Conn)
                Cmd.Parameters.AddWithValue("?", txtKode.Text)
                Cmd.Parameters.AddWithValue("?", txtNama.Text)
                Cmd.Parameters.AddWithValue("?", txtHarga.Text)
                Cmd.Parameters.AddWithValue("?", txtJumlah.Text)
                Cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil disimpan.")
            End If
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class