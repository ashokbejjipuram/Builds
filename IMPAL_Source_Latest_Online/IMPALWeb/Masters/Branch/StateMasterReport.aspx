<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StateMasterReport.aspx.cs"
    Inherits="IMPALWeb.Masters.Branch.StateMasterReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Branch/StateMaster.aspx" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            State</div>
        <asp:UpdatePanel ID="UpdPnlStateReport" runat="server">
            <ContentTemplate>
             <div class="reportButtons">
                    <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crBRState" runat="server" ReportName="BRState" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crBRState" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>