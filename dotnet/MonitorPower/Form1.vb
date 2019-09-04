Public Class Form1
    Public WM_SYSCOMMAND As Integer = &H112
    Public SC_MONITORPOWER As Integer = &HF170

    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    '    MONITOR_ON = -1,
    '13
    '            MONITOR_STANBY = 1,
    '14
    '            MONITOR_OFF

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        SendMessage(Me.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2)
    End Sub
End Class
