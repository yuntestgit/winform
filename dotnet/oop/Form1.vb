Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim a As New A
        a.B.C = 0
    End Sub
End Class

Class A
    Public B As New B
End Class

Class B
    Public C As String
End Class