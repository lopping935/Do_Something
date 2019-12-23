using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PublicClass
{
    static public class FileClass
    {
        public static string GetFileVersion(string _filepath)
        {
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(_filepath);
            return myFileVersionInfo.FileVersion;
        }
    }
}
