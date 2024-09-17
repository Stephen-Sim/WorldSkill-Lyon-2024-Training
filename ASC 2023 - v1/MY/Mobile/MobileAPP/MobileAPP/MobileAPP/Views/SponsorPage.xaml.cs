using MobileAPP.Models;
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
    public partial class SponsorPage : ContentPage
    {
        public Competitor Competitor { get; set; }

        public SponsorPage(Competitor competitor)
        {
            InitializeComponent();

            this.Competitor = competitor;

            this.Title = this.Competitor.name;
        }
    }
}