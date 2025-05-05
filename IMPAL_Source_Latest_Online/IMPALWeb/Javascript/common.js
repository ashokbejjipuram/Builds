function DisableButton() {
    document.getElementById('ctl00_CPHDetails_BtnSubmit').disabled = true;
    $('#ctl00_CPHDetails_BtnSubmit').prop('disabled', true);
    document.getElementById('<%=BtnSubmit.ClientID%>').disabled = true;
    $('#<%=BtnSubmit.ClientID %>').prop('disabled', true);
}
window.onbeforeunload = DisableButton;

function ValidateDate(txtFromDate, txtToDate, hidFromDate, hidToDate) {
    var oFromDateVal = txtFromDate.value.trim();
    var oToDateVal = txtToDate.value.trim();
    if (oFromDateVal == null || oFromDateVal == "") {
        alert("From Date should not be null!");
        txtFromDate.focus();
        return false;
    }
    else if (oToDateVal == null || oToDateVal == "") {
        alert("To Date should not be null!");
        txtToDate.focus();
        return false;
    }
    if (CheckDates(txtFromDate, txtToDate) == false)
        return false;
    else {
        var oFromDate = oFromDateVal.split("/");
        var oToDate = oToDateVal.split("/");
        var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
        var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
        if (hidFromDate != null)
            hidFromDate.value = oFromDateFormatted;
        if (hidToDate != null)
            hidToDate.value = oToDateFormatted;
    }
}

function CheckDates(txtFromDate, txtToDate) {
    var oFromDateVal = txtFromDate.value.trim();
    var oToDateVal = txtToDate.value.trim();
    if (fnIsDate(oFromDateVal) == false) {
        txtFromDate.focus();
        return false;
    }
    else if (fnIsDate(oToDateVal) == false) {
        txtToDate.focus();
        return false;
    }
    else {
        var oSysDate = new Date();
        var oFromDate = oFromDateVal.split("/");
        var oToDate = oToDateVal.split("/");
        var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
        var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
        if (oSysDate < new Date(oFromDateFormatted)) {
            alert("From Date should not be greater than System Date");
            txtFromDate.value = "";
            txtFromDate.focus();
            return false;
        }
        else if (oSysDate < new Date(oToDateFormatted)) {
            alert("To Date should not be greater than System Date");
            txtToDate.value = "";
            txtToDate.focus();
            return false;
        }
        else if (new Date(oFromDateFormatted) > new Date(oToDateFormatted)) {
            alert("To Date should be greater than From Date");
            txtToDate.value = "";
            txtToDate.focus();
            return false;
        }
    }
}

function ValidateDateValues(txtFromDate, txtToDate) {
    var oFromDateVal = txtFromDate.value.trim();
    var oToDateVal = txtToDate.value.trim();
    if (oFromDateVal == null || oFromDateVal == "") {
        alert("From Date should not be null!");
        txtFromDate.focus();
        return false;
    }
    else if (oToDateVal == null || oToDateVal == "") {
        alert("To Date should not be null!");
        txtToDate.focus();
        return false;
    }
    else {
        var oSysDate = new Date();
        var oFromDate = oFromDateVal.split("/");
        var oToDate = oToDateVal.split("/");
        var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
        var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
        if (oSysDate < new Date(oFromDateFormatted)) {
            alert("From Date should not be greater than System Date");
            txtFromDate.value = "";
            txtFromDate.focus();
            return false;
        }
        else if (oSysDate < new Date(oToDateFormatted)) {
            alert("To Date should not be greater than System Date");
            txtToDate.value = "";
            txtToDate.focus();
            return false;
        }
        else if (new Date(oFromDateFormatted) > new Date(oToDateFormatted)) {
            alert("To Date should be greater than From Date");
            txtToDate.value = "";
            txtToDate.focus();
            return false;
        }
    }
}


function validateacc(ddlAccountPeriod) {

    if (ddlAccountPeriod.value.trim() == "-1") {
        alert("Accounting Period Should not be null");
        ddlAccountPeriod.focus();
        return false;
    }
}

function validatemonthyear(ddlMonthYear) {

    if (ddlMonthYear.value.trim() == null || ddlMonthYear.value.trim() == "") {
        alert("Month/Year Should not be null");
        ddlMonthYear.focus();
        return false;
    }
}




function enterNumberOnly() {
    if (window.event.keyCode != 13) {
        if ((window.event.keyCode >= 32 && window.event.keyCode < 48) || (window.event.keyCode > 57)) {
            if (window.event.keyCode < 48 || window.event.keyCode > 57)
                window.event.keyCode = 0;
            return true;
        }
    }
}

function validatemonthyear1(frommonth, tomonth, year) {

    var re = /^[0-9]*$/;
    if (frommonth.value.trim() == 0) {
        alert("From Month Should be selected");
        frommonth.focus();
        return false;
    }
    else if (year.value.trim() == "" || year.value.trim() == null) {
        alert("Year Should not be null");
        year.focus();
        return false;
    }

    else if (year.value.trim() < 1900 || year.value.trim() > 2100) {
        alert("Year Should  be between 1900 and 2100");
        year.focus();
        return false;
    }

    else if (!re.test(year.value)) {
        alert("Only Numeric Year value is allowed");
        year.focus();
        return false;
    }
    else if (tomonth.value.trim() == 0) {
        alert("To Month Should be selected");
        tomonth.focus();
        return false;
    }

    else if (tomonth.value == frommonth.value) {
        alert("From and To month should not be equal");
        frommonth.focus();
        return false;
    }
    else if ((tomonth.value - frommonth.value) > 2) {
        alert("The period should be of quarter-year");
        frommonth.focus();
        return false;
    }
    else if ((tomonth.value - frommonth.value) == 1) {
        alert("The period should be of quarter-year");
        frommonth.focus();
        return false;
    }
    else if ((tomonth.value - frommonth.value) < 0) {
        alert("To-month should be greater than From-month");
        tomonth.focus();
        return false;
    }
}

function GetTodaysDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10)
        dd = '0' + dd;
    if (mm < 10)
        mm = '0' + mm;
    var today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function ValidateLineCode(ddlFromLine, ddlToLine) {
    var oFromLineCodeVal = ddlFromLine.value.trim();
    var oToLineCodeVal = ddlToLine.value.trim();
    if ((oFromLineCodeVal != "" && oToLineCodeVal == "") || (oFromLineCodeVal != "0" && oToLineCodeVal == "0")) {
        alert("ToLineCode should not be null");
        ddlToLine.focus();
        return false;
    }
}

function ValidatePONumber(ddlPONumber) {
    var oPONumberVal = ddlPONumber.value.trim();
    if (oPONumberVal == null || oPONumberVal == "") {
        alert("PO Number should not be null");
        ddlPONumber.focus();
        return false;
    }
}

function ValidateSupplierCode(ddlsup) {
    if (ddlsup.selectedIndex <= 0) {
        alert("Supplier Code should not be null")
        return false;
    }
    else
        return true;
}
//To Validate single date function
function ValidateOutstandingDate(txtFromDate) {
    var oFromDateVal = txtFromDate.value.trim();
    var oSysDate = new Date();
    var oFromDate = oFromDateVal.split("/");
    var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
    if (oFromDateVal == null || oFromDateVal == "") {
        alert("Date should not be null!");
        txtFromDate.focus();
        return false;
    }
    if (oFromDateVal != null && fnIsDate(txtFromDate.value) == false) {
        txtFromDate.focus();
        return false;

    }
    else if (oSysDate < new Date(oFromDateFormatted)) {
        alert("Date should not be greater than System Date");
        txtFromDate.value = "";
        txtFromDate.focus();
        return false;
    }
}

//To Validate Transaction Single date function
//Pass option param as "Enabled" if null values has to be validated

function ValidateTranDate(txtFromDate, option) {
    var oFromDateVal = txtFromDate.value.trim();
    var oSysDate = new Date();
    var oFromDate = oFromDateVal.split("/");
    var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];


    if ((oFromDateVal == null && oFromDateVal == "") && option == "Enabled") {
        alert("Date should not be null!");
        txtFromDate.focus();
        return false;
    }
    if ((oFromDateVal != null && oFromDateVal != "") && fnIsDate(txtFromDate.value) == false) {
        txtFromDate.value = "";
        txtFromDate.focus();
        return false;

    }
    else if (oSysDate < new Date(oFromDateFormatted)) {
        alert("Date should not be greater than System Date");
        txtFromDate.value = "";
        txtFromDate.focus();
        return false;
    }
}

//To validate Bank
function ValidateBank(ddlBank, ddlBranch, ddlAccountingPeriod) {
    var oBankVal = ddlBank.value.trim();
    var oBranchVal = ddlBranch.value.trim();
    var oAccPeriodVal = ddlAccountingPeriod.value.trim();
    if (oAccPeriodVal == null || oAccPeriodVal == "") {
        alert("Accounting Period should not be null!");
        ddlAccountingPeriod.focus();
        return false;
    }
    if (oBankVal == null || oBankVal == "") {
        alert("Bank should not be null!");
        ddlBank.focus();
        return false;
    }
    if (oBranchVal == null || oBranchVal == "") {
        alert("Branch should not be null!");
        ddlBranch.focus();
        return false;
    }
}

function ValidateChallan(ddlChallanNo) {
    var oChallanVal = ddlChallanNo.value.trim();
    if (oChallanVal == null || oChallanVal == "") {
        alert("Challan Number should not be null");
        ddlChallanNo.focus();
        return false;
    }
}

function ValidateOrderDate(txtFromDate, txtToDate) {
    var oFromDateVal = txtFromDate.value.trim();
    var oToDateVal = txtToDate.value.trim();
    if (oFromDateVal == "" && oToDateVal != "") {
        alert("From Date should not be null!");
        txtFromDate.focus();
        return false;
    }
    else if (oToDateVal == "" && oFromDateVal != "") {
        alert("To Date should be greater than or equal to From Date");
        txtToDate.focus();
        return false;
    }
    if (CheckDates(txtFromDate, txtToDate) == false)
        return false
}
//Below Code is used for validating date functionality

var dtCh = "/";

function isInteger(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}

function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
        if (i == 2) { this[i] = 29 }
    }
    return this
}

function fnIsDate(dtStr) {
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strDay = dtStr.substring(0, pos1)
    var strDy = strDay
    var strMonth = dtStr.substring(pos1 + 1, pos2)
    var strMnth = strMonth
    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    if (strDay == "" || strMonth == "" || isNaN(year) || pos1 == -1 || pos2 == -1) {
        alert("The date format should be : dd/mm/yyyy")
        return false
    }
    if (strMonth.length < 1 || strMnth.length < 2 || month < 1 || month > 12) {
        alert("Invalid Date")
        return false
    }
    if (strDay.length < 1 || strDy.length < 2 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        alert("Invalid Date")
        return false
    }
    if (strYear.length != 4 || year == 0) {
        alert("Invalid Date")
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        alert("Invalid Date")
        return false
    }
    return true
}

function ValidateDates(txtFromDate, txtToDate) {
    var oFromDateVal = txtFromDate.value.trim();
    var oToDateVal = txtToDate.value.trim();
    var oSysDate = new Date();
    var oFromDate = oFromDateVal.split("/");
    var oToDate = oToDateVal.split("/");
    var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
    var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];


    if (oFromDateVal == null || oFromDateVal == "") {
        alert("From Date should not be null!");
        txtFromDate.focus();
        return false;
    }
    else if (oToDateVal == null || oToDateVal == "") {
        alert("To Date should not be null!");
        txtToDate.focus();
        return false;
    }

    else if (oFromDateVal != null && fnIsDate(txtFromDate.value) == false) {
        txtFromDate.value = "";
        txtFromDate.focus();
        return false;

    }

    else if (oToDateVal != null && fnIsDate(txtToDate.value) == false) {
        txtToDate.value = "";
        txtToDate.focus();
        return false;
    }
    else if (oSysDate < new Date(oFromDateFormatted)) {
        alert("From Date should not be greater than System Date");
        txtFromDate.value = "";
        txtFromDate.focus();
        return false;
    }
    else if (oSysDate < new Date(oToDateFormatted)) {
        alert("To Date should not be greater than System Date");
        txtToDate.value = "";
        txtToDate.focus();
        return false;
    }
    else if (new Date(oFromDateFormatted) > new Date(oToDateFormatted)) {
        alert("To Date should be greater than or equal to From Date");
        txtToDate.value = "";
        txtToDate.focus();
        return false;
    }
}

function ValidateFromLine(ddlFromLine, ddlToLine) {
    if ((ddlToLine.value != "0") && (ddlFromLine.value == "0")) {
        alert("From Line should not be null!");
        ddlFromLine.focus();
        return false;
    }
}

function ValidateIndicator(ddlStation) {
    if (ddlStation.value == "") {
        alert("Indicator should not be null");
        ddlStation.focus();
        return false;
    }
}
//Added for Debit Credit 


function validate(inpval, fldname) {
    /*******************************************************************************************
    Name			:	validate
    Purpose			:	To validate a field .
    Parameters in		:	inp of type int
    Parameters out		:	boolean
    Called functions	:	not applicable
    --------------------------------------------------------------------------------------------
    Revision Log
    WHO					WHEN					ACTION					COMMENTS
	
	
    ********************************************************************************************/

    if (inpval == "") {
        alert(fldname + "should not be null")
        return true
    }
    else if (validatespl(inpval, fldname)) {
        return true
    }
    else {
        return false
    }
}

function validatespl(inpval, fldname) {
    /*******************************************************************************************
    Name			:	validate
    Purpose			:	To validate a field has first character as blank or special character.
    Parameters in	:	inp of type string
    Parameters out	:	return 1 if first char is balnk
    return 2 if firstchar is special character
    Called functions:	not applicable
    --------------------------------------------------------------------------------------------
    Revision Log
    WHO					WHEN					ACTION					COMMENTS
	
	
    ********************************************************************************************/

    firstchr = inpval.substring(0, 1, inpval)
    if (firstchr == " ") {
        alert("First character of " + fldname + " should not be blank")
        return true
    }
    else if (isspecialchar(firstchr)) {
        alert("First character of " + fldname + " should be alphabet or number")
        return true
    }


    for (i = 0; i < inpval.length; i++) {
        firstchr = inpval.substring(i, i + 1, inpval)
        if (firstchr == "") {
            alert("First character of " + fldname + " should not be blank")
            return true
        }
        else if (isspecial1(firstchr)) {
            alert("Characters in " + fldname + " should be alphabet or number")
            return true
        }
    }
    return false
}


function ValidateFirstCharacter(id, Field) {
    inpval = document.getElementById(id).value;
    firstchr = inpval.substring(0, 1, inpval)

    if (isspecialchar(firstchr)) {
        alert("First character of " + Field + " should be alphabet or number")
        document.getElementById(id).value = "";
        document.getElementById(id).focus();
        return false;
    }
}



function isspecialchar(c) {
    /*******************************************************************************************
    Name			:	isspecialchar
    Purpose			:	To check whether a character is speial character.
    Parameters in		:	inp of type char
    Parameters out		:	boolean
    Called functions	:	not applicable
    --------------------------------------------------------------------------------------------
    Revision Log
    WHO					WHEN					ACTION					COMMENTS
	
	
    ********************************************************************************************/

    myArray = ['!', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '`', '~', ']', '[', '|', '@', '/', '"', ':', ';', '{', '}', ',', "'", '.', '?', '\\'];

    for (var j = 0; j < myArray.length; j++) {
        if (c == myArray[j]) {
            return true;
        }

    }
    return false;
}

function isspecial1(c) {
    /*******************************************************************************************
    Name			:	isspecialchar
    Purpose			:	To check whether a character is speial character.
    Parameters in		:	inp of type char
    Parameters out		:	boolean
    Called functions	:	not applicable
    --------------------------------------------------------------------------------------------
    Revision Log
    WHO					WHEN					ACTION					COMMENTS
	
	
    ********************************************************************************************/

    myArray = ['"', "'"];
    for (var j = 0; j < myArray.length; j++) {
        if (c == myArray[j]) {
            return true;
        }

    }
    return false;
}


function CompareDate(DtLsrdate, DtGrtdate) {
    /*******************************************************************************************
    Name			:	CompareDate
    Purpose			:	To compare date . return 0 if DtLsrdate < DtGrtdate ,
    :	return 1 if DtLsrdate > DtGrtdate, return 2 if DtLsrdate = DtGrtdate 
    Parameters in	:	DtLsrdate, DtGrtdate of type string
    Parameters out	:	boolean
    Called functions:	DateLengthCheck()
    --------------------------------------------------------------------------------------------
    Revision Log
    WHO					WHEN					ACTION					COMMENTS
	
	
    ********************************************************************************************/
    if (parseInt(DtLsrdate.length) != 10) {
        DtLsrdate = DateLengthCheck(DtLsrdate)
    }
    if (parseInt(DtGrtdate.length) != 10) {
        DtGrtdate = DateLengthCheck(DtGrtdate)
    }

    var DtLsrdate1 = DtLsrdate.substring(6, DtLsrdate.length)
    var DtLsrdate2 = DtLsrdate.substring(3, DtLsrdate.length - 5)
    var DtLsrdate3 = DtLsrdate.substring(0, DtLsrdate.length - 8)

    var DtGrtdate1 = DtGrtdate.substring(6, DtGrtdate.length)
    var DtGrtdate2 = DtGrtdate.substring(3, DtGrtdate.length - 5)
    var DtGrtdate3 = DtGrtdate.substring(0, DtGrtdate.length - 8)

    if (DtLsrdate1 < DtGrtdate1) {
        return 0;
    }
    if (DtLsrdate1 > DtGrtdate1) {
        return 1;
    }
    if (DtLsrdate1 == DtGrtdate1) {
        if (DtLsrdate2 < DtGrtdate2) {
            return 0;
        }
        if (DtLsrdate2 > DtGrtdate2) {
            return 1;
        }
        if (DtLsrdate2 == DtGrtdate2) {
            if (DtLsrdate3 < DtGrtdate3) {
                return 0;
            }
            if (DtLsrdate3 > DtGrtdate3) {
                return 1;
            }
            if (DtLsrdate3 == DtGrtdate3) {
                return 2;
            }
        }
    }
}

function doValidateDate(f) {
    /*******************************************************************************************
    Name			:	doValidateDate
    Purpose			:	To validate a date field .
    Parameters in	:	inp of type char
    Parameters out	:	boolean
    Called functions:	not applicable
    --------------------------------------------------------------------------------------------
    Revision Log
    WHO					WHEN					ACTION					COMMENTS
	
	
    ********************************************************************************************/
    var elementname = f;
    var fromdate = f.value;
    var ind = fromdate.length;
    var flag = fromdate.indexOf("/");
    var symbol = "&";
    if (ind == 0) {
        return false;
    }
    if (flag > 0) {
        symbol = "/";
    }

    var count = 0;
    var i = 0;
    if (symbol != "&") {
        while (i < fromdate.length) {
            ind = fromdate.lastIndexOf(symbol, ind);
            if (ind-- > 0) {
                count++;
            }
            i++;
        }
    }

    if (count == 2) {
        var a = fromdate.split(symbol);
        var month = a[1];
        var day = a[0];
        var year = a[2];
        maxdays = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
        if (isNaN(month) || (month == "") || (month == "0")) {
            alert("Invalid value for month in Date");
            f.select();
            f.focus();
            return (true);
        }

        if (isNaN(day) || (day == "") || (day == "0")) {
            alert("Invalid value for day in Date");
            f.select();
            f.focus();
            return (true);
        }
        if (year.length < 4) {
            alert("Enter year in yyyy format");
            f.select();
            f.focus();
            return (true);
        }
        if (isNaN(year) || (year < 1900) || (year > 2100)) {
            alert("Enter a value between 1900 to 2100 for year in Date");
            f.select();
            f.focus();
            return (true);
        }
        if (((year % 4 == 0) && !(year % 100 == 0)) || ((year % 100 == 0) && (year % 400 == 0)))
            maxdays[1] = maxdays[1] + 1;
        if (month <= 0 || month > 12) {
            alert("Invalid value for month in Date");
            f.select();
            f.focus();
            return true;
        }

        if ((day <= 0) || (day > maxdays[month - 1])) {
            alert("Invalid Date");
            f.select();
            f.focus();
            return true;
        }
        return false;
    }
    else {
        alert("Enter the date in dd/mm/yyyy format");
        return true;
    }
    return false;
}

function DateLengthCheck(DtDateCheck) {
    /*******************************************************************************************
    Name			:	DateLengthCheck
    Purpose			:	To stuff date in dd/mm/yyyy format 
    Parameters in		:	DtDateCheck of type string
    Parameters out		:	DtDateCheck of type string
    Called functions	:	none
    --------------------------------------------------------------------------------------------
    Revision Log
    WHO					WHEN					ACTION					COMMENTS
	
	
    ********************************************************************************************/

    var Dtparts = DtDateCheck.split("/")
    for (var i = 0; i < Dtparts.length; i++) {
        if (Dtparts[i].length < 2) {
            Dtparts[i] = "0" + Dtparts[i]
        }
    }
    DtDateCheck = Dtparts.join("/")
    return DtDateCheck
}
// Validate Method For Transaction Code for EditListForTransaction.aspx Page
function ValidateTransactionCode(ddltrancode) {
    if (ddltrancode.value == "") {
        alert("Transaction Code should not be null");
        ddltrancode.focus();
        return false;
    }
}

// Validate method for CollectionAgin.aspx Page
function fnValidateFinance(ddlBranch, cboFromCustomer, cboToCustomer, txtFromDate) {

    if (ddlBranch.value == "" || ddlBranch.value == null) {
        alert("Branch Should not be null ");
        ddlBranch.focus();
        return false;
    }
    if (cboFromCustomer.value == "" || cboFromCustomer.value == null) {
        alert("From Customer Should not be null ");
        cboFromCustomer.focus();
        return false;
    }
    if (cboToCustomer.value == "" || cboToCustomer.value == null) {
        alert("To Customer Should not be null ");
        cboToCustomer.focus();
        return false;
    }

    if (cboToCustomer.value < cboFromCustomer.value) {
        alert("To Customer Should be greater");
        cboToCustomer.focus();
        return false;
    }
    if ((ValidateOutstandingDate(txtFromDate)) == false)
        return false;
}
//Used for Reports->Insurance pages
function ValidateClaimNumb(ddlClaimBillNumber) {
    var oClaimNumberVal = ddlClaimBillNumber.value.trim();
    if (oClaimNumberVal == null || oClaimNumberVal == "") {
        alert("ClaimNumber should not be null!");
        ddlClaimBillNumber.focus();
        return false;
    }
}

function CheckDateRequired(txtDate) {
    var CurrDate = new Date();
    var txtDateSplit = txtDate.value.split("/");
    var txtDateFormatted = txtDateSplit[0] + "/" + txtDateSplit[1] + "/" + txtDateSplit[2];
    if (txtDate.value.trim() == null || txtDate.value.trim() == "") {
        alert("Date should not be null");
        txtDate.focus();
        return false;
    }
    //    else if (CurrDate < new Date(txtDate.value.trim())) {
    //        alert("Date should not be greater than Current Date");
    //        txtDate.value = "";
    //        txtDate.focus();
    //        return false;
    //    }

}

function IsNotFutureDate(txtDate) {
    var CurrDate = new Date();
    var txtDateSplit = txtDate.value.split("/");
    var txtDateFormatted = txtDateSplit[1] + "/" + txtDateSplit[0] + "/" + txtDateSplit[2];

    if (CurrDate < new Date(txtDateFormatted)) {
        alert("Date should not be greater than Current Date");
        txtDate.value = "";
        txtDate.focus();
        return false;
    }
}

function enterOnlyIntegerWithOneDecimal(e, control) {

    if (e.keyCode == 46) {
        var patt1 = new RegExp("\\.");
        var ch = patt1.exec(control.value);
        if (ch == ".") {
            e.keyCode = 0;
        }
    }
    else if ((e.keyCode >= 48 && e.keyCode <= 57) || e.keyCode == 8)//Numbers or BackSpace
    {
        if (control.value.indexOf('.') != -1)//. Exisist in TextBox 
        {
            var pointIndex = control.value.indexOf('.');
            var beforePoint = control.value.substring(0, pointIndex);
            var afterPoint = control.value.substring(pointIndex + 1);
            var iCaretPos = 0;
            if (document.selection) {
                var selectionRange = document.selection.createRange();
                selectionRange.moveStart('character', -control.value.length);
                iCaretPos = selectionRange.text.length;
            }
            if (iCaretPos > pointIndex && afterPoint.length >= 2) {
                e.keyCode = 0;
            }
            else if (iCaretPos <= pointIndex && beforePoint.length >= 7) {
                e.keyCode = 0;
            }
        }
        else//. Not Exisist in TextBox
        {
            if (control.value.length >= 7) {
                e.keyCode = 0;
            }
        }
    }
    else {
        e.keyCode = 0;
    }
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

function NumericsOnly() {
    var AsciiValue = event.keyCode
    if (AsciiValue >= 48 && AsciiValue <= 57)
        event.returnValue = true;
    else
        event.returnValue = false;
}

function CheckValidDate(id, isFutureDate,Msg) {

 
    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            if (isFutureDate == true) {
                var fDate = idDate.split("/");
                var day = fDate[0];
                var month = fDate[1] - 1;
                var year = parseInt(fDate[2]);

                var frmDate = new Date();
                frmDate.setDate(day);
                frmDate.setMonth(month);
                frmDate.setFullYear(year);

                if (frmDate > new Date()) {
                    alert(Msg + " should not be greater than Today's Date");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
            }

        }

    }
}

function getDaysBetweenDates(date) {

    var str1 = date.split('/');
    var date1 = new Date(str1[2], str1[1] - 1, str1[0]);
    var date2 = new Date();
    date1.setHours(0);
    date1.setMinutes(0, 0, 0);
    date2.setHours(0);
    date2.setMinutes(0, 0, 0);
    var datediff = Math.abs(date1.getTime() - date2.getTime()); // difference
    return parseInt(datediff / (24 * 60 * 60 * 1000));
}

function CheckFromTodates(txtFromDate, txtToDate) {
    var oFromDateVal = txtFromDate.value.trim();
    var oToDateVal = txtToDate.value.trim();
    var oSysDate = new Date();

    if (oFromDateVal != '') {
        var oFromDate = oFromDateVal.split("/");
        var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
        if (!fnIsDate(oFromDateVal)) {
            txtFromDate.value = "";
            txtFromDate.focus();
            return false;
        }

        if (oSysDate < new Date(oFromDateFormatted)) {
            alert("From Date should not be greater than System Date");
            txtFromDate.value = "";
            txtFromDate.focus();
            return false;
        }

    }


    if (oToDateVal != '') {
        var oFromDate = oFromDateVal.split("/");
        var oToDate = oToDateVal.split("/");
        var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
        var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
        if (!fnIsDate(oToDateVal)) {
            txtToDate.value = "";
            txtToDate.focus();
            return false;
        }

        if (oSysDate < new Date(oToDateFormatted)) {
            alert("To Date should not be greater than System Date");
            txtToDate.value = "";
            txtToDate.focus();
            return false;
        }
        if (new Date(oFromDateFormatted) > new Date(oToDateFormatted)) {
            alert("To Date should be greater than From Date");
            txtToDate.value = "";
            txtToDate.focus();
            return false;
        }
    }
}

