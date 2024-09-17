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

namespace Session1
{
    public partial class CoreForm : Form
    {
        public CoreForm()
        {
            InitializeComponent();
        }

        public PrivateFontCollection pfc = new PrivateFontCollection();

        private void CoreForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.HeaderPanel.BackColor = Color.FromArgb(0, 144, 62);
                this.TimerPanel.BackColor = Color.FromArgb(0, 144, 62);

                pfc.AddFontFile("OpenSans-Light.ttf");
                pfc.AddFontFile("OpenSans-SemiBold.ttf");

                this.HeaderPanel.Font = new Font(pfc.Families[1], this.HeaderPanel.Font.Size);
                this.TimerPanel.Font = new Font(pfc.Families[1], this.TimerPanel.Font.Size);
                this.Font = new Font(pfc.Families[0], this.Font.Size);

                setStyle(this);
                loadtimer();
            }
            catch (Exception)
            {

            }
        }

        public void setStyle(Control Control)
        {
            try
            {
                foreach (Control c in Control.Controls)
                {
                    if (c.Name == "HeaderPanel")
                    {
                        c.Font = new Font(pfc.Families[1], c.Font.Size);
                    }
                    else
                    {
                        c.Font = new Font(pfc.Families[0], c.Font.Size);

                        if (c.HasChildren)
                        {
                            setStyle(c);
                        }
                    }


                }
            }
            catch (Exception)
            {
            }
        }

        async void loadtimer()
        {
            var datetime = new DateTime(2024, 9, 6, 6, 0, 0) - DateTime.Now;

            timerLabel.Text = $"{datetime.Days} days {datetime.Days} hours and {datetime.Minutes} minutes until the race starts!";

            await Task.Delay(1000);

            loadtimer();
        }
    }
}
