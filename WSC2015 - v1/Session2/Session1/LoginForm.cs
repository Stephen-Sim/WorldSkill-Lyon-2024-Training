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
    public partial class LoginForm : Session1.CoreForm
    {
        public WSC2015_Session1Entities ent { get; set; }

        public LoginForm()
        {
            InitializeComponent();
            ent = new WSC2015_Session1Entities();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var user = ent.Users.ToList()
                .FirstOrDefault(x => x.Email == textBox1.Text && x.Password == textBox2.Text);

            if (user == null)
            {
                MessageBox.Show("invalid login.");
                return;
            }


            this.Hide();

            if (user.Role.RoleId == "A")
            {
                new AdministratorMenuForm().ShowDialog();
            }
            else if (user.Role.RoleId == "C")
            {
                new CoordinatorMenuForm().ShowDialog();
            }
            else
            {
                new RunnerMenuForm().ShowDialog();
            }
        }
    }
}
