using System;
using System.Windows.Interop;

namespace DGP.Snap.Framework.Extensions
{
    public static class WindowExtensions
    {
        /// <summary>
        /// 获取窗体的句柄
        /// </summary>
        /// <param name="window"></param>
        /// <returns>整型窗体句柄指针</returns>
        public static IntPtr GetHandle(this System.Windows.Window window) => new WindowInteropHelper(window).Handle;
    }
}
