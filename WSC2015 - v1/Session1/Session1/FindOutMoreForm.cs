using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Session1
{
    public partial class FindOutMoreForm : Session1.CoreForm
    {
        public FindOutMoreForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();

            new ListofCharitiesForm().ShowDialog();
        }
    }
}
