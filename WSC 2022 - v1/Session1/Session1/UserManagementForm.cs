using Session1.Properties;
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
    public partial class UserManagementForm : Session1.BaseForm
    {
        public long UserID { get; set; }
        public UserManagementForm(long userID)
        {
            InitializeComponent();

            this.UserID = userID;

            ent = new WSC2022SE_Session1Entities();

            loadData();
        }

        public WSC2022SE_Session1Entities ent { get; set; }

        void loadData()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                dataGridView1.Rows.Clear();

                var rows = ent.Items.ToList();

                for (int i = 0; i < rows.Count; i++)
                {
                    dataGridView1.Rows.Add(rows[i].Title, rows[i].Capacity, rows[i].Area.Name, rows[i].ItemType.Name);
                    if (i % 2 == 0)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }

                label1.Text = $"{rows.Count} items found.";
            }
            else
            {
                dataGridView2.Rows.Clear();

                var rows = ent.Items.Where(x => x.UserID == this.UserID).ToList();

                for (int i = 0; i < rows.Count; i++)
                {
                    dataGridView2.Rows.Add(rows[i].ID, rows[i].Title, rows[i].Capacity, rows[i].Area.Name, rows[i].ItemType.Name, "Edit Details");
                    if (i % 2 == 0)
                    {
                        dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }

                label1.Text = $"{rows.Count} items found.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Settings.Default.userID = 0;
            Settings.Default.userTypeID = 0;
            Settings.Default.Save();

            this.Hide();

            new Form1().ShowDialog();   
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            var rows = ent.Items.Where(x => x.Title.StartsWith(textBox1.Text) || x.Area.Name.StartsWith(textBox1.Text) || x.ItemAttractions.Any(y => y.Attraction.Name.StartsWith(textBox1.Text) && y.Distance < 1)).ToList();

            for (int i = 0; i < rows.Count; i++)
            {
                dataGridView1.Rows.Add(rows[i].Title, rows[i].Capacity, rows[i].Area.Name, rows[i].ItemType.Name);
                if (i % 2 == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

            label1.Text = $"{rows.Count} items found.";
        }
    }
}
