Public Class Form1
    Public dm As New Dm.dmsoft

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim x As Integer = -1
        Dim y As Integer = -1
        Dim f As Integer = dm.FindPic(0, 0, 1920, 1080, "C:\Users\yun\Desktop\testfind.png", 0, 0.9, 0, x, y)
        If x >= 0 Then
            Me.Text = x & ", " & y
        Else
            Me.Text = x
        End If

    End Sub
End Class
