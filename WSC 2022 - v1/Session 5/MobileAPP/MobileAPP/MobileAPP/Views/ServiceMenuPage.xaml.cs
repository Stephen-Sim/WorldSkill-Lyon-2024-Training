using MobileAPP.Models;
using MobileAPP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceMenuPage : ContentPage
    {
        async void loadData()
        {
            unameLabel.Text = "Welcome " + App.User.FullName;

            var res = await ApiService<List<ServiceType>>.Get("GetServiceTypes");

            res.ForEach(service => {
                service.ImageSource = ImageSource.FromResource($"MobileAPP.Imgs.{service.IconName}");
            });

            lv.ItemsSource = res;
        }

        public ServiceMenuPage()
        {
            InitializeComponent();

            loadData();
        }
    }
}