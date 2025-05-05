<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="GoodsInTransit_Inward.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Query.GoodsInTransit_Inward" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
<script type="text/javascript">
      function pageLoad(sender, args) { 
          //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
          gridViewFixedHeader('<%=grdGoodsInTransitInward.ClientID%>', '1000', '400');
      }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>            
            <div class="subFormTitle">
                GOODS IN TRANSIT- INWARD
            </div>
            <table class="subFormTable">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblInwardNumber" SkinID="LabelNormal" runat="server" Text="Inward Number"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlInwardNumber" SkinID="DropDownListNormalBig" runat="server"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlInwardNumber_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblInwardDate" SkinID="LabelNormal" runat="server" Text="Inward Date"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtInwardDate" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    <td class="label">
                    </td>
                    <td class="inputcontrols">
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" SkinID="LabelNormal" runat="server" Text="Supplier"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtSupplier" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblBranchCode" SkinID="LabelNormal" runat="server" Text="Branch Code"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtBranchCode" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
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
                        <asp:Label ID="lblPlaceOfDespatch" SkinID="LabelNormal" runat="server" Text="Place of despatch"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtPlaceOfDespatch" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblCarrier" SkinID="LabelNormal" runat="server" Text="Carrier"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtCarrier" ReadOnly="true" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
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
                    <asp:GridView ID="grdGoodsInTransitInward" runat="server" SkinID="GridViewScroll" AutoGenerateColumns="False"
                        ShowFooter="false" AllowPaging="True" OnPageIndexChanging="grdGoodsInTransitInward_PageIndexChanging"
                        PageSize="50">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtItemCode" SkinID="GridViewTextBoxBig" ReadOnly="true"  
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
                                    <asp:TextBox ID="txtItemValue"  SkinID="GridViewTextBox" Style="text-align: right" ReadOnly="true"
                                          Text='<%#  string.Format("{0:#.00}", System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"ItemValue")),2)) %>'
                                        runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="25%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="btnReset" runat="server" CausesValidation="false" SkinID="ButtonNormalBig" OnClick="btnReset_Click"
                                Text="Reset" />
                        </div>
                    </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
