Public Class Form1

#Region "Findwindow"
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
#End Region

#Region "RECT"
    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure
#End Region

#Region "Postmessage"
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Public Const WM_MOUSE_MOVE = &H200
    Public Const WM_LBUTTON_DOWN = &H201
    Public Const WM_LBUTTON_UP = &H202
    Public Function MAKELPARAM(ByVal l As Integer, ByVal h As Integer) As Integer
        Dim r As Integer = l + (h << 16)
        Return r
    End Function
#End Region

#Region "Image"
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
            Return Nothing
        End Try
    End Function

    Public Function SearchBitmap(mainBmp As Bitmap, childBmp As Bitmap, Optional ByVal LocationX As Integer = -1, Optional ByVal LocationY As Integer = -1) As Point
        Try
            Dim c0 As Color = childBmp.GetPixel(0, 0)
            Dim rp As Point = New Point(-1, -1)
            If LocationX = -1 And LocationY = -1 Then
                Dim endsearch As Boolean = False
                For i = 0 To mainBmp.Width - 1
                    For j = 0 To mainBmp.Height - 1
                        If i + childBmp.Width < mainBmp.Width And j + childBmp.Height < mainBmp.Height Then
                            If mainBmp.GetPixel(i, j) = c0 Then
                                Dim isequal As Boolean = True
                                For i2 = 1 To childBmp.Width - 1
                                    For j2 = 1 To childBmp.Height - 1
                                        If mainBmp.GetPixel(i + i2, j + j2) <> childBmp.GetPixel(i2, j2) Then
                                            isequal = False
                                            Exit For
                                        End If
                                    Next
                                    If isequal = False Then
                                        Exit For
                                    End If
                                Next
                                If isequal Then
                                    rp = New Point(i, j)
                                    endsearch = True
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    If endsearch Then
                        Exit For
                    End If
                Next
            Else
                If LocationX + childBmp.Width < mainBmp.Width And LocationY + childBmp.Height < mainBmp.Height Then
                    Dim isequal As Boolean = True
                    For i = 0 To childBmp.Width - 1
                        For j = 0 To childBmp.Height - 1
                            If mainBmp.GetPixel(i + LocationX, j + LocationY) <> childBmp.GetPixel(i, j) Then
                                isequal = False
                                Exit For
                            End If
                        Next
                        If isequal = False Then
                            Exit For
                        End If
                    Next
                    If isequal Then
                        rp = New Point(LocationX, LocationY)
                    End If
                End If
            End If
            Return rp
        Catch ex As Exception
            Return New Point(-1, -1)
        End Try
    End Function
#End Region

    Dim h As Integer
    Dim bmp As Bitmap

    Dim index As Integer = 20
    Dim img(index) As Bitmap
    Dim pt(index) As Point
    Dim total As Integer

    Function sb(bindex As Integer) As Boolean
        Dim rp As Point = SearchBitmap(bmp, img(bindex), pt(bindex).X, pt(bindex).Y)
        If rp.X = -1 And rp.Y = -1 Then
            Return False
        Else
            Return True
        End If
    End Function

    Sub sendclick(cindex As Integer)
        Dim cp As Point = New Point(img(cindex).Width / 2 + pt(cindex).X, img(cindex).Height / 2 + pt(cindex).Y)
        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(cp.X, cp.Y))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(cp.X, cp.Y))
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        h = FindWindow("QWidget", vbNullString)
        Me.Top = 0
        Me.Left = 0

        Dim tmp As Integer = 0
        '0heart 0
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\5-0.png") : pt(tmp) = New Point(662, 20) : tmp += 1

        'basic 1-3
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\2-0.png") : pt(tmp) = New Point(305, 486) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\3-0.png") : pt(tmp) = New Point(406, 490) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\4-0.png") : pt(tmp) = New Point(442, 488) : tmp += 1

        'aera1 4-5
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\0-1.png") : pt(tmp) = New Point(655, 498) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\1-0.png") : pt(tmp) = New Point(609, 504) : tmp += 1

        'area2 6-7
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\j-0.png") : pt(tmp) = New Point(653, 496) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\k-0.png") : pt(tmp) = New Point(606, 501) : tmp += 1

        'accident 8-15
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\9-1.png") : pt(tmp) = New Point(828, 77) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\a-1.png") : pt(tmp) = New Point(443, 342) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\f-0.png") : pt(tmp) = New Point(444, 341) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\g-0.png") : pt(tmp) = New Point(438, 341) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\h-0.png") : pt(tmp) = New Point(443, 484) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\i-1.png") : pt(tmp) = New Point(437, 442) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\e-0.png") : pt(tmp) = New Point(445, 494) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\n-0.png") : pt(tmp) = New Point(533, 456) : tmp += 1
        '
        'img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\m-0.png") : pt(tmp) = New Point(459, 228) : tmp += 1
        'img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\m-0.png") : pt(tmp) = New Point(-9, -31) : tmp += 1

        total = 15
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Button2.Enabled = True
        Timer1.Start()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Button1.Enabled = True
        Button2.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If h Then
            bmp = PrintWindow(h)

            If Not sb(0) Then
                For i = 1 To total
                    If sb(i) Then
                        sendclick(i)
                        Exit For
                    End If
                Next
            End If
        Else
            h = FindWindow("QWidget", vbNullString)
        End If
    End Sub

    Dim gg As Integer = 0

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        gg += 1
        If gg = 600 Then
            Process.Start("C:\myProgram\dotnet\CookieRun 1.0\bin\Debug\CookieRun 1.0.exe")
            Me.Close()
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim WM_MOUSEWHEEL = &H20A
        Dim WHEEL_POS = &H780000
        Dim WHEEL_NEG = &HFF880000

        Dim a = CShort((MAKELPARAM(500, 600) >> 16)) - 1

        Dim h2 As Integer = FindWindow(vbNullString, "BlueStacks App Player")
        PostMessage(h2, WM_MOUSEWHEEL, a, MAKELPARAM(500, 600))
        'PostMessage(h2, WM_LBUTTON_DOWN, 0, MAKELPARAM(500, 400))
        'PostMessage(h2, WM_MOUSE_MOVE, 0, MAKELPARAM(500, 900))
        'PostMessage(h2, WM_LBUTTON_UP, 0, MAKELPARAM(500, 900))



        'Timer3.Start()
        'PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 228))
        'PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(430, 228))
        'PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 228))
        'PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(430, 228))

        'For i = 229 To 298
        '    PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 228))
        '    PostMessage(h, WM_MOUSE_MOVE, 0, MAKELPARAM(430, i))
        '    PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, i))
        'Next

        'PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 298))

        'PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(430, 298))
    End Sub

    'Public Declare Auto Function SetCursorPos Lib "User32.dll" (ByVal X As Integer, ByVal Y As Integer) As Integer
    'Public Declare Auto Function GetCursorPos Lib "User32.dll" (ByRef lpPoint As Point) As Integer
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    'Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)

    'Dim a, b As Point

    'Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
    '    SetCursorPos(a.X, a.Y)
    '    mouse_event(2, 0, 0, 0, 0)
    '    SetCursorPos(b.X, b.Y)
    '    'mouse_event(4, 0, 0, 0, 0)
    'End Sub



    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        'SendMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(430, 228))
        'SendMessage(h, WM_MOUSE_MOVE, 0, MAKELPARAM(430, 298))
        'SendMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 298))

        'If GetAsyncKeyState(Keys.F7) <> 0 Then
        '    Timer4.Start()
        'End If

        'If GetAsyncKeyState(Keys.F8) <> 0 Then
        '    Timer4.Stop()
        'End If
    End Sub

    Dim mvy As Integer = 228

    Private Sub Timer4_Tick(sender As System.Object, e As System.EventArgs) Handles Timer4.Tick
        If mvy = 228 Then
            PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(430, 228))
            PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 228))

            'PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 228))
        ElseIf mvy = 298 Then

            PostMessage(h, WM_MOUSE_MOVE, 0, MAKELPARAM(430, 298))
            PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(430, 298))

            PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 298))
            PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(430, 298))
            PostMessage(h, WM_MOUSE_MOVE, 0, MAKELPARAM(430, 298))

            'PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 298))
            'PostMessage(h, WM_MOUSE_MOVE, 0, MAKELPARAM(430, 298))
            'PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(430, 298))

            'PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(430, 298))
            'mvy = 228
            Timer4.Stop()
        Else
            PostMessage(h, WM_MOUSE_MOVE, 0, MAKELPARAM(430, mvy))
        End If
        mvy += 1
    End Sub

    Private Sub Timer5_Tick(sender As System.Object, e As System.EventArgs) Handles Timer5.Tick
        If GetAsyncKeyState(Keys.F7) <> 0 Then
            Timer6.Start()
            Timer7.Start()
            'Timer8.Start()
        End If

        If GetAsyncKeyState(Keys.F8) <> 0 Then
            Timer6.Stop()
            Timer7.Stop()
            'Timer8.Stop()
        End If
    End Sub

    Private Sub Timer6_Tick(sender As System.Object, e As System.EventArgs) Handles Timer6.Tick
        ' h = FindWindow("QWidget", vbNullString)

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(200, 200))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(200, 200))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(200, 300))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(200, 300))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(200, 400))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(200, 400))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(200, 500))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(200, 500))
    End Sub

    Private Sub Timer7_Tick(sender As System.Object, e As System.EventArgs) Handles Timer7.Tick

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(100, 200))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(100, 200))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(100, 300))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(100, 300))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(100, 400))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(100, 400))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(100, 500))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(100, 500))
    End Sub

    Private Sub Timer8_Tick(sender As System.Object, e As System.EventArgs) Handles Timer8.Tick
        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(300, 100))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(300, 100))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(300, 200))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(300, 200))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(100, 300))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(300, 300))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(300, 400))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(300, 400))

        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(300, 500))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(300, 500))
    End Sub
End Class
