<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="SLBQuery.aspx.cs"
    Inherits="IMPALWeb.Masters.SLB.SLBQuery" %>

<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript" language="javascript">
        function fnValidateGrid(btnCheck) {
            var SLBFormView = document.getElementById('<%=SLBFormView.ClientID%>');
            if (SLBFormView.innerHTML != "") {
                var ddlItemCode = document.getElementById(SLBFormView.id + '_ddlItemCode');
                var txtSalesTax = document.getElementById(SLBFormView.id + '_txtSalesTax');
                var txtSucharge = document.getElementById(SLBFormView.id + '_txtSurchage');
                var txtAdditionalSurcharge = document.getElementById(SLBFormView.id + '_txtAdditinalSurchage');
                var txtEntryTax = document.getElementById(SLBFormView.id + '_txtEntryTax');
                var txtOctroi = document.getElementById(SLBFormView.id + '_txtOctroi');
                var txtCess = document.getElementById(SLBFormView.id + '_txtCess');
                var txtOtherLevies1 = document.getElementById(SLBFormView.id + '_txtOtherLevies1');
                var txtOtherLevies2 = document.getElementById(SLBFormView.id + '_txtOtherLevies2');
                var txtInsurance = document.getElementById(SLBFormView.id + '_txtInsurance');
                var txtWarehouseSurcharge = document.getElementById(SLBFormView.id + '_txtWarehouseSurcharge');
                var txtFreight = document.getElementById(SLBFormView.id + '_txtFreight');
                var txtTurnoverTax = document.getElementById(SLBFormView.id + '_txtTurnoverTax');
                var txtBonus = document.getElementById(SLBFormView.id + '_txtBonus');
                var txtCouponCharges = document.getElementById(SLBFormView.id + '_txtCouponCharges');
                var txtGrossProfit = document.getElementById(SLBFormView.id + '_txtGrossProfit');
                var txtTurnoverDiscount = document.getElementById(SLBFormView.id + '_txtTurnoverDiscount');
                var txtSpecialTurnoverDiscount = document.getElementById(SLBFormView.id + '_txtSpecialTurnoverDiscount');
                var txtDealersDiscount = document.getElementById(SLBFormView.id + '_txtDealersDiscount');
                var txtHandlingCharges = document.getElementById(SLBFormView.id + '_txtHandlingCharges');
                var txtEDforSellingPrice = document.getElementById(SLBFormView.id + '_txtEDforSellingPrice');
                var txtSpecialDiscount1 = document.getElementById(SLBFormView.id + '_TxtSpecialDiscount1');
                var txtSpecialDiscount2 = document.getElementById(SLBFormView.id + '_TxtSpecialDiscount2');

                if (ddlItemCode != null && ddlItemCode.options[0].selected == true) {
                    alert("Item code should not be empty");
                    ddlItemCode.focus();
                    return false;
                }
                else if (isNaN(txtSalesTax.value)) {
                    alert("Sales tax % value should be Numeric & greater than 0 ");
                    txtSalesTax.focus();
                    return false;
                }
                else if ((txtSalesTax.value) > 100) {
                    alert("Sales Tax % should be between 0 to 100");
                    txtSalesTax.focus();

                    return false;
                }
                else if (isNaN(txtSucharge.value)) {
                    alert("Surcharge % should be Numeric & greater than 0 ");
                    txtSucharge.focus();
                    return false;
                }
                else if ((txtSucharge.value) > 100) {
                    alert("Surcharge % should be between 0 to 100");
                    txtSucharge.focus();
                    return false;
                }
                else if (isNaN(txtAdditionalSurcharge.value)) {
                    alert("Additional Surcharge % should be Numeric & greater than 0 ");
                    txtAdditionalSurcharge.focus();
                    return false;
                }
                else if ((txtAdditionalSurcharge.value) > 100) {
                    alert("Additional Surcharge % should be between 0 to 100");
                    txtAdditionalSurcharge.focus();
                    return false;
                }
                else if (isNaN(txtEntryTax.value)) {
                    alert("Entry Tax % should be Numeric & greater than 0 ");
                    txtEntryTax.focus();
                    return false;
                }
                else if ((txtEntryTax.value) > 100) {
                    alert("Entry Tax % should be between 0 to 100");
                    txtEntryTax.focus();
                    return false;
                }
                else if (isNaN(txtOctroi.value)) {
                    alert("Octroi % value should be Numeric & greater than 0 ");
                    txtOctroi.focus();
                    return false;
                }
                else if ((txtOctroi.value) > 100) {
                    alert("Octroi % should be between 0 to 100");
                    txtOctroi.focus();
                    return false;
                }
                else if (isNaN(txtCess.value)) {
                    alert("CESS % should be Numeric & greater than 0 ");
                    txtCess.focus();
                    return false;
                }
                else if ((txtCess.value) > 100) {
                    alert("CESS % should be between 0 to 100");
                    txtCess.focus();
                    return false;
                }
                else if (isNaN(txtOtherLevies1.value)) {
                    alert("Other Levies # 1 % value should be Numeric & greater than 0 ");
                    txtOtherLevies1.focus();
                    return false;
                }
                else if ((txtOtherLevies1.value) > 100) {
                    alert("Other Levies # 1 % should be between 0 to 100");
                    txtOtherLevies1.focus();
                    return false;
                }
                else if (isNaN(txtOtherLevies2.value)) {
                    alert("Other Levies # 2 %  should be Numeric & greater than 0 ");
                    txtOtherLevies2.focus();
                    return false;
                }
                else if ((txtOtherLevies2.value) > 100) {
                    alert("Other Levies # 2 % should be between 0 to 100");
                    txtOtherLevies2.focus();
                    return false;
                }
                else if (isNaN(txtInsurance.value)) {
                    alert("Insurance  should be Numeric & greater than 0 ");
                    txtInsurance.focus();
                    return false;
                }
                else if (isNaN(txtWarehouseSurcharge.value)) {
                    alert("Warehouse Surcharge %  should be Numeric & greater than 0 ");
                    txtWarehouseSurcharge.focus();
                    return false;
                }
                else if ((txtWarehouseSurcharge.value) > 100) {
                    alert("Warehouse Surcharge % should be between 0 to 100");
                    txtWarehouseSurcharge.focus();
                    return false;
                }
                else if (isNaN(txtFreight.value)) {
                    alert("Freight value should be Numeric & greater than 0 ");
                    txtFreight.focus();
                    return false;
                }
                else if (isNaN(txtTurnoverTax.value)) {
                    alert("Turnover Tax value should be Numeric & greater than 0 ");
                    txtTurnoverTax.focus();
                    return false;
                }
                else if (isNaN(txtBonus.value)) {
                    alert("Bonus value should be Numeric & greater than 0 ");
                    txtBonus.focus();
                    return false;
                }
                else if (isNaN(txtCouponCharges.value)) {
                    alert("Coupon Charges value should be Numeric & greater than 0 ");
                    txtCouponCharges.focus();
                    return false;
                }
                else if (isNaN(txtGrossProfit.value)) {
                    alert("Gross Profit value should be Numeric & greater than 0 ");
                    txtGrossProfit.focus();
                    return false;
                }
                else if (isNaN(txtTurnoverDiscount.value)) {
                    alert("Turnover Discount value should be Numeric & greater than 0 ");
                    txtTurnoverDiscount.focus();
                    return false;
                }
                else if (isNaN(txtSpecialTurnoverDiscount.value)) {
                    alert("Special turnover Discount value should be Numeric & greater than 0 ");
                    txtSpecialTurnoverDiscount.focus();
                    return false;
                }
                else if (isNaN(txtDealersDiscount.value)) {
                    alert("Dealers Discount value should be Numeric & greater than 0 ");
                    txtDealersDiscount.focus();
                    return false;
                }
                else if (isNaN(txtHandlingCharges.value)) {
                    alert("Handling Charges value should be Numeric & greater than 0 ");
                    txtHandlingCharges.focus();
                    return false;
                }
                else if (isNaN(txtEDforSellingPrice.value)) {
                    alert("ED for selling price should be Numeric & greater than 0 ");
                    txtEDforSellingPrice.focus();
                    return false;
                }
                else if (isNaN(txtSpecialDiscount1.value)) {
                    alert("Special discount #1 Value should be Numeric & greater than 0 ");
                    txtSpecialDiscount1.focus();
                    return false;
                }
                else if (isNaN(txtSpecialDiscount2.value)) {
                    alert("Special discount #2 Value should be Numeric & greater than 0 ");
                    txtSpecialDiscount2.focus();
                    return false;
                }
            }
            else
                return false;
        }
    </script>

    <div>
        <div class="subFormTitle">
            SLB Query</div>
        <div>
            <table class="subFormTable">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBranch" SkinID="LabelNormal" runat="server" Text="Branch Code"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlBranch" SkinID="GridViewDropDownListFooter" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSupplierLine" SkinID="LabelNormal" runat="server" Text="Supplier Line"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlSupplierLineCode" SkinID="GridViewDropDownListFooter" runat="server"
                            OnSelectedIndexChanged="ddlSupplierLineCode_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLCode" SkinID="LabelNormal" runat="server" Text="Line Code"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlLCode" SkinID="GridViewDropDownListFooter" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" SkinID="ButtonNormal" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
               <%-- <tr>
                    <td colspan="2">
                        <div id="subForm" class="subFormTitle" runat="server">
                            SLB</div>
                    </td>
                </tr>--%>
            </table>
             <div id="subForm" class="subFormTitle" runat="server">
                            SLB</div>
        </div>
        <asp:Panel ID="PnlSLB" runat="server">
            <asp:FormView ID="SLBFormView" runat="server" OnDataBound="SLBFormView_DataBound"
                OnPageIndexChanging="SLBFormView_PageIndexChanging">
                <HeaderStyle ForeColor="white" BackColor="Blue" />
                <EmptyDataTemplate>
                     <asp:Label ID="lblNoResult" runat="server" SkinID="GridViewLabel">No results found</asp:Label>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <table id="tblHeader" class="subFormTable">
                        <%--<tr>
                            <td>
                                <div id="subForm" class="subFormTitle" runat="server">
                                    SLB</div>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBranch" runat="server" SkinID="LabelNormal" Text="Branch Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranchCode" SkinID="GridViewDropDownListFooter" OnSelectedIndexChanged="ddlBranchCode_SelectedIndexChanged"
                                    runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSLB" runat="server" SkinID="LabelNormal" Text="Supplier Line Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSLC" SkinID="GridViewDropDownListFooter" OnSelectedIndexChanged="ddlSLC_SelectedIndexChanged"
                                    runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblItemCode" runat="server" SkinID="LabelNormal" Text="Item Code"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlItemCode" SkinID="GridViewDropDownListFooter" runat="server"
                                    OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblListPrice" runat="server" SkinID="LabelNormal" Text="List Price"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtListPrice" ReadOnly="true" runat="server" SkinID="TextBoxNormal"
                                    Text='<%# Bind("List_Price", "{0:0.00}") %>' Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblPurchaseDiscount" runat="server" SkinID="LabelNormal" Text="Purchase Discount"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtPurchaseDiscount" ReadOnly="true" runat="server" Text='<%# Bind("Supplier_Discount", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDisc1" runat="server" SkinID="LabelNormal" Text="A.Disc #1 ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlADisc1" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAddDisc1" runat="server" SkinID="LabelNormal" Text="Additional discount #1"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddDisc1" ReadOnly="true" runat="server" Text='<%# Bind("Addition_discount_1", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDisc2" runat="server" SkinID="LabelNormal" Text="A.Disc #2 ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlADisc2" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAddDisc2" runat="server" SkinID="LabelNormal" Text="Additional discount #2"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddDisc2" ReadOnly="true" runat="server" Text='<%# Bind("Addition_discount_2", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDisc3" runat="server" SkinID="LabelNormal" Text="A.Disc #3 ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlADisc3" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAddDisc3" runat="server" SkinID="LabelNormal" Text="Additional discount #3"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddDisc3" ReadOnly="true" runat="server" Text='<%# Bind("Addition_discount_3", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDisc4" runat="server" SkinID="LabelNormal" Text="A.Disc #4 ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlADisc4" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAddDisc4" runat="server" SkinID="LabelNormal" Text="Additional discount #4"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddDisc4" ReadOnly="true" runat="server" Text='<%# Bind("Addition_discount_4", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDisc5" runat="server" SkinID="LabelNormal" Text="A.Disc #5 ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlADisc5" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAddDisc5" runat="server" SkinID="LabelNormal" Text="Additional discount #5"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAddDisc5" ReadOnly="true" runat="server" Text='<%# Bind("Addition_discount_5", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblExciseDutyInd" runat="server" SkinID="LabelNormal" Text="Excise duty ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlExciseDuty" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblExciseDuty" runat="server" SkinID="LabelNormal" Text="Excise duty"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtExciseDuty" ReadOnly="true" runat="server" Text='<%# Bind("Excise_Duty_Percentage", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSalesTax" runat="server" SkinID="LabelNormal" Text="Sales tax %"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSalesTax" runat="server" Text='<%# Bind("Sales_Tax_Percentage", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSurcharge" runat="server" SkinID="LabelNormal" Text="Surcharge %"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSurchage" runat="server" Text='<%# Bind("Surcharge_Percentage", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAdditionalSurchageInd" runat="server" SkinID="LabelNormal" Text="Additional Surcharge ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlAdditionalSurchargeInd" SkinID="GridViewDropDownListFooter"
                                    runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAdditionalSurcharge" runat="server" SkinID="LabelNormal" Text="Additional Surcharge %"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAdditinalSurchage" runat="server" Text='<%# Bind("Additional_Surcharge", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEntryTax" runat="server" SkinID="LabelNormal" Text="Entry tax %"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtEntryTax" runat="server" Text='<%# Bind("Entry_Tax", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblOctroi" runat="server" SkinID="LabelNormal" Text="Octroi %"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtOctroi" runat="server" Text='<%# Bind("Octroi", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCESSCalculationIndicator" runat="server" SkinID="LabelNormal" Text="CESS calculation indicator"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCESSCalculationIndicator" SkinID="GridViewDropDownListFooter"
                                    runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCess" runat="server" SkinID="LabelNormal" Text="CESS %"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCess" runat="server" Text='<%# Bind("CESS", "{0:0.00}") %>' SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblOtherLevies1" runat="server" SkinID="LabelNormal" Text="Other Levies #1 %"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtOtherLevies1" Text='<%# Bind("Other_levies_1", "{0:0.00}") %>'
                                    runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblOtherLevies2" runat="server" SkinID="LabelNormal" Text="Other Levies #2 %"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtOtherLevies2" Text='<%# Bind("Other_levies_2", "{0:0.00}") %>'
                                    runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblInsuranceIndicator" runat="server" SkinID="LabelNormal" Text="Insurance indicator"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlInsuranceIndicator" SkinID="GridViewDropDownListFooter"
                                    runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblInsurance" runat="server" SkinID="LabelNormal" Text="Insurance"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInsurance" Text='<%# Bind("Insurance", "{0:0.00}") %>' runat="server"
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblWarehouseSCIndicator" runat="server" SkinID="LabelNormal" Text="Warehouse SC indicator"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlWarehouseSCIndicator" SkinID="GridViewDropDownListFooter"
                                    runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblWarehouseSurcharge" runat="server" SkinID="LabelNormal" Text="Warehouse surcharge %"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtWarehouseSurcharge" Text='<%# Bind("Warehouse_Surcharge", "{0:0.00}") %>'
                                    runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFreightCalculationIndicator" runat="server" SkinID="LabelNormal"
                                    Text="Freight calculation indicator"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlFreightCalculationIndicator" SkinID="GridViewDropDownListFooter"
                                    runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblFreight" runat="server" SkinID="LabelNormal" Text="Freight"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFreight" runat="server" Text='<%# Bind("Freight", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTOTind" runat="server" SkinID="LabelNormal" Text="TOT ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlTOTind" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblTurnoverTax" runat="server" SkinID="LabelNormal" Text="Turnover tax"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTurnoverTax" runat="server" Text='<%# Bind("Turnover_Tax", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBonusCalculationInd" runat="server" SkinID="LabelNormal" Text="Bonus calculation ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBonusCalculationInd" SkinID="GridViewDropDownListFooter"
                                    runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblBonus" runat="server" SkinID="LabelNormal" Text="Bonus"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBonus" runat="server" Text='<%# Bind("Bonus", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCoupleInd" runat="server" SkinID="LabelNormal" Text="Coupon ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCoupleInd" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCouponCharges" runat="server" SkinID="LabelNormal" Text="Coupon charges"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCouponCharges" Text='<%# Bind("Coupon_charges", "{0:0.00}" ) %>'
                                    runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGrossProfit" runat="server" SkinID="LabelNormal" Text="Gross Profit"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtGrossProfit" Text='<%# Bind("Gross_Profit_Percentage", "{0:0.00}") %>'
                                    runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblTurnoverDiscount" runat="server" SkinID="LabelNormal" Text="Turnover discount"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtTurnoverDiscount" Text='<%# Bind("Turnover_Discount", "{0:0.00}") %>'
                                    runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSplTODInd" runat="server" SkinID="LabelNormal" Text="Spl TOD ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSplTODInd" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSpecialTurnoverDiscount" runat="server" SkinID="LabelNormal" Text="Special turnover discount"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSpecialTurnoverDiscount" Text='<%# Bind("Spl_Turnover_Discount", "{0:0.00}") %>'
                                    runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCalculationIndicator" runat="server" SkinID="LabelNormal" Text="Calculation indicator"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCalculationIndicator" SkinID="GridViewDropDownListFooter"
                                    runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblDealersDiscount" runat="server" SkinID="LabelNormal" Text="Dealers discount"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDealersDiscount" runat="server" Text='<%# Bind("Dealers_Discount", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblHCInd" runat="server" SkinID="LabelNormal" Text="HC ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlHCInd" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblHandlingCharges" runat="server" SkinID="LabelNormal" Text="Handling charges"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtHandlingCharges" runat="server" Text='<%# Bind("Handling_Charges_Percentage", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSLBRoundingOff" runat="server" SkinID="LabelNormal" Text="SLB rounding off ind."></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSLBRoundingOff" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSLBText" runat="server" SkinID="LabelNormal" Text="SLB"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSLBText" ReadOnly="true" runat="server" Text='<%# Bind("SLB_Value", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEDForSellingPriceInd" runat="server" SkinID="LabelNormal" Text="ED for selling price ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlEDForSellingPriceInd" SkinID="GridViewDropDownListFooter"
                                    runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblEDforSellingPrice" runat="server" SkinID="LabelNormal" Text="ED for selling price"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtEDforSellingPrice" runat="server" Text='<%# Bind("ED_for_Selling_price", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSPLDisc1Ind" runat="server" SkinID="LabelNormal" Text="Spl Disc1 ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSPLDisc1Ind" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSpecialDiscount1" runat="server" SkinID="LabelNormal" Text="Special discount #1"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="TxtSpecialDiscount1" runat="server" Text='<%# Bind("Special_Discount_1", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSPLDisc2Ind" runat="server" SkinID="LabelNormal" Text="Spl disc2 ind"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSPLDisc2Ind" SkinID="GridViewDropDownListFooter" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSpecialDiscount2" runat="server" SkinID="LabelNormal" Text="Special discount #2"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="TxtSpecialDiscount2" runat="server" Text='<%# Bind("Special_Discount_2", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCostPrice" runat="server" SkinID="LabelNormal" Text="Cost price"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCostPrice" ReadOnly="true" runat="server" Text='<%# Bind("Cost_Price", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSellingPrice" runat="server" SkinID="LabelNormal" Text="Selling price"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSellingPrice" ReadOnly="true" runat="server" Text='<%# Bind("Selling_Price", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegularGrossProfit" runat="server" SkinID="LabelNormal" Text="Regular gross profit %"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtRegularGrossProfit" ReadOnly="true" runat="server" Text='<%# Bind("Regular_GP", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblGP1" runat="server" SkinID="LabelNormal" Text="GP + TOD - Bonus"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtGP1" ReadOnly="true" runat="server" Text='<%# Bind("GP_TOD", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGP2" runat="server" SkinID="LabelNormal" Text="GP - TOD + Bonus"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtGP2" ReadOnly="true" runat="server" Text='<%# Bind("GP_Bonus", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblGP3" runat="server" SkinID="LabelNormal" Text="GP + TOD + Bonus"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtGP3" ReadOnly="true" runat="server" Text='<%# Bind("GP_TOD_BONUS", "{0:0.00}") %>'
                                    SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </asp:Panel>
            <%--<asp:Label ID="lblNoResult" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>--%>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="validate" Text="Submit"
                    SkinID="ButtonNormal" OnClick="BtnSubmit_Click" />
                <asp:Button ID="btnCheck" runat="server" Text="Check" SkinID="ButtonNormal" OnClientClick="javaScript:return fnValidateGrid(this);"
                    OnClick="btnCheck_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                <%--<asp:Button ID="btnReport" runat="server" Text="Report" SkinID="ButtonNormal" />--%>
            </div>
        </div>
    </div>
</asp:Content>
