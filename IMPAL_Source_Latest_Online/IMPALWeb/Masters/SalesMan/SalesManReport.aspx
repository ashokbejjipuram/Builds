<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesManReport.aspx.cs" Inherits="IMPALWeb.Masters.SalesMan.SalesManReport" %>
<%@ Register src="~/UserControls/CrystalReport.ascx" tagname="CrystalReport" tagprefix="UC" %>
<%@ PreviousPageType VirtualPath="~/Masters/SalesMan/SalesMan.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            SalesMan Report
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crSalesMan" runat="server" ReportName="Impal_Sales_Man" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crSalesMan" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
