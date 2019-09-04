Imports System.IO

Public Class Form1
    Dim path As String = "E:\_setup\game\san 9\武將登錄\金庸1\D_TPrsn.S9"
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim sr As New System.IO.StreamReader("E:\_setup\game\san 9\武將登錄\金庸1\D_TPrsn.S9", System.Text.Encoding.UTF8)
        'Dim str = sr.ReadToEnd()
        'sr.Close()
        'TextBox1.Text = str

        'Dim bytes = My.Computer.FileSystem.ReadAllBytes("E:\_setup\game\san 9\武將登錄\金庸1\D_TPrsn.S9")
        'Dim out As String = ""
        'For i = 0 To bytes.Length
        '    out += bytes(i).ToString
        'Next
        'TextBox1.Text = out

        'Dim fs As New System.IO.FileStream("E:\_setup\game\san 9\武將登錄\金庸1\D_TPrsn.S9", IO.FileMode.Open, IO.FileAccess.Read)
        'fs.see()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim theFile As FileStream = File.Open(path, FileMode.Open, FileAccess.Read)
        Dim rdr As StreamReader = New StreamReader(theFile)
        TextBox1.Text = rdr.ReadToEnd()
        rdr.Close()
        theFile.Close()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim rdr As StreamReader = File.OpenText(path)
        TextBox1.Text = rdr.ReadToEnd()
        rdr.Close()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        TextBox1.Text = File.ReadAllText(path)
    End Sub
End Class
