<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesbySegment.aspx.cs" Inherits="IMPALWeb.Dashboard.SalesDashboard.SalesbySegment" Title="Sales by Segment" %>

 <%@ Register Src="~/UserControls/SSRSReport.ascx"TagName="SSRSReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
 <UC:SSRSReport ID="ssrsSalesbySegment" runat="server" ReportName = "SalesbySegment" />
</asp:Content>
