using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKDVS.Utils.ODBCMngr;
namespace OdbcDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SKDVS.Utils.ODBCMngr.ODBCDSN[] listDsns = SKDVS.Utils.ODBCMngr.ODBCManager.GetSystemDSNList();
            List <ODBCDSN> listDvsdsn =new List<ODBCDSN>();
            foreach (ODBCDSN dsn in listDsns)
            {
                string strName=dsn.GetDSNName().Substring(0,3).ToUpper();
                if (strName == "DVS") 
                {
                    listDvsdsn.Add(dsn); 
                }
            }
            cmbDSN.DataSource = listDvsdsn;
            //listDsns.Select
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Environment.Is64BitProcess == true)
            {
                label1.Text = "Running 64 bits";
            }
            else
            {
                label1.Text = "Running 32 bits";
            }
        }
    }
}
