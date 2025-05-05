<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="CustomerCreditLimit.aspx.cs"
    Inherits="IMPALWeb.CustomerCreditLimit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function FnValidate() {
            var ddlBranch = document.getElementById('ctl00_CPHDetails_ddlBranch');
            var ddlCustomer = document.getElementById('ctl00_CPHDetails_ddlCustomer');

            if (ddlBranch.value.trim() == "" || ddlBranch.value.trim() == "0" || ddlBranch.value.trim() == null) {
                alert('Please Select the Branch');
                ddlBranch.focus();
                return false;
            }

            if (ddlCustomer.value.trim() == "" || ddlCustomer.value.trim() == null) {
                alert('Please Select the Customer');
                ddlCustomer.focus();
                return false;
            }

            return CheckValidDateCrLimit();
        }

        function CreditLimitChange() {
            var OldCrLimit = document.getElementById('ctl00_CPHDetails_hdnCreditLimit').value;
            var NewCrLimit = document.getElementById('ctl00_CPHDetails_txtCreditLimit').value;
            var ValiditityInd = document.getElementById('ctl00_CPHDetails_ddlValidityIndicator');

            if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit))
                ValiditityInd.disabled = false;
            else
                ValiditityInd.disabled = true;
        }

        function CreditLimitValidity() {
            var DueDate = document.getElementById('ctl00_CPHDetails_txtCrlimitDueDate');
            var OldCrLimit = document.getElementById('ctl00_CPHDetails_hdnCreditLimit').value;
            var NewCrLimit = document.getElementById('ctl00_CPHDetails_txtCreditLimit').value;
            var ValiditityInd = document.getElementById('ctl00_CPHDetails_ddlValidityIndicator').value;

            if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd == "T")
                DueDate.disabled = false;
            else
                DueDate.disabled = true;
        }

        function CheckValidDateCrLimit() {
            var OldCrLimit = document.getElementById('ctl00_CPHDetails_hdnCreditLimit').value;
            var NewCrLimit = document.getElementById('ctl00_CPHDetails_txtCreditLimit').value;
            var ValiditityInd = document.getElementById('ctl00_CPHDetails_ddlValidityIndicator');
            var DueDate = document.getElementById('ctl00_CPHDetails_txtCrlimitDueDate');

            if (parseFloat(OldCrLimit) == parseFloat(NewCrLimit)) {
                alert('There is No Change in the Existing Credit Limit');
                return false;
            }

            if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "") {
                alert('Please Select Validity Indicator for Credit Limit Change');
                ValiditityInd.focus();
                return false;
            }

            if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T") {
                if (DueDate.value.trim() == "") {
                    alert('Please Select Validity Date for Credit Limit Change');
                    DueDate.focus();
                    return false;
                }

                if (fnIsDate(DueDate.value.trim()) == false) {
                    DueDate.focus();
                    return false;
                }
            }

            if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T" && DueDate.value.trim() != "") {
                var oSysDate = new Date();
                var oDate = DueDate.value.trim().split("/");

                var oCurDateFormatted = (oSysDate.getMonth() + 1) + '/' + oSysDate.getDate() + '/' + oSysDate.getFullYear();
                var oDateFormatted = oDate[1] + "/" + oDate[0] + "/" + oDate[2];

                if (new Date(oCurDateFormatted) > new Date(oDateFormatted)) {
                    alert("Validity Date should be greater than or equal to System Date");
                    DueDate.value = "";
                    DueDate.focus();
                    return false;
                }
            }
        }
    </script>

    <div class="reportFormTitle">
        Customer Details
    </div>
    <div class="reportFilters">
        <asp:UpdatePanel ID="upCusOSQuery" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlCustomer" />
            </Triggers>
            <ContentTemplate>
                <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                    <tr>
                        <td>
                            <table id="reportFiltersTable1" class="reportFiltersTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBranchCode" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode" AutoPostBack="true"
                                            TabIndex="1" SkinID="DropDownListNormal" OnSelectedIndexChanged="ddlBranch_OnSelectedIndexChanged" />
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblCustomer" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                            SkinID="DropDownListNormal" TabIndex="2" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                        </asp:DropDownList>
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
                                            <asp:Label runat="server" ID="lblCustomerCode" Text="Customer Code" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label Text="Address1" SkinID="LabelNormal" runat="server" ID="lblAddress1" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblAddress2" Text="Address2" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblAddress3" Text="Address3" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label Text="Address4" SkinID="LabelNormal" runat="server" ID="lblAddress4" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblLocation" Text="Location" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblGSTIN" Text="GSTIN No" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtGSTIN" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPinCode" Text="Pin Code" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtPinCode" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPhone" Text="Phone" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtPhone" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divOSdetails" style="display: none" runat="server">
                                <div class="reportFormTitle">
                                    Credit Limit Details
                                </div>
                                <table class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="Label1" Text="Credit Limit" SkinID="LabelNormal" />
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCreditLimit" runat="server" SkinID="TextBoxNormalBig" onchange="return CreditLimitChange()" />
                                            <asp:HiddenField ID="hdnCreditLimit" runat="server" />
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="Label3" runat="server" SkinID="LabelNormal" Text="Validity Indicator"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlValidityIndicator" runat="server" SkinID="DropDownListNormalBig" Enabled="false" onchange="return CreditLimitValidity()">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                <asp:ListItem Value="T">Temporary</asp:ListItem>
                                                <asp:ListItem Value="P">Permanent</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="Validity Date"></asp:Label><span
                                                class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtCrlimitDueDate" runat="server" SkinID="TextBoxCalendarExtenderNormal" Enabled="false"
                                                onchange="return CheckValidDateCrLimit();"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceCrlimitDueDate" PopupButtonID="imgCrlimitDueDate"
                                                Format="dd/MM/yyyy" runat="server" TargetControlID="txtCrlimitDueDate" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="reportButtons">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Update" SkinID="ButtonNormal" OnClick="BtnSubmit_Click" OnClientClick="return FnValidate();" />
                    <asp:Button ID="BtnReset" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
