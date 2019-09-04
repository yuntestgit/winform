Public Class Form1
    Dim hwnd As Integer
    Dim childhwnd As Integer
    Dim r As API.RECT
    Dim rk(4) As Integer

    Dim masker As PictureBox
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        rk(0) = Keys.S
        rk(1) = Keys.D
        rk(2) = Keys.S
        rk(3) = Keys.D

        'masker = New PictureBox
        'With masker
        '    .Left = 0
        '    .Top = 50
        '    .Width = Panel2.Width
        '    .Height = Panel2.Height
        'End With
        'Me.Controls.Add(masker)
        'masker.BringToFront()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = &H112 Then
            Dim mp As Integer = m.WParam.ToInt32
            Select Case mp
                Case &HF030 '最大化
                    Me.Top = 6
                Case 61490 '雙擊最大化
                    Me.Top = 6
                Case &HF060 '關閉
                    Dim temp As Integer = API.FindWindow("WindowsForms10.Window.8.app.0.3e799b_r13_ad1", "BlueStacks App Player")
                    If temp = 0 Then
                        API.SetParent(hwnd, 0)
                        API.SetWindowPos(hwnd, 0, 0, 0, 1033, 593, 4)
                        API.ShowWindow(hwnd, &H6)
                        API.ShowWindow(hwnd, &H1)
                    End If
                    Me.Close()
                Case Else
                    MyBase.WndProc(m)
            End Select
        Else
            MyBase.WndProc(m)
        End If

    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim temp As Integer = API.FindWindow("WindowsForms10.Window.8.app.0.3e799b_r13_ad1", "BlueStacks App Player")
        If temp Then
            hwnd = temp
            getAccepter(hwnd)
            API.GetClientRect(hwnd, r)
        End If
    End Sub

    Public Function MAKELPARAM(ByVal l As Integer, ByVal h As Integer) As Integer
        Dim r As Integer = l + (h << 16)
        Return r
    End Function

    Sub PressKey(key As Integer)
        API.PostMessage(childhwnd, API.WM_KEYDOWN, key, MAKELPARAM(key, API.WM_KEYDOWN))
        System.Threading.Thread.Sleep(10)
        API.PostMessage(childhwnd, API.WM_KEYUP, key, MAKELPARAM(key, API.WM_KEYUP))
    End Sub

    Sub getAccepter(h As Integer)
        Dim h2 As Integer = 0
        While True
            h2 = API.FindWindowEx(h, h2, vbNullString, vbNullString)
            If h2 Then
                Dim lpString As String = New String(Chr(0), 255)
                API.GetWindowText(h2, lpString, 255)
                Dim winText As String = Microsoft.VisualBasic.Left(lpString, 25)
                If winText = "BlueStacks Android Plugin" Then
                    childhwnd = h2
                Else
                    getAccepter(h2)
                End If
            Else
                Exit While
            End If
        End While
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Setin" Then
            API.SetParent(hwnd, Panel2.Handle)
            API.SetWindowPos(hwnd, 1, -398, -51, 1033, 593, 4) '1770 1007 / 1033 593
            Button1.Text = "Setout"
        Else
            API.SetParent(hwnd, 0)
            API.SetWindowPos(hwnd, 0, 0, 0, 1033, 593, 4)
            '
            API.ShowWindow(hwnd, &H6)
            API.ShowWindow(hwnd, &H1)
            Me.Activate()
            '
            Button1.Text = "Setin"
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'Dim rand As Integer = (Rnd() * 2)
        'MsgBox(rand)
        If Button2.Text = "Random Go" Then
            Timer2.Enabled = True
            Button2.Text = "Stop"
        Else
            Timer2.Enabled = False
            Button2.Text = "Random Go"
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        Dim rand As Integer = (Rnd() * 2)
        PressKey(rk(rand))
    End Sub
    
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'left top
        PressKey(Keys.A)
        PressKey(Keys.W)
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        'top
        PressKey(Keys.W)
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        'right top
        PressKey(Keys.D)
        PressKey(Keys.W)
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        'left
        PressKey(Keys.A)
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        'right
        PressKey(Keys.D)
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        'left bottom
        PressKey(Keys.A)
        PressKey(Keys.S)
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        'bottom
        PressKey(Keys.S)
    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        'right bottom
        PressKey(Keys.D)
        PressKey(Keys.S)
    End Sub
End Class

Class API
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure
    Public Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As Int32, ByVal hWndNewParent As Int32) As Boolean
    Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    Public Declare Function GetForegroundWindow Lib "user32" () As Integer
    Public Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Public Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (hwnd As Integer, lpString As String) As Integer
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Const WM_MOUSE_MOVE = &H200
    Public Const WM_LBUTTON_DOWN = &H201
    Public Const WM_LBUTTON_UP = &H202
    Public Const WM_KEYDOWN = &H100
    Public Const WM_KEYUP = &H101
    Public Const WM_CHAR = &H102

    '

    Public Const WM_GETDLGCODE = &H87

    Public Const WM_PAINT = &HF

    Public Declare Function ShowWindow Lib "user32" Alias "ShowWindow" (ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Integer
End Class