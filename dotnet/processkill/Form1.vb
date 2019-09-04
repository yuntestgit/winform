Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim p() As Process = Process.GetProcesses

        For i As Integer = 0 To p.Length - 1
            MsgBox(p(i).ProcessName)
        Next
    End Sub
End Class
