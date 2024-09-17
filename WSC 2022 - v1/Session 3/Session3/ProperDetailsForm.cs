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
    public partial class ProperDetailsForm : Form
    {
        public WSC2022SE_Session3Entities ent { get; set; }

        public ItemPrice ItemPrice { get; set; }

        public ProperDetailsForm(long itemPriceID, string searchText)
        {
            InitializeComponent();

            ent = new WSC2022SE_Session3Entities();

            this.ItemPrice = ent.ItemPrices.First(x => x.ID == itemPriceID);

            textBox1.Text = searchText;
            textBox1.Enabled = false;

            var user = ent.Users.First(x => x.ID == 3);

            label1.Text = $"Welcome {user.FullName}";
            this.Text = $"Seoul Stay - Property Details for \"{this.ItemPrice.Item.Title}\"";
            loadData();
        }

        void loadData()
        {
            label2.Text = $"Details of \"{this.ItemPrice.Item.Title}\" at {this.ItemPrice.Item.Area.Name}";
            label3.Text = $"Capacity: {this.ItemPrice.Item.Capacity} Bedrooms: {this.ItemPrice.Item.NumberOfBedrooms}" +
                $" Beds: {this.ItemPrice.Item.NumberOfBeds} Bathroom: {this.ItemPrice.Item.NumberOfBathrooms}";

            label5.Text = this.ItemPrice.Item.Description;
            label7.Text = this.ItemPrice.Item.HostRules;

            if (this.ItemPrice.Item.ItemPictures.Count != 0)
            {
                foreach (var item in this.ItemPrice.Item.ItemPictures)
                {
                    var uc = new ItemPictureUserControl();
                    uc.pictureBox1.Image = Image.FromFile(item.PictureFileName);
                    flowLayoutPanel1.Controls.Add(uc);
                }
            }

            if (this.ItemPrice.Item.ItemAmenities.Count != 0)
            {
                foreach (var item in this.ItemPrice.Item.ItemAmenities)
                {
                    var uc = new AmenityUserControl();
                    uc.pictureBox1.Image = Image.FromFile(item.Amenity.IconName);
                    uc.label1.Text= item.Amenity.Name;
                    flowLayoutPanel2.Controls.Add(uc);
                }
            }

            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = this.ItemPrice.Item.Capacity;

            label9.Text = $"Check in: {this.ItemPrice.Date.ToString("dd/MM/yyyy")}";
            label10.Text = $"Check in: {this.ItemPrice.Date.AddDays(this.ItemPrice.Item.MinimumNights).ToString("dd/MM/yyyy")}";

            label11.Text = $"Payable Amount: {this.ItemPrice.Price} USD";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var booking = new Booking()
            {
                GUID = Guid.NewGuid(),
                UserID = 3,
                BookingDate = DateTime.Now,
                AmountPaid = this.ItemPrice.Price
            };

            ent.Bookings.Add(booking);

            var bookingD = new BookingDetail() { 
                BookingID = booking.ID,
                GUID = Guid.NewGuid(),
                ItemPriceID = 3,
                isRefund = false,
            };

            ent.BookingDetails.Add(bookingD);

            ent.SaveChanges();

            this.Hide();

            new PaymentProcessForm(booking.ID).ShowDialog();
        }
    }
}
