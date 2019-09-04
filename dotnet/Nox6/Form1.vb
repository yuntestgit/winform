Public Class Form1

#Region "CreateButton"
    Dim SwitchButton0 As SwitchButton0
    Dim SwitchButton1 As SwitchButton1
    Dim KeyButton As KeyButton

    Sub CreateButton()
        SwitchButton0 = New SwitchButton0(Button6)
        SwitchButton1 = New SwitchButton1(Button6)
        SwitchButton0.Connect(SwitchButton1)
        SwitchButton1.Connect(SwitchButton0)
        AddHandler SwitchButton0.Click, AddressOf Switch0
        AddHandler SwitchButton1.Click, AddressOf Switch1
        KeyButton = New KeyButton(Button5)
        AddHandler KeyButton.KeyDown, AddressOf KeyButton_KeyDown
        Me.Controls.Add(SwitchButton0)
        Me.Controls.Add(SwitchButton1)
        Me.Controls.Add(KeyButton)
    End Sub

    Sub Switch0()
        Switch_Click(0)
    End Sub

    Sub Switch1()
        Switch_Click(1)
    End Sub
#End Region

    Const Path As String = "C:\Nox"

    Dim settings As New yun.Settings
    Dim drawMask As New yun.UserControl.Drawing
    Dim nox As New yun.Tool.Nox

    Dim bmpC() As yun.Settings.BitmapCollections
    Dim maskPicturebox As yun.UserControl.EventHandler.MouseMask


    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '1116, 628
        '316, 628
        Me.Width = 316
        Dim aeroform As New yun.UserControl.EventHandler.AeroForm(Me)
        Dim aerolist As New yun.UserControl.EventHandler.AeroListbox(ListBox1)
        maskPicturebox = New yun.UserControl.EventHandler.MouseMask(PictureBox1)

        CreateButton()

        settings.Load(Path & "\settings.txt", bmpC)
        ListboxUpdate()
    End Sub

    Sub Switch_Click(index As Integer)
        If index = 0 Then
            ListBox1.SelectedIndex = -1
            ListBox1.Enabled = False
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            KeyButton.Enabled = False
            TrackBar1.Enabled = False
            nox.Start(bmpC, 3000)
        Else
            ListBox1.SelectedIndex = -1
            ListBox1.Enabled = True
            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            KeyButton.Enabled = True
            TrackBar1.Enabled = True
            nox.Finish()
        End If
    End Sub

    Sub ListboxUpdate()
        ListBox1.Items.Clear()
        For i = 0 To bmpC.Length - 1
            ListBox1.Items.Add(bmpC(i).Path)
            'ListBox1.Items.Add("Picture" & (i + 1).ToString)
        Next
    End Sub

    Sub Save()
        settings.Save(Path & "\settings.txt", bmpC)
    End Sub

    Function UnlockLoad(LoadPath As String) As Image
        Dim rimg As Image
        Dim sr As System.IO.FileStream = New System.IO.FileStream(LoadPath, IO.FileMode.Open, IO.FileAccess.Read)
        rimg = Image.FromStream(sr)
        sr.Close()
        Return rimg
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        'Select Changed
        Dim index As Integer = ListBox1.SelectedIndex
        If index > -1 Then
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            KeyButton.Enabled = True
            TrackBar1.Enabled = True
            'image
            PictureBox1.Image = UnlockLoad(Path & "\" & bmpC(index).Path & ".bmp")

            'mask
            If bmpC(index).X <> -1 And bmpC(index).Y <> -1 And bmpC(index).Width <> -1 And bmpC(index).Height <> -1 Then
                drawMask.Mask(PictureBox1, bmpC(index).X, bmpC(index).Y, bmpC(index).Width, bmpC(index).Height)
            End If

            'offset
            TrackBar1.Value = bmpC(index).Offset

            'key
            KeyButton.LoadKey(bmpC(index).Key)

            'width
            Me.Width = 1116
        Else
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            KeyButton.Enabled = False
            TrackBar1.Enabled = False
            'image
            PictureBox1.Image = Nothing

            'mask
            PictureBox1.Refresh()

            'offset
            TrackBar1.Value = 0

            'key
            KeyButton.LoadKey(-1)

            'width
            Me.Width = 316
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'new bmpc
        nox.hwndRefresh()
        If nox.Running Then
            Dim bmp As Bitmap = nox.GetBitmap
            Dim time As New yun.Settings.Time
            Dim t As String = time.All
            Dim bmpPath As String = Path & "\" & t & ".bmp"
            Dim temp As New yun.Settings.BitmapCollections
            With temp
                .Path = t
                .X = -1
                .Y = -1
                .Width = -1
                .Height = -1
                .Offset = 1
                .Key = -1
            End With
            settings.Insert(bmpC, temp)
            Save()
            bmp.Save(bmpPath, System.Drawing.Imaging.ImageFormat.Bmp)
            ListboxUpdate()
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        'delete bmpc
        Dim index As Integer = ListBox1.SelectedIndex
        If index > -1 Then
            Dim deletePath As String = Path & "\" & bmpC(index).Path & ".bmp"
            My.Computer.FileSystem.DeleteFile(deletePath)
            settings.Delete(bmpC, index)
            Save()
            ListboxUpdate()
            If ListBox1.Items.Count <> 0 Then
                If index = ListBox1.Items.Count Then
                    ListBox1.SelectedIndex = index - 1
                Else
                    ListBox1.SelectedIndex = index
                End If
            Else
                Button2.Enabled = False
                Button3.Enabled = False
                Button4.Enabled = False
                KeyButton.Enabled = False
                TrackBar1.Enabled = False
                PictureBox1.Image = Nothing
                PictureBox1.Refresh()
                TrackBar1.Value = 0
                KeyButton.LoadKey(-1)
                Me.Width = 316
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'Listbox Itme Rise Up
        Dim index As Integer = ListBox1.SelectedIndex
        If index > 0 Then
            Dim temp As yun.Settings.BitmapCollections = bmpC(index)
            bmpC(index) = bmpC(index - 1)
            bmpC(index - 1) = temp
            ListboxUpdate()
            ListBox1.SelectedIndex = index - 1
            Save()
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'Listbox Itme Decline Down
        Dim index As Integer = ListBox1.SelectedIndex
        If index < bmpC.Length - 1 And index >= 0 Then
            Dim temp As yun.Settings.BitmapCollections = bmpC(index)
            bmpC(index) = bmpC(index + 1)
            bmpC(index + 1) = temp
            ListboxUpdate()
            ListBox1.SelectedIndex = index + 1
            Save()
        End If
    End Sub

    Private Sub Form1_Resize(sender As System.Object, e As System.EventArgs) Handles MyBase.Resize
        'redraw mask
        Dim index As Integer = ListBox1.SelectedIndex
        If index > -1 Then
            If bmpC(index).X <> -1 And bmpC(index).Y <> -1 And bmpC(index).Width <> -1 And bmpC(index).Height <> -1 Then
                drawMask.Mask(PictureBox1, bmpC(index).X, bmpC(index).Y, bmpC(index).Width, bmpC(index).Height)
            End If
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        'update mask
        Dim index As Integer = ListBox1.SelectedIndex
        If index > -1 Then
            Dim r As Rectangle = maskPicturebox.GetRect()
            bmpC(index).X = r.X
            bmpC(index).Y = r.Y
            bmpC(index).Width = r.Width
            bmpC(index).Height = r.Height
            Save()
        End If
    End Sub

    Private Sub TrackBar1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles TrackBar1.ValueChanged
        'update offset
        Dim index As Integer = ListBox1.SelectedIndex
        If index > -1 Then
            If bmpC(index).Offset <> TrackBar1.Value Then
                bmpC(index).Offset = TrackBar1.Value
                Save()
            End If
        End If
    End Sub

    Private Sub KeyButton_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        'update key
        Dim index As Integer = ListBox1.SelectedIndex
        If index > -1 Then
            If sender.BackColor = Color.Red Then
                bmpC(index).Key = e.KeyCode
                Save()
            End If
        End If
    End Sub
End Class

#Region "MyButton"
Public Class SwitchButton0
    Inherits Button

    Public Sub New(ByVal Target As Button)
        Me.Left = Target.Left
        Me.Top = Target.Top
        Me.Width = Target.Width
        Me.Height = Target.Height
        Me.Font = Target.Font
        Me.Tag = "Start"
        Me.BackColor = Color.Transparent
    End Sub

    Public Sub Connect(ByRef Target As SwitchButton1)
        AddHandler Target.Click, AddressOf Target_Click
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        Dim g As Graphics = Me.CreateGraphics
        g.DrawString(Me.Tag, Me.Font, Brushes.Black, New Point(45, 35))
    End Sub

    Private Sub _Click(sender As System.Object, e As System.EventArgs) Handles Me.Click
        Me.Visible = False
    End Sub

    Private Sub Target_Click(sender As System.Object, e As System.EventArgs)
        Me.Visible = True
        Me.Focus()
    End Sub
End Class

Public Class SwitchButton1
    Inherits Button

    Public Sub New(ByVal Target As Button)
        Me.Left = Target.Left
        Me.Top = Target.Top
        Me.Width = Target.Width
        Me.Height = Target.Height
        Me.Font = Target.Font
        Me.Tag = "Stop"
        Me.BackColor = Color.Transparent
    End Sub

    Public Sub Connect(ByRef Target As SwitchButton0)
        AddHandler Target.Click, AddressOf Target_Click
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        Dim g As Graphics = Me.CreateGraphics
        g.DrawString(Me.Tag, Me.Font, Brushes.Black, New Point(48, 35))
    End Sub

    Private Sub _Click(sender As System.Object, e As System.EventArgs) Handles Me.Click
        Me.Visible = False
    End Sub

    Private Sub Target_Click(sender As System.Object, e As System.EventArgs)
        Me.Visible = True
        Me.Focus()
    End Sub
End Class

Public Class KeyButton
    Inherits Button

    Public Sub New(ByVal Target As Button)
        Me.Left = Target.Left
        Me.Top = Target.Top
        Me.Width = Target.Width
        Me.Height = Target.Height
        Me.Font = Target.Font
        Me.ForeColor = Color.White
        Me.BackColor = Color.Black
        Me.Enabled = False
        Me.Tag = "-1"
        Me.Text = "Null"
        SetKey()
    End Sub

    Private Const total As Integer = 45
    Private KeyString(total) As String
    Private KeyValue(total) As Integer

    Private Sub SetKey()
        KeyString(0) = "A" : KeyValue(0) = Keys.A
        KeyString(1) = "B" : KeyValue(1) = Keys.B
        KeyString(2) = "C" : KeyValue(2) = Keys.C
        KeyString(3) = "D" : KeyValue(3) = Keys.D
        KeyString(4) = "E" : KeyValue(4) = Keys.E
        KeyString(5) = "F" : KeyValue(5) = Keys.F
        KeyString(6) = "G" : KeyValue(6) = Keys.G
        KeyString(7) = "H" : KeyValue(7) = Keys.H
        KeyString(8) = "I" : KeyValue(8) = Keys.I
        KeyString(9) = "J" : KeyValue(9) = Keys.J
        KeyString(10) = "K" : KeyValue(10) = Keys.K
        KeyString(11) = "L" : KeyValue(11) = Keys.L
        KeyString(12) = "M" : KeyValue(12) = Keys.M
        KeyString(13) = "N" : KeyValue(13) = Keys.N
        KeyString(14) = "O" : KeyValue(14) = Keys.O
        KeyString(15) = "P" : KeyValue(15) = Keys.P
        KeyString(16) = "Q" : KeyValue(16) = Keys.Q
        KeyString(17) = "R" : KeyValue(17) = Keys.R
        KeyString(18) = "S" : KeyValue(18) = Keys.S
        KeyString(19) = "T" : KeyValue(19) = Keys.T
        KeyString(20) = "U" : KeyValue(20) = Keys.U
        KeyString(21) = "V" : KeyValue(21) = Keys.V
        KeyString(22) = "W" : KeyValue(22) = Keys.W
        KeyString(23) = "X" : KeyValue(23) = Keys.X
        KeyString(24) = "Y" : KeyValue(24) = Keys.Y
        KeyString(25) = "Z" : KeyValue(25) = Keys.Z
        KeyString(26) = "0" : KeyValue(26) = Keys.D0
        KeyString(27) = "1" : KeyValue(27) = Keys.D1
        KeyString(28) = "2" : KeyValue(28) = Keys.D2
        KeyString(29) = "3" : KeyValue(29) = Keys.D3
        KeyString(30) = "4" : KeyValue(30) = Keys.D4
        KeyString(31) = "5" : KeyValue(31) = Keys.D5
        KeyString(32) = "6" : KeyValue(32) = Keys.D6
        KeyString(33) = "7" : KeyValue(33) = Keys.D7
        KeyString(34) = "8" : KeyValue(34) = Keys.D8
        KeyString(35) = "9" : KeyValue(35) = Keys.D9
        KeyString(36) = "Num0" : KeyValue(36) = Keys.NumPad0
        KeyString(37) = "Num1" : KeyValue(37) = Keys.NumPad1
        KeyString(38) = "Num2" : KeyValue(38) = Keys.NumPad2
        KeyString(39) = "Num3" : KeyValue(39) = Keys.NumPad3
        KeyString(40) = "Num4" : KeyValue(40) = Keys.NumPad4
        KeyString(41) = "Num5" : KeyValue(41) = Keys.NumPad5
        KeyString(42) = "Num6" : KeyValue(42) = Keys.NumPad6
        KeyString(43) = "Num7" : KeyValue(43) = Keys.NumPad7
        KeyString(44) = "Num8" : KeyValue(44) = Keys.NumPad8
        KeyString(45) = "Num9" : KeyValue(45) = Keys.NumPad9
    End Sub

    Private Sub _Click(sender As System.Object, e As System.EventArgs) Handles Me.Click
        If Me.BackColor = Color.Black Then
            Me.BackColor = Color.Red
        Else
            Me.BackColor = Color.Black
        End If
    End Sub

    Private Sub _LostFocus() Handles Me.LostFocus
        Me.BackColor = Color.Black
    End Sub

    Private Sub Enable_Changed() Handles Me.EnabledChanged
        If Me.Enabled = True Then
            Me.BackColor = Color.Black
        Else
            Me.BackColor = Color.DimGray
        End If
    End Sub

    Private Sub _KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.BackColor = Color.Red Then
            Dim ElseKey As Boolean = True
            For i = 0 To total
                If e.KeyCode = KeyValue(i) Then
                    ElseKey = False
                    Me.Text = KeyString(i)
                    Me.Tag = KeyValue(i)
                    Exit For
                End If
            Next
            If ElseKey Then
                Me.Text = "Null"
                Me.Tag = "-1"
            End If
        End If
    End Sub

    Public Sub LoadKey(KeyCode As Integer)
        Dim ElseKey As Boolean = True
        For i = 0 To total
            If KeyCode = KeyValue(i) Then
                ElseKey = False
                Me.Text = KeyString(i)
                Me.Tag = KeyValue(i)
                Exit For
            End If
        Next
        If ElseKey Then
            Me.Text = "Null"
            Me.Tag = "-1"
        End If
    End Sub
End Class

#End Region

Namespace yun
    Public Class API
        <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
        Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
        End Function

        Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
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
        Public Declare Function DwmIsCompositionEnabled Lib "dwmapi.dll" Alias "DwmIsCompositionEnabled" (ByRef pfEnabled As Boolean) As Integer
        Public Declare Function DwmExtendFrameIntoClientArea Lib "dwmapi.dll" Alias "DwmExtendFrameIntoClientArea" (ByVal hWnd As IntPtr, ByRef pMargin As Margins) As Integer
        Public Declare Function ReleaseCapture Lib "user32" Alias "ReleaseCapture" () As Boolean
        Public Declare Function MoveWindow Lib "user32" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean
        Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
        Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

        Public Const WM_MOUSE_MOVE = &H200
        Public Const WM_LBUTTON_DOWN = &H201
        Public Const WM_LBUTTON_UP = &H202
        Public Const WM_KEYDOWN = &H100
        Public Const WM_KEYUP = &H101
        Public Const WM_CHAR = &H102

        Public Structure Margins
            Public Left As Integer
            Public Right As Integer
            Public Top As Integer
            Public Bottom As Integer
        End Structure

        Public Structure RECT
            Dim Left As Integer
            Dim Top As Integer
            Dim Right As Integer
            Dim Bottom As Integer
        End Structure


        Public Const WM_SYSCOMMAND = &H112
        Public Const SC_MOVE = &HF010&
        Public Const HTCAPTION = &H2
        Public Const SRCCOPY = &HCC0020
    End Class

    Namespace UserControl
        Namespace EventHandler
            Public Class AeroForm
                Public Sub New(ByRef Target As Form)
                    Dim margins As API.Margins = New API.Margins
                    margins.Left = -1
                    margins.Right = -1
                    margins.Top = -1
                    margins.Bottom = -1
                    Dim result As Integer = API.DwmExtendFrameIntoClientArea(Target.Handle, margins)
                    Target.BackColor = Color.Black
                    AddHandler Target.MouseDown, AddressOf _MouseDown
                End Sub

                Private Sub _MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
                    API.ReleaseCapture()
                    API.MoveWindow(sender.Handle, API.WM_SYSCOMMAND, API.SC_MOVE + API.HTCAPTION, 0)
                End Sub
            End Class

            Public Class AeroListbox
                Public Sub New(ByRef Target As ListBox)
                    Target.BackColor = Color.Black
                    Target.DrawMode = DrawMode.OwnerDrawFixed
                    Target.Font = New Font("新細明體", 18, FontStyle.Bold)
                    AddHandler Target.DrawItem, AddressOf _DrawItem
                End Sub

                Private Const isSelected1 As Integer = DrawItemState.Selected
                Private Const isSelected2 As Integer = DrawItemState.Selected + DrawItemState.NoAccelerator
                Private Const isSelected3 As Integer = DrawItemState.Selected + DrawItemState.NoFocusRect
                Private Const isSelected4 As Integer = DrawItemState.Selected + DrawItemState.NoFocusRect + DrawItemState.NoAccelerator
                Private Const isSelected5 As Integer = DrawItemState.Selected + DrawItemState.Focus
                Private Const isSelected6 As Integer = DrawItemState.Selected + DrawItemState.Focus + DrawItemState.NoAccelerator
                Private Const isSelected7 As Integer = DrawItemState.Selected + DrawItemState.Focus + DrawItemState.NoFocusRect
                Private Const isSelected8 As Integer = DrawItemState.Selected + DrawItemState.Focus + DrawItemState.NoFocusRect + DrawItemState.NoAccelerator

                Private Sub _DrawItem(sender As System.Object, e As System.Windows.Forms.DrawItemEventArgs)
                    If sender.Items.Count <> 0 Then
                        If sender.Enabled = True Then
                            If e.State = isSelected1 Or _
                                                        e.State = isSelected2 Or _
                                                        e.State = isSelected3 Or _
                                                        e.State = isSelected4 Or _
                                                        e.State = isSelected5 Or _
                                                        e.State = isSelected6 Or _
                                                        e.State = isSelected7 Or _
                                                        e.State = isSelected8 Then
                                e.Graphics.FillRectangle(New SolidBrush(Color.DimGray), e.Bounds)
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X + 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X - 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, e.Bounds)
                            Else
                                e.DrawBackground()
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X + 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X - 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
                                e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, e.Bounds)
                            End If
                        Else
                            e.DrawBackground()
                            e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                            e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
                            e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X + 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                            e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X - 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
                            e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.DimGray, e.Bounds)
                        End If
                        e.DrawFocusRectangle()
                    End If
                End Sub
            End Class

            Public Class MouseMask
                Public Sub New(ByRef Target As PictureBox)
                    AddHandler Target.MouseDown, AddressOf _MouseDown
                    AddHandler Target.MouseMove, AddressOf _MouseMove
                    AddHandler Target.MouseUp, AddressOf _MouseUp
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
        End Namespace

        Public Class Drawing
            Public Sub SetBorder(Color As Color, Width As Integer)
                BorderBrush = New Pen(Color, Width)
            End Sub

            Public Sub SetMask(Color As Color, Opacity As Double)
                MaskBrush = New SolidBrush(Color.FromArgb(CInt(255 * Opacity), Color))
            End Sub

            Private MaskBrush As SolidBrush = New SolidBrush(Color.FromArgb(200, Color.Black))
            Private BorderBrush As Pen = New Pen(Color.Red, 1)

            Public Sub Mask(obj As PictureBox, X As Integer, Y As Integer, Width As Integer, Height As Integer)
                obj.Refresh()
                Dim g As Graphics = obj.CreateGraphics

                Dim p As New System.Drawing.Drawing2D.GraphicsPath

                p.AddRectangle(New Rectangle(0, 0, obj.Width, obj.Height))
                p.AddRectangle(New Rectangle(X, Y, Width, Height))

                g.FillPath(MaskBrush, p)
                g.DrawRectangle(BorderBrush, X, Y, Width, Height)
            End Sub
        End Class
    End Namespace

    Public Class Settings

#Region "BitmapCollections"
        Public Structure BitmapCollections
            Public Path As String
            Public X As Integer
            Public Y As Integer
            Public Width As Integer
            Public Height As Integer
            Public Offset As Integer
            Public Key As Integer
        End Structure

        Public Sub Load(File As String, ByRef bmpC() As BitmapCollections)
            Dim str As String
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(File, System.Text.Encoding.GetEncoding("big5"))
            str = sr.ReadToEnd
            sr.Close()

            If str = "" Then

            Else
                Dim ex() As String = str.Split(vbNewLine)
                ReDim bmpC(ex.Length - 1)

                For i = 0 To ex.Length - 1
                    ex(i) = ex(i).Replace(Chr(13), "")
                    ex(i) = ex(i).Replace(Chr(10), "")

                    Dim ex2() As String = ex(i).Split(",")

                    bmpC(i).Path = ex2(0)
                    bmpC(i).X = Val(ex2(1))
                    bmpC(i).Y = Val(ex2(2))
                    bmpC(i).Width = Val(ex2(3))
                    bmpC(i).Height = Val(ex2(4))
                    bmpC(i).Offset = Val(ex2(5))
                    bmpC(i).Key = Val(ex2(6))
                Next
            End If
        End Sub

        Public Sub Save(File As String, ByVal bmpC() As BitmapCollections)
            Dim str As String = ""

            For i = 0 To bmpC.Length - 1
                If i = 0 Then
                    str = bmpC(i).Path & "," _
                        & bmpC(i).X.ToString & "," _
                        & bmpC(i).Y.ToString & "," _
                        & bmpC(i).Width.ToString & "," _
                        & bmpC(i).Height.ToString & "," _
                        & bmpC(i).Offset.ToString & "," _
                        & bmpC(i).Key.ToString
                Else
                    str = str & vbNewLine _
                        & bmpC(i).Path & "," _
                        & bmpC(i).X.ToString & "," _
                        & bmpC(i).Y.ToString & "," _
                        & bmpC(i).Width.ToString & "," _
                        & bmpC(i).Height.ToString & "," _
                        & bmpC(i).Offset.ToString & "," _
                        & bmpC(i).Key.ToString
                End If
            Next

            Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(File, False, System.Text.Encoding.GetEncoding("big5"))
            sw.Write(str)
            sw.Close()
        End Sub

        Public Sub Insert(ByRef bmpC() As BitmapCollections, Inserted As BitmapCollections)
            If Not IsNothing(bmpC) Then
                Dim newbmpC(bmpC.Length) As BitmapCollections
                For i = 0 To newbmpC.Length - 2
                    newbmpC(i) = bmpC(i)
                Next
                newbmpC(newbmpC.Length - 1) = Inserted

                ReDim bmpC(newbmpC.Length - 1)
                For i = 0 To newbmpC.Length - 1
                    bmpC(i) = newbmpC(i)
                Next
            Else
                ReDim bmpC(0)
                bmpC(0) = Inserted
            End If
        End Sub

        Public Sub Delete(ByRef bmpC() As BitmapCollections, Index As Integer)
            Dim newbmpC(bmpC.Length - 2) As BitmapCollections
            For i = 0 To newbmpC.Length - 1
                If i < Index Then
                    newbmpC(i) = bmpC(i)
                Else
                    newbmpC(i) = bmpC(i + 1)
                End If
            Next

            ReDim bmpC(newbmpC.Length - 1)
            For i = 0 To newbmpC.Length - 1
                bmpC(i) = newbmpC(i)
            Next
        End Sub
#End Region

#Region "Time"
        Public Structure Time
            Public Function Year() As String
                Dim yy As Integer = DateTime.Today.Year()
                Return Format(yy, "0000")
            End Function

            Public Function Month() As String
                Dim mm As Integer = DateTime.Today.Month()
                Return Format(mm, "00")
            End Function

            Public Function Day() As String
                Dim dd As Integer = DateTime.Today.Day()
                Return Format(dd, "00")
            End Function

            Public Function Hour() As String
                Dim h As Integer = DateTime.Now.Hour()
                Return Format(h, "00")
            End Function

            Public Function Minute() As String
                Dim m As Integer = DateTime.Now.Minute()
                Return Format(m, "00")
            End Function

            Public Function Second() As String
                Dim s As Integer = DateTime.Now.Second()
                Return Format(s, "00")
            End Function

            Public Function Millisecond() As String
                Dim ms As Integer = DateTime.Now.Millisecond
                Return Format(ms, "000")
            End Function

            Public Function All() As String
                Dim yy As Integer = DateTime.Today.Year()
                Dim mm As Integer = DateTime.Today.Month()
                Dim dd As Integer = DateTime.Today.Day()
                Dim h As Integer = DateTime.Now.Hour()
                Dim m As Integer = DateTime.Now.Minute()
                Dim s As Integer = DateTime.Now.Second()
                Dim ms As Integer = DateTime.Now.Millisecond
                Return Format(yy, "0000") & Format(mm, "00") & Format(dd, "00") & Format(h, "00") & Format(m, "00") & Format(s, "00") & Format(ms, "000")
            End Function
        End Structure
#End Region

    End Class

    Namespace Tool
        Public Class Nox
            Private hwnd As Integer
            Private hwnd2 As Integer
            Private hwnd3 As Integer

            Private Target() As ChildBitmap

            Private Detect As Timer

            Private bgWorker As System.ComponentModel.BackgroundWorker

            Private Structure ChildBitmap
                'Private bgWorker As System.ComponentModel.BackgroundWorker

                Private bmp As Bitmap
                Private X As Integer
                Private Y As Integer
                Private Width As Integer
                Private Height As Integer
                Private Offset As Integer
                Public Key As Integer

                Private LocationP As Point
                Private EndP As Point

                'Private mainBitmap As Bitmap

                Public Sub New(bmpC As yun.Settings.BitmapCollections)
                    Dim sr As System.IO.FileStream = New System.IO.FileStream("C:\Nox\" & bmpC.Path & ".bmp", IO.FileMode.Open, IO.FileAccess.Read)
                    Me.bmp = Image.FromStream(sr)
                    sr.Close()

                    Me.X = bmpC.X
                    Me.Y = bmpC.Y
                    Me.Width = bmpC.Width
                    Me.Height = bmpC.Height
                    Me.Offset = bmpC.Offset * 2
                    Me.Key = bmpC.Key

                    If Me.X = -1 And Me.Y = -1 And Me.Width = -1 And Me.Height = -1 Then
                        Me.LocationP = New Point(-1, -1)
                        Me.EndP = New Point(-1, -1)
                    Else
                        Dim lx, ly, ex, ey As Integer
                        If Me.X - Me.Offset < 0 Then
                            lx = 0
                        Else
                            lx = Me.X - Me.Offset
                        End If
                        If Me.Y - Me.Offset < 0 Then
                            ly = 0
                        Else
                            ly = Me.Y - Me.Offset
                        End If
                        Me.LocationP = New Point(lx, ly)

                        If Me.X + Me.Width + Me.Offset > bmp.Width Then
                            ex = bmp.Width - Me.Width
                        Else
                            ex = Me.X + Me.Offset
                        End If
                        If Me.Y + Me.Height + Me.Offset > bmp.Height Then
                            ey = bmp.Height - Me.Height
                        Else
                            ey = Me.Y + Me.Offset
                        End If
                        Me.EndP = New Point(ex, ey)
                    End If
                End Sub

                Public Function Inside(mainbmp As Bitmap) As Boolean
                    If Me.X = -1 And Me.Y = -1 And Me.Width = -1 And Me.Height = -1 Then
                        Return False
                    Else
                        Dim found As Boolean = True
                        For i = Me.X To Me.X + Me.Width - 1
                            For j = Me.Y To Me.Y + Me.Height - 1
                                If mainbmp.GetPixel(i, j) <> Me.bmp.GetPixel(i, j) Then
                                    found = False
                                    Exit For
                                End If
                            Next
                            If found = False Then
                                Exit For
                            End If
                        Next

                        Return found
                    End If
                    Return False
                End Function

                Private Sub _DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
                    'Dim found As Boolean = True
                    ''MsgBox(mainBitmap.ToString)
                    'For i = Me.X To Me.X + Me.Width - 1
                    '    For j = Me.Y To Me.Y + Me.Height - 1
                    '        If Me.mainBitmap.GetPixel(i, j) <> Me.bmp.GetPixel(i, j) Then
                    '            found = False
                    '            Exit For
                    '        End If
                    '    Next
                    '    If found = False Then
                    '        Exit For
                    '    End If
                    'Next

                    'If found Then
                    '    MsgBox(1)
                    'End If
                End Sub
            End Structure


            Public Sub New()
                hwndRefresh()
                Detect = New Timer
                AddHandler Detect.Tick, AddressOf _Detect
            End Sub

            Public Sub hwndRefresh()
                '夜神安卓模拟器
                '夜神模拟器
                '夜神模拟器
                hwnd = API.FindWindow("Qt5QWindowIcon", "夜神模拟器")
                hwnd2 = API.FindWindowEx(hwnd, 0, "Qt5QWindowIcon", "ScreenBoardClassWindow")
                hwnd3 = API.FindWindowEx(hwnd2, 0, "Qt5QWindowIcon", "QWidgetClassWindow")
            End Sub

            Public Function Running() As Boolean
                If hwnd3 Then
                    Return True
                Else
                    Return False
                End If
            End Function

            Public Sub Start(bmpC() As yun.Settings.BitmapCollections, Interval As Integer)
                Dim newlen As Integer = bmpC.Length - 1
                ReDim Target(newlen)
                For i = 0 To newlen
                    Target(i) = New ChildBitmap(bmpC(i))

                Next

                'Dim bmp As Bitmap = GetBitmap()

                'For i = 0 To newlen
                '    'MsgBox(Target(i).Inside(bmp))
                '    If Target(i).Inside(bmp) Then
                '        MsgBox(i)
                '    End If
                'Next
                'ReDim TargetBitmap(newlen)

                'For i = 0 To newlen
                '    If bmpC(i).X <> -1 And bmpC(i).Y <> -1 And bmpC(i).Width <> -1 And bmpC(i).Height <> -1 Then
                '        TargetBitmap(i) = New Bitmap(CopyBitmap(UnlockLoad(bmpC(i).Path), New Rectangle(bmpC(i).X, bmpC(i).Y, bmpC(i).Width, bmpC(i).Height)))
                '    End If

                '    Target(i).X = bmpC(i).X
                '    Target(i).Y = bmpC(i).Y
                '    Target(i).Width = bmpC(i).Width
                '    Target(i).Height = bmpC(i).Height
                '    Target(i).Offset = bmpC(i).Offset
                '    Target(i).Key = bmpC(i).Key
                'Next
                Detect.Interval = Interval
                Detect.Start()

            End Sub

            Public Sub Finish()
                Detect.Stop()
            End Sub


            Public Function MAKELPARAM(ByVal l As Integer, ByVal h As Integer) As Integer
                Dim r As Integer = l + (h << 16)
                Return r
            End Function

            Private Sub _Detect()

                Dim bmp As Bitmap = GetBitmap()
                For i = 0 To Target.Length - 1
                    If Target(i).Inside(bmp) Then
                        If Target(i).Key <> -1 Then
                            API.PostMessage(hwnd, API.WM_KEYDOWN, Target(i).Key, MAKELPARAM(Target(i).Key, API.WM_KEYDOWN))
                            API.PostMessage(hwnd, API.WM_KEYUP, Target(i).Key, MAKELPARAM(Target(i).Key, API.WM_KEYUP))
                        End If
                    End If
                Next
            End Sub

            Private Function UnlockLoad(FileName As String) As Bitmap
                Dim rbmp As Bitmap
                Dim sr As System.IO.FileStream = New System.IO.FileStream("C:\Nox\" & FileName & ".bmp", IO.FileMode.Open, IO.FileAccess.Read)
                rbmp = Image.FromStream(sr)
                sr.Close()
                Return rbmp
            End Function

            Function CopyBitmap(source As Bitmap, part As Rectangle) As Bitmap
                Dim bmp As New Bitmap(part.Width, part.Height)

                Dim g As Graphics = Graphics.FromImage(bmp)
                g.DrawImage(source, 0, 0, part, GraphicsUnit.Pixel)
                g.Dispose()

                Return bmp
            End Function

            Public Function GetBitmap() As Bitmap
                Dim r As API.RECT
                API.GetWindowRect(hwnd3, r)

                Dim LocationX As Integer
                Dim LocationY As Integer
                Dim rWidth As Integer = r.Right - r.Left
                Dim rHeight As Integer = r.Bottom - r.Top

                Dim r2 As API.RECT
                API.GetClientRect(hwnd3, r2)
                Dim rborder As Integer = ((r.Right - r.Left) - r2.Right) / 2
                Dim rtitle As Integer = ((r.Bottom - r.Top) - r2.Bottom) - rborder
                LocationX = r.Left + rborder
                LocationY = r.Top + rtitle
                rWidth = r2.Right
                rHeight = r2.Bottom


                Dim Bhandle, DestDC, SourceDC As IntPtr

                SourceDC = API.GetDC(0)
                DestDC = API.CreateCompatibleDC(SourceDC)

                Bhandle = API.CreateCompatibleBitmap(SourceDC, rWidth, rHeight)
                API.SelectObject(DestDC, Bhandle)
                API.BitBlt(DestDC, 0, 0, rWidth, rHeight, SourceDC, LocationX, LocationY, API.SRCCOPY)

                Dim bmp As Bitmap = System.Drawing.Image.FromHbitmap(Bhandle)

                API.ReleaseDC(0, SourceDC)
                API.DeleteDC(DestDC)
                API.DeleteObject(Bhandle)
                GC.Collect()

                Return bmp
            End Function

            Public Function SearchBitmap(mainBmp As Bitmap, childBmp As Bitmap) As Point
                Dim rp As Point = New Point(-1, -1)

                For i = 0 To mainBmp.Width - childBmp.Width
                    For j = 0 To mainBmp.Height - childBmp.Height
                        Dim IsEqual As Boolean = True

                        For i2 = 1 To childBmp.Width - 1
                            For j2 = 1 To childBmp.Height - 1
                                If mainBmp.GetPixel(i + i2, j + j2) <> childBmp.GetPixel(i2, j2) Then
                                    IsEqual = False
                                    Exit For
                                End If
                            Next
                            If IsEqual = False Then
                                Exit For
                            End If
                        Next

                        If IsEqual Then
                            rp = New Point(i, j)
                            Exit For
                        End If
                    Next

                    If rp.X <> -1 And rp.Y <> -1 Then
                        Exit For
                    End If
                Next

                Return rp
            End Function

            Public Function SearchBitmap(mainBmp As Bitmap, childBmp As Bitmap, X As Integer, Y As Integer, Optional Offset As Integer = 0) As Point

            End Function

            Public Function SearchBitmapX(mainBmp As Bitmap, childBmp As Bitmap, Optional ByVal LocationX As Integer = -1, Optional ByVal LocationY As Integer = -1) As Point
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
        End Class
    End Namespace
End Namespace