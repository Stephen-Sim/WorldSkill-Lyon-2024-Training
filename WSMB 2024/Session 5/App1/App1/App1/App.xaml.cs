using App1.Models;
using App1.Views;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        public static User User { get; set; }

        public App()
        {
            InitializeComponent();

            if (Application.Current.Properties.ContainsKey("user"))
            {
                App.User = JsonConvert.DeserializeObject<User>(Application.Current.Properties["user"].ToString());
                MainPage = new NavigationPage(new CollectParcelPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
