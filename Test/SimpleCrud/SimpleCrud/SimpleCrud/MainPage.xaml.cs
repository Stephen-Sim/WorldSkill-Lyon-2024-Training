using SimpleCrud.Services;
using SimpleCrud.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleCrud
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uEntry.Text) || string.IsNullOrEmpty(pEntry.Text))
            {
                await DisplayAlert("", "all fields are required.", "ok");
                return;
            }

            var res = await ApiService<bool>.Check($"login?u={uEntry.Text}&p={pEntry.Text}");

            if (res)
            {
                await App.Current.MainPage.Navigation.PushAsync(new UserPage());
            }
            else
            {
                await DisplayAlert("", "Invalid Login", "Ok");
            }
        }
    }
}
