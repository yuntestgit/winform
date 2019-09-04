Public Class Form1
    '.jpg
    '.wmv
    '.mp4
    '.png
    '.avi
    '.mpg
    '.flv
    '.mkv
    '.rmvb
    '.rm
    '.jpeg
    'jpg png jpeg
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'System.IO.Directory.GetDirectories("path") 'string[] 所有資料夾
        'System.IO.Directory.GetFiles("path") 'string[] 所有檔案
        'System.IO.Path.GetExtension("path") 'string 副檔名

        'Dim ex(100) As String
        'Dim exi As Integer = -1
        'Dim d() As String = System.IO.Directory.GetDirectories("e:\completed")
        'For i = 0 To d.Length - 1
        '    Dim f() = System.IO.Directory.GetFiles(d(i))
        '    ListBox1.Items.Add(d(i))
        '    For j = 0 To f.Length - 1
        '        ListBox1.Items.Add(j & " - " & f(j))
        '        Dim jex As String = System.IO.Path.GetExtension(f(j)).ToLower
        '        Dim jexF As Boolean = True
        '        For k = 0 To exi
        '            If jex = ex(k) Then
        '                jexF = False
        '                Exit For
        '            End If
        '        Next
        '        If jexF = True Then
        '            exi += 1
        '            ex(exi) = jex
        '        End If
        '    Next
        'Next
        'For i = 0 To exi
        '    RichTextBox1.Text &= ex(i) & vbNewLine
        'Next

        '----------------------------------------------------------------------------------------

        Dim out As String = ""

        Dim d() As String = System.IO.Directory.GetDirectories("e:\completed")
        For i = 0 To d.Length - 1
            Dim f() = System.IO.Directory.GetFiles(d(i))
            'ListBox1.Items.Add(d(i))
            out &= d(i).Replace("e:\completed\", "").Replace("\", "/")
            For j = 0 To f.Length - 1
                Dim jex As String = System.IO.Path.GetExtension(f(j)).ToLower
                If jex <> ".png" Then
                    out &= "#" & f(j).Replace(d(i) & "\", "").Replace("\", "/")
                End If
            Next
            If i <> d.Length - 1 Then
                out &= vbNewLine
            End If
        Next

        '----------------------------------------------------------------------------------------
        'My.Computer.FileSystem.DeleteFile("test.txt")

        Dim sw As System.IO.StreamWriter
        sw = My.Computer.FileSystem.OpenTextFileWriter("test.txt", True)
        sw.Write(out)
        sw.Close()

    End Sub
End Class
