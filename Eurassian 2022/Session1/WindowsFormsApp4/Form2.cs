using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form2 : Form
    {
        public Eurassian2022Session1Entities ent { get; set; }

        public Form2()
        {
            InitializeComponent();
            ent = new Eurassian2022Session1Entities();
        }

        DateTime today = new DateTime(2022, 09, 07);

        void loadheader()
        {
            Label elabel = new Label();
            elabel.Dock = DockStyle.Fill;
            elabel.Margin = new Padding(0);
            elabel.BorderStyle = BorderStyle.None;
            elabel.TextAlign = ContentAlignment.MiddleCenter;
            elabel.BorderStyle = BorderStyle.FixedSingle;
            elabel.BackColor = Color.LightGray;
            elabel.Text = "Employee";

            tableLayoutPanel1.Controls.Add(elabel, 0, 0);
            tableLayoutPanel1.SetRowSpan(elabel, 2);

            var startDate = today.AddDays(-10);
            var endDate = startDate.AddDays(21);

            int j = 1;

            var months = new Dictionary<string, int>();

            var currentMonth = startDate.Month;
            int count = 0;

            for (var i = startDate; i < endDate; i = i.AddDays(1))
            {
                if (currentMonth == i.Month)
                {
                    count++;
                }
                else
                {
                    months.Add(i.AddDays(-1).ToString("MMMM"), count);
                    currentMonth = i.Month;
                    count = 1;
                }

                if (i == endDate.AddDays(-1))
                {
                    months.Add(i.ToString("MMMM"), count);
                }
            }

            foreach (var month in months.Reverse().ToList())
            {
                Label newLabel = new Label();
                newLabel.Dock = DockStyle.Fill;
                newLabel.Margin = new Padding(0);
                newLabel.BorderStyle = BorderStyle.FixedSingle;
                newLabel.TextAlign = ContentAlignment.MiddleCenter;
                newLabel.BackColor = Color.LightGray;
                newLabel.Text = month.Key;

                tableLayoutPanel1.Controls.Add(newLabel, 1, 0);
                this.tableLayoutPanel1.SetColumnSpan(newLabel, month.Value);
            }

            for (var i = startDate; i < endDate; i = i.AddDays(1))
            {
                Label newLabel = new Label();
                newLabel.Dock = DockStyle.Fill;
                newLabel.Margin = new Padding(0);
                newLabel.BorderStyle = BorderStyle.FixedSingle;
                newLabel.TextAlign = ContentAlignment.MiddleCenter;
                newLabel.BackColor = Color.LightGray;

                newLabel.Text = i.ToString("dd");

                if (i == today)
                {
                    newLabel.BackColor = Color.Pink;
                }

                tableLayoutPanel1.Controls.Add(newLabel, j++, 1);
            }
        }

        void loadData()
        {
            var employees = ent.Employees.ToList();

            int counter = 2;

            foreach (var employee in employees)
            {
                Label newLabel = new Label();
                newLabel.Dock = DockStyle.Fill;
                newLabel.Margin = new Padding(0);
                newLabel.BorderStyle = BorderStyle.None;
                newLabel.TextAlign = ContentAlignment.MiddleRight;
                newLabel.BackColor = Color.White;
                newLabel.Text = employee.FirstName + " " + employee.LastName;

                this.tableLayoutPanel1.Controls.Add(newLabel, 0, counter);

                var currday = today.AddDays(-10);

                for (var j = 1; j <= 21; j++)
                {
                    Label workLabel = new Label();
                    workLabel.Dock = DockStyle.Fill;
                    workLabel.Margin = new Padding(0);
                    workLabel.BorderStyle = BorderStyle.FixedSingle;
                    workLabel.TextAlign = ContentAlignment.MiddleRight;
                    workLabel.Text = "";

                    tableLayoutPanel1.Controls.Add(workLabel, j, counter);

                    var count = employee.WorkRequests.Where(x => x.FullfilledDate.Date == currday).Count();

                    if (count > 2)
                    {
                        workLabel.BackColor = Color.DarkGreen;
                    }
                    else if (count == 2)
                    {
                        workLabel.BackColor = Color.Green;
                    }
                    else if (count == 1)
                    {
                        workLabel.BackColor = Color.GreenYellow;
                    }
                    else if (count == 0)
                    {
                        workLabel.BackColor = Color.LightGreen;
                    }

                    if (!employee.Schedules.Any(x => x.DayOfWeek == (int) currday.DayOfWeek))
                    {
                        workLabel.BackColor = Color.Orange;
                    }

                    if (currday.DayOfWeek == DayOfWeek.Sunday || currday.DayOfWeek == DayOfWeek.Saturday)
                    {
                        workLabel.BackColor = Color.White;
                    }

                    currday = currday.AddDays(1);
                }

                counter++;
            }
        }

        void loadColor()
        {
            this.panel4.BackColor = Color.Pink;
            this.panel5.BackColor = Color.White;
            this.panel6.BackColor = Color.Yellow;
            this.panel7.BackColor = Color.Orange;
            this.panel8.BackColor = Color.LightGreen;
            this.panel9.BackColor = Color.GreenYellow;
            this.panel10.BackColor = Color.Green;
            this.panel11.BackColor = Color.DarkGreen;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            loadheader();
            loadData();
            loadColor();
        }
    }
}
