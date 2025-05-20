Imports System.Data.Odbc

Public Class ucAdmin

    ' Menampilkan data admin ke dalam DataGridView
    Sub TampilkanData(Optional filter As String = "")
        Try
            Call Koneksi()
            Dim query As String = "SELECT * FROM admin"
            If filter <> "" Then
                query &= " WHERE kodeadmin LIKE ? OR namaadmin LIKE ?"
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

            dgvAdmin.Columns.Clear()
            dgvAdmin.DataSource = dt
            dgvAdmin.AllowUserToAddRows = False

            ' Tambahkan kembali tombol Edit dan Hapus seperti di TampilkanData()
            TambahTombolEditHapus()
            AturStylingGrid()


        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub

    ' SUB: Tambah tombol Edit dan Hapus ke DataGridView
    Private Sub TambahTombolEditHapus()
        ' Hapus kolom tombol jika sudah ada
        If dgvAdmin.Columns.Contains("EditColumn") Then dgvAdmin.Columns.Remove("EditColumn")
        If dgvAdmin.Columns.Contains("HapusColumn") Then dgvAdmin.Columns.Remove("HapusColumn")

        ' Tambah kolom tombol Edit
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
        dgvAdmin.Columns.Add(btnEdit)

        ' Tambah kolom tombol Hapus
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
        dgvAdmin.Columns.Add(btnHapus)
    End Sub

    ' SUB: Styling grid dan pengaturan kolom
    Private Sub AturStylingGrid()
        With dgvAdmin
            ' Atur alignment tengah untuk semua kolom
            For Each col As DataGridViewColumn In .Columns
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            .BorderStyle = BorderStyle.Fixed3D
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None

            .ColumnHeadersHeight = 40
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            ' Atur lebar kolom dan header text
            .Columns("kodeadmin").Width = 140
            .Columns("namaadmin").Width = 210
            .Columns("passwordadmin").Width = 150
            .Columns("leveladmin").Width = 140

            .Columns("kodeadmin").HeaderText = "Username"
            .Columns("namaadmin").HeaderText = "Nama"
            .Columns("passwordadmin").HeaderText = "Password"
            .Columns("leveladmin").HeaderText = "Level"

            ' Header style
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .EnableHeadersVisualStyles = False

            ' Isi data style
            .DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
            .RowTemplate.Height = 35

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .AllowUserToResizeColumns = True
        End With
    End Sub


    Private Sub ucAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanData()
        IsiComboBoxLevel()
    End Sub

    Private Sub dgvAdmin_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAdmin.CellClick
        If e.RowIndex < 0 Then Exit Sub ' Abaikan klik header

        Dim kode As String = dgvAdmin.Rows(e.RowIndex).Cells("kodeadmin").Value.ToString()
        Dim nama As String = dgvAdmin.Rows(e.RowIndex).Cells("namaadmin").Value.ToString()

        If e.ColumnIndex = dgvAdmin.Columns("EditColumn").Index Then
            Dim frm As New FormAdmin()
            frm.ModeEdit = True
            frm.KodeAdminEdit = kode
            frm.ShowDialog()
            TampilkanData()

        ElseIf e.ColumnIndex = dgvAdmin.Columns("HapusColumn").Index Then
            If MessageBox.Show("Yakin menghapus user : " & nama & "?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Try
                    Call Koneksi()
                    Using cmd As New OdbcCommand("DELETE FROM admin WHERE kodeadmin = ?", Conn)
                        cmd.Parameters.AddWithValue("@kode", kode)
                        cmd.ExecuteNonQuery()
                    End Using
                    MessageBox.Show("Data user berhasil dihapus.")
                    TampilkanData()
                Catch ex As Exception
                    MessageBox.Show("Gagal menghapus data user :" & ex.Message)
                Finally
                    If Conn.State = ConnectionState.Open Then Conn.Close()
                End Try
            End If
        End If
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Dim frm As New FormAdmin()
        frm.ModeEdit = False
        frm.ShowDialog()
        TampilkanData()
    End Sub


    Public Sub FilterData(keyword As String)
        Try
            Call Koneksi()
            Dim query As String = "SELECT * FROM admin WHERE kodeadmin LIKE ? OR namaadmin LIKE ? OR leveladmin LIKE ?"
            Using da As New OdbcDataAdapter(query, Conn)
                da.SelectCommand.Parameters.AddWithValue("@p1", "%" & keyword & "%")
                da.SelectCommand.Parameters.AddWithValue("@p2", "%" & keyword & "%")
                da.SelectCommand.Parameters.AddWithValue("@p3", "%" & keyword & "%")


                Dim dt As New DataTable
                da.Fill(dt)

                dgvAdmin.Columns.Clear()
                dgvAdmin.DataSource = dt
                dgvAdmin.AllowUserToAddRows = False

                AturStylingGrid()
                TambahTombolEditHapus()

            End Using
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat filter data : " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub


    Private Sub IsiComboBoxLevel()
        Try
            Call Koneksi()
            Dim query As String = "SELECT DISTINCT leveladmin FROM admin"
            Dim cmd As New OdbcCommand(query, Conn)
            Dim reader = cmd.ExecuteReader()

            cmbLevel.Items.Clear()
            cmbLevel.Items.Add("- Level -") ' default

            While reader.Read()
                cmbLevel.Items.Add(reader("leveladmin").ToString())
            End While

            cmbLevel.SelectedIndex = 0 ' set default
        Catch ex As Exception
            MessageBox.Show("Error isi ComboBox: " & ex.Message)
        Finally
            If Conn.State = ConnectionState.Open Then Conn.Close()
        End Try
    End Sub



    Private Sub cmbLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLevel.SelectedIndexChanged
        Dim selectedLevel As String = cmbLevel.SelectedItem?.ToString()
        If selectedLevel = "- Level -" Then
            TampilkanData2()
        Else
            TampilkanData2(selectedLevel)
        End If
    End Sub

    Public Sub TampilkanData2(Optional filterLevel As String = "")
        Try
            Call Koneksi()
            Dim query As String = "SELECT * FROM admin"
            If filterLevel <> "" And filterLevel <> "- Level -" Then
                query &= " WHERE leveladmin = ?"
            End If

            Dim da As OdbcDataAdapter
            If filterLevel <> "" And filterLevel <> "- Level -" Then
                da = New OdbcDataAdapter(query, Conn)
                da.SelectCommand.Parameters.AddWithValue("@level", filterLevel)
            Else
                da = New OdbcDataAdapter(query, Conn)
            End If

            Dim dt As New DataTable
            da.Fill(dt)

            dgvAdmin.Columns.Clear()
            dgvAdmin.DataSource = dt
            dgvAdmin.AllowUserToAddRows = False

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
