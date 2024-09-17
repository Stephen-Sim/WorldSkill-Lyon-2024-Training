using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Session1
{
    public partial class CreateAccountForm : Session1.BaseForm
    {
        public CreateAccountForm()
        {
            InitializeComponent();

            ent = new WSC2022SE_Session1Entities();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new Form1().ShowDialog();
        }

        private void CreateAccountForm_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        bool isconread = false;

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (isconread == false)
            {
                checkBox1.Checked = false;

                MessageBox.Show("you should read terms and condition at least once");
                return;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            isconread = true;

            using (var sr = new StreamReader("Terms.txt"))
            {
                MessageBox.Show(sr.ReadToEnd());
            }
        }

        public WSC2022SE_Session1Entities ent { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("All the fields are required.");
                return;
            }

            if (ent.Users.Any(x => x.Username == textBox1.Text))
            {
                MessageBox.Show("Username is already taken in the system.");
                return;
            }

            if (numericUpDown1.Value < 0)
            {
                MessageBox.Show("Family count could not leasser than 1.");
                return;
            }

            if (textBox3.Text.Length < 5)
            {
                MessageBox.Show("Password lenght should at least 5.");
                return;
            }

            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("retype password is not matched..");
                return;
            }

            var user = new User()
            {
                Username = textBox1.Text,
                FullName = textBox2.Text,
                BirthDate  = dateTimePicker1.Value.Date,
                Password = textBox3.Text,
                Gender = radioButton1.Checked,
                GUID = Guid.NewGuid(),
                UserTypeID = 2,
                FamilyCount = (int) numericUpDown1.Value,
            };

            ent.Users.Add(user);

            MessageBox.Show("user is registerd");

            this.Hide();

            new UserManagementForm(user.ID).ShowDialog();
        }
    }
}
