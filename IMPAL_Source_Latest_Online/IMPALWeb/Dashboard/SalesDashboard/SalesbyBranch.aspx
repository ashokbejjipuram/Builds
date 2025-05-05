<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesbyBranch.aspx.cs" Inherits="IMPALWeb.Dashboard.SalesDashboard.SalesbyBranch" Title="Sales by Branch" %>
 <%@ Register Src="~/UserControls/SSRSReport.ascx"TagName="SSRSReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
 <UC:SSRSReport ID="ssrsSalesbyBranch" runat="server" ReportName = "SalesbyBranch" style="height:800px;width:90%;";/>
</asp:Content>

