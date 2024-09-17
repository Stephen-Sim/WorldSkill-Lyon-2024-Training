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
    public partial class AssetTransferPage : ContentPage
    {
        public ApiService apiService { get; set; }
        public Asset Asset { get; set; }
        public AssetTransferPage(Asset asset)
        {
            this.Asset = asset;

            apiService = new ApiService();

            InitializeComponent();

            loadData();
        }

        public List<Temp> Departments { get; set; }
        public List<Temp> Locations { get; set; }


        async void loadData()
        {
            Departments = await apiService.GetDepartments();
            Locations = await apiService.GetLocations();

            dPicker.ItemsSource = Departments;
            dPicker.ItemDisplayBinding = new Binding("Name");

            lPicker.ItemsSource = Locations;
            lPicker.ItemDisplayBinding = new Binding("Name");

            assetNameEntry.Text = this.Asset.AssetName;
            assetSNEntry.Text = this.Asset.AssetSN;
            departmentEntry.Text = this.Asset.DepartmentName;

            newAssetSN.Text = $"??/{this.Asset.AssetGroupID.ToString("00")}/????";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (dPicker.SelectedIndex == -1 ||
                lPicker.SelectedIndex == -1)
            {
                await DisplayAlert("", "all fields are required", "ok");
            }

            var newTransfer = new TransferAssetRequest()
            {
                AssetID = this.Asset.ID,
                NewAssetSN = newAssetSN.Text,
                DepartmentId = ((Temp)dPicker.SelectedItem).ID,
                LocationId = ((Temp)lPicker.SelectedItem).ID,
            };

            var res = await apiService.TransferAsset(newTransfer);

            if (res)
            {
                await DisplayAlert("", "asset is transfered.", "ok");
                App.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                await DisplayAlert("", "failed to transfer! department location may not exist", "ok");
            }
        }

        private async void lPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dPicker.SelectedIndex != -1 &&
                lPicker.SelectedIndex != -1)
            {
                var res = await apiService.GetNewAssetSN(((Temp)dPicker.SelectedItem).ID,
                    ((Temp)lPicker.SelectedItem).ID,
                    this.Asset.AssetGroupID);

                newAssetSN.Text = res;
            }
        }
    }
}