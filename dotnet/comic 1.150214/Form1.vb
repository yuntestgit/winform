Public Class Form1

    Dim html As HTML
    Dim test As New _8Comic

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RichTextBox1.Clear()

        test.SetID("7340")

        ' Dim cviewurl As String = test.CviewUrl
        'RichTextBox1.Text &= cviewurl & vbNewLine

        Dim books() As String = test.Books
        If Not IsNothing(books) Then
            Dim booksurl() As String = test.BooksUrl
            For i = 0 To books.Length - 1
                RichTextBox1.Text &= books(i) & " " & booksurl(i) & vbNewLine
            Next
            TextBox1.Text = booksurl(0)
        End If

        Dim sections() As String = test.Sections
        If Not IsNothing(sections) Then
            Dim sectionsurl() As String = test.SectionsUrl
            For i = 0 To sections.Length - 1
                RichTextBox1.Text &= sections(i) & " " & sectionsurl(i) & vbNewLine
            Next
            TextBox1.Text = sectionsurl(0)
        End If


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        RichTextBox2.Clear()
        test.SetView(TextBox1.Text)

        ' RichTextBox2.Text = test.script_cs
        test.SetView(TextBox1.Text)
        Dim output() As String = test.ImagesUrl
        For i = 0 To output.Length - 1
            RichTextBox2.Text &= output(i) & vbNewLine
        Next

        Dim url As String = output(0)
        Dim req As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(url), System.Net.HttpWebRequest)
        Dim rsp As System.Net.HttpWebResponse = DirectCast(req.GetResponse(), System.Net.HttpWebResponse)
        Dim img As Image = Nothing
        Try
            If rsp.StatusCode = System.Net.HttpStatusCode.OK Then
                img = New Bitmap(rsp.GetResponseStream())
            End If
        Finally
            rsp.Close()
        End Try

        If Not IsNothing(img) Then
            PictureBox1.Width = img.Width
            PictureBox1.Height = img.Height
            PictureBox1.Image = img
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
        'Me.script_cs = "cxy0y74y35vu54a7drwu4qpfsvxywa3q6e9755rjgqcsyqkhgycxy1y24y51xeaaqurqkntf2rvnvke7uu75jbxq6c2njwh7jyjkcxy2y24y41fngujt72uj9e34wm92wnh5qxpefetk2vybbw588fcxy3y24y432cny2924dxxbnn739pwhktg6r7dgn4dfkuy6wuq4cxy4y24y45j2nmre5psn2f77y2jgfc37x62q6kr22b8ktr9dn9cxy5y72y31nygj252aef89e58u72h6n3jg3f7k7b5d5tubc5gecxy6y23y3454j8sjyy2vjnfsfmxrctquyskvdynvrvbqqp5wmucxy7y23y367dxv3w2yevkebfmn5bt2fm7wsw83jrec5f7wqtg2cxy8y23y342er9rp9mdgd9ynfkxgaub65ddpw27pbxdu6ypca5cxy9y23y36a8t632886sfytrry7wx9dqveywf6y32c8vmh7ggnxy10y23y42yvrx2mfx2sdumrn3mw84u3vs2nhkugqyq7bukggvxy11y23y40bsc5ywwmpss3ftjf2574bdrkbrbfwg63uekgubvpxy12y23y43ju7a5p7vv96uwywu7kdwt4dbkfr6eamapchwx9xsxy13y23y388c8cfsft5apytp57bdhbjteu4ubpjry753heey7jxy14y23y312d8ycsh5k3twem87y4xcv4tddrr6af5h87j7psphxy15y23y374jhpsdtmvv7uf9fd2vbxdhkmccdscj8j5m9u365cxy16y24y31626wadvaafdxpb47guv9qax7kb93he656w57c2rexy17y33y337r6ntytdtak7q9j2s7g8dt9wqqddaugfqx235qykxy18y24y39c7fadds3t9bmebujt87j6njms2ruxs99p2tj4quhxy19y33y459u7dbsahnwhvvfn6mjtgrxk6q2yx4u7m7t89qmxfxy20y72y31v439vw8euuxntx398c72btfbcfa46vek5q4wsfpmxy21y33y38dmwg55qgwn59vq6yddkvy3hfwuegaa66g9vjaahvxy22y33y45dxjx2fmyw5cmejegn4gcxgvaet4rwe92yshqxs3cxy23y62y43v4knva2ak65q9kegknusamqbkpwph6s4u99hr4jbxy24y64y43waxhkvfbxbrrxe9wdbunf9y27yjfpwtu8ar9437vxy25y64y33485nmy2br6727e2c5w2s9pej52pqfjv7sausdxf8xy26y62y42ykj7m55sbuunmnfxr53r9h4nuu8sy5hfy36eygrpxy27y62y45t24qtmcc7vw964qwvkp78crbg9ygxmbcamyvew34xy28y62y44kuunvhgbdju8nnbn93fjrd2pmkbecg2awn33gx5vxy29y64y4189cu6ss7jtkdyrrbtdajvct67jma5rnj3tup65n4xy30y62y42tkdjf2snvdu75w9a9gdb4ycpg24hat7ca23p8b2sxy31y64y44etefdjx3qg2y5skbg2u9v8tw389sfsg2ptsw3f85xy32y64y43g2nnsjqenmgrppe6fn4d4rpwa79w5r8rus9gf758xy33y73y442pcdfpst5he667u6jtw8cx9k9j275dqramx2g7c3xy34y73y43vjbjukp5hykt4vhfjh8gadxuhem33upshak3rf2wxy35y73y43t8bgehsddyxn79qu85hs937rbmesae6vqs2ejgcexy36y73y452qp3aacb6a3mc7rfjr5jx7a86wkdp8sgun2h4wbhxy37y74y44eanv98s8dxdd7dd8v8wdg886vrg3c66rfjbwxcaqxy38y73y444au9w9hkjbucmrh8gfhwdhqd8g2v82a5b6jhxtcrxy39y74y442vq4bh823dju3nusektud7n2cvq6y49j2ej549cdxy40y74y448cnmeqs64rfbbq64wyjmnga4f9hrna28rvuubtgtxy41y73y44wmjgxuw59h8s3a8d5vmmhj899t569pnbk86x9dp7xy42y73y41hy5gv5q4n6fq7tsu6kq6guvqksqv7fbm6acpnfhvxy43y73y387nexqj6ptfxa9g42r7nfw94napexsa83fhkvbbycxy44y74y44r3tdmwxus9kme7k5knuyct3wygtcghfa6ftaymf4xy45y74y44xr2673uk2vct9anxwbyjpnhcr69mmp5p6uaxqrebxy46y73y45xhdayadq53kdhf68kww97qdvete72ae5sgkvv8smxy47y74y45bm8eeg8w7v56kpmw9shxevxcxp3578hse2fcypwbxy48y74y4566kvtxtd2wfdwkr24jpbhb8tyvkgv5m3h4dx4qtcxy49y74y4698ksx39w5u6p45bkk5bx33y63sdjcne2b9tady6qxy50y33y44w7h4qredu4e8ebmhqdsrtde79g5sctk5tapjar5pxy51y73y45mwucjf3b8h94xux6jk5wjqb2wxdjeh7hpqc2qs84xy52y74y458uuqg6eykwgr77w7nwrcm6rjevnfff85urqkb55dxy53y74y46gbw2bu42pcxkh4hh4tumfhh86gnhcmkn3hfpxxcbxy54y63y42s4j9gyf8c6ck4u44qgqkwmbr236m6eksrc8ps8y5xy55y73y45cx3rw6w5u8ecgcwbamxbre2955sgejua7jf4c9amxy56y74y4555x4xscagcmv934uqeer2phtx4rahnckjqqdwk28xy57y74y44vxn4nmrcunky49ynf2gpv29sbxwcbgg3rdg4pxtvxy58y64y46at8hxbdx9vkcqsxryrtbc967jqvevvt5cjf68329xy59y73y46pyprsqbpvwsrmheuj9xvmqquk8qs934ycax627snxy60y73y46u4fjqt62q98g88tukes98sabe4n63ng6bxyc8behxy61y23y45t5pw7dnu2skch7j695c8cx9bqd3btfvh3c8m372pxy62y63y41byypjwgjxxrvn62w99ndke7xsmvr9snwhbsr3srfxy63y23y41bb2yk4gyh37u8hfj35pd2qkcpu94xw5fbu6k8hbdxy64y72y45dsy2cbntt5qs936y2c9ajbu34neqdh8v6ekvyd5jxy65y23y44ahvk9457w5ns7t5yvud76ddprq2m8whqtktfhd42xy66y23y38v932e635jw77j2b9y6hajy86xevuyhwt3n39rmve8001y23y17bm9ypn5sb2cq4pc7yxnq6wywdn8ktgx4n7u588kx8002y24y18ckgydfavauec4wy6hut244qx7kq4ebs2pww478vn"
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
        'MsgBox(totalPage)
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
    'Private Function getHash(n As Integer) As Integer
    '    Return (((n - 1) / 10) Mod 10) + (((n - 1) Mod 10) * 3)
    'End Function

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