using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Session1
{
    public partial class SponsorRunnerForm : Session1.CoreForm
    {
        public WSC2015_Session1Entities ent { get; set; }

        decimal amount = 0;

        public SponsorRunnerForm()
        {
            InitializeComponent(); 
            ent = new WSC2015_Session1Entities();
            loadRunner();
        }

        void loadRunner()
        {
            var runner = ent.Registrations.ToList().Where(x => x.RegistrationEvents.Any(y => y.Event.MarathonId == 5)).Select(x => new
            {
                Id = x.RegistrationId,
                Name = $"{x.Runner.User.LastName}, {x.Runner.User.FirstName} - {x.RegistrationEvents.First().BibNumber} ({x.Runner.CountryCode})",
            }).ToList();

            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            comboBox1.DataSource = runner;

            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == string.Empty)
            {
                return; 
            }

            amount = int.Parse(maskedTextBox1.Text);

            amountLabel.Text = $"${amount}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (50 <= 0)
            {
                amount = 0;
                amountLabel.Text = $"${amount}";
                return;
            }

            amount -= 10;
            amountLabel.Text = $"${amount}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            amount += 10;
            amountLabel.Text = $"${amount}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) ||
                string.IsNullOrEmpty(textBox4.Text) ||
                string.IsNullOrEmpty(textBox5.Text) ||
                string.IsNullOrEmpty(textBox6.Text) ||
                comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("All the fields are required.");
                return;
            }

            if (amount <= 0)
            {
                MessageBox.Show("Sponsor amount should greater than 0.");
                return;
            }

            if (textBox3.Text.Length != 16)
            {
                MessageBox.Show("Invalid card number.");
                return;
            }

            var lowers = "qwertyuiopasdfyhjklzxcvbnm";

            foreach (var c in lowers)
            {
                if (textBox3.Text.Contains(c.ToString()) || textBox3.Text.Contains(c.ToString().ToUpper()))
                {
                    MessageBox.Show("Invalid input for credit card no.");
                    return;
                }
            }

            try
            {
                if (textBox6.Text.Length < 3)
                {
                    MessageBox.Show("Invalid CVC.");
                    return;
                }

                int year = int.Parse(textBox4.Text);
                int month = int.Parse(textBox5.Text);

                var cvc = int.Parse(textBox6.Text);

                var date = new DateTime(year, month, 1);

                if (date < DateTime.Now)
                {
                    MessageBox.Show("Card is expired.");
                    return; 
                }
            }
            catch
            {
                MessageBox.Show("Invalid input for year and month.");
                return;
            }

            var sponsor = new Sponsorship()
            {
                SponsorName = textBox1.Text,
                RegistrationId = regisId,
                Amount = amount
            };

            ent.Sponsorships.Add(sponsor);
            ent.SaveChanges();

            this.Hide();

            new SponsorConfirmForm(sponsor.SponsorshipId).ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            regisId = (int)comboBox1.SelectedValue;
            var regis = ent.Registrations.FirstOrDefault(x => x.RegistrationId == regisId);
            charityLabel.Text = $"{regis.Charity.CharityName}";
            MessageBox.Show($"{regis.Charity.CharityDescription}", $"{regis.Charity.CharityName}");
        }

        int regisId = 0;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                charityLabel.Visible = false;
                pictureBox1.Visible = false;
                return; 
            }

            charityLabel.Visible = true;
            pictureBox1.Visible = true;

            regisId = (int)comboBox1.SelectedValue;
            var regis = ent.Registrations.FirstOrDefault(x => x.RegistrationId == regisId);
            charityLabel.Text = $"{regis.Charity.CharityName}";
        }
    }
}
