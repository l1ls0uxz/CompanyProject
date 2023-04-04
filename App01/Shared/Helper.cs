using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App01;

namespace App01.Shared
{
    public static class Helper
    {
        private static readonly string baseURl = "http://192.168.1.26:55555/api/webapi/";
        public static async Task<string> GetDate(string name, string datefrom, string dateto)
        {

            using (HttpClient client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            }))
            {
                using (HttpResponseMessage res = await client.GetAsync(baseURl + $"{name}" + $"/'{datefrom}'" + $"/'{dateto}'"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();

                        if (data != null)
                        {

                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }

        //public static async Task<string> GetTable(string name, string date)
        //{

        //    using (HttpClient client = new HttpClient(new HttpClientHandler
        //    {
        //        AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
        //    }))
        //    {
        //        using (HttpResponseMessage res = await client.GetAsync(baseURl + $"{name}" + $"/{date}"))
        //        {
        //            using (HttpContent content = res.Content)
        //            {
        //                string data = await content.ReadAsStringAsync();

        //                if (data != null)
        //                {

        //                    return data;
        //                }
        //            }
        //        }
        //    }
        //    return string.Empty;
        //}

        //public static async Task<string> GetDateTime(string name, string datereport, string time, bool check)
        //{
        //    using (HttpClient client = new HttpClient(new HttpClientHandler
        //    {
        //        AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
        //    }))
        //    {
        //        using (HttpResponseMessage res = await client.GetAsync(baseURl + $"{name}" + $"/{datereport}" + $"/{time}" + $"/'{check}'"))
        //        {
        //            using (HttpContent content = res.Content)
        //            {
        //                string data = await content.ReadAsStringAsync();

        //                if (data != null)
        //                {

        //                    return data;
        //                }
        //            }
        //        }
        //    }
        //    return string.Empty;
        //}

        public static string BeautifyJson(string jsonStr)
        {
            JToken parseJson = JToken.Parse(jsonStr);
            return parseJson.ToString(Formatting.Indented);
        }
    }
}
