<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ItemLocationReport.aspx.cs"
    Inherits="IMPALWeb.Masters.Item.ItemLocationReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Item/ItemLocation.aspx" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            ITEM LOCATION</div>
        <asp:UpdatePanel ID="UpdPnlSLBReport" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crItemLocation" runat="server" ReportName="ItemLocation" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crItemLocation" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
