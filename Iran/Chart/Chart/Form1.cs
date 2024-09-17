using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            chart1.Titles.Clear();
            chart1.Titles.Add("Example Chart");

            chart1.Titles[0].Font = new Font("Arial", 12);

            chart1.Series.Add("Food");
            chart1.Series.Add("Utility");

            chart1.Series["Food"].Color = Color.Blue;
            chart1.Series["Utility"].Color = Color.Green;

            var y = new List<DateTime>()
            {
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                new DateTime(2020, 1, 3),
                new DateTime(2020, 1, 4),
                new DateTime(2020, 1, 5),
            };

            var foods = new List<decimal>()
            {
                10, 20, 10, 30, 10
            };

            var utils = new List<decimal>()
            {
                20, 10, 10, 30, 10
            };

            chart1.Series["Food"].ChartType = SeriesChartType.Polar;
            chart1.Series["Utility"].ChartType = SeriesChartType.Polar;

            for (int i = 0; i < y.Count; i++)
            {
                chart1.Series["Food"].Points.AddXY(y[i], foods[i]);
                chart1.Series["Utility"].Points.AddXY(y[i], utils[i]);
            }

            // set interval
            chart1.ChartAreas[0].AxisX.Interval = 2;

            // dashed line
            chart1.Series[0].BorderDashStyle = ChartDashStyle.Dash;

            // show value on chart
            chart1.Series["Food"].IsValueShownAsLabel = true;
            chart1.Series["Utility"].IsValueShownAsLabel = true;

            // marker
            chart1.Series["Food"].MarkerStyle = MarkerStyle.Circle;

            chart1.ChartAreas[0].RecalculateAxesScale();
        }
    }
}
