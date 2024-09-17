using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DesktopAPP
{
    public partial class AccountablePartyEMRequestForm : DesktopAPP.BaseForm
    {
        public Asset Asset { get; set; }
        public WSC2019_Session2Entities ent { get; set; }
        
        public AccountablePartyEMRequestForm(long AssetID)
        {
            InitializeComponent();
            ent = new WSC2019_Session2Entities();

            this.Asset = ent.Assets.First(x => x.ID == AssetID);
        }

        void loadData()
        {
            var prios = ent.Priorities.ToList();

            comboBox1.DataSource = prios;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Name";

            comboBox1.SelectedIndex = -1;

            label4.Text = this.Asset.AssetSN;
            label5.Text = this.Asset.AssetName;
            label6.Text = this.Asset.DepartmentLocation.Department.Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new AccountablePartyEMMangementForm().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("All the fields are required.");
                return; 
            }

            var em = new EmergencyMaintenance()
            {
                AssetID = this.Asset.ID,
                PriorityID = (long) comboBox1.SelectedValue,
                DescriptionEmergency = textBox1.Text,
                OtherConsiderations = textBox2.Text,
                EMReportDate = DateTime.Today
            };

            ent.EmergencyMaintenances.Add(em);
            ent.SaveChanges();

            MessageBox.Show("Request is added.");

            this.Hide();
            new AccountablePartyEMMangementForm().ShowDialog();
        }

        private void AccountablePartyEMRequestForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
