<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Worksheet.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.Worksheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <script type="text/javascript" src="../../../JavaScript/xlsx.full.min.js"></script>
    <script language="javascript" type="text/javascript">
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

            var range = XLSX.utils.decode_range(ws['!ref']);
            for (var r = range.s.r; r <= range.e.r; r++) {
                //console.log(range.s.r, range.e.r);
                for (var c = range.s.c; c <= range.e.c; c++) {
                    //console.log("---------------------");
                    //console.log(range.s.c, range.e.c);
                    //console.log("---------------------");
                    var cellName = XLSX.utils.encode_cell({ c: c, r: r });
                    //if (!cellName.startsWith('U')) {
                    //console.log(cellName);
                    ws[cellName].z = '@';
                    //}
                }
            }

            /* Add worksheet to workbook */
            XLSX.utils.book_append_sheet(wb, ws, ws_name);

            /* Write workbook and Download */
            //if (typeof console !== 'undefined') console.log(new Date());
            XLSX.writeFile(wb, filename);
            //if (typeof console !== 'undefined') console.log(new Date());
            alert('Work Sheet has been Downloaded Successfully');
        }

        function fnValidate() {
            var ddlsup = document.getElementById('<%=ddlSupplier.ClientID%>');
            var ddlrep = document.getElementById('<%=ddlReport.ClientID%>');
            var ddlItemCodes = document.getElementById('<%=ddlItemCodes.ClientID%>');
            var lstItem = document.getElementById('<%=lstItem.ClientID%>').options.length;

            if (ddlrep.value.trim() == "") {
                alert("Report Type should not be null");
                ddlrep.focus();
                return false;
            }

            if (ValidateSupplierCode(ddlsup) == false) {
                ddlsup.focus();
                return false;
            }

            if (ddlrep.value == "Direct Purchase Order" && lstItem <= 0) {
                alert("DPO Report Cannot be generated for whole Line. Please Add required Part Nos");
                ddlItemCodes.focus();
                return false;
            }
            else {
                document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
                return true;
            }
        }

        function ValidSupllier() {
            var ddlSupp = document.getElementById('<%=ddlSupplier.ClientID%>');
            var Supplier = ddlSupp.options[ddlSupp.selectedIndex].value;

            if (Supplier == "182" || Supplier == "230" || Supplier == "210" || Supplier == "300" || Supplier == "620" ||
                Supplier == "790" || Supplier == "830" || Supplier == "360") {
                ddlSupp.options[ddlSupp.selectedIndex].value = "0";
                ddlSupp.options[ddlSupp.options[ddlSupp.selectedIndex].value].selected = true;
                __doPostBack('<%=ddlSupplier.ClientID%>', "0");
                alert("Transaction Not Allowed For This Supplier");
                return false;
            }

            __doPostBack('<%=ddlSupplier.ClientID%>', ddlSupp.options[ddlSupp.selectedIndex].value);
            return true;
        }

        function fnReportType() {
            //if (document.getElementById('<%=ddlReport.ClientID%>').selectedIndex > 0) {
            //    document.getElementById('<%=btnReport.ClientID%>').style.display = "inline";
            //}
            //else {
            //    document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            //}
        }
    </script>

    <div class="reportFormTitle">
        Worksheet
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReport" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblReport" Text="Report Type" SkinID="LabelNormal" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReport" SkinID="DropDownListNormal" TabIndex="6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSupplier" Text="Supplier Code" SkinID="LabelNormal" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplier" SkinID="DropDownListNormal" AutoPostBack="true"
                                    runat="server" TabIndex="1" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label" style="display:none">
                                <asp:Label ID="lblItemCode" Text="Supp. Part#" SkinID="LabelNormal" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols" style="display:none">
                                <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="GridViewTextBox"></asp:TextBox>                                
                                <asp:DropDownList ID="ddlItemCodes" runat="server" SkinID="DropDownListNormal" TabIndex="2" DropDownStyle="DropDownList">
                                </asp:DropDownList>
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                    SkinID="GridViewButton" />
                            </td>
                            <td class="label" style="display:none">
                                <asp:Label ID="lblCustomer" SkinID="LabelNormal" runat="server" Text="Customer"></asp:Label>
                            </td>
                            <td class="inputcontrols" style="display:none">
                                <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" SkinID="DropDownListNormal" TabIndex="5">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="label">
                                <asp:Label ID="lblListBox" Text="List Box" SkinID="LabelNormal" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols" colspan="2">
                                <asp:ListBox ID="lstItem" SkinID="ListBoxNormal" runat="server" TabIndex="3" Style="width: 150px; height: 250px"></asp:ListBox>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td></td>
                            <td class="reportButtons">
                                <asp:Button runat="server" Text="Next" ID="btnNext" TabIndex="4" SkinID="ButtonNormal"
                                    OnClick="btnNext_Click" />&nbsp;
                                <asp:Button runat="server" ID="btnRemove" SkinID="ButtonNormal" Text="Remove" TabIndex="5"
                                    OnClick="btnRemove_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:HiddenField ID="hdnJSonExcelData" runat="server" />
            <div class="reportButtons">
                <asp:Button ID="btnReport" SkinID="ButtonViewReport" TabIndex="7" OnClientClick="javscript:return fnValidate();"
                    runat="server" Text="Generate Report" OnClick="btnReport_Click" />
                <asp:Button ID="btnBack" SkinID="ButtonViewReport" TabIndex="7" runat="server" OnClick="btnBack_Click" Text="Back" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
