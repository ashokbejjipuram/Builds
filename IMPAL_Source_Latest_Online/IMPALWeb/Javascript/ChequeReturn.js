var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvCashAndBankPayment_ctl02_";
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


window.onload = function() {
    window.history.forward(1);
}

function checkKey() {
    // Check for backSpace key    
    if (window.event.keyCode == 8) {
        return false; //aviod postback  
    }
}

function validateFutureDate(value) {
    var oSysDate = new Date();
    var oFromDate = value.split("/");
    var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];

    if (oSysDate < new Date(oFromDateFormatted)) {
        //alert(Caption + " should not be greater than System Date");
        //txtFromDate.focus();
        return false;
    }
}

function ValidateTransactionFields() {
    var TransactionDate = document.getElementById(CtrlIdPrefix + "txtTransactionDate").value;
    var Remarks = document.getElementById(CtrlIdPrefix + "txtRemarks").value;
    var ddlPayment = document.getElementById(CtrlIdPrefix + "ddlPayment").value;

    if (TransactionDate == "" || TransactionDate == null) {
        alert("Transaction Date should not be null");
        document.getElementById(CtrlIdPrefix + "txtTransactionDate").focus();
        return false;
    }

    if (ddlPayment == "0") {
        alert("Please Select the Payment Type");
        document.getElementById(CtrlIdPrefix + "ddlPayment").focus();
        return false;
    }

    if (Remarks == "" || Remarks == null) {
        alert("Remarks should not be null");
        document.getElementById(CtrlIdPrefix + "txtRemarks").focus();
        return false;
    }

    if (validatespl(document.getElementById(CtrlIdPrefix + "txtRemarks").value, "Remarks ")) {
        document.getElementById(CtrlIdPrefix + "txtRemarks").focus();
        return false;
    }

    if (validate(document.getElementById(CtrlIdPrefix + "txtChequeAmount").value, "Amount ")) {
        document.getElementById(CtrlIdPrefix + "txtChequeAmount").focus();
        return false;
    }

    if (document.getElementById(CtrlIdPrefix + "txtChequeAmount").value <= 0) {
        alert("Amount should be greater than zero");
        document.getElementById(CtrlIdPrefix + "txtChequeAmount").focus();
        return false;
    }

    if (document.getElementById(CtrlIdPrefix + "txtBankCharges").value == "") {
        alert("Bank Charges should not be Blank");
        document.getElementById(CtrlIdPrefix + "txtBankCharges").focus();
        return false;
    }

    if (!CurrencyChkForMoreThanOneDot(document.getElementById(CtrlIdPrefix + "txtBankCharges").value.trim())) {
        alert("Please enter valid Bank Charges");
        document.getElementById(CtrlIdPrefix + "txtBankCharges").focus();
        return false;
    }

    if (validate(document.getElementById(CtrlIdPrefix + "txtBankCharges").value, "Bank Charges ")) {
        document.getElementById(CtrlIdPrefix + "txtBankCharges").focus();
        return false;
    }    

    if (parseFloat(document.getElementById(CtrlIdPrefix + "txtBankCharges").value) < 0 || parseFloat(document.getElementById(CtrlIdPrefix + "txtBankCharges").value) > 1000) {
        alert("Bank Charges Should be between 0 to 1000 Rs.");
        document.getElementById(CtrlIdPrefix + "txtBankCharges").focus();
        return false;
    }

    if (parseFloat(document.getElementById(CtrlIdPrefix + "txtBankCharges").value) >= parseFloat(document.getElementById(CtrlIdPrefix + "txtChequeAmount").value)) {
        alert("Bank Charges Cannot be Equal or Exceed the Cheque Amount");
        return false;
    }

    if (validatespl(document.getElementById(CtrlIdPrefix + "txtRemarks").value, "Remarks ")) {
        document.getElementById(CtrlIdPrefix + "txtRemarks").focus();
        return false;
    }

    if (validate(document.getElementById(CtrlIdPrefix + "ddlLocalOutstation").value, "Local/Outstation ")) {
        document.getElementById(CtrlIdPrefix + "ddlLocalOutstation").focus();
        return false;
    }

    return true;
}

function GridValidate() {
    if (ValidateTransactionFields()) {
        var gvDrv = document.getElementById(CtrlIdPrefix + "grvChequeReturn");
        var TotRow = gvDrv.rows.length;

        if (TotRow > 2)
            return true;
        else {
            alert("No Details Found");
            return false;
        }
    }
    else
        return false;
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

function ValidateTransactionNumber() {
    var ddlTransactionNumber = document.getElementById(CtrlIdPrefix + "ddlTransactionNumber").value;
    if (ddlTransactionNumber == "" || ddlTransactionNumber == null) {
        alert("Select any Transaction Number to view its details");
        return false;
    }
    
    return true;
}