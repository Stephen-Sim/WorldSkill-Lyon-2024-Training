using MobileAPP.Models;
using MobileAPP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogPage : ContentPage
    {
        public Asset Asset { get; set; }

        public ApiService ApiService { get; set; }

        public List<TransferLog> TranserLogs { get; set; }

        public LogPage(Asset asset)
        {
            InitializeComponent();
            this.Asset = asset;

            ApiService = new ApiService();

            loadData();
        }

        async void loadData()
        {
            TranserLogs = await ApiService.GetAssetLogs(this.Asset.ID);

            if (TranserLogs.Count == 0)
            {
                await DisplayAlert("", "there is no transfer log", "ok");
                
                App.Current.MainPage = new NavigationPage(new MainPage());

                return;
            }
            else
            {
                lv.ItemsSource = TranserLogs;
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}