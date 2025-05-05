<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="CustomerSalesReq.aspx.cs"
    Inherits="IMPALWeb.CustomerSalesReq" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../Javascript/CustomerSalesReq.js" type="text/javascript"></script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmbCustomerName" />
            </Triggers>
            <ContentTemplate>
                <div>
                    <div class="subFormTitle">
                        CUSTOMER SALES REQUEST
                    </div>
                    <table class="subFormTable" width="800">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBranchCode" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch" DataTextField="BranchName"
                                    DataValueField="BranchCode" AutoPostBack="True" TabIndex="1" Enabled="false"
                                    SkinID="DropDownListDisabled" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCustomerReqNumber" runat="server" Text="Sales Request #" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCustomerReqNumber" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label4" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <ajaxToolkit:ComboBox ID="cmbCustomerName" runat="server" OnTextChanged="cmbCustomerName_OnSelectedIndexChanged" AutoPostBack="True" SkinID="ComboBoxNormal" Width="300px"
                                    DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" CaseSensitive="False" ItemInsertLocation="Append" OnKeyDown="javascript:return preventEnterKeyOnCombo();"
                                    OnPaste="javascript:return false;">
                                </ajaxToolkit:ComboBox>
                                <asp:HiddenField ID="HdnIndHexCust" runat="server" Visible="false" />
                            </td>
                            <td class="label">
                                <asp:Label runat="server" ID="lblPhone" Text="Phone" SkinID="LabelNormal" />
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtPhone" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                            </td>
                        </tr>
                    </table>
                    <div id="divCustomerInfo" style="display: none" runat="server">
                        <div class="reportFormTitle">
                            Customer Information
                        </div>
                        <table class="reportFiltersTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCustomerCreditLimit" runat="server" SkinID="LabelNormal" Text="Credit Limit"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCustomerCreditLimit" runat="server" SkinID="TextBoxDisabledBig" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblCustomerOutStanding" runat="server" SkinID="LabelNormal" Text="Out Standing"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCustomerOutStanding" runat="server" SkinID="TextBoxDisabledBig" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblCanBill" runat="server" SkinID="LabelNormal" Text="Can bill upto"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCanBillUpTo" runat="server" Enabled="false" SkinID="TextBoxDisabledBig"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label runat="server" ID="lblCustomerCode" Text="Customer Code" SkinID="LabelNormal" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxDisabledBig"
                                        ReadOnly="true" />
                                </td>
                                <td class="label">
                                    <asp:Label Text="Address1" SkinID="LabelNormal" runat="server" ID="lblAddress1" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxDisabledBig"
                                        ReadOnly="true" />
                                </td>
                                <td class="label">
                                    <asp:Label runat="server" ID="lblAddress2" Text="Address2" SkinID="LabelNormal" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxDisabledBig"
                                        ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label runat="server" ID="lblAddress3" Text="Address3" SkinID="LabelNormal" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxDisabledBig"
                                        ReadOnly="true" />
                                </td>
                                <td class="label">
                                    <asp:Label Text="Address4" SkinID="LabelNormal" runat="server" ID="lblAddress4" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxDisabledBig"
                                        ReadOnly="true" />
                                </td>
                                <td class="label">
                                    <asp:Label runat="server" ID="lblLocation" Text="Location" SkinID="LabelNormal" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxDisabledBig"
                                        ReadOnly="true" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlBranch" />
            </Triggers>
            <ContentTemplate>
                <div id="divItemDetails" runat="server">
                    <div class="subFormTitle">
                        ITEM DETAILS
                    </div>
                    <div id="divgrdRow" style="overflow: auto; width: 1000px; min-height: 100px; max-height: 450px; margin: 0; padding: 0;">
                        <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvItemDetails_OnRowDataBound"
                            SkinID="GridViewTransaction" OnRowDeleting="grvItemDetails_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSNo" runat="server" SkinID="GridViewTextBoxSmall" Text='<%# Container.DataItemIndex + 1 %>'
                                            Enabled="false"> </asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" SkinID="GridViewButtonFooter"
                                            Style="width: 40px !important;" OnClientClick="return CustomerSalesReqValidation(this.id);" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="GridViewDropDownList"
                                            OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="lblSupplier" runat="server" Enabled="false" SkinID="GridViewTextBox"></asp:TextBox>
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
                                                    SkinID="GridViewButton" />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Packing Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPackingQuantity" runat="server" SkinID="GridViewTextBoxSmall" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req.Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server" SkinID="GridViewTextBoxSmall" onkeypress="return IntegerValueOnly();"
                                            AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valid Days">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValidDays" runat="server" SkinID="GridViewTextBoxSmall" MaxLength="2" onkeypress="return IntegerValueOnly();"
                                            AutoPostBack="true" OnTextChanged="txtValidDays_TextChanged" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="List Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtListPrice" runat="server" SkinID="gridviewTextBoxSmall" Enabled="false"
                                            Style="width: 80px !important;"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Ord. Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMinOrdQty" runat="server" SkinID="gridviewTextBoxSmall" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtItemCode" runat="server" Enabled="false" SkinID="GridViewTextBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Description">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtItemDescription" runat="server" Enabled="false" SkinID="GridViewTextBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                            Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden" Visible="false"></asp:TextBox>
                    <input id="hdnRowCnt" type="hidden" runat="server" />
                    <div id="idtransButtons" class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" SkinID="ButtonNormal" runat="server" Text="Submit" OnClick="BtnSubmit_Click"
                                OnClientClick="return CustomerSalesReqValidation(this.id);" />
                            <asp:Button ID="BtnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
</asp:Content>
