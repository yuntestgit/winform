Public Class Form1

    '20160410163854087,553,490,160,48,1,65
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Timer1.Interval = 1
        Timer1.Start()

    End Sub

    Public Function GetBitmap() As Bitmap
        Dim hwnd As Integer = API.FindWindow("Qt5QWindowIcon", "夜神模拟器")
        Dim hwnd2 As Integer = API.FindWindowEx(hwnd, 0, "Qt5QWindowIcon", "ScreenBoardClassWindow")
        Dim hwnd3 As Integer = API.FindWindowEx(hwnd2, 0, "Qt5QWindowIcon", "QWidgetClassWindow")

        Dim r As API.RECT
        API.GetWindowRect(hwnd3, r)

        Dim LocationX As Integer
        Dim LocationY As Integer
        Dim rWidth As Integer = r.Right - r.Left
        Dim rHeight As Integer = r.Bottom - r.Top

        Dim r2 As API.RECT
        API.GetClientRect(hwnd3, r2)
        Dim rborder As Integer = ((r.Right - r.Left) - r2.Right) / 2
        Dim rtitle As Integer = ((r.Bottom - r.Top) - r2.Bottom) - rborder
        LocationX = r.Left + rborder
        LocationY = r.Top + rtitle
        rWidth = r2.Right
        rHeight = r2.Bottom


        Dim Bhandle, DestDC, SourceDC As IntPtr

        SourceDC = API.GetDC(0)
        DestDC = API.CreateCompatibleDC(SourceDC)

        Bhandle = API.CreateCompatibleBitmap(SourceDC, rWidth, rHeight)
        API.SelectObject(DestDC, Bhandle)
        API.BitBlt(DestDC, 0, 0, rWidth, rHeight, SourceDC, LocationX, LocationY, API.SRCCOPY)

        Dim bmp As Bitmap = System.Drawing.Image.FromHbitmap(Bhandle)

        API.ReleaseDC(0, SourceDC)
        API.DeleteDC(DestDC)
        API.DeleteObject(Bhandle)
        GC.Collect()

        Return bmp
    End Function

    Function inside() As Boolean
        Dim mainbmp As Bitmap = GetBitmap()
        Dim bmp As Bitmap
        Dim X As Integer = 553
        Dim Y As Integer = 490
        Dim Width As Integer = 160
        Dim Height As Integer = 48
        Dim Offset As Integer = 1
        Dim Key As Integer = 65

        Dim sr As System.IO.FileStream = New System.IO.FileStream("C:\Nox\20160410163854087.bmp", IO.FileMode.Open, IO.FileAccess.Read)
        bmp = Image.FromStream(sr)
        sr.Close()

        Dim found As Boolean = True

        For i = X To X + Width - 1

            For j = Y To Y + Height - 1
                If mainbmp.GetPixel(i, j) <> bmp.GetPixel(i, j) Then
                    'MsgBox(-1)
                    found = False
                    Exit For
                End If
            Next
            'If found = False Then
            '    Exit For
            'End If

        Next
        Return found
    End Function

    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If inside() Then
            MsgBox(1)
        End If
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If Not BackgroundWorker1.IsBusy Then
            BackgroundWorker1.RunWorkerAsync()
        End If
    End Sub
End Class

Public Class API
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
End Class