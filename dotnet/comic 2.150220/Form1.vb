Public Class Form1

    Dim pic As PictureBox = Nothing

    Public Declare Function GetClientRect Lib "user32 " (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer

    Structure RECT '視窗大小資料結構
        Dim x1 As Integer
        Dim y1 As Integer
        Dim x2 As Integer
        Dim y2 As Integer
    End Structure

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        pic = New PictureBox
        With pic
            .BackColor = Color.Black
            .Width = Me.Width / 2
            .Height = Me.Height / 2
            .Left = (Me.Width - pic.Width) / 2
            .Top = (Me.Height - pic.Height) / 2
        End With
        AddHandler pic.Resize, AddressOf pic_Resize
        Me.Controls.Add(pic)
        pic.Show()
    End Sub


    Sub pic_Resize()
        If Not IsNothing(pic) Then
            pic.Left = (Me.Width - pic.Width) / 2
            pic.Top = (Me.Height - pic.Height) / 2
        End If
    End Sub

    Dim firstResize As Boolean = False
    Dim maxmode As Boolean

    Private Sub Form1_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.F11 Then
            If Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None Then
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                If maxmode Then
                    Me.WindowState = FormWindowState.Maximized
                Else
                    Me.WindowState = FormWindowState.Normal
                End If
            Else
                If Me.WindowState = FormWindowState.Maximized Then
                    maxmode = True
                Else
                    maxmode = False
                End If
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                Me.WindowState = FormWindowState.Normal
                Me.WindowState = FormWindowState.Maximized
            End If
        End If
    End Sub

    Private Sub Form1_Resize() Handles MyBase.Resize
        If firstResize = False Then
            If Me.WindowState = FormWindowState.Maximized Then
                maxmode = True
            Else
                maxmode = False
            End If
            firstResize = True
        End If

        If Not IsNothing(pic) Then
            pic.Left = (Me.Width - pic.Width) / 2
            pic.Top = (Me.Height - pic.Height) / 2
        End If
        Me.AutoScroll = False
        Me.AutoScroll = True

        'Dim r As RECT
        'GetClientRect(Me.Handle, r)

        'Panel1.Top = 0
        'Panel1.Width = r.x2
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If (m.Msg = &H112) Then
            If (m.WParam = &HF020) Then '最小化

            ElseIf (m.WParam = &HF030) Then '最大化

            ElseIf (m.WParam = &HF060) Then '關閉

            ElseIf (m.WParam = &HF120) Then '最大化還原
                'Me.Text = "1"
            End If

        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim r As RECT
        GetClientRect(Me.Handle, r)

        Dim w As Integer = Me.Width - r.x2
        'MsgBox(r.y1)
        MsgBox(Me.VerticalScroll.Value)
    End Sub

    Private Sub Form1_Scroll(sender As System.Object, e As System.Windows.Forms.ScrollEventArgs) Handles MyBase.Scroll
        ' Panel1.Top = Me.VerticalScroll.Value
        MsgBox(1)
    End Sub
End Class
