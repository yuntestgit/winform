Public Class Form1
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer

    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Declare Function GetClientRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer

    Public Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Function PrintWindow(hwnd As Integer, Optional ByVal border As Boolean = False) As Bitmap
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
    End Function

    Function SearchBitmap(mainBmp As Bitmap, childBmp As Bitmap, Optional ByVal LocationX As Integer = -1, Optional ByVal LocationY As Integer = -1) As Point
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
    End Function


    'x-8
    'y-30
    'heart 670,50 24,10

    '0-0 670,50  662,20
    '0-1 663,528 655,498
    '1-0 617,534 609,504
    '2-0 313,516 305,486
    '3-0 414,520 406,490
    '4-0 450,518 442,488
    '5-0 670,50  662,20
    '9-0 439,91 431,61
    '9-1 836,107 828,77
    'a-0 396,215 388,185
    'a-1 451,372 443,342

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Dim h As Integer = FindWindow("QWidget", vbNullString)
        'Dim bmp1 As Bitmap = PrintWindow(h, False)
        'Me.BackgroundImage = bmp1
        Dim bmp1 As Bitmap = Image.FromFile("C:\Users\yun\Desktop\_printed\zr.png")
        Dim bmp2 As Bitmap = Image.FromFile("C:\Users\yun\Desktop\_printed\zr-0.png")

        'Dim p As Point = SearchBitmap(bmp1, bmp2, 670, 50)
        Dim p As Point = SearchBitmap(bmp1, bmp2)
        Dim r As Point = New Point(p.X - 8, p.Y - 30)
        Dim s As String = "img(tmp) = Image.FromFile(" & Chr(34) & "C:\Users\yun\Desktop\_printed\zr-0.png" & Chr(34) & ") : pt(tmp) = New Point(" & r.X & ", " & r.Y & ") : tmp += 1"
        RichTextBox1.Text = s
        'MsgBox(p.ToString)

        'Dim X, Y As Integer
        'X = 1400
        'Y = 900
        'Dim Screenshot As New Bitmap(X, Y, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        'Dim Graph As Graphics = Graphics.FromImage(Screenshot)
        'Graph.CopyFromScreen(-500, 0, 0, 0, New Size(X, Y), CopyPixelOperation.SourceCopy)

        'Me.BackgroundImage = Screenshot
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        'Dim h As Integer = FindWindow("QWidget", vbNullString)
        'Me.BackgroundImage = PrintWindow(h, False)
    End Sub
End Class
