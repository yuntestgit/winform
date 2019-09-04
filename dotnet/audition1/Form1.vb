Public Class Form1
#Region "API"
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer
    Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As IntPtr)
    Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer
#End Region
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Keys.F9) <> 0 Then 'Grand Fantasia 14
            Timer2.Start()
            Me.Text = "running"
        End If

        If GetAsyncKeyState(Keys.F10) <> 0 Then 'Grand Fantasia 14
            Timer2.Stop()
            Me.Text = "pause"
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        Dim k As Integer = 35
        keybd_event(k, MapVirtualKey(k, 0), 0, 0)
        System.Threading.Thread.Sleep(10)
        keybd_event(k, MapVirtualKey(k, 0), &H2, 0)
    End Sub

    Sub PressKey(key As Integer)
        'keybd_event(key, MapVirtualKey(key, 0), 0, 0)
        'System.Threading.Thread.Sleep(10)
        'keybd_event(key, MapVirtualKey(key, 0), &H2, 0)
    End Sub
End Class

'1 23 35
'2 28 40 k
'3 22 34 k
'4 25 37 k
'5 c  12
'6 27 39 k
'7 24 36 k
'8 26 38 k
'9 21 33 k