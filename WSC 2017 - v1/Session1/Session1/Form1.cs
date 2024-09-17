using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void loadStyle(Control control)
        {
            foreach (Control c in control.Controls)
            {

                c.ForeColor = Color.FromArgb(60, 40, 13);

                if (c is Button)
                {
                    var btn = (Button)c;
                    btn.FlatStyle = FlatStyle.Flat;
                }

                if (c is ComboBox)
                {
                    var comboBox = (ComboBox)c;
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }

                if (c.HasChildren)
                {
                    loadStyle(c);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(255, 250, 240);
                this.panel1.BackColor = Color.FromArgb(212, 160, 23);

                loadStyle(this);
            }
            catch (Exception)
            {

            }
            
        }
    }
}
