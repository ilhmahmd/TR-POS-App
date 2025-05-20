Imports System.Data.Odbc

Public Class ucProduk

    ' Menampilkan data produk ke dalam DataGridView
    Sub TampilkanData(Optional filter As String = "", Optional tipeFilter As String = "")
        Try
            Call Koneksi()
            Dim query As String = "SELECT * FROM barang WHERE 1=1"

            If filter <> "" Then
                query &= " AND (kodebarang LIKE ? OR namabarang LIKE ?)"
            End If

            If tipeFilter <> "" Then
                query &= " AND tipebarang = ?"
            End If

            Dim da As New OdbcDataAdapter(query, Conn)

            If filter <> "" Then
                da.SelectCommand.Parameters.AddWithValue("@kode", "%" & filter & "%")
                da.SelectCommand.Parameters.AddWithValue("@nama", "%" & filter & "%")
            End If

            If tipeFilter <> "" Then
                da.SelectCommand.Parameters.AddWithValue("@tipe", tipeFilter)
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

            ' Format tampilan DataGridView
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

                ' Lebar kolom dan header
                .Columns("kodebarang").Width = 80
                .Columns("namabarang").Width = 200
                .Columns("tipebarang").Width = 120
                .Columns("hargabarang").Width = 150
                .Columns("jumlahbarang").Width = 90

                .Columns("kodebarang").HeaderText = "Kode Barang"
                .Columns("namabarang").HeaderText = "Nama Barang"
                .Columns("tipebarang").HeaderText = "Tipe"
                .Columns("hargabarang").HeaderText = "Harga"
                .Columns("jumlahbarang").HeaderText = "Jumlah Stok"

                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
                .EnableHeadersVisualStyles = False

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
        IsiComboBoxTipe()
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
                .Columns("kodebarang").Width = 80
                .Columns("kodebarang").HeaderText = "Kode Barang"
            End If
            If .Columns.Contains("namabarang") Then
                .Columns("namabarang").Width = 200
                .Columns("namabarang").HeaderText = "Nama Barang"
            End If
            If .Columns.Contains("tipebarang") Then
                .Columns("tipebarang").Width = 120
                .Columns("tipebarang").HeaderText = "Tipe"
            End If
            If .Columns.Contains("hargabarang") Then
                .Columns("hargabarang").Width = 150
                .Columns("hargabarang").HeaderText = "Harga"
                .Columns("hargabarang").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If .Columns.Contains("jumlahbarang") Then
                .Columns("jumlahbarang").Width = 90
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

    Private Sub IsiComboBoxTipe()
        Try
            Call Koneksi()
            Dim query As String = "SELECT DISTINCT tipebarang FROM barang"
            Dim cmd As New OdbcCommand(query, Conn)
            Dim reader = cmd.ExecuteReader()

            cbTipe.Items.Clear()
            cbTipe.Items.Add("- Tipe -") ' default

            While reader.Read()
                cbTipe.Items.Add(reader("tipebarang").ToString())
            End While

            cbTipe.SelectedIndex = 0 ' set default
        Catch ex As Exception
            MessageBox.Show("Error isi ComboBox: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub



    Private Sub cmbLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTipe.SelectedIndexChanged
        Dim selectedLevel As String = cbTipe.SelectedItem?.ToString()
        If selectedLevel = "- Level -" Then
            TampilkanData2()
        Else
            TampilkanData2(selectedLevel)
        End If
    End Sub

    Public Sub TampilkanData2(Optional filtertipe As String = "")
        Try
            Call Koneksi()
            Dim query As String = "SELECT * FROM barang"
            If filtertipe <> "" And filtertipe <> "- Tipe -" Then
                query &= " WHERE tipebarang = ?"
            End If

            Dim da As OdbcDataAdapter
            If filtertipe <> "" And filtertipe <> "- Tipe -" Then
                da = New OdbcDataAdapter(query, Conn)
                da.SelectCommand.Parameters.AddWithValue("@tipe", filtertipe)
            Else
                da = New OdbcDataAdapter(query, Conn)
            End If

            Dim dt As New DataTable
            da.Fill(dt)

            dgvProduk.Columns.Clear()
            dgvProduk.DataSource = dt
            dgvProduk.AllowUserToAddRows = False

            ' Panggil styling dan tombol agar tampil sama persis
            AturStylingGrid()
            TambahTombolEditHapus()

        Catch ex As Exception
            MessageBox.Show("Error tampil data: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

End Class
