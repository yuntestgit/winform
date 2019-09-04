Public Class Form1
    Declare Function SetCursorPos Lib "user32" (ByVal x As Integer, ByVal y As Integer) As Boolean
    Declare Function GetCursorPos Lib "user32" Alias "GetCursorPos" (ByRef lpPoint As POINTAPI) As Integer
    Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
    End Structure

    Private Declare Function ClipCursor Lib "user32" (ByRef lpRect As RECT) As Long
    Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Dim LeftScreen As Boolean = False
    Dim RightScreen As Boolean = False

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Button1.Image = Nothing
        Button2.Image = Nothing
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If LeftScreen Then
            Button1.Image = Nothing
            'Button1.Text = "1680x1050"
            LeftScreen = False
        Else
            Button1.Image = My.Resources.lock
            'Button1.Text = ""
            LeftScreen = True
        End If
        SetRange()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If RightScreen Then
            Button2.Image = Nothing
            'Button2.Text = "1280x1024"
            RightScreen = False
        Else
            Button2.Image = My.Resources.lock
            'Button2.Text = ""
            RightScreen = True
        End If
        SetRange()
    End Sub

    Sub SetRange()
        Dim r As RECT

        If LeftScreen Then
            r.Left = 0
        Else
            r.Left = -1680
        End If

        If RightScreen Then
            r.Right = 1920
        Else
            r.Right = 1920 + 1280
        End If

        r.Top = 0
        r.Bottom = 1080

        ClipCursor(r)
    End Sub
End Class
