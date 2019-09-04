Imports System.Runtime.InteropServices

Public Class Form1

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function FindWindow( _
     ByVal lpClassName As String, _
     ByVal lpWindowName As String) As IntPtr
    End Function

    Declare Auto Function SetWindowText Lib "user32" (ByVal hWnd As IntPtr, ByVal lpstring As String) As Boolean

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim url As String
        If My.Application.CommandLineArgs.Count < 1 Then
            url = "is nothing"
        Else
            url = My.Application.CommandLineArgs(0)
        End If

        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter("test.txt", True)
        file.WriteLine(url)
        file.Close()

        'Dim hwnd As Integer = FindWindow(vbNullString, "yunTool - Accepter")
        'SetWindowText(hwnd, url)
        'Me.Close()
    End Sub
End Class