using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.Helper
{
    public static class Json
    {
        /// <summary>
        /// 将JSON异步反序列化为指定的.NET类型
        /// </summary>
        /// <typeparam name="T">要反序列化的对象的类型。</typeparam>
        /// <param name="value">要反序列化的JSON</param>
        /// <returns>JSON字符串中的反序列化对象</returns>
        public static T ToObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 将指定的对象序列化为JSON字符串
        /// </summary>
        /// <param name="value">要序列化的对象</param>
        /// <returns>对象的JSON字符串表示形式</returns>
        public static string Stringify(object value)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                Formatting = Formatting.Indented
            };
            return JsonConvert.SerializeObject(value, jsonSerializerSettings);
        }

        /// <summary>
        /// 向指定 <paramref name="requestUrl"/> 的服务器请求Json数据，并将结果返回为类型为 <typeparamref name="TRequestType"/> 的实例
        /// </summary>
        /// <typeparam name="TRequestType"></typeparam>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public static TRequestType GetWebRequestObject<TRequestType>(string requestUrl)
        {
            string jsonMetaString = GetWebResponse(requestUrl);
            return ToObject<TRequestType>(jsonMetaString);
        }

        /// <summary>
        /// 获取网页响应
        /// </summary>
        /// <param name="requestUrl">请求的URL</param>
        /// <returns>响应字符串</returns>
        private static string GetWebResponse(string requestUrl)
        {
            HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
            //为了能正常的获取GitHub的数据
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Credentials = CredentialCache.DefaultCredentials;

            request.Method = "GET";
            request.ContentType = "application/json;charset=UTF-8";
            request.UserAgent = "Wget/1.9.1";
            request.Timeout = 5000;
            string jsonMetaString;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())//获取响应
            {
                using (StreamReader responseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    jsonMetaString = responseStreamReader.ReadToEnd();
                }
            }
            return jsonMetaString;
        }
    }
}
