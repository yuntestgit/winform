Imports System.Runtime.InteropServices

Public Class Form1
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MARGINS
        Public cxLeftWidth As Integer
        Public cxRightWidth As Integer
        Public cyTopHeight As Integer
        Public cyButtomheight As Integer
    End Structure
    <DllImport("dwmapi.dll")> _
    Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarinset As MARGINS) As Integer
    End Function

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = 163 AndAlso Me.ClientRectangle.Contains(Me.PointToClient(New Point(m.LParam.ToInt32()))) AndAlso m.WParam.ToInt32() = 2 Then
            m.WParam = 1
        End If
        MyBase.WndProc(m)
        If m.Msg = 132 AndAlso m.Result.ToInt32() = 1 Then
            m.Result = 2
        End If
    End Sub

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function FindWindow( _
     ByVal lpClassName As String, _
     ByVal lpWindowName As String) As IntPtr
    End Function

    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Const WM_SYSCOMMAND = &H112
    Const SC_CLOSE = &HF060

    Dim ProcessName(100) As String
    Dim WindowName(100) As String
    Dim ClassName(100) As String

    Dim pindex As Integer = 0
    Dim windex As Integer = 0

    Dim settings As String

    Dim hwnd As Integer = 0

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim margins As MARGINS = New MARGINS
        margins.cxLeftWidth = 20
        margins.cxRightWidth = 20
        margins.cyTopHeight = 20
        margins.cyButtomheight = 20

        Dim hwnd As IntPtr = Me.Handle
        Dim result As Integer = DwmExtendFrameIntoClientArea(hwnd, margins)

        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Application.StartupPath & "\settings.txt", System.Text.Encoding.GetEncoding("big5"))
        settings = sr.ReadToEnd
        sr.Close()

        settings = settings.ToLower

        Dim last As Integer = 0
        While last <> -1
            last = settings.IndexOf("<$type=", last)
            If last <> -1 Then
                Dim start As Integer = last + 8
                Dim typeend As Integer = settings.IndexOf(Chr(34), start)
                Dim type As String = SubStr(settings, start, typeend - start)
                Dim endtag As Integer = settings.IndexOf("/>", start)
                Dim value As String = SubStr(settings, typeend + 2, endtag - typeend - 2)

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
    End Sub

    Function SubStr(str As String, start As Integer, length As Integer) As String
        Return Mid(str, start + 1, length)
    End Function

    Function toLength(str As String, length As Integer) As String
        Dim rstr As String = str
        While rstr.Length <> length
            rstr = "0" & rstr
        End While
        Return rstr
    End Function

    Function getTime() As String
        Return toLength(DateTime.Today.Year().ToString, 4) & "/" & toLength(DateTime.Today.Month().ToString, 2) & "/" & toLength(DateTime.Today.Day().ToString, 2) & " " & toLength(DateTime.Now.Hour().ToString, 2) & ":" & toLength(DateTime.Now.Minute().ToString, 2) & ":" & toLength(DateTime.Now.Second().ToString, 2)
    End Function

    Sub output(str As String)
        RichTextBox1.Text &= getTime() & str
        RichTextBox1.SelectionStart = RichTextBox1.TextLength
        RichTextBox1.ScrollToCaret()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        For i = 0 To windex - 1
            hwnd = FindWindow(ClassName(i), WindowName(i))
            If hwnd Then
                SendMessage(hwnd, WM_SYSCOMMAND, SC_CLOSE, 0)
                output(" -Close : " & WindowName(i) & ", " & ClassName(i) & vbNewLine)
            End If
        Next

        Dim p() As Process = Process.GetProcesses

        For i = 0 To p.Length - 1
            For j = 0 To pindex - 1
                If p(i).ProcessName.ToLower = ProcessName(j) Then
                    p(i).Kill()
                    output(" -Kill : " & p(i).ProcessName & ".exe" & vbNewLine)
                End If
            Next
        Next
    End Sub
End Class

'Internet Explorer_TridentDlgFrame 指令碼錯誤