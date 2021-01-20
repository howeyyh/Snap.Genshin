using DGP.Snap.Framework.Data.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.GenshinImpact.GachaStatistics.Model
{
    class GachaInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("retcode")] public int Retcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("message")] public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("data")] public GachaData Data { get; set; }
    }
}
