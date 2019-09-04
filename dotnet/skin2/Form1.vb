Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label2.Parent = Label1
        Label2.Top = 0
        Label2.Left = 0
        Form2.Show()
    End Sub

    Private Sub Form1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim g As Graphics = Me.CreateGraphics
        'g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Dim DG As New Drawing2D.GraphicsPath
        DG.AddString("Comic 5", New FontFamily("Arial"), 0, 10, New Point(0, 100), StringFormat.GenericDefault)

        g.DrawPath(New Pen(Color.Black, 5), DG)
        g.FillPath(Brushes.White, DG)
    End Sub
End Class
