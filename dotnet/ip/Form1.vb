Imports System.Net
Imports System.Net.Sockets

Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim myHostName As String = System.Net.Dns.GetHostName()
        Dim i As Integer
        Dim ipE As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(myHostName)
        Dim IpA() As System.Net.IPAddress = ipE.AddressList
        For i = 0 To IpA.GetUpperBound(0)
            MsgBox("IP Address " & i.ToString & " " & IpA(i).ToString)
        Next
    End Sub
End Class
