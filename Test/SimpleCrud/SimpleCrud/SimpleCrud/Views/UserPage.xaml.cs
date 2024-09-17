using SimpleCrud.Models;
using SimpleCrud.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleCrud.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();
            loadData();
        }

        async void loadData()
        {
            var res = await ApiService<List<User>>.Get("GetUsers");

            lv.ItemsSource = res;
        }
    }
}