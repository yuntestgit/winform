Imports PacketDotNet
Imports SharpPcap

Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim ver As String = SharpPcap.Version.VersionString
        'MsgBox(ver)
        start()
    End Sub

    Dim socketCount As Integer = 0

    Sub start()
        Dim deviceIndex As Integer = 0 '看你要監聽第幾張網卡的訊息
        Dim devices As CaptureDeviceList = CaptureDeviceList.Instance
        Dim device As ICaptureDevice = devices(deviceIndex)

        AddHandler device.OnPacketArrival, AddressOf device_OnPacketArrival
        Dim readTimeoutMilliseconds As Integer = 1000
        device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds)

        '日版卡卡的連線IP
        device.Filter = "src 203.191.228.51 and ip and tcp"
        device.Capture()

    End Sub



    Sub device_OnPacketArrival(ByVal sender As Object, ByVal e As CaptureEventArgs)
        On Error GoTo err

        socketCount += 1
        Dim b(255) As Byte
        Dim packet As PacketDotNet.Packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, b)
        Dim tcpPacket As PacketDotNet.TcpPacket = PacketDotNet.TcpPacket.GetEncapsulated(packet)

        If tcpPacket IsNot Nothing Then

            Dim ipPacket As PacketDotNet.IpPacket = tcpPacket.ParentPacket
            Dim srcIp As System.Net.IPAddress = ipPacket.SourceAddress
            Dim dstIp As System.Net.IPAddress = ipPacket.DestinationAddress

            Dim srcPort As Integer = tcpPacket.SourcePort
            Dim dstPort As Integer = tcpPacket.DestinationPort

            If tcpPacket.DataOffset = 5 Then
                ListBox1.Items.Add(b.ToString)
                '分析tcpPacket.Bytes的資料，印像是Index 20之後才是接收的Data？ 這部分忘記了
            End If
        End If
err:
    End Sub
End Class
