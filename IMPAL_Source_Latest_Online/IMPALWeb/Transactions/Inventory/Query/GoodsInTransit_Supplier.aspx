<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="GoodsInTransit_Supplier.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Query.GoodsInTransit_Supplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
<script type="text/javascript">
      function pageLoad(sender, args) { 
          //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
          gridViewFixedHeader('<%=grdGoodsInTransitSupplier.ClientID%>', '1000', '400');
      }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           
            <div class="subFormTitle">
                GOODS IN TRANSIT- SUPPLIER</div>
            <table class="subFormTable">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSupplier" SkinID="LabelNormal" runat="server" Text="Supplier"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlSupplier" SkinID="DropDownListNormalBig" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblBranch" SkinID="LabelNormal" runat="server" Text="Branch"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlBranches" Enabled="false" SkinID="DropDownListDisabledBig" runat="server"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td class="label">
                    </td>
                    <td class="inputcontrols">
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNoRecord" SkinID="Error" ForeColor="Red" runat="server"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                    </td>
                    <td class="label">
                    </td>
                    <td class="inputcontrols">
                    </td>
                    <td class="label">
                    </td>
                    <td class="inputcontrols">
                    </td>
                </tr>
            </table>
            <div id="divItemDetails" runat="server">
                <div class="subFormTitle">
                    ITEM DETAILS</div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="grdGoodsInTransitSupplier" runat="server" SkinID="GridViewScroll"  AutoGenerateColumns="False"
                        ShowFooter="false" AllowPaging="True" OnPageIndexChanging="grdGoodsInTransitSupplier_PageIndexChanging"
                        PageSize="50">
                        
                        <Columns>
                            <asp:TemplateField HeaderText="Invoice Number">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtInvoiceNumber" ReadOnly="true" SkinID="GridViewTextBox"
                                        Text='<%# Bind("InvoiceNumber") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtInvoiceDate"  ReadOnly="true" SkinID="GridViewTextBox"
                                        Text='<%# Bind("InvoiceDate") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LR Number">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLRNumber"  ReadOnly="true" SkinID="GridViewTextBox"
                                        Text='<%# Bind("LRNumber") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LR Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLRDate" SkinID="GridViewTextBox" ReadOnly="true" Text='<%# Bind("LRDate") %>'
                                        runat="server" />
                                </ItemTemplate>
                                <ItemStyle/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtItemCode"  ReadOnly="true" SkinID="GridViewTextBoxBig"
                                        Text='<%# Bind("ItemCode") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Part Number">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSupplierPartNumber" ReadOnly="true" SkinID="GridViewTextBox" Style="text-align: right"
                                          Text='<%# Bind("SupplierPartNumber") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" ReadOnly="true" SkinID="GridViewTextBoxSmall" Style="text-align: right"
                                          Text='<%# Bind("Quantity") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtItemValue" SkinID="GridViewTextBox" Style="text-align: right" ReadOnly="true"
                                          Text='<%# string.Format("{0:#.00}",System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Value")),2)) %>'
                                        runat="server" />
                                </ItemTemplate>
                                <ItemStyle/>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                      <asp:Button ID="btnReset" runat="server" CausesValidation="false"  OnClick="btnReset_Click" SkinID="ButtonNormalBig"
                                Text="Reset" />
                    </div>
                </div>
              
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
