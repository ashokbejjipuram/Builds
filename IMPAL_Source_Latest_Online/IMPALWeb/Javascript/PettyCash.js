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
function ValidateTransactionAmount(value) {
    var rgexp = new RegExp("^\d*([.]\d{2})?$");
    if (value.match(rgexp))
        return true;
    else {
        alert("Invalid Transaction Amount");
        return false;
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

function ValidateAmount(e, control) {

    if (e.keyCode == 46) {
        var patt1 = new RegExp("\\.");
        var ch = patt1.exec(control.value);
        if (ch == ".") {
            e.keyCode = 0;
        }
    }
    else if ((e.keyCode >= 48 && e.keyCode <= 57) || e.keyCode == 8)//Numbers or BackSpace
    {
        if (control.value.indexOf('.') != -1)//. Exisist in TextBox 
        {
            var pointIndex = control.value.indexOf('.');
            var beforePoint = control.value.substring(0, pointIndex);
            var afterPoint = control.value.substring(pointIndex + 1);
            var iCaretPos = 0;
            if (document.selection) {
                var selectionRange = document.selection.createRange();
                selectionRange.moveStart('character', -control.value.length);
                iCaretPos = selectionRange.text.length;
            }
            if (iCaretPos > pointIndex && afterPoint.length >= 2) {
                e.keyCode = 0;
            }
            else if (iCaretPos <= pointIndex && beforePoint.length >= 7) {
                e.keyCode = 0;
            }
        }
        else//. Not Exisist in TextBox
        {
            if (control.value.length >= 7) {
                e.keyCode = 0;
            }
        }
    }
    else {
        e.keyCode = 0;
    }
}
// Validate No.Of Transactions field on key press 
function ValidateNumbOfTransactions(e, control) {


    if ((e.keyCode >= 48 && e.keyCode <= 57) || e.keyCode == 8)//Numbers or BackSpace
    {
        if (control.value.length >= 3) {
            e.keyCode = 0;
        }

    }
    else {
        e.keyCode = 0;
    }
}

function isspecialchar(c) {
    myArray = ['!', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '`', '~', ']', '[', '|', '@', '/', '"', ':', ';', '{', '}', ',', "'", '.', '?', '\\'];
    for (var j = 0; j < myArray.length; j++) {
        if (c == myArray[j]) {
            return true;
        }
    }
    return false;
}

function validatespl(inpval, fldname) {
    var Stat;
    var firstchr = inpval.substring(0, 1, inpval);
    if (firstchr == " ") {
        alert("First character of " + fldname + " should not be blank");
        return Stat = "No";
    }
    else if (isspecialchar(firstchr)) {
        alert("First character of " + fldname + " should be alphabet or number");
        return Stat = "No";
    }

    for (i = 0; i < inpval.length; i++) {
        firstchr = inpval.substring(i, i + 1, inpval);
        if (firstchr == "") {
            alert("First character of " + fldname + " should not be blank");
            return Stat = "No";
        }
        else if (isspecial1(firstchr)) {
            alert("Characters in " + fldname + " should be alphabet or number");
            return Stat = "No";
        }
    }
    return Stat = "Yes";
}

function NoofValidate() {
    var TransAmount = document.getElementById(CtrlIdPrefix + "txtTransactionAmount");
    var txtNoOfTransactions = document.getElementById(CtrlIdPrefix + "txtNoOfTransactions");
    var txtBranch = document.getElementById(CtrlIdPrefix + "txtBranch");
    var txtDescription = document.getElementById(CtrlIdPrefix + "txtDescription_Name");

    if (txtDescription != undefined) {
        if (txtDescription.value == "") {
            alert('Please enter a Description / Name.');
            txtDescription.focus();
            return false;
        }
    }

    var stat = validatespl(txtDescription.value, "Description / Name");

    if (stat == "No") {
        return false
    }
    
    if (TransAmount != undefined) {
        if (TransAmount.value == "") {
            alert('Please enter a Transaction Amount.');
            TransAmount.focus();
            return false;
        }

        if (parseFloat(TransAmount.value) == 0) {
            alert('Transaction Amount greater than Zero.');
            TransAmount.focus();
            return false;
        }
    }

    if (txtNoOfTransactions != undefined) {
        if (txtNoOfTransactions.value == "") {
            alert('Please enter a No of Transactions.');
            txtNoOfTransactions.focus();
            return false;
        }

        if (parseFloat(txtNoOfTransactions.value) == 0) {
            alert('No of Transactions value greater than Zero.');
            txtNoOfTransactions.focus();
            return false;
        }
    }

    if (txtBranch != undefined) {
        if (txtBranch.value == "") {
            alert('Please enter a Branch.');
            txtBranch.focus();
            return false;
        }
    }    

    return true;
}

function Validate() {
    var grid = document.getElementById(CtrlIdPrefix + "grdPettyTransaction");
    var TransAmount = document.getElementById(CtrlIdPrefix + "txtTransactionAmount");
    var txtNoOfTransactions = document.getElementById(CtrlIdPrefix + "txtNoOfTransactions");
    var txtBranch = document.getElementById(CtrlIdPrefix + "txtBranch");
    var txtDescription = document.getElementById(CtrlIdPrefix + "txtDescription_Name");
    var totalAmount = 0.0;

    if (txtDescription != undefined) {
        if (txtDescription.value == "") {
            alert('Please enter a Description / Name.');
            txtDescription.focus();
            return false;
        }
    }

    var stat = validatespl(txtDescription.value, "Description / Name");

    if (stat == "No") {
        return false
    }
    
    if (TransAmount != undefined) {
        if (TransAmount.value == "") {
            alert('Please enter a Transaction Amount.');
            TransAmount.focus();
            return false;
        }
    }

    if (txtNoOfTransactions != undefined) {
        if (txtNoOfTransactions.value == "") {
            alert('Please enter a No of Transactions.');
            txtNoOfTransactions.focus();
            return false;
        }
    }

    if (txtBranch != undefined) {
        if (txtBranch.value == "") {
            alert('Please enter a Branch.');
            txtBranch.focus();
            return false;
        }
    }

    var grd = document.getElementById(CtrlIdPrefix + "grdPettyTransaction");

    for (jcount = 0; jcount < grd.rows.length - 2; jcount++) {

        var node1 = grd.rows[jcount].cells[1].childNodes[0];
        var node2 = grd.rows[jcount].cells[2].childNodes[0];
        var node3 = grd.rows[jcount].cells[3].childNodes[0];

        var browserName = navigator.appName;

        if (browserName == 'Netscape') {
            var node1 = grd.rows[jcount].cells[1].childNodes[1];
            var node2 = grd.rows[jcount].cells[2].childNodes[1];
            var node3 = grd.rows[jcount].cells[3].childNodes[1];
        }

        if (node2 != undefined) {
            if (parseFloat(node2.value) > 0) {
                if (node1 != undefined && node1.type == "text")
                    if (node1.value == "") {
                    alert("Please select Chart of Account.");
                    document.getElementById(node1.id).focus();
                    return false;

                }

                if (node2 != undefined && node2.type == "text")
                    if (node2.value == "") {
                    alert("Please enter Amount in the grid.");
                    document.getElementById(node2.id).focus();
                    return false;
                }

                if (node2.value == "0") {
                    alert("Please enter amount greater than zero.");
                    document.getElementById(node2.id).focus();
                    return false;
                }

                if (node3 != undefined && node3.type == "text")
                    if (node3.value == "") {
                    alert("Please enter Remarks in the grid.");
                    document.getElementById(node3.id).focus();
                    return false;
                }


                var stat = validatespl(node3.value, "Remarks");

                if (stat == "No") {
                    document.getElementById(node3.id).focus();
                    return false
                }

                totalAmount = totalAmount + parseFloat(node2.value);
            }
        }
    }

    if (parseFloat(TransAmount.value) == parseFloat(totalAmount)) {
    }
    else {
        alert("Transaction Amount should be match with Total Amount.");
        return false;
    }

    return true;
}