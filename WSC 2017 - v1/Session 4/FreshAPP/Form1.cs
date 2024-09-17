using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FreshAPP
{
    public partial class Form1 : FreshAPP.BaseForm
    {
        WSC2017_Session4Entities ent = new WSC2017_Session4Entities();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
            loadheader();
        }

        void loadData()
        {
            var surveys = ent.Surveys.ToList().Where(x => x.Gender != null && x.Age != null && x.CabinType != null && x.Arrival != null).ToList();

            label1.Text = $"Field World {surveys.Min(x => x.SurveyDate).ToString("MMMM yyyy")} - {surveys.Max(x => x.SurveyDate).ToString("MMMM yyyy")}";
            label2.Text = $"Sample Size: {surveys.Count()} Adults";

            var gendercount = surveys.GroupBy(x => new
            {
                x.Gender
            }).Select(x => new {
                Gender = x.Key.Gender == "M" ? "Male" : "Female",
                Count = x.Count()
            }).ToList();

            int counter = 0;

            foreach (var item in gendercount)
            {
                var label = new Label();
                label.Dock = DockStyle.Fill;
                label.Height = 20;
                label.Width = 70;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Margin = new Padding(0);
                label.Text = item.Count.ToString();

                tableLayoutPanel1.Controls.Add(label, counter++, 2);
            }

            var ageCategory = new
            {
                below24 = surveys.Where(x => x.Age >= 18 &&  x.Age <= 24).Count(),
                to39 = surveys.Where(x => x.Age > 24 && x.Age <= 39).Count(),
                to59 = surveys.Where(x => x.Age > 39 && x.Age <= 59).Count(),
                above60 = surveys.Where(x => x.Age >= 60).Count(),
            };

            var ageCateList = new List<int>() { ageCategory.below24, ageCategory.to39, ageCategory.to59, ageCategory.above60 };

            foreach (var age in ageCateList)
            {
                var label = new Label();
                label.Dock = DockStyle.Fill;
                label.Height = 20;
                label.Width = 70;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Margin = new Padding(0);
                label.Text = age.ToString();

                tableLayoutPanel1.Controls.Add(label, counter++, 2);
            }

            var cbs = ent.CabinTypes.ToList();

            foreach (var cb in cbs)
            {
                var label = new Label();
                label.Dock = DockStyle.Fill;
                label.Height = 20;
                label.Width = 70;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Margin = new Padding(0);
                label.Text = surveys.Where(x => x.CabinType1.ID == cb.ID).Count().ToString();

                tableLayoutPanel1.Controls.Add(label, counter++, 2);
            }

            var airports = ent.Airports.ToList();

            foreach (var airport in airports)
            {
                var label = new Label();
                label.Dock = DockStyle.Fill;
                label.Height = 20;
                label.Width = 70;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Margin = new Padding(0);
                label.Text = surveys.Where(x => x.Airport1.ID == airport.ID).Count().ToString();

                tableLayoutPanel1.Controls.Add(label, counter++, 2);
            }
        }

        void loadheader()
        {
            tableLayoutPanel1.ColumnCount = 2 + 4 + ent.CabinTypes.Count() + ent.Airports.Count();

            // header
            var genderLabel = new Label();
            genderLabel.Dock = DockStyle.Fill;
            genderLabel.BorderStyle = BorderStyle.FixedSingle;
            genderLabel.Margin = new Padding(0);
            genderLabel.TextAlign = ContentAlignment.MiddleCenter;
            genderLabel.Text = "Gender";

            tableLayoutPanel1.Controls.Add(genderLabel, 0, 0);
            tableLayoutPanel1.SetColumnSpan(genderLabel, 2);

            var ageLabel = new Label();
            ageLabel.Dock = DockStyle.Fill;
            ageLabel.BorderStyle = BorderStyle.FixedSingle;
            ageLabel.Margin = new Padding(0);
            ageLabel.TextAlign = ContentAlignment.MiddleCenter;
            ageLabel.Text = "Age";

            tableLayoutPanel1.Controls.Add(ageLabel, 1, 0);
            tableLayoutPanel1.SetColumnSpan(ageLabel, 4);

            var cabinTypeLabel = new Label();
            cabinTypeLabel.Dock = DockStyle.Fill;
            cabinTypeLabel.BorderStyle = BorderStyle.FixedSingle;
            cabinTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            cabinTypeLabel.Margin = new Padding(0);
            cabinTypeLabel.Text = "Cabin Type";

            tableLayoutPanel1.Controls.Add(cabinTypeLabel, 2, 0);
            tableLayoutPanel1.SetColumnSpan(cabinTypeLabel, 3);

            var airportLabel = new Label();
            airportLabel.Dock = DockStyle.Fill;
            airportLabel.BorderStyle = BorderStyle.FixedSingle;
            airportLabel.Margin = new Padding(0);
            airportLabel.TextAlign = ContentAlignment.MiddleCenter;
            airportLabel.Text = "Destination Airport";

            tableLayoutPanel1.Controls.Add(airportLabel, 3, 0);
            tableLayoutPanel1.SetColumnSpan(airportLabel, ent.Airports.Count());

            int counter = 0;

            var genders = new List<string>() { "Male",  "Female" };

            foreach (var gender in genders)
            {
                var label = new Label();
                label.Dock = DockStyle.Fill;
                label.Height = 20;
                label.Width = 70;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Margin = new Padding(0);
                label.Text = gender;

                tableLayoutPanel1.Controls.Add(label, counter++, 1);
            }

            var ages = new List<string>() { "18-24", "25-39", "40-59", "60+" };

            foreach (var age in ages)
            {
                var label = new Label();
                label.Dock = DockStyle.Fill;
                label.Height = 20;
                label.Width = 70;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Margin = new Padding(0);
                label.Text = age;

                tableLayoutPanel1.Controls.Add(label, counter++, 1);
            }

            var cbs = ent.CabinTypes.ToList();

            foreach (var cb in cbs)
            {
                var label = new Label();
                label.Dock = DockStyle.Fill;
                label.Height = 20;
                label.Width = 70;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Margin = new Padding(0);
                label.Text = cb.CabinType1;

                tableLayoutPanel1.Controls.Add(label, counter++, 1);
            }

            var airports = ent.Airports.ToList();

            foreach (var airport in airports)
            {
                var label = new Label();
                label.Dock = DockStyle.Fill;
                label.Height = 20;
                label.Width = 70;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Margin = new Padding(0);
                label.Text = airport.IATACode;

                tableLayoutPanel1.Controls.Add(label, counter++, 1);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();

            new DetailedForm().ShowDialog();
        }
    }
}
