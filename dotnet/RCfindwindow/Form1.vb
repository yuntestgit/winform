Public Class Form1
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function

    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim h As Integer = FindWindow("RCWindow", "RaidCall - 新勻弟弟")
        If h Then
            ListBox1.Items.Add(Hex(h).ToString)
            EnumWindows(h)
        End If
    End Sub

    Sub EnumWindows(h As Integer)
        Dim h2 As Integer = 0
        While True
            h2 = FindWindowEx(h, h2, vbNullString, vbNullString)
            If h2 Then
                ListBox1.Items.Add(Hex(h2).ToString)
                EnumWindows(h2)
            Else
                Exit While
            End If
        End While
    End Sub
End Class
