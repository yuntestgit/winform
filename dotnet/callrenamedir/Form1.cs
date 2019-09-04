using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace callrenamedir
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string read(string path)
        {
            UTF8Encoding utf8 = new UTF8Encoding(false);
            string r;
            System.IO.StreamReader sr = new System.IO.StreamReader(path, utf8);
            r = sr.ReadToEnd();
            sr.Close();
            return r;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string args = read(Application.StartupPath + @"\renamedir.txt");
            System.Diagnostics.Process.Start(Application.StartupPath + @"\renamedir.exe", args);
            this.Close();
        }
    }
}
