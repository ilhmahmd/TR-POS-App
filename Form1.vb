Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1

    Private Sub RichTextBox1_Enter(sender As Object, e As EventArgs) Handles txtCari.Enter
        If txtCari.Text = "Cari disini" Then
            txtCari.Text = ""
            txtCari.ForeColor = Color.Black
        End If
    End Sub

    Private Sub RichTextBox1_Leave(sender As Object, e As EventArgs) Handles txtCari.Leave
        If String.IsNullOrWhiteSpace(txtCari.Text) Then
            txtCari.Text = "Cari disini"
            txtCari.ForeColor = Color.Gray
        End If
    End Sub


    ' Aktifkan tombol
    Sub Terbuka()
        buttonproduk.Enabled = True
        buttontransaksi.Enabled = True
        buttonuser.Enabled = True
        buttonlogout.Enabled = True
        buttonlaporan.Enabled = True
    End Sub

    Sub Terbuka2()
        buttonproduk.Enabled = False
        buttontransaksi.Enabled = True
        buttonuser.Enabled = False
        buttonlogout.Enabled = True
        buttonlaporan.Enabled = True
    End Sub

    Sub KondisiAwal()
        login.TextBox1.Text = ""
        login.TextBox2.Text = ""
    End Sub
    Private Sub buttonlogout_Click(sender As Object, e As EventArgs) Handles buttonlogout.Click
        Dim result As DialogResult = MessageBox.Show(
        "Apakah Anda yakin ingin logout?",
        "Konfirmasi Logout",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If result = DialogResult.Yes Then
            login.Show()
            Call KondisiAwal()
            Me.Close()
        End If
    End Sub


    Public Sub LoadKontenKontrol(kontrol As UserControl)
        PanelMain.Controls.Clear()
        kontrol.Dock = DockStyle.Fill
        PanelMain.Controls.Add(kontrol)
    End Sub

    Private currentProdukControl As ucProduk = Nothing

    Private Sub buttonproduk_Click(sender As Object, e As EventArgs) Handles buttonproduk.Click
        currentProdukControl = New ucProduk()
        LoadKontenKontrol(currentProdukControl)
    End Sub


    Private Sub buttonDasbor_Click(sender As Object, e As EventArgs) Handles buttonDasbor.Click
        Dim dasbor As New ucDasbor()
        LoadKontenKontrol(dasbor)
    End Sub

    Private Sub buttontransaksi_Click(sender As Object, e As EventArgs) Handles buttontransaksi.Click
        Dim transaksi As New ucTrans()
        LoadKontenKontrol(transaksi)
    End Sub

    Private Sub buttonlaporan_Click(sender As Object, e As EventArgs) Handles buttonlaporan.Click
        Dim laporan As New ucLaporanPenjualan()
        LoadKontenKontrol(laporan)
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Dim keyword As String = txtCari.Text.Trim()
        If currentProdukControl IsNot Nothing Then
            currentProdukControl.FilterData(keyword)
        Else
            MessageBox.Show("Silakan buka menu Produk terlebih dahulu.")
        End If
    End Sub

    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        Dim keyword As String = txtCari.Text.Trim()

        ' Jika textbox kosong, tampilkan data full
        If keyword = "" Or keyword = "Cari disini" Then
            ResetFilter()
        Else
            FilterData(keyword)
        End If
    End Sub

    Private Sub txtCari_TextChanged2(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        Dim keyword As String = txtCari.Text.Trim()

        ' Jika textbox kosong, tampilkan data full
        If keyword = "" Or keyword = "Cari disini" Then
            ResetFilter2()
        Else
            FilterData2(keyword)
        End If
    End Sub

    Private Sub txtCari_TextChanged3(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        Dim keyword As String = txtCari.Text.Trim()

        For Each ctrl As Control In PanelMain.Controls
            If TypeOf ctrl Is ucLaporanPenjualan Then
                Dim laporan As ucLaporanPenjualan = CType(ctrl, ucLaporanPenjualan)
                If keyword = "" Or keyword = "Cari disini" Then
                    laporan.ResetFilter3()
                Else
                    laporan.FilterData3(keyword)
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub txtCari_TextChanged4(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        Dim keyword As String = txtCari.Text.Trim()

        For Each ctrl As Control In PanelMain.Controls
            If TypeOf ctrl Is ucAdmin Then
                Dim admin As ucAdmin = CType(ctrl, ucAdmin)

                If keyword = "" Or keyword = "Cari disini" Then
                    admin.TampilkanData()
                Else
                    admin.FilterData(keyword)
                End If

                Exit For
            End If
        Next
    End Sub




    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim keyword As String = txtCari.Text.Trim()
            FilterData(keyword)
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub FilterData(keyword As String)
        ' Cek UserControl ucProduk yang aktif di panel
        For Each ctrl As Control In PanelMain.Controls
            If TypeOf ctrl Is ucProduk Then
                Dim produk As ucProduk = CType(ctrl, ucProduk)
                produk.FilterData(keyword)
                Exit For
            End If
        Next
    End Sub

    Private Sub ResetFilter()
        For Each ctrl As Control In PanelMain.Controls
            If TypeOf ctrl Is ucProduk Then
                Dim produk As ucProduk = CType(ctrl, ucProduk)
                produk.TampilkanData() ' load data tanpa filter (full)
                Exit For
            End If
        Next
    End Sub

    Private Sub FilterData2(keyword As String)
        ' Cek UserControl ucProduk yang aktif di panel
        For Each ctrl As Control In PanelMain.Controls
            If TypeOf ctrl Is ucTrans Then
                Dim produk As ucTrans = CType(ctrl, ucTrans)
                produk.FilterData2(keyword)
                Exit For
            End If
        Next
    End Sub

    Private Sub ResetFilter2()
        For Each ctrl As Control In PanelMain.Controls
            If TypeOf ctrl Is ucTrans Then
                Dim produk As ucTrans = CType(ctrl, ucTrans)
                produk.TampilkanData() ' load data tanpa filter (full)
                Exit For
            End If
        Next
    End Sub

    Private Sub ResetFilter3()
        For Each ctrl As Control In PanelMain.Controls
            If TypeOf ctrl Is ucLaporanPenjualan Then
                Dim transaksi As ucLaporanPenjualan = CType(ctrl, ucLaporanPenjualan)
                transaksi.TampilkanData() ' load data tanpa filter (full)
                Exit For
            End If
        Next
    End Sub



    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub buttonuser_Click(sender As Object, e As EventArgs) Handles buttonuser.Click
        Dim admin As New ucAdmin()
        LoadKontenKontrol(admin)
    End Sub
End Class
