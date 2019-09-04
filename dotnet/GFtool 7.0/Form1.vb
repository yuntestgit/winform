Public Class Form1
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

    Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (hwnd As Integer, lpString As String) As Integer


    Dim hwnd As Integer = -1
    Dim img As Bitmap = My.Resources.shoot

    Dim working As Boolean = False

    Dim index As Integer = 15
    Dim check(index) As CheckBox
    Dim checktext(index) As String
    Dim key(index) As Integer

    Sub newControl()
        For i = 1 To 9
            checktext(i - 1) = i.ToString
            key(i - 1) = Keys.D1 + i - 1
        Next
        checktext(9) = "0" : key(9) = Keys.D0
        checktext(10) = "-" : key(10) = 189 '-
        checktext(11) = "=" : key(11) = 187 '=
        checktext(12) = "Space" : key(12) = Keys.Space

        For i = 0 To index - 3
            check(i) = New CheckBox
            With check(i)
                .Width = 60
                .Text = checktext(i)
                .Left = 15
                .Top = 15 + i * 24
            End With
            GroupBox2.Controls.Add(check(i))
        Next

        checktext(13) = "Alt Pause"
        check(13) = New CheckBox
        With check(13)
            .Width = 90
            .Text = checktext(13)
            .Left = 80
            .Top = 15
        End With
        GroupBox2.Controls.Add(check(13))

        checktext(14) = "Ctrl Pause"
        check(14) = New CheckBox
        With check(14)
            .Width = 90
            .Text = checktext(14)
            .Left = 80
            .Top = 39
        End With
        GroupBox2.Controls.Add(check(14))

        checktext(15) = "Image Buff"
        check(15) = New CheckBox
        With check(15)
            .Width = 90
            .Text = checktext(15)
            .Left = 80
            .Top = 87
        End With
        GroupBox2.Controls.Add(check(15))
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        newControl()

        RegisterHotKey(Me.Handle, 0, 0, 192) '`
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    If working Then
                        working = False
                    Else
                        working = True
                    End If
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 0)
    End Sub

    Sub PressKey(key As Integer)
        keybd_event(key, MapVirtualKey(key, 0), 0, 0)
        System.Threading.Thread.Sleep(10)
        keybd_event(key, MapVirtualKey(key, 0), &H2, 0)
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
                SetWindowText(hwnd, "Grand Fantasia #Locked#")
            End If
        End If

        If IsWindowVisible(hwnd) Then
            PictureBox1.Image = GetBitmap(hwnd)
            If PictureBox1.Visible = False Then
                PictureBox1.Visible = True
            End If
        Else
            If Not IsWindow(hwnd) Then
                hwnd = -1
            End If
            If PictureBox1.Visible = True Then
                PictureBox1.Visible = False
            End If
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        If working Then
            If GetForegroundWindow = hwnd Then
                Dim canPress As Boolean = True
                If check(13).Checked Then
                    If GetAsyncKeyState(Keys.LMenu) <> 0 Then
                        canPress = False
                    End If
                    If GetAsyncKeyState(Keys.RMenu) <> 0 Then
                        canPress = False
                    End If
                End If

                If check(14).Checked Then
                    If GetAsyncKeyState(Keys.LControlKey) <> 0 Then
                        canPress = False
                    End If
                    If GetAsyncKeyState(Keys.RControlKey) <> 0 Then
                        canPress = False
                    End If
                End If

                If canPress Then
                    For i = 0 To index - 3
                        If check(i).Checked Then
                            PressKey(key(i))
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        If working Then
            If IsWindow(hwnd) Then
                If check(15).Checked Then
                    If GetForegroundWindow = hwnd Then
                        Dim rp As Point = SearchBitmap(GetBitmap(hwnd), img, 522, 730)
                        If rp.X <> -1 And rp.Y <> -1 Then
                            Timer2.Stop()
                            PressKey(187)
                            Timer2.Start()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

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

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        '638, 382
        '207, 382
        If Button1.Text = "Display" Then
            Button1.Text = "Hide"
            Me.Width = 638
        Else
            Button1.Text = "Display"
            Me.Width = 207
        End If
    End Sub
End Class
