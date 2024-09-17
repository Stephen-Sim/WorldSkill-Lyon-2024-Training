using Session3.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session3
{
    public partial class Form1 : Form
    {
        public User User { get; set; }
        public WSC2022SE_Session3Entities ent { get; set; }


        public Form1()
        {
            InitializeComponent();

            ent = new WSC2022SE_Session3Entities();

            User = ent.Users.First(x => x.ID == 3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = $"Welcome {this.User.FullName}";
            loadData();
        }

        void loadData()
        {
            label2.Text = $"Reservation result for {textBox1.Text} from 25/11/2022 for 1 night";
            
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                label2.Text = $"Reservation result for all areas from 25/11/2022 for 1 night";
            }

            var itemPrices = ent.ItemPrices.ToList().Where(x => x.Item.Area.Name.StartsWith(textBox1.Text) && x.Date.Date >= new DateTime(2022, 11, 25)).ToList();

            flowLayoutPanel1.Controls.Clear();

            foreach (var item in itemPrices)
            {
                var uc = new ItemPriceUserControl();
                uc.ID = item.ID;
                uc.label1.Text = item.Item.Title;
                uc.label2.Text = item.Item.Area.Name;
                uc.label3.Text = item.Item.Capacity + " people";
                uc.label4.Text = "total price: " + item.Price + "$";

                if (item.Item.ItemPictures.Count == 0)
                {
                    uc.pictureBox1.Image = Image.FromFile("default.jpg");
                }
                else
                {
                    var random = new Random().Next(0, item.Item.ItemPictures.Count);

                    uc.pictureBox1.Image = Image.FromFile(item.Item.ItemPictures.Skip(random).First().PictureFileName);
                }

                uc.panel1.Click += (s ,e) =>
                {
                    this.Hide();
                    new ProperDetailsForm(uc.ID, textBox1.Text).ShowDialog();
                };

                flowLayoutPanel1.Controls.Add(uc);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
