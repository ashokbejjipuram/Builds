#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Security;
using System.Web.Caching;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IMPALLibrary;

#endregion Namespace

namespace IMPALWeb.Ordering
{
    public partial class InsuranceClaim : System.Web.UI.Page
    {
        #region Private Variables

        #endregion Private Variables

        #region Public Variables

        #endregion Public Variables

        #region Events

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(InsuranceClaim), exp);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    InitializeControl();
                }
            }
            catch (Exception)
            {
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Reset Button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Cancel Button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// GridView On Row Data Bound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvInsuranceClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// GridView On Row Deleting event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvInsuranceClaim_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Claim Number Selected Index Changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlClaimNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Claim Number Query Button Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgButtonQuery_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }

        }

        #endregion Events

        #region Userdefined Methods

        private void InitializeControl()
        {
            try
            {
                //Controls visibility set to False
                ddlClaimNumber.Visible = false;
            }
            catch (Exception)
            {
            }
        }

        #endregion Userdefined Methods
    }
}
