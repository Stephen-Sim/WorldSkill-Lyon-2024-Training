using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopAPP
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        private void clsButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        PrivateFontCollection pfc = new PrivateFontCollection();

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.titlePanel.BackColor = ColorTranslator.FromHtml("#E2231A");
                this.BackColor = ColorTranslator.FromHtml("#FFA823");

                pfc.AddFontFile("Helvetica-Normal Regular.ttf");
                pfc.AddFontFile("Myriad-Pro.OTF");

                this.Font = new Font(pfc.Families[0], 9f);
                this.titleLabel.Font = new Font(pfc.Families[1], 10f);
            }
            catch (Exception)
            {

            }
        }
    }
}
