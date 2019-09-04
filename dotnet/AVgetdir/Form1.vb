Public Class Form1

    Dim dir As String = ""

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim path As String = "d:\completed"

        Dim d() As String = System.IO.Directory.GetDirectories(path)
        For i = 0 To d.Length - 1
            Dim f() = System.IO.Directory.GetFiles(d(i))
            dir &= i & "?" & d(i).Replace(path + "\", "").Replace("\", "/") & "?"

            Dim tags As String = ""
            For j = 0 To f.Length - 1
                Dim jex As String = System.IO.Path.GetExtension(f(j)).ToLower
                If jex <> ".png" Then
                    If jex <> ".txt" Then
                        dir &= "$" & f(j).Replace(d(i) & "\", "").Replace("\", "/")
                    Else
                        tags = f(j).Replace(d(i) & "\", "").Replace("\", "/").Replace(".txt", "")
                    End If
                End If
            Next
            dir &= "?" & tags
            If i <> d.Length - 1 Then
                dir &= vbNewLine
            End If
        Next

        TextBox1.Text = dir
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'UTF8Encoding utf8 = new UTF8Encoding(false); 
        Dim utf8_bom As System.Text.Encoding = System.Text.Encoding.UTF8
        Dim utf8_nobom As System.Text.Encoding = New System.Text.UTF8Encoding(False)
        Dim sw As New System.IO.StreamWriter(Application.StartupPath & "\dir.txt", False, utf8_nobom)
        sw.Write(dir)
        sw.Close()

        MsgBox("success")
    End Sub
End Class
