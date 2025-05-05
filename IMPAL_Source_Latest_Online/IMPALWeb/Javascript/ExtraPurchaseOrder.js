var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_GridView1_ctl02_";

function CurrencyNumberOnly() {
    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function IntegerValueOnlyWithoutZero() {
    var AsciiValue = event.keyCode
    if ((AsciiValue > 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
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

function IntegerValueWithDot() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function IntegerValueOnlyWithSearch() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue == 37))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function CurrencyChkForMoreThanOneDot(strValue) {
    if (strValue.split('.').length > 2)
        return false;
    else
        return true;
}

//function CheckQty(id) {
//    if (document.getElementById(id).value.trim() != "") {
//        ddlid = id.replace("txtAccQty", "ddlEPOtype");

//        if (document.getElementById(id).value != "0")
//            document.getElementById(ddlid).disabled = false;
//        else
//            document.getElementById(ddlid).disabled = true;
//    }
//    else
//        document.getElementById(ddlid).disabled = true;
//}

//function fnChangeSubTypes() {
//    ddlEPOtype = document.getElementById(CtrlIdPrefix + "ddlEPOtype");
//    Label2 = document.getElementById(CtrlIdPrefix + "Label2");
//    Label3 = document.getElementById(CtrlIdPrefix + "Label3");
//    ddlEPOsubType = document.getElementById(CtrlIdPrefix + "ddlEPOsubType");
//    ddlCustomer = document.getElementById(CtrlIdPrefix + "ddlCustomer");

//    if (ddlEPOtype.value.trim() == "Dealer-Back to Back") {
//        Label2.style.display = "none";
//        ddlEPOsubType.style.display = "none";
//        Label3.style.display = "inline";
//        ddlCustomer.style.display = "inline";
//    }
//    else {
//        Label2.style.display = "inline";
//        ddlEPOsubType.style.display = "inline";
//        Label3.style.display = "none";
//        ddlCustomer.style.display = "none";
//    }
//}

function ValidateAdd() {
    ddlIndentNumber = document.getElementById(CtrlIdPrefix + "ddlIndentNumber");
    ddlEPOtype = document.getElementById(CtrlIdPrefix + "ddlEPOtype");
    ddlEPOsubType = document.getElementById(CtrlIdPrefix + "ddlEPOsubType");
    ddlCustomer = document.getElementById(CtrlIdPrefix + "ddlCustomer");
    ddlItemCode = document.getElementById(CtrlIdPrefix + "ddlItemCode");

    if (ddlIndentNumber.value.trim() == "") {
        alert("Please Select a PO Number");
        ddlIndentNumber.focus();
        return false;
    }

    if (ddlEPOtype.value.trim() == "") {
        alert("Please Select an EPO Type For Extra Purchase Order");
        ddlEPOtype.focus();
        return false;
    }

    if (ddlEPOtype.value.trim() == "Dealer-Back to Back") {
        if (ddlCustomer.value.trim() == "") {
            alert("Please Select a Customer");
            ddlCustomer.focus();
            return false;
        }
    }
    else if (ddlEPOtype.value.trim() != "MOQ/Shipper") {
        if (ddlEPOsubType.value.trim() == "") {
            alert("Please Select a EPO Sub Type");
            ddlEPOsubType.focus();
            return false;
        }
    }

    if (ddlItemCode.value.trim() == "") {
        alert("Please Select a PO Part #");
        ddlItemCode.focus();
        return false;
    }

    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grdPOIndent");
    var rowscount = gItemsDtl.rows.length;

    if (rowscount >= 3) {
        for (k = 1; k < gItemsDtl.rows.length - 1; k++) {
            var row = gItemsDtl.rows[k];
            PartNo = row.cells[0].children[0];
            txtQty = row.cells[10].children[0];

            if (PartNo.value.trim() == "" || PartNo.value.trim() == "0") {
                alert("Please Select Part #");
                PartNo.focus();
                return false;
            }

            if (txtQty.value.trim() == "" || txtQty.value.trim() == "0") {
                alert("Please Enter Extra Quantity");
                txtQty.focus();
                return false;
            }
        }
    }

    return true;
}

function ValidateSubmit(id) {
    ddlIndentNumber = document.getElementById(CtrlIdPrefix + "ddlIndentNumber");
    ddlEPOtype = document.getElementById(CtrlIdPrefix + "ddlEPOtype");
    ddlEPOsubType = document.getElementById(CtrlIdPrefix + "ddlEPOsubType");
    ddlCustomer = document.getElementById(CtrlIdPrefix + "ddlCustomer");

    if (ddlIndentNumber.value.trim() == "") {
        alert("Please Select a PO Number");
        ddlIndentNumber.focus();
        return false;
    }

    if (ddlEPOtype.value.trim() == "") {
        alert("Please Select an EPO Type For Extra Purchase Order");
        ddlEPOtype.focus();
        return false;
    }

    if (ddlEPOtype.value.trim() == "Dealer-Back to Back") {
        if (ddlCustomer.value.trim() == "") {
            alert("Please Select a Customer");
            ddlCustomer.focus();
            return false;
        }
    }
    else if (ddlEPOtype.value.trim() != "MOQ/Shipper") {
        if (ddlEPOsubType.value.trim() == "") {
            alert("Please Select a EPO Sub Type");
            ddlEPOsubType.focus();
            return false;
        }
    }

    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grdPOIndent");
    var rowscount = gItemsDtl.rows.length;

    ind = 0;

    if (rowscount >= 3) {
        for (k = 1; k < gItemsDtl.rows.length - 1; k++) {
            var row = gItemsDtl.rows[k];            
            txtPartNo = row.cells[0].children[0];
            //ddlPartNo = row.cells[0].children[0];            

            if (typeof txtPartNo === 'undefined') {
                ind = 1;
                break;
            }

            if (txtPartNo != null) {
                if (txtPartNo.value.trim() == "") {
                    alert("Please Enter a Part #");
                    txtPartNo.focus();
                    return false;
                }
            }

            btnSearch = row.cells[0].children[1];

            if (typeof btnSearch !== 'undefined') {
                if (btnSearch.value == "Search") {
                    alert("Please Search a Part # and Select the same");
                    txtPartNo.focus();
                    return false;
                }
            }

            //if (ddlPartNo != null) {
            //    if (ddlPartNo.value.trim() == "" || ddlPartNo.value.trim() == "0") {
            //        alert("Please Select a Part #");
            //        ddlPartNo.focus();
            //        return false;
            //    }
            //}
            
            txtQty = row.cells[10].children[0];

            if (txtQty.value.trim() == "" || txtQty.value.trim() == "0") {
                alert("Please Enter Extra Quantity");
                txtQty.focus();
                return false;
            }
        }
    }

    if (id == 2 && ind == 1) {
        if (rowscount <= 3) {
            alert("No Item Details available. Please Add.");
            return false;
        }
    }

    return true;
}


function fnShowHideBtns() {
    document.getElementById(CtrlIdPrefix + "btnReportPDF").style.display = "none";
    document.getElementById(CtrlIdPrefix + "btnReportExcel").style.display = "none";
    document.getElementById(CtrlIdPrefix + "btnReportRTF").style.display = "none";
    document.getElementById(CtrlIdPrefix + "PanelHeaderDtls").disabled = true;

    window.document.forms[0].target = '_blank';
}