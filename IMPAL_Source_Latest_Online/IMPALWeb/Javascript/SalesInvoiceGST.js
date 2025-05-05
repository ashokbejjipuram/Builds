var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvItemDetails_";

function GetRowIdPrefix(rowidprefix) {
    var rowCtlId = rowidprefix.split("_");

    return rowCtlId[3] + "_";

}

function ChangeCashDiscount() {
    var ddlCashDiscount = document.getElementById(CtrlIdPrefix + "ddlCashDiscount");
    var hdnCDstatus = document.getElementById(CtrlIdPrefix + "hdnCDstatus");

    var val = ddlCashDiscount.value;
    if (val == "0") {
        ddlCashDiscount.disabled = false;
        hdnCDstatus.value = "1";
    }
    else {
        ddlCashDiscount.disabled = true;
        hdnCDstatus.value = "0";
    }
}

function ChangeModeOfREceipt() {
    var ModeOfREceipt = document.getElementById(CtrlIdPrefix + "ddlModeOfREceipt");
    var chequeDraftNo = document.getElementById(CtrlIdPrefix + "txtChequeDraftNo");
    var chequeDraftDt = document.getElementById(CtrlIdPrefix + "txtChequeDraftDt");
    var LocalOutstation = document.getElementById(CtrlIdPrefix + "ddlLocalOutstation");
    var bank = document.getElementById(CtrlIdPrefix + "txtBank");
    var bankBranch = document.getElementById(CtrlIdPrefix + "txtBankBranch");

    if (ModeOfREceipt.value == "M" || ModeOfREceipt.value == "0") {
        chequeDraftNo.disabled = true;
        chequeDraftDt.disabled = true;
        bank.disabled = true;
        bankBranch.disabled = true;
        LocalOutstation.disabled = true;
        chequeDraftNo.value = "";
        chequeDraftDt.value = "";
        LocalOutstation.value = "L";
        bank.value = "";
        bankBranch.value = "";
    }
    else {
        chequeDraftNo.disabled = false;
        chequeDraftDt.disabled = false;
        LocalOutstation.disabled = false;
        bank.disabled = false;
        bankBranch.disabled = false;
    }
}

function BtnOutstandingReport() {
    var lblTextMsg = document.getElementById(CtrlIdPrefix + "lblHeaderMessage");
    var BtnOsreport = document.getElementById(CtrlIdPrefix + "BtnReportOs")
    var val = lblTextMsg.innerHTML;
    if (val == "" || val == null) {
        BtnOsreport.disabled = true;
    }
    else {
        BtnOsreport.disabled = false;
    }
}

function CheckQtyNotEmpty(QuantityID) {

    var Quantity = document.getElementById(QuantityID);
    if ((Quantity.value == 0 || Quantity.value == '')) {
        alert('Order quantity should not be Zero or empty.');
        Quantity.focus();
        return false;
    }
}


function SalesInvoiceQtyChange(RowNum, CanOrderQtyID, HdnReqOrderQtyID, QuantityID, ItemSaleValueID, DiscountID) {

    var cTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");

    var TotalValue = document.getElementById(CtrlIdPrefix + "txtTotalValue");
    var CustomerCreditLimit = document.getElementById(CtrlIdPrefix + "txtCustomerCreditLimit");
    var CustomerOutStanding = document.getElementById(CtrlIdPrefix + "txtCustomerOutStanding");
    var InsuranceCharges = document.getElementById(CtrlIdPrefix + "txtInsuranceCharges");
    var CourierCharges = document.getElementById(CtrlIdPrefix + "txtCourierCharges");
    var FreightCharges = document.getElementById(CtrlIdPrefix + "txtFrightAmount");
    var CanBillUpTo = document.getElementById(CtrlIdPrefix + "txtCanBillUpTo");
    var CustomerCreditLimit = document.getElementById(CtrlIdPrefix + "txtCustomerCreditLimit");
    var span = document.getElementById(CtrlIdPrefix + "lblHeaderMessage");

    if (InsuranceCharges.value == '')
        InsuranceCharges = 0;
    if (CourierCharges.value == '')
        CourierCharges = 0;
    if (FreightCharges.value == '')
        FreightCharges.value = 0;

    if (CanBillUpTo == '')
        CanBillUpTo = 0;

    var rowItemQtySaleValue, AllItemsQtySaleValue, courierCharge, insuranceCharge, freightCharge;

    AllItemsQtySaleValue = 0;

    var CanOrderQty = document.getElementById(CanOrderQtyID);
    var ReqOrderQty = document.getElementById(HdnReqOrderQtyID);
    var Quantity = document.getElementById(QuantityID);
    var ItemSaleValue = document.getElementById(ItemSaleValueID);
    var Discount = document.getElementById(DiscountID);

    if (Quantity != null && Quantity != "") {
        if (Quantity.value == "0" || Quantity.value == "") {
            alert("Please enter quantity in row " + RowNum);
            Quantity.focus();
            return false;
        }
        else {
            if (parseInt(Quantity.value.trim()) > 0 && (cTransactionType.value != "461")) {
                if (parseInt(Quantity.value.trim()) > parseInt(CanOrderQty.value.trim())) {
                    alert('Order quantity should not be greater than the Stock quantity.');
                    Quantity.value = 0;
                    Quantity.focus();
                    return false;
                }
                if (!(cTransactionType.value == "041" || cTransactionType.value == "141" || cTransactionType.value == "071" || cTransactionType.value == "171" || cTransactionType.value == "421" || cTransactionType.value == "431" || cTransactionType.value == "441" || cTransactionType.value == "461" || cTransactionType.value == "001" || cTransactionType.value == "101" || cTransactionType.value == "361")) {
                    if (parseInt(Quantity.value.trim()) > parseInt(ReqOrderQty.value.trim())) {
                        alert('Order quantity should not exceed the Requested quantity ' + ReqOrderQty.value);
                        Quantity.value = parseInt(ReqOrderQty.value.trim());
                        Quantity.focus();
                        return false;
                    }
                }
            }
        }
    }
}

function SalesInvoiceQtyChangeGST(RowNum, CanOrderQtyID, HdnReqOrderQtyID, QuantityID, SLBID, ItemSaleValueID, DiscountID) {

    var cTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");

    var TotalValue = document.getElementById(CtrlIdPrefix + "txtTotalValue");
    var CustomerCreditLimit = document.getElementById(CtrlIdPrefix + "txtCustomerCreditLimit");
    var CustomerOutStanding = document.getElementById(CtrlIdPrefix + "txtCustomerOutStanding");
    var InsuranceCharges = document.getElementById(CtrlIdPrefix + "txtInsuranceCharges");
    var CourierCharges = document.getElementById(CtrlIdPrefix + "txtCourierCharges");
    var FreightCharges = document.getElementById(CtrlIdPrefix + "txtFrightAmount");
    var CanBillUpTo = document.getElementById(CtrlIdPrefix + "txtCanBillUpTo");
    var CustomerCreditLimit = document.getElementById(CtrlIdPrefix + "txtCustomerCreditLimit");
    var span = document.getElementById(CtrlIdPrefix + "lblHeaderMessage");

    if (InsuranceCharges.value == '')
        InsuranceCharges = 0;
    if (CourierCharges.value == '')
        CourierCharges = 0;
    if (FreightCharges.value == '')
        FreightCharges.value = 0;

    if (CanBillUpTo == '')
        CanBillUpTo = 0;

    var rowItemQtySaleValue, AllItemsQtySaleValue, courierCharge, insuranceCharge, freightCharge;

    AllItemsQtySaleValue = 0;

    var CanOrderQty = document.getElementById(CanOrderQtyID);
    var ReqOrderQty = document.getElementById(HdnReqOrderQtyID);
    var Quantity = document.getElementById(QuantityID);
    var ItemSaleValue = document.getElementById(ItemSaleValueID);
    var Discount = document.getElementById(DiscountID);
    var SLB = document.getElementById(SLBID);

    if (Quantity != null && Quantity != "") {
        if (Quantity.value == "0" || Quantity.value == "") {
            alert("Please enter quantity in row " + RowNum);
            SLB.disabled = true;
            Quantity.focus();
            return false;
        }
        else {
            if (parseInt(Quantity.value.trim()) > 0 && (cTransactionType.value != "461")) {
                if (parseInt(Quantity.value.trim()) > parseInt(CanOrderQty.value.trim())) {
                    alert('Order quantity should not be greater than the Stock quantity.');
                    Quantity.value = 0;
                    SLB.disabled = true;
                    Quantity.focus();
                    return false;
                }
                if (!(cTransactionType.value == "041" || cTransactionType.value == "141" || cTransactionType.value == "071" || cTransactionType.value == "171" || cTransactionType.value == "421" || cTransactionType.value == "431" || cTransactionType.value == "441" || cTransactionType.value == "461" || cTransactionType.value == "001" || cTransactionType.value == "101" || cTransactionType.value == "361")) {
                    if (parseInt(Quantity.value.trim()) > parseInt(ReqOrderQty.value.trim())) {
                        alert('Order quantity should not exceed the Requested quantity ' + ReqOrderQty.value);
                        Quantity.value = 0;
                        SLB.disabled = true;
                        Quantity.focus();
                        return false;
                    }
                }
            }
        }
    }

    SLB.disabled = false;
    if (SLB.value.trim() != "0") {
        __doPostBack(QuantityID, 0);
        return true;
    }
    else
        return false;
}

function SalesInvoiceDiscountChange(CanOrderQtyID, QuantityID, ItemSaleValueID) {

    var cTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");

    var TotalValue = document.getElementById(CtrlIdPrefix + "txtTotalValue");
    var CustomerCreditLimit = document.getElementById(CtrlIdPrefix + "txtCustomerCreditLimit");
    var CustomerOutStanding = document.getElementById(CtrlIdPrefix + "txtCustomerOutStanding");
    var InsuranceCharges = document.getElementById(CtrlIdPrefix + "txtInsuranceCharges");
    var CourierCharges = document.getElementById(CtrlIdPrefix + "txtCourierCharges");
    var CanBillUpTo = document.getElementById(CtrlIdPrefix + "txtCanBillUpTo");
    var span = document.getElementById(CtrlIdPrefix + "lblHeaderMessage");


    if (InsuranceCharges == '')
        InsuranceCharges = 0;
    if (CourierCharges == '')
        CourierCharges = 0;

    if (CanBillUpTo == '')
        CanBillUpTo = 0;

    var rowItemQtySaleValue, AllItemsQtySaleValue;

    AllItemsQtySaleValue = 0;
    var checkflg = false;
    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grvItemDetails");

    if (gItemsDtl != null) {
        var rowscount = gItemsDtl.rows.length;

        if (rowscount >= 1) {
            //Text Qty validation
            var txtInputs = gItemsDtl.getElementsByTagName("input");
            var tslno = document.getElementById(CtrlIdPrefix + "hdnRowCnt").value;
            for (i = 0; i < txtInputs.length; i++) {

                CtrlGridRowId = GetRowIdPrefix(txtInputs[i].id);
                rowItemQtySaleValue = 0;
                var CanOrderQty = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtCanOrderQty");
                var ReqOrderQty = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtHdnReqOrderQty");
                var Quantity = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtQuantity");
                var ItemSaleValue = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtItemSaleValue");
                var DisDiscount = document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtDiscount");

                if (document.getElementById(CtrlGridRowIdPrefix + CtrlGridRowId + "txtQuantity") != null) {
                    if (txtInputs[i].id == Quantity.id) {
                        if (txtInputs[i].value == "0" || txtInputs[i].value == "") {
                            alert("Please enter quantity in row " + tslno);
                            document.getElementById(txtInputs[i].id).focus();
                            return false;
                        }
                        else {

                            if (parseInt(Quantity.value.trim()) > 0 && (cTransactionType.value != "461")) {
                                if (parseInt(Quantity.value.trim()) > parseInt(CanOrderQty.value.trim())) {
                                    alert('Order quantity should not be greater than the Stock qunatity.');
                                    Quantity.focus();
                                    return false;
                                }
                                else if (parseInt(Quantity.value.trim()) > parseInt(ReqOrderQty.value.trim())) {
                                    alert('Order quantity should not exceed the Requested quantity ' + ReqOrderQty.value);
                                    Quantity.value = parseInt(ReqOrderQty.value.trim());
                                    Quantity.focus();
                                    return false;
                                }
                                else if ((ItemSaleValue != null) && (ItemSaleValue != undefined) && (ItemSaleValue.value != 0)) {

                                if (cTransactionType.value == "141" || cTransactionType.value == "041" || cTransactionType.value == "481") {
                                        rowItemQtySaleValue = (parseFloat(ItemSaleValue.value.trim()) + (parseFloat(ItemSaleValue.value.trim()) * parseFloat(DisDiscount.value.trim() / 100))) * parseInt(Quantity.value.trim());
                                    }
                                    else {
                                        rowItemQtySaleValue = parseFloat(ItemSaleValue.value.trim()) * parseInt(Quantity.value.trim());
                                    }

                                    AllItemsQtySaleValue = parseFloat(AllItemsQtySaleValue) + parseFloat(rowItemQtySaleValue);
                                }
                            }
                        }
                    }   //If the control is Quantity textbox
                }
            } //for

            AllItemsQtySaleValue = parseFloat(AllItemsQtySaleValue);


            if (((CustomerCreditLimit != null) && (CustomerCreditLimit != undefined) && (CustomerCreditLimit.value != 0)) && ((CustomerOutStanding != null) && (CustomerOutStanding != undefined))) {

                if (((parseFloat(AllItemsQtySaleValue) + parseFloat(CustomerOutStanding.value.trim())) > parseFloat(CustomerCreditLimit.value.trim())) && (cTransactionType.value == "111")) {
                    alert("Credit Limit exceeds for this customer");
                    return false;
                }
                else {
                        return true;
                }
            }
        }
    }
}

function SalesInvoiceValidation(pid) {

    var cTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    if (cTransactionType.value == "0") {
        alert("Please select Transaction type");
        cTransactionType.focus();
        return false;
    }

    var cCustomer = document.getElementById(CtrlIdPrefix + "ddlCustomerName");
    if (cCustomer.value == "0") {
        alert("Please select Customer");
        cCustomer.focus();
        return false;
    }

    var cSalesMan = document.getElementById(CtrlIdPrefix + "ddlSalesMan");
    if (cSalesMan.value == "0") {
        alert("Please select Salesman");
        return false;
    }    

    var cCanBullupto = document.getElementById(CtrlIdPrefix + "txtCanBillUpTo");
    var lblTextMsg = document.getElementById(CtrlIdPrefix + "lblHeaderMessage");
    var val = lblTextMsg.innerHTML;
    
    if (!(cTransactionType.value == "001" || cTransactionType.value == "101" || cTransactionType.value == "071" || cTransactionType.value == "171")) {
        if (val != "" || parseFloat(cCanBullupto.value) < 0) {
            alert('Credit Limit exceeds for this customer');
            return false;
        }
    }

    var rdLRTran = document.getElementById(CtrlIdPrefix + "rdLRTransfer");
    var inputs = rdLRTran.getElementsByTagName("input");

    var notSelected = -1;

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].checked) {
            notSelected = i;
        }
    }
    if (notSelected == -1) {
        alert("Please select LR Transfer");
        return false;
    }

    if (!(cTransactionType.value == "041" || cTransactionType.value == "141" || cTransactionType.value == "071" || cTransactionType.value == "171" || cTransactionType.value == "421" || cTransactionType.value == "431" || cTransactionType.value == "441" || cTransactionType.value == "461" || cTransactionType.value == "001" || cTransactionType.value == "101" || cTransactionType.value == "361")) {
        var ReqNum = document.getElementById(CtrlIdPrefix + "ddlSalesReqNumber");
        if (ReqNum.value.trim() == "0" || ReqNum.value.trim() == "" || ReqNum.value.trim() == null) {
            alert("Please Select Sales Request Number");
            ReqNum.focus();
            return false;
        }
    }

    var cCashDisc = document.getElementById(CtrlIdPrefix + "ddlCashDiscount");
    if (cCashDisc.value == "0") {
        alert("Please select Cash Discount");
        cCashDisc.focus();
        return false;
    }

    var indicator = document.getElementById(CtrlIdPrefix + "ddlVindicator");
    if (indicator.value.trim() == "0" || indicator.value.trim() == "" || indicator.value.trim() == null) {
        alert("Indicator is required");
        indicator.focus();
        return false;
    }

    if (cTransactionType.value == "001" || cTransactionType.value == "101") {
        var modeOfReceipt = document.getElementById(CtrlIdPrefix + "ddlModeOfREceipt");
        if (modeOfReceipt.value == "0") {
            alert("Please select Mode of Receipt");
            modeOfReceipt.focus();
            return false;
        }
    }

    if (cTransactionType.value == "101" || cTransactionType.value == "001") {

        var modeofPayment = document.getElementById(CtrlIdPrefix + "ddlModeOfREceipt");

        if (modeofPayment.value != "M") {
            var chequeDraftNo = document.getElementById(CtrlIdPrefix + "txtChequeDraftNo");
            if (chequeDraftNo.value == "") {
                alert("Cheque/Draft Number is required");
                chequeDraftNo.focus();
                return false;
            }

            var chequeDraftDt = document.getElementById(CtrlIdPrefix + "txtChequeDraftDt");
            if (chequeDraftDt.value == "") {
                alert("Cheque/Draft Date is required");
                chequeDraftDt.focus();
                return false;
            }

            var bank = document.getElementById(CtrlIdPrefix + "txtBank");
            if (bank.value == "") {
                alert("Bank is required");
                bank.focus();
                return false;
            }

            var cashBillCustomerTown = document.getElementById(CtrlIdPrefix + "txtCashBillCustomerTown");
            if (cashBillCustomerTown.value == "") {
                alert("Town is required");
                cashBillCustomerTown.focus();
                return false;
            }

            var bankBranch = document.getElementById(CtrlIdPrefix + "txtBankBranch");
            if (bankBranch.value == "") {
                alert("Branch is required");
                bankBranch.focus();
                return false;
            }

            var cashBillCustomer = document.getElementById(CtrlIdPrefix + "txtCashBillCustomer");
            if (cashBillCustomer.value == "") {
                alert("Customer is required");
                cashBillCustomer.focus();
                return false;
            }
        }
    }

    if (cTransactionType.value == "461" || cTransactionType.value == "361" || cTransactionType.value == "371" || cTransactionType.value == "471") {
        var refDocument = document.getElementById(CtrlIdPrefix + "txtRefDocument");
        if (refDocument.value == "") {
            alert("Reference Document is required");
            refDocument.focus();
            return false;
        }
    }

    var gItemsDtl = document.getElementById(CtrlIdPrefix + "grvItemDetails");
    if (gItemsDtl != null) {
        var rowscount = gItemsDtl.rows.length;
        if (rowscount >= 3) {
            for (k = 1; k < gItemsDtl.rows.length; k++) {
                if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
                    if (pid == 2) {
                        alert('No Item details are found.');
                        return false;
                    }
                }
                else {
                    var row = gItemsDtl.rows[k];
                    txtSno = row.cells[1].children[0];

                    if (txtSno != null) {
                        ddlSupp = row.cells[1].children[0];
                        ddlItem = row.cells[2].children[0];
                        txtOSLS = row.cells[3].children[0];
                        txtQty = row.cells[4].children[0];
                        txtCanQty = row.cells[4].children[1];
                        ddlSLB = row.cells[5].children[0];
                        txtListPrice = row.cells[6].children[0];
                        txtSLBNet = row.cells[7].children[0];
                        txtDiscount = row.cells[8].children[0];
                        txtST = row.cells[9].children[0];
                        //txtGP = row.cells[10].children[0];
                        
                        if (ddlSupp.id != null && ddlSupp.id != "") {
                            if (ddlSupp.value == "0" || ddlSupp.value == "0.00" || ddlSupp.value == "") {
                                alert("Please select supplier name in row " + k);
                                document.getElementById(ddlSupp.id).focus();
                                return false;
                            }
                        }

                        if (ddlItem.id != null && ddlItem.id != "") {
                            if (ddlItem.value == "0" || ddlItem.value == "0.00" || ddlItem.value == "") {
                                alert("Please select supplier part number in row " + k);
                                document.getElementById(ddlItem.id).focus();
                                return false;
                            }
                        }

                        if (txtOSLS.id != null && txtOSLS.id != "") {
                            if (txtOSLS.value == "0" || txtOSLS.value == "0.00" || txtOSLS.value == "") {
                                alert("OS/LS is not available in row " + k);
                                document.getElementById(txtOSLS.id).focus();
                                return false;
                            }
                        }

                        if (txtQty.id != null && txtQty.id != "") {
                            if (txtQty.value == "0" || txtQty.value == "0.00" || txtQty.value == "") {
                                alert("Please enter quantity in row " + k);
                                document.getElementById(txtQty.id).focus();
                                return false;
                            }
                        }

                        if (txtCanQty.id != null && txtCanQty.id != "") {
                            if (parseInt(txtQty.value) > parseInt(txtCanQty.value)) {
                                alert("Quantity Should not exceed " + txtCanQty.value + " in row " + k);
                                txtQty.value = "";
                                document.getElementById(txtQty.id).focus();
                                return false;
                            }
                        }

                        if (!(cTransactionType.value == "041" || cTransactionType.value == "141" || cTransactionType.value == "481")) {
                            if (ddlSLB.id != null && ddlSLB.id != "") {
                                if (ddlSLB.value == "0" || ddlSLB.value == "0.00" || ddlSLB.value == "") {
                                    alert("Please select SLB in row " + k);
                                    document.getElementById(ddlSLB.id).focus();
                                    return false;
                                }
                            }
                        }

                        if (txtListPrice.id != null && txtListPrice.id != "") {
                            if (txtListPrice.value == "0" || txtListPrice.value == "0.00" || txtListPrice.value == "") {
                                alert("Consignment list price is not available in row " + k);
                                return false;
                            }
                        }

                        if (!(cTransactionType.value == "041" || cTransactionType.value == "141" || cTransactionType.value == "481")) {
                            if (txtOSLS.id != null && txtOSLS.id != "") {
                                if (txtOSLS.value == "L" || txtOSLS.value == "O") {
                                    if (parseInt(ddlSLB.value) <= 50) {
                                        if (txtSLBNet.id != null && txtSLBNet.id != "") {
                                            if (txtSLBNet.value == "") {
                                                alert("SLB net value is not available in row " + k);
                                                return false;
                                            }
                                            if (parseInt(txtSLBNet.value) > 100 || parseInt(txtSLBNet.value) < -100) {
                                                alert("SLB net value should be in between -100 and 100 in row " + k);
                                                return false;
                                            }
                                        }
                                    }

                                    if (parseInt(ddlSLB.value) > 50 && parseInt(ddlSLB.value) <= 90) {
                                        if (txtSLBNet.id != null && txtSLBNet.id != "") {
                                            if (txtSLBNet.value == "0" || txtSLBNet.value == "0.00" || txtSLBNet.value == "") {
                                                alert("SLB net value is not available in row " + k);
                                                return false;
                                            }

                                            if (parseInt(txtSLBNet.value) < 0 ||  txtSLBNet.value == "0.00") {
                                                alert("SLB net value should not be negative or zero in row " + k);
                                                return false;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (cTransactionType.value == "041" || cTransactionType.value == "141" || cTransactionType.value == "481") {
                            if (txtSLBNet.id != null && txtSLBNet.id != "") {
                                if (txtSLBNet.value == "0" || txtSLBNet.value == "0.00" || txtSLBNet.value == "") {
                                    alert("SLB net value is not available in row " + k);
                                    return false;
                                }
                            }

//                            if (txtDiscount.id != null && txtDiscount.id != "") {
//                                if (txtDiscount.value == "0" || txtDiscount.value == "0.00" || txtDiscount.value == "") {
//                                    alert("Discount is not available in row " + k);
//                                    document.getElementById(txtDiscount.id).focus();
//                                    return false;
//                                }
//                            }
                        }

                        if (!(cTransactionType.value == "071" || cTransactionType.value == "171")) {
                            if (txtST.id != null && txtST.id != "") {
                                if (txtST.value == "0" || txtST.value == "0.00" || txtST.value == "") {
                                    alert("Sales Tax is not available in row " + k);
                                    return false;
                                }
                            }
                        }

                        if (k > 1) {
                            txtST1 = gItemsDtl.rows[k - 1].cells[9].children[0];
                            if (txtST.value != "S") {
                                if (parseFloat(txtST.value) != parseFloat(txtST1.value)) {
                                    alert("Multiple Sales Tax Billing is not Possible in GST. Please Check");
                                    return false;
                                }
                            }
                        }

//                        if (!(cTransactionType.value == "041" || cTransactionType.value == "141")) {
//                            if (txtGP.id != null && txtGP.id != "") {
//                                if (txtGP.value == "0" || txtGP.value == "0.00" || txtGP.value == "") {
//                                    alert("Gross profit is not available in row " + k);
//                                    return false;
//                                }
//                            }
//                        }
                    }
                }
            }
        }
    }
    
    return true;
}


function SalesInvoiceValidationHeader() {

    var cTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    if (cTransactionType.value == "0") {
        alert("Please select Transaction type");
        cTransactionType.focus();
        return false;
    }

    var cCustomer = document.getElementById(CtrlIdPrefix + "ddlCustomerName");
    if (cCustomer.value == "0") {
        alert("Please select Customer");
        cCustomer.focus();
        return false;
    }

    var cSalesMan = document.getElementById(CtrlIdPrefix + "ddlSalesMan");
    if (cSalesMan.value == "0") {
        alert("Please select Salesman");
        return false;
    }

    var cCanBullupto = document.getElementById(CtrlIdPrefix + "txtCanBillUpTo");
    var lblTextMsg = document.getElementById(CtrlIdPrefix + "lblHeaderMessage");
    var val = lblTextMsg.innerHTML;

    if (!(cTransactionType.value == "001" || cTransactionType.value == "101" || cTransactionType.value == "071" || cTransactionType.value == "171")) {
        if (val != "" || parseFloat(cCanBullupto.value) < 0) {
            alert('Credit Limit exceeds for this customer');
            return false;
        }
    }

    var rdLRTran = document.getElementById(CtrlIdPrefix + "rdLRTransfer");
    var inputs = rdLRTran.getElementsByTagName("input");

    var notSelected = -1;

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].checked) {
            notSelected = i;
        }
    }
    if (notSelected == -1) {
        alert("Please select LR Transfer");
        return false;
    }

    if (!(cTransactionType.value == "041" || cTransactionType.value == "141" || cTransactionType.value == "071" || cTransactionType.value == "171" || cTransactionType.value == "421" || cTransactionType.value == "431" || cTransactionType.value == "441" || cTransactionType.value == "461" || cTransactionType.value == "001" || cTransactionType.value == "101" || cTransactionType.value == "361")) {
        var ReqNum = document.getElementById(CtrlIdPrefix + "ddlSalesReqNumber");
        if (ReqNum.value.trim() == "0" || ReqNum.value.trim() == "" || ReqNum.value.trim() == null) {
            alert("Please Select Sales Request Number");
            ReqNum.focus();
            return false;
        }
    }

    var cCashDisc = document.getElementById(CtrlIdPrefix + "ddlCashDiscount");
    if (cCashDisc.value == "0") {
        alert("Please select Cash Discount");
        cCashDisc.focus();
        return false;
    }

    var indicator = document.getElementById(CtrlIdPrefix + "ddlVindicator");
    if (indicator.value.trim() == "0" || indicator.value.trim() == "" || indicator.value.trim() == null) {
        alert("Indicator is required");
        indicator.focus();
        return false;
    }

    if (cTransactionType.value == "001" || cTransactionType.value == "101") {
        var modeOfReceipt = document.getElementById(CtrlIdPrefix + "ddlModeOfREceipt");
        if (modeOfReceipt.value == "0") {
            alert("Please select Mode of Receipt");
            modeOfReceipt.focus();
            return false;
        }
    }

    if (cTransactionType.value == "101" || cTransactionType.value == "001") {

        var modeofPayment = document.getElementById(CtrlIdPrefix + "ddlModeOfREceipt");

        if (modeofPayment.value != "M") {
            var chequeDraftNo = document.getElementById(CtrlIdPrefix + "txtChequeDraftNo");
            if (chequeDraftNo.value == "") {
                alert("Cheque/Draft Number is required");
                chequeDraftNo.focus();
                return false;
            }

            var chequeDraftDt = document.getElementById(CtrlIdPrefix + "txtChequeDraftDt");
            if (chequeDraftDt.value == "") {
                alert("Cheque/Draft Date is required");
                chequeDraftDt.focus();
                return false;
            }

            var bank = document.getElementById(CtrlIdPrefix + "txtBank");
            if (bank.value == "") {
                alert("Bank is required");
                bank.focus();
                return false;
            }

            var cashBillCustomerTown = document.getElementById(CtrlIdPrefix + "txtCashBillCustomerTown");
            if (cashBillCustomerTown.value == "") {
                alert("Town is required");
                cashBillCustomerTown.focus();
                return false;
            }

            var bankBranch = document.getElementById(CtrlIdPrefix + "txtBankBranch");
            if (bankBranch.value == "") {
                alert("Branch is required");
                bankBranch.focus();
                return false;
            }

            var cashBillCustomer = document.getElementById(CtrlIdPrefix + "txtCashBillCustomer");
            if (cashBillCustomer.value == "") {
                alert("Customer is required");
                cashBillCustomer.focus();
                return false;
            }
        }
    }

    if (cTransactionType.value == "461" || cTransactionType.value == "361" || cTransactionType.value == "371" || cTransactionType.value == "471") {
        var refDocument = document.getElementById(CtrlIdPrefix + "txtRefDocument");
        if (refDocument.value == "") {
            alert("Reference Document is required");
            refDocument.focus();
            return false;
        }
    }

    return true;
}


function SalesInvoiceEntrySubmit() {
    var status = document.getElementById("ctl00_CPHDetails_chkActive");
    
    if (status == null) {
        if (SalesInvoiceValidation(2)) {
            if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
                alert('No Item details are found.');
                return false;
            }
        }
        else
            return false;

    }
    else {
        return true;
    }

}

function fnReset() {
    if (confirm("You will loose the unsaved informations.\n\nAre you sure you want reset the page?"))
        return true;
    else
        return false;
}

function CurrencyDecimalOnly(e1, evt) {

    var currenyValue = document.getElementById(e1);
    var Isvalid = parseFloat(currenyValue.value);

    if (Isvalid.toString() == "NaN") {
        document.getElementById(e1).value = "";
        document.getElementById(e1).focus();
        return false;
    }
    else {

        if (currenyValue.value.split('.').length > 2) {
            document.getElementById(e1).value = Isvalid;
        }
        else {
            document.getElementById(e1).value = currenyValue.value;
        }
    }

    return true;
}

function checkDate(sender, args) {

    var strSender = sender._id;
    var idRefDocDate = strSender.replace("ceCustomerPODate", "txtCustomerPODate");

    if (sender._selectedDate > new Date()) {
        alert("Customer PO date should not be greater than Today's Date");

        if ("ctl00_CPHDetails_ceCustomerPODate" == strSender) {
            document.getElementById("ctl00_CPHDetails_txtCustomerPODate").value = "";
        }
        else {
            document.getElementById(idRefDocDate).value = "";
        }
    }
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

    var printWin = window.open('', '', 'left=0,top=0,width=800,height=600,status=0');
    printWin.document.write(document.getElementById("divReportPopUpPrintArea").innerHTML);
    printWin.document.close();
    printWin.focus();
    printWin.print();
    printWin.close();

    funCloseMEP();
}

function checkNum() {

    if ((event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 96 && event.keyCode < 123) || event.keyCode == 8)
        return true;
    else {
        return false;
    }
}

function calculateTotalValue() {

    var totalValue = 0;

    var grd = document.getElementById('ctl00_CPHDetails_grvTransactionDetails');
    document.getElementById("ctl00_CPHDetails_txtTotalValue").value = 0;
    document.getElementById("ctl00_CPHDetails_txtCanBillUpTo").value = 0;

    for (i = 1; i < grd.rows.length; i++) {
        var node1 = grd.rows[i].cells[6].childNodes[0];
        var node2 = grd.rows[i].cells[4].childNodes[0];

        var browserName = navigator.appName;
        if (browserName == 'Netscape') {
            var node1 = grd.rows[i].cells[6].childNodes[1];
            var node2 = grd.rows[i].cells[4].childNodes[1];
        }
        //-- Total
        if (node1 != undefined && node1.type == "text")
            if (!isNaN(node1.value) && node1.value != "")
            totalValue += parseFloat(node1.value) * parseInt(node2.value);
    }

    document.getElementById("ctl00_CPHDetails_txtTotalValue").value = parseFloat(Math.round(totalValue * 100) / 100).toFixed(2);
    document.getElementById("ctl00_CPHDetails_txtCanBillUpTo").value = parseFloat(Math.round(credittotal * 100) / 100).toFixed(2);
}

function HideButton() {

    var status = document.getElementById("ctl00_CPHDetails_chkActive").checked;

    if (status == true) {
        document.getElementById('ctl00_CPHDetails_BtnSubmit').disabled = false;
    }
    else {
        document.getElementById('ctl00_CPHDetails_BtnSubmit').disabled = true;
    }
}