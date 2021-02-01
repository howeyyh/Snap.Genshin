using System.Diagnostics;
using System.IO.Compression;
using System.Threading;

namespace DGP.Snap.Updater
{
    internal class Program
    {
        private static void Main(string[] args)
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
