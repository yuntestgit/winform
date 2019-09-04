Public Class Form1

    Public dm As New Dm.dmsoft

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim h As Integer = dm.FindWindow("DJO_CLASS", "Grand Fantasia")
        Dim s(3) As String
        s(0) = "normal"
        s(1) = "dx"
        s(2) = "dx2"

        '653,459
        If h Then
            Me.Text = Hex(h).ToString
            'dm.BindWindow(h, "dx", "dx2", "dx", 0)
            'dm.BindWindow(h, "gdi", "normal", "normal", 0)
            'dm.BindWindow(h, "dx2", "normal", "normal", 0)
            'dm.BindWindow(h, "normal", "dx2", "normal", 0)
            'dm.BindWindow(h, "dx", "dx", "dx", 0)
            'dm.BindWindow(h, "normal", "dx", "normal", 0)
            'dm.BindWindow(h, "gdi", "windows", "windows", 0)

            'dm.BindWindow(h, s(0), s(0), s(0), 0) '-1
            'dm.BindWindow(h, s(0), s(0), s(1), 0) '-1
            'dm.BindWindow(h, s(0), s(1), s(0), 0) '1
            'dm.BindWindow(h, s(0), s(1), s(1), 0) '1
            'dm.BindWindow(h, s(1), s(0), s(0), 0) '-1
            'dm.BindWindow(h, s(1), s(0), s(1), 0) '-1
            'dm.BindWindow(h, s(1), s(1), s(0), 0) '1
            'dm.BindWindow(h, s(1), s(1), s(1), 0)

            dm.BindWindow(h, "normal", "windows", "windows", 0)
            'Dim r As Integer = dm.BindWindow(h, "normal", "dx", "normal", 0)
            'Button1.Text = r
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        System.Runtime.InteropServices.Marshal.ReleaseComObject(dm)
        dm = Nothing
        GC.Collect()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'dm.MoveTo(653, 459)
        'System.Threading.Thread.Sleep(100)
        'dm.LeftClick()

        dm.KeyPress(Keys.I)
        System.Threading.Thread.Sleep(100)

        dm.MoveTo(581, 497)
        System.Threading.Thread.Sleep(100)
        dm.LeftClick()
    End Sub
End Class
