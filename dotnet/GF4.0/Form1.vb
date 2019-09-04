Public Class Form1
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint:="PostMessageA")> _
    Public Shared Function PostMessage(ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As String) As Integer
    End Function

    'Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
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

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim hwnd As Integer = FindWindow(vbNullString, "Grand Fantasia")
        Me.Text = Hex(hwnd).ToString

        Dim key As Integer = Keys.A

        'PostMessage(hwnd, WM_KEYDOWN, key, MAKELPARAM(key, WM_KEYDOWN))
        'System.Threading.Thread.Sleep(100)
        'PostMessage(hwnd, WM_KEYUP, key, MAKELPARAM(key, WM_KEYUP))
        PostMessage(hwnd, WM_KEYDOWN, key, 0)
        PostMessage(hwnd, WM_KEYUP, key, 0)
    End Sub
End Class
