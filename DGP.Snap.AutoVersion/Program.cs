using System;

namespace DGP.Snap.AutoVersion
{
    internal class Program
    {
        /// <summary>
        /// 编译时，项目版本号自动加1的工具
        /// <para>简单模式的编程思路：</para>
        /// <para>取项目 AssemblyInfo.cs 文件中的版本号</para>
        /// <para>找到指定内容的行，版本号加1，保存回去，完事</para>
        /// <para>使用方法：</para>
        /// <para>VS2008中打开要处理的项目，对其属性下的生成事件页签，预生成事件命令行输入</para>
        /// <para><code>T035 "$(ProjectDir)"</code></para>
        /// <para>T035前面要不要指定路径就由你自己决定了，最简单的，就是把T035.exe放到System32目录</para>
        /// </summary>
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("没有设置参数，应该要设为：Snap.AutoVersion \"$(ProjectDir)\"");
                return;
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("工具有多个参数，是不是你的命令行没带双引号，而项目路径又带空格？");
                return;
            }

            string sPath = args[0].Replace("\"", string.Empty) + "\\Properties\\";
            string sAssemOld = sPath + "AssemblyInfo.old";
            string sAssem = sPath + "AssemblyInfo.cs";
            string sAssemNew = sPath + "AssemblyInfo.new";

            // 检测AssemblyInfo文件是否存在来决定路径是否设置正确
            if (System.IO.File.Exists(sAssem) == false)
            {
                string sInfo = "未检测到文件存在：" + sAssem + "\r\n" + "路径是否有设置错误？";
                Console.WriteLine(sInfo);
                return;
            }

            // TODO: 自行检测文件是否只读等问题
            System.IO.StreamReader sr = new System.IO.StreamReader(sAssem);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(sAssemNew, false, sr.CurrentEncoding);

            string line;
            string newLine;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.IndexOf("[assembly: AssemblyVersion") == 0)
                {
                    // 找到这两行了，修改它们
                    newLine = GetTargetVersion(line);
                }
                else
                {
                    newLine = line;
                }

                sw.WriteLine(newLine);
            }

            sw.Close();
            sr.Close();

            // 备份文件删除（只读属性时将会出错）
            if (System.IO.File.Exists(sAssemOld) == true)
            {
                System.IO.File.Delete(sAssemOld);
            }

            // 原文件改名备份（只读属性下允许正常改名）
            System.IO.File.Move(sAssem, sAssemOld);
            // 新文件改为原文件（原只读属性将会丢失）
            System.IO.File.Move(sAssemNew, sAssem);
        }

        /// <summary>
        /// 对输入的字符串, 取其中版本最后部分+1
        /// </summary>
        /// <param name="sLine">输入的字符串，类似：[assembly: AssemblyVersion("1.0.0.4")]</param>
        /// <returns>版本最后部分+1 后的结果</returns>
        private static string GetTargetVersion(string sLine)
        {
            // 定位起始位置与结束位置
            int posStart = sLine.IndexOf("(\"");
            if (posStart < 0)
            {
                Console.WriteLine("该字符串找不到版本号起始标志\"：" + sLine);
                Environment.Exit(0);
            }
            int posEnd = sLine.IndexOf("\")", posStart);
            if (posEnd < 0)
            {
                Console.WriteLine("该字符串找不到版本号结束标志\"：" + sLine);
                Environment.Exit(0);
            }
            // TODO: 自行去保证数据正确性，例如：1.0.7 或 1.0.0.7A
            string sVer = sLine.Substring(posStart + 2, posEnd - posStart - 2);
            VersionEx currentVersion = new VersionEx(sVer);
            VersionEx newVersion = new VersionEx(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, currentVersion.Revision);
            if (newVersion > currentVersion)
            {
                newVersion.Revision = 0;
            }
            else
            {
                newVersion.Revision = currentVersion.Revision + 1;
            }

            return "[assembly: AssemblyVersion(\"" + newVersion + "\")]";
        }
    }
}
