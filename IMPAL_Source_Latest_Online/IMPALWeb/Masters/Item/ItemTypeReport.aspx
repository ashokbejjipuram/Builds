<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ItemTypeReport.aspx.cs"
    Inherits="IMPALWeb.Masters.Item.ItemTypeReport" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Item/ItemType.aspx" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Item Type
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crItemType" runat="server" ReportName="ItemType" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crItemType" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
