Public Class Form1

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try
            Dim myFile As String
            Dim test1 As New System.Windows.Forms.SaveFileDialog

            'saveFileDialog1.Title = "儲存圖片"
            'saveFileDialog1.Filter = "圖片(*.bmp)|*.bmp"
            'saveFileDialog1.FileName = "test.bmp"
            'saveFileDialog1.InitialDirectory = Application.StartupPath

            Dim root As String = Application.StartupPath & "\kingprinter2\"
            test1.InitialDirectory = root
            test1.Filter = "圖片(*.bmp)|*.bmp"
            test1.FileName = root & "ttt.bmp"
            test1.ShowDialog()

            myFile = test1.FileName
            MsgBox(myFile)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim img = Image.FromFile("C:\Users\yun\Desktop\img3.png")
        Me.BackgroundImage = img
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        ' Dim bmp As New Bitmap  = Image.FromFile("C:\Users\yun\Desktop\img3.png")
        ' Dim b As Bitmap

    End Sub

    Function CopyBitmap(source As Bitmap, part As Rectangle) As Bitmap
        Dim bmp As New Bitmap(part.Width, part.Height)

        Dim g As Graphics = Graphics.FromImage(bmp)
        g.DrawImage(source, 0, 0, part, GraphicsUnit.Pixel)
        g.Dispose()

        Return bmp
    End Function
End Class
