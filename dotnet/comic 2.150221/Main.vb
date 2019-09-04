Public Class Main

    Dim lister1 As Panel

    Dim testindex As Integer = 20
    Dim test1(testindex) As String
    Dim test2(testindex) As String

    Dim comic_button(testindex) As Label

    Private Sub Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Show_lister1()

        For i = 0 To test1.Length - 1
            test1(i) = "漫畫" & i
        Next

        For i = 0 To test2.Length - 1
            test2(i) = "10" & i
        Next

        lister1_add_item()
    End Sub

    Sub Show_lister1()
        lister1 = New Panel
        With lister1
            .BackColor = Color.Black
            .Width = 300
            .Height = 300
            .Top = 0
            .Left = 0
            .Font = New Font("新細明體", 15)
        End With

        Me.Controls.Add(lister1)
    End Sub

    Sub lister1_add_item()
        Dim maxwidth As Integer = 0
        Dim h As Integer

        For i = 0 To test1.Length - 1
            'MsgBox(i)

            comic_button(i) = New Label

            With comic_button(i)
                .BackColor = Color.Red
                .ForeColor = Color.Green
                .AutoSize = True
                .Top = 30 * i + 5
                .Left = 5
                .Text = test1(i)
                .Tag = test2(i)
            End With
            AddHandler comic_button(i).Click, AddressOf comic_button_click
            Me.Controls.Add(comic_button(i))
            comic_button(i).Parent = lister1
            If comic_button(i).Width > maxwidth Then
                maxwidth = comic_button(i).Width
            End If
            h += comic_button(i).Height
        Next

        lister1.Width = maxwidth + 10

        lister1.Height = test1.Length * 10 + h
    End Sub

    Sub comic_button_click(sender As System.Object, e As System.EventArgs)
        MsgBox(CType(sender, Label).Tag)
    End Sub
End Class
