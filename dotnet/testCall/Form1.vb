Imports System.Runtime.InteropServices

Public Class Form1

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function FindWindow( _
     ByVal lpClassName As String, _
     ByVal lpWindowName As String) As IntPtr
    End Function

    Declare Auto Function SetWindowText Lib "user32" (ByVal hWnd As IntPtr, ByVal lpstring As String) As Boolean

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim hwnd As Integer = FindWindow(vbNullString, "yunTool - Accepter")
        SetWindowText(hwnd, "C:\AppServ\www\av\test.flv")
        'Me.Close()
    End Sub
End Class