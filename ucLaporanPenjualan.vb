Imports System
Imports System.Data
Imports System.Data.Odbc
Imports System.Windows.Forms
Imports System.Drawing
Imports OfficeOpenXml
Imports System.IO

Public Class ucLaporanPenjualan

    Private ReadOnly Property KoneksiString() As String
        Get
            Return TR_POS_App.Module1.MyDB
        End Get
    End Property

    Private Sub ucLaporanPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbFilterMetodeBayar.Items.Clear()
        cbFilterMetodeBayar.Items.Add("- Metode -")

        Try
            Module1.Koneksi()
            Using cmd As New OdbcCommand("SELECT DISTINCT metode_bayar FROM transaksi ORDER BY metode_bayar", Module1.Conn)
                Using reader As OdbcDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        cbFilterMetodeBayar.Items.Add(reader("metode_bayar").ToString())
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Gagal mengisi filter metode bayar: " & ex.Message)
        End Try
        cbFilterMetodeBayar.SelectedIndex = 0

        cbFilterKasir.Items.Clear()
        cbFilterKasir.Items.Add("- Kasir -")
        Try
            Module1.Koneksi()
            Using cmd As New OdbcCommand("SELECT namaadmin FROM admin ORDER BY namaadmin", Module1.Conn)
                Using reader As OdbcDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        cbFilterKasir.Items.Add(reader("namaadmin").ToString())
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Gagal mengisi filter kasir: " & ex.Message)
        End Try
        cbFilterKasir.SelectedIndex = 0

        cbUrutkanTransaksi.Items.Clear()
        cbUrutkanTransaksi.Items.Add("- Harga -")
        cbUrutkanTransaksi.Items.Add("Tertinggi")
        cbUrutkanTransaksi.Items.Add("Terendah")
        cbUrutkanTransaksi.SelectedIndex = 0

        dtpTanggalAwal.Value = DateTime.Today.AddDays(-7)
        dtpTanggalAkhir.Value = DateTime.Today

        TampilkanData()
    End Sub

    Sub TampilkanData(Optional metodeBayarFilter As String = "", Optional kasirFilter As String = "", Optional sortBy As String = "", Optional tanggalAwalFilter As DateTime? = Nothing, Optional tanggalAkhirFilter As DateTime? = Nothing)
        Try
            Module1.Koneksi()
            Dim query As String = "SELECT t.idtransaksi, t.tanggaltransaksi, a.namaadmin, GROUP_CONCAT(b.namabarang SEPARATOR ', ') AS daftar_barang, t.totalharga, t.metode_bayar " &
                                  "FROM transaksi t " &
                                  "INNER JOIN admin a ON t.kodeadmin = a.kodeadmin " &
                                  "INNER JOIN detail_transaksi dt ON t.idtransaksi = dt.idtransaksi " &
                                  "INNER JOIN barang b ON dt.kodebarang = b.kodebarang " &
                                  "WHERE 1=1 "

            Dim conditions As New List(Of String)()
            Dim parameters As New List(Of OdbcParameter)()

            If Not String.IsNullOrEmpty(metodeBayarFilter) AndAlso metodeBayarFilter <> "- Metode -" Then
                conditions.Add("t.metode_bayar = ?")
                parameters.Add(New OdbcParameter("", metodeBayarFilter))
            End If

            If Not String.IsNullOrEmpty(kasirFilter) AndAlso kasirFilter <> "- Kasir -" Then
                conditions.Add("a.namaadmin = ?")
                parameters.Add(New OdbcParameter("", kasirFilter))
            End If

            If tanggalAwalFilter.HasValue AndAlso tanggalAkhirFilter.HasValue Then
                conditions.Add("t.tanggaltransaksi >= ? AND t.tanggaltransaksi <= ?")
                parameters.Add(New OdbcParameter("", tanggalAwalFilter.Value.Date))
                parameters.Add(New OdbcParameter("", tanggalAkhirFilter.Value.Date.AddDays(1).AddSeconds(-1)))
            End If

            If conditions.Count > 0 Then
                query &= " AND " & String.Join(" AND ", conditions)
            End If

            query &= " GROUP BY t.idtransaksi, t.tanggaltransaksi, a.namaadmin, t.totalharga, t.metode_bayar "

            If sortBy = "Tertinggi" Then
                query &= " ORDER BY t.totalharga DESC"
            ElseIf sortBy = "Terendah" Then
                query &= " ORDER BY t.totalharga ASC"
            End If

            Using cmd As New OdbcCommand(query, Module1.Conn)
                cmd.Parameters.AddRange(parameters.ToArray())
                Using da As New OdbcDataAdapter(cmd)
                    Dim dt As New DataTable
                    da.Fill(dt)

                    dgvLaporan.Columns.Clear()
                    dgvLaporan.DataSource = dt
                    dgvLaporan.AllowUserToAddRows = False
                    AturStylingGrid()
                    TampilkanRingkasan(dt)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub

    Private Sub TampilkanRingkasan(dt As DataTable)
        Dim totalTransaksi As Integer = dt.Rows.Count
        Dim totalPenjualan As Decimal = 0

        For Each row As DataRow In dt.Rows
            totalPenjualan += Convert.ToDecimal(row("totalharga"))
        Next

        lblTotalTransaksi.Text = totalTransaksi.ToString("N0")
        lblTotalPenjualan.Text = "Rp. " & totalPenjualan.ToString("N0")
    End Sub

    Private Sub AturStylingGrid()
        With dgvLaporan
            For Each col As DataGridViewColumn In dgvLaporan.Columns
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft ' Default jadi kiri
                col.DefaultCellStyle.Padding = New Padding(0, 5, 0, 5)
            Next

            .BorderStyle = BorderStyle.Fixed3D
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .RowHeadersVisible = False

            .ColumnHeadersHeight = 40
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            With .ColumnHeadersDefaultCellStyle
                .Font = New Font("Segoe UI", 10, FontStyle.Bold)
                .Alignment = DataGridViewContentAlignment.MiddleCenter
                .BackColor = Color.WhiteSmoke
            End With
            .EnableHeadersVisualStyles = False

            ' Atur lebar kolom
            If .Columns.Contains("idtransaksi") Then
                .Columns("idtransaksi").Width = 30
                .Columns("idtransaksi").HeaderText = "ID"
                .Columns("idtransaksi").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If .Columns.Contains("tanggaltransaksi") Then
                .Columns("tanggaltransaksi").Width = 180
                .Columns("tanggaltransaksi").HeaderText = "Tanggal Transaksi"
                .Columns("tanggaltransaksi").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"
                .Columns("tanggaltransaksi").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If .Columns.Contains("namaadmin") Then
                .Columns("namaadmin").Width = 150
                .Columns("namaadmin").HeaderText = "Kasir"
                .Columns("namaadmin").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If .Columns.Contains("daftar_barang") Then
                .Columns("daftar_barang").Width = 320
                .Columns("daftar_barang").HeaderText = "Barang"
                .Columns("daftar_barang").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            End If
            If .Columns.Contains("totalharga") Then
                .Columns("totalharga").Width = 100
                .Columns("totalharga").HeaderText = "Total Harga"
                .Columns("totalharga").DefaultCellStyle.Format = "N0"
                .Columns("totalharga").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If .Columns.Contains("metode_bayar") Then
                .Columns("metode_bayar").Width = 100
                .Columns("metode_bayar").HeaderText = "Metode Bayar"
                .Columns("metode_bayar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            ' Style isi cell
            .DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .RowTemplate.Height = 35

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .AllowUserToResizeColumns = True
        End With
    End Sub

    ' Event handler filter
    Private Sub cbFilterMetodeBayar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterMetodeBayar.SelectedIndexChanged
        FilterData()
    End Sub

    Private Sub cbFilterKasir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterKasir.SelectedIndexChanged
        FilterData()
    End Sub

    Private Sub cbUrutkanTransaksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbUrutkanTransaksi.SelectedIndexChanged
        FilterData()
    End Sub

    Private Sub dtpTanggalAwal_ValueChanged(sender As Object, e As EventArgs) Handles dtpTanggalAwal.ValueChanged
        FilterData()
    End Sub

    Private Sub dtpTanggalAkhir_ValueChanged(sender As Object, e As EventArgs) Handles dtpTanggalAkhir.ValueChanged
        FilterData()
    End Sub

    Private Sub FilterData()
        TampilkanData(
            metodeBayarFilter:=cbFilterMetodeBayar.SelectedItem?.ToString(),
            kasirFilter:=cbFilterKasir.SelectedItem?.ToString(),
            sortBy:=cbUrutkanTransaksi.SelectedItem?.ToString(),
            tanggalAwalFilter:=dtpTanggalAwal.Value,
            tanggalAkhirFilter:=dtpTanggalAkhir.Value
        )
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        ' Set License Context wajib
        ExcelPackage.License.SetNonCommercialPersonal("<Ilham Ahmad>")

        ' Tampilkan dialog simpan file
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.FileName = "Laporan Penjualan_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".xlsx"
        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                ' Buat file Excel baru dengan EPPlus
                Dim fileInfo As New FileInfo(saveFileDialog.FileName)

                Using package As New ExcelPackage(fileInfo)
                    ' Tambah worksheet baru
                    Dim worksheet = package.Workbook.Worksheets.Add("Laporan")

                    ' Tulis header kolom
                    For col As Integer = 0 To dgvLaporan.Columns.Count - 1
                        worksheet.Cells(1, col + 1).Value = dgvLaporan.Columns(col).HeaderText
                    Next

                    ' Tulis data dari DataGridView
                    For row As Integer = 0 To dgvLaporan.Rows.Count - 1
                        For col As Integer = 0 To dgvLaporan.Columns.Count - 1
                            worksheet.Cells(row + 2, col + 1).Value = dgvLaporan.Rows(row).Cells(col).Value?.ToString()
                        Next
                    Next

                    ' Autofit kolom
                    worksheet.Cells.AutoFitColumns()

                    ' Simpan file Excel
                    package.Save()

                    MessageBox.Show("Laporan berhasil diexport ke Excel.", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using

            Catch ex As Exception
                MessageBox.Show("Gagal export ke Excel: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnExportCetak_Click(sender As Object, e As EventArgs) Handles btnExportCetak.Click
        ' Implementasi export ke PDF atau cetak (gunakan PrintDocument atau library lain)
        Dim printDialog As New PrintDialog()
        Dim printDocument As New Printing.PrintDocument()

        ' Set properties printDocument jika diperlukan
        printDialog.Document = printDocument

        If printDialog.ShowDialog() = DialogResult.OK Then
            Try
                ' Implementasikan logika pencetakan di sini
                ' Anda perlu menangani PrintPage event dari printDocument
                ' untuk mengatur tata letak dan konten yang akan dicetak
                AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage
                printDocument.Print()
                RemoveHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage
            Catch ex As Exception
                MessageBox.Show("Gagal mencetak: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)
        ' Implementasikan logika untuk menggambar konten laporan pada halaman cetak
        ' Ini adalah bagian yang cukup kompleks dan memerlukan penyesuaian
        ' berdasarkan tata letak yang Anda inginkan.

        Dim fontHeader As New System.Drawing.Font("Arial", 12, FontStyle.Bold)
        Dim fontBody As New System.Drawing.Font("Arial", 10)
        Dim margin As Integer = 50
        Dim y As Integer = margin
        Dim lineHeight As Integer = fontBody.GetHeight(e.Graphics) + 5

        ' Judul Laporan
        e.Graphics.DrawString("Laporan Penjualan", fontHeader, Brushes.Black, margin, y)
        y += lineHeight * 2

        ' Header Kolom
        Dim x As Integer = margin
        For Each column As DataGridViewColumn In dgvLaporan.Columns
            e.Graphics.DrawString(column.HeaderText, fontBody, Brushes.Black, x, y)
            x += column.Width ' Perkiraan lebar, perlu disesuaikan
        Next
        y += lineHeight

        ' Data Baris
        For Each row As DataGridViewRow In dgvLaporan.Rows
            x = margin
            For Each cell As DataGridViewCell In row.Cells
                e.Graphics.DrawString(cell.Value?.ToString(), fontBody, Brushes.Black, x, y)
                x += dgvLaporan.Columns(cell.ColumnIndex).Width ' Perkiraan lebar
            Next
            y += lineHeight
            If y > e.MarginBounds.Bottom - lineHeight Then
                e.HasMorePages = True
                Return
            End If
        Next

        e.HasMorePages = False
    End Sub

    Public Sub ResetFilter3()
        TampilkanData() ' Menampilkan semua data tanpa filter
    End Sub

    Public Sub FilterData3(keyword As String)
        Try
            Module1.Koneksi()
            Dim query As String = "SELECT t.idtransaksi, t.tanggaltransaksi, a.namaadmin, " &
                              "GROUP_CONCAT(b.namabarang SEPARATOR ', ') AS daftar_barang, " &
                              "t.totalharga, t.metode_bayar " &
                              "FROM transaksi t " &
                              "INNER JOIN admin a ON t.kodeadmin = a.kodeadmin " &
                              "INNER JOIN detail_transaksi dt ON t.idtransaksi = dt.idtransaksi " &
                              "INNER JOIN barang b ON dt.kodebarang = b.kodebarang " &
                              "WHERE t.idtransaksi LIKE ? OR a.namaadmin LIKE ? OR b.namabarang LIKE ? " &
                              "GROUP BY t.idtransaksi, t.tanggaltransaksi, a.namaadmin, t.totalharga, t.metode_bayar"

            Using cmd As New OdbcCommand(query, Module1.Conn)
                Dim param As String = "%" & keyword & "%"
                cmd.Parameters.AddWithValue("@id", param)
                cmd.Parameters.AddWithValue("@admin", param)
                cmd.Parameters.AddWithValue("@barang", param)

                Using da As New OdbcDataAdapter(cmd)
                    Dim dt As New DataTable
                    da.Fill(dt)

                    dgvLaporan.Columns.Clear()
                    dgvLaporan.DataSource = dt
                    dgvLaporan.AllowUserToAddRows = False
                    AturStylingGrid()
                    TampilkanRingkasan(dt)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat mencari: " & ex.Message)
        End Try
    End Sub


End Class