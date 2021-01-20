using DGP.Snap.Framework.Data.Json;
using DGP.Snap.Framework.Net.Web;
using DGP.Snap.Framework.Net.Web.QueryString;
using DGP.Snap.GenshinImpact.GachaStatistics.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DGP.Snap.GenshinImpact.GachaStatistics.Services
{
    class QueryService
    {
        //private static readonly string gachaTypeQueryUrl = @"https://hk4e-api.mihoyo.com/event/gacha_info/api/getConfigList?";
        private static readonly string gachaInfoQueryUrl = @"https://hk4e-api.mihoyo.com/event/gacha_info/api/getGachaLog?";
        private static readonly string gachaItemMatchUrl = @"https://webstatic.mihoyo.com/hk4e/gacha_info/cn_gf01/items/zh-cn.json?ts=16046396";

        public static readonly string StarterGacha = "100";
        public static readonly string NormalGacha = "200";
        public static readonly string RoleUpGacha = "301";
        public static readonly string WeaponUpGacha = "302";

        private static List<GachaMatchInfo> gachaMatchInfos;
        private static List<GachaItem> starterGachaList;
        private static List<GachaItem> normalGachaList;
        private static List<GachaItem> roleUpGachaList;
        private static List<GachaItem> weponUpGachaList;

        public static List<GachaMatchInfo> GachaMatchInfos
        {
            get
            {
                if (gachaMatchInfos == null)
                    gachaMatchInfos = JsonMapper.ToObject<List<GachaMatchInfo>>(Http.GetWebResponse(gachaItemMatchUrl));
                return gachaMatchInfos;
            }
        }
        public static List<GachaItem> StarterGachaList
        {
            get
            {
                if (starterGachaList == null)
                    starterGachaList = GetGachaInfo(StarterGacha);
                return starterGachaList;
            }
            private set => starterGachaList = value;
        }

        public static List<GachaItem> NormalGachaList
        {
            get
            {
                if (normalGachaList == null)
                    normalGachaList = GetGachaInfo(NormalGacha);
                return normalGachaList;
            }
            private set => normalGachaList = value;
        }

        public static List<GachaItem> RoleUpGachaList
        {
            get
            {
                if (roleUpGachaList == null)
                    roleUpGachaList = GetGachaInfo(RoleUpGacha);
                return roleUpGachaList;
            }
            private set => roleUpGachaList = value;
        }

        public static List<GachaItem> WeponUpGachaList 
        {
            get
            {
                if (weponUpGachaList == null)
                    weponUpGachaList = GetGachaInfo(WeaponUpGacha);
                return weponUpGachaList;
            }
            set => weponUpGachaList = value; 
        }


        private static List<GachaItem> GetGachaInfo(string gachatype)
        {
            List<GachaItem> cache = ReadCachedGachaInfo(gachatype) ?? new List<GachaItem>();
            QueryString queryString = new QueryString
            {
                { "authkey_ver", "1" },
                { "sign_type", "2" },
                { "auth_appid", "webview_gacha" },
                { "init_type", gachatype },
                { "gacha_id", "eb44e687757162d2cd66b5c6bfaf980e5b7cf1" },
                { "region", "cn_gf01" },
                { "lang", "zh-cn" },
                { "device_type", "pc" },
                { "ext", "%7b%22loc%22%3a%7b%22x%22%3a2100.506591796875%2c%22y%22%3a210.6112823486328%2c%22z%22%3a-985.1941528320313%7d%7d"},
                { "game_version", "CNRELWin1.0.1_R1284249_S1393824_D1358691" },
                { "authkey", (string)SettingService.Setting["AuthKey"]},
                { "gacha_type", gachatype },
                { "page", "1" },
                { "size", "20" }
            };

            List<GachaItem> buffer = new List<GachaItem>();
            GachaInfo gachaInfo;
            do
            {
                gachaInfo = JsonMapper.ToObject<GachaInfo>(Http.GetWebResponse(gachaInfoQueryUrl + queryString));
                buffer.AddRange(gachaInfo.Data.List);
                queryString.Set("page", $"{Int32.Parse(queryString["page"]) + 1}");
            } while (gachaInfo.Data.List.Count == 20);
            ///merge two list
            GachaItem last = buffer.Find(item => item.Time == buffer.Last().Time);
            GachaItem redundant = cache.FindLast(item => item.Time == last.Time);
            cache.RemoveRange(0, cache.IndexOf(redundant));
            buffer.AddRange(cache);
            SaveGachaInfoCache(buffer, gachatype);

            return buffer;
        }
        private static void SaveGachaInfoCache(List<GachaItem> gachaInfos, string gachaType)
        {
            using (FileStream fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, gachaType + ".json"), FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(JsonMapper.ToJson(gachaInfos));
                }
            }
        }
        private static List<GachaItem> ReadCachedGachaInfo(string gachaType)
        {
            try
            {
                using (FileStream fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, gachaType + ".json"), FileMode.OpenOrCreate))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        char[] buffer = new char[streamReader.BaseStream.Length];
                        streamReader.Read(buffer, 0, (int)streamReader.BaseStream.Length);
                        return JsonMapper.ToObject<List<GachaItem>>(new string(buffer));
                    }
                }
            }
            catch
            {
                return null;
            }

        }
    }
}
