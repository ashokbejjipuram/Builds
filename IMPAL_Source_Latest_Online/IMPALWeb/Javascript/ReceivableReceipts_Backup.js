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



function funReceiptHeaderValidation() {
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var receiptDate = document.getElementById(CtrlIdPrefix + "txtReceiptDate");

    var ddlAccountingPeriod = document.getElementById(CtrlIdPrefix + "ddlAccountingPeriod");
    var AccountingPeriod = ddlAccountingPeriod.options[ddlAccountingPeriod.selectedIndex].value;

    var ddlCustomer = document.getElementById(CtrlIdPrefix + "ddlCustomer");
    var Customer = ddlCustomer.options[ddlCustomer.selectedIndex].value;

    var ddlModeOfReceipt = document.getElementById(CtrlIdPrefix + "ddlModeOfReceipt");
    var ModeOfReceipt = ddlModeOfReceipt.options[ddlModeOfReceipt.selectedIndex].value;

    var ddlLocalOrOutStation = document.getElementById(CtrlIdPrefix + "ddlLocalOrOutStation");
    var LocalOrOutStation = ddlLocalOrOutStation.options[ddlLocalOrOutStation.selectedIndex].value;

    var Amount = document.getElementById(CtrlIdPrefix + "txtAmount");
    var TempRecptNumber = document.getElementById(CtrlIdPrefix + "txtTempRecptNumber");
    var TempRecptDate = document.getElementById(CtrlIdPrefix + "txtTempRecptDate");
    var ChequeNumber = document.getElementById(CtrlIdPrefix + "txtChequeNumber");
    var ChequeDate = document.getElementById(CtrlIdPrefix + "txtChequeDate");
    var Bank = document.getElementById(CtrlIdPrefix + "txtBank");
    var BankBranch = document.getElementById(CtrlIdPrefix + "txtBranch");

    var FromDate = document.getElementById(CtrlIdPrefix + "txtFromDate");
    var ToDate = document.getElementById(CtrlIdPrefix + "txtToDate");

    if (Branch == "0") {
        alert('Please select a Branch.');
        ddlBrnch.focus();
        return false;
    }

    if (receiptDate.value.trim() == "") {
        alert("Receipt Date should not be empty.");
        receiptDate.value = "";
        receiptDate.focus();
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

    if (ModeOfReceipt == "0") {
        alert('Please select a Mode Of Receipt.');
        ddlModeOfReceipt.focus();
        return false;
    }

    if (Amount.value.trim() == "") {
        alert("Amount should not be empty.");
        Amount.value = "";
        Amount.focus();
        return false;
    }

    if (Amount.value.trim() == "0") {
        alert("Please enter valid Amount.");
        Amount.value = "";
        Amount.focus();
        return false;
    }

    if (!CurrencyChkForMoreThanOneDot(Amount.value.trim())) {
        alert("Please enter valid Amount value.");
        Amount.focus();
        return false;
    }



    var controlName = '';

    if (ModeOfReceipt == "DR")
        controlName = 'Draft';

    if (ModeOfReceipt == "CH")
        controlName = 'Cheque';


    if (ModeOfReceipt == "CH") {
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

        if (LocalOrOutStation == "0") {
            alert('Please select Local/OutStation details.');
            ddlLocalOrOutStation.focus();
            return false;
        }
    }
    else {

        if (TempRecptNumber.value.trim() == "") {
            alert("Temporary Recpt. Number should not be empty.");
            TempRecptNumber.value = "";
            TempRecptNumber.focus();
            return false;
        }

        if (TempRecptDate.value.trim() == "") {
            alert("Temporary Recpt. Date should not be empty.");
            TempRecptDate.value = "";
            TempRecptDate.focus();
            return false;
        }

        if (TempRecptDate.value.trim() != "") {
            var NoOfDays = getDaysBetweenDates(TempRecptDate.value.trim());
            if (NoOfDays > 12) {
                alert("Reciept Date should not be less than 12 Days from Current Date");
                TempRecptDate.focus();
                return false;
            }

        }

    }



    if (FromDate.value.trim() == "") {
        alert("From Date should not be empty.");
        FromDate.value = "";
        FromDate.focus();
        return false;
    }

    if (ToDate.value.trim() == "") {
        alert("To Date should not be empty.");
        ToDate.value = "";
        ToDate.focus();
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

function funFYRcvReceiptSubmit() {
    document.getElementById(CtrlIdPrefix + "BtnSubmit").disabled = false;
    if (funReceiptHeaderValidation()) {
        //alert(document.getElementById(CtrlIdPrefix + "hdnRowCnt").value);
        if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
            alert('No Item details are found.');
            return false;
        }

        if (GridCheck()) {
            var Amount = document.getElementById(CtrlIdPrefix + "txtAmount").value.trim();
            var TotalBalAmount = document.getElementById(CtrlIdPrefix + "txtTotalBalanceAmount").value.trim();
            if (parseFloat(TotalBalAmount) == 0) {
                return true; //post back gets happen for save/update.
                document.getElementById(CtrlIdPrefix + "BtnSubmit").disabled = true;
            }
            else {
                alert("Collection Amount does not match with the grid values adjustment.");
                return false;
            }
        }
        else
            return false;
    }
    else
        return false;
}


function GridCheck() {
    
    var hdnTxtCtrl = document.getElementById(CtrlIdPrefix + "txtHdnGridCtrls");

    var ctrlArr = hdnTxtCtrl.value.trim().split('|');
    var IsAnyRowSelected = "False";

    var DocAmount = "";
    var CollAmount = "";
    var PaymentInd = "";

    for (var i = 0; i <= ctrlArr.length - 1; i++) {
        var CtrlInner = ctrlArr[i].split(',');
        if (CtrlInner[0].trim() != '') {
            if ((document.getElementById(CtrlInner[0]) != null) && (document.getElementById(CtrlInner[0]) != undefined)) {

                if (document.getElementById(CtrlInner[0]).checked) {
                    DocAmount = document.getElementById(CtrlInner[1]).value.trim();
                    CollAmount = document.getElementById(CtrlInner[2]).value.trim();
                    PaymentInd = document.getElementById(CtrlInner[3]).value.trim();

                    //alert(DocAmount);
                    //alert(CollAmount);
                    //alert(PaymentInd);

                    if (CollAmount == "") {
                        IsAnyRowSelected = "False";
                        alert('Collection Amount should not be empty.');
                        document.getElementById(CtrlInner[2]).focus();
                        return false;
                    }
                    if (CollAmount == "0") {
                        IsAnyRowSelected = "False";
                        alert('Please enter valid collection Amount.');
                        document.getElementById(CtrlInner[2]).focus();
                        return false;
                    }

                    if (!CurrencyChkForMoreThanOneDot(CollAmount)) {
                        IsAnyRowSelected = "False";
                        alert('Please enter valid Collection Amount.');
                        document.getElementById(CtrlInner[2]).focus();
                        return false;
                    }
                    if (parseFloat(CollAmount) > parseFloat(DocAmount)) {
                        IsAnyRowSelected = "False";
                        alert('Collection Amount can not be higher than Document Amount.');
                        document.getElementById(CtrlInner[2]).focus();
                        return false;
                    }

                    if ((parseFloat(CollAmount) < parseFloat(DocAmount)) && (PaymentInd == "")) {
                        IsAnyRowSelected = "False";
                        alert('Please select a Payment Indicator.');
                        document.getElementById(CtrlInner[3]).focus();
                        return false;
                    }

                    IsAnyRowSelected = "True";
                }
            }
        }
    }
    if (IsAnyRowSelected == "True") {
        return true;
    }
    else {
        alert("No Items has been selected.");
        return false;
    }
}

function funCollectionAmountValidation(ChkBox, DocAmount, CollAmount, PaymentInd) {
    //alert('asd');
    /*alert(UnitId);
    alert(RateID);
    alert(DiscountID);
    alert(QtyID);
    alert(NetPriceID);*/

    var ChkBox1 = document.getElementById(ChkBox);
    var DocAmount1 = document.getElementById(DocAmount);
    var CollAmount1 = document.getElementById(CollAmount);
    var PaymentInd1 = document.getElementById(PaymentInd);
    if ((ChkBox1 != null) && (ChkBox1 != undefined)) {
        if (!ChkBox1.checked) {
            alert('Please select the Relavent Check Box.');
            ChkBox1.focus();
            return false;
        }
        if ((CollAmount1 != null) && (CollAmount1 != undefined) && (CollAmount1.value != 0) && (CollAmount1.value.trim() != "")) {
            if (!CurrencyChkForMoreThanOneDot(CollAmount1.value)) {
                alert('Please enter valid Collection Amount.');
                CollAmount1.focus();
                return false;
            }

            if (parseFloat(CollAmount1.value) > parseFloat(DocAmount1.value)) {
                alert('Collection Amount can not be higher than the Document Amount.');
                CollAmount1.focus();
                return false;
            }
        }
        else {
            alert('Please enter valid Collection Amount.');
            CollAmount1.focus();
            return false;
        }
        //alert("test");
        GetTotal();
        return true;
    }
}

function GetTotal() {
    totalCollectionAmount = 0.00;

    // Get the gridview
    var grid = document.getElementById("ctl00_CPHDetails_grvItemDetails");

    // Get all the input controls (can be any DOM element you would like)
    var inputs = grid.getElementsByTagName("input");
    var DocumentAmountID = "";
    var CollAmountID = "";
    var BalanceAmountID = "";

    var CustomerCollectionAmount = document.getElementById(CtrlIdPrefix + "txtAmount").value.trim();
    // Loop through all the DOM elements we grabbed
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].name.indexOf("ChkSelected") > 1) {
            var strId = inputs[i].id.split("_");
            DocumentAmountID = "ctl00_CPHDetails_grvItemDetails_" + strId[3] + "_txtDocumentValue";
            CollAmountID = "ctl00_CPHDetails_grvItemDetails_" + strId[3] + "_txtCollectionAmount";
            BalanceAmountID = "ctl00_CPHDetails_grvItemDetails_" + strId[3] + "_txtBalanceAmount";

            //alert(inputs[i].id);            
            //alert(inputs[i].checked)
            if (inputs[i].checked) {
                var docValue = document.getElementById(DocumentAmountID).value.trim();
                var CollValue = document.getElementById(CollAmountID).value.trim();
                //alert(inputs[i].value);
                //alert(parseFloat(inputs[i].value));
                totalCollectionAmount += parseFloat(CollValue);
                document.getElementById(BalanceAmountID).value = (parseFloat(docValue) - parseFloat(CollValue)).toFixed(2);
            }
        }
        //alert(inputs[i].name);
    }
    //alert(totalCollectionAmount);
    document.getElementById(CtrlIdPrefix + "txtTotalBalanceAmount").value = (parseFloat(CustomerCollectionAmount) - parseFloat(totalCollectionAmount)).toFixed(2);
}

function funGetDocuments() {
    if (funReceiptHeaderValidation()) {
        document.getElementById(CtrlIdPrefix + 'ddlBranch').disabled = true;
        document.getElementById(CtrlIdPrefix + 'ddlAccountingPeriod').disabled = true;
        document.getElementById(CtrlIdPrefix + 'ddlCustomer').disabled = true;
        document.getElementById(CtrlIdPrefix + 'ddlModeOfReceipt').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtAmount').disabled = true;
        return true;
    }
    return false;
}

function funFYRcvReceiptReset() {
    //if (confirm("You will loose the unsaved informations.\n\nAre you sure you want reset the page?"))
    return true;
    //else
    //  return false;
}

function funModeOfReceipt() {
    //alert(document.getElementById(CtrlIdPrefix + 'ddlModeOfReceipt').value);

    if (document.getElementById(CtrlIdPrefix + 'ddlModeOfReceipt').value == 'CA' || document.getElementById(CtrlIdPrefix + 'ddlModeOfReceipt').value == 'DR') {

        document.getElementById(CtrlIdPrefix + 'txtChequeNumber').value = "";
        document.getElementById(CtrlIdPrefix + 'txtChequeDate').value = "";
        document.getElementById(CtrlIdPrefix + 'txtBank').value = "";
        document.getElementById(CtrlIdPrefix + 'txtBranch').value = "";
        document.getElementById(CtrlIdPrefix + 'txtTempRecptNumber').value = "";
        document.getElementById(CtrlIdPrefix + 'txtTempRecptDate').value = "";
        document.getElementById(CtrlIdPrefix + 'ddlLocalOrOutStation').value = "L";

        document.getElementById(CtrlIdPrefix + 'txtChequeNumber').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtChequeDate').disabled = true;
        //        document.getElementById(CtrlIdPrefix + 'ImgChequeDate').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtBank').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtBranch').disabled = true;
        document.getElementById(CtrlIdPrefix + 'ddlLocalOrOutStation').disabled = true;
    }
    else {
        document.getElementById(CtrlIdPrefix + 'txtChequeNumber').disabled = false;
        document.getElementById(CtrlIdPrefix + 'txtChequeDate').disabled = false;
        //        document.getElementById(CtrlIdPrefix + 'ImgChequeDate').disabled = false;
        document.getElementById(CtrlIdPrefix + 'txtBank').disabled = false;
        document.getElementById(CtrlIdPrefix + 'txtBranch').disabled = false;
        document.getElementById(CtrlIdPrefix + 'txtTempRecptNumber').value = "";
        document.getElementById(CtrlIdPrefix + 'txtTempRecptDate').value = "";
        document.getElementById(CtrlIdPrefix + 'ddlLocalOrOutStation').disabled = false;
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

function CheckTempRecptDate(id, isFutureDate, Msg) {

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
                if (NoOfDays > 12) {
                    alert("Reciept Date should not be less than 12 Days from Current Date");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
            }
        }
    }
}