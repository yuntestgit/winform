Public Class Form1

    Dim img As Bitmap = Image.FromFile("C:\Users\yun\Desktop\sample.jpg")

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        PictureBox1.Image = img
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim img2 As New Bitmap(img.Width, img.Height)
        For y = 0 To img.Height - 1
            For x = 0 To img.Width - 1
                Dim c As Color = img.GetPixel(x, y)
                If c.R > 200 And c.G > 200 And c.B > 200 Then
                    img2.SetPixel(x, y, Color.White)
                Else
                    img2.SetPixel(x, y, Color.Black)
                End If
                'img2.SetPixel(x, y, c)
            Next
        Next
        PictureBox2.Image = img2
    End Sub
End Class
