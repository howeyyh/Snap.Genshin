using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.DataViewer.Helper
{
    public class FileEx
    {
        public FileEx(string fullPath)
        {
            FullPath = fullPath;
        }
        public string FullPath { get; set; }
        public string FileName => Path
            .GetFileNameWithoutExtension(FullPath)
            .Replace("Excel","").Replace("Config", "").Replace("Data", "");
        public override string ToString()
        {
            return FileName;
        }

        public string ReadFile()
        {
            string json;
            using (StreamReader sr = new StreamReader(FullPath))
            {
                json = sr.ReadToEnd();
            }
            return json;
        }
    }
}
