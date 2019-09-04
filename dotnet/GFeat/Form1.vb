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

    Dim p1, p2 As Point
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        hotkey(0, Keys.NumPad0)
        hotkey(1, Keys.NumPad1)
        hotkey(2, Keys.NumPad2)
        Me.Text = GetCursor.ToString
    End Sub

    Dim i As Integer = 0
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        i += 1

        If i Mod 2 = 0 Then
            SetCursorPos(p1.X, p1.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        Else
            SetCursorPos(p2.X, p2.Y)
            mouse_event(2, 0, 0, 0, 0)
            mouse_event(4, 0, 0, 0, 0)
        End If


    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    If Timer1.Enabled = False Then
                        Timer1.Enabled = True
                    Else
                        Timer1.Enabled = False
                    End If
                Case 1
                    p1 = GetCursor()
                Case 2
                    p2 = GetCursor()
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
    End Sub
End Class
