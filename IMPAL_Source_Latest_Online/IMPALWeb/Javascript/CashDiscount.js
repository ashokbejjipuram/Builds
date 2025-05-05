var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvItemDetails_ctl02_";

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

function convertDate(id) {
    var date = id.split("/");
    var day = parseInt(date[0]);
    var month = parseInt(date[1] - 1);
    var year = parseInt(date[2]);

    var validationDate = new Date(year, month, day);

    return validationDate;
}

function ValidateCashDiscount() {
    if (validate(document.getElementById(CtrlIdPrefix + "ddlAccountPeriod").value, "Accounting Period ")) {
        document.getElementById(CtrlIdPrefix + "ddlAccountPeriod").focus()
        return false;
    }
    if (validate(document.getElementById(CtrlIdPrefix + "ddlSuppCust").value, "Customer ")) {
        document.getElementById(CtrlIdPrefix + "ddlSuppCust").focus()
        return false;
    }
    if (validate(document.getElementById(CtrlIdPrefix + "ddlCustomerInd").value, "Customer Indicator ")) {
        document.getElementById(CtrlIdPrefix + "ddlCustomerInd").focus()
        return false;
    }
    if (validate(document.getElementById(CtrlIdPrefix + "txtFromDate").value, "From date ")) {
        document.getElementById(CtrlIdPrefix + "txtFromDate").focus()
        return false;
    }
    if (doValidateDate(document.getElementById(CtrlIdPrefix + "txtFromDate"))) {
        document.getElementById(CtrlIdPrefix + "txtFromDate").focus();
        return false;
    }
    if (CompareDate(document.getElementById(CtrlIdPrefix + "txtFromDate").value, document.getElementById(CtrlIdPrefix + "hdnStartdate").value) == 0) {
        alert("From Date should be greater than Accounting Period Start Date");
        document.getElementById(CtrlIdPrefix + "txtFromDate").focus();
        return false;
    }
    if (CompareDate(document.getElementById(CtrlIdPrefix + "hdnEnddate").value, document.getElementById(CtrlIdPrefix + "txtFromDate").value) == 0) {
        alert("From Date should be less than Accounting Period End Date");
        document.getElementById(CtrlIdPrefix + "txtFromDate").focus();
        return false;
    }
    if (validate(document.getElementById(CtrlIdPrefix + "txtToDate").value, "To date ")) {
        document.getElementById(CtrlIdPrefix + "txtToDate").focus()
        return false;
    }
    if (doValidateDate(document.getElementById(CtrlIdPrefix + "txtToDate"))) {
        document.getElementById(CtrlIdPrefix + "txtToDate").focus();
        return false;
    }
    if (document.getElementById("txtFromDate").value != "" && document.getElementById("txtToDate").value != "")
        return Checkdate();
}

function Checkdate() {

    var Fromdate = document.getElementById(CtrlIdPrefix + "txtFromDate");
    var Todate = document.getElementById(CtrlIdPrefix + "txtToDate");
    return CheckFromTodates(Fromdate, Todate);
}

function LoadCDPer(lnk) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var Check = null;
    var DelayValue = null;
    var NoOfDays = null;
    Check = row.cells[0].children[0];
    NoOfDays = row.cells[5].children[0];
    DelayValue = row.cells[8].children[0];
    if (DelayValue.value == "") {
        alert("Delay Number Of Days Should Not be Empty");
        DelayValue.focus();
        DelayValue.value = "0";
        return false;
    }
    else if (!Check.checked) {
        alert("Please Select a Document");
        return false;
    }
    else if (parseInt(DelayValue.value) > parseInt(NoOfDays.value)) {
        alert("Delay in despatch should not exceed Collected days");
        DelayValue.focus();
        DelayValue.value = "0";
        return false;
    }
    else if (parseInt(DelayValue.value) > 30 || parseInt(DelayValue.value) < 0) {
        alert("Delay in despatch range should be in 0 to 30");
        DelayValue.focus();
        DelayValue.value = "0";
        return false;
    }
    else {
        SelectedChange(lnk);
    }
}

function SelectedChange(lnk) {
    var CustomerCode = document.getElementById(CtrlIdPrefix + "ddlSuppCust");
    var Indicator = document.getElementById(CtrlIdPrefix + "ddlCustomerInd");
    var CustStatus = document.getElementById(CtrlIdPrefix + "FormDetailCustomer_hdnCustStatus");
    var filePath = document.getElementById(CtrlIdPrefix + "hdnpath").value + "/LoadCashPer.ashx";
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var DocumentDate = null;
    var DelayValue = null;
    var CDVAlue = null;
    var CDPer = null;
    var OrderValue = null;
    var Check = null;
    var NoOfDays = null;
    Check = row.cells[0].children[0];
    DocumentDate = row.cells[2].children[0];
    OrderValue = row.cells[3].children[0];
    NoOfDays = row.cells[5].children[0];
    CDPer = row.cells[6].children[0];
    hdnCDPer = row.cells[6].children[1];
    CDVAlue = row.cells[7].children[0];
    hdnCDVAlue = row.cells[7].children[1];
    DelayValue = row.cells[8].children[0];
    CDVAlue.value = "0";
    if (Check.checked) {
        $.post(filePath + "?CustomerCode=" + CustomerCode.value + "&Indicator=" + Indicator.value + "&NoOfDays=" + NoOfDays.value + "&DelayValue=" + DelayValue.value + "&Status=" + CustStatus.value + "&DocumentDate=" + DocumentDate.value, function(result) {
            var streets = eval(result);
            ClearStreetsDropDown(CDPer.id);
            PopulateGlMain(streets, CDPer.id);
        });
    }
    else {
        ClearStreetsDropDown(CDPer.id);
        CDVAlue.value = "0";
        DelayValue.value = "0";
        hdnCDPer.value = "0";
        hdnCDVAlue.value = "0";
    }

    CalAdjustValue();
}

function SelectedChangeFinal(lnk) {
    var CustomerCode = document.getElementById(CtrlIdPrefix + "ddlSuppCust");
    var Indicator = document.getElementById(CtrlIdPrefix + "ddlCustomerInd");
    var CustStatus = document.getElementById(CtrlIdPrefix + "FormDetailCustomer_hdnCustStatus");
    var filePath = document.getElementById(CtrlIdPrefix + "hdnpath").value + "/LoadCashPer.ashx";    
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var DocumentDate = null;
    var DelayValue = null;
    var CDVAlue = null;
    var CDPer = null;
    var OrderValue = null;
    var Check = null;
    var NoOfDays = null;
    Check = row.cells[0].children[0];
    DocumentDate = row.cells[2].children[0];
    OrderValue = row.cells[3].children[0];
    NoOfDays = row.cells[5].children[0];
    CDPer = row.cells[6].children[0];
    CDPerTxt = row.cells[6].children[1];
    CDVAlue = row.cells[7].children[0];
    hdnCDVAlue = row.cells[7].children[1];
    DelayValue = row.cells[8].children[0];
    CDVAlue.value = "0";
    if (Check.checked) {
        CDPerTxt.style.display = "none";
        CDPer.style.display = "inline";
        row.cells[0].children[1].value = "1";
        $.post(filePath + "?CustomerCode=" + CustomerCode.value + "&Indicator=" + Indicator.value + "&NoOfDays=" + NoOfDays.value + "&DelayValue=" + DelayValue.value + "&Status=" + CustStatus.value + "&DocumentDate=" + DocumentDate.value, function(result) {
            var streets = eval(result);
            ClearStreetsDropDown(CDPer.id);
            PopulateGlMain(streets, CDPer.id);
        });
    }
    else {
        CDPerTxt.style.display = "none";
        CDPer.style.display = "inline";
        ClearStreetsDropDown(CDPer.id);
        CDVAlue.value = "0";
        DelayValue.value = "0";
        hdnCDVAlue.value = "0";
    }

    CalAdjustValue();
}

function CalculateCD(lnk) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var Check = row.cells[0].children[0];
    var OrderValue = row.cells[3].children[0];
    var CDPer = row.cells[6].children[0];
    var hdnCDPer = row.cells[6].children[1];
    var CDVAlue = row.cells[7].children[0];
    var hdnCDValue = row.cells[7].children[1];

    if (Check.checked) {
        CDVAlue.value = round((parseFloat(OrderValue.value) * parseFloat(CDPer.value)) / 100, 0);
        hdnCDValue.value = CDVAlue.value;
        hdnCDPer.value = CDPer.value;
    }
    
    CalAdjustValue();
}

function CalAdjustValue() {
    var txtValue = document.getElementById(CtrlIdPrefix + "txtValue");
    var hdnValue = document.getElementById(CtrlIdPrefix + "hdnValue");
    var grdCD = document.getElementById(CtrlIdPrefix + "grdCD");
    var CollectAmount = 0.0;
    var Check = null;
    var CDVAlue = null;
    for (var x = 1; x < grdCD.rows.length - 1; x++) {
        var row = grdCD.rows[x]
        Check = row.cells[0].children[0];
        CDVAlue = row.cells[7].children[0];
        if (Check.checked) {
            CollectAmount = CollectAmount + parseFloat(CDVAlue.value);
        }
    }
    
    txtValue.value = round(CollectAmount, 2);
    hdnValue.value = txtValue.value;
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

function ClearStreetsDropDown(id) {
    $('#' + id + ' option').each(function(i, option) { $(option).remove(); });
}

function PopulateGlMain(streets, id) {
    $('#' + id).append($('<option></option>').val("0").html("0"));
    $(streets).each(function(i) {
        $('#' + id).append($('<option></option>').val(streets[i].cd_percentage).html(streets[i].cd_percentage));
    });
}

//function fnSubmitcheck(s) {

//    var bchecked = false;
//    var inputElements = document.getElementById(CtrlIdPrefix + "grdCD").getElementsByTagName('input');
//    for (var i = 0; i < inputElements.length; i++) {
//        var myElement = inputElements[i];
//        if (myElement.type == "checkbox")
//            if (myElement.checked)
//            bchecked = true;
//    }
//    if (!bchecked) {
//        alert("Please Select a Document number");
//        return false;
//    }
//    else {
//        if (s == "M") {
//            var Status = "Are you Sure to Approve the CD Entry?";

//            if (confirm(Status))
//                return true;
//            else
//                return false;
//        }
//        else {
//            return true;
//        }
//    }
//}

function fnSubmitcheck(s) {
    var bchecked = false;

    var grdCD = document.getElementById(CtrlIdPrefix + "grdCD");
    for (var x = 1; x < grdCD.rows.length - 1; x++) {
        var row = grdCD.rows[x]
        Check = row.cells[0].children[0];
        CDVAlue = row.cells[7].children[0];

        if (Check.checked) {
            bchecked = true;
        }

        if (Check.checked) {
            if (parseFloat(CDVAlue.value) <= 0) {
                alert("Please Select a CD Percentage or Uncheck the CheckBox");
                return false;
            }
        }
    }

    if (!bchecked) {
        alert("Please Select a Document number");
        return false;
    }
    else {
        var txtValue = document.getElementById(CtrlIdPrefix + "txtValue");

        if (parseFloat(txtValue.value) <= 0) {
            alert("CD Cannot be Submitted for Zero Value. Please select a Percentage.");
            return false;
        }

        if (s == "M") {
            var Status = "Are you Sure to Approve the CD Entry?";

            if (confirm(Status))
                return true;
            else
                return false;
        }
        else {
            return true;
        }
    }
}


function funCdReject() {
    var Remarks = prompt("Are you Sure to Reject the CD Entry? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
}
    