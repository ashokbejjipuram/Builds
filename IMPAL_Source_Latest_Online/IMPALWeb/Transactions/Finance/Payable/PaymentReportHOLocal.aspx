<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PaymentReportHOLocal.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.PaymentReportHOLocal" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
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
            return ValidateDates(txtFromDate, txtToDate);
        }
    </script>

    <div class="subFormTitle subFormTitleExtender250">
        Payment Report - HO Local
    </div>
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
                        runat="server" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" Text="To Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" runat="server"
                        ></asp:TextBox>
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
        <div class="reportButtons">
            <asp:Button SkinID="ButtonNormal" ID="btnreport" runat="server" Text="Report"
                TabIndex="3" OnClientClick="javaScript:return fnValidate();" OnClick="btnreport_Click" />
        </div>
    </div>
     <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crHOLocal" runat="server" ReportName="HOLocal" />
    </div>
      <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliersAcc"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranchesAcc"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches">
    </asp:ObjectDataSource>
</asp:Content>
