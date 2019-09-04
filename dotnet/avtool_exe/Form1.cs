using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace avtool_exe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            file f = new file();
            textBox1.Text = f.decode64(f.getdir());

            //renamedir //pass
            /*
            string oldname = @"C:\_夢乃あいか-生理が始まる2日前のセックス";
            string newname = @"C:\_夢乃あいか@哈哈囉-生理が始まる2日前のセックス";

            oldname = f.encode64(oldname);
            newname = f.encode64(newname);

            textBox2.Text = f.decode64(f.renamedir(oldname, newname));
            */


            //renamepng //pass
            /*
            string oldname = @"C:\_夢乃あいか@哈哈囉-生理が始まる2日前のセックス\temp.png";
            string newname = @"C:\_夢乃あいか@哈哈囉-生理が始まる2日前のセックス\temp2.png";

            oldname = f.encode64(oldname);
            newname = f.encode64(newname);

            f.renamepng(oldname, newname);
            */


            //renametxt //pass
            /*
            string oldname = @"D:\completed\__素人-20代の美乳妻が通う発狂媚薬エステ 8\#有碼#日韓#多女優#有封面#B Cup#D Cup#E Cup#上班族#女素人#偷拍#個別#自慰#誘騙#素人企劃.txt";
            string newname = @"D:\completed\__素人-20代の美乳妻が通う発狂媚薬エステ 8\#無碼#日韓#多女優#有封面#B Cup#D Cup#E Cup#上班族#女素人#偷拍#個別#自慰#誘騙#素人企劃.txt";


            oldname = f.encode64(oldname);
            newname = f.encode64(newname);

            f.renametxt(oldname, newname);
            */
        }
    }

    public class file
    {
        public string getdir(string path = @"d:\completed")
        {
            if (path.IndexOf(@"\", path.Length - 1) == -1)
            {
                path = path + @"\";
            }

            string rtext = "";

            string[] d = System.IO.Directory.GetDirectories(path);
            Array.Sort(d);

            for (int i = 0; i < d.Length; i++)
            {
                string[] f = System.IO.Directory.GetFiles(d[i]);
                string dir = "", id = "", files = "", tags = "";

                dir = d[i].Replace(path, "");
                for (int j = 0; j < f.Length; j++)
                {
                    string jex = System.IO.Path.GetExtension(f[j]).ToLower();

                    if (jex == ".png")
                    {
                        id = f[j].Replace(d[i], "").Replace(@"\", "").Replace(".png", "");
                    }
                    else if (jex == ".txt")
                    {
                        tags = f[j].Replace(d[i], "").Replace(@"\", "").Replace(".txt", "");
                    }
                    else
                    {
                        files += "$" + f[j].Replace(d[i], "").Replace(@"\", "");
                    }
                }

                rtext += dir + "?" + id + "?" + files + "?" + tags;

                if (i != d.Length - 1)
                {
                    rtext += Environment.NewLine;
                }
            }
            return encode64(rtext);
        }

        public string renamedir(string oldname, string newname)
        {
            oldname = this.decode64(oldname);
            newname = this.decode64(newname);

            string rtext; //Success || AccessDenied || PathError

            if (System.IO.Directory.Exists(oldname) && !System.IO.Directory.Exists(newname))
            {
                rtext = "Success";
                try
                {
                    System.IO.Directory.Move(oldname, newname);
                }
                catch (Exception)
                {
                    rtext = "AccessDenied";
                }
            }
            else
            {
                rtext = "PathError";
            }

            return encode64(rtext);
        }

        public string renamepng(string oldname, string newname)
        {
            try
            {
                oldname = this.decode64(oldname);
                newname = this.decode64(newname);

                System.IO.File.Move(oldname, newname);
                return this.encode64(oldname + newname);
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string renametxt(string oldname, string newname)
        {
            try
            {
                oldname = this.decode64(oldname);
                newname = this.decode64(newname);

                if (System.IO.File.Exists(oldname))
                {
                    System.IO.File.Delete(oldname);
                }

                if (newname != "")
                {
                    this.save(newname, "");
                }
                return this.encode64(oldname + newname);
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
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
