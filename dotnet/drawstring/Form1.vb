Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Form1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim g As Graphics = Me.CreateGraphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed

        Dim sb As New SolidBrush(Color.Red)
        Dim db As New Drawing2D.LinearGradientBrush(New Point(0, 52), New Point(0, 0), Color.White, Color.Red)
        Dim dstr As String = "繁體中文測試啦AbCdE"
        g.DrawString(dstr, New Font("新細明體", 40, FontStyle.Bold), Brushes.Black, 50, 50)
        g.DrawString(dstr, New Font("新細明體", 40, FontStyle.Bold), Brushes.Black, 50, 52)
        g.DrawString(dstr, New Font("新細明體", 40, FontStyle.Bold), Brushes.Black, 52, 50)
        g.DrawString(dstr, New Font("新細明體", 40, FontStyle.Bold), Brushes.Black, 52, 52)
        g.DrawString(dstr, New Font("新細明體", 40, FontStyle.Bold), db, 51, 51)

    End Sub
End Class
