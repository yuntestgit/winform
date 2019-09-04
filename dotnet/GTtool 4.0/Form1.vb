Public Class Form1
    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Public Const WM_HOTKEY As Integer = &H312

    Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As IntPtr)
    Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer


    Dim index As Integer = 11
    Dim check(index) As CheckBox
    Dim title(index) As String
    Dim kcode(index) As Integer

    Dim open As Boolean = False

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        hotkey(0, 192)
        'hotkey(1, &H6B)
        hotkey(2, Keys.Home)
        hotkey(3, Keys.End)
        hotkey(4, Keys.Insert)

        For i = 1 To 9
            title(i - 1) = i.ToString
            kcode(i - 1) = Keys.D1 + i - 1
        Next
        title(9) = "0" : kcode(9) = Keys.D0
        title(10) = "Space" : kcode(10) = Keys.Space
        title(11) = "=" : kcode(11) = 187 '=

        For i = 0 To index
            check(i) = New CheckBox
            With check(i)
                .Text = title(i)
                .Left = 10
                .Top = 10 + i * 23
            End With
            Me.Controls.Add(check(i))
        Next
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        For i = 0 To index
            If check(i).Checked Then
                PressKey(kcode(i))
            End If
        Next
    End Sub

    Sub PressKey(key As Integer)
        keybd_event(key, MapVirtualKey(key, 0), 0, 0)
        System.Threading.Thread.Sleep(10)
        keybd_event(key, MapVirtualKey(key, 0), &H2, 0)
    End Sub

    Sub key_double()
        For i = 0 To 10
            PressKey(222)
        Next
        For i = 0 To 180
            If i Mod 2 = 0 Then
                PressKey(219)
            Else
                PressKey(221)
            End If
        Next
        If CheckBox1.Checked Then
            System.Media.SystemSounds.Asterisk.Play()
        End If
    End Sub

    Sub key_run()
        For i = 0 To 10
            PressKey(222)
        Next
        If CheckBox1.Checked Then
            System.Media.SystemSounds.Exclamation.Play()
        End If
    End Sub

    Sub key_shield()
        For i = 0 To 10
            PressKey(222)
        Next
        For i = 0 To 180
            If i Mod 2 = 0 Then
                PressKey(219)
            Else
                PressKey(220)
            End If
        Next
        If CheckBox1.Checked Then
            System.Media.SystemSounds.Beep.Play()
        End If
    End Sub

    Sub switch()
        If open = False Then
            open = True
            Timer1.Start()
        Else
            open = False
            Timer1.Stop()
        End If
    End Sub

    Sub hotkey(id As Integer, key As Integer)
        RegisterHotKey(Me.Handle, id, 0, key)
    End Sub

    Sub unhotkey(id As Integer)
        UnregisterHotKey(Me.Handle, id)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case id
                Case 0
                    switch()
                Case 1
                    switch()
                Case 2
                    key_double()
                Case 3
                    key_run()
                Case 4
                    key_shield()
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "SetTopMost" Then
            Me.TopMost = True
            Button1.Text = "UnTopMost"
        Else
            Me.TopMost = False
            Button1.Text = "SetTopMost"
        End If
    End Sub

    Private Sub TrackBar1_Scroll(sender As System.Object, e As System.EventArgs) Handles TrackBar1.Scroll
        Me.Opacity = (TrackBar1.Value + 5) / 10
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        unhotkey(0)
        'unhotkey(1)
        unhotkey(2)
        unhotkey(3)
        unhotkey(4)
    End Sub
End Class