using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testsec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string args = @"D:\FavoritesVersion\v2 X0pVTElBLeW9vOWls+OBruOBiuWnieOBleOCk+OBr+W3qOS5s+OBqOS4reWHuuOBl09L44Gn5YOV44KS6KqY5oOR X0pVTElBQHh4eC3lvbzlpbPjga7jgYrlp4njgZXjgpPjga/lt6jkubPjgajkuK3lh7rjgZdPS+OBp+WDleOCkuiqmOaDkQ==";
            System.Diagnostics.Process.Start(@"D:\FavoritesVersion\v2\renamedir.exe", args);
        }

        public static string cmd(string commandText)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true; //不跳出cmd視窗
            string strOutput = null;

            try
            {
                p.Start();

                p.StandardInput.WriteLine("chcp 65001");

                p.StandardInput.WriteLine(commandText);
                p.StandardInput.WriteLine("exit");
                strOutput = p.StandardOutput.ReadToEnd();//匯出整個執行過程
                p.WaitForExit();
                p.Close();


            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }
            return strOutput;
        }

        public string decode64(string code)
        {
            byte[] bytes = Convert.FromBase64String(code);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public string encode64(string code)
        {
            Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(code);
            return Convert.ToBase64String(bytes); ;
        }
    }
}
