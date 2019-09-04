using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avtool10
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class file
    {
        private string path = @"d:\completed";

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

        public string renamedir(string startup, string oldname, string newname)
        {
            oldname = this.decode64(oldname);
            newname = this.decode64(newname);

            string oldpath = this.path + @"\" + oldname;
            string newpath = this.path + @"\" + newname;

            string rtext = "path error";

            if (System.IO.Directory.Exists(oldpath) && !System.IO.Directory.Exists(newpath))
            {
                try
                {
                    System.IO.Directory.Move(oldpath, newpath);
                    rtext = "success";
                }
                catch (Exception e)
                {
                    rtext = e.Message.ToString();
                }
                //System.IO.Directory.Move(oldpath, newpath);

                string dir = this.read(startup + @"\dir.txt");
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
                this.save(startup + @"\dir.txt", resave);
            }
            return rtext;
        }

        public void setTag(string startup, string index, string code)
        {
            string tag = this.getTag(code);
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

        public string getTag(string code)
        {
            string[] tagname = new string[62];
            tagname[0] = "已分類";
            tagname[1] = "未分類";
            tagname[2] = "有碼";
            tagname[3] = "無碼";
            tagname[4] = "日韓";
            tagname[5] = "本土";
            tagname[6] = "歐美";
            tagname[7] = "多女優";
            tagname[8] = "單女優";
            tagname[9] = "有封面";
            tagname[10] = "無封面";
            tagname[11] = "A Cup";
            tagname[12] = "B Cup";
            tagname[13] = "C Cup";
            tagname[14] = "D Cup";
            tagname[15] = "E Cup";
            tagname[16] = "F Cup";
            tagname[17] = "G Cup";
            tagname[18] = "H Cup";
            tagname[19] = "I Cup";
            tagname[20] = "J Cup";
            tagname[21] = "Other";

            tagname[22] = "學生"; tagname[26] = "醫生"; tagname[30] = "人妻"; tagname[34] = "上班族"; tagname[38] = "寫真"; tagname[42] = "個別"; tagname[46] = "眼鏡"; tagname[50] = "幼齒"; tagname[54] = "誘惑"; tagname[58] = "素人企劃";
            tagname[23] = "老師"; tagname[27] = "護士"; tagname[31] = "近親"; tagname[35] = "感謝祭"; tagname[39] = "自拍"; tagname[43] = "後宮"; tagname[47] = "白嫩"; tagname[51] = "熟女"; tagname[55] = "誘騙"; tagname[59] = "街頭約砲";
            tagname[24] = "店員"; tagname[28] = "病人"; tagname[32] = "明星"; tagname[36] = "男素人"; tagname[40] = "偷拍"; tagname[44] = "亂交"; tagname[48] = "豐滿"; tagname[52] = "自慰"; tagname[56] = "昏迷"; tagname[60] = "時間停止";
            tagname[25] = "警察"; tagname[29] = "看護"; tagname[33] = "女優"; tagname[37] = "女素人"; tagname[41] = "走光"; tagname[45] = "輪交"; tagname[49] = "纖瘦"; tagname[53] = "痴女"; tagname[57] = "強姦"; tagname[61] = "角色扮演";

            if (code == "")
            {
                return "";
            }
            string[] tags = code.Split(',');
            string r = "";
            for (int i = 0; i < tags.Length; i++)
            {
                int index = int.Parse(tags[i]);
                r += "#" + tagname[index];
            }
            return r;
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
