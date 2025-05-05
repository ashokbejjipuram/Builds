var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvItemDetails_";

function GetRowIdPrefix(rowidprefix) {
    var rowCtlId = rowidprefix.split("_");

    return rowCtlId[3] + "_";

}

function CurrencyNumberOnly() {
    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function CapitalAlphaNumericwithOutDot() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue >= 65 && AsciiValue <= 90))
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

function toIndianCurrency(id) {
    var amount = document.getElementById(id).value.replaceAll(',','');

    var afterPoint = '';
    if (amount.indexOf('.') > 0)
        afterPoint = amount.substring(amount.indexOf('.'), amount.length);
    amount = Math.floor(amount);
    amount = amount.toString();
    var lastThree = amount.substring(amount.length - 3);
    var otherNumbers = amount.substring(0, amount.length - 3);
    if (otherNumbers != '')
        lastThree = ',' + lastThree;
    var res = otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThree + afterPoint;

    document.getElementById(id).value = res;
}

function ShowHidePanel(id) {
    var panelid = document.getElementById(CtrlIdPrefix + id);

    if (panelid.style.display == "inline")
        panelid.style.display = "none";
    else
        panelid.style.display = "inline";
}

function checkDate(sender, args) {

    var strSender = sender._id;
    var idRefDocDate = strSender.replace("ceCustomerPODate", "txtCustomerPODate");

    if (sender._selectedDate > new Date()) {
        alert("date should not be greater than Today's Date");

        if (CtrlIdPrefix + "ceCustomerPODate" == strSender) {
            document.getElementById(CtrlIdPrefix + "txtCustomerPODate").value = "";
        }
        else {
            document.getElementById(idRefDocDate).value = "";
        }
    }
}

function checkNum() {

    if ((event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 96 && event.keyCode < 123) || event.keyCode == 8)
        return true;
    else {
        return false;
    }
}

function ValidatSisDealerGrid() {
    var grDtls = document.getElementById(CtrlIdPrefix + "grvGroupDealers");
    if (grDtls != null) {
        var rowscount = grDtls.rows.length;
        if (rowscount >= 3) {
            for (k = 1; k < grDtls.rows.length; k++) {
                var row = grDtls.rows[k];
                txtSno = row.cells[0].children[0];

                if (txtSno != null) {
                    ddlSisDealerBranch = row.cells[1].children[0];
                    ddlSisDealer = row.cells[2].children[0];
                    txtSisDealer = row.cells[3].children[0];
                    txtSisCrLim = row.cells[4].children[0];

                    if (ddlSisDealerBranch.id != null && ddlSisDealerBranch.id != "") {
                        if (ddlSisDealerBranch.value == "0" || ddlSisDealerBranch.value == "0.00" || ddlSisDealerBranch.value == "") {
                            alert("Please select Group Company Dealer Branch in row " + k);
                            document.getElementById(ddlSisDealerBranch.id).focus();
                            return false;
                        }
                    }

                    if (ddlSisDealer.id != null && ddlSisDealer.id != "") {
                        if (ddlSisDealer.value == "0" || ddlSisDealer.value == "0.00" || ddlSisDealer.value == "") {
                            alert("Please select Group Company Dealer in row " + k);
                            document.getElementById(ddlSisDealer.id).focus();
                            return false;
                        }
                    }

                    if (txtSisDealer.id != null && txtSisDealer.id != "") {
                        if (txtSisDealer.value == "") {
                            alert("Group Company Dealer Code is not available in row " + k);
                            document.getElementById(txtSisDealer.id).focus();
                            return false;
                        }
                    }

                    //if (txtSisCrLim.id != null && txtSisCrLim.id != "") {
                    //    if (txtSisCrLim.value == "") {
                    //        alert("Group Company Dealer Credit Limit is not available in row " + k);
                    //        document.getElementById(txtSisCrLim.id).focus();
                    //        return false;
                    //    }
                    //}
                }
            }
        }
    }
}

function validateprint() {
    if (confirm("Do you Want to Print the Application?")) {
        document.getElementById("header").style.display = "none";
        document.getElementById("ShowHideMenuHolder").style.display = "none";
        document.getElementById("ctl00_SiteMapPathHolder").style.display = "none";
        document.getElementById("footer").style.display = "none";

        return true;
    }
    else
        return false;
}

function fnprint() {
    document.getElementById("header").style.display = "none";
    document.getElementById("ctl00_ShowHideMenu").style.display = "none";
    document.getElementById("ctl00_SiteMapPathHolder").style.display = "none";
    document.getElementById("footer").style.display = "none";
    window.print();
}

function CreditLimitChange() {
    var panel3 = document.getElementById(CtrlIdPrefix + "Panel3");

    if (panel3) {
        var OldCrLimit = document.getElementById(CtrlIdPrefix + "txtExistingCrLimit").value.replaceAll(',', '');
        var NewCrLimit = document.getElementById(CtrlIdPrefix + "txtEnhCrLimtReq").value.replaceAll(',', '');
        var ValiditityInd = document.getElementById(CtrlIdPrefix + "ddlValidityIndicator");

        if (OldCrLimit == "")
            OldCrLimit = "0";

        if (NewCrLimit == "")
            NewCrLimit = "0";

        if (parseFloat(OldCrLimit) == parseFloat(NewCrLimit)) {
            alert('There is No Change in the Existing Credit Limit');
            return false;
        }

        //if (parseFloat(NewCrLimit) >= 1000000 && parseFloat(NewCrLimit) > parseFloat(OldCrLimit)) {
        //    alert('Credit Limit Change of Rs. 10 Lakhs and Above will be Updated by only HO Admin Department. Please Contact HO After Submitting this Form.');
        //}

        if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit))
            ValiditityInd.disabled = false;
        else
            ValiditityInd.disabled = true;
    }
    else
        ValiditityInd.disabled = true;
}

function ApprovedCrLimitValidation() {
    var div5 = document.getElementById(CtrlIdPrefix + "div5");

    if (div5 != null) {        
        var EnhCrLimit = document.getElementById(CtrlIdPrefix + "txtEnhCrLimtReq");
        var ApprCrLimit = document.getElementById(CtrlIdPrefix + "txtApprovedCrLimit");

        if (parseFloat(ApprCrLimit.value.replaceAll(',', '')) > parseFloat(EnhCrLimit.value.replaceAll(',', ''))) {
            alert('Approved Credit Limit should not exceed Requested Credit Limit');
            ApprCrLimit.focus();
            return false;
        }
    }
}

function CreditLimitValidity() {
    var panel3 = document.getElementById(CtrlIdPrefix + "Panel3");

    if (panel3) {
        var DueDate = document.getElementById(CtrlIdPrefix + "txtCrlimitDueDate");
        var OldCrLimit = document.getElementById(CtrlIdPrefix + "txtExistingCrLimit").value.replaceAll(',', '');
        var NewCrLimit = document.getElementById(CtrlIdPrefix + "txtEnhCrLimtReq").value.replaceAll(',', '');
        var ValiditityInd = document.getElementById(CtrlIdPrefix + "ddlValidityIndicator").value;

        var date = new Date(), y = date.getFullYear(), m = date.getMonth();
        var firstDay = new Date(y, m, 1);
        var lastDay = new Date(y, m + 1, 0);

        if (OldCrLimit == "")
            OldCrLimit = "0";

        if (NewCrLimit == "")
            NewCrLimit = "0";

        //if (parseFloat(NewCrLimit) >= 1000000 && parseFloat(OldCrLimit) < parseFloat(NewCrLimit)) {
        //    alert('Credit Limit Change of Rs. 10 Lakhs and Above will be Updated by only HO Admin Department. Please Contact HO after Submitting this form.');
        //}

        if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd == "T") {
            DueDate.value = lastDay.toLocaleString('en-GB', { timeZone: 'IST' }).split(',')[0];
            DueDate.disabled = false;
        }
        else {
            DueDate.value = "";
            DueDate.disabled = true;
        }
    }
    else
        ValiditityInd.disabled = true;
}


function CheckValidDateCrLimit() {
    var OldCrLimit = document.getElementById(CtrlIdPrefix + "txtExistingCrLimit").value.replaceAll(',', '');
    var NewCrLimit = document.getElementById(CtrlIdPrefix + "txtEnhCrLimtReq").value.replaceAll(',', '');
    var ValiditityInd = document.getElementById(CtrlIdPrefix + "ddlValidityIndicator");
    var DueDate = document.getElementById(CtrlIdPrefix + "txtCrlimitDueDate");

    if (OldCrLimit == "")
        OldCrLimit = "0";

    if (NewCrLimit == "")
        NewCrLimit = "0";

    if (parseFloat(OldCrLimit) == parseFloat(NewCrLimit)) {
        alert('There is No Change in the Existing Credit Limit');
    }

    if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "") {
        alert('Please Select Validity Indicator for Credit Limit Change');
        ValiditityInd.focus();
        return false;
    }

    if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T") {
        if (DueDate.value.trim() == "") {
            alert('Please Select Validity Date for Credit Limit Change');
            DueDate.focus();
            return false;
        }

        if (fnIsDate(DueDate.value.trim()) == false) {
            DueDate.focus();
            return false;
        }
    }

    if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T" && DueDate.value.trim() != "") {
        var oSysDate = new Date();
        var oDate = DueDate.value.trim().split("/");

        var oCurDateFormatted = (oSysDate.getMonth() + 1) + '/' + oSysDate.getDate() + '/' + oSysDate.getFullYear();
        var oDateFormatted = oDate[1] + "/" + oDate[0] + "/" + oDate[2];

        if (new Date(oCurDateFormatted) > new Date(oDateFormatted)) {
            alert("Validity Date should be greater than or equal to System Date");
            DueDate.value = "";
            DueDate.focus();
            return false;
        }
    }
}

function ValidateChkBox1(id) {
    var chkBox = document.getElementById(id);
    var txtPropName = document.getElementById(CtrlIdPrefix + "txtPropName");
    var txtPropMobile = document.getElementById(CtrlIdPrefix + "txtPropMobile");
    var txtContactPersonName = document.getElementById(CtrlIdPrefix + "txtContactPersonName");
    var txtContactPersonMobNo = document.getElementById(CtrlIdPrefix + "txtContactPersonMobNo");

    if (chkBox.checked) {
        txtContactPersonName.value = txtPropName.value;
        txtContactPersonMobNo.value = txtPropMobile.value;
    }
    else {
        txtContactPersonName.value = "";
        txtContactPersonMobNo.value = "";
    }
}

function fnSubmitForm(id) {
    var rbType = document.getElementById(CtrlIdPrefix + "rbType");
    var inputs = rbType.getElementsByTagName("input");

    var notSelected = -1;

    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].checked) {
            notSelected = i;
        }
    }

    var panel1 = document.getElementById(CtrlIdPrefix + "Panel1");

    if (panel1) {
        if (notSelected == 1) {
            var ddlCustomerCode = document.getElementById(CtrlIdPrefix + "ddlCustomerCode");
            if (ddlCustomerCode.value.trim() == "") {
                alert("Please select Customer");
                ddlCustomerCode.focus();
                return false;
            }
        }

        var txtCustName = document.getElementById(CtrlIdPrefix + "txtCustName");
        if (txtCustName.value.trim() == "") {
            alert("Please Enter Customer Name");
            txtCustName.focus();
            return false;
        }

        var txtAdd1 = document.getElementById(CtrlIdPrefix + "txtAdd1");
        var txtAdd2 = document.getElementById(CtrlIdPrefix + "txtAdd2");
        var txtAdd3 = document.getElementById(CtrlIdPrefix + "txtAdd3");
        var txtAdd4 = document.getElementById(CtrlIdPrefix + "txtAdd4");

        if (txtAdd1.value.trim() == "") {
            alert("Please Enter Address 1");
            txtAdd1.focus();
            return false;
        }
        if (txtAdd2.value.trim() == "") {
            alert("Please Enter Address 2");
            txtAdd2.focus();
            return false;
        }

        var txtPropName = document.getElementById(CtrlIdPrefix + "txtPropName");
        if (txtPropName.value.trim() == "") {
            alert("Please Enter Proprietor/ Director/ Partner/ Authorised Person Name");
            txtPropName.focus();
            return false;
        }

        var txtPropMobile = document.getElementById(CtrlIdPrefix + "txtPropMobile");
        if (txtPropMobile.value.trim() == "") {
            alert("Please Enter Proprietor Mobile No.");
            txtPropMobile.focus();
            return false;
        }

        var txtContactPersonName = document.getElementById(CtrlIdPrefix + "txtContactPersonName");
        if (txtContactPersonName.value.trim() == "") {
            alert("Please Enter Contact Person Name");
            txtContactPersonName.focus();
            return false;
        }

        var ddlDealerState = document.getElementById(CtrlIdPrefix + "ddlDealerState");
        if (ddlDealerState.value.trim() == "") {
            alert("Please Select Dealer State");
            ddlDealerState.focus();
            return false;
        }

        var txtContactPersonMobNo = document.getElementById(CtrlIdPrefix + "txtContactPersonMobNo");
        if (txtContactPersonMobNo.value.trim() == "") {
            alert("Please Enter Contact Person Mobile No.");
            txtContactPersonMobNo.focus();
            return false;
        }

        var ddlDealerDistrict = document.getElementById(CtrlIdPrefix + "ddlDealerDistrict");
        if (ddlDealerDistrict.value.trim() == "") {
            alert("Please Select Delaer District");
            ddlDealerDistrict.focus();
            return false;
        }

        var txtEmailid = document.getElementById(CtrlIdPrefix + "txtEmailid");
        if (txtEmailid.value.trim() == "") {
            alert("Please Enter Email id");
            txtEmailid.focus();
            return false;
        }

        if (txtEmailid.value.trim() != "") {
            var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

            if (txtEmailid.value.match(validRegex)) {                
            } else {
                alert("Invalid email address");
                txtEmailid.focus();
                return false;
            }
        }

        var ddlDealerTown = document.getElementById(CtrlIdPrefix + "ddlDealerTown");
        if (ddlDealerTown.value.trim() == "") {
            alert("Please Select Delaer Town");
            ddlDealerTown.focus();
            return false;
        }

        var txtLocation = document.getElementById(CtrlIdPrefix + "txtLocation");
        if (txtLocation.value.trim() == "") {
            alert("Please Enter Location");
            txtLocation.focus();
            return false;
        }

        var ddlTownClassification = document.getElementById(CtrlIdPrefix + "ddlTownClassification");
        if (ddlTownClassification.value.trim() == "") {
            alert("Please Select Dealer Town Location");
            ddlTownClassification.focus();
            return false;
        }

        var txtPinCode = document.getElementById(CtrlIdPrefix + "txtPinCode");
        if (txtPinCode.value.trim() == "") {
            alert("Please Enter PinCode");
            txtPinCode.focus();
            return false;
        }

        var ddlFirmType = document.getElementById(CtrlIdPrefix + "ddlFirmType");
        if (ddlFirmType.value.trim() == "") {
            alert("Please Select Type of Firm");
            ddlFirmType.focus();
            return false;
        }

        var ddlRegnType = document.getElementById(CtrlIdPrefix + "ddlRegnType");
        if (ddlRegnType.value.trim() == "") {
            alert("Please Select Type of Registration");
            ddlRegnType.focus();
            return false;
        }

        var ddlAssignedSMRR = document.getElementById(CtrlIdPrefix + "ddlAssignedSMRR");
        if (ddlAssignedSMRR.value.trim() == "") {
            alert("Please Select Salesman / RR assigned to the Dealer");
            ddlAssignedSMRR.focus();
            return false;
        }

        var txtDealerTarget = document.getElementById(CtrlIdPrefix + "txtDealerTarget");
        if (txtDealerTarget.value.trim() == "" || txtDealerTarget.value.trim() == "0" || txtDealerTarget.value.trim() == "0.00") {
            alert("Please Enter Dealer Target");
            txtDealerTarget.focus();
            return false;
        }
    }   

    var panel2 = document.getElementById(CtrlIdPrefix + "Panel2");

    if (panel2) {
        var ddlDealerClassification = document.getElementById(CtrlIdPrefix + "ddlDealerClassification");
        if (ddlDealerClassification.value.trim() == "") {
            alert("Please Select Delaer Classification");
            ddlDealerClassification.focus();
            return false;
        }

        var ddlDealerSegment = document.getElementById(CtrlIdPrefix + "ddlDealerSegment");
        if (ddlDealerSegment.value.trim() == "") {
            alert("Please Select Delaer Segment");
            ddlDealerSegment.focus();
            return false;
        }

        var txtTransporterName = document.getElementById(CtrlIdPrefix + "txtTransporterName");
        if (txtTransporterName.value.trim() == "") {
            alert("Please Enter Transporter Name");
            txtTransporterName.focus();
            return false;
        }
    }

    var panel3 = document.getElementById(CtrlIdPrefix + "Panel3");

    if (panel3) {
        var ddlFreightIndicator = document.getElementById(CtrlIdPrefix + "ddlFreightIndicator");
        if (ddlFreightIndicator.value.trim() == "") {
            alert("Please Select Freight Indicator");
            ddlFreightIndicator.focus();
            return false;
        }

        var OldCrLimit = document.getElementById(CtrlIdPrefix + "txtExistingCrLimit").value.replaceAll(',', '');
        var NewCrLimit = document.getElementById(CtrlIdPrefix + "txtEnhCrLimtReq").value.replaceAll(',', '');
        var CrLimitIndicator = document.getElementById(CtrlIdPrefix + "ddlCrLimitIndicator");
        var ValiditityInd = document.getElementById(CtrlIdPrefix + "ddlValidityIndicator");
        var DueDate = document.getElementById(CtrlIdPrefix + "txtCrlimitDueDate");

        if (OldCrLimit == "")
            OldCrLimit = "0";

        if (NewCrLimit == "")
            NewCrLimit = "0";

        if (parseFloat(OldCrLimit) == parseFloat(NewCrLimit)) {
            alert('There is No Change in the Existing Credit Limit');
        }        

        if (CrLimitIndicator.value.trim() == "") {
            alert('Please Select Credit Limit Indicator');
            CrLimitIndicator.focus();
            return false;
        }

        if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "") {
            alert('Please Select Validity Indicator for Credit Limit Change');
            ValiditityInd.focus();
            return false;
        }

        if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T") {
            if (DueDate.value.trim() == "") {
                alert('Please Select Validity Date for Credit Limit Change');
                DueDate.focus();
                return false;
            }

            if (fnIsDate(DueDate.value.trim()) == false) {
                DueDate.focus();
                return false;
            }
        }

        if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T" && DueDate.value.trim() != "") {
            var oSysDate = new Date();
            var oDate = DueDate.value.trim().split("/");

            var oCurDateFormatted = (oSysDate.getMonth() + 1) + '/' + oSysDate.getDate() + '/' + oSysDate.getFullYear();
            var oDateFormatted = oDate[1] + "/" + oDate[0] + "/" + oDate[2];

            if (new Date(oCurDateFormatted) > new Date(oDateFormatted)) {
                alert("Validity Date should be greater than or equal to System Date");
                DueDate.value = "";
                DueDate.focus();
                return false;
            }
        }       
    }

    if (id == "2") {
        var Status = "Are you Sure to Submit the E Credit Application Form";

        if (confirm(Status))
            return true;
        else
            return false;
    }
}

function fnSubmitFormHO(id) {

    var ddlApplicationNo = document.getElementById(CtrlIdPrefix + "ddlApplicationNo");
    if (ddlApplicationNo.value.trim() == "0") {
        var ddlBranch = document.getElementById(CtrlIdPrefix + "ddlBranch");
        if (ddlBranch.value.trim() == "") {
            alert("Please select Requesting Branch");
            ddlBranch.focus();
            return false;
        }
    }
    
    if (ddlApplicationNo.value.trim() == "0") {
        alert("Please select Application Number");
        ddlApplicationNo.focus();
        return false;
    }

    //var div1 = document.getElementById(CtrlIdPrefix + "div1");

    //if (div1) {
    //    var txtCustName = document.getElementById(CtrlIdPrefix + "txtCustName");
    //    if (txtCustName.value.trim() == "") {
    //        alert("Please Enter Customer Name");
    //        txtCustName.focus();
    //        return false;
    //    }

    //    var txtAdd1 = document.getElementById(CtrlIdPrefix + "txtAdd1");
    //    var txtAdd2 = document.getElementById(CtrlIdPrefix + "txtAdd2");
    //    var txtAdd3 = document.getElementById(CtrlIdPrefix + "txtAdd3");
    //    var txtAdd4 = document.getElementById(CtrlIdPrefix + "txtAdd4");

    //    if (txtAdd1.value.trim() == "") {
    //        alert("Please Enter Address 1");
    //        txtAdd1.focus();
    //        return false;
    //    }
    //    if (txtAdd2.value.trim() == "") {
    //        alert("Please Enter Address 2");
    //        txtAdd2.focus();
    //        return false;
    //    }

    //    var txtPropName = document.getElementById(CtrlIdPrefix + "txtPropName");
    //    if (txtPropName.value.trim() == "") {
    //        alert("Please Enter Proprietor/ Director/ Partner/ Authorised Person Name");
    //        txtPropName.focus();
    //        return false;
    //    }

    //    var txtPropMobile = document.getElementById(CtrlIdPrefix + "txtPropMobile");
    //    if (txtPropMobile.value.trim() == "") {
    //        alert("Please Enter Proprietor Mobile No.");
    //        txtPropMobile.focus();
    //        return false;
    //    }

    //    var txtContactPersonName = document.getElementById(CtrlIdPrefix + "txtContactPersonName");
    //    if (txtContactPersonName.value.trim() == "") {
    //        alert("Please Enter Contact Person Name");
    //        txtContactPersonName.focus();
    //        return false;
    //    }

    //    var ddlDealerState = document.getElementById(CtrlIdPrefix + "ddlDealerState");
    //    if (ddlDealerState.value.trim() == "") {
    //        alert("Please Select Dealer State");
    //        ddlDealerState.focus();
    //        return false;
    //    }

    //    var txtContactPersonMobNo = document.getElementById(CtrlIdPrefix + "txtContactPersonMobNo");
    //    if (txtContactPersonMobNo.value.trim() == "") {
    //        alert("Please Enter Contact Person Mobile No.");
    //        txtContactPersonMobNo.focus();
    //        return false;
    //    }

    //    var ddlDealerDistrict = document.getElementById(CtrlIdPrefix + "ddlDealerDistrict");
    //    if (ddlDealerDistrict.value.trim() == "") {
    //        alert("Please Select Delaer District");
    //        ddlDealerDistrict.focus();
    //        return false;
    //    }

    //    var txtEmailid = document.getElementById(CtrlIdPrefix + "txtEmailid");
    //    if (txtEmailid.value.trim() == "") {
    //        alert("Please Enter Email id");
    //        txtEmailid.focus();
    //        return false;
    //    }

    //    var ddlDealerTown = document.getElementById(CtrlIdPrefix + "ddlDealerTown");
    //    if (ddlDealerTown.value.trim() == "") {
    //        alert("Please Select Delaer Town");
    //        ddlDealerTown.focus();
    //        return false;
    //    }

    //    var txtLocation = document.getElementById(CtrlIdPrefix + "txtLocation");
    //    if (txtLocation.value.trim() == "") {
    //        alert("Please Enter Location");
    //        txtLocation.focus();
    //        return false;
    //    }

    //    var ddlTownClassification = document.getElementById(CtrlIdPrefix + "ddlTownClassification");
    //    if (ddlTownClassification.value.trim() == "") {
    //        alert("Please Select Dealer Town Location");
    //        ddlTownClassification.focus();
    //        return false;
    //    }

    //    var txtPinCode = document.getElementById(CtrlIdPrefix + "txtPinCode");
    //    if (txtPinCode.value.trim() == "") {
    //        alert("Please Enter PinCode");
    //        txtPinCode.focus();
    //        return false;
    //    }

    //    var ddlFirmType = document.getElementById(CtrlIdPrefix + "ddlFirmType");
    //    if (ddlFirmType.value.trim() == "") {
    //        alert("Please Select Type of Firm");
    //        ddlFirmType.focus();
    //        return false;
    //    }

    //    var ddlRegnType = document.getElementById(CtrlIdPrefix + "ddlRegnType");
    //    if (ddlRegnType.value.trim() == "") {
    //        alert("Please Select Type of Registration");
    //        ddlRegnType.focus();
    //        return false;
    //    }

    //    var ddlAssignedSMRR = document.getElementById(CtrlIdPrefix + "ddlAssignedSMRR");
    //    if (ddlAssignedSMRR.value.trim() == "") {
    //        alert("Please Select Salesman / RR assigned to the Dealer");
    //        ddlAssignedSMRR.focus();
    //        return false;
    //    }

    //    var txtDealerTarget = document.getElementById(CtrlIdPrefix + "txtDealerTarget");
    //    if (txtDealerTarget.value.trim() == "" || txtDealerTarget.value.trim() == "0" || txtDealerTarget.value.trim() == "0.00") {
    //        alert("Please Enter Dealer Target");
    //        txtDealerTarget.focus();
    //        return false;
    //    }
    //}

    //var div2 = document.getElementById(CtrlIdPrefix + "div2");

    //if (div2) {
    //    var ddlDealerClassification = document.getElementById(CtrlIdPrefix + "ddlDealerClassification");
    //    if (ddlDealerClassification.value.trim() == "") {
    //        alert("Please Select Delaer Classification");
    //        ddlDealerClassification.focus();
    //        return false;
    //    }

    //    var ddlDealerSegment = document.getElementById(CtrlIdPrefix + "ddlDealerSegment");
    //    if (ddlDealerSegment.value.trim() == "") {
    //        alert("Please Select Delaer Segment");
    //        ddlDealerSegment.focus();
    //        return false;
    //    }
    //}

    var div3 = document.getElementById(CtrlIdPrefix + "div3");

    if (div3 != null) {
        var OldCrLimit = document.getElementById(CtrlIdPrefix + "txtExistingCrLimit").value.replaceAll(',', '');
        var NewCrLimit = document.getElementById(CtrlIdPrefix + "txtEnhCrLimtReq").value.replaceAll(',', '');
        var CrLimitIndicator = document.getElementById(CtrlIdPrefix + "ddlCrLimitIndicator");
        var ValiditityInd = document.getElementById(CtrlIdPrefix + "ddlValidityIndicator");
        var DueDate = document.getElementById(CtrlIdPrefix + "txtCrlimitDueDate");

        if (OldCrLimit == "")
            OldCrLimit = "0";

        if (NewCrLimit == "")
            NewCrLimit = "0";

        if (parseFloat(OldCrLimit) == parseFloat(NewCrLimit)) {
            alert('There is No Change in the Existing Credit Limit');
        }

        if (CrLimitIndicator.value.trim() == "") {
            alert('Please Select Credit Limit Indicator');
            CrLimitIndicator.focus();
            return false;
        }

        if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "") {
            alert('Please Select Validity Indicator for Credit Limit Change');
            ValiditityInd.focus();
            return false;
        }

        if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T") {
            if (DueDate.value.trim() == "") {
                alert('Please Select Validity Date for Credit Limit Change');
                DueDate.focus();
                return false;
            }

            if (fnIsDate(DueDate.value.trim()) == false) {
                DueDate.focus();
                return false;
            }
        }

        if (parseFloat(OldCrLimit) < parseFloat(NewCrLimit) && ValiditityInd.value.trim() == "T" && DueDate.value.trim() != "") {
            var oSysDate = new Date();
            var oDate = DueDate.value.trim().split("/");

            var oCurDateFormatted = (oSysDate.getMonth() + 1) + '/' + oSysDate.getDate() + '/' + oSysDate.getFullYear();
            var oDateFormatted = oDate[1] + "/" + oDate[0] + "/" + oDate[2];

            if (new Date(oCurDateFormatted) > new Date(oDateFormatted)) {
                alert("Validity Date should be greater than or equal to System Date");
                DueDate.value = "";
                DueDate.focus();
                return false;
            }
        }
    }

    var div5 = document.getElementById(CtrlIdPrefix + "div5");

    if (div5 != null) {
        var EnhCrLimit = document.getElementById(CtrlIdPrefix + "txtEnhCrLimtReq");
        var ApprCrLimit = document.getElementById(CtrlIdPrefix + "txtApprovedCrLimit");

        if (parseFloat(ApprCrLimit.value.replaceAll(',', '')) > parseFloat(EnhCrLimit.value.replaceAll(',', ''))) {
            alert('Approved Credit Limit should not exceed Requested Credit Limit');
            ApprCrLimit.focus();
            return false;
        }
    }

    if (id == "2") {
        var Status = "Are you Sure to Approve the E Credit Application Form";

        if (confirm(Status))
            return true;
        else
            return false;
    }
}

function fnEcreditFormReject() {
    var Remarks = prompt("Are you Sure to Reject this E Credit Form? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
}

function fnEcreditFormWithDraw() {
    var Remarks = prompt("Are you Sure to Withdraw this E Credit Form? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
}