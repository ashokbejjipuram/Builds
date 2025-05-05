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
    if (PurchaseReturnHeaderValidation()) {
        document.getElementById(CtrlIdPrefix + 'ddlTransactionType').disabled = true;
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

function funOnChangeStdnMode(RefStockTranNoID) {
    //alert('asd');
    document.getElementById(RefStockTranNoID).value = "";
    return false;
}

function GridValidation() {
    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grvItemDetails");
    if (gItemsDtl != null) {
        var rowscount = gItemsDtl.rows.length;

        if (rowscount >= 3) {
            for (k = 1; k < gItemsDtl.rows.length - 1; k++) {
                var row = gItemsDtl.rows[k];
                ChkBox = row.cells[0].children[0];
                
                if (ChkBox.checked) {
                    txtGRNQty = row.cells[4].children[0];
                    txtBalQty = row.cells[5].children[0];
                    txtQty = row.cells[6].children[0];

                    if (txtQty.id != null && txtQty.id != "") {
                        if (txtQty.value == "0" || txtQty.value == "0.00" || txtQty.value == "") {
                            alert("Please enter quantity in row " + k);
                            document.getElementById(txtQty.id).focus();
                            return false;
                        }
                    }

                    if (txtGRNQty.id != null && txtGRNQty.id != "") {
                        if (parseInt(txtQty.value) > parseInt(txtGRNQty.value)) {
                            alert("Quantity Should not exceed GRN Qty. " + txtGRNQty.value + " in row " + k);                            
                            document.getElementById(txtQty.id).focus();
                            txtQty.value = "0";
                            return false;
                        }
                    }

                    if (txtBalQty.id != null && txtBalQty.id != "") {
                        if (parseInt(txtQty.value) > parseInt(txtBalQty.value)) {
                            alert("Quantity Should not exceed Available Qty. " + txtBalQty.value + " in row " + k);
                            document.getElementById(txtQty.id).focus();
                            txtQty.value = "0";
                            return false;
                        }
                    }
                }
            }
        }
    }

    return true;
}

function funQuantityValidationNew(InwardQtyCtrlID, AvailableQtyCtrlID, QuantityCtrlID, CostPerQtyID, TotCostPriceID) {
    var InwardQtyCtrlID1 = document.getElementById(InwardQtyCtrlID);
    var AvailableQtyCtrlID1 = document.getElementById(AvailableQtyCtrlID);
    var QuantityCtrlID1 = document.getElementById(QuantityCtrlID);
    var CostPerQtyID1 = document.getElementById(CostPerQtyID);
    var TotCostPriceID1 = document.getElementById(TotCostPriceID);

    if ((QuantityCtrlID1 != null) && (QuantityCtrlID1 != undefined) && (QuantityCtrlID1.value != 0) && (QuantityCtrlID1.value.trim() != "")) {
        if (parseInt(AvailableQtyCtrlID1.value) == 0) {
            alert('Selected item is not in stock.');
            return false;
        }

        if (parseInt(QuantityCtrlID1.value) > parseInt(InwardQtyCtrlID1.value)) {
            alert('Return Quantity can not be greater than GRN Qty.');
            QuantityCtrlID1.value = "";
            QuantityCtrlID1.focus();
            return false;
        }

        if (parseInt(QuantityCtrlID1.value) > parseInt(AvailableQtyCtrlID1.value)) {
            alert('Return Quantity can not be greater than Available Qty.');
            QuantityCtrlID1.value = "";
            QuantityCtrlID1.focus();
            return false;
        }

        var cPrice = parseFloat(QuantityCtrlID1.value) * parseFloat(CostPerQtyID1.value.trim());
        TotCostPriceID1.value = cPrice.toFixed(2);        

        TotalCostVal = 0.00;
        TotalTaxVal = 0.00;

        var gridview = document.getElementById("ctl00_CPHDetails_grvItemDetails");
        var rownum = String(gridview.rows.length);

        if (rownum.length == 1)
            rownum = "0" + rownum;

        for (i = 1; i <= gridview.rows.length - 2; i++) {
            var row = gridview.rows[i];
            chkBox1 = row.cells[0].children[0];
            TotCostPrice = row.cells[8].children[0];
            TotTaxValue = row.cells[9].children[0];

            if (chkBox1.checked) {
                TotalCostVal += parseFloat(TotCostPrice.value);
                TotalTaxVal += (parseFloat(TotCostPrice.value) * parseFloat(TotTaxValue.value)) / 100;
            }
        }

        var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
        var footerid_TaxValue = document.getElementById(CtrlIdPrefix + "hdnFooterTaxPrice").value;

        document.getElementById(footerid).innerText = TotalCostVal.toFixed(4);
        document.getElementById(footerid_TaxValue).innerText = TotalTaxVal.toFixed(4);
        document.getElementById(CtrlIdPrefix + "lblTotalPurchaseReturnVal").innerText = parseFloat(parseFloat(TotalCostVal.toFixed(4)) + parseFloat(TotalTaxVal.toFixed(4))).toFixed(0);

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

function funQuantityValidation(AvailableQtyCtrlID, QuantityCtrlID, CostPerQtyID, TotCostPriceID) {
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
            alert('Return Quantity can not be greater than Available Qty.');
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

function ValidateInwardNumber() {
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

    var txtInwardNumber = document.getElementById(CtrlIdPrefix + "txtInwardNumber");

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

    if (txtInwardNumber != null) {
        if (txtInwardNumber.value.trim() == "") {
            alert('Please Enter the Inward/GRN Number.');
            txtInwardNumber.focus();
            return false;
        }
    }
}

function SelectedChangeAll(id) {
    var chkAllBox = document.getElementById(id);
    var gridview = document.getElementById("ctl00_CPHDetails_grvItemDetails");
    var ChkCnt = 0;
    var rownum = String(gridview.rows.length);

    TotalCostVal = 0.00;
    TotalTaxVal = 0.00;

    if (rownum.length == 1)
        rownum = "0" + rownum;

    if (chkAllBox.checked) {
        for (i = 1; i <= gridview.rows.length - 2; i++) {
            var row = gridview.rows[i];
            chkBox = row.cells[0].children[0];
            txtQty = row.cells[6].children[0];
            TotCostPrice = row.cells[8].children[0];
            TotTaxValue = row.cells[9].children[0];
            chkBox.checked = true;
            txtQty.disabled = false;
            TotalCostVal += parseFloat(TotCostPrice.value);
            TotalTaxVal += (parseFloat(TotCostPrice.value) * parseFloat(TotTaxValue.value)) / 100;

            ChkCnt++;
        }
    }
    else {
        for (i = 1; i <= gridview.rows.length - 2; i++) {
            var row = gridview.rows[i];
            chkBox = row.cells[0].children[0];
            txtQty = row.cells[6].children[0];
            TotCostPrice = row.cells[8].children[0];
            TotTaxValue = row.cells[9].children[0];
            chkBox.checked = false;
            txtQty.value = "0";
            txtQty.disabled = true;
            TotCostPrice.value = 0;
            TotalCostVal = 0;
            TotalTaxVal = 0;            

            ChkCnt = 0;
        }
    }

    document.getElementById("ctl00_CPHDetails_ChkStatus").value = ChkCnt;
    var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
    var footerid_TaxValue = document.getElementById(CtrlIdPrefix + "hdnFooterTaxPrice").value;

    document.getElementById(footerid).innerText = TotalCostVal.toFixed(4);
    document.getElementById(footerid_TaxValue).innerText = TotalTaxVal.toFixed(4);
    document.getElementById(CtrlIdPrefix + "lblTotalPurchaseReturnVal").innerText = parseFloat(parseFloat(TotalCostVal.toFixed(4)) + parseFloat(TotalTaxVal.toFixed(4))).toFixed(0);
}

function SelectedChangeCheckBox(lnk) {
    var rowno = lnk.parentNode.parentNode;
    chkBox = rowno.cells[0].children[0];
    txtQty = rowno.cells[6].children[0];
    txtCostValue = rowno.cells[8].children[0];
    txtTaxValue = rowno.cells[9].children[0];

    TotalCostVal = 0.00;
    TotalTaxVal = 0.00;

    if (!chkBox.checked) {
        txtQty.value = "0";
        txtQty.disabled = true;
    }
    else
        txtQty.disabled = false;

    var ChkCnt = 0;

    var gridview = document.getElementById("ctl00_CPHDetails_grvItemDetails");
    var rownum = String(gridview.rows.length);

    if (rownum.length == 1)
        rownum = "0" + rownum;

    for (i = 1; i <= gridview.rows.length - 2; i++) {
        var row = gridview.rows[i];
        chkBox1 = row.cells[0].children[0];
        txtQty = row.cells[6].children[0];
        TotCostPrice = row.cells[8].children[0];
        TotTaxValue = row.cells[9].children[0];

        if (chkBox1.checked) {
            txtQty.disabled = false;
            TotalCostVal += parseFloat(TotCostPrice.value);
            TotalTaxVal += (parseFloat(TotCostPrice.value) * parseFloat(TotTaxValue.value)) / 100;
            ChkCnt++;
        }
    }    

    if (parseInt(ChkCnt) == parseInt(document.getElementById(CtrlIdPrefix + "hdnRowCnt").value))
        document.getElementById("ctl00_CPHDetails_grvItemDetails_ctl01_chkSelectAll").checked = true;
    else
        document.getElementById("ctl00_CPHDetails_grvItemDetails_ctl01_chkSelectAll").checked = false;

    document.getElementById("ctl00_CPHDetails_ChkStatus").value = ChkCnt;

    var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
    var footerid_TaxValue = document.getElementById(CtrlIdPrefix + "hdnFooterTaxPrice").value;

    document.getElementById(footerid).innerText = TotalCostVal.toFixed(4);
    document.getElementById(footerid_TaxValue).innerText = TotalTaxVal.toFixed(4);
    document.getElementById(CtrlIdPrefix + "lblTotalPurchaseReturnVal").innerText = parseFloat(parseFloat(TotalCostVal.toFixed(4)) + parseFloat(TotalTaxVal.toFixed(4))).toFixed(0);
}


function PurchaseReturnHeaderValidation() {
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var ddlPurchaseReturnNumber = document.getElementById(CtrlIdPrefix + "ddlPurchaseReturnNumber");

    if (ddlPurchaseReturnNumber == null) {
        var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
        var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

        var txtInwardNumber = document.getElementById(CtrlIdPrefix + "txtInwardNumber");

        var ddlInwardNumber = document.getElementById(CtrlIdPrefix + "ddlInwardNumber");

        if (ddlInwardNumber != null)
            var InwardNumber = ddlInwardNumber.options[ddlInwardNumber.selectedIndex].value;
        else
            var InwardNumber = "0";

        var ddlSupplierName = document.getElementById(CtrlIdPrefix + "ddlSupplierName");
        var SupplierName = ddlSupplierName.options[ddlSupplierName.selectedIndex].value;

        var ddlSupplyPlant = document.getElementById(CtrlIdPrefix + "ddlSupplyPlant");
        var ddlRemarks = document.getElementById(CtrlIdPrefix + "ddlRemarks");

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

        if (txtInwardNumber != null) {
            if (txtInwardNumber.value.trim() == "") {
                alert('Please Enter the Inward/GRN Number.');
                txtInwardNumber.focus();
                return false;
            }
        }

        if (ddlInwardNumber != null) {
            if (InwardNumber == "0") {
                alert('Please select an Inward/GRN Number.');
                ddlInwardNumber.focus();
                return false;
            }
        }

        if (SupplierName == "0" || ddlSupplyPlant.selectedIndex == -1) {
            alert('Please select a Supplier Name.');
            ddlSupplierName.focus();
            return false;
        }

        var SupplyPlant = ddlSupplyPlant.options[ddlSupplyPlant.selectedIndex].value;

        if (SupplyPlant == "0") {
            alert('Please select a Supplier Plant.');
            ddlSupplyPlant.focus();
            return false;
        }

        var Remarks = ddlRemarks.options[ddlRemarks.selectedIndex].value;

        if (Remarks.trim() == "") {
            alert('Please select a Reason for Return.');
            ddlRemarks.focus();
            return false;
        }
    }
    else {
        var ddlStatus = document.getElementById(CtrlIdPrefix + "ddlStatus");

        if (ddlStatus != null) {
            var status = ddlStatus.options[ddlStatus.selectedIndex].value;

            if (status.trim() == "") {
                alert('Please select the Status to Cancel the Purchase Return Entry.');
                ddlStatus.focus();
                return false;
            }
        }
    }

    return true;
}

function PurchaseReturnEntrySubmit(s) {
    if (s != "M") {
        if (PurchaseReturnHeaderValidation()) {
            if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
                alert('No Item details are found.');
                return false;
            }

            if (GridCheck()) {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    else {
        var Status = "Are you Sure to Approve the Purchase Return Entry";

        if (confirm(Status))
            return true;
        else
            return false;
    }
}

function funPurchaseReturnReject() {
    var Remarks = prompt("Are you Sure to Reject the Purchase Return Entry? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
}

function funPurchaseReturnCancel() {
    var Remarks = prompt("Are you Sure to Cancel the Purchase Return Entry? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
}

function GridCheck() {

    var ddlPurchaseReturnNumber = document.getElementById(CtrlIdPrefix + "ddlPurchaseReturnNumber");

    if (ddlPurchaseReturnNumber == null) {

        var ChkStatus = document.getElementById(CtrlIdPrefix + "ChkStatus");

        if (ChkStatus.value == "0") {
            alert('Please Select a Part # and Enter Return Quantity.');
            return false;
        }

        if (GridValidation()) {
            var Status = "Are you Sure to Raise the Purchase Return Entry??";

            if (confirm(Status))
                return true;
            else
                return false;
        }
        else
            return false;
    }
    else {
        return true;
    }
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