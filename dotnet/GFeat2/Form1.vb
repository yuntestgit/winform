Public Class Form1
    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Public Const WM_HOTKEY As Integer = &H312

    Declare Function SetCursorPos Lib "user32" (ByVal X As Integer, ByVal Y As Integer) As Boolean
    Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer
    Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
    End Structure
    Function GetCursor() As Point
        Dim pa As POINTAPI
        GetCursorPos(pa)
        Return New Point(pa.x, pa.y)
    End Function

    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)

    Dim p1, p2, p3, p4, p5, p6, p7, p8, p9 As Point
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        hotkey(0, Keys.NumPad0)
        hotkey(1, Keys.NumPad1)
        hotkey(2, Keys.NumPad2)
        hotkey(3, Keys.NumPad3)
        hotkey(4, Keys.NumPad4)
        hotkey(5, Keys.NumPad5)
        hotkey(6, Keys.NumPad6)
        hotkey(7, Keys.NumPad7)
        hotkey(8, Keys.NumPad8)
        hotkey(9, Keys.NumPad9)
        Me.Text = GetCursor.ToString
    End Sub

    Dim i As Integer = 0
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        i += 1

        If i = 1 Then
            SetCursorPos(p1.X, p1.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf i = 2 Then
            SetCursorPos(p2.X, p2.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf i = 3 Then
            SetCursorPos(p3.X, p3.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf i = 4 Then
            SetCursorPos(p4.X, p4.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf i = 5 Then
            SetCursorPos(p5.X, p5.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf i = 6 Then
            SetCursorPos(p6.X, p6.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf i = 7 Then
            SetCursorPos(p7.X, p7.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf i = 8 Then
            SetCursorPos(p8.X, p8.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        ElseIf i = 9 Then
            SetCursorPos(p9.X, p9.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        End If

        If Val(TextBox1.Text) = i Then
            i = 0
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    i = 0
                    If Timer1.Enabled = False Then
                        Timer1.Enabled = True
                    Else
                        Timer1.Enabled = False
                    End If
                Case 1
                    p1 = GetCursor()
                Case 2
                    p2 = GetCursor()
                Case 3
                    p3 = GetCursor()
                Case 4
                    p4 = GetCursor()
                Case 5
                    p5 = GetCursor()
                Case 6
                    p6 = GetCursor()
                Case 7
                    p7 = GetCursor()
                Case 8
                    p8 = GetCursor()
                Case 9
                    p9 = GetCursor()
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Sub hotkey(id As Integer, key As Integer)
        RegisterHotKey(Me.Handle, id, 0, key)
    End Sub

    Sub unhotkey(id As Integer)
        UnregisterHotKey(Me.Handle, id)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        unhotkey(0)
        unhotkey(1)
        unhotkey(2)
        unhotkey(3)
        unhotkey(4)
        unhotkey(5)
        unhotkey(6)
        unhotkey(7)
        unhotkey(8)
        unhotkey(9)
    End Sub
End Class
