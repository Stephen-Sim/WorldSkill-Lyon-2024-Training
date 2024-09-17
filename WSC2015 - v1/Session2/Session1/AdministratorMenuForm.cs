using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Session1
{
    public partial class AdministratorMenuForm : Session1.CoreForm
    {
        public AdministratorMenuForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new MainScreenForm().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            new LoginForm().ShowDialog();
        }
    }
}
