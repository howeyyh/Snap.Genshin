using System.Diagnostics;

namespace DGP.Snap.Framework.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="debugString"></param>
        public static void DebugWriteLine(this object obj, string debugString = null)
        {
            Debug.WriteLine(obj);
            if (debugString != null)
            {
                Debug.WriteLine(debugString);
            }
        }
    }
}
