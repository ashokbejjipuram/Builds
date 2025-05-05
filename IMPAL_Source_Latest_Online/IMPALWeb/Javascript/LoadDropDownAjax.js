function LoadSelect(id, filePath) {
    var Filter = document.getElementById(id + "hddFilter").value
    $.post(filePath + "?Filter=" + Filter, function(result) {
        var streets = eval(result);
        ClearStreetsDropDown(id + "drpGlMain");
        PopulateGlMain(streets, id + "drpGlMain");
    });
}


function LoadDropSub(id, filePath) {
    var selectedPostCode = $('#' + id + 'drpGlMain option:selected').val();
    var Filter = document.getElementById(id + "hddFilter").value;
    if (selectedPostCode && selectedPostCode != "0") {
        $.post(filePath + "?Filter=" + Filter + "&GlMain=" + selectedPostCode,
         function(result) {
             var streets = eval(result);
             ClearStreetsDropDown(id + "drpGlSub");
             ClearStreetsDropDown(id + "drpGlAccount");
             ClearStreetsDropDown(id + "drpBranch")
             document.getElementById(id + "txtChatAccCode").innerHTML = "";
             document.getElementById(id + "txtGlclass").innerHTML = "";
             document.getElementById(id + "txtGlGroup").innerHTML = "";
             PopulateGlSub(streets, id + "drpGlSub");
         });
    } else {
        ClearStreetsDropDown(id + "drpGlSub")
        ClearStreetsDropDown(id + "drpGlAccount")
        ClearStreetsDropDown(id + "drpBranch")
        document.getElementById(id + "txtChatAccCode").innerHTML = "";
        document.getElementById(id + "txtGlclass").innerHTML = "";
        document.getElementById(id + "txtGlGroup").innerHTML = "";
    }
}

function LoadDropAcc(id, filePath) {
    var GlMain = $('#' + id + 'drpGlMain option:selected').val();
    var GlSub = $('#' + id + 'drpGlSub option:selected').val();
    if (GlSub && GlSub != "0") {
        $.post(filePath + "?GlMain=" + GlMain + "&GlSub=" + GlSub,
         function(result) {
             var streets = eval(result);
             ClearStreetsDropDown(id + "drpGlAccount");
             ClearStreetsDropDown(id + "drpBranch")
             document.getElementById(id + "txtChatAccCode").innerHTML = "";
             document.getElementById(id + "txtGlclass").innerHTML = "";
             document.getElementById(id + "txtGlGroup").innerHTML = "";
             PopulateGlAccount(streets, id + "drpGlAccount");
         });
    } else {
        ClearStreetsDropDown(id + "drpGlAccount")
        ClearStreetsDropDown(id + "drpBranch")
        document.getElementById(id + "txtChatAccCode").innerHTML = "";
        document.getElementById(id + "txtGlclass").innerHTML = "";
        document.getElementById(id + "txtGlGroup").innerHTML = "";
    }
}

function LoadDropBranch(id, filePath) {
    var GlMain = $('#' + id + 'drpGlMain option:selected').val();
    var GlSub = $('#' + id + 'drpGlSub option:selected').val();
    var GlAcc = $('#' + id + 'drpGlAccount option:selected').val();
    if (GlAcc && GlAcc != "0") {
        $.post(filePath + "?GlMain=" + GlMain + "&GlSub=" + GlSub + "&GlAcc=" + GlAcc,
         function(result) {
             var streets = eval(result);
             ClearStreetsDropDown(id + "drpBranch");
             document.getElementById(id + "txtChatAccCode").innerHTML = "";
             document.getElementById(id + "txtGlclass").innerHTML = "";
             document.getElementById(id + "txtGlGroup").innerHTML = "";
             PopulateGlBranch(streets, id + "drpBranch");
         });
    } else {
        ClearStreetsDropDown(id + "drpBranch")
        document.getElementById(id + "txtChatAccCode").innerHTML = "";
        document.getElementById(id + "txtGlclass").innerHTML = "";
        document.getElementById(id + "txtGlGroup").innerHTML = "";
    }
}

function LoadChartAccount(id, filePath) {
    $('#' + id + 'BtnSubmit').prop('disabled', false);
    var GlMain = $('#' + id + 'drpGlMain option:selected').val();
    var GlSub = $('#' + id + 'drpGlSub option:selected').val();
    var GlAcc = $('#' + id + 'drpGlAccount option:selected').val();
    var Branch = $('#' + id + 'drpBranch option:selected').val();
    if (Branch && Branch != "0") {
        $.post(filePath + "?GlMain=" + GlMain + "&GlSub=" + GlSub + "&GlAcc=" + GlAcc + "&Branch=" + Branch,
         function(result) {
             var streets = eval(result);
             PopulateChartAccount(streets, id);
         });
    } else {
        document.getElementById(id + "txtChatAccCode").innerHTML = "";
        document.getElementById(id + "txtGlclass").innerHTML = "";
        document.getElementById(id + "txtGlGroup").innerHTML = "";
    }
}

function LoadCustomerDetails(id, filePath) {
    debugger;
    $('#' + id + 'BtnSubmit').prop('disabled', false);
    var GlSub = $('#' + id + 'drpGlSub option:selected').val();
    var GlAcc = $('#' + id + 'drpGlAccount option:selected').val();
    $.post(filePath + "?GlSub=" + GlSub + "&GlAcc=" + GlAcc,
         function(result) {
             var streets = eval(result);
             PopulateCustomer(streets, id);
         });
}

function LoadSelectOthers(id, filePath) {
    var Filter = document.getElementById(id + "hddFilter").value
    $.post(filePath + "?Filter=" + Filter, function(result) {
        var streets = eval(result);
        ClearStreetsDropDown(id + "drpGlMain");
        PopulateGlMainOthers(streets, id + "drpGlMain");
    });
}


function LoadDropSubOthers(id, filePath) {
    var selectedPostCode = $('#' + id + 'drpGlMain option:selected').val();
    var Filter = document.getElementById(id + "hddFilter").value;
    if (selectedPostCode && selectedPostCode != "0") {
        $.post(filePath + "?Filter=" + Filter + "&GlMain=" + selectedPostCode,
         function(result) {
             var streets = eval(result);
             ClearStreetsDropDown(id + "drpGlSub");
             ClearStreetsDropDown(id + "drpGlAccount");
             ClearStreetsDropDown(id + "drpBranch")
             document.getElementById(id + "txtChatAccCode").innerHTML = "";
             document.getElementById(id + "txtGlclass").innerHTML = "";
             document.getElementById(id + "txtGlGroup").innerHTML = "";
             PopulateGlSubOthers(streets, id + "drpGlSub");
         });
    } else {
        ClearStreetsDropDown(id + "drpGlSub")
        ClearStreetsDropDown(id + "drpGlAccount")
        ClearStreetsDropDown(id + "drpBranch")
        document.getElementById(id + "txtChatAccCode").innerHTML = "";
        document.getElementById(id + "txtGlclass").innerHTML = "";
        document.getElementById(id + "txtGlGroup").innerHTML = "";
    }
}

function LoadDropAccOthers(id, filePath) {
    var GlMain = $('#' + id + 'drpGlMain option:selected').val();
    var GlSub = $('#' + id + 'drpGlSub option:selected').val();
    if (GlSub && GlSub != "0") {
        $.post(filePath + "?GlMain=" + GlMain + "&GlSub=" + GlSub,
         function(result) {
             var streets = eval(result);
             ClearStreetsDropDown(id + "drpGlAccount");
             ClearStreetsDropDown(id + "drpBranch")
             document.getElementById(id + "txtChatAccCode").innerHTML = "";
             document.getElementById(id + "txtGlclass").innerHTML = "";
             document.getElementById(id + "txtGlGroup").innerHTML = "";
             PopulateGlAccountOthers(streets, id + "drpGlAccount");
         });
    } else {
        ClearStreetsDropDown(id + "drpGlAccount")
        ClearStreetsDropDown(id + "drpBranch")
        document.getElementById(id + "txtChatAccCode").innerHTML = "";
        document.getElementById(id + "txtGlclass").innerHTML = "";
        document.getElementById(id + "txtGlGroup").innerHTML = "";
    }
}

function LoadDropBranchOthers(id, filePath) {
    var GlMain = $('#' + id + 'drpGlMain option:selected').val();
    var GlSub = $('#' + id + 'drpGlSub option:selected').val();
    var GlAcc = $('#' + id + 'drpGlAccount option:selected').val();
    if (GlAcc && GlAcc != "0") {
        $.post(filePath + "?GlMain=" + GlMain + "&GlSub=" + GlSub + "&GlAcc=" + GlAcc,
         function(result) {
             var streets = eval(result);
             ClearStreetsDropDown(id + "drpBranch");
             document.getElementById(id + "txtChatAccCode").innerHTML = "";
             document.getElementById(id + "txtGlclass").innerHTML = "";
             document.getElementById(id + "txtGlGroup").innerHTML = "";
             PopulateGlBranchOthers(streets, id + "drpBranch");
         });
    } else {
        ClearStreetsDropDown(id + "drpBranch")
        document.getElementById(id + "txtChatAccCode").innerHTML = "";
        document.getElementById(id + "txtGlclass").innerHTML = "";
        document.getElementById(id + "txtGlGroup").innerHTML = "";
    }
}

function LoadChartAccountOthers(id, filePath) {
    $('#' + id + 'BtnSubmit').prop('disabled', false);
    var GlMain = $('#' + id + 'drpGlMain option:selected').val();
    var GlSub = $('#' + id + 'drpGlSub option:selected').val();
    var GlAcc = $('#' + id + 'drpGlAccount option:selected').val();
    var Branch = $('#' + id + 'drpBranch option:selected').val();
    if (Branch && Branch != "0") {
        $.post(filePath + "?GlMain=" + GlMain + "&GlSub=" + GlSub + "&GlAcc=" + GlAcc + "&Branch=" + Branch,
         function(result) {
             var streets = eval(result);
             PopulateChartAccountOthers(streets, id);
         });
    } else {
        document.getElementById(id + "txtChatAccCode").innerHTML = "";
        document.getElementById(id + "txtGlclass").innerHTML = "";
        document.getElementById(id + "txtGlGroup").innerHTML = "";
    }
}

function LoadCustomerDetailsOthers(id, filePath) {
    debugger;
    $('#' + id + 'BtnSubmit').prop('disabled', false);
    var GlSub = $('#' + id + 'drpGlSub option:selected').val();
    var GlAcc = $('#' + id + 'drpGlAccount option:selected').val();
    $.post(filePath + "?GlSub=" + GlSub + "&GlAcc=" + GlAcc,
         function(result) {
             var streets = eval(result);
             PopulateCustomerOthers(streets, id);
         });
}

function PopulateCustomer(streets, id) {
    document.getElementById(id + "txtCode").value = streets.Customer_Code;
    document.getElementById(id + "txtAddress1").value = streets.address1;
    document.getElementById(id + "txtAddress2").value = streets.address2;
    document.getElementById(id + "txtAddress3").value = streets.address3;
    document.getElementById(id + "txtAddress4").value = streets.address4;
    document.getElementById(id + "txtLocation").value = streets.Location;
}

function PopulateCustomerOthers(streets, id) {
    document.getElementById(id + "txtCode").value = streets.Customer_Code;
    document.getElementById(id + "txtAddress1").value = streets.address1;
    document.getElementById(id + "txtAddress2").value = streets.address2;
    document.getElementById(id + "txtAddress3").value = streets.address3;
    document.getElementById(id + "txtAddress4").value = streets.address4;
    document.getElementById(id + "txtLocation").value = streets.Location;
}


function ClearStreetsDropDown(id) {
    $('#' + id + ' option').each(function(i, option) { $(option).remove(); });
}

function PopulateGlMain(streets, id) {
    $('#' + id).append($('<option></option>').val("").html(""));
    $(streets).each(function(i) {
        $('#' + id).append($('<option></option>').val(streets[i].GlMainCode).html(streets[i].GlMainName));
    });

}

function PopulateGlSub(streets, id) {
    $('#' + id).append($('<option></option>').val("").html(""));
    $(streets).each(function(i) {
        $('#' + id).append($('<option></option>').val(streets[i].GlSubCode).html(streets[i].GlSubName));
    });

    var Count = countProperties(streets);
    if (Count == 1) {
        $('#' + id).prop('selectedIndex', 1);
        var ClientId = id.replace("drpGlSub", "");
        LoadDropAcc(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadAccount.ashx");
    }
}

function PopulateGlAccount(streets, id) {
    $('#' + id).append($('<option></option>').val("").html(""));
    $(streets).each(function(i) {
        $('#' + id).append($('<option></option>').val(streets[i].GlAccountCode).html(streets[i].GlAccountName));
    });

    var Count = countProperties(streets);
    if (Count == 1) {
        $('#' + id).prop('selectedIndex', 1);
        var ClientId = id.replace("drpGlAccount", "");
        LoadDropBranch(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadBranch.ashx");

    }
}

function PopulateGlBranch(streets, id) {
    $('#' + id).append($('<option></option>').val("").html(""));
    $(streets).each(function(i) {
        $('#' + id).append($('<option></option>').val(streets[i].BankCode).html(streets[i].BankName));
    });
    var ClientId = id.replace("drpBranch", "");
    var DefaultBranch = document.getElementById(ClientId + "hddDefaultBranch").value;
    var BranchName = document.getElementById(ClientId + "hddBranch").value;
    if (DefaultBranch != "") {
        if (DefaultBranch.toUpperCase() == "TRUE") {
            $('#' + id).val(BranchName);
            if (!(BranchName.toUpperCase() == "COR" || BranchName.toUpperCase() == "CRP")) {
                $('#' + id).prop('disabled', true);
            }
            LoadChartAccount(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadChartAccount.ashx");
        }
    }
}

function PopulateChartAccount(streets, id) {
    document.getElementById(id + "txtChatAccCode").innerHTML = streets.ChartCode;
    document.getElementById(id + "txtGlclass").innerHTML = streets.GlClassification;
    document.getElementById(id + "txtGlGroup").innerHTML = streets.GlGroup;
}

function PopulateGlMainOthers(streets, id) {
    $('#' + id).append($('<option></option>').val("").html(""));
    $(streets).each(function (i) {
        $('#' + id).append($('<option></option>').val(streets[i].GlMainCode).html(streets[i].GlMainName));
    });

}

function PopulateGlSubOthers(streets, id) {
    $('#' + id).append($('<option></option>').val("").html(""));
    $(streets).each(function (i) {
        $('#' + id).append($('<option></option>').val(streets[i].GlSubCode).html(streets[i].GlSubName));
    });

    var Count = countProperties(streets);
    if (Count == 1) {
        $('#' + id).prop('selectedIndex', 1);
        var ClientId = id.replace("drpGlSub", "");
        LoadDropAccOthers(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadAccountOthers.ashx");
    }
}

function PopulateGlAccountOthers(streets, id) {
    $('#' + id).append($('<option></option>').val("").html(""));
    $(streets).each(function (i) {
        $('#' + id).append($('<option></option>').val(streets[i].GlAccountCode).html(streets[i].GlAccountName));
    });

    var Count = countProperties(streets);
    if (Count == 1) {
        $('#' + id).prop('selectedIndex', 1);
        var ClientId = id.replace("drpGlAccount", "");
        LoadDropBranchOthers(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadBranchOthers.ashx");
    }
}

function PopulateGlBranchOthers(streets, id) {
    $('#' + id).append($('<option></option>').val("").html(""));
    $(streets).each(function (i) {
        $('#' + id).append($('<option></option>').val(streets[i].BankCode).html(streets[i].BankName));
    });
    var ClientId = id.replace("drpBranch", "");
    var DefaultBranch = document.getElementById(ClientId + "hddDefaultBranch").value;
    var BranchName = document.getElementById(ClientId + "hddBranch").value;
    if (DefaultBranch != "") {
        if (DefaultBranch.toUpperCase() == "TRUE") {
            $('#' + id).val(BranchName);
            if (!(BranchName.toUpperCase() == "COR" || BranchName.toUpperCase() == "CRP")) {
                $('#' + id).prop('disabled', true);
            }
            LoadChartAccountOthers(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadChartAccountOthers.ashx");
        }
    }
}

function PopulateChartAccountOthers(streets, id) {
    document.getElementById(id + "txtChatAccCode").innerHTML = streets.ChartCode;
    document.getElementById(id + "txtGlclass").innerHTML = streets.GlClassification;
    document.getElementById(id + "txtGlGroup").innerHTML = streets.GlGroup;
}

function LoadMain(Id) {
    var ClientId = Id.id.replace("imgPopup", "");
    LoadSelect(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadMain.ashx");
    $('#' + ClientId + 'BtnSubmit').prop('disabled', true);
    $('#' + ClientId + 'divCustomerDetails').hide();
}

function LoadSub(Id) {
    var ClientId = Id.id.replace("drpGlMain", "");
    LoadDropSub(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadGlSub.ashx");
    $('#' + ClientId + 'BtnSubmit').prop('disabled', true);
    $('#' + ClientId + 'divCustomerDetails').hide();
}

function LoadAcc(Id) {
    var ClientId = Id.id.replace("drpGlSub", "");
    LoadDropAcc(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadAccount.ashx");
    $('#' + ClientId + 'BtnSubmit').prop('disabled', true);
    $('#' + ClientId + 'divCustomerDetails').hide();
}

function LoadBranch(Id) {
    var ClientId = Id.id.replace("drpGlAccount", "");
    LoadDropBranch(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadBranch.ashx");
    var GlSub = $('#' + ClientId + 'drpGlSub option:selected').val();
    if (GlSub == "0220") {
        $('#' + ClientId + 'divCustomerDetails').show();
        LoadCustomerDetails(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadCustomer.ashx");
    }
    else {
        $('#' + ClientId + 'divCustomerDetails').hide();
    }
    $('#' + ClientId + 'BtnSubmit').prop('disabled', true);
}

function LoadChartAccountCode(Id) {
    var ClientId = Id.id.replace("drpBranch", "");
    LoadChartAccount(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadChartAccount.ashx");
    $('#' + ClientId + 'BtnSubmit').prop('disabled', false);
}

function LoadMainOthers(Id) {
    var ClientId = Id.id.replace("imgPopup", "");
    LoadSelectOthers(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadMainOthers.ashx");
    $('#' + ClientId + 'BtnSubmit').prop('disabled', true);
    $('#' + ClientId + 'divCustomerDetails').hide();
}

function LoadSubOthers(Id) {
    var ClientId = Id.id.replace("drpGlMain", "");
    LoadDropSubOthers(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadGlSubOthers.ashx");
    $('#' + ClientId + 'BtnSubmit').prop('disabled', true);
    $('#' + ClientId + 'divCustomerDetails').hide();
}

function LoadAccOthers(Id) {
    var ClientId = Id.id.replace("drpGlSub", "");
    LoadDropAccOthers(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadAccountOthers.ashx");
    $('#' + ClientId + 'BtnSubmit').prop('disabled', true);
    $('#' + ClientId + 'divCustomerDetails').hide();
}

function LoadBranchOthers(Id) {
    var ClientId = Id.id.replace("drpGlAccount", "");
    LoadDropBranchOthers(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadBranchOthers.ashx");
    var GlSub = $('#' + ClientId + 'drpGlSub option:selected').val();
    if (GlSub == "0220") {
        $('#' + ClientId + 'divCustomerDetails').show();
        LoadCustomerDetailsOthers(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadCustomerOthers.ashx");
    }
    else {
        $('#' + ClientId + 'divCustomerDetails').hide();
    }
    $('#' + ClientId + 'BtnSubmit').prop('disabled', true);
}

function LoadChartAccountCodeOthers(Id) {
    var ClientId = Id.id.replace("drpBranch", "");
    LoadChartAccountOthers(ClientId, document.getElementById(ClientId + "AshxPath").value + "/LoadChartAccountOthers.ashx");
    $('#' + ClientId + 'BtnSubmit').prop('disabled', false);
}

function countProperties(obj) {
    var prop;
    var propCount = 0;

    for (prop in obj) {
        propCount++;
    }
    return propCount;
}

function SubmitChart(Id) {
    var ClientId = Id.id.replace("BtnSubmit", "");
    var ChartAccount = document.getElementById(ClientId + "txtChatAccCode").innerHTML;
    var GlMain = $('#' + ClientId + 'drpGlMain option:selected').text();
    var ChartAccount = document.getElementById(ClientId + "txtChatAccCode").innerHTML;
    document.getElementById(ClientId + "hddChartAccount").value = ChartAccount;
    document.getElementById(ClientId + "hddDse").value = GlMain;
    //            $.get(document.getElementById(ClientId + "AshxPath").value + "/ChartSubmit.ashx?ChartAccount=" + ChartAccount + "&GlMain=" + GlMain);

}

function ClosePopup(Id) {
    var ClientId = Id.id.replace("imgBtnPopupExit", "");
    $find(ClientId + "MPESupplier").hide();
    ClearStreetsDropDown(ClientId + "drpGlSub")
    ClearStreetsDropDown(ClientId + "drpGlAccount")
    ClearStreetsDropDown(ClientId + "drpBranch")
    document.getElementById(ClientId + "txtChatAccCode").innerHTML = "";
    document.getElementById(ClientId + "txtGlclass").innerHTML = "";
    document.getElementById(ClientId + "txtGlGroup").innerHTML = "";
    $('#' + ClientId + 'drpGlMain').prop('selectedIndex', 0);
    $('#' + ClientId + 'divCustomerDetails').attr("visibility", "hide");
    return false;
}



function ResetPopup(Id) {
    var ClientId = Id.id.replace("btnReset", "");
    ClearStreetsDropDown(ClientId + "drpGlSub")
    ClearStreetsDropDown(ClientId + "drpGlAccount")
    ClearStreetsDropDown(ClientId + "drpBranch")
    document.getElementById(ClientId + "txtChatAccCode").innerHTML = "";
    document.getElementById(ClientId + "txtGlclass").innerHTML = "";
    document.getElementById(ClientId + "txtGlGroup").innerHTML = "";
    $('#' + ClientId + 'drpGlMain').prop('selectedIndex', 0);
    $('#' + ClientId + 'divCustomerDetails').hide();
//    $('#' + ClientId + 'divCustomerDetails').attr("visibility", "hide");
    return false;

}