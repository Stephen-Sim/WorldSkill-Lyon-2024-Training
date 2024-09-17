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
    public partial class MainPage : ContentPage
    {
        public ApiService apiService { get; set; }

        public MainPage()
        {
            InitializeComponent();

            apiService = new ApiService();

            loadData();
        }

        public List<Temp> Assets { get; set; }

        public List<Temp> Tasks { get; set; }

        public List<PMTask> PMTasks { get; set; }

        async void loadData()
        {
            Assets = await apiService.GetAssets();
            Tasks = await apiService.GetTasks();

            AssetPicker.ItemsSource = Assets;
            AssetPicker.ItemDisplayBinding = new Binding("Name");

            TaskPicker.ItemsSource = Tasks;
            TaskPicker.ItemDisplayBinding = new Binding("Name");

            PMTasks = await apiService.GetActiveTasks(activeDatePicker.Date);
            lv.ItemsSource = PMTasks;
        }

        private void AssetPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var temp = PMTasks;

            if (TaskPicker.SelectedIndex != -1)
            {
                var item = (Temp)TaskPicker.SelectedItem;

                temp = temp.Where(x => x.TaskID == item.ID).ToList();
            }

            if (AssetPicker.SelectedIndex != -1)
            {
                var item = (Temp)AssetPicker.SelectedItem;

                temp = temp.Where(x => x.AssetID == item.ID).ToList();
            }

            lv.ItemsSource = temp;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            TaskPicker.SelectedIndex = -1;
            AssetPicker.SelectedIndex = -1;

            lv.ItemsSource = PMTasks;
        }

        private async void activeDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            PMTasks = await apiService.GetActiveTasks(activeDatePicker.Date);
            lv.ItemsSource = PMTasks;
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var pmTask = (sender as Grid).BindingContext as PMTask;

            if (pmTask.TaskDone == true)
            {
                await DisplayAlert("", "Task is already proccessed.", "Ok");
                return;
            }

            var result = await DisplayAlert("", "are you sure to update the status?", "yes", "no");
            
            if (result)
            {
                var res = await apiService.UpdateStatus(pmTask.ID);

                if (res)
                {
                    await DisplayAlert("", "Task is proccessed.", "Ok");
                    PMTasks = await apiService.GetActiveTasks(activeDatePicker.Date);
                    lv.ItemsSource = PMTasks;
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreatePMTaskPage());
        }
    }
}