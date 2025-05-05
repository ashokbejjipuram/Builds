#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using System.Web.Security;
using System.Configuration;

#endregion Namespace

namespace IMPALWeb.Errors
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        #region Private Variables
        private const string queryStringStatusCode = "StatusCode";

        private string paramStatusCode;

        #endregion Private Variables
        
        #region Public Variables

        #endregion Public Variables

        #region Events
        /// <summary>
        /// Page Load Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //To get the value from Query String StatusCode
            paramStatusCode = Request.QueryString[queryStringStatusCode];

            //To display the correct message based on the Status Code
            switch (paramStatusCode)
            {
                case "0":
                    LoadErrorMessage();
                    break;
                case "404":
                    LoadPageNotFoundMessages();
                    break;
                default:
                    LoadErrorMessage();
                    break;
            }
        }

        #endregion Events

        #region Userdefined Methods
        /// <summary>
        /// Load error message when an unexpected exception is thrown
        /// </summary>
        private void LoadErrorMessage()
        {
            lblErrorContentDisplay.Text = "Multiple Users are Dealing with the Same Screen. Please Re-try in a While";
            lblErrorContentDisplaySubText.Text = "System has crashed due to an unexpected error.";
            lblErrorRight.Text = "Contact Info!";
            lblErrorRightSubText.Text = "Please contact System Administrator.";
        }

        /// <summary>
        /// Load page not found message when the accessed page is not available.
        /// </summary>
        private void LoadPageNotFoundMessages()
        {
            lblErrorContentDisplay.Text = "OOPs! Page Not Found";
            lblErrorContentDisplaySubText.Text = "The Page you are looking for is not available.";
            lblErrorRight.Text = "Contact Info!";
            lblErrorRightSubText.Text = "Please contact System Administrator.";
        }

        #endregion Userdefined Methods
    }
}
