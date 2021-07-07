using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

using System.Data;
using DataAccessLayer;

namespace SKDVS
{
    static class Globals
    {
        public static string strCurUsr;
        public static string strConnString;

        public static string HelloWorld()
        {
            return "Hello World";
        }

        public static string GetLogontype()
        {
            string strKey = Properties.Settings.Default.LogonType;
            return strKey;
        }
        
        public static string RetrieveUser(IDataLayer dal)
        {
            
            dal.Sql = "select user_name()";
            DataTable dt = null;
            dt = dal.ExecuteDataTable();
            foreach (DataRow row in dt.Rows)
            {
                strCurUsr = row["user_name()"].ToString();
            }
            return strCurUsr;
            //close dt
        }
    }
}
//https://www.arclab.com/en/kb/csharp/global-variables-fields-functions-static-class.html
