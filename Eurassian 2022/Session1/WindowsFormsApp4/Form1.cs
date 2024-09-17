using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.RowCount = 3;

            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 4);

            // Array of label texts
            string[] labelTexts = { "10-20", "20-30", "40-50", "60-70" };

            // Loop to create and add labels to the TableLayoutPanel
            for (int i = 0; i < labelTexts.Length; i++)
            {
                Label newLabel = new Label();
                newLabel.Dock = DockStyle.Fill;
                newLabel.Margin = new Padding(0);
                newLabel.BorderStyle = BorderStyle.FixedSingle;
                newLabel.TextAlign = ContentAlignment.MiddleCenter;
                newLabel.Text = labelTexts[i];

                // Add the new label to the TableLayoutPanel starting at column 2, row 1
                this.tableLayoutPanel1.Controls.Add(newLabel, i + 2, 1);
            }

            for (int i = 0; i < 6; i++)
            {
                Label newLabel = new Label();
                newLabel.Dock = DockStyle.Fill;
                newLabel.Margin = new Padding(0);
                newLabel.BorderStyle = BorderStyle.FixedSingle;
                newLabel.TextAlign = ContentAlignment.MiddleCenter;
                newLabel.Text = ((i + 1) * 10).ToString();

                tableLayoutPanel1.Controls.Add(newLabel, i, 2);
            }

            loadYear();
        }

        void loadYear()
        {
            tableLayoutPanel2.ColumnCount = 12;
            var years = new List<int>() { 2022, 2023, 2024 };
            var months = new List<string>() { "Jan", "Feb", "March", "April" };

            var i = 0; // Column index for years
            var j = 0; // Column index for months

            foreach (int year in years)
            {
                // Create and configure the year label
                Label yearLabel = new Label();
                yearLabel.Dock = DockStyle.Fill;
                yearLabel.Margin = new Padding(0);
                yearLabel.BorderStyle = BorderStyle.FixedSingle;
                yearLabel.TextAlign = ContentAlignment.MiddleCenter;
                yearLabel.Text = year.ToString();
                yearLabel.ForeColor = Color.White;
                yearLabel.BackColor = Color.Blue;

                // Add the year label to the first row and set column span to 4
                this.tableLayoutPanel2.Controls.Add(yearLabel, i++, 0);
                this.tableLayoutPanel2.SetColumnSpan(yearLabel, 4);

                // Add month labels under the year label
                foreach (var month in months)
                {
                    Label monthLabel = new Label();
                    monthLabel.Dock = DockStyle.Fill;
                    monthLabel.Margin = new Padding(0);
                    monthLabel.BorderStyle = BorderStyle.FixedSingle;
                    monthLabel.TextAlign = ContentAlignment.MiddleCenter;
                    monthLabel.Text = month;
                    monthLabel.ForeColor = Color.Black;
                    monthLabel.BackColor = Color.LightBlue;

                    // Add the month label to the second row
                    this.tableLayoutPanel2.Controls.Add(monthLabel, j, 1);
                    j++; // Move to the next column for the next month
                }
            }

            for (int x = 0; x < 10; x++)
            {
                foreach (int year in years)
                {
                    j = 0;
                    foreach (var month in months)
                    {
                        Label monthLabel = new Label();
                        monthLabel.Dock = DockStyle.Fill;
                        monthLabel.Margin = new Padding(0);
                        monthLabel.BorderStyle = BorderStyle.FixedSingle;
                        monthLabel.TextAlign = ContentAlignment.MiddleCenter;
                        monthLabel.BackColor = Color.White;
                        monthLabel.Text = (j + 1 * 10).ToString();

                        this.tableLayoutPanel2.Controls.Add(monthLabel, j++, x + 2);
                    }
                }
            }
        }

    }
}
