<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreditLimitMonitor.aspx.cs" Inherits="IMPALWeb.Dashboard.FinanceDashboard.CreditLimitMonitor"%>
<%@ Register Src="~/UserControls/SSRSReport.ascx" TagName="SSRSReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
<div class="reportFormTitle reportFormTitleExtender350">
        CREDIT LIMIT MONITORING 
    </div>
     <div>
        <table id="dashReportTable" class="dashReportTable" runat="server" 
            style="width:581px; height: 75px;">           
        </table>
       
    </div>
    <div class="dashReportHolder" id="dashReportHolder" runat="server">
        <UC:SSRSReport ID="DashCreLimitMonitor" runat="server" />
    </div>
</asp:Content>



