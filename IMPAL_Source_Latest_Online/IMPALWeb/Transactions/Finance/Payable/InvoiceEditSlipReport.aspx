<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InvoiceEditSlipReport.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.InvoiceEditSlipReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";
        
        function fnValidate() {

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            if (txtFromDate.value == "") {
                alert("Date should not be null");
                txtFromDate.focus();
                return false;
            }

            var status = fnIsDate(txtFromDate.value);
            if (!status) {
                txtFromDate.value = "";
                txtFromDate.focus();
                return false;
            }

//            var branch = document.getElementById(CtrlIdPrefix + "ddlBranch");
//            if (branch.value == "" || branch.value == "0") {
//                alert("Branch should not be null");
//                branch.focus();
//                return false;
//            }

//            var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplierCode");
//            if (supplier.value == "" || supplier.value == "0") {
//                alert("Supplier Code should not be null");
//                supplier.focus();
//                return false;
//            }
        }
    </script>

    <div class="subFormTitle subFormTitleExtender300">
        INVOICE ENTRY EDIT DETAILS REPORT
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblfromdate" Text="Date" SkinID="LabelNormal" runat="server"></asp:Label><span
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
                    <asp:Label ID="Label2" Text="Branch" SkinID="LabelNormal" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch" DataTextField="BranchName"
                        DataValueField="BranchCode" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="Label1" Text="Supplier" SkinID="LabelNormal" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplierCode" runat="server" SkinID="DropDownListNormal"
                        DataValueField="SupplierCode" DataSourceID="ODS_Suppliers" DataTextField="SupplierName">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnreport" runat="server" Text="Report"
                TabIndex="3" OnClientClick="javaScript:return fnValidate();" OnClick="btnreport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crEditSlip" runat="server" ReportName="EditSlip" />
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranchesAcc"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches">
    </asp:ObjectDataSource>
</asp:Content>
