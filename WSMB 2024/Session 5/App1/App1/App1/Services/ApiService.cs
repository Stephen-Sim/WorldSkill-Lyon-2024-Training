using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{
    public static class ApiService <T>
    {
        public static async Task<bool> Post(string @params, T obj)
        {
            using (var client = new HttpClient())
            {
                var url = "http://10.131.76.121:8090/api/values/" + @params;
                var json = JsonConvert.SerializeObject(obj);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await client.PostAsync(url, content);

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public static async Task<bool> Check(string @params)
        {
            using (var client = new HttpClient())
            {
                var url = "http://10.131.76.121:8090/api/values/" + @params;

                var res = await client.GetAsync(url);

                if (res.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }
        public static async Task<T> Get(string @params)
        {
            using (var client = new HttpClient())
            {
                var url = "http://10.131.76.121:8090/api/values/" + @params;

                var res = await client.GetStringAsync(url);

                return JsonConvert.DeserializeObject<T>(res);
            }
        }

    }
}
