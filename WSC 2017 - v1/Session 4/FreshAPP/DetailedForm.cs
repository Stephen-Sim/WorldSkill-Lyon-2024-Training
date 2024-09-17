using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FreshAPP
{
    public partial class DetailedForm : FreshAPP.BaseForm
    {
        public WSC2017_Session4Entities ent { get; set; }

        public DetailedForm()
        {
            ent = new WSC2017_Session4Entities();
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().ShowDialog();
        }

        void loadInit()
        {
            var surveys = ent.Surveys.ToList().Where(x => x.Gender != null && x.Age != null && x.CabinType != null && x.Arrival != null).ToList();

            var minDate = surveys.Min(x => x.SurveyDate);
            var maxDate = surveys.Max(x => x.SurveyDate);

            var dates = new List<DateData>();

            while (minDate <= maxDate)
            {
                dates.Add(new DateData
                {
                    DisplayDateTime = minDate.ToString("MMMM yyyy"),
                    Month = minDate.Month
                });

                minDate = minDate.AddMonths(1);
            }

            comboBox1.DisplayMember = "DisplayDateTime";
            comboBox1.ValueMember = "Month";
            comboBox1.DataSource = dates;
        }

        void loadPercentage()
        {
            var table = new TableLayoutPanel();
            table.RowCount = 1;
            table.ColumnCount = 7;
            table.Margin = new Padding(0, 8, 0, 8);
            table.Dock = DockStyle.Fill;

            List<decimal> percent = new List<decimal>()
            { 
                5, 5, 10, 15, 20, 20, 25  
            };

            List<Color> colors = new List<Color>()
            {
                Color.Gray, Color.Red, Color.Pink, Color.Pink, Color.LightGreen, Color.Green, Color.DarkGreen
            };

            for (int i = 0; i < percent.Count; i++)
            {
                var panel = new Panel();
                panel.Dock = DockStyle.Fill;
                panel.Margin = new Padding(0);
                panel.BackColor = colors[i];
                table.Controls.Add(panel);

                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)percent[i]));
            }

            tableLayoutPanel1.Controls.Add(table, 1, 0);

        }

        class DateData
        {
            public string DisplayDateTime { get; set; }
            public int Month { get; set; }
        }

        private void DetailedForm_Load(object sender, EventArgs e)
        {
            loadInit();
            loadPercentage();
        }
    }
}
