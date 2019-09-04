using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace renamedir
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string value1, string value2, string value3)
        {
            InitializeComponent();
            loading(value1, value2, value3);
        }

        public void loading(string value1, string value2, string value3)
        {
            startup = value1;
            oldname = value2;
            newname = value3;
            arg = "arg";
        }

        string arg, startup, oldname, newname, path = @"d:\completed";

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

        public string read(string path)
        {
            UTF8Encoding utf8 = new UTF8Encoding(false);
            string r;
            System.IO.StreamReader sr = new System.IO.StreamReader(path, utf8);
            r = sr.ReadToEnd();
            sr.Close();
            return r;
        }

        public void save(string path, string text)
        {
            UTF8Encoding utf8 = new UTF8Encoding(false);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, utf8);
            sw.Write(text);
            sw.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (arg == "arg")
            {
                string log = "";

                oldname = decode64(oldname);
                newname = decode64(newname);

                log += oldname + "\r\n\r\n";
                log += newname + "\r\n\r\n";

                string oldpath = path + @"\" + oldname;
                string newpath = path + @"\" + newname;

                string rtext = "success";
                if (System.IO.Directory.Exists(oldpath) && !System.IO.Directory.Exists(newpath))
                {
                    try
                    {
                        System.IO.Directory.Move(oldpath, newpath);
                    }
                    catch (Exception ex)
                    {
                        rtext = ex.Message.ToString();
                    }

                    if (rtext == "success")
                    {
                        string dir = read(startup + @"\dir.txt");
                        string resave = "";

                        string[] ex1 = dir.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < ex1.Length; i++)
                        {
                            string[] ex2 = ex1[i].Split('?');
                            if (ex2[1] == oldname)
                            {
                                ex2[1] = newname;
                                ex1[i] = ex2[0] + "?" + ex2[1] + "?" + ex2[2] + "?" + ex2[3];
                            }
                        }

                        for (int i = 0; i < ex1.Length; i++)
                        {
                            if (resave == "")
                            {
                                resave += ex1[i];
                            }
                            else
                            {
                                resave += Environment.NewLine + ex1[i];
                            }
                        }
                        save(startup + @"\dir.txt", resave);
                    }
                }
                else
                {
                    rtext = "path error";
                }
                log += rtext;
                save(startup + @"\log.txt", log);
                MessageBox.Show(rtext);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            startup = @"D:\FavoritesVersion\v2";
            oldname = "__素人-D奶現役専門大学生夜遊び事情";
            newname = "__素人@xxx-D奶現役専門大学生夜遊び事情";

            string log = "";

            //oldname = decode64(oldname);
            //newname = decode64(newname);

            log += oldname + "\r\n\r\n";
            log += newname + "\r\n\r\n";

            string oldpath = path + @"\" + oldname;
            string newpath = path + @"\" + newname;

            string rtext = "success";
            if (System.IO.Directory.Exists(oldpath) && !System.IO.Directory.Exists(newpath))
            {
                try
                {
                    System.IO.Directory.Move(oldpath, newpath);
                }
                catch (Exception ex)
                {
                    rtext = ex.Message.ToString();
                }

                if (rtext == "success")
                {
                    string dir = read(startup + @"\dir.txt");
                    string resave = "";

                    string[] ex1 = dir.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ex1.Length; i++)
                    {
                        string[] ex2 = ex1[i].Split('?');
                        if (ex2[1] == oldname)
                        {
                            ex2[1] = newname;
                            ex1[i] = ex2[0] + "?" + ex2[1] + "?" + ex2[2] + "?" + ex2[3];
                        }
                    }

                    for (int i = 0; i < ex1.Length; i++)
                    {
                        if (resave == "")
                        {
                            resave += ex1[i];
                        }
                        else
                        {
                            resave += Environment.NewLine + ex1[i];
                        }
                    }
                    save(startup + @"\dir.txt", resave);
                }
            }
            else
            {
                rtext = "path error";
            }
            log += rtext;
            save(startup + @"\log.txt", log);
            MessageBox.Show(rtext);
        }
    }
}
