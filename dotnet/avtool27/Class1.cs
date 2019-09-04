using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avtool27
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class file
    {
        public string getdir()
        {
            return getdir(@"d:\completed");
        }

        public string getdir(string path)
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
            return renamedir(@"d:\completed", oldname, newname);
        }

        public string renamedir(string path, string oldname, string newname)
        {
            if (path.IndexOf(@"\", path.Length - 1) == -1)
            {
                path = path + @"\";
            }

            oldname = this.decode64(oldname);
            newname = this.decode64(newname);

            string oldpath = path + oldname;
            string newpath = path + newname;

            string rtext; //Success || AccessDenied || PathError

            if (System.IO.Directory.Exists(oldpath) && !System.IO.Directory.Exists(newpath))
            {
                rtext = "Success";
                try
                {
                    System.IO.Directory.Move(oldpath, newpath);
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

        public void renamePNG(string path, string oldname, string newname)
        {
            if (path.IndexOf(@"\", path.Length - 1) == -1)
            {
                path = path + @"\";
            }

            oldname = this.decode64(oldname);
            newname = this.decode64(newname);

            string oldpath = path + oldname;
            string newpath = path + newname;

            if (newname == "")
            {
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Move(oldpath, "index.png");
                }
            }
            else
            {
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Move(oldpath, newpath);
                }
            }
        }

        public void renameTXT(string path, string oldname, string newname)
        {
            if (path.IndexOf(@"\", path.Length - 1) == -1)
            {
                path = path + @"\";
            }

            oldname = this.decode64(oldname);
            newname = this.decode64(newname);

            string oldpath = path + oldname;
            string newpath = path + newname;

            if (newname == "")
            {
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Delete(oldpath);
                }
            }
            else
            {
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Move(oldpath, newpath);
                }
                else
                {
                    this.save(newpath, "");
                }
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
