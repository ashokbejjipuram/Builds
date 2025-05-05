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

function CurrencyNumberOnly() {
    var AsciiValue = event.keyCode
    //alert(AsciiValue);
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

function AlphaNumericWithSlash() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 47) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
        event.returnValue = true;
    else
        event.returnValue = false;
}


function checkDate(sender, args) {
    var dtDate = new Date();
    dtDate.setDate(dtDate.getDate() - 90);
    //alert(dtDate);

    if (!((sender._selectedDate >= dtDate) && (sender._selectedDate <= new Date()))) {
        alert('Selected Date can not be a future date.');
        //sender._textbox.set_Value("");
        var dtDate = new Date();
        dtDate = dtDate.format(sender._format);
        sender._textbox.set_Value(dtDate);
    }
}

function CurrencyChkForMoreThanOneDot(strValue) {
    //alert('asd');
    //alert(strValue.split('.').length);
    if (strValue.split('.').length > 2)
        return false;
    else
        return true;
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

function funBtnAddRow() {
    //alert('asda');
    if (StockTransferReceiptHeaderValidation()) {
        document.getElementById(CtrlIdPrefix + 'ddlTransactionType').disabled = true;
        document.getElementById(CtrlIdPrefix + 'ddlFromBranch').disabled = true;
        //document.getElementById(CtrlIdPrefix + 'txtInvoiceValue').disabled = true;

        if (!GridCheck())
            return false;

        return true;
    }
    else
        return false;
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

function funReset() {
    //if (confirm("You will loose the unsaved informations.\n\nAre you sure you want reset the page?"))
    if (confirm("Are you sure you want reset the page?"))
        return true;
    else
        return false;
}

function funAcceptedQtyValidation(ReceivedQtyID, AcceptedQtyID, CostPriceQtyID, TotCostPriceID) {
    var ReceivedQty = document.getElementById(ReceivedQtyID);
    var AcceptedQty = document.getElementById(AcceptedQtyID);
    var CostPriceQty = document.getElementById(CostPriceQtyID);
    var TotCostPrice = document.getElementById(TotCostPriceID);

    if ((ReceivedQty != null) && (ReceivedQty != undefined) && (ReceivedQty.value != 0) && (ReceivedQty.value.trim() != "")) {
        if ((AcceptedQty != null) && (AcceptedQty != undefined)) {
            if (parseFloat(AcceptedQty.value.trim()) > parseFloat(ReceivedQty.value.trim())) {
                alert('Accepted Quantity should not be greater than the Received Quantity.');
                AcceptedQty.value = "0";
                GetTotal();                
                AcceptedQty.focus();
                return false;
            }
            
            funSTReceiptValidation(AcceptedQtyID, CostPriceQtyID, TotCostPriceID);
            
            if (parseFloat(AcceptedQty.value.trim()) < parseFloat(ReceivedQty.value.trim())) {
                document.getElementById(CtrlIdPrefix + "txtWareHouseNo").disabled = false;
                document.getElementById(CtrlIdPrefix + "txtWarehouseDate").disabled = false;
                return true;
            }
            else {
                document.getElementById(CtrlIdPrefix + "txtWareHouseNo").disabled = true;
                document.getElementById(CtrlIdPrefix + "txtWarehouseDate").disabled = true;
                return true;
            }
        }
        else
            return false;
    }
    else {
        alert('Please enter valid Accepted Quantity.');
        AcceptedQty.focus();
        return false;
    }
    
    return true;
}

function funSTReceiptValidation(ReceivedQtyCtrlID, CostPerQtyCtrlID, TotalCostPriceCtrlID) {
    var ReceivedQtyCtrlID1 = document.getElementById(ReceivedQtyCtrlID);
    var CostPerQtyCtrlID1 = document.getElementById(CostPerQtyCtrlID);
    var TotalCostPriceCtrlID1 = document.getElementById(TotalCostPriceCtrlID);

    if ((ReceivedQtyCtrlID1 != null) && (ReceivedQtyCtrlID1 != undefined) && (ReceivedQtyCtrlID1.value.trim() != "")) {        
        var cPrice = parseFloat(ReceivedQtyCtrlID1.value) * parseFloat(CostPerQtyCtrlID1.value.trim());
        TotalCostPriceCtrlID1.value = cPrice.toFixed(2);
        GetTotal();
        return true;
    }
}

function StockTransferReceiptHeaderValidation() {
    var ddlSTDNNumberOnline = document.getElementById(CtrlIdPrefix + "ddlSTDNNumberOnline");
    var STDNNumberOnline = ddlSTDNNumberOnline.options[ddlSTDNNumberOnline.selectedIndex].value;

    var ddlBranch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBranch.options[ddlBranch.selectedIndex].value;

    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

    var ddlFromBranch = document.getElementById(CtrlIdPrefix + "ddlFromBranch");
    var FromBranch = ddlFromBranch.options[ddlFromBranch.selectedIndex].value;

    var InvoiceValue = document.getElementById(CtrlIdPrefix + "txtInvoiceValue");
    var IGSTValue = document.getElementById(CtrlIdPrefix + "txtIGSTValue");
    var WareHouseNo = document.getElementById(CtrlIdPrefix + "txtWareHouseNo");
    var WareHouseDt = document.getElementById(CtrlIdPrefix + "txtWarehouseDate");
    var Status = document.getElementById(CtrlIdPrefix + "hdninterStateStatus");

    var LRNumber = document.getElementById(CtrlIdPrefix + "txtLRNumber");
    var LRDate = document.getElementById(CtrlIdPrefix + "txtLRDate");

    if (STDNNumberOnline == "") {
        alert('Please select STDN From Branch Number.');
        ddlSTDNNumberOnline.focus();
        return false;
    }
    
    if (Branch == "0") {
        alert('Please select a Branch.');
        ddlBrnch.focus();
        return false;
    }

    if (TransType == "0") {
        alert('Please select a Transaction Type.');
        ddlTransType.focus();
        return false;
    }

    if (FromBranch == "0") {
        alert('Please select a From Branch.');
        ddlFromBranch.focus();
        return false;
    }

    if (Branch == FromBranch) {
        alert('Branch and From Branch can not be same.');
        ddlFromBranch.focus();
        return false;
    }

    if (InvoiceValue.value.trim() == "") {
        alert("Invoice value should not be empty.");
        InvoiceValue.value = "";
        InvoiceValue.focus();
        return false;
    }

    if ((InvoiceValue.value.trim() == "0") || (InvoiceValue.value.trim() == "0.00")) {
        alert("Please enter valid Invoice value");
        InvoiceValue.value = "";
        InvoiceValue.focus();
        return false;
    }

    if (!CurrencyChkForMoreThanOneDot(InvoiceValue.value.trim())) {
        alert("Please enter valid Invoice Value.");
        InvoiceValue.focus();
        return false;
    }

    if (Status.value.trim() != "1") {
        if (IGSTValue.value.trim() == "") {
        alert("IGST Value should not be empty.");
        IGSTValue.value = "";
        IGSTValue.focus();
        return false;
        }

    if ((IGSTValue.value.trim() == "0") || (IGSTValue.value.trim() == "0.00")) {
        alert("Please enter valid IGST value");
        IGSTValue.value = "";
        IGSTValue.focus();
        return false;
        }
    }

    if (!CurrencyChkForMoreThanOneDot(IGSTValue.value.trim())) {
        alert("Please enter valid IGST Value.");
        IGSTValue.focus();
        return false;
    }

    if (!WareHouseNo.disabled) {
        if (WareHouseNo.value.trim() == "") {
            alert("Warehouse No. should not be empty");
            WareHouseNo.focus();
            return false;
        }
    }

    if (!WareHouseDt.disabled) {
        if (WareHouseDt.value.trim() == "") {
            alert("Warehouse Date should not be empty");
            WareHouseDt.focus();
            return false;
        }
    }
    
    if (LRNumber.value.trim() == "") {
        alert("LR Number should not be empty.");
        LRNumber.value = "";
        LRNumber.focus();
        return false;
    }

    if (LRDate.value.trim() == "") {
        alert("LR Date should not be empty.");
        LRDate.value = "";
        LRDate.focus();
        return false;
    }

    return true;
}

function StockTransferReceiptSubmit(s) {
    if (s != "M") {
        if (StockTransferReceiptHeaderValidation()) {

            if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
                alert('No Item details are found.');
                return false;
            }

            if (GridCheck()) {

                var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
                var ItemSummation = document.getElementById(footerid).innerText;
                var invoiceValue = document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value;
                var IGSTValue = document.getElementById(CtrlIdPrefix + "txtIGSTValue").value;
                var footeridTax = document.getElementById(CtrlIdPrefix + "hdnFooterTaxValue").value;
                var TaxSummation = document.getElementById(footeridTax).innerText;

                var differenceAmount = (parseFloat(invoiceValue) + parseFloat(IGSTValue)) - (parseFloat(ItemSummation) + parseFloat(TaxSummation));
                           
                if (differenceAmount < 0)
                    differenceAmount = differenceAmount * (-1)

                if ((differenceAmount > 1)) {
                    alert('Invoice value and Items values difference exceeds the allowable limit.');
                    return false;
                }

                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    else {
        var Status = "Are you Sure to Approve the STDN inWard";

        if (confirm(Status))
            return true;
        else
            return false;
    }

}

function funSTDNinwardReject() {
    var Remarks = prompt("Are you Sure to Reject the STDN inWard? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
}

function Validate(id, msg) {

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
                alert(msg + " date should be less than or equal to sysdate");
            }
        }
    }
}

function GridCheck() {
    GetTotal();

    var hdnTxtCtrl = document.getElementById(CtrlIdPrefix + "txtHdnGridCtrls");
    
    var ctrlArr = hdnTxtCtrl.value.trim().split(',');
    for (var i = 0; i <= ctrlArr.length - 1; i++) {
        if (ctrlArr[i].trim() != '') {
            var HeaderLabel = getLabelName(ctrlArr[i]);

            if ((document.getElementById(ctrlArr[i]) != null) && (document.getElementById(ctrlArr[i]) != undefined)) {
                if (document.getElementById(ctrlArr[i]).value.trim() == "") {
                    alert(HeaderLabel + ' should not be blank.');
                    document.getElementById(ctrlArr[i]).focus();
                    return false;
                }
            }
        }
    }
    
    return true;
}

function getLabelName(strCtrlID) {
    var labelName = "";
    var inputArray = strCtrlID.split('_');
    var crrlId = inputArray[4];

    if (crrlId.trim() == "txtCCWHNo") {
        labelName = "Reference Stock Transfer No.";
    }
    else if (crrlId.trim() == "txtRefStockTransfeDate") {
        labelName = "Reference Stock Transfer Date";
    }
    else if (crrlId.trim() == "ddlSupplierName") {
        labelName = "Supplier Line";
    }
    else if (crrlId.trim() == "txtSupplierPartNo") {
        labelName = "Supplier Part #";
    }
    else if (crrlId.trim() == "ddlSupplierPartNo") {
        labelName = "Supplier Part #";
    }
    else if (crrlId.trim() == "txtItemCode") {
        labelName = "Supplier Part #";
    }
    else if (crrlId.trim() == "txtCstPricePerQty") {
        labelName = "Cost Price/Qty";
    }
    else if (crrlId.trim() == "txtOriginalReceiptDate") {
        labelName = "Original Receipt Date";
    }
    else if (crrlId.trim() == "txtReceivedQuantity") {
        labelName = "Received Qty";
    }
    else if (crrlId.trim() == "txtAcceptedQuantity") {
        labelName = "Accepted Qty";
    }
    else if (crrlId.trim() == "txtItemLocation") {
        labelName = "Location Code";
    }
    else if (crrlId.trim() == "txtInvoiceNumber") {
        labelName = "Invoice Number";
    }
    else if (crrlId.trim() == "txtInvoiceDate") {
        labelName = "Invoice Date";
    }

    return labelName;
}

function GetTotal() {
    totalCostPrice = 0;
    TaxValue = 0;
    CostPrice = 0;
    totalTaxValue = 0;

    // Get the gridview
    var grid = document.getElementById("ctl00_CPHDetails_grvItemDetails");

    // Get all the input controls (can be any DOM element you would like)
    //var inputs = grid.getElementsByTagName("input");
    if (grid != null) {
        var rowscount = grid.rows.length;
    }

    var interStateStatus = document.getElementById(CtrlIdPrefix + "hdninterStateStatus");

    if (rowscount >= 3) {
        for (k = 1; k < rowscount - 1; k++) {
            var row = grid.rows[k];

            txtPartNo = row.cells[2].childNodes[0];
            txtTotCostPr = row.cells[9].children[0];
            txtGSTPer = row.cells[11].children[0];
            txtTaxGroupCode = row.cells[11].children[1];

            var browserName = navigator.appName;

            if (browserName == 'Netscape') {
                txtPartNo = row.cells[2].childNodes[1];
                txtTotCostPr = row.cells[9].childNodes[1];
                txtGSTPer = row.cells[11].childNodes[1];
                txtTaxGroupCode = row.cells[11].childNodes[3];
            }

            if (txtGSTPer.value != "") {
                TaxValue = txtGSTPer.value.replace("%", "");

                if (TaxValue == "")
                    TaxValue = 0;

                if (txtTaxGroupCode.value == "1" && parseFloat(TaxValue) != 28 && interStateStatus.value != "1") {
                    alert("Part No. " + txtPartNo.value + " is of 28% Slab. Please Get the Approval from HO to Change the GST Tax.");
                    //txtGSTPer.value = "28.00%";
                    //txtGSTPer.focus();
                    return false;
                }

                if (txtTaxGroupCode.value == "5" && parseFloat(TaxValue) != 18 && interStateStatus.value != "1") {
                    alert("Part No. " + txtPartNo.value + " is of 18% Slab. Please Get the Approval from HO to Change the GST Tax.");
                    //txtGSTPer.value = "18.00%";
                    //txtGSTPer.focus();
                    return false;
                }

                if (txtTotCostPr.value != "") {
                    totalCostPrice = parseFloat(totalCostPrice) + parseFloat(txtTotCostPr.value);
                    CostPrice = parseFloat(txtTotCostPr.value);
                }

                if (txtTotCostPr.value == "")
                    txtTotCostPr.value = 0;

                totalTaxValue = totalTaxValue + ((parseFloat(txtTotCostPr.value) * parseFloat(TaxValue)) / 100);
            }
        }
    }
    
    var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
    var footeridTax = document.getElementById(CtrlIdPrefix + "hdnFooterTaxValue").value;

    document.getElementById(footerid).innerText = totalCostPrice.toFixed(2);
    document.getElementById(footeridTax).innerText = totalTaxValue.toFixed(2);
}

function CheckOrginalreciptdate(id, isFutureDate, Msg) {
    
    var idRefDocDate = id.replace("txtOriginalReceiptDate", "txtRefStockTransfeDate");
    var idDate = document.getElementById(id).value;
    var idOrginalReciptDate = document.getElementById(idRefDocDate).value;

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

            var RecDate = new Date();
            RecDate = convertDate(idDate);

            var OriginalRecDate = new Date();
            OriginalRecDate = convertDate(idOrginalReciptDate);

            if (RecDate > OriginalRecDate) {
                alert('Original Receipt Date(' + document.getElementById(id).value + ') should be less than the Reference Stock Transfer Date.');
                document.getElementById(id).value = "";
                document.getElementById(id).focus();
                return false;
            }
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
