using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace callrenamedir2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s1 = @"D:\FavoritesVersion\v2";
            string s2 = encode64("___abc-D奶現役専門 大学生夜遊び事情20代の美乳妻が通う発狂媚薬エステ 8");
            string s3 = encode64("___abc@def-D奶現役専門 大学生夜遊び事情20代の美乳妻が通う発狂媚薬エステ 8");

            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = @"C:\myProgram\dotnet\renamedir\bin\Debug\renamedir.exe";
            exep.StartInfo.Arguments = s1 + " " + s2 + " " + s3;
            exep.StartInfo.CreateNoWindow = true;
            exep.StartInfo.UseShellExecute = false;
            exep.Start();
        }

        public string encode64(string code)
        {
            Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(code);
            return Convert.ToBase64String(bytes); ;
        }
    }
}
