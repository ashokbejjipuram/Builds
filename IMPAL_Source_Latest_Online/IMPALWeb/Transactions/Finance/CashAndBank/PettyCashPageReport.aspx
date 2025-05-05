<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PettyCashPageReport.aspx.cs" Inherits="IMPALWeb.Finance.PettyCashPageReport" %>
<%@ Register src="~/UserControls/CrystalReport.ascx" tagname="CrystalReport" tagprefix="UC" %>
<%@ PreviousPageType VirtualPath="~/Transactions/Finance/CashAndBank/PettyCash.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
<div id="DivOuter">
        <div class="subFormTitle">
       Petty Cash
        </div>
        <asp:UpdatePanel ID="updPnlPettyCash" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crPettyCashPageReport" runat="server" ReportName="PettyCashPageReport" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crPettyCashPageReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
