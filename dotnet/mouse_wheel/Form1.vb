Public Class Form1
    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If e.Delta < 0 Then
            Me.Text = "down"
            'Debug.WriteLine("滑鼠向後滾")
        Else
            Me.Text = "up"
            'Debug.WriteLine("滑鼠向前滾")
        End If
    End Sub
End Class
