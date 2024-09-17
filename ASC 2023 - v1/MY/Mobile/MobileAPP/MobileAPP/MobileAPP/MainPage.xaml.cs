using MobileAPP.Models;
using MobileAPP.Services;
using MobileAPP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAPP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            loadcoms();
        }

        async void loadcoms()
        {
            var res = await APIService<List<Competitor>>.Get("GetCompetitors");

            lv.ItemsSource = res;

            totalCom.Text = $"Total Competitors: {res.Count}";
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var com = (sender as ImageButton).CommandParameter as Competitor;

            if (com.color.ToLower() == "green")
            {
                await DisplayAlert("", "the competitor is already fully sponsored", "Ok");
                return;
            }

            await App.Current.MainPage.Navigation.PushAsync(new SponsorPage(com));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Launcher.OpenAsync($"tel:{123123}");
            }
            catch (Exception)
            {

            }
        }
    }
}
