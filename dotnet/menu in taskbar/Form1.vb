Public Class Form1
    Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nlndex As Integer, ByVal wNewLong As Integer) As Integer
    Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer

    Const WS_SYSMENU = &H80000
    Const WS_MINIMIZEBOX = &H20000

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim windowLong As Integer = GetWindowLong(Me.Handle, -16)
        SetWindowLong(Me.Handle, -16, windowLong Or WS_MINIMIZEBOX Or WS_SYSMENU)
    End Sub
End Class
