Public Class Form1
#Region "API"
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)
    Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As IntPtr)

    Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer

    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint:="PostMessageA")> _
    Public Shared Function PostMessage(ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As String) As Integer
    End Function
    Public Function MAKELPARAM(ByVal l As Integer, ByVal h As Integer) As Integer
        Dim r As Integer = l + (h << 16)
        Return r
    End Function
    Public Const WM_MOUSE_MOVE = &H200
    Public Const WM_LBUTTON_DOWN = &H201
    Public Const WM_LBUTTON_UP = &H202
    Public Const WM_KEYDOWN = &H100
    Public Const WM_KEYUP = &H101
    Public Const WM_CHAR = &H102

    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Public Const WM_HOTKEY As Integer = &H312
    Public Const MOD_ALT As Integer = &H1
    Public Const MOD_CONTROL As Integer = &H2
    Public Const MOD_SHIFT As Integer = &H4
    Public Const MOD_WIN As Integer = &H8
    Public Const MOD_NOREPEAT As Integer = &H4000

    Public Declare Function SetCursorPos Lib "user32" (ByVal X As Int32, ByVal Y As Int32) As Int32
    Public Declare Function GetCursorPos Lib "user32" Alias "GetCursorPos" (ByRef lpPoint As POINTAPI) As Integer
    Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
    End Structure
#End Region

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Me.Text = id.ToString
            Select Case (id.ToString)
                Case "0"
                    If RadioButton1.Checked Then
                        System.Threading.Thread.Sleep(500)
                        i = 0
                        Timer1.Start()
                    Else
                        If RadioButton2.Checked Then
                            Timer2.Start()
                        End If
                    End If
                Case "2"
                    If RadioButton1.Checked Then
                        System.Threading.Thread.Sleep(500)
                        i = 0
                        Timer1.Start()
                    Else
                        If RadioButton2.Checked Then
                            Timer2.Start()
                        End If
                    End If
                Case "3"
                    If RadioButton1.Checked Then
                        System.Threading.Thread.Sleep(500)
                        i = 0
                        Timer1.Start()
                    Else
                        If RadioButton2.Checked Then
                            Timer2.Start()
                        End If
                    End If
                Case "1"
                    Timer1.Stop()
                    Me.Text = "stop"
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RegisterHotKey(Me.Handle, 0, 0, Keys.F11)
        RegisterHotKey(Me.Handle, 2, MOD_CONTROL, Keys.F11)
        RegisterHotKey(Me.Handle, 3, MOD_SHIFT, Keys.F11)
        'RegisterHotKey(Me.Handle, 1, 0, Keys.NumPad0)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 0)
        UnregisterHotKey(Me.Handle, 1)
    End Sub

    Dim i As Integer = 0
    Dim pos As New Point(1240, 590)

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If i >= 60 Then
            Timer1.Stop()
            Me.Text = "stop"
        Else
            Dim x As Integer = i \ 5
            Dim y As Integer = i Mod 5
            Dim h As Integer = FindWindow("POEWindowClass", "Path of Exile")
            If h Then
                Me.Text = "working - " & i
                SetCursorPos(pos.X + x * 44, pos.Y + y * 44)
                System.Threading.Thread.Sleep(10)
                PostMessage(h, WM_LBUTTON_DOWN, 0, 0)
                System.Threading.Thread.Sleep(10)
                PostMessage(h, WM_LBUTTON_UP, 0, 0)
                System.Threading.Thread.Sleep(10)
            End If
            i += 1
        End If
    End Sub

    Private Sub Timer_Detect_Tick(sender As System.Object, e As System.EventArgs) Handles Timer_Detect.Tick
        If GetAsyncKeyState(Keys.F12) <> 0 Then
            Timer1.Stop()
            Timer2.Stop()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        mouse_event(2, 0, 0, 0, 0)
        mouse_event(4, 0, 0, 0, 0)
    End Sub
End Class
