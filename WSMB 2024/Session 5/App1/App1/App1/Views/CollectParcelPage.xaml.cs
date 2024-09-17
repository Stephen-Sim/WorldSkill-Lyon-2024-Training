using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectParcelPage : ContentPage
    {
        public CollectParcelPage()
        {
            InitializeComponent();
            loadData();
        }

        public byte[] Image { get; set; } = null;

        async void loadData()
        {
            Parcels = await ApiService<List<Parcels>>.Get($"GetParcels?uid={App.User.id}");

            lv.ItemsSource = Parcels;
        }

        public List<Parcels> Parcels { get; set; }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new PartnerPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.User = new User();
            Application.Current.Properties.Clear();
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var img = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = "Please xxx"
            });

            if (img == null)
            {
                return;
            }

            Image = File.ReadAllBytes(img.FullPath);
            imgSource.Source = ImageSource.FromFile(img.FullPath);
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(descEntry.Text) || string.IsNullOrEmpty(parcelIDEntry.Text) || Image == null)
            {
                await DisplayAlert("", "All fields are required.", "Ok");
                return;
            }

            var parcelId = int.Parse(parcelIDEntry.Text);

            if (!Parcels.Any(x => x.id == parcelId))
            {
                await DisplayAlert("", "invalid parcel id.", "Ok");
                return;
            }

            if (Parcels.Any(x => x.id == parcelId && x.status == "Delivered"))
            {
                await DisplayAlert("", "parcel is already delivered.", "Ok");
                return;
            }

            var colletRes = new CollectParcelRequest()
            {
                desc = descEntry.Text,
                img = Image,
                parcelid = parcelId,
                uid = (int)App.User.id
            };

            var res = await ApiService<CollectParcelRequest>.Post($"Collect", colletRes);

            if (res)
            {
                loadData();
                await DisplayAlert("", "parcel is delivered.", "Ok");
            }
            else
            {
                await DisplayAlert("", "fail to collect parcel.", "Ok");
            }
        }
    }
}