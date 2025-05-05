<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ConsignmentSummary.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Query.ConsignmentSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="SupplierPartNumberSearch" Src="~/UserControls/SupplierPartNumberSearch.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
<script type="text/javascript">
      function pageLoad(sender, args) { 
          //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
          gridViewFixedHeader('<%=grdConsignmentSummary.ClientID%>', '1000', '400');
      }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           
            <div class="subFormTitle">
                CONSIGNMENT SUMMARY</div>
            <uc1:SupplierPartNumberSearch ID="ucSupplierPartNumber" OnSearchImageClicked="ucSupplierPartNumber_SearchImageClicked"
                OnSupplierddlChanged="ucSupplierPartNumber_SupplierddlChanged" runat="server">
            </uc1:SupplierPartNumberSearch>
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
                        <asp:Label ID="lblLongDescription" SkinID="LabelNormal" runat="server" Text="Long Description"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtLongDescription" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblBalanceQuantity" SkinID="LabelNormal" runat="server" Text="Balance Quantity"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtBalanceQuantity" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblShortDescription2" runat="server" SkinID="LabelNormal" Text="Short Description"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtShortDescription" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblOSBalanceQuantity" SkinID="LabelNormal" runat="server" Text="OS Balance Quantity"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtOSBalanceQuantity" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblLSBalanceQuantity" SkinID="LabelNormal" runat="server" Text="LS Balance Quantity"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtLSBalanceQuantity" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                     
                </tr>
                <tr>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblFOCOSBalanceQuantity" SkinID="LabelNormal" runat="server" Text="FOC OS Balance Quantity"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtFOCOSBalanceQuantity" ReadOnly="true" SkinID="TextBoxDisabledBig"
                            runat="server"></asp:TextBox>
                    </td>
                    <td class="labelColSpan2">
                        <asp:Label ID="lblFOCLSBalanceQuantity" SkinID="LabelNormal" runat="server" Text="FOC LS Balance Quantity"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:TextBox ID="txtFOCLSBalanceQuantity" ReadOnly="true" SkinID="TextBoxDisabledBig"
                            runat="server"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td class="labelColSpan2" colspan="3">
                        <asp:Label ID="lblNoRecord" SkinID="Error" ForeColor="Red" runat="server"></asp:Label>
                    </td>
                    <%--<td class="inputcontrolsColSpan2">
                    </td>
                    <td class="labelColSpan2">
                    </td>--%>
                    <td class="inputcontrolsColSpan2">
                    </td>
                   
                </tr>
            </table>
            
            <div id="divItemDetails" runat="server">
                <div class="subFormTitle">
                    ITEM DETAILS</div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="grdConsignmentSummary" runat="server" PageSize="50"  SkinID="GridViewScroll"
                        AutoGenerateColumns="False" ShowFooter="true" AllowPaging="True" OnPageIndexChanging="grdConsignmentSummary_PageIndexChanging">
                         
                        <Columns>
                            <asp:TemplateField HeaderText="GRN Number" FooterText="GRN Number">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGRN_Number" SkinID="GridViewTextBox" ReadOnly="true" 
                                        Text='<%# Bind("GRN_Number") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GRN Date"  FooterText="GRN Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGRN_Date" SkinID="GridViewTextBox" ReadOnly="true"  
                                        Text='<%# Bind("GRN_Date") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="List Price" FooterText="List Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtList_Price" SkinID="GridViewTextBox" ReadOnly="true" 
                                        Style="text-align: right" Text='<%# string.Format("{0:#.00}",System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"List_Price")),2)) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cost Price"   FooterText="Cost Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCost_Price" SkinID="GridViewTextBox" Style="text-align: right" ReadOnly="true"
                                        Text='<%# string.Format("{0:#.00}", System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Cost_Price")),2)) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ED Ind" FooterText="ED Ind">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtED_Ind" SkinID="GridViewTextBoxSmall" Style="text-align: left" ReadOnly="true"
                                       Text='<%# Bind("ED_Ind") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ED Value" FooterText="ED Value">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtED_Value" SkinID="GridViewTextBox" Style="text-align: right" ReadOnly="true"
                                        Text='<%# string.Format("{0:#.00}", System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"ED_Value")),2)) %>'
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
                                    <asp:TextBox ID="txtOS_LS" Style="text-align: right" ReadOnly="true"
                                        SkinID="GridViewTextBoxSmall" Text='<%# Bind("OS_LS") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GRN Quantity"  FooterText="GRN Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGRN_Quantity" SkinID="GridViewTextBoxSmall" Style="text-align: right" ReadOnly="true"
                                        Text='<%# Bind("GRN_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance Quantity" FooterText="Balance Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBalance_Quantity"  SkinID="GridViewTextBoxSmall" Style="text-align: right" ReadOnly="true"
                                          Text='<%# Bind("Balance_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Original Receipt Date"   FooterText="Original Receipt Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOriginal_Receipt_Date" SkinID="GridViewTextBox" Style="text-align: left" ReadOnly="true"
                                        Text='<%# Bind("Original_Receipt_Date") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="btnReset" runat="server" SkinID="ButtonNormalBig" CausesValidation="false" OnClick="btnReset_Click"
                                Text="Reset" />
                        </div>
                    </div>
               
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
