var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_GridView1_ctl02_";

// START NEighboring Branch

function funMaxlimit(sender, args) {
    // var gridview = document.getElementById("<%=grdNeighboringBranch.ClientID%>");

    var gridview = document.getElementById("ctl00_CPHDetails_grdNeighboringBranch");
    var txt = gridview.getElementsByTagName("input");
    for (i = 0; i < txt.length; i++) {
        if (txt[i].id.indexOf("txtFreight") != -1) {
            if (txt[i].value > 1000) {
                args.IsValid = false;

                // return false;
                break;
            }
            else {
                // args.IsValid = true;
                // return true;
                // break;
            }
        }
    }
}

function funPriorityMaxlimit(sender, args) {
    var maxValue;
    var gridview = document.getElementById("ctl00_CPHDetails_grdNeighboringBranch");

    var txt = gridview.getElementsByTagName("input");

    for (i = 0; i < txt.length; i++) {
        if (txt[i].id.indexOf("txtPriority") != -1) {
            if (txt[i].value > 99) {
                args.IsValid = false;
                // return false;
                break;
            }
            else {
                //  args.IsValid = true;
                // return true;
                //  break;
            }
        }
    }
}

function funPriorityDuplication() {
    var maxValue;
    var varPriority1 = new Array();
    var NewArray = new Array();
    var gridview = document.getElementById("ctl00_CPHDetails_grdNeighboringBranch");

    var txt = gridview.getElementsByTagName("input");
    for (i = 0; i < txt.length; i++) {
        if (txt[i].id.indexOf("txtPriority") != -1) {
            if (txt[i].value !== 0)
                varPriority1.push(txt[i].value);
            //  var varPriority2 = txt[i].id.indexOf("txtPriority") 
        }
    }

    varPriority1.sort();
    //   NewArray = varPriority1;
    for (var i = 0; i < varPriority1.length; i++) {
        if (varPriority1[i] == "0") {
            delete varPriority1[i];
            varPriority1.splice(i, 1);
            i = 0;
            // deleteFromArray(NewArray, i);
        }
    }


    NewArray = varPriority1;
    NewArray.sort();
    for (var i = 0; i < NewArray.length - 1; i++) {
        if (NewArray[i] == NewArray[i + 1]) {

            alert("Duplicate value in the Priority");
            return false;
        }
    }

    return true;
}

function deleteFromArray(array, indexToDelete) {
    var remain = new Array();
    for (var i in array) {
        if (array[i] == indexToDelete) {
            continue;
        }
        remain.push(array[i]);
    }
    return remain;
}


// END NEighboring Branch


// Start Stock Adjustment

function funValidateTagNumber() {
    var ddlTagnumber = document.getElementById(CtrlIdPrefix + "ddlTagnumber");
    var txtTagnumber = document.getElementById(CtrlIdPrefix + "txtTagnumber");

    if (ddlTagnumber != undefined) {
        var Tagnumber = ddlTagnumber.options[ddlTagnumber.selectedIndex].value;
    }

    if (txtTagnumber != undefined) {
        if (txtTagnumber.value == "") {
            alert('Please enter a Tag Number.');
            txtTagnumber.focus();
            return false;
        }
    }

    if (ddlTagnumber != undefined) {
        if (Tagnumber == "0") {
            alert('Please select a Tag Number.');
            ddlTagnumber.focus();
            return false;
        }
    }
}

function funStockAdjustmentBtnSubmit() {

    var ddlTagnumber = document.getElementById(CtrlIdPrefix + "ddlTagnumber");
    var txtTagnumber = document.getElementById(CtrlIdPrefix + "txtTagnumber");
    var ddlRemarks = document.getElementById(CtrlIdPrefix + "ddlRemarks");
    if (ddlTagnumber != undefined) {
        var Tagnumber = ddlTagnumber.options[ddlTagnumber.selectedIndex].value;
    }

    if (txtTagnumber != undefined) {
        if (txtTagnumber.value == "") {
            alert('Please enter a Tag Number.');
            txtTagnumber.focus();
            return false;
        }
    }

    if (ddlTagnumber != undefined) {
        if (Tagnumber == "0") {
            alert('Please select a Tag Number.');
            ddlTagnumber.focus();
            return false;
        }
    }

    var txtPhysicalVerificationBy = document.getElementById(CtrlIdPrefix + "txtPhysicalVerificationBy");

    if (txtPhysicalVerificationBy != undefined) {
        if (txtPhysicalVerificationBy.value == "") {
            alert('Please enter Physical Verification By.');
            txtPhysicalVerificationBy.focus();
            return false;
        }
    }

    var txtPhysicalBalanceDate = document.getElementById(CtrlIdPrefix + "txtPhysicalBalanceDate");

    if (txtPhysicalBalanceDate != undefined) {
        if (txtPhysicalBalanceDate.value == "") {
            alert('Please enter  Physical Balance Date.');
            txtPhysicalBalanceDate.focus();
            return false;
        }
    }

    var txtApprovedBy = document.getElementById(CtrlIdPrefix + "txtApprovedBy");

    if (txtApprovedBy != undefined) {
        if (txtApprovedBy.value == "") {
            alert('Please enter  Approved By.');
            txtApprovedBy.focus();
            return false;
        }
    }

    var txtPhysicalBalance = document.getElementById(CtrlIdPrefix + "txtPhysicalBalance");

    if (txtPhysicalBalance != undefined) {
        if (txtPhysicalBalance.value == "") {
            alert('Please enter Physical Balance.');
            txtPhysicalBalance.focus();
            return false;
        }
    }

    if (ddlRemarks != undefined) {
        if (ddlRemarks.value == "") {
            alert('Please select a Remarks.');
            ddlRemarks.focus();
            return false;
        }
    }

    var txtLocation = document.getElementById(CtrlIdPrefix + "txtLocation");

    if (txtLocation != undefined) {
        if (txtLocation.value == "") {
            alert('Please enter Location Code.');
            txtLocation.focus();
            return false;
        }
    }

    if (txtLocation != undefined) {
        if (txtLocation.value.length < 12) {
            alert('Location Code Should be 12 Characters.');
            txtLocation.focus();
            return false;
        }
    }

    var txtWHRefNo_Reason = document.getElementById(CtrlIdPrefix + "txtWHRefNo_Reason");

    if (txtWHRefNo_Reason != undefined) {
        if (txtWHRefNo_Reason.value == "") {
            alert('Please enter  WH RefNo / Reason.');
            txtWHRefNo_Reason.focus();
            return false;
        }
    }

    var txtInvoiceNumber = document.getElementById(CtrlIdPrefix + "txtInvoiceNumber");

    if (txtInvoiceNumber != undefined) {
        if (txtInvoiceNumber.value == "") {
            alert('Please enter Invoice Number.');
            txtInvoiceNumber.focus();
            return false;
        }
    }

    var txtInvoiceDate = document.getElementById(CtrlIdPrefix + "txtInvoiceDate");

    if (txtInvoiceDate != undefined) {
        if (txtInvoiceDate.value == "") {
            alert('Please enter Invoice Number.');
            txtInvoiceDate.focus();
            return false;
        }
    }

    if (!SpecialCharacter()) {
        return false;
    }

    if (!SpecialCharacterPhysicalVerify()) {
        return false;
    }

    if (!FindLength()) {
        return false;
    }

    if (!FindBranch()) {
        return false;
    }

    if (!ComparePhyCurrentDate()) {
        return false;
    }

    if (!ComparePhyTagDate()) {
        return false;
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

function SpecialCharacter() {
    var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
    var txtApprovedBy = document.getElementById(CtrlIdPrefix + 'txtApprovedBy');

    for (var i = 0; i < txtApprovedBy.value.length; i++) {
        if (iChars.indexOf(txtApprovedBy.value.charAt(i)) != -1) {
            alert("Approved By field should not allow special characters.");
            txtApprovedBy.focus();
            return false;
        }

        if (txtApprovedBy.value.charAt(0) == " ") {
            alert("Approved By field should not allow empty space.");
            txtApprovedBy.focus();
            return false;
        }
    }
    return true;
}

function SpecialCharacterPhysicalVerify() {
    var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
    var txtPhysicalVerificationBy = document.getElementById(CtrlIdPrefix + 'txtPhysicalVerificationBy');
    for (var i = 0; i < txtPhysicalVerificationBy.value.length; i++) {
        if (iChars.indexOf(txtPhysicalVerificationBy.value.charAt(i)) != -1) {
            txtPhysicalVerificationBy.focus();
            return false;
        }

        if (txtPhysicalVerificationBy.value.charAt(0) == " ") {
            txtPhysicalVerificationBy.focus();
            return false;
        }
    }
    return true;
}

function FindLength() {

    var txtLocation = document.getElementById(CtrlIdPrefix + 'txtLocation');
    if (parseInt(txtLocation.value.length) < 12) {
        alert("Location length should be 12");
        txtLocation.focus();
        return false;
    }
    return true;
}

function FindBranch() {

    var len_location;
    var br_location;
    var hdn_BranchCode;
    hdn_BranchCode = document.getElementById(CtrlIdPrefix + 'hdnBranchCode');
    len_location = document.getElementById(CtrlIdPrefix + 'txtLocation');
    br_location = len_location.value.substring(0, 3);

    if (br_location.toUpperCase() != hdn_BranchCode.value.toUpperCase()) {
        alert("Location should be of the login branch");
        br_location.focus();
        return false;
    }
    return true;
}

function ComparePhyCurrentDate() {

    var txtEntrydate = document.getElementById(CtrlIdPrefix + "txtEntrydate");
    var txtPhysicalBalanceDate = document.getElementById(CtrlIdPrefix + "txtPhysicalBalanceDate");

    var arrEntryDate = txtEntrydate.value.split("/");
    var useEntryDate = new Date(arrEntryDate[2], arrEntryDate[1] - 1, arrEntryDate[0]);

    var arrPhyDate = txtPhysicalBalanceDate.value.split("/");
    var usePhyDate = new Date(arrPhyDate[2], arrPhyDate[1] - 1, arrPhyDate[0]);


    if (usePhyDate > useEntryDate) {
        alert('Physical Balance Date Should not be Greater than Current Date.');
        txtPhysicalBalanceDate.focus();
        return false;
    }
    return true;
}

function ComparePhyTagDate() {

    var txtTagDate = document.getElementById(CtrlIdPrefix + "txtTagDate");
    var txtPhysicalBalanceDate = document.getElementById(CtrlIdPrefix + "txtPhysicalBalanceDate");

    var arrTagDate = txtTagDate.value.split("/");
    var useTagDate = new Date(arrTagDate[2], arrTagDate[1] - 1, arrTagDate[0]);

    var arrPhyDate = txtPhysicalBalanceDate.value.split("/");
    var usePhyDate = new Date(arrPhyDate[2], arrPhyDate[1] - 1, arrPhyDate[0]);


    if (useTagDate > usePhyDate) {
        alert('Physical Balance Date Should not be less than Tag Date.');
        txtPhysicalBalanceDate.focus();
        return false;
    }
    return true;
}

function EnableAllCheckboxes(id) {
    var chkAllBox = document.getElementById(id);
    var cnt = 0;

    var gridview = document.getElementById("ctl00_CPHDetails_GridView1");
    if (chkAllBox.checked) {
        for (i = 1; i <= gridview.rows.length - 1; i++) {
            var row = gridview.rows[i];
            chkBox = row.cells[0].children[0];
            poQty = row.cells[4].children[0];

            southZoneBranch = row.cells[5].children[0];
            northZoneBranch = row.cells[6].children[0];
            eastZoneBranch = row.cells[7].children[0];
            westZoneBranch = row.cells[8].children[0];
            southZoneQty = row.cells[5].children[1];
            northZoneQty = row.cells[6].children[1];
            eastZoneQty = row.cells[7].children[1];
            westZoneQty = row.cells[8].children[1];
            southZoneAddBtn = row.cells[5].children[3];
            northZoneAddBtn = row.cells[6].children[3];
            eastZoneAddBtn = row.cells[7].children[3];
            westZoneAddBtn = row.cells[8].children[3];
            balQty = row.cells[10].children[0];
            removeBtn = row.cells[10].children[3];
            chkBox.checked = true;
            cnt += 1;
            poQty.disabled = false;

            if (southZoneBranch.length > 1)
                southZoneBranch.disabled = false;

            if (northZoneBranch.length > 1)
                northZoneBranch.disabled = false;

            if (eastZoneBranch.length > 1)
                eastZoneBranch.disabled = false;

            if (westZoneBranch.length > 1)
                westZoneBranch.disabled = false;

            southZoneQty.disabled = false;
            northZoneQty.disabled = false;
            eastZoneQty.disabled = false;
            westZoneQty.disabled = false
            southZoneAddBtn.disabled = false;
            northZoneAddBtn.disabled = false;
            eastZoneAddBtn.disabled = false;
            westZoneAddBtn.disabled = false;
            removeBtn.disabled = false;
        }
    }
    else {
        for (i = 1; i <= gridview.rows.length - 1; i++) {
            var row = gridview.rows[i];
            chkBox = row.cells[0].children[0];
            poQty = row.cells[4].children[0];

            southZoneBranch = row.cells[5].children[0];
            northZoneBranch = row.cells[6].children[0];
            eastZoneBranch = row.cells[7].children[0];
            westZoneBranch = row.cells[8].children[0];
            southZoneQty = row.cells[5].children[1];
            northZoneQty = row.cells[6].children[1];
            eastZoneQty = row.cells[7].children[1];
            westZoneQty = row.cells[8].children[1];
            southZoneAddBtn = row.cells[5].children[3];
            northZoneAddBtn = row.cells[6].children[3];
            eastZoneAddBtn = row.cells[7].children[3];
            westZoneAddBtn = row.cells[8].children[3];
            balQty = row.cells[10].children[0];
            removeBtn = row.cells[10].children[3];
            chkBox.checked = false;

            poQty.disabled = true;
            southZoneBranch.disabled = true;
            northZoneBranch.disabled = true;
            eastZoneBranch.disabled = true;
            westZoneBranch.disabled = true;
            southZoneQty.disabled = true;
            northZoneQty.disabled = true;
            eastZoneQty.disabled = true;
            westZoneQty.disabled = true;
            southZoneAddBtn.disabled = true;
            northZoneAddBtn.disabled = true;
            eastZoneAddBtn.disabled = true;
            westZoneAddBtn.disabled = true;
            removeBtn.disabled = true;
        }
    }

    document.getElementById(CtrlIdPrefix + "hdnRowCnt").value = cnt;

    return true;
}

function EnableDataChanges() {
    var gridview = document.getElementById("ctl00_CPHDetails_GridView1");
    var cnt = 0;
    var balQty = 0;

    for (i = 1; i <= gridview.rows.length - 1; i++) {
        var row = gridview.rows[i];
        chkBox = row.cells[0].children[0];
        poQty = row.cells[4].children[0];

        southZoneBranch = row.cells[5].children[0];
        northZoneBranch = row.cells[6].children[0];
        eastZoneBranch = row.cells[7].children[0];
        westZoneBranch = row.cells[8].children[0];
        southZoneQty = row.cells[5].children[1];
        northZoneQty = row.cells[6].children[1];
        eastZoneQty = row.cells[7].children[1];
        westZoneQty = row.cells[8].children[1];
        southZoneAddBtn = row.cells[5].children[3];
        northZoneAddBtn = row.cells[6].children[3];
        eastZoneAddBtn = row.cells[7].children[3];
        westZoneAddBtn = row.cells[8].children[3];
        balQty = row.cells[10].children[0];
        removeBtn = row.cells[10].children[3];

        if (chkBox.checked) {
            cnt += 1;
            poQty.disabled = false;

            if (southZoneBranch.length > 1)
                southZoneBranch.disabled = false;

            if (northZoneBranch.length > 1)
                northZoneBranch.disabled = false;

            if (eastZoneBranch.length > 1)
                eastZoneBranch.disabled = false;

            if (westZoneBranch.length > 1)
                westZoneBranch.disabled = false;

            southZoneQty.disabled = false;
            northZoneQty.disabled = false;
            eastZoneQty.disabled = false;
            westZoneQty.disabled = false
            southZoneAddBtn.disabled = false;
            northZoneAddBtn.disabled = false;
            eastZoneAddBtn.disabled = false;
            westZoneAddBtn.disabled = false;
            removeBtn.disabled = false;
        }
        else {
            poQty.disabled = true;
            southZoneBranch.disabled = true;
            northZoneBranch.disabled = true;
            eastZoneBranch.disabled = true;
            westZoneBranch.disabled = true;
            southZoneQty.disabled = true;
            northZoneQty.disabled = true;
            eastZoneQty.disabled = true;
            westZoneQty.disabled = true;
            southZoneAddBtn.disabled = true;
            northZoneAddBtn.disabled = true;
            eastZoneAddBtn.disabled = true;
            westZoneAddBtn.disabled = true;
            removeBtn.disabled = true;
        }
    }

    document.getElementById(CtrlIdPrefix + "hdnRowCnt").value = cnt;

    return true;
}

function CheckQty(id) {
    var gridview = document.getElementById("ctl00_CPHDetails_GridView1");
    var Qty = document.getElementById(id);
    var rowCtlId = id.split("txtQty");
    var poQty = document.getElementById(rowCtlId[0] + "txtOrderItem_PO_Quantity");
    var Brch = document.getElementById(id.replace("txtQty", "ddlBranches"));

//    if (poQty.value.trim() == "" || poQty.value.trim() == "0") {
//        alert("Please enter a valid PO Quantity");
//        Brch.focus();
//        return false;
//    }

//    if (Brch.value.trim() == "" || Brch.value.trim() == "0") {
//        alert("Please select an Indent branch");
//        Brch.focus();
//        return false;
//    }

//    if (Qty.value.trim() == "" || Qty.value.trim() == "0") {
//        alert("Please enter a valid Indent quantity");
//        return false;
//    }

//    if (parseInt(Qty.value) > parseInt(poQty.value)) {
//        alert("Indent Quantity should not exceed PO Quantity");
//        return false;
//    }

    return true;
}

function fnAddListitem(id) {
    var gridview = document.getElementById("ctl00_CPHDetails_GridView1");
    var Brch = document.getElementById(id.replace("btnAdd", "ddlBranches"));
    var Qty = document.getElementById(id.replace("btnAdd", "txtQty"));
    var rowCtlId = id.split("btnAdd");
    var poQty = document.getElementById(rowCtlId[0] + "txtOrderItem_PO_Quantity");
    var listBox = document.getElementById(rowCtlId[0] + "ddlListIndentBranches");
    var txtbalQty = document.getElementById(rowCtlId[0] + "txtQtyBalance");
    var balQty;

    if (poQty.value.trim() == "" || poQty.value.trim() == "0") {
        alert("Please enter a valid PO Quantity");
        poQty.focus();
        return false;
    }

    if (Brch.value.trim() == "" || Brch.value.trim() == "0") {
        alert("Please select an Indent branch");
        Brch.focus();
        return false;
    }

    if (Qty.value.trim() == "" || Qty.value.trim() == "0") {
        alert("Please enter a valid Indent quantity");
        Qty.focus();
        return false;
    }    

    if (parseInt(Qty.value) > parseInt(poQty.value)) {
        alert("Indent Quantity should not exceed PO Quantity");
        Qty.focus();
        return false;
    }

    BrchName = Brch.options[Brch.selectedIndex].text.split(" - ");
    var val = 0;

    for (i = 0; i < listBox.length; i++) {
        listItem = listBox.options[i].text;
        QtyItem = listItem.split(" - ");
        
        if (QtyItem[0].trim().toUpperCase() == BrchName[0].trim().toUpperCase()) {
            alert("Indent Branch is already added. Please select a different Branch");
            Brch.focus();
            return false;
        }

        val = parseInt(val) + parseInt(QtyItem[1]);
    }

    if (parseInt(val) == parseInt(poQty.value)) {
        Brch.selectedIndex = 0;
        Qty.value = "";
        alert("Added Indent Quantity already matches with the PO Quantity. Please submit and process the STDN");
        return false;
    }

    totVal = parseInt(val) + parseInt(Qty.value);

    if (parseInt(totVal) > parseInt(poQty.value)) {
        alert("Added Indent Quantity should not exceed PO Quantity. Please Adjust the Quantity or Remove few Branches");
        Qty.focus();
        return false;
    }

    document.getElementById(rowCtlId[0] + "hdnTotalIndentQty").value = val;
    txtbalQty.value = parseInt(poQty.value) - (parseInt(Qty.value) + parseInt(val));

    document.getElementById(CtrlIdPrefix + "BtnSubmit").disabled = false;
    return true;
}

function fnRemoveListitem(id) {
    var gridview = document.getElementById("ctl00_CPHDetails_GridView1");
    var Qty = document.getElementById(id.replace("btnRemove", "txtQty"));
    var rowCtlId = id.split("btnRemove");
    var poQty = document.getElementById(rowCtlId[0] + "txtOrderItem_PO_Quantity");
    var listBox = document.getElementById(rowCtlId[0] + "ddlListIndentBranches");
    var txtbalQty = document.getElementById(rowCtlId[0] + "txtQtyBalance");

    var val = 0;
    var cnt = 0;

    if (listBox.length <= 0) {
        alert("No Branch Item Exists in the list");
        return false;
    }
    else {
        for (i = 0; i < listBox.length; i++) {
            listItem = listBox.options[i].text;
            QtyItem = listItem.split(" - ");            
            val = parseInt(val) + parseInt(QtyItem[1]);

            if (listBox.options[i].selected) {
                cnt += 1;
            }
        }

        SelQty = listBox.options[listBox.selectedIndex].text.split(" - ");
        val = parseInt(val) - parseInt(SelQty[1]);
    }

    if (cnt <= 0) {
        alert("Please select an Item to delete");
        listBox.focus();
        return false;
    }

    document.getElementById(rowCtlId[0] + "hdnTotalIndentQty").value = val;
    txtbalQty.value = parseInt(poQty.value) - parseInt(val);

    document.getElementById(CtrlIdPrefix + "BtnSubmit").disabled = false;
    return true;
}

function funDirectPOBtnSubmitRow() {
    if (DirectPOHeaderValidation()) {

        document.getElementById(CtrlIdPrefix + 'ddlOrdTransactionType').disabled = true;
        document.getElementById(CtrlIdPrefix + 'ddlOrdSupplier').disabled = true;

        var ddlOrdBranchName = document.getElementById(CtrlIdPrefix + "ddlOrdBranchName");
        var objCustomer = document.getElementById(CtrlIdPrefix + 'ddlOrdCustomer');

        if (ddlOrdBranchName != undefined) {
            ddlOrdBranchName.disabled = true;
        }

        if (objCustomer != undefined) {
            objCustomer.disabled = true;
        }

        if (!GridCheck("BtnSubmit")) {
            return false;
        }

        var Status = "Are you Sure to Process the PO Order";

        if (confirm(Status))
            return true;
        else
            return false;
    }
    else
        return false;

}

function DirectPOValidSupllier() {
    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlOrdTransactionType");
    var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;
    var ddlSupp = document.getElementById(CtrlIdPrefix + "ddlOrdSupplier");
    var Supplier = ddlSupp.options[ddlSupp.selectedIndex].value;
    var BranchName = document.getElementById(CtrlIdPrefix + "txtOrdBranch").value;

    if (!(BranchName == "MORI GATE" || BranchName == "CALCUTTA" || BranchName == "DEHARADUN" || TransType == "202"))
    {
        if (!(TransType == "0" || Supplier == "0")) {
            if (Supplier == "182" || Supplier == "230" || Supplier == "210" || Supplier == "300" || Supplier == "620" || 
                Supplier == "790" || Supplier == "830" || Supplier == "360" || Supplier == "400") {
                ddlSupp.options[ddlSupp.selectedIndex].value = "0";
                ddlSupp.options[ddlSupp.options[ddlSupp.selectedIndex].value].selected = true;
                alert("Transaction Not Allowed For This Supplier");
                return false;
            }
            else {
                if (!(TransType == "451" || TransType == "452")) {
											
                    if (!(Supplier == "220" || Supplier == "550" || Supplier == "390" || Supplier == "391" || Supplier == "320" ||
                          Supplier == "410" || Supplier == "392" || Supplier == "630")) {
                        ddlSupp.options[ddlSupp.selectedIndex].value = "0";
                        ddlSupp.options[ddlSupp.options[ddlSupp.selectedIndex].value].selected = true;
                        alert("Transaction Not Allowed For This Supplier");
                        return false;
                    }
                }           
            }
        }
    }
    else
    {
//        __doPostBack(CtrlIdPrefix + "ddlOrdSupplier", null);
//        return true;
    }
    
    return true;
}

function DirectPOHeaderValidation() {
    var ddlPONumber = document.getElementById(CtrlIdPrefix + "ddlOrd_PONumber");
    var ddlTransType = document.getElementById(CtrlIdPrefix + "ddlOrdTransactionType");
    var ddlSupp = document.getElementById(CtrlIdPrefix + "ddlOrdSupplier");
    var ddlCust = document.getElementById(CtrlIdPrefix + "ddlOrdCustomer");
    var ddlOrdBranchName = document.getElementById(CtrlIdPrefix + "ddlOrdBranchName");
	var txtOrdRefIndentNumber = document.getElementById(CtrlIdPrefix + "txtOrdRefIndentNumber");
    var txtOrdRdPermitNumber = document.getElementById(CtrlIdPrefix + "txtOrdRdPermitNumber");
    var txtOrdCarrier = document.getElementById(CtrlIdPrefix + "txtOrdCarrier");
    var txtOrdDestination = document.getElementById(CtrlIdPrefix + "txtOrdDestination");
    var txtOrdCarrierInfoRemarks = document.getElementById(CtrlIdPrefix + "txtOrdCarrierInfoRemarks");																				
    if (ddlPONumber != undefined) {
        var PONumber = ddlPONumber.options[ddlPONumber.selectedIndex].value;
        if (PONumber == "0") {
            alert('Please select a Number.');
            ddlPONumber.focus();
            return false;
        }
    }

    if (ddlTransType != undefined) {
        var TransType = ddlTransType.options[ddlTransType.selectedIndex].value;
        if (TransType == "0") {
            alert('Please select a Transaction Type.');
            ddlTransType.focus();
            return false;
        }
    }

    if (ddlSupp != undefined) {
        var Supplier = ddlSupp.options[ddlSupp.selectedIndex].value;
        if (Supplier == "0") {
            alert('Please select a Supplier.');
            ddlSupp.focus();
            return false;
        }
    }

    if (ddlCust != undefined) {
        var Customer = ddlCust.options[ddlCust.selectedIndex].value;
        if (Customer == "0") {
            alert('Please select a Customer.');
            ddlCust.focus();
            return false;
        }
    }

    if (ddlOrdBranchName != undefined) {
        var Branch = ddlOrdBranchName.options[ddlOrdBranchName.selectedIndex].value;
        if (Branch == "0") {
            alert('Please select a Branch.');
            ddlOrdBranchName.focus();
            return false;
        }
    }

	if (txtOrdRefIndentNumber.value != "") {
        var stat = validatespl(txtOrdRefIndentNumber.value, "Order Reference Indent Number");
        if (stat == "No") {
            txtOrdRefIndentNumber.focus();
            return false
        }
    }

    if (txtOrdRdPermitNumber.value != "") {
        var stat = validatespl(txtOrdRdPermitNumber.value, "Order Road Permit Number");
        if (stat == "No") {
            txtOrdRdPermitNumber.focus();
            return false
        }
    }

    if (txtOrdCarrier.value != "") {
        var stat = validatespl(txtOrdCarrier.value, "Order Carrier");
        if (stat == "No") {
            txtOrdCarrier.focus();
            return false
        }
    }

    if (txtOrdDestination.value != "") {
        var stat = validatespl(txtOrdDestination.value, "Order Destination");
        if (stat == "No") {
            txtOrdDestination.focus();
            return false
        }
    }

    if (txtOrdCarrierInfoRemarks.value != "") {
        var stat = validatespl(txtOrdCarrierInfoRemarks.value, "Order Carrier Info Remarks");
        if (stat == "No") {
            txtOrdCarrierInfoRemarks.focus();
            return false
        }
    }
	
    return true;
}

function GridCheck(btn) {
    var iRowCount;
    iRowCount = document.getElementById(CtrlIdPrefix + "hdnRowCnt").value.trim();

    if (iRowCount > 0) {
        var gridview = document.getElementById("ctl00_CPHDetails_GridView1");
        var txt = gridview.getElementsByTagName("input");

        if (btn == "BtnSearch") {
            for (i = 0; i < txt.length; i++) {
                if (txt[i].id.indexOf("txtOrdSupplierPartNo") != -1) {
                    if (txt[i].value == "") {
                        txt[i].focus();
                        alert("Please enter the Supplier Part No");
                        return false;
                        break;
                    }
                }
            }            
        }
        else {
            for (k = 0; k < txt.length; k++) {

                if (txt[k].id.indexOf("txtOrdSupplierPartNo") != -1) {
                    if (txt[k].value == "") {
                        txt[k].focus();
                        alert("Please enter the Supplier Part No");
                        return false;
                        break;
                    }
                }

                if (txt[k].id.indexOf("txtOrdPackingQuantity") != -1) {
                    if (txt[k].value == "") {
                        txt[k].focus();
                        alert("Please enter the Supplier Part No");
                        return false;
                        break;
                    }
                }
                
                if (txt[k].id.indexOf("txtOrdItemCode") != -1) {
                    if (txt[k].value == "") {
                        txt[k].focus();
                        alert("Please enter the Item code");
                        return false;
                        break;
                    }

                    var maxValue;
                    var varPriority1 = new Array();
                    var NewArray = new Array();


                    var txt = gridview.getElementsByTagName("input");
                    for (i = 0; i < txt.length; i++) {
                        if (txt[i].id.indexOf("txtOrdItemCode") != -1) {
                            if (txt[i].value !== 0)
                                varPriority1.push(txt[i].value);
                        }
                    }

                    varPriority1.sort();

                    NewArray = varPriority1;
                    NewArray.sort();
                    for (var i = 0; i < NewArray.length - 1; i++) {
                        if (NewArray[i] == NewArray[i + 1]) {
                            alert("Duplicate value in the ItemCode");
                            return false;
                        }
                    }
                }

                if (txt[k].id.indexOf("txtOrderItem_PO_Quantity") != -1) {
                    if (txt[k].value == "") {
                        txt[k].focus();
                        alert("Please enter the  Item's Order Quantity.");
                        return false;
                        break;
                    }
                }

                if (txt[k].id.indexOf("txtOrderItem_PO_Quantity") != -1) {
                    if (parseInt(txt[k].value) == 0) {
                        txt[k].focus();
                        alert("Item PO Quantity should not have Zero.");
                        return false;
                        break;
                    }
                }
                
                if (txt[k].id.indexOf("txtValid_Days") != -1) {
                    if (txt[k].value == "") {
                        txt[k].focus();
                        alert("Please enter the Valid Days.");
                        return false;
                        break;
                    }
                }

                if (txt[k].id.indexOf("txtValid_Days") != -1) {
                    if (parseInt(txt[k].value) >= 100) {
                        txt[k].focus();
                        alert("Valid Days cannot exceeds 99.");
                        return false;
                        break;
                    }
                }
            }
        }

        return true;
    }

    if (btn == "BtnAdd") {

        if (iRowCount > 0)
            return false;
        else
            return true;
    }
    else if (btn == "BtnSubmit") {
        if (iRowCount > 0)
            return true;
        else {
            alert("Please add Item details.");
            return false;
        }
    }
}

function IntegerValueOnlyWithoutZero() {
    var AsciiValue = event.keyCode
    if ((AsciiValue > 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
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

function IntegerValueWithDot() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function IntegerValueOnlyWithSearch() {
    var AsciiValue = event.keyCode
    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue == 37))
        event.returnValue = true;
    else
        event.returnValue = false;
}

function CurrencyChkForMoreThanOneDot(strValue) {
    //alert('asd');
    //alert(strValue.split('.').length);
    if (strValue.split('.').length > 2)
        return false;
    else
        return true;
}

function resetAction() {
    var poNumber = document.getElementById(CtrlIdPrefix + "txtOrd_PONumber");

    if (poNumber != null) {
        return true;
    }

    var result = confirm("Are you want to lose the changes?");
    if (result == true) {
        return true;
    }
    return false;
}

// END Direct PO

// START ST Form Collection

function funSTSubmitValidation() {

    var ddlFormName = $get(CtrlIdPrefix + "ddlFormName");
    var txtFormName = $get(CtrlIdPrefix + "txtFormName");

    var ddlSupplierCustomer = $get(CtrlIdPrefix + "ddlSupplierCustomer");
    var txtSupplierCustomer = $get(CtrlIdPrefix + "txtSupplierCustomer");

    var ddlReferenceDocument = $get(CtrlIdPrefix + "ddlReferenceDocument");
    var txtReferenceDocument = $get(CtrlIdPrefix + "txtReferenceDocument");
    var txtFormNumber = $get(CtrlIdPrefix + "txtFormNumber");
    var txtFormDate = $get(CtrlIdPrefix + "txtFormDate");

    if (txtFormName != undefined) {
        if (txtFormName.value == "") {
            alert('Please enter Form Name.');
            txtFormName.focus();
            return false;
        }
    }

    if (ddlFormName != undefined) {
        var FormName = ddlFormName.options[ddlFormName.selectedIndex].value;
        if (FormName == "0") {
            alert('Please select a Form Name.');
            ddlFormName.focus();
            return false;
        }
    }

    if (ddlSupplierCustomer != undefined) {
        var SupplierCustomer = ddlSupplierCustomer.options[ddlSupplierCustomer.selectedIndex].value;
        if (SupplierCustomer == "0") {
            alert('Please select a Supplier / Customer.');
            ddlSupplierCustomer.focus();
            return false;
        }
    }

    if (txtSupplierCustomer != undefined) {
        if (txtSupplierCustomer.value == "") {
            alert('Please select a Supplier / Customer.');
            txtSupplierCustomer.focus();
            return false;
        }
    }

    if (ddlReferenceDocument != undefined) {
        var ReferenceDocument = ddlReferenceDocument.options[ddlReferenceDocument.selectedIndex].value;
        if (ReferenceDocument == "0") {
            alert('Please select a Reference Document.');
            ddlReferenceDocument.focus();
            return false;
        }
    }

    if (txtReferenceDocument != undefined) {
        if (txtReferenceDocument.value == "") {
            alert('Please select a Reference Document.');
            txtReferenceDocument.focus();
            return false;
        }
    }

    if (txtFormNumber != undefined) {
        if (txtFormNumber.value == "") {
            alert('Please select a Form Number.');
            txtFormNumber.focus();
            return false;
        }
    }

    if (txtFormDate != undefined) {
        if (txtFormDate.value == "") {
            alert('Please select a Form Date.');
            txtFormDate.focus();
            return false;
        }
    }
}

function FromCheckDate(sender, args) {
    var hdnSystemDate = $get(CtrlIdPrefix + "hdnSystemDate");
    var txtFormDate = $get(CtrlIdPrefix + "txtFormDate");

    var arrHdnDate = hdnSystemDate.value.split("/");
    var useHdnDate = new Date(arrHdnDate[2], arrHdnDate[1] - 1, arrHdnDate[0]);

    var arrCalDate = txtFormDate.value.split("/");
    var useCalDate = new Date(arrCalDate[2], arrCalDate[1] - 1, arrCalDate[0]);


    if (useCalDate > useHdnDate) {
        alert('Form Date Should not be Greater than Current Date.');
        sender._textbox.set_Value("");
        return false;
    }
    return true;
}

function TriggerCalender(CalenderCtrlClientId) {
    //alert('hi');
    var CtrlID = CtrlIdPrefix + CalenderCtrlClientId;
    document.getElementById(CtrlID).click();
}
// End ST Form Collection

// Start Stock Diversion


function funStockDiversionSubmitValidation() {
    var ddlFromTransactionType = $get(CtrlIdPrefix + "ddlFromTransactionType");
    var ddlToTransactionType = $get(CtrlIdPrefix + "ddlToTransactionType");
    var ddlInwardNumber = $get(CtrlIdPrefix + "ddlInwardNumber");
    var ddlSerailNo = $get(CtrlIdPrefix + "ddlSerailNo");
    var txtDiversionNumber = $get(CtrlIdPrefix + "txtDiversionNumber");
    var txtItemCode = $get(CtrlIdPrefix + "txtItemCode");
    var txtQuantity = $get(CtrlIdPrefix + "txtQuantity");
    var HdnOrgQuantity = $get(CtrlIdPrefix + "HdnOrgQuantity");


    if (ddlSerailNo != undefined) {
        var SerailNo = ddlSerailNo.options[ddlSerailNo.selectedIndex].value;
    }

    if (ddlFromTransactionType != undefined) {
        var FromTransactionType = ddlFromTransactionType.options[ddlFromTransactionType.selectedIndex].value;
        if (FromTransactionType == "0") {
            alert('Please select a From Transaction Type.');
            ddlFromTransactionType.focus();
            return false;
        }
    }

    if (ddlToTransactionType != undefined) {
        var ToTransactionType = ddlToTransactionType.options[ddlToTransactionType.selectedIndex].value;
        if (ToTransactionType == "0") {
            alert('Please select a To Transaction Type.');
            ddlToTransactionType.focus();
            return false;
        }
    }

    if (txtItemCode.value == "") {
        alert('Please enter Item code.');
        txtItemCode.focus();
        return false;
    }

    if (ddlInwardNumber != undefined) {
        var InwardNumber = ddlInwardNumber.options[ddlInwardNumber.selectedIndex].value;
        if (InwardNumber == "0") {
            alert('Please select a Inward Number.');
            ddlInwardNumber.focus();
            return false;
        }
    }

    if (ddlSerailNo != undefined) {
        if (SerailNo == "0") {
            alert('Please select a Serial No.');
            ddlSerailNo.focus();
            return false;
        }
    }

    if (txtQuantity.value == "") {
        alert('Please enter Quantity.');
        txtQuantity.focus();
        return false;
    }

    if (txtDiversionNumber != undefined) {
        if (parseInt(txtQuantity.value) > parseInt(HdnOrgQuantity.value)) {
            alert('Quantity cannot be more than : ' + HdnOrgQuantity.value);
            txtQuantity.focus();
            return false;
        }
    }
}


// End Stock Diversion

// Start OS LS Updation

function funOSLSSubmitValidation() {
    var ddlUpdationType = $get(CtrlIdPrefix + "ddlUpdationType");
    var ddlInward_STDNNo = $get(CtrlIdPrefix + "ddlInward_STDNNo");
    var ddlSerialNo = $get(CtrlIdPrefix + "ddlSerialNo");
    var ddlSupplierPartNo = $get(CtrlIdPrefix + "ddlSupplierPartNo");
    var ddlOS_LS = $get(CtrlIdPrefix + "ddlOS_LS");

    if (ddlUpdationType != undefined) {
        var UpdationType = ddlUpdationType.options[ddlUpdationType.selectedIndex].value;
        if (UpdationType == "0") {
            alert('Please select a Updation Type.');
            ddlUpdationType.focus();
            return false;
        }
    }

    if (ddlInward_STDNNo != undefined) {
        var Inward_STDNNo = ddlInward_STDNNo.options[ddlInward_STDNNo.selectedIndex].value;
        if (Inward_STDNNo == "0") {
            alert('Please select a Inward / STDN Number.');
            ddlInward_STDNNo.focus();
            return false;
        }
    }

    if (ddlSerialNo != undefined) {
        var SerialNo = ddlSerialNo.options[ddlSerialNo.selectedIndex].value;
        if (SerialNo == "0") {
            alert('Please select a Serial Number.');
            ddlSerialNo.focus();
            return false;
        }
    }

    if (ddlSupplierPartNo != undefined) {
        var SupplierPartNo = ddlSupplierPartNo.options[ddlSupplierPartNo.selectedIndex].value;
        if (SupplierPartNo == "0") {
            alert('Please select a Supplier Part Number.');
            ddlSupplierPartNo.focus();
            return false;
        }
    }

    if (ddlOS_LS != undefined) {
        var OS_LS = ddlOS_LS.options[ddlOS_LS.selectedIndex].value;
        if (OS_LS == "0") {
            alert('Please select a OS / LS Type.');
            ddlOS_LS.focus();
            return false;
        }
    }
}

// End OS LS Updation

function checkDate(sender, args) {
    var strSender = sender._id;
    var idRefDocDate = strSender.replace("RefIndDateCalendarExtender", "txtOrdRefIndentDate");

    if (sender._selectedDate > new Date()) {
        alert("Order Reference Indent Date should not be greater than System Date");

        if ("ctl00_CPHDetails_ceRequDocDate" == strSender) {
            document.getElementById("ctl00_CPHDetails_txtOrdRefIndentDate").value = "";
        }
        else {
            document.getElementById(idRefDocDate).value = "";
        }
    }
}

function checkRoadPermitDate(sender, args) {
    var strSender = sender._id;
    var idRefDocDate = strSender.replace("RefRdPermitDateCalendarExtender", "txtOrdRdPermitDate");

    if (sender._selectedDate > new Date()) {
        alert("Order Road Permit Date should not be greater than System Date");

        if ("ctl00_CPHDetails_ceRequDocDate" == strSender) {
            document.getElementById("ctl00_CPHDetails_txtOrdRdPermitDate").value = "";
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


//function CheckValidDate(id, isFutureDate, column) {

//    var idDate = document.getElementById(id).value;

//    if (idDate != '') {
//        var status = fnIsDate(idDate);

//        if (!status) {
//            document.getElementById(id).value = "";
//            document.getElementById(id).focus();
//        }
//        else {

//            if (isFutureDate == true) {
//                var fDate = idDate.split("/");
//                var day = parseInt(fDate[0]);
//                var month = parseInt(fDate[1] - 1);
//                var year = parseInt(fDate[2]);

//                var frmDate = new Date();
//                frmDate.setDate(day);
//                frmDate.setMonth(month);
//                frmDate.setFullYear(year);

//                if (frmDate > new Date()) {
//                    alert(column + "should not be greater than Today's Date");
//                    document.getElementById(id).value = "";
//                    document.getElementById(id).focus();
//                }
//            }

//        }

//    }
//}


function checkstackFutureDate(sender, args) {
    //alert('asdad');
    var dtDate = new Date('dd/MM/yyyy');
    alert(dtDate);
    alert(sender._selectedDate);
    if ((sender._selectedDate < dtDate)) {
        alert('Date should be a future date.');
        //sender._textbox.set_Value(dtDate.format(sender._format));
        sender._textbox.set_Value("");
    }
}

function CheckValidFutureDate() {
    var idDate = document.getElementById(CtrlIdPrefix + "txtPhysicalBalanceDate").value;
    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var fDate = idDate.split("/");
            var day = parseInt(fDate[0]);
            var month = parseInt(fDate[1] - 1);
            var year = parseInt(fDate[2]);

            var frmDate = new Date();
            frmDate.setDate(day);
            frmDate.setMonth(month);
            frmDate.setFullYear(year);
            if (frmDate < new Date()) {
                alert("Date should be greater than or Today's Date");
                document.getElementById(CtrlIdPrefix + "txtPhysicalBalanceDate").value = "";
                document.getElementById(CtrlIdPrefix + "txtPhysicalBalanceDate").focus();
            }
        }
    }
}

function isspecialchar(c) {
    myArray = ['!', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '`', '~', ']', '[', '|', '@', '/', '"', ':', ';', '{', '}', ',', "'", '.', '?', '\\'];
    for (var j = 0; j < myArray.length; j++) {
        if (c == myArray[j]) {
            return true;
        }
    }
    return false;
}

function validatespl(inpval, fldname) {
    var Stat;
    var firstchr = inpval.substring(0, 1, inpval);
    if (firstchr == " ") {
        alert("First character of " + fldname + " should not be blank");
        return Stat = "No";
    }
    else if (isspecialchar(firstchr)) {
        alert("First character of " + fldname + " should be alphabet or number");
        return Stat = "No";
    }

    for (i = 0; i < inpval.length; i++) {
        firstchr = inpval.substring(i, i + 1, inpval);
        if (firstchr == "") {
            alert("First character of " + fldname + " should not be blank");
            return Stat = "No";
        }
        else if (isspecial1(firstchr)) {
            alert("Characters in " + fldname + " should be alphabet or number");
            return Stat = "No";
        }
    }
    return Stat = "Yes";
}


function DownLoadExcelFile(uid) {
    //window.location.href= "DownloadExcel.aspx?FileName=" + uid;

    var rawData = document.getElementById("ctl00_CPHDetails_hdnJSonExcelData").value;
    //console.log(rawData );
    var excelData = JSON.parse(rawData)[0];
    var createXLSLFormatObj = [];

    /* XLS Head Columns */
    //var xlsHeader = ["EmployeeID", "Full Name"];

    //console.log(Object.keys(excelData[0]));
    createXLSLFormatObj.push(Object.keys(excelData[0]));
    $.each(excelData, function (index, value) {
        var innerRowData = [];

        $.each(value, function (ind, val) {

            innerRowData.push(val);
        });
        //console.log(innerRowData);
        createXLSLFormatObj.push(innerRowData);
    });

    /* File Name */
    var filename = uid;

    /* Sheet Name */
    var ws_name = "Sheet1";

    if (typeof console !== 'undefined') console.log(new Date());
    var wb = XLSX.utils.book_new(),
        ws = XLSX.utils.aoa_to_sheet(createXLSLFormatObj);

    //var range = XLSX.utils.decode_range(ws['!ref']);
    //for (var r = range.s.r; r <= range.e.r; r++) {
    //    //console.log(range.s.r, range.e.r);
    //    for (var c = range.s.c; c <= range.e.c; c++) {
    //        //console.log("---------------------");
    //        //console.log(range.s.c, range.e.c);
    //        //console.log("---------------------");
    //        var cellName = XLSX.utils.encode_cell({ c: c, r: r });
    //        //if (cellName.startsWith('Mate') || cellName.startsWith('PART') || cellName.startsWith('Part') || cellName.startsWith('SuppPart') || cellName.startsWith('OrderDate') || cellName.startsWith('CustOrderDt')) {
    //        //    //console.log(cellName);
    //        //ws[cellName].z = '@';
    //        //}
    //    }
    //}

    /* Add worksheet to workbook */
    XLSX.utils.book_append_sheet(wb, ws, ws_name);

    /* Write workbook and Download */
    //if (typeof console !== 'undefined') console.log(new Date());
    XLSX.writeFile(wb, filename);
    //if (typeof console !== 'undefined') console.log(new Date());
    //alert('Report has been Downloaded Successfully');
}