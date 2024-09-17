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
    public partial class RoomDetailsForm : Form
    {
        public int UserID { get; set; }

        public ASC2023_PH_DesktopEntities ent { get; set; }

        public RoomDetailsForm(int userid, int roomid)
        {
            InitializeComponent();

            this.UserID = userid;

            ent = new ASC2023_PH_DesktopEntities();

            var room = ent.rooms.First(x => x.ID == roomid);

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
    }
}
