using MobileAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileAPP.Services
{
    public class ApiService
    {
        public string URL { get; set; } = "http://10.131.76.96/api/values/";
        public HttpClient Client { get; set; }

        public ApiService()
        {
            Client = new HttpClient();
            // Client.DefaultRequestHeaders.Add("", "");
        }

        public async Task<List<Temp>> GetDepartments()
        {
            var url = this.URL + $"GetDepartments";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Temp>>(res);
        }

        public async Task<List<Temp>> GetAssetGroups()
        {
            var url = this.URL + $"GetAssetGroups";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Temp>>(res);
        }

        public async Task<List<Asset>> GetAssets()
        {
            var url = this.URL + $"GetAssets";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Asset>>(res);
        }

        public async Task<List<Temp>> GetLocations()
        {
            var url = this.URL + $"GetLocations";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Temp>>(res);
        }

        public async Task<List<Temp>> GetEmployees()
        {
            var url = this.URL + $"GetEmployees";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Temp>>(res);
        }

        public async Task<string> GetNewAssetSN(long DepartmentId, long LocationId, long AGId)
        {
            var url = this.URL + $"GetNewAssetSN?DepartmentId={DepartmentId}&LocationId={LocationId}&AGId={AGId}&";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<string>(res);
        }

        public async Task<List<TransferLog>> GetAssetLogs(long Id)
        {
            var url = this.URL + $"GetAssetLogs?Id={Id}";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<TransferLog>>(res);
        }

        public async Task<List<byte[]>> GetAssetPhotos(long Id)
        {
            var url = this.URL + $"GetAssetPhotos?Id={Id}";
            var res = await Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<byte[]>>(res);
        }

        public async Task<bool> StoreAsset(StoreAssetRequest storeAssetRequest)
        {
            var url = this.URL + $"StoreAsset";

            var json = JsonConvert.SerializeObject(storeAssetRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await Client.PostAsync(url, content);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EditAsset(EditAssetRequest storeAssetRequest)
        {
            var url = this.URL + $"EditAsset";

            var json = JsonConvert.SerializeObject(storeAssetRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await Client.PostAsync(url, content);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> TransferAsset(TransferAssetRequest transferAssetRequest)
        {
            var url = this.URL + $"TransferAsset";

            var json = JsonConvert.SerializeObject(transferAssetRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await Client.PostAsync(url, content);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
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
