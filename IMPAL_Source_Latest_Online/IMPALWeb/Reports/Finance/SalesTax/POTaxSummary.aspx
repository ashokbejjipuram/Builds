<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POTaxSummary.aspx.cs" Inherits="IMPALWeb.Reports.Finance.SalesTax.POTaxSummary"
    MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        PO Tax Summary
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Generate Report"
                SkinID="ButtonViewReport" TabIndex="1" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc:CrystalReport ID="rptCrystal" runat="server" OnUnload="rptCrystal_Unload" ReportName="POTaxSummary" />
    </div>
</asp:Content>
