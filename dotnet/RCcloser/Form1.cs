using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCcloser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int FindWindow(string strclassName, string strWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_CLOSE = 0xF060;

        private void timer1_Tick(object sender, EventArgs e)
        {
            string class_name = "RCWindow";
            string window_name = "";

            int i = FindWindow(class_name, window_name);
            if (i != 0)
            {
                SendMessage(i, WM_SYSCOMMAND, SC_CLOSE, 0);
                this.Text = "found";
            }
            else
            {
                this.Text = "didnt find";
            }
        }
    }
}
