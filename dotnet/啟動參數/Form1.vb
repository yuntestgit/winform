Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'For i As Integer = 0 To My.Application.CommandLineArgs.Count - 1
        '    ListBox1.Items.Add(My.Application.CommandLineArgs(i))
        'Next
        'Me.Text = My.Application.CommandLineArgs(0)
        Process.Start("C:/AppServ/www/av/test.flv")
    End Sub
End Class
