using DGP.Snap.Framework.NativeMethods;

namespace DGP.Snap.Framework.Device
{
    public class Monitor
    {
        public static int CurrentRefreshRate()
        {
            var vDevMode = new User32.DEVMODE();
            return User32.EnumDisplaySettings(null, User32.ENUM_CURRENT_SETTINGS, ref vDevMode) ? vDevMode.dmDisplayFrequency : 60;
        }
    }
}
