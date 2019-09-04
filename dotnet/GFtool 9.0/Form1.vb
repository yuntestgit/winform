Public Class Form1

#Region "API"
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer
    Declare Function IsWindow Lib "user32" (hwnd As Integer) As Boolean
    Declare Function IsWindowEnabled Lib "user32" (hwnd As Integer) As Boolean
    Declare Function IsWindowVisible Lib "user32" (hwnd As Integer) As Boolean
    Declare Function GetForegroundWindow Lib "user32" () As Integer

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Declare Function SelectObject Lib "gdi32" (ByVal hdc As Integer, ByVal hObject As Integer) As Integer
    Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer
    Declare Function CreateCompatibleBitmap Lib "gdi32" (ByVal hdc As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    Declare Function CreateDC Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, ByRef lpInitData As Integer) As Integer
    Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hdc As Integer) As Integer
    Declare Function GetDC Lib "user32" Alias "GetDC" (ByVal hwnd As Integer) As Integer
    Private Declare Function DeleteDC Lib "GDI32" (ByVal hDC As Integer) As Integer
    Private Declare Function DeleteObject Lib "GDI32" (ByVal hObj As Integer) As Integer
    Public Declare Function ReleaseDC Lib "user32.dll" (ByVal hWnd As Integer, ByVal prmlngHDc As Integer) As Integer

    Public Const WM_HOTKEY As Integer = &H312
    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As IntPtr)
    Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer

    Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)
    Public Declare Function SetCursorPos Lib "user32" (ByVal x As Integer, ByVal y As Integer) As Boolean

    Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (hwnd As Integer, lpString As String) As Integer

    Public Declare Function ReleaseCapture Lib "user32" Alias "ReleaseCapture" () As Boolean
    Public Declare Function MoveWindow Lib "user32" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean

    Public Const WM_SYSCOMMAND = &H112
    Public Const SC_MOVE = &HF010&
    Public Const HTCAPTION = &H2
    Public Const SRCCOPY = &HCC0020
#End Region

#Region "Function"
    Function GetBitmap(ByVal hwnd As Integer) As Bitmap
        Dim r As RECT
        GetWindowRect(hwnd, r)

        Dim r2 As RECT
        GetClientRect(hwnd, r2)
        Dim rborder As Integer = ((r.Right - r.Left) - r2.Right) / 2
        Dim rtitle As Integer = ((r.Bottom - r.Top) - r2.Bottom) - rborder

        Dim BitWidth As Integer = r.Right - r.Left - rborder * 2
        Dim BitHeight As Integer = r.Bottom - r.Top - rborder - rtitle

        Dim Bhandle, DestDC, SourceDC As IntPtr
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

    Sub PressKey(key As Integer)
        keybd_event(key, MapVirtualKey(key, 0), 0, 0)
        System.Threading.Thread.Sleep(10)
        keybd_event(key, MapVirtualKey(key, 0), &H2, 0)
    End Sub
#End Region

    'onground 521, 729
    'shoot 522, 730
    'common 506, 373

    Dim working As Boolean = False
    Dim hwnd As Integer

    Dim img_shoot As Bitmap = My.Resources.shoot
    Dim img_onground As Bitmap = My.Resources.onground
    Dim img_common As Bitmap = My.Resources.common

    Dim img_fiona As Bitmap = My.Resources.fiona

    Dim p1index As Integer = 15
    Dim p1check(p1index) As CheckBox
    Dim p1checktext(p1index) As String
    Dim p1key(p1index) As Integer

    Dim p2index As Integer = 5
    Dim p2check(p2index) As CheckBox
    Dim p2checktext(p2index) As String
    Dim p2key(p2index) As Integer

    Sub newControl()
        p2checktext(0) = "攻速、施法"
        p2checktext(1) = "攻擊、魔攻"
        p2checktext(2) = "物防、魔防"
        p2checktext(3) = "補血"
        p2checktext(4) = "緩速"
        For i = 0 To 4
            p2check(i) = New CheckBox
            p2key(i) = Keys.D1 + i
            With p2check(i)
                .Width = 120
                .Text = p2checktext(i)
                .Left = 15
                .Top = 15 + i * 24
            End With
            TabPage2.Controls.Add(p2check(i))
        Next

        For i = 1 To 9
            p1checktext(i - 1) = i.ToString
            p1key(i - 1) = Keys.D1 + i - 1
        Next
        p1checktext(9) = "0" : p1key(9) = Keys.D0
        p1checktext(10) = "-" : p1key(10) = 189 '-
        p1checktext(11) = "=" : p1key(11) = 187 '=
        p1checktext(12) = "Space" : p1key(12) = Keys.Space

        For i = 0 To p1index - 3
            p1check(i) = New CheckBox
            With p1check(i)
                .Width = 60
                .Text = p1checktext(i)
                .Left = 15
                .Top = 15 + i * 24
            End With
            TabPage1.Controls.Add(p1check(i))
        Next

        p1checktext(13) = "Alt Pause"
        p1check(13) = New CheckBox
        With p1check(13)
            .Width = 90
            .Text = p1checktext(13)
            .Left = 80
            .Top = 15
        End With
        TabPage1.Controls.Add(p1check(13))

        p1checktext(14) = "Ctrl Pause"
        p1check(14) = New CheckBox
        With p1check(14)
            .Width = 90
            .Text = p1checktext(14)
            .Left = 80
            .Top = 39
        End With
        TabPage1.Controls.Add(p1check(14))

        p1checktext(15) = "Image Buff"
        p1check(15) = New CheckBox
        With p1check(15)
            .Width = 90
            .Text = p1checktext(15)
            .Left = 80
            .Top = 87
        End With
        TabPage1.Controls.Add(p1check(15))
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        newControl()

        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\LRkey.exe") Then
            Me.Top = 120
            Me.Left = 5
            p2check(0).Checked = True
            p2check(1).Checked = True
            TabControl1.SelectedIndex = 1
            Process.Start(Application.StartupPath & "\LRkey.exe")
            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\GF.lnk") Then
                Process.Start(Application.StartupPath & "\GF.lnk")
            End If
        End If

        RegisterHotKey(Me.Handle, 0, 0, 192) '`
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    If working = False Then
                        _Start()
                    Else
                        _Stop()
                    End If
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If IsWindow(hwnd) Then
            SetWindowText(hwnd, "Grand Fantasia")
        End If

        UnregisterHotKey(Me.Handle, 0)
    End Sub

    Sub _Start()
        TabControl1.Enabled = False
        If TabControl1.SelectedIndex = 0 Then
            Timer2.Start()
        ElseIf TabControl1.SelectedIndex = 1 Then
            Timer3.Start()
        End If
        SetWindowText(hwnd, "Grand Fantasia #Locked #Working")
        working = True
    End Sub

    Sub _Stop()
        suptime = 0
        TabControl1.Enabled = True
        Timer2.Stop()
        Timer3.Stop()
        SetWindowText(hwnd, "Grand Fantasia #Locked")
        working = False
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Keys.Home) <> 0 Then 'Grand Fantasia 14
            Dim temp As Integer = GetForegroundWindow
            Dim lpString As String = New String(Chr(0), 255)
            GetWindowText(temp, lpString, 255)
            Dim winText As String = Microsoft.VisualBasic.Left(lpString, 14)

            If winText = "Grand Fantasia" Then
                SetWindowText(hwnd, "Grand Fantasia")
                hwnd = temp
                If working Then
                    SetWindowText(hwnd, "Grand Fantasia #Locked #Working")
                Else
                    SetWindowText(hwnd, "Grand Fantasia #Locked")
                End If
            End If
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        '普通模式
        If GetForegroundWindow = hwnd Then
            If p1check(15).Checked Then
                Dim rp As Point = SearchBitmap(GetBitmap(hwnd), img_shoot, 522, 730)
                If rp.X <> -1 And rp.Y <> -1 Then
                    System.Threading.Thread.Sleep(10)
                    PressKey(187)
                    System.Threading.Thread.Sleep(10)
                End If
            End If

            Dim canPress As Boolean = True
            If p1check(13).Checked Then
                If GetAsyncKeyState(Keys.LMenu) <> 0 Then
                    canPress = False
                End If
                If GetAsyncKeyState(Keys.RMenu) <> 0 Then
                    canPress = False
                End If
            End If

            If p1check(14).Checked Then
                If GetAsyncKeyState(Keys.LControlKey) <> 0 Then
                    canPress = False
                End If
                If GetAsyncKeyState(Keys.RControlKey) <> 0 Then
                    canPress = False
                End If
            End If

            If canPress Then
                For i = 0 To p1index - 3
                    If p1check(i).Checked Then
                        PressKey(p1key(i))
                    End If
                Next
            End If
        End If
    End Sub

    Dim suptime As Integer = 0
    Dim delay As Double
    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        '戰坐模式
        If GetForegroundWindow = hwnd Then
            Dim rp As Point = SearchBitmap(GetBitmap(hwnd), img_onground, 521, 729) 'normal
            'Dim rp As Point = SearchBitmap(GetBitmap(hwnd), img_fiona, 521, 729)
            If rp.X <> -1 And rp.Y <> -1 Then
                If suptime = 0 Then
                    delay = Microsoft.VisualBasic.Timer
                    suptime = 1
                ElseIf suptime = 1 Then
                    If Microsoft.VisualBasic.Timer - delay > 0.7 Then
                        delay = Microsoft.VisualBasic.Timer
                        suptime = 2
                    Else
                        PressKey(Keys.Space) 'normal
                        'PressKey(189) 'fiona
                    End If
                ElseIf suptime = 2 Then
                    If Microsoft.VisualBasic.Timer - delay > 0.7 Then
                        delay = 0
                        suptime = 3
                    Else
                        PressKey(Keys.D6)
                    End If
                ElseIf suptime = 3 Then
                    Dim rp2 As Point = SearchBitmap(GetBitmap(hwnd), img_common, 506, 373)
                    If rp2.X <> -1 And rp2.Y <> -1 Then
                        Dim r As RECT
                        GetWindowRect(hwnd, r)

                        Dim r2 As RECT
                        GetClientRect(hwnd, r2)

                        Dim rborder As Integer = ((r.Right - r.Left) - r2.Right) / 2
                        Dim rtitle As Integer = ((r.Bottom - r.Top) - r2.Bottom) - rborder

                        Dim ckx, cky As Integer

                        ckx = r.Left + rborder + 506 - 40
                        cky = r.Top + rborder + rtitle + 373 + 50

                        SetCursorPos(ckx, cky)
                        System.Threading.Thread.Sleep(10)
                        mouse_event(2, 0, 0, 0, 0)
                        System.Threading.Thread.Sleep(10)
                        mouse_event(4, 0, 0, 0, 0)
                        System.Threading.Thread.Sleep(10)
                    End If
                End If
            Else
                suptime = 0
                For i = 0 To 4
                    If p2check(i).Checked Then
                        PressKey(p2key(i))
                    End If
                Next
            End If
        End If
    End Sub
End Class