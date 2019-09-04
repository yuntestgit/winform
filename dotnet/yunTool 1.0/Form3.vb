Public Class Form3

    Private Sub Form3_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = -15000
        Me.Left = -15000
    End Sub

    Dim path As String

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If Me.Text <> "yunTool - Accepter" Then
            Timer1.Stop()
            path = Me.Text
            Me.Text = "yunTool - Accepter"
            Process.Start(path)
            Timer1.Start()
        End If
    End Sub
End Class