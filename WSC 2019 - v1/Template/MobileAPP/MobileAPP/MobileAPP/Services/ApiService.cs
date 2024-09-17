﻿using MobileAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileAPP.Services
{
    public class ApiService
    {
        public string URL { get; set; } = "";
        public HttpClient Client { get; set; }

        public ApiService()
        {
            Client = new HttpClient();
            // Client.DefaultRequestHeaders.Add("", "");
        }

        public async Task<bool> Login(string username, string password)
        {
            var url = this.URL + $"Login?username={username}&password={password}";
            var res = await Client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Store(Temp Temp)
        {
            var url = this.URL + $"Store";

            var json = JsonConvert.SerializeObject(Temp);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await Client.PostAsync(url, content);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<List<Temp>> GetTemps()
        {
            var url = this.URL + $"GetTemps";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Temp>>(res);
        }

        public async Task<Temp> GetTemp(long ID)
        {
            var url = this.URL + $"GetTemp?id={ID}";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Temp>(res);
        }
    }
}
