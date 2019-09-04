Public Class Form1
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

    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Public Const WM_HOTKEY As Integer = &H312

    Dim p(100) As Point

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        RegisterHotKey(Me.Handle, 0, 0, Keys.Z)
        RegisterHotKey(Me.Handle, 1, 0, Keys.A)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 0)
        UnregisterHotKey(Me.Handle, 1)
    End Sub

    Dim working As Boolean = False
    Dim setted As Boolean
    Dim p0 As Point

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    If working Then
                        endFuntion()
                    Else
                        beginFunction()
                    End If
                Case 1
                    If setted Then
                        SetCursorPos(p0.X, p0.Y)
                    Else
                        p0 = GetCursor()
                        setted = True
                    End If
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Sub beginFunction()
        working = True

        p(0) = GetCursor()
        For i = 1 To 30
            If i >= 1 And i <= 3 Then
                p(i) = New Point(p(0).X, p(0).Y - i * 50)
            ElseIf i >= 4 And i <= 12 Then
                p(i) = New Point(p(0).X - 40, p(0).Y - (i - 4) * 50)
            ElseIf i >= 13 And i <= 21 Then
                p(i) = New Point(p(0).X - 80, p(0).Y - (i - 13) * 50)
            ElseIf i >= 22 And i <= 30 Then
                p(i) = New Point(p(0).X - 120, p(0).Y - (i - 22) * 50)
            End If
        Next
        For i = 31 To 42
            If i Mod 3 = 1 Then
                p(i) = New Point(p(0).X - 466, p(0).Y - 410 + (i - 31) / 3 * 50)
            ElseIf i Mod 3 = 2 Then
                p(i) = New Point(p(0).X + 7, p(0).Y - 224)
            ElseIf i Mod 3 = 0 Then
                p(i) = New Point(p(0).X - 65, p(0).Y + 79)
            End If
        Next
        p(43) = New Point(p(0).X - 797, p(0).Y - 163)
        p(44) = New Point(p(0).X - 470, p(0).Y - 163)
        p(45) = New Point(p(0).X - 129, p(0).Y + 168)

        t = 0
        steper = 0
        index = 0
        Timer1.Start()
    End Sub

    Sub endFuntion()
        working = False
        Timer1.Stop()
        
    End Sub

    Dim t As Integer = 0
    Dim steper As Integer = 0
    Dim index As Integer = 0
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If steper = 0 Then
            If t = 3000 + Timer1.Interval Then
                steper = 1
            Else
                mouse_event(2, 0, 0, 0, 0)
                mouse_event(4, 0, 0, 0, 0)
                t += Timer1.Interval
            End If
        ElseIf steper = 1 Then
            index += 1
            SetCursorPos(p(index).X, p(index).Y)

            If index = 45 Then
                endFuntion()
            Else
                mouse_event(2, 0, 0, 0, 0)
                mouse_event(4, 0, 0, 0, 0)
            End If
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        Me.Text = GetCursor().ToString
    End Sub
End Class
