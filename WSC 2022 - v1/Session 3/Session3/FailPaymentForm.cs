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
    public partial class FailPaymentForm : Form
    {
        public FailPaymentForm(string text)
        {
            InitializeComponent();
            var ent = new WSC2022SE_Session3Entities();
            label1.Text = "Welcome " + ent.Users.First(x => x.ID == 3).FullName;
            label4.Text = text;
        }
    }
}
