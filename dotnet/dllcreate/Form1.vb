Public Class Form1

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        Dim file As String = TextBox1.Text

        Dim root As String = "C:\myProgram\dotnet\" & file & "\bin\Debug\"

        Dim dllpath As String = root & file & ".dll"

        Dim manifest As String = Application.StartupPath & "\app.manifest"

        Dim mt As String = "mt.exe -manifest " & manifest & " -outputresource:" & dllpath & ";#2"


        Dim sn As String = "sn -k " & "C:\myProgram\dotnet\" & file & "\" & file & ".snk"

        Dim regasm As String = "regasm " & dllpath

        Dim gacutil As String = "gacutil /I " & dllpath

        TextBox2.Text = sn

        TextBox3.Text = regasm

        TextBox4.Text = gacutil
    End Sub
End Class
