var CtrlIdPrefix = "ctl00_CPHDetails_";
var selectedItemCode = "";

document.onkeydown = function () {
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

function FieldValidationSubmit() {
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var ddlSupp = document.getElementById(CtrlIdPrefix + "ddlSupplierName");
    var Supplier = ddlSupp.options[ddlSupp.selectedIndex].value;

    var txtLocation = document.getElementById(CtrlIdPrefix + "txtLocation");

    if (Branch == "0") {
        alert("Please select a Branch.");
        ddlBrnch.focus();
        return false;
    }

    if (Supplier == "0") {
        alert("Please select a Supplier.");
        ddlSupp.focus();
        return false;
    }

    if (txtLocation.value.trim() == "") {
        alert("Please enter a Location.");
        txtLocation.focus();
        return false;
    }

    if (txtLocation.value.trim().length > 12) {
        alert("Location Code Should not exceed 12 Characters.");
        txtLocation.focus();
        return false;
    }

    txtLocation.value = txtLocation.value.toUpperCase().replace(' ', '').trim();

    if (Branch == txtLocation.value) {
        alert("Invalid Item Location.");
        txtLocation.focus();
        return false;
    }

    if (txtLocation.value.toUpperCase().replace(' ', '').trim().indexOf("LOCLOCLOC") > 0) {
        alert("Invalid Item Location.");
        txtLocation.focus();
        return false;
    }

    if (txtLocation.value.toUpperCase().replace(' ', '').trim().length == 3) {
        if (txtLocation.value.toUpperCase().replace(' ', '').trim().slice(-3) == "LOC") {
            alert("Invalid Item Location.");
            txtLocation.focus();
            return false;
        }
    }

    if (txtLocation.value.toUpperCase().replace(' ', '').trim().length == 6) {
        if (txtLocation.value.toUpperCase().replace(' ', '').trim().slice(-6) == "LOCLOC") {
            alert("Invalid Item Location.");
            txtLocation.focus();
            return false;
        }
    }

    if (txtLocation.value.toUpperCase().replace(' ', '').trim().length >= 9) {
        if (txtLocation.value.toUpperCase().replace(' ', '').trim().slice(-9) == "LOCLOCLOC") {
            alert("Invalid Item Location.");
            txtLocation.focus();
            return false;
        }
    }

    return true;
}

function FieldValidationReport() {
    var ddlBrnch = document.getElementById(CtrlIdPrefix + "ddlBranch");
    var Branch = ddlBrnch.options[ddlBrnch.selectedIndex].value;

    var ddlSupp = document.getElementById(CtrlIdPrefix + "ddlSupplierName");
    var Supplier = ddlSupp.options[ddlSupp.selectedIndex].value;

    if (Branch == "0") {
        alert("Please select a Branch.");
        ddlBrnch.focus();
        return false;
    }

    if (Supplier == "0") {
        alert("Please select a Supplier.");
        ddlSupp.focus();
        return false;
    }

    return true;
}

function DownLoadExcelFile(uid) {
    var rawData = document.getElementById(CtrlIdPrefix + "hdnViewState").value;
    //console.log(rawData);
    var excelData = JSON.parse(rawData)[0];
    var createXLSLFormatObj = [];

    /* XLS Head Columns */
    //var xlsHeader = ["EmployeeID", "Full Name"];

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

    /* Add worksheet to workbook */
    XLSX.utils.book_append_sheet(wb, ws, ws_name);

    /* Write workbook and Download */
    //if (typeof console !== 'undefined') console.log(new Date());
    XLSX.writeFile(wb, filename);
    //if (typeof console !== 'undefined') console.log(new Date());
}