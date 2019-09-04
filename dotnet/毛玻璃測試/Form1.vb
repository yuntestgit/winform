Public Class Form1

    Private Sub Form1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim g As Graphics = Me.CreateGraphics
        g.DrawPath(New Pen(Color.FromArgb(255, 255, 255), 1), gpath())
    End Sub

    Function gpath() As System.Drawing.Drawing2D.GraphicsPath
        Dim r As New System.Drawing.Drawing2D.GraphicsPath
        'r.AddRectangle(New Rectangle(0, 0, 1, 1))
        For y = 0 To Me.Height - 1
            For x = 0 To Me.Width - 1
                If x Mod 3 = 0 And y Mod 3 = 0 Then
                    r.AddRectangle(New Rectangle(x, y, 1, 1))
                End If
            Next
        Next
        Return r
    End Function

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Form2.Show()
        Form2.Top = Me.Top
        Form2.Left = Me.Left
        Form2.Owner = Me
    End Sub
End Class
