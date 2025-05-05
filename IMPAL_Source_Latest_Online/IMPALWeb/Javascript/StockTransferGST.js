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

function IsFutureDate(sender, args) {
    if (sender._selectedDate > new Date()) {
        alert('Future date can not be selected.');
        //sender._textbox.set_Value("");
        var dtDate = new Date();
        dtDate = dtDate.format(sender._format);
        sender._textbox.set_Value(dtDate);
        return false;
    }
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

function funBtnAddRow() {
    //alert('asda');
    if (StockTransferHeaderValidation()) {
        document.getElementById(CtrlIdPrefix + 'ddlTransactionType').disabled = true;
        document.getElementById(CtrlIdPrefix + 'ddlToBranch').disabled = true;
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
    if (confirm("Are you sure you want reset the page?"))
        return true;
    else
        return false;
}

function funOnBlurForCCWH() {
    //alert('asd');
    return true;
}

function funOnChangeStdnMode(RefStockTranNoID) {
    //alert('asd');
    document.getElementById(RefStockTranNoID).value = "";
    return false;
}

function STGridValidation() {

    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grvItemDetails");
    if (gItemsDtl != null) {
        var rowscount = gItemsDtl.rows.length;
        if (rowscount >= 3) {
            for (k = 1; k < gItemsDtl.rows.length - 1; k++) {
                var row = gItemsDtl.rows[k];
                txtSno = row.cells[1].children[0];

                if (txtSno != null) {
                    txtBalQty = row.cells[6].children[0];
                    txtQty = row.cells[7].children[0];

                    if (txtQty.id != null && txtQty.id != "") {
                        if (txtQty.value == "0" || txtQty.value == "0.00" || txtQty.value == "") {
                            alert("Please enter quantity in row " + k);
                            document.getElementById(txtQty.id).focus();
                            return false;
                        }
                    }

                    if (txtBalQty.id != null && txtBalQty.id != "") {
                        if (parseInt(txtQty.value) > parseInt(txtBalQty.value)) {
                            alert("Quantity Should not exceed " + txtBalQty.value + " in row " + k);
                            txtQty.value = "";
                            document.getElementById(txtQty.id).focus();
                            return false;
                        }
                    }
                }
            }
        }
    }

    return true;
}


function funSTQuantityValidation(AvailableQtyCtrlID, QuantityCtrlID, CostPerQtyID, TotCostPriceID) {
    var AvailableQtyCtrlID1 = document.getElementById(AvailableQtyCtrlID);
    var QuantityCtrlID1 = document.getElementById(QuantityCtrlID);
    var CostPerQtyID1 = document.getElementById(CostPerQtyID);
    var TotCostPriceID1 = document.getElementById(TotCostPriceID);

    if ((QuantityCtrlID1 != null) && (QuantityCtrlID1 != undefined) && (QuantityCtrlID1.value != 0) && (QuantityCtrlID1.value.trim() != "")) {

        if (parseInt(AvailableQtyCtrlID1.value) == 0) {
            alert('Selected item is not in stock.');
            return false;
        }

        if (parseInt(QuantityCtrlID1.value) > parseInt(AvailableQtyCtrlID1.value)) {
            alert('Transfer Quantity can not be greater than Available Qty.');
            QuantityCtrlID1.value = "";
            QuantityCtrlID1.focus();
            return false;
        }

        var cPrice = parseFloat(QuantityCtrlID1.value) * parseFloat(CostPerQtyID1.value.trim());
        TotCostPriceID1.value = cPrice.toFixed(2);
        return true;
    }
    else {
        alert('Please enter valid Quantity information.');
        QuantityCtrlID1.value = "";
        QuantityCtrlID1.focus();
        return false;
    }

    return true;
}


function StockTransferHeaderValidation() {
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

    var ddlToBrnch = document.getElementById(CtrlIdPrefix + "ddlToBranch");
    var ToBranch = ddlBrnch.options[ddlToBrnch.selectedIndex].value;

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

    if (ToBranch == "0") {
        alert('Please select a To Branch.');
        ddlToBrnch.focus();
        return false;
    }

    if (Branch == ToBranch) {
        alert('From Branch and To Branch can not be same.');
        ddlToBrnch.focus();
        return false;
    }

    return true;
}

function StockTransferEntrySubmit() {
    if (StockTransferHeaderValidation()) {

        if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
            alert('No Item details are found.');
            return false;
        }

        if (GridCheck()) {
            /*
            //Check the footer summation is equal to the invoice value.    
            var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
            var ItemSummation = document.getElementById(footerid).innerText;
            var invoiceValue = document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value;
            //alert(footerid);
            //alert(document.getElementById(footerid).innerText);
            var differenceAmount = parseFloat(invoiceValue) - parseFloat(ItemSummation);
            //alert(parseFloat(differenceAmount));
            //alert(Math.abs(differenceAmount));            
          
                if ((Math.abs(differenceAmount) > 50)) 
            {
            alert('Invoice value and Items values difference exceeds the allowable limit.');
            return false;
            }
            */
            return true; //post back gets happen for save/update.
        }
        else
            return false;
    }
    else
        return false;

}

function GridCheck() {
    //GetTotal();

    var hdnTxtCtrl = document.getElementById(CtrlIdPrefix + "txtHdnGridCtrls");
    //alert(hdnTxtCtrl.value.trim());
    var ctrlArr = hdnTxtCtrl.value.trim().split(',');
    for (var i = 0; i <= ctrlArr.length - 1; i++) {
        //alert(ctrlArr[i]);
        if (ctrlArr[i].trim() != '') {
            if ((document.getElementById(ctrlArr[i]) != null) && (document.getElementById(ctrlArr[i]) != undefined)) {
                if (document.getElementById(ctrlArr[i]).value.trim() == "0") {
                    alert('Please Select a value.');
                    document.getElementById(ctrlArr[i]).focus();
                    return false;
                }
                else if (document.getElementById(ctrlArr[i]).value.trim() == "") {
                    alert('Value should not be empty.');
                    document.getElementById(ctrlArr[i]).focus();
                    return false;
                }
            }
        }
    }

    var hdnScreenMode = document.getElementById(CtrlIdPrefix + "hdnScreenMode");

    if (hdnScreenMode.value == "A") {
        STGridValidation();        
    }
    
    return true;
}


function GetTotal() {
    totalListPrice = 0;
    totalCostPrice = 0;

    // Get the gridview
    var grid = document.getElementById("ctl00_CPHDetails_grvItemDetails");

    // Get all the input controls (can be any DOM element you would like)
    var inputs = grid.getElementsByTagName("input");

    // Loop through all the DOM elements we grabbed
    for (var i = 0; i < inputs.length; i++) {

        // In this case we are looping through all the Dek Volume and then the Mcf volume boxes in the grid and not an individual one and totalling them
        if (inputs[i].name.indexOf("txtListPrice") > 1) {
            if (inputs[i].value != "") {
                totalListPrice = totalListPrice + parseInt(inputs[i].value);
            }
        }

        if (inputs[i].name.indexOf("txtCostPrice") > 1) {
            //alert(inputs[i].value);
            if (inputs[i].value != "") {
                //alert(inputs[i].value);
                //alert(parseFloat(inputs[i].value));
                totalCostPrice = parseFloat(totalCostPrice) + parseFloat(inputs[i].value);
            }

        }
        //alert(inputs[i].name);
    }
    //alert(totalListPrice);
    //alert(totalCostPrice);
    var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
    //alert(footerid);
    //alert(document.getElementById(footerid).innerText);
    document.getElementById(footerid).innerText = totalCostPrice.toFixed(2);
}


function funGrnSubmit() {
    //alert("FUN_GRN_Submit");

    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var ddlInwardNo = document.getElementById(CtrlIdPrefix + "ddlInwardNumber");
    var InwardNo = ddlInwardNo.options[ddlInwardNo.selectedIndex].value;

    var inwardDate = document.getElementById(CtrlIdPrefix + "txtInwardDate");

    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

    var ddlSupp = document.getElementById(CtrlIdPrefix + "ddlSupplierName");
    var Supplier = ddlSupp.options[ddlSupp.selectedIndex].value;

    var ddlPackageStatus = document.getElementById(CtrlIdPrefix + "ddlPackageStatus");
    var PackageStatus = ddlPackageStatus.options[ddlPackageStatus.selectedIndex].value;

    var PackageOpenDate = document.getElementById(CtrlIdPrefix + "txtPackageOpenDate");

    var ddlLRTransfer = document.getElementById(CtrlIdPrefix + "ddlLRTransfer");
    var LRTransfer = ddlPackageStatus.options[ddlLRTransfer.selectedIndex].value;

    var localPurchaseTax = document.getElementById(CtrlIdPrefix + "txtLocalPurchaseTax");

    var Remarks = document.getElementById(CtrlIdPrefix + "txtRemarks");
    var ClearingAgentNo = document.getElementById(CtrlIdPrefix + "txtClearingAgentNo");
    var CheckPostName = document.getElementById(CtrlIdPrefix + "txtCheckPostName");
    var ClearenceDate = document.getElementById(CtrlIdPrefix + "txtClearenceDate");
    var ClearenceAmount = document.getElementById(CtrlIdPrefix + "txtClearenceAmount");


    //alert('<%=ViewState("GridRowCount")%>');
    if (Branch == "0") {
        alert('Please select a Branch.');
        ddlBrnch.focus();
        return false;
    }

    if (InwardNo == "0") {
        alert('Please select an Inward No.');
        ddlInwardNo.focus();
        return false;
    }
    if (inwardDate.value.trim() == "") {
        alert("Inward Date should not be empty.");
        inwardDate.value = "";
        inwardDate.focus();
        return false;
    }

    if (TransType == "0") {

    }

    if (Supplier == "0") {
        alert("Please select a Supplier.");
        ddlSupp.focus();
        return false;
    }

    if (PackageStatus == "0") {
        alert("Please select package status.");
        ddlPackageStatus.focus();
        return false;
    }

    if (PackageOpenDate.value.trim() == "") {
        alert("Package Open Date should not be empty.");
        PackageOpenDate.value = "";
        PackageOpenDate.focus();
        return false;
    }

    if (LRTransfer == "0") {
        alert("Please select LR Transfer.");
        ddlLRTransfer.focus();
        return false;
    }

    /*if (localPurchaseTax.value.trim() == "") {
    alert("Local purchase tax should not be empty.");
    localPurchaseTax.value = "";
    localPurchaseTax.focus();
    return false;
    }*/

    var Remarks = document.getElementById(CtrlIdPrefix + "txtRemarks");
    var ClearingAgentNo = document.getElementById(CtrlIdPrefix + "txtClearingAgentNo");
    var CheckPostName = document.getElementById(CtrlIdPrefix + "txtCheckPostName");
    var ClearenceDate = document.getElementById(CtrlIdPrefix + "txtClearenceDate");
    var ClearenceAmount = document.getElementById(CtrlIdPrefix + "txtClearenceAmount");

    if (ClearingAgentNo.value.trim() == "") {
        alert("Clearing agent no. should not be empty.");
        ClearingAgentNo.value = "";
        ClearingAgentNo.focus();
        return false;
    }

    if (!CheckGRNGridDuringUpdate()) {
        return false;
    }

    return true;
}

function funSetDestination() {
    var ddlToBrnch = document.getElementById(CtrlIdPrefix + "ddlToBranch");
    var ToBranch = ddlToBrnch.options[ddlToBrnch.selectedIndex].text;

    if (ToBranch != "0")
        document.getElementById(CtrlIdPrefix + "txtDestination").value = ToBranch;
    else
        document.getElementById(CtrlIdPrefix + "txtDestination").value = "";

    return false;
}