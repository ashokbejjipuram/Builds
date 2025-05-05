var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvItemDetails_ctl02_";
var selectedItemCode = "";

document.onkeydown = function () {
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

function funShowMEP() {
    var panelShadow = $get('divShadowPopUp');
    var panelProgressLoader = $get('divAjaxImageLoader');
    panelShadow.style.display = 'block';
    panelProgressLoader.style.display = 'block';
    var grdwidth = parseInt($('#blockWrapper').innerWidth()) + 'px';
    panelShadow.style.width = grdwidth;
}

function funCloseMEP() {
    document.getElementById(CtrlIdPrefix + "imgClose").click();
    var panelShadow = $get('divShadowPopUp');
    var panelProgressLoader = $get('divAjaxImageLoader');
    panelShadow.style.display = 'none';
    panelProgressLoader.style.display = 'none';
}

function funPrintMEP() {
    //alert('ad');
    var printWin = window.open('', '', 'left=0,top=0,width=1000,height=600,status=0');
    printWin.document.write(document.getElementById("divReportPopUpPrintArea").innerHTML);
    printWin.document.close();
    printWin.focus();
    printWin.print();
    printWin.close();

    funCloseMEP();
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

function funReset() {
    //if (confirm("You will loose the unsaved informations.\n\nAre you sure you want reset the page?"))
    return true;
    //else
    //  return false;
}



function fnPDCRegisterSubmit() {
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var ddlAccountingPeriod = document.getElementById(CtrlIdPrefix + "ddlAccountingPeriod");
    var AccountingPeriod = ddlAccountingPeriod.options[ddlAccountingPeriod.selectedIndex].value;

    var ddlCustomer = document.getElementById(CtrlIdPrefix + "ddlCustomer");
    var Customer = ddlCustomer.options[ddlCustomer.selectedIndex].value;  

    var ChequeNumber = document.getElementById(CtrlIdPrefix + "txtChequeNumber");
    var ChequeDate = document.getElementById(CtrlIdPrefix + "txtChequeDate");
    var ChequeAmount = document.getElementById(CtrlIdPrefix + "txtChequeAmount");
    var Bank = document.getElementById(CtrlIdPrefix + "txtBank");
    var BankBranch = document.getElementById(CtrlIdPrefix + "txtBranch");

    var ddlClearedStatus = document.getElementById(CtrlIdPrefix + "ddlClearedStatus");
    var ClearedStatus = ddlClearedStatus.options[ddlClearedStatus.selectedIndex].value;

    var ddlLocalOrOutStation = document.getElementById(CtrlIdPrefix + "ddlLocalOrOutStation");
    var LocalOrOutStation = ddlLocalOrOutStation.options[ddlLocalOrOutStation.selectedIndex].value;

    if (Branch == "0") {
        alert('Please select a Branch.');
        ddlBrnch.focus();
        return false;
    }

    if (AccountingPeriod == "0") {
        alert('Please select an Accounting Period.');
        ddlAccountingPeriod.focus();
        return false;
    }

    if (Customer == "0") {
        alert('Please select a Customer.');
        ddlCustomer.focus();
        return false;
    }

    if (ChequeNumber.value.trim() == "") {
        alert("Cheque Number should not be empty.");
        ChequeNumber.value = "";
        ChequeNumber.focus();
        return false;
    }

    if (ChequeDate.value.trim() == "") {
        alert("Cheque Date should not be empty.");
        ChequeDate.value = "";
        ChequeDate.focus();
        return false;
    }

    if (ChequeDate.value.trim() != "") {
        var NoOfDays = getDaysBetweenDates(ChequeDate.value.trim());
        if (NoOfDays > 90) {
            alert("Cheque Date should not be Greater than 90 Days from Current Date");
            ChequeDate.focus();
            return false;
        }
    }

    if (ChequeAmount.value.trim() == "") {
        alert("Cheque Amount should not be empty.");
        ChequeAmount.focus();
        return false;
    }

    if (!CurrencyChkForMoreThanOneDot(ChequeAmount.value.trim())) {
        alert("Please enter valid Cheque Amount.");
        ChequeAmount.focus();
        return false;
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

    if (ClearedStatus == "") {
        alert('Please select Cleared/Uncleared Status.');
        ddlClearedStatus.focus();
        return false;
    }

    if (LocalOrOutStation == "") {
        alert('Please select Local/OutStation Status.');
        ddlLocalOrOutStation.focus();
        return false;
    }

    return true;
}

function fnPDCRegisterUpdate() {
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var ddlAccountingPeriod = document.getElementById(CtrlIdPrefix + "ddlAccountingPeriod");
    var AccountingPeriod = ddlAccountingPeriod.options[ddlAccountingPeriod.selectedIndex].value;

    var ddlCustomer = document.getElementById(CtrlIdPrefix + "ddlCustomer");
    var Customer = ddlCustomer.options[ddlCustomer.selectedIndex].value;

    var ChequeNumber = document.getElementById(CtrlIdPrefix + "txtChequeNumber");

    var ddlClearedStatus = document.getElementById(CtrlIdPrefix + "ddlClearedStatus");
    var ClearedStatus = ddlClearedStatus.options[ddlClearedStatus.selectedIndex].value;

    var ddlLocalOrOutStation = document.getElementById(CtrlIdPrefix + "ddlLocalOrOutStation");
    var LocalOrOutStation = ddlLocalOrOutStation.options[ddlLocalOrOutStation.selectedIndex].value;

    if (Branch == "0") {
        alert('Please select a Branch.');
        ddlBrnch.focus();
        return false;
    }

    if (AccountingPeriod == "0") {
        alert('Please select an Accounting Period.');
        ddlAccountingPeriod.focus();
        return false;
    }

    if (Customer == "0") {
        alert('Please select a Customer.');
        ddlCustomer.focus();
        return false;
    }

    if (ChequeNumber.value.trim() == "") {
        alert("Cheque Number should not be empty.");
        ChequeNumber.value = "";
        ChequeNumber.focus();
        return false;
    }

    if (ClearedStatus == "") {
        alert('Please select Cleared/Uncleared Status.');
        ddlClearedStatus.focus();
        return false;
    }

    if (LocalOrOutStation == "") {
        alert('Please select Local/OutStation Status.');
        ddlLocalOrOutStation.focus();
        return false;
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

function CheckChequeDate(id, isFutureDate, Msg) {
    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {
            //if (isFutureDate == true) {
            //    var fDate = idDate.split("/");
            //    var day = fDate[0];
            //    var month = fDate[1] - 1;
            //    var year = parseInt(fDate[2]);

            //    var frmDate = new Date();
            //    frmDate.setDate(day);
            //    frmDate.setMonth(month);
            //    frmDate.setFullYear(year);

            //    if (frmDate > new Date()) {
            //        alert(Msg + " should not be greater than Today's Date");
            //        document.getElementById(id).value = "";
            //        document.getElementById(id).focus();
            //    }
            //}

            if (idDate.trim() != "") {
                var NoOfDays = getDaysBetweenDates(idDate.trim());
                if (NoOfDays > 90) {
                    alert("Cheque Date should not be greater than 90 Days from Current Date");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
            }
        }
    }
}