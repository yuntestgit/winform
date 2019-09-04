Public Class Form1
    Dim maskPicturebox As MouseMask
    Dim hwnd As Integer
    Dim root As String

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not isdir(Application.StartupPath & "\kingprinter") Then
            createdir(Application.StartupPath & "\kingprinter")
        End If
        root = Application.StartupPath & "\kingprinter"
        RegisterHotKey(Me.Handle, 0, 0, Keys.PrintScreen)
        maskPicturebox = New MouseMask(PictureBox1)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    hwnd = API.GetForegroundWindow
                    PictureBox1.Tag = ""
                    maskPicturebox = New MouseMask(PictureBox1)
                    PictureBox1.Image = APIplus.GetBitmap(hwnd)
                    Me.Activate()
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 0)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        '題目
        If PictureBox1.Tag = "null" Then
            MsgBox("尚未綁定視窗")
        End If

        If Label3.Text = "" Then
            MsgBox("尚未設定題庫")
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        '圖例

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        '選項

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        '下一題
        If hwnd Then
            maskPicturebox = New MouseMask(PictureBox1)
            PictureBox1.Image = APIplus.GetBitmap(hwnd)
        Else
            MsgBox("尚未綁定視窗")
        End If
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        '設定題庫
        Dim str As String = InputBox("設定題庫")
        If str <> "" Then
            Label3.Text = str
            If Not isdir(root & "\" & str) Then
                createdir(root & "\" & str)
            End If
        End If
    End Sub

    Sub islocked()

    End Sub

    Sub ispathed()

    End Sub

    Function isdir(path As String)
        Return System.IO.Directory.Exists(path)
    End Function

    Sub createdir(path As String)
        System.IO.Directory.CreateDirectory(path)
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

Public Class MouseMask
    Public Sub New(ByRef Target As PictureBox)
        AddHandler Target.MouseDown, AddressOf _MouseDown
        AddHandler Target.MouseMove, AddressOf _MouseMove
        AddHandler Target.MouseUp, AddressOf _MouseUp
    End Sub

    Public Sub Clear(ByRef Target As PictureBox)
        downpoint = New Point(-1, -1)
        uppoint = New Point(-1, -1)
        Target.Refresh()
    End Sub

    Public Sub SetBorder(Color As Color, Width As Integer)
        BorderBrush = New Pen(Color, Width)
    End Sub

    Public Sub SetMask(Color As Color, Opacity As Double)
        MaskBrush = New SolidBrush(Color.FromArgb(CInt(255 * Opacity), Color))
    End Sub

    Public Function GetRect() As Rectangle
        If downpoint.X <> uppoint.X Or downpoint.Y <> uppoint.Y Then
            Dim x, y, width, height As Integer

            If downpoint.X >= uppoint.X Then
                x = uppoint.X
            Else
                x = downpoint.X
            End If

            If downpoint.Y >= uppoint.Y Then
                y = uppoint.Y
            Else
                y = downpoint.Y
            End If

            width = Math.Abs(downpoint.X - uppoint.X)
            height = Math.Abs(downpoint.Y - uppoint.Y)
            Return New Rectangle(x, y, width, height)
        Else
            Return New Rectangle(-1, -1, -1, -1)
        End If
    End Function

    Private MaskBrush As SolidBrush = New SolidBrush(Color.FromArgb(200, Color.Black))
    Private BorderBrush As Pen = New Pen(Color.Red, 1)

    Private down As Boolean = False
    Private downpoint As Point
    Private uppoint As Point
    Private nowpoint As Point

    Private Sub _MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        sender.Refresh()
        downpoint = New Point(e.X, e.Y)
        down = True
    End Sub

    Private Sub _MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        If down = True Then
            sender.Refresh()
            nowpoint = New Point(e.X, e.Y)

            Dim g As Graphics = sender.CreateGraphics

            Dim x, y, width, height As Integer

            If downpoint.X >= nowpoint.X Then
                x = nowpoint.X
            Else
                x = downpoint.X
            End If

            If downpoint.Y >= nowpoint.Y Then
                y = nowpoint.Y
            Else
                y = downpoint.Y
            End If

            width = Math.Abs(downpoint.X - nowpoint.X)
            height = Math.Abs(downpoint.Y - nowpoint.Y)

            g.DrawRectangle(BorderBrush, x, y, width, height)
        End If
    End Sub

    Private Sub _MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        uppoint = New Point(e.X, e.Y)
        If downpoint.X <> uppoint.X Or downpoint.Y <> uppoint.Y Then
            Dim g As Graphics = sender.CreateGraphics

            Dim x, y, width, height As Integer

            If downpoint.X >= uppoint.X Then
                x = uppoint.X
            Else
                x = downpoint.X
            End If

            If downpoint.Y >= uppoint.Y Then
                y = uppoint.Y
            Else
                y = downpoint.Y
            End If

            width = Math.Abs(downpoint.X - uppoint.X)
            height = Math.Abs(downpoint.Y - uppoint.Y)

            Dim p As New System.Drawing.Drawing2D.GraphicsPath

            p.AddRectangle(New Rectangle(0, 0, sender.Width, sender.Height))
            p.AddRectangle(New Rectangle(x, y, width, height))

            g.FillPath(MaskBrush, p)
            g.DrawRectangle(BorderBrush, x, y, width, height)
        End If
        down = False
    End Sub
End Class