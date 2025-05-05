<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BranchReport.aspx.cs" Inherits="IMPALWeb.Masters.Bank.BranchReport" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Bank/Branch.aspx" %>


<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Bank Branch
        </div>
        <asp:UpdatePanel ID="updPnlBranchReport" runat="server">
            <ContentTemplate>
             <div class="reportButtons">
                    <asp:Button class="reportButtons" SkinID="ButtonViewReport" ID="btnBack" runat="server"
                        Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crBranch" runat="server" ReportName="Branch" />
                        </td>
                    </tr>
                </table>
               
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crBranch" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

