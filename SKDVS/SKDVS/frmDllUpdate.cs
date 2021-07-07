using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace SKDVS
{
    public partial class frmDllUpdate : Form
    {
        public frmDllUpdate()
        {
            InitializeComponent();
        }


        public void CheckUpdates()
        { 
             bool blnDllExist;
             this.Refresh();
            //Key ajouter manuellement dans App.config
            NameValueCollection section = new NameValueCollection();
            section = ConfigurationManager.GetSection("Application") as NameValueCollection;
             string strDriveApp = section["Drive"];
             string strPathApp = section["Path"];
            section = ConfigurationManager.GetSection("Update") as NameValueCollection;
            string strAppDepository = section["Drive"] + section["Path"];

            //Project -> properties -> Settings -> values
            string keyvalue1 = Properties.Settings.Default.UpdateDrive;
            string keyvalue2 = Properties.Settings.Default.LogonType;

            lblUpdate.Text = "Checking Updates";
            this.Refresh();
             DirectoryInfo di = new DirectoryInfo(strAppDepository);
             //curdir ?
             FileInfo[] files = di.GetFiles("*.dll");
                //list the names of all files in the specified directory
             foreach (FileInfo fileInfo in files)
             {
                 blnDllExist = File.Exists(strDriveApp + strPathApp + fileInfo.Name);
                 //check dll exist fileInfo.FullName fileInfo.LastWriteTimeUtc
                 if (!blnDllExist)
                 {
                     lblUpdate.Text = " Copying ... " + fileInfo.Name;
                     File.Copy(fileInfo.FullName, strDriveApp + strPathApp + fileInfo.Name);
                 }
                 else
                 {
                     DateTime dtlocFile = File.GetLastWriteTime(strDriveApp + strPathApp + fileInfo.Name);
                     TimeSpan tspdiff = (dtlocFile - fileInfo.LastWriteTime); //utc ?
                     if (tspdiff.TotalSeconds > 5)
                     {
                         lblUpdate.Text = " Copying ... " + fileInfo.Name;
                         File.Copy(fileInfo.FullName, strDriveApp + strPathApp + fileInfo.Name); 
                     }
                 }
                 System.Threading.Thread.Sleep(1000);
                 this.Close();
        }

        }
        private void frmDllUpdate_Load(object sender, EventArgs e)
        {
            
        }
    }
}
