Public Class Form1

#Region "Findwindow"
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
#End Region

    Dim loaded As Boolean = False

    Private Sub Form1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        'Process.Start("C:\Program Files\Sandboxie\Start.exe", "/box:box2 C:\Users\yun\Desktop\Open.lnk")
        If loaded = False Then
            loaded = True

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Application.StartupPath & "\..\settings\Box.txt", System.Text.Encoding.GetEncoding("big5"))
            Dim s As String = sr.ReadToEnd
            sr.Close()

            If s <> "" Then
                Dim ex() As String = s.Split(vbNewLine)
                For i = 0 To ex.Length - 1
                    ex(i) = ex(i).Replace(Chr(13), "")
                    ex(i) = ex(i).Replace(Chr(10), "")
                Next

                Dim sr2 As System.IO.StreamReader = New System.IO.StreamReader(Application.StartupPath & "\..\settings\Path.txt", System.Text.Encoding.GetEncoding("big5"))
                Dim s2 As String = sr2.ReadToEnd
                sr2.Close()

                Dim ex2() As String = s2.Split(vbNewLine)
                For i = 0 To ex2.Length - 1
                    ex2(i) = ex2(i).Replace(Chr(13), "")
                    ex2(i) = ex2(i).Replace(Chr(10), "")
                Next


                Dim FullBox As Boolean = True
                For i = 0 To ex.Length - 1
                    Dim h As Integer = FindWindow("Sandbox:" & ex(i) & ":DJO_CLASS", vbNullString)
                    If h Then
                    Else
                        FullBox = False
                        Process.Start(ex2(0) & "\Start.exe", "/box:" & ex(i) & " " & Application.StartupPath & "\Open.lnk")
                        Exit For
                    End If
                Next

                If FullBox Then
                    If MsgBox("沙盤已經滿了，請新增沙盤") Then
                        Me.Close()
                    End If
                Else
                    Me.Close()
                End If

            Else
                Me.Close()
            End If
        End If
    End Sub
End Class
