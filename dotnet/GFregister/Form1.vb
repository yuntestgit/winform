Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        WebBrowser1.Navigate("https://xdb2.x-legend.com.tw/member/memberRegister.php?game=5&ad=&redirect=http%3A%2F%2Fgf.x-legend.com.tw%2F00member%2Fmember_01.php")
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim test As Windows.Forms.HtmlElement = WebBrowser1.Document.GetElementById("username")
        test.InnerText = "holedigger000"
    End Sub
End Class
