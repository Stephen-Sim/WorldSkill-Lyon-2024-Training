using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Session1
{
    public partial class MainScreenForm : Session1.CoreForm
    {
        public MainScreenForm()
        {
            InitializeComponent();
        }

        private void MainScreenForm_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            new SponsorRunnerForm().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            new FindOutMoreForm().ShowDialog();
        }
    }
}
