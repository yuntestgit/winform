﻿Public Class Form1
    Public Function MAKELPARAM(ByVal l As Integer, ByVal h As Integer) As Integer
        Dim r As Integer = l + (h << 16)
        Return r
    End Function

    Dim m0 As Integer = -1
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim m As Integer = DateTime.Now.Minute()
        If m Mod 10 = 0 Then
            If m0 <> m Then
                Dim hwnd As Integer = API.FindWindow("Qt5QWindowIcon", "夜神安卓模拟器")

                Dim key As Integer = Keys.D
                API.PostMessage(hwnd, API.WM_KEYDOWN, key, MAKELPARAM(key, API.WM_KEYDOWN))
                API.PostMessage(hwnd, API.WM_KEYUP, key, MAKELPARAM(key, API.WM_KEYUP))
                m0 = m
            End If
        End If

        Me.Text = DateTime.Now
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        If API.GetAsyncKeyState(Keys.F7) <> 0 Then
            Dim hwnd As Integer = API.FindWindow("Qt5QWindowIcon", "夜神安卓模拟器")
            Dim key As Integer = Keys.H
            API.PostMessage(hwnd, API.WM_KEYDOWN, key, MAKELPARAM(key, API.WM_KEYDOWN))
            API.PostMessage(hwnd, API.WM_KEYUP, key, MAKELPARAM(key, API.WM_KEYUP))

            Timer3.Start()

        End If

        If API.GetAsyncKeyState(Keys.F8) <> 0 Then
            Timer3.Stop()
            API.SetCursorPos(p.x, p.y)
        End If

        If API.GetAsyncKeyState(Keys.F9) <> 0 Then
            API.GetCursorPos(p)
        End If
    End Sub

    Dim p As API.POINTAPI

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        Dim rxmax As Integer = 100
        Dim rxmin As Integer = -100
        Dim rx As Integer = Int((rxmax - rxmin + 1) * Rnd() + rxmin)

        Dim rymax As Integer = 200
        Dim rymin As Integer = -180
        Dim ry As Integer = Int((rymax - rymin + 1) * Rnd() + rymin)

        API.SetCursorPos(p.x + rx, p.y + ry)
        API.mouse_event(2, 0, 0, 0, 0)
        System.Threading.Thread.Sleep(10)
        API.mouse_event(4, 0, 0, 0, 0)
    End Sub
End Class

Public Class API
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Integer

    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Public Shared Function FindWindow( _
                ByVal lpClassName As String, _
                ByVal lpWindowName As String) As IntPtr
    End Function

    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer

    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Const WM_MOUSE_MOVE = &H200
    Public Const WM_LBUTTON_DOWN = &H201
    Public Const WM_LBUTTON_UP = &H202
    Public Const WM_KEYDOWN = &H100
    Public Const WM_KEYUP = &H101
    Public Const WM_CHAR = &H102

    Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, dx As Integer, ByVal dy As Integer, ByVal dwData As Integer, ByVal dwExtraInfo As Integer)
    Public Declare Function SetCursorPos Lib "user32" (ByVal x As Integer, ByVal y As Integer) As Boolean

    Public Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer
    Public Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
    End Structure
    Public Function GetCursor() As Point
        Dim pa As POINTAPI
        GetCursorPos(pa)
        Return New Point(pa.x, pa.y)
    End Function
End Class