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

function funTransTYpeChange() {
    var ddlTrtype = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var txtInvValue = document.getElementById(CtrlIdPrefix + "txtInvoiceValue");
    var txtValue1 = document.getElementById(CtrlIdPrefix + "txtSGSTValue");
    var txtValue2 = document.getElementById(CtrlIdPrefix + "txtUTGSTValue");
    var txtValue3 = document.getElementById(CtrlIdPrefix + "txtCGSTValue");
    var txtValue4 = document.getElementById(CtrlIdPrefix + "txtIGSTValue");

    txtInvValue.value = "0.00";
    txtValue1.value = "0.00";
    txtValue2.value = "0.00";
    txtValue3.value = "0.00";
    txtValue4.value = "0.00";
    
//    if (ddlTrtype.value == "171") {        
//        txtInvValue.disabled = true;
//        txtValue1.disabled = true;
//        txtValue2.disabled = true;
//        txtValue3.disabled = true;
//        txtValue4.disabled = true;
//    }
//    else {
        txtInvValue.disabled = false;
        txtValue1.disabled = false;
        txtValue2.disabled = false;
        txtValue3.disabled = false;
        txtValue4.disabled = false;        
    //}
}

function funOSIndicatorChange() {
    var ddlOSInd = document.getElementById(CtrlIdPrefix + "ddlOSIndicator");
    var SGSTValue = document.getElementById(CtrlIdPrefix + "txtSGSTValue");
    var CGSTValue = document.getElementById(CtrlIdPrefix + "txtCGSTValue");
    var IGSTValue = document.getElementById(CtrlIdPrefix + "txtIGSTValue");
    var UTGSTValue = document.getElementById(CtrlIdPrefix + "txtUTGSTValue");

    if (ddlOSInd.value == "O") {
        SGSTValue.disabled = true;
        UTGSTValue.disabled = true;
        CGSTValue.disabled = true;
        IGSTValue.disabled = false;        
    }
    else {
        SGSTValue.disabled = false;
        UTGSTValue.disabled = false;
        CGSTValue.disabled = false;
        IGSTValue.disabled = true;       
    }
}

function funcShowEditOption() {
    //alert(document.getElementById(CtrlIdPrefix + "txtInwardNumber"));
    //alert(txtFlag);
    //alert(document.getElementById(CtrlIdPrefix + "hdnScreenMode").value);
    if (document.getElementById(CtrlIdPrefix + "hdnScreenMode").value == "A") {
        //alert(txtFlag);
        //txtFlag = "EditMode";
        document.getElementById(CtrlIdPrefix + "hdnScreenMode").value = "E";
        document.getElementById(CtrlIdPrefix + "txtInwardNumber").style.visibility = "hidden";
        document.getElementById(CtrlIdPrefix + "ddlInwardNumber").style.display = "inline";
    }
    else {

        document.getElementById(CtrlIdPrefix + "hdnScreenMode").value = "A";
        document.getElementById(CtrlIdPrefix + "txtInwardNumber").style.visibility = "visible";
        document.getElementById(CtrlIdPrefix + "ddlInwardNumber").style.display = "none";
    }

    //alert(ddlObj);
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

    var CtrlID = CtrlIdPrefix + CalenderCtrlClientId;
    document.getElementById(CtrlID).click();
}

function TriggerGridCalender(GridCalenderCtrlClientId) {
    var CtrlID = GridCalenderCtrlClientId;
    document.getElementById(CtrlID).click();
}

function CurrencywithNegativeNumberOnly() {
    
    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46 || AsciiValue == 45))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function CurrencyDecimalOnly(e1, evt) {
    
    var currenyValue = document.getElementById(e1);

    var Isvalid = parseFloat(currenyValue.value);

    if (Isvalid.toString() == "NaN" && currenyValue.value != "-") {
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

function CurrencyValidateForNegative(e1, evt) {
    
    var currenyValue = document.getElementById(e1);

    var Isvalid = parseFloat(currenyValue.value);
    var cvalue = currenyValue.value;
    var firstchr = cvalue.substring(0, 1, cvalue);   

    if (cvalue.indexOf("-") > -1) {
        if (firstchr != "-") {
            alert("Please enter valid negative number");
            document.getElementById(e1).focus();
            return false;
        }
    }    

    return true;

}

function CurrencyNumberOnly() {
    
    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function AutoFillCGSTSGST(id) {
    
    var txtSGSTValue = document.getElementById(CtrlIdPrefix + "txtSGSTValue");
    var txtUTGSTValue = document.getElementById(CtrlIdPrefix + "txtUTGSTValue");
    var txtCGSTValue = document.getElementById(CtrlIdPrefix + "txtCGSTValue");    
    var Id = id.replace(CtrlIdPrefix, "");
    
    if (Id == "txtSGSTValue") {
        if (txtSGSTValue.style.display == "inline") {
            if (txtSGSTValue.value == "" || txtSGSTValue.value == "0" || txtSGSTValue.value == "0.00")
               txtSGSTValue.value = "0.00";
            
            txtCGSTValue.value = txtSGSTValue.value;
        }
    }
    
    if (Id == "txtUTGSTValue") {
        if (txtUTGSTValue.style.display == "inline") {
            if (txtUTGSTValue.value == "" || txtUTGSTValue.value == "0" || txtUTGSTValue.value == "0.00")
                txtUTGSTValue.value = "0.00";
                
            txtCGSTValue.value = txtUTGSTValue.value;
        }
    }
    
    if (Id == "txtCGSTValue") {
        if (txtSGSTValue.style.display == "inline") {
            if (txtCGSTValue.value == "" || txtCGSTValue.value == "0" || txtCGSTValue.value == "0.00")
                txtCGSTValue.value = "0.00";
                
            txtSGSTValue.value = txtCGSTValue.value;
        }
    }
    
    if (Id == "txtCGSTValue") {
        if (txtUTGSTValue.style.display == "inline") {
            if (txtCGSTValue.value == "" || txtCGSTValue.value == "0" || txtCGSTValue.value == "0.00")
                txtCGSTValue.value = "0.00";
                
            txtUTGSTValue.value = txtCGSTValue.value;
        }
    }    
}

function CurrencyNumberwithOneDotOnly(id) {

    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;

    strValue = document.getElementById(id);

    if (strValue.value.split('.').length > 2)
        return false;
    else
        return true;
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

function AlphaNumericWithSlashDash() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 45 || AsciiValue == 47) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function AlphaNumericWithSlashAndSpace() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 47 || AsciiValue == 32) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
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

        if ((sender.get_id() == "ctl00_CPHDetails_CalExtLRDate") || (sender.get_id() == "ctl00_CPHDetails_CalExtDcDate")) {
            sender._textbox.set_Value("");
        }
        else {
            sender._textbox.set_Value(dtDate);
        }
        return false;
    }

    if (sender.get_id() == "ctl00_CPHDetails_CalExtInvoiceDate") {
        if (document.getElementById(CtrlIdPrefix + "txtLRDate").value.trim() != "") {
            if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtInvoiceDate").value, document.getElementById(CtrlIdPrefix + "txtLRDate").value)) {
                alert('Invoice Date(' + document.getElementById(CtrlIdPrefix + "txtInvoiceDate").value + ') should be less than the LR Date.');
                var dtDate = new Date();
                dtDate = dtDate.format(sender._format);
                sender._textbox.set_Value("");
                return false;
            }
        }

        document.getElementById(CtrlIdPrefix + "txtDcDate").value = "";
        document.getElementById(CtrlIdPrefix + "txtLRDate").value = "";
    }

    if (sender.get_id() == "ctl00_CPHDetails_CalExtLRDate") {
        //alert(sender.get_id());
        if (document.getElementById(CtrlIdPrefix + "txtInvoiceDate").value.trim() != "") {
            if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtInvoiceDate").value, document.getElementById(CtrlIdPrefix + "txtLRDate").value)) {
                alert('LR Date(' + document.getElementById(CtrlIdPrefix + "txtLRDate").value + ') should be greater than the Invoice Date.');
                var dtDate = new Date();
                dtDate = dtDate.format(sender._format);
                sender._textbox.set_Value("");
                return false;
            }
        }
    }

    if (sender.get_id() == "ctl00_CPHDetails_CalExtDcDate") {
        if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtDcDate").value, document.getElementById(CtrlIdPrefix + "txtInvoiceDate").value)) {
            alert('DC Date(' + document.getElementById(CtrlIdPrefix + "txtDcDate").value + ') should be less than the Invoice Date.');
            var dtDate = new Date();
            dtDate = dtDate.format(sender._format);
            sender._textbox.set_Value("");
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

    if (dtDate1 > dtDate2)
        return true;
    else
        return false;
}

function IsHigherOrEqualDate(Date1, Date2) {
    var strDateArr1 = Date1.split('/');
    var strDateArr2 = Date2.split('/');

    var dtDate1 = new Date();
    var dtDate2 = new Date();
    dtDate1.setFullYear(strDateArr1[2], strDateArr1[1] - 1, strDateArr1[0]);
    dtDate2.setFullYear(strDateArr2[2], strDateArr2[1] - 1, strDateArr2[0]);

    if (dtDate1 >= dtDate2)
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

function checkDateForReceivedDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var idInvoiceDate = document.getElementById("ctl00_CPHDetails_txtInvoiceDate").value

            var toDate = new Date();
            toDate = convertDate(idDate);

            var frmDate = new Date();
            frmDate = convertDate(idInvoiceDate);
            
            if (toDate < frmDate) {
                document.getElementById(id).value = "";
                alert("Received Date should not be less than the Invoice Date");
            }
        }
    }
}

function checkDateForLRDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var idInvoiceDate = document.getElementById("ctl00_CPHDetails_txtInvoiceDate").value

            var toDate = new Date();
            toDate = convertDate(idDate);

            var frmDate = new Date();
            frmDate = convertDate(idInvoiceDate);

            if (toDate < frmDate) {
                document.getElementById(id).value = "";
                alert("LR Date should be greater than or equal to Invoice Date");
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

function checkDateForDCDate(id) {    

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var idInvoiceDate = document.getElementById("ctl00_CPHDetails_txtInvoiceDate").value

            var toDate = new Date();
            toDate = convertDate(idDate);

            var frmDate = new Date();
            frmDate = convertDate(idInvoiceDate);

            if (toDate > frmDate) {
                document.getElementById(id).value = "";
                alert("DC Date should be less than or equal to Invoice Date");
            }
        }
    }
}

function checkDateForInvoice(id, Stat) {

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
                alert("Invoice date should be less than or equal to sysdate");
            }
            else {
                if (Stat == "1") {
                    document.getElementById("ctl00_CPHDetails_txtReceivedDate").value = "";
                    document.getElementById("ctl00_CPHDetails_txtDcDate").value = "";
                    document.getElementById("ctl00_CPHDetails_txtLRDate").value = "";
                }
            }
        }
    }    
}


function checkPackageOpenDate(sender, args) {
    //alert('asdad');
    var dtDate = new Date();

    if ((sender._selectedDate > dtDate)) {
        alert('Package open date should not be a future date.');
        //sender._textbox.set_Value("");
        var dtDate = new Date();
        dtDate = dtDate.format(sender._format);
        sender._textbox.set_Value(dtDate);
    }
}

function checkClearenceDate(sender, args) {
    //alert('asdad');
    var dtDate = new Date();

    if ((sender._selectedDate > dtDate)) {
        alert('Clearence date should not be a future date.');
        //sender._textbox.set_Value("");
        var dtDate = new Date();
        dtDate = dtDate.format(sender._format);
        sender._textbox.set_Value(dtDate);
    }
}

function funBtnAddRow() {
    //alert('asda');
    if (InwardHeaderValidation()) {
        document.getElementById(CtrlIdPrefix + 'ddlTransactionType').disabled = true;
        document.getElementById(CtrlIdPrefix + 'ddlSupplierName').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtInvoiceValue').disabled = true;
        document.getElementById(CtrlIdPrefix + 'ddlOSIndicator').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtSGSTValue').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtUTGSTValue').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtCGSTValue').disabled = true;
        document.getElementById(CtrlIdPrefix + 'txtIGSTValue').disabled = true;

        if (!GridCheck())
            return false;

        return true;
    }
    else
        return false;
}
function funReset() {
    //alert(document.getElementById(CtrlIdPrefix + "DivHeader").disabled);
    if (!document.getElementById(CtrlIdPrefix + "DivHeader").disabled) {
        if (confirm("You will loose the unsaved informations.\n\nAre you sure you want reset the page?"))
            return true;
        else
            return false;
    }
    else
        return true;
}

function CurrencyChkForMoreThanOneDot(strValue) {
    //alert('asd');
    //alert(strValue.split('.').length);
    if (strValue.split('.').length > 2)
        return false;
    else
        return true;
}

function InwardHeaderValidation() {    
    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

    var ddlSupp = document.getElementById(CtrlIdPrefix + "ddlSupplierName");
    var Supplier = ddlSupp.options[ddlSupp.selectedIndex].value;

    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;
    
    var ddlFrgtInd = document.getElementById(CtrlIdPrefix + "ddlFrieghtIndicator");
    var FrgtInd = ddlFrgtInd.options[ddlFrgtInd.selectedIndex].value;
   
    var ddlSupplyPlant = document.getElementById(CtrlIdPrefix + "ddlSupplyPlant");
    var SupplyPlnt = ddlSupplyPlant.options[ddlSupplyPlant.selectedIndex].value;
    
    var ddlOSInd = document.getElementById(CtrlIdPrefix + "ddlOSIndicator");
    var OSIndicator = ddlOSInd.options[ddlOSInd.selectedIndex].value;
    
    var SecondSales = document.getElementById(CtrlIdPrefix + "hdnSecondSales").value;
    var invoiceNo = document.getElementById(CtrlIdPrefix + "txtInvoiceNo");
    var invoiceDate = document.getElementById(CtrlIdPrefix + "txtInvoiceDate");
    var rcvdDate = document.getElementById(CtrlIdPrefix + "txtReceivedDate");
    var invoiceValue = document.getElementById(CtrlIdPrefix + "txtInvoiceValue");
    var SGSTValue = document.getElementById(CtrlIdPrefix + "txtSGSTValue");
    var CGSTValue = document.getElementById(CtrlIdPrefix + "txtCGSTValue");
    var IGSTValue = document.getElementById(CtrlIdPrefix + "txtIGSTValue");
    var UTGSTValue = document.getElementById(CtrlIdPrefix + "txtUTGSTValue");

    var LRNumber = document.getElementById(CtrlIdPrefix + "txtLRNumber");
    var LRDate = document.getElementById(CtrlIdPrefix + "txtLRDate");
    var Carrier = document.getElementById(CtrlIdPrefix + "txtCarrier");
    var PlaceOfDespatch = document.getElementById(CtrlIdPrefix + "txtPlaceOfDespatch");
    
    if (Branch == "0") {
        alert('Please select a Branch.');
        ddlBrnch.focus();
        return false;
    }

    //alert('<%=ViewState("GridRowCount")%>');
    if (TransType == "0") {
        alert('Please select a Transaction Type.');
        ddlTransType.focus();
        return false;
    }
    //Free Of Cost

    if (TransType == "171") //Free Of Cost
    {
        //alert('in free of cost');
        if (Supplier == "0") {
            alert("Please select a Supplier.");
            ddlSupp.focus();
            return false;
        }

        if (Branch == "0") {
            alert("Please select a Branch.");
            ddlBrnch.focus();
            return false;
        }

        if (FrgtInd == "0") {
            alert("Please select a Freight Indicator.");
            ddlFrgtInd.focus();
            return false;
        }

        if (invoiceNo.value.trim() == "") {
            alert("Invoice Number should not be empty.");
            invoiceNo.value = "";
            invoiceNo.focus();
            return false;
        }

        if (invoiceDate.value.trim() == "") {
            alert("Invoice Date should not be empty.");
            invoiceDate.value = "";
            invoiceDate.focus();
            return false;
        }

        if (rcvdDate.value.trim() == "") {
            alert("Received Date should not be empty.");
            rcvdDate.value = "";
            rcvdDate.focus();
            return false;
        }

        var arrInvoiceDate = invoiceDate.value.split("/");
        var arrRcvdDate = rcvdDate.value.split("/");
        var invoiceDate1 = new Date(arrInvoiceDate[2] + "/" + arrInvoiceDate[1] + "/" + arrInvoiceDate[0]);
        var rcvdDate1 = new Date(arrRcvdDate[2] + "/" + arrRcvdDate[1] + "/" + arrRcvdDate[0]);

        if (invoiceDate1 > rcvdDate1) {
            alert("Invoice Date can not be greater than Received date.");
            invoiceDate.focus();
            return false;
        }

        if (invoiceValue.value.trim() == "") {
            alert("Invoice Value should not be empty.");
            invoiceValue.value = "";
            invoiceValue.focus();
            return false;
        }

        if (!CurrencyChkForMoreThanOneDot(invoiceValue.value.trim())) {
            alert("Please enter valid Invoice Value.");
            invoiceValue.focus();
            return false;
        }

        if (SupplyPlnt == "0") {
            alert("Please select a Supply Plant.");
            ddlSupplyPlant.focus();
            return false;
        }

        if (parseFloat(invoiceValue.value) > 0) {
            if (OSIndicator == "L") {
                if (SGSTValue.style.display == "inline") {
                    if (SGSTValue.value.trim() == "0.00" || SGSTValue.value.trim() == "" || parseInt(SGSTValue.value.trim()) == 0) {
                        alert("SGST Value should be greater than zero for LS.");
                        SGSTValue.focus();
                        return false;
                    }

                    if (!CurrencyChkForMoreThanOneDot(SGSTValue.value.trim())) {
                        alert("Please enter valid SGST value.");
                        SGSTValue.focus();
                        return false;
                    }
                }

                if (UTGSTValue.style.display == "inline") {
                    if (UTGSTValue.value.trim() == "0.00" || UTGSTValue.value.trim() == "" || parseInt(UTGSTValue.value.trim()) == 0) {
                        alert("UTGST Value should be greater than zero for OS.");
                        UTGSTValue.focus();
                        return false;
                    }

                    if (!CurrencyChkForMoreThanOneDot(UTGSTValue.value.trim())) {
                        alert("Please enter valid UTGST value.");
                        UTGSTValue.focus();
                        return false;
                    }
                }

                if (CGSTValue.value.trim() == "0.00" || CGSTValue.value.trim() == "" || parseInt(CGSTValue.value.trim()) == 0) {
                    alert("CGST Value should be greater than zero for LS.");
                    CGSTValue.focus();
                    return false;
                }

                if (!CurrencyChkForMoreThanOneDot(CGSTValue.value.trim())) {
                    alert("Please enter valid CGST value.");
                    CGSTValue.focus();
                    return false;
                }


                if (SGSTValue.style.display == "inline") {
                    if (SGSTValue.value.trim() != CGSTValue.value.trim()) {
                        alert("SGST Value and CGST Values are not Matching. Please Check");
                        CGSTValue.focus();
                        return false;
                    }
                }

                if (UTGSTValue.style.display == "inline") {
                    if (UTGSTValue.value.trim() != CGSTValue.value.trim()) {
                        alert("UTGST Value and CGST Values are not Matching. Please Check");
                        CGSTValue.focus();
                        return false;
                    }
                }

                if (SGSTValue.style.display == "inline") {
                    if ((parseFloat(SGSTValue.value.trim()) + parseFloat(CGSTValue.value.trim())) >= parseFloat(invoiceValue.value.trim())) {
                        alert("Sum of Header SGST Value and CGST Value Should be less than the Invoice value");
                        SGSTValue.value = "0.00";
                        CGSTValue.value = "0.00";
                        SGSTValue.focus();
                        return false;
                    }
                }

                if (UTGSTValue.style.display == "inline") {
                    if ((parseFloat(UTGSTValue.value.trim()) + parseFloat(CGSTValue.value.trim())) >= parseFloat(invoiceValue.value.trim())) {
                        alert("Sum of Header UTGST Value and CGST Value Should be less than the Invoice value");
                        UTGSTValue.value = "0.00";
                        CGSTValue.value = "0.00";
                        UTGSTValue.focus();
                        return false;
                    }
                }                
            }
            else {
                if (IGSTValue.value.trim() == "0.00" || IGSTValue.value.trim() == "" || parseInt(IGSTValue.value.trim()) == 0) {
                    alert("IGST Value should be greater than zero for OS.");
                    IGSTValue.focus();
                    return false;
                }

                if (!CurrencyChkForMoreThanOneDot(IGSTValue.value.trim())) {
                    alert("Please enter valid IGST value.");
                    IGSTValue.focus();
                    return false;
                }

                if (parseFloat(IGSTValue.value.trim()) >= parseFloat(invoiceValue.value.trim())) {
                    alert("Header IGST Value Should be less than the Invoice value");
                    IGSTValue.value = "0.00";
                    IGSTValue.focus();
                    return false;
                }
            }
        }
        else {
            if (OSIndicator == "L") {
                if (SGSTValue.style.display == "inline") {
                    if (SGSTValue.value.trim() == "0.00" || SGSTValue.value.trim() == "" || parseInt(SGSTValue.value.trim()) == 0) {
                        SGSTValue.value = "0.00";
                    }

                    if (!CurrencyChkForMoreThanOneDot(SGSTValue.value.trim())) {
                        alert("Please enter valid SGST value.");
                        SGSTValue.focus();
                        return false;
                    }
                }

                if (UTGSTValue.style.display == "inline") {
                    if (UTGSTValue.value.trim() == "0.00" || UTGSTValue.value.trim() == "" || parseInt(UTGSTValue.value.trim()) == 0) {
                        UTGSTValue.value = "0.00";
                    }

                    if (!CurrencyChkForMoreThanOneDot(UTGSTValue.value.trim())) {
                        alert("Please enter valid UTGST value.");
                        UTGSTValue.focus();
                        return false;
                    }
                }

                if (CGSTValue.value.trim() == "0.00" || CGSTValue.value.trim() == "" || parseInt(CGSTValue.value.trim()) == 0) {
                    CGSTValue.value = "0.00";
                }

                if (!CurrencyChkForMoreThanOneDot(CGSTValue.value.trim())) {
                    alert("Please enter valid CGST value.");
                    CGSTValue.focus();
                    return false;
                }
                
                if (SGSTValue.style.display == "inline") {
                    if (SGSTValue.value.trim() != CGSTValue.value.trim()) {
                        alert("SGST Value and CGST Values are not Matching. Please Check");
                        CGSTValue.focus();
                        return false;
                    }
                }

                if (UTGSTValue.style.display == "inline") {
                    if (UTGSTValue.value.trim() != CGSTValue.value.trim()) {
                        alert("UTGST Value and CGST Values are not Matching. Please Check");
                        CGSTValue.focus();
                        return false;
                    }
                }                

                if (SGSTValue.style.display == "inline") {
                    if (parseFloat(SGSTValue.value.trim()) > 0 && (parseFloat(SGSTValue.value.trim()) + parseFloat(CGSTValue.value.trim())) >= parseFloat(invoiceValue.value.trim())) {
                        alert("Sum of Header SGST Value and CGST Value Should be less than the Invoice value");
                        SGSTValue.value = "0.00";
                        CGSTValue.value = "0.00";
                        SGSTValue.focus();
                        return false;
                    }
                }

                if (UTGSTValue.style.display == "inline") {
                    if (parseFloat(UTGSTValue.value.trim()) > 0 && (parseFloat(UTGSTValue.value.trim()) + parseFloat(CGSTValue.value.trim())) >= parseFloat(invoiceValue.value.trim())) {
                        alert("Sum of Header UTGST Value and CGST Value Should be less than the Invoice value");
                        UTGSTValue.value = "0.00";
                        CGSTValue.value = "0.00";
                        UTGSTValue.focus();
                        return false;
                    }
                }
            }
            else {
                if (IGSTValue.value.trim() == "0.00" || IGSTValue.value.trim() == "" || parseInt(IGSTValue.value.trim()) == 0) {
                    alert("IGST Value should be greater than zero for OS.");
                    IGSTValue.focus();
                    return false;
                }

                if (!CurrencyChkForMoreThanOneDot(IGSTValue.value.trim())) {
                    alert("Please enter valid IGST value.");
                    IGSTValue.focus();
                    return false;
                }

                if (parseFloat(IGSTValue.value.trim()) >= parseFloat(invoiceValue.value.trim())) {
                    alert("Header IGST Value Should be less than the Invoice value");
                    IGSTValue.value = "0.00";
                    IGSTValue.focus();
                    return false;
                }
            }
        }
    }
    else if (TransType == "201") //GRN
    {
        if (Supplier == "0") {
            alert("Please select a Supplier.");
            ddlSupp.focus();
            return false;
        }

        if (Branch == "0") {
            alert("Please select a Branch.");
            ddlBrnch.focus();
            return false;
        }

        if (FrgtInd == "0") {
            alert("Please select a Freight Indicator.");
            ddlFrgtInd.focus();
            return false;
        }
        if (invoiceNo.value.trim() == "") {
            alert("Invoice Number should not be empty.");
            invoiceNo.value = "";
            invoiceNo.focus();
            return false;
        }
        if (invoiceDate.value.trim() == "") {
            alert("Invoice Date should not be empty.");
            invoiceDate.value = "";
            invoiceDate.focus();
            return false;
        }
        if (rcvdDate.value.trim() == "") {
            alert("Received Date should not be empty.");
            rcvdDate.value = "";
            rcvdDate.focus();
            return false;
        }

        var arrInvoiceDate = invoiceDate.value.split("/");
        var arrRcvdDate = rcvdDate.value.split("/");
        var invoiceDate1 = new Date(arrInvoiceDate[2] + "/" + arrInvoiceDate[1] + "/" + arrInvoiceDate[0]);
        var rcvdDate1 = new Date(arrRcvdDate[2] + "/" + arrRcvdDate[1] + "/" + arrRcvdDate[0]);

        if (invoiceDate1 > rcvdDate1) {
            alert("Invoice Date can not be greater than Received date.");
            invoiceDate.focus();
            return false;
        }

        if (invoiceValue.value.trim() == "") {
            alert("Invoice Value should not be empty.");
            invoiceValue.value = "";
            invoiceValue.focus();
            return false;
        }

        if (parseInt(invoiceValue.value.trim()) == 0) {
            alert("Invoice Value should not be zero.");
            invoiceValue.value = "";
            invoiceValue.focus();
            return false;
        }

        if (!CurrencyChkForMoreThanOneDot(invoiceValue.value.trim())) {
            alert("Please enter valid Invoice Value.");
            invoiceValue.focus();
            return false;
        }

        if (SupplyPlnt == "0") {
            alert("Please select a Supply Plant.");
            ddlSupplyPlant.focus();
            return false;
        }

        if (OSIndicator == "L") {
            if (SGSTValue.style.display == "inline") {
                if (SGSTValue.value.trim() == "0.00" || SGSTValue.value.trim() == "" || parseInt(SGSTValue.value.trim()) == 0) {
                    alert("SGST Value should be greater than zero for LS.");
                    SGSTValue.focus();
                    return false;
                }

                if (!CurrencyChkForMoreThanOneDot(SGSTValue.value.trim())) {
                    alert("Please enter valid SGST value.");
                    SGSTValue.focus();
                    return false;
                }
            }

            if (UTGSTValue.style.display == "inline") {
                if (UTGSTValue.value.trim() == "0.00" || UTGSTValue.value.trim() == "" || parseInt(UTGSTValue.value.trim()) == 0) {
                    alert("UTGST Value should be greater than zero for OS.");
                    UTGSTValue.focus();
                    return false;
                }

                if (!CurrencyChkForMoreThanOneDot(UTGSTValue.value.trim())) {
                    alert("Please enter valid UTGST value.");
                    UTGSTValue.focus();
                    return false;
                }
            }

            if (CGSTValue.value.trim() == "0.00" || CGSTValue.value.trim() == "" || parseInt(CGSTValue.value.trim()) == 0) {
                alert("CGST Value should be greater than zero for LS.");
                CGSTValue.focus();
                return false;
            }

            if (!CurrencyChkForMoreThanOneDot(CGSTValue.value.trim())) {
                alert("Please enter valid CGST value.");
                CGSTValue.focus();
                return false;
            }
            
            if (SGSTValue.style.display == "inline") {
                if (SGSTValue.value.trim() != CGSTValue.value.trim()) {
                    alert("SGST Value and CGST Values are not Matching. Please Check");
                    CGSTValue.focus();
                    return false;
                }
            }

            if (UTGSTValue.style.display == "inline") {
                if (UTGSTValue.value.trim() != CGSTValue.value.trim()) {
                    alert("UTGST Value and CGST Values are not Matching. Please Check");
                    CGSTValue.focus();
                    return false;
                }
            }

            if (SGSTValue.style.display == "inline") {
                if ((parseFloat(SGSTValue.value.trim()) + parseFloat(CGSTValue.value.trim())) >= parseFloat(invoiceValue.value.trim())) {
                    alert("Sum of Header SGST Value and CGST Value Should be less than the Invoice value");
                    SGSTValue.value = "";
                    CGSTValue.value = "";
                    SGSTValue.focus();
                    return false;
                }
            }

            if (UTGSTValue.style.display == "inline") {
                if ((parseFloat(UTGSTValue.value.trim()) + parseFloat(CGSTValue.value.trim())) >= parseFloat(invoiceValue.value.trim())) {
                    alert("Sum of Header UTGST Value and CGST Value Should be less than the Invoice value");
                    UTGSTValue.value = "";
                    CGSTValue.value = "";
                    UTGSTValue.focus();
                    return false;
                }
            }
        }
        else {
            if (IGSTValue.value.trim() == "0.00" || IGSTValue.value.trim() == "" || parseInt(IGSTValue.value.trim()) == 0) {
                alert("IGST Value should be greater than zero for OS.");
                IGSTValue.focus();
                return false;
            }

            if (!CurrencyChkForMoreThanOneDot(IGSTValue.value.trim())) {
                alert("Please enter valid IGST value.");
                IGSTValue.focus();
                return false;
            }

            if (parseFloat(IGSTValue.value.trim()) >= parseFloat(invoiceValue.value.trim())) {
                alert("Header IGST Value Should be less than the Invoice value");
                IGSTValue.value = "";
                IGSTValue.focus();
                return false;
            }
        }
    }
    else if (TransType == "461") //Factory Direct Order
    {
        if (Supplier == "0") {
            alert("Please select a Supplier.");
            ddlSupp.focus();
            return false;
        }

        if (Branch == "0") {
            alert("Please select a Branch.");
            ddlBrnch.focus();
            return false;
        }

        if (invoiceNo.value.trim() == "") {
            alert("Invoice Number should not be empty.");
            invoiceNo.value = "";
            invoiceNo.focus();
            return false;
        }
        if (invoiceDate.value.trim() == "") {
            alert("Invoice Date should not be empty.");
            invoiceDate.value = "";
            invoiceDate.focus();
            return false;
        }
        if (rcvdDate.value.trim() == "") {
            alert("Received Date should not be empty.");
            rcvdDate.value = "";
            rcvdDate.focus();
            return false;
        }

        var arrInvoiceDate = invoiceDate.value.split("/");
        var arrRcvdDate = rcvdDate.value.split("/");
        var invoiceDate1 = new Date(arrInvoiceDate[2] + "/" + arrInvoiceDate[1] + "/" + arrInvoiceDate[0]);
        var rcvdDate1 = new Date(arrRcvdDate[2] + "/" + arrRcvdDate[1] + "/" + arrRcvdDate[0]);
        if (invoiceDate1 > rcvdDate1) {
            alert("Invoice Date can not be greater than Received date.");
            invoiceDate.focus();
            return false;
        }

        if (invoiceValue.value.trim() == "") {
            alert("Invoice Value should not be empty.");
            invoiceValue.value = "";
            invoiceValue.focus();
            return false;
        }

        if (parseInt(invoiceValue.value.trim()) == 0) {
            alert("Invoice Value should not be zero.");
            invoiceValue.value = "";
            invoiceValue.focus();
            return false;
        }

        if (!CurrencyChkForMoreThanOneDot(invoiceValue.value.trim())) {
            alert("Please enter valid Invoice Value.");
            invoiceValue.focus();
            return false;
        }

        if (SupplyPlnt == "0") {
            alert("Please select a Supply Plant.");
            ddlSupplyPlant.focus();
            return false;
        }

        if (OSIndicator == "L") {
            if (SGSTValue.style.display == "inline") {
                if (SGSTValue.value.trim() == "0.00" || SGSTValue.value.trim() == "" || parseInt(SGSTValue.value.trim()) == 0) {
                    alert("SGST Value should be greater than zero for LS.");
                    SGSTValue.focus();
                    return false;
                }

                if (!CurrencyChkForMoreThanOneDot(SGSTValue.value.trim())) {
                    alert("Please enter valid SGST value.");
                    SGSTValue.focus();
                    return false;
                }
            }

            if (UTGSTValue.style.display == "inline") {
                if (UTGSTValue.value.trim() == "0.00" || UTGSTValue.value.trim() == "" || parseInt(UTGSTValue.value.trim()) == 0) {
                    alert("UTGST Value should be greater than zero for LS.");
                    UTGSTValue.focus();
                    return false;
                }

                if (!CurrencyChkForMoreThanOneDot(UTGSTValue.value.trim())) {
                    alert("Please enter valid UTGST value.");
                    UTGSTValue.focus();
                    return false;
                }
            }

            if (CGSTValue.value.trim() == "0.00" || CGSTValue.value.trim() == "" || parseInt(CGSTValue.value.trim()) == 0) {
                alert("CGST Value should be greater than zero for LS.");
                CGSTValue.focus();
                return false;
            }

            if (!CurrencyChkForMoreThanOneDot(CGSTValue.value.trim())) {
                alert("Please enter valid CGST value.");
                CGSTValue.focus();
                return false;
            }
            
            if (SGSTValue.style.display == "inline") {
                if (SGSTValue.value.trim() != CGSTValue.value.trim()) {
                    alert("SGST Value and CGST Values are not Matching. Please Check");
                    CGSTValue.focus();
                    return false;
                }
            }

            if (UTGSTValue.style.display == "inline") {
                if (UTGSTValue.value.trim() != CGSTValue.value.trim()) {
                    alert("UTGST Value and CGST Values are not Matching. Please Check");
                    CGSTValue.focus();
                    return false;
                }
            }

            if (SGSTValue.style.display == "inline") {
                if ((parseFloat(SGSTValue.value.trim()) + parseFloat(CGSTValue.value.trim())) >= parseFloat(invoiceValue.value.trim())) {
                    alert("Sum of Header SGST Value and CGST Value Should be less than the Invoice value");
                    SGSTValue.value = "";
                    CGSTValue.value = "";
                    SGSTValue.focus();
                    return false;
                }
            }

            if (UTGSTValue.style.display == "inline") {
                if ((parseFloat(UTGSTValue.value.trim()) + parseFloat(CGSTValue.value.trim())) >= parseFloat(invoiceValue.value.trim())) {
                    alert("Sum of Header UTGST Value and CGST Value Should be less than the Invoice value");
                    UTGSTValue.value = "";
                    CGSTValue.value = "";
                    UTGSTValue.focus();
                    return false;
                }
            }
        }
        else {
            if (CGSTValue.value.trim() == "0.00" || CGSTValue.value.trim() == "" || parseInt(CGSTValue.value.trim()) == 0) {
                alert("IGST Value should be greater than zero for OS.");
                IGSTValue.focus();
                return false;
            }

            if (!CurrencyChkForMoreThanOneDot(IGSTValue.value.trim())) {
                alert("Please enter valid IGST value.");
                IGSTValue.focus();
                return false;
            }

            if (parseFloat(SGSTValue.value.trim()) >= parseFloat(invoiceValue.value.trim())) {
                alert("Header IGST Value Should be less than the Invoice value");
                IGSTValue.value = "";
                IGSTValue.focus();
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

        if (Carrier.value.trim() == "") {
            alert("Carrier information should not be empty.");
            Carrier.value = "";
            Carrier.focus();
            return false;
        }
        
        if (PlaceOfDespatch.value.trim() == "") {
            alert("Place of dispatch should not be empty.");
            PlaceOfDespatch.value = "";
            PlaceOfDespatch.focus();
            return false;
        }

        if (FrgtInd == "0") {
            alert("Please select a Freight Indicator.");
            ddlFrgtInd.focus();
            return false;
        }
    }
    
    return true;
}

function InwardEntrySubmit() {
    if (InwardHeaderValidation()) {

        if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
            alert('No Item details are found.');
            return false;
        }

        if (GridCheck()) {
            var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
            var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

            //Check the footer summation is equal to the invoice value.
            var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
            var footerid_TaxValue = document.getElementById(CtrlIdPrefix + "hdnFooterTaxPrice").value;
            var footerid_Coupon = document.getElementById(CtrlIdPrefix + "hdnFooterCoupon").value;

            var ItemsCostValueSummation = document.getElementById(footerid).innerText;
            var ItemsTaxValueSummation = document.getElementById(footerid_TaxValue).innerText;
            var ItemsCouponValueSummation = document.getElementById(footerid_Coupon).innerText;
            
            var ddlOSInd = document.getElementById(CtrlIdPrefix + "ddlOSIndicator");
            var OSIndicator = ddlOSInd.options[ddlOSInd.selectedIndex].value;

            var HeaderinvoiceValue = document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value;            
            var HeaderCouponValue = document.getElementById(CtrlIdPrefix + "txtCouponCharges").value;            
            var HeaderSGSTValue = document.getElementById(CtrlIdPrefix + "txtSGSTValue").value;
            var HeaderCGSTValue = document.getElementById(CtrlIdPrefix + "txtCGSTValue").value;            
            var HeaderIGSTValue = document.getElementById(CtrlIdPrefix + "txtIGSTValue").value;
            var HeaderUTGSTValue = document.getElementById(CtrlIdPrefix + "txtUTGSTValue").value;
            
            if (OSIndicator == "L") {
                if (document.getElementById(CtrlIdPrefix + "txtSGSTValue").style.display == "inline") {
                    tot_value = parseFloat(HeaderinvoiceValue) + parseFloat(HeaderSGSTValue) + parseFloat(HeaderCGSTValue);
                }
                
                if (document.getElementById(CtrlIdPrefix + "txtUTGSTValue").style.display == "inline") {
                    tot_value = parseFloat(HeaderinvoiceValue) + parseFloat(HeaderUTGSTValue) + parseFloat(HeaderCGSTValue);
                }
            }
            else {
                tot_value = parseFloat(HeaderinvoiceValue) + parseFloat(HeaderIGSTValue);
            }
            
            
            var freightamount, insurance, postalcharges, couponcharges;

            freightamount = document.getElementById(CtrlIdPrefix + "txtFrieghtAmount").value;
            insurance = document.getElementById(CtrlIdPrefix + "txtInsurance").value;
            postalcharges = document.getElementById(CtrlIdPrefix + "txtPostalCharges").value;
            couponcharges = document.getElementById(CtrlIdPrefix + "txtCouponCharges").value;

            if (freightamount == '' || freightamount == undefined)
                freightamount = 0;
            if (insurance == '' || insurance == undefined)
                insurance = 0;
            if (postalcharges == '' || postalcharges == undefined)
                postalcharges = 0;
            if (couponcharges == '' || couponcharges == undefined)
                couponcharges = 0;
            if (HeaderCouponValue == '' || HeaderCouponValue == undefined)
                HeaderCouponValue = 0;

//            if ((parseFloat(freightamount) < 0)) {
//                freightamount = parseFloat(freightamount) * (-1);
//            }
//            if ((parseFloat(insurance) < 0)) {
//                insurance = parseFloat(insurance) * (-1);
//            }
//            if ((parseFloat(postalcharges) < 0)) {
//                postalcharges = parseFloat(postalcharges) * (-1);
//            }
//            if ((parseFloat(couponcharges) < 0)) {
//                couponcharges = parseFloat(couponcharges) * (-1);
//            }
            
            var ScreenMode = document.getElementById(CtrlIdPrefix + "hdnScreenMode");
            
            tot_value = parseFloat(tot_value) - parseFloat(HeaderCouponValue);  //+ parseFloat(freightamount) + parseFloat(insurance) + parseFloat(postalcharges) + parseFloat(couponcharges);

            insurance_Value = parseFloat(insurance) / parseFloat(HeaderinvoiceValue);

            disc_Value = (parseFloat(freightamount) + parseFloat(couponcharges) + parseFloat(postalcharges)) / parseFloat(ItemsCostValueSummation);
            
            if ((parseFloat(ItemsCouponValueSummation)-parseFloat(HeaderCouponValue)) != 0){
                alert("Header Coupon Value Doesn't Tally with the Grid Coupon Value. Please Check Once");                        
                return false;
            }
                 
            var differenceAmount = parseFloat(tot_value) - (((parseFloat(ItemsCostValueSummation) * (1 - (insurance_Value))) * (1 - disc_Value)) + ((parseFloat(ItemsTaxValueSummation) * (1 - (insurance_Value)))));

            if ((parseFloat(differenceAmount) < 0)) {
                differenceAmount = parseFloat(differenceAmount) * (-1);
            }
            
            //if (TransType != '171') {                                
                if ((parseFloat(differenceAmount) > 10)) {
                    var msg = 'Summation of Invoice value and Tax Value does not match with the Summation of Items cost price and Items Tax values.\n Click Ok to Save the record with Status as (E)\n Excess Value ' + parseFloat(differenceAmount).toFixed(4);
                    ScreenMode.value = "E";
                    
                    if (confirm(msg)){                            
                            return true;                            
                    }
                    else
                        return false;
 
                    return false;
                }
                else
                    ScreenMode.value = "A";
//            }
//            else
//                ScreenMode.value = "A";

            return true; //post back gets happen for save/update.
        }
        else
            return false;
    }
    else
        return false;
}

function funReceivedQtyValidation(ActQtyId, BalQtyID, CostPriceID, CostPricePerQty, existingQty) {

    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

    var ActQtyId1 = document.getElementById(ActQtyId);
    var BalQtyID1 = document.getElementById(BalQtyID);
    var CostPrice1 = document.getElementById(CostPriceID);
    var ExsQty1 = document.getElementById(existingQty);

    var TaxValueID = CostPriceID.split('_');
    var TaxValueID1 = TaxValueID[0] + "_" + TaxValueID[1] + "_" + TaxValueID[2] + "_" + TaxValueID[3] + "_txtItemTaxValue";
    var TaxPerID1 = TaxValueID[0] + "_" + TaxValueID[1] + "_" + TaxValueID[2] + "_" + TaxValueID[3] + "_txtTaxPercentage";
    var ItemCodeID1 = TaxValueID[0] + "_" + TaxValueID[1] + "_" + TaxValueID[2] + "_" + TaxValueID[3] + "_ddlItemCode";
    var ddlCCWHNo1 = TaxValueID[0] + "_" + TaxValueID[1] + "_" + TaxValueID[2] + "_" + TaxValueID[3] + "_ddlCCWHNo";
    //alert(TaxValueID1);
    //debugger;

    if (document.getElementById(CtrlIdPrefix + "hdnScreenMode").value == "A") {
        var ddlCCWHNo = document.getElementById(ddlCCWHNo1);
        var selCCWHNo = ddlCCWHNo.options[ddlCCWHNo.selectedIndex].value;

        if (selCCWHNo == "0" && (document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value == "0" || document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value == "0.00" || parseFloat(document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value) == 0))
            return true;

        var ddlItemCode = document.getElementById(ItemCodeID1);
        var selItemCode = ddlItemCode.options[ddlItemCode.selectedIndex].value;

        if (selItemCode == "0")
            return true;
    }

    if (document.getElementById(CtrlIdPrefix + "hdnScreenMode").value == "E") {
        var existingqty = parseFloat(ExsQty1.value.trim());
    }
    else {
        var existingqty = parseFloat(0);
    }

    if ((ActQtyId1 != null) && (ActQtyId1 != undefined) && (ActQtyId1.value != 0) && (ActQtyId1.value.trim() != "")) {
        if (parseFloat(ActQtyId1.value.trim()) > (parseFloat(BalQtyID1.value.trim()) + parseFloat(existingqty)) && (TransType != '171')) {
            alert('Quantity should not be greater than the Balance Quantity.');
            ActQtyId1.value = "0";
            ActQtyId1.focus();
            return false;
        }
        else {
            var cPrice = parseFloat(CostPricePerQty) * parseInt(ActQtyId1.value.trim());
            CostPrice1.value = cPrice.toFixed(4);

            var taxPercentage = document.getElementById(TaxPerID1).value;

            document.getElementById(TaxValueID1).value = (cPrice.toFixed(4) * (parseFloat(taxPercentage) / 100)).toFixed(4);
            GetTotal();
            return true;
        }
    }
    else {
        alert('Please enter valid Quantity information.');
        ActQtyId1.value = "";
        ActQtyId1.focus();
        return false;
    }
    
    return false;
}

function funCalculateTaxValue(ActQtyId, BalQtyID, CostPriceID, CostPricePerQty, existingQty) {
    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

    var ActQtyId1 = document.getElementById(ActQtyId);
    var BalQtyID1 = document.getElementById(BalQtyID);
    var CostPrice1 = document.getElementById(CostPriceID);
    var ExsQty1 = document.getElementById(existingQty);

    var TaxValueID = CostPriceID.split('_');
    var TaxValueID1 = TaxValueID[0] + "_" + TaxValueID[1] + "_" + TaxValueID[2] + "_" + TaxValueID[3] + "_txtItemTaxValue";
    var TaxPerID1 = TaxValueID[0] + "_" + TaxValueID[1] + "_" + TaxValueID[2] + "_" + TaxValueID[3] + "_txtTaxPercentage";
    var ItemCodeID1 = TaxValueID[0] + "_" + TaxValueID[1] + "_" + TaxValueID[2] + "_" + TaxValueID[3] + "_ddlItemCode";
    var ddlCCWHNo1 = TaxValueID[0] + "_" + TaxValueID[1] + "_" + TaxValueID[2] + "_" + TaxValueID[3] + "_ddlCCWHNo";
    var CouponID1 = TaxValueID[0] + "_" + TaxValueID[1] + "_" + TaxValueID[2] + "_" + TaxValueID[3] + "_txtItemCoupon";

    if (document.getElementById(CtrlIdPrefix + "hdnScreenMode").value == "A") {
        var ddlCCWHNo = document.getElementById(ddlCCWHNo1);
        var selCCWHNo = ddlCCWHNo.options[ddlCCWHNo.selectedIndex].value;

        if (selCCWHNo == "0" && (document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value == "0" || document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value == "0.00" || parseFloat(document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value) == 0))
            return true;

        var ddlItemCode = document.getElementById(ItemCodeID1);
        var selItemCode = ddlItemCode.options[ddlItemCode.selectedIndex].value;

        if (selItemCode == "0")
            return true;
    }

    if (document.getElementById(CtrlIdPrefix + "hdnScreenMode").value == "E") {
        var existingqty = parseFloat(ExsQty1.value.trim());
    }
    else {
        var existingqty = parseFloat(0);
    }

    if ((document.getElementById(TaxPerID1) != null) && (document.getElementById(TaxPerID1) != undefined) && (document.getElementById(TaxPerID1).value != 0) && (document.getElementById(TaxPerID1).value.trim() != "")) {
        var cPrice = (parseFloat(CostPricePerQty) * parseInt(ActQtyId1.value.trim())) - parseFloat(document.getElementById(CouponID1).value);
        //CostPrice1.value = cPrice.toFixed(4);
        var taxPercentage = document.getElementById(TaxPerID1).value;
        document.getElementById(TaxValueID1).value = (cPrice.toFixed(4) * (parseFloat(taxPercentage) / 100)).toFixed(4);
        GetTotalTaxValue();
        return true;
    }
    else {
        alert('Please enter valid Tax Percentage');
        document.getElementById(TaxPerID1).value = "";
        document.getElementById(TaxPerID1).focus();
        return false;
    }
    
    return false;    
}

function funCalculateCoupon(ActQtyId, BalQtyID, CouponID, CostPricePerQty, existingQty) {
    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

    var ActQtyId1 = document.getElementById(ActQtyId);
    var BalQtyID1 = document.getElementById(BalQtyID);
    var Coupon1 = document.getElementById(CouponID);
    var ExsQty1 = document.getElementById(existingQty);

    var GridRowID = CouponID.split('_');
    var CouponID1 = GridRowID[0] + "_" + GridRowID[1] + "_" + GridRowID[2] + "_" + GridRowID[3] + "_txtItemCoupon";
    var TaxValueID1 = GridRowID[0] + "_" + GridRowID[1] + "_" + GridRowID[2] + "_" + GridRowID[3] + "_txtItemTaxValue";
    var TaxPerID1 = GridRowID[0] + "_" + GridRowID[1] + "_" + GridRowID[2] + "_" + GridRowID[3] + "_txtTaxPercentage";
    var ItemCodeID1 = GridRowID[0] + "_" + GridRowID[1] + "_" + GridRowID[2] + "_" + GridRowID[3] + "_ddlItemCode";
    var ddlCCWHNo1 = GridRowID[0] + "_" + GridRowID[1] + "_" + GridRowID[2] + "_" + GridRowID[3] + "_ddlCCWHNo";

    if (document.getElementById(CtrlIdPrefix + "hdnScreenMode").value == "A") {
        var ddlCCWHNo = document.getElementById(ddlCCWHNo1);
        var selCCWHNo = ddlCCWHNo.options[ddlCCWHNo.selectedIndex].value;

        if (selCCWHNo == "0" && (document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value == "0" || document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value == "0.00" || parseFloat(document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value) == 0))
            return true;

        var ddlItemCode = document.getElementById(ItemCodeID1);
        var selItemCode = ddlItemCode.options[ddlItemCode.selectedIndex].value;

        if (selItemCode == "0")
            return true;
    }

    if (document.getElementById(CtrlIdPrefix + "hdnScreenMode").value == "E") {
        var existingqty = parseFloat(ExsQty1.value.trim());
    }
    else {
        var existingqty = parseFloat(0);
    }

    if ((document.getElementById(CouponID1) != null) && (document.getElementById(CouponID1) != undefined) && (document.getElementById(CouponID1).value != 0) && (document.getElementById(CouponID1).value.trim() != "")) {
        
        if ((document.getElementById(TaxPerID1) != null) && (document.getElementById(TaxPerID1) != undefined) && (document.getElementById(TaxPerID1).value != 0) && (document.getElementById(TaxPerID1).value.trim() != "")) {
        var cPrice = (parseFloat(CostPricePerQty) * parseInt(ActQtyId1.value.trim())) - parseFloat(document.getElementById(CouponID1).value);
        var taxPercentage = document.getElementById(TaxPerID1).value;
        document.getElementById(TaxValueID1).value = (cPrice.toFixed(4) * (parseFloat(taxPercentage) / 100)).toFixed(4);
        GetTotalTaxValue();
        GetTotalCouponValue();
        return true;
        }
        else {
            alert('Please enter valid Tax Percentage');
            document.getElementById(TaxPerID1).value = "";
            document.getElementById(TaxPerID1).focus();
            return false;
        }
                
        return true;
    }
    else {
        alert('Please enter valid Coupon');
        document.getElementById(CouponID1).value = "0";
        document.getElementById(CouponID1).focus();
        return false;
    }
    
    return false;    
}


function GridCheck() {
    GetTotal();

    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;

    var hdnTxtCtrl = document.getElementById(CtrlIdPrefix + "txtHdnGridCtrls");
    //alert(hdnTxtCtrl.value.trim());
    var ctrlArr = hdnTxtCtrl.value.trim().split(',');

    if (TransType != '171') {
        for (var i = 0; i <= ctrlArr.length - 1; i++) {
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

        var gItemsDtl = document.getElementById(CtrlIdPrefix + "grvItemDetails");
        if (gItemsDtl != null) {
            var rowscount = gItemsDtl.rows.length;
            if (rowscount >= 3) {
                for (k = 1; k < gItemsDtl.rows.length - 1; k++) {
                    var row = gItemsDtl.rows[k];
                    txtSno = row.cells[1].children[0];

                    if (txtSno != null) {
                        txtRecQty = row.cells[4].children[0];
                        txtBalQty = row.cells[5].children[0];
                        txtQty = row.cells[6].children[0];
                        txtItemCoupon = row.cells[10].children[0];
                        txtTaxPer = row.cells[13].children[0];
                        txtTaxVal = row.cells[14].children[0];

                        if (txtQty.id != null && txtQty.id != "") {
                            if (txtQty.value == "0" || txtQty.value == "0.00" || txtQty.value == "") {
                                alert("Please enter quantity in row " + k);
                                document.getElementById(txtQty.id).focus();
                                return false;
                            }
                        }
                        if (hdnScreenMode.value == "A") {
                            if (txtBalQty.id != null && txtBalQty.id != "") {
                                if (parseInt(txtQty.value) > parseInt(txtBalQty.value)) {
                                    alert("Quantity Should not exceed " + txtBalQty.value + " in row " + k);
                                    txtQty.value = "";
                                    document.getElementById(txtQty.id).focus();
                                    return false;
                                }
                            }
                        }
                        else {
                            if (txtBalQty.id != null && txtBalQty.id != "") {
                                if (parseInt(txtQty.value) > (parseInt(txtBalQty.value) + parseInt(txtRecQty.value))) {
                                    alert("Quantity Should not exceed " + parseInt(txtBalQty.value) + parseInt(txtRecQty.value) + " in row " + k);
                                    txtQty.value = "";
                                    document.getElementById(txtQty.id).focus();
                                    return false;
                                }
                            }
                        }
                        
                        if (txtItemCoupon.id != null && txtItemCoupon.id != "") {
                            if (txtItemCoupon.value == "" || isNaN(txtItemCoupon.value)) {
                                document.getElementById(txtItemCoupon.id).value = "0";
                                return false;
                            }
                        }

                        if (txtTaxPer.id != null && txtTaxPer.id != "") {
                            if (txtTaxPer.value == "0" || txtTaxPer.value == "0.00" || txtTaxPer.value == "" || isNaN(txtTaxVal.value)) {
                                alert("Please enter Tax Percentage in row " + k);
                                document.getElementById(txtTaxPer.id).focus();
                                return false;
                            }
                        }

                        if (txtTaxVal.id != null && txtTaxVal.id != "") {
                            if (txtTaxVal.value == "0" || txtTaxVal.value == "0.00" || txtTaxVal.value == "" || isNaN(txtTaxVal.value)) {
                                alert("Please enter Tax Percentage in row " + k);
                                document.getElementById(txtTaxPer.id).focus();
                                return false;
                            }
                        }
                    }
                }
            }
        }
    }
    else //FREE OF COST
    {
        for (var i = 0; i <= ctrlArr.length - 1; i++) {
            if (ctrlArr[i].trim() != '') {
                //alert(ctrlArr[i]);
                //alert(ctrlArr[i].indexOf('ddlCCWHNo'));
                if (ctrlArr[i].indexOf('ddlCCWHNo') > 1)
                    continue;

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
    }
    
    return true;
}

/*
function ChkDuplicateItems(ddlItemCodeCtrlID)
{
alert('asd');
//alert(ddlItemCodeCtrlID);
var ddlItem = document.getElementById(ddlItemCodeCtrlID);
//alert(ddlItem);
var ddlItemVal = ddlItem.options[ddlItem.selectedIndex].value;

var arr = selectedItemCode.split('|');
for (var index = 0; index < arr.length; index++) 
{
if (arr[index] == ddlItemVal) 
{
alert('The selected item already exists.Please select a different Supplier Part #.');
ddlItem.options[ddlItem.selectedIndex].value = "0";
ddlItem.focus();
return false;
}
}

selectedItemCode = selectedItemCode + "|" + ddlItemVal;
alert(selectedItemCode);
return true;
}*/

function GetTotal() {
    totalTaxValue = 0;
    totalCostPrice = 0;
    totalCoupon = 0;

    // Get the gridview
    var grid = document.getElementById("ctl00_CPHDetails_grvItemDetails");

    // Get all the input controls (can be any DOM element you would like)
    var inputs = grid.getElementsByTagName("input");

    // Loop through all the DOM elements we grabbed
    for (var i = 0; i < inputs.length; i++) {

        // In this case we are looping through all the Dek Volume and then the Mcf volume boxes in the grid and not an individual one and totalling them
        if (inputs[i].name.indexOf("txtItemTaxValue") > 1) {
            if (inputs[i].value != "") {
                totalTaxValue = parseFloat(totalTaxValue) + parseFloat(inputs[i].value);
            }
        }

        if (inputs[i].name.indexOf("txtCostPrice") > 1) {
            if (inputs[i].value != "") {
                totalCostPrice = parseFloat(totalCostPrice) + parseFloat(inputs[i].value);
            }
        }
        
        if (inputs[i].name.indexOf("txtItemCoupon") > 1) {
            if (inputs[i].value != "") {
                totalCoupon = parseFloat(totalCoupon) + parseFloat(inputs[i].value);
            }
        }
    }

    var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
    var footerid_TaxValue = document.getElementById(CtrlIdPrefix + "hdnFooterTaxPrice").value;
    var footerid_Coupon = document.getElementById(CtrlIdPrefix + "hdnFooterCoupon").value;

    document.getElementById(footerid).innerText = totalCostPrice.toFixed(4);
    document.getElementById(footerid_TaxValue).innerText = totalTaxValue.toFixed(4);
    document.getElementById(footerid_Coupon).innerText = totalCoupon.toFixed(4);
}

function GetTotalTaxValue() {
    totalTaxValue = 0;
    totalCostPrice = 0;

    // Get the gridview
    var grid = document.getElementById("ctl00_CPHDetails_grvItemDetails");

    // Get all the input controls (can be any DOM element you would like)
    var inputs = grid.getElementsByTagName("input");

    // Loop through all the DOM elements we grabbed
    for (var i = 0; i < inputs.length; i++) {

        // In this case we are looping through all the Dek Volume and then the Mcf volume boxes in the grid and not an individual one and totalling them
        if (inputs[i].name.indexOf("txtItemTaxValue") > 1) {
            if (inputs[i].value != "") {
                totalTaxValue = parseFloat(totalTaxValue) + parseFloat(inputs[i].value);
            }
        }
    }

    var footerid_TaxValue = document.getElementById(CtrlIdPrefix + "hdnFooterTaxPrice").value;
    document.getElementById(footerid_TaxValue).innerText = totalTaxValue.toFixed(4);
}

function GetTotalCouponValue() {
    totalCoupon = 0;

    // Get the gridview
    var grid = document.getElementById("ctl00_CPHDetails_grvItemDetails");

    // Get all the input controls (can be any DOM element you would like)
    var inputs = grid.getElementsByTagName("input");

    // Loop through all the DOM elements we grabbed
    for (var i = 0; i < inputs.length; i++) {

        // In this case we are looping through all the Dek Volume and then the Mcf volume boxes in the grid and not an individual one and totalling them
        if (inputs[i].name.indexOf("txtItemCoupon") > 1) {
            if (inputs[i].value != "") {
                totalCoupon = parseFloat(totalCoupon) + parseFloat(inputs[i].value);
            }
        }
    }

    var footerid_Coupon = document.getElementById(CtrlIdPrefix + "hdnFooterCoupon").value;
    document.getElementById(footerid_Coupon).innerText = totalCoupon.toFixed(4);
}


function funGrnSubmit(s) {
    if (s != "M") {
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
        var InvoiceNo = document.getElementById(CtrlIdPrefix + "txtInvoiceNo");
        var InvoiceDate = document.getElementById(CtrlIdPrefix + "txtInvoiceDate");
        var ClearingAgentNo = document.getElementById(CtrlIdPrefix + "txtClearingAgentNo");
        var CheckPostName = document.getElementById(CtrlIdPrefix + "txtCheckPostName");
        var ClearenceDate = document.getElementById(CtrlIdPrefix + "txtClearenceDate");
        var ClearenceAmount = document.getElementById(CtrlIdPrefix + "txtClearenceAmount");

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

        if (InvoiceNo.value.trim() == "") {
            alert("Invoice Number should not be empty.");
            InvoiceNo.focus();
            return false;
        }

        if (InvoiceDate.value.trim() == "") {
            alert("Invoice Date should not be empty.");
            InvoiceDate.focus();
            return false;
        }

        if (ClearingAgentNo.value.trim() == "") {
            alert("Clearing Agent No. should not be empty.");
            ClearingAgentNo.value = "";
            ClearingAgentNo.focus();
            return false;
        }

        if (!CheckGRNGridDuringUpdate()) {
            return false;
        }

        var Status = "Are you Sure with the Correctness of Invoice Number and Date?";

        if (confirm(Status))
            return true;
        else {
            InvoiceNo.focus();
            return false;
        }
    }
    else {
        var Status = "Are you Sure to Approve the GRN Entry";

        if (confirm(Status))
            return true;
        else
            return false;
    }
}

function funGrnReject() {
    var Remarks = prompt("Are you Sure to Reject the GRN Entry? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
}

function funWareHouseGrnSubmit() {
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

    var localPurchaseTax = document.getElementById(CtrlIdPrefix + "txtLocalPurchaseTax");

    var Remarks = document.getElementById(CtrlIdPrefix + "txtRemarks");
    var InvoiceNo = document.getElementById(CtrlIdPrefix + "txtInvoiceNo");
    var InvoiceDate = document.getElementById(CtrlIdPrefix + "txtInvoiceDate");
    var ClearingAgentNo = document.getElementById(CtrlIdPrefix + "txtClearingAgentNo");

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

    if (ClearingAgentNo.value.trim() == "") {
        alert("Clearing Agent No. should not be empty.");
        ClearingAgentNo.value = "";
        ClearingAgentNo.focus();
        return false;
    }

    if (!CheckGRNGridDuringUpdate()) {
        return false;
    }
}

function CheckGRNGridDuringUpdate() {
    var hdnTxtCtrl = document.getElementById(CtrlIdPrefix + "txtHdnGridCtrls").value;

    if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value.trim() == "0") {
        alert('No item details are avialable');
        return false;
    }

    var ctrlArrMain = hdnTxtCtrl.split('|');
    for (var row = 0; row <= ctrlArrMain.length - 1; row++) {
        if (ctrlArrMain[row].trim() == '')
            continue;

        var ctrlArr = ctrlArrMain[row].trim().split(',');

        if ((document.getElementById(ctrlArr[0]) != null) && (document.getElementById(ctrlArr[0]) != undefined)) {
            if (document.getElementById(ctrlArr[0]).value.trim() == "") {
                alert("Accepted Quantity should not be empty.");
                document.getElementById(ctrlArr[0]).focus();
                return false;
            }
        }

        if ((document.getElementById(ctrlArr[1]) != null) && (document.getElementById(ctrlArr[1]) != undefined)) {
            if ((parseInt(document.getElementById(ctrlArr[1]).value)) > 0) {
                if (document.getElementById(ctrlArr[2]).value.trim() == "") {
                    alert('Warehouse No. should not be empty.');
                    document.getElementById(ctrlArr[2]).focus();
                    return false;
                }
                if (document.getElementById(ctrlArr[3]).value.trim() == "") {
                    alert('Warehouse date should not be empty.');
                    document.getElementById(ctrlArr[3]).focus();
                    return false;
                }
                if (document.getElementById(ctrlArr[4]).value.trim() == "") {
                    alert('Item Location should not be empty.');
                    document.getElementById(ctrlArr[4]).focus();
                    return false;
                }
            }
        }
    }
    
    return true;
}

function funGRNAcceptedQtyValidation(InwardQtyID, AcceptedQtyID, ShoratageQtyID) {

    var InwardQty = document.getElementById(InwardQtyID);
    var AcceptedQty = document.getElementById(AcceptedQtyID);
    var ShortageQty = document.getElementById(ShoratageQtyID);

    if ((InwardQty != null) && (InwardQty != undefined) && (InwardQty.value != 0) && (InwardQty.value.trim() != "")) {
        if ((AcceptedQty != null) && (AcceptedQty != undefined)) {
            if (parseFloat(AcceptedQty.value.trim()) > parseFloat(InwardQty.value.trim())) {
                alert('Accepted Quantity should not be greater than the Inward Quantity.');
                AcceptedQty.value = "0";
                AcceptedQty.focus();
                return false;
            }
            else {
                ShortageQty.value = parseFloat(InwardQty.value.trim()) - parseFloat(AcceptedQty.value.trim());
                var strid = ShoratageQtyID.split("_");
                if (parseFloat(ShortageQty.value) > 0) {
                    document.getElementById("ctl00_CPHDetails_txtWareHouseNo").disabled = false;
                    document.getElementById("ctl00_CPHDetails_txtWarehouseDate").disabled = false;
                }
                else {
                    document.getElementById("ctl00_CPHDetails_txtWareHouseNo").value = "";
                    document.getElementById("ctl00_CPHDetails_txtWarehouseDate").value = "";

                    document.getElementById("ctl00_CPHDetails_txtWareHouseNo").disabled = true;
                    document.getElementById("ctl00_CPHDetails_txtWarehouseDate").disabled = true;
                }

                return true;
            }
        }
        else
            return false;
    }
    else {

        alert('Please enter valid Accepted Quantity.');
        ShortageQty.value = "0";
        AcceptedQty.focus();
        return false;
    }
    return false;
}

function funGrnReset() {
    if (confirm("Are you sure you want reset the page?"))
        return true;
    else
        return false;
}


function CheckForValidPeriod(id, value) {

    var val = document.getElementById(id).value;

    if (parseInt(val) > 100) {
        alert(value + " should be between 0 to 100");
        document.getElementById(id).value = "";
        document.getElementById(id).focus();
        return false;
    }
}

function CheckForValidWeek(id, value) {

    var val = document.getElementById(id).value;

    if (parseInt(val) > 100) {
        alert(value + " should be between 0 to 52");
        document.getElementById(id).focus();
        return false;
    }
}

function CheckForValidYear(id, value) {

    var val = document.getElementById(id).value;

    if (parseInt(val) > 365) {
        alert(value + " should be between 0 to 365");
        document.getElementById(id).focus();
        return false;
    }
}

function CheckForNumberRange(id, value, Range) {

    var val = document.getElementById(id).value;

    if (parseInt(val) > parseInt(Range)) {
        alert(value + " should be Greater than " + Range);
        document.getElementById(id).focus();
        return false;
    }
}

function checkDateForInwardDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var idInwardDate = document.getElementById("ctl00_CPHDetails_txtInwardDate").value

            var toDate = new Date();
            toDate = convertDate(idDate);

            var frmDate = new Date();
            frmDate = convertDate(idInwardDate);

            if (toDate < frmDate) {
                document.getElementById(id).value = "";
                alert("Warehouse Date should be greater than or equal to Inward Date");
            }
        }
    }
}

function checkDateForValidDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {            

        }
    }
}

function checkDateToDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            
            var idFromDate = document.getElementById("ctl00_CPHDetails_txtfromdate").value

            var toDate = new Date();
            toDate = convertDate(idDate);

            var frmDate = new Date();
            frmDate = convertDate(idFromDate);

            if (!(toDate.getMonth() == frmDate.getMonth())) {
                document.getElementById(id).value = "";
                alert("Date Range should be with in the same month");
            }
            else if (toDate < frmDate) {
                document.getElementById(id).value = "";
                alert("To Date should be greater than or equal to From Date");
            }
        }
    }
}