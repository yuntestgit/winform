Public Class Form1

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim h As Integer = Hour(Now)
        Dim m As Integer = Minute(Now)
        Dim s As Integer = Second(Now)
        Me.Text = h & ":" & m & ":" & s

        If h = 6 And m = 30 And s = 0 Then
            PlayMidiFile("mp3.mp3")
        End If
        'If Hour(Now) = 8 And Minute() = 0 And sr Then
    End Sub

    Private Declare Function mciSendStringA Lib "winmm.dll" _
       (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, _
       ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer

    Private Function PlayMidiFile(ByVal MusicFile As String) As Boolean
        If System.IO.File.Exists(MusicFile) Then
            mciSendStringA("stop music", "", 0, 0)
            mciSendStringA("close music", "", 0, 0)
            mciSendStringA("open " & MusicFile & " alias music", "", 0, 0)
            PlayMidiFile = mciSendStringA("play music", "", 0, 0) = 0
        End If
        Return False
    End Function

    Private Function StopMidi() As Boolean
        StopMidi = mciSendStringA("stop music", "", 0, 0) = 0
        mciSendStringA("close music", "", 0, 0)
    End Function

    Private Function PauseMidi() As Boolean
        Return mciSendStringA("pause music", "", 0, 0) = 0
    End Function

    Private Function ContinueMidi() As Boolean
        Return mciSendStringA("play music", "", 0, 0) = 0
    End Function
End Class
