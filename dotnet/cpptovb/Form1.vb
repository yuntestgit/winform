Public Class Form1

    Dim f As Integer = 50

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim comickey As String = "cxy0y74y35vu54a7drwu4qpfsvxywa3q6e9755rjgqcsyqkhgycxy1y24y51xeaaqurqkntf2rvnvke7uu75jbxq6c2njwh7jyjkcxy2y24y41fngujt72uj9e34wm92wnh5qxpefetk2vybbw588fcxy3y24y432cny2924dxxbnn739pwhktg6r7dgn4dfkuy6wuq4cxy4y24y45j2nmre5psn2f77y2jgfc37x62q6kr22b8ktr9dn9cxy5y72y31nygj252aef89e58u72h6n3jg3f7k7b5d5tubc5gecxy6y23y3454j8sjyy2vjnfsfmxrctquyskvdynvrvbqqp5wmucxy7y23y367dxv3w2yevkebfmn5bt2fm7wsw83jrec5f7wqtg2cxy8y23y342er9rp9mdgd9ynfkxgaub65ddpw27pbxdu6ypca5cxy9y23y36a8t632886sfytrry7wx9dqveywf6y32c8vmh7ggnxy10y23y42yvrx2mfx2sdumrn3mw84u3vs2nhkugqyq7bukggvxy11y23y40bsc5ywwmpss3ftjf2574bdrkbrbfwg63uekgubvpxy12y23y43ju7a5p7vv96uwywu7kdwt4dbkfr6eamapchwx9xsxy13y23y388c8cfsft5apytp57bdhbjteu4ubpjry753heey7jxy14y23y312d8ycsh5k3twem87y4xcv4tddrr6af5h87j7psphxy15y23y374jhpsdtmvv7uf9fd2vbxdhkmccdscj8j5m9u365cxy16y24y31626wadvaafdxpb47guv9qax7kb93he656w57c2rexy17y33y337r6ntytdtak7q9j2s7g8dt9wqqddaugfqx235qykxy18y24y39c7fadds3t9bmebujt87j6njms2ruxs99p2tj4quhxy19y33y459u7dbsahnwhvvfn6mjtgrxk6q2yx4u7m7t89qmxfxy20y72y31v439vw8euuxntx398c72btfbcfa46vek5q4wsfpmxy21y33y38dmwg55qgwn59vq6yddkvy3hfwuegaa66g9vjaahvxy22y33y45dxjx2fmyw5cmejegn4gcxgvaet4rwe92yshqxs3cxy23y62y43v4knva2ak65q9kegknusamqbkpwph6s4u99hr4jbxy24y64y43waxhkvfbxbrrxe9wdbunf9y27yjfpwtu8ar9437vxy25y64y33485nmy2br6727e2c5w2s9pej52pqfjv7sausdxf8xy26y62y42ykj7m55sbuunmnfxr53r9h4nuu8sy5hfy36eygrpxy27y62y45t24qtmcc7vw964qwvkp78crbg9ygxmbcamyvew34xy28y62y44kuunvhgbdju8nnbn93fjrd2pmkbecg2awn33gx5vxy29y64y4189cu6ss7jtkdyrrbtdajvct67jma5rnj3tup65n4xy30y62y42tkdjf2snvdu75w9a9gdb4ycpg24hat7ca23p8b2sxy31y64y44etefdjx3qg2y5skbg2u9v8tw389sfsg2ptsw3f85xy32y64y43g2nnsjqenmgrppe6fn4d4rpwa79w5r8rus9gf758xy33y73y442pcdfpst5he667u6jtw8cx9k9j275dqramx2g7c3xy34y73y43vjbjukp5hykt4vhfjh8gadxuhem33upshak3rf2wxy35y73y43t8bgehsddyxn79qu85hs937rbmesae6vqs2ejgcexy36y73y452qp3aacb6a3mc7rfjr5jx7a86wkdp8sgun2h4wbhxy37y74y44eanv98s8dxdd7dd8v8wdg886vrg3c66rfjbwxcaqxy38y73y444au9w9hkjbucmrh8gfhwdhqd8g2v82a5b6jhxtcrxy39y74y442vq4bh823dju3nusektud7n2cvq6y49j2ej549cdxy40y74y448cnmeqs64rfbbq64wyjmnga4f9hrna28rvuubtgtxy41y73y44wmjgxuw59h8s3a8d5vmmhj899t569pnbk86x9dp7xy42y73y41hy5gv5q4n6fq7tsu6kq6guvqksqv7fbm6acpnfhvxy43y73y387nexqj6ptfxa9g42r7nfw94napexsa83fhkvbbycxy44y74y44r3tdmwxus9kme7k5knuyct3wygtcghfa6ftaymf4xy45y74y44xr2673uk2vct9anxwbyjpnhcr69mmp5p6uaxqrebxy46y73y45xhdayadq53kdhf68kww97qdvete72ae5sgkvv8smxy47y74y45bm8eeg8w7v56kpmw9shxevxcxp3578hse2fcypwbxy48y74y4566kvtxtd2wfdwkr24jpbhb8tyvkgv5m3h4dx4qtcxy49y74y4698ksx39w5u6p45bkk5bx33y63sdjcne2b9tady6qxy50y33y44w7h4qredu4e8ebmhqdsrtde79g5sctk5tapjar5pxy51y73y45mwucjf3b8h94xux6jk5wjqb2wxdjeh7hpqc2qs84xy52y74y458uuqg6eykwgr77w7nwrcm6rjevnfff85urqkb55dxy53y74y46gbw2bu42pcxkh4hh4tumfhh86gnhcmkn3hfpxxcbxy54y63y42s4j9gyf8c6ck4u44qgqkwmbr236m6eksrc8ps8y5xy55y73y45cx3rw6w5u8ecgcwbamxbre2955sgejua7jf4c9amxy56y74y4555x4xscagcmv934uqeer2phtx4rahnckjqqdwk28xy57y74y44vxn4nmrcunky49ynf2gpv29sbxwcbgg3rdg4pxtvxy58y64y46at8hxbdx9vkcqsxryrtbc967jqvevvt5cjf68329xy59y73y46pyprsqbpvwsrmheuj9xvmqquk8qs934ycax627snxy60y73y46u4fjqt62q98g88tukes98sabe4n63ng6bxyc8behxy61y23y45t5pw7dnu2skch7j695c8cx9bqd3btfvh3c8m372pxy62y63y41byypjwgjxxrvn62w99ndke7xsmvr9snwhbsr3srfxy63y23y41bb2yk4gyh37u8hfj35pd2qkcpu94xw5fbu6k8hbdxy64y72y45dsy2cbntt5qs936y2c9ajbu34neqdh8v6ekvyd5jxy65y23y44ahvk9457w5ns7t5yvud76ddprq2m8whqtktfhd42xy66y23y38v932e635jw77j2b9y6hajy86xevuyhwt3n39rmve8001y23y17bm9ypn5sb2cq4pc7yxnq6wywdn8ktgx4n7u588kx8002y24y18ckgydfavauec4wy6hut244qx7kq4ebs2pww478vn"
        Dim itemid As String = "7340"
        Dim ch As String = "8002"

        Dim imgsUrl() As String = getUrl(comickey, itemid, ch)
        For i = 0 To imgsUrl.Length - 1
            cout(imgsUrl(i) & vbNewLine)
        Next
    End Sub

    Function getUrl(comickey As String, itemid As String, ch As String) As String()
        Dim subKey As String = subkeyCreater(comickey, ch)
        Dim totalPage As Integer = pageCounter(subKey)
        Dim imgsUrl(totalPage - 1) As String
        For i = 1 To totalPage
            imgsUrl(i - 1) = urlCreater(subKey, itemid, i)
        Next

        Return imgsUrl
    End Function

    Function subkeyCreater(comickey As String, ch As String)
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

    Function pageCounter(subKey As String) As Integer
        Return Val(strSplit(subKey, 7, 3))
    End Function

    Function urlCreater(subKey As String, itemid As String, page As Integer) As String
        Dim serverNum As String = strSplit(subKey, 4, 2)
        Dim serialNum As String = strSplit(subKey, 6, 1)
        Dim serialNum2 As String = strSplit(subKey, 0, 4)
        Dim pageNum As String = addZero(page)
        Dim hash As String = strSplit(subKey, getHash(page) + 10, 3, f)

        Return "http://img" + serverNum + ".8comic.com/" + serialNum + "/" + itemid + "/" + serialNum2 + "/" + pageNum + "_" + hash + ".jpg"
    End Function

    Function addZero(str As String) As String
        If str.Length = 1 Then
            Return "00" & str
        ElseIf str.Length = 2 Then
            Return "0" & str
        Else
            Return str
        End If
    End Function

    Function getHash(n As Integer) As Integer
        Return (((n - 1) / 10) Mod 10) + (((n - 1) Mod 10) * 3)
    End Function

    Function strSplit(str As String, start As Integer, count As Integer) As String
        Dim temp1 As String = substr(str, start, count)
        Dim temp2 As String = ""
        For i = 0 To temp1.Length - 1
            If IsNumeric(temp1(i)) Then
                temp2 &= temp1(i)
            End If
        Next
        Return temp2
    End Function

    Function strSplit(str As String, start As Integer, count As Integer, test As Integer) As String
        Return substr(str, start, count)
    End Function

    Function substr(str As String, starti As Integer, len As Integer) As String
        Return Mid(str, starti + 1, len)
    End Function

    Sub cout(str As String)
        RichTextBox1.Text = RichTextBox1.Text & str
    End Sub
End Class
