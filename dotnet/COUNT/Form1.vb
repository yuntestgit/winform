Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim a As Long = 1
        'For i = 1 To 5
        '    a *= i
        'Next
        'TextBox1.Text = a

        Dim i As Long = 1307674368000 * 120
        Dim a As Long = 2432902008176640000 / i
        TextBox1.Text = a
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
