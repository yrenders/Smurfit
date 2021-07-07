using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKDVS
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmDllUpdate frmUpdte = new frmDllUpdate();
            frmUpdte.Show();
            frmUpdte.CheckUpdates();
            frmUpdte.Close();
            string strLogonType = Globals.GetLogontype();


            Application.Run(new Form1());
        }
    }
}
