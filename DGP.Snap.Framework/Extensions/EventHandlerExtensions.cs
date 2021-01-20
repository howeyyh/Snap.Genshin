using System;

namespace DGP.Snap.Framework.Extensions
{
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="evt"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SafeInvoke<T>(this EventHandler<T> evt, object sender, T e) where T : EventArgs => evt?.Invoke(sender, e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SafeInvoke(this EventHandler evt, object sender, EventArgs e) => evt?.Invoke(sender, e);
    }
}
