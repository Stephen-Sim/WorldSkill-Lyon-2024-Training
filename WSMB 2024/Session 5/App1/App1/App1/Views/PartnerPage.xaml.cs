using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartnerPage : ContentPage
    {
        public PartnerPage()
        {
            InitializeComponent();

            loadData();
        }

        async void loadData()
        {
            var res = await ApiService<List<PathPartnerDTO>>.Get($"GetPathPartners?uid={App.User.id}");

            res.ForEach(x => {
                if (x.isBold)
                {
                    x.style = FontAttributes.Bold;
                }
                else
                {
                    x.style = FontAttributes.None;
                }
            });

            lv.ItemsSource = res;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.User = new User();
            Application.Current.Properties.Clear();
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new CollectParcelPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var phone = (sender as Button).CommandParameter as string;
            
            try
            {
                PhoneDialer.Open(phone);
            }
            catch (Exception err)
            {
                await DisplayAlert("", $"{err.Message}", "Ok");
            }
        }
    }
}