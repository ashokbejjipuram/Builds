var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvVendorPaymentDetails_ctl02_";
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
            //       case 8: // keycode for backspace
            //           event.returnValue = false;
            //           event.keyCode = 0;
            //           return false;
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

function checkDateForRefDocDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
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

function CheckChequeDate(id, isFutureDate, Msg) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            if (isFutureDate == true) {
                var fDate = idDate.split("/");
                var day = fDate[0];
                var month = fDate[1] - 1;
                var year = parseInt(fDate[2]);

                var frmDate = new Date();
                frmDate.setDate(day);
                frmDate.setMonth(month);
                frmDate.setFullYear(year);

                if (frmDate > new Date()) {
                    alert(Msg + " should not be greater than Today's Date");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
            }

            if (idDate.trim() != "") {
                var NoOfDays = getDaysBetweenDates(idDate.trim());
                if (NoOfDays > 90) {
                    alert("Cheque Date should not be less than 90 Days from Current Date");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
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
    //alert('hi');
    var CtrlID = CtrlIdPrefix + CalenderCtrlClientId;
    document.getElementById(CtrlID).click();
}

function TriggerGridCalender(GridCalenderCtrlClientId) {
    var CtrlID = GridCalenderCtrlClientId;
    document.getElementById(CtrlID).click();
}

function CurrencyChkForMoreThanOneDot(strValue) {
    //alert('asd');
    //alert(strValue.split('.').length);
    if (strValue.split('.').length > 2)
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
        alert('Future date can not be selected.');
        //sender._textbox.set_Value("");
        var dtDate = new Date();
        dtDate = dtDate.format(sender._format);
        sender._textbox.set_Value(dtDate);
        return false;
    }
    //From Date
    if (sender.get_id() == "ctl00_CPHDetails_CalendarExtender3") {
        if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtFromDate").value, document.getElementById(CtrlIdPrefix + "txtToDate").value)) {
            alert('From Date is greater than the To Date.');
            var dtDate = new Date();
            dtDate = dtDate.format(sender._format);
            sender._textbox.set_Value(dtDate);
            return false;

        }
    }
    //To Date
    else if (sender.get_id() == "ctl00_CPHDetails_CalendarExtender4") {
        if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtFromDate").value, document.getElementById(CtrlIdPrefix + "txtToDate").value)) {
            alert('From Date is greater than the To Date.');
            var dtDate = new Date();
            dtDate = dtDate.format(sender._format);
            sender._textbox.set_Value(dtDate);
            return false;

        }
    }
    return true;
}

function IsHigherDate(Date1, Date2) {
    var strDateArr1 = Date1.split('/');
    var strDateArr2 = Date2.split('/');

    var dtDate1 = new Date();
    var dtDate2 = new Date();
    dtDate1.setFullYear(strDateArr1[2], strDateArr1[1] - 1, strDateArr1[0]);
    dtDate2.setFullYear(strDateArr2[2], strDateArr2[1] - 1, strDateArr2[0]);

    //alert(dtDate1);
    //alert(dtDate2);
    //alert('dtDate1 is greater than dtDate2');

    if (dtDate1 > dtDate2)
        return true;
    else
        return false;

}

function funReset() {
    //if (confirm("You will loose the unsaved informations.\n\nAre you sure you want reset the page?"))
    return true;
    //else
    //  return false;
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

        if (currenyValue.value.split('.').length > 2) {
            document.getElementById(e1).value = Isvalid;
        }
        else {
            document.getElementById(e1).value = currenyValue.value;
        }
    }

    return true;
}

function getDaysBetweenDates(date) {

    var str1 = date.split('/');
    var date1 = new Date(str1[2], str1[1] - 1, str1[0]);
    var date2 = new Date();
    date1.setHours(0);
    date1.setMinutes(0, 0, 0);
    date2.setHours(0);
    date2.setMinutes(0, 0, 0);
    var datediff = Math.abs(date1.getTime() - date2.getTime()); // difference
    return parseInt(datediff / (24 * 60 * 60 * 1000));
}

function funModeOfPayment() {
    if (document.getElementById(CtrlIdPrefix + 'ddlModeOfPayment').value == 'H' || document.getElementById(CtrlIdPrefix + 'ddlModeOfPayment').value == 'D') {

        document.getElementById(CtrlIdPrefix + 'txtChequeNumber').value = "";
        document.getElementById(CtrlIdPrefix + 'txtChequeDate').value = "";
        document.getElementById(CtrlIdPrefix + 'txtBank').value = "";
        document.getElementById(CtrlIdPrefix + 'txtBranch').value = "";

        document.getElementById(CtrlIdPrefix + 'txtChequeNumber').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtChequeDate').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtBank').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtBranch').disabled = true;
    }
    else {
        document.getElementById(CtrlIdPrefix + 'txtChequeNumber').disabled = false;
        document.getElementById(CtrlIdPrefix + 'txtChequeDate').disabled = false;
        document.getElementById(CtrlIdPrefix + 'txtBank').disabled = false;
        document.getElementById(CtrlIdPrefix + 'txtBranch').disabled = false;
    }
}

function GetTotal() {
    totalPaymentAmount = 0.00;
    totalPaidAmount = 0.00;
    totalBookingAmount = 0.00;

    var grid = document.getElementById("ctl00_CPHDetails_grvVendorPaymentDetails");
    var inputs = grid.getElementsByTagName("input");
    var BookingAmountID = "";
    var PaymentAmountID = "";
    var BalanceAmountID = "";

    var PymentAmount = document.getElementById(CtrlIdPrefix + "txtAmount").value.trim();

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name.indexOf("ChkSelected") > 1) {
            var strId = inputs[i].id.split("_");

            BookingAmountID = "ctl00_CPHDetails_grvVendorPaymentDetails_" + strId[3] + "_txtBookingAmount";
            TDSAmountID = "ctl00_CPHDetails_grvVendorPaymentDetails_" + strId[3] + "_txtTDSAmount";
            PaidAmountID = "ctl00_CPHDetails_grvVendorPaymentDetails_" + strId[3] + "_txtPaidAmount";
            PaymentAmountID = "ctl00_CPHDetails_grvVendorPaymentDetails_" + strId[3] + "_txtPaymentAmount";

            if (inputs[i].checked) {
                //var BookingValue = document.getElementById(BookingAmountID).value.trim();
                //var PaidValue = document.getElementById(PaidAmountID).value.trim();
                var PymtValue = document.getElementById(PaymentAmountID).value.trim();
                //totalBookingAmount += parseFloat(BookingValue);
                //totalPaidAmount += parseFloat(PaidValue);
                totalPaymentAmount += parseFloat(PymtValue);
            }
        }
    }

    document.getElementById(CtrlIdPrefix + "txtTotalBalanceAmount").value = (parseFloat(PymentAmount) - parseFloat(totalPaymentAmount)).toFixed(2);
}

function funPaymentAmountValidation(ChkBox, BookAmount, TDSAmount, PaidAmount, PymtAmount) {
    var ChkBox1 = document.getElementById(ChkBox);
    var BookAmount1 = document.getElementById(BookAmount);
    var TDSAmount1 = document.getElementById(TDSAmount);
    var PaidAmount1 = document.getElementById(PaidAmount);
    var PymtAmount1 = document.getElementById(PymtAmount);
    
    if ((ChkBox1 != null) && (ChkBox1 != undefined)) {
        if (!ChkBox1.checked) {
            alert('Please select the Relavent Check Box.');
            ChkBox1.focus();
            return false;
        }

        if ((PymtAmount1 != null) && (PymtAmount1 != undefined) && (PymtAmount1.value != 0) && (PymtAmount1.value.trim() != "")) {
            if (!CurrencyChkForMoreThanOneDot(PymtAmount1.value)) {
                alert('Please enter valid Payment Amount.');
                PymtAmount1.focus();
                return false;
            }
            if (parseFloat(PymtAmount1.value) > (parseFloat(BookAmount1.value) - parseFloat(TDSAmount1.value) - parseFloat(PaidAmount1.value))) {
                alert('Payment Amount can not exceed ' + (parseFloat(BookAmount1.value) - parseFloat(TDSAmount1.value) - parseFloat(PaidAmount1.value)) + ' Amount.');
                PymtAmount1.value = "";
                PymtAmount1.focus();
                return false;
            }
        }
        else {
            alert('Please enter valid Payment Amount.');
            PymtAmount1.focus();
            return false;
        }

        GetTotal();
        return true;
    }
}

function GridCheck() {

    var hdnTxtCtrl = document.getElementById(CtrlIdPrefix + "txtHdnGridCtrls");

    var ctrlArr = hdnTxtCtrl.value.trim().split('|');
    //var IsAnyRowSelected = "False";

    var DocAmount = "";
    var CollAmount = "";
    var PaymentInd = "";

    for (var i = 0; i <= ctrlArr.length - 1; i++) {
        var CtrlInner = ctrlArr[i].split(',');
        if (CtrlInner[0].trim() != '') {
            if ((document.getElementById(CtrlInner[0]) != null) && (document.getElementById(CtrlInner[0]) != undefined)) {

                if (document.getElementById(CtrlInner[0]).checked) {
                    BookingAmount = document.getElementById(CtrlInner[1]).value.trim();
                    PaidAmount = document.getElementById(CtrlInner[3]).value.trim();
                    PaymentAmount = document.getElementById(CtrlInner[4]).value.trim();

                    if (PaymentAmount == "" || PaymentAmount == "0" || PaymentAmount == "0.00") {
                        //IsAnyRowSelected = "False";
                        alert('Payment Amount should not be empty or zero.');
                        document.getElementById(CtrlInner[2]).focus();
                        return false;
                    }
                    if (!CurrencyChkForMoreThanOneDot(PaymentAmount)) {
                        //IsAnyRowSelected = "False";
                        alert('Please enter valid Payment Amount.');
                        document.getElementById(CtrlInner[3]).focus();
                        return false;
                    }
                    if (parseFloat(PaymentAmount) > (parseFloat(BookingAmount) - parseFloat(PaidAmount))) {
                        //IsAnyRowSelected = "False";
                        alert('Payment Amount can not be higher than Booking Amount.');
                        document.getElementById(CtrlInner[3]).focus();
                        return false;
                    }

                    //IsAnyRowSelected = "True";                    
                }
            }
        }
    }
//    if (IsAnyRowSelected == "True") {
//        return true;
//    }
//    else {
//        alert("No Items has been selected.");
//        return false;
    //    }

    return true;
}

function funVendorHeaderValidation(ID, m) {
    var ddlBranch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBranch.options[ddlBranch.selectedIndex].value;
    var documentDate = document.getElementById(CtrlIdPrefix + "txtDocumentDate");
    var ddlAccountingPeriod = document.getElementById(CtrlIdPrefix + "ddlAccountingPeriod");
    var AccountingPeriod = ddlAccountingPeriod.options[ddlAccountingPeriod.selectedIndex].value;
    var ddlVendorCode = document.getElementById(CtrlIdPrefix + "ddlVendorCode");
    var VendorCode = ddlVendorCode.options[ddlVendorCode.selectedIndex].value;
    var Amount = document.getElementById(CtrlIdPrefix + "txtAmount");
    var Narration = document.getElementById(CtrlIdPrefix + "txtNarration");
    var ChartofAccount = document.getElementById(CtrlIdPrefix + "txtChartOfAccount");
    var ddlModeOfPayment = document.getElementById(CtrlIdPrefix + "ddlModeOfPayment");
    var ModeOfPayment = ddlModeOfPayment.options[ddlModeOfPayment.selectedIndex].value;
    var ChequeNumber = document.getElementById(CtrlIdPrefix + "txtChequeNumber");
    var ChequeDate = document.getElementById(CtrlIdPrefix + "txtChequeDate");
    var Bank = document.getElementById(CtrlIdPrefix + "txtBank");
    var BankBranch = document.getElementById(CtrlIdPrefix + "txtBranch");
	var ddlVendorCodeOthers = document.getElementById(CtrlIdPrefix + "ddlVendorCodeOthers");
    var VendorCodeOthers = ddlVendorCodeOthers.options[ddlVendorCodeOthers.selectedIndex].value;																						
    if (Branch.trim() == "") {
        alert('Branch Should not be empty.');
        ddlBranch.focus();
        return false;
    }
    
    if (documentDate.value.trim() == "") {
        alert("Document Date should not be empty.");
        documentDate.value = "";
        documentDate.focus();
        return false;
    }
    
    if (AccountingPeriod == "0") {
        alert('Please select an Accounting Period.');
        ddlAccountingPeriod.focus();
        return false;
    }
	if (ddlVendorCodeOthers.selectedIndex <= 0) {
        if (VendorCode.trim() == "0") {
            alert("Please Select Vendor.");
            ddlVendorCode.focus();
            return false;
        }
    }

    if (ddlVendorCode.selectedIndex <= 0) {
        if (VendorCodeOthers.trim() == "") {
            alert("Please Select Other Branch Vendor.");
            ddlVendorCodeOthers.focus();
            return false;
        }
    }

    if (Amount.value.trim() == "" || Amount.value.trim() == "0" || Amount.value.trim() == "0.00") {
        alert("Amount should not be empty.");
        Amount.focus();
        return false;
    }

    if (Narration.value.trim() == "") {
        alert("Narration should not be empty.");
        Narration.focus();
        return false;
    }

    if (!validatespl(Narration.value, "Narration")) {
    }
    else {
        Narration.focus();
        return false
    }

    if (ChartofAccount.value.trim() == "") {
        alert("Chart of Account should not be empty.");
        ChartofAccount.focus();
        return false;
    }

    if (ModeOfPayment.trim() == "") {
        alert("Please Select Mode Of Payment.");
        ddlModeOfPayment.focus();
        return false;
    }

    var controlName = '';

    if (ModeOfPayment == "D")
        controlName = 'Draft';

    if (ModeOfPayment == "Q")
        controlName = 'Cheque';

    if (ModeOfPayment == "Q") {
        if (ChequeNumber.value.trim() == "") {
            alert(controlName + " Number should not be empty.");
            ChequeNumber.value = "";
            ChequeNumber.focus();
            return false;
        }

        if (ChequeDate.value.trim() == "") {
            alert(controlName + " Date should not be empty.");
            ChequeDate.value = "";
            ChequeDate.focus();
            return false;
        }

        if (ChequeDate.value.trim() != "") {
            var NoOfDays = getDaysBetweenDates(ChequeDate.value.trim());
            if (NoOfDays > 90) {
                alert("Cheque Date should not be less than 90 Days from Current Date");
                ChequeDate.focus();
                return false;
            }
        }

        if (Bank.value.trim() == "") {
            alert("Bank should not be empty.");
            Bank.value = "";
            Bank.focus();
            return false;
        }

        if (BankBranch.value.trim() == "") {
            alert("Branch should not be empty.");
            BankBranch.value = "";
            BankBranch.focus();
            return false;
        }
    }

    if (m == 1) {
        var RowChecked = 0;
        var grid = document.getElementById("ctl00_CPHDetails_grvVendorPaymentDetails");
        var inputs = grid.getElementsByTagName("input");

        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].name.indexOf("ChkSelected") > 1) {
                if (inputs[i].checked) {
                    RowChecked += 1;
                }
            } 
        }

        document.getElementById(CtrlIdPrefix + "hdnRowCnt").value = RowChecked;
        
        __doPostBack(ID, 0);
        return true;
    }

    return true;
}

function ValidatePaymentNumber() {

    var ddlPaymentNumber = document.getElementById(CtrlIdPrefix + "ddlDocumentNumber");
    var PaymentNumber = ddlPaymentNumber.options[ddlPaymentNumber.selectedIndex].value;

    if (PaymentNumber.trim() == "-- Select --") {
        alert("Select a Payment Number");
        return false;
    }
    
    return true;
}

function funVendorSubmitValidation(m) {
    if (funVendorHeaderValidation(0, m)) {
        var grd = document.getElementById(CtrlIdPrefix + 'grvVendorPaymentDetails');
        if (grd != null) {
            if (m == "2") {
                if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "" || document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
                    alert('No Item details are found.');
                    return false;
                }
            }

            if (GridCheck()) {
                var Amount = document.getElementById(CtrlIdPrefix + "txtAmount").value.trim();
                var TotalBalAmount = document.getElementById(CtrlIdPrefix + "txtTotalBalanceAmount").value.trim();
                if (parseFloat(TotalBalAmount) == 0) {
                    return true;
                }
                else {
                    alert("Payment Amount does not Match with the Grid Values Adjustment.");
                    return false;
                }
            }
            else
                return false;
        }
    }
    else
        return false;
}

function CheckGreaterthanZero(id, name) {
    var idValue = document.getElementById(id).value;

    if (parseInt(idValue) == 0) {
        alert(name + " should be greater then zero");
        document.getElementById(id).value = "";
        document.getElementById(id).focus();
        return false;
    }
}

function CheckLessthanZero(id, name) {
    var idValue = document.getElementById(id).value;

    if (parseInt(idValue) <= 0) {
        alert(name + " should not be less then zero");
        document.getElementById(id).value = "";
        document.getElementById(id).focus();
        return false;
    }
}

