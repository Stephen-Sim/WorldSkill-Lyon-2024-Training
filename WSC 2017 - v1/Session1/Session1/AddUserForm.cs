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
    public partial class AddUserForm : Session1.Form1
    {
        public int id { get; set; }

        public AddUserForm(int current_login_id)
        {
            this.id = current_login_id;
            ent = new WSC2017_Session1Entities();
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MainMenuAdminForm(this.id).ShowDialog();
        }

        public WSC2017_Session1Entities ent { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) ||
                string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("All field are required.");
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("office is not selected.");
                return;
            }

            if (ent.Users.Any(x => x.Email == textBox1.Text))
            {
                MessageBox.Show("email is taken.");
                return;
            }

            var user = new User()
            {
                Email = textBox1.Text,
                FirstName = textBox2.Text,
                LastName = textBox3.Text,
                Birthdate = dateTimePicker1.Value.Date,
                OfficeID = (int)comboBox1.SelectedValue,
                RoleID = 2
            };


            ent.Users.Add(user);
            ent.SaveChanges();

            MessageBox.Show("user is register.");

            this.Hide();

            new MainMenuAdminForm(this.id).ShowDialog();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'wSC2017_Session1DataSet.Offices' table. You can move, or remove it, as needed.
            this.officesTableAdapter.Fill(this.wSC2017_Session1DataSet.Offices);
        }
    }
}
