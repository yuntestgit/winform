Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim aeroform As New yun.UserControl.EventHandler.AeroForm(Me)
        Dim aerolist As New yun.UserControl.EventHandler.AeroListbox(ListBox1)

        Dim b As New Button
        With b

        End With
        'Me.Controls.Add(b)
    End Sub
End Class

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
                    'Target.BackColor = Color.Black
                    'Target.DrawMode = DrawMode.OwnerDrawFixed
                    Target.Font = New Font("新細明體", 18, FontStyle.Bold)
                    AddHandler Target.DrawItem, AddressOf _DrawItem
                End Sub

                Private Sub _DrawItem(sender As System.Object, e As System.Windows.Forms.DrawItemEventArgs)
                    If e.State = DrawItemState.Selected Then
                        e.Graphics.FillRectangle(New SolidBrush(Color.DimGray), e.Bounds)

                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))

                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X + 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X - 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))

                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, e.Bounds)

                    ElseIf e.State = DrawItemState.None Then
                        e.DrawBackground()
                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))

                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X + 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X - 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
                        e.Graphics.DrawString(sender.Items.Item(e.Index), e.Font, Brushes.White, e.Bounds)
                    End If
                    e.DrawFocusRectangle()
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

    Class Settings

#Region "BitmapCollections"
        Public Structure BitmapCollections
            Public Path As String
            Public X As Integer
            Public Y As Integer
            Public Width As Integer
            Public Height As Integer
            Public Mode As Integer
            Public Offset As Integer
        End Structure

        Public Sub Load(File As String, ByRef bmpC() As BitmapCollections)
            Dim str As String
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(File, System.Text.Encoding.GetEncoding("big5"))
            str = sr.ReadToEnd
            sr.Close()

            Dim ex() As String = str.Split(vbNewLine)
            ReDim bmpC(ex.Length - 1)

            For i = 0 To ex.Length - 1
                ex(i) = ex(i).Replace(Chr(13), "")
                ex(i) = ex(i).Replace(Chr(10), "")

                Dim ex2() As String = ex(i).Split(",")

                bmpC(i).Path = ex2(0)
                bmpC(i).X = ex2(1)
                bmpC(i).Y = ex2(2)
                bmpC(i).Width = ex2(3)
                bmpC(i).Height = ex2(4)
                bmpC(i).Mode = ex2(5)
                bmpC(i).Offset = ex2(6)
            Next
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
                        & bmpC(i).Mode.ToString & "," _
                        & bmpC(i).Offset.ToString
                Else
                    str = str & vbNewLine _
                        & bmpC(i).Path & "," _
                        & bmpC(i).X.ToString & "," _
                        & bmpC(i).Y.ToString & "," _
                        & bmpC(i).Width.ToString & "," _
                        & bmpC(i).Height.ToString & "," _
                        & bmpC(i).Mode.ToString & "," _
                        & bmpC(i).Offset.ToString
                End If
            Next
            Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(File, False, System.Text.Encoding.GetEncoding("big5"))
            sw.Write(str)
            sw.Close()
        End Sub

        Public Sub Insert(ByRef bmpC() As BitmapCollections, Inserted As BitmapCollections)
            Dim newbmpC(bmpC.Length) As BitmapCollections
            For i = 0 To newbmpC.Length - 2
                newbmpC(i) = bmpC(i)
            Next
            newbmpC(newbmpC.Length - 1) = Inserted

            ReDim bmpC(newbmpC.Length - 1)
            For i = 0 To newbmpC.Length - 1
                bmpC(i) = newbmpC(i)
            Next
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

            Public Function All() As String
                Dim yy As Integer = DateTime.Today.Year()
                Dim mm As Integer = DateTime.Today.Month()
                Dim dd As Integer = DateTime.Today.Day()
                Dim h As Integer = DateTime.Now.Hour()
                Dim m As Integer = DateTime.Now.Minute()
                Dim s As Integer = DateTime.Now.Second()
                Return Format(yy, "0000") & Format(mm, "00") & Format(dd, "00") & Format(h, "00") & Format(m, "00") & Format(s, "00")
            End Function
        End Structure
#End Region

    End Class

    Namespace Tool
        Public Class Nox
            Private hwnd As Integer
            Private hwnd2 As Integer
            Private hwnd3 As Integer

            Public Sub New()
                hwndRefresh()
            End Sub

            Public Sub hwndRefresh()
                hwnd = API.FindWindow("Qt5QWindowIcon", "夜神模拟器")
                hwnd2 = API.FindWindowEx(hwnd, 0, "Qt5QWindowIcon", "ScreenBoardClassWindow")
                hwnd3 = API.FindWindowEx(hwnd2, 0, "Qt5QWindowIcon", "QWidgetClassWindow")
            End Sub

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

        End Class
    End Namespace
End Namespace