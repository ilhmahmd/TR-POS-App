Imports System.Data.Odbc
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status

Public Class ucProduk
    Sub TampilkanData()
        Try
            Call Koneksi()
            Da = New OdbcDataAdapter("SELECT * FROM barang", Conn)
            Ds = New DataSet
            Da.Fill(Ds, "barang")

            ' Kosongkan isi TableLayoutPanel
            TableLayoutPanel1.Controls.Clear()
            TableLayoutPanel1.RowCount = 1
            TableLayoutPanel1.ColumnCount = 5 ' Tambah 1 kolom untuk tombol Aksi
            TableLayoutPanel1.AutoSize = True
            TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
            TableLayoutPanel1.AutoScroll = True
            TableLayoutPanel1.Padding = New Padding(10, 10, 10, 40) ' Jarak bawah


            ' Judul kolom custom
            Dim headers() As String = {"Kode Barang", "Nama Barang", "Harga Barang", "Jumlah Barang", "Aksi"}
            For i As Integer = 0 To headers.Length - 1
                Dim lblHeader As New Label With {
                .Text = headers(i),
                .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                .ForeColor = Color.Black,
                .BackColor = Color.LightGray,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Dock = DockStyle.Fill,
                .Margin = New Padding(0)
            }
                TableLayoutPanel1.Controls.Add(lblHeader, i, 0)
            Next

            ' Menampilkan isi data
            For row As Integer = 0 To Ds.Tables("barang").Rows.Count - 1
                TableLayoutPanel1.RowCount += 1

                Dim dr = Ds.Tables("barang").Rows(row)
                Dim rowColor As Color = If(row Mod 2 = 0, Color.FromArgb(255, 230, 230), Color.FromArgb(255, 240, 240))

                ' Tambah label untuk data
                For col As Integer = 0 To 3
                    Dim lblCell As New Label With {
                    .Text = dr(col).ToString(),
                    .BackColor = rowColor,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                    .Dock = DockStyle.Fill,
                    .Margin = New Padding(0)
                }
                    TableLayoutPanel1.Controls.Add(lblCell, col, row + 1)
                Next

                ' Kolom Aksi (Edit dan Hapus)
                Dim panelAksi As New FlowLayoutPanel With {
                .FlowDirection = FlowDirection.LeftToRight,
                .BackColor = rowColor,
                .Dock = DockStyle.Fill,
                .AutoSize = True,
                .WrapContents = False
            }

                ' Tombol Edit
                Dim btnEdit As New Button With {
                .Text = "Edit",
                .Tag = dr("kodebarang").ToString(),
                .BackColor = Color.LightBlue,
                .AutoSize = True,
                .Margin = New Padding(3)
            }
                AddHandler btnEdit.Click, AddressOf BtnEdit_Click
                panelAksi.Controls.Add(btnEdit)

                ' Tombol Hapus
                Dim btnHapus As New Button With {
                .Text = "Hapus",
                .Tag = dr("kodebarang").ToString(),
                .BackColor = Color.LightCoral,
                .AutoSize = True,
                .Margin = New Padding(3)
            }
                AddHandler btnHapus.Click, AddressOf BtnHapus_Click
                panelAksi.Controls.Add(btnHapus)

                TableLayoutPanel1.Controls.Add(panelAksi, 4, row + 1)
            Next

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs)
        Dim kode As String = CType(sender, Button).Tag.ToString()
        MessageBox.Show("Edit data dengan kode: " & kode)

        ' Membuka FormBarang dengan mode edit dan mengirimkan kode barang
        Dim frm As New FormBarang()
        frm.ModeEdit = True
        frm.KodeBarangEdit = kode
        frm.ShowDialog()

        ' Setelah form edit ditutup, panggil TampilkanData() untuk memperbarui data jika perlu
        ' TampilkanData()
    End Sub


    Private Sub BtnHapus_Click(sender As Object, e As EventArgs)
        Dim kode As String = CType(sender, Button).Tag.ToString()
        If MessageBox.Show("Yakin ingin menghapus barang dengan kode: " & kode & "?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                Call Koneksi()
                Cmd = New OdbcCommand("DELETE FROM barang WHERE kodebarang='" & kode & "'", Conn)
                Cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil dihapus.")
                TampilkanData()
            Catch ex As Exception
                MessageBox.Show("Gagal menghapus data: " & ex.Message)
            End Try
        End If
    End Sub



    Private Sub ucProduk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frmBarang As New FormBarang()
        frmBarang.ModeEdit = False ' Set ke mode tambah
        frmBarang.ShowDialog() ' Tampilkan form barang sebagai dialog (modal)
        ' Opsional: Jika Anda perlu memperbarui daftar barang setelah menambah,
        ' Anda bisa menambahkan kode di sini setelah frmBarang.ShowDialog() kembali.
    End Sub
End Class
