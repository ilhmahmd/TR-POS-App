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
End Class
