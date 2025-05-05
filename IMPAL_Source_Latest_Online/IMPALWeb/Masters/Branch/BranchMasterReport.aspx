<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BranchMasterReport.aspx.cs" Inherits="IMPALWeb.Masters.Branch.BranchMasterReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Branch/BranchMaster.aspx" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            BRANCH</div>
        <asp:UpdatePanel ID="UpdPnlBranchReport" runat="server">
            <ContentTemplate>
             <div class="reportButtons">
                    <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crBRMaster" runat="server" ReportName="BRMaster" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crBRMaster" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
