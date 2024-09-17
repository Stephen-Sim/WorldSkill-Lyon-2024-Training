using Session1.UserControls;
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
    public partial class ListofCharitiesForm : Session1.CoreForm
    {
        public WSC2015_Session1Entities ent { get; set; }
        public ListofCharitiesForm()
        {
            InitializeComponent();

            ent = new WSC2015_Session1Entities();

            loadCharities();
        }

        void loadCharities()
        {
            flowLayoutPanel1.Controls.Clear();

            var charitiesId = ent.Registrations.ToList()
                .Where(x => x.RegistrationEvents.Any(y => y.Event.MarathonId == 5))
                .SelectMany(x => new int[]
                {
                    x.CharityId
                }).Distinct().ToList();

            foreach (var charityId in charitiesId)
            {
                var charity = ent.Charities.FirstOrDefault(x => x.CharityId == charityId);

                var uc = new CharityUserControl();

                uc.pictureBox1.Image = Image.FromFile(charity.CharityLogo);
                uc.label1.Text = charity.CharityName;
                uc.label2.Text = charity.CharityDescription;

                flowLayoutPanel1.Controls.Add(uc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }
    }
}
