Public Class Form1
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function

    Declare Function GetForegroundWindow Lib "user32" () As Integer
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

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

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer

    Declare Function ClipCursor Lib "user32" (ByRef lpRect As RECT) As Long
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function ReleaseCapture() As Boolean
    End Function

    Function getRange(hwnd As Integer) As RECT
        Dim outer, inner, range As RECT
        GetWindowRect(hwnd, outer)
        GetClientRect(hwnd, inner)
        Dim rborder As Integer = ((outer.Right - outer.Left) - inner.Right) / 2
        Dim rtitle As Integer = ((outer.Bottom - outer.Top) - inner.Bottom) - rborder
        range.Left = outer.Left + rborder
        range.Top = outer.Top + rtitle
        range.Right = range.Left + inner.Right
        range.Bottom = range.Top + inner.Bottom
        Return range
    End Function

    Dim lastState = -1
    Dim targetWindow As Integer = -1
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        lastState = Int(My.Computer.Keyboard.ScrollLock)
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim state As Integer = Int(My.Computer.Keyboard.ScrollLock)
        If state <> lastState Then
            lastState = state
            Me.Text = lastState
            If lastState = 0 Then
                'Timer2.Stop()
                Dim desktopWindow = FindWindow("Progman", "Program Manager")
                ClipCursor(getRange(desktopWindow))
                targetWindow = -1
            Else
                targetWindow = GetForegroundWindow
                Timer2.Start()
            End If
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        If targetWindow <> -1 Then
            If GetForegroundWindow = targetWindow Then
                Dim range As RECT = getRange(targetWindow)
                ClipCursor(range)
            End If
        End If
    End Sub
End Class
