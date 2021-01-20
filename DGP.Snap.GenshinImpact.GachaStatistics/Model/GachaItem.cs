using DGP.Snap.Framework.Data.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.GenshinImpact.GachaStatistics.Model
{
    class GachaItem
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("uid")] public string Uid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("gacha_type")] public string GachaType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("item_id")] public string ItemId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("count")] public string Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time")] public string Time { get; set; }
    }
}
