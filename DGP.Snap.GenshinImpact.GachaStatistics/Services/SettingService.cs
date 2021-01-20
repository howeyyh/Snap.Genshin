using DGP.Snap.Framework.Shell.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.GenshinImpact.GachaStatistics.Services
{
    public static class SettingService
    {
        public static Setting Setting { get; private set; } = new Setting("Snap GensheinImpact");
    }
}
