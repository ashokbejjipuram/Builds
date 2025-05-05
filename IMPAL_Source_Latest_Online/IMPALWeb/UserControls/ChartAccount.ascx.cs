using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;

namespace IMPALWeb.UserControls
{
    public partial class ChartAccount : System.Web.UI.UserControl
    {
        private string m_Filter = string.Empty;
        private string m_DefaultBranch = string.Empty;
        public event EventHandler SearchImageClicked;

        public string Filter
        {
            get
            {
                if (ViewState["Filter"] != null)
                { m_Filter = Convert.ToString(ViewState["Filter"]); };
                return m_Filter;
            }
            set { ViewState["Filter"] = value; }
        }

        public string DefaultBranch
        {
            get
            {
                if (ViewState["DefaultBranch"] != null)
                { m_DefaultBranch = Convert.ToString(ViewState["DefaultBranch"]); };
                return m_DefaultBranch;
            }
            set { ViewState["DefaultBranch"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            AshxPath.Value = Page.ResolveClientUrl("~/HandlerFile");
            hddFilter.Value = GetFiltervalue(Filter);
            hddDefaultBranch.Value = DefaultBranch;
            hddBranch.Value = Session["BranchCode"].ToString();
        }

        private string GetFiltervalue(string Filter)
        {
            string _filter = string.Empty;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!string.IsNullOrEmpty(Filter))
                {
                    string[] column = Filter.Split(',');
                    foreach (string col in column)
                    {
                        _filter += "'" + col + "',";
                    }
                    _filter = _filter.Substring(0, _filter.Length - 1);
                    _filter = "(" + _filter + ")";
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return _filter;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Session["ChatAccCode"] = hddChartAccount.Value;
            Session["ChatDescription"] = hddDse.Value;
            MPESupplier.Hide();
            if (SearchImageClicked != null)
                SearchImageClicked(this, EventArgs.Empty);
        }
    }
}