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
    public partial class AdminEMReqeustDetailForm : DesktopAPP.BaseForm
    {
        public WSC2019_Session2Entities ent { get; set; }

        public EmergencyMaintenance emergencyMaintenance { get; set; }

        public AdminEMReqeustDetailForm(long emID)
        {
            InitializeComponent();

            ent = new WSC2019_Session2Entities();

            emergencyMaintenance = ent.EmergencyMaintenances.First(x => x.ID == emID);
        }

        private void AdminEMReqeustDetailForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            Image img = Image.FromFile("add.png");
            var resizedImage = (Image)new Bitmap(img, 25, 25);
            button3.Image = resizedImage;

            var parts = ent.Parts.ToList();
            comboBox1.DataSource = parts;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.SelectedIndex = -1;

            label4.Text = this.emergencyMaintenance.Asset.AssetSN;
            label5.Text = this.emergencyMaintenance.Asset.AssetName;
            label6.Text = this.emergencyMaintenance.Asset.DepartmentLocation.Department.Name;

            if (emergencyMaintenance.EMStartDate != null)
            {
                dateTimePicker1.Value = (DateTime)emergencyMaintenance.EMStartDate;
                dateTimePicker1.Enabled = false;
            }

            if (emergencyMaintenance.EMEndDate != null)
            {
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                button1.Enabled = false;

                dateTimePicker2.Value = (DateTime) this.emergencyMaintenance.EMEndDate;
                textBox1.Text = this.emergencyMaintenance.DescriptionEmergency;
            }

            dataGridView1.Rows.Clear();

            foreach (var cp in this.emergencyMaintenance.ChangedParts)
            {
                dataGridView1.Rows.Add(cp.PartID, cp.Part.Name, (int)cp.Amount, "Remove");
            }

            textBox1_TextChanged(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new AdminEMManagementForm().ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || numericUpDown1.Value <= 0)
            {
                MessageBox.Show("all the fields are required.");
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var rowCPId = long.Parse(row.Cells[0].Value.ToString());

                if (rowCPId == (long)comboBox1.SelectedValue)
                {
                    MessageBox.Show("selected part already exist in the table, please remove it for the futher action.");
                    return;
                }
            }

            var part = ent.Parts.FirstOrDefault(x => x.ID == (long)comboBox1.SelectedValue);

            if (ent.ChangedParts.ToList().Any(x => x.PartID == part.ID && x.EmergencyMaintenanceID != emergencyMaintenance.ID && 
                x.EmergencyMaintenance.EMStartDate.Value.Date.AddDays((x.Part.EffectiveLife == null ? 0 : (long)x.Part.EffectiveLife)) > DateTime.Today))
            {
                MessageBox.Show("this part is used by other emergency request.");
            }

            dataGridView1.Rows.Add(part.ID, part.Name, (int)numericUpDown1.Value, "Remove");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var result = MessageBox.Show("are you sure to remove this part?", "", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var em = ent.EmergencyMaintenances.FirstOrDefault(x => x.ID == this.emergencyMaintenance.ID);

            em.EMStartDate = dateTimePicker1.Value;

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (dateTimePicker2.Value <= dateTimePicker1.Value)
                {
                    MessageBox.Show("the completed date could not greater than start date.");
                    return;
                }

                em.EMTechnicianNote = textBox1.Text;
                em.EMEndDate = dateTimePicker2.Value;   
            }

            ent.ChangedParts.RemoveRange(em.ChangedParts);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cp = new ChangedPart()
                {
                    PartID = long.Parse(row.Cells[0].Value.ToString()),
                    Amount = int.Parse(row.Cells[2].Value.ToString()),
                    EmergencyMaintenanceID = em.ID
                };

                ent.ChangedParts.Add(cp);
            }

            ent.SaveChanges();

            MessageBox.Show("The detail is saved.");

            this.Hide();

            new AdminEMManagementForm().ShowDialog();
        }
    }
}
