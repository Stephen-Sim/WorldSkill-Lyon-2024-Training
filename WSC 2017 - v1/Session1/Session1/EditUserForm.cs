using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Session1
{
    public partial class EditUserForm : Session1.Form1
    {
        public int login_id { get; set; }
        public int edit_id { get; set; }

        public WSC2017_Session1Entities ent { get; set; }

        public EditUserForm(int current_login_id, int edit_user_id)
        {
            ent = new WSC2017_Session1Entities();

            this.login_id = current_login_id;
            this.edit_id = edit_user_id;
            
            InitializeComponent();
        }
        
        void loaddata()
        {
            var usr = ent.Users.FirstOrDefault(x => x.ID == this.edit_id);

            label2.Text = usr.Email;
            label3.Text = usr.FirstName;
            label4.Text = usr.LastName ;
            label5.Text = usr.Office.Title;

            if (usr.RoleID == 2)
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

            new MainMenuAdminForm(this.login_id).ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var usr = ent.Users.FirstOrDefault(x => x.ID == this.edit_id);
            usr.RoleID = radioButton1.Checked ? 2 :1;
            ent.SaveChanges();

            this.Hide();

            new MainMenuAdminForm(this.login_id).ShowDialog();
        }
    }
}
