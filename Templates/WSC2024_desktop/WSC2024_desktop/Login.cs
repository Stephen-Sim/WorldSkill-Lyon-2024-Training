using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WSC2024_desktop
{
    public partial class Login : WSC2024_desktop.Form1
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            comboBox1.DataSource = new List<string>() { "abc", "yung huey", "123"};

            comboBox1.SelectedIndex = -1;
        }
    }
}
