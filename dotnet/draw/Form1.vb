Public Class Form1

    Dim g As Graphics
    Dim start As Boolean = False
    Dim p As Point

    Private Sub Form1_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        g.Clear(Color.Black)
        start = True
        p.X = e.X
        p.Y = e.Y
    End Sub

    Private Sub Form1_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If start = True Then

            Dim locationx As Integer
            Dim locationy As Integer
            Dim width As Integer
            Dim height As Integer

            If e.X >= p.X Then
                locationx = p.X
            Else
                locationx = e.X
            End If

            If e.Y >= p.Y Then
                locationy = p.Y
            Else
                locationy = e.Y
            End If

            width = Math.Abs(e.X - p.X)
            height = Math.Abs(e.Y - p.Y)

            g.Clear(Color.Black)
            'g.FillRectangle(Brushes.White, locationx - 1, locationy - 1, width + 2, height + 2)
            'g.DrawRectangle(Pens.Black, locationx, locationy, width, height)
            g.FillRectangle(Brushes.DimGray, locationx, locationy, width, height)
            'g.FillRectangle(Brushes.White, locationx + 1, locationy + 1, width - 1, height - 1)
        End If

    End Sub

    Private Sub Form1_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        start = False
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        g = Me.CreateGraphics()

        'SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        'PropertyInfo info = this.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        'info.SetValue(tableLayoutPanel1, true, null);
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles MyBase.Click

    End Sub
End Class
