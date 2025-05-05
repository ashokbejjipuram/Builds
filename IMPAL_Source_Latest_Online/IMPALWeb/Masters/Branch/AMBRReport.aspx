<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AMBRReport.aspx.cs"
    Inherits="IMPALWeb.Masters.Branch.AMBRReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Branch/AreaManagerBranch.aspx" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Area Manager & Branch
        </div>
                <asp:UpdatePanel ID="UpdPnlAMBRReport" runat="server">
                    <ContentTemplate>
                        <div class="reportButtons">
                    <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
                </div>
                        <table>
                            <tr>
                                <td>
                                    <UC:CrystalReport ID="crBRAreaManagerBranch" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="crBRAreaManagerBranch" />
                    </Triggers>
                </asp:UpdatePanel>
           </div>
</asp:Content>
