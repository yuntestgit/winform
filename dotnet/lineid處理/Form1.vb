Public Class Form1

    Dim s(50) As String
    Dim index As Integer = 0
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        For i = 0 To 49
            s(i) = ""
        Next
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim flag As Boolean = True
        For i = 0 To index
            If TextBox1.Text = s(i) Then
                flag = False
                Exit For
            End If
        Next

        If flag Then
            s(index) = TextBox1.Text
            RichTextBox1.Text = ""
            For i = 0 To index
                RichTextBox1.Text &= i & " - " & s(i) & vbNewLine
            Next
            index += 1
            TextBox1.Text = ""
        Else
            MsgBox("重複")
        End If
    End Sub


End Class
