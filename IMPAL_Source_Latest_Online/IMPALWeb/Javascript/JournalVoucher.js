var CtrlIdPrefix = "ctl00_CPHDetails_";
var CtrlGridRowIdPrefix = "ctl00_CPHDetails_grvItemDetails_ctl02_";

function JournalVoucherValidation() {
    var jvDate = document.getElementById(CtrlIdPrefix + "txtJVDate");
    var referencedocdate = document.getElementById(CtrlIdPrefix + "txtReferenceDocumentDate");
    var referenceDocNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocumentNumber");
    var narration = document.getElementById(CtrlIdPrefix + "txtNarration");
    var noofTransactions = document.getElementById(CtrlIdPrefix + "txtNoofTransactions");

    if (jvDate.value == "") {
        alert("JV Date should not be null");
        jvDate.focus();
        return false;
    }

    if (referencedocdate.value == "") {
        alert("Reference Document Date should not be null");
        referencedocdate.focus();
        return false;
    }

    if (referenceDocNumber.value == "") {
        alert("Reference Document Number should not be null");
        referenceDocNumber.focus();
        return false;
    }

    if (referenceDocNumber.value != "") {
        if (!validatespl(referenceDocNumber.value, "Reference Document Number")) {
        }
        else {
            referenceDocNumber.focus();
            return false
        }
    }

    if (narration.value == "") {
        alert("Narration should not be null");
        narration.focus();
        return false;
    }

    if (!validatespl(narration.value, "Narration")) {
    }
    else {
        narration.focus();
        return false
    }

    if (noofTransactions.value == "") {
        alert("No of Transactions should not be null");
        noofTransactions.focus();
        return false;
    }

    if (noofTransactions.value == "0") {
        alert("No of Transactions should not be Zero");
        noofTransactions.focus();
        return false;
    }

    if (parseInt(noofTransactions.value) == "1") {
        alert("No of Transactions should not be One");
        noofTransactions.focus();
        return false;
    }

    return true;
}

function calculateTotal() {

    var debittotal = 0;
    var credittotal = 0;
    var grd = document.getElementById('ctl00_CPHDetails_grvTransactionDetails');

    for (i = 1; i < grd.rows.length - 1; i++) {
        var node1 = grd.rows[i].cells[8].childNodes[0];
        var node2 = grd.rows[i].cells[9].childNodes[0];
        var browserName = navigator.appName;
        if (browserName == 'Netscape') {
            var node1 = grd.rows[i].cells[8].childNodes[1];
            var node2 = grd.rows[i].cells[9].childNodes[1];
        }

        //-- Debit Total
        if (node1 != undefined && node1.type == "text")
            if (!isNaN(node1.value) && node1.value != "")
                debittotal += parseFloat(node1.value);

        //-- Credit Total
        if (node2 != undefined && node2.type == "text")
            if (!isNaN(node2.value) && node2.value != "")
                credittotal += parseFloat(node2.value);

    }

    var rownum = String(grd.rows.length);

    if (rownum.length == 1)
        rownum = "0" + rownum;

    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalDebitAmount").value = parseFloat(Math.round(debittotal * 100) / 100).toFixed(2);
    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalCreditAmount").value = parseFloat(Math.round(credittotal * 100) / 100).toFixed(2);
}

function calculateTotalFinal() {

    var debittotal = 0;
    var credittotal = 0;
    var grd = document.getElementById('ctl00_CPHDetails_grvTransactionDetails');

    for (i = 1; i < grd.rows.length - 1; i++) {
        chkBox1 = grd.rows[i].cells[0].children[0];

        if (chkBox1.checked) {
            var node1 = grd.rows[i].cells[9].childNodes[0];
            var node2 = grd.rows[i].cells[10].childNodes[0];
            var browserName = navigator.appName;
            if (browserName == 'Netscape') {
                var node1 = grd.rows[i].cells[9].childNodes[1];
                var node2 = grd.rows[i].cells[10].childNodes[1];
            }

            //-- Debit Total
            if (node1 != undefined && node1.type == "text")
                if (!isNaN(node1.value) && node1.value != "")
                    debittotal += parseFloat(node1.value);

            //-- Credit Total
            if (node2 != undefined && node2.type == "text")
                if (!isNaN(node2.value) && node2.value != "")
                    credittotal += parseFloat(node2.value);
        }
    }

    var rownum = String(grd.rows.length);

    if (rownum.length == 1)
        rownum = "0" + rownum;

    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalDebitAmount").value = parseFloat(Math.round(debittotal * 100) / 100).toFixed(2);
    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalCreditAmount").value = parseFloat(Math.round(credittotal * 100) / 100).toFixed(2);
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

function ValidateDrCr(txtddldrcr, txtchart, txtdebit, txtcredit, txtrefdocnumber, txtrefdate) {
    var ddlDrCr = document.getElementById(txtddldrcr).value;
    var chartofaccount = document.getElementById(txtchart).value;
    var refdocnumber = document.getElementById(txtrefdocnumber).value;
    var refdocnumberdate = document.getElementById(txtrefdate).value;

    if (chartofaccount == "") {
        alert('Chart of Account should be selected')
        document.getElementById(txtddldrcr).value = "0";
        return false;
    }

    if (refdocnumber == "") {
        alert('Ref.Doc Number should not be null')
        document.getElementById(txtddldrcr).value = "0";
        document.getElementById(txtrefdocnumber).focus();
        return false;
    }

    if (refdocnumberdate == "") {
        alert('Ref.Doc Date should not be null')
        document.getElementById(txtddldrcr).value = "0";
        document.getElementById(txtrefdate).focus();
        return false;
    }

    var txtRefNo = document.getElementById("ctl00_CPHDetails_txtReferenceDocumentNumber").value;

    if (txtRefNo == refdocnumber) {
        alert('Ref.Doc Number should not be same as Reference Document Number')
        document.getElementById(txtddldrcr).value = "0";
        document.getElementById(txtrefdocnumber).focus();
        return false;
    }

    if (ddlDrCr == "D") {
        document.getElementById(txtdebit).disabled = false;
        document.getElementById(txtcredit).disabled = true;
        document.getElementById(txtdebit).focus();
        document.getElementById(txtcredit).value = "0";
    }
    else if (ddlDrCr == "C") {
        document.getElementById(txtdebit).disabled = true;
        document.getElementById(txtcredit).disabled = false;
        document.getElementById(txtcredit).focus();
        document.getElementById(txtdebit).value = "0";
    }
    else {
        document.getElementById(txtdebit).disabled = false;
        document.getElementById(txtcredit).disabled = false;
    }
}

function ValidateDrCrFinal(txtddldrcr, txtchart, txtdebit, txtcredit, txtrefdocnumber, txtrefdate) {
    var ddlDrCr = document.getElementById(txtddldrcr).value;
    var chartofaccount = document.getElementById(txtchart).value;
    var refdocnumber = document.getElementById(txtrefdocnumber).value;
    var refdocnumberdate = document.getElementById(txtrefdate).value;

    if (chartofaccount == "") {
        alert('Chart of Account should be selected')
        document.getElementById(txtddldrcr).value = "0";
        return false;
    }

    if (refdocnumber == "") {
        alert('Ref.Doc Number should not be null')
        document.getElementById(txtddldrcr).value = "0";
        document.getElementById(txtrefdocnumber).focus();
        return false;
    }

    if (refdocnumberdate == "") {
        alert('Ref.Doc Date should not be null')
        document.getElementById(txtddldrcr).value = "0";
        document.getElementById(txtrefdate).focus();
        return false;
    }

    var txtRefNo = document.getElementById("ctl00_CPHDetails_txtReferenceDocumentNumber").value;

    if (txtRefNo == refdocnumber) {
        alert('Ref.Doc Number should not be same as Reference Document Number')
        document.getElementById(txtddldrcr).value = "0";
        document.getElementById(txtrefdocnumber).focus();
        return false;
    }

    if (ddlDrCr == "D") {
        document.getElementById(txtdebit).disabled = false;
        document.getElementById(txtcredit).disabled = true;
        document.getElementById(txtdebit).focus();
        document.getElementById(txtcredit).value = "0";
    }
    else if (ddlDrCr == "C") {
        document.getElementById(txtdebit).disabled = true;
        document.getElementById(txtcredit).disabled = false;
        document.getElementById(txtcredit).focus();
        document.getElementById(txtdebit).value = "0";
    }
    else {
        document.getElementById(txtdebit).disabled = false;
        document.getElementById(txtcredit).disabled = false;
    }

    calculateTotalFinal();

    return true;
}

function ValidationSubmit() {
    var grd = document.getElementById('ctl00_CPHDetails_grvTransactionDetails');
    var rownum = String(grd.rows.length);

    if (rownum.length == 1)
        rownum = "0" + rownum;

    var txtDebitTotal = document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalDebitAmount").value;
    var txtCreditTotal = document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalCreditAmount").value;

    var debitamount = 0;
    var creditamount = 0;

    var referenceDocNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocumentNumber");

    var narration = document.getElementById(CtrlIdPrefix + "txtNarration");
    if (narration.value == "") {
        alert("Narration should not be null");
        narration.focus();
        return false;
    }

    if (!validatespl(narration.value, "Narration")) {
    }
    else {
        narration.focus();
        return false
    }

    if (referenceDocNumber.value != "") {
        if (!validatespl(referenceDocNumber.value, "Reference Document Number")) {
        }
        else {
            referenceDocNumber.focus();
            return false
        }
    }

    var grd = document.getElementById('ctl00_CPHDetails_grvTransactionDetails');

    for (i = 1; i < grd.rows.length; i++) {

        var node1 = grd.rows[i].cells[8].childNodes[0];
        var node2 = grd.rows[i].cells[9].childNodes[0];
        var node3 = grd.rows[i].cells[7].childNodes[0];
        var node4 = grd.rows[i].cells[1].childNodes[0];
        var node5 = grd.rows[i].cells[5].childNodes[0];
        var browserName = navigator.appName;
        if (browserName == 'Netscape') {
            var node1 = grd.rows[i].cells[8].childNodes[1];
            var node2 = grd.rows[i].cells[9].childNodes[1];
            var node3 = grd.rows[i].cells[7].childNodes[1];
            var node4 = grd.rows[i].cells[1].childNodes[1];
            var node5 = grd.rows[i].cells[5].childNodes[1];
        }

        if (node3.value != "0") {
            //--- DrCr
            if (node3 != undefined && node3.type == "select-one") {
                if (node3.value == "0") {
                    alert("Transaction(s) is/are not complete");
                    node3.focus();
                    return false;
                }
                else if (node4 != undefined && node4.type == "text") {
                    if (node4.value == "") {
                        alert("Chart of Account should not be null");
                        node4.focus;
                        return false;
                    }
                    else if (node5 != undefined && node5.type == "text") {
                        if (node5.value == "") {
                            alert("Reference Document Number should not be null in the grid");
                            node5.focus;
                            return false;
                        }
                        else {

                            var strDrCr = node3.value;

                            //-- Debit Amount
                            if (node1 != undefined && node1.type == "text")
                                if (!isNaN(node1.value) && node1.value != "")
                                    debitamount += parseFloat(node1.value);

                            //-- Credit Amount
                            if (node2 != undefined && node2.type == "text")
                                if (!isNaN(node2.value) && node2.value != "")
                                    creditamount += parseFloat(node2.value);

                            if ((strDrCr == "D") && (debitamount <= 0)) {
                                alert('Debit amount should be greater than Zero');
                                node1.focus();
                                return false;
                            }

                            if ((strDrCr == "C") && (creditamount <= 0)) {
                                alert('Credit amount should be greater than Zero');
                                node2.focus();
                                return false;
                            }
                        }
                    }
                }
            }
        }
    }

    if (parseFloat(txtCreditTotal) == 0 || parseFloat(txtDebitTotal) == 0) {
        alert("Transaction(s) is/are not complete");
        return false;
    }

    if (parseFloat(txtCreditTotal) != parseFloat(txtDebitTotal)) {
        alert('Debit does not tally with Credit')
        return false;
    }
}

function ValidationSubmitFinal() {
    var grd = document.getElementById('ctl00_CPHDetails_grvTransactionDetails');
    var rownum = String(grd.rows.length);

    if (rownum.length == 1)
        rownum = "0" + rownum;

    var txtDebitTotal = document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalDebitAmount").value;
    var txtCreditTotal = document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalCreditAmount").value;

    var debitamount = 0;
    var creditamount = 0;

    var referenceDocNumber = document.getElementById(CtrlIdPrefix + "txtReferenceDocumentNumber");

    var narration = document.getElementById(CtrlIdPrefix + "txtNarration");
    if (narration.value == "") {
        alert("Narration should not be null");
        narration.focus();
        return false;
    }

    if (!validatespl(narration.value, "Narration")) {
    }
    else {
        narration.focus();
        return false
    }

    if (referenceDocNumber.value != "") {
        if (!validatespl(referenceDocNumber.value, "Reference Document Number")) {
        }
        else {
            referenceDocNumber.focus();
            return false
        }
    }

    var grd = document.getElementById('ctl00_CPHDetails_grvTransactionDetails');

    for (i = 1; i < grd.rows.length - 1; i++) {

        var node0 = grd.rows[i].cells[0].childNodes[0];
        var node1 = grd.rows[i].cells[9].childNodes[0];
        var node2 = grd.rows[i].cells[10].childNodes[0];
        var node3 = grd.rows[i].cells[8].childNodes[0];
        var node4 = grd.rows[i].cells[2].childNodes[0];
        var node5 = grd.rows[i].cells[6].childNodes[0];
        var browserName = navigator.appName;
        if (browserName == 'Netscape') {
            var node0 = grd.rows[i].cells[0].childNodes[1];
            var node1 = grd.rows[i].cells[9].childNodes[1];
            var node2 = grd.rows[i].cells[10].childNodes[1];
            var node3 = grd.rows[i].cells[8].childNodes[1];
            var node4 = grd.rows[i].cells[2].childNodes[1];
            var node5 = grd.rows[i].cells[6].childNodes[1];
        }

        if (node0.checked) {
            if (node3.value != "0") {
                //--- DrCr
                if (node3 != undefined && node3.type == "select-one") {
                    if (node3.value == "0") {
                        alert("Transaction(s) is/are not complete");
                        node3.focus();
                        return false;
                    }
                    else if (node4 != undefined && node4.type == "text") {
                        if (node4.value == "") {
                            alert("Chart of Account should not be null");
                            node4.focus;
                            return false;
                        }
                        else if (node5 != undefined && node5.type == "text") {
                            if (node5.value == "") {
                                alert("Reference Document Number should not be null in the grid");
                                node5.focus;
                                return false;
                            }
                            else {
                                var strDrCr = node3.value;
                                
                                //-- Debit Amount
                                if (node1 != undefined && node1.type == "text")
                                    if (!isNaN(node1.value) && node1.value != "")
                                        debitamount += parseFloat(node1.value);

                                //-- Credit Amount
                                if (node2 != undefined && node2.type == "text")
                                    if (!isNaN(node2.value) && node2.value != "")
                                        creditamount += parseFloat(node2.value);

                                if ((strDrCr == "D") && (debitamount <= 0)) {
                                    alert('Debit amount should be greater than Zero');
                                    node1.focus();
                                    return false;
                                }

                                if ((strDrCr == "C") && (creditamount <= 0)) {
                                    alert('Credit amount should be greater than Zero');
                                    node2.focus();
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    if (parseFloat(txtCreditTotal) == 0 || parseFloat(txtDebitTotal) == 0) {
        alert("Transaction(s) is/are not complete");
        return false;
    }

    if (parseFloat(txtCreditTotal) != parseFloat(txtDebitTotal)) {
        alert('Debit does not tally with Credit')
        return false;
    }

    var Status = "Are you Sure to Approve the JV Entry";

    if (confirm(Status))
        return true;
    else
        return false;
}

function SelectedChangeAll(id) {
    var chkAllBox = document.getElementById(id);
    var gridview = document.getElementById("ctl00_CPHDetails_grvTransactionDetails");
    var TotalDebitAmount = 0.00;
    var TotalCreditAmount = 0.00;
    var ChkCnt = 0;
    var rownum = String(gridview.rows.length);

    if (rownum.length == 1)
        rownum = "0" + rownum;

    if (chkAllBox.checked) {
        for (i = 1; i <= gridview.rows.length - 2; i++) {
            var row = gridview.rows[i];
            chkBox = row.cells[0].children[0];
            DebitAmount = row.cells[9].children[0];
            CreditAmount = row.cells[10].children[0];
            chkBox.checked = true;

            ChkCnt++;

            if (DebitAmount.value.trim() != "")
                TotalDebitAmount += parseFloat(DebitAmount.value);

            if (CreditAmount.value.trim() != "")
                TotalCreditAmount += parseFloat(CreditAmount.value);
        }
    }
    else {
        for (i = 1; i <= gridview.rows.length - 2; i++) {
            var row = gridview.rows[i];
            chkBox = row.cells[0].children[0];
            chkBox.checked = false;

            ChkCnt = 0;
            TotalDebitAmount = 0.00;
            TotalCreditAmount = 0.00;
        }
    }

    document.getElementById("ctl00_CPHDetails_ChkStatus").value = ChkCnt;
    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalDebitAmount").value = TotalDebitAmount.toFixed(2);
    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalCreditAmount").value = TotalCreditAmount.toFixed(2);
}

function SelectedChangeCheckBox(lnk) {
    var rowno = lnk.parentNode.parentNode;
    chkBox = rowno.cells[0].children[0];

    var ChkCnt = 0;

    //if (chkBox.checked) {
    //    ChartAcc = rowno.cells[2].children[1];
    //    ChartAcc.style.display = "inline";
    //}
    //else {
    //    ChartAcc = rowno.cells[2].children[1];
    //    ChartAcc.style.display = "none";
    //}

    var TotalDebitAmount = 0.00;
    var TotalCreditAmount = 0.00;
    var gridview = document.getElementById("ctl00_CPHDetails_grvTransactionDetails");
    var rownum = String(gridview.rows.length);

    if (rownum.length == 1)
        rownum = "0" + rownum;

    for (i = 1; i <= gridview.rows.length - 2; i++) {
        var row = gridview.rows[i];
        chkBox1 = row.cells[0].children[0];

        if (chkBox1.checked) {
            DebitAmount = row.cells[9].children[0];
            CreditAmount = row.cells[10].children[0];

            if (DebitAmount.value.trim() != "")
                TotalDebitAmount += parseFloat(DebitAmount.value);

            if (CreditAmount.value.trim() != "")
                TotalCreditAmount += parseFloat(CreditAmount.value);

            ChkCnt++;
        }
    }

    //if (ChkCnt == gridview.rows.length - 2)
    //    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl01_chkSelectAll").checked = true;
    //else
    //    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl01_chkSelectAll").checked = false;

    document.getElementById("ctl00_CPHDetails_ChkStatus").value = ChkCnt;
    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalDebitAmount").value = TotalDebitAmount.toFixed(2);
    document.getElementById("ctl00_CPHDetails_grvTransactionDetails_ctl" + rownum + "_txtTotalCreditAmount").value = TotalCreditAmount.toFixed(2);
}

function funJVReject() {
    var Remarks = prompt("Are you Sure to Reject the JV Entry? Please Give the Reason Below.");

    if (Remarks) {
        PageMethods.SetSessionRemarks(Remarks);
        return true;
    }
    else
        return false;
}

function checkDate(sender, args) {

    var strSender = sender._id;
    var idRefDocDate = strSender.replace("ceReqDocDate", "txtReqDocDate");

    if (sender._selectedDate > new Date()) {
        alert("Reference Document Date should not be greater than System Date");

        if ("ctl00_CPHDetails_ceRequDocDate" == strSender) {
            document.getElementById("ctl00_CPHDetails_txtReferenceDocumentDate").value = "";
        }
        else {
            document.getElementById(idRefDocDate).value = "";
        }

    }
}

function MakeStaticHeader(gridId, width, height, headerHeight) {

    var tbl = document.getElementById(gridId);
    if (tbl) {
        var DivHR = document.getElementById('DivHeaderRow');
        var DivMC = document.getElementById('DivMainContent');

        DivHR.style.height = headerHeight + 'px';
        DivHR.style.width = (parseInt(width) - 16) + 'px';
        DivHR.style.position = 'relative';
        DivHR.style.top = '0px';
        DivHR.style.zIndex = '10';
        DivHR.style.verticalAlign = 'top';

        DivMC.style.width = width + 'px';
        DivMC.style.height = height + 'px';
        DivMC.style.position = 'relative';
        DivMC.style.top = -headerHeight + 'px';
        DivMC.style.zIndex = '1';

        DivHR.appendChild(tbl.cloneNode(true));
    }
}

function OnScrollDiv(Scrollablediv) {
    document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
}


function CurrencyDecimalOnly(e1, evt) {

    var currenyValue = document.getElementById(e1);

    //    var charCode = (evt.which) ? evt.which : event.keyCode;
    //    if (charCode != 46 && charCode > 31 && charCode != 190
    //        && (charCode < 48 || charCode > 57)) {
    //        document.getElementById(e1).value = "";
    //        return false;
    //    }

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

    calculateTotal();

    return true;
}

function CurrencyDecimalOnlyFinal(e1, evt) {

    var currenyValue = document.getElementById(e1);

    //    var charCode = (evt.which) ? evt.which : event.keyCode;
    //    if (charCode != 46 && charCode > 31 && charCode != 190
    //        && (charCode < 48 || charCode > 57)) {
    //        document.getElementById(e1).value = "";
    //        return false;
    //    }

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

    calculateTotalFinal();

    return true;
}

function CurrencyRoundOffOnly(e1, evt) {

    var currenyValue = document.getElementById(e1);
    var Isvalid = parseFloat(currenyValue.value).toFixed(2);
    if (Isvalid.toString() != "NaN") {
        document.getElementById(e1).value = Isvalid;
    }
    else {
        document.getElementById(e1).value = 0;
    }

    return true;
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

function CheckValidDateJV(id, isFutureDate, Msg) {
    var idDate = document.getElementById(id).value;
    var idJVDate = document.getElementById("ctl00_CPHDetails_txtJVDate").value;

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
                    return false;
                }

                var tDate = idJVDate.split("/");
                var day1 = tDate[0];
                var month1 = tDate[1] - 1;
                var year1 = parseInt(tDate[2]);

                var jvDate = new Date();
                jvDate.setDate(day1);
                jvDate.setMonth(month1);
                jvDate.setFullYear(year1);

                if (frmDate > jvDate) {
                    document.getElementById(id).value = "";
                    alert(Msg + " should be less than or equal to JV Date");
                    return false;
                }
            }
        }
    }
}


function checkDateForRefDocDate(id) {

    var idDate = document.getElementById(id).value;

    if (idDate != '') {
        var status = fnIsDate(idDate);

        if (!status) {
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
        }
        else {

            var idReferenceDocumentDate = document.getElementById("ctl00_CPHDetails_txtReferenceDocumentDate").value

            if (idReferenceDocumentDate != "") {
                var toDate = new Date();
                toDate = convertDate(idDate);

                var frmDate = new Date();
                frmDate = convertDate(idReferenceDocumentDate);

                if (toDate > frmDate) {
                    document.getElementById(id).value = "";
                    alert("Req Doc Date should be less than or equal to Reference Document Date");
                }
            }
            else {

                var fDate = idDate.split("/");
                var day = fDate[0];
                var month = fDate[1] - 1;
                var year = parseInt(fDate[2]);

                var frmDate = new Date();
                frmDate.setDate(day);
                frmDate.setMonth(month);
                frmDate.setFullYear(year);

                if (frmDate > new Date()) {
                    alert("Req Doc Date should not be greater than Today's Date");
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }

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