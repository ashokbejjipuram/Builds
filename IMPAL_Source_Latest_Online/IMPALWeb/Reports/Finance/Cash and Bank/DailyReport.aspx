<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DailyReport.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Cash_and_Bank.DailyReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        Daily Report</div>
    <div class="reportFilters">
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                TabIndex="1" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder">
        <uc1:CrystalReport runat="server" ID="crDailyReport" OnUnload="crDailyReport_Unload" ReportName="DailyReport" />
    </div>
</asp:Content>
