<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CashDiscountReport.aspx.cs" Inherits="IMPALWeb.Masters.Others.CashDiscountReport" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Cash Discount Master
        </div>
        <asp:UpdatePanel ID="updPnlCashDiscountReport" runat="server">
            <ContentTemplate>
             <div class="reportButtons">
                    <asp:Button class="reportButtons" SkinID="ButtonViewReport" ID="btnBack" runat="server"
                        Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crCashDiscount" runat="server" ReportName="CashDiscountMaster" />
                        </td>
                    </tr>
                </table>
               
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crCashDiscount" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
