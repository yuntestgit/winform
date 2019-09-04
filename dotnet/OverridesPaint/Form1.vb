Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim myselect As New yunSelect
        myselect.Width = 135
        myselect.Height = 100
        Me.Controls.Add(myselect)
    End Sub
End Class

Public Class yunSelect
    Inherits ProgressBar


    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        Dim g As Graphics = Me.CreateGraphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim pts As Point() = {New Point(Me.Width - 26, 20), New Point(Me.Width - 10, 20), New Point(Me.Width - 18, 30)}
        g.FillPolygon(Brushes.Black, pts)

        g.DrawString("sdfsdfd", New Font("微軟正黑體", 20, FontStyle.Bold), Brushes.Black, New Point(5, 5))
        g.DrawString("sdfgsdfgds", New Font("微軟正黑體", 20, FontStyle.Bold), Brushes.Black, New Point(5, 50))
    End Sub
End Class
