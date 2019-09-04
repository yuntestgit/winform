Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Application.StartupPath & "\settings\Box.txt", System.Text.Encoding.GetEncoding("big5"))
        Dim s As String = sr.ReadToEnd
        sr.Close()

        If s <> "" Then
            Dim ex() As String = s.Split(vbNewLine)
            For i = 0 To ex.Length - 1
                ex(i) = ex(i).Replace(Chr(13), "")
                ex(i) = ex(i).Replace(Chr(10), "")
                ListBox1.Items.Add(ex(i))
            Next
        End If

        Dim sr2 As System.IO.StreamReader = New System.IO.StreamReader(Application.StartupPath & "\settings\Path.txt", System.Text.Encoding.GetEncoding("big5"))
        Dim s2 As String = sr2.ReadToEnd
        sr2.Close()

        If s2 <> "" Then
            Dim ex() As String = s2.Split(vbNewLine)
            For i = 0 To ex.Length - 1
                ex(i) = ex(i).Replace(Chr(13), "")
                ex(i) = ex(i).Replace(Chr(10), "")
            Next
            TextBox1.Text = ex(0)
            TextBox2.Text = ex(1)
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        CreateShortCut(Application.StartupPath & "\launcher", "Launcher.exe", Application.StartupPath & "\icon\ShortCutR.ico", "Launcher.lnk")
        CreateShortCut(TextBox1.Text, "GrandFantasia.exe", Application.StartupPath & "\icon\ShortCutB.ico", "Open.lnk", Application.StartupPath & "\launcher")
        MsgBox("建立完成")
    End Sub

    Sub CreateShortCut(exePath As String, exeName As String, iconPath As String, lnkName As String, Optional ByVal lnkPath As String = "_Desktop")
        exeName = "\" & exeName
        lnkName = "\" & lnkName

        Dim wsh, MyShortcut As Object
        wsh = CreateObject("WScript.Shell")

        Dim DesktopPath As String
        If lnkPath = "_Desktop" Then
            DesktopPath = wsh.SpecialFolders("Desktop")
        Else
            DesktopPath = lnkPath
        End If

        MyShortcut = wsh.CreateShortcut(DesktopPath & lnkName)

        MyShortcut.TargetPath = wsh.ExpandEnvironmentStrings(exePath & exeName)

        MyShortcut.WorkingDirectory = wsh.ExpandEnvironmentStrings(exePath)

        MyShortcut.IconLocation = wsh.ExpandEnvironmentStrings(iconPath)

        MyShortcut.WindowStyle = 4

        MyShortcut.Save()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim ins As String = InputBox("新增沙盤")
        ListBox1.Items.Add(ins)
        Dim s As String = ""
        For i = 0 To ListBox1.Items.Count - 1
            If i = 0 Then
                s &= ListBox1.Items(i)
            Else
                s &= vbNewLine & ListBox1.Items(i)
            End If
        Next
        Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(Application.StartupPath & "\settings\Box.txt", False, System.Text.Encoding.GetEncoding("big5"))
        sw.Write(s)
        sw.Close()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If ListBox1.SelectedIndex = -1 Then
            MsgBox("請選擇沙盤")
        Else
            ListBox1.Items.Remove(ListBox1.SelectedItem)
            Dim s As String = ""
            For i = 0 To ListBox1.Items.Count - 1
                If i = 0 Then
                    s &= ListBox1.Items(i)
                Else
                    s &= vbNewLine & ListBox1.Items(i)
                End If
            Next
            Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(Application.StartupPath & "\settings\Box.txt", False, System.Text.Encoding.GetEncoding("big5"))
            sw.Write(s)
            sw.Close()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        Dim s As String = TextBox1.Text & vbNewLine & TextBox2.Text
        Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(Application.StartupPath & "\settings\Path.txt", False, System.Text.Encoding.GetEncoding("big5"))
        sw.Write(s)
        sw.Close()
    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        Dim s As String = TextBox1.Text & vbNewLine & TextBox2.Text
        Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(Application.StartupPath & "\settings\Path.txt", False, System.Text.Encoding.GetEncoding("big5"))
        sw.Write(s)
        sw.Close()
    End Sub
End Class
