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
using System.Windows.Forms.VisualStyles;

namespace Session1
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public PrivateFontCollection pfc { get; set; } = new PrivateFontCollection();

        private void BaseForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = ColorTranslator.FromHtml("#FFFAF0");
                this.HeaderPanel.BackColor = ColorTranslator.FromHtml("#D4A017");

                pfc.AddFontFile("Montserrat-Bold.ttf");
                pfc.AddFontFile("Montserrat-Medium.ttf");
                pfc.AddFontFile("OpenSans-Regular.ttf");

                this.TitleLabel.Font = new Font(pfc.Families[0], this.TitleLabel.Font.Size);

                loadStyle(this);
            }
            catch (Exception)
            {

            }
        }

        void loadStyle(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Name == "HeaderPanel")
                {
                    continue;
                }

                c.Font = new Font(pfc.Families[2], c.Font.Size);
                c.ForeColor = ColorTranslator.FromHtml("#3C280D");

                if (c is Button)
                {
                    var btn = (Button)c;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = ColorTranslator.FromHtml("#A4C2D6");
                    c.Font = new Font(pfc.Families[1], c.Font.Size);

                    if (c.Text.ToLower() == "close")
                    {
                        btn.BackColor = ColorTranslator.FromHtml("#C75D4D");
                    }
                }

                if (c is ComboBox)
                {
                    var btn = (ComboBox)c;
                    btn.DropDownStyle = ComboBoxStyle.DropDownList;
                }

                if (c.HasChildren)
                {
                    loadStyle(c);
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
