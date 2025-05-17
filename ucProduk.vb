Imports System.Data.Odbc

Public Class ucProduk

    ' Menampilkan data produk ke dalam DataGridView
    Sub TampilkanData(Optional filter As String = "")
        Try
            Call Koneksi()
            Dim query As String = "SELECT * FROM barang"
            If filter <> "" Then
                query &= " WHERE kodebarang LIKE ? OR namabarang LIKE ?"
            End If

            Dim da As OdbcDataAdapter
            If filter <> "" Then
                da = New OdbcDataAdapter(query, Conn)
                da.SelectCommand.Parameters.AddWithValue("@kode", "%" & filter & "%")
                da.SelectCommand.Parameters.AddWithValue("@nama", "%" & filter & "%")
            Else
                da = New OdbcDataAdapter(query, Conn)
            End If

            Dim dt As New DataTable
            da.Fill(dt)

            dgvProduk.Columns.Clear()
            dgvProduk.DataSource = dt
            dgvProduk.AllowUserToAddRows = False

            ' Hapus kolom tombol jika sudah ada
            If dgvProduk.Columns.Contains("EditColumn") Then dgvProduk.Columns.Remove("EditColumn")
            If dgvProduk.Columns.Contains("HapusColumn") Then dgvProduk.Columns.Remove("HapusColumn")

            ' Kolom tombol Edit
            Dim btnEdit As New DataGridViewButtonColumn With {
                .Name = "EditColumn",
                .HeaderText = "",
                .Text = "Edit",
                .UseColumnTextForButtonValue = True,
                .Width = 90,
                .DefaultCellStyle = New DataGridViewCellStyle With {
                        .BackColor = Color.LightGreen,
                        .ForeColor = Color.Black,
                        .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                        .Alignment = DataGridViewContentAlignment.MiddleCenter,
                        .Padding = New Padding(2)
                 }
            }
            dgvProduk.Columns.Add(btnEdit)

            ' Kolom tombol Hapus
            Dim btnHapus As New DataGridViewButtonColumn With {
                .Name = "HapusColumn",
                .HeaderText = "",
                .Text = "Hapus",
                .UseColumnTextForButtonValue = True,
                .Width = 90,
                .DefaultCellStyle = New DataGridViewCellStyle With {
                    .BackColor = Color.IndianRed,
                    .ForeColor = Color.White,
                    .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                    .Alignment = DataGridViewContentAlignment.MiddleCenter,
                    .Padding = New Padding(2)
                }
            }
            dgvProduk.Columns.Add(btnHapus)


            ' Atur lebar kolom utama dan styling
            With dgvProduk
                For Each col As DataGridViewColumn In dgvProduk.Columns
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Next

                .BorderStyle = BorderStyle.Fixed3D
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None

                .ColumnHeadersHeight = 40 ' misal 40 pixel, sesuaikan dengan kebutuhan
                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

                ' Atur lebar kolom
                .Columns("kodebarang").Width = 140
                .Columns("namabarang").Width = 200
                .Columns("hargabarang").Width = 150
                .Columns("jumlahbarang").Width = 150

                ' Ubah header text sesuai keinginan, tidak perlu sesuai nama kolom DB
                .Columns("kodebarang").HeaderText = "Kode Barang"
                .Columns("namabarang").HeaderText = "Nama Barang"
                .Columns("hargabarang").HeaderText = "Harga"
                .Columns("jumlahbarang").HeaderText = "Jumlah Stok"

                ' Style header
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
                .EnableHeadersVisualStyles = False

                ' Style isi cell
                .DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .RowTemplate.Height = 35

                .Columns("hargabarang").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("jumlahbarang").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                .AllowUserToResizeColumns = True
            End With


        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

    ' Saat UserControl dimuat
    Private Sub ucProduk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanData()
    End Sub

    ' Event klik tombol Edit / Hapus
    Private Sub dgvProduk_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProduk.CellClick
        If e.RowIndex < 0 Then Exit Sub ' Abaikan klik header

        Dim kode As String = dgvProduk.Rows(e.RowIndex).Cells("kodebarang").Value.ToString()
        Dim nama As String = dgvProduk.Rows(e.RowIndex).Cells("namabarang").Value.ToString()

        ' Jika klik tombol Edit
        If e.ColumnIndex = dgvProduk.Columns("EditColumn").Index Then
            Dim frm As New FormBarang()
            frm.ModeEdit = True
            frm.KodeBarangEdit = kode
            frm.ShowDialog()
            TampilkanData()

            ' Jika klik tombol Hapus
        ElseIf e.ColumnIndex = dgvProduk.Columns("HapusColumn").Index Then
            If MessageBox.Show("Yakin ingin menghapus barang : " & nama & "?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Try
                    Call Koneksi()
                    Using cmd As New OdbcCommand("DELETE FROM barang WHERE kodebarang = ?", Conn)
                        cmd.Parameters.AddWithValue("@kode", kode)
                        cmd.ExecuteNonQuery()
                    End Using
                    MessageBox.Show("Data berhasil dihapus.")
                    TampilkanData()
                Catch ex As Exception
                    MessageBox.Show("Gagal menghapus data: " & ex.Message)
                Finally
                    If Conn.State = ConnectionState.Open Then Conn.Close()
                End Try
            End If
        End If
    End Sub

    ' Styling tombol Edit dan Hapus
    Private Sub dgvProduk_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvProduk.CellFormatting
        If e.RowIndex < 0 Then Exit Sub

        ' Format harga dengan Rp dan titik ribuan
        If dgvProduk.Columns(e.ColumnIndex).Name = "hargabarang" Then
            Dim hargaValue As Integer
            If Integer.TryParse(e.Value?.ToString(), hargaValue) Then
                e.Value = "Rp " & hargaValue.ToString("N0").Replace(",", ".")
                e.FormattingApplied = True
            End If
        End If

        ' Format tombol Edit
        If dgvProduk.Columns(e.ColumnIndex).Name = "EditColumn" Then
            dgvProduk.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.LightGreen
            dgvProduk.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Black
            dgvProduk.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        ElseIf dgvProduk.Columns(e.ColumnIndex).Name = "HapusColumn" Then
            dgvProduk.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.LightCoral
            dgvProduk.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            dgvProduk.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        End If
    End Sub



    ' Tombol tambah barang
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frmBarang As New FormBarang()
        frmBarang.ModeEdit = False
        frmBarang.ShowDialog()
        TampilkanData()
    End Sub

    Public Sub FilterData(keyword As String)
        Try
            Call Koneksi()
            Dim query As String = "SELECT * FROM barang WHERE kodebarang LIKE ? OR namabarang LIKE ?"
            Using da As New OdbcDataAdapter(query, Conn)
                da.SelectCommand.Parameters.AddWithValue("@p1", "%" & keyword & "%")
                da.SelectCommand.Parameters.AddWithValue("@p2", "%" & keyword & "%")

                Dim dt As New DataTable
                da.Fill(dt)

                dgvProduk.Columns.Clear()
                dgvProduk.DataSource = dt
                dgvProduk.AllowUserToAddRows = False

                ' Tambahkan kembali tombol Edit dan Hapus seperti di TampilkanData()
                TambahTombolEditHapus()
                AturStylingGrid()
            End Using
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat filter: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

    Private Sub TambahTombolEditHapus()
        ' Hapus kolom tombol jika sudah ada
        If dgvProduk.Columns.Contains("EditColumn") Then dgvProduk.Columns.Remove("EditColumn")
        If dgvProduk.Columns.Contains("HapusColumn") Then dgvProduk.Columns.Remove("HapusColumn")

        Dim btnEdit As New DataGridViewButtonColumn With {
        .Name = "EditColumn",
        .HeaderText = "",
        .Text = "Edit",
        .UseColumnTextForButtonValue = True,
        .Width = 90,
        .DefaultCellStyle = New DataGridViewCellStyle With {
            .BackColor = Color.LightGreen,
            .ForeColor = Color.Black,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .Alignment = DataGridViewContentAlignment.MiddleCenter,
            .Padding = New Padding(2)
        }
    }
        dgvProduk.Columns.Add(btnEdit)

        Dim btnHapus As New DataGridViewButtonColumn With {
        .Name = "HapusColumn",
        .HeaderText = "",
        .Text = "Hapus",
        .UseColumnTextForButtonValue = True,
        .Width = 90,
        .DefaultCellStyle = New DataGridViewCellStyle With {
            .BackColor = Color.IndianRed,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .Alignment = DataGridViewContentAlignment.MiddleCenter,
            .Padding = New Padding(2)
        }
    }
        dgvProduk.Columns.Add(btnHapus)
    End Sub

    Private Sub AturStylingGrid()
        ' Atur styling kolom, header, font, dll
        With dgvProduk
            For Each col As DataGridViewColumn In dgvProduk.Columns
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            .BorderStyle = BorderStyle.Fixed3D
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None

            .ColumnHeadersHeight = 40
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            ' Atur lebar kolom sesuai kolom yang ada
            If .Columns.Contains("kodebarang") Then
                .Columns("kodebarang").Width = 140
                .Columns("kodebarang").HeaderText = "Kode Barang"
            End If
            If .Columns.Contains("namabarang") Then
                .Columns("namabarang").Width = 200
                .Columns("namabarang").HeaderText = "Nama Barang"
            End If
            If .Columns.Contains("hargabarang") Then
                .Columns("hargabarang").Width = 150
                .Columns("hargabarang").HeaderText = "Harga"
                .Columns("hargabarang").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If .Columns.Contains("jumlahbarang") Then
                .Columns("jumlahbarang").Width = 150
                .Columns("jumlahbarang").HeaderText = "Jumlah Stok"
                .Columns("jumlahbarang").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .EnableHeadersVisualStyles = False

            .DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .RowTemplate.Height = 35

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .AllowUserToResizeColumns = True
        End With
    End Sub

End Class
