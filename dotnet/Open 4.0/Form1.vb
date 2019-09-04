Public Class Form1
    Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer

    Dim max As Integer = 20
    Dim path As String = "C:\Program Files\Sandboxie\Start.exe"
    Dim lstr As String = "/box:box"
    Dim rstr As String = " C:\Users\yun\Desktop\Open.lnk"

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = 1035 - Me.Height
        Me.Left = 5
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        DeleteDirectories("C:\Sandbox\yun")
        MsgBox("刪除完成")
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim n As Integer = Val(TextBox1.Text)
        Dim nar(n - 1) As String
        Dim t As Integer = 0
        For i = 0 To max
            Dim istr As String
            If i < 10 Then
                istr = "0" & i.ToString
            Else
                istr = i.ToString
            End If

            Dim h As Integer = FindWindow("Sandbox:box" & istr & ":DJO_CLASS", vbNullString)
            If h Then
            Else
                nar(t) = istr
                t += 1
                If t = n Then
                    Exit For
                End If
            End If
        Next

        For i = 0 To n - 1
            Process.Start(path, lstr & nar(i) & rstr)
        Next

        TextBox1.Clear()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        ''Process.Start("C:\Program Files\Sandboxie\Start.exe", "/box:box2 C:\Users\yun\Desktop\Open.lnk")
        For i = 0 To max
            Dim istr As String
            If i < 10 Then
                istr = "0" & i.ToString
            Else
                istr = i.ToString
            End If

            Dim h As Integer = FindWindow("Sandbox:box" & istr & ":DJO_CLASS", vbNullString)
            If h Then
            Else
                Process.Start(path, lstr & istr & rstr)
                Exit For
            End If
        Next
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
End Class
