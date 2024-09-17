using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class BookRoomForm : Form
    {
        public int UserID { get; set; }

        public room room { get; set; }

        public ASC2023_PH_DesktopEntities ent { get; set; }

        public BookRoomForm(int userid, int roomid)
        {
            InitializeComponent();

            this.UserID = userid;

            ent = new ASC2023_PH_DesktopEntities();

            room = ent.rooms.First(x => x.ID == roomid);

            switch (room.Photo)
            {
                case "401.jpg":
                    pictureBox1.Image = Properties.Resources._401;
                    break;
                case "402.jpg":
                    pictureBox1.Image = Properties.Resources._402;
                    break;
                case "403.jpg":
                    pictureBox1.Image = Properties.Resources._403;
                    break;
                case "404.jpg":
                    pictureBox1.Image = Properties.Resources._404;
                    break;
                case "405.jpg":
                    pictureBox1.Image = Properties.Resources._405;
                    break;
                case "406.jpg":
                    pictureBox1.Image = Properties.Resources._406;
                    break;
                default:
                    break;
            }

            label2.Text = room.Description;
            label3.Text = room.roomtype1.Type;
            label4.Text = $"Php {((decimal)room.Price).ToString("0.00")}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new SearchRoomForm(UserID).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                MessageBox.Show($"Date To cannot smaller than date from.");
                return;
            }

            var day = dateTimePicker1.Value.Date;
            var day1 = dateTimePicker2.Value.Date;

            var count_day = (day1 - day).Days + 1;

            var booking = new booking()
            {
                Date = DateTime.Now,
                Guest = textBox1.Text,
                Phone = textBox2.Text,
                RoomID = this.room.ID,
                Date_From = dateTimePicker1.Value.Date,
                Date_To = dateTimePicker2.Value.Date,
                checkoutdate = null,
                Total = count_day * room.Price,
                UserID = this.UserID,
            };

            ent.bookings.Add(booking);
            ent.SaveChanges();

            MessageBox.Show("booking is made");

            this.Hide();

            new SearchRoomForm(this.UserID).ShowDialog();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                MessageBox.Show($"Date To cannot smaller than date from.");
            }

            dateChange();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                MessageBox.Show($"Date To cannot smaller than date from.");
            }

            dateChange();
        }

        void dateChange()
        {
            var day = dateTimePicker1.Value.Date;
            var day1 = dateTimePicker2.Value.Date;

            var count_day = (day1 - day).Days + 1;

            label9.Text = $"Nights: {count_day}";
            label7.Text = $"Total: Php ${count_day * room.Price}";

            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                label9.Text = $"Nights: -";
                label7.Text = $"Total: -";
            }
        }
    }
}
