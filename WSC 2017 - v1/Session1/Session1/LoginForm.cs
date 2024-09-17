using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session1
{
    public partial class LoginForm : Session1.Form1
    {
        public LoginForm()
        {
            InitializeComponent();
            ent = new WSC2017_Session1Entities();


            label4.Text = string.Empty;
        }

        int attempt = 0;

        public WSC2017_Session1Entities ent { get; set; }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var user = ent.Users.ToList().FirstOrDefault(x => x.Email == textBox1.Text && x.Password == textBox2.Text);

            if (user == null)
            {
                MessageBox.Show("user is not found.");
                attempt++;

                if (attempt == 3)
                {
                    attempt = 0;
                    MessageBox.Show("Login suspended for 10 seconds.");
  
                    button2.Enabled = false;

                    for (int i = 10; i >=0; i--)
                    {
                        label4.Text = $"login lock for {i}s.";
                        await Task.Delay(1000);
                    }
                    label4.Text = string.Empty;

                    button2.Enabled = true;
                }
                return;
            }

            if (user.Active == false)
            {
                MessageBox.Show("user is not active.");
                return;
            }

            var log = ent.LoginLogs.Where(x => x.user_id == user.ID && x.datetime_logout == null && x.reason == null).FirstOrDefault();

            var loginLog = new LoginLog()
            {
                datetime = DateTime.Now,
                user_id = user.ID,
            };

            ent.LoginLogs.Add(loginLog);
            ent.SaveChanges();

            if (log != null)
            {
                this.Hide();
                new NoLogoutDetectedForm(log.id, user.ID, loginLog.id).ShowDialog();
            }
            else if (user.RoleID == 1)
            {
                this.Hide();

                new MainMenuAdminForm(user.ID).ShowDialog();
            }
            else
            {
                new AutoSystemForm(user.ID, loginLog.id).ShowDialog();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.label4.ForeColor = Color.Red;
        }
    }
}
