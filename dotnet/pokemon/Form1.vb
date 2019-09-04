Public Class Form1
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim hwnd As Integer = API.FindWindow("Qt5QWindowIcon", "夜神安卓模拟器")
        Dim hwnd2 As Integer = API.FindWindowEx(hwnd, 0, "Qt5QWindowIcon", "ScreenBoardClassWindow")
        Dim hwnd3 As Integer = API.FindWindowEx(hwnd2, 0, "Qt5QWindowIcon", "QWidgetClassWindow")
        Dim hwnd4 As Integer = API.FindWindowEx(hwnd3, 0, "subWin", "sub")

        Dim r As API.RECT
        API.GetWindowRect(hwnd4, r)
        Me.Width = r.Right - r.Left
        Me.Height = r.Bottom - r.Top
        Me.Left = r.Left
        Me.Top = r.Top
        
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim h As Integer = API.FindWindow("Qt5QWindow", "Nox")
        If h Then
            API.SetParent(h, Me.Handle)
        End If
        'Dim lpString As String = New String(Chr(0), 255)
        'API.GetWindowText(API.GetForegroundWindow, lpString, 255)
        'Dim winText As String = Microsoft.VisualBasic.Left(lpString, 3)
        'Dim correctWindow As Boolean
        'If winText = "Nox" Then
        '    correctWindow = True
        'ElseIf winText = "夜神安" Then
        '    correctWindow = True
        'Else
        '    correctWindow = False
        'End If
        'If correctWindow Then
        '    Me.Show()
        'Else
        '    Me.Hide()
        'End If
        '' Button1.Text = Hex(API.GetForegroundWindow).ToString
        'Me.TopMost = True
    End Sub
End Class

Class API
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure
    Public Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As Int32, ByVal hWndNewParent As Int32) As Boolean
    Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    Public Declare Function GetForegroundWindow Lib "user32" () As Integer
    Public Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Public Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (hwnd As Integer, lpString As String) As Integer
End Class