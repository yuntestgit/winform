Public Class Form1
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer
    Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function

    Const TOGGLE_HIDEWINDOW = &H80
    Const TOGGLE_UNHIDEWINDOW = &H40


    Dim first As Boolean = True

    Private Sub Form1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        If first Then
            Me.Top = 0
            Me.Left = 0
            Me.Opacity = 0.01
            first = False
        End If
    End Sub

    Dim hwnd As Integer

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Keys.Home) <> 0 Then
            Me.Show()
            hwnd = FindWindow("Shell_traywnd", vbNullString)
            If hwnd Then SetWindowPos(hwnd, 0, 0, 0, 0, 0, TOGGLE_HIDEWINDOW)
        End If


        If GetAsyncKeyState(Keys.End) <> 0 Then
            Me.Hide()
            SetWindowPos(hwnd, 0, 0, 0, 0, 0, TOGGLE_UNHIDEWINDOW)
        End If
    End Sub
End Class
