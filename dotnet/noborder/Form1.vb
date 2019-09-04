Public Class Form1
    
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
                Dim 目標寬度 As Integer = 5
                Dim nPosX = CInt(m.LParam) Mod 65536    '---LoWord---
                Dim nPosY = CInt(m.LParam) / 65536      '---HiWord---
                Dim 上 As Boolean = (nPosY <= Top + 目標寬度)
                Dim 右 As Boolean = (nPosX >= Left + Width - 目標寬度)
                Dim 下 As Boolean = (nPosY >= Top + Height - 目標寬度)
                Dim 左 As Boolean = (nPosX <= Left + 目標寬度)
                Select Case True
                    Case 上 * 右 : m.Result = New IntPtr(HTTOPRIGHT)
                    Case 上 * 左 : m.Result = New IntPtr(HTTOPLEFT)
                    Case 下 * 右 : m.Result = New IntPtr(HTBOTTOMRIGHT)
                    Case 下 * 左 : m.Result = New IntPtr(HTBOTTOMLEFT)
                    Case 上 : m.Result = New IntPtr(HTTOP)
                    Case 右 : m.Result = New IntPtr(HTRIGHT)
                    Case 下 : m.Result = New IntPtr(HTBOTTOM)
                    Case 左 : m.Result = New IntPtr(HTLEFT)
                End Select
        End Select
    End Sub
End Class
