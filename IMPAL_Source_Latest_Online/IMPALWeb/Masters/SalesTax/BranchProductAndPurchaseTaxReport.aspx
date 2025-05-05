<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BranchProductAndPurchaseTaxReport.aspx.cs" Inherits="IMPALWeb.Masters.SalesTax.BranchProductAndPurchaseTaxReport" %>
<%@ Register src="~/UserControls/CrystalReport.ascx" tagname="CrystalReport" tagprefix="UC" %>
<%@ PreviousPageType VirtualPath="~/Masters/SalesTax/BranchProductAndPurchaseTax.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitleExtender350 subFormTitle" >
        Branch Product & Purchase Tax Report
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crBranchProductPurchaseTax" runat="server" ReportName="Impal_Salestax_Branch_Product" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crBranchProductPurchaseTax" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
