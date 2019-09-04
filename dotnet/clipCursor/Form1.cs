using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace clipCursor
{
    public partial class Form1 : Form
    {
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public static implicit operator Rectangle(RECT rect)
            {
                return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
            public static implicit operator RECT(Rectangle rect)
            {
                return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ClipCursor(ref RECT lpRect);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool GetWindowRect(System.IntPtr hWnd, ref RECT lpRect);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, ref RECT lpRect);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Global.lockState = Control.IsKeyLocked(Keys.Scroll);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool nowState = Control.IsKeyLocked(Keys.Scroll);
            if (nowState != Global.lockState) {
                Global.lockState = nowState;
                if (nowState)
                {
                    RECT rect = new RECT(10, 10, 20, 20);
                    ClipCursor(ref rect);
                }
                else
                {
                    //ClipCursor(IntPtr.Zero);
                }
            }
            this.Text = Global.lockState.ToString();
        }

        //public RECT getRange(IntPtr hwnd)
        //{

        //}
    }

    public class Global
    {
        public static bool lockState = false;
    }
}
