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
    public partial class Backup : System.Web.UI.Page
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

                //string _fpath = Server.MapPath("~/Backup/");
                Database ImpalDB = null;
                ImpalDB = DataAccess.GetDatabase();
                string ConnectionString = ImpalDB.ConnectionString;
                string[] strConn = ConnectionString.Split(';');

                //string _mDir = "\\\\" + strConn[0].Replace("Data Source=","") + "\\";
                //bool ismDirExits = System.IO.Directory.Exists(_mDir);
                //if (!ismDirExits)
                //  System.IO.Directory.CreateDirectory(_mDir);

                //string _fpath = "\\\\20.201.110.166\\Impal_New\\Backup\\";
                //string _fpath = "\\\\" + strConn[0].Replace("Data Source=","") + "\\Backup\\";
                string _fpath = Server.MapPath("\\Backup\\");

                bool isDirExits = System.IO.Directory.Exists(_fpath);

                if (!isDirExits)
                    System.IO.Directory.CreateDirectory(_fpath);

                string FldmthyrSubFolder = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString();

                bool isExits = System.IO.Directory.Exists(_fpath + FldmthyrSubFolder);

                if (!isExits)
                    System.IO.Directory.CreateDirectory(_fpath + FldmthyrSubFolder);

                string DateForm = "";
                string MonthForm = "";

                if (System.DateTime.Now.Day < 10)
                    DateForm = "0" + System.DateTime.Now.Day.ToString();
                else
                    DateForm = System.DateTime.Now.Day.ToString();

                if (System.DateTime.Now.Month < 10)
                    MonthForm = "0" + System.DateTime.Now.Month.ToString();
                else
                    MonthForm = System.DateTime.Now.Month.ToString();

                //Create/Open File in Write mode
                string strFileName = _fpath + FldmthyrSubFolder + "\\" + System.DateTime.Now.Year.ToString() + MonthForm + DateForm + Session["BranchCode"] + ".dat";

                bool isFileExists = System.IO.File.Exists(strFileName);

                if (isFileExists)
                    System.IO.File.Delete(strFileName);

                IMPALLibrary.Backup _backup = new IMPALLibrary.Backup();
                int IsResult = _backup.BackupFile(strFileName, strConn[1].Replace("Initial Catalog=", ""));

                if (IsResult == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "IMPAL", "alert('Backup created successfully');", true);
                }

                //Response.Write "<BR> <STRONG>DONE</STRONG>"
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

    }
}
