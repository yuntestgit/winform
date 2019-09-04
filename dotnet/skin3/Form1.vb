Public Class Main
    Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nlndex As Integer, ByVal wNewLong As Integer) As Integer
    Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer

    Const WS_SYSMENU = &H80000
    Const WS_MINIMIZEBOX = &H20000

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Const WM_NCHITTEST As Integer = &H84
        Const HTCLIENT As Integer = &H1
        Const HTCAPTION As Integer = &H2
        Const HTLEFT As Integer = 10
        Const HTRIGHT As Integer = 11
        Const HTTOP As Integer = 12
        Const HTTOPLEFT As Integer = 13
        Const HTTOPRIGHT As Integer = 14
        Const HTBOTTOM As Integer = 15
        Const HTBOTTOMLEFT As Integer = 16
        Const HTBOTTOMRIGHT As Integer = 17
        MyBase.WndProc(m)
        Select Case m.Msg
            Case WM_NCHITTEST
                If m.Result.ToInt32 = HTCLIENT Then
                    m.Result = New IntPtr(HTCAPTION)
                End If
                Dim target_width As Integer = 5
                Dim nPosX = CInt(m.LParam) Mod 65536    '---LoWord---
                Dim nPosY = CInt(m.LParam) / 65536      '---HiWord---
                Dim bTop As Boolean = (nPosY <= Top + target_width)
                Dim bRight As Boolean = (nPosX >= Left + Width - target_width)
                Dim bBottom As Boolean = (nPosY >= Top + Height - target_width)
                Dim bLeft As Boolean = (nPosX <= Left + target_width)
                Select Case True
                    Case bTop * bRight : m.Result = New IntPtr(HTTOPRIGHT)
                    Case bTop * bLeft : m.Result = New IntPtr(HTTOPLEFT)
                    Case bBottom * bRight : m.Result = New IntPtr(HTBOTTOMRIGHT)
                    Case bBottom * bLeft : m.Result = New IntPtr(HTBOTTOMLEFT)
                    Case bTop : m.Result = New IntPtr(HTTOP)
                    Case bRight : m.Result = New IntPtr(HTRIGHT)
                    Case bBottom : m.Result = New IntPtr(HTBOTTOM)
                    Case bLeft : m.Result = New IntPtr(HTLEFT)
                End Select
        End Select
    End Sub

    Private Sub Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim windowLong As Integer = GetWindowLong(Me.Handle, -16)
        'SetWindowLong(Me.Handle, -16, windowLong Or WS_MINIMIZEBOX Or WS_SYSMENU)
        'Dim h As Integer = Screen.PrimaryScreen.Bounds.Height - Screen.GetWorkingArea(New Point()).Height
        'MsgBox(h)
    End Sub
End Class
