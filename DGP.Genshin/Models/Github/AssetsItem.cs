using Newtonsoft.Json;

namespace DGP.Genshin.Models.Github
{
    public class AssetsItem
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("browser_download_url")] public string BrowserDownloadUrl { get; set; }
    }
}
