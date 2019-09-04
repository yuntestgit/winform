using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ontitlebar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("User32.dll")]

        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {

            const int WM_NCPAINT = 0x85;
            const int WM_NCCREATE = 0x0081;
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_NCACTIVATE = 0x0086;

            SolidBrush myBrush;

            base.WndProc(ref m);
            if (m.Msg == WM_NCACTIVATE || m.Msg == WM_NCPAINT || m.Msg == WM_NCCREATE || m.Msg == WM_NCCALCSIZE)
            {
                IntPtr hdc = GetWindowDC(m.HWnd);
                if ((int)hdc != 0)
                {
                    Graphics g = Graphics.FromHdc(hdc);

                    myBrush = new SolidBrush(SystemColors.ActiveCaption);
                    Pen myPen = new Pen(myBrush, 5);

                    myBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
                    g.FillRectangle(myBrush, 0, 0, 150, SystemInformation.CaptionHeight);

                    myBrush = new SolidBrush(Color.FromArgb(120, 255, 0, 0));
                    myPen = new Pen(myBrush, 2);
                    g.DrawRectangle(myPen, 0, 1, 150, SystemInformation.CaptionHeight);

                    myBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
                    g.FillEllipse(myBrush, 150 - 20, 0, 30, SystemInformation.CaptionHeight);

                    myBrush = new SolidBrush(Color.FromArgb(80, 255, 0, 0));
                    g.FillEllipse(myBrush, 150 - 17, 1, 30, SystemInformation.CaptionHeight + 1);

                    myBrush = new SolidBrush(Color.FromArgb(150, 255, 0, 0));
                    myPen = new Pen(myBrush, SystemInformation.FrameBorderSize.Width * 2);
                    g.DrawRectangle(myPen, 0, 0, this.Width, this.Height);


                    myBrush = new SolidBrush(Color.White);
                    Font myFont = new Font(new FontFamily("Arial"), 18, FontStyle.Bold, GraphicsUnit.Pixel);
                    g.DrawString("Some Text", myFont, myBrush, 5, 3);
                    this.Text = "0";
                    g.Flush();
                    ReleaseDC(m.HWnd, hdc);
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
