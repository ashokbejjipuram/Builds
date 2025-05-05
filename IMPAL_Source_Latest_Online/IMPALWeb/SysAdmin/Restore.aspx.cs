using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IMPALLibrary.Masters.Sales;
using IMPALLibrary;
using IMPALWeb.UserControls;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace IMPALWeb.SysAdmin
{
    public partial class Restore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = null;
                ImpalDB = DataAccess.GetDatabase();
                string ConnectionString = ImpalDB.ConnectionString;
                string _fpath = Server.MapPath("~/Backup/");
                string[] strConn = ConnectionString.Split(';');
                //string _fpath = "\\\\20.201.110.166\\Impal_New\\Backup\\";

                bool isDirExits = System.IO.Directory.Exists(_fpath);
                string FldmthyrSubFolder = string.Empty;
                string strFileName = string.Empty;
                IMPALLibrary.Backup _backup = new IMPALLibrary.Backup();
                int IsResult = 0;

                DateTime dtRestore = DateTime.ParseExact(txtRestoreDate.Text, "dd/MM/yyyy", null);

                if (isDirExits)
                    FldmthyrSubFolder = dtRestore.Year.ToString() + dtRestore.Month.ToString();

                bool isExits = System.IO.Directory.Exists(_fpath + FldmthyrSubFolder);

                if (isExits)
                    strFileName = _fpath + FldmthyrSubFolder + "\\" + dtRestore.Year.ToString() + dtRestore.Month.ToString() + dtRestore.Day.ToString() + ".dat";


                bool isFileExits = System.IO.File.Exists(strFileName);
                if (isFileExits)
                    IsResult = _backup.RestoreFile(strFileName, strConn[1].Replace("Initial Catalog=", ""));

                if (IsResult == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Restored successfully');", true);
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

        }

    }
}
