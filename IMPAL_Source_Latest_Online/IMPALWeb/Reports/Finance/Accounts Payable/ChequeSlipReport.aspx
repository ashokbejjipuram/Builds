<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ChequeSlipReport.aspx.cs" 
Inherits="IMPALWeb.Reports.Finance.Accounts_Payable.ChequeSlipReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
           Cheque Slip Report
        </div>
        <asp:UpdatePanel ID="updPnlChequeSlip" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" 
                        onclick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crChequeSlip" runat="server" ReportName="ChequeSlipReport_COR" OnUnload="crChequeSlip_Unload" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crChequeSlip" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>