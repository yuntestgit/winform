Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim c As HTML.Condition
        c._Property = ""
        c._Value = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim html As New HTML(RichTextBox1.Text)
        'Dim content() As String
        'content = html.GetMid("tr")
        'For i = 0 To content.Length - 1
        '    RichTextBox2.Text &= content(i) & vbNewLine & "vbnewline" & vbNewLine
        'Next

        'Dim html As New HTML(RichTextBox1.Text)
        'Dim c As HTML.Condition
        'c._Property = "class"
        'c._Value = "a"
        'Dim content() As String
        'content = html.GetMid("td", c)
        'For i = 0 To content.Length - 1
        '    RichTextBox2.Text &= content(i) & vbNewLine & "vbnewline" & vbNewLine
        'Next

        'Dim html As New HTML(RichTextBox1.Text)
        'Dim c(1) As HTML.Condition
        'c(0)._Property = "class"
        'c(0)._Value = "a"
        'c(1)._Property = "width"
        'c(1)._Value = "500"
        'Dim content() As String
        'content = html.GetMid("td", c)
        'For i = 0 To content.Length - 1
        '    RichTextBox2.Text &= content(i) & vbNewLine & "vbnewline" & vbNewLine
        'Next


        'Dim html As New HTML(RichTextBox1.Text)
        'Dim content() As String
        'content = html.GetValue("td", "width")
        'For i = 0 To content.Length - 1
        '    RichTextBox2.Text &= content(i) & vbNewLine & "vbnewline" & vbNewLine
        'Next

        'Dim html As New HTML(RichTextBox1.Text)
        'Dim c As HTML.Condition
        'c._Property = "class"
        'c._Value = "a"
        'Dim content() As String
        'content = html.GetValue("td", c, "width")
        'For i = 0 To content.Length - 1
        '    RichTextBox2.Text &= content(i) & vbNewLine & "vbnewline" & vbNewLine
        'Next

        'Dim html As New HTML(RichTextBox1.Text)
        'Dim c(1) As HTML.Condition
        'c(0)._Property = "class"
        'c(0)._Value = "a"
        'c(1)._Property = "width"
        'c(1)._Value = "500"
        'Dim content() As String
        'content = html.GetValue("td", c, "height")
        'For i = 0 To content.Length - 1
        '    RichTextBox2.Text &= content(i) & vbNewLine & "vbnewline" & vbNewLine
        'Next

        Dim html As New HTML("http://www.comicvip.com/html/103.html", "big5")
        RichTextBox2.Text = html.GetCode
    End Sub
End Class

Public Class HTML
    Private Code As String

    Public Structure Condition
        Public _Property As String
        Public _Value As String
    End Structure

#Region "New"
    Public Sub New(ByVal code As String)
        Me.Code = code
    End Sub

    Public Sub New(ByVal url As String, ByVal coding As String)
        Dim Req As Net.WebRequest
        Dim ReceiveStream As IO.Stream
        Dim encode As System.Text.Encoding = System.Text.Encoding.UTF8
        Dim sr As IO.StreamReader
        Dim result As Net.WebResponse
        Req = Net.WebRequest.Create(url)
        result = Req.GetResponse()
        ReceiveStream = result.GetResponseStream

        If coding = "utf8" Then
            encode = System.Text.Encoding.UTF8
        ElseIf coding = "big5" Then
            encode = System.Text.Encoding.GetEncoding("big5")
        End If
        sr = New IO.StreamReader(ReceiveStream, encode)
        Me.Code = sr.ReadToEnd()
    End Sub
#End Region

    Public Function GetCode() As String
        Return Code
    End Function

    Private Function CopyString(ByVal Target As String, ByVal Start As String, ByVal Length As Integer) As String
        Return Mid(Target, Start + 1, Length)
    End Function

    ' chr(34) = "  , chr(39) = '

#Region "GetMid"
    Public Function GetMid(ByVal Tag As String) As String()
        Dim index As Integer = 0
        Dim tempStr(1024 ^ 2) As String

        Dim startTag As String = "<" & Tag & ">"
        Dim endTag As String = "</" & Tag & ">"

        Dim last As Integer = 0
        Dim tail As Integer

        While (True)
            last = Code.IndexOf(startTag, last + startTag.Length)

            If last <> -1 Then
                tail = Code.IndexOf(endTag, last + startTag.Length)
                tempStr(index) = CopyString(Code, last + startTag.Length, tail - last - startTag.Length)
                index += 1
            Else
                Exit While
            End If
        End While


        Dim rStr(index - 1) As String

        For i = 0 To rStr.Length - 1
            rStr(i) = tempStr(i)
        Next

        Return rStr
    End Function

    Public Function GetMid(ByVal Tag As String, ByVal Condition As HTML.Condition) As String()
        Dim index As Integer = 0
        Dim tempStr(1024 ^ 2) As String

        Dim startTag As String = "<" & Tag
        Dim endTag As String = "</" & Tag & ">"

        Dim c1 As String = Condition._Property & "=" & Chr(34) & Condition._Value & Chr(34)
        Dim c2 As String = Condition._Property & "=" & Chr(39) & Condition._Value & Chr(39)

        Dim last As Integer = 0
        Dim tail As Integer

        Dim startTail As Integer
        Dim allProperty As String

        While (True)
            last = Code.IndexOf(startTag, last + startTag.Length)

            If last <> -1 Then
                tail = Code.IndexOf(endTag, last + startTag.Length)

                startTail = Code.IndexOf(">", last + startTag.Length)
                allProperty = CopyString(Code, last + startTag.Length, startTail - last - startTag.Length)

                If allProperty.IndexOf(c1) <> -1 Or allProperty.IndexOf(c2) <> -1 Then
                    tempStr(index) = CopyString(Code, startTail + 1, tail - (startTail + 1))
                    index += 1
                End If
            Else
                Exit While
            End If
        End While


        Dim rStr(index - 1) As String

        For i = 0 To rStr.Length - 1
            rStr(i) = tempStr(i)
        Next

        Return rStr
    End Function

    Public Function GetMid(ByVal Tag As String, ByVal Condition As HTML.Condition()) As String()
        Dim index As Integer = 0
        Dim tempStr(1024 ^ 2) As String

        Dim startTag As String = "<" & Tag
        Dim endTag As String = "</" & Tag & ">"

        Dim c1(Condition.Length - 1) As String
        Dim c2(Condition.Length - 1) As String

        For i = 0 To Condition.Length - 1
            c1(i) = Condition(i)._Property & "=" & Chr(34) & Condition(i)._Value & Chr(34)
            c2(i) = Condition(i)._Property & "=" & Chr(39) & Condition(i)._Value & Chr(39)
        Next

        Dim last As Integer = 0
        Dim tail As Integer

        Dim startTail As Integer
        Dim allProperty As String

        While (True)
            last = Code.IndexOf(startTag, last + startTag.Length)

            If last <> -1 Then
                tail = Code.IndexOf(endTag, last + startTag.Length)

                startTail = Code.IndexOf(">", last + startTag.Length)
                allProperty = CopyString(Code, last + startTag.Length, startTail - last - startTag.Length)

                Dim allInclude As Boolean = True
                For i = 0 To Condition.Length - 1
                    If allProperty.IndexOf(c1(i)) = -1 And allProperty.IndexOf(c2(i)) = -1 Then
                        allInclude = False
                    End If
                Next

                If allInclude = True Then
                    tempStr(index) = CopyString(Code, startTail + 1, tail - (startTail + 1))
                    index += 1
                End If
            Else
                Exit While
            End If
        End While


        Dim rStr(index - 1) As String

        For i = 0 To rStr.Length - 1
            rStr(i) = tempStr(i)
        Next

        Return rStr
    End Function
#End Region

#Region "GetValue"
    Public Function GetValue(ByVal Tag As String, ByVal _Property As String) As String()
        Dim index As Integer = 0
        Dim tempStr(1024 ^ 2) As String

        Dim startTag As String = "<" & Tag

        Dim last As Integer = 0

        Dim startTail As Integer
        Dim allProperty As String

        Dim p1 As String = _Property & "=" & Chr(34)
        Dim p2 As String = _Property & "=" & Chr(39)

        Dim TailString As String = ""
        Dim ValueStart As Integer
        Dim ValueTail As Integer

        While (True)
            last = Code.IndexOf(startTag, last + startTag.Length)

            If last <> -1 Then
                startTail = Code.IndexOf(">", last + startTag.Length)
                allProperty = CopyString(Code, last + startTag.Length, startTail - last - startTag.Length)

                If allProperty.IndexOf(p1) <> -1 Then
                    TailString = Chr(34)
                    ValueStart = allProperty.IndexOf(p1) + p1.Length
                End If

                If allProperty.IndexOf(p2) <> -1 Then
                    TailString = Chr(39)
                    ValueStart = allProperty.IndexOf(p2) + p2.Length
                End If

                If allProperty.IndexOf(p1) <> -1 Or allProperty.IndexOf(p2) <> -1 Then
                    ValueTail = allProperty.IndexOf(TailString, ValueStart)
                    tempStr(index) = CopyString(allProperty, ValueStart, ValueTail - ValueStart)
                    index += 1
                End If
            Else
                Exit While
            End If
        End While


        Dim rStr(index - 1) As String

        For i = 0 To rStr.Length - 1
            rStr(i) = tempStr(i)
        Next

        Return rStr
    End Function

    Public Function GetValue(ByVal Tag As String, ByVal Condition As HTML.Condition, ByVal _Property As String) As String()
        Dim index As Integer = 0
        Dim tempStr(1024 ^ 2) As String

        Dim startTag As String = "<" & Tag

        Dim last As Integer = 0

        Dim startTail As Integer
        Dim allProperty As String

        Dim c1 As String = Condition._Property & "=" & Chr(34) & Condition._Value & Chr(34)
        Dim c2 As String = Condition._Property & "=" & Chr(39) & Condition._Value & Chr(39)

        Dim p1 As String = _Property & "=" & Chr(34)
        Dim p2 As String = _Property & "=" & Chr(39)

        Dim TailString As String = ""
        Dim ValueStart As Integer
        Dim ValueTail As Integer

        While (True)
            last = Code.IndexOf(startTag, last + startTag.Length)

            If last <> -1 Then
                startTail = Code.IndexOf(">", last + startTag.Length)
                allProperty = CopyString(Code, last + startTag.Length, startTail - last - startTag.Length)

                If allProperty.IndexOf(c1) <> -1 Or allProperty.IndexOf(c2) <> -1 Then
                    If allProperty.IndexOf(p1) <> -1 Then
                        TailString = Chr(34)
                        ValueStart = allProperty.IndexOf(p1) + p1.Length
                    End If

                    If allProperty.IndexOf(p2) <> -1 Then
                        TailString = Chr(39)
                        ValueStart = allProperty.IndexOf(p2) + p2.Length
                    End If

                    If allProperty.IndexOf(p1) <> -1 Or allProperty.IndexOf(p2) <> -1 Then
                        ValueTail = allProperty.IndexOf(TailString, ValueStart)
                        tempStr(index) = CopyString(allProperty, ValueStart, ValueTail - ValueStart)
                        index += 1
                    End If
                End If
            Else
                Exit While
            End If
        End While


        Dim rStr(index - 1) As String

        For i = 0 To rStr.Length - 1
            rStr(i) = tempStr(i)
        Next

        Return rStr
    End Function

    Public Function GetValue(ByVal Tag As String, ByVal Condition As HTML.Condition(), ByVal _Property As String) As String()
        Dim index As Integer = 0
        Dim tempStr(1024 ^ 2) As String

        Dim startTag As String = "<" & Tag

        Dim last As Integer = 0

        Dim startTail As Integer
        Dim allProperty As String

        Dim c1(Condition.Length - 1) As String
        Dim c2(Condition.Length - 1) As String

        For i = 0 To Condition.Length - 1
            c1(i) = Condition(i)._Property & "=" & Chr(34) & Condition(i)._Value & Chr(34)
            c2(i) = Condition(i)._Property & "=" & Chr(39) & Condition(i)._Value & Chr(39)
        Next

        Dim p1 As String = _Property & "=" & Chr(34)
        Dim p2 As String = _Property & "=" & Chr(39)

        Dim TailString As String = ""
        Dim ValueStart As Integer
        Dim ValueTail As Integer

        While (True)
            last = Code.IndexOf(startTag, last + startTag.Length)

            If last <> -1 Then
                startTail = Code.IndexOf(">", last + startTag.Length)
                allProperty = CopyString(Code, last + startTag.Length, startTail - last - startTag.Length)

                Dim allInclude As Boolean = True
                For i = 0 To Condition.Length - 1
                    If allProperty.IndexOf(c1(i)) = -1 And allProperty.IndexOf(c2(i)) = -1 Then
                        allInclude = False
                    End If
                Next

                If allInclude = True Then
                    If allProperty.IndexOf(p1) <> -1 Then
                        TailString = Chr(34)
                        ValueStart = allProperty.IndexOf(p1) + p1.Length
                    End If

                    If allProperty.IndexOf(p2) <> -1 Then
                        TailString = Chr(39)
                        ValueStart = allProperty.IndexOf(p2) + p2.Length
                    End If

                    If allProperty.IndexOf(p1) <> -1 Or allProperty.IndexOf(p2) <> -1 Then
                        ValueTail = allProperty.IndexOf(TailString, ValueStart)
                        tempStr(index) = CopyString(allProperty, ValueStart, ValueTail - ValueStart)
                        index += 1
                    End If
                End If
            Else
                Exit While
            End If
        End While


        Dim rStr(index - 1) As String

        For i = 0 To rStr.Length - 1
            rStr(i) = tempStr(i)
        Next

        Return rStr
    End Function
#End Region

End Class