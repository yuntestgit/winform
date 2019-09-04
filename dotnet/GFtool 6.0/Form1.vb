Public Class Form1
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Dim index As Integer = 11
    Dim check(index) As CheckBox
    Dim title(index) As String
    Dim kcode(index) As Integer

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        For i = 1 To 9
            title(i - 1) = i.ToString
            kcode(i - 1) = Keys.D1 + i - 1
        Next
        title(9) = "0" : kcode(9) = Keys.D0
        title(10) = "-" : kcode(10) = 189 '-
        title(11) = "=" : kcode(11) = 187 '=

        For i = 0 To index
            check(i) = New CheckBox
            With check(i)
                .Text = title(i)
                .Left = 10
                .Top = 10 + i * 23
            End With
            'Me.Controls.Add(check(i))
            TabPage1.Controls.Add(check(i))
        Next
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        For i = 0 To 1000
            If GetAsyncKeyState(i) <> 0 Then
                Me.Text = i
            Else
                Me.Text = ""
            End If
            'Me.Text = GetAsyncKeyState(i)
        Next

    End Sub
End Class
