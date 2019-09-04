Public Class Form1

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        '02:47:41,725 --> 02:47:47,186
        Dim plustime As String = TextBox1.Text

        Dim out As String = ""

        For i = 0 To RichTextBox1.Lines.Count - 1
            If RichTextBox1.Lines(i).IndexOf("-->") <> -1 Then
                Dim s() As String = RichTextBox1.Lines(i).Split(" ")
                Dim r1 As String = timePlus(s(0), plustime)
                Dim r2 As String = timePlus(s(2), plustime)
                out &= r1 & " --> " & r2 & vbNewLine
            Else
                If i = RichTextBox1.Lines.Count - 1 Then
                    out &= RichTextBox1.Lines(i)
                Else
                    out &= RichTextBox1.Lines(i) & vbNewLine
                End If
            End If
            Dim per As Double = i / (RichTextBox1.Lines.Count - 1) * 100
            ProgressBar1.Value = Int(per)
        Next
        RichTextBox1.Text = out
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim subtracktime As String = TextBox1.Text

        Dim out As String = ""

        For i = 0 To RichTextBox1.Lines.Count - 1
            If RichTextBox1.Lines(i).IndexOf("-->") <> -1 Then
                Dim s() As String = RichTextBox1.Lines(i).Split(" ")
                Dim r1 As String = timesubtrack(s(0), subtracktime)
                Dim r2 As String = timesubtrack(s(2), subtracktime)
                out &= r1 & " --> " & r2 & vbNewLine
            Else
                If i = RichTextBox1.Lines.Count - 1 Then
                    out &= RichTextBox1.Lines(i)
                Else
                    out &= RichTextBox1.Lines(i) & vbNewLine
                End If
            End If
            Dim per As Double = i / (RichTextBox1.Lines.Count - 1) * 100
            ProgressBar1.Value = Int(per)
        Next
        RichTextBox1.Text = out
    End Sub

    Function timePlus(orgtime As String, plustime As String) As String
        Dim ex_org() As String = orgtime.Split(",")

        Dim hour1 As String = ex_org(0).Split(":")(0)
        Dim minute1 As String = ex_org(0).Split(":")(1)
        Dim second1 As String = ex_org(0).Split(":")(2)
        Dim minisecond1 As String = ex_org(1)

        Dim ex_plus() As String = plustime.Split(",")

        Dim hour2 As String = ex_plus(0).Split(":")(0)
        Dim minute2 As String = ex_plus(0).Split(":")(1)
        Dim second2 As String = ex_plus(0).Split(":")(2)
        Dim minisecond2 As String = ex_plus(1)

        Dim hour As Integer = Val(hour1) + Val(hour2)
        Dim minute As Integer = Val(minute1) + Val(minute2)
        Dim second As Integer = Val(second1) + Val(second2)
        Dim minisecond As Integer = Val(minisecond1) + Val(minisecond2)

        While minisecond >= 1000
            minisecond -= 1000
            second += 1
        End While

        While second >= 60
            second -= 60
            minute += 1
        End While

        While minute >= 60
            minute -= 60
            hour += 1
        End While

        Dim rstr As String = tolen(hour, 2) & ":" & tolen(minute, 2) & ":" & tolen(second, 2) & "," & tolen(minisecond, 3)
        Return rstr
    End Function

    Function timesubtrack(orgtime As String, plustime As String) As String
        Dim ex_org() As String = orgtime.Split(",")

        Dim hour1 As String = ex_org(0).Split(":")(0)
        Dim minute1 As String = ex_org(0).Split(":")(1)
        Dim second1 As String = ex_org(0).Split(":")(2)
        Dim minisecond1 As String = ex_org(1)

        Dim ex_plus() As String = plustime.Split(",")

        Dim hour2 As String = ex_plus(0).Split(":")(0)
        Dim minute2 As String = ex_plus(0).Split(":")(1)
        Dim second2 As String = ex_plus(0).Split(":")(2)
        Dim minisecond2 As String = ex_plus(1)

        Dim hour As Integer = Val(hour1) - Val(hour2)
        Dim minute As Integer = Val(minute1) - Val(minute2)
        Dim second As Integer = Val(second1) - Val(second2)
        Dim minisecond As Integer = Val(minisecond1) - Val(minisecond2)

        While minisecond < 0
            minisecond += 1000
            second -= 1
        End While

        While second < 0
            second += 60
            minute -= 1
        End While

        While minute < 0
            minute += 60
            hour -= 1
        End While

        Dim rstr As String = tolen(hour, 2) & ":" & tolen(minute, 2) & ":" & tolen(second, 2) & "," & tolen(minisecond, 3)
        Return rstr
    End Function

    Function tolen(target As Integer, len As Integer) As String
        Dim rstr As String = target.ToString
        While rstr.Length < len
            rstr = "0" & rstr
        End While
        Return rstr
    End Function
End Class
