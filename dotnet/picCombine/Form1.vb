Public Class Form1

    Dim _width As Integer = 1080
    Dim _height As Integer = 0

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim s As String = "C:\Users\yun\Desktop\dog\"
        Dim bmp(12) As Bitmap



        

        For i = -1 To 10
            bmp(i + 1) = BitmapFromFile(s & i.ToString & ".png")
            _height += bmp(i + 1).Height
        Next

        

        Dim cbmp As New Bitmap(width, height)

        Dim pic(12) As PictureBox
        For i = 0 To 11
            pic(i) = New PictureBox
            With pic(i)
                .Image = bmp(i)
                .Width = bmp(i).Width
                .Height = bmp(i).Height
                .Left = 0
                If i = 0 Then
                    .Top = 0
                Else
                    .Top = pic(i - 1).Top + pic(i - 1).Height
                    Me.Text = Me.Text & "," & pic(i - 1).Top + pic(i - 1).Height
                End If
            End With

            Me.Controls.Add(pic(i))
        Next

        'Dim g As Graphics = Me.CreateGraphics
        'Me.DrawToBitmap(cbmp, New Rectangle(0, 0, width, height))
        Me.Width = _width + 30
        Me.Text = Me.Text & "-" & _height
        'Me.Height = _height
    End Sub

    Function BitmapFromFile(Path As String) As Bitmap
        Dim bmp As Bitmap
        Dim sr As System.IO.FileStream = New System.IO.FileStream(Path, IO.FileMode.Open, IO.FileAccess.Read)
        bmp = Image.FromStream(sr)
        sr.Close()
        Return bmp
    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim cbmp As Bitmap
        cbmp = PrintWindow(Me.Handle)
        cbmp.Save("C:\Users\yun\Desktop\dog\_0.bmp", System.Drawing.Imaging.ImageFormat.Bmp)
    End Sub

    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function

    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function SelectObject Lib "gdi32" (ByVal hdc As Integer, ByVal hObject As Integer) As Integer
    Public Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer
    Public Declare Function CreateCompatibleBitmap Lib "gdi32" (ByVal hdc As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    Public Declare Function CreateDC Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, ByRef lpInitData As Integer) As Integer
    Public Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hdc As Integer) As Integer
    Public Declare Function GetDC Lib "user32" Alias "GetDC" (ByVal hwnd As Integer) As Integer
    Public Declare Function DeleteDC Lib "GDI32" (ByVal hDC As Integer) As Integer
    Public Declare Function DeleteObject Lib "GDI32" (ByVal hObj As Integer) As Integer
    Public Declare Function ReleaseDC Lib "user32.dll" (ByVal hWnd As Integer, ByVal prmlngHDc As Integer) As Integer
    Public Declare Function DwmIsCompositionEnabled Lib "dwmapi.dll" Alias "DwmIsCompositionEnabled" (ByRef pfEnabled As Boolean) As Integer
    Public Declare Function DwmExtendFrameIntoClientArea Lib "dwmapi.dll" Alias "DwmExtendFrameIntoClientArea" (ByVal hWnd As IntPtr, ByRef pMargin As Margins) As Integer
    Public Declare Function ReleaseCapture Lib "user32" Alias "ReleaseCapture" () As Boolean
    Public Declare Function MoveWindow Lib "user32" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Const WM_MOUSE_MOVE = &H200
    Public Const WM_LBUTTON_DOWN = &H201
    Public Const WM_LBUTTON_UP = &H202
    Public Const WM_KEYDOWN = &H100
    Public Const WM_KEYUP = &H101
    Public Const WM_CHAR = &H102

    Public Structure Margins
        Public Left As Integer
        Public Right As Integer
        Public Top As Integer
        Public Bottom As Integer
    End Structure

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure


    Public Const WM_SYSCOMMAND = &H112
    Public Const SC_MOVE = &HF010&
    Public Const HTCAPTION = &H2
    Public Const SRCCOPY = &HCC0020

    Public Function PrintWindow(hwnd As Integer, Optional ByVal border As Boolean = False) As Bitmap
        Dim Bhandle, DestDC, SourceDC As IntPtr

        SourceDC = GetDC(hwnd)
        DestDC = CreateCompatibleDC(SourceDC)

        Bhandle = CreateCompatibleBitmap(SourceDC, _width, _height)
        SelectObject(DestDC, Bhandle)
        BitBlt(DestDC, 0, 0, _width, _height, SourceDC, 0, 0, SRCCOPY)

        Dim bmp As Bitmap = System.Drawing.Image.FromHbitmap(Bhandle)

        ReleaseDC(0, SourceDC)
        DeleteDC(DestDC)
        DeleteObject(Bhandle)
        GC.Collect()

        Return bmp
    End Function
End Class