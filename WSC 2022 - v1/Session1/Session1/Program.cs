using Session1.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Settings.Default.userTypeID == 1)
            {
                Application.Run(new EmployeeForm(Settings.Default.userID));
            }
            else if (Settings.Default.userID != 0)
            {
                Application.Run(new UserManagementForm(Settings.Default.userID));
            }
            else
            {
                Application.Run(new Form1());
            }
        }
    }
}
