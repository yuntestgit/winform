Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim test(1) As String
        test(0) = "as"
        Me.Text = test(0) & ", " & test.Length
    End Sub
End Class
