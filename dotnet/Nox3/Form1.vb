'Imports System.Runtime.InteropServices

Public Class Form1
#Region "API"
    '<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    'Public Shared Function FindWindow( _
    ' ByVal lpClassName As String, _
    ' ByVal lpWindowName As String) As IntPtr
    'End Function

    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Public Const WM_MOUSE_MOVE = &H200
    Public Const WM_LBUTTON_DOWN = &H201
    Public Const WM_LBUTTON_UP = &H202
    Public Const WM_KEYDOWN = &H100
    Public Const WM_KEYUP = &H101
    Public Const WM_CHAR = &H102
#End Region

    Public Function PrintWindow(hwnd As Integer, Optional ByVal border As Boolean = False) As Bitmap
        Try
            Dim r As RECT
            GetWindowRect(hwnd, r)

            Dim LocationX As Integer
            Dim LocationY As Integer
            Dim rWidth As Integer = r.Right - r.Left
            Dim rHeight As Integer = r.Bottom - r.Top

            If border = False Then
                Dim r2 As RECT
                GetClientRect(hwnd, r2)
                Dim rborder As Integer = ((r.Right - r.Left) - r2.Right) / 2
                Dim rtitle As Integer = ((r.Bottom - r.Top) - r2.Bottom) - rborder
                LocationX = r.Left + rborder
                LocationY = r.Top + rtitle
                rWidth = r2.Right
                rHeight = r2.Bottom
            Else
                LocationX = r.Left
                LocationY = r.Top
                rWidth = r.Right - r.Left
                rHeight = r.Bottom - r.Top
            End If

            Dim Screenshot As New Bitmap(rWidth, rHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            Dim Graph As Graphics = Graphics.FromImage(Screenshot)
            Graph.CopyFromScreen(LocationX, LocationY, 0, 0, New Size(rWidth, rHeight), CopyPixelOperation.SourceCopy)

            Return Screenshot
        Catch ex As Exception
            Return Image.FromFile("C:\Users\yun\Desktop\nothing.png")
        End Try
    End Function

    'C:\Users\yun\Desktop\nothing.png

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        'Try
        '    'Dim hwnd As Integer = FindWindow(vbNullString, "夜神模拟器")
        '    Dim bmp As Bitmap = PrintWindow(&H103AC)
        '    PictureBox1.Image = bmp
        'Catch ex As Exception

        'End Try
        Dim myImage As New Bitmap(1024, 768)
        Dim g = Graphics.FromImage(myImage)
        g.CopyFromScreen(New Point(0, 0), New Point(0, 0), New Size(1024, 768))
        PictureBox1.Image = myImage
    End Sub
End Class

Class Nox


End Class