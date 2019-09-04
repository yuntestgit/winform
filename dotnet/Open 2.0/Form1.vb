Public Class Form1
    Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (hwnd As Integer, lpString As String) As Integer

    Dim index As Integer = 8

    Dim chbox(index) As CheckBox
    Dim acc(index) As TextBox
    Dim acc2(index) As TextBox
    Dim str(index) As String
    Dim str2(index) As String
    Dim btn(index) As Button

    'Grand Fantasia

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = 1035 - Me.Height
        Me.Left = 5

        str(0) = "xuan0511"
        str(1) = "yu1102"
        str(2) = "ling0904"
        str(3) = "ting0227"
        str(4) = "ailin0609"
        str(5) = "small0513"
        str(6) = "qazidy"
        str(7) = "qazidy2"
        str(8) = "qazidy3"

        str2(0) = "魔劍姬"
        str2(1) = "夏夢柔"
        str2(2) = "冬夢玲"
        str2(3) = "日記"
        str2(4) = "秋芷雨"
        str2(5) = "春芷雪"
        str2(6) = "依呀依呀呦"
        str2(7) = "記豪"
        str2(8) = "禿仔"

        Dim selectAll As New Button
        Dim openArr As New Button

        With selectAll
            .Text = "全選"
            .Top = 15
            .Left = 15
            AddHandler selectAll.Click, AddressOf selectAll_click
        End With
        Me.Controls.Add(selectAll)

        With openArr
            .Text = "開啟勾選"
            .Top = 250
            .Left = 15
            AddHandler openArr.Click, AddressOf openArr_click
        End With
        Me.Controls.Add(openArr)

        Dim _left As Integer = 115
        For i = 0 To index
            chbox(i) = New CheckBox
            With chbox(i)
                .Text = ""
                .Left = _left
                .Top = 15 + 30 * i
                .Width = 15
            End With

            acc(i) = New TextBox
            With acc(i)
                .Text = str(i)
                .Left = _left + 20
                .Top = 15 + 30 * i
                .Width = 80
            End With

            acc2(i) = New TextBox
            With acc2(i)
                .Text = str2(i)
                .Left = _left + 120
                .Top = 15 + 30 * i
                .Width = 80
            End With

            btn(i) = New Button
            With btn(i)
                .Text = "box" & (i + 1).ToString
                .Left = _left + 220
                .Top = 15 + 30 * i
                AddHandler btn(i).Click, AddressOf btn_click
            End With

            Me.Controls.Add(chbox(i))
            Me.Controls.Add(acc(i))
            Me.Controls.Add(acc2(i))
            Me.Controls.Add(btn(i))
        Next
    End Sub

    Sub btn_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim box As String = DirectCast((sender), Button).Text
        Process.Start("C:\Program Files\Sandboxie\Start.exe", "/box:" & box & " C:\Users\yun\Desktop\Open.lnk")
    End Sub

    Sub selectAll_click()
        For i = 0 To index
            If chbox(i).Checked = False Then
                chbox(i).Checked = True
            End If
        Next
    End Sub

    Sub openArr_click()
        For i = 0 To index
            If chbox(i).Checked = True Then
                chbox(i).Checked = False
                Process.Start("C:\Program Files\Sandboxie\Start.exe", "/box:" & btn(i).Text & " C:\Users\yun\Desktop\Open.lnk")
            End If
        Next
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        For i = 0 To index
            Dim h As Integer = FindWindow("Sandbox:" & btn(i).Text & ":DJO_CLASS", vbNullString)
            If h Then
                btn(i).Enabled = False
                SetWindowText(h, "Grand Fantasia #" & btn(i).Text & "#")
            Else
                btn(i).Enabled = True
            End If
        Next
    End Sub
End Class
