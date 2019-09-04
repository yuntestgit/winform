Imports System.Runtime.InteropServices

Public Class Form1
    <DllImport("user32.dll")> _
    Shared Function GetAsyncKeyState(ByVal vKey As System.Windows.Forms.Keys) As Short
    End Function

    <DllImport("user32.dll")> _
    Private Shared Sub mouse_event(dwFlags As UInteger, dx As UInteger, dy As UInteger, dwData As UInteger, dwExtraInfo As Integer)
    End Sub

    Const MOUSEEVENTF_ABSOLUTE As UInteger = &H8000
    Const MOUSEEVENTF_LEFTDOWN As UInteger = &H2
    Const MOUSEEVENTF_LEFTUP As UInteger = &H4
    Const MOUSEEVENTF_MIDDLEDOWN As UInteger = &H20
    Const MOUSEEVENTF_MIDDLEUP As UInteger = &H40
    Const MOUSEEVENTF_MOVE As UInteger = &H1
    Const MOUSEEVENTF_RIGHTDOWN As UInteger = &H8
    Const MOUSEEVENTF_RIGHTUP As UInteger = &H10
    Const MOUSEEVENTF_XDOWN As UInteger = &H80
    Const MOUSEEVENTF_XUP As UInteger = &H100
    Const MOUSEEVENTF_WHEEL As UInteger = &H800
    Const MOUSEEVENTF_HWHEEL As UInteger = &H1000

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Keys.F11) Then
            Me.Text = "true"
            Timer2.Start()
        ElseIf GetAsyncKeyState(Keys.F12) Then
            Me.Text = "false"
            Timer2.Stop()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        mouse_event(6, 0, 0, 0, 0)
    End Sub
End Class
