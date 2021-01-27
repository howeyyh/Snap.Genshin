using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DGP.Snap.Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            InstallPackage();
        }
        private static void InstallPackage()
        {
            Thread.Sleep(2000);
            using (ZipArchive archive = ZipFile.OpenRead("Package.zip"))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    entry.ExtractToFile(entry.FullName, overwrite: true);
                }
            }
            Process.Start("DGP.Genshin.exe");
        }
    }
}
