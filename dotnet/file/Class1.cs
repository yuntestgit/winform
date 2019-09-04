using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace file
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class file
    {
        public bool rename(string oldname, string newname)
        {
            if (System.IO.File.Exists(oldname))
            {
                System.IO.File.Move(oldname, newname);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool delete(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
