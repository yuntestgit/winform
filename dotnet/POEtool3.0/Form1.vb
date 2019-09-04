Public Class Form1

#Region "API"
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)
    Public Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As IntPtr)

    Public Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer

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

    'FOR API
    Sub PostKey(hwnd As Integer, key As Integer)
        If hwnd Then
            PostMessage(hwnd, WM_KEYDOWN, key, 0)
        End If
    End Sub
#End Region

    Dim sw As Boolean = False
    Dim h As Integer
    Dim TimersEnable() As Boolean = {0, False, False, False, False, False}
    Dim Timer1_T, Timer2_T, Timer3_T, Timer4_T, Timer5_T As Double
    Dim Timer1_CD, Timer2_CD, Timer3_CD, Timer4_CD, Timer5_CD As Double
    Dim Timer1_Flag, Timer2_Flag, Timer3_Flag, Timer4_Flag, Timer5_Flag As Integer
    Dim Timer1_Keys(), Timer2_Keys(), Timer3_Keys(), Timer4_Keys(), Timer5_Keys() As Integer
    Dim Macro() As Integer
    Dim FormName As String = "POEtool3.0"
    Dim mode As Integer = -1

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case (id.ToString)
                Case "0"
                    If sw = False Then
                        sw = True
                        sw_start()
                    End If
                Case "1"
                    For i = 0 To Macro.Length - 1
                        If i <> 0 Then
                            PostKey(h, Macro(i))
                        End If
                    Next
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Sub sw_start()
        If TimersEnable(0) Then
            RegisterHotKey(Me.Handle, 1, 0, Macro(0))
        End If
        Timer1.Enabled = TimersEnable(1) : Timer1_T = 0 : Timer1_Flag = 0
        Timer2.Enabled = TimersEnable(2) : Timer2_T = 0 : Timer2_Flag = 0
        Timer3.Enabled = TimersEnable(3) : Timer3_T = 0 : Timer3_Flag = 0
        Timer4.Enabled = TimersEnable(4) : Timer4_T = 0 : Timer4_Flag = 0
        Timer5.Enabled = TimersEnable(5) : Timer5_T = 0 : Timer5_Flag = 0
        SetColor()
    End Sub

    Sub sw_stop()
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Timer4.Enabled = False
        Timer5.Enabled = False
        If TimersEnable(0) Then
            UnregisterHotKey(Me.Handle, 1)
        End If
        SetColor()
    End Sub

    Sub SetMode(newMode As Integer)
        If mode <> newMode Then
            Select Case newMode
                Case 1
                    Me.Text = FormName & " - " & "貴族元打"
                    TimersEnable = {1, True, True, False, False, False}
                    Timer1_Keys = {Keys.D5} : Timer1_CD = 5.5
                    Timer2_Keys = {Keys.D3, Keys.D4} : Timer2_CD = 4.5
                    Macro = {Keys.Space, Keys.D2}
                Case 2
                    Me.Text = FormName & " - " & "追獵元打"
                    TimersEnable = {1, True, True, False, False, False}
                    Timer1_Keys = {Keys.D5} : Timer1_CD = 7
                    Timer2_Keys = {Keys.D4} : Timer2_CD = 6.5
                    Macro = {Keys.Space, Keys.D2, Keys.D3}
                Case 3
                    Me.Text = FormName & " - " & "冠軍元打"
                    TimersEnable = {1, True, True, False, False, False}
                    Timer1_Keys = {Keys.D5} : Timer1_CD = 6
                    Timer2_Keys = {Keys.D4} : Timer2_CD = 5.5
                    Macro = {Keys.Space, Keys.D2, Keys.D3}
                Case 4
                    Me.Text = FormName & " - " & "冰川老頭"
                    TimersEnable = {1, True, False, False, False, False}
                    Timer1_Keys = {Keys.D4, Keys.D5} : Timer1_CD = 4.5
                    Macro = {Keys.Space, Keys.D2, Keys.D3}
                Case 5
                    Me.Text = FormName & " - " & "新手套路"
                    TimersEnable = {0, True, False, False, False, False}
                    Timer1_Keys = {Keys.D3, Keys.D4, Keys.D5} : Timer1_CD = 4.5
                Case 6
                    Me.Text = FormName & " - " & "勇士奉獻"
                    TimersEnable = {1, True, True, False, False, False}
                    Timer1_Keys = {Keys.D5} : Timer1_CD = 5.5
                    Timer2_Keys = {Keys.D4} : Timer2_CD = 5.5
                    Macro = {Keys.Space, Keys.D2, Keys.D3}
            End Select

            mode = newMode
            Dim swriter As New System.IO.StreamWriter(Application.StartupPath & "\poetool.txt")
            swriter.Write(newMode)
            swriter.Close()

            SetColor()
        End If
    End Sub

    Sub SetColor()
        If sw = False Then
            Dim btn As Button
            For Each btn In Me.Controls
                btn.Enabled = True
                If Val(btn.Name.Replace("Button", "")) = mode Then
                    btn.Enabled = False
                    btn.BackColor = Color.LightGray
                Else
                    btn.Enabled = True
                    btn.BackColor = Color.SkyBlue
                End If
            Next
        Else
            Dim btn As Button
            For Each btn In Me.Controls
                btn.Enabled = False
                btn.BackColor = Color.LightGray
            Next
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = Screen.PrimaryScreen.Bounds.Height - Me.Height - 40 - 5
        Me.Left = Screen.PrimaryScreen.Bounds.Width - Me.Width - 5
        Me.TopMost = True
        Me.Text = FormName

        Dim sreader As New System.IO.StreamReader(Application.StartupPath & "\poetool.txt")
        Dim r As String = sreader.ReadToEnd()
        sreader.Close()

        SetMode(Val(r))

        RegisterHotKey(Me.Handle, 0, 0, Keys.F11)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 0)
        Try
            UnregisterHotKey(Me.Handle, 1)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        SetMode(1)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        SetMode(2)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        SetMode(3)
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        SetMode(4)
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        SetMode(5)
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        SetMode(6)
    End Sub

    Private Sub TimerDetecter_Tick(sender As System.Object, e As System.EventArgs) Handles TimerDetecter.Tick
        h = FindWindow("POEWindowClass", "Path of Exile")
        If GetAsyncKeyState(Keys.F12) <> 0 Then
            sw = False
            sw_stop()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim dt As Double = Math.Abs(Microsoft.VisualBasic.Timer - Timer1_T)
        If dt >= Timer1_CD Then
            PostKey(h, Timer1_Keys(Timer1_Flag))
            Timer1_Flag += 1
            If Timer1_Flag = Timer1_Keys.Length Then
                Timer1_Flag = 0
            End If
            Timer1_T = Microsoft.VisualBasic.Timer
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        Dim dt As Double = Math.Abs(Microsoft.VisualBasic.Timer - Timer2_T)
        If dt >= Timer2_CD Then
            PostKey(h, Timer2_Keys(Timer2_Flag))
            Timer2_Flag += 1
            If Timer2_Flag = Timer2_Keys.Length Then
                Timer2_Flag = 0
            End If
            Timer2_T = Microsoft.VisualBasic.Timer
        End If
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        Dim dt As Double = Math.Abs(Microsoft.VisualBasic.Timer - Timer3_T)
        If dt >= Timer3_CD Then
            PostKey(h, Timer3_Keys(Timer3_Flag))
            Timer3_Flag += 1
            If Timer3_Flag = Timer3_Keys.Length Then
                Timer3_Flag = 0
            End If
            Timer3_T = Microsoft.VisualBasic.Timer
        End If
    End Sub

    Private Sub Timer4_Tick(sender As System.Object, e As System.EventArgs) Handles Timer4.Tick
        Dim dt As Double = Math.Abs(Microsoft.VisualBasic.Timer - Timer4_T)
        If dt >= Timer4_CD Then
            PostKey(h, Timer4_Keys(Timer4_Flag))
            Timer4_Flag += 1
            If Timer4_Flag = Timer4_Keys.Length Then
                Timer4_Flag = 0
            End If
            Timer4_T = Microsoft.VisualBasic.Timer
        End If
    End Sub

    Private Sub Timer5_Tick(sender As System.Object, e As System.EventArgs) Handles Timer5.Tick
        Dim dt As Double = Math.Abs(Microsoft.VisualBasic.Timer - Timer5_T)
        If dt >= Timer5_CD Then
            PostKey(h, Timer5_Keys(Timer5_Flag))
            Timer5_Flag += 1
            If Timer5_Flag = Timer5_Keys.Length Then
                Timer5_Flag = 0
            End If
            Timer5_T = Microsoft.VisualBasic.Timer
        End If
    End Sub
End Class
