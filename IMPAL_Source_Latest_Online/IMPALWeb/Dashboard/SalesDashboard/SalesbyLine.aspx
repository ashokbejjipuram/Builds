<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesbyLine.aspx.cs" Inherits="IMPALWeb.Dashboard.SalesDashboard.SalesbyLine" Title="Sales By Line" %>
 <%@ Register Src="~/UserControls/SSRSReport.ascx"TagName="SSRSReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <UC:SSRSReport ID="ssrsSalesbyLine" runat="server" ReportName = "SalesbyLine" style="height:800px;width:90%;";/>
</asp:Content>
