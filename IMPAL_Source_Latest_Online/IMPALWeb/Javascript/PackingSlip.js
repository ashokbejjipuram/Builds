var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvItemDetails_ctl02_";
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

function funEditToggle() {
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    if (Branch == "0") {
        alert('Please select a Branch');
        ddlBrnch.focus();
        return false;
    }
    return true;
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

function CurrencywithNegativeNumberOnly() {
    
    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46 || AsciiValue == 45))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function CurrencyDecimalOnly(e1, evt) {
    
    var currenyValue = document.getElementById(e1);

    var Isvalid = parseFloat(currenyValue.value);

    if (Isvalid.toString() == "NaN" && currenyValue.value != "-") {
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

function CurrencyValidateForNegative(e1, evt) {
    
    var currenyValue = document.getElementById(e1);

    var Isvalid = parseFloat(currenyValue.value);
    var cvalue = currenyValue.value;
    var firstchr = cvalue.substring(0, 1, cvalue);   

    if (cvalue.indexOf("-") > -1) {
        if (firstchr != "-") {
            alert("Please enter valid negative number");
            document.getElementById(e1).focus();
            return false;
        }
    }    

    return true;
}

function CurrencyNumberOnly() {
    
    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function CurrencyNumberwithOneDotOnly(id) {

    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;

    strValue = document.getElementById(id);

    if (strValue.value.split('.').length > 2)
        return false;
    else
        return true;
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

function AlphaNumericWithSlash() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 47) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function AlphaNumericWithSlashDash() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 45 || AsciiValue == 47) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function AlphaNumericWithSlashAndSpace() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 47 || AsciiValue == 32) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
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

        if ((sender.get_id() == "ctl00_CPHDetails_CalExtLRDate") || (sender.get_id() == "ctl00_CPHDetails_CalExtDcDate")) {
            sender._textbox.set_Value("");
        }
        else {
            sender._textbox.set_Value(dtDate);
        }
        return false;
    }

    if (sender.get_id() == "ctl00_CPHDetails_CalExtInvoiceDate") {
        if (document.getElementById(CtrlIdPrefix + "txtLRDate").value.trim() != "") {
            if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtSalesInvoiceDate").value, document.getElementById(CtrlIdPrefix + "txtLRDate").value)) {
                alert('Invoice Date(' + document.getElementById(CtrlIdPrefix + "txtSalesInvoiceDate").value + ') should be less than the LR Date.');
                var dtDate = new Date();
                dtDate = dtDate.format(sender._format);
                sender._textbox.set_Value("");
                return false;
            }
        }

        document.getElementById(CtrlIdPrefix + "txtDcDate").value = "";
        document.getElementById(CtrlIdPrefix + "txtLRDate").value = "";
    }

    if (sender.get_id() == "ctl00_CPHDetails_CalExtLRDate") {
        //alert(sender.get_id());
        if (document.getElementById(CtrlIdPrefix + "txtSalesInvoiceDate").value.trim() != "") {
            if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtSalesInvoiceDate").value, document.getElementById(CtrlIdPrefix + "txtLRDate").value)) {
                alert('LR Date(' + document.getElementById(CtrlIdPrefix + "txtLRDate").value + ') should be greater than the Invoice Date.');
                var dtDate = new Date();
                dtDate = dtDate.format(sender._format);
                sender._textbox.set_Value("");
                return false;
            }
        }
    }

    if (sender.get_id() == "ctl00_CPHDetails_CalExtDcDate") {
        if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtDcDate").value, document.getElementById(CtrlIdPrefix + "txtSalesInvoiceDate").value)) {
            alert('DC Date(' + document.getElementById(CtrlIdPrefix + "txtDcDate").value + ') should be less than the Invoice Date.');
            var dtDate = new Date();
            dtDate = dtDate.format(sender._format);
            sender._textbox.set_Value("");
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

    if (dtDate1 > dtDate2)
        return true;
    else
        return false;
}

function IsHigherOrEqualDate(Date1, Date2) {
    var strDateArr1 = Date1.split('/');
    var strDateArr2 = Date2.split('/');

    var dtDate1 = new Date();
    var dtDate2 = new Date();
    dtDate1.setFullYear(strDateArr1[2], strDateArr1[1] - 1, strDateArr1[0]);
    dtDate2.setFullYear(strDateArr2[2], strDateArr2[1] - 1, strDateArr2[0]);

    if (dtDate1 >= dtDate2)
        return true;
    else
        return false;
}

function checkDate(sender, args) {
    var dtDate = new Date();
    dtDate.setDate(dtDate.getDate() - 90);
    //alert(dtDate);

    if (IsFutureDate(sender, args)) {

        if (!((sender._selectedDate >= dtDate) && (sender._selectedDate <= new Date()))) {
            alert('Date should be with in 90 days range from today');
            //sender._textbox.set_Value("");
            var dtDate = new Date();
            dtDate = dtDate.format(sender._format);
            sender._textbox.set_Value(dtDate);
        }
    }
}

function checkDateForReceivedDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var idInvoiceDate = document.getElementById("ctl00_CPHDetails_txtSalesInvoiceDate").value

            var toDate = new Date();
            toDate = convertDate(idDate);

            var frmDate = new Date();
            frmDate = convertDate(idInvoiceDate);
            
            if (toDate < frmDate) {
                document.getElementById(id).value = "";
                alert("Received Date should not be less than the Invoice Date");
            }
        }
    }
}

function checkDateForLRDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var idInvoiceDate = document.getElementById("ctl00_CPHDetails_txtSalesInvoiceDate").value

            var toDate = new Date();
            toDate = convertDate(idDate);

            var frmDate = new Date();
            frmDate = convertDate(idInvoiceDate);

//            if (toDate < frmDate) {
//                document.getElementById(id).value = "";
//                alert("LR Date should be greater than or equal to Invoice Date");
//            }
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

function checkDateForDCDate(id) {    

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var idInvoiceDate = document.getElementById("ctl00_CPHDetails_txtSalesInvoiceDate").value

            var toDate = new Date();
            toDate = convertDate(idDate);

            var frmDate = new Date();
            frmDate = convertDate(idInvoiceDate);

            if (toDate > frmDate) {
                document.getElementById(id).value = "";
                alert("DC Date should be less than or equal to Invoice Date");
            }
        }
    }
}

function checkDateForInvoice(id, Stat) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var date = new Date();
            date.setHours(0);
            date.setMinutes(0);
            date.setSeconds(0);
            date.setMilliseconds(0);

            var frmDate = new Date();
            frmDate = convertDate(idDate);

            if (frmDate > date) {
                document.getElementById(id).value = "";
                alert("Invoice date should be less than or equal to sysdate");
            }
            else {
                if (Stat == "1") {
                    document.getElementById("ctl00_CPHDetails_txtReceivedDate").value = "";
                    document.getElementById("ctl00_CPHDetails_txtDcDate").value = "";
                    document.getElementById("ctl00_CPHDetails_txtLRDate").value = "";
                }
            }
        }
    }    
}


function checkPackageOpenDate(sender, args) {
    //alert('asdad');
    var dtDate = new Date();

    if ((sender._selectedDate > dtDate)) {
        alert('Package open date should not be a future date.');
        //sender._textbox.set_Value("");
        var dtDate = new Date();
        dtDate = dtDate.format(sender._format);
        sender._textbox.set_Value(dtDate);
    }
}

function checkClearenceDate(sender, args) {
    //alert('asdad');
    var dtDate = new Date();

    if ((sender._selectedDate > dtDate)) {
        alert('Clearence date should not be a future date.');
        //sender._textbox.set_Value("");
        var dtDate = new Date();
        dtDate = dtDate.format(sender._format);
        sender._textbox.set_Value(dtDate);
    }
}

function ValidationEdit(i) {
    var invoicenumber = document.getElementById(CtrlIdPrefix + "txtSalesInvoiceNumber");

    if (invoicenumber) {
        if (invoicenumber.value.trim() == "") {
            alert("Invoice Number should not be null");
            invoicenumber.focus();
            return false;
        }
    }

    if (i == 2) {
        var imgtoggle = document.getElementById(CtrlIdPrefix + "imgEditToggle");

        if (imgtoggle) {
            alert('Please select Sales Invoice Number.');
            imgtoggle.focus();
            return false;
        }
    }

    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var ddlSalesInvoiceNumber = document.getElementById(CtrlIdPrefix + "ddlSalesInvoiceNumber");
    var SalesInvoiceNumber = ddlSalesInvoiceNumber.options[ddlSalesInvoiceNumber.selectedIndex].value;

    var LRNumber = document.getElementById(CtrlIdPrefix + "txtLRNumber");
    var LRDate = document.getElementById(CtrlIdPrefix + "txtLRDate");
    var Carrier = document.getElementById(CtrlIdPrefix + "txtCarrier");
    var MarkingNumber = document.getElementById(CtrlIdPrefix + "txtMarkingNumber");
    var Weight = document.getElementById(CtrlIdPrefix + "txtWeight");
    var NoOfCases = document.getElementById(CtrlIdPrefix + "txtNoOfCases");    

    if (SalesInvoiceNumber.trim() == "-- Select --" || SalesInvoiceNumber.trim() == "0") {
        alert('Please select Sales Invoice Number.');
        ddlSalesInvoiceNumber.focus();
        return false;
    }

    if (Branch == "0") {
        alert('Please select a Branch.');
        ddlBrnch.focus();
        return false;
    }

    //if (LRNumber.value.trim() == "") {
    //    alert("LR Number should not be empty.");
    //    LRNumber.value = "";
    //    LRNumber.focus();
    //    return false;
    //}

    //if (LRNumber.value.length > 40) {
    //    alert("LR Number should not exceed 40 Characters.");
    //    LRNumber.value = "";
    //    LRNumber.focus();
    //    return false;
    //}

    //if (LRDate.value.trim() == "") {
    //    alert("LR Date should not be empty.");
    //    LRDate.value = "";
    //    LRDate.focus();
    //    return false;
    //}

    if (Carrier.value.trim() == "") {
        alert("Carrier information should not be empty.");
        Carrier.value = "";
        Carrier.focus();
        return false;
    }

    if (MarkingNumber.value.trim() == "") {
        alert("Marking Number should not be empty.");
        MarkingNumber.value = "";
        MarkingNumber.focus();
        return false;
    }

    //if (Weight.value.trim() == "" || Weight.value.trim() == "0.0000") {
    //    alert("Weight should not be empty.");
    //    Weight.value = "";
    //    Weight.focus();
    //    return false;
    //}

    //if (Weight.value.trim() != "") {
    //    if (Weight.value.split('.').length > 2) {
    //        alert("weight should be in correct format");
    //        Weight.value = "";
    //        Weight.focus();
    //        return false;
    //    }
    //}

    //if (parseFloat(Weight.value.trim()) <= 0) {
    //    alert("Weight should not be zero.");
    //    Weight.value = "";
    //    Weight.focus();
    //    return false;
    //}

    if (NoOfCases.value.trim() == "") {
        alert("No. Of Cases should not be empty.");
        NoOfCases.value = "";
        NoOfCases.focus();
        return false;
    }

    if (NoOfCases.value.trim() != "") {
        if (NoOfCases.value.split('.').length > 1) {
            alert("No. Of Cases should be a Number");
            NoOfCases.value = "";
            NoOfCases.focus();
            return false;
        }
    }

    ddlSalesInvoiceNumber.disabled = true;
    document.getElementById(CtrlIdPrefix + "BtnReport").style.display = "none";
    window.document.forms[0].target = '_blank';
}