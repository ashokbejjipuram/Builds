<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DirectPurchaseOrderHO.aspx.cs"
    Inherits="IMPALWeb.Ordering.Ord_DirectPurchaseOrderHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <script type="text/javascript" src="../../../JavaScript/xlsx.full.min.js"></script>
    <script src="../../Javascript/DirectPOcommonHO.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {

            iRowCount = document.getElementById(CtrlIdPrefix + "hdnFreezeRowCnt").value.trim();
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)a
            if (parseInt(iRowCount) > 0)
                gridViewFixedHeader('<%=GridView1.ClientID%>', '1000', '200');
        }        
    </script>
    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div>
                        <div class="subFormTitle subFormTitleExtender450">
                            PO-WORKSHEET / STDN GENERATION</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdBranch" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdBranchName" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdBranchName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblHeaderMessage" runat="server" Text="PO Number" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrd_PONumber" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"
                                        TabIndex="1"></asp:TextBox>
                                    <asp:DropDownList ID="ddlOrd_PONumber" SkinID="DropDownListNormal" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlOrd_PONumber_SelectedIndexChanged"
                                        TabIndex="2">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="ImgButtonQuery" ImageUrl="~/images/ifind.png" alt="Query" runat="server"
                                        SkinID="ImageButtonSearch" OnClick="ImgButtonQuery_Click" TabIndex="1" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdIndentDate" runat="server" SkinID="LabelNormal" Text="PO date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdIndentDate" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"
                                        TabIndex="3"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdTransactionType" runat="server" SkinID="LabelNormal" Text="Transaction type"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdTransactionType" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdTransactionType_SelectedIndexChanged" TabIndex="3">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdSupplier" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdSupplier" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdSupplier_SelectedIndexChanged" TabIndex="4">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblPOType" runat="server" SkinID="LabelNormal" Text="PO Type"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtPOType" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"
                                        TabIndex="3"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="PO Value"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtPOValue" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"
                                        TabIndex="3"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblCustomer" SkinID="LabelNormal" runat="server" Text="Customer"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdCustomer" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdCustomer_SelectedIndexChanged" TabIndex="5">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            CUSTOMER INFORMATION</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerCode" SkinID="LabelNormal" runat="server" Text="Customer Code"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerCode" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerAddress1" SkinID="LabelNormal" runat="server" Text="Address1"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerAddress1" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerAddress2" SkinID="LabelNormal" runat="server" Text="Address2"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerAddress2" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblTin_NoAddress3" SkinID="LabelNormal" runat="server" Text="Address3"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdAddress3" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerAddress4" runat="server" SkinID="LabelNormal" Text="Address4"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerAddress4" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerLocation" SkinID="LabelNormal" runat="server" Text="Location"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerLocation" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divItemDetails" runat="server">
            <div class="subFormTitle">
                ITEM DETAILS</div>
            <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="gridViewScrollFullPage" style="vertical-align: middle !important; text-align: center !important;">
                        <asp:GridView ID="GridView1" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                            OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound"
                            SkinID="GridViewScroll" PageSize="1500">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptySearch" SkinID="GridViewLabel" runat="server">No Results Found</asp:Label>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="1%" />
                                    <HeaderTemplate><asp:CheckBox ID="ChkAllSelected" runat="server" AutoPostBack="false" OnClick="return EnableAllCheckboxes(this.id);" /></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSelected" runat="server" AutoPostBack="false" OnClick="return EnableDataChanges();" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="S_No" HeaderText="S.No" Visible="false" />
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="1%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supp. Part #" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="2%" />
                                    <ItemTemplate>
                                        <b>Item Value: </b><asp:Label ID="lblItemValue" runat="server"></asp:Label><br /><br />
                                        <asp:TextBox ID="txtOrdSupplierPartNo" SkinID="TextBoxDisabled" ReadOnly="true" Enabled="false"
                                            TabIndex="14" runat="server" />
                                        <asp:HiddenField ID="hdnSNo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <HeaderStyle Width="1%" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrdItemCode" SkinID="TextBoxDisabled" ReadOnly="true" Enabled="false"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Packing<br />Qty" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="1%" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrdPackingQuantity" SkinID="TextBoxDisabledSmall" ReadOnly="true"
                                            Enabled="false" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order<br />Qty" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="1%" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrderItem_PO_Quantity" MaxLength="6" onkeypress="return IntegerValueOnly();"
                                            ondrop="return false;" onpaste="return false;" SkinID="GridViewTextBoxSmall"
                                            runat="server" Width="50" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="South Zone" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlBranchesSouthZone" runat="server" SkinID="GridViewDropDownList"
                                            AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlBranchesSouthZone_SelectedIndexChanged" />
                                        <asp:TextBox ID="txtQtySouthZone" MaxLength="6" onkeypress="return IntegerValueOnly();"
                                            ondrop="return false;" onpaste="return false;" SkinID="GridViewTextBoxSmall"
                                            runat="server" Width="50" Enabled="false" onblur="return CheckQty(this.id)" />
                                        <br />
                                        <asp:Button ID="btnAddSouthZone" SkinID="ButtonNormal" runat="server" Text="Add"
                                            OnClick="btnAddSouthZone_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="North Zone" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlBranchesNorthZone" runat="server" SkinID="GridViewDropDownList"
                                            AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlBranchesNorthZone_SelectedIndexChanged" />
                                        <asp:TextBox ID="txtQtyNorthZone" MaxLength="6" onkeypress="return IntegerValueOnly();"
                                            ondrop="return false;" onpaste="return false;" SkinID="GridViewTextBoxSmall"
                                            runat="server" Width="50" Enabled="false" onblur="return CheckQty(this.id)" />
                                        <br />
                                        <asp:Button ID="btnAddNorthZone" SkinID="ButtonNormal" runat="server" Text="Add"
                                            OnClick="btnAddNorthZone_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="East Zone" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlBranchesEastZone" runat="server" SkinID="GridViewDropDownList"
                                            AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlBranchesEastZone_SelectedIndexChanged" />
                                        <asp:TextBox ID="txtQtyEastZone" MaxLength="6" onkeypress="return IntegerValueOnly();"
                                            ondrop="return false;" onpaste="return false;" SkinID="GridViewTextBoxSmall"
                                            runat="server" Width="50" Enabled="false" onblur="return CheckQty(this.id)" />
                                        <br />
                                        <asp:Button ID="btnAddEastZone" SkinID="ButtonNormal" runat="server" Text="Add" OnClick="btnAddEastZone_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="West Zone" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlBranchesWestZone" runat="server" SkinID="GridViewDropDownList"
                                            AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlBranchesWestZone_SelectedIndexChanged" />
                                        <asp:TextBox ID="txtQtyWestZone" MaxLength="6" onkeypress="return IntegerValueOnly();"
                                            SkinID="GridViewTextBoxSmall" runat="server" Width="50" Enabled="false" onblur="return CheckQty(this.id)" />
                                        <br />
                                        <asp:Button ID="btnAddWestZone" SkinID="ButtonNormal" runat="server" Text="Add" OnClick="btnAddWestZone_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Selected Details" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemTemplate>
                                        <asp:ListBox ID="ddlListIndentBranches" SkinID="ListBoxNormal" runat="server" />
                                        <input id="hdnTotalIndentQty" type="hidden" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQtyBalance" SkinID="GridViewTextBoxSmall" runat="server" Width="50"
                                            Enabled="false" />
                                        <br />
                                        <br />
                                        <asp:Button runat="server" ID="btnRemove" SkinID="ButtonNormal" Text="Remove" Enabled="false"
                                            OnClick="btnRemove_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" OnClientClick="return funDirectPOBtnSubmitRow();"
                                SkinID="ButtonNormal" OnClick="BtnSubmit_Click" CausesValidation="true" Text="Submit" />
                            <asp:Button ID="btnReset" runat="server" OnClientClick="return resetAction();" CausesValidation="true"
                                SkinID="ButtonNormal" OnClick="btnReset_Click" Text="Reset" />
                            <asp:Button SkinID="ButtonViewReport" ID="BtnSupplierExcelFile" runat="server" Text="Supplier Excel"
                                TabIndex="3" OnClick="BtnSupplierExcelFile_Click" Visible="false" />
                            <asp:Button SkinID="ButtonViewReport" ID="BtnBranchExcelFile" runat="server" Text="Branch Excel"
                                TabIndex="3" OnClick="BtnBranchExcelFile_Click" Visible="false" />
                        </div>
                    </div>
                    <input id="hdnRowCnt" type="hidden" runat="server" />
                    <input id="hdnFreezeRowCnt" type="hidden" runat="server" />
                    <asp:HiddenField ID="hdnJSonExcelData" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlOrdSupplier" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlOrd_PONumber" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlOrdBranchName" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="BtnSupplierExcelFile" />
                    <asp:PostBackTrigger ControlID="BtnBranchExcelFile" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
