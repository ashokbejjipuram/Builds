<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TransactionTypeReport.aspx.cs" Inherits="IMPALWeb.Masters.Others.TransactionTypeReport" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Others/TransactionType.aspx" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Transaction Type Master </div>
        <asp:UpdatePanel ID="updPnlTransactionType" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crTransactionType" runat="server" ReportName="TransactionType" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crTransactionType" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

