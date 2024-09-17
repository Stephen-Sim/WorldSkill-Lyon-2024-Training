using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreshAPP
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(187, 187, 187);
                this.panel1.BackColor = Color.FromArgb(229, 26, 46);

                pfc.AddFontFile("WSC2022SE_TP09_Gotham-Bold_actual_en.otf");
                pfc.AddFontFile("OpenSans-Regular.ttf");
                pfc.AddFontFile("OpenSans-SemiBold.ttf");

                TitleFont = new Font(pfc.Families[0], 10f);
                RegularFont = new Font(pfc.Families[1], 9f);
                BoldFont = new Font(pfc.Families[2], 9f);

                this.Font = RegularFont;
                this.Title.Font = TitleFont;

                SetStyle(this);
            }
            catch (Exception)
            {

            }
        }

        void SetStyle(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is Button && c.Name != "CloseButton")
                {
                    ((Button)c).BackColor = ColorTranslator.FromHtml("#bbbbbb");
                    ((Button)c).ForeColor = Color.Black;
                    ((Button)c).FlatStyle = FlatStyle.Flat;
                }
                else if (c is ComboBox)
                {
                    var combo = (ComboBox)c;
                    combo.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                else if (c.HasChildren)
                {
                    SetStyle(c);
                }
            }
        }

        PrivateFontCollection pfc = new PrivateFontCollection();

        public Font TitleFont { get; set; }
        public Font RegularFont { get; set; }
        public Font BoldFont { get; set; }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
