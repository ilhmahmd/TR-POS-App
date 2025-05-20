Imports System
Imports System.Data
Imports System.Data.Odbc

Public Class ucDasbor

    Dim bulan As Integer = Date.Now.Month
    Dim tahun As Integer = Date.Now.Year
    Dim totalPenjualan As Decimal = 0

    Private ReadOnly Property KoneksiString() As String
        Get
            Return TR_POS_App.Module1.MyDB
        End Get
    End Property
    Private Sub TampilkanData()
        Try
            Koneksi() ' Pastikan koneksi terbuka dari Module1

            Dim query As String = "SELECT t.tanggaltransaksi, GROUP_CONCAT(b.namabarang SEPARATOR ', ') AS daftar_barang, t.totalharga " &
                              "FROM transaksi t " &
                              "JOIN detail_transaksi td ON t.idtransaksi = td.idtransaksi " &
                              "JOIN barang b ON td.kodebarang = b.kodebarang " &
                              "GROUP BY t.idtransaksi " &
                              "ORDER BY t.tanggaltransaksi DESC LIMIT 5"

            Da = New OdbcDataAdapter(query, Conn)
            Dim dt As New DataTable()
            Da.Fill(dt)

            dgvLaporan.DataSource = dt
            AturStylingGrid()
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub


    Private Sub AturStylingGrid()
        With dgvLaporan
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.None
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .RowHeadersVisible = False
            dgvLaporan.Enabled = False
            .ClearSelection() ' Tidak ada baris yang dipilih secara default
            .DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke
            .DefaultCellStyle.SelectionForeColor = .DefaultCellStyle.ForeColor
            .DefaultCellStyle.BackColor = Color.WhiteSmoke

            .ColumnHeadersHeight = 30
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            With .ColumnHeadersDefaultCellStyle
                .Font = New Font("Segoe UI", 10, FontStyle.Bold)
                .Alignment = DataGridViewContentAlignment.MiddleCenter
                .BackColor = Color.WhiteSmoke
            End With

            .EnableHeadersVisualStyles = False

            If .Columns.Contains("tanggaltransaksi") Then
                .Columns("tanggaltransaksi").Width = 80
                .Columns("tanggaltransaksi").HeaderText = "Tanggal"
                .Columns("tanggaltransaksi").DefaultCellStyle.Format = "dd/MM/yy"
                .Columns("tanggaltransaksi").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            If .Columns.Contains("daftar_barang") Then
                .Columns("daftar_barang").Width = 109
                .Columns("daftar_barang").HeaderText = "Barang"
                .Columns("daftar_barang").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            End If

            If .Columns.Contains("totalharga") Then
                .Columns("totalharga").Width = 80
                .Columns("totalharga").HeaderText = "Total"
                .Columns("totalharga").DefaultCellStyle.Format = "N0"
                .Columns("totalharga").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            .DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .RowTemplate.Height = 35
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .AllowUserToResizeColumns = True
        End With
    End Sub
    Sub TampilkanKalender()
        tblKalender.Controls.Clear()
        tblKalender.ColumnCount = 7
        tblKalender.RowCount = 7
        tblKalender.ColumnStyles.Clear()
        tblKalender.RowStyles.Clear()

        ' Atur kolom dan baris agar merata
        For i = 1 To 7
            tblKalender.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100 / 7))
        Next
        For i = 1 To 7
            tblKalender.RowStyles.Add(New RowStyle(SizeType.Percent, 100 / 7)) ' ← baris tinggi tetap
        Next

        ' Tampilkan nama hari
        Dim namaHari() As String = {"Sen", "Sel", "Rab", "Kam", "Jum", "Sab", "Min"}
        For i = 0 To 6
            Dim lbl As New Label With {
                .Text = namaHari(i),
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                .Margin = New Padding(2)
            }
            tblKalender.Controls.Add(lbl, i, 0)
        Next

        ' Tanggal awal bulan
        Dim tanggalAwal As New DateTime(tahun, bulan, 1)
        Dim hariAwal As Integer = (CInt(tanggalAwal.DayOfWeek) + 6) Mod 7 'Senin = 0
        Dim jumlahHari As Integer = Date.DaysInMonth(tahun, bulan)
        Dim hitung As Integer = 1

        For baris = 1 To 6
            For kolom = 0 To 6
                If baris = 1 And kolom < hariAwal Then
                    tblKalender.Controls.Add(New Label(), kolom, baris)
                ElseIf hitung <= jumlahHari Then
                    Dim lbl As New Label With {
                        .Text = hitung.ToString(),
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleCenter,
                        .Font = New Font("Segoe UI", 10),
                        .Margin = New Padding(2)
                    }

                    ' Tandai jika hari ini
                    If tahun = Date.Now.Year And bulan = Date.Now.Month And hitung = Date.Now.Day Then
                        lbl.BackColor = Color.LightCoral
                        lbl.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                        lbl.ForeColor = Color.WhiteSmoke
                    End If

                    tblKalender.Controls.Add(lbl, kolom, baris)
                    hitung += 1
                End If
            Next
        Next

        ' Tampilkan nama bulan (3 huruf) dan tahun
        lblBulan.Text = Format(New DateTime(tahun, bulan, 1), "MMM, yyyy") ' Contoh: Jan, 2025
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        bulan -= 1
        If bulan < 1 Then
            bulan = 12
            tahun -= 1
        End If
        TampilkanKalender()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        bulan += 1
        If bulan > 12 Then
            bulan = 1
            tahun += 1
        End If
        TampilkanKalender()
    End Sub

    Private Sub ucDasbor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelWelcome.Text = login.NamaPenggunaLogin & "!" ' ← tampilkan nama user
        TampilkanKalender()
        TampilkanData()
        TampilkanRingkasan()
    End Sub

    Private Sub TampilkanRingkasan()
        Try
            Koneksi()
            Dim query As String = "SELECT SUM(totalharga) AS total_penjualan FROM transaksi"
            Cmd = New OdbcCommand(query, Conn)
            Dim result = Cmd.ExecuteScalar()

            Dim totalPenjualan As Decimal = If(IsDBNull(result), 0, Convert.ToDecimal(result))
            lbPemasukan.Text = "Rp. " & totalPenjualan.ToString("N0")
        Catch ex As Exception
            MessageBox.Show("Gagal mengambil ringkasan: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult = MessageBox.Show(
        "Yakin keluar aplikasi?",
        "Exit App",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If result = DialogResult.Yes Then
            End
        End If
    End Sub
End Class
