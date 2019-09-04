Public Class Form1

    Dim d As New Keyhandler

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'd = New Keyhandler
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        ListBox1.Items.Add(d.State)
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1
    End Sub
End Class

Class Keyhandler
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    Public State As Integer = 2

    Private detect As New Timer

    Sub New()
        AddHandler detect.Tick, AddressOf detect_tick
        'detect = New Timer
        detect.Interval = 1
        detect.Start()
        'MsgBox(0)
    End Sub

    Private Sub detect_tick()
        If GetAsyncKeyState(Keys.Space) <> 0 Then
            Me.State = 1
        Else
            Me.State = 0
        End If
        'MsgBox(0)
    End Sub
End Class