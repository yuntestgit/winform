Public Class Form1
    Public Const WM_HOTKEY As Integer = &H312
    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)

    Public Declare Function ReleaseCapture Lib "user32" Alias "ReleaseCapture" () As Boolean
    Public Declare Function MoveWindow Lib "user32" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean

    Public Const WM_SYSCOMMAND = &H112
    Public Const SC_MOVE = &HF010&
    Public Const HTCAPTION = &H2
    Public Const SRCCOPY = &HCC0020

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = 5
        Me.Left = 5
        RegisterHotKey(Me.Handle, 0, 0, 188) '<
        RegisterHotKey(Me.Handle, 1, 0, 190) '>
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    mouse_event(2, 0, 0, 0, 0)
                    mouse_event(4, 0, 0, 0, 0)
                Case 1
                    mouse_event(8, 0, 0, 0, 0)
                    mouse_event(16, 0, 0, 0, 0)
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 0)
        UnregisterHotKey(Me.Handle, 1)
    End Sub

    Private Sub Form1_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        ReleaseCapture()
        MoveWindow(Me.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0)
    End Sub
End Class
