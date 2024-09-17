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
    public partial class AccountablePartyEMMangementForm : DesktopAPP.BaseForm
    {
        public WSC2019_Session2Entities ent { get; set; }
        public AccountablePartyEMMangementForm()
        {
            InitializeComponent();
            ent = new WSC2019_Session2Entities();
        }

        void loadData()
        {
            dataGridView1.Rows.Clear();

            var assets = ent.Assets.ToList().Select(x => new
            {
                x.ID,
                x.AssetSN,
                x.AssetName,
                LastClosedEM = x.EmergencyMaintenances.Any(y => y.EMEndDate != null) ? ((DateTime)x.EmergencyMaintenances.FirstOrDefault(y => y.EMEndDate != null).EMEndDate).ToString("yyyy-MM-dd") : "--",
                NumberOfEMs = x.EmergencyMaintenances.Count(),
                IsAllCompleted = x.EmergencyMaintenances.Any(y => y.EMEndDate == null)
            }).ToList();

            foreach (var asset in assets)
            {
                var idx = dataGridView1.Rows.Add(asset.ID, asset.AssetSN, asset.AssetName, asset.LastClosedEM, asset.NumberOfEMs);

                if (!asset.IsAllCompleted)
                {
                    dataGridView1.Rows[idx].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("No Asset is selected.");
                return;
            }

            var AssetID = long.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            this.Hide();

            new AccountablePartyEMRequestForm(AssetID).ShowDialog();

        }

        private void AccountablePartyEMMangementForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
