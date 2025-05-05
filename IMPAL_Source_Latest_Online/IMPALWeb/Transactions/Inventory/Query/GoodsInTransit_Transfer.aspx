<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="GoodsInTransit_Transfer.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Query.GoodsInTransit_Transfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
<script type="text/javascript">
      function pageLoad(sender, args) { 
          //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
          gridViewFixedHeader('<%=grdGoodsInTransitTransfer.ClientID%>', '1000', '400');
      }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           
            <div class="subFormTitle">
                GOODS IN TRANSIT- TRANSFER</div>
            <table class="subFormTable">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSTDNNumber" SkinID="LabelNormal" runat="server" Text="STDN Number"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlSTDN" SkinID="DropDownListNormalBig" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlSTDN_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblSTDNDate" SkinID="LabelNormal" runat="server" Text="STDN Date"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtSTDNDate" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    <td class="label">
                    </td>
                    <td class="inputcontrols">
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFromBranch" SkinID="LabelNormal" runat="server" Text="From Branch"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtFromBranch" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblToBranch" SkinID="LabelNormal" runat="server" Text="To Branch"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtToBranch" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    <td class="label">
                    </td>
                    <td class="inputcontrols">
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLRNumber" SkinID="LabelNormal" runat="server" Text="LR Number"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtLRNumber" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblLRDate" SkinID="LabelNormal" runat="server" Text="LR Date"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtLRDate" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
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
                <asp:GridView ID="grdGoodsInTransitTransfer" runat="server" SkinID="GridViewScroll" AutoGenerateColumns="False"
                    ShowFooter="false" AllowPaging="True" OnPageIndexChanging="grdGoodsInTransitTransfer_PageIndexChanging"
                    PageSize="50"> 
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:TextBox ID="txtItemCode"   ReadOnly="true" SkinID="GridViewTextBox"
                                    Text='<%# Bind("ItemCode") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="25%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier Part Number">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSupplierPartNumber" SkinID="GridViewTextBox" ReadOnly="true"
                                    Style="text-align: right" Text='<%# Bind("SupplierPartNumber") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="25%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" SkinID="GridViewTextBox" ReadOnly="true"
                                    Style="text-align: right" Text='<%# Bind("Quantity") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="25%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value">
                            <ItemTemplate>
                                <asp:TextBox ID="txtItemValue" SkinID="GridViewTextBox" Style="text-align: right"
                                    ReadOnly="true" Text='<%# string.Format("{0:#.00}",System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Value")),2)) %>'
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="25%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            </div>
            <div class="transactionButtons">
                <div class="transactionButtonsHolder">
                    <asp:Button ID="btnReset" runat="server" CausesValidation="false" OnClick="btnReset_Click"
                        SkinID="ButtonNormalBig"     Text="Reset" />
                </div>
            </div>
            </div>
           
           
        </ContentTemplate>
    </asp:UpdatePanel>
   </asp:Content>
