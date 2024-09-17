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

namespace CustomerTrend
{
    public partial class Form1 : Form
    {
        public chartPracticeEntities ent { get; set; }

        public Form1()
        {
            InitializeComponent();

            ent = new chartPracticeEntities();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            var chart = new Chart
            {
                Size = new Size(397, 355),
            };

            // gender count
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();

            chart.ChartAreas.Add(new ChartArea());

            chart.Series.Add("Gender");
            
            chart.Legends.Add("Gender");

            chart.Series["Gender"].Color = Color.LightBlue;

            chart.Series["Gender"].ChartType = SeriesChartType.Column;

            var genderCount = ent.Customers.GroupBy(x => x.Gender).Select(x => new {
                Gender = x.Key,
                Count = x.Count(),
            }).ToList();

            chart.Series["Gender"].Points.AddXY("Mela", genderCount.First(x => x.Gender == "Male").Count);
            chart.Series["Gender"].Points.AddXY("Female", genderCount.First(x => x.Gender == "Female").Count);

            chart.Series["Gender"].IsValueShownAsLabel = true;
            chart.Series["Gender"].IsValueShownAsLabel = true;

            chart.ChartAreas[0].AxisX.Title = "Number of Occurrences";
            chart.ChartAreas[0].AxisY.Title = "Employment Type";
            

            flowLayoutPanel1.Controls.Add(chart);

            //// gender count
            
            chart = new Chart
            {
                Size = new Size(397, 355),
            };

            chart.Series.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            chart.ChartAreas.Add(new ChartArea());

            chart.Series.Add("Gender");

            chart.Legends.Add("Gender");

            chart.Series["Gender"].Color = Color.LightBlue;

            chart.Series["Gender"].ChartType = SeriesChartType.Pie;

            var genderPercent = ent.Customers.ToList().GroupBy(x => x.Gender).Select(x => new {
                Gender = x.Key,
                Percentage = (x.Count() * 1.0 / ent.Customers.Count() * 100),
            }).ToList();

            chart.Series["Gender"].Points.AddXY("Mela", genderPercent.First(x => x.Gender == "Male").Percentage);
            chart.Series["Gender"].Points.AddXY("Female", genderPercent.First(x => x.Gender == "Female").Percentage);

            chart.Series["Gender"].IsValueShownAsLabel = true;
            chart.Series["Gender"].IsValueShownAsLabel = true;

            flowLayoutPanel1.Controls.Add(chart);

            chart = new Chart
            {
                Size = new Size(397, 355),
            };

            // Clear previous settings
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            // Add a chart area
            var chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Add histogram series
            var histogramSeries = new Series("Histogram")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.SkyBlue,
                BorderColor = Color.Black,
                BorderWidth = 1,
                IsVisibleInLegend = true
            };

            chart.Series.Add(histogramSeries);

            // Data retrieval and processing
            var customers = ent.Customers.ToList();

            for (int i = 0; i <= 100; i += 5)
            {
                int count = customers.Count(x => x.Age >= i && x.Age <= i + 5);
                double density = (double)count / customers.Count();

                chart.Series["Histogram"].Points.AddXY($"{i} - {i + 5}", density);
            }

            // Add labels and titles
            chart.Titles.Add("Age Distribution Histogram");
            chart.ChartAreas[0].AxisX.Title = "Age";
            chart.ChartAreas[0].AxisY.Title = "Density";

            chart.ChartAreas[0].AxisX.IsStartedFromZero = true;

            // Add legend
            chart.Legends.Add(new Legend());

            // Add the chart to the FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(chart);

        }
    }
}
