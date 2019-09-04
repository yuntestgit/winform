Imports System.Runtime.InteropServices

Public Class Form1

#Region "My Window Basic"
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If (m.Msg = &H112) Then
            If (m.WParam = &HF020) Then 'minimize
                Me.Hide()
            ElseIf (m.WParam = &HF060) Then 'close
                Me.Hide()
            Else
                MyBase.WndProc(m)
            End If
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Dim firstload As Boolean = False
    Dim closehandle As Integer

    Private Sub Form1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        If firstload = False Then
            firstload = True
            Me.Hide()
            Me.Opacity = 100
            Form3.Show()
        End If
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Form2.Show()
        ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Show()
        End If
    End Sub
#End Region

#Region "CloseTool API"
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function FindWindow( _
     ByVal lpClassName As String, _
     ByVal lpWindowName As String) As IntPtr
    End Function

    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
#End Region

#Region "When Loading & Global Variable"
    Const WM_SYSCOMMAND = &H112
    Const SC_CLOSE = &HF060

    Dim ProcessName(100) As String
    Dim WindowName(100) As String
    Dim ClassName(100) As String

    Dim pindex As Integer = 0
    Dim windex As Integer = 0

    Dim CloseTool_settings As String

    Dim hwnd As Integer = 0

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Application.StartupPath & "\CloseTool.txt", System.Text.Encoding.GetEncoding("big5"))
        CloseTool_settings = sr.ReadToEnd
        sr.Close()

        CloseTool_settings = CloseTool_settings.ToLower

        Dim last As Integer = 0
        While last <> -1
            last = CloseTool_settings.IndexOf("<$type=", last)
            If last <> -1 Then
                Dim start As Integer = last + 8
                Dim typeend As Integer = CloseTool_settings.IndexOf(Chr(34), start)
                Dim type As String = SubStr(CloseTool_settings, start, typeend - start)
                Dim endtag As Integer = CloseTool_settings.IndexOf("/>", start)
                Dim value As String = SubStr(CloseTool_settings, typeend + 2, endtag - typeend - 2)

                If type = "window" Then
                    Dim start1 As Integer = value.IndexOf("$windowname=") + 13
                    Dim end1 As Integer = value.IndexOf(Chr(34), start1)
                    Dim wn As String = SubStr(value, start1, end1 - start1)

                    Dim start2 As Integer = value.IndexOf("$classname=") + 12
                    Dim end2 As Integer = value.IndexOf(Chr(34), start2)
                    Dim cn As String = SubStr(value, start2, end2 - start2)

                    If wn = "\null" Then
                        wn = vbNullString
                    End If
                    If cn = "\null" Then
                        cn = vbNullString
                    End If
                    WindowName(windex) = wn
                    ClassName(windex) = cn
                    windex += 1
                ElseIf type = "process" Then
                    Dim start1 As Integer = value.IndexOf("$processname=") + 14
                    Dim end1 As Integer = value.IndexOf(Chr(34), start1)
                    Dim pn As String = SubStr(value, start1, end1 - start1)

                    pn = pn.Replace(".exe", "")
                    ProcessName(pindex) = pn
                    pindex += 1
                End If

                last = endtag
            End If
        End While
        Timer1.Start()
    End Sub

    Function SubStr(str As String, start As Integer, length As Integer) As String
        Return Mid(str, start + 1, length)
    End Function
#End Region

#Region "CloseTool"
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        For i = 0 To windex - 1
            hwnd = FindWindow(ClassName(i), WindowName(i))
            If hwnd Then
                SendMessage(hwnd, WM_SYSCOMMAND, SC_CLOSE, 0)
            End If
        Next

        Dim p() As Process = Process.GetProcesses

        For i = 0 To p.Length - 1
            For j = 0 To pindex - 1
                If p(i).ProcessName.ToLower = ProcessName(j) Then
                    p(i).Kill()
                End If
            Next
        Next
    End Sub
#End Region

End Class
