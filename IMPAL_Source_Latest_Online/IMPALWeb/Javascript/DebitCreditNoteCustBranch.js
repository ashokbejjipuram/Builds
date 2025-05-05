var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grdDA_ctl02_";
var selectedItemCode = "";

function ValidateDebitCreditFields() {
    if (validate(document.getElementById(CtrlIdPrefix + "ddlAccountPeriod").value, "Accounting Period ")) {
        document.getElementById(CtrlIdPrefix + "ddlAccountPeriod").focus()
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value, "DebitCreditNote ")) {
        document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").focus()
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "ddlSuppCustBranchInd").value, "Customer/Branch Indicator ")) {
        document.getElementById(CtrlIdPrefix + "ddlSuppCustBranchInd").focus()
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "ddlSuppCustBranch").value, "Customer/Branch ")) {
        document.getElementById(CtrlIdPrefix + "ddlSuppCustBranch").focus()
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "ddlTransactionType").value, "Transaction Type ")) {
        document.getElementById(CtrlIdPrefix + "ddlTransactionType").focus()
        return false;
    }

    if (!((document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value == "CA" && document.getElementById(CtrlIdPrefix + "ddlTransactionType").value == "751")
                    || document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value == "DA")) {
        if (validate(document.getElementById(CtrlIdPrefix + "txtDocumentNumber").value, "Document Number ")) {
            document.getElementById(CtrlIdPrefix + "txtDocumentNumber").focus()
            return false;
        }
        else if (parseInt(document.getElementById(CtrlIdPrefix + "txtDocumentNumber").value.length) != 5) {
            alert("Document Number should be 5 digit")
            document.getElementById(CtrlIdPrefix + "txtDocumentNumber").focus()
            return false;
        }
    }

    if (validate(document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value, "Reference Document Number ")) {
        document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").focus()
        return false;
    }
    else if (validatespl(document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value, "Reference Document Number ")) {
        document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").focus();
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").value, "Reference Document Date ")) {
        document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").focus()
        return false;
    }
    else if (doValidateDate(document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate"))) {
        document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").focus();
        return false;
    }
    else if (CompareDate(document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").value, document.getElementById(CtrlIdPrefix + "txtDocumentDate").value) == 1) {
        alert("Reference Document Date is greater than Document Date");
        document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").focus();
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "txtValue").value, "Value ")) {
        document.getElementById(CtrlIdPrefix + "txtValue").focus();
        return false;
    }
    else if (validate(document.getElementById(CtrlIdPrefix + "txtRemarks").value, "Remarks ")) {
        document.getElementById(CtrlIdPrefix + "txtRemarks").focus();
        return false;
    }
    else if (validatespl(document.getElementById(CtrlIdPrefix + "txtRemarks").value, "Remarks")) {
        document.getElementById(CtrlIdPrefix + "txtRemarks").focus();
        return false;
    }
}

function CalAdjustValue() {
    var txtCollectedAmount = document.getElementById(CtrlIdPrefix + "txtCollectedAmount");
    var hddreturnQntCA = document.getElementById(CtrlIdPrefix + "hddreturnQntCA").value;
    var gvDrv = document.getElementById(CtrlIdPrefix + "grdDA");
    var ddlTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType").value;
    var txtReturnvalue = null;
    var txtSGST = null;
    var txtCGST = null;
    var txtIGST = null;
    var txtUTGST = null;
    var txtValue = null;
    var txtReturnquantity = null;
    var CollectAmount = 0.0;
    var Check = null;
    var txtQuantity = null;

    for (var x = 1; x < gvDrv.rows.length - 1; x++) {

        var row = gvDrv.rows[x]
        Check = row.cells[0].children[0];
        txtQuantity = row.cells[2].children[0];
        txtValue = row.cells[3].children[0];
        txtReturnquantity = row.cells[4].children[0];
        txtReturnvalue = row.cells[5].children[0];
        txtSGST = row.cells[6].children[0];
        txtCGST = row.cells[7].children[0];
        txtIGST = row.cells[8].children[0];
        txtUTGST = row.cells[9].children[0];

        if (Check.checked) {
            if (hddreturnQntCA == "1") {
                txtSGST.value = 0;
                txtCGST.value = 0;
                txtIGST.value = 0;
                txtUTGST.value = 0;
                CollectAmount = CollectAmount + parseFloat(txtReturnvalue.value) + parseFloat(txtSGST.value) + parseFloat(txtCGST.value) + parseFloat(txtIGST.value) + parseFloat(txtUTGST.value);
            }
            else if (txtReturnquantity.value != "0")
                CollectAmount = CollectAmount + parseFloat(txtReturnvalue.value) + parseFloat(txtSGST.value) + parseFloat(txtCGST.value) + parseFloat(txtIGST.value) + parseFloat(txtUTGST.value);

            if (ddlTransactionType == "653" && txtReturnquantity.value == "0") {
                CollectAmount = CollectAmount + parseFloat(txtReturnvalue.value) + parseFloat(txtSGST.value) + parseFloat(txtCGST.value) + parseFloat(txtIGST.value) + parseFloat(txtUTGST.value);
            }
        }
    }

    txtCollectedAmount.value = round(CollectAmount, 4);
}

function CalculateTax(lnk, Flag) {alert(lnk);
    var txtCollectedAmount = document.getElementById(CtrlIdPrefix + "txtCollectedAmount");
    var ddlTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType").value;
    var txtReturnvalue = null;
    var txtSGST = null;
    var txtCGST = null;
    var txtIGST = null;
    var txtUTGST = null;
    var txtValue = null;
    var txtReturnquantity = null;
    var CollectAmount = 0.0;
    var Check = null;
    var txtQuantity = null;

    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;

    Check = row.cells[0].children[0];
    txtQuantity = row.cells[2].children[0];
    txtValue = row.cells[3].children[0];
    txtReturnquantity = row.cells[4].children[0];
    txtReturnvalue = row.cells[5].children[0];
    txtSGST = row.cells[6].children[0];
    txtCGST = row.cells[7].children[0];
    txtIGST = row.cells[8].children[0];
    txtUTGST = row.cells[9].children[0];
    txtActualRetQty = row.cells[9].children[1];
    txtCouponCharges = row.cells[9].children[2];

    if (Math.round(txtReturnquantity.value) > (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value))) {
        alert("Return quantity cannot be greater than " + (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value)));
        //txtReturnquantity.value = (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value));
        return false;
    }

    if (Check.checked) {

        var streets = null;
        return PageMethods.GetCollection(function(Result) {
            if (Flag == 1) {
                txtReturnvalue.value = round((txtValue.value * txtReturnquantity.value), 4);
                txtSGST.value = round(((parseFloat(txtReturnvalue.value) + parseFloat(txtCouponCharges.value)) * Result[rowIndex].SGSTSalesPer) / 100, 4);
                txtCGST.value = round(((parseFloat(txtReturnvalue.value) + parseFloat(txtCouponCharges.value)) * Result[rowIndex].CGSTSalesPer) / 100, 4);
                txtIGST.value = round(((parseFloat(txtReturnvalue.value) + parseFloat(txtCouponCharges.value)) * Result[rowIndex].IGSTSalesPer) / 100, 4);
                txtUTGST.value = round(((parseFloat(txtReturnvalue.value) + parseFloat(txtCouponCharges.value)) * Result[rowIndex].UTGSTSalesPer) / 100, 4);
            }
            else {
                txtSGST.value = round(((parseFloat(txtReturnvalue.value) + parseFloat(txtCouponCharges.value)) * Result[rowIndex].SGSTSalesPer) / 100, 4);
                txtCGST.value = round(((parseFloat(txtReturnvalue.value) + parseFloat(txtCouponCharges.value)) * Result[rowIndex].CGSTSalesPer) / 100, 4);
                txtIGST.value = round(((parseFloat(txtReturnvalue.value) + parseFloat(txtCouponCharges.value)) * Result[rowIndex].IGSTSalesPer) / 100, 4);
                txtUTGST.value = round(((parseFloat(txtReturnvalue.value) + parseFloat(txtCouponCharges.value)) * Result[rowIndex].UTGSTSalesPer) / 100, 4);
            }

            CalAdjustValue();

            if (ddlTransactionType != "656" && ddlTransactionType != "756" && ddlTransactionType != "658" && ddlTransactionType != "758") {
                if (Math.round(txtReturnvalue.value) > 0 && (parseInt(txtReturnvalue.value) < parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value)))) {
                    alert("Return value cannot be less than " + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value)));
                    //txtReturnquantity.value = txtActualRetQty.value;
                    return false;
                }

                if (Math.round(txtReturnvalue.value) > 0 && (parseFloat(txtReturnvalue.value) > parseFloat(1 + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value))))) {
                    alert("Return value cannot be greater than " + parseFloat(1 + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value))));
                    //txtReturnquantity.value = txtActualRetQty.value;
                    return false;
                }
            }
        });
    }
}

function SelectedChange(lnk) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    GetValue(row, rowIndex);
    CalAdjustValue();
}

function GetValue(row, index) {
    var txtReturnvalue = null;
    var txtSGST = null;
    var txtCGST = null;
    var txtIGST = null;
    var txtUTGST = null;
    var txtValue = null;
    var txtReturnquantity = null;
    var CollectAmount = 0.0;
    var Check = null;
    var txtQuantity = null;

    Check = row.cells[0].children[0];
    txtQuantity = row.cells[2].children[0];
    txtValue = row.cells[3].children[0];
    txtReturnquantity = row.cells[4].children[0];
    txtReturnvalue = row.cells[5].children[0];
    txtSGST = row.cells[6].children[0];
    txtCGST = row.cells[7].children[0];
    txtIGST = row.cells[8].children[0];
    txtUTGST = row.cells[9].children[0];

    if (!Check.checked) {
        return PageMethods.GetCollection(function(Result) {
            //txtReturnquantity.value = "0";
            txtReturnvalue.value = "0";
            txtSGST.value = round(Result[index].SGSTSalesPer, 4);
            txtCGST.value = round(Result[index].CGSTSalesPer, 4);
            txtIGST.value = round(Result[index].IGSTSalesPer, 4);
            txtUTGST.value = round(Result[index].UTGSTSalesPer, 4);
            txtReturnquantity.value = round(Result[index].return_quantity, 4);
        });
    }
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

function checkAmount(Ind) {
    var txtAmount = document.getElementById(CtrlIdPrefix + "txtValue");
    var txtRoundoff = document.getElementById(CtrlIdPrefix + "txtRoundoff");
    var ObjGrid = null;
    var inputElements = null;
    var Type = document.getElementById(CtrlIdPrefix + "hdnTransType").value;
    var hdnChart = document.getElementById(CtrlIdPrefix + "hdnChart").value;
    var hddItemCode = document.getElementById(CtrlIdPrefix + "hddItemCode").value;
    var ddlTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType").value;
    var ChartAccount = null;
    var Value = null;
    var Remarks = null;
    var ItemCode = null;
    var Quantity = null;
    var SalesTaxCode = null;
    var SalesTaxPer = null;
    var SalesTaxAmount = null;
    var ScPer = null;
    var ScAmt = null;
    var ASCper = null;
    var ASCAmt = null;
    var TotPer = null;
    var TotAmt = null;
    var SupplierPartNo = null;
    var CollectAmount = "0.00";
    ObjGrid = document.getElementById(CtrlIdPrefix + "grdCA")

    if (Type != null) {
        if (Type == "1") {

            for (var x = 1; x < ObjGrid.rows.length - 1; x++) {

                var row = ObjGrid.rows[x]
                ChartAccount = row.cells[1].children[0];
                Value = row.cells[2].children[0];
                Remarks = row.cells[3].children[0];
                ItemCode = row.cells[4].children[0];
                Quantity = row.cells[5].children[0];
                SalesTaxCode = row.cells[6].children[0];
                SalesTaxPer = row.cells[7].children[0];
                SalesTaxAmount = row.cells[8].children[0];
                ScPer = row.cells[9].children[0];
                ScAmt = row.cells[10].children[0];
                ASCper = row.cells[11].children[0];
                ASCAmt = row.cells[12].children[0];
                TotPer = row.cells[13].children[0];
                TotAmt = row.cells[14].children[0];
                SupplierPartNo = row.cells[15].children[0];
                if (ChartAccount.value == "") {
                    alert("ChartAccount is required");
                    return false;
                }
                if (Value.value == "") {
                    alert("Value is required");
                    return false;
                }

                if (Remarks.value == "") {
                    alert("Remarks is required");
                    return false;
                }
                if (hdnChart == "1") {

                    if (SalesTaxPer.value == "") {
                        alert("SalesPer is required");
                        return false;
                    }

                    if (SalesTaxAmount.value == "") {
                        alert("SalesAmt is required");
                        return false;
                    }

                    if (ScPer.value == "") {
                        alert("SC % is required");
                        return false;
                    }

                    if (ScAmt.value == "") {
                        alert("SCAmount is required");
                        return false;
                    }

                    if (ASCper.value == "") {
                        alert("AscPer is required");
                        return false;
                    }

                    if (ASCAmt.value == "") {
                        alert("AscAmt is required");
                        return false;
                    }

                    if (TotPer.value == "") {
                        alert("TotPer is required");
                        return false;
                    }

                    if (TotAmt.value == "") {
                        alert("TotAmt is required");
                        return false;
                    }
                }

                if (hddItemCode == '1') {
                    if (ItemCode.value == "") {
                        alert("Item code is required");
                        return false;
                    }
                }

                CollectAmount = Math.round((parseFloat(CollectAmount) + parseFloat(Value.value) + parseFloat(SalesTaxAmount.value) + parseFloat(ScAmt.value) + parseFloat(ASCAmt.value) + parseFloat(TotAmt.value)) * 100) / 100;
            }

            if (Ind == 1) {
                if ((parseFloat(CollectAmount) > parseFloat(txtAmount.value))) {
                    alert("Amount Differ Check the Amount");
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                if ((parseFloat(CollectAmount) > parseFloat(txtAmount.value)) || (parseFloat(CollectAmount) < parseFloat(txtAmount.value))) {
                    alert("Amount Differ Check the Amount");
                    return false;
                }
            }

        }
        else {
            ObjGrid = document.getElementById(CtrlIdPrefix + "grdDA")
            for (var x = 1; x < ObjGrid.rows.length - 1; x++) {

                var row = ObjGrid.rows[x]
                Check = row.cells[0].children[0];
                if (Check.checked) {
                    txtQuantity = row.cells[2].children[0];
                    txtValue = row.cells[3].children[0];
                    txtReturnquantity = row.cells[4].children[0];
                    txtReturnvalue = row.cells[5].children[0];
                    txtActualRetQty = row.cells[9].children[1];

                    if (Math.round(txtReturnquantity.value) > (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value))) {
                        alert("Return quantity cannot be greater than " + (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value)));
                        //txtReturnquantity.value = (Math.round(txtQuantity.value) - Math.round(txtActualRetQty.value));
                        return false;
                    }
                    if (ddlTransactionType != "656" && ddlTransactionType != "756" && ddlTransactionType != "658" && ddlTransactionType != "758") {
                        if (Math.round(txtReturnvalue.value) > 0 && (parseInt(txtReturnvalue.value) < parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value)))) {
                            alert("Return value cannot be less than " + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value)));
                            //txtReturnquantity.value = txtActualRetQty.value;
                            return false;
                        }

                        if (Math.round(txtReturnvalue.value) > 0 && (parseFloat(txtReturnvalue.value) > parseFloat(1 + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value))))) {
                            alert("Return value cannot be greater than " + parseFloat(1 + parseInt(Math.round(txtReturnquantity.value) * parseFloat(txtValue.value))));
                            //txtReturnquantity.value = txtActualRetQty.value;
                            return false;
                        }
                    }
                }
            }

            //var TotalAmount = parseFloat(document.getElementById(CtrlIdPrefix + "txtTotalAmount").value);
            //var CollectedAmount = parseFloat(document.getElementById(CtrlIdPrefix + "txtCollectedAmount").value);
            //txtRoundoff.value = parseFloat(document.getElementById(CtrlIdPrefix + "txtTotalAmount").value) - parseFloat(document.getElementById(CtrlIdPrefix + "txtCollectedAmount").value);
            alert(parseFloat(document.getElementById(CtrlIdPrefix + "txtTotalAmount").value) - parseFloat(document.getElementById(CtrlIdPrefix + "txtCollectedAmount").value));
            //alert(parseFloat(document.getElementById(CtrlIdPrefix + "txtTotalAmount").value));
            alert(txtRoundoff.id);
            var TotalAmount = parseInt(document.getElementById(CtrlIdPrefix + "txtTotalAmount").value);
            var CollectedAmount = parseInt(document.getElementById(CtrlIdPrefix + "txtCollectedAmount").value);

            if ((TotalAmount > CollectedAmount) || (TotalAmount < CollectedAmount)) {
                alert("Amount Differ Check the Amount");
                return false;
            }
        }
    }
}

function GetDocumentDate() {
    var DocumentNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value;
    var Code = document.getElementById(CtrlIdPrefix + "ddlSuppCustBranch").value;
    var ddlSuppCustBranchInd = document.getElementById(CtrlIdPrefix + "ddlSuppCustBranchInd").value;
    var filePath = document.getElementById(CtrlIdPrefix + "hdnpath").value + "/LoadDocumentDate.ashx";    
    var ddlDebitCreditNote = document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value;
    var ddlTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType").value;

    if (ddlDebitCreditNote == "CA" && ddlTransactionType != "651" && ddlTransactionType != "751") {
        var CustBranchInd;
        if (ddlSuppCustBranchInd == "Customer") {
            CustBranchInd = "1";
        }
        else {
            CustBranchInd = "2";
        }
        $.post(filePath + "?DocumentNumber=" + DocumentNumber + "&Code=" + Code + "&CustBranchInd=" + CustBranchInd, function(result) {
            var streets = eval(result);
            ValidateRefDocDate(streets);
        });
    }

}

function ValidateRefDocDate(streets) {
    var txtRefDocumnetDate = document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate");
    var DocumentNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber");
    var hddrefdate = document.getElementById(CtrlIdPrefix + "hddrefdate");
    if (streets.RefCode == 0) {
        txtRefDocumnetDate.value = streets.RefDate;
        hddrefdate.value = "1";
        txtRefDocumnetDate.disabled = true;
    }
    else if (streets.RefCode == 1) {
        if (DocumentNumber.value != "") {
            alert("Reference document number not available for this customer");
            txtRefDocumnetDate.value = "";
            DocumentNumber.value = "";
            DocumentNumber.focus();
            hddrefdate.value = "0";
            txtRefDocumnetDate.disabled = false;
        }
    }
    else if (streets.RefCode == 2) {
        if (DocumentNumber.value != "") {
            alert("Reference document number not available for this  branch");
            txtRefDocumnetDate.value = "";
            DocumentNumber.value = "";
            DocumentNumber.focus();
            hddrefdate.value = "0";
            txtRefDocumnetDate.disabled = false;
        }
    }
    else if (streets.RefCode == 3) {
        if (DocumentNumber.value != "") {
            alert("Already credit note passed for this reference document number");
            txtRefDocumnetDate.value = "";
            DocumentNumber.value = "";
            DocumentNumber.focus();
            hddrefdate.value = "0";
            txtRefDocumnetDate.disabled = false;
        }
    }

}