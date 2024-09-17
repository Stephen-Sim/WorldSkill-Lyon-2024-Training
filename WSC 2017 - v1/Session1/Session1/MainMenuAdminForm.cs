using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Session1
{
    public partial class MainMenuAdminForm : Session1.Form1
    {
        public int id { get; set; }

        public MainMenuAdminForm(int id)
        {
            InitializeComponent();
            ent = new WSC2017_Session1Entities();
            this.id = id;

            loadData();
        }

        public WSC2017_Session1Entities ent { get; set; }

        private void label3_Click(object sender, EventArgs e)
        {
            var log = ent.LoginLogs.Where(x => x.user_id == this.id).Last();

            log.datetime_logout = DateTime.Now;

            ent.SaveChanges();

            this.Hide();

            new LoginForm().ShowDialog();
        }

        void loadData()
        {
            var temps = new List<Temp>() { new Temp() { id = 0, name = "All offices"} };

            var offices = ent.Offices.ToList().Select(x => new
            {
                x.ID,
                x.Title
            });

            foreach (var off in offices)
            {
                temps.Add(new Temp() { id = off.ID, name = off.Title });
            }

            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = temps;
        }

        public class Temp
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            var user = ent.Users.ToList().Where(x => x.RoleID == 2 && x.ID != this.id).Select(x => new
            {
                x.ID,
                x.FirstName,
                x.LastName,
                Age = DateTime.Today.Year - x.Birthdate.Value.Year,
                x.Email,
                Office = x.Office.Title,
                x.Active,
                x.OfficeID
            }).ToList();

            var id = (int)comboBox1.SelectedValue;
  
            if (id!= 0)
            {
                user = user.Where(x => x.OfficeID == id).ToList();
            }

            for (int i = 0; i < user.Count(); i++)
            {
                dataGridView1.Rows.Add(user[i].ID, user[i].FirstName, user[i].LastName, user[i].Age, user[i].Email, user[i].Office);

                if (user[i].Active == true)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("no row is selected.");
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];

            var id = int.Parse(selectedRow.Cells[0].Value.ToString());

            var user = ent.Users.FirstOrDefault(x => x.ID == id);

            user.Active = !user.Active;
            ent.SaveChanges();

            MessageBox.Show("user is enable or disable.");

            loadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("no row is selected.");
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];

            this.Hide();

            var id = int.Parse(selectedRow.Cells[0].Value.ToString());

            this.Hide();

            new EditUserForm(this.id, id).ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new AddUserForm(this.id).ShowDialog();
        }

        private void MainMenuAdminForm_Load(object sender, EventArgs e)
        {

        }
    }
}
