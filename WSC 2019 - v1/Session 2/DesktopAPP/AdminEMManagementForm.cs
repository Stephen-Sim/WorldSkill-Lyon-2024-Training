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
    public partial class AdminEMManagementForm : DesktopAPP.BaseForm
    {
        public WSC2019_Session2Entities ent { get; set; }
        public AdminEMManagementForm()
        {
            InitializeComponent();

            ent = new WSC2019_Session2Entities();
        }

        void loadData()
        {
            dataGridView1.Rows.Clear();

            var ems = ent.EmergencyMaintenances.ToList().OrderByDescending(x => x.PriorityID).ThenBy(x => x.EMReportDate).Select(x => new {
                x.ID,
                x.Asset.AssetSN,
                x.Asset.AssetName,
                EMReportDate = x.EMReportDate.ToString("yyyy-MM-dd"),
                EmployeeFullName = x.Asset.Employee.FirstName + " " + x.Asset.Employee.LastName,
                DepartmentName = x.Asset.DepartmentLocation.Department.Name
            }).ToArray();

            foreach ( var e in ems )
            {
                dataGridView1.Rows.Add(e.ID, e.AssetSN, e.AssetName, e.EMReportDate, e.EmployeeFullName, e.DepartmentName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("requesting em is not selected.");
                return;
            }

            var emId = long.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            Hide();

            new AdminEMReqeustDetailForm(emId).ShowDialog();
        }

        private void AdminEMManagementForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
