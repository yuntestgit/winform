Public Class Form1
    Declare Function SelectObject Lib "gdi32" (ByVal hdc As Integer, ByVal hObject As Integer) As Integer
    Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer
    Declare Function CreateCompatibleBitmap Lib "gdi32" (ByVal hdc As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    Declare Function CreateDC Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, ByRef lpInitData As Integer) As Integer
    Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hdc As Integer) As Integer

    Declare Function GetDC Lib "user32" Alias "GetDC" (ByVal hwnd As Integer) As Integer


    Private Declare Function DeleteDC Lib "GDI32" (ByVal hDC As Integer) As Integer
    Private Declare Function DeleteObject Lib "GDI32" (ByVal hObj As Integer) As Integer
    Public Declare Function ReleaseDC Lib "user32.dll" (ByVal hWnd As Integer, ByVal prmlngHDc As Integer) As Integer

    Function GetBitmap(Optional ByVal hwnd As Integer = 0, Optional ByVal BitWidth As Integer = -1, Optional ByVal BitHeight As Integer = -1) As Bitmap
        '13369376
        '&HCC0020

        Dim SRCCOPY As Integer = &HCC0020
        Dim SRCPAINT As Integer = &HEE0086
        Dim SRCAND As Integer = &H8800C6
        Dim SRCINVERT As Integer = &H660046
        Dim SRCERASE As Integer = &H440328
        Dim NOTSRCCOPY As Integer = &H330008
        Dim NOTSRCERASE As Integer = &H1100A6
        Dim MERGECOPY As Integer = &HC000CA
        Dim MERGEPAINT As Integer = &HBB0226
        Dim PATCOPY As Integer = &HF00021
        Dim PATPAINT As Integer = &HFB0A09
        Dim PATINVERT As Integer = &H5A0049
        Dim DSTINVERT As Integer = &H550009
        Dim BLACKNESS As Integer = &H42
        Dim WHITENESS As Integer = &HFF0062
        Dim CAPTUREBLT As Integer = &H40000000

        If BitWidth < 0 Then BitWidth = My.Computer.Screen.Bounds.Width
        If BitHeight < 0 Then BitHeight = My.Computer.Screen.Bounds.Height
        Dim Bhandle, DestDC, SourceDC As IntPtr
        'SourceDC = CreateDC("DISPLAY", Nothing, Nothing, 0)
        SourceDC = GetDC(hwnd)
        DestDC = CreateCompatibleDC(SourceDC)

        Bhandle = CreateCompatibleBitmap(SourceDC, BitWidth, BitHeight)
        SelectObject(DestDC, Bhandle)
        BitBlt(DestDC, 0, 0, BitWidth, BitHeight, SourceDC, 0, 0, SRCCOPY)

        Dim bmp As Bitmap = Image.FromHbitmap(Bhandle)

        ReleaseDC(hwnd, SourceDC)
        DeleteDC(DestDC)
        DeleteObject(Bhandle)
        GC.Collect()

        Return bmp

    End Function


    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer



    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim h As Integer = FindWindow("RCWindow", vbNullString)
        ' h = FindWindowEx(h, 0, vbNullString, "widgetTitleWindow")

        Dim b As Bitmap = GetBitmap(&H1304E8)
        Me.BackgroundImage = b



    End Sub
End Class
