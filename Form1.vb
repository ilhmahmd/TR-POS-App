Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Awal tampilan
        RichTextBox1.Text = "Cari disini..."
        RichTextBox1.ForeColor = Color.Gray
    End Sub

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

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class
