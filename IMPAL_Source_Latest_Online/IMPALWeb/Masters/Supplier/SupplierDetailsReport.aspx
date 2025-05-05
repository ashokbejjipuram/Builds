<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SupplierDetailsReport.aspx.cs" Inherits="IMPALWeb.Masters.Supplier.SupplierDetailsReport" %>
<%@ Register src="~/UserControls/CrystalReport.ascx" tagname="CrystalReport" tagprefix="UC" %>
<%@ PreviousPageType VirtualPath="~/Masters/Supplier/SupplierDetails.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
<div id="DivOuter">
        <div class="subFormTitle">
            Supplier Details Report
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" 
                        onclick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crSupplierDetails" runat="server" ReportName="Impal_Supplier" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crSupplierDetails" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
