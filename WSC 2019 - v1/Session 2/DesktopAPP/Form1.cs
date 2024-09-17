using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesktopAPP
{
    public partial class Form1 : DesktopAPP.BaseForm
    {
        public WSC2019_Session2Entities ent { get; set; }

        public Form1()
        {
            InitializeComponent();
            ent = new WSC2019_Session2Entities();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            var user = ent.Employees.FirstOrDefault(x => x.Username == textBox1.Text && x.Password == textBox2.Text);

            if (user is null)
            {
                MessageBox.Show("Employee is not found");
                return;
            }

            this.Hide();
            if (user.isAdmin == true)
            {
                new AdminEMManagementForm().ShowDialog();
            }
            else
            {
                new AccountablePartyEMMangementForm().ShowDialog();
            }
        }
    }
}
