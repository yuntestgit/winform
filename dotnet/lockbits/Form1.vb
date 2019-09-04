Public Class Form1
    Dim b As Bitmap = Image.FromFile("C:\Users\yun\Desktop\key\step11.png")
    Dim b2 As Bitmap = Image.FromFile("C:\Users\yun\Desktop\key\1.png")

    Dim c(b2.Width - 1, b2.Height - 1) As String
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'PictureBox1.Image = b
        Dim tempb As Bitmap = New Bitmap(b)
        PictureBox1.Image = tempb
        'For w = 0 To b2.Width - 1
        '    For h = 0 To b2.Height - 1
        '        c(w, h) = b2.GetPixel(w, h).R.ToString & "," & b2.GetPixel(w, h).G.ToString & "," & b2.GetPixel(w, h).B.ToString
        '    Next
        'Next
    End Sub

    Public Function SearchBitmap2(mainBmp As Bitmap, childBmp As Bitmap, Optional ByVal LocationX As Integer = -1, Optional ByVal LocationY As Integer = -1) As Point
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

    Function SearchBitmap(src As Bitmap, trg As Bitmap) As Point
        Dim bmp As Bitmap = src
        Dim rect As New Rectangle(0, 100, bmp.Width, bmp.Height - 100)
        Dim bmpData As System.Drawing.Imaging.BitmapData = bmp.LockBits(rect, Drawing.Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format24bppRgb)
        Dim ptr As IntPtr = bmpData.Scan0
        Dim bytes As Integer = Math.Abs(bmpData.Stride) * (bmp.Height - 100)
        Dim rgbValues(bytes - 1) As Byte
        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)

        Dim l As Integer = 0
        Dim R, G, B As Int32

        Dim bmp2 As Bitmap = trg
        Dim rect2 As New Rectangle(0, 0, bmp2.Width, bmp2.Height)
        Dim bmpData2 As System.Drawing.Imaging.BitmapData = bmp2.LockBits(rect2, Drawing.Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format24bppRgb)
        Dim ptr2 As IntPtr = bmpData2.Scan0
        Dim bytes2 As Integer = Math.Abs(bmpData2.Stride) * bmp2.Height
        Dim rgbValues2(bytes2 - 1) As Byte
        System.Runtime.InteropServices.Marshal.Copy(ptr2, rgbValues2, 0, bytes2)

        Dim l2 As Integer = 0
        Dim R2, G2, B2 As Int32

        'Dim str(src.Width - 1, src.Height - 1) As String
        For x = 0 To bmp.Width - 1
            For y = 0 To bmp.Height - 101
                l = ((bmp.Width * 3 * y) + (x * 3))
                R = rgbValues(l + 2)
                G = rgbValues(l + 1)
                B = rgbValues(l)

                If R = rgbValues2(2) And G = rgbValues2(1) And B = rgbValues2(0) Then
                    If x + bmp2.Width < bmp.Width And y + bmp2.Height < bmp.Height Then
                        Dim eq As Boolean = True
                        For w = 0 To bmp2.Width - 1
                            For h = 0 To bmp2.Height - 1

                            Next
                        Next
                    End If
                End If
            Next
        Next
        bmp.UnlockBits(bmpData)


        Dim rp As New Point(-1, -1)
        'For x = 0 To bmp.Width - 1
        '    For y = 0 To bmp.Height - 1
        '        If str(x, y) = c(0, 0) Then
        '            If b2.Width + x < bmp.Width And b2.Height + y < bmp.Height Then
        '                Dim eq As Boolean = True
        '                For i = 1 To b2.Width - 1
        '                    For j = 1 To b2.Height - 1
        '                        If str(x + i, y + j) <> c(i, j) Then
        '                            eq = False
        '                            Exit For
        '                        End If
        '                    Next
        '                Next
        '                If eq Then
        '                    r = New Point(x, y)
        '                End If
        '            End If
        '        End If
        '    Next
        'Next
        Return rp

    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim t1 As Double = Microsoft.VisualBasic.Timer
        Dim rp As String = SearchBitmap(b, b2).ToString
        'Dim rp As String = SearchBitmap(b, b2).ToString()
        Dim t2 As Double = Microsoft.VisualBasic.Timer
        Me.Text = rp & (t2 - t1).ToString
        'Me.Text = SearchBitmap(b, b2).ToString
    End Sub
End Class
