<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BranchSalesTaxFormsReport.aspx.cs" Inherits="IMPALWeb.Masters.SalesTax.BranchSalesTaxFormsReport" %>
<%@ Register src="~/UserControls/CrystalReport.ascx" tagname="CrystalReport" tagprefix="UC" %>
<%@ PreviousPageType VirtualPath="~/Masters/SalesTax/BranchSalesTaxForms.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
 <div id="DivOuter">
        <div class="subFormTitle">
            Branch Sales Tax Forms Report
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <uc:crystalreport id="crBranchSalesTaxFormsReport" runat="server" reportname="Impal_branchSTform" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crBranchSalesTaxFormsReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
