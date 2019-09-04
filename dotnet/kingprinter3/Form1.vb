Public Class Form1
    Dim maskPicturebox As MouseMask
    Dim screenshot As Bitmap

    Dim version As String = "3"
    Dim root As String

    Dim database As String = ""
    Dim topic As String = ""

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        root = Application.StartupPath & "\kingprinter" + version
        If Not FileIO.DirectoryExists(root) Then
            FileIO.CreateDirectory(root)
        End If

        RegisterHotKey(Me.Handle, 0, 0, Keys.PrintScreen)

        RegisterHotKey(Me.Handle, 1, 0, Keys.Z)
        RegisterHotKey(Me.Handle, 2, 0, Keys.X)
        RegisterHotKey(Me.Handle, 3, 0, Keys.C)
        RegisterHotKey(Me.Handle, 4, 0, Keys.V)
        RegisterHotKey(Me.Handle, 5, 0, Keys.B)

        RegisterHotKey(Me.Handle, 6, 0, Keys.A)
        RegisterHotKey(Me.Handle, 7, 0, Keys.S)
        RegisterHotKey(Me.Handle, 8, 0, Keys.D)

        maskPicturebox = New MouseMask(PictureBox1)
        CheckBox1.SendToBack()
        CheckBox2.SendToBack()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    PrintScreen()
                Case Else
                    Dim key As Integer = id
                    temp(key)
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        For i = 0 To 10
            UnregisterHotKey(Me.Handle, i)
        Next
    End Sub

    Sub PrintScreen()
        maskPicturebox.Clear()
        screenshot = APIplus.GetBitmap(API.GetForegroundWindow)
        PictureBox1.Image = screenshot
        API.OpenIcon(Me.Handle)
        Me.Activate()
    End Sub

    Function CopyBitmap(source As Bitmap, part As Rectangle) As Bitmap
        Dim bmp As New Bitmap(part.Width, part.Height)

        Dim g As Graphics = Graphics.FromImage(bmp)
        g.DrawImage(source, 0, 0, part, GraphicsUnit.Pixel)
        g.Dispose()

        Return bmp
    End Function

    Sub Save(mode As String)
        Dim r As Rectangle = maskPicturebox.GetRect
        Dim canSave As Boolean = True

        If IsNothing(PictureBox1.Image) Then
            canSave = False
            MsgBox("沒有畫面")
        End If

        If Not (r.X <> -1 And r.Y <> -1 And r.Width <> -1 And r.Height <> -1) Then
            canSave = False
            MsgBox("請選擇範圍")
        End If

        If canSave Then
            SelectPath(mode)
        End If
    End Sub

    Sub SelectPath(mode As String)
        Try
            Dim path As String
            Dim sfd As New System.Windows.Forms.SaveFileDialog

            If CheckBox1.Checked = True Then
                If database = "" Then
                    sfd.InitialDirectory = root
                Else
                    If FileIO.DirectoryExists(root & "\" & database) Then
                        If CheckBox2.Checked = True Then
                            If topic = "" Then
                                sfd.InitialDirectory = root & "\" & database
                            Else
                                sfd.InitialDirectory = root & "\" & database & "\" & topic
                            End If
                        Else
                            sfd.InitialDirectory = root & "\" & database
                            topic = ""
                        End If
                    Else
                        sfd.InitialDirectory = root
                        database = ""
                    End If
                End If
            Else
                sfd.InitialDirectory = root
            End If
            sfd.Title = "Save"
            sfd.Filter = "Image|*"
            sfd.FileName = mode

            If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                path = sfd.FileName
                FileIO.MakeSureExt(path, "bmp")

                Dim bmp As Bitmap = CopyBitmap(PictureBox1.Image, maskPicturebox.GetRect)
                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Bmp)
                maskPicturebox.Clear()

                If CheckBox1.Checked = True Then
                    path = FileIO.RemoveRoot(root, path)
                    database = FileIO.GetRoot(path)
                    If CheckBox2.Checked = True Then
                        path = FileIO.RemoveRoot(database, path)
                        topic = FileIO.GetRoot(path)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub temp(key As Integer)
        If API.GetForegroundWindow = Me.Handle Then
            If key >= 1 And key <= 5 Then
                Save("Ans" + key.ToString)
            ElseIf key = 6 Then
                Save("Tpc")
            ElseIf key = 7 Then
                Save("Img")
            ElseIf key = 8 Then
                Save("correctAns")
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Tpc
        Save("Tpc")
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'Img
        Save("Img")
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'Ans1
        Save("Ans1")
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        'Ans2
        Save("Ans2")
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        'Ans3
        Save("Ans3")
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        'Ans4
        Save("Ans4")
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        'Ans5
        Save("Ans5")
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        'correctAns
        Save("correctAns")
    End Sub

    Dim ch2m As Boolean = True
    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            ch2m = CheckBox2.Checked
            CheckBox2.Enabled = False
            CheckBox2.Checked = False
        Else
            CheckBox2.Enabled = True
            CheckBox2.Checked = ch2m
        End If
    End Sub
End Class

Module FileIO
    Public Function DirectoryExists(Path As String) As Boolean
        Return System.IO.Directory.Exists(Path)
    End Function

    Public Sub CreateDirectory(Path As String)
        System.IO.Directory.CreateDirectory(Path)
    End Sub

    Public Function RemoveRoot(Root As String, Path As String) As String
        If Path.IndexOf(Root) = 0 Then
            Return Microsoft.VisualBasic.Right(Path, Path.Length - Root.Length)
        Else
            Return Path
        End If
    End Function

    Public Function GetRoot(Path As String) As String
        If Path.IndexOf("\") = 0 Then
            Path = Microsoft.VisualBasic.Right(Path, Path.Length - 1)
        End If

        Dim exp() As String = Path.Split("\")
        Return exp(0)
    End Function

    Public Sub MakeSureExt(ByRef Path As String, Ext As String)
        If Path.IndexOf(Ext, Path.Length - Ext.Length) < 0 Then
            Path &= "." & Ext
        End If
    End Sub
End Module

Module API
    Public Const WM_HOTKEY As Integer = &H312
    Public Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Public Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Public Declare Function GetForegroundWindow Lib "user32" () As Integer
    Public Declare Function OpenIcon Lib "user32" (hwnd As Integer) As Boolean

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

    Public Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As IntPtr)
    Public Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer
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

    Public Sub PressKey(key As Integer)
        API.keybd_event(key, API.MapVirtualKey(key, 0), 0, 0)
        API.keybd_event(key, API.MapVirtualKey(key, 0), &H2, 0)
    End Sub
End Module

Public Class MouseMask
    Public Sub New(ByRef Target As PictureBox)
        AddHandler Target.MouseDown, AddressOf _MouseDown
        AddHandler Target.MouseMove, AddressOf _MouseMove
        AddHandler Target.MouseUp, AddressOf _MouseUp
        _Target = Target
    End Sub

    Public Sub SetBorder(Color As Color, Width As Integer)
        BorderBrush = New Pen(Color, Width)
    End Sub

    Public Sub SetMask(Color As Color, Opacity As Double)
        MaskBrush = New SolidBrush(Color.FromArgb(CInt(255 * Opacity), Color))
    End Sub

    Public Sub Clear()
        downpoint = New Point(-1, -1)
        uppoint = New Point(-1, -1)
        _Target.Refresh()
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

    Private _Target As PictureBox
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