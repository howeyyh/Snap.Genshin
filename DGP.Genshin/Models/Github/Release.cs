using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.Models.Github
{
    public class Release
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("tag_name")] public string TagName { get; set; }
        [JsonProperty("target_commitish")] public string TargetCommitish { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("author")] public People Author { get; set; }
        [JsonProperty("prerelease")] public string Prerelease { get; set; }
        [JsonProperty("created_at")] public string CreatedAt { get; set; }
        [JsonProperty("assets")] public List<AssetsItem> Assets { get; set; }
        [JsonProperty("body")] public string Body { get; set; }
    }
}
