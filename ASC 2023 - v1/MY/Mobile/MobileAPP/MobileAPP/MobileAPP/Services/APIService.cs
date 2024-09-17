using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileAPP.Services
{
    public static class APIService<T>
    {
        public static async Task<T> Get(string @params)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"http://10.131.76.121/api/values/" + @params;
                var res = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<T>(res);
            }
        }
    }
}
