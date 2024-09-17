using MobileAPP.Models;
using MobileAPP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrEditAssetInfoPage : ContentPage
    {
        public ApiService ApiService { get; set; }

        public List<Temp> Departments { get; set; }
        public List<Temp> AssetGroups { get; set; }
        public List<Temp> Locations { get; set; }
        public List<Temp> Employees { get; set; }

        public Asset Asset { get; set; }
        public ObservableCollection<ImageSource> ImageSources { get; set; }
        public List<byte[]> ImageByteArrays { get; set; }

        async void loadData()
        {
            Departments = await ApiService.GetDepartments();
            AssetGroups = await ApiService.GetAssetGroups();
            Locations = await ApiService.GetLocations();
            Employees = await ApiService.GetEmployees();

            dPicker.ItemsSource = Departments;
            dPicker.ItemDisplayBinding = new Binding("Name");

            agPicker.ItemsSource = AssetGroups;
            agPicker.ItemDisplayBinding = new Binding("Name");

            lPicker.ItemsSource = Locations;
            lPicker.ItemDisplayBinding = new Binding("Name");

            ePicker.ItemsSource = Employees;
            ePicker.ItemDisplayBinding = new Binding("Name");

            if (Asset != null)
            {
                loadEditData();
            }
        }

        public AddOrEditAssetInfoPage()
        {
            InitializeComponent();

            ImageSources = new ObservableCollection<ImageSource>();
            ImageByteArrays = new List<byte[]>();
            ApiService = new ApiService();
            
            lv.ItemsSource = ImageSources;

            loadData();
        }

        public AddOrEditAssetInfoPage(Asset asset)
        {
            InitializeComponent();
            
            ApiService = new ApiService();

            ImageSources = new ObservableCollection<ImageSource>();
            ImageByteArrays = new List<byte[]>();
            this.Asset = asset;

            lv.ItemsSource = ImageSources;

            loadData();
        }

        async void loadEditData()
        {
            assetNameEntry.Text = this.Asset.AssetName;
            AssetSNLabel.Text = this.Asset.AssetSN;

            AssetDescEditor.Text = this.Asset.Description;
            
            dPicker.SelectedItem = Departments.FirstOrDefault(x => x.ID == this.Asset.DepartmentID);
            agPicker.SelectedItem = AssetGroups.FirstOrDefault(x => x.ID == this.Asset.AssetGroupID);
            lPicker.SelectedItem = Locations.FirstOrDefault(x => x.ID == this.Asset.LocationID);
            ePicker.SelectedItem = Employees.FirstOrDefault(x => x.ID == this.Asset.EmployeeID);

            dPicker.IsEnabled = false;
            agPicker.IsEnabled = false;
            lPicker.IsEnabled = false;

            if (Asset.WarrantyDate != null)
            {
                ExpiredDatePicker.Date = (DateTime)Asset.WarrantyDate;
            }

            var res = await ApiService.GetAssetPhotos(this.Asset.ID);

            if (res.Count() != 0)
            {
                ImageByteArrays = res;

                foreach (var item in ImageByteArrays)
                {
                    var memeory = new MemoryStream(item);
                    ImageSources.Add(ImageSource.FromStream(() => memeory));
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            // cam
            var image = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Please Pick an Image."
            });

            if (image != null)
            {
                ImageSources.Add(ImageSource.FromFile(image.FullPath));
                ImageByteArrays.Add(File.ReadAllBytes(image.FullPath));
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            // file
            var image = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please Pick an Image."
            });

            if (image != null)
            {
                ImageSources.Add(ImageSource.FromFile(image.FullPath));
                ImageByteArrays.Add(File.ReadAllBytes(image.FullPath));
            }
        }

        private async void dPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Asset == null && dPicker.SelectedIndex != -1 &&
                lPicker.SelectedIndex != -1 &&
                agPicker.SelectedIndex != -1)
            {
                var res = await ApiService.GetNewAssetSN(((Temp)dPicker.SelectedItem).ID,
                    ((Temp)lPicker.SelectedItem).ID,
                    ((Temp)agPicker.SelectedItem).ID);

                AssetSNLabel.Text = res;
            }
        }

        public bool IsDateSelected { get; set; } = false;

        private async void ExpiredDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (ExpiredDatePicker.Date < DateTime.Today)
            {
                await DisplayAlert("", "expired date could not samller than today.", "");
                IsDateSelected = false;
                return;
            }

            IsDateSelected = false;
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(assetNameEntry.Text) || dPicker.SelectedIndex == -1 ||
                agPicker.SelectedIndex == -1 || lPicker.SelectedIndex == -1 ||
                ePicker.SelectedIndex == -1 || string.IsNullOrEmpty(AssetDescEditor.Text))
            {
                await DisplayAlert("", "all the fields are required except date and image.", "Ok");
                return;
            }

            if (Asset != null)
            {
                var editAsset = new EditAssetRequest()
                {
                    AssetID = this.Asset.ID,
                    AssetName = assetNameEntry.Text,
                    AssetDesc = AssetDescEditor.Text,
                    EmployeeId = ((Temp)ePicker.SelectedItem).ID,
                    ExipredDate = IsDateSelected ? (DateTime?)ExpiredDatePicker.Date : null
                };

                if (ImageByteArrays.Count() != 0)
                {
                    editAsset.AssetImages = ImageByteArrays;
                }

                var res = await ApiService.EditAsset(editAsset);

                if (res)
                {
                    await DisplayAlert("", "asset is edited!", "ok");

                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    await DisplayAlert("", "failed to edit!", "ok");
                }
            }
            else
            {
                var newAsset = new StoreAssetRequest()
                {
                    AssetDesc = AssetDescEditor.Text,
                    AssetGroupId = ((Temp)agPicker.SelectedItem).ID,
                    AssetName = assetNameEntry.Text,
                    AssetSN = AssetSNLabel.Text,
                    DepartmentId = ((Temp)dPicker.SelectedItem).ID,
                    EmployeeId = ((Temp)ePicker.SelectedItem).ID,
                    LocationId = ((Temp)lPicker.SelectedItem).ID,
                    ExipredDate = IsDateSelected ? (DateTime?) ExpiredDatePicker.Date : null
                };

                if (ImageByteArrays.Count() != 0)
                {
                    newAsset.AssetImages = ImageByteArrays;
                }

                var res = await ApiService.StoreAsset(newAsset);

                if (res)
                {
                    await DisplayAlert("", "asset is stored!", "ok");

                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    await DisplayAlert("", "failed to store! Maybe there is no such department location.", "ok");
                }
            }
        }
    }
}