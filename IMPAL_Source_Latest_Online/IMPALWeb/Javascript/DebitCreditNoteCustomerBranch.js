var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grdview_ctl02_";
var selectedItemCode = "";

document.onkeydown = function() {
    switch (event.keyCode) {
        case 116: //F5 button
            event.returnValue = false;
            event.keyCode = 0;
            return false;
        case 82: //R button
            if (event.ctrlKey) {
                event.returnValue = false;
                event.keyCode = 0;
                return false;
            }
    }
}

function CheckTransDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {
            var FutureDate = new Date();

            IdDate = convertDate(idDate);

            if (IdDate > FutureDate) {
                document.getElementById(id).value = "";
                alert("Future date can not be Entered.");
            }
        }
    }
}

function convertDate(id) {
    var date = id.split("/");
    var day = parseInt(date[0]);
    var month = parseInt(date[1] - 1);
    var year = parseInt(date[2]);

    var validationDate = new Date(year, month, day);
    
    return validationDate;
}

function checkDateForRefDocDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != "") {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {
            var idInvoiceDate = document.getElementById("ctl00_CPHDetails_txtReferenceDocumentDate").value

            var FutureDate = new Date();

            var InvDate = new Date();
            InvDate = convertDate(idInvoiceDate);

            if (InvDate > FutureDate) {
                document.getElementById(id).value = "";
                alert("Future date can not be Entered.");
            }
        }
    }
}

function CheckToday(sender, e) {
    var format = sender._todaysDateFormat;
    var date = new Date();
    var formateddate = date.format(format);
    var daycells = $get(sender.get_id() + "_body").getElementsByTagName("DIV");
    for (var i = 0; i < daycells.length; i++) {
        if (daycells[i].title.indexOf(formateddate) > -1) {
            daycells[i].style.backgroundColor = "Blue";
            daycells[i].style.color = "White";
            break;
        }
    }
}

function TriggerCalender(CalenderCtrlClientId) {
    var CtrlID = CtrlIdPrefix + CalenderCtrlClientId;
    document.getElementById(CtrlID).click();
}

function TriggerGridCalender(GridCalenderCtrlClientId) {
    var CtrlID = GridCalenderCtrlClientId;
    document.getElementById(CtrlID).click();
}

function CurrencyChkForMoreThanOneDot(strValue) {
    if (strValue.split(".").length > 2)
        return false;
    else
        return true;
}

function CurrencyNumberOnly() {
    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function IntegerValueOnly() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function AlphaNumericOnly() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
        event.returnValue = true;
    else
        event.returnValue = false;
}
function AlphaNumericOnlyWithSpace() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 32 || AsciiValue == 127 || AsciiValue == 46) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function AlphaNumericWithSlash() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 47) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function IsFutureDate(sender, args) {
    if (sender._selectedDate > new Date()) {
        alert("Future date can not be selected.");
        var dtDate = new Date();
        dtDate = dtDate.format(sender._format);
        sender._textbox.set_Value(dtDate);
        return false;
    }

    if (sender.get_id() == "ctl00_CPHDetails_CalendarExtender3") {
        if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtFromDate").value, document.getElementById(CtrlIdPrefix + "txtToDate").value)) {
            alert("From Date is greater than the To Date.");
            var dtDate = new Date();
            dtDate = dtDate.format(sender._format);
            sender._textbox.set_Value(dtDate);
            return false;

        }
    }

    else if (sender.get_id() == "ctl00_CPHDetails_CalendarExtender4") {
        if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtFromDate").value, document.getElementById(CtrlIdPrefix + "txtToDate").value)) {
            alert("From Date is greater than the To Date.");
            var dtDate = new Date();
            dtDate = dtDate.format(sender._format);
            sender._textbox.set_Value(dtDate);
            return false;

        }
    }
    return true;
}

function IsHigherDate(Date1, Date2) {
    var strDateArr1 = Date1.split("/");
    var strDateArr2 = Date2.split("/");

    var dtDate1 = new Date();
    var dtDate2 = new Date();
    dtDate1.setFullYear(strDateArr1[2], strDateArr1[1] - 1, strDateArr1[0]);
    dtDate2.setFullYear(strDateArr2[2], strDateArr2[1] - 1, strDateArr2[0]);

    if (dtDate1 > dtDate2)
        return true;
    else
        return false;

}

function funReset() {
    return true;
}

function validatespl(inpval, fldname) {
    var firstchr = inpval.substring(0, 1, inpval);
    if (firstchr == "") {
        alert("First character of " + fldname + " should not be blank");
        return true;
    }
    else if (isspecialchar(firstchr)) {
        alert("First character of " + fldname + " should be alphabet or number");
        return true;
    }

    for (i = 0; i < inpval.length; i++) {
        firstchr = inpval.substring(i, i + 1, inpval);
        if (firstchr == "") {
            alert("First character of " + fldname + " should not be blank");
            return true;
        }
        else if (isspecial1(firstchr)) {
            alert("Characters in " + fldname + " should be alphabet or number");
            return true;
        }
    }

    return false;
}

function CurrencyDecimalOnly(e1, evt) {

    var currenyValue = document.getElementById(e1);
    var Isvalid = parseFloat(currenyValue.value);

    if (Isvalid.toString() == "NaN") {
        document.getElementById(e1).value = "";
        document.getElementById(e1).focus();
        return false;
    }
    else {

        if (currenyValue.value.split(".").length > 2) {
            document.getElementById(e1).value = Isvalid;
        }
        else {
            document.getElementById(e1).value = currenyValue.value;
        }
    }

    return true;
}

function getDaysBetweenDates(date) {

    var str1 = date.split("/");
    var date1 = new Date(str1[2], str1[1] - 1, str1[0]);
    var date2 = new Date();
    date1.setHours(0);
    date1.setMinutes(0, 0, 0);
    date2.setHours(0);
    date2.setMinutes(0, 0, 0);
    var datediff = Math.abs(date1.getTime() - date2.getTime()); // difference
    return parseInt(datediff / (24 * 60 * 60 * 1000));
}

function ValidateDebitCreditFields() {
    if (validate(document.getElementById(CtrlIdPrefix + "ddlAccountPeriod").value, "Accounting Period ")) {
        document.getElementById(CtrlIdPrefix + "ddlAccountPeriod").focus()
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "txtDocumentDate").value, "Document Date ")) {
        document.getElementById(CtrlIdPrefix + "txtDocumentDate").focus()
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value, "DebitCreditNote ")) {
        document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").focus()
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "ddlSuppCustBranchInd").value, "Customer/Branch Indicator ")) {
        document.getElementById(CtrlIdPrefix + "ddlSuppCustBranchInd").focus()
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "ddlSuppCustBranch").value, "Customer/Branch ")) {
        document.getElementById(CtrlIdPrefix + "ddlSuppCustBranch").focus()
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "ddlTransactionType").value, "Transaction Type ")) {
        document.getElementById(CtrlIdPrefix + "ddlTransactionType").focus()
        return false;
    }

    if (!((document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value == "CA" && document.getElementById(CtrlIdPrefix + "ddlTransactionType").value == "751")
                    || document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value == "DA")) {
        if (validate(document.getElementById(CtrlIdPrefix + "txtDocumentNumber").value, "Document Number ")) {
            document.getElementById(CtrlIdPrefix + "txtDocumentNumber").focus()
            return false;
        }
        else if (parseInt(document.getElementById(CtrlIdPrefix + "txtDocumentNumber").value.length) != 5) {
            alert("Document Number should be 5 digit")
            document.getElementById(CtrlIdPrefix + "txtDocumentNumber").focus()
            return false;
        }
    }

    if (validate(document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value, "Reference Document Number ")) {
        document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").focus()
        return false;
    }
    else if (validatespl(document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value, "Reference Document Number ")) {
        document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").focus();
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").value, "Reference Document Date ")) {
        document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").focus()
        return false;
    }
    else if (doValidateDate(document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate"))) {
        document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").focus();
        return false;
    }
    else if (CompareDate(document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").value, document.getElementById(CtrlIdPrefix + "txtDocumentDate").value) == 1) {
        alert("Reference Document Date is greater than Document Date");
        document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").focus();
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "txtValue").value, "TurnOver Value ")) {
        document.getElementById(CtrlIdPrefix + "txtValue").focus();
        return false;
    }
    else if (document.getElementById(CtrlIdPrefix + "hdninterStateStatus").value != "1" && validate(document.getElementById(CtrlIdPrefix + "txtGSTValue").value, "GST Value ")) {
        document.getElementById(CtrlIdPrefix + "txtGSTValue").focus();
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "txtRemarks").value, "Remarks ")) {
        document.getElementById(CtrlIdPrefix + "txtRemarks").focus();
        return false;
    }
    else if (validatespl(document.getElementById(CtrlIdPrefix + "txtRemarks").value, "Remarks")) {
        document.getElementById(CtrlIdPrefix + "txtRemarks").focus();
        return false;
    }

    if (parseFloat(document.getElementById(CtrlIdPrefix + "txtValue").value) <= parseFloat(document.getElementById(CtrlIdPrefix + "txtGSTValue").value)) {
        alert("TurnOver Value Cannot be less than or Equal to GST Value");
        document.getElementById(CtrlIdPrefix + "txtValue").value = "0.00";
        document.getElementById(CtrlIdPrefix + "txtGSTValue").value = "0.00";
        document.getElementById(CtrlIdPrefix + "txtValue").focus();
        return false;
    }
}

function CalAdjustValue() {
    var txtCollectedAmount = document.getElementById(CtrlIdPrefix + "txtCollectedAmount");
    var hddreturnQntCA = document.getElementById(CtrlIdPrefix + "hddreturnQntCA").value;
    var gvDrv = document.getElementById(CtrlIdPrefix + "grdDA");
    var ddlTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType").value;
    var txtReturnvalue = null;
    var txtSGST = null;
    var txtCGST = null;
    var txtIGST = null;
    var txtUTGST = null;
    var txtValue = null;
    var txtReturnquantity = null;
    var CollectAmount = 0.0;
    var Check = null;
    var txtQuantity = null;

    for (var x = 1; x < gvDrv.rows.length - 1; x++) {
        var row = gvDrv.rows[x]
        Check = row.cells[0].children[0];
        txtQuantity = row.cells[2].children[0];
        txtValue = row.cells[3].children[0];
        txtReturnquantity = row.cells[4].children[0];
        txtReturnvalue = row.cells[5].children[0];
        txtSGST = row.cells[6].children[0];
        txtCGST = row.cells[7].children[0];
        txtIGST = row.cells[8].children[0];
        txtUTGST = row.cells[9].children[0];

        if (Check.checked) {
            if (hddreturnQntCA == "1") {
                if (!(ddlTransactionType == "651" || ddlTransactionType == "652" || ddlTransactionType == "654" || ddlTransactionType == "655" || ddlTransactionType == "656" || ddlTransactionType == "657" || ddlTransactionType == "658" || ddlTransactionType == "751" || ddlTransactionType == "752" || ddlTransactionType == "754" || ddlTransactionType == "755" || ddlTransactionType == "756" || ddlTransactionType == "757" || ddlTransactionType == "758")) {
                    txtSGST.value = 0;
                    txtCGST.value = 0;
                    txtIGST.value = 0;
                    txtUTGST.value = 0;
                }
                CollectAmount = CollectAmount + parseFloat(txtReturnvalue.value) + parseFloat(txtSGST.value) + parseFloat(txtCGST.value) + parseFloat(txtIGST.value) + parseFloat(txtUTGST.value);
            }
            else if (txtReturnquantity.value != "0")
                CollectAmount = CollectAmount + parseFloat(txtReturnvalue.value) + parseFloat(txtSGST.value) + parseFloat(txtCGST.value) + parseFloat(txtIGST.value) + parseFloat(txtUTGST.value);

            if (ddlTransactionType == "653" && txtReturnquantity.value == "0") {
                CollectAmount = CollectAmount + parseFloat(txtReturnvalue.value) + parseFloat(txtSGST.value) + parseFloat(txtCGST.value) + parseFloat(txtIGST.value) + parseFloat(txtUTGST.value);
            }
        }
    }

    txtCollectedAmount.value = round(CollectAmount, 4);
}

function CalculateTax(lnk, Flag) {
    var txtRefType = "";
    var txtCollectedAmount = document.getElementById(CtrlIdPrefix + "txtCollectedAmount");
    var ddlTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType").value;
    var ddlSuppCustBranchInd = document.getElementById(CtrlIdPrefix + "ddlSuppCustBranchInd").value;
    var txtReferenceDocNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value;
    var txtGSTAmount = document.getElementById(CtrlIdPrefix + "txtGSTValue");

    if (txtGSTAmount.value == "")
        txtGSTAmount.value = "0.00";

    if (txtReferenceDocNumber.length >= 3)
        txtRefType = txtReferenceDocNumber.slice(txtReferenceDocNumber.length - 3, txtReferenceDocNumber.length);

    var txtReturnvalue = null;
    var txtSGST = null;
    var txtCGST = null;
    var txtIGST = null;
    var txtUTGST = null;
    var txtValue = null;
    var txtReturnquantity = null;
    var CollectAmount = 0.0;
    var Check = null;
    var txtQuantity = null;

    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;

    Check = row.cells[0].children[0];
    txtQuantity = row.cells[2].children[0];
    txtValue = row.cells[3].children[0];
    txtReturnquantity = row.cells[4].children[0];
    txtReturnvalue = row.cells[5].children[0];
    txtSGST = row.cells[6].children[0];
    txtCGST = row.cells[7].children[0];
    txtIGST = row.cells[8].children[0];
    txtUTGST = row.cells[9].children[0];
    txtActualRetQty = row.cells[9].children[1];
    txtCouponCharges = row.cells[9].children[2];

    if (Math.round(txtReturnquantity.value) > (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value))) {
        alert("Return quantity cannot be greater than " + (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value)));
        //txtReturnquantity.value = (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value));
        return false;
    }

    if (Check.checked) {

        var streets = null;
        return PageMethods.GetCollection(function (Result) {
            if (Flag == 1) {
                if (ddlSuppCustBranchInd == "Customer" && txtRefType == "171")
                    txtReturnvalue.value = round((txtValue.value), 4);
                else
                    txtReturnvalue.value = round((txtValue.value * txtReturnquantity.value), 4);

                if (parseFloat(txtGSTAmount.value) == 0) {
                    txtSGST.value = "0.00";
                    txtCGST.value = "0.00";
                    txtIGST.value = "0.00";
                    txtUTGST.value = "0.00";
                }
                else {
                    txtSGST.value = round((parseFloat(txtReturnvalue.value) * Result[rowIndex].SGSTSalesPer) / 100, 4);
                    txtCGST.value = round((parseFloat(txtReturnvalue.value) * Result[rowIndex].CGSTSalesPer) / 100, 4);
                    txtIGST.value = round((parseFloat(txtReturnvalue.value) * Result[rowIndex].IGSTSalesPer) / 100, 4);
                    txtUTGST.value = round((parseFloat(txtReturnvalue.value) * Result[rowIndex].UTGSTSalesPer) / 100, 4);
                }
            }
            else {
                if (parseFloat(txtGSTAmount.value) == 0) {
                    txtSGST.value = "0.00";
                    txtCGST.value = "0.00";
                    txtIGST.value = "0.00";
                    txtUTGST.value = "0.00";
                }
                else {
                    txtSGST.value = round((parseFloat(txtReturnvalue.value) * Result[rowIndex].SGSTSalesPer) / 100, 4);
                    txtCGST.value = round((parseFloat(txtReturnvalue.value) * Result[rowIndex].CGSTSalesPer) / 100, 4);
                    txtIGST.value = round((parseFloat(txtReturnvalue.value) * Result[rowIndex].IGSTSalesPer) / 100, 4);
                    txtUTGST.value = round((parseFloat(txtReturnvalue.value) * Result[rowIndex].UTGSTSalesPer) / 100, 4);
                }
            }

            CalAdjustValue();

            if (ddlTransactionType != "656" && ddlTransactionType != "756" && ddlTransactionType != "658" && ddlTransactionType != "758") {
                if (ddlSuppCustBranchInd == "Customer" && txtRefType == "171") {
                    if (Math.round(txtReturnvalue.value) > 0 && (parseInt(txtReturnvalue.value) < parseInt(parseFloat(txtValue.value)))) {
                        alert("Return value cannot be less than " + parseInt(parseFloat(txtValue.value)));
                        //txtReturnquantity.value = txtActualRetQty.value;
                        return false;
                    }
                }
                else {
                    if (Math.round(txtReturnvalue.value) > 0 && (parseInt(txtReturnvalue.value) < parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value)))) {
                        alert("Return value cannot be less than " + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value)));
                        //txtReturnquantity.value = txtActualRetQty.value;
                        return false;
                    }
                }

                if (Math.round(txtReturnvalue.value) > 0 && (parseFloat(txtReturnvalue.value) > parseFloat(1 + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value))))) {
                    alert("Return value cannot be greater than " + parseFloat(1 + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value))));
                    //txtReturnquantity.value = txtActualRetQty.value;
                    return false;
                }
            }
        });
    }
}

function SelectedChange(lnk) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    GetValue(row, rowIndex);
    CalAdjustValue();
}

function GetValue(row, index) {
    var txtGSTAmount = document.getElementById(CtrlIdPrefix + "txtGSTValue");

    if (txtGSTAmount.value == "")
        txtGSTAmount.value = "0.00";

    var txtReturnvalue = null;
    var txtSGST = null;
    var txtCGST = null;
    var txtIGST = null;
    var txtUTGST = null;
    var txtValue = null;
    var txtReturnquantity = null;
    var CollectAmount = 0.0;
    var Check = null;
    var txtQuantity = null;

    Check = row.cells[0].children[0];
    txtQuantity = row.cells[2].children[0];
    txtValue = row.cells[3].children[0];
    txtReturnquantity = row.cells[4].children[0];
    txtReturnvalue = row.cells[5].children[0];
    txtSGST = row.cells[6].children[0];
    txtCGST = row.cells[7].children[0];
    txtIGST = row.cells[8].children[0];
    txtUTGST = row.cells[9].children[0];

    if (!Check.checked) {
        return PageMethods.GetCollection(function(Result) {
            txtReturnvalue.value = "0";

            if (parseFloat(txtGSTAmount.value) == 0) {
                txtSGST.value = "0.00";
                txtCGST.value = "0.00";
                txtIGST.value = "0.00";
                txtUTGST.value = "0.00";
            }
            else {
                txtSGST.value = round(Result[index].SGSTSalesPer, 4);
                txtCGST.value = round(Result[index].CGSTSalesPer, 4);
                txtIGST.value = round(Result[index].IGSTSalesPer, 4);
                txtUTGST.value = round(Result[index].UTGSTSalesPer, 4);
            }

            txtReturnquantity.value = round(Result[index].return_quantity, 4);
        });
    }
}

function round(value, exp) {
    if (typeof exp === "undefined" || +exp === 0)
        return Math.round(value);

    value = +value;
    exp = +exp;

    if (isNaN(value) || !(typeof exp === "number" && exp % 1 === 0))
        return NaN;

    // Shift
    value = value.toString().split("e");
    value = Math.round(+(value[0] + "e" + (value[1] ? (+value[1] + exp) : exp)));

    // Shift back
    value = value.toString().split("e");
    return +(value[0] + "e" + (value[1] ? (+value[1] - exp) : -exp));
}

function checkAmount(Ind) {
    var txtAmount = document.getElementById(CtrlIdPrefix + "txtValue");
    var txtGSTAmount = document.getElementById(CtrlIdPrefix + "txtGSTValue");
    var ObjGrid = null;
    var inputElements = null;
    var Type = document.getElementById(CtrlIdPrefix + "hdnTransType").value;
    var hdnChart = document.getElementById(CtrlIdPrefix + "hdnChart").value;
    var hdninterStateStatus = document.getElementById(CtrlIdPrefix + "hdninterStateStatus").value;
    var hddItemCode = document.getElementById(CtrlIdPrefix + "hddItemCode").value;
    var ddlTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType").value;
    var txtBranch = document.getElementById(CtrlIdPrefix + "txtBranchCode");
    var ddlSuppCustBranchInd = document.getElementById(CtrlIdPrefix + "ddlSuppCustBranchInd").value;
    var ddlDebitCreditNote = document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value;

    var txtRefType = "";
    var txtReferenceDocNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value;

    if (txtReferenceDocNumber.length >= 3)
        txtRefType = txtReferenceDocNumber.slice(txtReferenceDocNumber.length - 3, txtReferenceDocNumber.length);

    var ChartAccount = null;
    var Value = null;
    var Remarks = null;
    var ItemCode = null;
    var Quantity = null;
    var SGSTCode = null;
    var SGSTPer = null;
    var SGSTAmt = null;
    var CGSTCode = null;
    var CGSTPer = null;
    var CGSTAmt = null;
    var IGSTCode = null;
    var IGSTPer = null;
    var IGSTAmt = null;
    var UTGSTCode = null;
    var UTGSTPer = null;
    var UTGSTAmt = null;
    var SupplierPartNo = null;
    var CollectAmount = "0.00";
    var TotalItemTaxAmount = 0.0;
    var TotalReturnValue = 0.0;
    ObjGrid = document.getElementById(CtrlIdPrefix + "grdCA")

    if (Type != null) {
        if (Type == "1") {

            for (var x = 1; x < ObjGrid.rows.length - 1; x++) {

                var row = ObjGrid.rows[x]
                ChartAccount = row.cells[1].children[0];
                Value = row.cells[2].children[0];
                Remarks = row.cells[3].children[0];
                ItemCode = row.cells[4].children[0];
                Quantity = row.cells[5].children[0];

                if (txtBranch.value.toUpperCase() == "CHANDIGARH") {
                    UTGSTCode = row.cells[6].children[0];
                    UTGSTPer = row.cells[7].children[0];
                    UTGSTAmt = row.cells[8].children[0];
                }
                else {
                    SGSTCode = row.cells[6].children[0];
                    SGSTPer = row.cells[7].children[0];
                    SGSTAmt = row.cells[8].children[0];
                }

                CGSTCode = row.cells[9].children[0];
                CGSTPer = row.cells[10].children[0];
                CGSTAmt = row.cells[11].children[0];
                IGSTCode = row.cells[12].children[0];
                IGSTPer = row.cells[13].children[0];
                IGSTAmt = row.cells[14].children[0];
                SupplierPartNo = row.cells[15].children[0];

                if (ChartAccount.value == "") {
                    alert("ChartAccount is required");
                    return false;
                }

                if (Value.value == "") {
                    alert("Value is required");
                    return false;
                }

                if (Remarks.value == "") {
                    alert("Remarks is required");
                    return false;
                }

                if (hddItemCode == "1") {
                    if (ItemCode.value == "") {
                        alert("Item code is required");
                        return false;
                    }
                }
                
                if (parseFloat(txtGSTAmount.value) > 0) {
                    if (hdnChart == "1") {
                        if (txtBranch.value.toUpperCase() == "CHANDIGARH") {
                            if ((UTGSTPer.value == "0" || UTGSTPer.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                                alert("UTGST % is required");
                                return false;
                            }

                            if ((UTGSTAmt.value == "0" || UTGSTAmt.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                                alert("UTGST Amt is required");
                                return false;
                            }
                        }
                        else {
                            if ((SGSTPer.value == "0" || SGSTPer.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                                alert("SGST % is required");
                                return false;
                            }

                            if ((SGSTAmt.value == "0" || SGSTAmt.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                                alert("SGST Amt is required");
                                return false;
                            }
                        }

                        if ((CGSTPer.value == "0" || CGSTPer.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                            alert("CGST % is required");
                            return false;
                        }

                        if ((CGSTAmt.value == "0" || CGSTAmt.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                            alert("CGST Amt is required");
                            return false;
                        }

                        if (txtBranch.value.toUpperCase() == "CHANDIGARH") {
                            if ((UTGSTPer.value == "0" || UTGSTPer.value == "") && (CGSTPer.value == "0" || CGSTPer.value == "")) {
                                if (IGSTPer.value == "0" || IGSTPer.value == "") {
                                    alert("IGST % is required");
                                    return false;
                                }

                                if (IGSTAmt.value == "0" || IGSTAmt.value == "") {
                                    alert("IGST Amt is required");
                                    return false;
                                }
                            }
                        }
                        else {
                            if ((SGSTPer.value == "0" || SGSTPer.value == "") && (CGSTPer.value == "0" || CGSTPer.value == "")) {
                                if (IGSTPer.value == "0" || IGSTPer.value == "") {
                                    alert("IGST % is required");
                                    return false;
                                }

                                if (IGSTAmt.value == "0" || IGSTAmt.value == "") {
                                    alert("IGST Amt is required");
                                    return false;
                                }
                            }
                        }
                    }
                    else if (hdnChart == "0" && hdninterStateStatus == "2" && ddlSuppCustBranchInd.toUpperCase() == "BRANCH") {
                        if (IGSTPer.value == "0" || IGSTPer.value == "") {
                            alert("IGST % is required");
                            return false;
                        }

                        if (IGSTAmt.value == "0" || IGSTAmt.value == "") {
                            alert("IGST Amt is required");
                            return false;
                        }
                    }
                }

                if (txtBranch.value.toUpperCase() == "CHANDIGARH") {
                    CollectAmount = Math.round((parseFloat(CollectAmount) + parseFloat(Value.value) + parseFloat(CGSTAmt.value) + parseFloat(IGSTAmt.value) + parseFloat(UTGSTAmt.value)) * 100) / 100;
                }
                else {
                    CollectAmount = Math.round((parseFloat(CollectAmount) + parseFloat(Value.value) + parseFloat(SGSTAmt.value) + parseFloat(CGSTAmt.value) + parseFloat(IGSTAmt.value)) * 100) / 100;
                }
            }

            var hdntxtRoundoff = document.getElementById(CtrlIdPrefix + "hdntxtRoundoff");

            if (ddlDebitCreditNote.toUpperCase() == "CA") {
                hdntxtRoundoff.value = Math.round(((-1) * ((parseFloat(txtAmount.value) + parseFloat(txtGSTAmount.value)) - parseFloat(CollectAmount))) * 10000) / 10000;
            }
            else {
                hdntxtRoundoff.value = Math.round(((parseFloat(txtAmount.value) + parseFloat(txtGSTAmount.value)) - parseFloat(CollectAmount)) * 10000) / 10000;
            }

            if (Ind == 1) {
//                if (parseFloat(hdntxtRoundoff.value) > 0.99 || parseFloat(hdntxtRoundoff.value) < -0.99) {
//                    alert("Amount Differ Check the Amount");
//                    return false;
//                }
//                else {
//                    return true;
//                }
            }
            else {
                if (parseFloat(hdntxtRoundoff.value) > 0.99 || parseFloat(hdntxtRoundoff.value) < -0.99) {
                    alert("Amount Differ Check the Amount");
                    return false;
                }
            }
        }
        else {
            ObjGrid = document.getElementById(CtrlIdPrefix + "grdDA")
            for (var x = 1; x < ObjGrid.rows.length - 1; x++) {

                var row = ObjGrid.rows[x]
                Check = row.cells[0].children[0];
                if (Check.checked) {
                    txtQuantity = row.cells[2].children[0];
                    txtValue = row.cells[3].children[0];
                    txtReturnquantity = row.cells[4].children[0];
                    txtReturnvalue = row.cells[5].children[0];
                    txtSGST = row.cells[6].children[0];
                    txtCGST = row.cells[7].children[0];
                    txtIGST = row.cells[8].children[0];
                    txtUTGST = row.cells[9].children[0];
                    txtActualRetQty = row.cells[9].children[1];

                    if (Math.round(txtReturnquantity.value) > (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value))) {
                        alert("Return quantity cannot be greater than " + (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value)));
                        //txtReturnquantity.value = (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value));
                        return false;
                    }

                    if (ddlTransactionType != "656" && ddlTransactionType != "756" && ddlTransactionType != "658" && ddlTransactionType != "758") {
                        if (ddlSuppCustBranchInd == "Customer" && txtRefType == "171") {
                            if (Math.round(txtReturnvalue.value) > 0 && (parseInt(txtReturnvalue.value) < parseInt(parseFloat(txtValue.value)))) {
                                alert("Return value cannot be less than " + parseInt(parseFloat(txtValue.value)));
                                //txtReturnquantity.value = txtActualRetQty.value;
                                return false;
                            }
                        }
                        else {
                            if (Math.round(txtReturnvalue.value) > 0 && (parseInt(txtReturnvalue.value) < parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value)))) {
                                alert("Return value cannot be less than " + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value)));
                                //txtReturnquantity.value = txtActualRetQty.value;
                                return false;
                            }
                        }

                        if (Math.round(txtReturnvalue.value) > 0 && (parseFloat(txtReturnvalue.value) > parseFloat(1 + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value))))) {
                            alert("Return value cannot be greater than " + parseFloat(1 + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value))));
                            //txtReturnquantity.value = txtActualRetQty.value;
                            return false;
                        }
                    }

                    TotalReturnValue = TotalReturnValue + parseFloat(txtReturnvalue.value);
                    TotalItemTaxAmount = TotalItemTaxAmount + parseFloat(txtSGST.value) + parseFloat(txtCGST.value) + parseFloat(txtIGST.value) + parseFloat(txtUTGST.value);
                }
            }
            
            var TotalAmount = parseInt(document.getElementById(CtrlIdPrefix + "txtTotalAmount").value);
            var CollectedAmount = parseInt(document.getElementById(CtrlIdPrefix + "txtCollectedAmount").value);
            var hdntxtRoundoff = document.getElementById(CtrlIdPrefix + "hdntxtRoundoff");

            if ((parseFloat(txtAmount.value) - parseFloat(TotalReturnValue)) > 0.49 || (parseFloat(txtAmount.value) - parseFloat(TotalReturnValue)) < -0.49) {
                alert("Header TurnOver Amount Differs With Grid Return Value Amount");
                return false;
            }

            if ((parseFloat(txtGSTAmount.value) - parseFloat(TotalItemTaxAmount)) > 0.49 || (parseFloat(txtGSTAmount.value) - parseFloat(TotalItemTaxAmount)) < -0.49) {
                alert("Header GST Amount Differs With Grid GST Amount");
                return false;
            }

            hdntxtRoundoff.value = Math.round(((-1) * (parseFloat(document.getElementById(CtrlIdPrefix + "txtTotalAmount").value) - parseFloat(document.getElementById(CtrlIdPrefix + "txtCollectedAmount").value))) * 10000) / 10000;

            //if ((TotalAmount > CollectedAmount) || (TotalAmount < CollectedAmount)) {
            if (parseFloat(hdntxtRoundoff.value) > 0.99 || parseFloat(hdntxtRoundoff.value) < -0.99) {
                alert("Amount Differ Check the Amount");
                return false;
            }
        }
    }
}

function GetDocumentDate() {
    var BranchCode = document.getElementById(CtrlIdPrefix + "hdnBranchCode").value;
    var DocumentNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value;
    var CustBrCode = document.getElementById(CtrlIdPrefix + "ddlSuppCustBranch").value;
    var ddlSuppCustBranchInd = document.getElementById(CtrlIdPrefix + "ddlSuppCustBranchInd").value;
    var filePath = document.getElementById(CtrlIdPrefix + "hdnpath").value + "/LoadDocumentDate.ashx";
    var ddlDebitCreditNote = document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value;
    var ddlTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType").value;
    var txtRefDocumnetDate = document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate");

    if (DocumentNumber.trim() == "") {
        txtRefDocumnetDate.value = "";
        txtRefDocumnetDate.disabled = false;
    }
    else {
        if ((ddlDebitCreditNote == "CA" && ddlTransactionType != "651" && ddlTransactionType != "751") ||
            (ddlDebitCreditNote == "CA" && ddlSuppCustBranchInd == "Branch" && (ddlTransactionType == "651" || ddlTransactionType == "751")) ||
            (ddlDebitCreditNote == "DA" && ddlSuppCustBranchInd == "Customer")) {

            var CustBranchInd;
            if (ddlSuppCustBranchInd == "Customer") {
                CustBranchInd = "1";
            }
            else {
                CustBranchInd = "2";
            }

            $.post(filePath + "?BranchCode=" + BranchCode + "&DocumentNumber=" + DocumentNumber + "&CustBrCode=" + CustBrCode + "&CustBranchInd=" + CustBranchInd, function(result) {
                var streets = eval(result);
                ValidateRefDocDate(streets);
            });
        }
    }
}

function ValidateRefDocDate(streets) {
    var txtRefDocumnetDate = document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate");
    var DocumentNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber");
    var hddrefdate = document.getElementById(CtrlIdPrefix + "hddrefdate");
    var hddrefStatus = document.getElementById(CtrlIdPrefix + "hddrefStatus");
    var ddlDebitCreditNote = document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value;
    var ddlSuppCustBranchInd = document.getElementById(CtrlIdPrefix + "ddlSuppCustBranchInd").value;

    hddrefStatus.value = streets.RefCode;

    if (streets.RefCode == 0) {
        txtRefDocumnetDate.value = streets.RefDate;
        hddrefdate.value = "1";
        txtRefDocumnetDate.disabled = true;
    }
    else if (streets.RefCode == 1) {
        if (!(ddlDebitCreditNote == "DA" && ddlSuppCustBranchInd == "Customer")) {
            if (DocumentNumber.value != "") {
                alert("Reference document number not available for this customer");
                txtRefDocumnetDate.value = "";
                DocumentNumber.value = "";
                DocumentNumber.focus();
                hddrefdate.value = "0";
                txtRefDocumnetDate.disabled = false;
            }
        }
        else {
            txtRefDocumnetDate.value = "";
            txtRefDocumnetDate.disabled = false;
        }
    }
    else if (streets.RefCode == 2) {
        if (DocumentNumber.value != "") {
            alert("Reference document number not available for this  branch");
            txtRefDocumnetDate.value = "";
            DocumentNumber.value = "";
            DocumentNumber.focus();
            hddrefdate.value = "0";
            txtRefDocumnetDate.disabled = false;
        }
    }
    else if (streets.RefCode == 3) {
        if (DocumentNumber.value != "") {
            alert("Already credit note passed for this reference document number");
            txtRefDocumnetDate.value = "";
            DocumentNumber.value = "";
            DocumentNumber.focus();
            hddrefdate.value = "0";
            txtRefDocumnetDate.disabled = false;
        }
    }
}