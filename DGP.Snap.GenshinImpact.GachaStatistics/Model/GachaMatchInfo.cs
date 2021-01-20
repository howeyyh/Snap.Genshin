using DGP.Snap.Framework.Data.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.GenshinImpact.GachaStatistics.Model
{
    class GachaMatchInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("item_id")] public string ItemId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")] public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("item_type")] public string ItemType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("rank_type")] public string Rank { get; set; }
    }
}
