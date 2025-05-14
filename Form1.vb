Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1

    Private Sub RichTextBox1_Enter(sender As Object, e As EventArgs) Handles RichTextBox1.Enter
        If RichTextBox1.Text = "Cari disini..." Then
            RichTextBox1.Text = ""
            RichTextBox1.ForeColor = Color.Black
        End If
    End Sub

    Private Sub RichTextBox1_Leave(sender As Object, e As EventArgs) Handles RichTextBox1.Leave
        If String.IsNullOrWhiteSpace(RichTextBox1.Text) Then
            RichTextBox1.Text = "Cari disini..."
            RichTextBox1.ForeColor = Color.Gray
        End If
    End Sub

    ' Tidak perlu parameter karena ini memanggil kontrol di dalam form ini

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
        login.Show()
        Call KondisiAwal()
        Me.Close()
    End Sub

    Public Sub LoadKontenKontrol(kontrol As UserControl)
        PanelMain.Controls.Clear()
        kontrol.Dock = DockStyle.Fill
        PanelMain.Controls.Add(kontrol)
    End Sub

    Private Sub buttonproduk_Click(sender As Object, e As EventArgs) Handles buttonproduk.Click
        Dim produk As New ucProduk()
        LoadKontenKontrol(produk)
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
        Dim laporan As New ucLapor()
        LoadKontenKontrol(laporan)
    End Sub
End Class
