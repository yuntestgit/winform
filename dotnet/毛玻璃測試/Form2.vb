Public Class Form2

    Private Sub Form2_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim g As Graphics = Me.CreateGraphics
        g.DrawPath(New Pen(Color.FromArgb(100, 100, 100), 1), gpath())
    End Sub

    Function gpath() As System.Drawing.Drawing2D.GraphicsPath
        Dim r As New System.Drawing.Drawing2D.GraphicsPath
        'r.AddRectangle(New Rectangle(0, 0, 1, 1))
        For y = 0 To Me.Height - 1
            For x = 0 To Me.Width - 1
                If x Mod 3 = 1 And y Mod 3 = 1 Then
                    r.AddRectangle(New Rectangle(x, y, 1, 1))
                End If
            Next
        Next
        Return r
    End Function
End Class