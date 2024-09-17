using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session3.UserControls
{
    public partial class ItemPriceUserControl : UserControl
    {
        private long id;

        public long ID
        {
            get { return id; }
            set { id = value; }
        }

        public ItemPriceUserControl()
        {
            InitializeComponent();
        }
    }
}
