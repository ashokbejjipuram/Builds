<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Class_OS_MonthWise_AgePercentageWise.aspx.cs" Inherits="IMPALWeb.Dashboard.FinanceDashboard.Class_OS_MonthWise_AgePercentageWise" Title="Untitled Page" %>
<%@ Register Src="~/UserControls/SSRSReport.ascx" TagName="SSRSReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle reportFormTitleExtender350">
        Age/% Wise-Classification of Outstanding MonthWise
    </div>
   <div>
      <table id="dashReportTable" class="dashReportTable" runat="server" 
            style="width:581px; height: 75px;" visible="False">           
        </table>       
    </div>
    <div class="dashReportHolder" id="dashReportHolder" runat="server">
        <UC:SSRSReport ID="DashClassOSMonthwise" runat="server" />
    </div>
</asp:Content>
