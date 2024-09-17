using MobileAPP.Models;
using MobileAPP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public ApiService apiService { get; set; }

        public List<Temp> Departments { get; set; }
        public List<Temp> AssetGroups { get; set; }

        public List<Asset> Assets { get; set; }

        private bool visible;

        public bool Visible
        {
            get { return visible; }
            set { visible = value; OnPropertyChanged(); }
        }

        private string textColor;

        public string TextColor
        {
            get { return textColor; }
            set { textColor = value; OnPropertyChanged(); }
        }

        private StackOrientation infoStackOrientation;

        public StackOrientation InfoStackOrientation
        {
            get { return infoStackOrientation; }
            set { infoStackOrientation = value; OnPropertyChanged(); }
        }


        public MainPage()
        {
            InitializeComponent();

            InfoStackOrientation = StackOrientation.Vertical;

            this.BindingContext = this;
            apiService = new ApiService();

            loadData();
        }

        async void loadData()
        {
            Departments = await apiService.GetDepartments();
            AssetGroups = await apiService.GetAssetGroups();

            dPicker.ItemsSource = Departments;
            dPicker.ItemDisplayBinding = new Binding("Name");

            agPicker.ItemsSource = AssetGroups;
            agPicker.ItemDisplayBinding = new Binding("Name");

            Assets = await apiService.GetAssets();
            lv.ItemsSource = Assets;

            statusLabel.Text = $"{Assets.Count} assets from {Assets.Count}";
        }

        void loadSearch()
        {
            var temp = Assets;

            if (dPicker.SelectedIndex != -1)
            {
                var item = (Temp) dPicker.SelectedItem;

                temp = temp.Where(x => x.DepartmentID == item.ID).ToList();
            }

            if (agPicker.SelectedIndex != -1)
            {
                var item = (Temp)agPicker.SelectedItem;

                temp = temp.Where(x => x.AssetGroupID == item.ID).ToList();
            }

            if (!string.IsNullOrEmpty(sEntry.Text) && sEntry.Text.Length > 2)
            {
                temp = temp.Where(x => x.AssetName.StartsWith(sEntry.Text) || x.AssetSN.StartsWith(sEntry.Text)).ToList();
            }

            temp = temp.Where(x => x.WarrantyDate != null && x.WarrantyDate > sDatePicker.Date && x.WarrantyDate < eDatePicker.Date).ToList();

            statusLabel.Text = $"{temp.Count} assets from {Assets.Count}";

            lv.ItemsSource = temp;
        }

        private void dPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSearch();
        }

        private void sEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadSearch();
        }

        private async void sDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (sDatePicker.Date > eDatePicker.Date)
            {
                await DisplayAlert("", "start date could not greather than end date.", "ok");
                return;
            }

            loadSearch();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                Visible = false;
                InfoStackOrientation = StackOrientation.Horizontal;
                TextColor = "Red";
            }
            else
            {
                Visible = true;
                InfoStackOrientation = StackOrientation.Vertical;
                TextColor = "Default";
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            // edit
            var asset = (sender as ImageButton).CommandParameter as Asset;
            App.Current.MainPage = new NavigationPage(new AddOrEditAssetInfoPage(asset));
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            // transfer
            var asset = (sender as ImageButton).CommandParameter as Asset;
            App.Current.MainPage = new NavigationPage(new AssetTransferPage(asset));
        }

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            // log
            var asset = (sender as ImageButton).CommandParameter as Asset;
            App.Current.MainPage = new NavigationPage(new LogPage(asset));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            // add
            App.Current.MainPage = new NavigationPage(new AddOrEditAssetInfoPage());
        }
    }
}