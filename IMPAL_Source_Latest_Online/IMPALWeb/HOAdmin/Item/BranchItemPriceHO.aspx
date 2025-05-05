<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BranchItemPriceHO.aspx.cs"
    Inherits="IMPALWeb.HOAdmin.Item.BranchItemPriceHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" src="../../Javascript/BranchItemPriceHO.js" type="text/javascript"></script>

    <div class="reportFormTitle">
        Branch Item Price - HO
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:CheckBox ID="chkZone" runat="server" Checked="true" Text="" Style="display: none" />
                    <asp:Label ID="lblZone" Text="Zone" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlZone" runat="server" DataTextField="ZoneName" DataValueField="ZoneCode"
                        TabIndex="1" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged" AutoPostBack="True"
                        Enabled="false" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                </td>
                <td class="label">
                    <asp:CheckBox ID="chkState" runat="server" Text="" Style="display: none" />
                    <asp:Label ID="lblState" Text="State" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateCode"
                        OnSelectedIndexChanged="ddlState_OnSelectedIndexChanged" TabIndex="2" Enabled="false"
                        AutoPostBack="True" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                </td>
                <td class="label">
                    <asp:CheckBox ID="chkBranch" runat="server" Text="" Style="display: none" />
                    <asp:Label ID="lblBranch" Text="Branch" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                        TabIndex="3" Enabled="false" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="subFormTitle">
                    LIST / COST PRICE DETAILS
                </div>
                <br />
                <asp:GridView ID="grdItemDetails" runat="server" SkinID="GridViewScroll" AutoGenerateColumns="False" OnRowDataBound="grdItemDetails_OnRowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSNo" runat="server" SkinID="GridViewTextBoxSmall" Text='<%# Container.DataItemIndex + 1 %>'
                                    Enabled="false"> </asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" SkinID="GridViewButtonFooter"
                                    Style="width: 40px !important;" OnClientClick="return fnValidateSubmit();" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier Name">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="GridViewDropDownList"
                                    OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier Part #">
                            <ItemTemplate>
                                <div class="itemResetHolder">
                                    <div class="itemReset">
                                        <asp:DropDownList ID="ddlSupplierPartNo" runat="server" AutoPostBack="True" Visible="false"
                                            OnSelectedIndexChanged="ddlSupplierPartNo_OnSelectedIndexChanged" SkinID="GridViewDropDownList">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                    </div>
                                    <div class="itemResetButton">
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                            SkinID="GridViewButton" OnClientClick="javaScript:return fnValidateSearch();" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="List Price">
                            <ItemTemplate>
                                <asp:TextBox ID="txtListPrice" Style="width: 80px" SkinID="GridViewTextBox" runat="server" onblur="javascript:return fnCalculate(this.id);" onkeypress="return CurrencyNumberOnly(this.id)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pur. Disc %">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPurchaseDiscount" Style="width: 60px" SkinID="GridViewTextBox" runat="server" onblur="javascript:return fnCalculate(this.id);" onkeypress="return CurrencyNumberOnly(this.id)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ListLess Disc">
                            <ItemTemplate>
                                <asp:TextBox ID="txtListLessDiscount" Style="right; width: 80px" Enabled="false" SkinID="TextBoxDisabled" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Coupon">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCoupon" Style="width: 60px" SkinID="GridViewTextBox" runat="server" onblur="javascript:return fnCalculate(this.id);" onkeypress="return CurrencyNumberOnly(this.id)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="After Coupon">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAfterCoupon" Style="width: 80px" Enabled="false" SkinID="TextBoxDisabled" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CD %">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCDPercentage" Style="width: 60px" SkinID="GridViewTextBox" runat="server" onblur="javascript:return fnCalculate(this.id);" onkeypress="return CurrencyNumberOnly(this.id)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CD Amount">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCDAmount" Style="width: 80px" Enabled="false" SkinID="TextBoxDisabled" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="After CD">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAfterCD" Style="width: 80px" Enabled="false" SkinID="TextBoxDisabled" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WC %">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWCPercentage" Style="width: 60px" SkinID="GridViewTextBox" runat="server" onblur="javascript:return fnCalculate(this.id);" onkeypress="return CurrencyNumberOnly(this.id)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WC Amount">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWCAmount" Style="width: 80px" Enabled="false" SkinID="TextBoxDisabled" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost Price">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCostPrice" Style="width: 80px" Enabled="false" SkinID="TextBoxDisabled" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MRP">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMRP" Style="width: 80px" SkinID="GridViewTextBox" runat="server" onkeypress="return CurrencyNumberOnly(this.id)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:TextBox ID="txtItemCode" runat="server" Enabled="false" SkinID="TextBoxDisabledBig" Style="width: 150px"></asp:TextBox>
                                <asp:HiddenField ID="hdnCostPrice" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonNormal" ID="btnSubmit" runat="server" Text="Submit"
                TabIndex="3" OnClientClick="javaScript:return fnValidateSubmit();" Style="width: 80px" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnReset" runat="server" CausesValidation="false" SkinID="ButtonNormal"
                OnClick="btnReset_Click" Text="Reset" />
        </div>
    </div>
</asp:Content>
