Public Class ucDasbor
    Private Sub ucDasbor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelWelcome.Text = login.NamaPenggunaLogin & "!" ' ← tampilkan nama user
    End Sub
End Class
