Public Class Form1
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)

    Public Declare Sub keybd_event Lib "user32" (ByVal bVk As Integer, ByVal bScan As Integer, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
    Public Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer

    Private Function MakeKeyLparam(ByVal VirtualKey As Long, ByVal flag As Long) As Long
        Dim s As String
        Dim Firstbyte As String     'lparam参数的24-31位
        Dim WM_KEYDOWN = &H100
        If flag = WM_KEYDOWN Then   '如果是按下键
            Firstbyte = "00"
        Else
            Firstbyte = "C0"        '如果是释放键
        End If
        Dim Scancode As Long
        '获得键的扫描码
        Scancode = MapVirtualKey(VirtualKey, 0)
        Dim Secondbyte As String    'lparam参数的16-23位，即虚拟键扫描码
        Secondbyte = Microsoft.VisualBasic.Right("00" & Hex(Scancode), 2)
        s = Firstbyte & Secondbyte & "0001"   '0001为lparam参数的0-15位，即发送次数和其它扩展信息
        MakeKeyLparam = Val("&H" & s)
    End Function

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Keys.F7) <> 0 Then
            Timer2.Start()
        End If

        If GetAsyncKeyState(Keys.F8) <> 0 Then
            Timer2.Stop()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        mouse_event(8, 0, 0, 0, 0)
        mouse_event(16, 0, 0, 0, 0)

        If Dkey.Checked Then
            sendkeypress(Keys.D)
        End If

        If FKey.Checked Then
            sendkeypress(Keys.F)
        End If


        If QKey.Checked Then
            sendkeypress(Keys.Q)
        End If

        If WKey.Checked Then
            sendkeypress(Keys.W)
        End If

        If EKey.Checked Then
            sendkeypress(Keys.E)
        End If

        If RKey.Checked Then
            sendkeypress(Keys.R)
        End If

        If TKey.Checked Then
            sendkeypress(Keys.T)
        End If


        If Key1.Checked Then
            sendkeypress(Keys.D1)
        End If

        If Key2.Checked Then
            sendkeypress(Keys.D2)
        End If

        If Key3.Checked Then
            sendkeypress(Keys.D3)
        End If

        If Key4.Checked Then
            sendkeypress(Keys.D4)
        End If

        If Key5.Checked Then
            sendkeypress(Keys.D5)
        End If

        If Key6.Checked Then
            sendkeypress(Keys.D6)
        End If

        If Key7.Checked Then
            sendkeypress(Keys.D7)
        End If

        If Key192.Checked Then
            sendkeypress(192)
        End If


        If F5Key.Checked Then
            sendkeypress(Keys.F5)
        End If

    End Sub

    Sub sendkeypress(keycode As Integer)
        keybd_event(keycode, MapVirtualKey(keycode, 0), 0, 0)
        keybd_event(keycode, MapVirtualKey(keycode, 0), 2, 0)
    End Sub
End Class
