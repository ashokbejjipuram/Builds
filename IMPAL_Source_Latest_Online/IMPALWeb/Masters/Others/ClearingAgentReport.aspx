<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClearingAgentReport.aspx.cs" Inherits="IMPALWeb.Masters.Others.ClearingAgentReport" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Others/ClearingAgent.aspx" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Clearing Agent Master </div>
        <asp:UpdatePanel ID="updPnlClearingAgent" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crClearingAgent" runat="server" ReportName="ClearingAgent" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crClearingAgent" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>