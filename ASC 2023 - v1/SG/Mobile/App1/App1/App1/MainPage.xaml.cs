using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            initLoad();

            loadResult();

            changeUrl();
        }

        async void initLoad()
        {
            var quizes = await ApiService<List<Quiz>>.Get("GetQuizzes");
            quizPicker.ItemsSource = quizes;
            quizPicker.ItemDisplayBinding = new Binding("Name");

            var list = new List<Temp>()
            { 
                new Temp() { ID = 1, Name = "All attempts"},
                new Temp() { ID = 2, Name = "Attempts completed within the last 30 days"},
                new Temp() { ID = 3, Name = "Attempts completed within the last 60 days"},
                new Temp() { ID = 4, Name = "Attempts completed within the last 90 days"},
                new Temp() { ID = 5, Name = "Attempts in progress"},
                new Temp() { ID = 6, Name = "Not attempted"},
            };

            typePicker.ItemsSource = list;
            typePicker.ItemDisplayBinding = new Binding("Name");

            quizPicker.SelectedIndex = 0;
            typePicker.SelectedIndex = 0;
        }

        async void loadResult()
        {
            var quiz = (Quiz)quizPicker.SelectedItem;
            var type = (Temp)typePicker.SelectedItem;
            var isExpert = isExpertCheckBox.IsChecked;
            
            if (quiz == null || type == null)
            {
                return;
            }

            var res = await ApiService<List<Data>>.Get($"GetData?quizId={quiz.ID}&typeId={type.ID}&isExpert={isExpert}");
            lv.ItemsSource = res;
        }

        void changeUrl()
        {
            var quiz = (Quiz)quizPicker.SelectedItem;
            var type = (Temp)typePicker.SelectedItem;
            var isExpert = isExpertCheckBox.IsChecked;

            if (quiz == null || type == null)
            {
                return;
            }

            var str = $"Download excel report at http://10.131.76.121:8090/home/download?quizId={quiz.ID}&typeId={type.ID}&isExpert={isExpert}";

            urlLabel.Text = str;
        }

        private void isExpertCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            loadResult();
            changeUrl();
        }

        private void quizPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadResult();
            changeUrl();
        }
    }
}
