using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrud.Services
{
    public class ApiService <T>
    {
        public static string URL { get; set; } = "http://10.105.13.186:8090/api/values/";

        public static async Task<T> Get(string uri)
        {
            var url = URL + $"{uri}";

            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<T>(res);
            }
        }

        public static async Task<bool> Check(string uri)
        {
            var url = URL + $"{uri}";

            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync(url);

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public static async Task<bool> Store(string uri, T obj)
        {
            var url = URL + $"{uri}";
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var res = await client.PostAsync(url, content);

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
