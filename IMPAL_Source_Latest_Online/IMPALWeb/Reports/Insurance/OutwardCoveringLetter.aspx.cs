#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using IMPALLibrary;
using System.Web.UI.WebControls;
#endregion

namespace IMPALWeb.Reports.Insurance
{
    public partial class OutwardCoveringLetter : System.Web.UI.Page
    {
        #region Page Init
        /// <summary>
        /// Page Init event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Init", "Entering Page_Init");
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion

        #region Page Load
        /// <summary>
        /// Page load event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Log.WriteLog(source, "Page_Load", "Entering Page_Load");
            try
            {
            if (!IsPostBack)
            {
                crOutwardCoveringLetter.ReportName = "OutwardCoveringLetter";
                crOutwardCoveringLetter.RecordSelectionFormula = "";
                crOutwardCoveringLetter.GenerateReport();
            }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
        #endregion
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (crOutwardCoveringLetter != null)
            {
                crOutwardCoveringLetter.Dispose();
                crOutwardCoveringLetter = null;
            }
        }
        protected void crOutwardCoveringLetter_Unload(object sender, EventArgs e)
        {
            if (crOutwardCoveringLetter != null)
            {
                crOutwardCoveringLetter.Dispose();
                crOutwardCoveringLetter = null;
            }
        }
    }
}
