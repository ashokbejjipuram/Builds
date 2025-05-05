<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="AllBranchStockDetails.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Query.AllBranchStockDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="SupplierPartNumberSearch" Src="~/UserControls/SupplierPartNumberSearch.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="../../../App_Themes/Styles/GridStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pageLoad(sender, args) {
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
            gridViewFixedHeader('<%=grdConsignmentDeails.ClientID%>', '1000', '400');
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnExport1" />
        </Triggers>
        <ContentTemplate>
            <div class="subFormTitle subFormTitleExtender250">
                ALL BRANCH STOCK DETAILS
            </div>
            <uc1:SupplierPartNumberSearch ID="ucSupplierPartNumber" OnSearchImageClicked="ucSupplierPartNumber_SearchImageClicked"
                OnSupplierddlChanged="ucSupplierPartNumber_SupplierddlChanged" runat="server"></uc1:SupplierPartNumberSearch>
            <table class="subFormTable">
                <tr>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblBranchName" SkinID="LabelNormal" runat="server" Text="Branch Name"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:DropDownList ID="ddlBranchName" SkinID="DropDownListDisabledBig" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" />
                    </td>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblBalanceQuantity" SkinID="LabelNormal" runat="server" Text="Balance Quantity"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtBalanceQuantity" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblVehicleApplication" SkinID="LabelNormal" runat="server" Text="Vehicle Application"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtVehicleApplication" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblShortDescription" SkinID="LabelNormal" runat="server" Text="Short Description"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtShortDescription" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblProductDescription" SkinID="LabelNormal" runat="server" Text="Product Description"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtProductDescription" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblSegment" SkinID="LabelNormal" runat="server" Text="Application Segment"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtApplicationSegment" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelColSpan2" colspan="3" style="text-align: center; font-size: 22px">
                        <asp:Label ID="lblNoRecord" SkinID="Error" ForeColor="Red" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="transactionButtons">
                <div class="transactionButtonsHolder">
                    <asp:Button ID="btnReset" runat="server" SkinID="ButtonNormalBig" CausesValidation="false" OnClick="btnReset_Click" Text="Reset" />
                    <asp:Button ID="btnExport" SkinID="ButtonNormalBig" Text="Export Report" runat="server" OnClick="btnExport_Click" />
                </div>
            </div>
            <div id="divItemDetails" runat="server">
                <div class="subFormTitle subFormTitleExtender250">
                    BRANCH WISE STOCK DETAILS
                </div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="grdConsignmentDeails" runat="server" Width="100%" PageSize="300" SkinID="GridViewScroll" AutoGenerateColumns="true" ShowFooter="true"
                        AllowPaging="true" CssClass="Grid" PagerStyle-CssClass="pgr" OnPageIndexChanging="grdConsignmentDeails_PageIndexChanging">
                        <HeaderStyle BackColor="#003399" ForeColor="White"></HeaderStyle>
                    </asp:GridView>
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnReset1" runat="server" SkinID="ButtonNormalBig" CausesValidation="false" OnClick="btnReset_Click" Text="Reset" />
                        <asp:Button ID="btnExport1" SkinID="ButtonNormalBig" Text="Export Report" runat="server" OnClick="btnExport1_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
