<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BMSPaymentReport.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Accounts_Payable.BMSPaymentReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="ContentBMSPaymentReport" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        BMS Payment - Report</div>
    <div class="reportFilters" runat="server" id="divSelection">
        <div class="reportButtons" runat="server" id="divButton" style="float: left">
            <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="ButtonViewReport"
                TabIndex="3" OnClick="btnBack_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crBMSPaymentReport" runat="server" OnUnload="crBMSPaymentReport_Unload" />
    </div>
</asp:Content>