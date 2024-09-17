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
    public partial class MainPage : ContentPage
    {
        public ApiService apiService { get; set; }

        public MainPage()
        {
            InitializeComponent();

            apiService = new ApiService();
        }

        void loadData()
        {

        }
    }
}