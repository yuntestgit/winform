Public Class Form1
    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)

    Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As IntPtr)

    Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer


    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Public Const WM_HOTKEY As Integer = &H312

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RegisterHotKey(Me.Handle, 123, 0, 192)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case (id.ToString)
                Case "123"
                    Dim key As Integer = Keys.Space
                    keybd_event(key, MapVirtualKey(key, 0), 0, 0)
                    System.Threading.Thread.Sleep(10)
                    keybd_event(key, MapVirtualKey(key, 0), &H2, 0)
            End Select
        End If
        MyBase.WndProc(m)
    End Sub
End Class
