Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim settings As String
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Application.StartupPath & "\D_TPrsn.S9", System.Text.Encoding.BigEndianUnicode)
        settings = sr.ReadToEnd
        sr.Close()

        RichTextBox1.Text = settings
    End Sub
End Class
