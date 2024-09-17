using MobileAPP.Models;
using MobileAPP.Services;
using MobileAPP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAPP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var res = await ApiService<User>.Get($"Login?u={uEntry.Text}&p={pEntry.Text}");

            if (res == null)
            {
                await DisplayAlert("", "Invalid Login", "Ok");
                return;
            }

            App.User = res;

            App.Current.MainPage = new NavigationPage(new ServiceMenuPage());
        }
    }
}
