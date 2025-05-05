var CtrlIdPrefix = "ctl00_CPHDetails_";

function ValidateFile() {
    var btnUpload = document.getElementById(CtrlIdPrefix + "btnFileUpload");
    if (btnUpload.value == "" || btnUpload.value == null) {
        alert("Please Select a File");
        btnUpload.focus();
        return false;
    }

    if (btnUpload.value.slice(btnUpload.value.length - 4) != ".xls") {
        alert("Excel file should be 97-2003 (.xls) WorkBook");
        return false;
    }
}

function fnValidateSubmit() {
    var btnUpload = document.getElementById(CtrlIdPrefix + "btnFileUpload");

    if (btnUpload != null) {
        if (btnUpload.value == "" || btnUpload.value == null) {
            alert("Please Select a File");
            btnUpload.focus();
            return false;
        }
    }

    if (btnUpload.value.slice(btnUpload.value.length - 4) != ".xls") {
        alert("Excel file should be 97-2003 (.xls) WorkBook");
        return false;
    }

    var RowCnt = document.getElementById(CtrlIdPrefix + "hdnRowCount").value;
    
    if (parseInt(RowCnt) <= 0) {
        alert("No Records Exist or All Part Nos in the File are to be added in Item Master. Please Check and Proceed");
        return false;
    }
    else {
        var Cnt = document.getElementById(CtrlIdPrefix + "hdnMissingPartNos").value;

        if (parseInt(Cnt) == parseInt(RowCnt)) {
            alert("All the Part Nos Exist in the File are New Addition and to be added in Item Master. Please Check the file Once");
            return false;
        }
        else {
            if (parseInt(Cnt) > 0) {
                if (confirm("Few Part Nos Exists in the File are New Addition and to be added in Item Master. You Want to Proceed with Existing Data?"))
                    return true;
                else
                    return false;
            }
        }
    }
}