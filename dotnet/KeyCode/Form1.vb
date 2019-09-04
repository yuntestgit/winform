Public Class Form1
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Me.Text = 0
        For i = 0 To 1000
            If GetAsyncKeyState(i) <> 0 Then
                Me.Text = i & " = &H" & Hex(i).ToString
            End If
        Next
    End Sub
End Class
