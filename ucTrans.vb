Imports System.Data.Odbc
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ApplicationServices

Public Class ucTrans

    ' List untuk menyimpan item keranjang
    ' List untuk menyimpan item keranjang
    Private keranjang As New List(Of OrderItem)
    Private selectedPaymentMethod As String = ""
    Private cashAmountGiven As Decimal = 0
    Private changeAmount As Decimal = 0

    Private Function GetLoggedInAdminCode() As String
        Return login.KodeAdminLogin ' Akses variabel Shared dari kelas login
    End Function

    Private Sub ucTrans_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanData()
        KonfigurasiGridKeranjang()
        HitungTotalHarga()
    End Sub

    Private Sub KonfigurasiGridKeranjang()
        dgvKeranjang.AutoGenerateColumns = False
        dgvKeranjang.Columns.Clear()

        ' Styling umum untuk header
        With dgvKeranjang.ColumnHeadersDefaultCellStyle
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .BackColor = Color.WhiteSmoke
        End With
        dgvKeranjang.EnableHeadersVisualStyles = False

        ' Styling umum untuk cell
        With dgvKeranjang.DefaultCellStyle
            .Font = New Font("Segoe UI", 8, FontStyle.Regular)
            .Alignment = DataGridViewContentAlignment.MiddleLeft
        End With

        ' Kolom Nama Barang
        Dim colNamaBarang As New DataGridViewTextBoxColumn()
        colNamaBarang.DataPropertyName = "NamaBarang"
        colNamaBarang.HeaderText = "Nama Barang"
        colNamaBarang.ReadOnly = True
        colNamaBarang.Width = 120 ' Sesuaikan dengan lebar kolom Nama Barang di dgvProduk
        dgvKeranjang.Columns.Add(colNamaBarang)

        ' Kolom Jumlah
        Dim colJumlah As New DataGridViewTextBoxColumn()
        colJumlah.DataPropertyName = "JumlahBeli"
        colJumlah.HeaderText = "Jumlah"
        colJumlah.Width = 50 ' Sesuaikan dengan lebar kolom Stok di dgvProduk
        colJumlah.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvKeranjang.Columns.Add(colJumlah)

        ' Kolom Subtotal
        Dim colSubtotal As New DataGridViewTextBoxColumn()
        colSubtotal.DataPropertyName = "Subtotal"
        colSubtotal.HeaderText = "Subtotal"
        colSubtotal.ReadOnly = True
        colSubtotal.Width = 70 ' Sesuaikan dengan lebar kolom Harga di dgvProduk
        colSubtotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvKeranjang.Columns.Add(colSubtotal)

        ' Kolom Hapus
        Dim btnHapus As New DataGridViewButtonColumn()
        btnHapus.Name = "HapusKeranjangColumn"
        btnHapus.HeaderText = ""
        btnHapus.Text = "Hapus"
        btnHapus.UseColumnTextForButtonValue = True
        btnHapus.Width = 50 ' Lebar tombol Hapus
        dgvKeranjang.Columns.Add(btnHapus)

        ' Styling tambahan untuk DataGridView keranjang
        dgvKeranjang.BorderStyle = BorderStyle.Fixed3D
        dgvKeranjang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvKeranjang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvKeranjang.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvKeranjang.ColumnHeadersHeight = 40
        dgvKeranjang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvKeranjang.RowTemplate.Height = 35
        dgvKeranjang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgvKeranjang.AllowUserToResizeColumns = False ' Atur ke False jika tidak ingin pengguna mengubah lebar kolom
    End Sub

    Sub TampilkanData(Optional filter As String = "")
        Try
            Call Koneksi()
            Dim query As String = "SELECT kodebarang, namabarang, hargabarang, jumlahbarang FROM barang WHERE jumlahbarang > 0" ' Hanya tampilkan yang stok > 0 di awal
            If filter <> "" Then
                query &= " AND (kodebarang LIKE ? OR namabarang LIKE ?)"
            End If

            Using da As New OdbcDataAdapter(query, Conn)
                If filter <> "" Then
                    da.SelectCommand.Parameters.AddWithValue("@kode", "%" & filter & "%")
                    da.SelectCommand.Parameters.AddWithValue("@nama", "%" & filter & "%")
                End If

                Dim dt As New DataTable
                da.Fill(dt)

                ' Hitung jumlah item di keranjang untuk setiap produk
                Dim keranjangLookup = keranjang.GroupBy(Function(item) item.KodeBarang).ToDictionary(Function(g) g.Key, Function(g) g.Sum(Function(item) item.JumlahBeli))

                ' Buat DataTable baru untuk menampung produk yang akan ditampilkan
                Dim dtFiltered As New DataTable
                If dt.Columns.Count > 0 Then
                    For Each col As DataColumn In dt.Columns
                        dtFiltered.Columns.Add(col.ColumnName, col.DataType)
                    Next
                End If

                ' Filter dan sesuaikan stok untuk ditampilkan
                For Each rowData As DataRow In dt.Rows
                    Dim kodeBarang As String = rowData("kodebarang").ToString()
                    Dim stokDatabase As Integer
                    Integer.TryParse(rowData("jumlahbarang").ToString(), stokDatabase)
                    Dim jumlahDiKeranjang As Integer = If(keranjangLookup.ContainsKey(kodeBarang), keranjangLookup(kodeBarang), 0)
                    Dim stokTersediaUntukDijual As Integer = stokDatabase - jumlahDiKeranjang

                    If stokTersediaUntukDijual > 0 Then ' Hanya tambahkan ke tampilan jika masih ada stok tersedia
                        Dim newRow As DataRow = dtFiltered.NewRow()
                        For Each col As DataColumn In dt.Columns
                            newRow(col.ColumnName) = rowData(col.ColumnName)
                        Next
                        newRow("jumlahbarang") = stokTersediaUntukDijual ' Tampilkan stok yang tersedia
                        dtFiltered.Rows.Add(newRow)
                    End If
                Next

                dgvProduk.DataSource = dtFiltered

                ' Tambahkan kembali tombol Tambah ke Keranjang dan atur styling
                If Not dgvProduk.Columns.Contains("TambahKeranjangColumn") Then TambahTombolKeranjang()
                AturStylingGrid()
            End Using
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

    Private Sub AturStylingGrid()
        With dgvProduk
            For Each col As DataGridViewColumn In .Columns
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            .BorderStyle = BorderStyle.Fixed3D
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None

            .ColumnHeadersHeight = 40
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            ' Atur lebar kolom
            If .Columns.Contains("kodebarang") Then
                .Columns("kodebarang").Width = 80
                .Columns("kodebarang").HeaderText = "Kode Barang"
                .Columns("kodebarang").ReadOnly = True
            End If
            If .Columns.Contains("namabarang") Then
                .Columns("namabarang").Width = 190
                .Columns("namabarang").HeaderText = "Nama Barang"
                .Columns("namabarang").ReadOnly = True
            End If
            If .Columns.Contains("hargabarang") Then
                .Columns("hargabarang").Width = 90
                .Columns("hargabarang").HeaderText = "Harga"
                .Columns("hargabarang").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("hargabarang").ReadOnly = True
            End If
            If .Columns.Contains("jumlahbarang") Then
                .Columns("jumlahbarang").Width = 70
                .Columns("jumlahbarang").HeaderText = "Stok"
                .Columns("jumlahbarang").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("jumlahbarang").ReadOnly = True
            End If
            If .Columns.Contains("TambahKeranjangColumn") Then
                .Columns("TambahKeranjangColumn").Width = 50
            End If

            ' Style header
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .EnableHeadersVisualStyles = False

            ' Style isi cell
            .DefaultCellStyle.Font = New Font("Segoe UI", 8, FontStyle.Regular)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .RowTemplate.Height = 35

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .AllowUserToResizeColumns = True
        End With


    End Sub

    ' Event klik tombol Tambah ke Keranjang
    ' Event klik tombol Tambah ke Keranjang
    Private Sub dgvProduk_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProduk.CellClick
        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex = dgvProduk.Columns("TambahKeranjangColumn").Index Then
            Dim kodeBarang As String = dgvProduk.Rows(e.RowIndex).Cells("kodebarang").Value.ToString()
            Dim namaBarang As String = dgvProduk.Rows(e.RowIndex).Cells("namabarang").Value.ToString()
            Dim hargaBarang As Decimal
            Decimal.TryParse(dgvProduk.Rows(e.RowIndex).Cells("hargabarang").Value?.ToString(), hargaBarang)
            Dim jumlahStokTersediaAwal As Integer
            Integer.TryParse(dgvProduk.Rows(e.RowIndex).Cells("jumlahbarang").Value?.ToString(), jumlahStokTersediaAwal)

            If jumlahStokTersediaAwal > 0 Then
                Dim inputJumlah As String = InputBox($"Masukkan jumlah '{namaBarang}' yang ingin dibeli (Stok Tersedia: {jumlahStokTersediaAwal}):", "Masukkan Jumlah", "1")
                Dim jumlahBeli As Integer
                If Integer.TryParse(inputJumlah, jumlahBeli) AndAlso jumlahBeli > 0 AndAlso jumlahBeli <= jumlahStokTersediaAwal Then
                    Dim existingItem = keranjang.FirstOrDefault(Function(item) item.KodeBarang = kodeBarang)
                    If existingItem IsNot Nothing Then
                        existingItem.JumlahBeli += jumlahBeli
                    Else
                        Dim newItem As New OrderItem With {
                        .KodeBarang = kodeBarang,
                        .NamaBarang = namaBarang,
                        .Harga = hargaBarang,
                        .JumlahBeli = jumlahBeli
                    }
                        keranjang.Add(newItem)
                    End If
                    PerbaruiTampilanKeranjang()
                    TampilkanData() ' Refresh tampilan dgvProduk, akan menghilangkan jika stok tersedia menjadi 0
                    MessageBox.Show($"'{namaBarang}' ({jumlahBeli}) ditambahkan ke keranjang.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf Not String.IsNullOrEmpty(inputJumlah) Then
                    MessageBox.Show("Jumlah tidak valid atau melebihi stok.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MessageBox.Show($"Stok '{namaBarang}' habis.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    ' Styling tombol Tambah ke Keranjang
    Private Sub dgvProduk_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvProduk.CellFormatting
        If e.RowIndex < 0 Then Exit Sub

        ' Format harga
        If dgvProduk.Columns(e.ColumnIndex).Name = "hargabarang" Then
            Dim hargaValue As Integer
            If Integer.TryParse(e.Value?.ToString(), hargaValue) Then
                e.Value = "Rp " & hargaValue.ToString("N0").Replace(",", ".")
                e.FormattingApplied = True
            End If
        End If

        ' Style tombol Tambah ke Keranjang
        If dgvProduk.Columns(e.ColumnIndex).Name = "TambahKeranjangColumn" Then
            dgvProduk.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.LightSkyBlue
            dgvProduk.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Black
            dgvProduk.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        End If
    End Sub

    ' Fungsi untuk melakukan filter data (jika diperlukan di ucTrans)
    Public Sub FilterData2(keyword As String)
        Try
            Call Koneksi()
            Dim query As String = "SELECT kodebarang, namabarang, hargabarang, jumlahbarang FROM barang WHERE kodebarang LIKE ? OR namabarang LIKE ?"
            Using da As New OdbcDataAdapter(query, Conn)
                da.SelectCommand.Parameters.AddWithValue("@p1", "%" & keyword & "%")
                da.SelectCommand.Parameters.AddWithValue("@p2", "%" & keyword & "%")

                Dim dt As New DataTable
                da.Fill(dt)

                dgvProduk.Columns.Clear()
                dgvProduk.DataSource = dt
                dgvProduk.AllowUserToAddRows = False

                ' Tambahkan kembali tombol Tambah ke Keranjang dan atur styling
                TambahTombolKeranjang()
                AturStylingGrid()
            End Using
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat filter: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

    Private Sub TambahTombolKeranjang()
        ' Hapus kolom tombol jika sudah ada
        If dgvProduk.Columns.Contains("TambahKeranjangColumn") Then dgvProduk.Columns.Remove("TambahKeranjangColumn")

        Dim btnTambahKeranjang As New DataGridViewButtonColumn With {
            .Name = "TambahKeranjangColumn",
            .HeaderText = "",
            .Text = "+",
            .UseColumnTextForButtonValue = True,
            .Width = 50,
            .DefaultCellStyle = New DataGridViewCellStyle With {
                .BackColor = Color.LightSkyBlue,
                .ForeColor = Color.Black,
                .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                .Alignment = DataGridViewContentAlignment.MiddleCenter,
                .Padding = New Padding(2)
            }
        }
        dgvProduk.Columns.Add(btnTambahKeranjang)
    End Sub

    ' Event handler untuk tombol Bayar
    Private Sub btnTunai_Click(sender As Object, e As EventArgs) Handles btnTunai.Click
        selectedPaymentMethod = "Tunai"
        cashAmountGiven = 0
        changeAmount = 0

        ' Efek visual untuk tombol Tunai (merah, teks putih)
        btnTunai.BackColor = Color.Red
        btnTunai.ForeColor = Color.White
        btnTunai.FlatStyle = FlatStyle.Standard

        ' Reset tampilan tombol QRIS dan Transfer ke default
        btnQRIS.BackColor = SystemColors.Control
        btnQRIS.ForeColor = SystemColors.ControlText
        btnQRIS.FlatStyle = FlatStyle.System

        btnTransfer.BackColor = SystemColors.Control
        btnTransfer.ForeColor = SystemColors.ControlText
        btnTransfer.FlatStyle = FlatStyle.System
    End Sub

    Private Sub btnQRIS_Click(sender As Object, e As EventArgs) Handles btnQRIS.Click
        selectedPaymentMethod = "QRIS"
        cashAmountGiven = 0
        changeAmount = 0

        ' Efek visual untuk tombol QRIS (merah, teks putih)
        btnQRIS.BackColor = Color.Red
        btnQRIS.ForeColor = Color.White
        btnQRIS.FlatStyle = FlatStyle.Standard

        ' Reset tampilan tombol Tunai dan Transfer ke default
        btnTunai.BackColor = SystemColors.Control
        btnTunai.ForeColor = SystemColors.ControlText
        btnTunai.FlatStyle = FlatStyle.System

        btnTransfer.BackColor = SystemColors.Control
        btnTransfer.ForeColor = SystemColors.ControlText
        btnTransfer.FlatStyle = FlatStyle.System
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        selectedPaymentMethod = "Transfer"
        cashAmountGiven = 0
        changeAmount = 0

        ' Efek visual untuk tombol Transfer (merah, teks putih)
        btnTransfer.BackColor = Color.Red
        btnTransfer.ForeColor = Color.White
        btnTransfer.FlatStyle = FlatStyle.Standard

        ' Reset tampilan tombol Tunai dan QRIS ke default
        btnTunai.BackColor = SystemColors.Control
        btnTunai.ForeColor = SystemColors.ControlText
        btnTunai.FlatStyle = FlatStyle.System

        btnQRIS.BackColor = SystemColors.Control
        btnQRIS.ForeColor = SystemColors.ControlText
        btnQRIS.FlatStyle = FlatStyle.System
    End Sub

    ' Event handler untuk tombol Bayar
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnBayar.Click
        If keranjang.Count > 0 Then
            If String.IsNullOrEmpty(selectedPaymentMethod) Then
                MessageBox.Show("Pilih metode pembayaran terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If String.IsNullOrEmpty(GetLoggedInAdminCode()) Then
                MessageBox.Show("Kode admin tidak valid. Silakan login kembali.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If selectedPaymentMethod = "Tunai" Then
                Dim totalHarga As Decimal = TotalHargaKeranjang()
                Dim inputJumlah As String = InputBox($"Masukkan jumlah tunai (Total: Rp {totalHarga:N0}):", "Pembayaran Tunai", totalHarga.ToString())
                Dim jumlahTunaiDiberikan As Decimal
                If Decimal.TryParse(inputJumlah, jumlahTunaiDiberikan) Then
                    If jumlahTunaiDiberikan >= totalHarga Then
                        changeAmount = jumlahTunaiDiberikan - totalHarga
                        MessageBox.Show($"Kembalian: Rp {changeAmount:N0}", "Kembalian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cashAmountGiven = jumlahTunaiDiberikan
                        SimpanTransaksiKeDatabase()
                    Else
                        MessageBox.Show("Jumlah tunai kurang.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                ElseIf Not String.IsNullOrEmpty(inputJumlah) Then
                    MessageBox.Show("Jumlah tunai tidak valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                SimpanTransaksiKeDatabase() ' Untuk metode pembayaran non-tunai
            End If

            If selectedPaymentMethod <> "" Then
                keranjang.Clear()
                PerbaruiTampilanKeranjang()
                TampilkanData() ' Refresh data produk setelah transaksi
                selectedPaymentMethod = "" ' Reset metode pembayaran
                cashAmountGiven = 0
                changeAmount = 0
            End If
        Else
            MessageBox.Show("Keranjang belanja kosong.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    ' Event handler untuk klik di DataGridView keranjang (untuk tombol Hapus)
    Private Sub dgvKeranjang_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvKeranjang.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = dgvKeranjang.Columns("HapusKeranjangColumn").Index Then
            keranjang.RemoveAt(e.RowIndex)
            PerbaruiTampilanKeranjang()
            TampilkanData() ' Refresh tampilan dgvProduk, akan menampilkan kembali produk jika stok tersedia > 0
        End If
    End Sub

    ' Subrutin untuk memperbarui tampilan DataGridView keranjang
    Private Sub PerbaruiTampilanKeranjang()

        dgvKeranjang.DataSource = Nothing
        If keranjang.Count > 0 Then
            dgvKeranjang.DataSource = keranjang

            ' Pastikan kolom "KodeBarang" ada sebelum mencoba menyembunyikannya
            If dgvKeranjang.Columns.Contains("KodeBarang") Then
                dgvKeranjang.Columns("KodeBarang").Visible = False ' Sembunyikan Kode Barang
            End If

            If dgvKeranjang.Columns.Contains("NamaBarang") Then dgvKeranjang.Columns("NamaBarang").HeaderText = "Nama Barang"
            If dgvKeranjang.Columns.Contains("JumlahBeli") Then dgvKeranjang.Columns("JumlahBeli").HeaderText = "Jumlah"
            If dgvKeranjang.Columns.Contains("Subtotal") Then dgvKeranjang.Columns("Subtotal").HeaderText = "Subtotal"



            ' Tambahkan tombol "Hapus" jika belum ada
            If Not dgvKeranjang.Columns.Contains("HapusKeranjangColumn") Then
                Dim btnHapus As New DataGridViewButtonColumn With {
                    .Name = "HapusKeranjangColumn",
                    .HeaderText = "",
                    .Text = "Hapus",
                    .UseColumnTextForButtonValue = True,
                    .Width = 50
                }
                dgvKeranjang.Columns.Add(btnHapus)
            End If
        Else
            dgvKeranjang.DataSource = Nothing ' Kosongkan jika tidak ada item
            dgvKeranjang.Columns.Clear()
            KonfigurasiGridKeranjang() ' Reset kolom
        End If
        HitungTotalHarga()
    End Sub

    ' Subrutin untuk menghitung total harga keranjang
    Private Function TotalHargaKeranjang() As Decimal
        Dim total As Decimal = 0
        For Each item As OrderItem In keranjang
            total += item.Subtotal
        Next
        Return total
    End Function

    ' Subrutin untuk memperbarui label total harga
    Private Sub HitungTotalHarga()
        lblTotalHarga.Text = $"Total: Rp {TotalHargaKeranjang():N0}"
    End Sub

    Private Sub CetakStruk(transaksiID As Integer, daftarBarang As List(Of OrderItem), totalHarga As Decimal, metodeBayar As String, jumlahTunai As Decimal, kembalian As Decimal)
        Dim pd As New PrintDocument()
        AddHandler pd.PrintPage, AddressOf pd_PrintPage

        ' Simpan informasi transaksi ke dalam properti kelas atau variabel lokal agar dapat diakses di event PrintPage
        Me.transaksiUntukDicetakID = transaksiID
        Me.daftarBarangUntukDicetak = daftarBarang
        Me.totalHargaUntukDicetak = totalHarga
        Me.metodeBayarUntukDicetak = metodeBayar
        Me.jumlahTunaiUntukDicetak = jumlahTunai
        Me.kembalianUntukDicetak = kembalian

        Dim printDialog As New PrintDialog()
        printDialog.Document = pd

        If printDialog.ShowDialog() = DialogResult.OK Then
            Try
                pd.Print()
            Catch ex As Exception
                MessageBox.Show($"Gagal mencetak struk: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        RemoveHandler pd.PrintPage, AddressOf pd_PrintPage
    End Sub

    ' Variabel untuk menyimpan informasi transaksi yang akan dicetak
    Private transaksiUntukDicetakID As Integer
    Private daftarBarangUntukDicetak As List(Of OrderItem)
    Private totalHargaUntukDicetak As Decimal
    Private metodeBayarUntukDicetak As String
    Private jumlahTunaiUntukDicetak As Decimal
    Private kembalianUntukDicetak As Decimal

    ' Event handler untuk menggambar halaman yang akan dicetak
    Private Sub pd_PrintPage(sender As Object, e As PrintPageEventArgs)
        Dim fontHeader As New Font("Arial", 12, FontStyle.Bold)
        Dim fontBody As New Font("Arial", 10, FontStyle.Regular)
        Dim fontBoldBody As New Font("Arial", 10, FontStyle.Bold)
        Dim y As Single = e.MarginBounds.Top
        Dim garis As String = "----------------------------------------"

        ' Judul Struk
        Dim judul As String = "Struk Pembelian"
        Dim judulLebar As Single = e.Graphics.MeasureString(judul, fontHeader).Width
        e.Graphics.DrawString(judul, fontHeader, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - judulLebar) / 2, y)
        y += fontHeader.GetHeight(e.Graphics) + 5

        ' Informasi Transaksi
        e.Graphics.DrawString($"ID Transaksi: {transaksiUntukDicetakID}", fontBody, Brushes.Black, e.MarginBounds.Left, y)
        y += fontBody.GetHeight(e.Graphics)
        e.Graphics.DrawString($"Tanggal: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", fontBody, Brushes.Black, e.MarginBounds.Left, y)
        y += fontBody.GetHeight(e.Graphics)
        e.Graphics.DrawString(garis, fontBody, Brushes.Black, e.MarginBounds.Left, y)
        y += fontBody.GetHeight(e.Graphics)

        ' Daftar Barang
        e.Graphics.DrawString("Barang", fontBoldBody, Brushes.Black, e.MarginBounds.Left, y)
        e.Graphics.DrawString("Jumlah", fontBoldBody, Brushes.Black, e.MarginBounds.Left + 200, y)
        e.Graphics.DrawString("Subtotal", fontBoldBody, Brushes.Black, e.MarginBounds.Left + 250, y, New StringFormat With {.Alignment = StringAlignment.Far})
        y += fontBoldBody.GetHeight(e.Graphics)
        e.Graphics.DrawString(garis, fontBody, Brushes.Black, e.MarginBounds.Left, y)
        y += fontBody.GetHeight(e.Graphics)

        For Each item As OrderItem In daftarBarangUntukDicetak
            e.Graphics.DrawString(item.NamaBarang, fontBody, Brushes.Black, e.MarginBounds.Left, y)
            e.Graphics.DrawString(item.JumlahBeli.ToString(), fontBody, Brushes.Black, e.MarginBounds.Left + 200, y)
            e.Graphics.DrawString($"Rp {item.Subtotal:N0}", fontBody, Brushes.Black, e.MarginBounds.Left + 250, y, New StringFormat With {.Alignment = StringAlignment.Far})
            y += fontBody.GetHeight(e.Graphics)
        Next

        e.Graphics.DrawString(garis, fontBody, Brushes.Black, e.MarginBounds.Left, y)
        y += fontBody.GetHeight(e.Graphics)

        ' Total Harga
        e.Graphics.DrawString("Total:", fontBoldBody, Brushes.Black, e.MarginBounds.Left + 150, y)
        e.Graphics.DrawString($"Rp {totalHargaUntukDicetak:N0}", fontBoldBody, Brushes.Black, e.MarginBounds.Left + 250, y, New StringFormat With {.Alignment = StringAlignment.Far})
        y += fontBoldBody.GetHeight(e.Graphics) + 5

        ' Metode Pembayaran
        e.Graphics.DrawString($"Metode Bayar: {metodeBayarUntukDicetak}", fontBody, Brushes.Black, e.MarginBounds.Left, y)
        y += fontBody.GetHeight(e.Graphics)

        ' Informasi Tambahan untuk Tunai
        If metodeBayarUntukDicetak = "Tunai" Then
            e.Graphics.DrawString($"Tunai Diberikan: Rp {jumlahTunaiUntukDicetak:N0}", fontBody, Brushes.Black, e.MarginBounds.Left, y)
            y += fontBody.GetHeight(e.Graphics)
            e.Graphics.DrawString($"Kembalian: Rp {kembalianUntukDicetak:N0}", fontBoldBody, Brushes.Black, e.MarginBounds.Left, y)
            y += fontBoldBody.GetHeight(e.Graphics) + 5
        End If

        ' Pesan Penutup
        Dim pesan As String = "Terima Kasih Atas Kunjungan Anda!"
        Dim pesanLebar As Single = e.Graphics.MeasureString(pesan, fontBody).Width
        e.Graphics.DrawString(pesan, fontBody, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - pesanLebar) / 2, y)

        ' Tentukan apakah ada halaman lain untuk dicetak (biasanya False untuk struk sederhana)
        e.HasMorePages = False
    End Sub


    Private Sub SimpanTransaksiKeDatabase()
        Try
            Call Koneksi()
            If Conn.State <> ConnectionState.Open Then
                MessageBox.Show("Koneksi database gagal dibuka.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            Dim queryTransaksi As String = "INSERT INTO transaksi (tanggaltransaksi, kodeadmin, totalharga, metode_bayar, jumlah_tunai, kembalian) VALUES (?, ?, ?, ?, ?, ?)"
            Using cmdTransaksi As New OdbcCommand(queryTransaksi, Conn)
                cmdTransaksi.Parameters.AddWithValue("@tanggaltransaksi", DateTime.Now)
                cmdTransaksi.Parameters.AddWithValue("@kodeadmin", GetLoggedInAdminCode())
                cmdTransaksi.Parameters.AddWithValue("@totalharga", TotalHargaKeranjang().ToString(System.Globalization.CultureInfo.InvariantCulture))
                cmdTransaksi.Parameters.AddWithValue("@metode_bayar", selectedPaymentMethod)
                Dim jumlahTunaiUntukDB As Object = If(selectedPaymentMethod = "Tunai", cashAmountGiven, 0)
                cmdTransaksi.Parameters.AddWithValue("@jumlah_tunai", cashAmountGiven.ToString(System.Globalization.CultureInfo.InvariantCulture))
                cmdTransaksi.Parameters.AddWithValue("@kembalian", If(selectedPaymentMethod = "Tunai", changeAmount.ToString(System.Globalization.CultureInfo.InvariantCulture), "0"))

                Debug.WriteLine("--- INSERT INTO transaksi ---")
                Debug.WriteLine($"Tanggal Transaksi: {DateTime.Now} ({DateTime.Now.GetType().Name})")
                Debug.WriteLine($"Kode Admin: {GetLoggedInAdminCode()} ({GetLoggedInAdminCode().GetType().Name})")
                Debug.WriteLine($"Total Harga: {TotalHargaKeranjang().ToString()} ({TotalHargaKeranjang().GetType().Name})")
                Debug.WriteLine($"Metode Bayar: {selectedPaymentMethod} ({selectedPaymentMethod.GetType().Name})")
                Debug.WriteLine($"Jumlah Tunai: {jumlahTunaiUntukDB} ({jumlahTunaiUntukDB.GetType().Name})")
                Debug.WriteLine($"Kembalian: {If(selectedPaymentMethod = "Tunai", changeAmount, DBNull.Value)} ({(If(selectedPaymentMethod = "Tunai", changeAmount, DBNull.Value)).GetType().Name})")

                cmdTransaksi.ExecuteNonQuery()

                Dim lastIDQuery As String = "SELECT @@IDENTITY"
                Dim cmdLastID As New OdbcCommand(lastIDQuery, Conn)
                Dim transaksiID As Integer = Convert.ToInt32(cmdLastID.ExecuteScalar())

                Dim queryDetail As String = "INSERT INTO detail_transaksi (idtransaksi, kodebarang, jumlahbeli, subtotal) VALUES (?, ?, ?, ?)"
                Using cmdDetail As New OdbcCommand(queryDetail, Conn)
                    Debug.WriteLine("--- INSERT INTO detail_transaksi ---")
                    For Each item As OrderItem In keranjang
                        cmdDetail.Parameters.Clear()
                        cmdDetail.Parameters.AddWithValue("@idtransaksi", transaksiID)
                        cmdDetail.Parameters.AddWithValue("@kodebarang", item.KodeBarang)
                        cmdDetail.Parameters.AddWithValue("@jumlahbeli", item.JumlahBeli)
                        cmdDetail.Parameters.AddWithValue("@subtotal", CInt(item.Subtotal))

                        Debug.WriteLine($"  ID Transaksi: {transaksiID} ({transaksiID.GetType().Name})")
                        Debug.WriteLine($"  Kode Barang: {item.KodeBarang} ({item.KodeBarang.GetType().Name})")
                        Debug.WriteLine($"  Jumlah Beli: {item.JumlahBeli} ({item.JumlahBeli.GetType().Name})")
                        Debug.WriteLine($"  Subtotal (Integer): {CInt(item.Subtotal)} ({CInt(item.Subtotal).GetType().Name})")

                        cmdDetail.ExecuteNonQuery()
                    Next
                End Using

                MessageBox.Show("Transaksi berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' --- Update Stok Barang di Database ---
                Dim queryUpdateStok As String = "UPDATE barang SET jumlahbarang = jumlahbarang - ? WHERE kodebarang = ?"
                Using cmdUpdateStok As New OdbcCommand(queryUpdateStok, Conn)
                    For Each item As OrderItem In keranjang
                        cmdUpdateStok.Parameters.Clear()
                        cmdUpdateStok.Parameters.AddWithValue("@jumlahbeli", item.JumlahBeli)
                        cmdUpdateStok.Parameters.AddWithValue("@kodebarang", item.KodeBarang)
                        cmdUpdateStok.ExecuteNonQuery()
                    Next
                End Using
                ' --- End Update Stok Barang

                ' --- Cetak Struk ---
                CetakStruk(transaksiID, keranjang.ToList(), TotalHargaKeranjang(), selectedPaymentMethod, cashAmountGiven, changeAmount)

            End Using
        Catch ex As Exception
            MessageBox.Show($"Gagal menyimpan transaksi: {ex.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

End Class



' (Pastikan definisi Class OrderItem ada di project Anda)
Public Class OrderItem
    Public Property KodeBarang As String
    Public Property NamaBarang As String
    Public Property Harga As Decimal
    Public Property JumlahBeli As Integer
    Public ReadOnly Property Subtotal As Decimal
        Get
            Return Harga * JumlahBeli
        End Get
    End Property
End Class