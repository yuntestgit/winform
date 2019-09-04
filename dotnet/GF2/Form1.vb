Public Class Form1
#Region "Findwindow"
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
#End Region

#Region "Message"
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (hwnd As Integer, lpString As String) As Integer

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

#Region "god"
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
#End Region

    Dim h As Integer

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        h = FindWindow("DJO_CLASS", "Grand Fantasia")
        Me.Text = Hex(h).ToString
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click



        'Dim didItPost As IntPtr

        'didItPost = PostMessage(h, WM_KEYDOWN, New IntPtr(&H41), CreateLParamFor_WM_KEYDOWN(1, &H1E, False, False))
        'System.Threading.Thread.Sleep(50)
        'didItPost = PostMessage(h, WM_KEYUP, New IntPtr(&H41), CreateLParamFor_WM_KEYUP(1, &H1E, False))

        'PostMessage(h, WM_LBUTTON_DOWN, 1, 0)
        Dim cp As Point = New Point(500, 500)
        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(cp.X, cp.Y))
        System.Threading.Thread.Sleep(50)
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(cp.X, cp.Y))

        'Timer1.Start()
    End Sub


    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim didItPost As IntPtr

        didItPost = PostMessage(h, WM_KEYDOWN, New IntPtr(&H41), CreateLParamFor_WM_KEYDOWN(1, &H1E, False, False))
        System.Threading.Thread.Sleep(50)
        didItPost = PostMessage(h, WM_KEYUP, New IntPtr(&H41), CreateLParamFor_WM_KEYUP(1, &H1E, False))
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        SetWindowText(h, "test string")
    End Sub
End Class
