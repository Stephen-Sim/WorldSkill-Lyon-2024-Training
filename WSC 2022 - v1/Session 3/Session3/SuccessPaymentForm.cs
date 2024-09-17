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
    public partial class SuccessPaymentForm : Form
    {
        public Booking Booking { get; set; }

        public SuccessPaymentForm(Booking booking)
        {
            InitializeComponent();
            var ent = new WSC2022SE_Session3Entities();

            this.Booking = ent.Bookings.First(x => x.ID == booking.ID);

            label2.Text = $"Congratulations, the accommodation has been booked for you from {Booking.BookingDate.ToString("dd")} " +
                $"of {Booking.BookingDate.ToString("MMMM")} to {Booking.BookingDate.AddDays(Booking.BookingDetails.First().ItemPrice.Item.MinimumNights).ToString("dd")}th " +
                $"of {Booking.BookingDate.AddDays(Booking.BookingDetails.First().ItemPrice.Item.MinimumNights).ToString("MMMM")}. We wish you a nice stay!” should be " +
                $"placed at the top of the page with a sizing that emboldens and emphasizes its importance";
            label1.Text = "Welcome " + ent.Users.First(x => x.ID == 3).FullName;
            label4.Text = $"Transaction No: ${booking.TransactionID}";
            label5.Text = $"Propery Title: ${booking.BookingDetails.First().ItemPrice.Item.Title}";
            label6.Text = $"Total Fee: ${booking.AmountPaid}";
        }
    }
}
