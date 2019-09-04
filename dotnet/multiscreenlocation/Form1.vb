Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Left = 1921
        Me.Top = 74
        Me.Width = 1279
        Me.Height = 1024
    End Sub

    Private Sub Panel1_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDoubleClick
        Me.Close()
    End Sub
End Class
