using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Global.iVars = 0;
            Global.iType = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Global.iVars = 0;
            Global.iType = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Global.iVars = 0;
            Global.iType = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Global.iVars = 0;
            Global.iType = 4;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Global.iVars += 1;
            //this.Text = Global.iVars.ToString();

            if (Global.iType != 0)
            {
                if (Global.iType == 1)
                {
                    if (pictureBox1.Top - Global.iVars <= 0)
                    {
                        pictureBox1.Top = 0;
                    }
                    else
                    {
                        pictureBox1.Top -= Global.iVars;
                    }
                }
                else if (Global.iType == 2)
                {
                    if (pictureBox1.Left - Global.iVars <= 0)
                    {
                        pictureBox1.Left = 0;
                    }
                    else
                    {
                        pictureBox1.Left -= Global.iVars;
                    }
                }
                else if (Global.iType == 3)
                {
                    if (pictureBox1.Top + pictureBox1.Height + Global.iVars >= panel1.Height)
                    {
                        pictureBox1.Top = panel1.Height - pictureBox1.Height;
                    }
                    else
                    {
                        pictureBox1.Top += Global.iVars;
                    }
                }
                else if (Global.iType == 4)
                {
                    if (pictureBox1.Left + pictureBox1.Width + Global.iVars >= panel1.Width)
                    {
                        pictureBox1.Left = panel1.Width - pictureBox1.Width;
                    }
                    else
                    {
                        pictureBox1.Left += Global.iVars;
                    }
                }
            }
        }
    }

    public class Global
    {
        public static int iVars = 0;
        public static int iType = 0;
    }
}
