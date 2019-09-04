Imports ex13Interest
Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim test As New ex13Interest.Interest
        Dim d As Double = test.CalInterest(123.456, 654.789)
        MsgBox(d)
    End Sub
End Class
