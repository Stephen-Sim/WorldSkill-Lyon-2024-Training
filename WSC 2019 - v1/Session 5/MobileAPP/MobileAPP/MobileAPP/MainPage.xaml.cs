using MobileAPP.Models;
using MobileAPP.Services;
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
            
            checkConnectivity();

            loadPicker();
        }

        async void loadPicker()
        {
            if (isConnect == true)
            {
                var res = await APIService<List<Well>>.Get("GetWells");
                wellPicker.ItemsSource = res;
                wellPicker.ItemDisplayBinding = new Binding("Name");

                capLabel.Text = "Well Capacity: " + res.First().Capacity;

                wellPicker.SelectedIndex = 0;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
        }

        private async void wellPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = ((Well)wellPicker.SelectedItem).Id;
            var res = await APIService<List<Layer>>.Get($"GetWellLayers?id={id}");

            lv.ItemsSource = res;
        }

        int timer = 0;

        bool isConnect = false;

        async void checkConnectivity()
        {
            await Task.Delay(1000);

            timer++;

            if (timer == 5)
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet && isConnect == false)
                {
                    isConnect = true;

                    await DisplayAlert("", "Device is connected", "Ok");

                    loadPicker();
                }
                else
                {
                    isConnect = false;

                    await DisplayAlert("", "Device is disconnected", "Ok");
                }

                timer = 0;
            }

            checkConnectivity();
        }
    }
}
