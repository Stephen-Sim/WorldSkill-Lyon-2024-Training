using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows.Forms;

namespace Session1
{
    public partial class Form1 : Session1.BaseForm
    {
        public Form1()
        {
            InitializeComponent();

            ent = new WSC2022SE_Session1Entities();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = !checkBox2.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public WSC2022SE_Session1Entities ent { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == null)
            {
                MessageBox.Show("User name could not be null");
                return;
            }

            var userTypeId = !string.IsNullOrEmpty(textBox1.Text) ? 1 : 2;

            var user = ent.Users.ToList().Where(x => x.Username == (userTypeId == 1 ? textBox1.Text : textBox2.Text) 
                && x.Password == textBox3.Text 
                && x.UserTypeID == userTypeId)
                .FirstOrDefault();

            if (user == null) 
            {
                MessageBox.Show("User is not found");
                return;
            }

            if (userTypeId == 2)
            {
                if (checkBox1.Checked)
                {
                    Properties.Settings.Default.userID = user.ID;
                    Properties.Settings.Default.userTypeID = user.UserTypeID;
                    Properties.Settings.Default.Save();

                }

                this.Hide();

                new UserManagementForm(user.ID).ShowDialog();
            }
            else
            {
                var findUser = ent.Users.FirstOrDefault(x => x.Username == textBox2.Text);

                if (findUser == null)
                {
                    MessageBox.Show("user is not found");
                    return;
                }

                if (checkBox1.Checked)
                {
                    Properties.Settings.Default.userID = findUser.ID;
                    Properties.Settings.Default.userTypeID = user.UserTypeID;
                    Properties.Settings.Default.Save();
                }

                this.Hide();

                new EmployeeForm(findUser.ID).ShowDialog();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            new CreateAccountForm().ShowDialog();
        }
    }
}
