Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Process.Start("C:\AppServ\www\av\test.flv")
        'Process.Start("cmd.exe C:\Program Files (x86)\GRETECH\GomPlayer\GOM.EXE")
        Me.Close()
    End Sub
End Class
