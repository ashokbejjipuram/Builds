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

function CheckPaymentDueDate(id) {

    var idDate = document.getElementById(id).value;
    //ddlAccountingPeriod = document.getElementById("ctl00_CPHDetails_ddlAccountingPeriod");
    var BookingDate = document.getElementById("ctl00_CPHDetails_txtDocumentDate").value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {
            //var FutureDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

            IdDate = convertDate(idDate);
            BookingDate = convertDate(BookingDate);

            //alert(IdDate);
            //alert(BookingDate);

            if (IdDate < BookingDate) {
                document.getElementById(id).value = "";
                alert("Past date can not be Entered.");
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
            var idInvoiceDate = document.getElementById("ctl00_CPHDetails_txtReferenceDocumentDate").value;

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

function funRejectVendorEntry() {
    var Remarks = prompt("Are You Sure You Want to Reject the Vendor Booking Entry? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
}

function funCancelVendorEntry() {
    var Remarks = prompt("Are You Sure You Want to Cancel the Vendor Booking Entry? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
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

    calculateTotal();

    return true;
}

function calculateTotal() {
    var totalAmount = 0;
    var totalTaxAmount = 0;
    var grd = document.getElementById('ctl00_CPHDetails_grvVendorBookingDetails');

    for (i = 1; i < grd.rows.length; i++) {
        var node1 = grd.rows[i].cells[4].childNodes[0];
        var node2 = grd.rows[i].cells[7].childNodes[0];
        var node3 = grd.rows[i].cells[10].childNodes[0];
        var node4 = grd.rows[i].cells[13].childNodes[0];
        var browserName = navigator.appName;
        if (browserName == 'Netscape') {
            var node1 = grd.rows[i].cells[4].childNodes[1];
            var node2 = grd.rows[i].cells[7].childNodes[1];
            var node3 = grd.rows[i].cells[10].childNodes[1];
            var node4 = grd.rows[i].cells[13].childNodes[1];
        }

        //-- Debit Total
        if (node1 != undefined && node1.type == "text")
            if (!isNaN(node1.value) && node1.value != "")
                totalAmount += parseFloat(node1.value);

        //-- Credit Total
        if (node2 != undefined && node2.type == "text")
            if (!isNaN(node2.value) && node2.value != "")
                totalTaxAmount += parseFloat(node2.value);

        if (node3 != undefined && node3.type == "text")
            if (!isNaN(node3.value) && node3.value != "")
                totalTaxAmount += parseFloat(node3.value);

        if (node4 != undefined && node4.type == "text")
            if (!isNaN(node4.value) && node4.value != "")
                totalTaxAmount += parseFloat(node4.value);
    }

    document.getElementById(CtrlIdPrefix + "hdnTotalAmount").value = parseFloat(Math.round(totalAmount * 100) / 100).toFixed(2);

    var ddlBranch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var brch = ddlBranch.options[ddlBranch.selectedIndex].value;

    document.getElementById(CtrlIdPrefix + "hdnTotalTaxAmount").value = parseFloat(Math.round(totalTaxAmount * 100) / 100).toFixed(2);
}

function validateTDStype() {
    var TDSvalue = document.getElementById(CtrlIdPrefix + "txtTDSAmount");
    var ddlTDStype = document.getElementById(CtrlIdPrefix + "ddlTDStype");
    var TDStype = ddlTDStype.options[ddlTDStype.selectedIndex].value;

    if (TDSvalue.value.trim() == "") {
        alert("TDS Amount should not be empty.");
        TDSvalue.focus();
        return false;
    }
    
    if (parseFloat(TDSvalue.value) == 0) {
        ddlTDStype.selectedIndex = 0;
        ddlTDStype.disabled = true;
    }

    if (parseFloat(TDSvalue.value) > 0) {
        if (TDStype.trim() == "") {
            ddlTDStype.disabled = false;
            ddlTDStype.focus();
            return false;
        }
    }

    return true;
}

function fnCalculateTDS() {
    var Invoicevalue = document.getElementById(CtrlIdPrefix + "txtInvoiceAmount");
    var GSTvalue = document.getElementById(CtrlIdPrefix + "txtGSTAmount");
    var ddlTDStype = document.getElementById(CtrlIdPrefix + "ddlTDStype");
    
    if (Invoicevalue.value.trim() == "" || isNaN(Invoicevalue.value)) Invoicevalue.value = "0.00";
    if (GSTvalue.value.trim() == "" || isNaN(GSTvalue.value)) GSTvalue.value = "0.00";

    document.getElementById(CtrlIdPrefix + "txtTDSAmount").value = Math.round(((parseFloat(Invoicevalue.value) - parseFloat(GSTvalue.value)) * parseFloat(ddlTDStype.value)) / 100);
    document.getElementById(CtrlIdPrefix + "hdnTDStype").value = ddlTDStype.options[ddlTDStype.selectedIndex].text;
}

function funVendorHeaderValidation() {
    var ddlBranch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBranch.options[ddlBranch.selectedIndex].value;
    var documentDate = document.getElementById(CtrlIdPrefix + "txtDocumentDate");
    var ddlAccountingPeriod = document.getElementById(CtrlIdPrefix + "ddlAccountingPeriod");
    var AccountingPeriod = ddlAccountingPeriod.options[ddlAccountingPeriod.selectedIndex].value;
    var ddlVendorCode = document.getElementById(CtrlIdPrefix + "ddlVendorCode");
    var VendorCode = ddlVendorCode.options[ddlVendorCode.selectedIndex].value;
    var Invoicevalue = document.getElementById(CtrlIdPrefix + "txtInvoiceAmount");
    var GSTvalue = document.getElementById(CtrlIdPrefix + "txtGSTAmount");
    var TDSvalue = document.getElementById(CtrlIdPrefix + "txtTDSAmount");
    var RefDocNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocumentNumber");
    var RefDocDate = document.getElementById(CtrlIdPrefix + "txtReferenceDocumentDate");
    var Narration = document.getElementById(CtrlIdPrefix + "txtNarration");
    var ddlRCMStatus = document.getElementById(CtrlIdPrefix + "ddlRCMStatus");
    var RCMStatus = ddlRCMStatus.options[ddlRCMStatus.selectedIndex].value;
    var ddlTDStype = document.getElementById(CtrlIdPrefix + "ddlTDStype");
    var TDStype = ddlTDStype.options[ddlTDStype.selectedIndex].value;
	var ddlPaymentBranch = document.getElementById(CtrlIdPrefix + "ddlPaymentBranch");
    var PaymentBranch = ddlPaymentBranch.options[ddlPaymentBranch.selectedIndex].value;
    var paymentDueDate = document.getElementById(CtrlIdPrefix + "txtPaymentDueDate");

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

    if (VendorCode.trim() == "0") {
        alert("Please Select Vendor.");
        ddlVendorCode.focus();
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

    if (Invoicevalue.value.trim() == "" || Invoicevalue.value.trim() == "0" || Invoicevalue.value.trim() == "0.00") {
        alert("Invoice Amount should not be empty.");
        Invoicevalue.focus();
        return false;
    }

    if (GSTvalue.value.trim() == "") {
        alert("GST Amount should not be empty.");
        GSTvalue.focus();
        return false;
    }

    if (TDSvalue.value.trim() == "") {
        alert("TDS Amount should not be empty.");
        TDSvalue.focus();
        return false;
    }

//    if (TDSvalue.value.trim() == "") {
//        TDSvalue.value = "0";
//    }

    if (parseFloat(TDStype.value) == 0) {
        TDSvalue.value = 0;
    }
    
    //if (parseFloat(TDSvalue.value) > 0) {
    //    if (TDStype.trim() == "") {
    //        alert("Please Select TDS Type.");
    //        ddlTDStype.disabled = false;
    //        ddlTDStype.focus();
    //        return false;
    //    }
    //}

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

    if (RCMStatus.trim() == "") {
        alert("Please select RCM Status.");
        ddlRCMStatus.focus();
        return false;
    }
	
	if (PaymentBranch.trim() == "0") {
        alert('Payment Branch Should not be empty.');
        ddlPaymentBranch.focus();
        return false;
    }

    if (paymentDueDate.value.trim() == "") {
        alert("Payment Due Date should not be empty.");
        paymentDueDate.value = "";
        paymentDueDate.focus();
        return false;
    }
    return true;
}

function funVendorSubmitValidation(m) {
    if (funVendorHeaderValidation()) {
        var grd = document.getElementById(CtrlIdPrefix + 'grvVendorBookingDetails');
        if (grd != null) {
            if (m == "2") {
                if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "" || document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
                    alert('No Item details are found.');
                    return false;
                }
            }

            calculateTotal();

            var txtHeaderInvoiceValue = document.getElementById(CtrlIdPrefix + "txtInvoiceAmount").value;
            var txtHeaderGSTValue = document.getElementById(CtrlIdPrefix + "txtGSTAmount").value;
            var txtHeaderTDS = document.getElementById(CtrlIdPrefix + "txtTDSAmount").value;

            var txtTotalAmount = document.getElementById(CtrlIdPrefix + "hdnTotalAmount").value;
            var txtTotalGSTAmount = document.getElementById(CtrlIdPrefix + "hdnTotalTaxAmount").value;
            var amount = 0;
            var gstamount = 0;
            var Ind = 0;

            if (m == "1" && (parseFloat(txtHeaderInvoiceValue) - (parseFloat(txtTotalAmount) + parseFloat(txtTotalGSTAmount))) < 1) {
                alert("Header & Details Accounts are Tallied. Please Make Sure of the Same");
            }

            for (i = 1; i < grd.rows.length - 1; i++) {
                var node1 = grd.rows[i].cells[1].childNodes[0];
                var node2 = grd.rows[i].cells[2].childNodes[0];
                var node3 = grd.rows[i].cells[3].childNodes[0];
                var node4 = grd.rows[i].cells[4].childNodes[0];
                var node5 = grd.rows[i].cells[5].childNodes[0];
                var node6 = grd.rows[i].cells[6].childNodes[0];
                var node7 = grd.rows[i].cells[6].childNodes[0];
                var browserName = navigator.appName;
                if (browserName == 'Netscape') {
                    var node1 = grd.rows[i].cells[1].childNodes[1];
                    var node2 = grd.rows[i].cells[2].childNodes[1];
                    var node3 = grd.rows[i].cells[3].childNodes[1];
                    var node4 = grd.rows[i].cells[4].childNodes[1];
                    var node5 = grd.rows[i].cells[5].childNodes[1];
                    var node6 = grd.rows[i].cells[6].childNodes[1];
                    var node7 = grd.rows[i].cells[6].childNodes[1];
                }

                //                if (Ind == "0") {
                if (node1 != undefined && node1.type == "text") {
                    if (node1.value == "") {
                        alert("Chart of Account should not be null");
                        return false;
                    }
                }

                if (node2 != undefined && node2.type == "text") {
                    if (node2.value == "") {
                        alert("Description should not be null");
                        document.getElementById(node2.id).focus();
                        return false;
                    }
                }

                if (node3 != undefined && node3.type == "text") {
                    if (node3.value == "") {
                        alert("Remarks should not be null");
                        document.getElementById(node3.id).focus();
                        return false;
                    }
                }

                if (node4 != undefined && node4.type == "text") {
                    if (!isNaN(node4.value) && node4.value != "")
                        amount += parseFloat(node4.value);
                }

                if (node5 != undefined && node5.type == "text") {
                    if (!isNaN(node5.value) && node5.value != "")
                        gstamount += parseFloat(node5.value);
                }

                if (node6 != undefined && node6.type == "text") {
                    if (!isNaN(node6.value) && node6.value != "")
                        gstamount += parseFloat(node6.value);
                }

                if (node7 != undefined && node7.type == "text") {
                    if (!isNaN(node7.value) && node7.value != "")
                        gstamount += parseFloat(node7.value);
                }

                if (amount <= 0) {
                    alert('amount should be greater than Zero');
                    node4.focus();
                    return false;
                }
            }

            if (m == "2") {
                var differenceTaxAmount = parseFloat(txtHeaderGSTValue) - parseFloat(txtTotalGSTAmount);

                if ((parseFloat(differenceTaxAmount) < 0)) {
                    differenceTaxAmount = parseFloat(differenceTaxAmount) * (-1);
                }

                if (parseFloat(differenceTaxAmount) < 1) {
                    return true;
                }
                else {
                    alert('Invoice GST Value and Details GST Amount Rounding Off does not tally');
                    return false;
                }

                var differenceAmount = parseFloat(txtHeaderInvoiceValue) - (parseFloat(txtTotalAmount) + parseFloat(txtHeaderGSTValue));

                if ((parseFloat(differenceAmount) < 0)) {
                    differenceAmount = parseFloat(differenceAmount) * (-1);
                }

                if (parseFloat(differenceAmount) < 1) {
                    return true;
                }
                else {
                    alert('Invoice Value and Sum of Details Amount and GST Amount does not tally');
                    return false;
                }
            }
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

