var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grdItemDetails_";

function GetRowIdPrefix(rowidprefix) {
    var rowCtlId = rowidprefix.split("_");

    return rowCtlId[3] + "_";

}

function fnValidate() {
    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grdItemDetails");
    if (gItemsDtl != null) {
        var rowscount = gItemsDtl.rows.length;
        if (rowscount >= 3) {
            for (k = 1; k < gItemsDtl.rows.length; k++) {
                var row = gItemsDtl.rows[k];
                txtSupp = row.cells[1].children[0];
                CtrlGridRowId = GetRowIdPrefix(txtSupp.id);

                var SupplierName = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "ddlSupplierName");
                var SupplierPartNo = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "ddlSupplierPartNo");

                alert(SupplierName.value);
                alert(SupplierPartNo.value);

                if (SupplierName.value == "0") {
                    alert("Please Select the Supplier");
                    ddlSupplier.focus();
                    return false;
                }

                if (SupplierPartNo.value == "0") {
                    alert("Please Select the Supplier Part #");
                    SupplierPartNo.focus();
                    return false;
                }
            }
        }
    }
}

function fnValidateSearch() {

    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grdItemDetails");
    if (gItemsDtl != null) {
        var rowscount = gItemsDtl.rows.length;
        if (rowscount >= 3) {
            for (k = 1; k < gItemsDtl.rows.length; k++) {
                var row = gItemsDtl.rows[k];
                txtSupp = row.cells[1].children[0];
                CtrlGridRowId = GetRowIdPrefix(txtSupp.id);

                var SupplierName = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "ddlSupplierName");
                var SupplierPartNo = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtSupplierPartNo");

                if (SupplierName.value == "0") {
                    alert("Please Select the Supplier");
                    SupplierName.focus();
                    return false;
                }

                if (SupplierPartNo.value.trim() == "") {
                    alert("Please Enter the Supplier Part #");
                    SupplierPartNo.focus();
                    return false;
                }
            }
        }
    }
}

function fnValidateSubmit() {

    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grdItemDetails");
    if (gItemsDtl != null) {
        var rowscount = gItemsDtl.rows.length;
        if (rowscount >= 3) {
            for (k = 1; k < gItemsDtl.rows.length; k++) {
                var row = gItemsDtl.rows[k];
                txtSupp = row.cells[1].children[0];
                CtrlGridRowId = GetRowIdPrefix(txtSupp.id);

                var SupplierName = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "ddlSupplierName");
                var SupplierPartNo = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtSupplierPartNo");
                var ddlSupplierPartNo = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "ddlSupplierPartNo");
                var txtListPrice = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtListPrice");
                var txtPurchaseDiscount = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtPurchaseDiscount");
                var txtListLessDiscount = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtListLessDiscount");
                var txtCoupon = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtCoupon");
                var txtAfterCoupon = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtAfterCoupon");
                var txtCDPercentage = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtCDPercentage");
                var txtCDAmount = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtCDAmount");
                var txtAfterCD = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtAfterCD");
                var txtWCPercentage = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtWCPercentage");
                var txtWCAmount = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtWCAmount");
                var txtCostPrice = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtCostPrice");
                var txtMRP = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtMRP");
                
                if (SupplierName.value == "0") {
                    alert("Please Select the Supplier");
                    SupplierName.focus();
                    return false;
                }

                if (SupplierPartNo != null) {
                    if (SupplierPartNo.value.trim() == "") {
                        alert("Please Enter the Supplier Part #");
                        SupplierPartNo.focus();
                        return false;
                    }
                }
                
                if (ddlSupplierPartNo != null) {
                    if (ddlSupplierPartNo.value == "0") {
                        alert("Please Select the Supplier Part #");
                        ddlSupplierPartNo.focus();
                        return false;
                    }
                }
                
                if (txtListPrice.value.trim() == "") {
                    alert("List Price Shoud not be Null");
                    txtListPrice.focus();
                    return false;
                }

                if (parseFloat(txtListPrice.value.trim()) <= 0) {
                    alert("List Price Shoud not be Zero");
                    txtListPrice.focus();
                    return false;
                }

                if (txtPurchaseDiscount.value.trim() == "") {
                    alert("Purchase Discount Shoud not be Null");
                    txtPurchaseDiscount.focus();
                    return false;
                }

                if (txtListLessDiscount.value.trim() == "") {
                    alert("ListLess Discount Shoud not be Null");
                    txtListLessDiscount.focus();
                    return false;
                }

                if (txtCoupon.value.trim() == "") {
                    alert("Coupon Shoud not be Null");
                    txtCoupon.focus();
                    return false;
                }

                if (txtAfterCoupon.value.trim() == "") {
                    alert("After Coupon Shoud not be Null");
                    txtAfterCoupon.focus();
                    return false;
                }

                if (txtCDPercentage.value.trim() == "") {
                    alert("CD Percentage Shoud not be Null");
                    txtCDPercentage.focus();
                    return false;
                }

                if (txtCDAmount.value.trim() == "") {
                    alert("CD Amount Shoud not be Null");
                    txtCDAmount.focus();
                    return false;
                }

                if (txtAfterCD.value.trim() == "") {
                    alert("After CD Shoud not be Null");
                    txtAfterCD.focus();
                    return false;
                }

                if (txtWCPercentage.value.trim() == "") {
                    alert("WC Percentage Shoud not be Null");
                    txtWCPercentage.focus();
                    return false;
                }

                if (txtWCAmount.value.trim() == "") {
                    alert("WC Amount Shoud not be Null");
                    txtWCAmount.focus();
                    return false;
                }

                if (txtCostPrice.value.trim() == "") {
                    alert("Cost Price Shoud not be Null");
                    txtCostPrice.focus();
                    return false;
                }

                if (parseFloat(txtCostPrice.value.trim()) <= 0) {
                    alert("Cost Price Shoud not be Zero");
                    txtCostPrice.focus();
                    return false;
                }

                if (txtMRP.value.trim() == "") {
                    alert("MRP Shoud not be Null");
                    txtMRP.focus();
                    return false;
                }

                if (parseFloat(txtMRP.value.trim()) <= 0) {
                    alert("MRP Shoud not be Zero");
                    txtMRP.focus();
                    return false;
                }
            }
        }
    }
}

function fnCalculate(id) {
    CtrlGridRowId = GetRowIdPrefix(id);

    var ListPrice = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtListPrice");
    var PurDisc = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtPurchaseDiscount");
    var ListLessDisc = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtListLessDiscount");
    var Coupon = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtCoupon");
    var AfterCoupon = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtAfterCoupon");
    var CDPer = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtCDPercentage");
    var CDAmt = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtCDAmount");
    var AfterCD = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtAfterCD");
    var WCPer = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtWCPercentage");
    var WCAmt = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtWCAmount");
    var CostPrice = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtCostPrice");

    if (ListPrice.value.trim() == "" || isNaN(ListPrice.value)) { ListPrice.value = 0.00 }
    if (PurDisc.value.trim() == "" || isNaN(PurDisc.value)) { PurDisc.value = 0.00 }
    if (Coupon.value.trim() == "" || isNaN(Coupon.value)) { Coupon.value = 0.00 }
    if (CDPer.value.trim() == "" || isNaN(CDPer.value)) { CDPer.value = 0.00 }
    if (WCPer.value.trim() == "" || isNaN(WCPer.value)) { WCPer.value = 0.00 }

    ListLessDisc.value = (parseFloat(ListPrice.value) - ((parseFloat(ListPrice.value) * parseFloat(PurDisc.value)) / 100)).toFixed(4);

    AfterCoupon.value = parseFloat(ListLessDisc.value) - parseFloat(Coupon.value);

    CDAmt.value = (parseFloat(AfterCoupon.value) * (parseFloat(CDPer.value) / 100)).toFixed(4);
    AfterCD.value = (parseFloat(AfterCoupon.value) - parseFloat(CDAmt.value)).toFixed(4);

    WCAmt.value = (parseFloat(AfterCD.value) * (parseFloat(WCPer.value) / 100)).toFixed(4);
    CostPrice.value = (parseFloat(AfterCD.value) + parseFloat(WCAmt.value)).toFixed(4);
    
    document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "hdnCostPrice").value = parseFloat(CostPrice.value);
}

function UnCheckBox(id) {
    document.getElementById(CtrlIdPrefix + 'chkZone').checked = false;
    document.getElementById(CtrlIdPrefix + 'chkState').checked = false;
    document.getElementById(CtrlIdPrefix + 'chkBranch').checked = false;

    if (id == CtrlIdPrefix + "ddlZone" || id == CtrlIdPrefix + "ddlState") {
        __doPostBack(id, 0);
    }

    return true;
}

function CurrencyNumberOnly(id) {
    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;

    //if (document.getElementById(id).value.split('.').length > 1) {
    //    event.returnValue = false;
    //}
}