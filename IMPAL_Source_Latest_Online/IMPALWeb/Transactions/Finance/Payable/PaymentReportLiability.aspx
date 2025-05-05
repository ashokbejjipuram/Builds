<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PaymentReportLiability.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.PaymentReportLiability" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";

        function fnValidate() {

            //            var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplierCode");
            //            if (supplier.value == "" || supplier.value == "0") {
            //                alert("Supplier Code should not be null");
            //                supplier.focus();
            //                return false;
            //            }

            //            var branch = document.getElementById(CtrlIdPrefix + "ddlBranch");
            //            if (branch.value == "" || branch.value == "0") {
            //                alert("Branch should not be null");
            //                branch.focus();
            //                return false;
            //            }

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');

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

        function fnShowHideBtns() {
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";

            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="subFormTitle subFormTitleExtender250">
        Payment Report - Liability
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="crBMSLiabilty" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplierCode" Text="Supplier" SkinID="LabelNormal" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplierCode" runat="server" SkinID="DropDownListNormal"
                                    DataValueField="SupplierCode" DataSourceID="ODS_Suppliers" DataTextField="SupplierName">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblBranch" Text="Branch" SkinID="LabelNormal" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch" DataTextField="BranchName"
                                    DataValueField="BranchCode" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblfromdate" Text="From Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="1"
                                    runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                                    TargetControlID="txtFromDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" Text="To Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal" TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport ID="crBMSLiabilty" runat="server" ReportName="PymtReportBMSDetail" OnUnload="crBMSLiabilty_Unload" />
            </div>
            <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliersAcc"
                TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranchesAcc"
                TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
