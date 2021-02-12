using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGP.Genshin.DataViewer.Services
{
    class WorkingFolderService
    {
        public static string SelectWorkingFolder()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog
            {
                Description = "选择数据所在文件夹",
            };
            if (folder.ShowDialog() == DialogResult.OK)
            {
                return folder.SelectedPath;
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
