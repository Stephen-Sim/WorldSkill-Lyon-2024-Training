using App1.Models;
using App1.Services;
using App1.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var res = await ApiService<User>.Get($"login?u={uEntry.Text}&p={pEntry.Text}");

            if (res != null)
            {
                App.User = res;

                await App.Current.MainPage.Navigation.PushAsync(new ValidatePasscodePage());
            }
            else
            {
                await DisplayAlert("", "login failed", "Ok");
            }
        }
    }
}
