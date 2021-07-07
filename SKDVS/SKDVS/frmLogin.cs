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
using DataAccessLayer;

using SKDVS.Utils.ErrorHandler;

namespace SKDVS
{
    public partial class frmLogin : Form
    {
        private IDataLayer _dal;
        public string strUserName { get; set; }
        public string strPassword { get; set; }
        public string strDatabase { get; set; }
        public frmLogin()
        {
            InitializeComponent();
            cmbDSN.DataSource = OdbcInit();
#if DEBUG
            txtUser.Text = "yrenders";
            txtPassword.Text = "kkkkkkkk";
#endif
        }

        public List<ODBCDSN> OdbcInit()
        {
            ODBCDSN[] listDsns = ODBCManager.GetSystemDSNList();
            List<ODBCDSN> listDvsdsn = new List<ODBCDSN>();
            foreach (ODBCDSN dsn in listDsns)
            {
                string strName = dsn.GetDSNName().Substring(0, 3).ToUpper();
                if (strName == "DVS")
                {
                    listDvsdsn.Add(dsn);
                }
            }
            return listDvsdsn;
            //cmbDSN.DataSource = listDvsdsn;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string strConn = "Data Source=" + this.cmbDSN.Text + ";UID=" + this.txtUser.Text + ";PWD=" + this.txtPassword.Text;
                strUserName = txtUser.Text; strPassword = txtPassword.Text; strDatabase = cmbDSN.Text;
                _dal = DataLayer.GetInstance(DatabaseTypes.SybaseAnywhere, strConn);
                _dal.Open();
                if (_dal.IsError)
                    throw new DataLayerException();
                else
                {
                    this.DialogResult = DialogResult.OK;
                    _dal.Close();
                    this.Close();
                }
            }
            catch (DataLayerException dle)
            {
                int code = dle.HResult;             
                HandledExceptionManager.ShowDialog(_dal.LastError, "You cannot connect ", "Try again ! ", dle);
                

            }
            catch (Exception)
            {
                MessageBox.Show(" Error in Login ");
                //throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();

        }
    }

}
