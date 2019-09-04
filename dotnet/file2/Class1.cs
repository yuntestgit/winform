using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace file2
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class AVtool
    {
        private string path = @"d:\completed";

        public void setTag(string startup, string index, string tag)
        {
            string dir = this.read(startup + @"\dir.txt");

            string[] ex1 = dir.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string tempdir = "", oldname = "", newname = "", resave = "";

            for (int i = 0; i < ex1.Length; i++)
            {
                string[] ex2 = ex1[i].Split('?');
                if (ex2[0] == index)
                {
                    tempdir = this.path + @"\" + ex2[1];
                    oldname = tempdir + @"\" + ex2[3] + ".txt";
                    newname = tempdir + @"\" + tag + ".txt";

                    if (tag == "")
                    {
                        if (System.IO.File.Exists(oldname))
                        {
                            System.IO.File.Delete(oldname);
                        }
                    }
                    else
                    {
                        if (System.IO.File.Exists(oldname))
                        {
                            System.IO.File.Move(oldname, newname);
                        }
                        else
                        {
                            this.save(newname, "");
                        }
                    }

                    ex2[3] = tag;
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

            this.save(startup + @"\dir.txt", resave);
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
    }
}
