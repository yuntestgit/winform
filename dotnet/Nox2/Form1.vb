Imports System.Runtime.InteropServices
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


#Region "SetParent"
    Public Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As Int32, ByVal hWndNewParent As Int32) As Boolean
    Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
#End Region

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


#Region "Findwindow"
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function FindWindow( _
     ByVal lpClassName As String, _
     ByVal lpWindowName As String) As IntPtr
    End Function
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
#End Region

#Region "Message"
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Const WM_MOUSE_MOVE = &H200
    Public Const WM_LBUTTON_DOWN = &H201
    Public Const WM_LBUTTON_UP = &H202
    Public Const WM_KEYDOWN = &H100
    Public Const WM_KEYUP = &H101
    Public Const WM_CHAR = &H102
    Public Function MAKELPARAM(ByVal l As Integer, ByVal h As Integer) As Integer
        Dim r As Integer = l + (h << 16)
        Return r
    End Function
#End Region

    Dim i As Integer = 1
    Dim hwnd, hwnd2, hwnd3 As Integer

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        hwnd = FindWindow("Qt5QWindowIcon", "夜神模拟器")
        hwnd2 = FindWindowEx(hwnd, 0, "Qt5QWindowIcon", "ScreenBoardClassWindow")
        hwnd3 = FindWindowEx(hwnd2, 0, "Qt5QWindowIcon", "QWidgetClassWindow")
        'Me.Text = Hex(hwnd3).ToString
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick

        'SetParent(hwnd, Me.Handle)
        'SetWindowPos(Me.Handle, 0, 10, 688, 300, 300, 4)
        If i = 0 Then
            Dim b As Bitmap = GetBitmap(hwnd3)
            PictureBox2.Image = b
        Else
            Dim b As Bitmap = GetBitmap(PictureBox1.Handle)
            PictureBox2.Image = b
        End If

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Dim h As Integer = FindWindow(vbNullString, "夜神模拟器")
        'hwnd = &H1E08B4
        SetParent(hwnd3, PictureBox1.Handle)
        SetWindowPos(hwnd, 0, 0, 0, 800, 600, 4)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        i = 1
    End Sub

    
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        PictureBox1.BackColor = Color.Green
    End Sub
End Class
