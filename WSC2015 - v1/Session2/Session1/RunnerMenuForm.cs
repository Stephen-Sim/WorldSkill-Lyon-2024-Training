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
    public partial class RunnerMenuForm : Session1.CoreForm
    {
        public WSC2015_Session1Entities ent { get; set; }

        public RunnerMenuForm()
        {
            InitializeComponent();
            ent = new WSC2015_Session1Entities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            new LoginForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var cors = ent.Users.Where(x => x.RoleId == "C").Select(x => new
            {
                Name = x.FirstName + " " + x.LastName,
                x.Email,
            }).ToList();

            string str = string.Empty;

            for (int i = 0; i < cors.Count; i++) 
            {
                str += $"{i+1}. {cors[i].Name} ({cors[i].Email})\n";
            }

            MessageBox.Show(str);
        }
    }
}
