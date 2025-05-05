var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvItemDetails_";

function GetRowIdPrefix(rowidprefix) {
    var rowCtlId = rowidprefix.split("_");

    return rowCtlId[3] + "_";

}

function IntegerValueOnly() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function preventEnterKeyOnCombo() {
    var AsciiValue = event.keyCode;

    if (AsciiValue == 13) {
        return false;
    }
}

function CustomerSalesReqValidation(id) {

    var cCustomer = document.getElementById(CtrlIdPrefix + "cmbCustomerName_TextBox");

    if (cCustomer.value.trim() == null || cCustomer.value.trim() == "") {
        alert("Please select Customer");
        cCustomer.focus();
        return false;
    }

    if (id.indexOf("btnAdd") < 0) {
        if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
            alert('No Item details are found. Please Add');
            return false;
        }
    }

    var checkflg = false;

    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grvItemDetails");
    if (gItemsDtl != null) {
        var rowscount = gItemsDtl.rows.length;
        if (rowscount >= 1) {

            //dropdown validation
            var Inputs = gItemsDtl.getElementsByTagName("select");
            var dslno = document.getElementById(CtrlIdPrefix + "hdnRowCnt").value;

            for (i = 0; i < Inputs.length; i++) {

                if (checkflg == false) {
                    CtrlGridRowId = GetRowIdPrefix(Inputs[i].id);
                    checkflg = true;
                }

                if (document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "ddlSupplierName") != null) {
                    if (Inputs[i].id == document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "ddlSupplierName").id) {
                        if (Inputs[i].value == "0" || Inputs[i].value == "0.00" || Inputs[i].value == "") {
                            alert("Please select supplier name in row " + dslno);
                            document.getElementById(Inputs[i].id).focus();
                            return false;
                        }
                    }
                }

                if (document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "ddlItemCode") != null) {
                    if (Inputs[i].id == document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "ddlItemCode").id) {
                        if (Inputs[i].value == "0" || Inputs[i].value == "0.00" || Inputs[i].value == "") {
                            alert("Please select supplier part number in row " + dslno);
                            document.getElementById(Inputs[i].id).focus();
                            return false;
                        }
                    }
                }
            }

            checkflg = false;
            //for textbox validation
            var txtInputs = gItemsDtl.getElementsByTagName("input");
            var tslno = document.getElementById(CtrlIdPrefix + "hdnRowCnt").value;


            if (tslno != 0 && Inputs.length != 0) {
                for (i = 0; i < txtInputs.length; i++) {

                    if (checkflg == false && tslno != 0) {
                        CtrlGridRowId = GetRowIdPrefix(Inputs[i].id);
                        checkflg = true;
                    }

                    if (document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtSupplierPartNo") != null) {
                        if (txtInputs[i].id == document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtSupplierPartNo").id) {
                            if (txtInputs[i].value == "0" || txtInputs[i].value == "0.00" || txtInputs[i].value == "") {
                                alert("Please select supplier part number in row " + tslno);
                                document.getElementById(txtInputs[i].id).focus();
                                return false;
                            }
                        }
                    }

                    if (document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtQuantity") != null) {
                        if (txtInputs[i].id == document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtQuantity").id) {
                            if (txtInputs[i].value == "0" || txtInputs[i].value == "0.00" || txtInputs[i].value == "") {
                                alert("Please enter quantity in row " + tslno);
                                document.getElementById(txtInputs[i].id).focus();
                                return false;
                            }
                        }
                    }

                    if (document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtValidDays") != null) {
                        if (txtInputs[i].id == document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtValidDays").id) {
                            //if (txtInputs[i].value == "0" || txtInputs[i].value == "0.00" || txtInputs[i].value == "") {
                            if (txtInputs[i].value == "" || (txtInputs[i].value != "" && parseInt(txtInputs[i].value) < 0)) {
                                alert("Please enter Valid Days in row " + tslno);
                                document.getElementById(txtInputs[i].id).focus();
                                return false;
                            }
                        }
                    }
                }
            }
        }
    }

    return true;
}