using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSC2024_desktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void setStyle(Control control)
        {
            if (control.Name == "panel1")
            {

            }
            else
            {
                foreach (Control c in control.Controls)
                {
                    c.ForeColor = Color.FromArgb(60, 40, 13);

                    if (c is Button)
                    {
                        Button b = (Button)c;

                        b.FlatStyle = FlatStyle.Flat;
                    }

                    if (c is ComboBox)
                    {
                        ComboBox b = (ComboBox)c;

                        b.DropDownStyle = ComboBoxStyle.DropDownList;
                    }

                    if (c.HasChildren)
                    {
                        setStyle(c);
                    }
                }

            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(212, 160, 33);
            this.BackColor = Color.FromArgb(255, 250, 240);

            try
            {
                setStyle(this);
            }
            catch (Exception)
            {

            }
        }
    }
}
