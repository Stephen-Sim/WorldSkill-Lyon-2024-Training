using App1.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidatePasscodePage : ContentPage
    {
        public ValidatePasscodePage()
        {
            InitializeComponent();

            loadData();
        }

        void loadData()
        {
            uLabel.Text = $"Username: {App.User.name}";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pEntry.Text))
            {
                await DisplayAlert("", "passcode canntot be empty.", "Ok");
                return;
            }

            var res = await ApiService<bool>.Check($"Validate?pass={pEntry.Text}");

            if (res)
            {
                Application.Current.Properties["user"] = JsonConvert.SerializeObject(App.User); ;

                App.Current.MainPage = new NavigationPage(new CollectParcelPage());
            }
            else
            {
                await DisplayAlert("", "invalid passcode.", "Ok");
                return;
            }
        }
    }
}