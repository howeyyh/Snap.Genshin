using System;
using System.Windows.Forms;

namespace DGP.Snap.Framework.Extensions
{
    public static class NotifyIconExtensions
    {
        /// <summary>
        /// 显示Windows系统通知
        /// </summary>
        /// <param name="content">显示的内容</param>
        /// <param name="clickEvent">点击通知触发的<see cref="Action"/></param>
        /// <param name="closeEvent">通知消失时触发的<see cref="Action"/></param>
        /// <param name="timedout">通知显示的时间，以毫秒为单位</param>
        public static void ShowNotification(this NotifyIcon icon, string content, Action clickEvent = null, Action closeEvent = null, int timedout = 3000)
        {
            //NotifyIcon icon = NotifyIconManager.GetInstance().NotifyIcon;
            icon.ShowBalloonTip(timedout, null, content, ToolTipIcon.None);
            icon.BalloonTipClicked += OnIconOnBalloonTipClicked;
            icon.BalloonTipClosed += OnIconOnBalloonTipClosed;

            void OnIconOnBalloonTipClicked(object sender, EventArgs e)
            {
                clickEvent?.Invoke();
                icon.BalloonTipClicked -= OnIconOnBalloonTipClicked;
            }

            void OnIconOnBalloonTipClosed(object sender, EventArgs e)
            {
                closeEvent?.Invoke();
                icon.BalloonTipClosed -= OnIconOnBalloonTipClosed;
            }
        }
    }
}
