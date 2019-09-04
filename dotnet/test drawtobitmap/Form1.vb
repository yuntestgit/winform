Public Class Form1
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function

    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer

#Region "SetParent"
    Public Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As Int32, ByVal hWndNewParent As Int32) As Boolean
    Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
#End Region

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim childHWND As Integer = FindWindow("QWidget", vbNullString)
        Me.Text = childHWND
        SetParent(childHWND, PictureBox1.Handle)
        SetWindowPos(childHWND, 0, 10, 66, 1068, 602, 4)
    End Sub



    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim h As Integer = FindWindowEx(PictureBox1.Handle, 0, "QWidget", vbNullString)
        SetParent(h, 0)
        SetWindowPos(h, 0, 10, 66, 1068, 602, 4)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim b As Bitmap = New Bitmap(PictureBox1.Width, PictureBox1.Height)

        Me.DrawToBitmap(b, New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))

        PictureBox2.Image = b
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Dim h As Integer = FindWindowEx(PictureBox1.Handle, 0, "QWidget", vbNullString)
        Dim d
    End Sub
End Class
