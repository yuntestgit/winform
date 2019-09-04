Public Class Form1
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer
    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Keys.F7) <> 0 Then
            Me.Enabled = False
            Timer2.Start()
        End If

        If GetAsyncKeyState(Keys.F8) <> 0 Then
            Me.Enabled = True
            Timer2.Stop()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        If RadioButton1.Checked Then
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf RadioButton2.Checked Then
            mouse_event(2, 0, 0, 0, 0)
            System.Threading.Thread.Sleep(10)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf RadioButton3.Checked Then

        End If
    End Sub
End Class
