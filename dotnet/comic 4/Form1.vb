Public Class Main
    Public Declare Function GetClientRect Lib "user32 " (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer

    Structure RECT
        Dim x1 As Integer
        Dim y1 As Integer
        Dim x2 As Integer
        Dim y2 As Integer
    End Structure

    Dim settings As String
    Dim comic_id() As String
    Dim comic_name() As String
    Dim comic_last() As String
    Dim comic_index As Integer = 0

    Dim contentLister() As ListBox
    Dim UrlLister() As ListBox

    Dim comicCenter() As _8Comic

    Dim imgViewer As Form
    Dim imgShower As PictureBox

    Dim imgsUrl() As String
    Dim pageCount As Integer
    Dim page As Integer
    Dim isloaded As Integer
    Dim img() As Image


#Region "GetSettings"
    Sub GetSettings()
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Application.StartupPath & "\settings.txt", System.Text.Encoding.GetEncoding("big5"))
        settings = sr.ReadToEnd
        sr.Close()

        Dim count As Integer = 0
        Dim last As Integer = 0
        While last <> -1
            last = settings.IndexOf("<$", last)
            If last <> -1 Then
                count += 1
                last += 2
            End If
        End While
        ReDim comic_id(count)
        ReDim comic_name(count)
        ReDim comic_last(count)
        ReDim contentLister(count)
        ReDim UrlLister(count)
        ReDim comicCenter(count)

        last = 0
        While last <> -1
            last = settings.IndexOf("<$", last)
            If last <> -1 Then
                Dim tagstart As Integer = last + 2
                Dim tagend As Integer = settings.IndexOf("/>", tagstart)
                Dim value As String = SubStr(settings, tagstart, tagend - tagstart)
                comic_id(comic_index) = value.Split(",")(0)
                comic_name(comic_index) = value.Split(",")(1)
                comic_last(comic_index) = value.Split(",")(2)
                last = tagend + 2
                comic_index += 1
            End If
        End While
    End Sub

    Function SubStr(str As String, start As Integer, length As Integer) As String
        Return Mid(str, start + 1, length)
    End Function
#End Region

    Private Sub Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim loadingForm As New Form
        Dim pBar As New ProgressBar
        With loadingForm
            .StartPosition = FormStartPosition.CenterScreen
            .Width = 852
            .Height = 606
            .Text = "Comic 4"
        End With
        With pBar
            .Maximum = 100
            .Width = 836
            .Height = 568
        End With
        loadingForm.Controls.Add(pBar)
        loadingForm.Show()

        GetSettings()
        For i = 0 To comic_index - 1
            comicLister.Items.Add(comic_name(i))

            contentLister(i) = New ListBox
            With contentLister(i)
                .Visible = False
                .Width = 600
                .Height = comicLister.Height
                .Anchor = comicLister.Anchor
                .Top = comicLister.Top
                .Left = comicLister.Left * 2 + comicLister.Width
                .Font = New Font("新細明體", 20, FontStyle.Bold)
                .Tag = i
            End With
            AddHandler contentLister(i).DoubleClick, New EventHandler(AddressOf viewer_DoubleClick)
            AddHandler contentLister(i).KeyDown, AddressOf viewer_KeyDown
            Me.Controls.Add(contentLister(i))

            UrlLister(i) = New ListBox
            With UrlLister(i)
                .Visible = False
                .Tag = i
            End With
            Me.Controls.Add(UrlLister(i))

            comicCenter(i) = New _8Comic
            comicCenter(i).SetID(comic_id(i))
            Dim books() As String = comicCenter(i).Books
            If Not IsNothing(books) Then
                Dim booksurl() As String = comicCenter(i).BooksUrl
                For j = 0 To books.Length - 1
                    contentLister(i).Items.Add(IIf(books(j) <> "", books(j), booksurl(j)))
                    UrlLister(i).Items.Add(booksurl(j))
                    If booksurl(j) = comic_last(i) Then
                        contentLister(i).SelectedIndex = contentLister(i).Items.Count - 1
                    End If
                Next
            End If

            Dim sections() As String = comicCenter(i).Sections
            If Not IsNothing(sections) Then
                Dim sectionsurl() As String = comicCenter(i).SectionsUrl
                For j = 0 To sections.Length - 1
                    contentLister(i).Items.Add(IIf(sections(j) <> "", sections(j), sectionsurl(j)))
                    UrlLister(i).Items.Add(sectionsurl(j))
                    If sectionsurl(j) = comic_last(i) Then
                        contentLister(i).SelectedIndex = contentLister(i).Items.Count - 1
                    End If
                Next
            End If
            Dim per As Double = (i + 1) / comic_index
            pBar.Value = Int(per * 100)
        Next
        loadingForm.Close()
    End Sub

    Private Sub comicLister_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles comicLister.SelectedIndexChanged
        For i = 0 To comic_index - 1
            contentLister(i).Visible = False
        Next
        contentLister(comicLister.SelectedIndex).Visible = True
    End Sub

    Private Sub viewer_DoubleClick(sender As System.Object, e As System.EventArgs)
        Dim listbox_index As Integer = Val(CType(sender, ListBox).Tag.ToString)
        Dim viewStr As String = UrlLister(listbox_index).Items(contentLister(listbox_index).SelectedIndex)
        view(listbox_index, viewStr)
    End Sub

    Private Sub viewer_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim listbox_index As Integer = Val(CType(sender, ListBox).Tag.ToString)
            Dim viewStr As String = UrlLister(listbox_index).Items(contentLister(listbox_index).SelectedIndex)
            view(listbox_index, viewStr)
        End If
    End Sub

    Sub view(index As Integer, viewStr As String)
        comicCenter(index).SetView(viewStr)

        imgViewer = New Form
        imgShower = New PictureBox

        With imgViewer
            .Text = comic_name(index) & "-" & viewStr
            .StartPosition = FormStartPosition.CenterScreen
            .WindowState = FormWindowState.Maximized
            .BackColor = Color.Black
            .AutoScroll = True
        End With
        With imgShower
            .BackColor = Color.White
            .Width = 600
            .Height = 600
        End With

        AddHandler imgShower.Resize, AddressOf _Resize
        AddHandler imgViewer.Resize, AddressOf _Resize
        AddHandler imgViewer.KeyDown, AddressOf imgViewer_KeyDown
        AddHandler imgViewer.Click, AddressOf viewer_Click
        AddHandler imgShower.Click, AddressOf viewer_Click

        imgViewer.Controls.Add(imgShower)
        imgViewer.Show()
        _Resize(Nothing, Nothing)

        pageCount = comicCenter(index).ImagesUrl.Length
        ReDim imgsUrl(pageCount)
        imgsUrl = comicCenter(index).ImagesUrl
        ReDim img(pageCount)

        Dim url As String = imgsUrl(0)
        Dim req As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(url), System.Net.HttpWebRequest)
        Dim rsp As System.Net.HttpWebResponse = DirectCast(req.GetResponse(), System.Net.HttpWebResponse)

        Try
            If rsp.StatusCode = System.Net.HttpStatusCode.OK Then
                img(0) = New Bitmap(rsp.GetResponseStream())
            End If
        Finally
            rsp.Close()
        End Try


        page = 0
        changePage()

        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        For i = 1 To pageCount - 1
            Dim url As String = imgsUrl(i)
            Dim req As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(url), System.Net.HttpWebRequest)
            Dim rsp As System.Net.HttpWebResponse = DirectCast(req.GetResponse(), System.Net.HttpWebResponse)

            Try
                If rsp.StatusCode = System.Net.HttpStatusCode.OK Then
                    img(i) = New Bitmap(rsp.GetResponseStream())
                End If
            Finally
                rsp.Close()
            End Try
            isloaded = i
        Next
    End Sub

    Private Sub _Resize(sender As System.Object, e As System.EventArgs)
        If imgViewer.Visible = True Then
            Dim r As RECT
            GetClientRect(imgViewer.Handle, r)
            Dim pw As Integer = r.x2 - r.x1

            If pw > imgShower.Width Then
                imgShower.Left = (pw - imgShower.Width) / 2
            Else
                imgShower.Left = 0
            End If

            imgShower.Top = 0
        End If
    End Sub

    Private Sub imgViewer_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Left Then
            If page >= 1 Then
                pageBack()
            End If
        ElseIf e.KeyCode = Keys.Up Then
            If page >= 1 Then
                pageBack()
            End If
        ElseIf e.KeyCode = Keys.Right Then
            If page < pageCount Then
                pageNext()
            End If
        ElseIf e.KeyCode = Keys.Space Then
            If page < pageCount Then
                pageNext()
            End If
        End If
    End Sub

    Private Sub viewer_Click(sender As System.Object, e As System.EventArgs)
        pageNext()
    End Sub

    Sub pageNext()
        If isloaded >= page + 1 Then
            page += 1
            changePage()
        End If
    End Sub

    Sub pageBack()
        page -= 1
        changePage()
    End Sub

    Sub changePage()
        If Not IsNothing(img(page)) Then
            imgShower.Width = img(page).Width
            imgShower.Height = img(page).Height
            imgShower.Top = 0
            imgShower.Image = img(page)
        End If
    End Sub
End Class

Public Class _8Comic
    Public Books() As String
    Public BooksUrl() As String
    Public Sections() As String
    Public SectionsUrl() As String

    Public script_cs As String

    Public ImagesUrl() As String

    Private comicID As String
    Private CviewUrl As String

    Public Sub SetID(ByVal id As String)
        Me.comicID = id
        Dim html As New HTML("http://www.comicvip.com/html/" & id & ".html", "big5")
        Dim cviewstr As String = "cview('"
        Dim cviewstrend As String = ".html"
        Dim code As String = html.GetCode
        Dim cviewUrl As String = Nothing
        Dim cviewUrlstr As String = ".html',"
        Dim cviewUrlstrend As String = ");return false;"
        Dim cviewUrlstart As Integer
        Dim cviewUrlend As Integer

        Dim book_table_id(1) As String
        book_table_id(0) = "rp_ctl00_comiclist2_dl"
        book_table_id(1) = "rp_comiclist2_0_dl_0"

        Dim section_table_id(1) As String
        section_table_id(0) = "rp_ctl00_comiclist11_dl"
        section_table_id(1) = "rp_comiclist11_0_dl_0"

        For idi = 0 To book_table_id.Length - 1
            If code.IndexOf("<table id=" & Chr(34) & book_table_id(idi) & Chr(34)) <> -1 Then
                Dim book_table_c As HTML.Condition
                book_table_c._Property = "id"
                book_table_c._Value = book_table_id(idi)
                Dim book As String = html.GetMid("table", book_table_c)(0)
                Dim html_book As New HTML(book)
                Dim bookc As HTML.Condition
                bookc._Property = "class"
                bookc._Value = "Vol"

                Dim book_rstr() As String = html_book.GetMid("a", bookc)
                For i = 0 To book_rstr.Length - 1
                    book_rstr(i) = Replace(book_rstr(i), vbNewLine, "")
                    book_rstr(i) = Trim(book_rstr(i))
                Next
                Me.Books = book_rstr

                Dim bookurl_rstr() As String = html_book.GetValue("a", bookc, "onclick")
                Dim bookurlstart As Integer
                Dim bookurlend As Integer
                For i = 0 To bookurl_rstr.Length - 1
                    bookurl_rstr(i) = Replace(bookurl_rstr(i), vbNewLine, "")
                    bookurl_rstr(i) = Trim(bookurl_rstr(i))
                    If IsNothing(cviewUrl) Then
                        cviewUrlstart = bookurl_rstr(i).IndexOf(cviewUrlstr)
                        cviewUrlend = bookurl_rstr(i).IndexOf(cviewUrlstrend)
                        cviewUrl = Mid(bookurl_rstr(i), cviewUrlstart + cviewUrlstr.Length + 1, cviewUrlend - cviewUrlstart - cviewUrlstr.Length)
                    End If
                    bookurlstart = bookurl_rstr(i).IndexOf(cviewstr)
                    bookurlend = bookurl_rstr(i).IndexOf(cviewstrend)
                    bookurl_rstr(i) = Mid(bookurl_rstr(i), cviewstr.Length + 1, bookurlend - bookurlstart - cviewstrend.Length - 2)
                    bookurl_rstr(i) = bookurl_rstr(i).Split("-")(1)
                Next
                Me.BooksUrl = bookurl_rstr
                Exit For
            End If
        Next


        For idi = 0 To section_table_id.Length - 1
            If code.IndexOf("<table id=" & Chr(34) & section_table_id(idi) & Chr(34)) <> -1 Then
                Dim section_table_c As HTML.Condition
                section_table_c._Property = "id"
                section_table_c._Value = section_table_id(idi)

                Dim section As String = html.GetMid("table", section_table_c)(0)
                Dim html_section As New HTML(section)
                Dim sectionc As HTML.Condition
                sectionc._Property = "class"
                sectionc._Value = "Ch"

                Dim sectionc_rstr() As String = html_section.GetMid("a", sectionc)
                Dim last_section As String = "<font color=red"
                For i = 0 To sectionc_rstr.Length - 1
                    sectionc_rstr(i) = Replace(sectionc_rstr(i), vbNewLine, "")
                    sectionc_rstr(i) = Trim(sectionc_rstr(i))
                    If sectionc_rstr(i).IndexOf(last_section) <> -1 Then
                        Dim start As Integer = sectionc_rstr(i).IndexOf(">", last_section.Length)
                        Dim tail As Integer = sectionc_rstr(i).IndexOf("</font>", start)
                        sectionc_rstr(i) = Mid(sectionc_rstr(i), start + 2, tail - start - 1)
                    End If
                Next
                Me.Sections = sectionc_rstr

                Dim sectionurl_rstr() As String = html_section.GetValue("a", sectionc, "onclick")
                Dim sectionurlstart As Integer
                Dim sectionurlend As Integer
                For i = 0 To sectionurl_rstr.Length - 1
                    sectionurl_rstr(i) = Replace(sectionurl_rstr(i), vbNewLine, "")
                    sectionurl_rstr(i) = Trim(sectionurl_rstr(i))
                    If IsNothing(cviewUrl) Then
                        cviewUrlstart = sectionurl_rstr(i).IndexOf(cviewUrlstr)
                        cviewUrlend = sectionurl_rstr(i).IndexOf(cviewUrlstrend)
                        cviewUrl = Mid(sectionurl_rstr(i), cviewUrlstart + cviewUrlstr.Length + 1, cviewUrlend - cviewUrlstart - cviewUrlstr.Length)
                    End If
                    sectionurlstart = sectionurl_rstr(i).IndexOf(cviewstr)
                    sectionurlend = sectionurl_rstr(i).IndexOf(cviewstrend)
                    sectionurl_rstr(i) = Mid(sectionurl_rstr(i), cviewstr.Length + 1, sectionurlend - sectionurlstart - cviewstrend.Length - 2)
                    sectionurl_rstr(i) = sectionurl_rstr(i).Split("-")(1)
                Next
                Me.SectionsUrl = sectionurl_rstr
                Exit For
            End If
        Next

        Me.CviewUrl = cviewUrl


        Dim UrlConvert(22) As String
        UrlConvert(0) = ""
        UrlConvert(1) = "cool-"
        UrlConvert(2) = "cool-"
        UrlConvert(3) = "best-manga-"
        UrlConvert(4) = "cool-"
        UrlConvert(5) = "cool-"
        UrlConvert(6) = "cool-"
        UrlConvert(7) = "cool-"
        UrlConvert(8) = "best-manga-"
        UrlConvert(9) = "cool-"
        UrlConvert(10) = "best-manga-"
        UrlConvert(11) = "best-manga-"
        UrlConvert(12) = "cool-"
        UrlConvert(13) = "best-manga-"
        UrlConvert(14) = "best-manga-"
        UrlConvert(15) = "best-manga-"
        UrlConvert(16) = "best-manga-"
        UrlConvert(17) = ""
        UrlConvert(18) = "best-manga-"
        UrlConvert(19) = "cool-"
        UrlConvert(20) = "best-manga-"
        UrlConvert(21) = "cool-"
        UrlConvert(22) = "cool-"
        'http://new.comicvip.com/show/best-manga-104.html?ch=1
        'http://new.comicvip.com/show/cool-103.html?ch=1

        Dim viewpage As String = "http://new.comicvip.com/show/" & UrlConvert(Me.CviewUrl) & id & ".html?ch=1"
        Dim pagehtml As New HTML(viewpage, "big5")
        Dim allcode As String = pagehtml.GetCode

        Dim scriptstr As String = "<script>var chs="
        Dim scriptstrend As String = "</script>"
        Dim scriptstart As Integer = allcode.IndexOf(scriptstr)
        Dim scriptend As Integer = allcode.IndexOf(scriptstrend, scriptstart + scriptstr.Length + 1)
        Dim script As String = Mid(allcode, scriptstart + scriptstr.Length + 1, scriptend - scriptstart - scriptstr.Length)

        scriptstr = "var cs='"
        scriptstrend = "';eval(unescape"
        scriptstart = script.IndexOf(scriptstr)
        scriptend = script.IndexOf(scriptstrend, scriptstart + scriptstr.Length + 1)
        script = Mid(script, scriptstart + scriptstr.Length + 1, scriptend - scriptstart - scriptstr.Length)

        Me.script_cs = script
    End Sub

    Public Sub SetView(ByVal ch As String)
        Me.ImagesUrl = getUrl(Me.script_cs, Me.comicID, ch)
    End Sub

#Region "url"
    Private f As Integer = 50
    Private Function getUrl(comickey As String, itemid As String, ch As String) As String()
        Dim subKey As String = subkeyCreater(comickey, ch)
        Dim totalPage As Integer = pageCounter(subKey)
        Dim imgsUrl(totalPage - 1) As String
        For i = 1 To totalPage
            imgsUrl(i - 1) = urlCreater(subKey, itemid, i)
        Next
        Return imgsUrl
    End Function

    Private Function subkeyCreater(comickey As String, ch As String)
        Dim subKey As String = ""
        Dim keyLength As Integer = comickey.Length
        For i = 0 To (keyLength / f) - 1
            strSplit(comickey, i * f, 4)
            If strSplit(comickey, i * f, 4) = ch Then
                subKey = strSplit(comickey, i * f, f, f)
            End If
        Next
        If subKey = "" Then
            subKey = strSplit(comickey, keyLength - f, f)
        End If
        Return subKey
    End Function

    Private Function pageCounter(subKey As String) As Integer
        Return Val(strSplit(subKey, 7, 3))
    End Function

    Private Function urlCreater(subKey As String, itemid As String, page As Integer) As String
        Dim serverNum As String = strSplit(subKey, 4, 2)
        Dim serialNum As String = strSplit(subKey, 6, 1)
        Dim serialNum2 As String = strSplit(subKey, 0, 4)
        Dim pageNum As String = addZero(page)
        Dim hash As String = strSplit(subKey, getHash(page) + 10, 3, f)

        Return "http://img" + serverNum + ".8comic.com/" + serialNum + "/" + itemid + "/" + serialNum2 + "/" + pageNum + "_" + hash + ".jpg "
    End Function

    Private Function addZero(str As String) As String
        If str.Length = 1 Then
            Return "00" & str
        ElseIf str.Length = 2 Then
            Return "0" & str
        Else
            Return str
        End If
    End Function

    Private Function getHash(n As Integer) As Integer
        Return (((n - 1) \ 10) Mod 10) + (((n - 1) Mod 10) * 3)
    End Function

    Private Function strSplit(str As String, start As Integer, count As Integer) As String
        Dim temp1 As String = substr(str, start, count)
        Dim temp2 As String = ""
        For i = 0 To temp1.Length - 1
            If IsNumeric(temp1(i)) Then
                temp2 &= temp1(i)
            End If
        Next
        Return temp2
    End Function

    Private Function strSplit(str As String, start As Integer, count As Integer, test As Integer) As String
        Return substr(str, start, count)
    End Function

    Private Function substr(str As String, starti As Integer, len As Integer) As String
        Return Mid(str, starti + 1, len)
    End Function
#End Region

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