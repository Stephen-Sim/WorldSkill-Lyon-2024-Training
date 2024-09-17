using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Session1
{
    public partial class NoLogoutDetectedForm : Session1.Form1
    {
        public int userId { get; set; }
        public int id { get; set; }
        public int current_log { get; set; }

        public NoLogoutDetectedForm(int id, int userId, int current_log)
        {
            this.userId = userId;
            this.id = id;
            this.current_log = current_log;
            InitializeComponent();

            ent = new WSC2017_Session1Entities();

            this.radioButton1.Checked = true;
        }

        public WSC2017_Session1Entities  ent { get; set; }

        private void NoLogoutDetectedForm_Load(object sender, EventArgs e)
        {
            var log = ent.LoginLogs.FirstOrDefault(x => x.id == id);

            this.label2.Text = $"No logout for you last logi {((DateTime)log.datetime).ToString("dd/MM/yyyy")} on {((DateTime)log.datetime).ToString("hh:mm")}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("REASON is required.");
                return;
            }

            var log = ent.LoginLogs.FirstOrDefault(x=> x.id == this.id);
            log.reason = textBox1.Text;
            log.isSoftwareCrashed = radioButton1.Checked;

            ent.SaveChanges();

            this.Hide();

            var user = ent.Users.First(x => x.ID == this.userId);

            if (user.RoleID == 1)
            {
                this.Hide();

                new MainMenuAdminForm(this.userId).ShowDialog();
            }
            else
            {
                this.Hide();

                new AutoSystemForm(this.userId, current_log).ShowDialog();
            }
        }
    }
}
