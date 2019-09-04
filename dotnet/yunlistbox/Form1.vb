Public Class Form1
    Declare Auto Function DwmIsCompositionEnabled Lib "dwmapi.dll" Alias "DwmIsCompositionEnabled" (ByRef pfEnabled As Boolean) As Integer

    Declare Auto Function DwmExtendFrameIntoClientArea Lib "dwmapi.dll" Alias "DwmExtendFrameIntoClientArea" (ByVal hWnd As IntPtr, ByRef pMargin As Margins) As Integer

    Public Structure Margins
        Public Left As Integer
        Public Right As Integer
        Public Top As Integer
        Public Bottom As Integer
    End Structure

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ListBox1.DrawMode = DrawMode.OwnerDrawFixed

        Dim margins As Margins = New Margins
        margins.Left = -1
        margins.Right = -1
        margins.Top = -1
        margins.Bottom = -1
        Dim hwnd As IntPtr = Me.Handle
        Dim result As Integer = DwmExtendFrameIntoClientArea(hwnd, margins)
        Me.BackColor = Color.Black
    End Sub

    Private Sub ListBox1_DrawItem(sender As System.Object, e As System.Windows.Forms.DrawItemEventArgs) Handles ListBox1.DrawItem

        If e.State = DrawItemState.Selected Then
            e.Graphics.FillRectangle(New SolidBrush(Color.DimGray), e.Bounds)    'Fill the item's rectangle with our highlight

            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))

            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X + 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.White, New Rectangle(e.Bounds.X - 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))

            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.Black, e.Bounds)  'Draw the text for the item

        ElseIf e.State = DrawItemState.None Then
            e.DrawBackground()
            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))

            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X + 1, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height))
            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.Black, New Rectangle(e.Bounds.X - 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
            e.Graphics.DrawString(ListBox1.Items.Item(e.Index), e.Font, Brushes.White, e.Bounds)
        End If



        e.DrawFocusRectangle()
    End Sub
End Class