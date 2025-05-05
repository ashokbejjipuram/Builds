var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grdChart_ctl02_";
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

function convertDate(id) {
    var date = id.split("/");
    var day = parseInt(date[0]);
    var month = parseInt(date[1] - 1);
    var year = parseInt(date[2]);

    var validationDate = new Date(year, month, day);

    return validationDate;
}

function CheckTransDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {
            var FutureDate = new Date();

            IdDate = convertDate(idDate);

            if (IdDate > FutureDate) {
                document.getElementById(id).value = "";
                alert("Future date can not be Entered.");
            }
        }
    }
}

function checkDateForRefDocDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != "") {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {
            var idInvoiceDate = document.getElementById("ctl00_CPHDetails_txtReferenceDocumentDate").value

            var FutureDate = new Date();

            var InvDate = new Date();
            InvDate = convertDate(idInvoiceDate);

            if (InvDate > FutureDate) {
                document.getElementById(id).value = "";
                alert("Future date can not be Entered.");
            }
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

function TriggerCalender(CalenderCtrlClientId) {
    var CtrlID = CtrlIdPrefix + CalenderCtrlClientId;
    document.getElementById(CtrlID).click();
}

function TriggerGridCalender(GridCalenderCtrlClientId) {
    var CtrlID = GridCalenderCtrlClientId;
    document.getElementById(CtrlID).click();
}

function CurrencyChkForMoreThanOneDot(strValue) {
    if (strValue.split(".").length > 2)
        return false;
    else
        return true;
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
function AlphaNumericOnlyWithSpace() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 32 || AsciiValue == 127 || AsciiValue == 46) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
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
        alert("Future date can not be selected.");
        var dtDate = new Date();
        dtDate = dtDate.format(sender._format);
        sender._textbox.set_Value(dtDate);
        return false;
    }

    if (sender.get_id() == "ctl00_CPHDetails_CalendarExtender3") {
        if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtFromDate").value, document.getElementById(CtrlIdPrefix + "txtToDate").value)) {
            alert("From Date is greater than the To Date.");
            var dtDate = new Date();
            dtDate = dtDate.format(sender._format);
            sender._textbox.set_Value(dtDate);
            return false;

        }
    }

    else if (sender.get_id() == "ctl00_CPHDetails_CalendarExtender4") {
        if (IsHigherDate(document.getElementById(CtrlIdPrefix + "txtFromDate").value, document.getElementById(CtrlIdPrefix + "txtToDate").value)) {
            alert("From Date is greater than the To Date.");
            var dtDate = new Date();
            dtDate = dtDate.format(sender._format);
            sender._textbox.set_Value(dtDate);
            return false;

        }
    }
    return true;
}

function IsHigherDate(Date1, Date2) {
    var strDateArr1 = Date1.split("/");
    var strDateArr2 = Date2.split("/");

    var dtDate1 = new Date();
    var dtDate2 = new Date();
    dtDate1.setFullYear(strDateArr1[2], strDateArr1[1] - 1, strDateArr1[0]);
    dtDate2.setFullYear(strDateArr2[2], strDateArr2[1] - 1, strDateArr2[0]);

    if (dtDate1 > dtDate2)
        return true;
    else
        return false;

}

function funReset() {
    return true;
}

function validatespl(inpval, fldname) {
    var firstchr = inpval.substring(0, 1, inpval);
    if (firstchr == "") {
        alert("First character of " + fldname + " should not be blank");
        return true;
    }
    else if (isspecialchar(firstchr)) {
        alert("First character of " + fldname + " should be alphabet or number");
        return true;
    }

    for (i = 0; i < inpval.length; i++) {
        firstchr = inpval.substring(i, i + 1, inpval);
        if (firstchr == "") {
            alert("First character of " + fldname + " should not be blank");
            return true;
        }
        else if (isspecial1(firstchr)) {
            alert("Characters in " + fldname + " should be alphabet or number");
            return true;
        }
    }

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

        if (currenyValue.value.split(".").length > 2) {
            document.getElementById(e1).value = Isvalid;
        }
        else {
            document.getElementById(e1).value = currenyValue.value;
        }
    }

    return true;
}

function getDaysBetweenDates(date) {

    var str1 = date.split("/");
    var date1 = new Date(str1[2], str1[1] - 1, str1[0]);
    var date2 = new Date();
    date1.setHours(0);
    date1.setMinutes(0, 0, 0);
    date2.setHours(0);
    date2.setMinutes(0, 0, 0);
    var datediff = Math.abs(date1.getTime() - date2.getTime()); // difference
    return parseInt(datediff / (24 * 60 * 60 * 1000));
}

function ValidateDebitCreditFields() {
    //            if (validate(document.getElementById(CtrlIdPrefix + "txtDocumentNumber").value, "Document Number ")) {
    //                document.getElementById(CtrlIdPrefix + "txtDocumentNumber").focus()
    //                return false;
    //            }
    //            else if (parseInt(document.getElementById(CtrlIdPrefix + "txtDocumentNumber").value.length) != 5) {
    //                alert("Document Number should be 5 digit")
    //                document.getElementById(CtrlIdPrefix + "txtDocumentNumber").focus()
    //                return false;

    //            }
    if (validate(document.getElementById(CtrlIdPrefix + "ddlAccountPeriod").value, "Accounting Period ")) {
        document.getElementById(CtrlIdPrefix + "ddlAccountPeriod").focus()
        return false;
    }

    if (validate(document.getElementById(CtrlIdPrefix + "txtDocumentDate").value, "Document Date ")) {
        document.getElementById(CtrlIdPrefix + "txtDocumentDate").focus()
        return false;
    }

    if (validate(document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value, "DebitCreditNote ")) {
        document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").focus()
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "ddlSupplierName").value, "Supplier ")) {
        document.getElementById(CtrlIdPrefix + "ddlSupplierName").focus()
        return false;
    }

    if (validate(document.getElementById(CtrlIdPrefix + "ddlSupplyPlant").value, "Supply Plant ")) {
        document.getElementById(CtrlIdPrefix + "ddlSupplyPlant").focus()
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "ddlTransactionType").value, "Transaction Type ")) {
        document.getElementById(CtrlIdPrefix + "ddlTransactionType").focus()
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value, "Reference Document Number ")) {
        document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").focus()
        return false;
    }
    
    if (validatespl(document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").value, "Reference Document Number ")) {
        document.getElementById(CtrlIdPrefix + "txtReferenceDocNumber").focus();
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").value, "Reference Document Date ")) {
        document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").focus()
        return false;
    }
    
    if (doValidateDate(document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate"))) {
        document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").focus();
        return false;
    }
    
    if (CompareDate(document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").value, document.getElementById(CtrlIdPrefix + "txtDocumentDate").value) == 1) {
        alert("Reference Document Date is greater than Document Date");
        document.getElementById(CtrlIdPrefix + "txtRefDocumnetDate").focus();
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "txtValue").value, "TurnOver Value ")) {
        document.getElementById(CtrlIdPrefix + "txtValue").focus();
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "txtGSTValue").value, "GST Value ")) {
        document.getElementById(CtrlIdPrefix + "txtGSTValue").focus();
        return false;
    }
    
    if (validate(document.getElementById(CtrlIdPrefix + "txtRemarks").value, "Remarks ")) {
        document.getElementById(CtrlIdPrefix + "txtRemarks").focus();
        return false;
    }
    
    if (validatespl(document.getElementById(CtrlIdPrefix + "txtRemarks").value, "Remarks")) {
        document.getElementById(CtrlIdPrefix + "txtRemarks").focus();
        return false;
    }

    if (document.getElementById(CtrlIdPrefix + "ddlTransactionType").value == "765") {
        if (validate(document.getElementById(CtrlIdPrefix + "txtNoTransaction").value, "No Of Transaction")) {
            document.getElementById(CtrlIdPrefix + "txtNoTransaction").focus();
            return false;
        }
    }
}

function CalculateAmount() {
    if (checkAmount(1) == false)
        return false;

    var txttrans = document.getElementById(CtrlIdPrefix + "txtNoTransaction")
    var rowscount = document.getElementById(CtrlIdPrefix + "grdChart").rows.length - 2;
    if (txttrans.value == rowscount) {
        alert("Number of Transcation Exceeds");
        return false;
    }

}

function checkAmount(Ind) {
    var txtAmount = document.getElementById(CtrlIdPrefix + "txtValue");
    var txtGSTAmount = document.getElementById(CtrlIdPrefix + "txtGSTValue");
    var ObjGrid = null;
    var inputElements = null;
    var Type = document.getElementById(CtrlIdPrefix + "hdnTransType").value;
    var ddlTransactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType").value;
    var txtBranch = document.getElementById(CtrlIdPrefix + "txtBranchCode");
    var ddlDebitCreditNote = document.getElementById(CtrlIdPrefix + "ddlDebitCreditNote").value;
    var OsLsIndicator = document.getElementById(CtrlIdPrefix + "hdnOsLsIndicator").value;
    var ChartAccount = null;
    var Value = null;
    var Remarks = null;
    var SGSTCode = null;
    var SGSTPer = null;
    var SGSTAmt = null;
    var CGSTCode = null;
    var CGSTPer = null;
    var CGSTAmt = null;
    var IGSTCode = null;
    var IGSTPer = null;
    var IGSTAmt = null;
    var UTGSTCode = null;
    var UTGSTPer = null;
    var UTGSTAmt = null;

    var CollectAmount = "0.00";
    ObjGrid = document.getElementById(CtrlIdPrefix + "grdCA")

    if (Type != null) {
        if (Type == "1") {

            for (var x = 1; x < ObjGrid.rows.length - 1; x++) {

                var row = ObjGrid.rows[x]
                ChartAccount = row.cells[1].children[0];
                Value = row.cells[2].children[0];
                Remarks = row.cells[3].children[0];

                if (txtBranch.value.toUpperCase() == "CHANDIGARH") {
                    UTGSTCode = row.cells[4].children[0];
                    UTGSTPer = row.cells[5].children[0];
                    UTGSTAmt = row.cells[6].children[0];
                }
                else {
                    SGSTCode = row.cells[4].children[0];
                    SGSTPer = row.cells[5].children[0];
                    SGSTAmt = row.cells[6].children[0];
                }

                CGSTCode = row.cells[7].children[0];
                CGSTPer = row.cells[8].children[0];
                CGSTAmt = row.cells[9].children[0];
                IGSTCode = row.cells[10].children[0];
                IGSTPer = row.cells[11].children[0];
                IGSTAmt = row.cells[12].children[0];

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

                if (parseFloat(txtGSTAmount.value) > 0) {
                    if (OsLsIndicator == "L") {
                        if (txtBranch.value.toUpperCase() == "CHANDIGARH") {
                            if ((UTGSTPer.value == "0" || UTGSTPer.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                                alert("UTGST % is required");
                                return false;
                            }

                            if ((UTGSTAmt.value == "0" || UTGSTAmt.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                                alert("UTGST Amt is required");
                                return false;
                            }
                        }
                        else {
                            if ((SGSTPer.value == "0" || SGSTPer.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                                alert("SGST % is required");
                                return false;
                            }

                            if ((SGSTAmt.value == "0" || SGSTAmt.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                                alert("SGST Amt is required");
                                return false;
                            }
                        }

                        if ((CGSTPer.value == "0" || CGSTPer.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                            alert("CGST % is required");
                            return false;
                        }

                        if ((CGSTAmt.value == "0" || CGSTAmt.value == "") && (IGSTPer.value == "0" || IGSTPer.value == "")) {
                            alert("CGST Amt is required");
                            return false;
                        }
                    }

                    if (OsLsIndicator == "O") {
                        if (txtBranch.value.toUpperCase() == "CHANDIGARH") {
                            if ((UTGSTPer.value == "0" || UTGSTPer.value == "") && (CGSTPer.value == "0" || CGSTPer.value == "")) {
                                if (IGSTPer.value == "0" || IGSTPer.value == "") {
                                    alert("IGST % is required");
                                    return false;
                                }

                                if (IGSTAmt.value == "0" || IGSTAmt.value == "") {
                                    alert("IGST Amt is required");
                                    return false;
                                }
                            }
                        }
                        else {
                            if ((SGSTPer.value == "0" || SGSTPer.value == "") && (CGSTPer.value == "0" || CGSTPer.value == "")) {
                                if (IGSTPer.value == "0" || IGSTPer.value == "") {
                                    alert("IGST % is required");
                                    return false;
                                }

                                if (IGSTAmt.value == "0" || IGSTAmt.value == "") {
                                    alert("IGST Amt is required");
                                    return false;
                                }
                            }
                        }
                    }
                }

                if (txtBranch.value.toUpperCase() == "CHANDIGARH") {
                    CollectAmount = Math.round((parseFloat(CollectAmount) + parseFloat(Value.value) + parseFloat(CGSTAmt.value) + parseFloat(IGSTAmt.value) + parseFloat(UTGSTAmt.value)) * 100) / 100;
                }
                else {
                    CollectAmount = Math.round((parseFloat(CollectAmount) + parseFloat(Value.value) + parseFloat(SGSTAmt.value) + parseFloat(CGSTAmt.value) + parseFloat(IGSTAmt.value)) * 100) / 100;
                }
            }

            var hdntxtRoundoff = Math.round(((parseFloat(txtAmount.value) + parseFloat(txtGSTAmount.value)) - parseFloat(CollectAmount)) * 10000) / 10000;

            if (ddlDebitCreditNote.toUpperCase() == "CA") {
                document.getElementById(CtrlIdPrefix + "hdnRoundoff").value = (-1) * hdntxtRoundoff;
            }
            else {
                document.getElementById(CtrlIdPrefix + "hdnRoundoff").value = hdntxtRoundoff;
            }

            if (Ind == 1) {
                if (parseFloat(hdntxtRoundoff) < -0.99) {
                    alert("Details Amount Exceeds the Header Amount and the Rounding off is allowed till 0.99 Paisa Only. Please Check");
                    return false;
                }
                else if (parseFloat(hdntxtRoundoff) == -0.99) {
                    alert("Details Amount is Tallied with the Header Amount for Allowed Rounding Off. Please Submit");
                    return false;
                }
            }
            else {
                if (parseFloat(hdntxtRoundoff) > 0.99 || parseFloat(hdntxtRoundoff) < -0.99) {
                    alert("Amount Differs. Please Check the Amount");
                    return false;
                }
            }
        }
    }
}