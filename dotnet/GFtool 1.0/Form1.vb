Public Class Form1

#Region "API"
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)

    Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As IntPtr)

    Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer


    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Public Const WM_HOTKEY As Integer = &H312
#End Region

    Dim open As Boolean = False

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RegisterHotKey(Me.Handle, 123, 0, 192)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case (id.ToString)
                Case "123"
                    If open Then
                        open = False
                        _stop()
                    Else
                        open = True
                        _start()
                    End If
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Sub _start()
        Timer2.Start()
        If CheckBox1.Checked Then
            Dim i As Integer = Val(TextBox1.Text) * 1000 + 1
            Timer3.Interval = i
            Timer3.Start()
        End If
    End Sub

    Sub _stop()
        Timer2.Stop()
        Timer3.Stop()
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        If CheckBox2.Checked Then
            PressKey(Keys.D1)
        End If

        If CheckBox3.Checked Then
            PressKey(Keys.D2)
        End If

        If CheckBox4.Checked Then
            PressKey(Keys.D3)
        End If

        If CheckBox5.Checked Then
            PressKey(Keys.D4)
        End If

        If CheckBox6.Checked Then
            PressKey(Keys.D5)
        End If

        If CheckBox7.Checked Then
            PressKey(Keys.D6)
        End If

        If CheckBox8.Checked Then
            PressKey(Keys.D7)
        End If

        If CheckBox9.Checked Then
            PressKey(Keys.D8)
        End If

        If CheckBox10.Checked Then
            PressKey(Keys.D9)
        End If

        If CheckBox11.Checked Then
            PressKey(Keys.D0)
        End If

        If CheckBox12.Checked Then
            PressKey(189)
        End If

        If CheckBox13.Checked Then
            PressKey(187)
        End If
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        If CheckBox1.Checked Then
            PressKey(Keys.Tab)
        End If
    End Sub

    Sub PressKey(key As Integer)
        keybd_event(key, MapVirtualKey(key, 0), 0, 0)
        System.Threading.Thread.Sleep(10)
        keybd_event(key, MapVirtualKey(key, 0), &H2, 0)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox1.Enabled = True
        Else
            TextBox1.Enabled = False
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 123)
    End Sub

    Private Sub TrackBar1_Scroll(sender As System.Object, e As System.EventArgs) Handles TrackBar1.Scroll
        Me.Opacity = (TrackBar1.Value + 5) / 10
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "視窗置頂" Then
            Me.TopMost = True
            Button1.Text = "取消置頂"
        Else
            Me.TopMost = False
            Button1.Text = "視窗置頂"
        End If
    End Sub
End Class
