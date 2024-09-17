using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileAPP.Services
{
    public static class ApiService <T>
    {
        public static async Task<T> Get(string @params)
        {
            string url = "http://10.131.76.121/api/values/" + @params;

            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<T>(res);
            }
        }

        public static async Task<bool> Check(string @params)
        {
            string url = "http://10.131.76.121/api/values/" + @params;

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

        public static async Task<bool> Post(string @params, T t)
        {
            string url = "http://10.131.76.121/api/values/" + @params;

            var jsonStr = JsonConvert.SerializeObject(t);
            var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

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
