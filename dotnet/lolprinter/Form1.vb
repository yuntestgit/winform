Public Class Form1
    'bmp.Save(bmpPath, System.Drawing.Imaging.ImageFormat.Bmp)

    Dim hwnd As Integer
    Dim bmp As Bitmap

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RegisterHotKey(Me.Handle, 0, 0, Keys.Home)
        RegisterHotKey(Me.Handle, 1, 0, Keys.NumPad4)
        RegisterHotKey(Me.Handle, 2, 0, Keys.NumPad5)
        RegisterHotKey(Me.Handle, 3, 0, Keys.NumPad1)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    hwnd = API.GetForegroundWindow
                    Me.Text = hwnd
                Case 1
                    bmp = APIplus.GetBitmap(hwnd)
                    bmphandling(4)
                Case 2
                    bmp = APIplus.GetBitmap(hwnd)
                    bmphandling(5)
                Case 3
                    bmp = APIplus.GetBitmap(hwnd)
                    bmphandling(1)
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 0)
        UnregisterHotKey(Me.Handle, 1)
        UnregisterHotKey(Me.Handle, 2)
        UnregisterHotKey(Me.Handle, 3)
    End Sub

    Sub bmphandling(num As Integer)
        Dim location As New Point(423, 628)
        Dim range As New Size(115, 65)
        If num = 5 Then
            location.X = 471
        ElseIf num = 4 Then
            location.X = 423
        ElseIf num = 1 Then
            location.X = 520
        End If
        ''Dim location As New Point(373, 558)
        ''Dim range As New Size(215, 165)
        ''If num = 5 Then
        ''    location.X = 471 - 50
        ''ElseIf num = 4 Then
        ''    location.X = 423 - 50
        ''ElseIf num = 1 Then
        ''    location.X = 520 - 50
        ''End If

        Dim bmp2 As New Bitmap(range.Width, range.Height)
        Dim g As Graphics = Graphics.FromImage(bmp2)
        g.DrawImage(bmp, New Rectangle(0, 0, range.Width, range.Height), New Rectangle(location.X, location.Y, range.Width, range.Height), GraphicsUnit.Pixel)

        For i = 0 To 1000
            Dim path As String = "C:\Users\yun\Desktop\lolskin\" + Format(i, "000") + ".bmp"
            If Not My.Computer.FileSystem.FileExists(path) Then
                bmp2.Save(path, System.Drawing.Imaging.ImageFormat.Bmp)
                Exit For
            End If
        Next

        Me.BackgroundImage = bmp2
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick

    End Sub


End Class

Module API
    Public Const WM_HOTKEY As Integer = &H312
    Public Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Public Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer
    Public Declare Function GetForegroundWindow Lib "user32" () As Integer

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

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure
End Module

Module APIplus
    Public Function GetBitmap(ByVal hwnd As Integer) As Bitmap
        Dim r As API.RECT
        API.GetWindowRect(hwnd, r)

        Dim r2 As API.RECT
        API.GetClientRect(hwnd, r2)
        Dim rborder As Integer = ((r.Right - r.Left) - r2.Right) / 2
        Dim rtitle As Integer = ((r.Bottom - r.Top) - r2.Bottom) - rborder

        Dim BitWidth As Integer = r.Right - r.Left - rborder * 2
        Dim BitHeight As Integer = r.Bottom - r.Top - rborder - rtitle

        Dim Bhandle, DestDC, SourceDC As IntPtr
        SourceDC = API.GetDC(hwnd)
        DestDC = API.CreateCompatibleDC(SourceDC)

        Bhandle = API.CreateCompatibleBitmap(SourceDC, BitWidth, BitHeight)
        API.SelectObject(DestDC, Bhandle)
        API.BitBlt(DestDC, 0, 0, BitWidth, BitHeight, SourceDC, 0, 0, &HCC0020)

        Dim bmp As Bitmap = Image.FromHbitmap(Bhandle)

        API.ReleaseDC(hwnd, SourceDC)
        API.DeleteDC(DestDC)
        API.DeleteObject(Bhandle)
        GC.Collect()

        Return bmp
    End Function
End Module