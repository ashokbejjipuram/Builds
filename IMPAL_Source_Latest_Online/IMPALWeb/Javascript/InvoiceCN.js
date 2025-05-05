var CtrlIdPrefix = "ctl00_CPHDetails_";

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

function ValidationSubmit(id) {
    if (id == 1) {
        var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
        if (supplier.value == "0") {
            alert("Supplier should not be null");
            supplier.focus();
            return false;
        }

        var branch = document.getElementById(CtrlIdPrefix + "ddlBranch");
        if (branch.value == "0") {
            alert("Branch should not be null");
            branch.focus();
            return false;
        }

        var invoicenumber = document.getElementById(CtrlIdPrefix + "txtInvoiceNumber");
        if (invoicenumber.value == "") {
            alert("Invoice Number should not be null");
            invoicenumber.focus();
            return false;
        }

        var invoicedate = document.getElementById(CtrlIdPrefix + "txtInvoiceDate");
        if (invoicedate.value == "") {
            alert("Invoice Date should not be null");
            invoicedate.focus();
            return false;
        }

        var status = fnIsDate(invoicedate.value);

        if (!status) {
            invoicedate.value = "";
            invoicedate.focus();
            return false;
        }

        var invoiceamount = document.getElementById(CtrlIdPrefix + "txtInvoiceAmount");
        if (invoiceamount.value == "") {
            alert("Invoice Amount should not be null");
            invoiceamount.focus();
            return false;
        }
    }
    else {
        var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
        if (supplier.value == "0") {
            alert("Supplier should not be null");
            supplier.focus();
            return false;
        }

        var invoicenumber = document.getElementById(CtrlIdPrefix + "txtInvoiceNumber");
        if (invoicenumber.value == "") {
            alert("Invoice Number should not be null");
            invoicenumber.focus();
            return false;
        }
    }
}

function ValidationEdit() {
    var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
    if (supplier.value == "0") {
        alert("Supplier should not be null");
        supplier.focus();
        return false;
    }

    var branch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    if (branch.value == "0") {
        alert("Branch should not be null");
        branch.focus();
        return false;
    }

    var invoicenumber = document.getElementById(CtrlIdPrefix + "txtInvoiceNumber");
    if (invoicenumber.value == "") {
        alert("Invoice Number should not be null");
        invoicenumber.focus();
        return false;
    }
}

function ChkScreenMode() {
    var ScreenMode = document.getElementById(CtrlIdPrefix + "hdnScreenMode");
    var InvNumber = document.getElementById(CtrlIdPrefix + "txtInvoiceNumber");
    var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");

    if (ScreenMode.value != "A" && supplier.value != "" && InvNumber.value != "") {
        __doPostBack(CtrlIdPrefix + "txtInvoiceNumber", 0);
        return true;
    }
    else {
        return false;
    }
}

function __doPostBack(eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
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

function checkInvoiceDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
            return false;
        }
        else {

            var toDate = new Date();
            toDate = convertDate(idDate);

            var date = new Date();
            date.setHours(0);
            date.setMinutes(0);
            date.setSeconds(0);
            date.setMilliseconds(0);

            if (toDate > date) {
                document.getElementById(id).value = "";
                alert("Invoice Date should be less than or equal to System Date");
                return false;
            }
        }

        __doPostBack(CtrlIdPrefix + "txtInvoiceDate", 0);
        return true;
    }
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