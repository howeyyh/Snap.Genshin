using System.Runtime.InteropServices;

namespace DGP.Snap.Framework.NativeMethods
{
    public class Kernel32
    {
        #region GlobalMemoryStatus
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MemoryStatusEx
        {
            public uint dwLength; //当前结构体大小
            public uint dwMemoryLoad; //当前内存使用率
            public ulong ullTotalPhys; //总计物理内存大小
            public ulong ullAvailPhys; //可用物理内存大小
            public ulong ullTotalPageFile; //总计交换文件大小
            public ulong ullAvailPageFile; //总计交换文件大小
            public ulong ullTotalVirtual; //总计虚拟内存大小
            public ulong ullAvailVirtual; //可用虚拟内存大小
            public ulong ullAvailExtendedVirtual; //保留 这个值始终为0
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="meminfo"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx(ref MemoryStatusEx meminfo);
        #endregion
    }
}
