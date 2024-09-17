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
    public partial class AutoSystemForm : Session1.Form1
    {
        public WSC2017_Session1Entities ent { get; set; }
       
        public int id { get; set; }

        public int logId { get; set; }

        public AutoSystemForm(int iD, int logId)
        {
            this.logId = logId;
            this.id = iD;
            ent = new WSC2017_Session1Entities();
            InitializeComponent();

            loadData();
        }

        void loadData()
        {
            var usr = ent.Users.FirstOrDefault(x => x.ID == this.id);

            var logs = ent.LoginLogs.ToList().Where(x => x.user_id == this.id && x.id != this.logId).Select(x => new
            {
                date = ((DateTime)x.datetime).ToString("dd/MM/yyyy"),
                login_time = ((DateTime)x.datetime).ToString("hh:mm"),
                logout_time = x.datetime_logout == null ? "--" : ((DateTime)x.datetime_logout).ToString("mm:ss"),
                time_spent = x.datetime_logout == null ? "--" : new Func<string>(() =>
                {
                    var time = ((DateTime)x.datetime_logout - (DateTime)x.datetime).Seconds;
                    var m = time / 60;
                    var s = time % 60;
                    return $"{m}:{s}";
                })(),
                time_spent_value = x.datetime_logout == null ? 0 : ((DateTime)x.datetime_logout - (DateTime)x.datetime).Seconds,
                x.reason,
                isCrashed = x.datetime_logout == null
            }).ToList();

            label2.Text = $"hi, {usr.FirstName}. Weclome to AMONIC Ariline.";

            var total = logs.Sum(x => x.time_spent_value);

            var minutes = total / 60;
            var seconds = total % 60;

            var hours = 0;

            if (minutes > 60)
            {
                hours = minutes / 60;

                minutes = minutes % 60;
            }

            label3.Text = $"Time spend on system: {hours}:{minutes}:{seconds}";
            label4.Text = $"Number of crashes: {logs.Count(x => x.isCrashed == true)}";

            for (int i = 0; i < logs.Count(); i++)
            {
                dataGridView1.Rows.Add(logs[i].date, logs[i].login_time, logs[i].logout_time, logs[i].time_spent, logs[i].reason);

                if (logs[i].isCrashed)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            var log = ent.LoginLogs.Where(x => x.id == this.logId).First();

            log.datetime_logout = DateTime.Now;

            ent.SaveChanges();

            this.Hide();

            new LoginForm().ShowDialog();
        }
    }
}
