Public Class Form1

    Declare Auto Function DwmIsCompositionEnabled Lib "dwmapi.dll" Alias "DwmIsCompositionEnabled" (ByRef pfEnabled As Boolean) As Integer

    Declare Auto Function DwmExtendFrameIntoClientArea Lib "dwmapi.dll" Alias "DwmExtendFrameIntoClientArea" (ByVal hWnd As IntPtr, ByRef pMargin As Margins) As Integer

    Public Structure Margins
        Public Left As Integer
        Public Right As Integer
        Public Top As Integer
        Public Bottom As Integer
    End Structure

    Dim NormalButton As New yunButton
    Dim OffsetButton As New yunButton

    Public Declare Function GetDCEx Lib "User32.dll" (ByVal hWnd As IntPtr, ByVal hrgnClip As IntPtr, ByVal flags As Integer) As IntPtr

    'Protected Overrides Sub  WndProc( ByRef  m  As  Message)
    '     MyBase.WndProc(m)
    '     If (m.Msg = 133) Then
    '         Dim DCX_CACHE As Integer = 2
    '         Dim DCX_WINDOW As Integer = 1
    '         Dim g As Graphics = Graphics.FromHdc(GetDCEx(Me.Handle, m.WParam, (DCX_WINDOW Or DCX_CACHE)))
    '         g.DrawLine(Pens.White, New Point(0, 100), New Point(100, -50)) 'Draw Here 
    '     End If
    ' End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        If (m.Msg = 133) Then
            Dim DCX_INTERSECTRGN As Integer = 128
            Dim DCX_WINDOW As Integer = 1
            Dim g As Graphics = Graphics.FromHdc(GetDCEx(IntPtr.Zero, m.WParam, (DCX_WINDOW Or DCX_INTERSECTRGN)))
            g.TranslateTransform(Me.Location.X, Me.Location.Y)
            g.DrawLine(Pens.Black, New Point(0, 100), New Point(100, -50)) 'Draw Here 
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim margins As Margins = New Margins
        margins.Left = -1
        margins.Right = -1
        margins.Top = -1
        margins.Bottom = -1
        Dim hwnd As IntPtr = Me.Handle
        Dim result As Integer = DwmExtendFrameIntoClientArea(hwnd, margins)
        Me.BackColor = Color.Black

        'For i = 0 To 100
        '    ListBox1.Items.Add(i)
        'Next

        'Dim form2 As New Form
        'With form2
        '    .Top = 50
        '    .Left = 50
        '    .Width = 300
        '    .Height = 300
        '    .Icon = My.Resources.gear
        'End With
        'form2.Show()

        AddHandler NormalButton.MouseClick, AddressOf btn_click
        With NormalButton
            .Tag = "Normal"
            .Left = 1267
            .Top = 12
            .BackColor = Color.Transparent
            .Visible = True
        End With
        Me.Controls.Add(NormalButton)

        AddHandler OffsetButton.MouseClick, AddressOf btn_click
        With OffsetButton
            .Tag = "Offset"
            .Left = 1267
            .Top = 12
            .BackColor = Color.Transparent
            .Visible = False
        End With
        Me.Controls.Add(OffsetButton)


        Dim num As New NumericButton
        With num
            .Left = 1172
            .Top = 618
            .BackColor = Color.Transparent
            .Enabled = False
        End With
        Me.Controls.Add(num)

        '_buttPosition.Size = SystemInformation.CaptionButtonSize

    End Sub

    Sub btn_click(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If NormalButton.Visible = True Then
                NormalButton.Visible = False
                OffsetButton.Visible = True
                OffsetButton.Focus()
            Else
                OffsetButton.Visible = False
                NormalButton.Visible = True
                NormalButton.Focus()
            End If
        End If
    End Sub

    Private Sub TabControl1_Selected(sender As System.Object, e As System.Windows.Forms.TabControlEventArgs) Handles TabControl1.Selected
        If TabControl1.SelectedIndex = 0 Then
            Me.Width = 300
            Me.Height = 300
        Else
            '1360, 723
            Me.Width = 1360
            Me.Height = 723
        End If
    End Sub
End Class

Public Class yunButton
    Inherits Button

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        Dim g As Graphics = Me.CreateGraphics
        'g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        If Me.Tag = "Normal" Then
            g.DrawString(Me.Tag, New Font("新細明體", 9, FontStyle.Regular), Brushes.Black, New Point(18, 5))
        Else
            g.DrawString(Me.Tag, New Font("新細明體", 9, FontStyle.Regular), Brushes.Black, New Point(21, 5))
        End If

    End Sub
End Class


Public Class NumericButton
    Inherits Button

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        Dim g As Graphics = Me.CreateGraphics
        'g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        g.DrawString("+", New Font("新細明體", 9, FontStyle.Regular), Brushes.Black, New Point(5, 5))
        g.DrawString("-", New Font("新細明體", 9, FontStyle.Regular), Brushes.Black, New Point(Me.Width - 12, 5))

        g.DrawString("0", New Font("新細明體", 9, FontStyle.Regular), Brushes.Black, New Point(Me.Width / 2 - 5, 5))
    End Sub
End Class