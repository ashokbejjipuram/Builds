<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AMReport.aspx.cs"
    Inherits="IMPALWeb.Masters.Branch.BRAMReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Branch/AreaManager.aspx" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Area Manager
        </div>
        <asp:UpdatePanel ID="UpdPnlAreaManagerReport" runat="server">
            <ContentTemplate>
                 <div class="reportButtons">
                    <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crBRAreaManager" runat="server" ReportName="BRAreaManager" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crBRAreaManager" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
