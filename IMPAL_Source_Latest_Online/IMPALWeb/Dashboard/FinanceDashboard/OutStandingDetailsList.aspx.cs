using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using IMPALLibrary;

namespace IMPALWeb.Dashboard.FinanceDashboard
{
    public partial class OutStandingDetailsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlReportType.SelectedIndex = 0;
                LoadZone();
                ddlZone.Enabled = true;
                LoadState(Convert.ToInt32(ddlZone.SelectedValue));
                ddlState.Enabled = false;
                
            }
        }

        private void LoadState(int zoneID)
        {
            try
            {
                CommonDashboard obj = new CommonDashboard();
                ddlState.DataSource = obj.GetZoneBasedStates(Convert.ToInt32(zoneID));
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateCode";
                ddlState.DataBind();
                ddlState.SelectedIndex = 0;
                LoadBranch(Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlState.SelectedValue));
                ddlBranch.Enabled = false;
            }
            catch 
            {
                
                throw;
            }
        }

        private void LoadBranch(int zoneID ,int stateID)
        {
            CommonDashboard obj = new CommonDashboard();
            ddlBranch.DataSource = obj.GetStateBasedBranch(zoneID,stateID);
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataBind();
            ddlBranch.SelectedIndex = 0;  
        }

        private void LoadZone()
        {
            try
            {
                CommonDashboard obj = new CommonDashboard();
                ddlZone.DataTextField = "ZoneName";
                ddlZone.DataValueField = "ZoneCode";
                ddlZone.DataSource = obj.GetAllZones();
                ddlZone.DataBind();
                ddlZone.SelectedIndex = 0;
                
                ddlState.Enabled = false;
            }
            catch
            {
                
                throw;
            }
        }
       

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (ddlZone.SelectedValue == "-1")
                {
                    LoadState(Convert.ToInt32(ddlZone.SelectedValue));
                    ddlState.Enabled = false;
                }
                else
                {
                    LoadState(Convert.ToInt32(ddlZone.SelectedValue));  
                    ddlState.Enabled = true;
                }
            }
            catch 
            {
                
                throw;
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlState.SelectedValue == "-1")
                {
                    LoadBranch(Convert.ToInt32(ddlZone.SelectedValue),Convert.ToInt32(ddlState.SelectedValue));
                    ddlBranch.Enabled = false;
                }
                else
                {
                    LoadBranch(Convert.ToInt32(ddlZone.SelectedValue),Convert.ToInt32(ddlState.SelectedValue));
                    ddlBranch.Enabled = true;
                }
            }
            catch
            {

                throw;
            }
        }

        protected void btnReport1_Click(object sender, EventArgs e)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            CommonDashboard obj = new CommonDashboard();
            try
            {
                Main mainmaster = (Main)Page.Master;
                mainmaster.ShowHideFilters(btnReport1, dashReportTable, dashReportHolder);
                List<ReportParameter> lstReportParameter = new List<ReportParameter>();
                ReportParameter p1 = new ReportParameter("ZONECODE", ddlZone.SelectedValue.ToString());
                lstReportParameter.Add(p1);
                ReportParameter p2 = new ReportParameter("STATECODE", ddlState.SelectedValue.ToString());
                lstReportParameter.Add(p2);
                ReportParameter p3 = new ReportParameter("BRANCHCODE", ddlBranch.SelectedValue.ToString());
                lstReportParameter.Add(p3);
                if (btnReport1.Text == "Back")
                {
                    switch (ddlReportType.SelectedValue.Trim())
                    {
                        case "Above 90":                           
                                dashOutStandingDetailsList.lstReportParameter = lstReportParameter;
                                dashOutStandingDetailsList.ReportName = "OutStandingReport90Above";
                                dashOutStandingDetailsList.ReportPath = System.Configuration.ConfigurationManager.AppSettings["OutStandingReport90Above"].ToString();
                                dashOutStandingDetailsList.GenerateReport();
                                break;
                        case "Above 180":                                
                                dashOutStandingDetailsList.lstReportParameter = lstReportParameter;
                                dashOutStandingDetailsList.ReportName = "OutStandingReportBy180";
                                dashOutStandingDetailsList.ReportPath = System.Configuration.ConfigurationManager.AppSettings["OutStandingReportBy180"].ToString();
                                dashOutStandingDetailsList.GenerateReport();
                                break;
                            

                        
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }
    }
}
