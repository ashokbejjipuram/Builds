<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="ChequeSlip.aspx.cs" Inherits="IMPALWeb.Transactions.Finance.Payable.ChequeSlip" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="Account" %>
<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function CalculateDiscAmount(cashdiscper, cdvaluebox, totvaluebox, calcatotamount, checked) {

            if (checked == 0) {
                var cdvalue;
                var rlength = 2;

                cdvalue = (document.getElementById(totvaluebox).value * document.getElementById(cashdiscper).value) / 100;
                cdvalue = Math.round(cdvalue * Math.pow(10, rlength)) / Math.pow(10, rlength);
                document.getElementById(cdvaluebox).value = cdvalue;
                document.getElementById(totvaluebox).value = (totvaluebox - cdvalue);
                document.getElementById(calcatotamount).value = document.getElementById(calcatotamount).value + (totvaluebox - cdvalue);
            }
            else if (checked == 1) {
                document.getElementById(cdvaluebox).value = 0;
                document.getElementById(totvaluebox).value = 0;
                document.getElementById(calcatotamount).value = (document.getElementById(calcatotamount).value - document.getElementById(totvaluebox).value);
            }
        }

        function CdValueOnBlur(rowid, CdValueClientId, chkValId, TotalValueId, calcAmountId, Invoiceval) {

            if (document.getElementById(chkValId).checked == true) {
                var calAmount = document.getElementById(calcAmountId).value;
                var cdValue = document.getElementById(CdValueClientId).value;
                var totVal = document.getElementById(TotalValueId).value;
                if ((totVal + cdValue) > Invoiceval) {
                    alert("Total value exceeds invoice value");
                    //                     TotalValueId.focus();
                    return false;
                }
                else {

                    calAmount = +calAmount + (totVal - cdValue);
                }
            }
            if (document.getElementById(chkValId).checked != true) {
                var calcAmount = document.getElementById(calcAmountId).value;
                calcAmount = "0.00";
                cdValue = "0.00";
                totVal = "0.00";
            }
        }

        function CalculateTotalValue(lnk) {

            var txtCalculatedAmount = document.getElementById('<%=txtCalculatedAmount.ClientID%>');
            var grdChequeDtl = document.getElementById("<%=grdChequeDtl.ClientID%>");
            var InvoiceValue = null;
            var txtCDValue = null;
            var txtTotalValue = null;
            var Check = null;
            //InvoiceValue = grdChequeDtl.Rows[linti].Cells[5].Text;
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            Check = row.cells[0].children[0];
            InvoiceValue = row.cells[5].lastChild.nodeValue;
            txtCDValue = row.cells[6].children[0];
            txtTotalValue = row.cells[7].children[0];

            //            if (Math.round(txtTotalValue.value + txtCDValue.value) > Math.round(InvoiceValue.value)) {
            //                alert("Total value exceeds invoice value")
            //                return false;
            //            }
            //             for (var x = 1; x < gvDrv.rows.length - 1; x++) {

            //                var row = gvDrv.rows[x]
            if (Check.checked) {
                txtTotalValue.value = (InvoiceValue - txtCDValue.value)
                txtTotalValue.value = round(txtTotalValue.value, 2);
            }
            CalculateAmount();
        }

        function fnShowHideBtns() {
            window.document.forms[0].target = '_blank';
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";            
        }

        function CalculateAmount() {
            var txtCalculatedAmount = document.getElementById('<%=txtCalculatedAmount.ClientID%>');
            var grdChequeDtl = document.getElementById("<%=grdChequeDtl.ClientID%>");
            var txtTotalValue = null;
            var CalculatedAmount = 0.0;
            var Check = null;

            for (var x = 1; x < grdChequeDtl.rows.length - 1; x++) {

                var row = grdChequeDtl.rows[x]
                Check = row.cells[0].children[0];
                txtTotalValue = row.cells[7].children[0];

                if (Check.checked) {
                    CalculatedAmount = CalculatedAmount + parseFloat(txtTotalValue.value);
                }
            }
            txtCalculatedAmount.value = CalculatedAmount;
        }

        function round(value, exp) {
            if (typeof exp === 'undefined' || +exp === 0)
                return Math.round(value);

            value = +value;
            exp = +exp;

            if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0))
                return NaN;

            // Shift
            value = value.toString().split('e');
            value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp)));

            // Shift back
            value = value.toString().split('e');
            return +(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp));
        }

        function ValidateChequeSlip() {

            var CtrlIdPrefix = "ctl00_CPHDetails_";

            var cChequeSlip = document.getElementById(CtrlIdPrefix + "txtChequeSlipDate");
            if (cChequeSlip.value == "") {
                alert("ChequeSlip Date can't be left blank");
                cChequeSlip.focus();
                return false;
            }

            var cRemarks = document.getElementById(CtrlIdPrefix + "txtRemarks");
            if (cRemarks.value == "") {
                alert("Remarks should not be null");
                cRemarks.focus();
                return false;
            }

            if (validatespl(document.getElementById("<%=txtRemarks.ClientID%>").value, "Remarks ")) {
                document.getElementById("<%=txtRemarks.ClientID%>").select()
                return false;
            }


            //                   var cAmount = document.getElementById(CtrlIdPrefix + "txtAmount");
            //                   if (cAmount.value == "") {
            //                          alert("Amount can't be left blank");
            //                          cAmount.focus();
            //                           return false;
            //                     }

            var cChartOfAccount = document.getElementById(CtrlIdPrefix + "txtChartofAccount");
            if (cChartOfAccount.value == "") {
                alert("Please select chart of account");
                //cChartOfAccount.focus();
                return false;
            }

            var ctxtChequeNo = document.getElementById(CtrlIdPrefix + "txtChequeNo");
            if ((ctxtChequeNo.value == "") && !(ctxtChequeNo.disabled)) {
                alert("ChequeNo can't be left blank");
                ctxtChequeNo.focus();
                return false;
            }

            if (validatespl(document.getElementById("<%=txtChequeNo.ClientID%>").value, "Cheque Number ")) {
                document.getElementById("<%=txtChequeNo.ClientID%>").select()
                return false;
            }


            var ctxtChequeDate = document.getElementById(CtrlIdPrefix + "txtChequeDate");
            if ((ctxtChequeDate.value == "") && !(ctxtChequeDate.disabled)) {
                alert("Cheque Date can't be left blank");
                ctxtChequeDate.focus();
                return false;
            }
            if (ctxtChequeDate.value != "") {
                if (fnIsDate(ctxtChequeDate.value) == false) {
                    ctxtChequeDate.focus();
                    return false;
                }
            }

            var a = new Date(ctxtChequeDate.value);
            var b = GetTodayDate();

            if (a > b) {
                alert("Cheque Date should not be greater than System Date");
                ctxtChequeDate.focus();
                return false;
            }

            if (ctxtChequeDate.value != "") {
                var chequeDate = Date.parse(ctxtChequeDate.value);
                var sysDate = Date.parse(new Date());
                var timeDiff = chequeDate - sysDate;


                var daysDiff = Math.floor(timeDiff / (1000 * 60 * 60 * 24));


                if (daysDiff < 90) {
                    alert("Cheque Date should not be less than 90 Days from Current Date");
                    ctxtChequeDate.focus();
                    return false;
                }
            }
            if (CheckRefType() == false) {
                return false;
            }
            return checkItemsSelected();

        }
        //To check whether any items are selected in grid

        function checkItemsSelected() {
            var grdChequeDtl = document.getElementById("<%=grdChequeDtl.ClientID%>");
            var flag = false;
            for (var x = 1; x < grdChequeDtl.rows.length - 1; x++) {
                var row = grdChequeDtl.rows[x]
                Check = row.cells[0].children[0];
                if (Check.checked) {
                    flag = true;
                }
            }
            if (flag == false) {
                alert("Please select any items");
                return false;
            }
            else
                return true;
        }

        function fn_DateCompare(DateA, DateB) {
            var a = new Date(DateA);
            var b = new Date(DateB);

            var msDateA = Date.UTC(a.getFullYear(), a.getMonth() + 1, a.getDate());
            var msDateB = Date.UTC(b.getFullYear(), b.getMonth() + 1, b.getDate());

            if (parseFloat(msDateA) < parseFloat(msDateB))
                return -1;  // less than
            else if (parseFloat(msDateA) == parseFloat(msDateB))
                return 0;  // equal
            else if (parseFloat(msDateA) > parseFloat(msDateB))
                return 1;  // greater than
            else
                return null;  // error
        }

        function GetTodayDate() {

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            today = mm + '/' + dd + '/' + yyyy;
            return today;
        }
        //To validate Chequeslip number in view mode
        function ValidateChequeslipNumber() {

            var ddlChequeSlipNo = document.getElementById("<%=ddlChequeSlipNo.ClientID%>").value;
            if (ddlChequeSlipNo == "0") {
                alert("Select any ChequeSlip Number to view its details");
                return false;
            }

            return true;
        }
        //To validate Date controls
        function CheckDates() {
            var txtFromDate = document.getElementById(CtrlIdPrefix + "txtFromdateDate");
            var txtToDate = document.getElementById(CtrlIdPrefix + "txtToDate");
            var cddlRefType = document.getElementById(CtrlIdPrefix + "ddlRefType");
            if (ValidateDates(txtFromDate, txtToDate) == false) {
                cddlRefType.value = "";
                return false;
            }
            else {
                return true;
            }
        }

        //To validate Reference Type
        function CheckRefType() {
            var cddlRefType = document.getElementById(CtrlIdPrefix + "ddlRefType");
            var txtFromDate = document.getElementById(CtrlIdPrefix + "txtFromdateDate");
            var txtToDate = document.getElementById(CtrlIdPrefix + "txtToDate");
            var cddlSupplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");

            if (cddlSupplier.value == "0") {
                alert("Please select supplier");
                cddlRefType.value = "0";
                cddlSupplier.focus();
                return false;
            }
            else if (txtFromDate.value == "") {
                alert("From Date should not be null");
                cddlRefType.value = "0";
                txtFromDate.focus();
                return false;
            }
            else if (txtToDate.value == "") {
                alert("To Date should not be null");
                cddlRefType.value = "0";
                txtToDate.focus();
                return false;
            }
            else if (cddlRefType.value == "0") {
                alert("Please select Reference Type");
                cddlRefType.focus();
                return false;
            }
            return true;
        }

        function ValidateHeaderFields() {
            var CtrlIdPrefix = "ctl00_CPHDetails_";

            var cddlSupplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
            var cddlRefType = document.getElementById(CtrlIdPrefix + "ddlRefType");
            var txtFromDate = document.getElementById(CtrlIdPrefix + "txtFromdateDate");
            var txtToDate = document.getElementById(CtrlIdPrefix + "txtToDate");

            var cChequeSlip = document.getElementById(CtrlIdPrefix + "txtChequeSlipDate");
            if (cChequeSlip.value == "") {
                alert("ChequeSlip Date can't be left blank");
                cChequeSlip.focus();
                return false;
            }
            var cRemarks = document.getElementById(CtrlIdPrefix + "txtRemarks");
            if (cRemarks.value == "") {
                alert("Remarks should not be null");
                cddlSupplier.value = "0";
                cddlRefType.value = "0";
                txtFromDate.value = "";
                txtToDate.value = "";
                cRemarks.focus();
                return false;
            }
            if (validatespl(document.getElementById("<%=txtRemarks.ClientID%>").value, "Remarks ")) {
                document.getElementById("<%=txtRemarks.ClientID%>").select()
                return false;
            }
            var cChartOfAccount = document.getElementById(CtrlIdPrefix + "txtChartofAccount");
            if (cChartOfAccount.value == "") {
                alert("Please select chart of account");
                cddlSupplier.value = "0";
                cddlRefType.value = "0";
                txtFromDate.value = "";
                txtToDate.value = "";
                // cChartOfAccount.focus();
                return false;
            }

            var ctxtChequeNo = document.getElementById(CtrlIdPrefix + "txtChequeNo");
            if ((ctxtChequeNo.value == "") && !(ctxtChequeNo.disabled)) {
                alert("ChequeNo can't be left blank");
                cddlSupplier.value = "0";
                cddlRefType.value = "0";
                txtFromDate.value = "";
                txtToDate.value = "";
                ctxtChequeNo.focus();
                return false;
            }

            if (validatespl(document.getElementById("<%=txtChequeNo.ClientID%>").value, "Cheque Number ")) {
                document.getElementById("<%=txtChequeNo.ClientID%>").select()
                return false;
            }

            var ctxtChequeDate = document.getElementById(CtrlIdPrefix + "txtChequeDate");
            if ((ctxtChequeDate.value == "") && !(ctxtChequeDate.disabled)) {
                alert("Cheque Date can't be left blank");
                cddlSupplier.value = "0";
                cddlRefType.value = "0";
                txtFromDate.value = "";
                txtToDate.value = "";
                ctxtChequeDate.focus();
                return false;
            }
            if (ctxtChequeDate.value != "") {
                if (fnIsDate(ctxtChequeDate.value) == false) {
                    ctxtChequeDate.focus();
                    return false;
                }
            }

            var a = new Date(ctxtChequeDate.value);
            var b = GetTodayDate();

            if (a > b) {
                alert("Cheque Date should not be greater than System Date");
                ctxtChequeDate.focus();
                return false;
            }

            if (ctxtChequeDate.value != "") {
                var chequeDate = Date.parse(ctxtChequeDate.value);
                var sysDate = Date.parse(new Date());
                var timeDiff = chequeDate - sysDate;


                var daysDiff = Math.floor(timeDiff / (1000 * 60 * 60 * 24));


                if (daysDiff < 90) {
                    alert("Cheque Date should not be less than 90 Days from Current Date");
                    ctxtChequeDate.focus();
                    return false;
                }
            }
            return true;
        }
    </script>

    <div id="divMain" runat="server" style="width: 100%">
        <div id="divPartOne" runat="server">
            <asp:UpdatePanel ID="updPanelPartOne" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="subFormTitle">
                        CHEQUE SLIP
                    </div>
                    <table id="tblPartOne" class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblChequeSlipNo" runat="server" SkinID="LabelNormal" Text="Cheque Slip No"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeSlipNo" runat="server" SkinID="TextBoxDisabled" ReadOnly="true"></asp:TextBox>
                                <asp:DropDownList ID="ddlChequeSlipNo" runat="server" SkinID="DropDownListNormal"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlChequeSlipNo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblChequeSlipDate" runat="server" SkinID="LabelNormal" Text="Cheque Slip Date"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeSlipDate" runat="server" SkinID="TextBoxDisabled" Enabled="False"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBranchName" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRemarks" runat="server" SkinID="LabelNormal" Text="Remarks"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtRemarks" runat="server" SkinID="TextBoxNormal" Text=""></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblChartOfAccount" runat="server" SkinID="LabelNormal" Text="Chart of Account"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChartofAccount" runat="server" SkinID="TextBoxDisabledBig" Text=""
                                    ReadOnly="True"></asp:TextBox>
                                <Account:ChartAccount ID="ucChartofAccount" runat="server" Filter="047,049" DefaultBranch="true"
                                    OnSearchImageClicked="ucChartofAccount_SearchImageClicked" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblAmount" runat="server" SkinID="LabelNormal" Text="Amount"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAmount" runat="server" SkinID="TextBoxNormal" Text="" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="imgEditToggle" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnReport" />
                    <asp:PostBackTrigger ControlID="btnReportExcel" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updPanelPartTwo" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table id="tblPartTwo" class="subFormTable">
                        <tr>
                            <td>
                                <div class="subFormTitle">
                                    CHEQUE DETAILS
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblChequeNo" runat="server" SkinID="LabelNormal" Text="Cheque Number"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeNo" runat="server" SkinID="TextBoxNormal" MaxLength="6"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblChequeDate" runat="server" SkinID="LabelNormal" Text="Cheque date"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    Enabled="False"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtChequeDate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBank" runat="server" Text="Bank" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBank" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblBankBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtBranch" runat="server" Text="" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnReport" />
                    <asp:PostBackTrigger ControlID="btnReportExcel" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updDocumentDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table id="tblDocDetail" class="subFormTable">
                        <tr>
                            <td>
                                <div class="subFormTitle">
                                    DOCUMENT DETAILS
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplier" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplier" runat="server" SkinID="DropDownListNormal" onchange="return ValidateHeaderFields()">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCalculatedAmount" runat="server" SkinID="LabelNormal" Text="Calculated Amount"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCalculatedAmount" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" runat="server" Text="Ref. Document From Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromdateDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    onblur="return CheckValidDate(this.id, true,'From Date');"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromdateDate" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" runat="server" Text="Ref. Document To Date" SkinID="LabelNormal"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    onblur="return CheckValidDate(this.id, true,'From Date');"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImgToDt" runat="server"
                                    Format="dd/MM/yyyy" TargetControlID="txtToDate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRefType" runat="server" SkinID="LabelNormal" Text="Reference Type"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlRefType" runat="server" SkinID="DropDownListNormal" onchange="CheckRefType()"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlRefType_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Purchase Invoice" Value="SV"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCashDiscount" runat="server" SkinID="LabelNormal" Text="Cash Discount"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCashDiscount" runat="server" SkinID="DropDownListNormal"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCashDiscount_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnReport" />
                    <asp:PostBackTrigger ControlID="btnReportExcel" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updGridLineItem" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="gridViewScrollFullPage">
                        <div>
                            <asp:CheckBox ID="ChkHeader" Text="Select All" CssClass="labelSubTitle" runat="server"
                                AutoPostBack="true" OnCheckedChanged="ChkHeader_CheckedChanged" />
                        </div>
                        <asp:GridView ID="grdChequeDtl" runat="server" AutoGenerateColumns="False" SkinID="GridViewTransaction"
                            OnRowCreated="grdChequeDtl_RowCreated" AllowPaging="false">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Selected">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelected" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelected_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Indicator" HeaderText="Indicator" DataFormatString="{0}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BranchCode" HeaderText="Branch Code" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice #" />
                                <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="InvoiceValue" HeaderText="Invoice Value">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="CD Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCDValue" onblur="CalculateTotalValue(this);" onkeypress="return CurrencyNumberOnly();"
                                            runat="server" SkinID="GridViewTextBox" OnTextChanged="txtCDValue_TextChanged">0.00</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotalValue" runat="server" onkeypress="return CurrencyNumberOnly();"
                                            SkinID="GridViewTextBox">0.00</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormal" OnClick="BtnSubmit_Click"
                                TabIndex="12" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click"
                                TabIndex="13" />
                            <asp:Button ID="btnReport" runat="server" Text="PDF Report" SkinID="ButtonViewReport"
                                OnClick="btnReportPDF_Click" OnClientClick="javascript: return fnShowHideBtns();"
                                TabIndex="15" />
                            <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                                OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" />
                            <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSupplier" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlRefType" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCashDiscount" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlChequeSlipNo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnReport" />
                    <asp:PostBackTrigger ControlID="btnReportExcel" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
            <UC:CrystalReport runat="server" ID="crChequeSlip" OnUnload="crChequeSlip_Unload" />
        </div>
    </div>
    <input id="hdnFromDate" type="hidden" runat="server" />
    <input id="hdnToDate" type="hidden" runat="server" />
</asp:Content>
