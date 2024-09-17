using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Session1
{
    public partial class SponsorConfirmForm : Session1.CoreForm
    {
        public WSC2015_Session1Entities ent { get; set; }

        public SponsorConfirmForm(int sponsorId)
        {
            InitializeComponent();
            ent = new WSC2015_Session1Entities();

            var sponsor = ent.Sponsorships.FirstOrDefault(x => x.SponsorshipId == sponsorId);
            label3.Text = $"{sponsor.Registration.Runner.User.FirstName} {sponsor.Registration.Runner.User.LastName} " +
                $"({sponsor.Registration.RegistrationEvents.First().BibNumber}) " +
                $"from {sponsor.Registration.Runner.CountryCode}";
            label4.Text = $"{sponsor.Registration.Charity.CharityName}";
            label5.Text = $"${sponsor.Amount}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }
    }
}
