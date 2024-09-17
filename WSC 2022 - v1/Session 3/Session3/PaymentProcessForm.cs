using Newtonsoft.Json;
using Session3.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session3
{
    public partial class PaymentProcessForm : Form
    {
        public Booking Booking { get; set; }

        public WSC2022SE_Session3Entities ent { get; set; }

        public long transacID { get; set; }

        public PaymentProcessForm(long bookingID)
        {
            InitializeComponent();
            ent = new WSC2022SE_Session3Entities();

            this.Booking = ent.Bookings.First(x => x.ID == bookingID);
        }

        public string token { get; set; }

        private async void PaymentProcessForm_Load(object sender, EventArgs e)
        {
            label10.Text = ((decimal)this.Booking.AmountPaid).ToString("0.00");
            HttpClient client = new HttpClient();
            
            var data = new
            {
                customer = ent.Users.First(x => x.ID == 3).FullName,
                orderId = this.Booking.ID.ToString(),
                price = this.Booking.AmountPaid,
                extraInfo = $"transcation for booking {this.Booking.BookingDetails.First().ItemPrice.Item.Title}",
                callBackUrl = "https://prim.my/derma"
            };  

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync("http://localhost:5000/Payment/PaymentToken", content);

            if (res.IsSuccessStatusCode)
            {
                token = await res.Content.ReadAsStringAsync();

                var transaction = new Transaction()
                {
                    Amount = (decimal)Booking.AmountPaid,
                    GUID = Guid.NewGuid(),
                    TransactionTypeID = 1,
                    UserID = 3,
                    TransactionDate = DateTime.Now,
                    GatewayReturnID = token
                };

                ent.Transactions.Add(transaction);

                ent.SaveChanges();

                transacID = transaction.ID;

                label11.Text = $"{transacID}";
                label12.Text = $"transcation for booking {this.Booking.BookingDetails.First().ItemPrice.Item.Title}";
            }

            this.button1.Text = $"Pay Now (${this.Booking.AmountPaid})";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new Form1().ShowDialog();
        }

        string[] strs = { "success", "low credit", "wrong password"};

        private async void button1_Click(object sender, EventArgs e)
        {
            var obj = new {
                cardNo =  textBox1.Text,
                orderId = this.Booking.ID.ToString(),
                extraInfo = $"transcation for booking {this.Booking.BookingDetails.First().ItemPrice.Item.Title}",
                status = strs[new Random().Next(0, strs.Length)],
                trackId = $"{token}",
                price = this.Booking.AmountPaid,
                callBackUrl = "https://prim.my/derma",
                verify = true
            };

            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var payementres = await client.PostAsync("http://localhost:5000/Payment/Pay", content);

            if (payementres.IsSuccessStatusCode)
            {

                var trackid = await payementres.Content.ReadAsStringAsync();

                var res = await client.GetAsync($"http://localhost:5000/Payment/VerifyPayment?trackId={trackid}");

                if (res.IsSuccessStatusCode)
                {
                    this.Hide();
                    var jsonStr = await res.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<PaymentResult>(jsonStr);

                    if (result.status == "success")
                    {
                        var booking = ent.Bookings.FirstOrDefault(x => x.ID == this.Booking.ID);

                        booking.TransactionID = transacID;

                        ent.SaveChanges();

                        new SuccessPaymentForm(booking).ShowDialog();
                    }
                    else
                    {
                        new FailPaymentForm(result.status).ShowDialog();
                    }
                }
            }
        }
    }
}
