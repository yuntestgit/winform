Public Class Form1

    Dim btn(10) As Button
    Dim lab(10) As Label

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        For i = 0 To 10
            btn(i) = New Button
            With btn(i)
                .Top = 10 + 25 * i
                .Left = 10
                .Text = "Button" & i
                .Tag = i
            End With

            lab(i) = New Label
            With lab(i)
                .Top = 10 + 25 * i
                .Left = 100
                .Text = "Label" & i & ".Text"
            End With

            Me.Controls.Add(lab(i))
            Me.Controls.Add(btn(i))
            AddHandler btn(i).Click, AddressOf Button_Click
        Next
    End Sub

    Sub Button_Click(sender As System.Object, e As System.EventArgs)
        Dim index As Integer = Val(CType(sender, Button).Tag)
        MsgBox(lab(index).Text)
    End Sub
End Class
