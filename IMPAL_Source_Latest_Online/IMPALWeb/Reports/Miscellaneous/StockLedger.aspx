<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockLedger.aspx.cs" Inherits="IMPALWeb.Reports.Miscellaneous.StockLedger"
    MasterPageFile="~/Main.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtSupplier = document.getElementById('<%=ddlSupplier.ClientID%>');

            if (txtSupplier.value.trim() == "0" || txtSupplier.value.trim() == "") {
                alert("Please Select a Supplier.");
                txtSupplier.focus();
                return false;
            }

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
            var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');

            var oFromDateVal = txtFromDate.value.trim();            
            var oToDateVal = txtToDate.value.trim();            

            if (oFromDateVal == null || oFromDateVal == "") {
                alert("From Date should not be null");
                txtFromDate.focus();
                return false;
            }

            if (oToDateVal == null || oToDateVal == "") {
                alert("To Date should not be null");
                txtToDate.focus();
                return false;
            }            

            if (CheckDates(txtFromDate, txtToDate) == false)
                return false;
            else {
                var oFromDate = oFromDateVal.split("/");
                var oToDate = oToDateVal.split("/");
                var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
                var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
                if (hidFromDate != null)
                    hidFromDate.value = oFromDateFormatted;
                if (hidToDate != null)
                    hidToDate.value = oToDateFormatted;
            }
        }
    </script>

    <div class="reportFormTitle">
        Stock Ledger Report
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblSupplierName" Text="Supplier Name" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                </td>
                <td class="label">
                    <asp:Label ID="lblSupplierPartNo" SkinID="LabelNormal" runat="server" Text="Supplier Part Number"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                    <asp:DropDownList ID="ddlSupplierPartNo" runat="server" Visible="false" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" SkinID="ButtonNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80" SkinID="TextBoxNormal" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:HiddenField ID="hidFromDate" runat="server" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" Width="80" SkinID="TextBoxNormal" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:HiddenField ID="hidToDate" runat="server" />
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliersGST"
            TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                Text="Generate Report" SkinID="ButtonViewReport" TabIndex="3" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
    </div>
</asp:Content>
