using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class Form1 : Form
    {
        public ASC2023_PH_DesktopEntities ent { get; set; }

        public Form1()
        {
            InitializeComponent();
            ent = new ASC2023_PH_DesktopEntities();
        }

        int loginAttempt = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            var user = ent.users.ToList()
                    .FirstOrDefault(x => x.username == textBox1.Text && 
                        x.password == textBox2.Text && 
                        x.isActive == 1);

            if (user == null)
            {
                loginAttempt++;
                MessageBox.Show("invalid login credential.");

                if (loginAttempt == 3)
                {
                    lockLogin();
                }

                return;
            }

            this.Hide();

            new SearchRoomForm(user.id).ShowDialog();
            
        }

        async void lockLogin()
        {
            button1.Enabled = false;

            for (int i = 10; i > 0; i--)
            {
                label3.Text = $"Count down: {i}s";
                await Task.Delay(1000);
            }

            loginAttempt = 0;
            label3.Text = string.Empty;

            button1.Enabled = true;
        }
    }
}
