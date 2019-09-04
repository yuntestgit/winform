Public Class Form1
    Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (hwnd As Integer, lpString As String) As Integer

    Dim index As Integer = 19

    Dim chbox(index) As CheckBox
    Dim showtext(index) As TextBox
    Dim showtext1(index) As TextBox
    Dim showtext2(index) As TextBox
    Dim showtext3(index) As TextBox
    Dim btn(index) As Button

    Dim boxText(index) As String

    Dim str(index) As String

    Dim str1(index) As String
    Dim str2(index) As String
    Dim str3(index) As String



    'Grand Fantasia

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = 1035 - Me.Height
        Me.Left = 5

        Dim i As Integer = 0
        boxText(i) = "box0" : str(i) = "xuan0511" : str1(i) = "魔劍姬" : str2(i) = "" : str3(i) = "" : i += 1
        boxText(i) = "box1" : str(i) = "yu1102" : str1(i) = "夏夢柔" : str2(i) = "白礦石倉庫" : str3(i) = "野豬騎士" : i += 1
        boxText(i) = "box2" : str(i) = "ling0904" : str1(i) = "冬夢玲" : str2(i) = "白草木倉庫" : str3(i) = "白獵物倉庫" : i += 1
        boxText(i) = "box3" : str(i) = "ting0227" : str1(i) = "日記" : str2(i) = "心臟剝離" : str3(i) = "" : i += 1
        boxText(i) = "box4" : str(i) = "ailin0609" : str1(i) = "秋芷雨" : str2(i) = "橘C倉庫" : str3(i) = "橘材王" : i += 1
        boxText(i) = "box5" : str(i) = "small0513" : str1(i) = "春芷雪" : str2(i) = "徽章王" : str3(i) = "小牛倉庫" : i += 1
        boxText(i) = "box6" : str(i) = "qazidy" : str1(i) = "依呀依呀呦" : str2(i) = "工會會長" : str3(i) = "暫時倉庫1號" : i += 1
        boxText(i) = "box7" : str(i) = "qazidy2" : str1(i) = "記豪" : str2(i) = "雜倉庫" : str3(i) = "鳴石符文倉庫" : i += 1
        boxText(i) = "box8" : str(i) = "qazidy3" : str1(i) = "ExUsial祤" : str2(i) = "baby小炎超慢" : str3(i) = "禿仔" : i += 1
        boxText(i) = "box9" : str(i) = "" : str1(i) = "" : str2(i) = "" : str3(i) = "" : i += 1
        boxText(i) = "boxa0" : str(i) = "temp0001" : str1(i) = "力量倉庫" : str2(i) = "天賦倉庫" : str3(i) = "轉生天賦倉庫" : i += 1
        boxText(i) = "boxa1" : str(i) = "temp0002" : str1(i) = "綠材倉庫" : str2(i) = "布雷克霸克斯" : str3(i) = "小奈倉庫" : i += 1
        boxText(i) = "boxa2" : str(i) = "temp0003" : str1(i) = "拆解老師" : str2(i) = "藥水老師" : str3(i) = "卷軸老師" : i += 1
        boxText(i) = "boxa3" : str(i) = "temp0004" : str1(i) = "碎片老師" : str2(i) = "戒指老師" : str3(i) = "橘材老師" : i += 1
        boxText(i) = "boxa4" : str(i) = "temp0005" : str1(i) = "天賦老師" : str2(i) = "" : str3(i) = "" : i += 1
        boxText(i) = "boxa5" : str(i) = "temp0006" : str1(i) = "" : str2(i) = "" : str3(i) = "" : i += 1
        boxText(i) = "boxa6" : str(i) = "temp0007" : str1(i) = "" : str2(i) = "" : str3(i) = "" : i += 1
        boxText(i) = "boxa7" : str(i) = "temp0008" : str1(i) = "" : str2(i) = "" : str3(i) = "" : i += 1
        boxText(i) = "boxa8" : str(i) = "temp0009" : str1(i) = "" : str2(i) = "" : str3(i) = "" : i += 1
        boxText(i) = "boxa9" : str(i) = "" : str1(i) = "" : str2(i) = "" : str3(i) = "" : i += 1


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
            .Top = 585
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

            showtext(i) = New TextBox
            With showtext(i)
                .ReadOnly = True
                .Text = str(i)
                .Left = _left + 20
                .Top = 15 + 30 * i
                .Width = 80
                AddHandler showtext(i).MouseDown, AddressOf showtext_click
            End With

            showtext1(i) = New TextBox
            With showtext1(i)
                .ReadOnly = True
                .Text = str1(i)
                .Left = _left + 120
                .Top = 15 + 30 * i
                .Width = 80
                AddHandler showtext1(i).MouseDown, AddressOf showtext_click
            End With

            showtext2(i) = New TextBox
            With showtext2(i)
                .ReadOnly = True
                .Text = str2(i)
                .Left = _left + 220
                .Top = 15 + 30 * i
                .Width = 80
                AddHandler showtext2(i).MouseDown, AddressOf showtext_click
            End With

            showtext3(i) = New TextBox
            With showtext3(i)
                .ReadOnly = True
                .Text = str3(i)
                .Left = _left + 320
                .Top = 15 + 30 * i
                .Width = 80
                AddHandler showtext3(i).MouseDown, AddressOf showtext_click
            End With

            btn(i) = New Button
            With btn(i)
                .Text = boxText(i)
                .Left = _left + 420
                .Top = 15 + 30 * i
                AddHandler btn(i).Click, AddressOf btn_click
            End With

            Me.Controls.Add(chbox(i))
            Me.Controls.Add(showtext(i))
            Me.Controls.Add(showtext1(i))
            Me.Controls.Add(showtext2(i))
            Me.Controls.Add(showtext3(i))
            Me.Controls.Add(btn(i))
        Next
    End Sub

    Sub btn_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim box As String = DirectCast((sender), Button).Text
        Process.Start("C:\Program Files\Sandboxie\Start.exe", "/box:" & box & " C:\Users\yun\Desktop\Open.lnk")
    End Sub

    Sub showtext_click(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        sender.SelectAll()
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
