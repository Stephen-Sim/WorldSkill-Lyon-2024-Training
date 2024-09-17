using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DesktopAPP
{
    public partial class Form1 : DesktopAPP.BaseForm
    {
        public WSC2019_Session4Entities ent { get; set; }

        public Form1()
        {
            InitializeComponent();
            ent = new WSC2019_Session4Entities();
        }

        void loadData()
        {
            dataGridView1.Rows.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();

            var orderItems = ent.OrderItems.ToList().Select(x => new
            {
                x.ID,
                PartName = x.Part.Name,
                TransactionType = x.Order.TransactionType.Name,
                Date = x.Order.Date.ToString("yyyy-MM-dd"),
                x.Amount,
                Source = x.Order.Warehouse == null ? x.Order.Supplier.Name : x.Order.Warehouse.Name,
                Destination = x.Order.Warehouse1.Name,
                x.Order.TransactionTypeID
            });

            foreach (var orderItem in orderItems)
            {
                var rowIndex = dataGridView1.Rows.Add(orderItem.ID, orderItem.PartName, orderItem.TransactionType, orderItem.Date, orderItem.Amount, orderItem.Source, orderItem.Destination, "Edit", "Remove");

                if (orderItem.TransactionTypeID == 1)
                {
                    dataGridView1.Rows[rowIndex].Cells[4].Style.BackColor = Color.Green;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
