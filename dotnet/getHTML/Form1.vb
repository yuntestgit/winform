Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Req As Net.WebRequest
        Dim ReceiveStream As IO.Stream
        Dim encode As System.Text.Encoding
        Dim sr As IO.StreamReader
        Dim result As Net.WebResponse
        Req = Net.WebRequest.Create("http://www.comicvip.com/html/103.html")
        result = Req.GetResponse()
        ReceiveStream = result.GetResponseStream
        encode = System.Text.Encoding.GetEncoding("big5")
        'encode = System.Text.Encoding.UTF8
        sr = New IO.StreamReader(ReceiveStream, encode)
        Dim text1 = sr.ReadToEnd() '抓網頁的原始檔
        RichTextBox1.Text = text1
    End Sub
End Class
