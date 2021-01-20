using DGP.Snap.Framework.Data.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.GenshinImpact.GachaStatistics.Model
{
    class GachaData
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("page")] public string Page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("size")] public string Size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("total")] public string Total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("list")] public List<GachaItem> List { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("region")] public string Region { get; set; }
    }
}
