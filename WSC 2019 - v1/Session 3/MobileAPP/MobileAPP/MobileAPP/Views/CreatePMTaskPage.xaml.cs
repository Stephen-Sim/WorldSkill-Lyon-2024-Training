using MobileAPP.Models;
using MobileAPP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePMTaskPage : ContentPage
    {
        public ApiService apiService { get; set; }

        public List<Temp> Assets { get; set; }

        public List<Temp> Tasks { get; set; }

        public CreatePMTaskPage()
        {
            InitializeComponent();
            apiService = new ApiService();
            AddedAssets = new ObservableCollection<Temp>();

            loadData();

            lv.ItemsSource = AddedAssets;
        }

        public ObservableCollection<Temp> AddedAssets { get; set; }

        async void loadData()
        {
            Assets = await apiService.GetAssets();
            Tasks = await apiService.GetTasks();

            AssetPicker.ItemsSource = Assets;
            AssetPicker.ItemDisplayBinding = new Binding("Name");

            TaskPicker.ItemsSource = Tasks;
            TaskPicker.ItemDisplayBinding = new Binding("Name");

            var models = new List<Temp>() { 
                new Temp(){ID = 1, Name = "Run based tasks"},
                new Temp(){ID = 2, Name = "Daily intervals"},
                new Temp(){ID = 3, Name = "Weekly intervals"},
                new Temp(){ID = 4, Name = "Monthly intervals"},
            };

            modelPicker.ItemsSource = models;
            modelPicker.ItemDisplayBinding = new Binding("Name");

            var nums = new List<int>();

            for (int i = 1; i <= 30; i++)
            {
                nums.Add(i);
            }

            monthPicker.ItemsSource = nums;

            var weekly = new List<DayOfWeek>()
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
            };

            weeklyPicker.ItemsSource = weekly;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (AssetPicker.SelectedItem == null)
            {
                await DisplayAlert("", "please select an asset.", "ok");
                return;
            }

            var item = (Temp)AssetPicker.SelectedItem;

            if (AddedAssets.Any(x => x.ID == item.ID))
            {
                await DisplayAlert("", "asset is already added in the list.", "ok");
                return;
            }

            AddedAssets.Add(item);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            var item = (sender as Button).CommandParameter as Temp;
            AddedAssets.Remove(item);
        }

        private void modelPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (Temp)modelPicker.SelectedItem;

            if (item.ID != 1)
            {
                DateRangeStackLayout.IsVisible = true;
                MilageStackLayout.IsVisible = false;

                if (item.ID == 2)
                {
                    dailyEntry.IsVisible = true;
                    weeklyPicker.IsVisible = false;
                    monthPicker.IsVisible = false;
                }
                else if (item.ID == 3)
                {
                    dailyEntry.IsVisible = false;
                    weeklyPicker.IsVisible = true;
                    monthPicker.IsVisible = false;
                }
                else
                {
                    dailyEntry.IsVisible = false;
                    weeklyPicker.IsVisible = false;
                    monthPicker.IsVisible = true;
                }
            }
            else
            {
                DateRangeStackLayout.IsVisible = false;
                MilageStackLayout.IsVisible = true;
            }
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            if (TaskPicker.SelectedIndex == -1)
            {
                await DisplayAlert("", "Task is required.", "Ok");
                return;
            }

            if (AddedAssets.Count == 0)
            {
                await DisplayAlert("", "at least one asset is required.", "Ok");
                return;
            }

            if (modelPicker.SelectedIndex == -1)
            {
                await DisplayAlert("", "Model is required.", "Ok");
                return;
            }

            var item = (Temp)modelPicker.SelectedItem;

            var CreatePMTaskRequest = new CreatePMTaskRequest();
            CreatePMTaskRequest.ModelId = ((Temp)modelPicker.SelectedItem).ID;
            CreatePMTaskRequest.TaskId = ((Temp)TaskPicker.SelectedItem).ID;
            CreatePMTaskRequest.AssetIds = AddedAssets.ToList().Select(x => x.ID).ToList();

            if (item.ID != 1)
            {
                if (startDate.Date > endDate.Date)
                {
                    await DisplayAlert("", "start date could not bigger than end date.", "Ok");
                    return;
                }

                CreatePMTaskRequest.StartDate = startDate.Date;
                CreatePMTaskRequest.EndDate = endDate.Date;

                if (item.ID == 2)
                {
                    if (string.IsNullOrEmpty(dailyEntry.Text))
                    {
                        await DisplayAlert("", "daily interval is required.", "Ok");
                        return;
                    }

                    CreatePMTaskRequest.DailyInterval = int.Parse(dailyEntry.Text);
                }
                else if (item.ID == 3)
                {
                    if (weeklyPicker.SelectedIndex == -1)
                    {
                        await DisplayAlert("", "weekly interval is required.", "Ok");
                        return;
                    }

                    CreatePMTaskRequest.DayOfWeekInterval = (DayOfWeek)weeklyPicker.SelectedItem;
                }
                else
                {
                    if (monthPicker.SelectedIndex == -1)
                    {
                        await DisplayAlert("", "monthly interval is required.", "Ok");
                        return;
                    }

                    CreatePMTaskRequest.MonthlyInterval = (int)monthPicker.SelectedItem;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(fromMilageEntry.Text) || string.IsNullOrEmpty(TomilageEntry.Text) || string.IsNullOrEmpty(milageEntry.Text))
                {
                    await DisplayAlert("", "milage info is required.", "Ok");
                    return;
                }
                CreatePMTaskRequest.StartMilage = int.Parse(fromMilageEntry.Text);
                CreatePMTaskRequest.EndMilage = int.Parse(TomilageEntry.Text);
                CreatePMTaskRequest.MilageInteval = int.Parse(milageEntry.Text);
            }

            var res = await apiService.StoreTask(CreatePMTaskRequest);

            if (res)
            {
                await DisplayAlert("", "task is created", "ok");
                App.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                await DisplayAlert("", "store failed.", "ok");
            }
        }
    }
}