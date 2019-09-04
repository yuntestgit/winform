Public Class Form1

#Region "Findwindow"
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
#End Region

#Region "Message"
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Const WM_MOUSE_MOVE = &H200
    Public Const WM_LBUTTON_DOWN = &H201
    Public Const WM_LBUTTON_UP = &H202

    Public Const WM_KEYDOWN = &H100
    Public Const WM_KEYUP = &H101
    Public Const WM_CHAR = &H102

    Public Function MAKELPARAM(ByVal l As Integer, ByVal h As Integer) As Integer
        Dim r As Integer = l + (h << 16)
        Return r
    End Function
#End Region

    

    Shared Function CreateLParam( _
      ByVal RepeatCount As UShort, _
      ByVal ScanCode As Byte, _
      ByVal IsExtendedKey As Boolean, _
      ByVal DownBefore As Boolean, _
      ByVal State As Boolean) As IntPtr

        Dim value As Int32

        value = RepeatCount Or CInt(ScanCode) << 16
        If IsExtendedKey Then value = value Or &H10000
        If DownBefore Then value = value Or &H40000000
        If State Then value = value Or &H80000000

        Return New IntPtr(value)

    End Function
    Public Shared Function CreateLParamFor_WM_KEYDOWN( _
       ByVal RepeatCount As UShort, _
       ByVal ScanCode As Byte, _
       ByVal IsExtendedKey As Boolean, _
       ByVal DownBefore As Boolean) _
       As IntPtr

        Return CreateLParam(RepeatCount, ScanCode, IsExtendedKey, DownBefore, False)

    End Function
    Public Shared Function CreateLParamFor_WM_KEYUP( _
       ByVal RepeatCount As UShort, _
       ByVal ScanCode As Byte, _
       ByVal IsExtendedKey As Boolean) _
       As IntPtr

        Return CreateLParam(RepeatCount, ScanCode, IsExtendedKey, True, True)

    End Function

    Const WM_SETTEXT As Integer = &HC

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim h As Integer = FindWindow(vbNullString, "Grand Fantasia")

        'Dim h2 As Integer = FindWindowEx(h, 0, "Edit", vbNullString)
        'If h2 Then
        '    Me.Text = "TRUE"
        'End If

        PostMessage(h, WM_KEYDOWN, Keys.I, 0)
        'System.Threading.Thread.Sleep(100)
        PostMessage(h, WM_KEYUP, Keys.I, 0)


        '--

        'PostMessage(h, WM_KEYDOWN, Keys.I, MAKELPARAM(Keys.I, WM_KEYDOWN))
        'System.Threading.Thread.Sleep(100)
        'PostMessage(h, WM_KEYUP, Keys.I, MAKELPARAM(Keys.I, WM_KEYUP))

        '--

        'Dim didItPost As IntPtr

        'didItPost = PostMessage(h, WM_KEYDOWN, New IntPtr(&H41), CreateLParamFor_WM_KEYDOWN(1, &H1E, False, False))
        'System.Threading.Thread.Sleep(100)
        'didItPost = PostMessage(h, WM_KEYUP, New IntPtr(&H41), CreateLParamFor_WM_KEYUP(1, &H1E, False))

        '--

        'PostMessage(h, WM_KEYDOWN, Keys.F6, MAKELPARAM(Keys.F6, WM_KEYDOWN))
        'PostMessage(h, WM_KEYUP, Keys.F6, MAKELPARAM(Keys.F6, WM_KEYUP))

        'tab

        '--

        'Dim cp As Point = New Point(579, 644)
        'PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(cp.X, cp.Y))
        'PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(cp.X, cp.Y))


        'PostMessage(h, WM_KEYDOWN, Keys.I, MAKELPARAM(Keys.I, WM_KEYDOWN))
        'System.Threading.Thread.Sleep(100)
        'PostMessage(h, WM_KEYUP, Keys.I, MAKELPARAM(Keys.I, WM_KEYUP))

        'System.Threading.Thread.Sleep(100)
        'PostMessage(h, WM_CHAR, Asc("I"), MAKELPARAM(Keys.I, WM_KEYDOWN))



        'System.Threading.Thread.Sleep(10)

        'PostMessage(hwnd, WM_KEYDOWN, VK_A, MakeKeyLparam(VK_A, WM_KEYDOWN)) '按下A鍵

        'PostMessage(hwnd, WM_CHAR, Asc("A"), MakeKeyLparam(VK_A, WM_KEYDOWN)) '輸入字元A

        'PostMessage(hwnd, WM_UP, VK_A, MakeKeyLparam(VK_A, WM_UP)) '釋放A鍵

        'PostMessage(h, WM_KEYDOWN, Keys.F6, MAKELPARAM(Keys.F6, WM_KEYDOWN))
        'PostMessage(h, WM_KEYUP, Keys.F6, MAKELPARAM(Keys.F6, WM_KEYUP))
    End Sub
End Class
