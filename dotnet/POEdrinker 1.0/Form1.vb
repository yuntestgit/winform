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
#End Region

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Me.Text = id.ToString
            Select Case (id.ToString)
                Case "0"
                    If sw Then
                        sw = False
                        timer_stop()
                    Else
                        sw = True
                        timer_start()
                    End If
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Dim sw As Boolean = False
    Dim h As Integer
    Dim t1 As Double = 0
    Dim t2 As Double = 0
    Dim t3 As Double = 0
    Dim cd1 As Double = 5
    Dim cd2 As Double = 6
    Dim cd3 As Double = 10
    '6.5(A), 6.5(A), 6.5(B), 6.5(B), 12 <百抗追獵>
    'off, off, 4.5(A), 4.5(B), 4.5(B) <冰川>
    '5(A), 5(A), 6(B), 6(B), 10 <雙筆普蘭>
    Dim delay As Double = 0.5

    Dim timer2_flag As Integer = 0
    Dim timer3_flag As Integer = 0
    Dim timer4_flag As Integer = 0

    Sub timer_start()
        t1 = 0 : t2 = 0 : t3 = 0 : timer2_flag = 0 : timer3_flag = 0 : timer4_flag = 0
        Timer2.Start()
        Timer3.Start()
        Timer4.Start()
        Me.Text = "POEdrinker 1.0 - Working"
        Me.BackColor = Color.Red
    End Sub

    Sub timer_stop()
        Timer2.Stop()
        Timer3.Stop()
        Timer4.Stop()
        Me.Text = "POEdrinker 1.0"
        Me.BackColor = Color.Black
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        h = FindWindow("POEWindowClass", "Path of Exile")
        If GetAsyncKeyState(Keys.F12) <> 0 Then
            sw = False
            timer_stop()
        End If
    End Sub

    Sub PressKey1(key As Integer)
        keybd_event(key, MapVirtualKey(key, 0), 0, 0)
        keybd_event(key, MapVirtualKey(key, 0), &H2, 0)
    End Sub

    Sub PostKey(hwnd As Integer, key As Integer)
        If hwnd Then
            PostMessage(hwnd, WM_KEYDOWN, key, 0)
            'System.Threading.Thread.Sleep(10)
            'PostMessage(hwnd, WM_KEYUP, key, 0)
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        Dim dt As Double = Microsoft.VisualBasic.Timer - t1
        If dt >= cd1 Then
            If timer2_flag = 0 Then
                PostKey(h, Keys.D1)
                timer2_flag = 1
            ElseIf timer2_flag = 1 Then
                PostKey(h, Keys.D2)
                timer2_flag = 0
            End If
            t1 = Microsoft.VisualBasic.Timer
        End If
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        Dim dt As Double = Microsoft.VisualBasic.Timer - t2
        If dt >= cd2 Then
            If timer3_flag = 0 Then
                PostKey(h, Keys.D3)
                timer3_flag = 1
            ElseIf timer3_flag = 1 Then
                PostKey(h, Keys.D4)
                timer3_flag = 0
            End If
            t2 = Microsoft.VisualBasic.Timer
        End If
    End Sub

    Private Sub Timer4_Tick(sender As System.Object, e As System.EventArgs) Handles Timer4.Tick
        Dim dt As Double = Microsoft.VisualBasic.Timer - t3
        If dt >= cd3 Then
            If timer4_flag = 0 Then
                PostKey(h, Keys.D5)
                timer4_flag = 1
            ElseIf timer4_flag = 1 Then
                PostKey(h, Keys.D5)
                timer4_flag = 0
            End If
            t3 = Microsoft.VisualBasic.Timer
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = 5 + 28
        Me.Left = Screen.PrimaryScreen.Bounds.Width - Me.Width - 5
        Me.TopMost = True

        RegisterHotKey(Me.Handle, 0, 0, Keys.F11)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 0)
    End Sub
End Class
