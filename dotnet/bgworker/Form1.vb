Public Class Form1

    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        For i = 0 To 100000
            For j = 0 To 10000

            Next
        Next
        MsgBox(1)
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'BackgroundWorker1.RunWorkerAsync()

        Dim bg As New test
        bg.go()
    End Sub


End Class


Class test
    Private bg As System.ComponentModel.BackgroundWorker

    Public Sub New()
        bg = New System.ComponentModel.BackgroundWorker
        AddHandler bg.DoWork, AddressOf running
    End Sub

    Public Function go() As Integer
        Dim r As Integer = 12345
        bg.RunWorkerAsync(r)
        Return r
    End Function

    Private Function running(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) As Integer
        For i = 0 To 100000
            For j = 0 To 10000

            Next
        Next
        e.Argument.ToString()
        MsgBox(e.Argument.ToString())
    End Function

    Private Sub _RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Dim Area As Double = CDbl(e.Result)
        'MsgBox(e.Result)
    End Sub
End Class