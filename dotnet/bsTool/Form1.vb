Public Class Form1

    Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer

    Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer

    Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer


    Public Const MOUSEEVENTF_LEFTDOWN = &H201    '模擬鼠標左鍵按下
    Public Const MOUSEEVENTF_LEFTUP = &H202    '模擬鼠標左鍵抬起
    Public Function MAKELPARAM(ByVal l As Integer, ByVal h As Integer) As Integer '仿C# MAKELPARAM()函數
        'Dim ll As Integer
        'Dim lh As Integer
        'Dim r As Integer
        'll = Val(Format(Hex(l), "####"))
        'lh = Val(Format(Hex(h), "####"))

        'r = CInt("&h" & Replace(lh & ll, " ", "0"))

        '~~~~~~~~~~~阿倫修改~~~~~~~~~~~~~~
        Dim r As Integer = l + (h << 16)
        '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        Return r
    End Function


    'Private Declare Function CreateCompatibleDC Lib "GDI32" (ByVal hDC As Integer) As Integer
    'Private Declare Function CreateCompatibleBitmap Lib "GDI32" (ByVal hDC As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    'Private Declare Function SelectObject Lib "GDI32" (ByVal hDC As Integer, ByVal hObject As Integer) As Integer
    'Private Declare Function BitBlt Lib "GDI32" (ByVal srchDC As Integer, ByVal srcX As Integer, ByVal srcY As Integer, ByVal srcW As Integer, ByVal srcH As Integer, ByVal desthDC As Integer, ByVal destX As Integer, ByVal destY As Integer, ByVal op As Integer) As Integer
    'Private Declare Function DeleteDC Lib "GDI32" (ByVal hDC As Integer) As Integer
    'Private Declare Function DeleteObject Lib "GDI32" (ByVal hObj As Integer) As Integer
    'Declare Function GetDC Lib "user32" Alias "GetDC" (ByVal hwnd As Integer) As Integer
    'Const SRCCOPY As Integer = &HCC0020

    'Public Declare Function ReleaseDC Lib "user32.dll" (ByVal hWnd As Integer, ByVal prmlngHDc As Integer) As Integer

    'Function PrintWindow(hwnd As Integer) As Image
    '    Dim rimg As Image

    '    Dim hDC, hMDC As Integer
    '    Dim hBMP, hBMPOld As Integer
    '    Dim sw, sh As Integer
    '    'hDC = GetDC(0)
    '    hDC = GetDC(hwnd)
    '    hMDC = CreateCompatibleDC(hDC)
    '    '因為不知道要指定抓多大的範圍,所以以螢幕的大小為準
    '    sw = Screen.PrimaryScreen.Bounds.Width
    '    sh = Screen.PrimaryScreen.Bounds.Height

    '    hBMP = CreateCompatibleBitmap(hDC, sw, sh)
    '    hBMPOld = SelectObject(hMDC, hBMP)
    '    BitBlt(hMDC, 0, 0, sw, sh, hDC, 0, 0, SRCCOPY)
    '    hBMP = SelectObject(hMDC, hBMPOld)
    '    'PictureBox1.Image = Image.FromHbitmap(New IntPtr(hBMP))
    '    rimg = Image.FromHbitmap(New IntPtr(hBMP))

    '    DeleteDC(hDC)
    '    ReleaseDC(hwnd, hDC)
    '    DeleteDC(hMDC)
    '    DeleteObject(hBMP)

    '    Return rimg
    'End Function

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

        If BitWidth < 0 Then BitWidth = My.Computer.Screen.Bounds.Width
        If BitHeight < 0 Then BitHeight = My.Computer.Screen.Bounds.Height
        Dim Bhandle, DestDC, SourceDC As IntPtr
        'SourceDC = CreateDC("DISPLAY", Nothing, Nothing, 0)
        SourceDC = GetDC(hwnd)
        DestDC = CreateCompatibleDC(SourceDC)

        Bhandle = CreateCompatibleBitmap(SourceDC, BitWidth, BitHeight)
        SelectObject(DestDC, Bhandle)
        BitBlt(DestDC, 0, 0, BitWidth, BitHeight, SourceDC, 0, 0, &HCC0020)

        Dim bmp As Bitmap = Image.FromHbitmap(Bhandle)

        ReleaseDC(hwnd, SourceDC)
        DeleteDC(DestDC)
        DeleteObject(Bhandle)
        GC.Collect()

        Return bmp

    End Function

    Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As Int32, ByVal hWndNewParent As Int32) As Boolean

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Dim b As Bitmap = New Bitmap(1920, 1080)
        Me.DrawToBitmap(b, New System.Drawing.Rectangle(0, 0, 1920, 1080))
        PictureBox1.Image = b

        'Dim h As Integer = 0
        'PictureBox1.Image = GetBitmap(h)

        'QWidget
        'Dim h As Integer = FindWindow("QWidget", vbNullString)
        'Dim h2 As Integer = 0
        'Dim h3 As Integer = 0
        'Dim h4 As Integer
        'If h Then
        '    While True
        '        h2 = FindWindowEx(h, h2, "QWidget", vbNullString)
        '        h3 = FindWindowEx(h2, 0, "QWidget", vbNullString)
        '        h4 = h3
        '        h3 = FindWindowEx(h3, 0, "subWin", "sub")
        '        If h3 Then
        '            Exit While
        '        Else
        '            h3 = 0
        '        End If
        '    End While

        '    If h3 <> 0 Then
        '        Me.Text = Hex(h3).ToString
        '        PictureBox1.Image = GetBitmap(h4)
        '    End If
        'End If

        'Dim h As Integer = FindWindow(vbNullString, "BlueStacks App Player")
        'Dim h2 As Integer = FindWindowEx(h, 0, vbNullString, vbNullString)



        'Dim x As Integer = 20
        'Dim y As Integer = 20

        'Dim h3 As Integer = FindWindowEx(Me.Handle, 0, vbNullString, "Button2")

        'Me.Text = h.ToString & ", " & h2.ToString & "," & h3.ToString

        'PostMessage(h, MOUSEEVENTF_LEFTDOWN, 0, MAKELPARAM(x, y))    '點下滑鼠左鍵

        'PostMessage(h, MOUSEEVENTF_LEFTUP, 0, MAKELPARAM(x, y))      '放開滑鼠左鍵
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'SetParent(&H50270, Me.Handle)
    End Sub

    Dim x As Integer = 500
    Dim y As Integer = 450

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        'Dim h As Integer = FindWindow(vbNullString, "BlueStacks App Player")
        'GetSerPic(h)
        'PictureBox1.Image = GetBitmap(h)
        Dim h As Integer = FindWindow("QWidget", vbNullString)

        PostMessage(h, MOUSEEVENTF_LEFTDOWN, 0, MAKELPARAM(x, y))    '點下滑鼠左鍵

        PostMessage(h, MOUSEEVENTF_LEFTUP, 0, MAKELPARAM(x, y))      '放開滑鼠左鍵
    End Sub
End Class
