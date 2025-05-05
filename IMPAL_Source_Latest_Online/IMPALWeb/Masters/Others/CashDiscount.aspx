<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CashDiscount.aspx.cs" Inherits="IMPALWeb.Masters.Others.CashDiscount" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
<script type="text/javascript">
    function validateFields(source, args) {
//        debugger;
        var cashDiscountFormView = document.getElementById('<%= cashDiscountFormView.ClientID %>');
        var txtCustDueDays = document.getElementById(cashDiscountFormView.id + '_txtDueDays');
        var txtSuppDueDays = document.getElementById(cashDiscountFormView.id + '_txtSupDueDays');
        var ddlLineItem = document.getElementById(cashDiscountFormView.id + '_ddlLineItem');

        if(!(txtCustDueDays.disabled)){
        if  (txtCustDueDays.value == "") {
            args.IsValid = false;
            return;
        }
        else {
            args.IsValid = true;
            return;
        }
        }
        else if((!(txtSuppDueDays.disabled)) && (!(ddlLineItem.disabled))){
        if  (txtSuppDueDays.value == "") {
            args.IsValid = false;
            return;
        }
        else {
            args.IsValid = true;
            return;
        }
        if (ddlLineItem.options[0].selected == true) {
            args.IsValid = false;
            return;
        }
        else {
            args.IsValid = true;
            return;
        }
        }
    }

</script>
    <div>
        <div class="subFormTitle">
            Cash Discount Master</div>
        <div>
            <table class="subFormTable">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCode" SkinID="LabelNormal" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="drpDiscCode" SkinID="GridViewDropDownListFooter" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" SkinID="ButtonNormal" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="PnlCustomer" runat="server">
                    <asp:FormView ID="cashDiscountFormView" runat="server">
                        <HeaderStyle ForeColor="white" BackColor="Blue" />
                        <ItemTemplate>
                            <table id="tblHeader" class="subFormTable">
                                <tr>
                                    <td>
                                        <div class="subFormTitle">
                                            CASH DISCOUNT</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblCashCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtCashCode"  ReadOnly="true" Enabled="false" runat="server" Text='<%#Bind("CashDiscCode")%>' SkinID="TextBoxNormal"></asp:TextBox>
                                    </td>   
                                    <td class="label">
                                        <asp:Label ID="lblNorms" runat="server" SkinID="LabelNormal" Text="Norms"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtNorms" runat="server" Text='<%#Bind("Norms")%>' SkinID="TextBoxNormal"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldNorms" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtNorms" SetFocusOnError="true"
                                                ErrorMessage="Description should not be null" ></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblIndicator" runat="server" SkinID="LabelNormal" Text="Indicator"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:RadioButtonList runat="server" ID="rdIndicator" RepeatDirection="Horizontal"
                                            SkinID="RadioButtonNormal" onselectedindexchanged="rdIndicator_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="S" Selected>Supp</asp:ListItem>
                                            <asp:ListItem Value="C">Customer</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblDiscount" runat="server" SkinID="LabelNormal" Text="Discount %"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtDiscount" runat="server" Text='<%#Bind("DiscountPer")%>' SkinID="TextBoxNormal"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldDiscount" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtDiscount" SetFocusOnError="true"
                                                ErrorMessage="Discount should not be null" ></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle" id="divCustomer">
                                            CUSTOMER</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblDueDays" runat="server" SkinID="LabelNormal" Text="Due Days"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtDueDays" runat="server" Text='<%#Bind("CustomerDueDays")%>' SkinID="TextBoxNormal"></asp:TextBox>
                                            <br />
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldCustDueDays" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtDueDays" SetFocusOnError="true" 
                                                ErrorMessage="Due days should not be null" ></asp:RequiredFieldValidator>--%>
                                                <asp:CustomValidator ID="CustValCustDueDays" runat="server" Display="Dynamic" ValidationGroup="validate"
                                                ControlToValidate="txtDueDays" ClientValidationFunction="validateFields" ForeColor="Red" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Due days should not be null"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="subFormTitle" id="divSupplier">
                                            SUPPLIER</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSupDueDays" runat="server" SkinID="LabelNormal" Text="Due Days"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:TextBox ID="txtSupDueDays" runat="server" Text='<%#Bind("SupDueDays")%>' SkinID="TextBoxNormal"></asp:TextBox>
                                            <br />
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldSupDueDays" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="txtSupDueDays" SetFocusOnError="true"
                                                ErrorMessage="Due days should not be null" ></asp:RequiredFieldValidator>--%>
                                                <asp:CustomValidator ID="CustValSupDueDays" runat="server" Display="Dynamic" ValidationGroup="validate"
                                                ControlToValidate="txtSupDueDays" ClientValidationFunction="validateFields" ForeColor="Red" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Due days should not be null"></asp:CustomValidator>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblLineItem" runat="server" SkinID="LabelNormal" Text="Line Item"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlLineItem" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"> 
                                        </asp:DropDownList>
                                            <br />
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldLineItem" ValidationGroup="validate"
                                                runat="server" ForeColor="Red" ControlToValidate="ddlLineItem" SetFocusOnError="true"
                                                ErrorMessage="Line Item should not be null" InitialValue="0"></asp:RequiredFieldValidator> --%> 
                                                <asp:CustomValidator ID="CustValLineItem" runat="server" Display="Dynamic" ValidationGroup="validate"
                                                ControlToValidate="ddlLineItem" ClientValidationFunction="validateFields" ForeColor="Red" SetFocusOnError="true" ValidateEmptyText="true"
                                                ErrorMessage="Line Item should not be null"></asp:CustomValidator>                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblCalcIndicator" runat="server" SkinID="LabelNormal" Text="Calculation Indicator"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:RadioButtonList runat="server" ID="rdlCalcIndicator" RepeatDirection="Horizontal"
                                            SkinID="RadioButtonNormal">
                                            <asp:ListItem Value="L" Selected>List</asp:ListItem>
                                            <asp:ListItem Value="I">Invoice</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblPurchaseIndicator" runat="server" SkinID="LabelNormal" Text="Purchase Indicator"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:RadioButtonList runat="server" ID="rdlPurchaseIndicator" RepeatDirection="Horizontal"
                                            SkinID="RadioButtonNormal">
                                            <asp:ListItem Value="L" Selected>Local</asp:ListItem>
                                            <asp:ListItem Value="O">Outstation</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPayIndicator" runat="server" SkinID="LabelNormal" Text="Payment Indicator"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:RadioButtonList runat="server" ID="rdlPaymentIndicator" RepeatDirection="Horizontal"
                                            SkinID="RadioButtonNormal">
                                            <asp:ListItem Value="H" Selected>HO</asp:ListItem>
                                            <asp:ListItem Value="D">Depot</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormal"  ValidationGroup="validate"
                    CausesValidation="true" onclick="BtnSubmit_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" 
                    onclick="btnReset_Click" />
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport" 
                    onclick="btnReport_Click" />
            </div>
        </div>
    </div>
</asp:Content>
