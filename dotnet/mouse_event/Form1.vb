Public Class Form1
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer
    Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)

    Public Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As IntPtr)


    Private Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Keys.F7) <> 0 Then
            Timer2.Start()
        End If

        If GetAsyncKeyState(Keys.F8) <> 0 Then
            Timer2.Stop()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        mouse_event(2, 0, 0, 0, 0)
        mouse_event(4, 0, 0, 0, 0)

        'mouse_event(8, 0, 0, 0, 0)
        'mouse_event(16, 0, 0, 0, 0)

        'Dim key As Integer = Keys.Tab
        'keybd_event(key, MapVirtualKey(key, 0), 0, 0)
        'System.Threading.Thread.Sleep(100)
        'keybd_event(key, MapVirtualKey(key, 0), &H2, 0)

        'SendKeys.SendWait("i")
        'SendKeys.Flush()
    End Sub
End Class
