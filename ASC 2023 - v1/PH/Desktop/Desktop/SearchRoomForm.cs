using Desktop.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class SearchRoomForm : Form
    {
        public ASC2023_PH_DesktopEntities ent { get; set; }

        public int UserID { get; set; }

        public SearchRoomForm(int userID)
        {
            InitializeComponent();
            this.UserID = userID;

            ent = new ASC2023_PH_DesktopEntities();
            loadData();
            loadActiveBookings();
        }

        public class Temp
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        void loadData()
        {
            var roomtypes = new List<Temp>();

            roomtypes.Add(new Temp() { id = 0, name = "All"});

            var types = ent.roomtypes.ToList();

            foreach (var type in types)
            {
                roomtypes.Add(new Temp() { id = type.id, name = type.Type });
            }

            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = roomtypes;
        }

        void loadActiveBookings()
        {
            dataGridView1.Rows.Clear();

            var bookings = ent.bookings.Where(x => x.UserID == this.UserID && x.checkoutdate == null).ToList();

            foreach (var booking in bookings)
            {
                dataGridView1.Rows.Add(booking.id, 
                    booking.Guest,
                    booking.room.roomtype1.Type,
                    booking.room.RoomNumber,
                    ((decimal)booking.room.Price).ToString("0.00"),
                    $"{(booking.Date_To - booking.Date_From).Value.Days} nigths",
                    ((DateTime)booking.Date_From).ToString("yyyy-M-d"),
                    ((DateTime)booking.Date_To).ToString("yyyy-M-d"),
                    ((decimal)booking.Total).ToString("0.00"),
                    "checkout");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("all fields are required.");
                return;
            }
            flowLayoutPanel1.Controls.Clear();

            var rooms = ent.rooms.ToList();

            if (comboBox1.SelectedIndex != 0)
            {
                rooms = rooms.Where(x => x.RoomType == (int)comboBox1.SelectedValue).ToList();
            }

            foreach (var room in rooms)
            {
                var uc = new RoomUserControl();

                uc.Id = room.ID;

                switch (room.Photo)
                {
                    case "401.jpg":
                        uc.pictureBox1.Image = Properties.Resources._401;
                        break;
                    case "402.jpg":
                        uc.pictureBox1.Image = Properties.Resources._402;
                        break;
                    case "403.jpg":
                        uc.pictureBox1.Image = Properties.Resources._403;
                        break;
                    case "404.jpg":
                        uc.pictureBox1.Image = Properties.Resources._404;
                        break;
                    case "405.jpg":
                        uc.pictureBox1.Image = Properties.Resources._405;
                        break;
                    case "406.jpg":
                        uc.pictureBox1.Image = Properties.Resources._406;
                        break;
                    default:
                        break;
                }

                uc.label1.Text = room.roomtype1.Type;
                uc.label2.Text = $"R-{room.RoomNumber}";
                uc.label3.Text = $"Php {((decimal)room.Price).ToString("0.00")} / Night";

                var bookings = ent.bookings.Where(x => x.RoomID == room.ID && x.checkoutdate == null).ToList();

                foreach (var booking in bookings)
                {
                    bool isTrue = false;

                    for (var i = booking.Date_From.Value; i <= booking.Date_To.Value; i = i.AddDays(1))
                    {
                        if (i.Date >= dateTimePicker1.Value.Date && i.Date <= dateTimePicker2.Value.Date)
                        {
                            isTrue = true;
                            break;
                        }
                    }

                    if (isTrue)
                    {
                        uc.panel1.BackColor = Color.Orange;
                        uc.button1.Visible = false;
                        break;
                    }
                }

                uc.button2.Click += (se, et) =>
                {
                    this.Hide();
                    new RoomDetailsForm(UserID, room.ID).ShowDialog();
                };

                uc.button1.Click += (se, et) =>
                {
                    this.Hide();
                    new BookRoomForm(UserID, room.ID).ShowDialog();
                };

                flowLayoutPanel1.Controls.Add(uc);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                var res = MessageBox.Show("Are you sure to checkout", "", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    var id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                    var booking = ent.bookings.First(x => x.id == id);

                    booking.checkoutdate = DateTime.Now.Date.ToString("yyyy-MM-dd");

                    ent.SaveChanges();
                    loadActiveBookings();
                }
            }
        }
    }
}
