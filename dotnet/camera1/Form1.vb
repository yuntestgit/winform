Imports System.Runtime.InteropServices
Public Class Form1
    Private Declare Function SetWindowsHookEx Lib "user32" Alias "SetWindowsHookExA" (ByVal idHook As Integer, ByVal lpfn As GetWinMsgDelete, ByVal hmod As Integer, ByVal dwThreadId As Integer) As Integer 

    Private Declare Function CallNextHookEx Lib "user32" Alias "CallNextHookEx" (ByVal hHook As Integer, ByVal ncode As Integer, ByVal wParam As Integer, ByVal lParam As MSG) As Integer

    Private Declare Function UnhookWindowsHookEx Lib "user32" Alias "UnhookWindowsHookEx" (ByVal hHook As Integer) As Integer

    Private Const WH_GETMESSAGE As Integer = 3

    Private Const WM_PASTE As Integer = &H302

    Dim hRet As Integer

    Public Structure MSG
        Public hwnd As Integer
        Public message As Integer
        Public wParam As Integer
        Public lParam As Integer
        Public time As Integer
        Public pt As POINTAPI
    End Structure

    Public Structure POINTAPI
        Private x As Integer
        Private y As Integer
    End Structure

    Private Delegate Function GetWinMsgDelete(ByVal nCode As Integer, _
    ByVal wParam As Integer, _
    ByVal lParam As MSG) As Integer

    Private Function proGetWinMsg(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As MSG) As Integer
        Debug.WriteLine("Message Callback. Code=" & nCode)
        If nCode = 0 Then
            If lParam.message = WM_PASTE Then
                MessageBox.Show("asdfsad")
                Return 1
            End If
            Return CallNextHookEx(hRet, nCode, wParam, lParam)
        Else
            Return 0
        End If
    End Function

    Public Sub GetMsg()
        hRet = SetWindowsHookEx(WH_GETMESSAGE, AddressOf proGetWinMsg, Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly.GetModules()(0)).ToInt32, 0)
    End Sub
    Public Sub Unhook()
        If hRet <> 0 Then
            UnhookWindowsHookEx(hRet)
            hRet = 0
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetMsg()
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Unhook()
    End Sub
End Class
