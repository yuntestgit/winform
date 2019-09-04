Imports System.Runtime.InteropServices

Public Class Main
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function FindWindow( _
     ByVal lpClassName As String, _
     ByVal lpWindowName As String) As IntPtr
    End Function

    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Const WM_SYSCOMMAND = &H112
    Const SC_CLOSE = &HF060


    Dim YS As New yunSystem
    Dim RootPath As String = "C:\yunSystem\"

    Dim cProcessName(100) As String
    Dim cWindowName(100) As String
    Dim cClassName(100) As String
    Dim cOpenFile(100) As String

    Dim indexProcess As Integer = 0
    Dim indexWindow As Integer = 0
    Dim indexOpenFile As Integer = 0

    Dim firstload As Boolean = False

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

    Private Sub Main_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        If firstload = False Then
            firstload = True
            Me.Hide()
            Me.Opacity = 100
        End If
    End Sub

    Private Sub Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim txtCloseProcess As String = YS.ReadTextFile(RootPath & "_CloseProcess.txt", yunSystem.Encoding.BIG5)
        Dim txtCloseWindow As String = YS.ReadTextFile(RootPath & "_CloseWindow.txt", yunSystem.Encoding.BIG5)
        Dim txtOpenFile As String = YS.ReadTextFile(RootPath & "_OpenFile.txt", yunSystem.Encoding.BIG5)

        GetProcess(txtCloseProcess)
        GetWindow(txtCloseWindow)
        SetOpenFile(txtOpenFile)

        Detect_Process.Start()
        Detect_Window.Start()
        Detect_OpenFile.Start()
    End Sub

    Sub GetProcess(txtCloseProcess As String)
        Dim ex() As String = txtCloseProcess.Split(vbNewLine)
        For i = 0 To ex.Length - 1
            ex(i) = ex(i).Replace(Chr(13), "")
            ex(i) = ex(i).Replace(Chr(10), "")
            cProcessName(i) = ex(i)
            indexProcess = i
            ListBox1.Items.Add(cProcessName(i)) 'out
        Next
    End Sub

    Sub GetWindow(txtCloseWindow As String)
        Dim ex() As String = txtCloseWindow.Split(vbNewLine)
        For i = 0 To ex.Length - 1
            ex(i) = ex(i).Replace(Chr(13), "")
            ex(i) = ex(i).Replace(Chr(10), "")
            If i Mod 2 = 0 Then
                cWindowName(i) = ex(i)
                indexWindow = i
            Else
                cClassName(i) = ex(i)
                ListBox2.Items.Add(cWindowName(i - 1) & ", " & cClassName(i)) 'out
            End If
        Next
    End Sub

    Sub SetOpenFile(txtOpenFile As String)
        Dim ex() As String = txtOpenFile.Split(vbNewLine)
        For i = 0 To ex.Length - 1
            ex(i) = ex(i).Replace(Chr(13), "")
            ex(i) = ex(i).Replace(Chr(10), "")
            cOpenFile(i) = ex(i)
            indexOpenFile = i
            ListBox3.Items.Add(cOpenFile(i)) 'out
        Next
    End Sub

    Private Sub Detect_Process_Tick(sender As System.Object, e As System.EventArgs) Handles Detect_Process.Tick
        Try
            Dim p() As Process = Process.GetProcesses

            For i = 0 To p.Length - 1
                For j = 0 To indexProcess
                    If p(i).ProcessName.ToLower = cProcessName(j) Then
                        p(i).Kill()
                    End If
                Next
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Detect_Window_Tick(sender As System.Object, e As System.EventArgs) Handles Detect_Window.Tick
        Try
            For i = 0 To indexWindow
                Dim hwnd As Integer = FindWindow(cClassName(i), cWindowName(i))
                If hwnd Then
                    SendMessage(hwnd, WM_SYSCOMMAND, SC_CLOSE, 0)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Detect_OpenFile_Tick(sender As System.Object, e As System.EventArgs) Handles Detect_OpenFile.Tick
        Try
            For i = 0 To indexOpenFile
                If My.Computer.FileSystem.FileExists(cOpenFile(i)) = True Then
                    Detect_OpenFile.Stop()
                    Dim OpenFileName As String = YS.ReadTextFile(cOpenFile(i), yunSystem.Encoding.UTF8)
                    My.Computer.FileSystem.DeleteFile(cOpenFile(i))
                    Me.Text = OpenFileName
                    Process.Start(OpenFileName)
                    Detect_OpenFile.Start()
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Alert.Show()
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Show()
        End If
    End Sub
End Class

Class yunSystem
    Public Enum Encoding
        UTF8 = 0
        BIG5 = 1
    End Enum

    Public Function ReadTextFile(ByVal Path As String, ByVal Encoder As Encoding) As String
        Dim sStr As String = ""
        Try
            Dim sysEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("big5")
            If Encoder = Encoding.BIG5 Then
                sysEncoding = System.Text.Encoding.GetEncoding("big5")
            ElseIf Encoder = Encoding.UTF8 Then
                sysEncoding = System.Text.Encoding.UTF8
            End If

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(Path, sysEncoding)
            sStr = sr.ReadToEnd
            sr.Close()

            Return sStr
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub ReNameDirectory(ByVal Oldpath As String, ByVal Newpath As String)

    End Sub
End Class
