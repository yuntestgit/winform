Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text

Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Const PROCESS_WM_READ As Integer = &H10
    <DllImport("kernel32.dll")>
    Public Shared Function OpenProcess(dwDesiredAccess As UInteger, bInheritHandle As Boolean, dwProcessId As Integer) As Integer
    End Function

    <DllImport("kernel32.dll")>
    Public Shared Function ReadProcessMemory(hProcess As Integer, lpBaseAddress As Integer, buffer As Byte(), size As Integer, lpNumberOfBytesRead As Integer) As Boolean
    End Function

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim notepadProcess As Process = Process.GetProcessesByName("GrandFantasia")(0)
        Dim processHandle As IntPtr = OpenProcess(PROCESS_WM_READ, False, notepadProcess.Id)
        Dim bytesRead As Integer = 0
        Dim buffer As Byte() = New Byte(23) {}

        For i = 0 To &H100
            ReadProcessMemory(CInt(processHandle), i, buffer, buffer.Length, bytesRead)
            'MsgBox(Encoding.UTF8.GetString(buffer))
            ListBox1.Items.Add(":" & Encoding.UTF8.GetString(buffer) & ";")
        Next

    End Sub
End Class
