Public Class Form1

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'System.IO.Directory.Delete("C:\Sandbox\yun\box8", True)
        Dim test = New System.IO.DirectoryInfo("C:\Sandbox\yun\box5")
        test.Attributes = IO.FileAttributes.Normal

        For Each file In test.GetFiles
            file.Attributes = IO.FileAttributes.Normal
            file.Delete()
        Next

        For Each dire In test.GetDirectories
            dire.Attributes = IO.FileAttributes.Normal
            dire.Delete()
        Next

        test.Delete()
        'Me.Text = test.ToString
    End Sub

    Sub DeleteDirectories(ByVal path As String)
        If System.IO.Directory.Exists(path) Then
            Dim dinf As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(path)
            dinf.Attributes = IO.FileAttributes.Normal

            For Each file In dinf.GetFiles
                file.Attributes = IO.FileAttributes.Normal
                file.Delete()
            Next

            Dim dire() As String = System.IO.Directory.GetDirectories(path)
            For i = 0 To dire.Length - 1
                DeleteDirectories(dire(i).ToString)
            Next

            dinf.Delete()
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        DeleteDirectories("C:\Sandbox\yun")
    End Sub
End Class
