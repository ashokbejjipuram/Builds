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
        sender._textbox.set_Value("");
        return false;
    }
    return true;
}


function checkDate(sender, args) {
    var dtDate = new Date();
    dtDate.setDate(dtDate.getDate() - 90);
    //alert(dtDate);

    if (IsFutureDate(sender, args)) {

        if (!((sender._selectedDate >= dtDate) && (sender._selectedDate <= new Date()))) {
            alert('Date should be with in 90 days range from today');
            //sender._textbox.set_Value(dtDate.format(sender._format));
            sender._textbox.set_Value("");
        }
    }
}

function funReset() {
    if (confirm("You will loose the unsaved informations.\n\nAre you sure you want reset the page?"))
        return true;
    else
        return false;
}

function funBtnAddRow() {
    //alert('asda');
    if (FYRcvInvoiceHeaderValidation()) {
        //document.getElementById(CtrlIdPrefix + 'ddlTransactionType').disabled = true;
        //document.getElementById(CtrlIdPrefix + 'ddlSupplierName').disabled = true;
        //document.getElementById(CtrlIdPrefix + 'txtInvoiceValue').disabled = true;
        if (!GridCheck())
            return false;

        return true;
    }
    else
        return false;
}

function FYRcvInvoiceHeaderValidation() 
{    
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var invoiceDate = document.getElementById(CtrlIdPrefix + "txtInvoiceDate");

    var ddlAccountingPeriod = document.getElementById(CtrlIdPrefix + "ddlAccountingPeriod");
    var AccountingPeriod = ddlAccountingPeriod.options[ddlAccountingPeriod.selectedIndex].value;

    var ddlCustomer = document.getElementById(CtrlIdPrefix + "ddlCustomer");
    var Customer = ddlCustomer.options[ddlCustomer.selectedIndex].value;

    if (Branch == "0") {
        alert('Please select a Branch.');
        ddlBrnch.focus();
        return false;
    }

    if (invoiceDate.value.trim() == "") {
        alert("Invoice Date should not be empty.");
        invoiceDate.value = "";
        invoiceDate.focus();
        return false;
    }

    if (AccountingPeriod == "0") {
        alert('Please select an Accounting Period.');
        ddlAccountingPeriod.focus();
        return false;
    }

    if (Customer == "0") {
        alert('Please select a Customer.');
        ddlCustomer.focus();
        return false;
    }
    return true;
}

function funFYRcvInvoiceSubmit() 
{
    if (FYRcvInvoiceHeaderValidation()) 
    {
        if (document.getElementById(CtrlIdPrefix + "hdnRowCnt").value == "0") {
            alert('No Item details are found.');
            return false;
        }

        if (GridCheck()) {
            //Check the footer summation is equal to the invoice value.    
            var footerid = document.getElementById(CtrlIdPrefix + "hdnFooterCostPrice").value;
            var ItemSummation = document.getElementById(footerid).innerText;
            var invoiceValue = document.getElementById(CtrlIdPrefix + "txtInvoiceValue").value;
            //alert(footerid);
            //alert(document.getElementById(footerid).innerText);
            var differenceAmount = parseFloat(invoiceValue) - parseFloat(ItemSummation);
            //alert(parseFloat(differenceAmount));
            //alert(Math.abs(differenceAmount));            

            if ((Math.abs(differenceAmount) > 50)) {
                alert('Invoice value and Items values difference exceeds the allowable limit.');
                document.getElementById(CtrlIdPrefix + 'btnCancel').click();
                return false;
            }

            return true; //post back gets happen for save/update.
        }
        else
            return false;
    }
    else
        return false;
}


function GridCheck()
{
    //GetTotal();
    var hdnTxtCtrl = document.getElementById(CtrlIdPrefix + "txtHdnGridCtrls");
    //alert(hdnTxtCtrl.value.trim());
    var ctrlArr = hdnTxtCtrl.value.trim().split(',');
    
    for (var i = 0; i <= ctrlArr.length - 1; i++) 
        {
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
    return true;
}

function funQuantityValidation(UnitId, RateID, DiscountID, QtyID, NetPriceID) 
{
    /*alert(UnitId);
    alert(RateID);
    alert(DiscountID);
    alert(QtyID);
    alert(NetPriceID);*/     

    var RateID1 = document.getElementById(RateID);
    var DiscountID1 = document.getElementById(DiscountID);
    var QtyID1 = document.getElementById(QtyID);
    var NetPriceID1 = document.getElementById(NetPriceID);

    //alert(ActQtyId1);
    //alert(BalQtyID1);
    //alert(ActQtyId1.value.trim());
    //alert(BalQtyID1.value.trim());
    

    if ((QtyID1 != null) && (QtyID1 != undefined) && (QtyID1.value != 0) && (QtyID1.value.trim() != "")) 
    {
        if ((RateID1 != null) && (RateID1 != undefined) && (RateID1.value != 0) && (RateID1.value.trim() != ""))
        {
             var cPrice = parseFloat(RateID1.value) * parseInt(QtyID1.value.trim());
             NetPriceID1.value = cPrice.toFixed(2);
             GetTotal();
             return true;            
        }
        else 
        {
            alert('Please enter valid Rate.');
            RateID1.focus();
            return false;
        }
    }
    else {
        alert('Please enter valid Quantity information.');
        QtyID1.value = "0";
        QtyID1.focus();
        return false;
    }
    return true;
}

function GetTotal() 
{    
    totalCostPrice = 0;

    // Get the gridview
    var grid = document.getElementById("ctl00_CPHDetails_grvItemDetails");

    // Get all the input controls (can be any DOM element you would like)
    var inputs = grid.getElementsByTagName("input");

    // Loop through all the DOM elements we grabbed
    for (var i = 0; i < inputs.length; i++) 
    {
        if (inputs[i].name.indexOf("txtNetPrice") > 1) {
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

function funGetDocuments() 
{
    document.getElementById(CtrlIdPrefix + 'ddlBranch').disabled = true;
    document.getElementById(CtrlIdPrefix + 'ddlAccountingPeriod').disabled = true;
    document.getElementById(CtrlIdPrefix + 'ddlCustomer').disabled = true;
    document.getElementById(CtrlIdPrefix + 'ddlModeOfReceipt').disabled = true;
    document.getElementById(CtrlIdPrefix + 'txtAmount').disabled = true;    
    
}