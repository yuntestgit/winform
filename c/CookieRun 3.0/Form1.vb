Public Class Form1

#Region "Findwindow"
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
#End Region

#Region "SetParent"
    Public Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As Int32, ByVal hWndNewParent As Int32) As Boolean
    Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
#End Region

#Region "Image"
    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

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
            Return Image.FromFile("C:\Users\yun\Desktop\_printed\nothing.png")
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

#Region "Message"
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

#Region "Window Control"
    Private Sub detect_Tick(sender As System.Object, e As System.EventArgs) Handles detect.Tick
        Dim hwnd As Integer = FindWindow("QWidget", vbNullString)
        If hwnd Then
            Dim sameClass1 As Integer = FindWindow("QWidget", "player")
            Dim sameClass2 As Integer = FindWindow("QWidget", "Genymotion for personal use")
            If hwnd <> sameClass1 And hwnd <> sameClass2 Then
                Button1.Enabled = True
            Else
                Button1.Enabled = False
            End If
        Else
            Button1.Enabled = False
        End If

        hwnd = FindWindow(vbNullString, "Program Manager")
        hwnd = FindWindowEx(hwnd, 0, "QWidget", vbNullString)
        If hwnd Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Desk
        Dim parentHWND As Integer = FindWindow(vbNullString, "Program Manager")
        Dim childHWND As Integer = FindWindow("QWidget", vbNullString)
        SetParent(childHWND, parentHWND)
        SetWindowPos(childHWND, 0, 10, 66, 1068, 602, 4)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'Normal
        Dim parentHWND As Integer = 0
        Dim childHWND As Integer = FindWindow(vbNullString, "Program Manager") : childHWND = FindWindowEx(childHWND, 0, "QWidget", vbNullString)
        SetParent(childHWND, parentHWND)
        SetWindowPos(childHWND, 0, -1270, 66, 1068, 602, 4)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'Position
        Dim hwnd As Integer = FindWindow(vbNullString, "Program Manager")
        hwnd = FindWindowEx(hwnd, 0, "QWidget", vbNullString)
        If hwnd Then
            SetWindowPos(hwnd, 0, 10, 66, 1068, 602, 4)
        Else
            hwnd = FindWindow("QWidget", vbNullString)
            SetWindowPos(hwnd, 0, -1270, 66, 1068, 602, 4)
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        'Close
        Me.Close()
    End Sub
#End Region

#Region "Basic"
    Dim h As Integer
    Dim bmp As Bitmap

    Dim index As Integer = 50
    Dim img(index) As Bitmap
    Dim pt(index) As Point
    Dim total As Integer

    Function sb(bindex As Integer) As Boolean
        Try
            Dim rp As Point = SearchBitmap(bmp, img(bindex), pt(bindex).X, pt(bindex).Y)
            If rp.X = -1 And rp.Y = -1 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Sub sendclick(cindex As Integer)
        Dim cp As Point = New Point(img(cindex).Width / 2 + pt(cindex).X, img(cindex).Height / 2 + pt(cindex).Y)
        PostMessage(h, WM_LBUTTON_DOWN, 0, MAKELPARAM(cp.X, cp.Y))
        PostMessage(h, WM_LBUTTON_UP, 0, MAKELPARAM(cp.X, cp.Y))
    End Sub
#End Region

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim parentHWND As Integer = FindWindow(vbNullString, "Program Manager")
        SetParent(Me.Handle, parentHWND)
        SetWindowPos(Me.Handle, 0, 10, 688, 300, 300, 4)

        Dim tmp As Integer = 0
        '0heart 0
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\5-0.png") : pt(tmp) = New Point(662, 20) : tmp += 1

        'basic 1-4
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\2-0.png") : pt(tmp) = New Point(305, 486) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\3-0.png") : pt(tmp) = New Point(406, 490) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\4-0.png") : pt(tmp) = New Point(442, 488) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\w-0.png") : pt(tmp) = New Point(307, 489) : tmp += 1

        'aera1 5-6
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\0-1.png") : pt(tmp) = New Point(655, 498) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\1-0.png") : pt(tmp) = New Point(609, 504) : tmp += 1

        'area2 7-8
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\j-0.png") : pt(tmp) = New Point(653, 496) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\k-0.png") : pt(tmp) = New Point(606, 501) : tmp += 1

        'area3 9-10
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\zn-0.png") : pt(tmp) = New Point(653, 497) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\zo-0.png") : pt(tmp) = New Point(607, 503) : tmp += 1

        'area4 11-12
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\zd-0.png") : pt(tmp) = New Point(652, 497) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\ze-0.png") : pt(tmp) = New Point(607, 502) : tmp += 1

        'accident 13-28
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\9-1.png") : pt(tmp) = New Point(828, 77) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\a-1.png") : pt(tmp) = New Point(443, 342) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\f-0.png") : pt(tmp) = New Point(444, 341) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\g-0.png") : pt(tmp) = New Point(438, 341) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\h-0.png") : pt(tmp) = New Point(443, 484) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\i-1.png") : pt(tmp) = New Point(437, 442) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\e-0.png") : pt(tmp) = New Point(445, 494) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\n-0.png") : pt(tmp) = New Point(533, 456) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\c-0.png") : pt(tmp) = New Point(441, 496) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\d-0.png") : pt(tmp) = New Point(441, 429) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\o-0.png") : pt(tmp) = New Point(833, 31) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\u-0.png") : pt(tmp) = New Point(834, 33) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\zh-0.png") : pt(tmp) = New Point(446, 492) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\zi-0.png") : pt(tmp) = New Point(444, 342) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\zk-0.png") : pt(tmp) = New Point(442, 345) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\zj-0.png") : pt(tmp) = New Point(448, 488) : tmp += 1
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\zr-0.png") : pt(tmp) = New Point(408, 492) : tmp += 1
        total = 29

        'receive total+1-+3 +4-+7 +8-+9
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\q-0.png") : pt(tmp) = New Point(377, 232) : tmp += 1 '沒有可接收text
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\q-1.png") : pt(tmp) = New Point(444, 342) : tmp += 1 '沒有可接收 btn
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\6-1.png") : pt(tmp) = New Point(505, 80) : tmp += 1 'exit btn

        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\7-0.png") : pt(tmp) = New Point(563, 347) : tmp += 1 '確定傳送 btn
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\p-0.png") : pt(tmp) = New Point(230, 235) : tmp += 1 '全部傳送btn
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\6-0.png") : pt(tmp) = New Point(235, 344) : tmp += 1 '全部傳送btn
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\8-1.png") : pt(tmp) = New Point(439, 340) : tmp += 1 '傳送段落完成 btn

        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\v-0.png") : pt(tmp) = New Point(485, 524) : tmp += 1 'mail btn
        img(tmp) = Image.FromFile("C:\Users\yun\Desktop\_printed\0-2.png") : pt(tmp) = New Point(469, 537) : tmp += 1 'event btn
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        'Start/Stop
        If Button5.Text = "Start" Then
            Button5.Text = "Stop"
            Timer1.Start()
        ElseIf Button5.Text = "Stop" Then
            Button5.Text = "Start"
            Timer1.Stop()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        h = FindWindow(vbNullString, "Program Manager")
        h = FindWindowEx(h, 0, "QWidget", vbNullString)

        Try
            bmp = PrintWindow(h)

            For i = 1 To total
                If sb(i) Then
                    sendclick(i)
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class
