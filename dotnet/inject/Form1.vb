Public Class Form1
    Public Declare Function VirtualAllocEx Lib "kernel32" (ByVal hProcess As Integer, ByVal lpAddress As Integer, ByVal dwSize As Integer, ByVal flAllocationType As Integer, ByVal flProtect As Integer) As Integer
    Public Const MEM_COMMIT = 4096, PAGE_EXECUTE_READWRITE = &H40
    Public Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer As Byte(), ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Public Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
    Private Declare Function GetModuleHandle Lib "Kernel32" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Integer
    Public Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByRef lpThreadId As Integer) As Integer


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim DllPath As String = Application.StartupPath + "/test.dll"
        '先宣告變數來存放你要注入的DLL

        'Dim TargetHandle As IntPtr = Process.GetProcessesByName("ProcessName")(0).Handle
        Dim TargetHandle As IntPtr = Me.Handle
        '07EA0838

        '然後透過Process類別去取得Handle
        '特別注意一下這個Handle取到了就同等於用OpenProcess得到控制句柄了喔

        If (TargetHandle.Equals(IntPtr.Zero)) Then
            MsgBox("對進程進行打開進程行為失敗.")
            Exit Sub
        End If

        '獲取LoadLibraryA的地址(PS:不同進程但同API,地址相同).
        Dim GetAdrOfLLBA As IntPtr = GetProcAddress(GetModuleHandle("Kernel32"), "LoadLibraryA")
        If (GetAdrOfLLBA.Equals(IntPtr.Zero)) Then
            MsgBox("取得LoadLibraryA API函數基址失敗.")
            Exit Sub
        End If
        '將DLL路徑轉為Char()陣列.
        Dim OperaChar As Byte() = System.Text.Encoding.Default.GetBytes(DllPath)
        '在目標進程申請一塊空間存放路徑字串.
        Dim DllMemPathAdr = VirtualAllocEx(TargetHandle, 0&, &H64, MEM_COMMIT, PAGE_EXECUTE_READWRITE)
        If (DllMemPathAdr.Equals(IntPtr.Zero)) Then
            MsgBox("對進程申請空間時發生錯誤.")
            Exit Sub
        End If
        '將申請來的記憶體空間寫入路徑Char()陣列.
        If (WriteProcessMemory(TargetHandle, DllMemPathAdr, OperaChar, OperaChar.Length, 0) = False) Then
            MsgBox("對進程寫入記憶體時發生錯誤!")
            Exit Sub
        End If
        '令目標進程呼叫LoadLibraryA加載Char()陣列中存放的路徑.
        CreateRemoteThread(TargetHandle, 0, 0, GetAdrOfLLBA, DllMemPathAdr, 0, 0)
        MsgBox("對進程注入完成")
    End Sub
End Class
