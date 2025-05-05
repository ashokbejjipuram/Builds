<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelPopupChart.aspx.cs"
    Inherits="IMPALWeb.UserControls.ModelPopupChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="../Javascript/jquery-1.11.0.min.js"></script>

    <script type="text/javascript" language="JavaScript">


        function closeDialog() {

            document.getElementById('drpGlMain').value;
            windowReturnValue = document.getElementById('txtChatAccCode').innerHTML + '~' + $('#drpGlMain option:selected').text();
            window.returnValue = windowReturnValue;
            window.close();
            return false;
        }


        function LoadSelect(filePath, Loadid) {
            var Filter = document.getElementById('hddFilter').value
            $.post(filePath + "?Filter=" + Filter, function(result) {
                var streets = eval(result);
                ClearStreetsDropDown(Loadid);
                PopulateGlMain(streets, Loadid);
            });

        }


        function LoadDropSub(id, filePath, Loadid) {
            var selectedPostCode = $('#' + id + ' option:selected').val();
            if (selectedPostCode && selectedPostCode != "0") {
                $.post(filePath + "?GlMain=" + selectedPostCode,
         function(result) {
             var streets = eval(result);
             ClearStreetsDropDown(Loadid);
             ClearStreetsDropDown('drpGlAccount')
             ClearStreetsDropDown('drpBranch')
             document.getElementById('txtChatAccCode').innerHTML = "";
             document.getElementById('txtGlclass').innerHTML = "";
             document.getElementById('txtGlGroup').innerHTML = "";
             PopulateGlSub(streets, Loadid);
         });
            } else {
                ClearStreetsDropDown('drpGlSub')
                ClearStreetsDropDown('drpGlAccount')
                ClearStreetsDropDown('drpBranch')
                document.getElementById('txtChatAccCode').innerHTML = "";
                document.getElementById('txtGlclass').innerHTML = "";
                document.getElementById('txtGlGroup').innerHTML = "";
            }
        }

        function LoadDropAcc(Main, Sub, filePath, Loadid) {
            var GlMain = $('#' + Main + ' option:selected').val();
            var GlSub = $('#' + Sub + ' option:selected').val();
            if (GlSub && GlSub != "0") {
                $.post(filePath + "?GlMain=" + GlMain + "&GlSub=" + GlSub,
         function(result) {
             var streets = eval(result);
             ClearStreetsDropDown(Loadid);
             ClearStreetsDropDown('drpBranch')
             document.getElementById('txtChatAccCode').innerHTML = "";
             document.getElementById('txtGlclass').innerHTML = "";
             document.getElementById('txtGlGroup').innerHTML = "";
             PopulateGlAccount(streets, Loadid);
         });
            } else {
                ClearStreetsDropDown('drpGlAccount')
                ClearStreetsDropDown('drpBranch')
                document.getElementById('txtChatAccCode').innerHTML = "";
                document.getElementById('txtGlclass').innerHTML = "";
                document.getElementById('txtGlGroup').innerHTML = "";
            }
        }

        function LoadDropBranch(Main, Sub, Acc, filePath, Loadid) {
            var GlMain = $('#' + Main + ' option:selected').val();
            var GlSub = $('#' + Sub + ' option:selected').val();
            var GlAcc = $('#' + Acc + ' option:selected').val();
            if (GlAcc && GlAcc != "0") {
                $.post(filePath + "?GlMain=" + GlMain + "&GlSub=" + GlSub + "&GlAcc=" + GlAcc,
         function(result) {
             var streets = eval(result);
             ClearStreetsDropDown(Loadid);
             document.getElementById('txtChatAccCode').innerHTML = "";
             document.getElementById('txtGlclass').innerHTML = "";
             document.getElementById('txtGlGroup').innerHTML = "";
             PopulateGlBranch(streets, Loadid);
         });
            } else {
                ClearStreetsDropDown('drpBranch')
                document.getElementById('txtChatAccCode').innerHTML = "";
                document.getElementById('txtGlclass').innerHTML = "";
                document.getElementById('txtGlGroup').innerHTML = "";
            }
        }

        function LoadChartAccount(Main, Sub, Acc, Branch, filePath) {
            var GlMain = $('#' + Main + ' option:selected').val();
            var GlSub = $('#' + Sub + ' option:selected').val();
            var GlAcc = $('#' + Acc + ' option:selected').val();
            var Branch = $('#' + Branch + ' option:selected').val();
            if (Branch && Branch != "0") {
                $.post(filePath + "?GlMain=" + GlMain + "&GlSub=" + GlSub + "&GlAcc=" + GlAcc + "&Branch=" + Branch,
         function(result) {
             var streets = eval(result);
             PopulateChartAccount(streets);
         });
            } else {
                document.getElementById('txtChatAccCode').innerHTML = "";
                document.getElementById('txtGlclass').innerHTML = "";
                document.getElementById('txtGlGroup').innerHTML = "";
            }
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
                $('#drpGlSub').prop('selectedIndex', 1);
                LoadAcc();
            }
        }

        function PopulateGlAccount(streets, id) {
            $('#' + id).append($('<option></option>').val("").html(""));
            $(streets).each(function(i) {
                $('#' + id).append($('<option></option>').val(streets[i].GlAccountCode).html(streets[i].GlAccountName));
            });

            var Count = countProperties(streets);
            if (Count == 1) {
                $('#drpGlAccount').prop('selectedIndex', 1);
                LoadBranch();
            }
        }

        function PopulateGlBranch(streets, id) {
            $('#' + id).append($('<option></option>').val("").html(""));
            $(streets).each(function(i) {
                $('#' + id).append($('<option></option>').val(streets[i].BankCode).html(streets[i].BankName));
            });
            var DefaultBranch = document.getElementById('hddDefaultBranch').value;
            var BranchName = document.getElementById('hddBranch').value;
            if (DefaultBranch != "") {
                if (DefaultBranch.toUpperCase() == "TRUE") {
                    $('#drpBranch').val(BranchName);
                    LoadChartAccountCode();
                }
            }
        }

        function PopulateChartAccount(streets) {
            document.getElementById('txtChatAccCode').innerHTML = streets.ChartCode;
            document.getElementById('txtGlclass').innerHTML = streets.GlClassification;
            document.getElementById('txtGlGroup').innerHTML = streets.GlGroup;
        }

        function LoadMain() {
            LoadSelect(document.getElementById('AshxPath').value + "/LoadMain.ashx", 'drpGlMain');
        }

        function LoadSub() {
            LoadDropSub('drpGlMain', document.getElementById('AshxPath').value + "/LoadGlSub.ashx", 'drpGlSub');
        }

        function LoadAcc() {
            LoadDropAcc('drpGlMain', 'drpGlSub', document.getElementById('AshxPath').value + "/LoadAccount.ashx", 'drpGlAccount');
        }

        function LoadBranch() {
            LoadDropBranch('drpGlMain', 'drpGlSub', 'drpGlAccount', document.getElementById('AshxPath').value + "/LoadBranch.ashx", 'drpBranch');
        }

        function LoadChartAccountCode() {
            LoadChartAccount('drpGlMain', 'drpGlSub', 'drpGlAccount', 'drpBranch', document.getElementById('AshxPath').value + "/LoadChartAccount.ashx");
        }

        function countProperties(obj) {
            var prop;
            var propCount = 0;

            for (prop in obj) {
                propCount++;
            }
            return propCount;
        }

        function ResetPopup() {
            ClearStreetsDropDown('drpGlSub')
            ClearStreetsDropDown('drpGlAccount')
            ClearStreetsDropDown('drpBranch')
            document.getElementById('txtChatAccCode').innerHTML = "";
            document.getElementById('txtGlclass').innerHTML = "";
            document.getElementById('txtGlGroup').innerHTML = "";
            $('#drpGlMain').prop('selectedIndex', 0);
            return false;

        }
    
    </script>

</head>
<body style="vertical-align:top;horizantal-align:">
    <form id="form1" runat="server">
    <div>
        <div id="divpopup" class="divPopup" runat="server">
            <table class="modalExtenderTable">
                <tr>
                    <td colspan="2">
                        <div class="modalExtenderTitle">
                            ChartAccount Details</div>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGlMain" SkinID="LabelNormal" Text="GL main" runat="server"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <select id="drpGlMain" class="dropDownListNormalPopup" runat="server" onchange="LoadSub();">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGlSub" SkinID="LabelNormal" Text="GL Sub" runat="server"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <select id="drpGlSub" class="dropDownListNormalPopup" runat="server" onchange="LoadAcc();">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGlAccount" SkinID="LabelNormal" Text="GL Account" runat="server"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <select id="drpGlAccount" class="dropDownListNormalPopup" runat="server" onchange="LoadBranch();">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBranch" SkinID="LabelNormal" Text="Branch" runat="server"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <select id="drpBranch" class="dropDownListNormalPopup" runat="server" onchange="LoadChartAccountCode();">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGlGroup" SkinID="LabelNormal" Text="GL Group" runat="server"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:Label ID="txtGlGroup" SkinID="LabelNormalPopupContent" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGlClass" SkinID="LabelNormal" Text="GL Classfication" runat="server"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:Label ID="txtGlclass" SkinID="LabelNormalPopupContent" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblChatAccCode" SkinID="LabelNormal" Text="Chart Of Account Code"
                            runat="server"></asp:Label>
                    </td>
                    <td class="inputcontrols">
                        <asp:Label ID="txtChatAccCode" SkinID="LabelNormalPopupContent" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="modalExtenderButtons">
                            <div class="modalExtenderButtonsHolder">
                                <asp:Button ID="BtnSubmit" runat="server" OnClientClick="return closeDialog('1');"
                                    Text="Submit" SkinID="ButtonNormal" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClientClick="return ResetPopup();" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="AshxPath" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hddFilter" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hddDefaultBranch" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hddBranch" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hddChartAccount" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hddChartDesc" runat="server"></asp:HiddenField>
    </div>
    </div>
    </form>

    <script type="text/javascript" language="javascript">
        LoadMain();
    </script>

</body>
</html>
