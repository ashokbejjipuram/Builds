<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PartyTypeReport.aspx.cs" Inherits="IMPALWeb.Masters.Others.PartyTypeReport" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Others/PartyType.aspx" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Party Type Master </div>
        <asp:UpdatePanel ID="updPnlPartyType" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crPartyType" runat="server" ReportName="PartyType" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crPartyType" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

