using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cShapOOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        txtfile txt = new txtfile();
        List<target> t = new List<target>();
        string windowsPath = Application.StartupPath + @"/" + "windows.txt";
        string processesPath = Application.StartupPath + @"/" + "processes.txt";

        private void Form1_Load(object sender, EventArgs e)
        {
            string windows = txt.read(windowsPath), processes = txt.read(processesPath);
            string[] exwin = windows.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] expro = processes.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < exwin.Length; i++)
            {
                target temp = new target(false, exwin[i]);
                t.Add(temp);
                listBox1.Items.Add("window: " + exwin[i]);
            }

            for (int i = 0; i < expro.Length; i++)
            {
                target temp = new target(true, expro[i]);
                t.Add(temp);
                listBox1.Items.Add("process: " + expro[i]);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string windows = "", processes = "";
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].getType())
                {
                    if (processes == "")
                    {
                        processes += t[i].getName();
                    }
                    else
                    {
                        processes += Environment.NewLine + t[i].getName();
                    }
                }
                else
                {
                    if (windows == "")
                    {
                        windows += t[i].getName();
                    }
                    else
                    {
                        windows += Environment.NewLine + t[i].getName();
                    }
                }
            }
            txt.save(processesPath, processes);
            txt.save(windowsPath, windows);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //start & stop
            foreach (Control myControl in this.Controls)
            {
                myControl.Enabled = timer1.Enabled;
            }
            if (button1.Text == "start")
            {
                button1.Text = "stop";
                timer1.Enabled = true;
            }
            else
            {
                button1.Text = "start";
                timer1.Enabled = false;
            }
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //delete
            int index = listBox1.SelectedIndex;
            t.RemoveAt(index);
            listBox1.Items.RemoveAt(index);
            if (index == listBox1.Items.Count)
            {
                listBox1.SelectedIndex = index - 1;
            }
            else
            {
                listBox1.SelectedIndex = index;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //new process
            if (textBox1.Text != "")
            {
                listBox1.Items.Add("process: " + textBox1.Text);
                target temp = new target(true, textBox1.Text);
                t.Add(temp);
                textBox1.Text = "";
                textBox1.Focus();
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //new window
            if (textBox1.Text != "")
            {
                listBox1.Items.Add("window: " + textBox1.Text);
                target temp = new target(false, textBox1.Text);
                t.Add(temp);
                textBox1.Text = "";
                textBox1.Focus();
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].alive())
                {
                    t[i].kill();
                }
            }
        }
    }

    class txtfile
    {
        public string read(string path)
        {
            string r;
            System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("big5"));
            r = sr.ReadToEnd();
            sr.Close();
            return r;
        }

        public void save(string path, string text)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, System.Text.Encoding.GetEncoding("big5"));
            sw.Write(text);
            sw.Close();
        }
    }

    class target
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int FindWindow(string strclassName, string strWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_CLOSE = 0xF060;

        private bool type; //type = true ? process : window
        private string name;

        public target(bool type, string name)
        {
            this.type = type;
            this.name = name;
        }

        public bool getType()
        {
            return this.type;
        }

        public string getName()
        {
            return this.name;
        }

        public bool alive()
        {
            if (this.type)
            {
                System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcesses();
                for (int i = 0; i < p.Length; i++)
                {
                    if (p[i].ProcessName.ToLower() == this.name.ToLower())
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                if (FindWindow(null, this.name) != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void kill()
        {
            try
            {
                if (this.type)
                {
                    System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcesses();
                    for (int i = 0; i < p.Length; i++)
                    {
                        if (p[i].ProcessName.ToLower() == this.name.ToLower())
                        {
                            p[i].Kill();
                        }
                    }
                }
                else
                {
                    int i = FindWindow(null, this.name);
                    SendMessage(i, WM_SYSCOMMAND, SC_CLOSE, 0);
                }
            }
            catch
            { }
        }
    }
}