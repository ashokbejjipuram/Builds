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

function funTODGenerationHeaderValidation() {
    var Branch = document.getElementById(CtrlIdPrefix + "ddlBranch").value;
    var AccountingPeriod = document.getElementById(CtrlIdPrefix + "ddlAccountingPeriod").value;
    var Supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier").value;
    var SupplyPlant = document.getElementById(CtrlIdPrefix + "ddlSupplyPlant").value;
    var Customer = document.getElementById(CtrlIdPrefix + "ddlCustomer").value;
    var SLBValue = document.getElementById(CtrlIdPrefix + "ddlSLBValue").value;
    var SLBType = document.getElementById(CtrlIdPrefix + "ddlSLBType").value;

    if (document.getElementById(CtrlIdPrefix + "ddlTODMonthsGOGO") != null)
        var TODMonth = document.getElementById(CtrlIdPrefix + "ddlTODMonthsGOGO").value;

    var SLBPercentage = document.getElementById(CtrlIdPrefix + "ddlSLBPercentage").value;
    var CDtype = document.getElementById(CtrlIdPrefix + "ddlBeforeAfterCD").value;

    var FromDate = document.getElementById(CtrlIdPrefix + "txtFromDate");
    var ToDate = document.getElementById(CtrlIdPrefix + "txtToDate");

    if (Branch == "0") {
        alert('Please select a Branch.');
        document.getElementById(CtrlIdPrefix + "ddlBranch").focus();
        return false;
    }

    if (AccountingPeriod == "0") {
        alert('Please select an Accounting Period.');
        document.getElementById(CtrlIdPrefix + "ddlAccountingPeriod").focus();
        return false;
    }

    
    if (Supplier == "-- Select --") {
        alert('Please select a Supplier.');
        document.getElementById(CtrlIdPrefix + "ddlSupplier").focus();
        return false;
    }

    if (SupplyPlant == "") {
        alert('Please select a Supply Plant.');
        document.getElementById(CtrlIdPrefix + "ddlSupplyPlant").focus();
        return false;
    }

    if (Customer == "0") {
        alert('Please select a Customer.');
        document.getElementById(CtrlIdPrefix + "ddlCustomer").focus();
        return false;
    }

    if (SLBValue == "") {
        alert('Please select Customer Location.');
        document.getElementById(CtrlIdPrefix + "ddlSLBValue").focus();
        return false;
    }

    if (SLBType == "") {
        alert('Please select TOD Target Type.');
        document.getElementById(CtrlIdPrefix + "ddlSLBType").focus();
        return false;
    }

    if (CDtype == "") {
        alert('Please select Before/After CD Indicator.');
        document.getElementById(CtrlIdPrefix + "ddlBeforeAfterCD").focus();
        return false;
    }

    if (TODMonth == "") {
        alert('Please select TOD Month.');
        document.getElementById(CtrlIdPrefix + "ddlTODMonthsGOGO").focus();
        return false;
    } 

    if (SLBPercentage == "") {
        alert('Dealer is not Eligible for TOD due to Low Sale Value as per Supplier TOD Policy.');
        document.getElementById(CtrlIdPrefix + "ddlSLBPercentage").focus();
        return false;
    }

    if (SLBPercentage == "0.00" || SLBPercentage == "0") {
        alert('SLB Percentage Does not Exists. Please Check the Data');
        return false;
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

function funTODGenerationSubmit() {
    if (funTODGenerationHeaderValidation()) {
        
        if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
            alert('No Item details are found.');
            return false;
        }

        if (document.getElementById(CtrlIdPrefix + "ChkStatus").value == "0") {
            alert('Please Select atleast a Document # to Pass on TOD Credit Note.');
            return false;
        }
        
//        var gItemsDtl = document.getElementById(CtrlIdPrefix + "grvItemDetails");
//        if (gItemsDtl != null) {
//            var rowscount = gItemsDtl.rows.length;
//        }
//        
//        var txtInputs = gItemsDtl.getElementsByTagName("input");

//        for (i = 0; i < rowscount; i++) {
//            if (txtInputs[i].getAttribute("type") == "checkbox") {

//                alert(gItemsDtl.rows[i].cells[0].children[0]);
//            }
//     
//        }
        
//        var hdnTxtCtrl = document.getElementById(CtrlIdPrefix + "txtHdnGridCtrls");

//        var ctrlArr = hdnTxtCtrl.value.trim().split('|');
//        var IsAnyRowSelected = "False";

//        for (var i = 0; i <= ctrlArr.length - 1; i++) {
//            var CtrlInner = ctrlArr[i].split(',');
//            if (CtrlInner[0].trim() != '') {
//                if ((document.getElementById(CtrlInner[0]) != null) && (document.getElementById(CtrlInner[0]) != undefined)) {
//                    if (document.getElementById(CtrlInner[0]).checked) {
//                        IsAnyRowSelected = "True";
//                    }
//                }
//            }
//        }

//        if (IsAnyRowSelected == "True") {
            return true;
//        }
//        else {
//            alert("No Items has been selected.");
//            return false;
//        }
    }
    else
        return false;
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

function SelectedChange(lnk) {
    var row = lnk.parentNode.parentNode;
    Check = row.cells[0].children[0];
    var TODtotal = 0;
    var ChkCnt = 0;
    var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
    var gridview = document.getElementById(CtrlIdPrefix + "grvItemDetails");
    var rownum = String(gridview.rows.length);

    if (rownum.length == 1)
        rownum = "0" + rownum;

    if (supplier.value == '980') {
        var TodPercentage = document.getElementById(CtrlIdPrefix + "ddlSLBPercentage");

        for (i = 1; i <= gridview.rows.length - 2; i++) {
            var row = gridview.rows[i];
            chkBox1 = row.cells[0].children[0];
            todval = row.cells[3].children[0];
            Totaltodval = row.cells[4].children[0];

            if (chkBox1.checked) {
                Totaltodval.value = round(parseFloat(todval.value) * (parseFloat(TodPercentage.value) / 100), 2);
                TODtotal += parseFloat(Totaltodval.value);

                ChkCnt++;
            }
            else {
                Totaltodval.value = "0.00";
            }
        }
    }
    else {
        var ColId = gridview.rows[0].cells.length - 1;

        for (i = 1; i <= gridview.rows.length - 2; i++) {
            var row = gridview.rows[i];
            chkBox1 = row.cells[0].children[0];

            if (chkBox1.checked) {
                todval = row.cells[ColId].children[0];
                TODtotal += parseFloat(todval.value);

                ChkCnt++;
            }
        }
    }

    if (ChkCnt != gridview.rows.length - 2) {
        document.getElementById("ctl00_CPHDetails_grvItemDetails_ctl01_chkSelectAll").checked = false;
    }
    else {
        document.getElementById("ctl00_CPHDetails_grvItemDetails_ctl01_chkSelectAll").checked = true;
    }

    document.getElementById("ctl00_CPHDetails_ChkStatus").value = ChkCnt;
    document.getElementById("ctl00_CPHDetails_grvItemDetails_ctl" + rownum + "_txtTotalTODValue").value = parseFloat(Math.round(TODtotal * 100) / 100).toFixed(2);
}


function EnableAllCheckboxes(id) {
    var chkAllBox = document.getElementById(id);
    var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
    var gridview = document.getElementById(CtrlIdPrefix + "grvItemDetails");
    var rownum = String(gridview.rows.length);
    var TODtotal = 0;
    var ChkCnt = 0;    

    if (rownum.length == 1)
        rownum = "0" + rownum;

    if (supplier.value == '980') {
        var TodPercentage = document.getElementById(CtrlIdPrefix + "ddlSLBPercentage");

        if (chkAllBox.checked) {
            for (i = 1; i <= gridview.rows.length - 2; i++) {
                var row = gridview.rows[i];
                chkBox = row.cells[0].children[0];
                chkBox.checked = true;
                todval = row.cells[3].children[0];
                Totaltodval = row.cells[4].children[0];

                Totaltodval.value = round(parseFloat(todval.value) * (parseFloat(TodPercentage.value) / 100), 2);
                TODtotal += parseFloat(Totaltodval.value);
                ChkCnt++;
            }
        }
        else {
            for (i = 1; i <= gridview.rows.length - 2; i++) {
                var row = gridview.rows[i];
                chkBox = row.cells[0].children[0];
                Totaltodval = row.cells[4].children[0];

                Totaltodval.value = "0.00";
                chkBox.checked = false;
            }
        }
    }
    else {
        var ColId = gridview.rows[0].cells.length - 1;

        if (chkAllBox.checked) {
            for (i = 1; i <= gridview.rows.length - 2; i++) {
                var row = gridview.rows[i];
                chkBox = row.cells[0].children[0];
                chkBox.checked = true;
                todval = row.cells[ColId].children[0];
                TODtotal += parseFloat(todval.value);
                ChkCnt++;
            }
        }
        else {
            for (i = 1; i <= gridview.rows.length - 2; i++) {
                var row = gridview.rows[i];
                chkBox = row.cells[0].children[0];
                chkBox.checked = false;
            }
        }
    }

    document.getElementById("ctl00_CPHDetails_ChkStatus").value = ChkCnt;
    document.getElementById("ctl00_CPHDetails_grvItemDetails_ctl" + rownum + "_txtTotalTODValue").value = parseFloat(Math.round(TODtotal * 100) / 100).toFixed(0);

    return true;
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