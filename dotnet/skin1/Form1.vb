Public Class Form1

    'Dim pos As Point
    Dim pos As MouseEventArgs = Nothing

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = 0
        Me.Left = 0
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        PictureBox1.Top -= 10
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        PictureBox1.Top += 10
    End Sub

    Private Sub PictureBox1_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        Dim c As Control = DirectCast(sender, Control) ' 型別轉換作業

        If c.Capture Then ' 控制項是否 Capture Mouse

            ' 指定控制項新位置

            'c.Location = New Point(e.X + c.Location.X - pos.X, e.Y + c.Location.Y - pos.Y)

            'c.Left = e.X + c.Location.X - pos.X

            c.Top = e.Y + c.Location.Y - pos.Y
        End If

    End Sub

    Private Sub PictureBox1_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        ' pos = MousePosition
        pos = e
    End Sub
End Class
