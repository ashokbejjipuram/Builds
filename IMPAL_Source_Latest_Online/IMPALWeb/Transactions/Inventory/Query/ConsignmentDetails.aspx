<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ConsignmentDetails.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Query.ConsignmentDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="SupplierPartNumberSearch" Src="~/UserControls/SupplierPartNumberSearch.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
            gridViewFixedHeader('<%=grdConsignmentDeails.ClientID%>', '1000', '400');
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="subFormTitle">
                CONSIGNMENT DETAIL
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
                        <asp:Label ID="lblLongDescription" SkinID="LabelNormal" runat="server" Text="Vehicle Application"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtLongDescription" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblBalanceQuantity" SkinID="LabelNormal" runat="server" Text="Balance Quantity"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtBalanceQuantity" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblShortDescription" SkinID="LabelNormal" runat="server" Text="Short Description"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtShortDescription" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="labelColSpan2" colspan="3" style="text-align:center; font-size:22px">
                        <asp:Label ID="lblNoRecord" SkinID="Error" ForeColor="Red" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="transactionButtons">
                <div class="transactionButtonsHolder">
                    <asp:Button ID="btnReset" runat="server" SkinID="ButtonNormalBig" CausesValidation="false" OnClick="btnReset_Click"
                        Text="Reset" />
                </div>
            </div>
            <div id="divItemDetails" runat="server">
                <div class="subFormTitle">
                    ITEM DETAILS
                </div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="grdConsignmentDeails" runat="server" PageSize="50" SkinID="GridViewScroll"
                        AutoGenerateColumns="False" ShowFooter="true" AllowPaging="true" OnPageIndexChanging="grdConsignmentDeails_PageIndexChanging">

                        <Columns>
                            <asp:TemplateField HeaderText="GRN Number" FooterText="GRN Number">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGRN_Number" SkinID="GridViewTextBox" ReadOnly="true" Text='<%# Bind("GRN_Number") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GRN Date" FooterText="GRN Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGRN_Date" SkinID="GridViewTextBox" ReadOnly="true" Text='<%# Bind("GRN_Date") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="List Price" FooterText="List Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtList_Price" SkinID="GridViewTextBox" ReadOnly="true" Style="text-align: right"
                                        Text='<%#string.Format("{0:#.00}", System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"List_Price")),2)) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cost Price" FooterText="Cost Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCost_Price" SkinID="GridViewTextBox" Style="text-align: right"
                                        ReadOnly="true" Text='<%# string.Format("{0:#.00}",System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Cost_Price")),2)) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ED Ind" FooterText="ED Ind">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtED_Ind" SkinID="GridViewTextBoxSmall" Style="text-align: left" ReadOnly="true"
                                        Text='<%# Bind("ED_Ind") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ED Value " FooterText="ED Value">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtED_Value" SkinID="GridViewTextBox" Style="text-align: right"
                                        ReadOnly="true" Text='<%# string.Format("{0:#.00}",System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"ED_Value")),2)) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tr Type" FooterText="Tr Type">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTr_Type" SkinID="GridViewTextBoxSmall" Style="text-align: right" ReadOnly="true"
                                        Text='<%# Bind("Tr_Type") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OS/LS" FooterText="OS/LS">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOS_LS" SkinID="GridViewTextBoxSmall" Style="text-align: left" ReadOnly="true"
                                        Text='<%# Bind("OS_LS") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GRN Quantity" FooterText="GRN Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGRN_Quantity" SkinID="GridViewTextBoxSmall" Style="text-align: right"
                                        ReadOnly="true" Text='<%# Bind("GRN_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Billed Quantity" FooterText="Billed Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBilled_Quantity" SkinID="GridViewTextBoxSmall" Style="text-align: right"
                                        ReadOnly="true" Text='<%# Bind("Billed_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Number" FooterText="Invoice Number">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtInvoice_Number" SkinID="GridViewTextBox" Style="text-align: left"
                                        ReadOnly="true" Text='<%# Bind("Invoice_Number") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Date" FooterText="Invoice Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtInvoice_Date" SkinID="GridViewTextBox" Style="text-align: left"
                                        ReadOnly="true" Text='<%# Bind("Invoice_Date") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" FooterText="Location">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLocation" SkinID="GridViewTextBox" Style="text-align: left"
                                        ReadOnly="true" Text='<%# Bind("Location") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Original Receipt Date" FooterText="Original Receipt Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOriginal_Receipt_Date" SkinID="GridViewTextBox" ReadOnly="true"
                                        Style="text-align: left" Text='<%# Bind("Original_Receipt_Date") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnReset1" runat="server" SkinID="ButtonNormalBig" CausesValidation="false" OnClick="btnReset_Click"
                            Text="Reset" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
