<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BranchProductAndSalesTaxReport.aspx.cs" Inherits="IMPALWeb.Masters.SalesTax.BranchProductAndSalesTaxReport" %>
<%@ Register src="~/UserControls/CrystalReport.ascx" tagname="CrystalReport" tagprefix="UC" %>
<%@ PreviousPageType VirtualPath="~/Masters/SalesTax/BranchProductAndSalesTax.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle subFormTitleExtender300">
            Branch Product & Sales Tax Report
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <uc:crystalreport id="crBranchProductSalesTax" runat="server" reportname="Impal_Salestax_Branch_Product" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crBranchProductSalesTax" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
