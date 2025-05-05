var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvMiscDetails_ctl02_";
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

function funMiscBillsHeaderValidation() {
    var txtBranch = document.getElementById(CtrlIdPrefix + "txtBranch");
    var documentDate = document.getElementById(CtrlIdPrefix + "txtInvoiceDate");
    var ddlAccountingPeriod = document.getElementById(CtrlIdPrefix + "ddlAccountingPeriod");
    var AccountingPeriod = ddlAccountingPeriod.options[ddlAccountingPeriod.selectedIndex].value;
    var ddlSupplierLine = document.getElementById(CtrlIdPrefix + "ddlSupplierLine");
    var SupplierLine = ddlSupplierLine.options[ddlSupplierLine.selectedIndex].value;
    var SupplierName = document.getElementById(CtrlIdPrefix + "txtSupplierName");
    var SupplierPlace = document.getElementById(CtrlIdPrefix + "txtSupplierPlace");
    var RefDocNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocumentNumber");
    var RefDocDate = document.getElementById(CtrlIdPrefix + "txtReferenceDocumentDate");
    var Remarks = document.getElementById(CtrlIdPrefix + "txtRemarks");

    if (txtBranch.value.trim() == "") {
        alert('Branch Sould not be empty.');
        ddlBrnch.focus();
        return false;
    }

    if (documentDate.value.trim() == "") {
        alert("Document Date should not be empty.");
        receiptDate.value = "";
        receiptDate.focus();
        return false;
    }

    if (AccountingPeriod == "0") {
        alert('Please select an Accounting Period.');
        ddlAccountingPeriod.focus();
        return false;
    }

    if (SupplierLine == "0" || SupplierLine == "--Select--") {
        alert('Please select an Supplier Line.');
        ddlSupplierLine.focus();
        return false;
    }

    if (SupplierName.value.trim() == "") {
        alert("Supplier Name should not be empty.");
        SupplierName.focus();
        return false;
    }

    if (SupplierPlace.value.trim() == "") {
        alert("Supplier Place should not be empty.");
        SupplierPlace.focus();
        return false;
    }

    if (RefDocNumber.value.trim() == "") {
        alert("Reference Document Number should not be empty.");
        RefDocNumber.focus();
        return false;
    }

    if (RefDocDate.value.trim() == "") {
        alert("Reference Document Date should not be empty.");
        RefDocDate.focus();
        return false;
    }

    if (Remarks.value.trim() == "") {
        alert("Remarks should not be empty.");
        Remarks.focus();
        return false;
    }

    return true;
}

function funMisBillsSubmitValidation() {
    if (funMiscBillsHeaderValidation()) {
        if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0" || document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "") {
            alert('No Item details are found.');
            return false;
        }

        var grd = document.getElementById('ctl00_CPHDetails_grvMiscDetails');

        if (grd != null) {
            for (i = 1; i < grd.rows.length; i++) {
                var node1 = grd.rows[i].cells[1].childNodes[0];
                var node2 = grd.rows[i].cells[2].childNodes[0];
                var node3 = grd.rows[i].cells[3].childNodes[0];
                var node4 = grd.rows[i].cells[4].childNodes[0];
                var node5 = grd.rows[i].cells[5].childNodes[0];
                var node6 = grd.rows[i].cells[6].childNodes[0];

                var browserName = navigator.appName;

                if (browserName == 'Netscape') {
                    var node1 = grd.rows[i].cells[1].childNodes[1];
                    var node2 = grd.rows[i].cells[2].childNodes[1];
                    var node3 = grd.rows[i].cells[3].childNodes[1];
                    var node4 = grd.rows[i].cells[4].childNodes[1];
                    var node5 = grd.rows[i].cells[5].childNodes[1];
                    var node6 = grd.rows[i].cells[6].childNodes[1];
                }

                if (node1 != undefined && node1.type == "text")
                    if (node1.value == "") {
                    alert("Chart of Account should not be null");
                    return false;
                }

                if (node2 != undefined && node2.type == "text")
                    if (node2.value == "") {
                    alert("Description should not be null");
                    document.getElementById(node2.id).focus();
                    return false;
                }

                if (node3 != undefined && node3.type == "text")
                    if (node3.value == "") {
                    alert("Unit should not be null");
                    document.getElementById(node3.id).focus();
                    return false;
                }

                if (node4 != undefined && node4.type == "text")
                    if (node4.value == "") {
                    alert("Quantity should not be null");
                    document.getElementById(node4.id).focus();
                    return false;
                }

                if (node5 != undefined && node5.type == "text")
                    if (node5.value == "") {
                    alert("Rate should not be null");
                    document.getElementById(node5.id).focus();
                    return false;
                }

                if (node6 != undefined && node6.type == "text")
                    if (node6.value == "") {
                    alert("Discount % should not be null");
                    document.getElementById(node6.id).focus();
                    return false;
                }
            }
        }
    }
    else {
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